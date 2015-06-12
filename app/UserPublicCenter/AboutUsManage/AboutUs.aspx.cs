using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 关于我们
    /// 功能：显示关于我们信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04  
    public partial class AboutUs : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AboutUsHeadControl1.SetImgType = "1";
                this.AboutUsLeftControl1.SetIndex = "1";
            }
        }
    }
}
