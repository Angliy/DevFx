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
using System.Xml;

namespace HTB.DevFx.Config
{
	/// <summary>
	/// 配置在<c>web.config</c>中，.NET需要的配置节处理器
	/// </summary>
	/// <remarks>
	/// 这个提供给配置内容存放的一种方式，就存放在与应用程序配置文件一起，例如<c>web.config，app.config</c><br />
	/// 按下面的方式添加：
	///		<code>
	///			&lt;configuration&gt;
	///				&lt;configSections&gt;
	///					&lt;section name="htb.devfx" type="HTB.DevFx.Config.ConfigSectionHandler, HTB.DevFx" /&gt;
	///				&lt;/configSections&gt;
	///				......
	///				
	///				&lt;htb.devfx&gt;
	///					&lt;framework&gt;
	///					......
	///					&lt;/framework&gt;
	///					......
	///				&lt;/htb.devfx&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public class ConfigSectionHandler : IConfigurationSectionHandler
	{
		#region private members

		private bool isInit = false;

		#endregion

		#region IConfigurationSectionHandler Members

		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section) {
			if(!this.isInit) {
				this.isInit = true;
				return section;
			} else {
				throw new ConfigException("配置节重复");
			}
		}

		#endregion
	}
}