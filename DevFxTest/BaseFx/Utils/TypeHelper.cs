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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HTB.DevFx.Data;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// �������͡�ʵ����һЩʵ�÷���
	/// </summary>
	public static class TypeHelper
	{
		/// <summary>
		/// �����������д�������
		/// </summary>
		/// <param name="typeName">������</param>
		/// <param name="throwOnError">ʧ��ʱ�Ƿ��׳��쳣</param>
		/// <returns>Type</returns>
		public static Type CreateType(string typeName, bool throwOnError) {
			return Type.GetType(typeName, throwOnError, false);
		}

		/// <summary>
		/// �������д��������͵�ʵ��
		/// </summary>
		/// <param name="type">����</param>
		/// <param name="expectedType">����������</param>
		/// <param name="throwOnError">ʧ��ʱ�Ƿ��׳��쳣</param>
		/// <param name="parameterTypes">����ʵ����������������б�</param>
		/// <param name="parameterValues">����ʵ������Ĳ���ֵ�б�</param>
		/// <returns>����ʵ��</returns>
		public static object CreateObject(Type type, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues) {
			if (expectedType != null && !expectedType.IsAssignableFrom(type)) {
				if (throwOnError) {
					throw new Exception(string.Format("��Ҫ���������ͣ�{0}���������������ͣ�{1}", type.FullName, expectedType.FullName));
				}
				return null;
			}
			if (parameterTypes != null && parameterValues != null && parameterTypes.Length != parameterValues.Length) {
				if (throwOnError) {
					throw new Exception("���캯���������������Ͳ���������һ��");
				}
			}
			object createdObject = null;
			ConstructorInfo constructor = type.GetConstructor(parameterTypes);
			if (constructor == null) {
				try {
					createdObject = Activator.CreateInstance(type, BindingFlags.CreateInstance | (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance)), null, parameterValues, null);
				} catch (Exception e) {
					if (throwOnError) {
						throw new Exception("�������������Ͳ�֧��ָ���Ĺ��캯����" + e.Message, e);
					}
				}
			} else {
				try {
					createdObject = constructor.Invoke(parameterValues);
				} catch (Exception e) {
					throw new Exception("���󴴽�ʧ�ܣ�" + e.Message, e);
				}
			}
			return createdObject;
		}

		/// <summary>
		/// �������д��������͵�ʵ������������֧�ֲ�����ΪNull�Ĺ��캯����
		/// </summary>
		/// <param name="type">����</param>
		/// <param name="expectedType">����������</param>
		/// <param name="throwOnError">ʧ��ʱ�Ƿ��׳��쳣</param>
		/// <param name="parameters">����ʵ������Ĳ���ֵ�б�</param>
		/// <returns>����ʵ��</returns>
		public static object CreateObject(Type type, Type expectedType, bool throwOnError, params object[] parameters) {
			int paramNum = 0;
			if (parameters != null) {
				paramNum = parameters.Length;
			}
			Type[] paramTypes = new Type[paramNum];
			object[] paramValues = new object[paramNum];
			for (int i = 0; i < paramNum; i++) {
				if (parameters[i] == null) {
					if (throwOnError) {
						throw new Exception("��֧�ֲ�����ΪNull�Ĺ��캯������ʹ�ñ��������������ذ汾");
					} else {
						return null;
					}
				}
				paramTypes[i] = parameters[i].GetType();
				paramValues[i] = parameters[i];
			}
			return CreateObject(type, expectedType, throwOnError, paramTypes, paramValues);
		}

		/// <summary>
		/// ���������д��������͵�ʵ��
		/// </summary>
		/// <param name="typeName">������</param>
		/// <param name="expectedType">����������</param>
		/// <param name="throwOnError">ʧ��ʱ�Ƿ��׳��쳣</param>
		/// <param name="parameters">����ʵ������Ĳ���ֵ�б�</param>
		/// <returns>����ʵ��</returns>
		public static object CreateObject(string typeName, Type expectedType, bool throwOnError, params object[] parameters) {
			Type type = CreateType(typeName, throwOnError);
			return CreateObject(type, expectedType, throwOnError, parameters);
		}

		/// <summary>
		/// ���������д��������͵�ʵ��
		/// </summary>
		/// <param name="typeName">������</param>
		/// <param name="expectedType">����������</param>
		/// <param name="throwOnError">ʧ��ʱ�Ƿ��׳��쳣</param>
		/// <param name="parameterTypes">����ʵ����������������б�</param>
		/// <param name="parameterValues">����ʵ������Ĳ���ֵ�б�</param>
		/// <returns>����ʵ��</returns>
		public static object CreateObject(string typeName, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues) {
			Type type = CreateType(typeName, throwOnError);
			return CreateObject(type, expectedType, throwOnError, parameterTypes, parameterValues);
		}

		/// <summary>
		/// ʹ�÷�����÷���
		/// </summary>
		/// <param name="obj">����ʵ��</param>
		/// <param name="methodName">������</param>
		/// <param name="parameters">�����б�</param>
		/// <returns>��������ֵ</returns>
		public static object Invoke(object obj, string methodName, params object[] parameters) {
			if (obj == null) {
				return obj;
			}
			return obj.GetType().GetMethod(methodName, FieldMemberInfo.FieldBindingFlags).Invoke(obj, parameters);
		}

		/// <summary>
		/// �ڵ�ǰӦ�ó������в���ָ��������
		/// </summary>
		/// <param name="typeName">����ȫ�������������ռ䣩</param>
		/// <returns>�ҵ��򷵻�ָ�������ͣ����򷵻ؿ�</returns>
		public static Type FindType(string typeName) {
			Type type = null;
			List<string> files = new List<string>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				type = assembly.GetType(typeName, false);
				if (type != null) {
					break;
				} else if(!assembly.GlobalAssemblyCache) {
					files.Add(assembly.ManifestModule.ScopeName.ToLower());
				}
			}
			if(type == null) {
				string[] fileNames = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll", SearchOption.TopDirectoryOnly);
				foreach (string file in fileNames) {
					string fileName = Path.GetFileName(file);
					if (!files.Contains(fileName.ToLower())) {
						string assemblyName = Path.GetFileNameWithoutExtension(fileName);
						string typeFullName = typeName + ", " + assemblyName;
						type = CreateType(typeFullName, false);
						if(type != null) {
							break;
						}
					}
				}
			}
			return type;
		}

		/// <summary>
		/// �ӳ����л��Ԫ����
		/// </summary>
		/// <param name="assemblies">���򼯣����Ϊnull����ӵ�ǰӦ�ó������л�ȡ����������г���</param>
		/// <returns>�ҵ���Ԫ���Ե�����</returns>
		public static T[] GetAttributeFromAssembly<T>(Assembly[] assemblies) where T : Attribute {
			List<T> list = new List<T>();
			T[] attributes = null;
			if (assemblies == null) {
				assemblies = AppDomain.CurrentDomain.GetAssemblies();
			}
			foreach (Assembly assembly in assemblies) {
				attributes = (T[])assembly.GetCustomAttributes(typeof(T), false);
				if (attributes != null && attributes.Length > 0) {
					list.AddRange(attributes);
				}
			}
			return list.ToArray();
		}

		/// <summary>
		/// ������ʱ�Ķ�ջ�л�ȡԪ����
		/// </summary>
		/// <param name="includeAll">�Ƿ������ջ�����е�Ԫ����</param>
		/// <typeparam name="T">Ԫ��������</typeparam>
		/// <returns>�ҵ���Ԫ���Ե�����</returns>
		public static T[] GetAttributeFromRuntimeStack<T>(bool includeAll) where T : Attribute {
			var list = new List<T>();
			var t = new StackTrace();
			for (var i = 0; i < t.FrameCount; i++) {
				var f = t.GetFrame(i);
				var m = (MethodInfo)f.GetMethod();
				var a = Attribute.GetCustomAttributes(m, typeof(T)) as T[];
				if (a != null && a.Length > 0) {
					list.AddRange(a);
					if (!includeAll) {
						break;
					}
				}
			}
			return list.ToArray();
		}
	}
}
