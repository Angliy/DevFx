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
using System.Runtime.InteropServices;
using HTB.DevFx.ExceptionManagement;

namespace HTB.DevFx.Core
{
	#region AOPResult class

	/// <summary>
	/// 对象处理返回的结果
	/// </summary>
	[Serializable]
	[Guid("CF646C8C-1A90-45BD-990D-08BD6A9DAB8C")]
	public class AOPResult : IAOPResult
	{
		#region constructor

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		/// <param name="resultAttachObject">相应的附加信息</param>
		/// <param name="innerAOPResult">内部AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, object resultAttachObject, IAOPResult innerAOPResult) {
			this.resultNo = resultNo;
			this.resultDescription = resultDescription;
			this.resultAttachObject = resultAttachObject;
			this.innerAOPResult = innerAOPResult;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		public AOPResult(int resultNo)
			: this(resultNo, null, null, null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		public AOPResult(int resultNo, string resultDescription)
			: this(resultNo, resultDescription, null, null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		/// <param name="innerAOPResult">内部AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, IAOPResult innerAOPResult)
			: this(resultNo, resultDescription, null, innerAOPResult) {
		}

		#endregion

		#region protected fields

		/// <summary>
		/// 返回代码
		/// </summary>
		protected int resultNo;
		/// <summary>
		/// 对应的描述信息
		/// </summary>
		protected string resultDescription;
		/// <summary>
		/// 相应的附加信息
		/// </summary>
		protected object resultAttachObject;
		/// <summary>
		/// 内部AOPResult
		/// </summary>
		protected IAOPResult innerAOPResult;

		#endregion protected fields

		#region public static methods

		/// <summary>
		/// 隐式转换成Boolean类型，以期简化判断
		/// </summary>
		/// <param name="result">AOPResult</param>
		/// <returns>true/false</returns>
		public static implicit operator bool(AOPResult result) {
			return result.IsSuccess;
		}

		/// <summary>
		/// 把<c>IAOPResult&lt;Q&gt;</c>转换成<c>IAOPResult&lt;P&gt;</c>，其中<typeparamref name="Q"/>从<typeparamref name="P"/>派生
		/// </summary>
		/// <typeparam name="Q">附加的泛型</typeparam>
		/// <typeparam name="P">附加的泛型</typeparam>
		/// <param name="result">待转换的<c>IAOPResult&lt;T&gt;</c></param>
		/// <returns>转换后的<c>IAOPResult&lt;T&gt;</c></returns>
		public static IAOPResult<P> Convert<Q, P>(IAOPResult<Q> result) where Q : P {
			return new AOPResult<P>(result.ResultNo, result.ResultDescription, result.ResultAttachObjectEx, result);
		}

		/// <summary>
		/// 把<c>IAOPResult&lt;Q&gt;</c>转换成<c>IAOPResult&lt;P&gt;</c>
		/// </summary>
		/// <typeparam name="Q">附加的泛型</typeparam>
		/// <typeparam name="P">附加的泛型</typeparam>
		/// <param name="result">待转换的<c>IAOPResult&lt;T&gt;</c></param>
		/// <returns>转换后的<c>IAOPResult&lt;T&gt;</c></returns>
		public static IAOPResult<P> ConvertTo<Q, P>(IAOPResult<Q> result) {
			return new AOPResult<P>(result.ResultNo, result.ResultDescription, default(P), result);
		}

		#endregion public static methods

		#region public properties

		/// <summary>
		/// 返回代码
		/// </summary>
		public virtual int ResultNo {
			get { return this.resultNo; }
			set { this.resultNo = value; }
		}

		/// <summary>
		/// 对应的描述信息
		/// </summary>
		public virtual string ResultDescription {
			get { return this.resultDescription; }
			set { this.resultDescription = value; }
		}

		/// <summary>
		/// 相应的附加信息
		/// </summary>
		public virtual object ResultAttachObject {
			get { return this.resultAttachObject; }
			set { this.resultAttachObject = value; }
		}

		/// <summary>
		/// 内部AOPResult
		/// </summary>
		public virtual IAOPResult InnerAOPResult {
			get { return this.innerAOPResult; }
			set { this.innerAOPResult = value; }
		}

		/// <summary>
		/// 处理结果是否成功（ResultNo == 0）
		/// </summary>
		public virtual bool IsSuccess {
			get { return this.resultNo == 0; }
		}

		/// <summary>
		/// 处理结果是否失败（ResultNo != 0 ）
		/// </summary>
		public bool IsNotSuccess {
			get { return this.resultNo != 0; }
		}

		/// <summary>
		/// 处理结果是否失败（ResultNo &lt; 0 ）
		/// </summary>
		public virtual bool IsFailed {
			get { return this.resultNo < 0; }
		}

		/// <summary>
		/// 已处理，但有不致命的错误（ResultNo &gt; 0）
		/// </summary>
		public virtual bool IsPassedButFailed {
			get { return this.resultNo > 0; }
		}

		#endregion public properties

		#region public methods

		/// <summary>
		/// 如果处理失败，则抛出异常 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>返回本身</returns>
		public virtual IAOPResult ThrowErrorOnFailed() {
			if (this.IsFailed) {
				throw new BaseException(this.resultNo, this.resultDescription);
			}
			return this;
		}

		#endregion public methods

		#region IAOPResult Members

		/// <summary>
		/// 返回代码
		/// </summary>
		int IAOPResult.ResultNo {
			get { return this.resultNo; }
		}

		/// <summary>
		/// 对应的描述信息
		/// </summary>
		string IAOPResult.ResultDescription {
			get { return this.resultDescription; }
		}

		/// <summary>
		/// 相应的附加信息
		/// </summary>
		object IAOPResult.ResultAttachObject {
			get { return this.resultAttachObject; }
		}

		/// <summary>
		/// 内部AOPResult
		/// </summary>
		IAOPResult IAOPResult.InnerAOPResult {
			get { return this.innerAOPResult; }
		}

		/// <summary>
		/// 处理结果是否成功（ResultNo == 0）
		/// </summary>
		bool IAOPResult.IsSuccess {
			get { return this.IsSuccess; }
		}

		/// <summary>
		/// 处理结果是否失败（ResultNo &lt; 0 ）
		/// </summary>
		bool IAOPResult.IsFailed {
			get { return this.IsFailed; }
		}

		/// <summary>
		/// 已处理，但有不致命的错误（ResultNo &gt; 0）
		/// </summary>
		bool IAOPResult.IsPassedButFailed {
			get { return this.IsPassedButFailed; }
		}

		/// <summary>
		/// 如果处理失败，则抛出异常 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>返回本身</returns>
		IAOPResult IAOPResult.ThrowErrorOnFailed() {
			return this.ThrowErrorOnFailed();
		}

		#endregion
	}

	#endregion AOPResult class

	#region AOPResult<T> class

	/// <summary>
	/// 对象处理返回的结果（泛型）
	/// </summary>
	/// <typeparam name="T">附加对象类型</typeparam>
	[Serializable]
	[Guid("06CFDA21-11FB-4434-A6B7-13C0EE9FE2FF")]
	public class AOPResult<T> : AOPResult, IAOPResult<T>
	{
		#region constructor

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		/// <param name="resultAttachObject">相应的附加信息</param>
		/// <param name="innerAOPResult">内部AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, T resultAttachObject, IAOPResult innerAOPResult)
			: base(resultNo, resultDescription, resultAttachObject, innerAOPResult) {
			this.resultAttachObjectEx = resultAttachObject;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		public AOPResult(int resultNo)
			: this(resultNo, null, default(T), null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		public AOPResult(int resultNo, string resultDescription)
			: this(resultNo, resultDescription, default(T), null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		/// <param name="resultAttachObject">相应的附加信息</param>
		public AOPResult(int resultNo, string resultDescription, T resultAttachObject)
			: this(resultNo, resultDescription, resultAttachObject, null) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="resultNo">返回代码</param>
		/// <param name="resultDescription">对应的描述信息</param>
		/// <param name="innerAOPResult">内部AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, IAOPResult innerAOPResult)
			: this(resultNo, resultDescription, default(T), innerAOPResult) {
		}

		#endregion constructor

		#region properties

		/// <summary>
		/// 附加信息（泛型）
		/// </summary>
		protected T resultAttachObjectEx;

		/// <summary>
		/// 附加信息（泛型）
		/// </summary>
		public virtual T ResultAttachObjectEx {
			get { return this.resultAttachObjectEx; }
			set { this.resultAttachObjectEx = value; }
		}

		#endregion properties

		#region static members

		/// <summary>
		/// 返回执行成功结果（附加对象）
		/// </summary>
		/// <param name="resultAttachObject">附加对象</param>
		/// <returns>AOPResult（泛型）</returns>
		public static AOPResult<T> Success(T resultAttachObject) {
			return new AOPResult<T>(0, string.Empty, resultAttachObject);
		}

		#endregion static members

		#region IAOPResult<T> Members

		/// <summary>
		/// 泛型附加对象
		/// </summary>
		T IAOPResult<T>.ResultAttachObjectEx {
			get { return this.ResultAttachObjectEx; }
		}

		#endregion
	}

	#endregion AOPResult<T> class
}
