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

using System.Configuration;
using System.Reflection;
using System.Xml;
using HTB.DevFx.Data;

namespace HTB.DevFx.Config.DotNetConfig
{
	/// <summary>
	/// 配置节基础处理类，继承自 <see cref="ConfigurationSection"/>
	/// </summary>
	/// <typeparam name="T">派生类</typeparam>
	/// <remarks>
	/// 注意与 <see cref="BaseConfigurationElement"/> 的区别
	/// </remarks>
	public abstract class SectionBaseHandler<T> : ConfigurationSection where T : SectionBaseHandler<T>
	{
		/// <summary>
		/// 获取当前配置节实例
		/// </summary>
		public static T Current {
			get { return GroupHandler.GetSection<T>(false); }
		}

		/// <summary>
		/// 获取一个值，该值指示反序列化过程中是否遇到未知属性
		/// </summary>
		/// <param name="name">无法识别的属性的名称</param>
		/// <param name="value">无法识别的属性的值</param>
		/// <returns>如果反序列化过程中遇到未知属性，则为<c>true</c></returns>
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// 获取一个值，该值指示反序列化过程中是否遇到未知元素
		/// </summary>
		/// <param name="elementName">未知的子元素的名称</param>
		/// <param name="reader">用于反序列化的 <seealso cref="XmlReader"/> 对象</param>
		/// <returns>如果反序列化过程中遇到未知元素，则为 true</returns>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// 是否遇到未知的属性或元素
		/// </summary>
		/// <remarks>
		///		<para>派生类如果要允许未定义的属性，则必须重写本属性</para>
		/// </remarks>
		protected virtual bool OnDeserializeUnrecognizedFlag {
			get { return false; }
		}

		/// <summary>
		/// 本配置节对应的Xml
		/// </summary>
		public virtual string OuterXml {
			get { return this.outerXml; }
		}

		private string outerXml;

		/// <summary>
		/// 读取配置文件中的 XML
		/// </summary>
		/// <param name="reader">在配置文件中进行读取操作的 <seealso cref="XmlReader"/></param>
		/// <param name="serializeCollectionKey">为 <c>true</c>，则只序列化集合的键属性；否则为 <c>false</c></param>
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey) {
			FieldInfo field = reader.GetType().GetField("_rawXml", FieldMemberInfo.FieldBindingFlags);
			this.outerXml = (string)field.GetValue(reader);
			base.DeserializeElement(reader, serializeCollectionKey);
		}
	}
}