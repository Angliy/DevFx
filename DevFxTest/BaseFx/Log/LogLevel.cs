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

namespace HTB.DevFx.Log
{
	/// <summary>
	/// ��־�ȼ���ϵͳԤ�ã�
	/// </summary>
	/// <remarks>
	/// ��־�ȼ�һ������Ӧ�ó����Լ�����ģ�����ֻ���ṩһ��ĵȼ����࣬Ӧ�ó���Ҳ���Բ���ѭ�˷���
	/// </remarks>
	public static class LogLevel
	{
		/// <summary>
		/// �ȼ����ֵ
		/// </summary>
		public const int MAX = Int32.MaxValue;

		/// <summary>
		/// �����¼�����־�ȼ���120000��
		/// </summary>
		public const int EMERGENCY = 120000;

		/// <summary>
		/// �����¼�����־�ȼ���110000��
		/// </summary>
		public const int FATAL = 110000;

		/// <summary>
		///  �����¼�����־�ȼ���100000��
		/// </summary>
		public const int ALERT = 100000;

		/// <summary>
		/// �����¼�����־�ȼ���70000��
		/// </summary>
		public const int ERROR = 70000;

		/// <summary>
		/// �����¼�����־�ȼ���60000��
		/// </summary>
		public const int WARN = 60000;

		/// <summary>
		/// ֪ͨ�¼�����־�ȼ���50000��
		/// </summary>
		public const int NOTICE = 50000;

		/// <summary>
		/// ��Ϣ�¼�����־�ȼ���40000��
		/// </summary>
		public const int INFO = 40000;

		/// <summary>
		/// �����¼�����־�ȼ���30000��
		/// </summary>
		public const int DEBUG = 30000;

		/// <summary>
		/// �����¼�����־�ȼ���20000��
		/// </summary>
		public const int TRACE = 20000;

		/// <summary>
		/// ����ʾ�κεȼ���-1��
		/// </summary>
		public const int NA = -1;

		/// <summary>
		/// ��С�ȼ�
		/// </summary>
		public const int MIN = Int32.MinValue;

		/// <summary>
		/// �����ƻ�ȡ�ȼ�
		/// </summary>
		/// <param name="levelName">��־�ȼ�����</param>
		/// <returns>�ȼ�����</returns>
		public static int Parse(string levelName) {
			if(levelName == null) {
				throw new LogException("��������ΪNull");
			}
			int level = Parse(levelName, LogLevel.NA);
			if(level == LogLevel.NA) {
				throw new LogException("û�з���ָ�����Ƶļ���");
			}
			return level;
		}

		/// <summary>
		/// �����ƻ�ȡ�ȼ�
		/// </summary>
		/// <param name="levelName">��־�ȼ�����</param>
		/// <param name="defaultValue">���û�ҵ���ȱʡ�ĵȼ�����</param>
		/// <returns>�ȼ�����</returns>
		public static int Parse(string levelName, int defaultValue) {
			if(levelName == null) {
				return defaultValue;
			}
			levelName = levelName.ToUpper();
			switch(levelName) {
				case "MAX":
					return LogLevel.MAX;
				case "EMERGENCY":
					return LogLevel.EMERGENCY;
				case "FATAL":
					return LogLevel.FATAL;
				case "ALERT":
					return LogLevel.ALERT;
				case "ERROR":
					return LogLevel.ERROR;
				case "WARN":
					return LogLevel.WARN;
				case "NOTICE":
					return LogLevel.NOTICE;
				case "INFO":
					return LogLevel.INFO;
				case "DEBUG":
					return LogLevel.DEBUG;
				case "TRACE":
					return LogLevel.TRACE;
				case "MIN":
					return LogLevel.MIN;
				default:
					return defaultValue;
			}
		}

		/// <summary>
		/// ���Դ����ƻ�ȡ�ȼ�
		/// </summary>
		/// <param name="levelName">��־�ȼ�����</param>
		/// <param name="levelValue">����ĵȼ�����</param>
		/// <returns>�Ƿ�ɹ���ȡ�ȼ�����</returns>
		public static bool TryParse(string levelName, ref int levelValue) {
			int level = Parse(levelName, LogLevel.NA);
			if(level == LogLevel.NA) {
				return false;
			} else {
				levelValue = level;
				return true;
			}
		}
	}
}
