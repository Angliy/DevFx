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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using HTB.DevFx.Data.Attributes;

namespace HTB.DevFx.Data
{
	/// <summary>
	/// 实现<see cref="IFieldMemberInfo"/>接口
	/// </summary>
	public class FieldMemberInfo : IFieldMemberInfo
	{
		#region constructor
		
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="columnAttribute">特性</param>
		/// <param name="property">属性信息</param>
		public FieldMemberInfo(ColumnAttribute columnAttribute, PropertyInfo property) {
			this.columnAttribute = columnAttribute;
			this.property = property;
			this.memberInfo = property;
			this.name = columnAttribute.ColumnName == null ? property.Name : columnAttribute.ColumnName;
			this.canRead = !columnAttribute.WriteOnly && property.CanRead;
			this.canWrite = !columnAttribute.ReadOnly && property.CanWrite;
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="columnAttribute">特性</param>
		/// <param name="field">字段信息</param>
		public FieldMemberInfo(ColumnAttribute columnAttribute, FieldInfo field) {
			this.columnAttribute = columnAttribute;
			this.field = field;
			this.memberInfo = field;
			this.name = columnAttribute.ColumnName == null ? field.Name : columnAttribute.ColumnName;
			this.canRead = !columnAttribute.WriteOnly;
			this.canWrite = !columnAttribute.ReadOnly;
		}

		#endregion

		#region private members

		private ColumnAttribute columnAttribute;
		private PropertyInfo property;
		private FieldInfo field;
		private MemberInfo memberInfo;
		private string name;
		private bool canRead;
		private bool canWrite;

		#endregion

		#region IFieldMemberInfo Members

		/// <summary>
		/// 字段是否可读
		/// </summary>
		public bool CanRead {
			get { return this.canRead; }
		}

		/// <summary>
		/// 字段是否可写
		/// </summary>
		public bool CanWrite {
			get { return this.canWrite; }
		}

		/// <summary>
		/// 字段名
		/// </summary>
		public string Name {
			get { return this.name; }
		}

		/// <summary>
		/// 字段加载的成员信息<see cref="IFieldMemberInfo.MemberInfo"/>
		/// </summary>
		public MemberInfo MemberInfo {
			get { return this.memberInfo; }
		}

		/// <summary>
		/// 字段本身的加载信息<see cref="ColumnAttribute"/>
		/// </summary>
		public ColumnAttribute Column {
			get { return this.columnAttribute; }
		}

		/// <summary>
		/// 获取字段的值
		/// </summary>
		/// <param name="obj">包含此字段的实例</param>
		/// <returns>字段的值</returns>
		public object GetValue(object obj) {
			if(this.property != null) {
				return this.property.GetValue(obj, null);
			} else {
				return this.field.GetValue(obj);
			}
		}

		/// <summary>
		/// 设置字段的值
		/// </summary>
		/// <param name="obj">包含此字段的实例</param>
		/// <param name="value">字段的值</param>
		public void SetValue(object obj, object value) {
			Type type;
			if(this.property != null) {
				type = this.property.PropertyType;
			} else {
				type = this.field.FieldType;
			}
			if(Convert.IsDBNull(value)) {
				if(this.columnAttribute.DefaultValue != null) {
					value = this.columnAttribute.DefaultValue;
				} else if(!type.IsValueType) {
					value = null;
				} else {
					return;
				}
			}
			if(this.property != null) {
				this.property.SetValue(obj, value, null);
			} else {
				this.field.SetValue(obj, value);
			}
		}

		#endregion

		#region static members

		private static Dictionary<Type, IFieldMemberInfo[]> cache = new Dictionary<Type,IFieldMemberInfo[]>();
		private static object lockObject = new object();

		/// <summary>
		/// 字段绑定预置值
		/// </summary>
		public const BindingFlags FieldBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		/// <summary>
		/// 获取绑定的字段信息列表
		/// </summary>
		/// <param name="type">被获取的类型</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type) {
			return GetFieldMembers(type, FieldBindingFlags);
		}

		/// <summary>
		/// 获取绑定的字段信息列表
		/// </summary>
		/// <param name="type">被获取的类型</param>
		/// <param name="columnBindingFlags">绑定标识</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type, BindingFlags columnBindingFlags) {
			return GetFieldMembers(type, columnBindingFlags, false);
		}

		/// <summary>
		/// 获取绑定的字段信息列表
		/// </summary>
		/// <param name="type">被获取的类型</param>
		/// <param name="columnBindingFlags">绑定标识</param>
		/// <param name="inherit">是否包含继承类</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type, BindingFlags columnBindingFlags, bool inherit) {
			if(cache.ContainsKey(type)) {
				return cache[type];
			}
			lock (lockObject) {
				if (cache.ContainsKey(type)) {
					return cache[type];
				}
				ArrayList fieldMembers = new ArrayList();
				PropertyInfo[] props = type.GetProperties(columnBindingFlags);
				for (int i = 0; i < props.Length; i++) {
					ColumnAttribute[] columnAttributes = (ColumnAttribute[])props[i].GetCustomAttributes(typeof(ColumnAttribute), inherit);
					if (columnAttributes != null && columnAttributes.Length > 0) {
						fieldMembers.Add(new FieldMemberInfo(columnAttributes[0], props[i]));
					}
				}
				FieldInfo[] fields = type.GetFields(columnBindingFlags);
				for (int i = 0; i < fields.Length; i++) {
					ColumnAttribute[] columnAttributes = (ColumnAttribute[])fields[i].GetCustomAttributes(typeof(ColumnAttribute), inherit);
					if (columnAttributes != null && columnAttributes.Length > 0) {
						fieldMembers.Add(new FieldMemberInfo(columnAttributes[0], fields[i]));
					}
				}
				IFieldMemberInfo[] members = (IFieldMemberInfo[])fieldMembers.ToArray(typeof(IFieldMemberInfo));
				if (!cache.ContainsKey(type)) {
					cache.Add(type, members);
				}
				return members;
			}
		}

		/// <summary>
		/// 获取绑定的字段信息
		/// </summary>
		/// <param name="type">被获取的类型</param>
		/// <param name="fieldName">字段名</param>
		/// <param name="columnBindingFlags">绑定标识</param>
		/// <param name="inherit">是否包含继承类</param>
		/// <returns>IFieldMemberInfo</returns>
		public static IFieldMemberInfo GetFieldMember(Type type, string fieldName, BindingFlags columnBindingFlags, bool inherit) {
			PropertyInfo prop = type.GetProperty(fieldName, columnBindingFlags);
			if(prop != null) {
				ColumnAttribute[] columnAttributes = (ColumnAttribute[])prop.GetCustomAttributes(typeof(ColumnAttribute), inherit);
				if(columnAttributes != null && columnAttributes.Length > 0) {
					return new FieldMemberInfo(columnAttributes[0], prop);
				}
			}

			FieldInfo field = type.GetField(fieldName, columnBindingFlags);
			if(field != null) {
				ColumnAttribute[] columnAttributes = (ColumnAttribute[])field.GetCustomAttributes(typeof(ColumnAttribute), inherit);
				if(columnAttributes != null && columnAttributes.Length > 0) {
					return new FieldMemberInfo(columnAttributes[0], field);
				}
			}

			return null;
		}

		#endregion
	}
}
