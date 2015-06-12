using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Control;
using EyouSoft.Common;

namespace UserBackCenter.GeneralShop
{
    public partial class GeneShopMainPage : EyouSoft.Common.Control.BackPage
    {
        protected string ShopPath = "";
        protected string AppliPath = "return SystemIndex.tabChange('ApplicationEShop.aspx')";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                //{
                //    Response.Clear();
                //    Response.Write("<div style=\"text-align:center;  margin-top:75px; margin-bottom:75px;\">您不是专线商，没有该功能！</a></div>");
                //    Response.End();
                //}

                //组团社没有高级网店
                this.spanApplyShop.Visible = !this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团);

                bool IsOpenEShop = false;
                //EyouSoft.Model.CompanyStructure.CompanyState  state=null;
                //Utils.GetCompanyDomain(this.SiteUserInfo.CompanyID, out state ,out IsOpenEShop, EyouSoft.Model.CompanyStructure.CompanyType .专线);
                IsOpenEShop = Utils.IsOpenHighShop(SiteUserInfo.CompanyID);
                if (IsOpenEShop)
                {
                    AppliPath = "javascript:alert('您已经开通了高级网店！');";
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page,"$(\"#ApplShop\").css(\"visibility\",\"hidden\"");
                }               
            }
            ShopPath = Utils.GetShopUrl(this.SiteUserInfo.CompanyID);            
        }

    }
}
