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
	/// 缓存器接口
	/// </summary>
	/// <remarks>
	///	缓存器的配置：
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="缓存器名称" type="缓存器类型" interval="检查过期的时间间隔，0或小于0表示不进行检查"&gt;
	///							&lt;!--这里配置缓存存储器--&gt;
	///							&lt;cacheStorage type="HTB.DevFx.Cache.NullCacheStorage" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICache
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// 缓存器名称
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// 以健值方式获取/设置缓存项（值）
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <remarks>
		///	如果是设置值，则使用永不过期策略缓存
		/// </remarks>
		object this[string key] { get; set; }

		/// <summary>
		/// 按指定健值和过期策略来设置缓存项（值）
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="cacheDependency">缓存项的过期策略</param>
		object this[string key, ICacheDependency cacheDependency] { set; }

		/// <summary>
		/// 获取此缓存器所缓存项的个数
		/// </summary>
		int Count { get; }

		/// <summary>
		/// 添加一项到缓存器中
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="value">缓存的对象</param>
		/// <param name="cacheDependency">缓存项的过期策略</param>
		void Add(string key, object @value, ICacheDependency cacheDependency);

		/// <summary>
		/// 添加一项到缓存器中
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <param name="value">缓存的对象</param>
		/// <remarks>
		/// 没有指定过期策略，则使用永不过期策略缓存
		/// </remarks>
		void Add(string key, object @value);

		/// <summary>
		/// 获取缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <returns>缓存的对象，如果缓存中没有命中，则返回<c>null</c></returns>
		object Get(string key);

		/// <summary>
		/// 移除缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		void Remove(string key);

		/// <summary>
		/// 判断缓存器中是否包含指定健值的缓存项
		/// </summary>
		/// <param name="key">缓存项的健值</param>
		/// <returns>是/否</returns>
		bool Contains(string key);

		/// <summary>
		/// 清除此缓存器中所有的项
		/// </summary>
		/// <remarks>
		/// 不影响配置文件中其他缓存器
		/// </remarks>
		void Clear();
	}
}
