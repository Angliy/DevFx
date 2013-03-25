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
using HTB.DevFx.Log;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// 异常管理接口
	/// </summary>
	/// <remarks>
	/// 配置格式：
	///		<code>
	///			&lt;exception type="HTB.DevFx.ExceptionManagement.ExceptionManager"&gt;
	///				&lt;logModule name="log" /&gt;
	///				&lt;handlers&gt;
	///					......
	///				&lt;/handlers&gt;
	///			&lt;/exception&gt;
	///		</code>
	///	依赖日志管理器的配置
	/// </remarks>
	public interface IExceptionManager
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		/// <param name="logManager">日志管理器</param>
		void Init(IConfigSetting setting, ILogManager logManager);

		/// <summary>
		/// 处理异常
		/// </summary>
		/// <param name="e">异常</param>
		void Publish(Exception e);

		/// <summary>
		/// 处理异常
		/// </summary>
		/// <param name="e">异常</param>
		/// <param name="level">异常等级（决定由哪个日志记录器记录）</param>
		void Publish(Exception e, int level);
	}
}
