using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common;

namespace IMFrame.TourAgency.TourOrder
{
    /// <summary>
    /// 页面功能:ajax调用 订单合计汇总
    /// </summary>
    /// 金海锋 2009-9-10
    ///MQ修改 luofx 2010-8-16
    ///
    public partial class AjaxOrderTotal : EyouSoft.ControlCommon.Control.MQPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitOrderList();
            }
        }
        /// <summary>
        /// 初始化团队
        /// </summary>
        private void InitOrderList()
        {
            string allorder = string.Empty;

            //集合中统计的所有订单均为未发团的订单
            var dic =
                EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance().GetOrderTravelCount(SiteUserInfo.CompanyID);

            if (dic != null)
            {
                if (CheckGrant(TravelPermission.组团_线路散客订单管理))
                {
                    //所有未发团散客订单
                    allorder += GetOrderNumAndLink(dic, 1, "所有未发团散客订单");

                    // 散客订单未处理
                    allorder += GetOrderNumAndLink(dic, 2, "未处理散客订单");

                    // 预留待付款
                    allorder += GetOrderNumAndLink(dic, 3, "预留待付款");

                    // 已确认散客订单
                    allorder += GetOrderNumAndLink(dic, 4, "已确认散客订单");
                }

                if (CheckGrant(TravelPermission.组团_线路团队订单管理))
                {
                    // 所有未发团团队订单
                    allorder += GetOrderNumAndLink(dic, 5, "所有团队订单");

                    // 未确认团队订单
                    allorder += GetOrderNumAndLink(dic, 6, "未确认团队订单");

                    // 已确认团队订单
                    allorder += GetOrderNumAndLink(dic, 7, "已确认团队订单");
                }
            }

            Response.Clear();
            Response.Write(allorder);
            Response.End();

        }

        /// <summary>
        /// 获取订单统计的数量和链接
        /// </summary>
        /// <param name="dic">统计字典数据集合</param>
        /// <param name="key">
        /// 1、所有未发团散客订单；2、未处理散客订单；3、预留待付款订单；4、已确认散客订单；5、未发团团队预订单；6、未确认团队订单；7、已确认团队订单；
        /// </param>
        /// <param name="text">显示的文字</param>
        /// <returns></returns>
        private string GetOrderNumAndLink(Dictionary<string, int> dic, int key, string text)
        {
            var strReturn = new StringBuilder("<a target=\"_blank\" href=\"");
            int orderNum = 0;
            if (dic == null || key <= 0 || key > 7)
                return string.Empty;

            string strUrl = key <= 4
                                ? string.Format("/teamservice/fitorders.aspx?goTimeS={0}",
                                                DateTime.Now.ToShortDateString())
                                : string.Format("/teamservice/teamorders.aspx?ref=0&goTimeS={0}",
                                                DateTime.Now.ToShortDateString());

            switch (key)
            {
                case 1:
                    if (dic.ContainsKey("未发团散客预订单"))
                        orderNum += dic["未发团散客预订单"];
                    break;
                case 2:
                    if (dic.ContainsKey("散客订单未处理"))
                        orderNum += dic["散客订单未处理"];

                    strUrl += string.Format("&status={0},{1},{2}",
                                       (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理,
                                       (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社已阅,
                                       (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商待处理);
                    break;
                case 3:
                    if (dic.ContainsKey("散客订单预留待付款"))
                        orderNum += dic["散客订单预留待付款"];

                    strUrl += string.Format("&status={0}", (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留);
                    break;
                case 4:
                    if (dic.ContainsKey("散客订单已确认"))
                        orderNum += dic["散客订单已确认"];

                    strUrl += string.Format("&status={0},{1}",
                                       (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已确定,
                                       (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单);
                    break;
                case 5:
                    if (dic.ContainsKey("未发团团队预订单"))
                        orderNum += dic["未发团团队预订单"];
                    break;
                case 6:
                    if (dic.ContainsKey("团队未确认"))
                        orderNum += dic["团队未确认"];

                    strUrl += string.Format("&status={0}", (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认);
                    break;
                case 7:
                    if (dic.ContainsKey("团队已确认"))
                        orderNum += dic["团队已确认"];

                    strUrl += string.Format("&status={0},{1}",
                                            (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.已确认,
                                            (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.结单);
                    break;
            }

            strReturn.AppendFormat("{0}\">{1}（{2}）</a><br/>",
                                           GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + strUrl),
                                           text,
                                           orderNum);

            return strReturn.ToString();
        }
    }
}
