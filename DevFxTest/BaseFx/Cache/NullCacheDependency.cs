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
	/// 永不过期的缓存策略
	/// </summary>
	[Serializable]
	public class NullCacheDependency : ICacheDependency
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public NullCacheDependency() {
		}

		#region ICacheDependency Members

		/// <summary>
		/// 是否已过期（永远返回<c>false</c>）
		/// </summary>
		public bool IsExpired {
			get {
				return false;
			}
		}

		/// <summary>
		/// 重置缓存策略（相当于重新开始缓存）
		/// </summary>
		public void Reset() {
		}

		#endregion
	}
}
