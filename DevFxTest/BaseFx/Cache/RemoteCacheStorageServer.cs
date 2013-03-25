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
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// �洢��Զ�ˣ�����������
	/// </summary>
	/// <remarks>
	/// ��� <see cref="RemoteCacheStorageProxy"/>���Ѵ洢���ݴ洢��Զ��<br />
	/// ע�⣺Ӧ�ðѱ�������ΪSingletonģʽ
	/// </remarks>
	public class RemoteCacheStorageServer : MarshalByRefObject, ICacheStorage
	{
		#region private members

		private static List<RemoteCacheStorageServer> instances = new List<RemoteCacheStorageServer>();

		private ICacheStorage cacheStorage = new NullCacheStorage();

		#endregion

		#region constructors

		private RemoteCacheStorageServer() {
			instances.Add(this);
		}

		#endregion

		#region public static members

		/// <summary>
		/// ��ȡ�����ʵ���б�
		/// </summary>
		/// <remarks>
		/// ���ô����ԣ����԰ѿͻ�����ѯ����ʱ������Ϊ-1��Ȼ���ڷ������˽�����ѯ�������Ч��
		/// </remarks>
		public static RemoteCacheStorageServer[] Instances {
			get {
				return instances.ToArray();
			}
		}

		#endregion

		#region ICacheStorage Members

		/// <summary>
		/// ���һ��洢����
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ���������ͬ�Ľ�ֵ������´洢�Ķ���
		/// </remarks>
		public void Add(string key, object value) {
			this.cacheStorage.Add(key, value);
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(string key) {
			return this.cacheStorage.Get(key);
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(int index) {
			return this.cacheStorage.Get(index);
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
			this.cacheStorage.Set(key, @value);
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
			this.cacheStorage.Set(index, @value);
		}

		/// <summary>
		/// �Ƴ��洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public void Remove(string key) {
			this.cacheStorage.Remove(key);
		}

		/// <summary>
		/// ��ָ����λ���Ƴ��洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		public void RemoveAt(int index) {
			this.cacheStorage.RemoveAt(index);
		}

		/// <summary>
		/// �жϴ洢�����Ƿ����ָ����ֵ�Ĵ洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>��/��</returns>
		public bool Contains(string key) {
			return this.cacheStorage.Contains(key);
		}

		/// <summary>
		/// ����˴洢�������е���
		/// </summary>
		public void Clear() {
			this.cacheStorage.Clear();
		}

		/// <summary>
		/// ��ô˴洢����������Ľ�ֵ
		/// </summary>
		/// <returns>��ֵ�б����飩</returns>
		public string[] GetAllKeys() {
			return this.cacheStorage.GetAllKeys();
		}

		/// <summary>
		/// ��ȡ�˴洢�����������ֵ
		/// </summary>
		/// <returns>�洢���б����飩</returns>
		public object[] GetAllValues() {
			return this.cacheStorage.GetAllValues();
		}

		/// <summary>
		/// ��ȡ�˴洢�����洢��ĸ���
		/// </summary>
		public int Count {
			get { return this.cacheStorage.Count; }
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public void Init(IConfigSetting setting) {
			this.cacheStorage.Init(setting);
		}

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public object this[string key] {
			get { return this.cacheStorage[key]; }
			set { this.cacheStorage[key] = value; }
		}

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		public object this[int index] {
			get { return this.cacheStorage[index]; }
			set { this.cacheStorage[index] = value; }
		}

		#endregion
	}
}
