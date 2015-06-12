using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using EyouSoft.Common;

namespace IMFrame.RouteAgency.TourOrder
{
    /// <summary>
    /// 页面功能:ajax调用批发商的订单主页面
    /// 章已泉 2009-9-10
    ///MQ修改 luofx 2010-8-16
    /// </summary>
    public partial class Default : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string OrderType = "";
        /// <summary>
        /// 历史订单数量
        /// </summary>
        protected string OrderHistoryNum = string.Empty;
        /// <summary>
        /// 待处理订单数量
        /// </summary>
        protected string OrderOrdersReceivedNum = string.Empty;
        /// <summary>
        /// 处理中订单
        /// </summary>
        protected string OrderProcessedNum = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsLogin)
            {
                if (!this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    Response.Clear();
                    Response.Write("对不起，不属于专线商，没有权限操作该页面！");
                    Response.End();
                }
                Dictionary<string, int> dic = null;
                EyouSoft.IBLL.NewTourStructure.ITourList iorderBll = EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance();
                dic = iorderBll.GetOrderBusinessCount(SiteUserInfo.CompanyID);
                if (dic != null)
                {
                    OrderOrdersReceivedNum = dic["散客订单未处理"].ToString();
                    //预留 确定 结单 取消   dic["散客订单已确认"]包含结单的订单
                    OrderProcessedNum = (dic["散客预留待付款"] + dic["散客订单已确认"] + dic["散客订单已取消"]).ToString();
                    OrderHistoryNum = dic["历史订单"].ToString();
                }
            }
        }
    }
}
