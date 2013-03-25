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
	/// ���ڲ�����֤��һЩʵ�÷���
	/// </summary>
	public static class Checker
	{
		/// <summary>
		/// ����ַ����Ƿ�Ϊ�գ�null���߳���Ϊ0��
		/// </summary>
		/// <param name="argName">�ַ�����</param>
		/// <param name="argValue">�������ַ���</param>
		/// <param name="throwError">Ϊ��ʱ�Ƿ��׳��쳣</param>
		/// <returns>Ϊ���򷵻�true</returns>
		public static bool CheckEmptyString(string argName, string argValue, bool throwError) {
			CheckArgumentNull("argName", argName, true);
			bool ret = (argValue == null || argValue.Length == 0);
			if(ret && throwError) {
				throw new ArgumentException("�ַ���Ϊ��", argName);
			}
			return ret;
		}

		/// <summary>
		/// �������Ƿ�Ϊ�����ã�null��
		/// </summary>
		/// <param name="argName">������</param>
		/// <param name="argValue">�����Ĳ���</param>
		/// <param name="throwError">Ϊ������ʱ�Ƿ��׳��쳣</param>
		/// <returns>Ϊ���򷵻�true</returns>
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
		/// ��������Ƿ�Ϊ�գ�����Ϊ0��
		/// </summary>
		/// <param name="argName">������</param>
		/// <param name="argValue">����������ʵ��</param>
		/// <param name="throwError">Ϊ������ʱ�Ƿ��׳��쳣</param>
		/// <returns>Ϊ���򷵻�true</returns>
		public static bool CheckEmptyArray(string argName, Array argValue, bool throwError) {
			if(argName == null) {
				throw new ArgumentNullException("argName");
			}
			bool ret = (argValue == null || argValue.Length == 0);
			if(ret && throwError) {
				throw new ArgumentException("����Ϊ��", argName);
			}
			return ret;
		}

		/// <summary>
		/// �ж�ĳֵ�Ƿ���ö���ڣ�λö�٣�
		/// </summary>
		/// <param name="checkingValue">������ö��ֵ</param>
		/// <param name="expectedValue">������ö��ֵ</param>
		/// <returns>�Ƿ����</returns>
		public static bool CheckFlagsEnumEquals(Enum checkingValue, Enum expectedValue) {
			int intCheckingValue = Convert.ToInt32(checkingValue);
			int intExpectedValue = Convert.ToInt32(expectedValue);
			return (intCheckingValue & intExpectedValue) == intExpectedValue;
		}
	}
}
