using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 设置公司经纬度信息
    /// </summary>
    /// 方琪 2011-11-24
    public partial class SetGoogleMap : EyouSoft.Common.Control.BasePage
    {
        protected double Longitude = 116.389503;
        protected double Latitude = 39.918953;
        protected string GoogleMapKey = string.Empty;
        protected int ZoomLevel = 0;
        protected string SceniceName = string.Empty;
        protected string SceniceID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            string jingdu = Utils.GetQueryStringValue("jingdu");
            string weidu = Utils.GetQueryStringValue("weidu");
            SceniceName = Utils.GetQueryStringValue("SceniceName");
            SceniceID = Utils.GetQueryStringValue("SceniceID");
            if (!IsPostBack)
            {
                InitCompanyPositionInfo(jingdu, weidu);
            }
        }

        /// <summary>
        /// 初始化公司地理信息
        /// </summary>
        private void InitCompanyPositionInfo(string jingdu, string weidu)
        {
            GoogleMapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY") ;
            if (jingdu != "" && weidu != "")
            {

                Longitude = Convert.ToDouble(jingdu);
                Latitude = Convert.ToDouble(weidu);

            }
        }


    }
}
