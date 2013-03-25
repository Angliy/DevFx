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
using HTB.DevFx.Cache;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.ExceptionManagement;
using HTB.DevFx.Log;

namespace HTB.DevFx
{
	/// <summary>
	/// 统一开发框架的统一入口类
	/// </summary>
	/// <remarks>
	/// 在使用框架前，建议调用本类的 <see cref="Framework.Init()" /> 静态方法，以让框架进行前期初始化工作<br />
	/// 配置文件格式请参见另附的文件：htb.devfx.config
	/// </remarks>
	public sealed class Framework : CoreFramework
	{
		static Framework() {
			Init(true);
		}

		/// <summary>
		/// 私有构造函数，防止外部实例化
		/// </summary>
		private Framework() {}

		private static readonly object initLockObject = new object();
		private static bool initializing;
		private static Framework framework;

		private static void Init(bool initializingBySelf) {
			if(framework == null && !initializing) {
				lock(initLockObject) {
					if(framework == null && !initializing) {
						try {
							initializing = true;
							framework = new Framework();
						} catch(Exception e) {
							LoggorHelper.WriteLog(typeof(Framework), LogLevel.FATAL, e.ToString());
							try {
								Config.Configer.Reset();
							} catch {}

							if(!initializingBySelf) {
								throw;
							}
						} finally {
							initializing = false;
						}
					}
				}
			}
		}

		/// <summary>
		/// 框架的前期初始化
		/// </summary>
		/// <remarks>
		/// 建议在使用框架前的代码中调用请此方法，如果是在WEB项目中，也可加入HttpModule，此HttpModule自动调用本方法<br />
		/// HttpModule在web.config内的配置格式如下：
		///		<code>
		///			......
		///			&lt;system.web&gt;
		///				&lt;httpModules&gt;
		///					&lt;add name="ExceptionHttpModule" type="HTB.DevFx.ExceptionManagement.Web.ExceptionHttpModule, HTB.DevFx" /&gt;
		///				&lt;/httpModules&gt;
		///				......
		///				......
		///			&lt;/system.web&gt;
		///			......
		///		</code>
		/// </remarks>
		public static void Init() {
			Init(false);
		}

		/// <summary>
		/// 获取整个框架的配置管理器
		/// </summary>
		/// <remarks>
		/// 建议在应用程序中不要直接使用此管理器，要获得应用程序自己的配置信息，请在框架配置文件中添加如下的节
		///		<code>
		///			&lt;framework&gt;
		///				&lt;modules&gt;
		///					......
		///					&lt;!--name表示应用程序模块的名称，configName表示配置节名称--&gt;
		///					&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
		///				&lt;/modules&gt;
		///			&lt;/framework&gt;
		///			......
		///			......
		///			&lt;!--下面就可以填入应用程序自己的配置信息--&gt;
		///			&lt;app&gt;
		///			&lt;/app&gt;
		///		</code>
		///	在代码中要获得&lt;app&gt;&lt;/app&gt;这个配置信息，请使用如下代码：
		///		<code>
		///			......
		///			Framework.GetModule("app").Setting;
		///			......
		///		</code>
		///	更具体的信息，请参看 <see cref="HTB.DevFx.Core.AppModule{T}"/>
		/// </remarks>
		public static IConfigManager Configer {
			get {
				Init();
				return Config.Configer.Current;
			}
		}

		/// <summary>
		/// 日志管理器
		/// </summary>
		/// <remarks>
		/// 还可以直接使用 <see cref="HTB.DevFx.Log.Loggor"/> 来写日志<br />
		/// 在调用此方法前，请在框架配置文件中配置好，具体请参见日志的配置说明
		/// </remarks>
		public static ILogManager Loggor {
			get {
				Init();
				return Log.Loggor.Current;
			}
		}

		/// <summary>
		/// 异常管理器
		/// </summary>
		/// <remarks>
		/// 还可以直接使用 <see cref="HTB.DevFx.ExceptionManagement.Exceptor"/> 来处理<br />
		/// 在调用此方法前，请在框架配置文件中配置好，具体请参见异常处理的配置说明
		/// </remarks>
		public static IExceptionManager Exceptor {
			get {
				Init();
				return ExceptionManagement.Exceptor.Instance;
			}
		}

		/// <summary>
		/// 缓存管理器
		/// </summary>
		/// <remarks>
		/// 还可以直接使用 <see cref="HTB.DevFx.Cache.Cacher"/> 来处理<br />
		/// 在调用此方法前，请在框架配置文件中配置好，具体请参见缓存的配置说明
		/// </remarks>
		public static ICacheManager Cacher {
			get {
				Init();
				return Cache.Cacher.Current;
			}
		}

		/// <summary>
		/// 获取指定的模块
		/// </summary>
		/// <param name="moduleName">模块名</param>
		/// <returns>IModule</returns>
		public static IModule GetModule(string moduleName) {
			Init();
			return ((IFramework)framework).GetModule(moduleName);
		}

		/// <summary>
		/// 所有模块
		/// </summary>
		public static IModule[] Modules {
			get {
				Init();
				return ((IFramework)framework).Modules;
			}
		}
	}
}