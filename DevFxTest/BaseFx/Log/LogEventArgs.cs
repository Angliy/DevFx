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
using HTB.DevFx.Core;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ��־д���¼�������
	/// </summary>
	[Serializable]
	public class LogEventArgs : BaseEventArgs
	{
		private int level;
		private string message;
		private DateTime logTime;
		
		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="sender">��־������</param>
		/// <param name="level">��־�ȼ����μ�<see cref="LogLevel"/></param>
		/// <param name="logTime">��־����ʱ��</param>
		/// <param name="message">��־��Ϣ</param>
		public LogEventArgs(object sender, int level, DateTime logTime, string message) : base(sender) {
			this.level = level;
			this.logTime = logTime;
			this.message = message;
		}

		/// <summary>
		/// ��־�ȼ����μ�<see cref="LogLevel"/>
		/// </summary>
		public int Level {
			get { return this.level; }
		}

		/// <summary>
		/// ��־����ʱ��
		/// </summary>
		public DateTime LogTime {
			get { return this.logTime; }
		}

		/// <summary>
		/// ��־��Ϣ
		/// </summary>
		public string Message {
			get { return this.message; }
		}
	}
}