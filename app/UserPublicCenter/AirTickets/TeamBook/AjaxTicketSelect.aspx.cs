using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using EyouSoft.BLL.TicketStructure;
using EyouSoft.Model.TicketStructure;
namespace UserPublicCenter.AirTickets.TeamBook
{   
    /// <summary>
    /// 页面功能:ajax获取机票查询结果
    /// 开发人：xuty 开发时间2010-10-22
    /// </summary>
    public partial class AjaxTicketSelect : EyouSoft.Common.Control.BasePage
    {
        protected string startCityName;//始发地名
        protected string toCityName;//目的地名
        protected string startDatestr;//出发日期
        protected string backDatestr;//返回日期
        protected TravellerType? peopleType;//旅客类型(内宾,外宾)
        protected FreightType airType;//航班类型(单程,来回程)
        protected int airTypestr;
        protected string startCity;//始发地三字码ID
        protected string toCity;//目的地三字码ID
        protected int startCityId;
        protected int toCityId;
        protected int pType;
        protected string imageServerUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                Response.Clear();
                Response.Write("你尚未登录");
                Response.End();
                return;
            }
            int m = 100;
            string y = Utils.GetMoney(m);
            airTypestr = Utils.GetInt(Utils.GetQueryStringValue("airType"),1);
            FreightType airType = (FreightType)airTypestr;
            startCity =Utils.GetQueryStringValue("startCity");
            toCity =Utils.GetQueryStringValue("toCity");
            int peopleNum =Utils.GetInt(Utils.GetQueryStringValue("peopleNum"),-1);
            startDatestr = Utils.GetQueryStringValue("startDate");
            backDatestr = Utils.GetQueryStringValue("backDate");
            DateTime startDate = Utils.GetDateTime(startDatestr);
            DateTime backDate = Utils.GetDateTime(backDatestr);
            string airCompany =Utils.GetQueryStringValue("airCompany");
            airCompany=Regex.Replace(airCompany, @"\(.*\)", "");
            int airId = -1;
            pType= Utils.GetInt(Utils.GetQueryStringValue("peopleType"),0);
            if (pType== 0)
            {
                peopleType = new Nullable<TravellerType>(TravellerType.内宾);
            }
            else if (pType == 1)
            {
                peopleType = new Nullable<TravellerType>(TravellerType.外宾);
            }
            startCityName = Server.UrlDecode((Request.QueryString["startCity1"] ?? ""));
            toCityName = Server.UrlDecode((Request.QueryString["toCity1"] ?? ""));
            //string[] sCityArr = startCity.Split('|');
            //string[] tCityArr = toCity.Split('|');
            //if (sCityArr.Length > 1 && tCityArr.Length > 1)
            //{
                startCityId = Utils.GetInt(startCity);
                toCityId = Utils.GetInt(toCity);
            //}
            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> airList=EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance().GetTicketFlightCompanyList(airCompany);
            if (airList != null && airList.Count == 1 && airList[0].AirportName == airCompany)
            {
                airId = airList[0].Id;
            }
            else
            {
                airId = -1;
            }
            

            QueryTicketFreightInfo queryInfo = new QueryTicketFreightInfo(null, null, startCityId, toCityId, peopleNum, startDate, backDate, airId, peopleType);
            EyouSoft.IBLL.TicketStructure.ITicketFreightInfo tfiBll = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance();

           
            imageServerUrl = ImageManage.GetImagerServerUrl(1);
           
