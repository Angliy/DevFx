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
using System.IO;
using System.Web;
using HTB.DevFx.Web.HttpModules;

namespace HTB.DevFx.ExceptionManagement.Web
{
	/// <summary>
	/// 捕捉WEB项目异常的<see cref="IHttpModule"/>
	/// </summary>
	/// <remarks>
	/// 在web.config中添加如下的配置：
	///		<code>
	///			&lt;system.web&gt;
	///				&lt;httpModules&gt;
	///					......
	///					&lt;add name="ExceptionHttpModule" type="HTB.DevFx.ExceptionManagement.Web.ExceptionHttpModule, HTB.DevFx" /&gt;
	///					......
	///				&lt;/httpModules&gt;
	///				......
	///			&lt;/system.web&gt;
	///		</code>
	/// </remarks>
	public class ExceptionHttpModule : HttpModuleWrap
	{
		/// <summary>
		/// WEB应用程序异常捕捉
		/// </summary>
		private void WebOnError(object sender, EventArgs e) {
			HttpApplication httpApp = (HttpApplication)sender;
			Exception ex = httpApp.Server.GetLastError();
			Exception ex0 = BaseException.FindSourceException(ex);
			if(ex0 is FileNotFoundException) {
				return;
			}
			string message = null;
			if(ex != null) {
				message = ex.Message;
			}
			Exceptor.Publish(new HttpWebException(message, ex, httpApp));
		}

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="context"><see cref="HttpApplication"/> 实例</param>
		public override void Init(HttpApplication context) {
			base.Init(context);
			Framework.Init();
			context.Error += this.WebOnError;
		}
	}
}
