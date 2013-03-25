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

namespace HTB.DevFx.Core
{
	/// <summary>
	/// �¼�����������
	/// </summary>
	[Serializable]
	public class BaseEventArgs : EventArgs
	{
		/// <summary>
		/// �¼�������
		/// </summary>
		protected object sender;

		/// <summary>
		/// �¼�����
		/// </summary>
		protected string eventType;

		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="sender">�¼�������</param>
		public BaseEventArgs(object sender) : this(sender, null) {}

		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="sender">�¼�������</param>
		/// <param name="eventType">�¼�����</param>
		public BaseEventArgs(object sender, string eventType) {
			this.sender = sender;
			this.eventType = eventType;
		}

		/// <summary>
		/// �¼�������
		/// </summary>
		public virtual object Sender {
			get { return this.sender; }
		}

		/// <summary>
		/// �¼�����
		/// </summary>
		public virtual string EventType {
			get { return this.eventType; }
		}
	}

	/// <summary>
	/// �¼����������ࣨ���ͣ�
	/// </summary>
	[Serializable]
	public class BaseEventArgs<T> : BaseEventArgs
	{
		/// <summary>
		/// �¼�����ֵ
		/// </summary>
		protected T eventValue;

		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="sender">�¼�������</param>
		/// <param name="eventType">�¼�����</param>
		/// <param name="eventValue">�¼�����ֵ</param>
		public BaseEventArgs(object sender, string eventType, T eventValue) : base(sender, eventType) {
			this.eventValue = eventValue;
		}

		/// <summary>
		/// �¼�����ֵ
		/// </summary>
		public virtual T EventValue {
			get { return this.eventValue; }
			set { this.eventValue = value; }
		}
	}

	/// <summary>
	/// �¼�ί�У����ͣ�
	/// </summary>
	/// <typeparam name="T">�¼���������</typeparam>
	/// <param name="sender">�¼�������</param>
	/// <param name="e">�¼�����</param>
	[Serializable]
	public delegate void EventHandlerDelegate<T>(object sender, T e) where T : BaseEventArgs;

	/// <summary>
	/// �¼�ί�У����ͣ�
	/// </summary>
	/// <typeparam name="T">�¼���������</typeparam>
	/// <typeparam name="V">�¼�����ֵ����</typeparam>
	/// <param name="sender">�¼�������</param>
	/// <param name="e">�¼�����</param>
	[Serializable]
	public delegate void EventHandlerDelegate<T, V>(object sender, T e) where T : BaseEventArgs<V>;
}