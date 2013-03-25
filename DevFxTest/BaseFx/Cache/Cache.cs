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
using System.Timers;
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// ʵ�л������ӿڵĴ洢����
	/// </summary>
	public class Cache : ICache
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">�����������ý�</param>
		public Cache(IConfigSetting setting) {
			this.Init(setting);
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="cacheStorage">ָ���洢��</param>
		/// <param name="interval">���ļ��ʱ��</param>
		public Cache(ICacheStorage cacheStorage, int interval) {
			this.Init(cacheStorage, interval);
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		public Cache() {
		}

		private bool isInit;
		private IConfigSetting setting;
		private string name;
		private ICacheStorage cacheStorage;
		private int interval;
		private Timer timer;

		private bool IsInit() {
			if(!this.isInit) {
				throw new CacheException("����洢��û�б���ȷ��ʼ��");
			}
			return this.isInit;
		}

		private void Monitor() {
			if(this.interval <= 0) {
				return;
			}
			this.timer = new Timer(this.interval);
			this.timer.Enabled = false;
			this.timer.AutoReset = false;
			this.timer.Elapsed += new ElapsedEventHandler(TimerOnElapsed);
			this.timer.Start();
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e) {
			this.timer.Enabled = false;

			for(int i = 0; i < this.cacheStorage.Count;) {
				CacheItem item = (CacheItem)this.cacheStorage[i];
				if(item != null && item.CacheDependency.IsExpired) {
					this.cacheStorage.RemoveAt(i);
				} else {
					i++;
				}
			}

			this.timer.Start();
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="cacheStorage">ָ���洢��</param>
		/// <param name="interval">���ļ��ʱ��</param>
		public void Init(ICacheStorage cacheStorage, int interval) {
			if(this.isInit) {
				return;
			}
			this.cacheStorage = cacheStorage;
			this.interval = interval;
			this.Monitor();
			this.isInit = true;
		}

		#region ICache Members

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public void Init(IConfigSetting setting) {
			if(this.isInit) {
				return;
			}

			this.setting = setting;
			this.name = this.setting.Property["name"].Value;
			this.cacheStorage = setting["cacheStorage"].Property["type"].ToObject<ICacheStorage>(true);
			this.cacheStorage.Init(setting["cacheStorage"]);
			this.interval = setting.Property["interval"].ToInt32();
			this.Monitor();
			this.isInit = true;
		}

		/// <summary>
		/// ����������
		/// </summary>
		public string Name {
			get { return this.name; }
		}

		/// <summary>
		/// �Խ�ֵ��ʽ��ȡ/���û����ֵ��
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <remarks>
		///	���������ֵ����ʹ���������ڲ��Ի���
		/// </remarks>
		public object this[string key] {
			get { return this.Get(key); }
			set { this.Add(key, value, new NullCacheDependency()); }
		}

		/// <summary>
		/// ��ָ����ֵ�͹��ڲ��������û����ֵ��
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="cacheDependency">������Ĺ��ڲ���</param>
		public object this[string key, ICacheDependency cacheDependency] {
			set { this.Add(key, value, cacheDependency); }
		}

		/// <summary>
		/// ��ȡ�˻�������������ĸ���
		/// </summary>
		public int Count {
			get {
				this.IsInit();
				return this.cacheStorage.Count;
			}
		}

		/// <summary>
		/// ���һ���������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="value">����Ķ���</param>
		/// <param name="cacheDependency">������Ĺ��ڲ���</param>
		public void Add(string key, object value, ICacheDependency cacheDependency) {
			this.IsInit();
			CacheItem item = (CacheItem)this.cacheStorage[key];
			if(item == null) {
				item = new CacheItem(key, value, cacheDependency);
			} else {
				item.Value = value;
			}
			this.cacheStorage[key] = item;
		}

		/// <summary>
		/// ���һ���������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="value">����Ķ���</param>
		/// <remarks>
		/// û��ָ�����ڲ��ԣ���ʹ���������ڲ��Ի���
		/// </remarks>
		public void Add(string key, object value) {
			this.Add(key, value, new NullCacheDependency());
		}

		/// <summary>
		/// ��ȡ������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <returns>����Ķ������������û�����У��򷵻�<c>null</c></returns>
		public object Get(string key) {
			this.IsInit();
			CacheItem item = (CacheItem)this.cacheStorage[key];
			object @value = null;
			if(item != null) {
				if(!item.CacheDependency.IsExpired) {
					@value = item.Value;
					item.Hits++;
					item.LastAccessTime = DateTime.Now;
					item.CacheDependency.Reset();
				} else {
					item = null;
					this.cacheStorage.Remove(key);
				}
			}
			return @value;
		}

		/// <summary>
		/// �Ƴ�������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		public void Remove(string key) {
			this.IsInit();
			this.cacheStorage.Remove(key);
		}

		/// <summary>
		/// �жϻ��������Ƿ����ָ����ֵ�Ļ�����
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <returns>��/��</returns>
		public bool Contains(string key) {
			this.IsInit();
			return this.cacheStorage.Contains(key);
		}

		/// <summary>
		/// ����˻����������е���
		/// </summary>
		/// <remarks>
		/// ��Ӱ�������ļ�������������
		/// </remarks>
		public void Clear() {
			this.IsInit();
			this.cacheStorage.Clear();
		}

		#endregion
	}
}
