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

using HTB.DevFx.Cache.Config;
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 缓存实用方法类
	/// </summary>
	public static class CacheHelper
	{
		#region constructor

		static CacheHelper() {
			if (SectionHandler.Current != null) {
				string xml = SectionHandler.Current.OuterXml;
				IConfigSetting setting = ConfigHelper.CreateFromXmlString(xml);
				CreateCacheManager(setting);
			}
		}

		#endregion

		#region private static members
		
		private static ICacheManager cacheManager;

		private static void CreateCacheManager(IConfigSetting setting) {
			cacheManager = setting.Property["type"].ToObject<ICacheManager>();
			cacheManager.Init(setting);
		}

		#endregion

		#region public static members

		/// <summary>
		/// 获取已配置的缓存器
		/// </summary>
		/// <param name="cacheName">配置文件中配置的缓存器名称</param>
		/// <returns>ICache的实例</returns>
		public static ICache GetCache(string cacheName) {
			if(cacheManager != null) {
				return cacheManager.GetCache(cacheName);
			} else {
				return null;
			}
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
	}
}
