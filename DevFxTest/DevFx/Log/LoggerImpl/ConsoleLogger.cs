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

namespace HTB.DevFx.Log.LoggerImpl
{
	/// <summary>
	/// 以控制台输出方式的日志记录器
	/// </summary>
	/// <remarks>
	/// 提供给控制台应用程序使用<br />
	/// 配置格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="HTB.DevFx.Log.LogManager"&gt;
	///					&lt;loggers&gt;
	///						......
	///						&lt;logger name="consoleLogger" minLevel="此记录器处理的最小Level" maxLevel="此记录器处理的最大Level" type="HTB.DevFx.Log.LoggerImpl.ConsoleLogger" /&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public class ConsoleLogger : Logger
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ConsoleLogger() : base() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">对应的配置节</param>
		public ConsoleLogger(IConfigSetting setting) : base(setting) {
		}

		/// <summary>
		/// 日志记录
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志级别（决定处理方法）</param>
		/// <param name="message">日志信息</param>
		/// <returns>返回处理结果</returns>
		public override IAOPResult Log(object source, int level, string message) {
			IAOPResult result = base.Log(source, level, message);
			if(result.IsFailed) {
				return result;
			}
			source = this.GetExactSourc(source);
			string msg = string.Format("[{0}]Source={1}, Level={2}, Message={3}{4}--------------------------", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), source, level, message, Environment.NewLine);
			if(Environment.UserInteractive) {
				Console.WriteLine(msg);
			}
			return new AOPResult(0);
		}
	}
}
