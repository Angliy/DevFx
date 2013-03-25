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

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志管理器接口
	/// </summary>
	/// <remarks>
	///	日志管理器的配置：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="日志管理器类型"&gt;
	///					&lt;loggers&gt;&lt;!--这里配置日志记录器--&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public interface ILogManager
	{
		/// <summary>
		/// 初始化，由框架调用
		/// </summary>
		/// <param name="setting">日志管理器的配置节</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		void WriteLog(int level, string message);

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		/// <remarks>
		/// 日志来源将从系统堆栈中获取
		/// </remarks>
		void WriteLog(int level, object message);

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		void WriteLog(object source, int level, string message);

		/// <summary>
		/// 写日志
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志等级，<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">日志消息</param>
		void WriteLog(object source, int level, object message);
	}
}
