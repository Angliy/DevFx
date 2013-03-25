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
using HTB.DevFx.Core;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Config
{
	/// <summary>
	/// 整个框架的配置管理器
	/// </summary>
	/// <remarks>
	/// 在应用程序中可以这么使用：<c>Configer.Setting["配置节名"]</c><br />
	/// 建议在应用程序中不要直接使用此管理器，要获得应用程序自己的配置信息，请在框架配置文件中添加如下的节
	///		<code>
	///			&lt;framework&gt;
	///				&lt;modules&gt;
	///					......
	///					&lt;!--name表示应用程序模块的名称，linkNode表示配置节名称--&gt;
	///					&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
	///				&lt;/modules&gt;
	///			&lt;/framework&gt;
	///			......
	///			......
	///			&lt;!--下面就可以填入应用程序自己的配置信息--&gt;
	///			&lt;app&gt;
	///			&lt;/app&gt;
	///		</code>
	///	在代码中要获得<c>&lt;app&gt;&lt;/app&gt;</c>这个配置信息，请使用如下代码：
	///		<code>
	///			......
	///			Framework.GetModule("app").Setting;
	///			......
	///		</code>
	///	更具体的信息，请参看 <see cref="AppModule{T}"/>
	/// </remarks>
	public class Configer : CoreModule, IConfigManager, IFactory
	{
		#region constructor

		static Configer() {
			Framework.Init();
		}

		private Configer() {
			if(instance == null) {
				instance = this;
			}
		}

		#endregion

		#region private members

		private object lockObject = new object();
		private ConfigManagerCollection configManagerCollection = new ConfigManagerCollection();

		private IConfigManager GetConfigManager(string configFile, bool monitor) {
			IConfigManager configManager = configManagerCollection[configFile];
			lock(lockObject) {
				if(configManager == null) {
					configManager = CreateConfigManager(configFile, monitor);
					configManagerCollection.Add(configFile, configManager);
				}
			}
			return configManager;
		}

		private IConfigManager CreateConfigManager(string configFile, bool monitor) {
			DevFxConfigAttribute[] configAttributes = DevFxConfigAttribute.GetConfigAttributeFromAssembly(null);
			IConfigManager createdObject = null;
			if(configAttributes != null && configAttributes.Length > 0) {
				DevFxConfigAttribute configAttribute = configAttributes[0];
				for(int i = 1; i < configAttributes.Length; i++) {
					if(configAttributes[i].Priority > configAttribute.Priority) {
						configAttribute = configAttributes[i];
					}
				}
				Type type = configAttribute.RealType;
				if(type == null) {
					try {
						type = TypeHelper.CreateType(configAttribute.TypeName, true);
					} catch(Exception e) {
						throw new ConfigException("类型载入错误：" + e.Message, e);
					}
				}
				try {
					createdObject = (IConfigManager)TypeHelper.CreateObject(type, typeof(IConfigManager), true);
					createdObject.Init(configFile, monitor);
				} catch(Exception e) {
					throw new ConfigException(e.Message, e);
				}
			}
			return createdObject;
		}

		#endregion

		#region static members

		private static Configer instance = new Configer();
		private static IConfigManager globalConfigManager;

		private static IConfigManager GlobalConfigManager {
			get {
				if(globalConfigManager == null) {
					globalConfigManager = Current.GetConfigManager(null, false);
				}
				return globalConfigManager;
			}
		}

		/// <summary>
		/// Configer的唯一实例，提供给框架使用
		/// </summary>
		internal static Configer Current {
			get {
				if(instance == null) {
					instance = new Configer();
				}
				return instance;
			}
		}

		internal static void Reset() {
			Current.configManagerCollection.Clear();
			globalConfigManager = null;
		}

		#endregion

		#region IConfigManager members

		void IConfigManager.Init(string configFile, bool monitor) {}

		/// <summary>
		/// 重新载入相关配置信息
		/// </summary>
		void IConfigManager.Reset() {
			GlobalConfigManager.Reset();
		}

		IConfigSetting IConfigManager.Setting {
			get { return GlobalConfigManager.Setting; }
		}

		/// <summary>
		/// 获得配置节
		/// </summary>
		/// <param name="xpath">配置节的XPath，如果为<c>null</c>，则返回根配置节</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		IConfigSetting IConfigManager.GetSetting(string xpath) {
			return GlobalConfigManager.GetSetting(xpath);
		}

		#endregion

		#region override module members

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		public override void Init(IFramework framework, IConfigSetting setting) {
			if(setting != null) {
				base.Init(framework, setting);
			}
		}

		/// <summary>
		/// 获取本模块的事例（可以是单例模式也可以是多例模式）
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return Current;
		}

		#endregion

		#region IFactory members

		object IFactory.GetManager(params object[] parameters) {
			string configFile = null;
			bool monitor = true;
			if(parameters != null) {
				if(parameters.Length > 0) {
					configFile = (string)parameters[0];
				}
				if(parameters.Length > 1) {
					monitor = (bool)parameters[1];
				}
			}
			return this.GetConfigManager(configFile, monitor);
		}

		#endregion
	}
}