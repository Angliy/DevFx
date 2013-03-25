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
	/// ������ܵĻ��������
	/// </summary>
	/// <remarks>
	/// ���ܵ�������ø�ʽ��
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;!--�������ģ��--&gt;
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
	/// �����ʾ����ʾ�������ʱ����ڲ������������
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
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		protected override void OnInit(IFramework framework, IConfigSetting setting) {
			base.OnInit(framework, setting);
			cacheManager = this.GetCacheManager();
		}

		/// <summary>
		/// ��ȡ��ģ��������������ǵ���ģʽҲ�����Ƕ���ģʽ��
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
		/// �����������Ψһʵ��������ģʽ��
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
		/// ��ȡ�����õĻ�����
		/// </summary>
		/// <param name="cacheName">�����ļ������õĻ���������</param>
		/// <returns>ICache��ʵ��</returns>
		public static ICache GetCache(string cacheName) {
			return Current.cacheManager.GetCache(cacheName);
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

		#region IFactory Members

		object IFactory.GetManager(params object[] parameters) {
			return cacheManager;
		}

		#endregion
	}
}
