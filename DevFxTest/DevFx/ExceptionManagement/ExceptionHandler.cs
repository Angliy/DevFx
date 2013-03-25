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
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Log;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// �쳣�������ӿ�ʵ�֣�����Ӧ�ó����Զ���Ĵ��������ӱ���̳�
	/// </summary>
	public class ExceptionHandler : IExceptionHandle
	{
		#region constructor

		/// <summary>
		/// ���캯��
		/// </summary>
		public ExceptionHandler() {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">��Ӧ�����ý�</param>
		/// <param name="logManager">��־��¼��</param>
		public ExceptionHandler(IConfigSetting setting, ILogManager logManager) {
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
		/// ��������
		/// </summary>
		protected string handlerName;
		/// <summary>
		/// ������쳣����
		/// </summary>
		protected Type exceptionType;
		/// <summary>
		/// �쳣�ռ���
		/// </summary>
		protected IExceptionFormatter exceptionFormatter;
		/// <summary>
		/// �Ƿ��ʼ��
		/// </summary>
		protected bool isInit;

		#endregion

		#region IExceptionHandle Members

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
			this.handlerName = setting.Property["name"].Value;
			this.exceptionType = setting.Property["exceptionType"].ToType();
			this.ExceptionFormatter = (IExceptionFormatter)setting.Property["exceptionFormatter"].ToObject(typeof(IExceptionFormatter), true);
			this.isInit = true;
		}

		/// <summary>
		/// �쳣��������
		/// </summary>
		public virtual string Name {
			get { return this.handlerName; }
		}

		/// <summary>
		/// ���쳣������������쳣����
		/// </summary>
		public virtual Type ExceptionType {
			get {
				return this.exceptionType;
			}
		}

		/// <summary>
		/// �쳣��Ϣ�ռ���ʽ��
		/// </summary>
		public IExceptionFormatter ExceptionFormatter {
			get { return this.exceptionFormatter; }
			set { this.exceptionFormatter = value; }
		}

		/// <summary>
		/// �����쳣�������쳣���������ã�
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="level">�쳣�ȼ������ݸ���־��¼������</param>
		/// <returns>����������Ӱ������Ĵ�����</returns>
		/// <remarks>
		/// �쳣�����������ݷ��صĽ��������һ���Ĵ���Լ����<br />
		///		���صĽ���У�ResultNoֵ��
		///		<list type="bullet">
		///			<item><description>
		///				С��0����ʾ�����쳣���������������˳��쳣����
		///			</description></item>
		///			<item><description>
		///				0����������
		///			</description></item>
		///			<item><description>
		///				1���Ѵ�����Ҫ��һ���쳣��������һ������<br />
		///				��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�
		///			</description></item>
		///			<item><description>
		///				2���Ѵ�����Ҫ������ѯ�쳣���������д���<br />
		///					��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�<br />
		///					��ʱ�쳣�����������½����쳣����
		///			</description></item>
		///		</list>
		/// </remarks>
		public virtual IAOPResult Handle(Exception e, int level) {
			if(!this.isInit) {
				return new AOPResult(-1, "�쳣������û�б���ȷ��ʼ��", e, null);
			}
			this.logManager.WriteLog(level, this.exceptionFormatter.GetFormatString(e, null));
			return new AOPResult(0);
		}

		#endregion
	}
}
