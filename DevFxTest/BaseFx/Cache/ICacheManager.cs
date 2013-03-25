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
	/// 缓存管理器接口
	/// </summary>
	/// <remarks>
	///	缓存管理器的配置：
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="缓存管理器类型"&gt;
	///					&lt;caches&gt;&lt;!--这里配置缓存空间（多实例模式）--&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICacheManager
	{
		/// <summary>
		/// 初始化，由框架调用
		/// </summary>
		/// <param name="setting">缓存管理器的配置节</param>
		void Init(IConfigSetting setting);
		
		/// <summary>
		/// 获取缓存空间
		/// </summary>
		/// <param name="cacheName">在配置文件上配置的缓存空间名</param>
		/// <returns>实现ICache接口的缓存器实例</returns>
		ICache GetCache(string cacheName);

		/// <summary>
		/// 以一定的配置节来实例化缓存器
		/// </summary>
		/// <param name="cacheSetting">缓存器配置节</param>
		/// <returns>实现ICache接口的缓存器实例</returns>
		ICache GetCache(IConfigSetting cacheSetting);
	}
}
