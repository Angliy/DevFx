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

using System.Collections.Specialized;
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// �ڴ����ݴ洢��ʽ�Ĵ洢��
	/// </summary>
	public class NullCacheStorage : NameObjectCollectionBase, ICacheStorage
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public NullCacheStorage() : base() {
			this.IsReadOnly = false;
		}

		#region ICacheStorage Members

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public void Init(IConfigSetting setting) {
		}

		/// <summary>
		/// ��������ʽ��ȡ����һ��洢ֵ
		/// </summary>
		/// <param name="index">�洢�������</param>
		public object this[int index] {
			get { return this.Get(index); }
			set { this.Set(index, value); }
		}

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public object this[string key] {
			get { return this.Get(key); }
			set { this.Set(key, value); }
		}

		/// <summary>
		/// ���һ��洢����
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		public void Add(string key, object value) {
			this.BaseAdd(key, value);
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(string key) {
			return this.BaseGet(key);
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(int index) {
			return this.BaseGet(index);
		}

		/// <summary>
		/// ���ô洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ����Դ��ڴ洢��������ڣ��򲻽����κβ���
		/// </remarks>
		public void Set(string key, object @value) {
			this.BaseSet(key, @value);
		}

		/// <summary>
		/// ���ô洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ����Դ��ڴ洢��������ڣ��򲻽����κβ���
		/// </remarks>
		public void Set(int index, object @value) {
			this.BaseSet(index, @value);
		}

		/// <summary>
		/// �Ƴ��洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public void Remove(string key) {
			this.BaseRemove(key);
		}

		/// <summary>
		/// ��ָ����λ���Ƴ��洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		public void RemoveAt(int index) {
			this.BaseRemoveAt(index);
		}

		/// <summary>
		/// �жϴ洢�����Ƿ����ָ����ֵ�Ĵ洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>��/��</returns>
		public bool Contains(string key) {
			return this.BaseGet(key) == null;
		}

		/// <summary>
		/// ����˴洢�������е���
		/// </summary>
		public void Clear() {
			this.BaseClear();
		}

		/// <summary>
		/// ��ô˴洢����������Ľ�ֵ
		/// </summary>
		/// <returns>��ֵ�б����飩</returns>
		public string[] GetAllKeys() {
			return this.BaseGetAllKeys();
		}

		/// <summary>
		/// ��ȡ�˴洢�����������ֵ
		/// </summary>
		/// <returns>�洢���б����飩</returns>
		public object[] GetAllValues() {
			return this.BaseGetAllValues();
		}

		#endregion
	}
}
