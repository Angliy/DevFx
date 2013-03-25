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
using System.Timers;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.Utils.Mail;

namespace HTB.DevFx.Log.LoggerImpl
{
	/// <summary>
	/// ���ʼ����ͷ�ʽ����־��¼��
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
	///						&lt;logger name="mailLogger" minLevel="min" maxLevel="max" type="HTB.DevFx.Log.LoggerImpl.MailLogger"&gt;
	///							&lt;!--bufferSizeΪ�ռ�������־������ȥ�����ʼ���mailFormΪ�����˵�ַ��mailSubjectΪ�ʼ����⣬mailListΪ�ʼ��������б������������Ӣ�ķֺš�;������--&gt;
	///							&lt;mail bufferSize="5" mailFrom="������&lt;group@devfx.net&gt;" mailSubject="����ͳһ�������ϵͳ��־" mailList="R2@DevFx.NET" /&gt;
	///						&lt;/logger&gt;
	///						......
	///					&lt;/loggers&gt;
	///				&lt;/log&gt;
	///				
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	����¼������<see cref="HTB.DevFx.Utils.Mail.MailSender"/>����ȷ����
	/// </remarks>
	public class MailLogger : Logger
	{
		#region constructor
		
		/// <summary>
		/// ���캯��
		/// </summary>
		public MailLogger() : base() {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="setting">��Ӧ�����ý�</param>
		public MailLogger(IConfigSetting setting) : base(setting) {
		}

		#endregion

		#region private members

		private int msgCount;
		private int bufferSize;
		private string mailFrom;
		private string mailSubject;
		private string mailList;
		private Timer timer;
		private StringBuilder msg;

		/// <summary>
		/// ��ʼ���뷢�ʼ�����
		/// </summary>
		private void SendMail() {
			this.timer = new Timer(1000);
			this.timer.Enabled = false;
			this.timer.AutoReset = false;
			this.timer.Elapsed += new ElapsedEventHandler(TimerOnElapsed);
			this.timer.Start();
		}

		/// <summary>
		/// ���ʼ�
		/// </summary>
		/// <param name="force">�Ƿ�ǿ�Ʒ��ͣ���������־���Ƿ񵽴�BufferSize</param>
		private void SendMessage(bool force) {
			lock(this.msg) {
				if((force || this.msgCount >= this.bufferSize) && this.msg.Length > 0) {
					try {
						MailSender.Send(this.mailFrom, this.mailList, this.mailSubject, this.msg.ToString());
						this.msg.Remove(0, this.msg.Length);
						this.msgCount = 0;
					} catch { }
				}
			}
		}

		private void TimerOnElapsed(object sender, ElapsedEventArgs e) {
			this.timer.Enabled = false;

			this.SendMessage(false);

			this.timer.Start();
		}

		/// <summary>
		/// �������������ռ�������־���ͳ�ȥ
		/// </summary>
		~MailLogger() {
			this.SendMessage(true);
		}

		#endregion

		#region override members

		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="setting">���ý�</param>
		public override void Init(IConfigSetting setting) {
			if(!this.isInit) {
				base.Init(setting);
				this.msgCount = 0;
				this.bufferSize = setting["mail"].Property["bufferSize"].ToInt32();
				this.mailFrom = setting["mail"].Property["mailFrom"].Value;
				this.mailSubject = setting["mail"].Property["mailSubject"].Value;
				this.mailList = setting["mail"].Property["mailList"].Value;
				this.msg = new StringBuilder();
				this.SendMail();
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
			msg.AppendFormat("[{0}]Level={2}, Source={1}\r\n{3}\r\n---------------------------\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), source, level, message);
			msgCount++;

			return new AOPResult(0);
		}

		#endregion
	}
}
