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
using System.Web;

namespace HTB.DevFx.ExceptionManagement.Web
{
	/// <summary>
	/// WEB��Ŀ���쳣
	/// </summary>
	/// <remarks>
	/// ��WEB��Ŀ�У��ܷ��ֵ��쳣�����װ�ɴ����ʵ���������쳣����<see cref="HTB.DevFx.ExceptionManagement.Web.HttpWebExceptionHandler"/>����
	/// </remarks>
	[Serializable]
	public class HttpWebException : BaseException
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="innerException">�ڲ��쳣</param>
		/// <param name="httpApp">HttpApplicationʵ��</param>
		public HttpWebException(Exception innerException, HttpApplication httpApp) : this(null, innerException, httpApp) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="httpApp">HttpApplicationʵ��</param>
		public HttpWebException(HttpApplication httpApp) : this(null, httpApp) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		/// <param name="httpApp">HttpApplicationʵ��</param>
		public HttpWebException(string message, Exception innerException, HttpApplication httpApp) : this(0, message, innerException, httpApp) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="errorNo">�쳣����</param>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		/// <param name="httpApp">HttpApplicationʵ��</param>
		public HttpWebException(int errorNo, string message, Exception innerException, HttpApplication httpApp) : base(errorNo, message, innerException) {
			this.httpApp = httpApp;
		}

		private HttpApplication httpApp;

		/// <summary>
		/// HttpApplicationʵ��
		/// </summary>
		public HttpApplication HttpAppInstance {
			get { return this.httpApp; }
		}
	}
}
