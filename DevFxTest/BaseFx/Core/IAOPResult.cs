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

using System.Runtime.InteropServices;

namespace HTB.DevFx.Core
{
	#region IAOPResult
	
	/// <summary>
	/// �������صĽ���ӿ�
	/// </summary>
	/// <remarks>
	/// �����ڴ�����÷���ֵ�ж����ô���ʵ��Ϊ����ֵ<br />
	/// һ��ResultNoС��0��ʾ�쳣��0��ʾ�ɹ�������0��ʾ����һ����ʾ��Ϣ
	/// </remarks>
	[Guid("106009C4-859D-4139-8F1D-9799D941662B")]
	public interface IAOPResult
	{
		/// <summary>
		/// ���ش���
		/// </summary>
		int ResultNo { get; }

		/// <summary>
		/// ��Ӧ��������Ϣ
		/// </summary>
		string ResultDescription { get; }

		/// <summary>
		/// ��Ӧ�ĸ�����Ϣ
		/// </summary>
		object ResultAttachObject { get; }

		/// <summary>
		/// �ڲ�AOPResult
		/// </summary>
		IAOPResult InnerAOPResult { get; }

		/// <summary>
		/// �������Ƿ�ɹ���ResultNo == 0��
		/// </summary>
		bool IsSuccess { get; }

		/// <summary>
		/// �������Ƿ�ʧ�ܣ�ResultNo != 0 ��
		/// </summary>
		bool IsNotSuccess { get; }

		/// <summary>
		/// �������Ƿ�ʧ�ܣ�ResultNo &lt; 0 ��
		/// </summary>
		bool IsFailed { get; }

		/// <summary>
		/// �Ѵ������в������Ĵ���ResultNo &gt; 0��
		/// </summary>
		bool IsPassedButFailed { get; }

		/// <summary>
		/// �������ʧ�ܣ����׳��쳣 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>���ر���</returns>
		IAOPResult ThrowErrorOnFailed();
	}

	#endregion IAOPResult

	#region IAOPResult<T>

	/// <summary>
	/// �������صĽ���ӿڣ����ͣ�
	/// </summary>
	[Guid("27F931B0-44E2-48E9-BC3F-ED707E68525C")]
	public interface IAOPResult<T> : IAOPResult
	{
		/// <summary>
		/// ���͸��Ӷ���
		/// </summary>
		T ResultAttachObjectEx { get; }
	}
 
	#endregion
}
