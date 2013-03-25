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
using System.Web;

namespace HTB.DevFx.ExceptionManagement.Web
{
	/// <summary>
	/// WEB项目的异常
	/// </summary>
	/// <remarks>
	/// 在WEB项目中，能发现的异常都会包装成此类的实例，此类异常将由<see cref="HTB.DevFx.ExceptionManagement.Web.HttpWebExceptionHandler"/>处理
	/// </remarks>
	[Serializable]
	public class HttpWebException : BaseException
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="innerException">内部异常</param>
		/// <param name="httpApp">HttpApplication实例</param>
		public HttpWebException(Exception innerException, HttpApplication httpApp) : this(null, innerException, httpApp) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="httpApp">HttpApplication实例</param>
		public HttpWebException(HttpApplication httpApp) : this(null, httpApp) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		/// <param name="httpApp">HttpApplication实例</param>
		public HttpWebException(string message, Exception innerException, HttpApplication httpApp) : this(0, message, innerException, httpApp) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编码</param>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		/// <param name="httpApp">HttpApplication实例</param>
		public HttpWebException(int errorNo, string message, Exception innerException, HttpApplication httpApp) : base(errorNo, message, innerException) {
			this.httpApp = httpApp;
		}

		private HttpApplication httpApp;

		/// <summary>
		/// HttpApplication实例
		/// </summary>
		public HttpApplication HttpAppInstance {
			get { return this.httpApp; }
		}
	}
}
