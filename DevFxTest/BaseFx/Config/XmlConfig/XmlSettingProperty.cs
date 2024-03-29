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

using System.Text;
using System.Xml;

namespace HTB.DevFx.Config.XmlConfig
{
	/// <summary>
	/// 使用XML实现配置节属性：<see cref="SettingProperty"/>
	/// </summary>
	public class XmlSettingProperty : SettingProperty
	{
		#region constructor

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="properties">属性集合</param>
		/// <param name="readonly">是否只读</param>
		protected XmlSettingProperty(SettingValueCollection properties, bool @readonly) : base(properties, @readonly) {}

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="readonly">是否只读</param>
		public XmlSettingProperty(bool @readonly) : base(new SettingValueCollection(), @readonly) { }

		/// <summary>
		/// 使用XmlNode初始化
		/// </summary>
		/// <param name="xmlNode">XmlNode</param>
		/// <param name="readonly">是否只读</param>
		public XmlSettingProperty(XmlNode xmlNode, bool @readonly) : this(@readonly) {
			this.InitData(xmlNode, @readonly);
		}

		#endregion

		#region private members

		/// <summary>
		/// 初始化XML结点
		/// </summary>
		private void InitData(XmlNode xmlNode, bool @readonly) {
			foreach(XmlNode attribute in xmlNode.Attributes) {
				string name = attribute.Name;
				string @value = attribute.Value;
				this.properties.Set(new XmlSettingValue(name, @value, @readonly));
			}
		}

		#endregion

		/// <summary>
		/// 创建配置属性实例
		/// </summary>
		/// <param name="properties">属性集合</param>
		/// <param name="readonly">是否只读</param>
		/// <returns>SettingProperty</returns>
		protected override SettingProperty CreateSettingProperty(SettingValueCollection properties, bool @readonly) {
			return new XmlSettingProperty(properties, @readonly);
		}

		/// <summary>
		/// 创建配置值
		/// </summary>
		/// <param name="name">配置值名</param>
		/// <param name="value">配置值</param>
		/// <param name="readonly">是否只读</param>
		/// <returns>SettingValue</returns>
		protected override SettingValue CreateSettingValue(string name, string value, bool @readonly) {
			return new XmlSettingValue(name, value, @readonly);
		}

		/// <summary>
		/// 转换成字符串格式
		/// </summary>
		/// <returns>字符串</returns>
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			foreach(SettingValue value in this.properties.Values) {
				sb.AppendFormat(" {0}", value);
			}
			if(sb.Length > 0) {
				sb.Remove(0, 1);
			}
			return sb.ToString();
		}
	}
}