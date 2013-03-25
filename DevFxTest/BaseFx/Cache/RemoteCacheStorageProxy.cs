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
using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// Զ�̻���洢������
	/// </summary>
	/// <remarks>
	///	Զ�̻���洢��������ã�
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="����������" type="����������" interval="�����ڵ�ʱ������0��С��0��ʾ�����м��"&gt;
	///							&lt;!--�������û���洢��--&gt;
	///							&lt;cacheStorage type="HTB.DevFx.Cache.RemoteCacheStorageProxy" url="Զ�˶�������ã����磺tcp://localhost:8085/RemoteCacheStorage" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public class RemoteCacheStorageProxy : ICacheStorage
	{
		#region private members
		
		private ICacheStorage remoteStorage;

		private bool isInit = false;
		private string remoteUrl = null;

		private void GetRemoteObject() {
			if(this.remoteStorage == null) {
				this.remoteStorage = (ICacheStorage)Activator.GetObject(typeof(ICacheStorage), this.remoteUrl);
			}
		}

		private bool RemotingIsReady() {
			if(!this.isInit) {
				return false;
			}
			this.GetRemoteObject();
			if(this.remoteStorage == null) {
				return false;
			}
			try {
				this.remoteStorage.ToString();
				return true;
			} catch {
				//Exceptor.Publish(new CacheException("Զ�̷����������ã����飡", e), LogLevel.EMERGENCY);
				return false;
			}
		}

		#endregion

		#region ICacheStorage Members

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public void Init(IConfigSetting setting) {
			if(!this.isInit) {
				this.remoteUrl = setting.Property["url"].Value;
				this.GetRemoteObject();
				this.isInit = true;
			}
		}

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public object this[string key] {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage[key];
				} else {
					return null;
				}
			}
			set {
				if(this.RemotingIsReady()) {
					this.remoteStorage[key] = value;
				}
			}
		}

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		public object this[int index] {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage[index];
				} else {
					return null;
				}
			}
			set {
				if(this.RemotingIsReady()) {
					this.remoteStorage[index] = value;
				}
			}
		}

		/// <summary>
		/// ��ȡ�˴洢�����洢��ĸ���
		/// </summary>
		/// <remarks>
		/// ���Զ�˶���û��׼���ã��򷵻�-1
		/// </remarks>
		public int Count {
			get {
				if(this.RemotingIsReady()) {
					return this.remoteStorage.Count;
				} else {
					return -1;
				}
			}
		}

		/// <summary>
		/// ���һ��洢����
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ���������ͬ�Ľ�ֵ������´洢�Ķ���
		/// </remarks>
		public void Add(string key, object @value) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Add(key, @value);
			}
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(string key) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Get(key);
			} else {
				return null;
			}
		}

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		public object Get(int index) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Get(index);
			} else {
				return null;
			}
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
			if(this.RemotingIsReady()) {
				this.remoteStorage.Set(key, @value);
			}
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
			if(this.RemotingIsReady()) {
				this.remoteStorage.Set(index, @value);
			}
		}

		/// <summary>
		/// �Ƴ��洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		public void Remove(string key) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Remove(key);
			}
		}

		/// <summary>
		/// ��ָ����λ���Ƴ��洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		public void RemoveAt(int index) {
			if(this.RemotingIsReady()) {
				this.remoteStorage.RemoveAt(index);
			}
		}

		/// <summary>
		/// �жϴ洢�����Ƿ����ָ����ֵ�Ĵ洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>��/��</returns>
		public bool Contains(string key) {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.Contains(key);
			} else {
				return false;
			}
		}

		/// <summary>
		/// ����˴洢�������е���
		/// </summary>
		public void Clear() {
			if(this.RemotingIsReady()) {
				this.remoteStorage.Clear();
			}
		}

		/// <summary>
		/// ��ô˴洢����������Ľ�ֵ
		/// </summary>
		/// <returns>��ֵ�б����飩</returns>
		public string[] GetAllKeys() {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.GetAllKeys();
			} else {
				return null;
			}
		}

		/// <summary>
		/// ��ȡ�˴洢�����������ֵ
		/// </summary>
		/// <returns>�洢���б����飩</returns>
		public object[] GetAllValues() {
			if(this.RemotingIsReady()) {
				return this.remoteStorage.GetAllValues();
			} else {
				return null;
			}
		}

		#endregion
	}
}