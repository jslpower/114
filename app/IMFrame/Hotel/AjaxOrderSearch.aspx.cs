using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace IMFrame.Hotel
{   
    /// <summary>
    /// AiaxMQ酒店订单页(历史订单,当前订单)
    /// </summary>
    public partial class AjaxOrderSearch : EyouSoft.ControlCommon.Control.MQPage
    {
        protected int pageSize = 10;
        protected int pageIndex = 1;
        protected int recordCount;
        protected int recordCountNow;//处理中的订单数
        protected int recordCountHistory;//历史记录数
        protected string remark = "";//备注(用于提示有无订单)
        IList<EyouSoft.Model.HotelStructure.OrderInfo> orderList;//订单列表
        string orderType;//订单类别(历史订单,当前订单)
        protected void Page_Load(object sender, EventArgs e)
        {
            orderType = Utils.GetQueryStringValue("orderType");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            //查询实体
            EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo = new EyouSoft.Model.HotelStructure.SearchOrderInfo();
            SearchInfo.CompanyId = SiteUserInfo.CompanyID;
            if (orderType == "now")//当前订单
            {
                SearchInfo.CreateSDate = DateTime.Now.Date;//预定开始时间为今天零点
                orderList = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, SearchInfo);
                recordCountNow=recordCount;//当前订单数
            }
            else if(orderType=="history")//历史订单
            {
                SearchInfo.CreateEDate = DateTime.Now.Date; //预定结束时间为今天零点
                orderList = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, SearchInfo);
                recordCountHistory=recordCount;//历史订单数
            }
            else//默认为显示当前订单(并获取历史订单数,当前订单数)
            {
                SearchInfo.CreateEDate = DateTime.Now.Date;//预定结束时间为今天零点
               orderList = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, SearchInfo);
               recordCountNow=recordCount;//当前订单数
               SearchInfo = new EyouSoft.Model.HotelStructure.SearchOrderInfo();
               SearchInfo.CreateSDate = DateTime.Now.Date; //预定结束时间为今天零点
               SearchInfo.CompanyId = SiteUserInfo.CompanyID;
               orderList = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, SearchInfo);
               recordCountHistory=recordCount;//历史订单数
            }
            //绑定订单列表
            if(orderList != null && orderList.Count > 0)
            {
                rpt_orderList.DataSource = orderList;
                rpt_orderList.DataBind();
                BindExportPage();
               
            }
            else
            {   //无订单则隐藏分页控件，显示无订单消息
                ExporPageInfoSelect1.Visible = false;
                remark = "暂无订单信息";
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        protected void BindExportPage()
        {
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.intRecordCount = recordCount;
            //添加点击事件用ajax获取订单数据
            ExporPageInfoSelect1.AttributesEventAdd("onclick", "GetOrderList(this,\""+orderType+"\")", 1);
            
        }
        protected string GetOrderLink(object orderId)
        {
            return GetDesPlatformUrl(Domain.UserBackCenter + "/HotelCenter/HotelOrderManage/HotelOrderList.aspx?isMQOrder=ordermqyesss&orderId=" + (string)orderId);
        }

    }
}
