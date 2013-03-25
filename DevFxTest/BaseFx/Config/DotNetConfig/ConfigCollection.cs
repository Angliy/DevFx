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

namespace HTB.DevFx.Config.DotNetConfig
{
	/// <summary>
	/// ���ü��ϣ����ͣ�
	/// </summary>
	/// <typeparam name="T">����Ԫ������</typeparam>
	public class ConfigCollection<T> : BaseConfigurationElementCollection where T : ConfigurationElement, new()
	{
		/// <summary>
		/// ��������ʽ��ȡԪ��
		/// </summary>
		/// <param name="index">����</param>
		/// <returns>Ԫ��</returns>
		public virtual T this[int index] {
			get { return (T)this.BaseGet(index); }
		}

		/// <summary>
		/// ����ֵ��ʽ��ȡԪ��
		/// </summary>
		/// <param name="key">��ֵ</param>
		/// <returns>Ԫ��</returns>
		public virtual T this[object key] {
			get { return (T)this.BaseGet(key); }
		}

		/// <summary>
		/// ����ת��������
		/// </summary>
		/// <returns>Ԫ������</returns>
		public virtual T[] ToArray() {
			T[] values = new T[this.Count];
			this.CopyTo(values, 0);
			return values;
		}

		/// <summary>
		/// ������Ԫ��
		/// </summary>
		/// <returns>��Ԫ��</returns>
		protected override ConfigurationElement CreateNewElement() {
			return new T();
		}
	}
}