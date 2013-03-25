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
using HTB.DevFx.Core;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 整个框架的缓存管理器
	/// </summary>
	/// <remarks>
	/// 与框架的配合配置格式：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;!--缓存管理模块--&gt;
	///						&lt;module name="cache" type="HTB.DevFx.Cache.Cacher" configName="cache" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///					
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	/// <example>
	/// 下面的示例演示了以相对时间过期策略来缓存对象：
	///		<code>
	///			......
	///			string key = Guid.NewGuid().ToString();
	///			object cachingObject = new YourCachingObject();
	///			ICache cache = Cacher.GetCache("your cache instance name");
	///			cache.Add(key, cachingObject, new ExpirationCacheDependency(TimeSpan.FromSeconds(20)));
	///			......
	///		</code>
	/// </example>
	public class Cacher : CoreModule, ICacheManager, IFactory
	{
		#region constructor

		static Cacher() {
			Framework.Init();
		}
		
		private Cacher() {
			if(current == null) {
				current = this;
			}
		}

		#endregion

		#region private members
		
		private ICacheManager cacheManager;

		private ICacheManager CreateCacheManager(IConfigSetting setting) {
			ICacheManager cacheManager = (ICacheManager)setting.Property["type"].ToObject(typeof(ICacheManager), true);
			cacheManager.Init(setting);
			return cacheManager;
		}

		private ICacheManager GetCacheManager() {
			if(cacheManager == null) {
				cacheManager = this.CreateCacheManager(this.setting);
			}
			return cacheManager;
		}

		#endregion

		#region override module members

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			cacheManager = this.GetCacheManager();
		}

		/// <summary>
		/// 获取本模块的事例（可以是单例模式也可以是多例模式）
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return Current;
		}

		#endregion

		#region ICacheManager Members

		void ICacheManager.Init(IConfigSetting setting) {
		}

		ICache ICacheManager.GetCache(string cacheName) {
			return cacheManager.GetCache(cacheName);
		}

		ICache ICacheManager.GetCache(IConfigSetting cacheSetting) {
			return cacheManager.GetCache(cacheSetting);
		}

		#endregion

		#region static members

		private static Cacher current;

		/// <summary>
		/// 缓存管理器的唯一实例（单件模式）
		/// </summary>
		public static Cacher Current {
			get {
				if(current == null) {
					current = new Cacher();
				}
				return current;
			}
		}

		/// <summary>
		/// 获取已配置的缓存器
		/// </summary>
		/// <param name="cacheName">配置文件中配置的缓存器名称</param>
		/// <returns>ICache的实例</returns>
		public static ICache GetCache(string cacheName) {
			return Current.cacheManager.GetCache(cacheName);
		}

		/// <summary>
		/// 获取缓存项值
		/// </summary>
		/// <param name="cacheName">配置文件中配置的缓存器名称</param>
		/// <param name="key">缓存项健值</param>
		/// <returns>缓存项值，如果没有命中，则返回<c>null</c></returns>
		public static object GetCacheValue(string cacheName, string key) {
			return GetCacheValue(cacheName, key, false);
		}

		/// <summary>
		/// 获取缓存项值
		/// </summary>
		/// <param name="cacheName">配置文件中配置的缓存器名称</param>
		/// <param name="key">缓存项健值</param>
		/// <param name="throwOnError">如果有错误，是否抛出异常</param>
		/// <returns>缓存项值，如果没有命中，则返回<c>null</c></returns>
		public static object GetCacheValue(string cacheName, string key, bool throwOnError) {
			ICache cache = GetCache(cacheName);
			object @value = null;
			if(cache == null) {
				if(throwOnError) {
					throw new CacheException("没有配置Cache：" + cacheName);
				}
			} else {
				@value = cache[key];
			}

			return @value;
		}

		#endregion

		#region IFactory Members

		object IFactory.GetManager(params object[] parameters) {
			return cacheManager;
		}

		#endregion
	}
}
