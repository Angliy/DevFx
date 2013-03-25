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
using System.Collections.Generic;
using System.Timers;
using HTB.DevFx.Core;
using HTB.DevFx.Utils;
using LogSetting = HTB.DevFx.Log.Config.SectionHandler;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志的帮助类
	/// </summary>
	public class LoggorHelper : IBaseLogger, IDisposable
	{
		/// <summary>
		/// 日志实例（单件）
		/// </summary>
		protected static LoggorHelper logger = new LoggorHelper();

		/// <summary>
		/// 构造方法
		/// </summary>
		protected LoggorHelper() {
			LogSetting logSetting = LogSetting.Current;
			if(logSetting != null) {
				this.logFile = logSetting.LogFile;
				this.logPath = WebHelper.GetFullPath(logSetting.LogPath + "\\");

				this.logEvent += new EventHandlerDelegate<LogEventArgs>(DefaultLogEventHandler);
			}
			this.timer = new Timer(100);
			this.timer.Elapsed += new ElapsedEventHandler(this.TimerOnElapsed);
			this.timer.Start();
		}

		/// <summary>
		/// 时间到了后的操作（会引发写入日志操作）
		/// </summary>
		/// <param name="sender">Object</param>
		/// <param name="e">ElapsedEventArgs</param>
		protected virtual void TimerOnElapsed(object sender, ElapsedEventArgs e) {
			this.timer.Stop();

			while(this.queue.Count > 0) {
				LogEventArgs ex = queue.Dequeue();
				this.OnLogEvent(ex);
			}

			this.timer.Start();
		}

		/// <summary>
		/// 日志队列
		/// </summary>
		protected Queue<LogEventArgs> queue = new Queue<LogEventArgs>(1024);
		/// <summary>
		/// 日志事件
		/// </summary>
		protected event EventHandlerDelegate<LogEventArgs> logEvent;
		/// <summary>
		/// 日志定时器
		/// </summary>
		protected Timer timer;
		/// <summary>
		/// 日志保存路径
		/// </summary>
		protected string logPath;
		/// <summary>
		/// 日志文件
		/// </summary>
		protected string logFile;

		/// <summary>
		/// 日志写入触发方法
		/// </summary>
		/// <param name="e">LogEventArgs</param>
		protected virtual void OnLogEvent(LogEventArgs e) {
			if(this.logEvent != null) {
				try {
					this.logEvent(this, e);
				} catch { }
			}
		}

		#region static members

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="message">日志消息</param>
		public static void WriteLog(string message) {
			WriteLog((object)null, LogLevel.INFO, message);
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="sender">调用者</param>
		/// <param name="message">日志消息</param>
		public static void WriteLog(object sender, string message) {
			WriteLog(sender, LogLevel.INFO, message);
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="logFormat">日志格式</param>
		/// <param name="parameters">格式化参数</param>
		public static void WriteLog(string logFormat, params object[] parameters) {
			WriteLog(string.Format(logFormat, parameters));
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="sender">调用者</param>
		/// <param name="logFormat">日志格式</param>
		/// <param name="parameters">格式化参数</param>
		public static void WriteLog(object sender, string logFormat, params object[] parameters) {
			WriteLog(sender, LogLevel.INFO, string.Format(logFormat, parameters));
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="sender">调用者</param>
		/// <param name="level">日志等级，参见<see cref="LogLevel"/></param>
		/// <param name="logFormat">日志格式</param>
		/// <param name="parameters">格式化参数</param>
		public static void WriteLog(object sender, int level, string logFormat, params object[] parameters) {
			WriteLog(sender, level, string.Format(logFormat, parameters));
		}

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="sender">调用者</param>
		/// <param name="level">日志等级，参见<see cref="LogLevel"/></param>
		/// <param name="message">日志消息</param>
		public static void WriteLog(object sender, int level, string message) {
			(logger as IBaseLogger).WriteLog(sender, level, message);
		}

		/// <summary>
		/// 写入日志的事件
		/// </summary>
		public static event EventHandlerDelegate<LogEventArgs> LogEvent {
			add { logger.logEvent += value; }
			remove { logger.logEvent -= value; }
		}

		/// <summary>
		/// 缺省的日志写入处理
		/// </summary>
		/// <param name="sender">调用者</param>
		/// <param name="e">LogEventArgs</param>
		public static void DefaultLogEventHandler(object sender, LogEventArgs e) {
			string log = string.Format("[{0}][{1}][{2}]\r\n{3}\r\n------------------\r\n", e.LogTime, e.Sender, e.Level, e.Message);
			LogHelper.WriteLog(logger.logPath, logger.logFile, log);
		}

		#endregion static members

		#region IBaseLogger Members

		void IBaseLogger.WriteLog(object sender, int level, string message) {
			LogEventArgs e = new LogEventArgs(sender, level, DateTime.Now, message);
			this.queue.Enqueue(e);
		}

		#endregion

		#region IDisposable Members

		void IDisposable.Dispose() {}

		#endregion
	}
}