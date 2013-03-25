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

namespace HTB.DevFx.Config
{
	/// <summary>
	/// ���ù������ĳ���ʵ��
	/// </summary>
	public abstract class ConfigManager : IConfigManager
	{
		#region constructor

		/// <summary>
		/// ���캯��
		/// </summary>
		protected ConfigManager() {
		}
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configFile">�����ļ���Ϣ</param>
		/// <param name="monitor">�Ƿ�Ҫ���������ļ�</param>
		protected ConfigManager(string configFile, bool monitor) {
			this.Init(configFile, monitor);
		}

		#endregion

		#region protected fields

		/// <summary>
		/// �����ļ���Ϣ
		/// </summary>
		protected string configFile;
		/// <summary>
		/// �Ƿ�Ҫ���������ļ�
		/// </summary>
		protected bool monitor;
		/// <summary>
		/// �������Ƿ��ѳ�ʼ��
		/// </summary>
		protected bool initialized;

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="configFile">�����ļ���Ϣ</param>
		/// <param name="monitor">�Ƿ�Ҫ���Ӵ����õı仯</param>
		/// <remarks>
		///	����<paramref name="monitor" />��ʾ�����������ļ��仯���Ը���������Ϣ
		/// </remarks>
		protected virtual void OnInit(string configFile, bool monitor) {
			this.configFile = configFile;
			this.monitor = monitor;
		}

		#endregion

		#region IConfigManager Members

		/// <summary>
		/// �˹����������������
		/// </summary>
		public abstract IConfigSetting Setting { get; }

		/// <summary>
		/// ��ʼ�����ṩ����ܵ��ã����й������ĳ�ʼ���������������������ļ��ȵ�
		/// </summary>
		/// <param name="configFile">�����ļ���Ϣ</param>
		/// <param name="monitor">�Ƿ�Ҫ���Ӵ����õı仯</param>
		/// <remarks>
		///	����<paramref name="monitor" />��ʾ�����������ļ��仯���Ը���������Ϣ
		/// </remarks>
		public virtual void Init(string configFile, bool monitor) {
			if(!this.initialized) {
				this.OnInit(configFile, monitor);
				this.initialized = true;
			}
		}

		/// <summary>
		/// ������ý�
		/// </summary>
		/// <param name="xpath">���ýڵ�XPath�����Ϊ<c>null</c>���򷵻ظ����ý�</param>
		/// <returns><see cref="IConfigSetting"/></returns>
		public virtual IConfigSetting GetSetting(string xpath) {
			return this.Setting.GetChildSetting(xpath);
		}

		/// <summary>
		/// �����������������Ϣ
		/// </summary>
		public abstract void Reset();

		#endregion
	}
}
