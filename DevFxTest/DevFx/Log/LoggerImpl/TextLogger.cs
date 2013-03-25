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
using System.Text;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Log.LoggerImpl
{
	/// <summary>
	/// ���ı��ļ���ʽ����־��¼��
	/// </summary>
	/// <remarks>
	/// ���ø�ʽ��
	///		<code>
	///			&lt;htb.devfx&gt;
	///				......
	///				
	///				&lt;log type="HTB.DevFx.Log.LogManager"&gt;
	///					&lt;loggers&gt;
	///						......
	///						&lt;logger name="textLogger" minLevel="min" maxLevel="max" type="HTB.DevFx.Log.LoggerImpl.TextLogger"&gt;
	///							&lt;!--ע����ʹ��ʱ������directory���ԣ���ָ����־��ŵ�Ŀ¼��filenameFormatΪ��־�ļ�����ʽ����ѭ.NET��ʽ���﷨--&gt;
	///							&lt;file filenameFormat="yyyy-MM-dd\.\l\o\g" directory="��־�ļ���Ŀ¼��ַ" /&gt;
	///						&lt;/logger>
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	/// </remarks>
	public class TextLogger : Logger
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public TextLogger() : base() {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">��Ӧ�����ý�</param>
		public TextLogger(IConfigSetting setting) : base(setting) {
		}

		private string directory;
		private string filenameFormat;

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public override void Init(IConfigSetting setting) {
			if(!this.isInit) {
				base.Init (setting);
				string directory = setting["file"].Property["directory"].Value;
				this.directory = WebHelper.GetFullPath(directory);
				this.filenameFormat = setting["file"].Property["filenameFormat"].Value;
			}
		}

		/// <summary>
		/// ��־��¼
		/// </summary>
		/// <param name="source">��־��Դ</param>
		/// <param name="level">��־���𣨾�����������</param>
		/// <param name="message">��־��Ϣ</param>
		/// <returns>���ش�����</returns>
		public override IAOPResult Log(object source, int level, string message) {
			IAOPResult result = base.Log(source, level, message);
			if(result.IsFailed) {
				return result;
			}

			source = this.GetExactSourc(source);
			StringBuilder msg = new StringBuilder();
			msg.AppendFormat("[{0}]Level={2}, Source={1}\r\n{3}\r\n---------------------------------", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), source, level, message);
			LogHelper.WriteLog(this.directory, this.filenameFormat, msg);
			return new AOPResult(0);
		}
	}
}
