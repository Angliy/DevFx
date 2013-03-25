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
	/// 时间过期的过期策略（包括相对时间过期、绝对时间过期）
	/// </summary>
	[Serializable]
	public class ExpirationCacheDependency : ICacheDependency
	{
		/// <summary>
		/// 构造函数（绝对时间过期方式）
		/// </summary>
		/// <param name="absoluteExperation">绝对过期时间</param>
		public ExpirationCacheDependency(DateTime absoluteExperation) {
			this.absoluteExperation = absoluteExperation;
			this.slidingExperation = TimeSpan.MaxValue;
			this.isSliding = false;
		}

		/// <summary>
		/// 构造函数（相对时间过期方式）
		/// </summary>
		/// <param name="slidingExperation">相对过期时间</param>
		public ExpirationCacheDependency(TimeSpan slidingExperation) {
			this.slidingExperation = slidingExperation;
			this.absoluteExperation = DateTime.Now.Add(slidingExperation);
			this.isSliding = true;
		}

		private bool isSliding;
		private DateTime absoluteExperation;
		private TimeSpan slidingExperation;

		#region ICacheDependency Members

		/// <summary>
		/// 是否已过期
		/// </summary>
		public bool IsExpired {
			get {
				return (this.absoluteExperation < DateTime.Now);
			}
		}

		/// <summary>
		/// 重置缓存策略（相当于重新开始缓存），针对于相对时间过期策略有效
		/// </summary>
		public void Reset() {
			if(this.isSliding) {
				this.absoluteExperation = DateTime.Now.Add(this.slidingExperation);
			}
		}

		#endregion
	}
}
