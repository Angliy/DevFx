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
	/// ���ýڻ��������࣬�̳��� <see cref="ConfigurationSection"/>
	/// </summary>
	/// <typeparam name="T">������</typeparam>
	/// <remarks>
	/// ע���� <see cref="BaseConfigurationElement"/> ������
	/// </remarks>
	public abstract class SectionBaseHandler<T> : ConfigurationSection where T : SectionBaseHandler<T>
	{
		/// <summary>
		/// ��ȡ��ǰ���ý�ʵ��
		/// </summary>
		public static T Current {
			get { return GroupHandler.GetSection<T>(false); }
		}

		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ�����л��������Ƿ�����δ֪����
		/// </summary>
		/// <param name="name">�޷�ʶ������Ե�����</param>
		/// <param name="value">�޷�ʶ������Ե�ֵ</param>
		/// <returns>��������л�����������δ֪���ԣ���Ϊ<c>true</c></returns>
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ�����л��������Ƿ�����δ֪Ԫ��
		/// </summary>
		/// <param name="elementName">δ֪����Ԫ�ص�����</param>
		/// <param name="reader">���ڷ����л��� <seealso cref="XmlReader"/> ����</param>
		/// <returns>��������л�����������δ֪Ԫ�أ���Ϊ true</returns>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// �Ƿ�����δ֪�����Ի�Ԫ��
		/// </summary>
		/// <remarks>
		///		<para>���������Ҫ����δ��������ԣ��������д������</para>
		/// </remarks>
		protected virtual bool OnDeserializeUnrecognizedFlag {
			get { return false; }
		}

		/// <summary>
		/// �����ýڶ�Ӧ��Xml
		/// </summary>
		public virtual string OuterXml {
			get { return this.outerXml; }
		}

		private string outerXml;

		/// <summary>
		/// ��ȡ�����ļ��е� XML
		/// </summary>
		/// <param name="reader">�������ļ��н��ж�ȡ������ <seealso cref="XmlReader"/></param>
		/// <param name="serializeCollectionKey">Ϊ <c>true</c>����ֻ���л����ϵļ����ԣ�����Ϊ <c>false</c></param>
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey) {
			FieldInfo field = reader.GetType().GetField("_rawXml", FieldMemberInfo.FieldBindingFlags);
			this.outerXml = (string)field.GetValue(reader);
			base.DeserializeElement(reader, serializeCollectionKey);
		}
	}
}