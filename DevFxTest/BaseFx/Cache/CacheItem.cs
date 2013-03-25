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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// ������İ�װ��
	/// </summary>
	[Serializable]
	public class CacheItem
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="key">�����ֵ</param>
		/// <param name="value">������ֵ</param>
		/// <param name="cacheDependency">���ڲ���</param>
		public CacheItem(string key, object @value, ICacheDependency cacheDependency) {
			this.key = key;
			this.value = @value;
			this.cacheDependency = cacheDependency;
			this.hits = 0;
			this.lastAccessTime = DateTime.Now;
		}

		private string key;
		private object @value;
		private ICacheDependency cacheDependency;
		private int hits;
		private DateTime lastAccessTime;

		/// <summary>
		/// ��ȡ���ڲ���
		/// </summary>
		public ICacheDependency CacheDependency {
			get { return this.cacheDependency; }
		}

		/// <summary>
		/// ��ȡ��ֵ
		/// </summary>
		public string Key {
			get { return this.key; }
		}

		/// <summary>
		/// ��ȡ������
		/// </summary>
		public object Value {
			get { return this.value; }
			set { this.value = value; }
		}

		/// <summary>
		/// ���д���
		/// </summary>
		public int Hits {
			get { return this.hits; }
			set { this.hits = value; }
		}

		/// <summary>
		/// �������ʱ��
		/// </summary>
		public DateTime LastAccessTime {
			get { return this.lastAccessTime; }
			set { this.lastAccessTime = value; }
		}
	}
}