            IList<EyouSoft.Model.TicketStructure.TicketFreightInfoList> tfilList=tfiBll.GetFreightInfoList(queryInfo);
            if (tfilList != null && tfilList.Count > 0)
            {
                acl_rptCustomerList.DataSource = tfilList;
                acl_rptCustomerList.DataBind();
            }
            else
            {
                acl_rptCustomerList.EmptyText = "<table><tr><td style='text-align:center'>暂无机票信息</td></tr><table>";
            }

        }
        protected string GetSuppliers(object FreightInfoList)
        {   
            StringBuilder builder = new StringBuilder();
            IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> ticketFIList = FreightInfoList as IList<EyouSoft.Model.TicketStructure.TicketFreightInfo>;
            foreach (EyouSoft.Model.TicketStructure.TicketFreightInfo info in ticketFIList)
            {
                builder.Append("<tr style=\"border-bottom:1px #dcd8d8 dashed;\"><td colspan=\"10\" align=\"center\" bgcolor=\"#FFFFFF\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                builder.AppendFormat("<tr><td width=\"18%\" {11} align=\"center\"><a href=\"javascript:void(0);\" onclick=\"return TicketSelect.getSupplierInfo('{0}',this);\"<strong>{17}</strong><div>({15})</div></td><th width=\"7%\" height=\"35\" align=\"center\">去程：</th>" +
                "<td width=\"7%\" align=\"center\">￥{1}</td>" +
                "<td width=\"10%\" align=\"center\">{2}%</td>" +
                "<td width=\"8%\" align=\"center\">￥{3}</td>" +
                "<td width=\"8%\" align=\"center\">{4}（人）</td>" +
                "<td width=\"12%\" align=\"center\">￥{5}/￥{6}</td>" +
                "<td width=\"5%\" {11} align=\"center\">{7} </td>" +
                "<td width=\"10%\" {11} align=\"center\"><a href=\"#\" title=\"{8}\" class=\"remark\">详情</a></td>" +
                "<td width=\"8%\" {11} align=\"center\">{9}</td>" +
                "<td {11} align=\"center\"><a href=\"TicketBook.aspx?id={10}&startDate={12}&backDate={13}&peopleType={14}&startCity={18}&toCity={19}&airType={20}\"><img src=\"{16}/images/jipiao/yuding.jpg\" width=\"65\" height=\"22\" /></a></td></tr>", info.Company.ID, Utils.GetMoney(info.FromReferPrice), Utils.GetMoney(info.FromReferRate), Utils.GetMoney(info.FromSetPrice), info.MaxPCount, Utils.GetMoney(info.FromFuelPrice), Utils.GetMoney(info.FromBuildPrice), info.ProductType, info.SupplierRemark, Utils.GetMQ(info.ContactMQ), info.Id, info.FreightType == FreightType.来回程 ? "rowspan=\"2\"" : "", startDatestr, backDatestr, Utils.GetQueryStringValue("peopleType"), GetDate(info), imageServerUrl, info.Company.CompanyName, startCity, toCity, airTypestr);
                if (info.FreightType == FreightType.来回程)
                {
                    builder.AppendFormat("<tr><th  height=\"35\" align=\"center\">回程：</th>" +
                "<td align=\"center\">￥{0}</td><td align=\"center\">{1}%</td>" +
                "<td align=\"center\">￥{2}</td><td align=\"center\">{3}（人）</td>" +
                "<td align=\"center\">￥{4}/￥{5}</td></tr>", Utils.GetMoney(info.ToReferPrice), Utils.GetMoney(info.ToReferRate), Utils.GetMoney(info.ToSetPrice), info.MaxPCount, Utils.GetMoney(info.ToFuelPrice), Utils.GetMoney(info.ToBuildPrice));
                }
                builder.Append("</table></td></tr>");
            }
            return builder.ToString();
        }
        /// <summary>
        /// 计算去程运价有效期
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected string GetDate(EyouSoft.Model.TicketStructure.TicketFreightInfo info)
        {
            if (info.FromSelfDate.HasValue)
                return info.FromSelfDate.Value.ToString("yyyy-MM-dd");
            else if (info.FromForDay != string.Empty)
                return info.FromForDay;
            else
            {
                string thedate = "";
                if (info.FreightStartDate.HasValue)
                    thedate = info.FreightStartDate.Value.ToString("yyyy-MM-dd");
                if( info.FreightEndDate.HasValue)
                    thedate+="至"+info.FreightEndDate.Value.ToString("yyyy-MM-dd");
                if (thedate == "")
                    thedate = "无有效期";
                return thedate;
            }
               
        }

       
        
    }
}
