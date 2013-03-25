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
	/// ����������ӿ�
	/// </summary>
	/// <remarks>
	///	��������������ã�
	///		<code>
	///			&lt;configuration&gt;
	///				......
	///				
	///				&lt;cache type="�������������"&gt;
	///					&lt;caches&gt;&lt;!--�������û���ռ䣨��ʵ��ģʽ��--&gt;
	///						......
	///					&lt;/caches&gt;
	///				&lt;/cache&gt;
	///				
	///				......
	///			&lt;/configuration&gt;
	///		</code>
	/// </remarks>
	public interface ICacheManager
	{
		/// <summary>
		/// ��ʼ�����ɿ�ܵ���
		/// </summary>
		/// <param name="setting">��������������ý�</param>
		void Init(IConfigSetting setting);
		
		/// <summary>
		/// ��ȡ����ռ�
		/// </summary>
		/// <param name="cacheName">�������ļ������õĻ���ռ���</param>
		/// <returns>ʵ��ICache�ӿڵĻ�����ʵ��</returns>
		ICache GetCache(string cacheName);

		/// <summary>
		/// ��һ�������ý���ʵ����������
		/// </summary>
		/// <param name="cacheSetting">���������ý�</param>
		/// <returns>ʵ��ICache�ӿڵĻ�����ʵ��</returns>
		ICache GetCache(IConfigSetting cacheSetting);
	}
}
