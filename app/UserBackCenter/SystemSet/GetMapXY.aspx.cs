using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 创建人：徐从栎  2012-01-12
    /// 说明：标注地图返回经纬度
    /// </summary>
    public partial class GetMapXY : EyouSoft.Common.Control.BasePage
    {
        protected decimal x = 0m;//经度  
        protected decimal y = 0m;//纬度
        protected string callBackFunction = string.Empty;//回调函数名
        protected string mapKey = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.callBackFunction = Utils.GetQueryStringValue("callBackFunction");
                decimal.TryParse(Convert.ToString(Request.QueryString["x"]), out x);
                decimal.TryParse(Convert.ToString(Request.QueryString["y"]), out y);
                if (x == 0 && y == 0)
                {   //默认为北京
                    x = 116.407413m;
                    y = 39.904214m;
                }
                this.mapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
            }
        }
    }
}