using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.Text;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 最新订单
    /// DYZ   2012-01-09
    /// </summary>
    /// 修改
    /// 添加了单个团的订单列表
    /// GetNewPowderOrder方法添加了一个团号参数
    /// 柴逸宁
    public partial class NewOrders : YunYingPage
    {
        /// <summary>
        /// 容器ID
        /// </summary>
        protected string tblID = "";
        #region 查询条件
        /// <summary>
        /// 选中线路区域ID
        /// </summary>
        protected int areaID;
        #endregion

        #region 分页变量
        protected int pageSize = 15;
        public int pageIndex = 1;
        protected int recordCount;
        #endregion

        /// <summary>
        /// 订单bll操作对象
        /// </summary>
        EyouSoft.IBLL.NewTourStructure.ITourOrder orderBll = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance();

        protected void Page_Load(object sender, EventArgs e)
        {
            tblID = Guid.NewGuid().ToString();
            string searchKey = Server.UrlDecode(Utils.GetQueryStringValue("searchKey"));
            areaID = Utils.GetInt(Utils.GetQueryStringValue("areaID"));
            string dateBegin = Utils.GetQueryStringValue("dateBegin");
            string dateEnd = Utils.GetQueryStringValue("dateEnd");

            //初始化查询条件
            if (searchKey == "")
            {
                this.txtSearchKey.Text = "订单编号、线路名称";
            }
            else
            {
                this.txtSearchKey.Text = searchKey;
            }

            this.txtDateBegin.Text = dateBegin == "" ? "" : Convert.ToDateTime(dateBegin).ToString("yyyy-MM-dd");
            this.txtDateEnd.Text = dateEnd == "" ? "" : Convert.ToDateTime(dateEnd).ToString("yyyy-MM-dd");

            //页面初始化
            DataInit(searchKey, areaID, dateBegin, dateEnd);
        }


        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="searchKey">搜索关键字</param>
        /// <param name="areaID">线路区域编号</param>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        protected void DataInit(string searchKey, int areaID, string begin, string end)
        {
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
            //声明bll 操作对象
            EyouSoft.IBLL.CompanyStructure.ICompanyUser cBLL = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            //声明公司对象
            EyouSoft.Model.CompanyStructure.CompanyUser cModel = cBLL.GetModel(0);//SiteUserInfo.ID
            if (cModel != null && cModel.Area != null && cModel.Area.Count > 0)
            {
                //绑定公司的线路区域
                this.rptAreaList.DataSource = cModel.Area;
                this.rptAreaList.DataBind();
                //绑定下拉框线路区域
                AreaListBind(cModel.Area, areaID);
                //团号(团号参数为null则最新订单,存在团号既查询单独团号下的订单)
                string tourId = Request.QueryString["tourId"];
                #region 团查询实体赋值
                EyouSoft.Model.NewTourStructure.MTourListSearch searchModel = new EyouSoft.Model.NewTourStructure.MTourListSearch();
                searchModel.TourKey = searchKey;
                if (tourId == null)
                {
                    searchModel.TourOrderStatus = EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认;
                }
                searchModel.SLeaveDate = Utils.GetDateTimeNullable(begin);
                searchModel.ELeaveDate = Utils.GetDateTimeNullable(end);
                #endregion



                //获得未处理订单的团
                EyouSoft.IBLL.NewTourStructure.ITourList tourBll = EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance();

                IList<EyouSoft.Model.NewTourStructure.MPowderOrder> tourList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetNewPowderOrder(
                    pageSize,
                    pageIndex,
                    ref recordCount,
                    "",
                    //SiteUserInfo.CompanyID,
                    searchKey,
                    areaID,
                    Utils.GetDateTimeNullable(begin),
                    Utils.GetDateTimeNullable(end),
                    tourId);//团号
                if (tourList != null && tourList.Count > 0)
                {
                    this.rptList.DataSource = tourList;
                    this.rptList.DataBind();
                    BindPage();
                }
                else
                {
                    this.ExportPageInfo1.Visible = false;
                    this.litMsg.Text = "暂无最新订单";
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 根据团号获得未处理订单
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        protected string GetOrderListByTourID(object list)
        {
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.NewTourStructure.MTourOrder> orderList = (List<EyouSoft.Model.NewTourStructure.MTourOrder>)list;
            if (orderList != null && orderList.Count > 0)
            {
                for (int i = 0; i < orderList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        sb.Append("<tr>");
                    }
                    else
                    {
                        sb.Append("<tr class=\"odd\">");
                    }
                    sb.Append(" <td height=\"30\" align=\"center\">" + orderList[i].OrderNo + "</td>");
                    sb.Append("<td align=\"left\"><a target=\"_blank\" href=\"" + Utils.GetCompanyDomain(orderList[i].Travel, EyouSoft.Model.CompanyStructure.CompanyType.组团) + "\">" + orderList[i].TravelName + "</a></td>");
                    sb.Append("<td align=\"center\">" + orderList[i].TravelContact + "</td>");
                    sb.Append("<td align=\"center\">" + orderList[i].TravelTel + "</td>");
                    sb.Append("<td align=\"center\">" + orderList[i].IssueTime.ToString("yyyy-MM-dd") + "</td>");
                    sb.Append("<td align=\"center\">" + orderList[i].AdultNum.ToString() + "<sup>+" + orderList[i].ChildrenNum + "</sup></td>");
                    if (orderList[i].OrderStatus == EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留)
                    {
                        sb.Append("<td align=\"center\">预留<br />" + orderList[i].SaveDate == null ? "无" : Convert.ToDateTime(orderList[i].SaveDate).ToString("yyyy-MM-dd") + "</td>");
                    }
                    else
                    {
                        sb.Append("<td align=\"center\">" + orderList[i].OrderStatus.ToString() + "</td>");
                    }

                    sb.Append("<td align=\"left\">" + orderList[i].PaymentStatus.ToString() + "</td>");
                    if (orderList[i].OrderStatus == EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单)
                    {
                        sb.Append("<td nowrap=\"nowrap\" align=\"center\"><a target=\"_blank\" href=\"/PrintPage/OutGroupNotices.aspx?TeamId=" + orderList[i].TourId + "\">出团通知书</a></td>");
                    }
                    else
                    {
                        sb.Append("<td nowrap=\"nowrap\" align=\"center\"><a target=\"_blank\" href=\"/PrintPage/TeamConfirm.aspx?OrderId=" + orderList[i].OrderId + "\">团队确认单</a></td>");
                    }
                    sb.Append("<td nowrap=\"nowrap\" align=\"center\"><a href=\"javascript:void(0);\" onclick=\"javascript:topTab.open('/order/routeagency/OrderStateUpdate.aspx?orderID=" + orderList[i].OrderId + "','订单查看')\">查看</a> <a target=\"_blank\" href=\"/RouteAgency/OrderStateLog.aspx?orderID=" + orderList[i].OrderId + "\">日志</a></td>");
                    sb.Append("</tr>");

                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 绑定下拉框 线路区域
        /// </summary>
        /// <param name="list">数据集合</param>
        /// <param name="areaID">选中线路区域ID</param>
        protected void AreaListBind(IList<EyouSoft.Model.SystemStructure.AreaBase> list, int areaID)
        {
            this.ddlAreaList.DataSource = list;
            this.ddlAreaList.DataTextField = "AreaName";
            this.ddlAreaList.DataValueField = "AreaId";
            this.ddlAreaList.DataBind();
            if (areaID > 0)
            {
                this.ddlAreaList.SelectedValue = areaID.ToString();
            }
        }

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] .ToString() + "?";
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion
    }
}
