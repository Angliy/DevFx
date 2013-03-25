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

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ��־�������ӿ�
	/// </summary>
	/// <remarks>
	///	��־�����������ã�
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="��־����������"&gt;
	///					&lt;loggers&gt;&lt;!--����������־��¼��--&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public interface ILogManager
	{
		/// <summary>
		/// ��ʼ�����ɿ�ܵ���
		/// </summary>
		/// <param name="setting">��־�����������ý�</param>
		void Init(IConfigSetting setting);

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		void WriteLog(int level, string message);

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		void WriteLog(int level, object message);

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		void WriteLog(object source, int level, string message);

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		void WriteLog(object source, int level, object message);
	}
}
