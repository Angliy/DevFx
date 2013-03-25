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
	/// 分页事件参数类
	/// </summary>
	internal class PagerPageEventArgs : EventArgs
	{
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="newPageIndex">新的页索引</param>
		/// <param name="pageSize">页大小</param>
		public PagerPageEventArgs(int newPageIndex, int pageSize) {
			this.NewPageIndex = newPageIndex;
			this.pageSize = pageSize;
		}

		/// <summary>
		/// 获取或设置新的页索引
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
		/// 获取页大小
		/// </summary>
		public int PageSize {
			get { return pageSize; }
		}

		private int newPageIndex;
		private readonly int pageSize;
	}

	/// <summary>
	/// 分页事件的委托
	/// </summary>
	/// <param name="sender">事件发送者</param>
	/// <param name="e">分页事件参数</param>
	internal delegate void PagerPageEventHandler(object sender, PagerPageEventArgs e);
}
