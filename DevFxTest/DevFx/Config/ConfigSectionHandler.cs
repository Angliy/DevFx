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
	/// ������<c>web.config</c>�У�.NET��Ҫ�����ýڴ�����
	/// </summary>
	/// <remarks>
	/// ����ṩ���������ݴ�ŵ�һ�ַ�ʽ���ʹ������Ӧ�ó��������ļ�һ������<c>web.config��app.config</c><br />
	/// ������ķ�ʽ��ӣ�
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
				throw new ConfigException("���ý��ظ�");
			}
		}

		#endregion
	}
}