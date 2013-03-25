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
using HTB.DevFx.Utils;

namespace HTB.DevFx.Core
{
	/// <summary>
	/// 框架核心类
	/// </summary>
	/// <remarks>
	/// 对各模块进行初始化工作<br />
	/// 配置文件格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;module name="config" type="HTB.DevFx.Config.ConfigFactory" configName="config" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///				......
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public class CoreFramework : IFramework
	{
		/// <summary>
		/// 保护构造函数，不允许实例化
		/// </summary>
		protected CoreFramework() {
			this.FrameworkInit();
		}

		private CollectionBase<IModule> modules;
		private bool initialized;

		/// <summary>
		/// 初始化框架设置（读取配置等）
		/// </summary>
		protected virtual void FrameworkInit() {
			if(!this.initialized) {
				this.OnInit();
				this.initialized = true;
			}
		}

		/// <summary>
		/// 初始化框架设置（读取配置等）
		/// </summary>
		protected virtual void OnInit() {
			IConfigManager configManager = Configer.Current;
			IConfigSetting setting = configManager.GetSetting("htb.devfx");

			this.modules = new CollectionBase<IModule>();
			IConfigSetting[] moduleSettings = setting["framework"]["modules"].GetChildSettings();
			for (int i = 0; i < moduleSettings.Length; i++) {
				string name = moduleSettings[i].Property["name"].Value;
				if (this.modules.Contains(name)) {
					throw new BaseException("框架模块名重复");
				}
				IModule module = (IModule)moduleSettings[i].Property["type"].ToObject(typeof(IModule), true);
				module = module.GetInstance();
				module.Init(this, moduleSettings[i]);
				this.modules.Add(name, module);
			}
		}

		#region IFramework Members

		IModule IFramework.GetModule(string moduleName) {
			return this.modules[moduleName];
		}

		IModule[] IFramework.Modules {
			get {
				return this.modules.CopyToArray();
			}
		}

		#endregion
	}
}
