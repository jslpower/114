using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 招聘信息
    /// 功能：显示招聘信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04 
    public partial class Recruit : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AboutUsHeadControl1.SetImgType = "5";
                this.AboutUsLeftControl1.SetIndex = "5";
            }
        }
    }
}
