using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Security.Membership;

namespace UserBackCenter.SupplyManage
{
    /// <summary>
    /// 供应商后台:首页
    /// 罗伏先   2010-07-22
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.BasePage
    {
        private string CompanyID = string.Empty;
        protected string LogoImagePath = string.Empty;
        protected bool IsHighShopSupply = false;
        protected string WebSiteUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsLogin)
            {
                CompanyID = this.SiteUserInfo.CompanyID;
                if (!this.IsCompanyCheck) {
                    tbIsCheck.Visible = true;
                }
                //if (!this.IsSupplyUser)
                //{
                //    Utils.ShowError("对不起，你的公司身份不是属于供应商，没有权限进入供应商网店管理！", "SupplyError");
                //    return;
                //}
                string companyLogoPath = string.Empty;
                companyLogoPath = EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(CompanyID).CompanyLogo.ImagePath;
                if (string.IsNullOrEmpty(companyLogoPath))
                {
                    LogoImagePath = Domain.ServerComponents + "/images/logo.gif";
                }
                else {
                    LogoImagePath = Domain.FileSystem + companyLogoPath;   
                }                
                if (!IsPostBack)
                {
                    InitPage();
                }
            }
            else
            {
                UserProvider.RedirectLogin(Domain.UserPublicCenter + "/Default.aspx", "对不起，你还未登录或登录过期，请登录！");
                return;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            EyouSoft.Model.CompanyStructure.SupplierInfo model = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyID);
            if (model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                IsHighShopSupply = true;
                WebSiteUrl = Domain.SeniorOnlineShop + "/scenicspots/T1/Default.aspx?cid=" + CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
            }
            else
            {

                switch (this.SiteUserInfo.CompanyRole.RoleItems[0])
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        WebSiteUrl = EyouSoft.Common.URLREWRITE.ScenicSpot.ScenicDetailsUrl(CompanyID, this.SiteUserInfo.CityId);
                        //UserPublicCenter + "/ScenicManage/ScenicDetails.aspx?cid=" + this.SiteUserInfo.CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        WebSiteUrl = Domain.UserPublicCenter + "/HotelManage/HotelDetails.aspx?cid=" + this.SiteUserInfo.CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        WebSiteUrl = Domain.UserPublicCenter + "/ShoppingInfo/GeneralShoppingShop.aspx?cid=" + this.SiteUserInfo.CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        WebSiteUrl = EyouSoft.Common.URLREWRITE.TravelProtects.TravelDetailsUrl(this.SiteUserInfo.CityId, CompanyID);
                        //Domain.UserPublicCenter + "/TravelManage/TravelDetails.aspx?cid=" + this.SiteUserInfo.CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
                        break;
                    default:
                        WebSiteUrl = EyouSoft.Common.URLREWRITE.Car.CardGeneralCarShopUrl(this.SiteUserInfo.CityId, CompanyID);
                        //Domain.UserPublicCenter + "/CarInfo/GeneralCarShop.aspx?cid=" + this.SiteUserInfo.CompanyID + "&CityId=" + this.SiteUserInfo.CityId;
                        break;
                }                
            }
            model = null;

            #region 公告区
            this.lblLoginUser.Text = this.SiteUserInfo.UserName;
            ltrCompanyName.Text = this.SiteUserInfo.CompanyName;
            EyouSoft.IBLL.SystemStructure.ISummaryCount SummaryBll = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance();
            EyouSoft.Model.SystemStructure.SummaryCount SummaryModel = SummaryBll.GetSummary();
            if (SummaryModel != null)
            {
                this.lblRouteAgencyCount.Text = (SummaryModel.TravelAgency + SummaryModel.TravelAgencyVirtual).ToString();
                this.lblHotelCount.Text = (SummaryModel.Hotel + SummaryModel.HotelVirtual).ToString();
                this.lblSightCount.Text = (SummaryModel.Sight + SummaryModel.SightVirtual).ToString();
                this.lblCarCount.Text = (SummaryModel.Car + SummaryModel.CarVirtual).ToString();
                this.lblShoppingCount.Text = (SummaryModel.Shop + SummaryModel.ShopVirtual).ToString();
            }
            SummaryModel = null;
            SummaryBll = null;
            #endregion
            #region 初始化运营广告

            IList<EyouSoft.Model.SystemStructure.Affiche> list = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(10, EyouSoft.Model.SystemStructure.AfficheType.旅行社后台广告);
            if (list != null && list.Count > 0)
            {
                this.dlAdv.DataSource = list;
                this.dlAdv.DataBind();
            }
            list = null;

            #endregion
        }
    }
}
