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

using HTB.DevFx.Config;
using HTB.DevFx.ExceptionManagement;

namespace HTB.DevFx.Core
{
	/// <summary>
	/// ʵ��IModule�ĺ���ģ�飬�������е�ģ�鶼�Ӵ���̳�
	/// </summary>
	public abstract class CoreModule : IModule
	{
		/// <summary>
		/// ���ÿ�ܽ��г�ʼ��
		/// </summary>
		static CoreModule() {
			Framework.Init();
		}

		/// <summary>
		/// ģ�����ƣ��������ļ������ã�
		/// </summary>
		protected string name;
		/// <summary>
		/// IFramework��ʵ��
		/// </summary>
		protected IFramework framework;
		/// <summary>
		/// ģ������ý�
		/// </summary>
		protected IConfigSetting moduleSetting;
		/// <summary>
		/// ģ��ʵ��ָ������ý�
		/// </summary>
		protected IConfigSetting setting;
		/// <summary>
		/// �Ƿ��ѳ�ʼ��
		/// </summary>
		protected bool initialized;

		/// <summary>
		/// ģ���Ƿ񱻳�ʼ��
		/// </summary>
		/// <param name="throwOnError">���û�г�ʼ�����Ƿ��׳��쳣</param>
		/// <returns>true/false</returns>
		/// <remarks>
		/// ���û�б���ʼ�������׳��쳣 <see cref="BaseException" />
		/// </remarks>
		protected virtual bool Initialized(bool throwOnError) {
			if(!this.initialized && throwOnError) {
				throw new BaseException("ģ��û�б���ȷ��ʼ��");
			}
			return this.initialized;
		}

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		protected virtual void OnInit(IFramework framework, IConfigSetting setting) {
			if (setting == null) {
				throw new BaseException("ģ���ʼ��ʧ�ܣ�û���ṩ���ý�");
			}
			this.framework = framework;
			this.moduleSetting = setting;
			this.name = setting.Property["name"].Value;
			string linkNode = setting.Property.TryGetPropertyValue("linkNode");
			if (string.IsNullOrEmpty(linkNode)) {
				if (setting.Children > 0) {
					this.setting = setting[0];
				}
			} else {
				this.setting = setting.GetChildSetting(linkNode);
			}
		}

		#region IModule Members

		/// <summary>
		/// ģ����
		/// </summary>
		public virtual string Name {
			get {
				this.Initialized(true);
				return this.name;
			}
		}

		/// <summary>
		/// ��ʼ��ģ��
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">��Ӧ�����ý�</param>
		public virtual void Init(IFramework framework, IConfigSetting setting) {
			if(!this.initialized) {
				this.OnInit(framework, setting);
				this.initialized = true;
			}
		}

		/// <summary>
		/// ��ģ������ý�
		/// </summary>
		public virtual IConfigSetting Setting {
			get {
				this.Initialized(true);
				return this.setting;
			}
		}

		/// <summary>
		/// ��ȡ��ģ���ʵ���������ǵ���ģʽҲ�����Ƕ���ģʽ�������󷽷�
		/// </summary>
		/// <returns><see cref="IModule"/></returns>
		public abstract IModule GetInstance();

		#endregion

		#region IModule Members

		string IModule.Name {
			get { return this.Name; }
		}

		void IModule.Init(IFramework framework, IConfigSetting setting) {
			this.Init(framework, setting);
		}

		IConfigSetting IModule.Setting {
			get { return this.Setting; }
		}

		IModule IModule.GetInstance() {
			return this.GetInstance();
		}

		#endregion
	}
}
