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

using System.Web.Security;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// �������ݼӽ��ܵ�һЩʵ�÷���
	/// </summary>
	/// <remarks>
	/// ����ӽ���ʵ�÷�����μ� <seealso cref="HTB.DevFx.Cryptography"/> �����ռ�
	/// </remarks>
	public static class Encryption
	{
		/// <summary>
		/// Hash�㷨���ṩMD5��SHA1�㷨
		/// </summary>
		/// <param name="encryptingString">��Hash���ַ���</param>
		/// <param name="encryptFormat">Hash�㷨����"md5"��"sha1"��"clear"�����ģ��������ܣ���</param>
		/// <returns>Hash����ַ���</returns>
		/// <remarks>
		/// ������<paramref name="encryptFormat" />��Ϊ"md5"��"sha1"��"clear"ʱ��ֱ�ӷ��ز���<paramref name="encryptingString" />
		/// </remarks>
		public static string Encrypt(string encryptingString, string encryptFormat) {
			if(string.Compare(encryptFormat, "md5", true) == 0 || string.Compare(encryptFormat, "sha1", true) == 0) {
				return FormsAuthentication.HashPasswordForStoringInConfigFile(encryptingString, encryptFormat);
			}
			return encryptingString;
		}
	}
}