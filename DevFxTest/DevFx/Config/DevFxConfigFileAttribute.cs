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
	/// �����ļ�����
	/// </summary>
	/// <remarks>
	/// ����ṩ�����ļ��ϲ���һ�ַ�ʽ<br />
	/// �÷�����Ӧ�ó������������Ԫ���ԣ�ע��ConfigIndex���벻С��1000����������
	///		<code>
	///			[assembly: DevFxConfigFile(ConfigFile="res://HTB.DevFx.Config.htb.devfx.config", ConfigIndex = 1000)]
	///		</code>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class DevFxConfigFileAttribute : Attribute
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="configFile">ָ�������ļ���ַ</param>
		/// <param name="fileInType">�����������Դ��ָ������Դ���ڵ�<see cref="Assembly"/></param>
		public DevFxConfigFileAttribute(string configFile, Type fileInType) {
			this.configFile = configFile;
			this.fileInType = fileInType;
		}

		internal DevFxConfigFileAttribute(string configFile, Type fileInType, int mergeIndex) : this(configFile, fileInType) {
			this.configIndex = mergeIndex;
		}

		private string configFile = null;
		private int configIndex = 1000;
		private Type fileInType = null;
		private string fileInTypeName = null;

		/// <summary>
		/// ��ȡ/���������ļ�������Ӳ���ļ������������ǰ׺
		/// </summary>
		/// <remarks>
		/// ������Դ��res:// <br />
		/// HTTP��Դ��http://
		/// </remarks>
		public string ConfigFile {
			get { return this.configFile; }
			set { this.configFile = value; }
		}

		/// <summary>
		/// ��ȡ/���ô����úϲ�ʱ��˳��
		/// </summary>
		public int ConfigIndex {
			get { return this.configIndex; }
			set {
				if(value < 1000) {
					throw new ConfigException("�����ļ�˳����С��1000");
				}
				this.configIndex = value;
			}
		}

		/// <summary>
		/// ��ȡ/���ô���Դ���ڵ�<see cref="Assembly"/>
		/// </summary>
		public Type FileInType {
			get { return this.fileInType; }
			set { this.fileInType = value; }
		}

		/// <summary>
		/// ��ȡ/���ô���Դ���ڵ�<see cref="Assembly"/>����
		/// </summary>
		public string FileInTypeName {
			get { return this.fileInTypeName; }
			set { this.fileInTypeName = value; }
		}

		/// <summary>
		/// ��ȡ����Դ���ڵ�<see cref="Assembly"/>
		/// </summary>
		/// <returns>Type</returns>
		public Type GetFileInType() {
			if(this.fileInType == null) {
				this.fileInType = TypeHelper.CreateType(this.fileInTypeName, false);
			}
			return this.fileInType;
		}

		/// <summary>
		/// �ӳ����л��Ԫ����
		/// </summary>
		/// <param name="assemblies">���򼯣����Ϊnull����ӵ�ǰӦ�ó������л�ȡ����������г���</param>
		/// <returns>�ҵ���Ԫ���Ե�����</returns>
		public static DevFxConfigFileAttribute[] GetConfigFileAttributeFromAssembly(Assembly[] assemblies) {
			return TypeHelper.GetAttributeFromAssembly<DevFxConfigFileAttribute>(assemblies);
		}
	}
}