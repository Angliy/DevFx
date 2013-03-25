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
using HTB.DevFx.Log;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// 异常处理器接口
	/// </summary>
	/// <example>
	///	配置节格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				&lt;exception type="......"&gt;
	///					.....
	///					&lt;handlers&gt;
	///						......
	///						&lt;handler name="异常处理器名" exceptionType="所处理的异常类型" exceptionFormatter="异常收集器类型" type="异常处理器类型" /&gt;
	///						......
	///					&lt;/handlers&gt;
	///				&lt;/exception&gt;
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </example>
	public interface IExceptionHandle
	{
		/// <summary>
		/// 初始化异常处理器（由异常管理器调用）
		/// </summary>
		/// <param name="setting">对应的配置节</param>
		/// <param name="logManager">日志记录器</param>
		void Init(IConfigSetting setting, ILogManager logManager);

		/// <summary>
		/// 异常处理器名
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// 此异常处理器处理的异常类型
		/// </summary>
		Type ExceptionType { get; }

		/// <summary>
		/// 异常信息收集格式化
		/// </summary>
		IExceptionFormatter ExceptionFormatter { get; set; }

		/// <summary>
		/// 进行异常处理（由异常管理器调用）
		/// </summary>
		/// <param name="e">异常</param>
		/// <param name="level">异常等级（传递给日志记录器处理）</param>
		/// <returns>处理结果，将影响下面的处理器</returns>
		/// <remarks>
		/// 异常管理器将根据返回的结果进行下一步的处理，约定：<br />
		///		返回的结果中，ResultNo值：
		///		<list type="bullet">
		///			<item><description>
		///				小于0：表示处理异常，管理器将立即退出异常处理
		///			</description></item>
		///			<item><description>
		///				0：处理正常
		///			</description></item>
		///			<item><description>
		///				1：已处理，需要下一个异常处理器进一步处理，<br />
		///				此时ResultAttachObject为返回的异常（可能与传入的异常是不一致的）
		///			</description></item>
		///			<item><description>
		///				2：已处理，需要重新轮询异常处理器进行处理<br />
		///					此时ResultAttachObject为返回的异常（可能与传入的异常是不一致的）<br />
		///					此时异常管理器将重新进行异常处理
		///			</description></item>
		///		</list>
		/// </remarks>
		IAOPResult Handle(Exception e, int level);
	}
}
