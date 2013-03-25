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
	/// �ʼ����͹�������ģ��
	/// </summary>
	/// <remarks>
	/// ���ýڸ�ʽ��
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
	///				&lt;smtpServer&gt;SMTP��������ַ&lt;/smtpServer&gt;
	///				&lt;serverPort&gt;�����������˿�&lt;/serverPort&gt;
	///				&lt;userName&gt;��֤�û���&lt;/userName&gt;
	///				&lt;password&gt;��֤�û�����&lt;/password&gt;
	///			&lt;/mail&gt;
	///		</code>
	///	ʹ�÷����� <see cref="System.Net.Mail.SmtpClient"/> һ��
	/// </remarks>
	public sealed class MailModule : AppModule
	{
		#region IModule methods
		
		/// <summary>
		/// ģ��ĳ�ʼ������ȡ������Ϣ����Ϊ֧��Framework��
		/// </summary>
		/// <param name="framework">������IFramework</param>
		/// <param name="setting">���ý�</param>
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
