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
using System.Reflection;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Config
{
	/// <summary>
	/// ����Ԫ����
	/// </summary>
	/// <remarks>
	/// ����ṩ�����ļ���ַ�����ù�������һ�ַ�ʽ<br />
	/// �÷�����Ӧ�ó������������Ԫ���ԣ�ע��Priority�������1000����������
	///		<code>
	///			[assembly: DevFxConfig(RealType=typeof(HTB.DevFx.Config.XmlConfigImpl.ConfigManager), Priority = 1000)]
	///		</code>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
	public class DevFxConfigAttribute : Attribute
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public DevFxConfigAttribute() {}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configFile">ָ�������ļ���ַ</param>
		public DevFxConfigAttribute(string configFile) {
			this.configFile = configFile;
		}

		private string configFile = null;
		private int priority = 0;
		private Type realType = null;
		private string typeName = null;

		/// <summary>
		/// ��ȡ/���������ļ�
		/// </summary>
		public string ConfigFile {
			get { return this.configFile; }
			set { this.configFile = value; }
		}

		/// <summary>
		/// ��ȡ/���ô����õ����ȼ�
		/// </summary>
		public int Priority {
			get { return this.priority; }
			set { this.priority = value; }
		}

		/// <summary>
		/// ��ȡ/���ô����ö�Ӧ�����ù���������
		/// </summary>
		public Type RealType {
			get { return this.realType; }
			set { this.realType = value; }
		}

		/// <summary>
		/// ��ȡ/���ô����ù�������������
		/// </summary>
		public string TypeName {
			get { return this.typeName; }
			set { this.typeName = value; }
		}

		/// <summary>
		/// �ӳ����л������Ԫ����
		/// </summary>
		/// <param name="assemblies">���򼯣����Ϊnull����ӵ�ǰӦ�ó������л�ȡ����������г���</param>
		/// <returns>�ҵ�������Ԫ���Ե�����</returns>
		public static DevFxConfigAttribute[] GetConfigAttributeFromAssembly(Assembly[] assemblies) {
			return TypeHelper.GetAttributeFromAssembly<DevFxConfigAttribute>(assemblies);
		}
	}
}