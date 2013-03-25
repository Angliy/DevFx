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

namespace HTB.DevFx.Core
{
	/// <summary>
	/// 应用程序模块（泛型）
	/// </summary>
	/// <remarks>
	/// 一般的应用系统，可以把此类作为配置来源<br />
	/// 例如：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///				......
	///				......
	///				&lt;app&gt;&lt;/app&gt;
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	在代码中要获得<c>&lt;app&gt;&lt;/app&gt;</c>这个配置节信息，请使用如下代码：
	///		<code>
	///			......
	///			IConfigSetting mySetting = Framework.GetModules("app").Setting;
	///			......
	///		</code>
	///	或者应用系统自己继续新的类来获取，这种方式比较灵活，推荐使用这种方式获取应用系统的配置节<br />
	///	例如：
	///		<code>
	///			//使用单例模式
	///			public class MyAppModule : AppModule&lt;MyAppModule&gt;
	///			{
	///				......
	///				
	///				//重写此方法以获取配置信息，注意这里传进来的setting是模块本身的配置节
	///				public override void Init(IFramework framework, IConfigSetting setting) {
	///					if(!this.initialized) {
	///						base.Init(framework, setting);
	///						IConfigSetting mySetting = this.setting;//获得自己的配置信息
	///						//在这里进行您的初始化
	///						......
	///					}
	///				}
	///				
	///				......
	///			}
	///			
	///			//在你的代码中，可以这样获得配置节信息：
	///			IConfigSetting mySetting = MyAppModule.Current.Setting;
	///			......
	///			......
	///		</code>
	///	当然，也可以实现多例模式（默认为单例模式），但请注意，<c>Init</c>方法只有一次被框架调用的机会
	/// </remarks>
	public class AppModule<T> : AppModule where T : AppModule<T>
	{
		/// <summary>
		/// 保护构造函数
		/// </summary>
		protected AppModule() {
			if(instance == null) {
				instance = (T)this;
			}
		}

		/// <summary>
		/// 获取本模块的实例（可以是单例模式也可以是多例模式，由实现者决定）
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return instance;
		}

		/// <summary>
		/// 此模块的静态实例
		/// </summary>
		protected static T instance;

		/// <summary>
		/// 获取此模块的当前实例（如果有的话）
		/// </summary>
		public static T Current {
			get { return instance; }
		}
	}

	/// <summary>
	/// 应用程序模块，请参见泛型版本的说明<see cref="AppModule{T}"/>
	/// </summary>
	public class AppModule : CoreModule
	{
		/// <summary>
		/// 保护构造函数
		/// </summary>
		protected AppModule() {
		}

		/// <summary>
		/// 获取本模块的实例（可以是单例模式也可以是多例模式，由实现者决定）
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return this;
		}
	}
}
