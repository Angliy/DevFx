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
	/// 关于数据加解密的一些实用方法
	/// </summary>
	/// <remarks>
	/// 更多加解密实用方法请参见 <seealso cref="HTB.DevFx.Cryptography"/> 命名空间
	/// </remarks>
	public static class Encryption
	{
		/// <summary>
		/// Hash算法，提供MD5、SHA1算法
		/// </summary>
		/// <param name="encryptingString">被Hash的字符串</param>
		/// <param name="encryptFormat">Hash算法，有"md5"、"sha1"、"clear"（明文，即不加密）等</param>
		/// <returns>Hash结果字符串</returns>
		/// <remarks>
		/// 当参数<paramref name="encryptFormat" />不为"md5"、"sha1"、"clear"时，直接返回参数<paramref name="encryptingString" />
		/// </remarks>
		public static string Encrypt(string encryptingString, string encryptFormat) {
			if(string.Compare(encryptFormat, "md5", true) == 0 || string.Compare(encryptFormat, "sha1", true) == 0) {
				return FormsAuthentication.HashPasswordForStoringInConfigFile(encryptingString, encryptFormat);
			}
			return encryptingString;
		}
	}
}