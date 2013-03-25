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
	/// ����WEB�쳣���쳣������
	/// </summary>
	/// <remarks>
	/// ���ø�ʽ��
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
	///	����defaultRedirectΪ�쳣ʱת���ҳ���ַ�����в�������Ϊ��
	///	<list type="bullet">
	///		<item><description>hc�������쳣��ҳ��Hash Code</description></item>
	///		<item><description>ec���쳣����</description></item>
	///		<item><description>level���쳣�ȼ�</description></item>
	///		<item><description>msg���쳣��Ϣ</description></item>
	///		<item><description>url�������쳣��ҳ���ַ</description></item>
	///	</list>
	/// </remarks>
	public class HttpWebExceptionHandler : ExceptionHandler
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public HttpWebExceptionHandler() : base() {
		}

		private bool checkRedirectFileExists = true;
		private string defaultRedirect;

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		/// <param name="logManager">��־������</param>
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
		/// �����쳣�������쳣���������ã�
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="level">�쳣�ȼ������ݸ���־��¼������</param>
		/// <returns>����������Ӱ������Ĵ�����</returns>
		/// <remarks>
		/// �쳣�����������ݷ��صĽ��������һ���Ĵ���Լ����<br />
		///		���صĽ���У�ResultNoֵ��
		///		<list type="bullet">
		///			<item><description>
		///				С��0����ʾ�����쳣���������������˳��쳣����
		///			</description></item>
		///			<item><description>
		///				0����������
		///			</description></item>
		///			<item><description>
		///				1���Ѵ�����Ҫ��һ���쳣��������һ������<br />
		///				��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�
		///			</description></item>
		///			<item><description>
		///				2���Ѵ�����Ҫ������ѯ�쳣���������д���<br />
		///					��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�<br />
		///					��ʱ�쳣�����������½����쳣����
		///			</description></item>
		///		</list>
		/// </remarks>
		public override IAOPResult Handle(Exception e, int level) {
			if(!this.isInit) {
				return new AOPResult(-1, "�쳣������û�б���ȷ��ʼ��");
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
