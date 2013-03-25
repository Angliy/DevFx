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
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Log;
using HTB.DevFx.Utils;

namespace HTB.DevFx.ExceptionManagement.Web
{
	/// <summary>
	/// 处理WEB异常的异常处理器
	/// </summary>
	/// <remarks>
	/// 配置格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				&lt;exception type="......"&gt;
	///					.....
	///					&lt;handlers&gt;
	///						......
	///						&lt;handler name="HttpWebExceptionHandler"
	///							exceptionType="HTB.DevFx.ExceptionManagement.Web.HttpWebException"
	///							exceptionFormatter="HTB.DevFx.ExceptionManagement.Web.HttpWebExceptionFormatter"
	///							type="HTB.DevFx.ExceptionManagement.Web.HttpWebExceptionHandler"
	///							defaultRedirect="~/frame/error.aspx?hc={0}&amp;amp;ec={1}&amp;amp;level={2}&amp;amp;msg={3}&amp;amp;url={4}" /&gt;
	///						......
	///					&lt;/handlers&gt;
	///				&lt;/exception&gt;
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	其中defaultRedirect为异常时转向的页面地址，其中参数含义为：
	///	<list type="bullet">
	///		<item><description>hc：发生异常的页面Hash Code</description></item>
	///		<item><description>ec：异常编码</description></item>
	///		<item><description>level：异常等级</description></item>
	///		<item><description>msg：异常信息</description></item>
	///		<item><description>url：发生异常的页面地址</description></item>
	///	</list>
	/// </remarks>
	public class HttpWebExceptionHandler : ExceptionHandler
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public HttpWebExceptionHandler() : base() {
		}

		private bool checkRedirectFileExists = true;
		private string defaultRedirect;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		/// <param name="logManager">日志管理器</param>
		public override void Init(IConfigSetting setting, ILogManager logManager) {
			if(!this.isInit) {
				base.Init (setting, logManager);
				if(setting.Property["defaultRedirect"] != null) {
					this.defaultRedirect = setting.Property["defaultRedirect"].Value;
					this.checkRedirectFileExists = setting.Property.TryGetPropertyValue("checkRedirectFileExists", this.checkRedirectFileExists);
				}
			}
		}

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
		public override IAOPResult Handle(Exception e, int level) {
			if(!this.isInit) {
				return new AOPResult(-1, "异常处理器没有被正确初始化");
			}
			HttpWebException ex = e as HttpWebException;
			if(ex != null) {
				HttpApplication app = ex.HttpAppInstance;
				Exception sourceException = BaseException.FindSourceException(ex);
				this.logManager.WriteLog(level, this.exceptionFormatter.GetFormatString(sourceException, app));

				app.Server.ClearError();
				app.Response.Clear();
				bool fileExists = false;
				if(this.defaultRedirect != null && this.checkRedirectFileExists) {
					string filePath = WebHelper.UrlCombine(app.Request.ApplicationPath, this.defaultRedirect, false);
					fileExists = File.Exists(app.Request.MapPath(filePath));
				}
				if(!fileExists && this.checkRedirectFileExists) {
					this.logManager.WriteLog(this.GetType(), LogLevel.WARN, "WARNING: The file defined in HttpWebExceptionHandler's defaultRedirect do not exists, please check it!");
				}
				if((fileExists || !this.checkRedirectFileExists) && !WebHelper.IsUrlEquals(this.defaultRedirect, app.Request)) {
					app.Response.Redirect(string.Format(this.defaultRedirect, sourceException.GetHashCode(), 0, level, HttpUtility.UrlEncode(HttpUtility.HtmlEncode(sourceException.Message), app.Request.ContentEncoding), HttpUtility.UrlEncode(HttpUtility.HtmlEncode(app.Request.Url.PathAndQuery), app.Request.ContentEncoding)), true);
				} else {
					string message = 
								@"<style type='text/css'>
									body, hr, a, font, div, p, table, td, th, span {{font-size: 9pt; font-family: simsun}}
								</style>
								<font color=red><h4>ERROR OCCURRED!</h4></font>
								<hr style='height:1px'>
								<b>An unkown error occurred, please try again! a log has been saved, and also sent to webmaster by email or sms.</b><br>
								<br>
								<a href=# onclick=history.back()>[BACK]</a> || <a href='mailto:R2@DevFx.NET'>[CONTACT]</a>
								<hr style='height:1px'>
								<font onclick=""document.all('DETAIL').style.display=''"" style='cursor:hand;color=red'>
									Error message: {0}
								</font>
								<div id=DETAIL style='display:none;color:red'>
									Url: {1}<br>
									Stack:<br>
									<pre>{2}</pre>
								</div>";
					app.Response.Write(String.Format(message, ex.InnerException.Message, ex.HttpAppInstance.Request.Url, ex.InnerException));
					app.CompleteRequest();
				}
			}
			return new AOPResult(0);
		}
	}
}
