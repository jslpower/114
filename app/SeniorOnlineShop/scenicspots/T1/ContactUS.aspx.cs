using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.scenicspots.T1
{
    /// <summary>
    /// 景区联系我们
    /// </summary>
    /// 周文超 2010-12-09
    public partial class ContactUS : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.CTAB = SeniorOnlineShop.master.SPOTT1TAB.联系我们;
            this.Master.IsEnableLeftGoogleMap = false;
            if (!IsPostBack)
            {
                if (this.Master.CompanyInfo != null)
                {
                    this.ltrContacter.Text = this.Master.CompanyInfo.ContactInfo.ContactName;
                    this.ltrTelephone.Text = this.Master.CompanyInfo.ContactInfo.Tel;
                    this.ltrFax.Text = this.Master.CompanyInfo.ContactInfo.Fax;
                    this.ltrAddress.Text = this.Master.CompanyInfo.CompanyAddress;
                    this.ltrMQ.Text = Utils.GetBigImgMQ(this.Master.CompanyInfo.ContactInfo.MQ);
                    this.ltrQQ.Text = Utils.GetQQ(this.Master.CompanyInfo.ContactInfo.QQ, "在线客服QQ");
                    this.ltrWebSite.Text = this.Master.CompanyInfo.WebSite;

                    GoogleMapControl1.ShowTitleText = this.Master.CompanyInfo.CompanyName;
                }
                if (this.Master.CompanyEShopInfo != null && this.Master.CompanyEShopInfo.PositionInfo != null)
                {
                    GoogleMapControl1.Longitude = this.Master.CompanyEShopInfo.PositionInfo.Longitude;
                    GoogleMapControl1.Latitude = this.Master.CompanyEShopInfo.PositionInfo.Latitude;
                    GoogleMapControl1.ZoomLevel = this.Master.CompanyEShopInfo.PositionInfo.ZoomLevel;
                    GoogleMapControl1.LoadintText = "正在载入...";
                    GoogleMapControl1.GoogleMapKey = string.IsNullOrEmpty(this.Master.CompanyEShopInfo.GoogleMapKey) ? EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY") : this.Master.CompanyEShopInfo.GoogleMapKey;
                }
            }
        }
    }
}
