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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// �ļ�������ʽ�Ĺ��ڲ���
	/// </summary>
	[Serializable]
	public class FileCacheDependency : ICacheDependency
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="fileName">��Ҫ���ӵ��ļ���������·����</param>
		/// <param name="filters">���ӷ�ʽ</param>
		public FileCacheDependency(string fileName, NotifyFilters filters) {
			this.fileName = fileName;
			this.filters = filters;
			this.init();
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="fileName">��Ҫ���ӵ��ļ���������·����</param>
		/// <remarks>
		/// ���ӷ�ʽĬ��Ϊ�ļ������д�루�޸ģ�ʱ��
		/// </remarks>
		public FileCacheDependency(string fileName) : this(fileName, NotifyFilters.LastWrite) {
		}

		private string fileName;
		private FileSystemWatcher fileWatcher;
		private NotifyFilters filters;
		private bool isExpired = false;

		private void init() {
			string fileName = Path.GetFileName(this.fileName);
			string filePath = Path.GetDirectoryName(this.fileName);
			this.fileWatcher = new FileSystemWatcher(filePath, fileName);
			this.fileWatcher.NotifyFilter = this.filters;
			this.fileWatcher.Changed += new FileSystemEventHandler(this.FileChanged);
			this.fileWatcher.EnableRaisingEvents = true;
		}

		private void FileChanged(object sender, FileSystemEventArgs e) {
			this.fileWatcher.EnableRaisingEvents = false;
			this.isExpired = true;
			this.fileWatcher.Dispose();
			this.fileWatcher = null;
		}

		#region ICacheDependency Members

		/// <summary>
		/// �Ƿ��ѹ���
		/// </summary>
		public bool IsExpired {
			get { return this.isExpired; }
		}

		/// <summary>
		/// ���û�����ԣ��൱�����¿�ʼ���棩
		/// </summary>
		public void Reset() {
		}

		#endregion
	}
}
