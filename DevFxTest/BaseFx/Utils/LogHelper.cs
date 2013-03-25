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
using System.IO;
using System.Text;
using HTB.DevFx.Log;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// 记录日志的实用类
	/// </summary>
	/// <remarks>
	/// 注意与 <see cref="LoggorHelper"/> 的区别
	/// </remarks>
	public static class LogHelper
	{
		private static object lockObject = new object();

		/// <summary>
		/// 向指定文件添加文本信息
		/// </summary>
		/// <param name="logPath">所在路径</param>
		/// <param name="fileName">文本文件名（支持DateTime格式）</param>
		/// <param name="msgs">信息列表</param>
		public static void WriteLog(string logPath, string fileName, params object[] msgs) {
			fileName = string.Format("{0}\\{1}", logPath, DateTime.Now.ToString(fileName));
			WriteLog(fileName, msgs);
		}

		/// <summary>
		/// 向指定文件添加文本信息
		/// </summary>
		/// <param name="fileName">全路径文本文件名</param>
		/// <param name="msgs">信息列表</param>
		public static void WriteLog(string fileName, params object[] msgs) {
			FileInfo file = new FileInfo(fileName);
			if(!file.Directory.Exists) {
				Directory.CreateDirectory(file.Directory.FullName);
			}
			if(msgs == null || msgs.Length == 0) {
				return;
			}
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < msgs.Length; i++) {
				sb.AppendFormat("{{{0}}}\r\n", i);
			}
			string messageFormat = sb.ToString();
			lock(lockObject) {
				File.AppendAllText(fileName, string.Format(messageFormat, msgs));
			}
		}

		/// <summary>
		/// 向指定文件添加文本信息
		/// </summary>
		/// <param name="fileName">全路径文本文件名</param>
		/// <param name="message">文本信息</param>
		public static void WriteLog(string fileName, string message) {
			WriteLog(fileName, (object)message);
		}
	}
}
