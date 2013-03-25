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

using System.Runtime.InteropServices;

namespace HTB.DevFx.Core
{
	#region IAOPResult
	
	/// <summary>
	/// 对象处理返回的结果接口
	/// </summary>
	/// <remarks>
	/// 建议在代码调用返回值中都采用此类实例为返回值<br />
	/// 一般ResultNo小于0表示异常，0表示成功，大于0表示其它一般提示信息
	/// </remarks>
	[Guid("106009C4-859D-4139-8F1D-9799D941662B")]
	public interface IAOPResult
	{
		/// <summary>
		/// 返回代码
		/// </summary>
		int ResultNo { get; }

		/// <summary>
		/// 对应的描述信息
		/// </summary>
		string ResultDescription { get; }

		/// <summary>
		/// 相应的附加信息
		/// </summary>
		object ResultAttachObject { get; }

		/// <summary>
		/// 内部AOPResult
		/// </summary>
		IAOPResult InnerAOPResult { get; }

		/// <summary>
		/// 处理结果是否成功（ResultNo == 0）
		/// </summary>
		bool IsSuccess { get; }

		/// <summary>
		/// 处理结果是否失败（ResultNo != 0 ）
		/// </summary>
		bool IsNotSuccess { get; }

		/// <summary>
		/// 处理结果是否失败（ResultNo &lt; 0 ）
		/// </summary>
		bool IsFailed { get; }

		/// <summary>
		/// 已处理，但有不致命的错误（ResultNo &gt; 0）
		/// </summary>
		bool IsPassedButFailed { get; }

		/// <summary>
		/// 如果处理失败，则抛出异常 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>返回本身</returns>
		IAOPResult ThrowErrorOnFailed();
	}

	#endregion IAOPResult

	#region IAOPResult<T>

	/// <summary>
	/// 对象处理返回的结果接口（泛型）
	/// </summary>
	[Guid("27F931B0-44E2-48E9-BC3F-ED707E68525C")]
	public interface IAOPResult<T> : IAOPResult
	{
		/// <summary>
		/// 泛型附加对象
		/// </summary>
		T ResultAttachObjectEx { get; }
	}
 
	#endregion
}
