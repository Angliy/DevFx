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
	/// 异常模块异常，框架的基础异常类，所有的异常请从本类派生
	/// </summary>
	[Serializable]
	public class BaseException : ApplicationException
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public BaseException() : this(0, null, null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public BaseException(string message, Exception innerException) : this(0, message, innerException) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		public BaseException(string message) : this(0, message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		public BaseException(int errorNo, string message) : this(errorNo, message, null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public BaseException(int errorNo, string message, Exception innerException) : base(message, innerException) {
			this.errorNo = errorNo;
		}

		/// <summary>
		/// 异常编号
		/// </summary>
		protected int errorNo;

		/// <summary>
		/// 异常编号
		/// </summary>
		public int ErrorNo {
			get { return this.errorNo; }
		}

		/// <summary>
		/// 查找原始的异常
		/// </summary>
		/// <param name="e">异常</param>
		/// <returns>原始的异常</returns>
		public static Exception FindSourceException(Exception e) {
			Exception e1 = e;
			while(e1 != null) {
				e = e1;
				e1 = e1.InnerException;
			}
			return e;
		}

		/// <summary>
		/// 从异常树种查找指定类型的异常
		/// </summary>
		/// <param name="e">异常</param>
		/// <param name="expectedExceptionType">期待的异常类型</param>
		/// <returns>所要求的异常，如果找不到，返回null</returns>
		public static Exception FindSourceException(Exception e, Type expectedExceptionType) {
			while(e != null) {
				if(e.GetType() == expectedExceptionType) {
					return e;
				}
				e = e.InnerException;
			}
			return null;
		}
	}
}
