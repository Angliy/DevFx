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

using System.Configuration;

namespace HTB.DevFx.Config.DotNetConfig
{
	/// <summary>
	/// 配置集合（泛型）
	/// </summary>
	/// <typeparam name="T">集合元素类型</typeparam>
	public class ConfigCollection<T> : BaseConfigurationElementCollection where T : ConfigurationElement, new()
	{
		/// <summary>
		/// 按索引方式获取元素
		/// </summary>
		/// <param name="index">索引</param>
		/// <returns>元素</returns>
		public virtual T this[int index] {
			get { return (T)this.BaseGet(index); }
		}

		/// <summary>
		/// 按键值方式获取元素
		/// </summary>
		/// <param name="key">键值</param>
		/// <returns>元素</returns>
		public virtual T this[object key] {
			get { return (T)this.BaseGet(key); }
		}

		/// <summary>
		/// 集合转换成数组
		/// </summary>
		/// <returns>元素数组</returns>
		public virtual T[] ToArray() {
			T[] values = new T[this.Count];
			this.CopyTo(values, 0);
			return values;
		}

		/// <summary>
		/// 创建新元素
		/// </summary>
		/// <returns>新元素</returns>
		protected override ConfigurationElement CreateNewElement() {
			return new T();
		}
	}
}