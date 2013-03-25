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
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using HTB.DevFx.Config;
using HTB.DevFx.Utils;
using ConfigManager=HTB.DevFx.Config.XmlConfigImpl.ConfigManager;
[assembly : DevFxConfig(RealType = typeof(ConfigManager), Priority = 1000)]
[assembly : DevFxConfigFile("res://HTB.DevFx.Config.htb.devfx.config", typeof(ConfigManager), 0)]

namespace HTB.DevFx.Config.XmlConfigImpl
{
	/// <summary>
	/// 使用XML实现配置管理器接口
	/// </summary>
	/// <remarks>
	/// 如果不指定配置文件，则从下面顺序寻找配置文件：
	/// <list type="number">
	///		<item>
	///			<description>
	///				在以下目录寻找名为htb.devfx.config的配置文件：
	///				<code>
	///					"./",
	///					"./../",
	///					"./../../",
	///					"./../../../",
	///					"./../Configuration/",
	///					"./../../Configuration/",
	///					"./../../../Configuration/",
	///					Environment.CurrentDirectory + "/",
	///					AppDomain.CurrentDomain.SetupInformation.ApplicationBase
	///				</code>
	///			</description>
	///		</item>
	///		<item>
	///			<description>
	///				如果没找到，则查找由<see cref="HTB.DevFx.Config.DevFxConfigAttribute"/>指定的配置文件
	///			</description>
	///		</item>
	///		<item>
	///			<description>
	///				如果没找到，则查找由web/app.config中的如下节指定位置的配置文件：
	///				<code>
	///					&lt;appSettings&gt;
	///						&lt;add key="htb.devfx.config" value="..\configFiles\htb.devfx.config" /&gt;
	///					&lt;/appSettings&gt;
	///				</code>
	///				其中key值指定为“htb.devfx.config”，value为配置文件相对应用程序根目录的相对地址
	///			</description>
	///		</item>
	///		<item>
	///			<description>
	///				如果还没找到，则抛出异常
	///			</description>
	///		</item>
	/// </list>
	/// </remarks>
	internal class ConfigManager : Config.ConfigManager
	{
		#region constructor

		/// <summary>
		/// 构造函数
		/// </summary>
		public ConfigManager() : this(null, false) {}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configFile">指定配置文件地址</param>
		/// <param name="monitor">是否监视配置文件</param>
		public ConfigManager(string configFile, bool monitor) : base(configFile, monitor) {}

		#endregion

		#region IConfigSetting Members

		/// <summary>
		/// 此管理器所管理的配置
		/// </summary>
		public override IConfigSetting Setting {
			get {
				if(!this.initialized) {
					throw new ConfigException("配置管理器没有被正确初始化");
				}
				return this.setting;
			}
		}

		/// <summary>
		/// 初始化，提供给框架调用，进行管理器的初始化工作，比如载入配置文件等等
		/// </summary>
		/// <param name="configFile">配置文件信息</param>
		/// <param name="monitor">是否要监视此配置的变化</param>
		/// <remarks>
		///	参数<paramref name="monitor" />表示，监视配置文件变化，以更新配置信息
		/// </remarks>
		public override void Init(string configFile, bool monitor) {
			if(!this.initialized) {
				base.Init(configFile, monitor);
				this.InitData(configFile);
			}
		}

		/// <summary>
		/// 重新载入相关配置信息
		/// </summary>
		public override void Reset() {
			if(this.initialized) {
				this.InitSetting();
			}
		}

		#endregion

		#region private members

		private IConfigSetting setting = null;

		internal const string DEFAULT_CONFIG_FILE = "htb.devfx.config";
		internal const string DEFAULT_CONFIG_FILE_PATTERN = "htb.devfx.*.config";

		/// <summary>
		/// 初始化配置节数据
		/// </summary>
		/// <param name="configFile">配置文件地址</param>
		private void InitData(string configFile) {
			if(configFile == null) {
				configFile = this.FindConfigFile();
			}
			configFile = WebHelper.GetFullPath(configFile);
			this.configFile = configFile;
			this.InitSetting();
		}

