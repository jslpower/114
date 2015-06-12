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
using EyouSoft.Common.Control;
using System.Collections.Generic;
using EyouSoft.Common;
using System.IO;
using System.Text;
using EyouSoft.Common.Function;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 页面功能；酒店订单查询列表
    /// Author:liuym
    /// CreateTime:2010-12-06
    /// </summary>
    public partial class HotelOrderList :BackPage
    {
        #region Private Mebers
        protected int PageSize = 10;//每页显示的记录
        protected int PageIndex = 1;//页码
        protected int RecordCount=0;//总记录
        protected string IsFirstKey = string.Empty;//是否第一次加载
        protected int cityId = 0;//城市ID
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["IsFirstKey"]))
            {
                IsFirstKey = Request.QueryString["IsFirstKey"].ToString();
            }
            #region 导出到Excel中
            if (!string.IsNullOrEmpty(Utils.InputText(Request.QueryString["isExport"])))
            {
                this.ExcelClick();
            }  
            #endregion

            if (!Page.IsPostBack)
            {
                cityId = this.SiteUserInfo.CityId;
                //权限判断
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    Response.Clear();
                    Response.Write("对不起，您没有权限");
                    Response.End();
                    return;
                }
                //绑定订单类型
                BindOrderType();         
                //绑定酒店订单列表
                BindHotelOrderList();       
            }
        }
        #endregion    

        #region 绑定订单类型
        private void BindOrderType()
        {
            this.hol_ddlOrderType.Items.Add(new ListItem("-请选择-", ""));
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.HotelStructure.OrderType));
            if (typeList != null && typeList.Length > 0)
            {
                foreach (string str in typeList)
                {
                    this.hol_ddlOrderType.Items.Add(new ListItem(str, ((int)Enum.Parse(typeof(EyouSoft.Model.HotelStructure.OrderType), str)).ToString()));
                }
            }
            //释放资源
            typeList = null;
        }
        #endregion

        #region 绑定酒店订单查询列表
        protected void BindHotelOrderList()
        {
            ////默认查询两周内日期
            DateTime? StartTime =DateTime.Now.AddDays(-14);
            DateTime? EndTime =DateTime.Now;
            string FirstLoadStarTime = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");//第一次加载开始预订日期
            string FirstLoadEndTime = DateTime.Now.ToString("yyyy-MM-dd");//第一次加载结束预定日期
           
            PageIndex = Utils.GetInt(Request.QueryString["Page"], 1);//获得当前页码
            string orderId = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderId"] ?? "")),"");//订单编号
            string orderType = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderType"] ?? "")), "");//订单类型
            DateTime? orderStartTime = null;
            DateTime? orderEndTime = null;
            DateTime? checkInStartTime = null;
            DateTime? checkInEndTime = null;
            if (Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInStartTime"] ?? "")), "")!="")
            {
                checkInStartTime=DateTime.Parse(Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInStartTime"] ?? "")), ""));//开始入住日期
            }
            if (Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInEndTime"] ?? "")), "")!="")
            {
               checkInEndTime =DateTime.Parse(Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInEndTime"] ?? "")), ""));//结束入住日期
            }
                string hotelName = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["hotelName"] ?? "")), "");//酒店名称
            string orderStatus = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderStatus"] ?? "")), "");//订单状态
            string customerName = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["customerName"] ?? "")), "");//客人名称


            if (this.IsFirstKey == "1")//第一次加载
            {
                this.hol_txtOrderStartTime.Value = FirstLoadStarTime;
                this.hol_txtOrderEndTime.Value = FirstLoadEndTime;
                orderStartTime =DateTime.Parse(FirstLoadStarTime);
                orderEndTime = DateTime.Parse(FirstLoadEndTime);
            }
            else
            {
                orderStartTime=Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["orderStartTime"] ?? null)),null);//开始预订日期
                orderEndTime=Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["orderEndTime"] ?? null)),null);//开始预订日期
                this.hol_txtOrderStartTime.Value = orderStartTime.ToString();//开始预订日期;
                this.hol_txtOrderEndTime.Value = orderEndTime.ToString();//结束预订日期;
            }
            #region 查询实体赋值
            EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo = new EyouSoft.Model.HotelStructure.SearchOrderInfo();
            SearchInfo.ResOrderId = orderId;//订单编号
            SearchInfo.HotelName = hotelName;  //酒店名称       
            if (orderStatus.ToString() != "ahol_ckAll"&&orderStatus!="")
            {//订单状态
                SearchInfo.OrderState = (EyouSoft.Model.HotelStructure.OrderStateList)int.Parse(orderStatus);
            }
            if (orderType != "")
            {//订单类型
                SearchInfo.OrderType = (EyouSoft.Model.HotelStructure.OrderType)int.Parse(orderType);
            }
            SearchInfo.CustomerName = customerName;//客人姓名
            SearchInfo.CheckInSDate = checkInStartTime == null ? null : checkInStartTime;//开始入住日期  
            SearchInfo.CheckInEDate= checkInEndTime== null ? null :checkInEndTime;//结束入住日期 预定结束日期
            SearchInfo.CreateSDate = orderStartTime;//预定开始日期
            SearchInfo.CreateEDate = orderEndTime;//预定结束日期
            SearchInfo.CompanyId = this.SiteUserInfo.CompanyID;//公司ID
            #endregion

            #region 初始化查询后表单值
            this.hol_txtHotelName.Value = hotelName;//酒店名称
            string startTime1=string.Empty;
            string endTime1=string.Empty;
            if (this.IsFirstKey == "1")
            {
                 startTime1 =FirstLoadStarTime;//开始预订日期
                 endTime1 = FirstLoadEndTime;//预订结束日期
            }
            else
            {
                 startTime1 = Utils.InputText(Server.UrlDecode(Request.QueryString["orderStartTime"] ?? "")).ToString();//开始预订日期
                 endTime1 = Utils.InputText(Server.UrlDecode(Request.QueryString["orderEndTime"] ?? "")).ToString();//预订结束日期

            }
            string starTime2 = Utils.InputText(Server.UrlDecode(Request.QueryString["checkInStartTime"] ?? "")).ToString();//开始入住日期
            string endTime2 = Utils.InputText(Server.UrlDecode(Request.QueryString["checkInEndTime"] ?? "")).ToString();//结束入住日期

            this.hol_txtOrderStartTime.Value = startTime1 ;//预定开始日期
            this.hol_txtOrderEndTime.Value = endTime1;     //预定结束日期        
            this.hol_txtCheckInStartTime.Value =starTime2==""?"":starTime2;    //入住开始日期           
            this.hol_txtCheckInEndTime.Value = endTime2==""?"":endTime2;//入住结束日期
            this.hol_txtOrderId.Value = orderId;//订单编号
            this.hol_txtCustomerName.Value = customerName;   //客人姓名      
            this.hol_ddlOrderType.SelectedValue = orderType == "" ? "" : orderType;//订单类别              
            switch (orderStatus)//订单状态
            {
                case "ahol_ckAll":
                    this.ahol_ckAll.Checked = true;//全部
                    break;
                case "":
                    this.ahol_ckAll.Checked = true;//全部
                    break;
                case "0":
                    this.ahol_ckOK.Checked = true;//确认
                    this.ahol_ckAll.Checked = false;
                    break;
                case "1":
                    this.ahol_ckHandle.Checked = true;//处理
                    this.ahol_ckAll.Checked = false;
                    break;
                case "2":
                    this.ahol_ckCansel.Checked = true;//取消
                    this.ahol_ckAll.Checked = false;
                    break;
                //case "4":
                //    this.ahol_ckNOWSHOW.Checked = true;//NOWSHOW
                //    this.ahol_ckAll.Checked = false;
                //    break;
                //default:
                //    this.ahol_ckAll.Checked = true;//默认选择全部
                //    break;
            }
            #endregion

            //查询酒店订单集合
            IList<EyouSoft.Model.HotelStructure.OrderInfo> list = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(PageSize, PageIndex, ref RecordCount, SearchInfo);
            if (list.Count != 0 && list != null)
            {
                this.hol_crp_HotelOrderList.DataSource = list;
                this.hol_crp_HotelOrderList.DataBind();
                //绑定分页
                BindPage(this.SiteUserInfo.CompanyID,orderId,orderType,hotelName,customerName,orderStatus,orderStartTime,orderEndTime,checkInStartTime,checkInEndTime);
            }
            else
            {
                this.hol_crp_HotelOrderList.EmptyText = "<tr height=\"100px\"><td colspan=\"8\" style='text-align:center;'>暂无订单信息</td></tr>";
                this.hol_ExportPageInfo1.Visible = false;
            }
            //释放资源
            list = null;
        }
        #endregion       
        
        #region 设置分页
        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="orderId">订单号</param>
        /// <param name="orderType">订单类型</param>
        /// <param name="hotelName">酒店名称</param>
        /// <param name="customerName">客人姓名</param>
        /// <param name="orderStatus">订单状态</param>       
        /// <param name="orderStartTime">开始预定时间</param>
        /// <param name="orderEndTime">预定结束时间</param>
        /// <param name="checkInStartTime">入住开始时间</param>
        /// <param name="checkInEndTime">入住结束时间</param>
        protected void BindPage(string companyId, string orderId, string orderType, string hotelName, string customerName, string orderStatus, DateTime? orderStartTime, DateTime? orderEndTime, DateTime? checkInStartTime, DateTime? checkInEndTime)
        {
            this.hol_ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.hol_ExportPageInfo1.UrlParams.Add("orderId", Utils.InputText(Server.UrlDecode(Request.QueryString["orderId"] ?? "")));
            this.hol_ExportPageInfo1.UrlParams.Add("orderType", Utils.InputText(Server.UrlDecode(Request.QueryString["orderType"] ?? "")));
            this.hol_ExportPageInfo1.UrlParams.Add("hotelName", Utils.InputText(Server.UrlDecode(Request.QueryString["hotelName"] ?? "")));
            this.hol_ExportPageInfo1.UrlParams.Add("customerName", Utils.InputText(Server.UrlDecode(Request.QueryString["customerName"] ?? "")));
            this.hol_ExportPageInfo1.UrlParams.Add("orderStatus", Utils.InputText(Server.UrlDecode(Request.QueryString["orderStatus"] ?? "")));
            this.hol_ExportPageInfo1.UrlParams.Add("companyId",this.SiteUserInfo.CompanyID);
            this.hol_ExportPageInfo1.UrlParams.Add("orderStartTime", Utils.InputText(Server.UrlDecode(Request.QueryString["orderStartTime"] ?? null)));
            this.hol_ExportPageInfo1.UrlParams.Add("orderEndTime", Utils.InputText(Server.UrlDecode(Request.QueryString["orderEndTime"] ?? null)));
            this.hol_ExportPageInfo1.UrlParams.Add("checkInStartTime", Utils.InputText(Server.UrlDecode(Request.QueryString["checkInStartTime"] ?? null)));
            this.hol_ExportPageInfo1.UrlParams.Add("checkInEndTime", Utils.InputText(Server.UrlDecode(Request.QueryString["checkInEndTime"] ?? null)));
            this.hol_ExportPageInfo1.intPageSize = PageSize;
            this.hol_ExportPageInfo1.CurrencyPage = PageIndex;
            this.hol_ExportPageInfo1.intRecordCount = RecordCount;
        }
        #endregion

        #region 导出Excel文件
        /// <summary>
        /// 将酒店常旅客信息记录导出Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExcelClick()
        {
            string[] TableTitle = { "订单号", "订单状态", "审核状态", "旅客姓名", "酒店名称", "房型", "预定入住日期", "预定离店日期", "间夜数", "总价", "返佣比例", "返佣金额" };
            string ExcelName = string.Empty;
            PageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            string orderId = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderId"] ?? "")), "");//订单编号
            //string electtronOrderNum = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["electtronOrderNum"] ?? "")), null);//电子订单号
            string orderType = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderType"] ?? "")), "");//订单类型
            DateTime? orderStartTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["orderStartTime"] ?? null)), new System.Nullable<DateTime>());//开始预订日期
            DateTime? orderEndTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["orderEndTime"] ?? null)), new System.Nullable<DateTime>());//预订结束日期
            DateTime? checkInStartTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInStartTime"] ?? null)), new System.Nullable<DateTime>());//开始入住日期
            DateTime? checkInEndTime = Utils.GetDateTimeNullable(Utils.InputText(Server.UrlDecode(Request.QueryString["checkInEndTime"] ?? null)), new System.Nullable<DateTime>());//结束入住日期
            string hotelName = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["hotelName"] ?? "")), "");//酒店名称
            string orderStatus = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["orderStatus"] ?? "")), "");//订单状态
            string customerName = Utils.GetString(Utils.InputText(Server.UrlDecode(Request.QueryString["customerName"] ?? "")), "");//客人名称

            EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo = new EyouSoft.Model.HotelStructure.SearchOrderInfo();
            SearchInfo.ResOrderId = orderId;
            SearchInfo.HotelName = hotelName;
            if (orderStatus.ToString() != "" && orderStatus.ToString() != "ahol_ckAll")
            {
                SearchInfo.OrderState = (EyouSoft.Model.HotelStructure.OrderStateList)int.Parse(orderStatus);
            }
            SearchInfo.CustomerName = customerName;
            SearchInfo.CheckInSDate = checkInStartTime;
            SearchInfo.CheckInEDate = checkInEndTime;
            SearchInfo.CreateSDate = orderStartTime;
            SearchInfo.CreateEDate = orderEndTime;
            if (orderType.ToString() != "")
            {
                SearchInfo.OrderType = (EyouSoft.Model.HotelStructure.OrderType)int.Parse(orderType);
            }
            SearchInfo.CompanyId = this.SiteUserInfo.CompanyID;
            IList<EyouSoft.Model.HotelStructure.OrderInfo> list = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().GetList(PageSize, PageIndex, ref RecordCount, SearchInfo);
            if (list != null && list.Count > 0)
            {
                EduceExcel(list, ExcelName, TableTitle);
                list = null;
            }
            else
            {
                MessageBox.ResponseScript(this, "alert('没有订单信息 ');topTab.url(topTab.activeTabIndex,'/HotelCenter/HotelOrderManage/HotelOrderList.aspx');");
                return;
            }
        }
        #endregion

        #region 导出Excel
        /// <summary>   
        /// 导出Excel   
        /// </summary>   
        /// <param name="ds">Ilist数据源</param>   
        /// <param name="FileName">文件名</param>   
        /// <param name="biaotou">表头</param>   
        public void EduceExcel(IList<EyouSoft.Model.HotelStructure.OrderInfo> ds, string FileName, string[] tableTitle)
        {
            //清空页面所有内容输出
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;//设置缓冲输出     
            HttpContext.Current.Response.Charset = Encoding.UTF8.ToString();//设置输出流的HTTP字符集  
            //获取编码
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            //将Http头添加到输出流
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("酒店订单信息.xls", System.Text.Encoding.UTF8).ToString());
            //设置输出流Http类型
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Write(HTML(ds, tableTitle));
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Clear();

        }
        #endregion

        #region 使用Ilist数据源
        /// <summary>   
        /// 使用Ilist作为数据源   
        /// </summary>   
        /// <param name="ds"></param>   
        /// <param name="biaotou"></param>   
        /// <returns></returns>   
        private string HTML(IList<EyouSoft.Model.HotelStructure.OrderInfo> ds, string[] tableTitle)
        {
            StringBuilder ss = new StringBuilder();
            ss.Append("<table border=\"1\">");
            ss.Append("<tr>");           
            foreach (string str in tableTitle)
            {
                ss.Append("<td>&nbsp;" + str + "</td>");
            }
            ss.Append("</tr>");
            for (int i = 0; i < ds.Count;i++ )
            {
                ss.Append("<tr>");
                ss.Append("<td>&nbsp;" + ds[i].ResOrderId.ToString() + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].OrderState.ToString() + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].CheckState.ToString() + "</td>");
               
                IList<EyouSoft.HotelBI.HBEResGuestInfo> list = ds[i].ResGuests;
                string customerName = string.Empty;
                if (list != null && list.Count != 0)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        customerName = GetHotelVisitors(list);
                    }
                    ss.Append("<td>&nbsp;" + customerName.ToString() + "</td>");
                }
                else
                    ss.Append("<td>&nbsp;</td>");
                ss.Append("<td>&nbsp;" + ds[i].HotelName.ToString() + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].RoomTypeName.ToString() + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].CheckInDate.ToString("yyyy-MM-dd") + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].CheckOutDate.ToString("yyyy-MM-dd") + "</td>");
                ss.Append("<td>&nbsp;" + ds[i].RoomNight.ToString() + "</td>");
                ss.Append("<td>&nbsp;" + Utils.GetMoney(Convert.ToDecimal(Convert.ToString(ds[i].TotalAmount))) + "</td>");
                ss.Append("<td>&nbsp;" + Utils.GetMoney(ds[i].CommissionPercent * 100) + "%" + "</td>");
                ss.Append("<td>&nbsp;" + Utils.GetMoney(Convert.ToDecimal(ds[i].TotalCommission.ToString())) + "</td>");
                ss.Append("</tr>");
            }
            ss.Append("</table>");
            return ss.ToString();
        }
        #endregion      

        #region 获取旅客姓名
        protected string GetHotelVisitors(IList<EyouSoft.HotelBI.HBEResGuestInfo> ResGuests)
        {
            string CustomerName = "";
            IList<EyouSoft.HotelBI.HBEResGuestInfo> list = ResGuests;
            if (list != null && list.Count != 0)
            {
                for (int i = 0; i < list.Count;i++ )
                {
                    CustomerName = CustomerName + list[i].PersonName + ",";
                }
            }
            if (CustomerName.Length > 0)
                return CustomerName.Substring(0, CustomerName.LastIndexOf(","));
            else
                return CustomerName;
        }
        #endregion      
    }
}
