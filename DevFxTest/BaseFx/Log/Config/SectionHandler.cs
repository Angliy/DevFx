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
using System.Configuration;
using HTB.DevFx.Config.DotNetConfig;

namespace HTB.DevFx.Log.Config
{
	/// <summary>
	/// 日志的配置信息
	/// </summary>
	/// <remarks>
	/// 配置文件格式和说明：
	///		<code>
	///			&lt;configSections&gt;
	///				&lt;sectionGroup name="htb.devfx" type="HTB.DevFx.Config.GroupHandler, HTB.DevFx.BaseFx"&gt;
	///					&lt;section name="log" type="HTB.DevFx.Log.Config.SectionHandler, HTB.DevFx.BaseFx" /&gt;
	///					......
	///				&lt;/sectionGroup&gt;
	///			&lt;/configSections&gt;
	/// 
	///			......
	/// 
	///			&lt;htb.devfx&gt;
	///				&lt;log&gt;
	///					......
	///				&lt;/log&gt;
	///			&lt;/htb.devfx&gt;
	///			......
	///		</code>
	/// </remarks>
	public class SectionHandler : SectionBaseHandler<SectionHandler>
	{
		/// <summary>
		/// 日志保存的路径（可为相对应用程序的路径）
		/// </summary>
		[ConfigurationProperty("logPath", DefaultValue = ".\\Logs\\")]
		public string LogPath {
			get { return (string)this["logPath"]; }
		}

		/// <summary>
		/// 日志文件名规则，遵循<see cref="DateTime"/>的格式化表达式
		/// </summary>
		[ConfigurationProperty("logFile", DefaultValue = @"\\yyyy\\MM\\dd\\HH\.\t\x\t")]
		public string LogFile {
			get { return (string)this["logFile"]; }
		}
	}
}
