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

namespace HTB.DevFx.Config
{
	/// <summary>
	/// ���ù������ӿ�
	/// </summary>
	public interface IConfigManager
	{
		/// <summary>
		/// �˹���������������ý�
		/// </summary>
		IConfigSetting Setting { get; }

		/// <summary>
		/// ������ý�
		/// </summary>
		/// <param name="xpath">���ýڵ�XPath�����Ϊ<c>null</c>���򷵻ظ����ý�</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		IConfigSetting GetSetting(string xpath);

		/// <summary>
		/// ��ʼ�����ṩ����ܵ��ã����й������ĳ�ʼ���������������������ļ��ȵ�
		/// </summary>
		/// <param name="configFile">�����ļ���Ϣ</param>
		/// <param name="monitor">�Ƿ�Ҫ���Ӵ����õı仯</param>
		/// <remarks>
		///	����<paramref name="monitor" />��ʾ�����������ļ��仯���Ը���������Ϣ
		/// </remarks>
		void Init(string configFile, bool monitor);

		/// <summary>
		/// �����������������Ϣ
		/// </summary>
		void Reset();
	}
}
