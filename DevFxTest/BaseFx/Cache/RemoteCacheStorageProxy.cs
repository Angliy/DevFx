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
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 远程缓存存储代理类
	/// </summary>
	/// <remarks>
	///	远程缓存存储代理的配置：
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="缓存器名称" type="缓存器类型" interval="检查过期的时间间隔，0或小于0表示不进行检查"&gt;
	///							&lt;!--这里配置缓存存储器--&gt;
	///							&lt;cacheStorage type="HTB.DevFx.Cache.RemoteCacheStorageProxy" url="远端对象的配置，例如：tcp://localhost:8085/RemoteCacheStorage" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public class RemoteCacheStorageProxy : ICacheStorage
	{
		#region private members
		
		private ICacheStorage remoteStorage;

		private bool isInit = false;
		private string remoteUrl = null;

		private void GetRemoteObject() {
			if(this.remoteStorage == null) {
				this.remoteStorage = (ICacheStorage)Activator.GetObject(typeof(ICacheStorage), this.remoteUrl);
			}
		}

		private bool RemotingIsReady() {
			if(!this.isInit) {
				return false;
			}
			this.GetRemoteObject();
			if(this.remoteStorage == null) {
				return false;
			}
			try {
				this.remoteStorage.ToString();
				return true;
			} catch {
				//Exceptor.Publish(new CacheException("远程服务器不可用，请检查！", e), LogLevel.EMERGENCY);
				return false;
			}
		}

		#endregion

		#region ICacheStorage Members

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		public void Init(IConfigSetting setting) {
			if(!this.isInit) {
				this.remoteUrl = setting.Property["url"].Value;
				this.GetRemoteObject();
				this.isInit = true;
			}
		}

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public object this[string key] {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage[key];
				} else {
					return null;
				}
			}
			set {
				if(this.RemotingIsReady()) {
					this.remoteStorage[key] = value;
				}
			}
		}

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		public object this[int index] {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage[index];
				} else {
					return null;
				}
			}
			set {
				if(this.RemotingIsReady()) {
					this.remoteStorage[index] = value;
				}
			}
		}

		/// <summary>
		/// 获取此存储器所存储项的个数
		/// </summary>
		/// <remarks>
		/// 如果远端对象没有准备好，则返回-1
		/// </remarks>
		public int Count {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage.Count;
				} else {
					return -1;
				}
			}
		}

		/// <summary>
		/// 添加一项到存储器中
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 如果存在相同的健值，则更新存储的对象
		/// </remarks>
		public void Add(string key, object @value) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Add(key, @value);
			}
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(string key) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Get(key);
			} else {
				return null;
			}
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(int index) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Get(index);
			} else {
				return null;
			}
		}

		/// <summary>
		/// 设置存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 仅针对存在存储项，若不存在，则不进行任何操作
		/// </remarks>
		public void Set(string key, object @value) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Set(key, @value);
			}
		}

		/// <summary>
		/// 设置存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 仅针对存在存储项，若不存在，则不进行任何操作
		/// </remarks>
		public void Set(int index, object @value) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Set(index, @value);
			}
		}

		/// <summary>
		/// 移除存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public void Remove(string key) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Remove(key);
			}
		}

		/// <summary>
		/// 在指定的位置移除存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		public void RemoveAt(int index) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.RemoveAt(index);
			}
		}

		/// <summary>
		/// 判断存储器中是否包含指定健值的存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>是/否</returns>
		public bool Contains(string key) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Contains(key);
			} else {
				return false;
			}
		}

		/// <summary>
		/// 清除此存储器中所有的项
		/// </summary>
		public void Clear() {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Clear();
			}
		}

		/// <summary>
		/// 获得此存储器中所有项的健值
		/// </summary>
		/// <returns>健值列表（数组）</returns>
		public string[] GetAllKeys() {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.GetAllKeys();
			} else {
				return null;
			}
		}

		/// <summary>
		/// 获取此存储器中所有项的值
		/// </summary>
		/// <returns>存储项列表（数组）</returns>
		public object[] GetAllValues() {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.GetAllValues();
			} else {
				return null;
			}
		}

		#endregion
	}
}