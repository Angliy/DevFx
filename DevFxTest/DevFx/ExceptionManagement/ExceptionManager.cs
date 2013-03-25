/******************************************************************************
	Copyright 2005-2007 R2@DevFx.NET 
	DevFx.NET is free software; you can redistribute it and/or modify
	it under the terms of the Lesser GNU General Public License as published by
	the Free Software Foundation; either version 2 of the License, or
	(at your option) any later version.

	DevFx.NET is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	Lesser GNU General Public License for more details.

	You should have received a copy of the Lesser GNU General Public License
	along with this program; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
/*******************************************************************************/

using System;
using System.Web;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Log;
using HTB.DevFx.Utils;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// �쳣�������ӿ�ʵ��
	/// </summary>
	/// <remarks>���Ҫ�滻Ϊ�Լ��Ĺ�����������ӱ���̳�</remarks>
	public class ExceptionManager : IExceptionManager
	{
		#region constructor
		
		/// <summary>
		/// ���캯��
		/// </summary>
		public ExceptionManager() {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">���ý�</param>
		/// <param name="logManager">��־������</param>
		public ExceptionManager(IConfigSetting setting, ILogManager logManager) {
			this.Init(setting, logManager);
		}

		#endregion

		#region protected members

		/// <summary>
		/// ���ý�
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// ��־������
		/// </summary>
		protected ILogManager logManager;
		/// <summary>
		/// �쳣����������
		/// </summary>
		protected CollectionBase<IExceptionHandle> handlers;
		/// <summary>
		/// �Ƿ��ʼ��
		/// </summary>
		protected bool isInit;

		#endregion

		#region IExceptionManager Members

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		/// <param name="logManager">��־������</param>
		public virtual void Init(IConfigSetting setting, ILogManager logManager) {
			if(this.isInit) {
				return;
			}
			this.setting = setting;
			this.logManager = logManager;
			this.handlers = new CollectionBase<IExceptionHandle>();
			IConfigSetting[] settings = setting["handlers"].GetChildSettings();
			for(int i = 0; i < settings.Length; i++) {
				string handlerName = settings[i].Property["name"].Value;
				if(handlerName == null) {
					throw new BaseException("�쳣��������ΪNull");
				}
				if(this.handlers.Contains(handlerName)) {
					throw new BaseException("�쳣���������ظ���" + handlerName);
				}
				IExceptionHandle handler = (IExceptionHandle)settings[i].Property["type"].ToObject(typeof(IExceptionHandle), true);
				handler.Init(settings[i], logManager);
				this.handlers.Add(handlerName, handler);
			}
			if(HttpContext.Current == null) {
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppOnError);
			}
			this.isInit = true;
		}

		/// <summary>
		/// �����쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		public virtual void Publish(Exception e) {
			this.Publish(e, LogLevel.ERROR);
		}

		/// <summary>
		/// �����쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="level">�쳣�ȼ����������ĸ���־��¼����¼��</param>
		public virtual void Publish(Exception e, int level) {
			if(!this.isInit) {
				throw new BaseException("�쳣������û�б���ȷ��ʼ��");
			}
			if(e == null) {
				return;
			}

			IAOPResult result = null;
			for(int i = 0; i < this.handlers.Count; i++) {
				IExceptionHandle handler = this.handlers[i];
				if(handler.ExceptionType.IsAssignableFrom(e.GetType())) {
					result = handler.Handle(e, level);
					if(result.ResultNo <= 0) {
						break;
					} else if(result.ResultNo == 1) {
						if(result.ResultAttachObject != null) {
							e = (Exception)result.ResultAttachObject;
						}
					} else if(result.ResultNo == 2) {
						if(result.ResultAttachObject != null) {
							e = (Exception)result.ResultAttachObject;
						}
						this.Publish(e, level);
						break;
					}
				}
			}
		}

		#endregion

		#region internal static members
		
		/// <summary>
		/// ����Ӧ�ó������׽
		/// </summary>
		internal static void AppOnError(object sender, UnhandledExceptionEventArgs e) {
			Exceptor.Publish((Exception)e.ExceptionObject);
		}

		#endregion
	}
}
