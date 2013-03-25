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

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// ֵת�����ض����͵Ľӿ�
	/// </summary>
	public interface IConverting
	{
		#region Converting

		/// <summary>
		/// ֵת����String����
		/// </summary>
		/// <returns>String</returns>
		string ToString();

		/// <summary>
		/// ֵת����Byte����
		/// </summary>
		/// <returns>Byte</returns>
		byte ToByte();

		/// <summary>
		/// ֵת����Char����
		/// </summary>
		/// <returns>Char</returns>
		char ToChar();

		/// <summary>
		/// ֵת����DateTime����
		/// </summary>
		/// <returns>DateTime</returns>
		DateTime ToDateTime();

		/// <summary>
		/// ֵת����Int16���ͣ�C#Ϊshort��
		/// </summary>
		/// <returns>Int16</returns>
		short ToInt16();

		/// <summary>
		/// ֵת����Int32���ͣ�C#Ϊint��
		/// </summary>
		/// <returns>Int32</returns>
		int ToInt32();

		/// <summary>
		/// ֵת����Int64���ͣ�C#Ϊlong��
		/// </summary>
		/// <returns></returns>
		long ToInt64();

		/// <summary>
		/// ֵת����SByte����
		/// </summary>
		/// <returns>SByte</returns>
		sbyte ToSByte();

		/// <summary>
		/// ֵת����Double����
		/// </summary>
		/// <returns>Double</returns>
		double ToDouble();

		/// <summary>
		/// ֵת����Decimal����
		/// </summary>
		/// <returns>Decimal</returns>
		decimal ToDecimal();

		/// <summary>
		/// ֵת����Single���ͣ�C#Ϊfloat��
		/// </summary>
		/// <returns>Single</returns>
		float ToSingle();

		/// <summary>
		/// ֵת����Single���ͣ�C#Ϊushort��
		/// </summary>
		/// <returns>UInt16</returns>
		ushort ToUInt16();

		/// <summary>
		/// ֵת����UInt32���ͣ�C#Ϊuint��
		/// </summary>
		/// <returns>UInt32</returns>
		uint ToUInt32();

		/// <summary>
		/// ֵת����UInt64���ͣ�C#Ϊulong��
		/// </summary>
		/// <returns>UInt64</returns>
		ulong ToUInt64();

		/// <summary>
		/// ֵת����Boolean���ͣ�C#Ϊbool��
		/// </summary>
		/// <returns>Boolean</returns>
		bool ToBoolean();

		/// <summary>
		/// ����������Type��Ϣ����ת����Type
		/// </summary>
		/// <returns>Type</returns>
		Type ToType();

		/// <summary>
		/// ����������Type��Ϣ����ת����Type
		/// </summary>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <returns>Type</returns>
		Type ToType(bool throwError);

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <returns>����</returns>
		object ToObject();

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="parameters">���캯���Ĳ���</param>
		/// <returns>����</returns>
		object ToObject(params object[] parameters);

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="expectedType">����������</param>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <param name="parameters">���캯���Ĳ���</param>
		/// <returns>����</returns>
		object ToObject(Type expectedType, bool throwError, params object[] parameters);

		/// <summary>
		/// ����ֵ��������
		/// </summary>
		/// <param name="expectedType">����������</param>
		/// <param name="throwError">�Ƿ��׳��쳣</param>
		/// <param name="paramTypes">���캯���Ĳ��������б�</param>
		/// <param name="paramValues">���캯���Ĳ����б�</param>
		/// <returns>����</returns>
		object ToObject(Type expectedType, bool throwError, Type[] paramTypes, object[] paramValues);

		/// <summary>
		/// ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <returns>ת���������ʵ��</returns>
		T ToObject<T>();

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="parameters">�ɱ�����б�</param>
		/// <returns>ת���������ʵ��</returns>
		T ToObject<T>(params object[] parameters);

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="throwError">���ʧ�ܣ��Ƿ��׳��쳣</param>
		/// <param name="parameters">�ɱ�����б�</param>
		/// <returns>ת���������ʵ��</returns>
		T ToObject<T>(bool throwError, params object[] parameters);

		/// <summary>
		/// ת����ָ�����ͣ���������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="throwError">���ʧ�ܣ��Ƿ��׳��쳣</param>
		/// <param name="paramTypes">���������б�</param>
		/// <param name="paramValues">����ֵ�б�</param>
		/// <returns>ת���������ʵ��</returns>
		T ToObject<T>(bool throwError, Type[] paramTypes, object[] paramValues);

		/// <summary>
		/// ����ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <returns>ת���������ʵ��</returns>
		T TryToObject<T>();

		/// <summary>
		/// ����ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת���ɵ�����</typeparam>
		/// <param name="defaultValue">���ת��ʧ�ܷ��ص�ȱʡֵ</param>
		/// <returns>ת���������ʵ��</returns>
		T TryToObject<T>(T defaultValue);

		#endregion Converting
	}
}