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
	/// ����WEB��Ŀ��һЩʵ�÷���
	/// </summary>
	public static class WebHelper
	{
		#region Url Util

		/// <summary>
		/// �ж�Url�Ƿ���ͬ��QueryString��������ͬ��
		/// </summary>
		/// <param name="expectedUrl">���Ƚϵ�Url</param>
		/// <param name="httpRequest">HttpRequestʵ��</param>
		/// <returns>��ͬ����true</returns>
		public static bool IsUrlEquals(string expectedUrl, HttpRequest httpRequest) {
			if(expectedUrl == null || expectedUrl.Length == 0 || httpRequest == null) {
				return false;
			}

			string url = MakeUrlRelative(httpRequest.Url.AbsolutePath, httpRequest.ApplicationPath);
			return expectedUrl.ToLower().StartsWith(url.ToLower());
		}

		/// <summary>
		/// ��ָ����Urlת������Ե�ַ����"~/"��ͷ��
		/// </summary>
		/// <param name="url">��ת����Url</param>
		/// <param name="basePath">����ַ����������Ŀ¼�ĵ�ַ��/WebApplication1��</param>
		/// <returns>ת�������Ե�ַ</returns>
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
		/// ��ָ����·����Ϣ����Ե�ַ����"~/"��ͷ���ϲ��ɾ���·������"/"��ͷ��
		/// </summary>
		/// <param name="basePath">����ַ����������Ŀ¼�ĵ�ַ��/WebApplication1��</param>
		/// <param name="url">��ת����Url</param>
		/// <param name="includeQueryString">�Ƿ����Url����</param>
		/// <returns>ת����ľ��Ե�ַ�����url������"~/"��ͷ���򷵻�ԭurl</returns>
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
		/// ��/ֵ����ת����QueryString��ʽ
		/// </summary>
		/// <param name="collection">��/ֵ����</param>
		/// <param name="encoding">����</param>
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
		/// ��/ֵ����ת����QueryString��ʽ
		/// </summary>
		/// <param name="collection">��/ֵ����</param>
		/// <param name="encodingName">��������</param>
		/// <returns>QueryString</returns>
		public static string NameValueCollectionToQueryString(NameValueCollection collection, string encodingName) {
			return NameValueCollectionToQueryString(collection, Encoding.GetEncoding(encodingName));
		}

		#endregion Url Util

		#region WebControl Util

		/// <summary>
		/// ��ȫ����<see cref="DropDownList"/>��ֵ
		/// </summary>
		/// <param name="control">��Ҫ���õ�<see cref="DropDownList"/></param>
		/// <param name="value">���õ�ֵ</param>
		/// <returns>�ҵ���<see cref="ListItem"/>�����û�ҵ����򷵻ؿ�����</returns>
		/// <remarks>
		/// ���<paramref name="control" />��<see cref="DropDownList"/>ʵ��û��ָ����<paramref name="value" />����ô�������κβ���
		/// </remarks>
		public static ListItem SetDropDownListValueSafely(DropDownList control, string value) {
			return SetListControlSafely(control, value, null, false, false, null, null);
		}

		/// <summary>
		/// ��ȫ����<see cref="ListControl"/>��ֵ
		/// </summary>
		/// <param name="listCtrl">��Ҫ���õ� see cref="ListControl"/></param>
		/// <param name="value">���õ�ֵ</param>
		/// <param name="text">���õ�ֵ��</param>
		/// <param name="addIfNotExists">����������Ƿ��������</param>
		/// <param name="addEmptyItem">�Ƿ���ӿ���</param>
		/// <param name="eventHandler">���ֵ��ѡ�У�����ô�ί��</param>
		/// <param name="e">ί���¼�����</param>
		/// <returns>�ҵ���<see cref="ListItem"/>�����û�ҵ����򷵻ؿ�����</returns>
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
		/// ����ָ�����ƵĿؼ�
		/// </summary>
		/// <param name="control">�ڴ˿ؼ��²���</param>
		/// <param name="controlID">�����ҿؼ�������</param>
		/// <returns>�����ҵĿռ�</returns>
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
		/// �ڿͻ�����ʾ��Ϣ
		/// </summary>
		/// <param name="message">��Ҫ��ʾ����Ϣ</param>
		/// <param name="clientScript">��Ҫ�ڿͻ���ִ�е�JavaScript���루����д��ת��ű��ȣ�</param>
		/// <param name="endResponse">�Ƿ�����˴�����</param>
		/// <param name="doAlert">�Ƿ���Ҫ�ڿͻ��˵���һ��Alert�Ի���</param>
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
		/// ���ַ�������BASE64����
		/// </summary>
		/// <param name="inputString">ԭ�ַ���</param>
		/// <param name="encodingName">�����ʽ��</param>
		/// <returns>�������ַ���</returns>
		public static string ToBase64(string inputString, string encodingName) {
			return Convert.ToBase64String(Encoding.GetEncoding(encodingName).GetBytes(inputString));
		}

		/// <summary>
		/// ��BASE64�ַ������н���
		/// </summary>
		/// <param name="base64String">��������ַ���</param>
		/// <param name="encodingName">�����ʽ��</param>
		/// <returns>�������ַ���</returns>
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
		/// ���ֽ�����ת����16���Ʊ�ʾ���ַ���
		/// </summary>
		/// <param name="sArray">�ֽ�����</param>
		/// <returns>16���Ʊ�ʾ���ַ���</returns>
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
		/// ��16���Ʊ�ʾ���ַ���ת�����ֽ�����
		/// </summary>
		/// <param name="hexString">���Ʊ�ʾ���ַ���</param>
		/// <returns>�ֽ�����</returns>
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
				throw new ArgumentException("��Ч��16�����ַ�����ʽ");
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
		/// ��ȡ����·��
		/// </summary>
		/// <param name="basePath">����·��</param>
		/// <param name="path">���·��</param>
		/// <param name="createdIfNotExists">���Ŀ¼�����ڣ��Ƿ��Զ�����</param>
		/// <returns>����·��</returns>
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
		/// ��ȡ���Ӧ��ϵͳ����Ŀ¼�ľ���·��
		/// </summary>
		/// <param name="path">���·��</param>
		/// <returns>����·��</returns>
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