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
	/// 转换抽象类
	/// </summary>
	public abstract class Converting : IConverting
	{
		/// <summary>
		/// 被转换的值
		/// </summary>
		protected abstract string ConvertingValue { get; }

		#region IConverting

		/// <summary>
		/// 值转换成String类型
		/// </summary>
		/// <returns>String</returns>
		string IConverting.ToString() {
			return this.ConvertingValue;
		}

		/// <summary>
		/// 值转换成Byte类型
		/// </summary>
		/// <returns>Byte</returns>
		byte IConverting.ToByte() {
			return Convert.ToByte(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Char类型
		/// </summary>
		/// <returns>Char</returns>
		char IConverting.ToChar() {
			return Convert.ToChar(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成DateTime类型
		/// </summary>
		/// <returns>DateTime</returns>
		DateTime IConverting.ToDateTime() {
			return Convert.ToDateTime(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Int16类型（C#为short）
		/// </summary>
		/// <returns>Int16</returns>
		short IConverting.ToInt16() {
			return Convert.ToInt16(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Int32类型
		/// </summary>
		/// <returns>Int32</returns>
		int IConverting.ToInt32() {
			return Convert.ToInt32(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Int64类型（C#为long）
		/// </summary>
		/// <returns></returns>
		long IConverting.ToInt64() {
			return Convert.ToInt64(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成SByte类型
		/// </summary>
		/// <returns>SByte</returns>
		sbyte IConverting.ToSByte() {
			return Convert.ToSByte(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Double类型
		/// </summary>
		/// <returns>Double</returns>
		double IConverting.ToDouble() {
			return Convert.ToDouble(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Decimal类型
		/// </summary>
		/// <returns>Decimal</returns>
		decimal IConverting.ToDecimal() {
			return Convert.ToDecimal(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Single类型（C#为float）
		/// </summary>
		/// <returns>Single</returns>
		float IConverting.ToSingle() {
			return Convert.ToSingle(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Single类型（C#为ushort）
		/// </summary>
		/// <returns>UInt16</returns>
		ushort IConverting.ToUInt16() {
			return Convert.ToUInt16(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成UInt32类型（C#为uint）
		/// </summary>
		/// <returns>UInt32</returns>
		uint IConverting.ToUInt32() {
			return Convert.ToUInt32(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成UInt64类型（C#为ulong）
		/// </summary>
		/// <returns>UInt64</returns>
		ulong IConverting.ToUInt64() {
			return Convert.ToUInt64(this.ConvertingValue);
		}

		/// <summary>
		/// 值转换成Boolean类型
		/// </summary>
		/// <returns>Boolean</returns>
		bool IConverting.ToBoolean() {
			return Convert.ToBoolean(this.ConvertingValue);
		}

		/// <summary>
		/// 如果保存的是Type信息，则转换成Type
		/// </summary>
		/// <returns>Type</returns>
		Type IConverting.ToType() {
			return (this as IConverting).ToType(true);
		}

		/// <summary>
		/// 如果保存的是Type信息，则转换成Type
		/// </summary>
		/// <param name="throwError">是否抛出异常</param>
		/// <returns>Type</returns>
		Type IConverting.ToType(bool throwError) {
			Type type = null;
			try {
				type = TypeHelper.CreateType(this.ConvertingValue, throwError);
			} catch(Exception e) {
				if(throwError) {
					throw new BaseException("转换成Type时错误", e);
				}
			}
			return type;
		}

		/// <summary>
		/// 根据值创建对象
		/// </summary>
		/// <returns>对象</returns>
		object IConverting.ToObject() {
			return (this as IConverting).ToObject(null);
		}

		/// <summary>
		/// 根据值创建对象
		/// </summary>
		/// <param name="parameters">构造函数的参数</param>
		/// <returns>对象</returns>
		object IConverting.ToObject(params object[] parameters) {
			return TypeHelper.CreateObject(this.ConvertingValue, null, true, parameters);
		}

		/// <summary>
		/// 根据值创建对象
		/// </summary>
		/// <param name="expectedType">期望的类型</param>
		/// <param name="throwError">是否抛出异常</param>
		/// <param name="parameters">构造函数的参数</param>
		/// <returns>对象</returns>
		object IConverting.ToObject(Type expectedType, bool throwError, params object[] parameters) {
			return TypeHelper.CreateObject(this.ConvertingValue, expectedType, throwError, parameters);
		}

		/// <summary>
		/// 根据值创建对象
		/// </summary>
		/// <param name="expectedType">期望的类型</param>
		/// <param name="throwError">是否抛出异常</param>
		/// <param name="paramTypes">构造函数的参数类型列表</param>
		/// <param name="paramValues">构造函数的参数列表</param>
		/// <returns>对象</returns>
		object IConverting.ToObject(Type expectedType, bool throwError, Type[] paramTypes, object[] paramValues) {
			return TypeHelper.CreateObject(this.ConvertingValue, expectedType, throwError, paramTypes, paramValues);
		}

		/// <summary>
		/// 转换成指定类型
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <returns>转换后的类型实例</returns>
		T IConverting.ToObject<T>() {
			return (T)(this as IConverting).ToObject(typeof(T), true);
		}

		/// <summary>
		/// 转换成指定类型（带参数）
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <param name="parameters">可变参数列表</param>
		/// <returns>转换后的类型实例</returns>
		T IConverting.ToObject<T>(params object[] parameters) {
			return (T)(this as IConverting).ToObject(typeof(T), true, parameters);
		}

		/// <summary>
		/// 转换成指定类型（带参数）
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <param name="throwError">如果失败，是否抛出异常</param>
		/// <param name="parameters">可变参数列表</param>
		/// <returns>转换后的类型实例</returns>
		T IConverting.ToObject<T>(bool throwError, params object[] parameters) {
			return (T)(this as IConverting).ToObject(typeof(T), throwError, parameters);
		}

		/// <summary>
		/// 转换成指定类型（带参数）
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <param name="throwError">如果失败，是否抛出异常</param>
		/// <param name="paramTypes">参数类型列表</param>
		/// <param name="paramValues">参数值列表</param>
		/// <returns>转换后的类型实例</returns>
		T IConverting.ToObject<T>(bool throwError, Type[] paramTypes, object[] paramValues) {
			return (T)(this as IConverting).ToObject(typeof(T), throwError, paramTypes, paramValues);
		}

		/// <summary>
		/// 尝试转换成指定类型
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <returns>转换后的类型实例</returns>
		T IConverting.TryToObject<T>() {
			return (this as IConverting).TryToObject<T>(default(T));
		}

		/// <summary>
		/// 尝试转换成指定类型
		/// </summary>
		/// <typeparam name="T">转换成的类型</typeparam>
		/// <param name="defaultValue">如果转换失败返回的缺省值</param>
		/// <returns>转换后的类型实例</returns>
		T IConverting.TryToObject<T>(T defaultValue) {
			try {
				return (T)Convert.ChangeType(this.ConvertingValue, typeof(T));
			} catch {
				return defaultValue;
			}
		}

		#endregion IConverting

		/// <summary>
		/// 字符串转换成枚举
		/// </summary>
		/// <typeparam name="T">枚举类型</typeparam>
		/// <param name="enumString">字符串</param>
		/// <returns>枚举值</returns>
		public static T StringToEnum<T>(string enumString) where T : struct {
			Type type = typeof(T);
			if (!type.IsEnum) {
				throw new ArgumentException("类型必须是枚举", "T");
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