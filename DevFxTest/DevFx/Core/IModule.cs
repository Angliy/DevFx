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

using HTB.DevFx.Config;

namespace HTB.DevFx.Core
{
	/// <summary>
	/// 框架模块接口
	/// </summary>
	/// <remarks>
	/// 模块如要嵌入到框架中，请按如下格式配置：
	///		<code>
	///			&lt;htb.devfx&gt;
	///				&lt;framework&gt;
	///					&lt;modules&gt;
	///						......
	///						&lt;module name="模块名称" type="实现模块接口的类型" configName="此模块实际的配置节名" /&gt;
	///						......
	///					&lt;/modules&gt;
	///				&lt;/framework&gt;
	///				......
	///			&lt;/htb.devfx&gt;
	///		</code>
	///	IFramework实例将会对配置的模块进行初始化，调用<c>IModule.Init(IFramework, IConfigSetting)</c>
	/// </remarks>
	public interface IModule
	{
		/// <summary>
		/// 模块名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 初始化模块
		/// </summary>
		/// <param name="framework">IFramework</param>
		/// <param name="setting">对应的配置节</param>
		void Init(IFramework framework, IConfigSetting setting);

		/// <summary>
		/// 本模块的配置节
		/// </summary>
		IConfigSetting Setting { get; }

		/// <summary>
		/// 获取本模块的实例（可以是单例模式也可以是多例模式）
		/// </summary>
		/// <returns>IModule</returns>
		IModule GetInstance();
	}
}
