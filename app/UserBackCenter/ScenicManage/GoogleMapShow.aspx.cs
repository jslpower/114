using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace UserBackCenter.ScenicManage
{
    /// <summary>
    /// 根据经纬度返回google Map
    /// 创建时间：2012-01-05
    /// 创建人：徐从栎
    /// </summary>
    public partial class GoogleMapShow : EyouSoft.Common.Control.BasePage
    {
        protected decimal x=0m;//经度
        protected decimal y = 0m;//纬度
        protected string mapKey = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                decimal.TryParse(Convert.ToString(Request.QueryString["x"]), out x);
                decimal.TryParse(Convert.ToString(Request.QueryString["y"]), out y);
                if (x == 0 && y == 0)
                {
                    Response.Clear();
                    Response.Write("<div style='width:100%;text-align:center;color:#f00;'>未正确设置地理坐标，地图加载失败！</div>");
                    Response.End();
                }
                this.mapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
            }
        }
    }
}