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

using HTB.DevFx.Config;
using HTB.DevFx.Core;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ������ܵ���־������
	/// </summary>
	/// <remarks>
	/// ���ܵ�������ø�ʽ��
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;!--��־����ģ��--&gt;
	///						&lt;module name="log" type="HTB.DevFx.Log.Loggor" linkNode="../../../log" /&gt;
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
	///			Loggor.WriteLog(LogLevel.INFO, "this is a log message!");
	///			......
	///		</code>
	/// </example>
	public class Loggor : CoreModule, ILogManager, IFactory
	{
		#region constructor

		static Loggor() {
			Framework.Init();
		}

		private Loggor() {
			if(current == null) {
				current = this;
			}
		}

		#endregion

		#region private members

		private ILogManager logManager;

		private ILogManager CreateLogManager(IConfigSetting setting) {
			ILogManager logManager = (ILogManager)setting.Property["type"].ToObject(typeof(ILogManager), true);
			logManager.Init(setting);
			return logManager;
		}

		#endregion

		#region override members

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			logManager = this.CreateLogManager(this.setting);
		}

		/// <summary>
		/// ��ȡ��ģ��������������ǵ���ģʽҲ�����Ƕ���ģʽ��
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return Current;
		}

		#endregion

		#region static members

		private static Loggor current;

		/// <summary>
		/// ��־��������Ψһʵ��������ģʽ��
		/// </summary>
		public static Loggor Current {
			get {
				if(current == null) {
					current = new Loggor();
				}
				return current;
			}
		}

		/// <summary>
		/// �������ù�����д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		public static void WriteLog(int level, string message) {
			Current.logManager.WriteLog(level, message);
		}

		/// <summary>
		/// �������ù�����д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		public static void WriteLog(int level, object message) {
			Current.logManager.WriteLog(level, message);
		}

		/// <summary>
		/// ��־��¼
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־���𣨾�����������</param>
		/// <param name="message">��־��Ϣ</param>
		/// <returns>���ش�����</returns>
		public static void WriteLog(object source, int level, string message) {
			Current.logManager.WriteLog(source, level, message);
		}

		/// <summary>
		/// ��־��¼
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־���𣨾�����������</param>
		/// <param name="message">��־��Ϣ</param>
		/// <returns>���ش�����</returns>
		public static void WriteLog(object source, int level, object message) {
			Current.logManager.WriteLog(source, level, message);
		}

		#endregion

		#region ILogManager Members

		void ILogManager.Init(IConfigSetting setting) {
		}

		void ILogManager.WriteLog(int level, string message) {
			this.Initialized(true);
			logManager.WriteLog(level, message);
		}

		void ILogManager.WriteLog(int level, object message) {
			this.Initialized(true);
			logManager.WriteLog(level, message);
		}

		void ILogManager.WriteLog(object source, int level, string message) {
			this.Initialized(true);
			logManager.WriteLog(source, level, message);
		}

		void ILogManager.WriteLog(object source, int level, object message) {
			this.Initialized(true);
			logManager.WriteLog(source, level, message);
		}

		#endregion

		#region IFactory Members

		object IFactory.GetManager(params object[] parameters) {
			return this.logManager;
		}

		#endregion
	}
}
