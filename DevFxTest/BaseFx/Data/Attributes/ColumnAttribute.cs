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

namespace HTB.DevFx.Data.Attributes
{
	/// <summary>
	/// 一个简单实现O/R关系的描述属性
	/// </summary>
	/// <remarks>
	/// 配合 <see cref="HTB.DevFx.Data.Utils.DataTransfer"/> 来实现对象和数据的转换
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false)]
	public class ColumnAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ColumnAttribute() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="columnName">列名</param>
		public ColumnAttribute(string columnName) {
			this.columnName = columnName;
		}

		private string columnName;
		private string columnGroup;
		private Type columnType;
		private int columnSize;
		private object defaultValue;
		private bool readOnly;
		private bool writeOnly;
		private bool isPrimaryKey;
		private bool isNullable = true;

		/// <summary>
		/// 设置/获取列名
		/// </summary>
		public string ColumnName {
			get { return this.columnName; }
			set { this.columnName = value; }
		}

		/// <summary>
		/// 设置/获取列隶属的组分类
		/// </summary>
		public string ColumnGroup {
			get { return this.columnGroup; }
			set { this.columnGroup = value; }
		}

		/// <summary>
		/// 设置/获取列的类型
		/// </summary>
		public Type ColumnType {
			get { return this.columnType; }
			set { this.columnType = value; }
		}

		/// <summary>
		/// 设置/获取列类型的长度
		/// </summary>
		public int ColumnSize {
			get { return this.columnSize; }
			set { this.columnSize = value; }
		}

		/// <summary>
		/// 设置/获取列的缺省值
		/// </summary>
		public object DefaultValue {
			get { return this.defaultValue; }
			set { this.defaultValue = value; }
		}

		/// <summary>
		/// 设置/获取指示此列是否只读
		/// </summary>
		public bool ReadOnly {
			get { return this.readOnly; }
			set { this.readOnly = value; }
		}

		/// <summary>
		/// 设置/获取指示此列是否只写
		/// </summary>
		public bool WriteOnly {
			get { return this.writeOnly; }
			set { this.writeOnly = value; }
		}

		/// <summary>
		/// 是否为主键
		/// </summary>
		public bool IsPrimaryKey {
			get { return this.isPrimaryKey; }
			set { this.isPrimaryKey = value; }
		}

		/// <summary>
		/// 是否可<c>null</c>
		/// </summary>
		public bool IsNullable {
			get { return this.isNullable; }
			set { this.isNullable = value; }
		}
	}
}
