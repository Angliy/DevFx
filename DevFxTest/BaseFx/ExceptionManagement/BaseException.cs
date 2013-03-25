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

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// �쳣ģ���쳣����ܵĻ����쳣�࣬���е��쳣��ӱ�������
	/// </summary>
	[Serializable]
	public class BaseException : ApplicationException
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public BaseException() : this(0, null, null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		public BaseException(string message, Exception innerException) : this(0, message, innerException) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="message">�쳣��Ϣ</param>
		public BaseException(string message) : this(0, message) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="errorNo">�쳣���</param>
		/// <param name="message">�쳣��Ϣ</param>
		public BaseException(int errorNo, string message) : this(errorNo, message, null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="errorNo">�쳣���</param>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		public BaseException(int errorNo, string message, Exception innerException) : base(message, innerException) {
			this.errorNo = errorNo;
		}

		/// <summary>
		/// �쳣���
		/// </summary>
		protected int errorNo;

		/// <summary>
		/// �쳣���
		/// </summary>
		public int ErrorNo {
			get { return this.errorNo; }
		}

		/// <summary>
		/// ����ԭʼ���쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <returns>ԭʼ���쳣</returns>
		public static Exception FindSourceException(Exception e) {
			Exception e1 = e;
			while(e1 != null) {
				e = e1;
				e1 = e1.InnerException;
			}
			return e;
		}

		/// <summary>
		/// ���쳣���ֲ���ָ�����͵��쳣
		/// </summary>
		/// <param name="e">�쳣</param>
		/// <param name="expectedExceptionType">�ڴ����쳣����</param>
		/// <returns>��Ҫ����쳣������Ҳ���������null</returns>
		public static Exception FindSourceException(Exception e, Type expectedExceptionType) {
			while(e != null) {
				if(e.GetType() == expectedExceptionType) {
					return e;
				}
				e = e.InnerException;
			}
			return null;
		}
	}
}
