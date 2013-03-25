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
using HTB.DevFx.Cache;
using HTB.DevFx.Config;
using HTB.DevFx.Core;
using HTB.DevFx.ExceptionManagement;
using HTB.DevFx.Log;

namespace HTB.DevFx
{
	/// <summary>
	/// ͳһ������ܵ�ͳһ�����
	/// </summary>
	/// <remarks>
	/// ��ʹ�ÿ��ǰ��������ñ���� <see cref="Framework.Init()" /> ��̬���������ÿ�ܽ���ǰ�ڳ�ʼ������<br />
	/// �����ļ���ʽ��μ������ļ���htb.devfx.config
	/// </remarks>
	public sealed class Framework : CoreFramework
	{
		static Framework() {
			Init(true);
		}

		/// <summary>
		/// ˽�й��캯������ֹ�ⲿʵ����
		/// </summary>
		private Framework() {}

		private static readonly object initLockObject = new object();
		private static bool initializing;
		private static Framework framework;

		private static void Init(bool initializingBySelf) {
			if(framework == null && !initializing) {
				lock(initLockObject) {
					if(framework == null && !initializing) {
						try {
							initializing = true;
							framework = new Framework();
						} catch(Exception e) {
							LoggorHelper.WriteLog(typeof(Framework), LogLevel.FATAL, e.ToString());
							try {
								Config.Configer.Reset();
							} catch {}

							if(!initializingBySelf) {
								throw;
							}
						} finally {
							initializing = false;
						}
					}
				}
			}
		}

		/// <summary>
		/// ��ܵ�ǰ�ڳ�ʼ��
		/// </summary>
		/// <remarks>
		/// ������ʹ�ÿ��ǰ�Ĵ����е�����˷������������WEB��Ŀ�У�Ҳ�ɼ���HttpModule����HttpModule�Զ����ñ�����<br />
		/// HttpModule��web.config�ڵ����ø�ʽ���£�
		///		<code>
		///			......
		///			&lt;system.web&gt;
		///				&lt;httpModules&gt;
		///					&lt;add name="ExceptionHttpModule" type="HTB.DevFx.ExceptionManagement.Web.ExceptionHttpModule, HTB.DevFx" /&gt;
		///				&lt;/httpModules&gt;
		///				......
		///				......
		///			&lt;/system.web&gt;
		///			......
		///		</code>
		/// </remarks>
		public static void Init() {
			Init(false);
		}

		/// <summary>
		/// ��ȡ������ܵ����ù�����
		/// </summary>
		/// <remarks>
		/// ������Ӧ�ó����в�Ҫֱ��ʹ�ô˹�������Ҫ���Ӧ�ó����Լ���������Ϣ�����ڿ�������ļ���������µĽ�
		///		<code>
		///			&lt;framework&gt;
		///				&lt;modules&gt;
		///					......
		///					&lt;!--name��ʾӦ�ó���ģ������ƣ�configName��ʾ���ý�����--&gt;
		///					&lt;module name="app" type="HTB.DevFx.Core.AppModule" linkNode="../../../app" /&gt;
		///				&lt;/modules&gt;
		///			&lt;/framework&gt;
		///			......
		///			......
		///			&lt;!--����Ϳ�������Ӧ�ó����Լ���������Ϣ--&gt;
		///			&lt;app&gt;
		///			&lt;/app&gt;
		///		</code>
		///	�ڴ�����Ҫ���&lt;app&gt;&lt;/app&gt;���������Ϣ����ʹ�����´��룺
		///		<code>
		///			......
		///			Framework.GetModule("app").Setting;
		///			......
		///		</code>
		///	���������Ϣ����ο� <see cref="HTB.DevFx.Core.AppModule{T}"/>
		/// </remarks>
		public static IConfigManager Configer {
			get {
				Init();
				return Config.Configer.Current;
			}
		}

		/// <summary>
		/// ��־������
		/// </summary>
		/// <remarks>
		/// ������ֱ��ʹ�� <see cref="HTB.DevFx.Log.Loggor"/> ��д��־<br />
		/// �ڵ��ô˷���ǰ�����ڿ�������ļ������úã�������μ���־������˵��
		/// </remarks>
		public static ILogManager Loggor {
			get {
				Init();
				return Log.Loggor.Current;
			}
		}

		/// <summary>
		/// �쳣������
		/// </summary>
		/// <remarks>
		/// ������ֱ��ʹ�� <see cref="HTB.DevFx.ExceptionManagement.Exceptor"/> ������<br />
		/// �ڵ��ô˷���ǰ�����ڿ�������ļ������úã�������μ��쳣���������˵��
		/// </remarks>
		public static IExceptionManager Exceptor {
			get {
				Init();
				return ExceptionManagement.Exceptor.Instance;
			}
		}

		/// <summary>
		/// ���������
		/// </summary>
		/// <remarks>
		/// ������ֱ��ʹ�� <see cref="HTB.DevFx.Cache.Cacher"/> ������<br />
		/// �ڵ��ô˷���ǰ�����ڿ�������ļ������úã�������μ����������˵��
		/// </remarks>
		public static ICacheManager Cacher {
			get {
				Init();
				return Cache.Cacher.Current;
			}
		}

		/// <summary>
		/// ��ȡָ����ģ��
		/// </summary>
		/// <param name="moduleName">ģ����</param>
		/// <returns>IModule</returns>
		public static IModule GetModule(string moduleName) {
			Init();
			return ((IFramework)framework).GetModule(moduleName);
		}

		/// <summary>
		/// ����ģ��
		/// </summary>
		public static IModule[] Modules {
			get {
				Init();
				return ((IFramework)framework).Modules;
			}
		}
	}
}