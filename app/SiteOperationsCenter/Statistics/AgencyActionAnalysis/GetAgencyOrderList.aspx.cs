using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics.AgencyActionAnalysis
{
    /// <summary>
    /// 功能：获的零售商预订列表
    /// 开发人:刘玉灵   时间：2010-9-17
    /// </summary>
    public partial class GetAgencyOrderList : EyouSoft.Common.Control.YunYingPage
    { 
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        private int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int Count = 0;
        /// <summary>
        /// 是否为查看所有
        /// 默认查看top10
        /// </summary>
        private bool isLookAll = false;
        protected int? company_OrderIndex = null;
        protected int? ordernum_OrderIndex = null;
        protected int? orderpeople_OrderIndex = null;
        protected int currentOrderIndex = 5;
        /// <summary>
        /// 类型名称
        /// </summary>
        protected string strOrderTypeName = "所有";
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限控制
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, false);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["LookAll"]))
            {
                isLookAll = true;
                PageSize = 20;
                PageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            }
            if (!Page.IsPostBack)
            {
                this.BindOrderList();
            }
        }

        /// <summary>
        /// 获的批发商预定列表
        /// </summary>
        private void BindOrderList()
        {
            //初始化排序参数
            InitOrderIndex();
            //初始化查询参数
            int recordCount = 0;
            string CompanyName = Server.UrlDecode(Request.QueryString["CompanyName"]);
            DateTime? StartDate = Utils.GetDateTimeNullable(Request.QueryString["StartDate"]);
            DateTime? EndDate = Utils.GetDateTimeNullable(Request.QueryString["EndDate"]);
            int OrderType = Utils.GetInt(Request.QueryString["OrderType"],-1);
            EyouSoft.Model.TourStructure.OrderState  ? OrderState=null;
            if(OrderType>0)
            {
                switch(OrderType)
                {
                    case 5:
                         OrderState=EyouSoft.Model.TourStructure.OrderState.已成交;
                         strOrderTypeName = "预定成功";
                     break;
                     case 2:
                         OrderState=EyouSoft.Model.TourStructure.OrderState.已留位;
                         strOrderTypeName = "留位订单";
                        break;
                    case 3:
                         OrderState=EyouSoft.Model.TourStructure.OrderState.留位过期;
                         strOrderTypeName = "留位过期";
                         break;
                    case 4:
                         OrderState=EyouSoft.Model.TourStructure.OrderState.不受理;
                         strOrderTypeName = "不受理";
                         break;
                }
            }
            IList<EyouSoft.Model.TourStructure.OrderStatistics> OrderList = new List<EyouSoft.Model.TourStructure.OrderStatistics>();
            if (!isLookAll)
            {
                OrderList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderStatistics(PageSize, CompanyName, MasterUserInfo.AreaId, StartDate, EndDate, OrderState, 3);
                if (OrderList != null && OrderList.Count > 0)
                {
                    this.rep_CompanyOrderList.DataSource = OrderList;
                    this.rep_CompanyOrderList.DataBind();
                }
                else {
                    this.rep_CompanyOrderList.EmptyText = "<tr><td height='100px' align='center' colspan='5'>暂无预定记录</td></tr>";
                }
            }
            else
            {
                OrderList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderStatistics(PageSize, PageIndex, ref recordCount, currentOrderIndex, CompanyName,MasterUserInfo.AreaId, StartDate, EndDate, OrderState);
                if (recordCount > 0)
                {
                    this.ExporPageInfoSelect1.intPageSize = PageSize;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                    this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                    this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                    this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "AllOrderList.LoadData(this);", 1);
                    this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "AllOrderList.LoadData(this);", 0);
                    this.rep_CompanyOrderList.DataSource = OrderList;
                    this.rep_CompanyOrderList.DataBind();

                    this.tbExporPage.Visible = true;
                }
                else {
                    this.rep_CompanyOrderList.EmptyText = "<tr><td height='100px' align='center' colspan='5'>暂无预定记录</td></tr>";
                    this.tbExporPage.Visible = false;
                }
            }
            OrderList = null;
        }
        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            if (isLookAll)
            {
                return ++Count + (PageIndex - 1) * PageSize;
            }
            else
            {
                return ++Count;
            }
        }

        /// <summary>
        /// 初始化排序参数
        /// </summary>
        protected void InitOrderIndex()
        {
            company_OrderIndex = Utils.GetIntNull(Request.QueryString["companyorder"]);
            ordernum_OrderIndex = Utils.GetIntNull(Request.QueryString["ordernumorder"]);
            orderpeople_OrderIndex = Utils.GetIntNull(Request.QueryString["orderpeopleorder"]);
            if (company_OrderIndex.HasValue)
            {
                if (company_OrderIndex.Value != 0 && company_OrderIndex.Value != 1)
                {
                    company_OrderIndex = null;
                }
            }
            if (ordernum_OrderIndex.HasValue)
            {
                if (ordernum_OrderIndex.Value != 2 && ordernum_OrderIndex.Value != 3)
                {
                    ordernum_OrderIndex = null;
                }
            }
            if (orderpeople_OrderIndex.HasValue)
            {
                if (orderpeople_OrderIndex.Value != 4 && orderpeople_OrderIndex.Value != 5)
                {
                    orderpeople_OrderIndex = null;
                }
            }

            if (orderpeople_OrderIndex.HasValue)
            {
                currentOrderIndex = orderpeople_OrderIndex.Value;
            }
            else if (ordernum_OrderIndex.HasValue)
            {
                currentOrderIndex = ordernum_OrderIndex.Value;
            }
            else if (company_OrderIndex.HasValue)
            {
                currentOrderIndex = company_OrderIndex.Value;
            }
            else
            {
                currentOrderIndex = 5;
            }
        }

        protected string GetTableHead(int index)
        {
            string head = string.Empty;
            switch (index)
            {
                case 1:
                    if (!isLookAll)
                    {
                        head = "<strong>零售商名称</strong>";
                    }
                    else
                    {
                        if (company_OrderIndex.HasValue&&company_OrderIndex.Value==currentOrderIndex)
                        {
                            if (currentOrderIndex == 0)
                            {
                                head = "<a class='DataListUpGreen' onclick='AllOrderList.UpdateOrderIndex(\"companyorder\",\"1\");return false;' title='点击按照零售商名称排序' href='#'>零售商名称</a>";
                            }
                            else
                            {
                                head = "<a class='DataListDownGreen' onclick='AllOrderList.UpdateOrderIndex(\"companyorder\",\"0\");return false;' title='点击按照零售商名称排序' href='#'>零售商名称</a>";
                            }
                        }
                        else
                        {
                            head = "<a class='DataListDownGray' onclick='AllOrderList.UpdateOrderIndex(\"companyorder\",\"1\");return false;'  title='点击按照零售商名称排序' href='#'>零售商名称</a>";
                        }
                    }
                    break;
                case 2:
                    if (!isLookAll)
                    {
                        head = "<strong>预订次数</strong>";
                    }
                    else
                    {
                        if (ordernum_OrderIndex.HasValue && ordernum_OrderIndex.Value == currentOrderIndex)
                        {
                            if (currentOrderIndex == 2)
                            {
                                head = "<a class='DataListUpGreen' onclick='AllOrderList.UpdateOrderIndex(\"ordernumorder\",\"3\");return false;' title='点击按照预订次数从高到低排序' href='#'>预订次数</a>";
                            }
                            else
                            {
                                head = "<a class='DataListDownGreen' onclick='AllOrderList.UpdateOrderIndex(\"ordernumorder\",\"2\");return false;'  title='点击按照预订次数从低到高排序' href='#'>预订次数</a>";
                            }
                        }
                        else
                        {
                            head = "<a class='DataListDownGray' onclick='AllOrderList.UpdateOrderIndex(\"ordernumorder\",\"3\");return false;' title='点击按照预订次数从高到低排序' href='#'>预订次数</a>";
                        }
                    }
                    break;
                case 3:
                    if (!isLookAll)
                    {
                        head = "<strong>预订总人数</strong>";
                    }
                    else
                    {
                        if (orderpeople_OrderIndex.HasValue && orderpeople_OrderIndex.Value == currentOrderIndex)
                        {
                            if (currentOrderIndex == 4)
                            {
                                head = "<a class='DataListUpGreen' onclick='AllOrderList.UpdateOrderIndex(\"orderpeopleorder\",\"5\");return false;' title='点击按照预订总人数从高到低排序' href='#'>预订总人数</a>";
                            }
                            else
                            {
                                head = "<a class='DataListDownGreen' onclick='AllOrderList.UpdateOrderIndex(\"orderpeopleorder\",\"4\");return false;' title='点击按照预订总人数从低到高排序' href='#'>预订总人数</a>";
                            }
                        }
                        else
                        {
                            head = "<a class='DataListDownGray' onclick='AllOrderList.UpdateOrderIndex(\"orderpeopleorder\",\"5\");return false;' title='点击按照预订总人数从高到低排序' href='#'>预订总人数</a>";
                        }
                    }
                    break;
            }
            return head;
        }
    }
}
