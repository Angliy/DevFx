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

using System.Configuration;
using HTB.DevFx.Config.DotNetConfig;

namespace HTB.DevFx.Utils.Mail.Config
{
	/// <summary>
	/// 邮件服务器配置信息，参看<seealso cref="SectionHandler"/>
	/// </summary>
	public class MailSetting : BaseConfigurationElement
	{
		/// <summary>
		/// 服务器地址（IP或域名）
		/// </summary>
		[ConfigurationProperty("server", IsRequired = true)]
		public string Server {
			get { return (string)this["server"]; }
		}

		/// <summary>
		/// 服务的侦听端口
		/// </summary>
		[ConfigurationProperty("port", DefaultValue = 25)]
		public int Port {
			get { return (int)this["port"]; }
		}

		/// <summary>
		/// 认证用户名
		/// </summary>
		[ConfigurationProperty("userName")]
		public string UserName {
			get { return (string)this["userName"]; }
		}

		/// <summary>
		/// 认证密码
		/// </summary>
		[ConfigurationProperty("password")]
		public string Password {
			get { return (string)this["password"]; }
		}

		internal static MailSetting Current {
			get {
				if (SectionHandler.Current != null) {
					return SectionHandler.Current.MailSetting;
				}
				return null;
			}
		}
	}
}
