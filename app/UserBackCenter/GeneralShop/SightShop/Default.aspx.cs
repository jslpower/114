using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.GeneralShop.SightShop
{
    /// <summary>
    /// 用户后台景区普通网店首页
    /// </summary>
    /// 周文超 2011-11-16
    public partial class Default : EyouSoft.Common.Control.BackPage
    {
        protected string ShopPath = "";
        protected string AppliPath = "return SystemIndex.tabChange('ApplicationEShop.aspx')";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                {
                    Response.Clear();
                    Response.Write("<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">您不是专线商，没有该功能！</div>");
                    Response.End();
                }

                bool isOpenEShop = false;
                isOpenEShop = Utils.IsOpenHighShop(SiteUserInfo.CompanyID);
                if (isOpenEShop)
                {
                    AppliPath = "javascript:alert('您已经开通了高级网店！');";
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "$(\"#ApplShop\").css(\"visibility\",\"hidden\"");
                }
            }
            ShopPath = Utils.GetShopUrl(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.CompanyType.景区, -1);
        }
    }
}
