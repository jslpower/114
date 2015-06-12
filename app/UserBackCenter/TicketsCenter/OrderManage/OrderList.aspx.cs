using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace UserBackCenter.TicketsCenter.OrderManage
{
    /// <summary>
    /// 订单处理
    /// 罗丽娥   2010-10-21
    /// </summary>
    public partial class OrderList : EyouSoft.Common.Control.BackPage
    {
        public IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> diclist = null;

        protected string ContainerID = "OrderList";

        protected void Page_Load(object sender, EventArgs e)
        {
            ContainerID += Guid.NewGuid().ToString();

            if (!Page.IsPostBack)
            {
                diclist = EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().GetSupplierHandelStats(SiteUserInfo.CompanyID);

            }
        }
    }
}
