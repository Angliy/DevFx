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

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// 异常信息收集格式化接口，用于收集对异常时的所处的环境信息以及对这些信息进行格式化
	/// </summary>
	/// <remarks>
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
	/// </remarks>
	public interface IExceptionFormatter
	{
		/// <summary>
		/// 获取格式化的信息
		/// </summary>
		/// <param name="e">发生的异常</param>
		/// <param name="attachObject">附加对象</param>
		/// <returns>格式化后的字符串</returns>
		string GetFormatString(Exception e, object attachObject);
	}
}
