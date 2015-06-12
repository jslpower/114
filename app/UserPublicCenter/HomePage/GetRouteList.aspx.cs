using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.HomePage
{
    /// <summary>
    /// 散拼中心（线路区域）
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class GetRouteList : System.Web.UI.Page
    {
        protected string ImageServerPath = ImageManage.GetImagerServerUrl(1);
        protected string strRouteList1 = "";
        protected string strRouteList2 = "";
        protected string strRouteList3 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.BindRouteList();
            }
        }

        protected void BindRouteList()
        {

            for (int i = 0; i < 20; i++)
            {
                strRouteList1 += "<a href=\"/TourManage/TourList.aspx\">北京专线</a>";
                strRouteList2 += "<a href=\"#\">云南专线</a>";
                strRouteList3 += "<a href=\"#\">青岛大连线</a>";
            }
        }
    }
}