		/// <summary>
		/// 分析初始化配置节
		/// </summary>
		private void InitSetting() {
			bool init = false;
			if(this.configFile != null) {
				try {
					this.setting = ConfigHelper.CreateFromXmlFile(configFile);
					init = true;
				} catch(ConfigException) {}
			}
			if(!init) {
				XmlNode configNode = (XmlNode)ConfigurationManager.GetSection("htb.devfx");
				if(configNode != null) {
					try {
						this.setting = ConfigHelper.CreateFromXmlNode(configNode);
						init = true;
					} catch(ConfigException) {}
				}
			}
			DevFxConfigFileAttribute[] configFileAttributes;
			if(!init) {
				configFileAttributes = DevFxConfigFileAttribute.GetConfigFileAttributeFromAssembly(null);
				if(configFileAttributes != null && configFileAttributes.Length > 0) {
					for(int i = 0; i < configFileAttributes.Length; i++) {
						if(configFileAttributes[i].ConfigIndex == 0) {
							string configFile = configFileAttributes[i].ConfigFile;
							Type fileInType = configFileAttributes[i].GetFileInType();
							this.setting = ConfigHelper.CreateFromXmlSource(configFile, fileInType);
							init = true;
							break;
						}
					}
				}
			}
			if(!init) {
				throw new ConfigException("配置文件没找到");
			}
			configFileAttributes = DevFxConfigFileAttribute.GetConfigFileAttributeFromAssembly(null);
			if(configFileAttributes != null && configFileAttributes.Length > 0) {
				SortedList<int, DevFxConfigFileAttribute> sortedConfigFiles = new SortedList<int, DevFxConfigFileAttribute>();
				string indexList = string.Empty;
				for(int i = 0; i < configFileAttributes.Length; i++) {
					if(configFileAttributes[i].ConfigIndex != 0) {
						indexList += configFileAttributes[i].ConfigIndex + ", ";
						if(sortedConfigFiles.ContainsKey(configFileAttributes[i].ConfigIndex)) {
							throw new ConfigException(string.Format("在所有使用DevFxConfigFileAttribute定义中已有如下的顺序号 {0}，请修改顺序号定义后重新编译代码", indexList));
						}
						sortedConfigFiles.Add(configFileAttributes[i].ConfigIndex, configFileAttributes[i]);
					}
				}
				for(int i = 0; i < sortedConfigFiles.Count; i++) {
					DevFxConfigFileAttribute configFile = sortedConfigFiles.Values[i];
					ConfigSetting configSetting = (ConfigSetting)ConfigHelper.CreateFromXmlSource(configFile.ConfigFile, configFile.GetFileInType());
					this.setting.Merge(configSetting);
				}
			}
			string[] files = ConfigHelper.SearchConfigFileWithPattern(DEFAULT_CONFIG_FILE_PATTERN, null);
			foreach(string file in files) {
				ConfigSetting configSetting = (ConfigSetting)ConfigHelper.CreateFromXmlFile(file);
				this.setting.Merge(configSetting);
			}
		}

		/// <summary>
		/// 查找配置文件地址
		/// </summary>
		/// <returns>配置文件地址</returns>
		private string FindConfigFile() {
			string fileName = ConfigHelper.SearchConfigFile(DEFAULT_CONFIG_FILE, null);
			if(fileName != null) {
				return fileName;
			}

			DevFxConfigAttribute[] configAttributes = DevFxConfigAttribute.GetConfigAttributeFromAssembly(null);
			if(configAttributes != null && configAttributes.Length > 0) {
				for(int i = 0; i < configAttributes.Length; i++) {
					fileName = ConfigHelper.SearchConfigFile(configAttributes[i].ConfigFile, null);
					if(fileName != null) {
						return fileName;
					}
				}
			}

			fileName = ConfigurationManager.AppSettings[DEFAULT_CONFIG_FILE];
			fileName = ConfigHelper.SearchConfigFile(fileName, null);

			return fileName;
		}

		#endregion
	}
}