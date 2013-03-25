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
using System.Text;
using System.Web;

namespace HTB.DevFx.ExceptionManagement.Web
{
	/// <summary>
	/// 收集WEB异常的异常收集器
	/// </summary>
	/// <remarks>
	/// 主要收集了异常发生时的如下信息：
	/// <list type="bullet">
	///		<item><description>异常信息</description></item>
	///		<item><description>HTTP请求方式 请求的页面</description></item>
	///		<item><description>请求页面的Hash Code（提供给对照异常使用）</description></item>
	///		<item><description>请求的HTTP头</description></item>
	///		<item><description>异常堆栈</description></item>
	///		<item><description>请求的HTTP BODY</description></item>
	///		<item><description>客户端IP</description></item>
	///		<item><description>服务端IP</description></item>
	///		<item><description>当前登录的用户名</description></item>
	/// </list>
	/// </remarks>
	public class HttpWebExceptionFormatter : IExceptionFormatter
	{
		private const string formatMessage =
@"{0}
{1} {2}
hash code:{3}
request heads:
{4}
error data:
{5}
request data:
{6}
client ip:{7}
server name/ip:{8}
auth username:{9}
";

		#region IExceptionFormatter Members

		string IExceptionFormatter.GetFormatString(Exception e, object attachObject) {
			HttpApplication app = attachObject as HttpApplication;
			if(app == null) {
				return e.ToString();
			}
			StringBuilder ret = new StringBuilder();
			StreamReader streamReader = new StreamReader(app.Request.InputStream, app.Request.ContentEncoding);
			ret.AppendFormat(formatMessage, e.Message, app.Request.HttpMethod, app.Request.Url.ToString(), e.GetHashCode(), app.Request.Headers, e.ToString(), streamReader.ReadToEnd(), app.Request.UserHostAddress, app.Server.MachineName, app.User.Identity.Name);
			return ret.ToString();
		}

		#endregion
	}
}
