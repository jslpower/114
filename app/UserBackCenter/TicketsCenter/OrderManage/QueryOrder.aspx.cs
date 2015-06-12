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
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserBackCenter.TicketsCenter.OrderManage
{
    /// <summary>
    /// 订单查询
    /// 罗丽娥  2010-10-19
    /// </summary>
    public partial class QueryOrder : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitFlightList();
            }
        }

        /// <summary>
        /// 初始化航空公司
        /// </summary>
        private void InitFlightList()
        {
            QueryOrder_drpFlightName.Items.Clear();

            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompanyList(null);
            if (list != null && list.Count > 0)
            {
                foreach (EyouSoft.Model.TicketStructure.TicketFlightCompany model in list)
                {
                    this.QueryOrder_drpFlightName.Items.Add(new ListItem(model.AirportName, model.Id.ToString()));
                }
            }

            QueryOrder_drpFlightName.Items.Insert(0, new ListItem("请选择航空公司", ""));
            list = null;
        }
    }
}
