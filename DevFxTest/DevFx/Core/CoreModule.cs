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

using HTB.DevFx.Config;
using HTB.DevFx.ExceptionManagement;

namespace HTB.DevFx.Core
{
	/// <summary>
	/// 实现IModule的核心模块，建议所有的模块都从此类继承
	/// </summary>
	public abstract class CoreModule : IModule
	{
		/// <summary>
		/// 调用框架进行初始化
		/// </summary>
		static CoreModule() {
			Framework.Init();
		}

		/// <summary>
		/// 模块名称（在配置文件中配置）
		/// </summary>
		protected string name;
		/// <summary>
		/// IFramework的实例
		/// </summary>
		protected IFramework framework;
		/// <summary>
		/// 模块的配置节
		/// </summary>
		protected IConfigSetting moduleSetting;
		/// <summary>
		/// 模块实际指向的配置节
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// 是否已初始化
		/// </summary>
		protected bool initialized;

		/// <summary>
		/// 模块是否被初始化
		/// </summary>
		/// <param name="throwOnError">如果没有初始化，是否抛出异常</param>
		/// <returns>true/false</returns>
		/// <remarks>
		/// 如果没有被初始化，则抛出异常 <see cref="BaseException" />
		/// </remarks>
		protected virtual bool Initialized(bool throwOnError) {
			if(!this.initialized && throwOnError) {
				throw new BaseException("模块没有被正确初始化");
			}
			return this.initialized;
		}

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		protected virtual void OnInit(IFramework framework, IConfigSetting setting) {
			if (setting == null) {
				throw new BaseException("模块初始化失败：没有提供配置节");
			}
			this.framework = framework;
			this.moduleSetting = setting;
			this.name = setting.Property["name"].Value;
			string linkNode = setting.Property.TryGetPropertyValue("linkNode");
			if (string.IsNullOrEmpty(linkNode)) {
				if (setting.Children > 0) {
					this.setting = setting[0];
				}
			} else {
				this.setting = setting.GetChildSetting(linkNode);
			}
		}

		#region IModule Members

		/// <summary>
		/// 模块名
		/// </summary>
		public virtual string Name {
			get {
				this.Initialized(true);
				return this.name;
			}
		}

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		public virtual void Init(IFramework framework, IConfigSetting setting) {
			if(!this.initialized) {
				this.OnInit(framework, setting);
				this.initialized = true;
			}
		}

		/// <summary>
		/// 本模块的配置节
		/// </summary>
		public virtual IConfigSetting Setting {
			get {
				this.Initialized(true);
				return this.setting;
			}
		}

		/// <summary>
		/// 获取本模块的实例（可以是单例模式也可以是多例模式），抽象方法
		/// </summary>
		/// <returns><see cref="IModule"/></returns>
		public abstract IModule GetInstance();

		#endregion

		#region IModule Members

		string IModule.Name {
			get { return this.Name; }
		}

		void IModule.Init(IFramework framework, IConfigSetting setting) {
			this.Init(framework, setting);
		}

		IConfigSetting IModule.Setting {
			get { return this.Setting; }
		}

		IModule IModule.GetInstance() {
			return this.GetInstance();
		}

		#endregion
	}
}
