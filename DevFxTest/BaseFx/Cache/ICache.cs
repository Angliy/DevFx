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
	/// �������ӿ�
	/// </summary>
	/// <remarks>
	///	�����������ã�
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="HTB.DevFx.Cache.CacheManager"&gt;
	///					&lt;caches&gt;
	///						......
	///						&lt;cache name="����������" type="����������" interval="�����ڵ�ʱ������0��С��0��ʾ�����м��"&gt;
	///							&lt;!--�������û���洢��--&gt;
	///							&lt;cacheStorage type="HTB.DevFx.Cache.NullCacheStorage" /&gt;
	///						&lt;/cache&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICache
	{
		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// ����������
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// �Խ�ֵ��ʽ��ȡ/���û����ֵ��
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <remarks>
		///	���������ֵ����ʹ���������ڲ��Ի���
		/// </remarks>
		object this[string key] { get; set; }

		/// <summary>
		/// ��ָ����ֵ�͹��ڲ��������û����ֵ��
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="cacheDependency">������Ĺ��ڲ���</param>
		object this[string key, ICacheDependency cacheDependency] { set; }

		/// <summary>
		/// ��ȡ�˻�������������ĸ���
		/// </summary>
		int Count { get; }

		/// <summary>
		/// ���һ���������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="value">����Ķ���</param>
		/// <param name="cacheDependency">������Ĺ��ڲ���</param>
		void Add(string key, object @value, ICacheDependency cacheDependency);

		/// <summary>
		/// ���һ���������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <param name="value">����Ķ���</param>
		/// <remarks>
		/// û��ָ�����ڲ��ԣ���ʹ���������ڲ��Ի���
		/// </remarks>
		void Add(string key, object @value);

		/// <summary>
		/// ��ȡ������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <returns>����Ķ������������û�����У��򷵻�<c>null</c></returns>
		object Get(string key);

		/// <summary>
		/// �Ƴ�������
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		void Remove(string key);

		/// <summary>
		/// �жϻ��������Ƿ����ָ����ֵ�Ļ�����
		/// </summary>
		/// <param name="key">������Ľ�ֵ</param>
		/// <returns>��/��</returns>
		bool Contains(string key);

		/// <summary>
		/// ����˻����������е���
		/// </summary>
		/// <remarks>
		/// ��Ӱ�������ļ�������������
		/// </remarks>
		void Clear();
	}
}
