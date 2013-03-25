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

using System.Diagnostics;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志管理器接口实现
	/// </summary>
	/// <remarks>如果要替换为自己的管理器，建议从本类继承</remarks>
	public class LogManager : ILogManager
	{
		#region constructor
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public LogManager() {
		}
		
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">配置节</param>
		public LogManager(IConfigSetting setting) {
			this.Init(setting);
		}

		#endregion

		#region protected members

		/// <summary>
		/// 配置节
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// 日志记录器集合
		/// </summary>
		protected CollectionBase<ILogger> loggers;
		/// <summary>
		/// 是否初始化
		/// </summary>
		protected bool isInit;

		#endregion

		#region overrde members

		/// <summary>
		/// 初始化，由框架调用
		/// </summary>
		/// <param name="setting">日志管理器的配置节</param>
		public virtual void Init(IConfigSetting setting) {
			if(this.isInit) {
				return;
			}

			this.setting = setting;
			this.loggers = new CollectionBase<ILogger>();
			IConfigSetting[] logSettings = setting["loggers"].GetChildSettings();
			for(int i = 0; i < logSettings.Length; i++) {
				string loggerName = logSettings[i].Property["name"].Value;
				if(loggerName == null) {
					throw new LogException("日志记录器名为Null");
				}
				if(this.loggers.Contains(loggerName)) {
					throw new LogException("日志记录器名重复：" + loggerName);
				}
				ILogger logger = (ILogger)logSettings[i].Property["type"].ToObject(typeof(ILogger), true);
				logger.Init(logSettings[i]);
				this.loggers.Add(loggerName, logger);
			}
			this.isInit = true;
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		public virtual void WriteLog(int level, string message) {
			StackTrace t = new StackTrace();
			StackFrame f = t.GetFrame(t.FrameCount - 1);
			this.WriteLog(f.GetMethod(), level, message);
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		public virtual void WriteLog(int level, object message) {
			this.WriteLog(level, message.ToString());
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		public virtual void WriteLog(object source, int level, string message) {
			if(!this.isInit) {
				throw new LogException("日志管理器没有被正确初始化");
			}

			for(int i = 0; i < this.loggers.Count; i++) {
				ILogger logger = this.loggers[i];
				if(level >= logger.MinLevel && level <= logger.MaxLevel) {
					IAOPResult result = logger.Log(source, level, message);
					if(result.IsFailed) {
						break;
					}
				}
			}
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		public virtual void WriteLog(object source, int level, object message) {
			this.WriteLog(source, level, message.ToString());
		}

		#endregion
	}
}
