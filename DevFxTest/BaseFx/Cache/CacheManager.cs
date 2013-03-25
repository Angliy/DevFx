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
using HTB.DevFx.Utils;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// ���������ʵ���࣬����Ӧ��ϵͳʵ�ִ˽ӿڵ��඼�ӱ���̳�
	/// </summary>
	public class CacheManager : ICacheManager
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public CacheManager() {
		}
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configSetting">���ý�</param>
		public CacheManager(IConfigSetting configSetting) {
			this.Init(configSetting);
		}

		/// <summary>
		/// ���ý�
		/// </summary>
		protected IConfigSetting setting;
		private CollectionBase<ICache> caches;
		private bool isInit;

		private ICache CreateCache(IConfigSetting cacheSetting) {
			ICache cache = cacheSetting.Property["type"].ToObject<ICache>();
			cache.Init(cacheSetting);
			return cache;
		}

		#region ICacheManager Members

		/// <summary>
		/// ��ʼ�����ɿ�ܵ���
		/// </summary>
		/// <param name="setting">��������������ý�</param>
		public void Init(IConfigSetting setting) {
			if(this.isInit) {
				return;
			}
			this.setting = setting;
			this.caches = new CollectionBase<ICache>();
			IConfigSetting[] settings = setting["caches"].GetChildSettings();
			for(int i = 0; i < settings.Length; i++) {
				string cacheName = settings[i].Property["name"].Value;
				if(cacheName == null) {
					throw new CacheException("����洢����ΪNull");
				}
				if(this.caches.Contains(cacheName)) {
					throw new CacheException("����洢�����ظ���" + cacheName);
				}
				ICache cache = this.CreateCache(settings[i]);
				this.caches.Add(cacheName, cache);
			}
			this.isInit = true;
		}

		/// <summary>
		/// ��ȡ����ռ�
		/// </summary>
		/// <param name="cacheName">�������ļ������õĻ���ռ���</param>
		/// <returns>ʵ��ICache�ӿڵĻ�����ʵ��</returns>
		public ICache GetCache(string cacheName) {
			return this.caches[cacheName];
		}

		/// <summary>
		/// ��һ�������ý���ʵ����������
		/// </summary>
		/// <param name="cacheSetting">���������ý�</param>
		/// <returns>ʵ��ICache�ӿڵĻ�����ʵ��</returns>
		public ICache GetCache(IConfigSetting cacheSetting) {
			string cacheName = cacheSetting.Property["name"].Value;
			ICache cache = this.GetCache(cacheName);
			if(cache == null) {
				cache = this.CreateCache(cacheSetting);
				this.caches.Add(cacheName, cache);
			}
			return cache;
		}

		#endregion
	}
}
