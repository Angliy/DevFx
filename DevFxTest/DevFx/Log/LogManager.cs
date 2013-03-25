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

using System.Diagnostics;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ��־�������ӿ�ʵ��
	/// </summary>
	/// <remarks>���Ҫ�滻Ϊ�Լ��Ĺ�����������ӱ���̳�</remarks>
	public class LogManager : ILogManager
	{
		#region constructor
		
		/// <summary>
		/// ���캯��
		/// </summary>
		public LogManager() {
		}
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public LogManager(IConfigSetting setting) {
			this.Init(setting);
		}

		#endregion

		#region protected members

		/// <summary>
		/// ���ý�
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// ��־��¼������
		/// </summary>
		protected CollectionBase<ILogger> loggers;
		/// <summary>
		/// �Ƿ��ʼ��
		/// </summary>
		protected bool isInit;

		#endregion

		#region overrde members

		/// <summary>
		/// ��ʼ�����ɿ�ܵ���
		/// </summary>
		/// <param name="setting">��־�����������ý�</param>
		public virtual void Init(IConfigSetting setting) {
			if(this.isInit) {
				return;
			}

			this.setting = setting;
			this.loggers = new CollectionBase<ILogger>();
			IConfigSetting[] logSettings = setting["loggers"].GetChildSettings();
			for(int i = 0; i < logSettings.Length; i++) {
				string loggerName = logSettings[i].Property["name"].Value;
				if(loggerName == null) {
					throw new LogException("��־��¼����ΪNull");
				}
				if(this.loggers.Contains(loggerName)) {
					throw new LogException("��־��¼�����ظ���" + loggerName);
				}
				ILogger logger = (ILogger)logSettings[i].Property["type"].ToObject(typeof(ILogger), true);
				logger.Init(logSettings[i]);
				this.loggers.Add(loggerName, logger);
			}
			this.isInit = true;
		}

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		public virtual void WriteLog(int level, string message) {
			StackTrace t = new StackTrace();
			StackFrame f = t.GetFrame(t.FrameCount - 1);
			this.WriteLog(f.GetMethod(), level, message);
		}

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		/// <remarks>
		/// ��־��Դ����ϵͳ��ջ�л�ȡ
		/// </remarks>
		public virtual void WriteLog(int level, object message) {
			this.WriteLog(level, message.ToString());
		}

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		public virtual void WriteLog(object source, int level, string message) {
			if(!this.isInit) {
				throw new LogException("��־������û�б���ȷ��ʼ��");
			}

			for(int i = 0; i < this.loggers.Count; i++) {
				ILogger logger = this.loggers[i];
				if(level >= logger.MinLevel && level <= logger.MaxLevel) {
					IAOPResult result = logger.Log(source, level, message);
					if(result.IsFailed) {
						break;
					}
				}
			}
		}

		/// <summary>
		/// д��־
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־�ȼ���<see cref=" HTB.DevFx.Log.LogLevel"/></param>
		/// <param name="message">��־��Ϣ</param>
		public virtual void WriteLog(object source, int level, object message) {
			this.WriteLog(source, level, message.ToString());
		}

		#endregion
	}
}
