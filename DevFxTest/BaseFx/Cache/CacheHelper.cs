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
	/// ����ʵ�÷�����
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
		/// ��ȡ�����õĻ�����
		/// </summary>
		/// <param name="cacheName">�����ļ������õĻ���������</param>
		/// <returns>ICache��ʵ��</returns>
		public static ICache GetCache(string cacheName) {
			if(cacheManager != null) {
				return cacheManager.GetCache(cacheName);
			} else {
				return null;
			}
		}

		/// <summary>
		/// ��ȡ������ֵ
		/// </summary>
		/// <param name="cacheName">�����ļ������õĻ���������</param>
		/// <param name="key">�����ֵ</param>
		/// <returns>������ֵ�����û�����У��򷵻�<c>null</c></returns>
		public static object GetCacheValue(string cacheName, string key) {
			return GetCacheValue(cacheName, key, false);
		}

		/// <summary>
		/// ��ȡ������ֵ
		/// </summary>
		/// <param name="cacheName">�����ļ������õĻ���������</param>
		/// <param name="key">�����ֵ</param>
		/// <param name="throwOnError">����д����Ƿ��׳��쳣</param>
		/// <returns>������ֵ�����û�����У��򷵻�<c>null</c></returns>
		public static object GetCacheValue(string cacheName, string key, bool throwOnError) {
			ICache cache = GetCache(cacheName);
			object @value = null;
			if(cache == null) {
				if(throwOnError) {
					throw new CacheException("û������Cache��" + cacheName);
				}
			} else {
				@value = cache[key];
			}

			return @value;
		}

		#endregion
	}
}
