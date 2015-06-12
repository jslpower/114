using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 专线商行订单列表页
    /// 功能：专线商订单列表
    /// 创建人：戴银柱
    /// 创建时间： 2010-09-19  
    /// </summary>
    public partial class LineOrderDetailsList : EyouSoft.Common.Control.YunYingPage
    {
        #region 分页变量
        protected int pageSize = 20;  //分页大小
        public int pageIndex = 1;     //当前页
        protected int recordCount;    //数据行数
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断权限
                if (!CheckMasterGrant(YuYingPermission.统计分析_专线商行为分析))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_专线商行为分析, true);
                    return;
                }

                #region Request 参数
                //设置时间控件的title
                this.StartAndEndDate1.SetTitle = "时间";

                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);

                //获得开始时间
                DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("startTime"));
                if (startTime != null)
                {
                    this.StartAndEndDate1.SetStartDate = Convert.ToDateTime(startTime).ToString("yyyy-MM-dd");
                }
                
                //获得结束时间
                DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("endTime"), DateTime.Now);
                if (endTime != null)
                {
                    this.StartAndEndDate1.SetEndDate =Convert.ToDateTime(endTime).ToString("yyyy-MM-dd");
                }
                

                //订单类型
                string orderType = Utils.GetQueryStringValue("orderType");
                this.hideOrderType.Value = orderType;

                //获得公司Id
                string companyId = Utils.GetQueryStringValue("companyId");
                this.hideCompanyId.Value = companyId;

                //获得排序值
                int orderIndex = Utils.GetInt(Utils.GetQueryStringValue("orderIndex"));
                this.hideOrderIndex.Value = orderIndex.ToString();

                //组团社名称
                string buyCompanyName = Utils.GetQueryStringValue("buyCompanyName") == "" ? null : Server.UrlDecode(Utils.GetQueryStringValue("buyCompanyName"));
                this.txtTourCompanyName.Value = buyCompanyName;

                //团号
                string tourNo = Utils.GetQueryStringValue("tourNo") == "" ? null : Server.UrlDecode(Utils.GetQueryStringValue("tourNo"));
                this.txtTourNo.Value = tourNo;

                //线路名称
                string routeName = Utils.GetQueryStringValue("RouteName") == "" ? null : Server.UrlDecode(Utils.GetQueryStringValue("RouteName"));
                this.txtRouteName.Value = routeName;

                //查询方式
                string type = Utils.GetQueryStringValue("type");
                this.hideType.Value = type;

                #endregion

                #region 根据排序参数改变样式
                switch (orderIndex)
                {
                    case 0: this.litTime.Text = "<a onclick=\"DataSort()\" class=\"DataListUpGreen\">下单时间</a>";
                        this.hideOrderIndex.Value = "1"; break;
                    case 1: this.litTime.Text = "<a onclick=\"DataSort()\" class=\"DataListDownGreen\">下单时间</a>";
                        this.hideOrderIndex.Value = "0"; break;
                    default: this.litTime.Text = "<a onclick=\"DataSort()\" class=\"DataListDownGray\">下单时间</a>";
                        this.hideOrderIndex.Value = "2"; break;
                }
                #endregion

               
                //初始化列表
                DataListInit(orderType, orderIndex, buyCompanyName, tourNo, routeName, startTime, endTime, type, companyId);
            }
        }

        #region 列表绑定
        protected void DataListInit(string orderType, int orderIndex, string buyCompanyName, string tourNo, string routeName, DateTime? startTime, DateTime? endTime, string type, string companyId)
        {
            //订单类型
            EyouSoft.Model.TourStructure.OrderState orderState = EyouSoft.Model.TourStructure.OrderState.已成交;
            switch (orderType)
            {
                case "1": orderState = EyouSoft.Model.TourStructure.OrderState.已成交; break;
                case "2": orderState = EyouSoft.Model.TourStructure.OrderState.留位过期; break;
                case "3": orderState = EyouSoft.Model.TourStructure.OrderState.未处理; break;
                case "4": orderState = EyouSoft.Model.TourStructure.OrderState.已留位; break;
            }
            IList<EyouSoft.Model.TourStructure.TourOrder> list = null;
            if (type != "" && companyId == "")
            {
                list = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderList(pageSize, pageIndex, ref  recordCount, orderIndex, null, null, startTime, endTime, orderState);
            }
            else
            {
                list = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderList(pageSize, pageIndex, ref recordCount, orderIndex, companyId, buyCompanyName, tourNo, routeName, 0, 0, orderState, startTime, endTime);
            }

            if (list != null && list.Count > 0)
            {
                this.rpt_list.DataSource = list;
                this.rpt_list.DataBind();
                BindPage();
                this.lblMsg.Text = "";
            }
            else
            {
                this.lblMsg.Text = "未查到相关数据";
                this.ExportPageInfo1.Visible = false;
            }
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion
    }
}
