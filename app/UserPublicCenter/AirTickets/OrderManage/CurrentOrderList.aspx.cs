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
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace UserPublicCenter.AirTickets.OrderManage
{
    /// <summary>
    /// 机票当前最新订单查询
    /// 袁惠
    /// </summary>
    public partial class CurrentOrderList : EyouSoft.Common.Control.FrontPage
    {
        protected string NowDate = DateTime.Now.ToString("yyyy年MM月dd日");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.Master.Naviagtion = AirTicketNavigation.机票订单管理;
                this.Title = "当前最新订单_机票";
                string CancelOrder = Utils.InputText(Request.QueryString["CancelOrder"]); 
                if(CancelOrder=="true")       //取消订单操作
                {
                    if (EyouSoft.BLL.TicketStructure.TicketOrder.CreateInstance().CancleOrder(Utils.GetQueryStringValue("OrderId"), SiteUserInfo.OpUserId.ToString(),SiteUserInfo.CompanyID))
                    {
                        Utils.ResponseMeg(true, "操作成功！");
                        return;
                    }
                    else
                    {
                        Utils.ResponseMeg(false, "操作失败！");
                        return;
                    }
                }               
                BindOrderState();
            }
        }
        #region 订单状态控件绑定
        private void BindOrderState()
        {
            string [] orderstate=Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderState));
            sltOrderState.Items.Add(new ListItem("-请选择-", "0"));
            if(orderstate.Length>0)
            {
                for (int i = 0; i <orderstate.Length; i++)
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
                    sltOrderState.Items.Add(new ListItem("所有"+changetype[i],changetype[i]));
                    types =types+ changetype[i] + ","; 
			    }
                hidchangeType.Value =types;
            }
            string [] changestate = Enum.GetNames(typeof(EyouSoft.Model.TicketStructure.OrderChangeState));
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
