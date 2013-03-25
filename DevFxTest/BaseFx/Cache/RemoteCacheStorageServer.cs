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
using System.Collections.Generic;
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 存储器远端（服务器）类
	/// </summary>
	/// <remarks>
	/// 配合 <see cref="RemoteCacheStorageProxy"/>，把存储数据存储到远端<br />
	/// 注意：应该把本类配置为Singleton模式
	/// </remarks>
	public class RemoteCacheStorageServer : MarshalByRefObject, ICacheStorage
	{
		#region private members

		private static List<RemoteCacheStorageServer> instances = new List<RemoteCacheStorageServer>();

		private ICacheStorage cacheStorage = new NullCacheStorage();

		#endregion

		#region constructors

		private RemoteCacheStorageServer() {
			instances.Add(this);
		}

		#endregion

		#region public static members

		/// <summary>
		/// 获取本类的实例列表
		/// </summary>
		/// <remarks>
		/// 利用此属性，可以把客户端轮询过期时间设置为-1，然后在服务器端进行轮询，以提高效率
		/// </remarks>
		public static RemoteCacheStorageServer[] Instances {
			get {
				return instances.ToArray();
			}
		}

		#endregion

		#region ICacheStorage Members

		/// <summary>
		/// 添加一项到存储器中
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 如果存在相同的健值，则更新存储的对象
		/// </remarks>
		public void Add(string key, object value) {
			this.cacheStorage.Add(key, value);
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(string key) {
			return this.cacheStorage.Get(key);
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(int index) {
			return this.cacheStorage.Get(index);
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
			this.cacheStorage.Set(key, @value);
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
			this.cacheStorage.Set(index, @value);
		}

		/// <summary>
		/// 移除存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public void Remove(string key) {
			this.cacheStorage.Remove(key);
		}

		/// <summary>
		/// 在指定的位置移除存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		public void RemoveAt(int index) {
			this.cacheStorage.RemoveAt(index);
		}

		/// <summary>
		/// 判断存储器中是否包含指定健值的存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>是/否</returns>
		public bool Contains(string key) {
			return this.cacheStorage.Contains(key);
		}

		/// <summary>
		/// 清除此存储器中所有的项
		/// </summary>
		public void Clear() {
			this.cacheStorage.Clear();
		}

		/// <summary>
		/// 获得此存储器中所有项的健值
		/// </summary>
		/// <returns>健值列表（数组）</returns>
		public string[] GetAllKeys() {
			return this.cacheStorage.GetAllKeys();
		}

		/// <summary>
		/// 获取此存储器中所有项的值
		/// </summary>
		/// <returns>存储项列表（数组）</returns>
		public object[] GetAllValues() {
			return this.cacheStorage.GetAllValues();
		}

		/// <summary>
		/// 获取此存储器所存储项的个数
		/// </summary>
		public int Count {
			get { return this.cacheStorage.Count; }
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		public void Init(IConfigSetting setting) {
			this.cacheStorage.Init(setting);
		}

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public object this[string key] {
			get { return this.cacheStorage[key]; }
			set { this.cacheStorage[key] = value; }
		}

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		public object this[int index] {
			get { return this.cacheStorage[index]; }
			set { this.cacheStorage[index] = value; }
		}

		#endregion
	}
}
