using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景区介绍
    /// </summary>
    /// 周文超 2010-12-9
    public partial class ScenicIntro : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.CTAB = SeniorOnlineShop.master.SPOTT1TAB.首页;

            if (!IsPostBack)
            {
                if (this.Master.CompanyEShopInfo != null)
                {
                    ltrIntro.Text = this.Master.CompanyEShopInfo.CompanyInfo;
                }
            }
        }
    }
}
