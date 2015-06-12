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
using System.Collections.Generic;

namespace UserBackCenter.EShop
{
    public partial class AjaxFriendLinkManage :System.Web.UI.Page
    {
        protected string StrIndex = "1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = HttpContext.Current.Request.QueryString["CompanyId"];
                if (!string.IsNullOrEmpty(id))
                {
                    IList<EyouSoft.Model.ShopStructure.HighShopFriendLink> list = EyouSoft.BLL.ShopStructure.HighShopFriendLink.CreateInstance().GetList(id);
                    if (list.Count > 0 && list != null)
                    {
                        StrIndex = (list.Count + 1).ToString();
                        this.rpt_FriendLinkManage.DataSource = list;
                        this.rpt_FriendLinkManage.DataBind();
                        list = null;
                    }                
                }
            }
        }
    }
}
