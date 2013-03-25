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

using System.Reflection;
using HTB.DevFx.Data.Attributes;

namespace HTB.DevFx.Data
{
	/// <summary>
	/// 对<see cref="ColumnAttribute"/>的包装
	/// </summary>
	public interface IFieldMemberInfo
	{
		/// <summary>
		/// 字段是否可读
		/// </summary>
		bool CanRead { get; }

		/// <summary>
		/// 字段是否可写
		/// </summary>
		bool CanWrite { get; }

		/// <summary>
		/// 字段名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 字段加载的成员信息<see cref="MemberInfo"/>
		/// </summary>
		MemberInfo MemberInfo { get; }

		/// <summary>
		/// 字段本身的加载信息<see cref="ColumnAttribute"/>
		/// </summary>
		ColumnAttribute Column { get; }

		/// <summary>
		/// 获取字段的值
		/// </summary>
		/// <param name="obj">包含此字段的实例</param>
		/// <returns>字段的值</returns>
		object GetValue(object obj);

		/// <summary>
		/// 设置字段的值
		/// </summary>
		/// <param name="obj">包含此字段的实例</param>
		/// <param name="value">字段的值</param>
		void SetValue(object obj, object value);
	}
}
