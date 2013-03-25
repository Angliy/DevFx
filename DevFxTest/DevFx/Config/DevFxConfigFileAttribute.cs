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
	/// 配置文件属性
	/// </summary>
	/// <remarks>
	/// 这个提供配置文件合并的一种方式<br />
	/// 用法，在应用程序中添加如下元属性，注意ConfigIndex必须不小于1000才能起作用
	///		<code>
	///			[assembly: DevFxConfigFile(ConfigFile="res://HTB.DevFx.Config.htb.devfx.config", ConfigIndex = 1000)]
	///		</code>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class DevFxConfigFileAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configFile">指定配置文件地址</param>
		/// <param name="fileInType">如果是内置资源，指定此资源所在的<see cref="Assembly"/></param>
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
		/// 获取/设置配置文件，除了硬盘文件，其他必需加前缀
		/// </summary>
		/// <remarks>
		/// 内置资源：res:// <br />
		/// HTTP资源：http://
		/// </remarks>
		public string ConfigFile {
			get { return this.configFile; }
			set { this.configFile = value; }
		}

		/// <summary>
		/// 获取/设置此配置合并时的顺序
		/// </summary>
		public int ConfigIndex {
			get { return this.configIndex; }
			set {
				if(value < 1000) {
					throw new ConfigException("配置文件顺序不能小于1000");
				}
				this.configIndex = value;
			}
		}

		/// <summary>
		/// 获取/设置此资源所在的<see cref="Assembly"/>
		/// </summary>
		public Type FileInType {
			get { return this.fileInType; }
			set { this.fileInType = value; }
		}

		/// <summary>
		/// 获取/设置此资源所在的<see cref="Assembly"/>名称
		/// </summary>
		public string FileInTypeName {
			get { return this.fileInTypeName; }
			set { this.fileInTypeName = value; }
		}

		/// <summary>
		/// 获取此资源所在的<see cref="Assembly"/>
		/// </summary>
		/// <returns>Type</returns>
		public Type GetFileInType() {
			if(this.fileInType == null) {
				this.fileInType = TypeHelper.CreateType(this.fileInTypeName, false);
			}
			return this.fileInType;
		}

		/// <summary>
		/// 从程序集中获得元属性
		/// </summary>
		/// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
		/// <returns>找到的元属性的数组</returns>
		public static DevFxConfigFileAttribute[] GetConfigFileAttributeFromAssembly(Assembly[] assemblies) {
			return TypeHelper.GetAttributeFromAssembly<DevFxConfigFileAttribute>(assemblies);
		}
	}
}