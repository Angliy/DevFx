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

using HTB.DevFx.Config;

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// �������Ĵ洢�ӿ�
	/// </summary>
	/// <remarks>
	///	�������洢�ӿڵ����ã�
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="����������" type="����������" interval="�����ڵ�ʱ������0��С��0��ʾ�����м��"&gt;
	///							&lt;!--�������û���洢��--&gt;
	///							&lt;cacheStorage type="ʵ�ֻ���洢���ӿڵ�����" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICacheStorage
	{
		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		void Init(IConfigSetting setting);
		
		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		object this[string key] { get; set; }

		/// <summary>
		/// ��ȡ����һ���洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		object this[int index] { get; set; }

		/// <summary>
		/// ��ȡ�˴洢�����洢��ĸ���
		/// </summary>
		int Count { get; }

		/// <summary>
		/// ���һ��洢����
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ���������ͬ�Ľ�ֵ������´洢�Ķ���
		/// </remarks>
		void Add(string key, object @value);

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		object Get(string key);

		/// <summary>
		/// ��ȡ�洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <returns>�洢�Ķ�������洢��û�����У��򷵻�<c>null</c></returns>
		object Get(int index);

		/// <summary>
		/// ���ô洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ����Դ��ڴ洢��������ڣ��򲻽����κβ���
		/// </remarks>
		void Set(string key, object @value);

		/// <summary>
		/// ���ô洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		/// <param name="value">�洢�Ķ���</param>
		/// <remarks>
		/// ����Դ��ڴ洢��������ڣ��򲻽����κβ���
		/// </remarks>
		void Set(int index, object @value);

		/// <summary>
		/// �Ƴ��洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		void Remove(string key);

		/// <summary>
		/// ��ָ����λ���Ƴ��洢��
		/// </summary>
		/// <param name="index">�洢�������ֵ</param>
		void RemoveAt(int index);

		/// <summary>
		/// �жϴ洢�����Ƿ����ָ����ֵ�Ĵ洢��
		/// </summary>
		/// <param name="key">�洢��Ľ�ֵ</param>
		/// <returns>��/��</returns>
		bool Contains(string key);

		/// <summary>
		/// ����˴洢�������е���
		/// </summary>
		void Clear();

		/// <summary>
		/// ��ô˴洢����������Ľ�ֵ
		/// </summary>
		/// <returns>��ֵ�б����飩</returns>
		string[] GetAllKeys();

		/// <summary>
		/// ��ȡ�˴洢�����������ֵ
		/// </summary>
		/// <returns>�洢���б����飩</returns>
		object[] GetAllValues();
	}
}
