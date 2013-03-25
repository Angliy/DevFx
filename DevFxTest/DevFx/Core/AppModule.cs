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

namespace HTB.DevFx.Core
{
	/// <summary>
	/// Ӧ�ó���ģ�飨���ͣ�
	/// </summary>
	/// <remarks>
	/// һ���Ӧ��ϵͳ�����԰Ѵ�����Ϊ������Դ<br />
	/// ���磺
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///				......
	///				......
	///				&lt;app&gt;&lt;/app&gt;
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	�ڴ�����Ҫ���<c>&lt;app&gt;&lt;/app&gt;</c>������ý���Ϣ����ʹ�����´��룺
	///		<code>
	///			......
	///			IConfigSetting mySetting = Framework.GetModules("app").Setting;
	///			......
	///		</code>
	///	����Ӧ��ϵͳ�Լ������µ�������ȡ�����ַ�ʽ�Ƚ����Ƽ�ʹ�����ַ�ʽ��ȡӦ��ϵͳ�����ý�<br />
	///	���磺
	///		<code>
	///			//ʹ�õ���ģʽ
	///			public class MyAppModule : AppModule&lt;MyAppModule&gt;
	///			{
	///				......
	///				
	///				//��д�˷����Ի�ȡ������Ϣ��ע�����ﴫ������setting��ģ�鱾������ý�
	///				public override void Init(IFramework framework, IConfigSetting setting) {
	///					if(!this.initialized) {
	///						base.Init(framework, setting);
	///						IConfigSetting mySetting = this.setting;//����Լ���������Ϣ
	///						//������������ĳ�ʼ��
	///						......
	///					}
	///				}
	///				
	///				......
	///			}
	///			
	///			//����Ĵ����У���������������ý���Ϣ��
	///			IConfigSetting mySetting = MyAppModule.Current.Setting;
	///			......
	///			......
	///		</code>
	///	��Ȼ��Ҳ����ʵ�ֶ���ģʽ��Ĭ��Ϊ����ģʽ��������ע�⣬<c>Init</c>����ֻ��һ�α���ܵ��õĻ���
	/// </remarks>
	public class AppModule<T> : AppModule where T : AppModule<T>
	{
		/// <summary>
		/// �������캯��
		/// </summary>
		protected AppModule() {
			if(instance == null) {
				instance = (T)this;
			}
		}

		/// <summary>
		/// ��ȡ��ģ���ʵ���������ǵ���ģʽҲ�����Ƕ���ģʽ����ʵ���߾�����
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return instance;
		}

		/// <summary>
		/// ��ģ��ľ�̬ʵ��
		/// </summary>
		protected static T instance;

		/// <summary>
		/// ��ȡ��ģ��ĵ�ǰʵ��������еĻ���
		/// </summary>
		public static T Current {
			get { return instance; }
		}
	}

	/// <summary>
	/// Ӧ�ó���ģ�飬��μ����Ͱ汾��˵��<see cref="AppModule{T}"/>
	/// </summary>
	public class AppModule : CoreModule
	{
		/// <summary>
		/// �������캯��
		/// </summary>
		protected AppModule() {
		}

		/// <summary>
		/// ��ȡ��ģ���ʵ���������ǵ���ģʽҲ�����Ƕ���ģʽ����ʵ���߾�����
		/// </summary>
		/// <returns>IModule</returns>
		public override IModule GetInstance() {
			return this;
		}
	}
}
