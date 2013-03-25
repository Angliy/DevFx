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
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Log;
using HTB.DevFx.Utils;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// 异常管理器接口实现
	/// </summary>
	/// <remarks>如果要替换为自己的管理器，建议从本类继承</remarks>
	public class ExceptionManager : IExceptionManager
	{
		#region constructor
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public ExceptionManager() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">配置节</param>
		/// <param name="logManager">日志管理器</param>
		public ExceptionManager(IConfigSetting setting, ILogManager logManager) {
			this.Init(setting, logManager);
		}

		#endregion

		#region protected members

		/// <summary>
		/// 配置节
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// 日志管理器
		/// </summary>
		protected ILogManager logManager;
		/// <summary>
		/// 异常处理器集合
		/// </summary>
		protected CollectionBase<IExceptionHandle> handlers;
		/// <summary>
		/// 是否初始化
		/// </summary>
		protected bool isInit;

		#endregion

		#region IExceptionManager Members

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		/// <param name="logManager">日志管理器</param>
		public virtual void Init(IConfigSetting setting, ILogManager logManager) {
			if(this.isInit) {
				return;
			}
			this.setting = setting;
			this.logManager = logManager;
			this.handlers = new CollectionBase<IExceptionHandle>();
			IConfigSetting[] settings = setting["handlers"].GetChildSettings();
			for(int i = 0; i < settings.Length; i++) {
				string handlerName = settings[i].Property["name"].Value;
				if(handlerName == null) {
					throw new BaseException("异常处理器名为Null");
				}
				if(this.handlers.Contains(handlerName)) {
					throw new BaseException("异常处理器名重复：" + handlerName);
				}
				IExceptionHandle handler = (IExceptionHandle)settings[i].Property["type"].ToObject(typeof(IExceptionHandle), true);
				handler.Init(settings[i], logManager);
				this.handlers.Add(handlerName, handler);
			}
			if(HttpContext.Current == null) {
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppOnError);
			}
			this.isInit = true;
		}

		/// <summary>
		/// 处理异常
		/// </summary>
		/// <param name="e">异常</param>
		public virtual void Publish(Exception e) {
			this.Publish(e, LogLevel.ERROR);
		}

		/// <summary>
		/// 处理异常
		/// </summary>
		/// <param name="e">异常</param>
		/// <param name="level">异常等级（决定由哪个日志记录器记录）</param>
		public virtual void Publish(Exception e, int level) {
			if(!this.isInit) {
				throw new BaseException("异常管理器没有被正确初始化");
			}
			if(e == null) {
				return;
			}

			IAOPResult result = null;
			for(int i = 0; i < this.handlers.Count; i++) {
				IExceptionHandle handler = this.handlers[i];
				if(handler.ExceptionType.IsAssignableFrom(e.GetType())) {
					result = handler.Handle(e, level);
					if(result.ResultNo <= 0) {
						break;
					} else if(result.ResultNo == 1) {
						if(result.ResultAttachObject != null) {
							e = (Exception)result.ResultAttachObject;
						}
					} else if(result.ResultNo == 2) {
						if(result.ResultAttachObject != null) {
							e = (Exception)result.ResultAttachObject;
						}
						this.Publish(e, level);
						break;
					}
				}
			}
		}

		#endregion

		#region internal static members
		
		/// <summary>
		/// 桌面应用程序错误捕捉
		/// </summary>
		internal static void AppOnError(object sender, UnhandledExceptionEventArgs e) {
			Exceptor.Publish((Exception)e.ExceptionObject);
		}

		#endregion
	}
}
