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
using HTB.DevFx.ExceptionManagement;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 缓存模块异常
	/// </summary>
	/// <remarks>
	/// 在缓存模块里面，能发现的异常都会包装成此类的实例
	/// </remarks>
	[Serializable]
	public class CacheException : BaseException
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public CacheException() : base() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public CacheException(string message, Exception innerException) : base(message, innerException) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		public CacheException(string message) : base(message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		public CacheException(int errorNo, string message) : base(errorNo, message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public CacheException(int errorNo, string message, Exception innerException) : base(errorNo, message, innerException) {
		}
	}
}
