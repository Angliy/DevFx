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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 缓存器的存储接口
	/// </summary>
	/// <remarks>
	///	缓存器存储接口的配置：
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="缓存器名称" type="缓存器类型" interval="检查过期的时间间隔，0或小于0表示不进行检查"&gt;
	///							&lt;!--这里配置缓存存储器--&gt;
	///							&lt;cacheStorage type="实现缓存存储器接口的类型" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICacheStorage
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		void Init(IConfigSetting setting);
		
		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		object this[string key] { get; set; }

		/// <summary>
		/// 获取设置一个存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		object this[int index] { get; set; }

		/// <summary>
		/// 获取此存储器所存储项的个数
		/// </summary>
		int Count { get; }

		/// <summary>
		/// 添加一项到存储器中
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 如果存在相同的健值，则更新存储的对象
		/// </remarks>
		void Add(string key, object @value);

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		object Get(string key);

		/// <summary>
		/// 获取存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <returns>存储的对象，如果存储中没有命中，则返回<c>null</c></returns>
		object Get(int index);

		/// <summary>
		/// 设置存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 仅针对存在存储项，若不存在，则不进行任何操作
		/// </remarks>
		void Set(string key, object @value);

		/// <summary>
		/// 设置存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		/// <param name="value">存储的对象</param>
		/// <remarks>
		/// 仅针对存在存储项，若不存在，则不进行任何操作
		/// </remarks>
		void Set(int index, object @value);

		/// <summary>
		/// 移除存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		void Remove(string key);

		/// <summary>
		/// 在指定的位置移除存储项
		/// </summary>
		/// <param name="index">存储项的索引值</param>
		void RemoveAt(int index);

		/// <summary>
		/// 判断存储器中是否包含指定健值的存储项
		/// </summary>
		/// <param name="key">存储项的健值</param>
		/// <returns>是/否</returns>
		bool Contains(string key);

		/// <summary>
		/// 清除此存储器中所有的项
		/// </summary>
		void Clear();

		/// <summary>
		/// 获得此存储器中所有项的健值
		/// </summary>
		/// <returns>健值列表（数组）</returns>
		string[] GetAllKeys();

		/// <summary>
		/// 获取此存储器中所有项的值
		/// </summary>
		/// <returns>存储项列表（数组）</returns>
		object[] GetAllValues();
	}
}
