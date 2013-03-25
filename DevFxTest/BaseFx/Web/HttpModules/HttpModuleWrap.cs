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

namespace HTB.DevFx.Web.HttpModules
{
	/// <summary>
	/// <see cref="IHttpModule"/>包装，也为后续类提供基类
	/// </summary>
	public class HttpModuleWrap : IHttpModule
	{
		#region IHttpModule Members

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="context"><see cref="HttpApplication"/> 实例</param>
		public virtual void Init(HttpApplication context) {
			context.BeginRequest += this.OnBeginRequest;
			context.AuthenticateRequest += this.OnAuthenticateRequest;
			context.AuthorizeRequest += this.OnAuthorizeRequest;
			context.ResolveRequestCache += this.OnResolveRequestCache;
			context.AcquireRequestState += this.OnAcquireRequestState;
			context.PreRequestHandlerExecute += this.OnPreRequestHandlerExecute;
			context.PostRequestHandlerExecute += this.OnPostRequestHandlerExecute;
			context.ReleaseRequestState += this.OnReleaseRequestState;
			context.UpdateRequestCache += this.OnUpdateRequestCache;
			context.EndRequest += this.OnEndRequest;
			context.PreSendRequestHeaders += this.OnPreSendRequestHeaders;
			context.PreSendRequestContent += this.OnPreSendRequestContent;
			context.Error += this.OnError;
		}

		/// <summary>
		/// 释放模块
		/// </summary>
		public virtual void Dispose() {}

		#endregion

		#region HttpApplication Event Hanlders

		/// <summary>
		/// <see cref="HttpApplication"/> 事件处理者
		/// </summary>
		public static event EventHandler<HttpApplicationEventArgs> HttpApplicationEvent;

		/// <summary>
		/// 事件发生时处理方法
		/// </summary>
		/// <param name="sender">事件发送者，一般为<see cref="HttpApplication"/></param>
		/// <param name="eventType"><see cref="HttpApplicationEventTypeEnum"/></param>
		protected virtual void OnEvent(object sender, HttpApplicationEventTypeEnum eventType) {
			HttpApplication context = (HttpApplication)sender;
			if (HttpApplicationEvent != null) {
				HttpApplicationEvent(this, new HttpApplicationEventArgs(context, eventType));
			}
		}

		/// <summary>
		/// BeginRequest
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnBeginRequest(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.BeginRequest);
		}

		/// <summary>
		/// AuthenticateRequest
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnAuthenticateRequest(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.AuthenticateRequest);
		}

		/// <summary>
		/// AuthorizeRequest
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnAuthorizeRequest(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.AuthorizeRequest);
		}

		/// <summary>
		/// ResolveRequestCache
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnResolveRequestCache(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.ResolveRequestCache);
		}

		/// <summary>
		/// AcquireRequestState
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnAcquireRequestState(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.AcquireRequestState);
		}

		/// <summary>
		/// PreRequestHandlerExecute
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnPreRequestHandlerExecute(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.PreRequestHandlerExecute);
		}

		/// <summary>
		/// PostRequestHandlerExecute
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnPostRequestHandlerExecute(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.PostRequestHandlerExecute);
		}

		/// <summary>
		/// ReleaseRequestState
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnReleaseRequestState(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.ReleaseRequestState);
		}

		/// <summary>
		/// UpdateRequestCache
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnUpdateRequestCache(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.UpdateRequestCache);
		}

		/// <summary>
		/// EndRequest
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnEndRequest(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.EndRequest);
		}

		/// <summary>
		/// PreSendRequestHeaders
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnPreSendRequestHeaders(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.PreSendRequestHeaders);
		}

		/// <summary>
		/// PreSendRequestContent
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnPreSendRequestContent(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.PreSendRequestContent);
		}

		/// <summary>
		/// Error
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/></param>
		/// <param name="e"><see cref="EventArgs"/></param>
		protected virtual void OnError(object sender, EventArgs e) {
			this.OnEvent(sender, HttpApplicationEventTypeEnum.Error);
		}

		#endregion HttpApplication Event Hanlders
	}

	/// <summary>
	/// <see cref="HttpApplication"/> 的事件类型
	/// </summary>
	public enum HttpApplicationEventTypeEnum
	{
		/// <summary>
		/// BeginRequest
		/// </summary>
		BeginRequest,
		/// <summary>
		/// AuthenticateRequest
		/// </summary>
		AuthenticateRequest,
		/// <summary>
		/// AuthorizeRequest
		/// </summary>
		AuthorizeRequest,
		/// <summary>
		/// ResolveRequestCache
		/// </summary>
		ResolveRequestCache,
		/// <summary>
		/// AcquireRequestState
		/// </summary>
		AcquireRequestState,
		/// <summary>
		/// PreRequestHandlerExecute
		/// </summary>
		PreRequestHandlerExecute,
		/// <summary>
		/// PostRequestHandlerExecute
		/// </summary>
		PostRequestHandlerExecute,
		/// <summary>
		/// ReleaseRequestState
		/// </summary>
		ReleaseRequestState,
		/// <summary>
		/// UpdateRequestCache
		/// </summary>
		UpdateRequestCache,
		/// <summary>
		/// EndRequest
		/// </summary>
		EndRequest,
		/// <summary>
		/// PreSendRequestHeaders
		/// </summary>
		PreSendRequestHeaders,
		/// <summary>
		/// PreSendRequestContent
		/// </summary>
		PreSendRequestContent,
		/// <summary>
		/// Error
		/// </summary>
		Error
	}

	/// <summary>
	/// <see cref="HttpApplication"/> 的事件参数
	/// </summary>
	public class HttpApplicationEventArgs : EventArgs
	{
		private HttpApplicationEventTypeEnum eventType;
		private HttpApplication context;

		/// <summary>
		/// 事件类型
		/// </summary>
		public HttpApplicationEventTypeEnum EventType {
			get { return this.eventType; }
		}

		/// <summary>
		/// 当前 <see cref="HttpApplication"/> 实例
		/// </summary>
		public HttpApplication Context {
			get { return this.context; }
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="context">当前 <see cref="HttpApplication"/> 实例</param>
		/// <param name="eventType">事件类型</param>
		public HttpApplicationEventArgs(HttpApplication context, HttpApplicationEventTypeEnum eventType) {
			this.context = context;
			this.eventType = eventType;
		}
	}
}