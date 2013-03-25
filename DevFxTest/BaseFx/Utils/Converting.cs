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
using HTB.DevFx.ExceptionManagement;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// ת��������
	/// </summary>
	public abstract class Converting : IConverting
	{
		/// <summary>
		/// ��ת����ֵ
		/// </summary>
		protected abstract string ConvertingValue { get; }

		#region IConverting

		/// <summary>
		/// ֵת����String����
		/// </summary>
		/// <returns>String</returns>
		string IConverting.ToString() {
			return this.ConvertingValue;
		}

		/// <summary>
		/// ֵת����Byte����
		/// </summary>
		/// <returns>Byte</returns>
		byte IConverting.ToByte() {
			return Convert.ToByte(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Char����
		/// </summary>
		/// <returns>Char</returns>
		char IConverting.ToChar() {
			return Convert.ToChar(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����DateTime����
		/// </summary>
		/// <returns>DateTime</returns>
		DateTime IConverting.ToDateTime() {
			return Convert.ToDateTime(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Int16���ͣ�C#Ϊshort��
		/// </summary>
		/// <returns>Int16</returns>
		short IConverting.ToInt16() {
			return Convert.ToInt16(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Int32����
		/// </summary>
		/// <returns>Int32</returns>
		int IConverting.ToInt32() {
			return Convert.ToInt32(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Int64���ͣ�C#Ϊlong��
		/// </summary>
		/// <returns></returns>
		long IConverting.ToInt64() {
			return Convert.ToInt64(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����SByte����
		/// </summary>
		/// <returns>SByte</returns>
		sbyte IConverting.ToSByte() {
			return Convert.ToSByte(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Double����
		/// </summary>
		/// <returns>Double</returns>
		double IConverting.ToDouble() {
			return Convert.ToDouble(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Decimal����
		/// </summary>
		/// <returns>Decimal</returns>
		decimal IConverting.ToDecimal() {
			return Convert.ToDecimal(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Single���ͣ�C#Ϊfloat��
		/// </summary>
		/// <returns>Single</returns>
		float IConverting.ToSingle() {
			return Convert.ToSingle(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Single���ͣ�C#Ϊushort��
		/// </summary>
		/// <returns>UInt16</returns>
		ushort IConverting.ToUInt16() {
			return Convert.ToUInt16(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����UInt32���ͣ�C#Ϊuint��
		/// </summary>
		/// <returns>UInt32</returns>
		uint IConverting.ToUInt32() {
			return Convert.ToUInt32(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����UInt64���ͣ�C#Ϊulong��
		/// </summary>
		/// <returns>UInt64</returns>
		ulong IConverting.ToUInt64() {
			return Convert.ToUInt64(this.ConvertingValue);
		}

		/// <summary>
		/// ֵת����Boolean����
		/// </summary>
		/// <returns>Boolean</returns>
		bool IConverting.ToBoolean() {
			return Convert.ToBoolean(this.ConvertingValue);
		}

		/// <summary>
		/// ����������Type��Ϣ����ת����Type
		/// </summary>
		/// <returns>Type</returns>
		Type IConverting.ToType() {
			return (this as IConverting).ToType(true);
		}

		/// <summary>
		/// ����������Type��Ϣ����ת����Type
		/// </summary>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <returns>Type</returns>
		Type IConverting.ToType(bool throwError) {
			Type type = null;
			try {
				type = TypeHelper.CreateType(this.ConvertingValue, throwError);
			} catch(Exception e) {
				if(throwError) {
					throw new BaseException("ת����Typeʱ����", e);
				}
			}
			return type;
		}

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <returns>����</returns>
		object IConverting.ToObject() {
			return (this as IConverting).ToObject(null);
		}

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="parameters">���캯���Ĳ���</param>
		/// <returns>����</returns>
		object IConverting.ToObject(params object[] parameters) {
			return TypeHelper.CreateObject(this.ConvertingValue, null, true, parameters);
		}

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="expectedType">����������</param>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <param name="parameters">���캯���Ĳ���</param>
		/// <returns>����</returns>
		object IConverting.ToObject(Type expectedType, bool throwError, params object[] parameters) {
			return TypeHelper.CreateObject(this.ConvertingValue, expectedType, throwError, parameters);
		}

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="expectedType">����������</param>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <param name="paramTypes">���캯���Ĳ��������б�</param>
		/// <param name="paramValues">���캯���Ĳ����б�</param>
		/// <returns>����</returns>
		object IConverting.ToObject(Type expectedType, bool throwError, Type[] paramTypes, object[] paramValues) {
			return TypeHelper.CreateObject(this.ConvertingValue, expectedType, throwError, paramTypes, paramValues);
		}

		/// <summary>
		/// ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.ToObject<T>() {
			return (T)(this as IConverting).ToObject(typeof(T), true);
		}

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="parameters">�ɱ�����б�</param>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.ToObject<T>(params object[] parameters) {
			return (T)(this as IConverting).ToObject(typeof(T), true, parameters);
		}

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="throwError">���ʧ�ܣ��Ƿ��׳��쳣</param>
		/// <param name="parameters">�ɱ�����б�</param>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.ToObject<T>(bool throwError, params object[] parameters) {
			return (T)(this as IConverting).ToObject(typeof(T), throwError, parameters);
		}

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="throwError">���ʧ�ܣ��Ƿ��׳��쳣</param>
		/// <param name="paramTypes">���������б�</param>
		/// <param name="paramValues">����ֵ�б�</param>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.ToObject<T>(bool throwError, Type[] paramTypes, object[] paramValues) {
			return (T)(this as IConverting).ToObject(typeof(T), throwError, paramTypes, paramValues);
		}

		/// <summary>
		/// ����ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.TryToObject<T>() {
			return (this as IConverting).TryToObject<T>(default(T));
		}

		/// <summary>
		/// ����ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="defaultValue">���ת��ʧ�ܷ��ص�ȱʡֵ</param>
		/// <returns>ת���������ʵ��</returns>
		T IConverting.TryToObject<T>(T defaultValue) {
			try {
				return (T)Convert.ChangeType(this.ConvertingValue, typeof(T));
			} catch {
				return defaultValue;
			}
		}

		#endregion IConverting

		/// <summary>
		/// �ַ���ת����ö��
		/// </summary>
		/// <typeparam name="T">ö������</typeparam>
		/// <param name="enumString">�ַ���</param>
		/// <returns>ö��ֵ</returns>
		public static T StringToEnum<T>(string enumString) where T : struct {
			Type type = typeof(T);
			if (!type.IsEnum) {
				throw new ArgumentException("���ͱ�����ö��", "T");
			}
			Enum e;
			try {
				e = (Enum)Enum.Parse(type, enumString, true);
			} catch {
				e = default(T) as Enum;
			}
			return (T)Convert.ChangeType(e, type);
		}
	}
}