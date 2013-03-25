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
	/// 整个框架的日志管理器
	/// </summary>
	/// <remarks>
	/// 与框架的配合配置格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;!--日志管理模块--&gt;
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
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			logManager = this.CreateLogManager(this.setting);
		}

		/// <summary>
		/// 获取本模块的事例（可以是单例模式也可以是多例模式）
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return Current;
		}

		#endregion

		#region static members

		private static Loggor current;

		/// <summary>
		/// 日志管理器的唯一实例（单件模式）
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
		/// 调用配置管理器写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		public static void WriteLog(int level, string message) {
			Current.logManager.WriteLog(level, message);
		}

		/// <summary>
		/// 调用配置管理器写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		public static void WriteLog(int level, object message) {
			Current.logManager.WriteLog(level, message);
		}

		/// <summary>
		/// 日志记录
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志级别（决定处理方法）</param>
		/// <param name="message">日志信息</param>
		/// <returns>返回处理结果</returns>
		public static void WriteLog(object source, int level, string message) {
			Current.logManager.WriteLog(source, level, message);
		}

		/// <summary>
		/// 日志记录
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志级别（决定处理方法）</param>
		/// <param name="message">日志信息</param>
		/// <returns>返回处理结果</returns>
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
