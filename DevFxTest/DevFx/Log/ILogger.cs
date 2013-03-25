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
	/// 日志记录器接口
	/// </summary>
	/// <remarks>
	///	日志记录器的配置：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="HTB.DevFx.Log.LogManager"&gt;
	///					&lt;loggers&gt;
	///						......
	///						&lt;logger name="记录器名" minLevel="此记录器处理的最小Level" maxLevel="此记录器处理的最大Level" type="记录器类型名" /&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public interface ILogger
	{
		/// <summary>
		/// 日志记录器名称
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 此日志记录器记录级别的最小值
		/// </summary>
		int MinLevel { get; }

		/// <summary>
		/// 此日志记录器记录级别的最大值
		/// </summary>
		int MaxLevel { get; }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// 日志记录
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志级别（决定处理方法）</param>
		/// <param name="message">日志信息</param>
		/// <returns>返回处理结果</returns>
		IAOPResult Log(object source, int level, string message);
	}
}
