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
	/// ʹ��XMLʵ�����ù������ӿ�
	/// </summary>
	/// <remarks>
	/// �����ָ�������ļ����������˳��Ѱ�������ļ���
	/// <list type="number">
	///		<item>
	///			<description>
	///				������Ŀ¼Ѱ����Ϊhtb.devfx.config�������ļ���
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
	///				���û�ҵ����������<see cref="HTB.DevFx.Config.DevFxConfigAttribute"/>ָ���������ļ�
	///			</description>
	///		</item>
	///		<item>
	///			<description>
	///				���û�ҵ����������web/app.config�е����½�ָ��λ�õ������ļ���
	///				<code>
	///					&lt;appSettings&gt;
	///						&lt;add key="htb.devfx.config" value="..\configFiles\htb.devfx.config" /&gt;
	///					&lt;/appSettings&gt;
	///				</code>
	///				����keyֵָ��Ϊ��htb.devfx.config����valueΪ�����ļ����Ӧ�ó����Ŀ¼����Ե�ַ
	///			</description>
	///		</item>
	///		<item>
	///			<description>
	///				�����û�ҵ������׳��쳣
	///			</description>
	///		</item>
	/// </list>
	/// </remarks>
	internal class ConfigManager : Config.ConfigManager
	{
		#region constructor

		/// <summary>
		/// ���캯��
		/// </summary>
		public ConfigManager() : this(null, false) {}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configFile">ָ�������ļ���ַ</param>
		/// <param name="monitor">�Ƿ���������ļ�</param>
		public ConfigManager(string configFile, bool monitor) : base(configFile, monitor) {}

		#endregion

		#region IConfigSetting Members

		/// <summary>
		/// �˹����������������
		/// </summary>
		public override IConfigSetting Setting {
			get {
				if(!this.initialized) {
					throw new ConfigException("���ù�����û�б���ȷ��ʼ��");
				}
				return this.setting;
			}
		}

		/// <summary>
		/// ��ʼ�����ṩ����ܵ��ã����й������ĳ�ʼ���������������������ļ��ȵ�
		/// </summary>
		/// <param name="configFile">�����ļ���Ϣ</param>
		/// <param name="monitor">�Ƿ�Ҫ���Ӵ����õı仯</param>
		/// <remarks>
		///	����<paramref name="monitor" />��ʾ�����������ļ��仯���Ը���������Ϣ
		/// </remarks>
		public override void Init(string configFile, bool monitor) {
			if(!this.initialized) {
				base.Init(configFile, monitor);
				this.InitData(configFile);
			}
		}

		/// <summary>
		/// �����������������Ϣ
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
		/// ��ʼ�����ý�����
		/// </summary>
		/// <param name="configFile">�����ļ���ַ</param>
		private void InitData(string configFile) {
			if(configFile == null) {
				configFile = this.FindConfigFile();
			}
			configFile = WebHelper.GetFullPath(configFile);
			this.configFile = configFile;
			this.InitSetting();
		}

		/// <summary>
		/// ������ʼ�����ý�
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
				throw new ConfigException("�����ļ�û�ҵ�");
			}
			configFileAttributes = DevFxConfigFileAttribute.GetConfigFileAttributeFromAssembly(null);
			if(configFileAttributes != null && configFileAttributes.Length > 0) {
				SortedList<int, DevFxConfigFileAttribute> sortedConfigFiles = new SortedList<int, DevFxConfigFileAttribute>();
				string indexList = string.Empty;
				for(int i = 0; i < configFileAttributes.Length; i++) {
					if(configFileAttributes[i].ConfigIndex != 0) {
						indexList += configFileAttributes[i].ConfigIndex + ", ";
						if(sortedConfigFiles.ContainsKey(configFileAttributes[i].ConfigIndex)) {
							throw new ConfigException(string.Format("������ʹ��DevFxConfigFileAttribute�������������µ�˳��� {0}�����޸�˳��Ŷ�������±������", indexList));
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
		/// ���������ļ���ַ
		/// </summary>
		/// <returns>�����ļ���ַ</returns>
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