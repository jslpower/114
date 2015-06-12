using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 设置公司经纬度信息
    /// </summary>
    /// 周文超 2010-12-10
    public partial class SetGoogleMap : EyouSoft.Common.Control.BasePage
    {
        protected double Longitude = 116.389503;
        protected double Latitude = 39.918953;
        protected string GoogleMapKey = string.Empty;
        protected int ZoomLevel = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }

            if (!IsPostBack)
            {
                InitCompanyPositionInfo();

                if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["HandleType"])) && Utils.InputText(Request.QueryString["HandleType"]).ToLower() == "save")
                {
                    bool IsResult = false;
                    SaveCompanyPositionInfo(ref IsResult);
                    if (IsResult)
                    {
                        Response.Clear();
                        Response.Write("1");
                        Response.End();
                    }
                }
            }
        }

        /// <summary>
        /// 初始化公司地理信息
        /// </summary>
        private void InitCompanyPositionInfo()
        {
            EyouSoft.Model.ShopStructure.HighShopCompanyInfo model = EyouSoft.BLL.ShopStructure.HighShopCompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
            if (model != null)
            {
                GoogleMapKey = string.IsNullOrEmpty(model.GoogleMapKey) ? EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY") : model.GoogleMapKey;
                if (model.PositionInfo != null && model.PositionInfo.Longitude > 0 && model.PositionInfo.Latitude > 0)
                {
                    Longitude = model.PositionInfo.Longitude;
                    Latitude = model.PositionInfo.Latitude;
                    ZoomLevel = model.PositionInfo.ZoomLevel;
                }
            }
        }

        /// <summary>
        /// 保存公司经纬度信息
        /// </summary>
        private void SaveCompanyPositionInfo(ref bool IsResult)
        {
            IsResult = false;
            string strLongitude = Utils.InputText(Request.QueryString["Longitude"]);
            string strLatitude = Utils.InputText(Request.QueryString["Latitude"]);
            string strZoomLevel = Utils.InputText(Request.QueryString["ZoomLevel"]);
            if ((EyouSoft.Common.Function.StringValidate.IsDecimal(strLongitude) || EyouSoft.Common.Function.StringValidate.IsDecimalSign(strLongitude)) && (EyouSoft.Common.Function.StringValidate.IsDecimal(strLatitude) || EyouSoft.Common.Function.StringValidate.IsDecimalSign(strLatitude))
                &&EyouSoft.Common.Function.StringValidate.IsInteger(strZoomLevel))
            {
                double Longitude = 0;
                double Latitude = 0;
                double.TryParse(strLongitude, out Longitude);
                double.TryParse(strLatitude, out Latitude);
                if (Longitude == 0 || Latitude == 0)
                    return;

                EyouSoft.Model.ShopStructure.PositionInfo pModel = new EyouSoft.Model.ShopStructure.PositionInfo();
                pModel.Longitude = Longitude;
                pModel.Latitude = Latitude;
                pModel.ZoomLevel = int.Parse(strZoomLevel);
                IsResult = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance().UpdateCompanyPositionInfo(SiteUserInfo.CompanyID, pModel);
                pModel = null;
            }
        }
    }
}
