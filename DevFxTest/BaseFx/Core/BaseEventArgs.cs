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
	/// 事件参数基础类
	/// </summary>
	[Serializable]
	public class BaseEventArgs : EventArgs
	{
		/// <summary>
		/// 事件发生者
		/// </summary>
		protected object sender;

		/// <summary>
		/// 事件类型
		/// </summary>
		protected string eventType;

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="sender">事件发生者</param>
		public BaseEventArgs(object sender) : this(sender, null) {}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="sender">事件发生者</param>
		/// <param name="eventType">事件类型</param>
		public BaseEventArgs(object sender, string eventType) {
			this.sender = sender;
			this.eventType = eventType;
		}

		/// <summary>
		/// 事件发生者
		/// </summary>
		public virtual object Sender {
			get { return this.sender; }
		}

		/// <summary>
		/// 事件类型
		/// </summary>
		public virtual string EventType {
			get { return this.eventType; }
		}
	}

	/// <summary>
	/// 事件参数基础类（泛型）
	/// </summary>
	[Serializable]
	public class BaseEventArgs<T> : BaseEventArgs
	{
		/// <summary>
		/// 事件附加值
		/// </summary>
		protected T eventValue;

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="sender">事件发生者</param>
		/// <param name="eventType">事件类型</param>
		/// <param name="eventValue">事件附加值</param>
		public BaseEventArgs(object sender, string eventType, T eventValue) : base(sender, eventType) {
			this.eventValue = eventValue;
		}

		/// <summary>
		/// 事件附加值
		/// </summary>
		public virtual T EventValue {
			get { return this.eventValue; }
			set { this.eventValue = value; }
		}
	}

	/// <summary>
	/// 事件委托（泛型）
	/// </summary>
	/// <typeparam name="T">事件参数类型</typeparam>
	/// <param name="sender">事件发生者</param>
	/// <param name="e">事件参数</param>
	[Serializable]
	public delegate void EventHandlerDelegate<T>(object sender, T e) where T : BaseEventArgs;

	/// <summary>
	/// 事件委托（泛型）
	/// </summary>
	/// <typeparam name="T">事件参数类型</typeparam>
	/// <typeparam name="V">事件附加值类型</typeparam>
	/// <param name="sender">事件发生者</param>
	/// <param name="e">事件参数</param>
	[Serializable]
	public delegate void EventHandlerDelegate<T, V>(object sender, T e) where T : BaseEventArgs<V>;
}