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

using System.Collections.Specialized;
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 内存数据存储方式的存储器
	/// </summary>
	public class NullCacheStorage : NameObjectCollectionBase, ICacheStorage
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public NullCacheStorage() : base() {
			this.IsReadOnly = false;
		}

		#region ICacheStorage Members

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		public void Init(IConfigSetting setting) {
		}

		/// <summary>
		/// 以索引方式获取设置一项存储值
		/// </summary>
		/// <param name="index">存储项的索引</param>
		public object this[int index] {
			get { return this.Get(index); }
			set { this.Set(index, value); }
		}

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public object this[string key] {
			get { return this.Get(key); }
			set { this.Set(key, value); }
		}

		/// <summary>
		/// 添加一项到存储器中
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		public void Add(string key, object value) {
			this.BaseAdd(key, value);
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(string key) {
			return this.BaseGet(key);
		}

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		public object Get(int index) {
			return this.BaseGet(index);
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
			this.BaseSet(key, @value);
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
			this.BaseSet(index, @value);
		}

		/// <summary>
		/// 移除存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		public void Remove(string key) {
			this.BaseRemove(key);
		}

		/// <summary>
		/// 在指定的位置移除存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		public void RemoveAt(int index) {
			this.BaseRemoveAt(index);
		}

		/// <summary>
		/// 判断存储器中是否包含指定健值的存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>是/否</returns>
		public bool Contains(string key) {
			return this.BaseGet(key) == null;
		}

		/// <summary>
		/// 清除此存储器中所有的项
		/// </summary>
		public void Clear() {
			this.BaseClear();
		}

		/// <summary>
		/// 获得此存储器中所有项的健值
		/// </summary>
		/// <returns>健值列表（数组）</returns>
		public string[] GetAllKeys() {
			return this.BaseGetAllKeys();
		}

		/// <summary>
		/// 获取此存储器中所有项的值
		/// </summary>
		/// <returns>存储项列表（数组）</returns>
		public object[] GetAllValues() {
			return this.BaseGetAllValues();
		}

		#endregion
	}
}
