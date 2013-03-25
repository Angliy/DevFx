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

using System.Reflection;
using HTB.DevFx.Config;
using HTB.DevFx.Core;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// 日志记录器的抽象实现，建议应用系统自己编写的记录器都从此类继承
	/// </summary>
	public abstract class Logger : ILogger
	{
		#region constructor
		
		/// <summary>
		/// 构造函数
		/// </summary>
		public Logger() {
		}
		
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="setting">对应的配置节</param>
		public Logger(IConfigSetting setting) {
			this.Init(setting);
		}

		#endregion

		#region private members

		private string loggerName;
		private int minLevel;
		private int maxLevel;

		#endregion

		#region protected members

		/// <summary>
		/// 配置节
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// 是否已初始化
		/// </summary>
		protected bool isInit;

		/// <summary>
		/// 获取正确的日志来源（当缺省日志来源时，从调用堆栈中获取发出调用的方法）
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <returns>如果找到，则返回定义调用方法的类和方法名等信息</returns>
		protected virtual object GetExactSourc(object source) {
			MethodInfo method = source as MethodInfo;
			if(method != null) {
				return method.DeclaringType.ToString() + "::" + method.ToString();
			} else {
				return source;
			}
		}

		#endregion

		#region ILogger Members

		/// <summary>
		/// 日志记录器名称
		/// </summary>
		public string Name {
			get { return this.loggerName; }
		}

		/// <summary>
		/// 此日志记录器记录级别的最小值
		/// </summary>
		public int MinLevel {
			get { return this.minLevel; }
		}

		/// <summary>
		/// 此日志记录器记录级别的最大值
		/// </summary>
		public int MaxLevel {
			get { return this.maxLevel; }
		}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="setting">配置节</param>
		public virtual void Init(IConfigSetting setting) {
			if(this.isInit) {
				return;
			}
			this.setting = setting;
			loggerName = setting.Property["name"].Value;
			if(!LogLevel.TryParse(setting.Property["minLevel"].Value, ref minLevel)) {
				minLevel = setting.Property["minLevel"].ToInt32();
			}
			if (!LogLevel.TryParse(setting.Property["maxLevel"].Value, ref maxLevel)) {
				maxLevel = setting.Property["maxLevel"].ToInt32();
			}
			this.isInit = true;
		}

		/// <summary>
		/// 日志记录
		/// </summary>
		/// <param name="source">日志来源</param>
		/// <param name="level">日志级别（决定处理方法）</param>
		/// <param name="message">日志信息</param>
		/// <returns>返回处理结果</returns>
		public virtual IAOPResult Log(object source, int level, string message) {
			if(!this.isInit) {
				return new AOPResult(-1, "日志记录器没有被正确初始化");
			} else {
				return new AOPResult(0);
			}
		}

		#endregion
	}
}
