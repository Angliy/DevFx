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
	/// ������ܵ����ù�����
	/// </summary>
	/// <remarks>
	/// ��Ӧ�ó����п�����ôʹ�ã�<c>Configer.Setting["���ý���"]</c><br />
	/// ������Ӧ�ó����в�Ҫֱ��ʹ�ô˹�������Ҫ���Ӧ�ó����Լ���������Ϣ�����ڿ�������ļ���������µĽ�
	///		<code>
	///			&lt;framework&gt;
	///				&lt;modules&gt;
	///					......
	///					&lt;!--name��ʾӦ�ó���ģ������ƣ�linkNode��ʾ���ý�����--&gt;
	///					&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
	///				&lt;/modules&gt;
	///			&lt;/framework&gt;
	///			......
	///			......
	///			&lt;!--����Ϳ�������Ӧ�ó����Լ���������Ϣ--&gt;
	///			&lt;app&gt;
	///			&lt;/app&gt;
	///		</code>
	///	�ڴ�����Ҫ���<c>&lt;app&gt;&lt;/app&gt;</c>���������Ϣ����ʹ�����´��룺
	///		<code>
	///			......
	///			Framework.GetModule("app").Setting;
	///			......
	///		</code>
	///	���������Ϣ����ο� <see cref="AppModule{T}"/>
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
						throw new ConfigException("�����������" + e.Message, e);
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
		/// Configer��Ψһʵ�����ṩ�����ʹ��
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
		/// �����������������Ϣ
		/// </summary>
		void IConfigManager.Reset() {
			GlobalConfigManager.Reset();
		}

		IConfigSetting IConfigManager.Setting {
			get { return GlobalConfigManager.Setting; }
		}

		/// <summary>
		/// ������ý�
		/// </summary>
		/// <param name="xpath">���ýڵ�XPath�����Ϊ<c>null</c>���򷵻ظ����ý�</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		IConfigSetting IConfigManager.GetSetting(string xpath) {
			return GlobalConfigManager.GetSetting(xpath);
		}

		#endregion

		#region override module members

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		public override void Init(IFramework framework, IConfigSetting setting) {
			if(setting != null) {
				base.Init(framework, setting);
			}
		}

		/// <summary>
		/// ��ȡ��ģ��������������ǵ���ģʽҲ�����Ƕ���ģʽ��
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