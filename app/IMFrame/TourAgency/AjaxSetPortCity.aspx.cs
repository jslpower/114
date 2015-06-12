using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace TourUnion.WEB.IM.TourAgency
{
    /// <summary>
    /// ajax调用设置出港城市
    /// </summary>
    public partial class AjaxSetPortCity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int SiteId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["SiteId"]);

                int CityId = WEB.ProceFlow.ValidatorValueManage.GetIntValue(Request.QueryString["CityId"]);

                Response.Write(WEB.ProceFlow.PortCityManage.UpdatePortCity(CityId, SiteId));
            }
        }
    }
}
