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
	/// �������صĽ��
	/// </summary>
	[Serializable]
	[Guid("CF646C8C-1A90-45BD-990D-08BD6A9DAB8C")]
	public class AOPResult : IAOPResult
	{
		#region constructor

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		/// <param name="resultAttachObject">��Ӧ�ĸ�����Ϣ</param>
		/// <param name="innerAOPResult">�ڲ�AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, object resultAttachObject, IAOPResult innerAOPResult) {
			this.resultNo = resultNo;
			this.resultDescription = resultDescription;
			this.resultAttachObject = resultAttachObject;
			this.innerAOPResult = innerAOPResult;
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		public AOPResult(int resultNo)
			: this(resultNo, null, null, null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		public AOPResult(int resultNo, string resultDescription)
			: this(resultNo, resultDescription, null, null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		/// <param name="innerAOPResult">�ڲ�AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, IAOPResult innerAOPResult)
			: this(resultNo, resultDescription, null, innerAOPResult) {
		}

		#endregion

		#region protected fields

		/// <summary>
		/// ���ش���
		/// </summary>
		protected int resultNo;
		/// <summary>
		/// ��Ӧ��������Ϣ
		/// </summary>
		protected string resultDescription;
		/// <summary>
		/// ��Ӧ�ĸ�����Ϣ
		/// </summary>
		protected object resultAttachObject;
		/// <summary>
		/// �ڲ�AOPResult
		/// </summary>
		protected IAOPResult innerAOPResult;

		#endregion protected fields

		#region public static methods

		/// <summary>
		/// ��ʽת����Boolean���ͣ����ڼ��ж�
		/// </summary>
		/// <param name="result">AOPResult</param>
		/// <returns>true/false</returns>
		public static implicit operator bool(AOPResult result) {
			return result.IsSuccess;
		}

		/// <summary>
		/// ��<c>IAOPResult&lt;Q&gt;</c>ת����<c>IAOPResult&lt;P&gt;</c>������<typeparamref name="Q"/>��<typeparamref name="P"/>����
		/// </summary>
		/// <typeparam name="Q">���ӵķ���</typeparam>
		/// <typeparam name="P">���ӵķ���</typeparam>
		/// <param name="result">��ת����<c>IAOPResult&lt;T&gt;</c></param>
		/// <returns>ת�����<c>IAOPResult&lt;T&gt;</c></returns>
		public static IAOPResult<P> Convert<Q, P>(IAOPResult<Q> result) where Q : P {
			return new AOPResult<P>(result.ResultNo, result.ResultDescription, result.ResultAttachObjectEx, result);
		}

		/// <summary>
		/// ��<c>IAOPResult&lt;Q&gt;</c>ת����<c>IAOPResult&lt;P&gt;</c>
		/// </summary>
		/// <typeparam name="Q">���ӵķ���</typeparam>
		/// <typeparam name="P">���ӵķ���</typeparam>
		/// <param name="result">��ת����<c>IAOPResult&lt;T&gt;</c></param>
		/// <returns>ת�����<c>IAOPResult&lt;T&gt;</c></returns>
		public static IAOPResult<P> ConvertTo<Q, P>(IAOPResult<Q> result) {
			return new AOPResult<P>(result.ResultNo, result.ResultDescription, default(P), result);
		}

		#endregion public static methods

		#region public properties

		/// <summary>
		/// ���ش���
		/// </summary>
		public virtual int ResultNo {
			get { return this.resultNo; }
			set { this.resultNo = value; }
		}

		/// <summary>
		/// ��Ӧ��������Ϣ
		/// </summary>
		public virtual string ResultDescription {
			get { return this.resultDescription; }
			set { this.resultDescription = value; }
		}

		/// <summary>
		/// ��Ӧ�ĸ�����Ϣ
		/// </summary>
		public virtual object ResultAttachObject {
			get { return this.resultAttachObject; }
			set { this.resultAttachObject = value; }
		}

		/// <summary>
		/// �ڲ�AOPResult
		/// </summary>
		public virtual IAOPResult InnerAOPResult {
			get { return this.innerAOPResult; }
			set { this.innerAOPResult = value; }
		}

		/// <summary>
		/// �������Ƿ�ɹ���ResultNo == 0��
		/// </summary>
		public virtual bool IsSuccess {
			get { return this.resultNo == 0; }
		}

		/// <summary>
		/// �������Ƿ�ʧ�ܣ�ResultNo != 0 ��
		/// </summary>
		public bool IsNotSuccess {
			get { return this.resultNo != 0; }
		}

		/// <summary>
		/// �������Ƿ�ʧ�ܣ�ResultNo &lt; 0 ��
		/// </summary>
		public virtual bool IsFailed {
			get { return this.resultNo < 0; }
		}

		/// <summary>
		/// �Ѵ������в������Ĵ���ResultNo &gt; 0��
		/// </summary>
		public virtual bool IsPassedButFailed {
			get { return this.resultNo > 0; }
		}

		#endregion public properties

		#region public methods

		/// <summary>
		/// �������ʧ�ܣ����׳��쳣 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>���ر���</returns>
		public virtual IAOPResult ThrowErrorOnFailed() {
			if (this.IsFailed) {
				throw new BaseException(this.resultNo, this.resultDescription);
			}
			return this;
		}

		#endregion public methods

		#region IAOPResult Members

		/// <summary>
		/// ���ش���
		/// </summary>
		int IAOPResult.ResultNo {
			get { return this.resultNo; }
		}

		/// <summary>
		/// ��Ӧ��������Ϣ
		/// </summary>
		string IAOPResult.ResultDescription {
			get { return this.resultDescription; }
		}

		/// <summary>
		/// ��Ӧ�ĸ�����Ϣ
		/// </summary>
		object IAOPResult.ResultAttachObject {
			get { return this.resultAttachObject; }
		}

		/// <summary>
		/// �ڲ�AOPResult
		/// </summary>
		IAOPResult IAOPResult.InnerAOPResult {
			get { return this.innerAOPResult; }
		}

		/// <summary>
		/// �������Ƿ�ɹ���ResultNo == 0��
		/// </summary>
		bool IAOPResult.IsSuccess {
			get { return this.IsSuccess; }
		}

		/// <summary>
		/// �������Ƿ�ʧ�ܣ�ResultNo &lt; 0 ��
		/// </summary>
		bool IAOPResult.IsFailed {
			get { return this.IsFailed; }
		}

		/// <summary>
		/// �Ѵ������в������Ĵ���ResultNo &gt; 0��
		/// </summary>
		bool IAOPResult.IsPassedButFailed {
			get { return this.IsPassedButFailed; }
		}

		/// <summary>
		/// �������ʧ�ܣ����׳��쳣 <see cref="HTB.DevFx.ExceptionManagement.BaseException"/>
		/// </summary>
		/// <returns>���ر���</returns>
		IAOPResult IAOPResult.ThrowErrorOnFailed() {
			return this.ThrowErrorOnFailed();
		}

		#endregion
	}

	#endregion AOPResult class

	#region AOPResult<T> class

	/// <summary>
	/// �������صĽ�������ͣ�
	/// </summary>
	/// <typeparam name="T">���Ӷ�������</typeparam>
	[Serializable]
	[Guid("06CFDA21-11FB-4434-A6B7-13C0EE9FE2FF")]
	public class AOPResult<T> : AOPResult, IAOPResult<T>
	{
		#region constructor

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		/// <param name="resultAttachObject">��Ӧ�ĸ�����Ϣ</param>
		/// <param name="innerAOPResult">�ڲ�AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, T resultAttachObject, IAOPResult innerAOPResult)
			: base(resultNo, resultDescription, resultAttachObject, innerAOPResult) {
			this.resultAttachObjectEx = resultAttachObject;
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		public AOPResult(int resultNo)
			: this(resultNo, null, default(T), null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		public AOPResult(int resultNo, string resultDescription)
			: this(resultNo, resultDescription, default(T), null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		/// <param name="resultAttachObject">��Ӧ�ĸ�����Ϣ</param>
		public AOPResult(int resultNo, string resultDescription, T resultAttachObject)
			: this(resultNo, resultDescription, resultAttachObject, null) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="resultNo">���ش���</param>
		/// <param name="resultDescription">��Ӧ��������Ϣ</param>
		/// <param name="innerAOPResult">�ڲ�AOPResult</param>
		public AOPResult(int resultNo, string resultDescription, IAOPResult innerAOPResult)
			: this(resultNo, resultDescription, default(T), innerAOPResult) {
		}

		#endregion constructor

		#region properties

		/// <summary>
		/// ������Ϣ�����ͣ�
		/// </summary>
		protected T resultAttachObjectEx;

		/// <summary>
		/// ������Ϣ�����ͣ�
		/// </summary>
		public virtual T ResultAttachObjectEx {
			get { return this.resultAttachObjectEx; }
			set { this.resultAttachObjectEx = value; }
		}

		#endregion properties

		#region static members

		/// <summary>
		/// ����ִ�гɹ���������Ӷ���
		/// </summary>
		/// <param name="resultAttachObject">���Ӷ���</param>
		/// <returns>AOPResult�����ͣ�</returns>
		public static AOPResult<T> Success(T resultAttachObject) {
			return new AOPResult<T>(0, string.Empty, resultAttachObject);
		}

		#endregion static members

		#region IAOPResult<T> Members

		/// <summary>
		/// ���͸��Ӷ���
		/// </summary>
		T IAOPResult<T>.ResultAttachObjectEx {
			get { return this.ResultAttachObjectEx; }
		}

		#endregion
	}

	#endregion AOPResult<T> class
}
