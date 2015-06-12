using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.ScenicManage
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
            GoogleMapKey = Utils.GetGoogleMapKeyByXml();
            if (Utils.GetQueryStringValue("weidu") != "")
                Latitude = Convert.ToDouble(Utils.GetQueryStringValue("weidu"));
            if (Utils.GetQueryStringValue("jindu") != "")
                Longitude = Convert.ToDouble(Utils.GetQueryStringValue("jindu"));
        }
    }
}