using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票历史订单查询
    /// 袁惠
    /// 2010-12-29
    /// </summary>
    public partial class HistoryOrder : EyouSoft.Common.Control.FrontPage
    {
        protected string NowDate = DateTime.Now.ToString("yyyy年MM月dd日");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Naviagtion = AirTicketNavigation.机票订单管理;
                this.Title = "历史订单_机票";
            }
        }
    }
}
