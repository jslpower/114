using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 酒店搜索列表页
    /// 功能：用来显示搜索获得酒店的的列表
    /// 创建人：戴银柱
    /// 创建时间： 2010-07-26  
    /// </summary>
    public partial class HotelList : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
                //this.CityAndMenu1.HeadMenuIndex = 5;
            }
        }
    }
}
