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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// ������ڲ��Խӿ�
	/// </summary>
	/// <example>
	/// �����ʾ����ʾ�������ʱ����ڲ������������
	///		<code>
	///			......
	///			string key = Guid.NewGuid().ToString();
	///			object cachingObject = new YourCachingObject();
	///			ICache cache = CacheHelper.GetCache("your cache instance name");
	///			cache.Add(key, cachingObject, new ExpirationCacheDependency(TimeSpan.FromSeconds(20)));
	///			......
	///		</code>
	/// </example>
	public interface ICacheDependency
	{
		/// <summary>
		/// �Ƿ��ѹ���
		/// </summary>
		bool IsExpired { get; }

		/// <summary>
		/// ���û�����ԣ��൱�����¿�ʼ���棩
		/// </summary>
		void Reset();
	}
}
