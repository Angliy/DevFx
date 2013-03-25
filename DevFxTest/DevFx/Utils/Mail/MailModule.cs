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
using HTB.DevFx.ExceptionManagement;
using HTB.DevFx.Log;

namespace HTB.DevFx.Utils.Mail
{
	/// <summary>
	/// 邮件发送工具配置模块
	/// </summary>
	/// <remarks>
	/// 配置节格式：
	///		<code>
	///			&lt;framework&gt;
	///				&lt;modules&gt;
	///					......
	///					&lt;module name="mail" type="HTB.DevFx.Utils.Mail.MailModule, HTB.DevFx" configName="mail" /&gt;
	///					......
	///				&lt;/modules&gt;
	///			&lt;/framework&gt;
	///			......
	///			......
	///			&lt;mail&gt;
	///				&lt;smtpServer&gt;SMTP服务器地址&lt;/smtpServer&gt;
	///				&lt;serverPort&gt;服务器侦听端口&lt;/serverPort&gt;
	///				&lt;userName&gt;认证用户名&lt;/userName&gt;
	///				&lt;password&gt;认证用户密码&lt;/password&gt;
	///			&lt;/mail&gt;
	///		</code>
	///	使用方法和 <see cref="System.Net.Mail.SmtpClient"/> 一样
	/// </remarks>
	public sealed class MailModule : AppModule
	{
		#region IModule methods
		
		/// <summary>
		/// 模块的初始化（获取配置信息，仅为支持Framework）
		/// </summary>
		/// <param name="framework">依附的IFramework</param>
		/// <param name="setting">配置节</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			try {
				MailSender.SmtpServer = this.setting["smtpServer"].Value.Value;
				MailSender.ServerPort = this.setting["serverPort"].Value.ToInt32();
				MailSender.UserName = this.setting["userName"].Value.Value;
				MailSender.Password = this.setting["password"].Value.Value;
			} catch (Exception e) {
				Exceptor.Publish(e, LogLevel.WARN);
			}
		}

		#endregion
	}
}
