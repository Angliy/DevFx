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

namespace HTB.DevFx.Web.UI
{
	/// <summary>
	/// ��ҳ�¼�������
	/// </summary>
	internal class PagerPageEventArgs : EventArgs
	{
		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="newPageIndex">�µ�ҳ����</param>
		/// <param name="pageSize">ҳ��С</param>
		public PagerPageEventArgs(int newPageIndex, int pageSize) {
			this.NewPageIndex = newPageIndex;
			this.pageSize = pageSize;
		}

		/// <summary>
		/// ��ȡ�������µ�ҳ����
		/// </summary>
		public int NewPageIndex {
			get { return this.newPageIndex; }
			set {
				if(value < 0) {
					throw new ArgumentOutOfRangeException("value");
				}
				this.newPageIndex = value;
			}
		}

		/// <summary>
		/// ��ȡҳ��С
		/// </summary>
		public int PageSize {
			get { return pageSize; }
		}

		private int newPageIndex;
		private readonly int pageSize;
	}

	/// <summary>
	/// ��ҳ�¼���ί��
	/// </summary>
	/// <param name="sender">�¼�������</param>
	/// <param name="e">��ҳ�¼�����</param>
	internal delegate void PagerPageEventHandler(object sender, PagerPageEventArgs e);
}
