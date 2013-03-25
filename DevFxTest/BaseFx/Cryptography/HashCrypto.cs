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

using System.IO;
using System.Security.Cryptography;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Cryptography
{
	/// <summary>
	/// 关于Hash的一些实用方法
	/// </summary>
	public static class HashCrypto
	{
		/// <summary>
		/// Hash算法
		/// </summary>
		/// <param name="input">被Hash的字节数组</param>
		/// <param name="hashFormat">Hash算法："md5"、"sha1"</param>
		/// <returns>Hash结果字节数组</returns>
		/// <remarks>
		/// 当参数<paramref name="hashFormat">不为"md5"、"sha1"时，返回<c>null</c></paramref>
		/// </remarks>
		public static byte[] Hash(byte[] input, string hashFormat) {
			HashAlgorithm algorithm = null;
			if(string.Compare(hashFormat, "sha1", true) == 0) {
				algorithm = SHA1.Create();
			} else if(string.Compare(hashFormat, "md5", true) == 0) {
				algorithm = MD5.Create();
			}
			byte[] result = null;
			if(algorithm != null) {
				result = algorithm.ComputeHash(input);
			}
			return result;
		}

		/// <summary>
		/// Hash算法
		/// </summary>
		/// <param name="input">被Hash的字节流</param>
		/// <param name="hashFormat">Hash算法："md5"、"sha1"</param>
		/// <returns>Hash结果字节数组</returns>
		/// <remarks>
		/// 当参数<paramref name="hashFormat">不为"md5"、"sha1"时，返回<c>null</c></paramref>
		/// </remarks>
		public static byte[] Hash(Stream input, string hashFormat) {
			HashAlgorithm algorithm = null;
			if(string.Compare(hashFormat, "sha1", true) == 0) {
				algorithm = SHA1.Create();
			} else if(string.Compare(hashFormat, "md5", true) == 0) {
				algorithm = MD5.Create();
			}
			byte[] result = null;
			if(algorithm != null) {
				result = algorithm.ComputeHash(input);
			}
			return result;
		}

		/// <summary>
		/// Hash文件
		/// </summary>
		/// <param name="fileName">被Hash的文件（包括路径）</param>
		/// <param name="hashFormat">Hash算法："md5"、"sha1"</param>
		/// <returns>Hash结果字符串</returns>
		/// <remarks>
		/// 当参数<paramref name="hashFormat">不为"md5"、"sha1"时，返回<c>null</c></paramref>
		/// </remarks>
		public static string HashFile(string fileName, string hashFormat) {
			byte[] hashBytes = HashFileReturnRawData(fileName, hashFormat);
			if(hashBytes == null) {
				return null;
			} else {
				return WebHelper.ToHexString(hashBytes);
			}
		}

		/// <summary>
		/// Hash文件
		/// </summary>
		/// <param name="fileName">被Hash的文件（包括路径）</param>
		/// <param name="hashFormat">Hash算法："md5"、"sha1"</param>
		/// <returns>Hash结果</returns>
		/// <remarks>
		/// 当参数<paramref name="hashFormat">不为"md5"、"sha1"时，返回<c>null</c></paramref>
		/// </remarks>
		public static byte[] HashFileReturnRawData(string fileName, string hashFormat) {
			using(FileStream fs = File.OpenRead(fileName)) {
				return Hash(fs, hashFormat);
			}
		}
	}
}