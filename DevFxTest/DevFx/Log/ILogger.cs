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
using HTB.DevFx.Core;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ��־��¼���ӿ�
	/// </summary>
	/// <remarks>
	///	��־��¼�������ã�
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="HTB.DevFx.Log.LogManager"&gt;
	///					&lt;loggers&gt;
	///						......
	///						&lt;logger name="��¼����" minLevel="�˼�¼���������СLevel" maxLevel="�˼�¼����������Level" type="��¼��������" /&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public interface ILogger
	{
		/// <summary>
		/// ��־��¼������
		/// </summary>
		string Name { get; }

		/// <summary>
		/// ����־��¼����¼�������Сֵ
		/// </summary>
		int MinLevel { get; }

		/// <summary>
		/// ����־��¼����¼��������ֵ
		/// </summary>
		int MaxLevel { get; }

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// ��־��¼
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־���𣨾�����������</param>
		/// <param name="message">��־��Ϣ</param>
		/// <returns>���ش�����</returns>
		IAOPResult Log(object source, int level, string message);
	}
}
