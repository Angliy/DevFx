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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// 缓存过期策略接口
	/// </summary>
	/// <example>
	/// 下面的示例演示了以相对时间过期策略来缓存对象：
	///		<code>
	///			......
	///			string key = Guid.NewGuid().ToString();
	///			object cachingObject = new YourCachingObject();
	///			ICache cache = CacheHelper.GetCache("your cache instance name");
	///			cache.Add(key, cachingObject, new ExpirationCacheDependency(TimeSpan.FromSeconds(20)));
	///			......
	///		</code>
	/// </example>
	public interface ICacheDependency
	{
		/// <summary>
		/// 是否已过期
		/// </summary>
		bool IsExpired { get; }

		/// <summary>
		/// 重置缓存策略（相当于重新开始缓存）
		/// </summary>
		void Reset();
	}
}
