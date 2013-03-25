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
using HTB.DevFx.Core;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志写入事件参数类
	/// </summary>
	[Serializable]
	public class LogEventArgs : BaseEventArgs
	{
		private int level;
		private string message;
		private DateTime logTime;
		
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="sender">日志发送者</param>
		/// <param name="level">日志等级，参见<see cref="LogLevel"/></param>
		/// <param name="logTime">日志发生时间</param>
		/// <param name="message">日志消息</param>
		public LogEventArgs(object sender, int level, DateTime logTime, string message) : base(sender) {
			this.level = level;
			this.logTime = logTime;
			this.message = message;
		}

		/// <summary>
		/// 日志等级，参见<see cref="LogLevel"/>
		/// </summary>
		public int Level {
			get { return this.level; }
		}

		/// <summary>
		/// 日志发生时间
		/// </summary>
		public DateTime LogTime {
			get { return this.logTime; }
		}

		/// <summary>
		/// 日志消息
		/// </summary>
		public string Message {
			get { return this.message; }
		}
	}
}
