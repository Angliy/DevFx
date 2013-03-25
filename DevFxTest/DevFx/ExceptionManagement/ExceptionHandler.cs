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
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Log;

namespace HTB.DevFx.ExceptionManagement
{
	/// <summary>
	/// 异常处理器接口实现，建议应用程序自定义的处理器都从本类继承
	/// </summary>
	public class ExceptionHandler : IExceptionHandle
	{
		#region constructor

		/// <summary>
		/// 构造函数
		/// </summary>
		public ExceptionHandler() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">对应的配置节</param>
		/// <param name="logManager">日志记录器</param>
		public ExceptionHandler(IConfigSetting setting, ILogManager logManager) {
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
		/// 处理器名
		/// </summary>
		protected string handlerName;
		/// <summary>
		/// 处理的异常类型
		/// </summary>
		protected Type exceptionType;
		/// <summary>
		/// 异常收集器
		/// </summary>
		protected IExceptionFormatter exceptionFormatter;
		/// <summary>
		/// 是否初始化
		/// </summary>
		protected bool isInit;

		#endregion

		#region IExceptionHandle Members

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
			this.handlerName = setting.Property["name"].Value;
			this.exceptionType = setting.Property["exceptionType"].ToType();
			this.ExceptionFormatter = (IExceptionFormatter)setting.Property["exceptionFormatter"].ToObject(typeof(IExceptionFormatter), true);
			this.isInit = true;
		}

		/// <summary>
		/// 异常处理器名
		/// </summary>
		public virtual string Name {
			get { return this.handlerName; }
		}

		/// <summary>
		/// 此异常处理器处理的异常类型
		/// </summary>
		public virtual Type ExceptionType {
			get {
				return this.exceptionType;
			}
		}

		/// <summary>
		/// 异常信息收集格式化
		/// </summary>
		public IExceptionFormatter ExceptionFormatter {
			get { return this.exceptionFormatter; }
			set { this.exceptionFormatter = value; }
		}

		/// <summary>
		/// 进行异常处理（由异常管理器调用）
		/// </summary>
		/// <param name="e">异常</param>
		/// <param name="level">异常等级（传递给日志记录器处理）</param>
		/// <returns>处理结果，将影响下面的处理器</returns>
		/// <remarks>
		/// 异常管理器将根据返回的结果进行下一步的处理，约定：<br />
		///		返回的结果中，ResultNo值：
		///		<list type="bullet">
		///			<item><description>
		///				小于0：表示处理异常，管理器将立即退出异常处理
		///			</description></item>
		///			<item><description>
		///				0：处理正常
		///			</description></item>
		///			<item><description>
		///				1：已处理，需要下一个异常处理器进一步处理，<br />
		///				此时ResultAttachObject为返回的异常（可能与传入的异常是不一致的）
		///			</description></item>
		///			<item><description>
		///				2：已处理，需要重新轮询异常处理器进行处理<br />
		///					此时ResultAttachObject为返回的异常（可能与传入的异常是不一致的）<br />
		///					此时异常管理器将重新进行异常处理
		///			</description></item>
		///		</list>
		/// </remarks>
		public virtual IAOPResult Handle(Exception e, int level) {
			if(!this.isInit) {
				return new AOPResult(-1, "异常处理器没有被正确初始化", e, null);
			}
			this.logManager.WriteLog(level, this.exceptionFormatter.GetFormatString(e, null));
			return new AOPResult(0);
		}

		#endregion
	}
}
