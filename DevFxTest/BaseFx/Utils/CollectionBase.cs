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
using System.Collections.Generic;
using System.Collections.Specialized;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// ���ϻ����ࣨ���ͣ�
	/// </summary>
	/// <typeparam name="T">�����ռ����������</typeparam>
	public class CollectionBase<T> : NameObjectCollectionBase
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public CollectionBase() : this(false) {}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="uniqueKey">���Ƿ�����ΪΨһ</param>
		public CollectionBase(bool uniqueKey) : base() {
			this.uniqueKey = uniqueKey;
		}

		private bool uniqueKey;

		/// <summary>
		/// ���Ƿ�����ΪΨһ
		/// </summary>
		public virtual bool UniqueKey {
			get { return this.uniqueKey; }
			protected set { this.uniqueKey = value; }
		}

		/// <summary>
		/// �������ķ�ʽ��ȡ��
		/// </summary>
		/// <param name="index">����</param>
		public virtual T this[int index] {
			get { return (T)this.BaseGet(index); }
		}

		/// <summary>
		/// ����ֵ��ʽ��ȡ��
		/// </summary>
		/// <param name="key">��ֵ</param>
		public virtual T this[string key] {
			get { return (T)base.BaseGet(key); }
		}

		/// <summary>
		/// ���һ�������
		/// </summary>
		/// <param name="key">��ֵ</param>
		/// <param name="value">��</param>
		public virtual void Add(string key, T @value) {
			if(this.UniqueKey && this.Contains(key)) {
				throw new Exception("�Ѵ��ڼ�ֵ��" + key);
			}
			this.BaseAdd(key, @value);
		}

		/// <summary>
		/// ���/�滻һ�������
		/// </summary>
		/// <param name="key">��ֵ</param>
		/// <param name="value">��</param>
		public virtual void Set(string key, T @value) {
			this.BaseSet(key, @value);
		}

		/// <summary>
		/// �������Ƿ����ĳ��
		/// </summary>
		/// <param name="key">��ֵ</param>
		/// <returns>��/��</returns>
		public virtual bool Contains(string key) {
			return (this.BaseGet(key) != null);
		}

		/// <summary>
		/// �Ƴ�ĳ��
		/// </summary>
		/// <param name="key">��ֵ</param>
		public virtual void Remove(string key) {
			this.BaseRemove(key);
		}

		/// <summary>
		/// ��ָ�����Ƴ�ĳ��
		/// </summary>
		/// <param name="index">����</param>
		public virtual void RemoveAt(int index) {
			this.BaseRemoveAt(index);
		}

		/// <summary>
		/// ��ռ���������Ԫ��
		/// </summary>
		public virtual void Clear() {
			this.BaseClear();
		}

		/// <summary>
		/// ���Ƶ�������
		/// </summary>
		/// <returns>����</returns>
		public virtual T[] CopyToArray() {
			return base.BaseGetAllValues(typeof(T)) as T[];
		}

		/// <summary>
		/// ��ȡ������ͬKey����
		/// </summary>
		/// <param name="key">��ֵ</param>
		/// <returns>������ͬKey����</returns>
		public virtual T[] GetItem(string key) {
			List<T> list = new List<T>();
			for(int i = 0; i < this.Count; i++) {
				if(string.Compare(this.Keys[i], key, true) == 0) {
					list.Add(this[i]);
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// ��ȡ����ֵ
		/// </summary>
		public virtual T[] Values {
			get { return base.BaseGetAllValues(typeof(T)) as T[]; }
		}
	}
}