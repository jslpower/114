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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Collections.Generic;
using System.IO;

namespace UserBackCenter.EShop
{
    public partial class SetResourcesList :EyouSoft.Common.Control.BasePage
    {
        protected int intPageSize = 15;
        protected int CurrencyPage = 1;
        
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }

            if (!Page.IsPostBack)
            {
                int intRecordCount = 0; //总记录数
                CurrencyPage = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
                IList<EyouSoft.Model.ShopStructure.HighShopResource> guid_List = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetList(intPageSize, CurrencyPage, ref intRecordCount, this.SiteUserInfo.CompanyID, "");
                //如果数据存在则邦定
                if (guid_List != null && guid_List.Count > 0)
                {
                    rptResources.DataSource = guid_List;
                    rptResources.DataBind();
                    this.ExportPageInfo1.intPageSize = intPageSize;
                    this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                    this.ExportPageInfo1.intRecordCount = intRecordCount;
                    this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                }
                else
                {
                    pnlNoData.Visible = true;
                    ExportPageInfo1.Visible = false;
                }
                guid_List = null;
            }
        }
    }
}
