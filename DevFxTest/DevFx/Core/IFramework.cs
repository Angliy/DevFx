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

namespace HTB.DevFx.Core
{
	/// <summary>
	/// 框架接口
	/// </summary>
	/// <remarks>
	/// 为各模块起到桥梁协调作用
	/// </remarks>
	public interface IFramework
	{
		/// <summary>
		/// 获取指定的模块
		/// </summary>
		/// <param name="moduleName">模块名</param>
		/// <returns>IModule</returns>
		IModule GetModule(string moduleName);

		/// <summary>
		/// 所有模块
		/// </summary>
		IModule[] Modules { get; }
	}
}
