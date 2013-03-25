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

using System.Security.Cryptography;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// �����ʵ����
	/// </summary>
	public static class RandomHelper
	{
		/// <summary>
		/// ��ȡ����ֽ�����
		/// </summary>
		/// <param name="length">�ֽ����еĳ���</param>
		/// <returns>�ֽ�����</returns>
		public static byte[] GetRandomBytes(int length) {
			if(length <= 0) {
				return new byte[0];
			}
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] ret = new byte[length];
			rng.GetNonZeroBytes(ret);
			return ret;
		}

		/// <summary>
		/// ȱʡ���ַ���ȡֵ��Χ
		/// </summary>
		public const string DEFAULT_CHARLIST = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		/// <summary>
		/// �ɶ����ַ���ȡֵ��Χ
		/// </summary>
		public const string READ_CHARLIST  = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// ��ȡ����ַ���
		/// </summary>
		/// <param name="length">�ַ�������</param>
		/// <param name="charList">�ַ���ȡֵ��Χ�����ΪNull��Ϊ�գ��򷵻ؿ��ַ�����</param>
		/// <returns>����ַ���</returns>
		public static string GetRandomString(int length, string charList) {
			if(length <= 0 || Checker.CheckEmptyString("charList", charList, false)) {
				return string.Empty;
			}
			int num = charList.Length;
			char[] ret = new char[length];
			byte[] rnd = GetRandomBytes(length);
			for(int i = 0; i < rnd.Length; i++) {
				ret[i] = charList[rnd[i] % num];
			}
			return new string(ret);
		}

		/// <summary>
		/// ��ȡ����ַ���
		/// </summary>
		/// <param name="length">�ַ�������</param>
		/// <returns>����ַ���</returns>
		/// <remarks>
		/// ȱʡʹ��ASCII��33��126��94���ַ���Ϊȡֵ��Χ
		/// </remarks>
		public static string GetRandomString(int length) {
			return GetRandomString(length, DEFAULT_CHARLIST);
		}
	}
}
