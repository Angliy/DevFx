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
	/// ��־��¼���ĳ���ʵ�֣�����Ӧ��ϵͳ�Լ���д�ļ�¼�����Ӵ���̳�
	/// </summary>
	public abstract class Logger : ILogger
	{
		#region constructor
		
		/// <summary>
		/// ���캯��
		/// </summary>
		public Logger() {
		}
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">��Ӧ�����ý�</param>
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
		/// ���ý�
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// �Ƿ��ѳ�ʼ��
		/// </summary>
		protected bool isInit;

		/// <summary>
		/// ��ȡ��ȷ����־��Դ����ȱʡ��־��Դʱ���ӵ��ö�ջ�л�ȡ�������õķ�����
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <returns>����ҵ����򷵻ض�����÷�������ͷ���������Ϣ</returns>
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
		/// ��־��¼������
		/// </summary>
		public string Name {
			get { return this.loggerName; }
		}

		/// <summary>
		/// ����־��¼����¼�������Сֵ
		/// </summary>
		public int MinLevel {
			get { return this.minLevel; }
		}

		/// <summary>
		/// ����־��¼����¼��������ֵ
		/// </summary>
		public int MaxLevel {
			get { return this.maxLevel; }
		}

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
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
		/// ��־��¼
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־���𣨾�������������</param>
		/// <param name="message">��־��Ϣ</param>
		/// <returns>���ش������</returns>
		public virtual IAOPResult Log(object source, int level, string message) {
			if(!this.isInit) {
				return new AOPResult(-1, "��־��¼��û�б���ȷ��ʼ��");
			} else {
				return new AOPResult(0);
			}
		}

		#endregion
	}
}