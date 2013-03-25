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

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// 关于参数验证的一些实用方法
	/// </summary>
	public static class Checker
	{
		/// <summary>
		/// 检查字符串是否为空（null或者长度为0）
		/// </summary>
		/// <param name="argName">字符串名</param>
		/// <param name="argValue">被检查的字符串</param>
		/// <param name="throwError">为空时是否抛出异常</param>
		/// <returns>为空则返回true</returns>
		public static bool CheckEmptyString(string argName, string argValue, bool throwError) {
			CheckArgumentNull("argName", argName, true);
			bool ret = (argValue == null || argValue.Length == 0);
			if(ret && throwError) {
				throw new ArgumentException("字符串为空", argName);
			}
			return ret;
		}

		/// <summary>
		/// 检查参数是否为空引用（null）
		/// </summary>
		/// <param name="argName">参数名</param>
		/// <param name="argValue">被检查的参数</param>
		/// <param name="throwError">为空引用时是否抛出异常</param>
		/// <returns>为空则返回true</returns>
		public static bool CheckArgumentNull(string argName, object argValue, bool throwError) {
			if(argName == null) {
				throw new ArgumentNullException("argName");
			}
			if(argValue == null) {
				if(throwError) {
					throw new ArgumentNullException(argName);
				} else {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 检查数组是否为空（长度为0）
		/// </summary>
		/// <param name="argName">数组名</param>
		/// <param name="argValue">被检查的数组实例</param>
		/// <param name="throwError">为空引用时是否抛出异常</param>
		/// <returns>为空则返回true</returns>
		public static bool CheckEmptyArray(string argName, Array argValue, bool throwError) {
			if(argName == null) {
				throw new ArgumentNullException("argName");
			}
			bool ret = (argValue == null || argValue.Length == 0);
			if(ret && throwError) {
				throw new ArgumentException("数组为空", argName);
			}
			return ret;
		}

		/// <summary>
		/// 判断某值是否在枚举内（位枚举）
		/// </summary>
		/// <param name="checkingValue">被检测的枚举值</param>
		/// <param name="expectedValue">期望的枚举值</param>
		/// <returns>是否包含</returns>
		public static bool CheckFlagsEnumEquals(Enum checkingValue, Enum expectedValue) {
			int intCheckingValue = Convert.ToInt32(checkingValue);
			int intExpectedValue = Convert.ToInt32(expectedValue);
			return (intCheckingValue & intExpectedValue) == intExpectedValue;
		}
	}
}
