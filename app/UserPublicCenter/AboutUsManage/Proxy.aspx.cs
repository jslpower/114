using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AboutUsManage
{
    /// <summary>
    /// 代理合作
    /// 功能：显示代理合作信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-08-04 
    public partial class Proxy : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AboutUsHeadControl1.SetImgType = "4";
                this.AboutUsLeftControl1.SetIndex = "4";
            }
        }
    }
}
