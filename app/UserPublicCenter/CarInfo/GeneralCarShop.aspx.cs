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

namespace UserPublicCenter.CarInfo
{
    /// <summary>
    /// 页面功能：车队——车队普通网店页
    /// 开发人：杜桂云      开发时间：2010-07-23
    /// </summary>
    public partial class GeneralCarShop : EyouSoft.Common.Control.FrontPage
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            string CompanyID = Utils.InputText(Request.QueryString["cId"]);
            if(string.IsNullOrEmpty(CompanyID))
            {
                CompanyID="0";
            }
            //将公司编号传给普通网店通用控件
            this.GeneralShopControl1.SetAgencyId = CompanyID;
            this.CityAndMenu1.HeadMenuIndex = 6;          
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //设置Title.....
            this.Title = string.Format(EyouSoft.Common.PageTitle.CarDetail_Title, CityModel.CityName, GeneralShopControl1.CompanyName);
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.CarDetail_Des, CityModel.CityName,GeneralShopControl1.CompanyName));
            AddMetaTag("keywords", EyouSoft.Common.PageTitle.CarDetail_Keywords);
        }
        #endregion
    }
}
