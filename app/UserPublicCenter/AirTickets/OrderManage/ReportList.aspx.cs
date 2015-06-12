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

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 页面功能：机票订单管理——报表页
    /// 开发人：刘咏梅     
    /// 开发时间：2010-10-19
    /// </summary>
    public partial class ReportList : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!Page.IsPostBack)
            {
                this.Master.Naviagtion = AirTicketNavigation.机票订单管理;
                this.Title = "报表_机票";
            }
        }
    }
}
