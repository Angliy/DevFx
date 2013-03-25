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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 缓存项的包装类
	/// </summary>
	[Serializable]
	public class CacheItem
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="key">缓存项健值</param>
		/// <param name="value">储存项值</param>
		/// <param name="cacheDependency">过期策略</param>
		public CacheItem(string key, object @value, ICacheDependency cacheDependency) {
			this.key = key;
			this.value = @value;
			this.cacheDependency = cacheDependency;
			this.hits = 0;
			this.lastAccessTime = DateTime.Now;
		}

		private string key;
		private object @value;
		private ICacheDependency cacheDependency;
		private int hits;
		private DateTime lastAccessTime;

		/// <summary>
		/// 获取过期策略
		/// </summary>
		public ICacheDependency CacheDependency {
			get { return this.cacheDependency; }
		}

		/// <summary>
		/// 获取健值
		/// </summary>
		public string Key {
			get { return this.key; }
		}

		/// <summary>
		/// 获取缓存项
		/// </summary>
		public object Value {
			get { return this.value; }
			set { this.value = value; }
		}

		/// <summary>
		/// 命中次数
		/// </summary>
		public int Hits {
			get { return this.hits; }
			set { this.hits = value; }
		}

		/// <summary>
		/// 最后命中时间
		/// </summary>
		public DateTime LastAccessTime {
			get { return this.lastAccessTime; }
			set { this.lastAccessTime = value; }
		}
	}
}
