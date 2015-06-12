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

namespace UserPublicCenter.ScenicManage
{
    public partial class ScenicMap : EyouSoft.Common.Control.BasePage
    {
        protected double Longitude = 116.389503;
        protected double Latitude = 39.918953;
        protected string GoogleMapKey = string.Empty;
        protected int ZoomLevel = 0;
        protected string scenicname = Utils.GetQueryStringValue("ScenicName");
        protected string X = Utils.GetQueryStringValue("X");
        protected string Y = Utils.GetQueryStringValue("Y");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitCompanyPositionInfo();
            }
        }

        /// <summary>
        /// 初始化公司地理信息
        /// </summary>
        private void InitCompanyPositionInfo()
        {
            if (!string.IsNullOrEmpty(X) && !string.IsNullOrEmpty(Y))
            {
                Latitude = Convert.ToDouble(X);
                Longitude = Convert.ToDouble(Y);
            }
        }
    }
}