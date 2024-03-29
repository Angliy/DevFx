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
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTB.DevFx.Utils
{
	/// <summary>
	/// 关于WEB项目的一些实用方法
	/// </summary>
	public static class WebHelper
	{
		#region Url Util

		/// <summary>
		/// 判断Url是否相同（QueryString参数允许不同）
		/// </summary>
		/// <param name="expectedUrl">被比较的Url</param>
		/// <param name="httpRequest">HttpRequest实例</param>
		/// <returns>相同返回true</returns>
		public static bool IsUrlEquals(string expectedUrl, HttpRequest httpRequest) {
			if(expectedUrl == null || expectedUrl.Length == 0 || httpRequest == null) {
				return false;
			}

			string url = MakeUrlRelative(httpRequest.Url.AbsolutePath, httpRequest.ApplicationPath);
			return expectedUrl.ToLower().StartsWith(url.ToLower());
		}

		/// <summary>
		/// 把指定的Url转换成相对地址（以"~/"打头）
		/// </summary>
		/// <param name="url">被转换的Url</param>
		/// <param name="basePath">基地址（比如虚拟目录的地址：/WebApplication1）</param>
		/// <returns>转换后的相对地址</returns>
		public static string MakeUrlRelative(string url, string basePath) {
			if(string.IsNullOrEmpty(url)) {
				return "~/";
			}

			if(basePath == null || !basePath.StartsWith("/")) {
				basePath = "/" + basePath;
			}
			if(url.StartsWith("http://", true, null)) {
				Uri uri = new Uri(url);
				url = uri.PathAndQuery;
			} else if (!url.StartsWith("/")) {
				url = "/" + url;
			}
			if(basePath == "/") {
				return "~" + url;
			}
			basePath = basePath.ToLower();
			string url1 = url.ToLower();
			url = url.Substring(url1.IndexOf(basePath) + basePath.Length);
			if(url.StartsWith("/")) {
				url = "~" + url;
			} else {
				url = "~/" + url;
			}
			return url;
		}

		/// <summary>
		/// 把指定的路径信息和相对地址（以"~/"打头）合并成绝对路径（以"/"打头）
		/// </summary>
		/// <param name="basePath">基地址（比如虚拟目录的地址：/WebApplication1）</param>
		/// <param name="url">被转换的Url</param>
		/// <param name="includeQueryString">是否包含Url参数</param>
		/// <returns>转换后的绝对地址，如果url不是以"~/"打头，则返回原url</returns>
		public static string UrlCombine(string basePath, string url, bool includeQueryString) {
			if(basePath == null || !basePath.StartsWith("/")) {
				basePath = "/" + basePath;
			}
			if(basePath.Length > 1 && !basePath.EndsWith("/")) {
				basePath = basePath + "/";
			}
			if(string.IsNullOrEmpty(url)) {
				return basePath;
			}
			if(url.StartsWith("~/")) {
				int length = url.Length - 2;
				if(!includeQueryString) {
					int i = url.IndexOf('?');
					if(i > 2) {
						length = i - 2;
					}
				}
				url = basePath + url.Substring(2, length);
			}
			return url;
		}

		/// <summary>
		/// 名/值集合转换成QueryString形式
		/// </summary>
		/// <param name="collection">名/值集合</param>
		/// <param name="encoding">编码</param>
		/// <returns>QueryString</returns>
		public static string NameValueCollectionToQueryString(NameValueCollection collection, Encoding encoding) {
			StringBuilder sb = new StringBuilder();
			foreach (string name in collection.Keys) {
				string @value = collection[name];
				sb.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(name, encoding), HttpUtility.UrlEncode(@value, encoding));
			}
			return sb.ToString().TrimStart('&');
		}

		/// <summary>
		/// 名/值集合转换成QueryString形式
		/// </summary>
		/// <param name="collection">名/值集合</param>
		/// <param name="encodingName">编码名称</param>
		/// <returns>QueryString</returns>
		public static string NameValueCollectionToQueryString(NameValueCollection collection, string encodingName) {
			return NameValueCollectionToQueryString(collection, Encoding.GetEncoding(encodingName));
		}

		#endregion Url Util

		#region WebControl Util

		/// <summary>
		/// 安全设置<see cref="DropDownList"/>的值
		/// </summary>
		/// <param name="control">需要设置的<see cref="DropDownList"/></param>
		/// <param name="value">设置的值</param>
		/// <returns>找到的<see cref="ListItem"/>，如果没找到，则返回空引用</returns>
		/// <remarks>
		/// 如果<paramref name="control" />的<see cref="DropDownList"/>实例没有指定的<paramref name="value" />，那么不进行任何操作
		/// </remarks>
		public static ListItem SetDropDownListValueSafely(DropDownList control, string value) {
			return SetListControlSafely(control, value, null, false, false, null, null);
		}

		/// <summary>
		/// 安全设置<see cref="ListControl"/>的值
		/// </summary>
		/// <param name="listCtrl">需要设置的 see cref="ListControl"/></param>
		/// <param name="value">设置的值</param>
		/// <param name="text">设置的值名</param>
		/// <param name="addIfNotExists">如果不存在是否添加新项</param>
		/// <param name="addEmptyItem">是否添加空项</param>
		/// <param name="eventHandler">如果值被选中，则调用此委托</param>
		/// <param name="e">委托事件参数</param>
		/// <returns>找到的<see cref="ListItem"/>，如果没找到，则返回空引用</returns>
		public static ListItem SetListControlSafely(ListControl listCtrl, string value, string text, bool addIfNotExists, bool addEmptyItem, EventHandler eventHandler, EventArgs e) {
			if (listCtrl == null) {
				return null;
			}
			ListItem li = null;
			if (value != null) {
				li = listCtrl.Items.FindByValue(value);
				if (li == null && addIfNotExists) {
					if (string.IsNullOrEmpty(text)) {
						text = string.Format("({0})", value);
					}
					li = new ListItem(text, value);
				}
			}
			if (addEmptyItem) {
				listCtrl.Items.Insert(0, string.Empty);
			}
			if (li != null) {
				listCtrl.ClearSelection();
				li.Selected = true;
				if (eventHandler != null) {
					eventHandler(listCtrl, e);
				}
			}
			return li;
		}

		/// <summary>
		/// 查找指定名称的控件
		/// </summary>
		/// <param name="control">在此控件下查找</param>
		/// <param name="controlID">被查找控件的名称</param>
		/// <returns>被查找的空间</returns>
		public static Control FindControl(Control control, string controlID) {
			if (control == control.Page) {
				return control.FindControl(controlID);
			}
			Control control1 = control;
			Control control2 = null;
			while ((control2 == null) && (control1 != control.Page)) {
				control1 = control1.NamingContainer;
				if (control1 == null) {
					throw new Exception("NoNamingContainer");
				}
				control2 = control1.FindControl(controlID);
			}
			return control2;
		}

		#endregion WebControl Util

		#region Web Client Util

		/// <summary>
		/// 在客户端显示消息
		/// </summary>
		/// <param name="message">需要显示的信息</param>
		/// <param name="clientScript">需要在客户端执行的JavaScript代码（可以写入转向脚本等）</param>
		/// <param name="endResponse">是否结束此次请求</param>
		/// <param name="doAlert">是否需要在客户端弹出一个Alert对话框</param>
		/// <example>
		///		<code>
		///			private void Page_Load(object sender, EventArgs e){
		///				// your code here
		///				......
		///				if(failed) {
		///					WebHelper.ShowMessage("submit false, please try again!", "history.back()", true, true);
		///				}
		///				......
		///			}
		///		</code>
		/// </example>
		public static void ShowMessage(string message, string clientScript, bool endResponse, bool doAlert) {
			if(message == null) {
				message = string.Empty;
			}
			message = message.Replace("\"", "\\\"");
			message = message.Replace("\\", "\\\\");
			message = message.Replace("\r", "");
			message = message.Replace("\n", "<br>");

			HttpResponse response = HttpContext.Current.Response;
			string script = @"
					<script language=javascript>
						var msg=""{0}"";
						if(msg) {{
							document.write('<center><font style=""font-size:9pt;color:red"">' + msg + '</font></center>');
							msg = msg.replace(/<br>/ig, '\n');
							if({1}) {{
								alert(msg);
							}}
						}}
						{2}
					</script>
				";
			response.Clear();
			response.Write(string.Format(script, message, doAlert ? "true" : "false", clientScript));
			if(endResponse) {
				response.End();
			}
		}

		#endregion Web Client Util

		#region Base64 Util

		/// <summary>
		/// 把字符串进行BASE64编码
		/// </summary>
		/// <param name="inputString">原字符串</param>
		/// <param name="encodingName">编码格式名</param>
		/// <returns>编码后的字符串</returns>
		public static string ToBase64(string inputString, string encodingName) {
			return Convert.ToBase64String(Encoding.GetEncoding(encodingName).GetBytes(inputString));
		}

		/// <summary>
		/// 对BASE64字符串进行解码
		/// </summary>
		/// <param name="base64String">待解码的字符串</param>
		/// <param name="encodingName">编码格式名</param>
		/// <returns>解码后的字符串</returns>
		public static string FromBase64(string base64String, string encodingName) {
			return Encoding.GetEncoding(encodingName).GetString(Convert.FromBase64String(base64String));
		}

		#endregion Base64 Util

		#region Hex Util

		private static readonly char[] hexValues = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

		private static int ConvertHexDigit(char val) {
			if(val <= '9') {
				return (val - '0');
			}
			if(val >= 'a') {
				return ((val - 'a') + '\n');
			}
			return ((val - 'A') + '\n');
		}

		/// <summary>
		/// 把字节数组转换成16进制表示的字符串
		/// </summary>
		/// <param name="sArray">字节数组</param>
		/// <returns>16进制表示的字符串</returns>
		public static string ToHexString(byte[] sArray) {
			if(sArray == null) {
				return null;
			}
			char[] chArray = new char[sArray.Length * 2];
			int i = 0;
			int k = 0;
			while(i < sArray.Length) {
				int t = (sArray[i] & 240) >> 4;
				chArray[k++] = hexValues[t];
				t = sArray[i] & 15;
				chArray[k++] = hexValues[t];
				i++;
			}
			return new string(chArray);
		}

		/// <summary>
		/// 把16进制表示的字符串转换成字节数组
		/// </summary>
		/// <param name="hexString">进制表示的字符串</param>
		/// <returns>字节数组</returns>
		public static byte[] FromHexString(string hexString) {
			byte[] buffer;
			if(hexString == null) {
				throw new ArgumentNullException("hexString");
			}
			bool flag = false;
			int i = 0;
			int hexLength = hexString.Length;
			if(((hexLength >= 2) && (hexString[0] == '0')) && ((hexString[1] == 'x') || (hexString[1] == 'X'))) {
				hexLength = hexString.Length - 2;
				i = 2;
			}
			if(((hexLength % 2) != 0) && ((hexLength % 3) != 2)) {
				throw new ArgumentException("无效的16进制字符串格式");
			}
			if((hexLength >= 3) && (hexString[i + 2] == ' ')) {
				flag = true;
				buffer = new byte[(hexLength / 3) + 1];
			} else {
				buffer = new byte[hexLength / 2];
			}
			for(int k = 0; i < hexString.Length; k++) {
				int h = ConvertHexDigit(hexString[i]);
				int l = ConvertHexDigit(hexString[i + 1]);
				buffer[k] = (byte)(l | (h << 4));
				if(flag) {
					i++;
				}
				i += 2;
			}
			return buffer;
		}

		#endregion Hex Util

		#region File Util

		/// <summary>
		/// 获取绝对路径
		/// </summary>
		/// <param name="basePath">基本路径</param>
		/// <param name="path">相对路径</param>
		/// <param name="createdIfNotExists">如果目录不存在，是否自动创建</param>
		/// <returns>绝对路径</returns>
		public static string GetPhysicalPath(string basePath, string path, bool createdIfNotExists) {
			if (string.IsNullOrEmpty(basePath) || path == null) {
				return basePath;
			}
			if (basePath.StartsWith("~/")) {
				basePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, basePath.Substring(2));
			} else if (basePath.StartsWith("..")) {
				basePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, basePath);
			}
			if (path.StartsWith("/") || path.StartsWith("\\")) {
				path = path.Substring(1);
			}
			path = Path.Combine(basePath, path);
			path = Path.GetFullPath(path);
			string fileName = Path.GetFileName(path);
			path = Path.GetDirectoryName(path);
			if (!path.EndsWith("\\")) {
				path += "\\";
			}
			if (createdIfNotExists) {
				if (!Directory.Exists(path)) {
					Directory.CreateDirectory(path);
				}
			}
			if (!string.IsNullOrEmpty(fileName)) {
				path = Path.Combine(path, fileName);
			}
			return path;
		}

		/// <summary>
		/// 获取相对应用系统部署目录的绝对路径
		/// </summary>
		/// <param name="path">相对路径</param>
		/// <returns>绝对路径</returns>
		public static string GetFullPath(string path) {
			if(string.IsNullOrEmpty(path)) {
				return path;
			}
			if(Path.IsPathRooted(path)) {
				return path;
			} else {
				return Path.GetFullPath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\" + path);
			}
		}

		#endregion File Util
	}
}