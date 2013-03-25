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
using System.Data;
using HTB.DevFx.Data.Attributes;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Data.Utils
{
	/// <summary>
	/// �������ݷ��ʵ�һЩ���õķ���
	/// </summary>
	public static class DataHelper
	{
		private static object GetValueSafely(DataRow row, string fieldName, object value, object defaultValue) {
			if (Convert.IsDBNull(value)) {
				DataColumn column = row.Table.Columns[fieldName];
				if (!Convert.IsDBNull(column.DefaultValue)) {
					value = column.DefaultValue;
				} else {
					value = defaultValue;
				}
			}
			return value;
		}

		/// <summary>
		/// ��ȫ�Ļ�ȡDataRowָ���е�ֵ
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <param name="fieldName">����</param>
		/// <param name="defaultValue">���е�Ĭ��ֵ�������ȡʧ�ܣ��򷵻ش�Ĭ��ֵ��</param>
		/// <returns>�е�ֵ</returns>
		public static object GetValueSafely(DataRow row, string fieldName, object defaultValue) {
			return GetValueSafely(row, fieldName, row[fieldName], defaultValue);
		}

		/// <summary>
		/// ��ȫ�Ļ�ȡDataRowָ����ָ���汾��ֵ
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <param name="fieldName">����</param>
		/// <param name="rowVersion">�а汾</param>
		/// <param name="defaultValue">���е�Ĭ��ֵ�������ȡʧ�ܣ��򷵻ش�Ĭ��ֵ��</param>
		/// <returns>�е�ֵ</returns>
		public static object GetValueSafely(DataRow row, string fieldName, DataRowVersion rowVersion, object defaultValue) {
			return GetValueSafely(row, fieldName, row[fieldName, rowVersion], defaultValue);
		}

		/// <summary>
		/// ����DataRow�ĸ���ֵΪȱʡֵ
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="values">ȱʡֵ�б���DataRow���о�����ͬ�ĸ�����˳��</param>
		public static void SetDefaultValueWhenDBNull(DataRow dr, object[] values) {
			if(dr.Table.Columns.Count != values.Length) {
				return;
			}
			for(int i = 0; i < dr.Table.Columns.Count; i++) {
				string fieldName = dr.Table.Columns[i].ColumnName;
				if(!dr.Table.Columns[i].AllowDBNull && Convert.IsDBNull(dr[fieldName])) {
					if(!Convert.IsDBNull(dr.Table.Columns[i].DefaultValue)) {
						dr[fieldName] = dr.Table.Columns[i].DefaultValue;
					} else {
						dr[fieldName] = values[i];
					}
				}
			}
		}

		/// <summary>
		/// ����DataRow�ĸ���ֵΪȱʡֵ
		/// </summary>
		/// <param name="dr">DataRow</param>
		/// <param name="values">ȱʡֵ�б���DataRow���о�����ͬ�ĸ�����˳��</param>
		public static void SetDataRowDefaultValues(DataRow dr, object[] values) {
			if(values == null || values.Length <= 0 || dr == null) {
				return;
			}
			int num = values.Length;
			if(dr.Table.Columns.Count < num) {
				num = dr.Table.Columns.Count;
			}
			for (int i = 0; i < num; i++) {
				if (!dr.Table.Columns[i].ReadOnly) {
					dr[i] = values[i];
				}
			}
		}

		/// <summary>
		/// ����DataRow�ĸ���ֵΪȱʡֵ����DataTable��Schema�ж�ȡȱʡֵ��
		/// </summary>
		/// <param name="dr">DataRow</param>
		public static void SetDefaultValueWhenDBNull(DataRow dr) {
			for(int i = 0; i < dr.Table.Columns.Count; i++) {
				string fieldName = dr.Table.Columns[i].ColumnName;
				if(!dr.Table.Columns[i].AllowDBNull && Convert.IsDBNull(dr[fieldName])) {
					if(!Convert.IsDBNull(dr.Table.Columns[i].DefaultValue)) {
						dr[fieldName] = dr.Table.Columns[i].DefaultValue;
					} else {
						dr[fieldName] = TypeHelper.CreateObject(dr.Table.Columns[i].DataType, null, true);
					}
				}
			}
		}

		/// <summary>
		/// ����DataTable�е�ȱʡֵ
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <param name="values">ȱʡֵ�б���DataTable���о�����ͬ˳�����������ͬ��</param>
		public static void SetColumnDefaultValue(DataTable dt, object[] values) {
			Checker.CheckArgumentNull("DataTable", dt, true);
			Checker.CheckEmptyArray("values", values, true);
			int num = dt.Columns.Count;
			if(values.Length < num) {
				num = values.Length;
			}
			for(int i = 0; i < num; i++) {
				if(!dt.Columns[i].AllowDBNull && !dt.Columns[i].AutoIncrement && Convert.IsDBNull(dt.Columns[i].DefaultValue)) {
					dt.Columns[i].DefaultValue = values[i];
				}
			}
		}

		/// <summary>
		/// ����DataTable���Ƿ�����DBNull
		/// </summary>
		/// <param name="dt">DataTable</param>
		/// <param name="values">�����Ƿ�����DBNull����DataTable���о�����ͬ˳�����������ͬ��</param>
		public static void SetColumnAllowDBNull(DataTable dt, bool[] values) {
			Checker.CheckArgumentNull("DataTable", dt, true);
			Checker.CheckEmptyArray("values", values, true);
			int num = dt.Columns.Count;
			if(values.Length < num) {
				num = values.Length;
			}
			for(int i = 0; i < num; i++) {
				dt.Columns[i].AllowDBNull = values[i];
			}
		}
		
		/// <summary>
		/// ��ȡ���� <see cref="ColumnAttribute"/> ���Ե�ȱʡֵ
		/// </summary>
		/// <param name="type">��Ҫ���������</param>
		/// <returns>ȱʡֵ�б�</returns>
		public static object[] GetDefaultColumnAttibuteValues(Type type) {
			IFieldMemberInfo[] members = FieldMemberInfo.GetFieldMembers(type);
			object[] defaultValues = new object[members.Length];
			for(int i = 0; i < members.Length; i++) {
				IFieldMemberInfo member = members[i];
				Type memberType = member.MemberInfo.DeclaringType;
				object defaultValue = member.Column.DefaultValue;
				if (defaultValue == null) {
					defaultValues[i] = memberType.TypeInitializer.Invoke(null);
				} else {
					if(memberType == typeof(DateTime)) {
						switch(defaultValue.ToString()) {
							case "DateTime.Now":
								defaultValue = DateTime.Now;
								break;
							case "DateTime.MinValue":
								defaultValue = DateTime.MinValue;
								break;
							case "DateTime.MaxValue":
								defaultValue = DateTime.MaxValue;
								break;
						}
					}
					defaultValues[i] = defaultValue;
				}
			}
			return defaultValues;
		}
	}
}
