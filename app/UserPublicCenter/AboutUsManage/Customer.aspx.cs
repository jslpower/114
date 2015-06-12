using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 客服中心
    /// 功能：显示客服中心信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04 
    public partial class Customer : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AboutUsHeadControl1.SetImgType = "2";
                this.AboutUsLeftControl1.SetIndex = "2";
            }
        }
    }
}
