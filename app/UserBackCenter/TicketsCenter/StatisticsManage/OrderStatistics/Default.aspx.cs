using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserBackCenter.TicketsCenter.StatisticsManage.OrderStatistics
{
    public partial class Default : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderState();
            }
        }


        #region 订单状态控件绑定
        private void BindOrderState()
        {
            string[] orderstate = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderState));
            sltOrderState.Items.Add(new ListItem("-请选择-", "0"));
            if (orderstate.Length > 0)
            {
                for (int i = 0; i < orderstate.Length; i++)
                {
                    sltOrderState.Items.Add(new ListItem(orderstate[i], orderstate[i]));
                }
            }
            string[] changetype = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderChangeType));
            if (changetype.Length > 0)
            {
                string types = "";
                for (int i = 0; i < changetype.Length; i++)
                {
                    sltOrderState.Items.Add(new ListItem("所有" + changetype[i], changetype[i]));
                    types = types + changetype[i] + ",";
                }
                hidchangeType.Value = types;
            }
            string[] changestate = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderChangeState));
            sltOrdChangetype.Items.Add(new ListItem("-请选择-", "0"));
            if (changestate.Length > 0)
            {
                for (int i = 0; i < changestate.Length; i++)
                {
                    sltOrdChangetype.Items.Add(new ListItem(changestate[i], changestate[i]));
                }
            }
        }
        #endregion
    }
}
