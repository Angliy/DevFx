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
using HTB.DevFx.Core;
using HTB.DevFx.Log;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// �쳣�������ӿ�
	/// </summary>
	/// <example>
	///	���ýڸ�ʽ��
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				&lt;exception type="......"&gt;
	///					.....
	///					&lt;handlers&gt;
	///						......
	///						&lt;handler name="�쳣��������" exceptionType="��������쳣����" exceptionFormatter="�쳣�ռ�������" type="�쳣����������" /&gt;
	///						......
	///					&lt;/handlers&gt;
	///				&lt;/exception&gt;
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </example>
	public interface IExceptionHandle
	{
		/// <summary>
		/// ��ʼ���쳣�����������쳣���������ã�
		/// </summary>
		/// <param name="setting">��Ӧ�����ý�</param>
		/// <param name="logManager">��־��¼��</param>
		void Init(IConfigSetting setting, ILogManager logManager);

		/// <summary>
		/// �쳣��������
		/// </summary>
		string Name { get; }
		
		/// <summary>
		/// ���쳣������������쳣����
		/// </summary>
		Type ExceptionType { get; }

		/// <summary>
		/// �쳣��Ϣ�ռ���ʽ��
		/// </summary>
		IExceptionFormatter ExceptionFormatter { get; set; }

		/// <summary>
		/// �����쳣�������쳣���������ã�
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="level">�쳣�ȼ������ݸ���־��¼������</param>
		/// <returns>����������Ӱ������Ĵ�����</returns>
		/// <remarks>
		/// �쳣�����������ݷ��صĽ��������һ���Ĵ���Լ����<br />
		///		���صĽ���У�ResultNoֵ��
		///		<list type="bullet">
		///			<item><description>
		///				С��0����ʾ�����쳣���������������˳��쳣����
		///			</description></item>
		///			<item><description>
		///				0����������
		///			</description></item>
		///			<item><description>
		///				1���Ѵ�����Ҫ��һ���쳣��������һ������<br />
		///				��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�
		///			</description></item>
		///			<item><description>
		///				2���Ѵ�����Ҫ������ѯ�쳣���������д���<br />
		///					��ʱResultAttachObjectΪ���ص��쳣�������봫����쳣�ǲ�һ�µģ�<br />
		///					��ʱ�쳣�����������½����쳣����
		///			</description></item>
		///		</list>
		/// </remarks>
		IAOPResult Handle(Exception e, int level);
	}
}
