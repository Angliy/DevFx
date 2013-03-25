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
	/// ������ܵ��쳣������
	/// </summary>
	/// <remarks>
	/// ���ܵ�������ø�ʽ��
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;!--�쳣����ģ��--&gt;
	///						&lt;module name="exception" type="HTB.DevFx.ExceptionManagement.Exceptor" configName="exception" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///					
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	/// <example>
	///		<code>
	///			......
	///			try {
	///				......
	///			} catch(Exception e) {
	///				Exceptor.Publish(e);
	///				throw e;
	///			} finally {
	///				......
	///			}
	///			......
	///		</code>
	/// </example>
	public class Exceptor : CoreModule, IExceptionManager, IFactory
	{
		#region constructor

		static Exceptor() {
			Framework.Init();
		}
		
		private Exceptor() {
			if(instance == null) {
				instance = this;
			}
		}

		#endregion

		#region private members
		
		private object lockObject = new object();
		private IExceptionManager exceptionManager;


		private IExceptionManager GetExceptionManager() {
			lock(lockObject) {
				if(exceptionManager == null) {
					IFactory logFactory = (IFactory)this.framework.GetModule(this.setting["logModule"].Property["name"].Value);
					ILogManager logManager = (ILogManager)logFactory.GetManager();
					exceptionManager = CreateExceptionManager(this.setting, logManager);
				}
			}
			return exceptionManager;
		}

		private IExceptionManager CreateExceptionManager(IConfigSetting setting, ILogManager logManager) {
			IExceptionManager exceptionManager = (IExceptionManager)setting.Property["type"].ToObject(typeof(IExceptionManager), true);
			exceptionManager.Init(setting, logManager);
			return exceptionManager;
		}

		#endregion

		#region static members

		private static Exceptor instance;

		/// <summary>
		/// �쳣��������Ψһ��ʵ��������ģʽ��
		/// </summary>
		public static Exceptor Instance {
			get {
				if(instance == null) {
					instance = new Exceptor();
				}
				return instance;
			}
		}

		/// <summary>
		/// �����쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		public static void Publish(Exception e) {
			Instance.exceptionManager.Publish(e);
		}

		/// <summary>
		/// �����쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="level">�쳣�ȼ����������ĸ���־��¼����¼��</param>
		public static void Publish(Exception e, int level) {
			Instance.exceptionManager.Publish(e, level);
		}

		#endregion

		#region IExceptionManager members

		void IExceptionManager.Init(IConfigSetting setting, ILogManager logManager) {
		}

		void IExceptionManager.Publish(Exception e) {
		}

		void IExceptionManager.Publish(Exception e, int level) {
		}

		#endregion

		#region override module members

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			exceptionManager = this.GetExceptionManager();
		}

		/// <summary>
		/// ��ȡ��ģ��������������ǵ���ģʽҲ�����Ƕ���ģʽ��
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return Instance;
		}

		#endregion

		#region IFactory members

		object IFactory.GetManager(params object[] parameters) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
