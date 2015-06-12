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
    /// 页面功能：预定游客信息列表
    /// 开发时间：2010-09-20 开发人：xuty
    /// </summary>
    public partial class BookPeopleCount : EyouSoft.Common.Control.YunYingPage
    {
        protected int pageIndex;
        protected int pageSize = 10;
        protected int recordCount;
        protected string companyName;//专线商
        protected string peopleName;//游客姓名
        protected string companyId;//组团社ID
        protected string strOrderState;
        protected void Page_Load(object sender, EventArgs e)
        {   
            //判断权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, true);
                return;
            }
            bpc_date.SetTitle = "下单时间";//设置时间控件的标题
            DateTime? startDate=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("BeginTime"));//下单开始时间
            DateTime? endDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndTime"));//下单结束时间
            companyName = Utils.GetQueryStringValue("companyname");
            peopleName = Utils.GetQueryStringValue("peoplename");
            string sexStr=Utils.GetQueryStringValue("sex");//性别
            bool? sex;
            if (sexStr == "")
            {
                sex = null;
            }
            else
            {
                sex = sexStr == "1" ? true : false;
                if (sex.Value)
                {
                    bpc_selSex.Value = "1";
                }
                else
                {
                    bpc_selSex.Value = "0";
                }
            }
            companyId=Utils.GetQueryStringValue("companyId");
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.TourStructure.TourOrderCustomer> custList;
            //获取游客信息列表
            EyouSoft.IBLL.TourStructure.ITourOrderCustomer orderCustomerBll = EyouSoft.BLL.TourStructure.TourOrderCustomer.CreateInstance();
            strOrderState = Utils.GetQueryStringValue("OrderState");
            EyouSoft.Model.TourStructure.OrderState orderState = EyouSoft.Model.TourStructure.OrderState.未处理;
            switch (strOrderState)
            {
                case "5":
                    orderState = EyouSoft.Model.TourStructure.OrderState.已成交;
                    break;
                case "2":
                    orderState = EyouSoft.Model.TourStructure.OrderState.已留位;
                    break;
                case "3":
                    orderState = EyouSoft.Model.TourStructure.OrderState.留位过期;
                    break;
                case "4":
                    orderState = EyouSoft.Model.TourStructure.OrderState.不受理;
                   break;
            }
            if (orderState!=EyouSoft.Model.TourStructure.OrderState.未处理)
            {
               
                custList = orderCustomerBll.GetCustomerList(pageSize, pageIndex, ref recordCount, -1, companyId, companyName, peopleName, sex, startDate, endDate, orderState);
            }
            else
            {
                custList = orderCustomerBll.GetCustomerList(pageSize, pageIndex, ref recordCount, -1, companyId, companyName, peopleName, sex, startDate, endDate);
            }
           if (custList != null && custList.Count > 0)
            {
                bpc_rpt_applyList.DataSource = custList;
                bpc_rpt_applyList.DataBind();
                BindPage();
            }
            else
            {
                bpc_rpt_applyList.EmptyText = "暂无人数信息";
                this.ExporPageInfoSelect1.Visible = false;
            }
            //设置时间控件上的时间
            if (startDate.HasValue)
            {
                bpc_date.SetStartDate = startDate.Value.ToString("yyyy-MM-dd");
            }
            if (endDate.HasValue)
            {
                bpc_date.SetEndDate = endDate.Value.ToString("yyyy-MM-dd");
            }
        }
       /// <summary>
       /// 设置分页控件
       /// </summary>
        protected void BindPage()
        {
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.UrlHref;
            
            this.ExporPageInfoSelect1.UrlParams=Request.QueryString;
           
            this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "ExporPageInfoSelect_Change(this);", 0);
          
        }
    }
}
