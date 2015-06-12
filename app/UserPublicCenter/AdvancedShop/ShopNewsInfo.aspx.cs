using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.AdvancedShop
{
    /// <summary>
    /// 高级网店资讯信息
    /// 功能：显示资讯的详细信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    public partial class ShopNewsInfo : EyouSoft.Common.Control.FrontPage
    {
        public string MQ = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string newsId = EyouSoft.Common.Utils.GetString(Request.QueryString["NewsId"],"1");
                NewsDetailsInit(newsId); 
            }
            
        }

        #region 获得新闻详细信息
        protected void NewsDetailsInit(string newsId)
        { 
            //获得News实体
            EyouSoft.Model.CompanyStructure.CompanyAffiche NModel = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().GetModel(EyouSoft.Common.Utils.GetInt(newsId));
            string CompanyId ="0";
            if (NModel != null)
            {
                this.Page.Title = NModel.AfficheTitle;
                CompanyId = NModel.CompanyId;
                this.AdvancedControl11.SetAgencyId = CompanyId;
                this.AdvancedControl11.CityId = this.CityId;
                this.lblTitle.Text = NModel.AfficheTitle;
                this.lblContent.Text = NModel.AfficheInfo;
            }

            //获得网店信息
            EyouSoft.Model.CompanyStructure.SupplierInfo SModel = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyId);
            if (SModel != null)
            {
                string backurl = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店) == true ? EyouSoft.Common.URLREWRITE.TravelProtects.TravelAdvancedShopUrl(this.CityId, SModel.ID) : EyouSoft.Common.URLREWRITE.Car.CarAdvancedShopUrl(this.CityId, SModel.ID);
                this.litGoBack.Text = "<a href=\"" + backurl+ "\" style=\"cursor:pointer; font-size:12px;\">返回</a>";
                this.AdvancedControl11.SetAgencyId = SModel.ID;
                this.ImgHead.ImageUrl = Domain.FileSystem + SModel.CompanyImg;
                this.lblUserName.Text = SModel.ContactInfo.ContactName;
                this.lblTelPhone.Text = SModel.ContactInfo.Tel;
                this.lblFax.Text = SModel.ContactInfo.Fax;
                this.lblUrl.Text = "<a href=\"" + SModel.WebSite + "\" target=\"_blank\" >" + SModel.WebSite.Replace("http://", "") + "</a>";
                MQ = SModel.ContactInfo.MQ;
                this.lblAddress.Text = SModel.CompanyAddress;
                this.Page.Title += "_"+SModel.CompanyName+"_";
            }

            if (SModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
            {
                this.CityAndMenu1.HeadMenuIndex = 4;
                this.Page.Title += "景区";
            }
            if (SModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
            {
                this.CityAndMenu1.HeadMenuIndex = 5;
                this.Page.Title += "酒店";
            }
            if (SModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队))
            {
                this.CityAndMenu1.HeadMenuIndex = 6;
                this.Page.Title += "车队";
            }
            if (SModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
            {
                this.CityAndMenu1.HeadMenuIndex = 7;
                this.Page.Title += "旅游用品店";
            }
            if (SModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店))
            {
                this.CityAndMenu1.HeadMenuIndex = 8;
                this.Page.Title += "购物店";
            }
        }
        #endregion
    }

}
