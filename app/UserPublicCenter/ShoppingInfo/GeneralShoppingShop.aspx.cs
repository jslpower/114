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
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common.Function;

namespace UserPublicCenter.ShoppingInfo
{
    /// <summary>
    /// 页面功能：购物点——购物点普通网店页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class GeneralShoppingShop : EyouSoft.Common.Control.FrontPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            string CompanyID = "0";
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["cId"])))
            {
                CompanyID = Utils.InputText(Request.QueryString["cId"]);
            }
            //将公司编号传给普通网店通用控件
            this.GeneralShopControl1.SetAgencyId = CompanyID;
            this.CityAndMenu1.HeadMenuIndex = 8;
        }
        #endregion
    }
}
