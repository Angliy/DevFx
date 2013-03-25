using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using HTB.DevFx.Utils;

namespace HTB.DevFx.Web.UI
{
	internal abstract class PagerControlBase : UserControl
	{
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
		}

		protected bool initialized = false;
		protected int pageIndex = 0;
		protected int pageSize = 10;
		protected int itemCount = 0;
		protected int pageCount = 0;
		protected int startIndex = 0;
		protected int itemLength = 0;
		protected string pagedControlId = null;
		protected Control repeater;
		protected object dataSource = null;

		protected virtual void InitializeComponent() {
			if (this.initialized) {
				return;
			}
			this.initialized = true;

			if (this.pagedControlId != null) {
				this.repeater = WebHelper.FindControl(this, this.pagedControlId);
			}
		}

		///// <summary>
		///// 分页触发事件
		///// </summary>
		//public event PagerPageEventHandler PageIndexChanged;
	}
}
