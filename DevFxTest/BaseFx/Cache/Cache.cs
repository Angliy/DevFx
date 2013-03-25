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
	/// 实行缓存器接口的存储器类
	/// </summary>
	public class Cache : ICache
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">缓存器的配置节</param>
		public Cache(IConfigSetting setting) {
			this.Init(setting);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="cacheStorage">指定存储器</param>
		/// <param name="interval">检测的间隔时间</param>
		public Cache(ICacheStorage cacheStorage, int interval) {
			this.Init(cacheStorage, interval);
		}

		/// <summary>
		/// 构造函数
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
				throw new CacheException("缓存存储器没有被正确初始化");
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
		/// 初始化
		/// </summary>
		/// <param name="cacheStorage">指定存储器</param>
		/// <param name="interval">检测的间隔时间</param>
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
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
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
		/// 缓存器名称
		/// </summary>
		public string Name {
			get { return this.name; }
		}

		/// <summary>
		/// 以健值方式获取/设置缓存项（值）
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <remarks>
		///	如果是设置值，则使用永不过期策略缓存
		/// </remarks>
		public object this[string key] {
			get { return this.Get(key); }
			set { this.Add(key, value, new NullCacheDependency()); }
		}

		/// <summary>
		/// 按指定健值和过期策略来设置缓存项（值）
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="cacheDependency">缓存项的过期策略</param>
		public object this[string key, ICacheDependency cacheDependency] {
			set { this.Add(key, value, cacheDependency); }
		}

		/// <summary>
		/// 获取此缓存器所缓存项的个数
		/// </summary>
		public int Count {
			get {
				this.IsInit();
				return this.cacheStorage.Count;
			}
		}

		/// <summary>
		/// 添加一项到缓存器中
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="value">缓存的对象</param>
		/// <param name="cacheDependency">缓存项的过期策略</param>
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
		/// 添加一项到缓存器中
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="value">缓存的对象</param>
		/// <remarks>
		/// 没有指定过期策略，则使用永不过期策略缓存
		/// </remarks>
		public void Add(string key, object value) {
			this.Add(key, value, new NullCacheDependency());
		}

		/// <summary>
		/// 获取缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <returns>缓存的对象，如果缓存中没有命中，则返回<c>null</c></returns>
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
		/// 移除缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		public void Remove(string key) {
			this.IsInit();
			this.cacheStorage.Remove(key);
		}

		/// <summary>
		/// 判断缓存器中是否包含指定健值的缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <returns>是/否</returns>
		public bool Contains(string key) {
			this.IsInit();
			return this.cacheStorage.Contains(key);
		}

		/// <summary>
		/// 清除此缓存器中所有的项
		/// </summary>
		/// <remarks>
		/// 不影响配置文件中其他缓存器
		/// </remarks>
		public void Clear() {
			this.IsInit();
			this.cacheStorage.Clear();
		}

		#endregion
	}
}
