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

namespace HTB.DevFx.Config.DotNetConfig
{
	/// <summary>
	/// 基础的配置元素集合（抽象），继承自 <seealso cref="ConfigurationElementCollection"/>
	/// </summary>
	public abstract class BaseConfigurationElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// 获得元素的键
		/// </summary>
		/// <param name="element">配置元素</param>
		/// <returns>键</returns>
		protected override object GetElementKey(ConfigurationElement element) {
			return element.GetHashCode();
		}
	}
}