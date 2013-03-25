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
	/// 配置元属性
	/// </summary>
	/// <remarks>
	/// 这个提供配置文件地址和配置管理器的一种方式<br />
	/// 用法，在应用程序中添加如下元属性，注意Priority必须大于1000才能起作用
	///		<code>
	///			[assembly: DevFxConfig(RealType=typeof(HTB.DevFx.Config.XmlConfigImpl.ConfigManager), Priority = 1000)]
	///		</code>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
	public class DevFxConfigAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public DevFxConfigAttribute() {}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="configFile">指定配置文件地址</param>
		public DevFxConfigAttribute(string configFile) {
			this.configFile = configFile;
		}

		private string configFile = null;
		private int priority = 0;
		private Type realType = null;
		private string typeName = null;

		/// <summary>
		/// 获取/设置配置文件
		/// </summary>
		public string ConfigFile {
			get { return this.configFile; }
			set { this.configFile = value; }
		}

		/// <summary>
		/// 获取/设置此配置的优先级
		/// </summary>
		public int Priority {
			get { return this.priority; }
			set { this.priority = value; }
		}

		/// <summary>
		/// 获取/设置此配置对应的配置管理器类型
		/// </summary>
		public Type RealType {
			get { return this.realType; }
			set { this.realType = value; }
		}

		/// <summary>
		/// 获取/设置此配置管理器的类型名
		/// </summary>
		public string TypeName {
			get { return this.typeName; }
			set { this.typeName = value; }
		}

		/// <summary>
		/// 从程序集中获得配置元属性
		/// </summary>
		/// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
		/// <returns>找到的配置元属性的数组</returns>
		public static DevFxConfigAttribute[] GetConfigAttributeFromAssembly(Assembly[] assemblies) {
			return TypeHelper.GetAttributeFromAssembly<DevFxConfigAttribute>(assemblies);
		}
	}
}