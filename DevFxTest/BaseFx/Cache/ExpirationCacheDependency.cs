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

namespace HTB.DevFx.Cache
{
	/// <summary>
	/// ʱ����ڵĹ��ڲ��ԣ��������ʱ����ڡ�����ʱ����ڣ�
	/// </summary>
	[Serializable]
	public class ExpirationCacheDependency : ICacheDependency
	{
		/// <summary>
		/// ���캯��������ʱ����ڷ�ʽ��
		/// </summary>
		/// <param name="absoluteExperation">���Թ���ʱ��</param>
		public ExpirationCacheDependency(DateTime absoluteExperation) {
			this.absoluteExperation = absoluteExperation;
			this.slidingExperation = TimeSpan.MaxValue;
			this.isSliding = false;
		}

		/// <summary>
		/// ���캯�������ʱ����ڷ�ʽ��
		/// </summary>
		/// <param name="slidingExperation">��Թ���ʱ��</param>
		public ExpirationCacheDependency(TimeSpan slidingExperation) {
			this.slidingExperation = slidingExperation;
			this.absoluteExperation = DateTime.Now.Add(slidingExperation);
			this.isSliding = true;
		}

		private bool isSliding;
		private DateTime absoluteExperation;
		private TimeSpan slidingExperation;

		#region ICacheDependency Members

		/// <summary>
		/// �Ƿ��ѹ���
		/// </summary>
		public bool IsExpired {
			get {
				return (this.absoluteExperation < DateTime.Now);
			}
		}

		/// <summary>
		/// ���û�����ԣ��൱�����¿�ʼ���棩����������ʱ����ڲ�����Ч
		/// </summary>
		public void Reset() {
			if(this.isSliding) {
				this.absoluteExperation = DateTime.Now.Add(this.slidingExperation);
			}
		}

		#endregion
	}
}
