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

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志等级（系统预置）
	/// </summary>
	/// <remarks>
	/// 日志等级一般是由应用程序自己定义的，本类只是提供一般的等级分类，应用程序也可以不遵循此分类
	/// </remarks>
	public static class LogLevel
	{
		/// <summary>
		/// 等级最大值
		/// </summary>
		public const int MAX = Int32.MaxValue;

		/// <summary>
		/// 紧急事件的日志等级（120000）
		/// </summary>
		public const int EMERGENCY = 120000;

		/// <summary>
		/// 致命事件的日志等级（110000）
		/// </summary>
		public const int FATAL = 110000;

		/// <summary>
		///  警报事件的日志等级（100000）
		/// </summary>
		public const int ALERT = 100000;

		/// <summary>
		/// 错误事件的日志等级（70000）
		/// </summary>
		public const int ERROR = 70000;

		/// <summary>
		/// 警告事件的日志等级（60000）
		/// </summary>
		public const int WARN = 60000;

		/// <summary>
		/// 通知事件的日志等级（50000）
		/// </summary>
		public const int NOTICE = 50000;

		/// <summary>
		/// 信息事件的日志等级（40000）
		/// </summary>
		public const int INFO = 40000;

		/// <summary>
		/// 调试事件的日志等级（30000）
		/// </summary>
		public const int DEBUG = 30000;

		/// <summary>
		/// 跟踪事件的日志等级（20000）
		/// </summary>
		public const int TRACE = 20000;

		/// <summary>
		/// 不表示任何等级（-1）
		/// </summary>
		public const int NA = -1;

		/// <summary>
		/// 最小等级
		/// </summary>
		public const int MIN = Int32.MinValue;

		/// <summary>
		/// 从名称获取等级
		/// </summary>
		/// <param name="levelName">日志等级名称</param>
		/// <returns>等级代码</returns>
		public static int Parse(string levelName) {
			if(levelName == null) {
				throw new LogException("级别名称为Null");
			}
			int level = Parse(levelName, LogLevel.NA);
			if(level == LogLevel.NA) {
				throw new LogException("没有发现指定名称的级别");
			}
			return level;
		}

		/// <summary>
		/// 从名称获取等级
		/// </summary>
		/// <param name="levelName">日志等级名称</param>
		/// <param name="defaultValue">如果没找到，缺省的等级代码</param>
		/// <returns>等级代码</returns>
		public static int Parse(string levelName, int defaultValue) {
			if(levelName == null) {
				return defaultValue;
			}
			levelName = levelName.ToUpper();
			switch(levelName) {
				case "MAX":
					return LogLevel.MAX;
				case "EMERGENCY":
					return LogLevel.EMERGENCY;
				case "FATAL":
					return LogLevel.FATAL;
				case "ALERT":
					return LogLevel.ALERT;
				case "ERROR":
					return LogLevel.ERROR;
				case "WARN":
					return LogLevel.WARN;
				case "NOTICE":
					return LogLevel.NOTICE;
				case "INFO":
					return LogLevel.INFO;
				case "DEBUG":
					return LogLevel.DEBUG;
				case "TRACE":
					return LogLevel.TRACE;
				case "MIN":
					return LogLevel.MIN;
				default:
					return defaultValue;
			}
		}

		/// <summary>
		/// 尝试从名称获取等级
		/// </summary>
		/// <param name="levelName">日志等级名称</param>
		/// <param name="levelValue">传入的等级代码</param>
		/// <returns>是否成功获取等级代码</returns>
		public static bool TryParse(string levelName, ref int levelValue) {
			int level = Parse(levelName, LogLevel.NA);
			if(level == LogLevel.NA) {
				return false;
			} else {
				levelValue = level;
				return true;
			}
		}
	}
}
