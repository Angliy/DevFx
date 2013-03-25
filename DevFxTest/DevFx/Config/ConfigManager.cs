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
	/// 配置管理器的抽象实现
	/// </summary>
	public abstract class ConfigManager : IConfigManager
	{
		#region constructor

		/// <summary>
		/// 构造函数
		/// </summary>
		protected ConfigManager() {
		}
		
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configFile">配置文件信息</param>
		/// <param name="monitor">是否要监视配置文件</param>
		protected ConfigManager(string configFile, bool monitor) {
			this.Init(configFile, monitor);
		}

		#endregion

		#region protected fields

		/// <summary>
		/// 配置文件信息
		/// </summary>
		protected string configFile;
		/// <summary>
		/// 是否要监视配置文件
		/// </summary>
		protected bool monitor;
		/// <summary>
		/// 管理器是否已初始化
		/// </summary>
		protected bool initialized;

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="configFile">配置文件信息</param>
		/// <param name="monitor">是否要监视此配置的变化</param>
		/// <remarks>
		///	参数<paramref name="monitor" />表示，监视配置文件变化，以更新配置信息
		/// </remarks>
		protected virtual void OnInit(string configFile, bool monitor) {
			this.configFile = configFile;
			this.monitor = monitor;
		}

		#endregion

		#region IConfigManager Members

		/// <summary>
		/// 此管理器所管理的配置
		/// </summary>
		public abstract IConfigSetting Setting { get; }

		/// <summary>
		/// 初始化，提供给框架调用，进行管理器的初始化工作，比如载入配置文件等等
		/// </summary>
		/// <param name="configFile">配置文件信息</param>
		/// <param name="monitor">是否要监视此配置的变化</param>
		/// <remarks>
		///	参数<paramref name="monitor" />表示，监视配置文件变化，以更新配置信息
		/// </remarks>
		public virtual void Init(string configFile, bool monitor) {
			if(!this.initialized) {
				this.OnInit(configFile, monitor);
				this.initialized = true;
			}
		}

		/// <summary>
		/// 获得配置节
		/// </summary>
		/// <param name="xpath">配置节的XPath，如果为<c>null</c>，则返回根配置节</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		public virtual IConfigSetting GetSetting(string xpath) {
			return this.Setting.GetChildSetting(xpath);
		}

		/// <summary>
		/// 重新载入相关配置信息
		/// </summary>
		public abstract void Reset();

		#endregion
	}
}
