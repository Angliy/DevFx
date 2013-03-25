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

namespace HTB.DevFx.Config
{
	/// <summary>
	/// 配置管理器接口
	/// </summary>
	public interface IConfigManager
	{
		/// <summary>
		/// 此管理器所管理的配置节
		/// </summary>
		IConfigSetting Setting { get; }

		/// <summary>
		/// 获得配置节
		/// </summary>
		/// <param name="xpath">配置节的XPath，如果为<c>null</c>，则返回根配置节</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		IConfigSetting GetSetting(string xpath);

		/// <summary>
		/// 初始化，提供给框架调用，进行管理器的初始化工作，比如载入配置文件等等
		/// </summary>
		/// <param name="configFile">配置文件信息</param>
		/// <param name="monitor">是否要监视此配置的变化</param>
		/// <remarks>
		///	参数<paramref name="monitor" />表示，监视配置文件变化，以更新配置信息
		/// </remarks>
		void Init(string configFile, bool monitor);

		/// <summary>
		/// 重新载入相关配置信息
		/// </summary>
		void Reset();
	}
}
