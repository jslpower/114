using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票告知单打印页
    /// </summary>
    public partial class PrintTicketJournal : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.Naviagtion = EyouSoft.Common.AirTicketNavigation.机票订单管理;
                this.Title = "机票告知单打印_机票";
            }

        }

       
    }
}
