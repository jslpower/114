using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserPublicCenter.AirTickets.TeamBook
{   
    /// <summary>
    /// 页面功能：显示机票查询结果
    /// 开发人:xuty 开发时间：2010-10-21
    /// </summary>
    public partial class TicketSelect : EyouSoft.Common.Control.FrontPage
    {
        protected string startCityName;//始发地名
        protected string toCityName;//目的地名
        protected string airType;//航班类型(单程,来回程)
        protected string airCompany;//航空公司
        protected string startDate;//出发时间
        protected string backDate;//返回时间
        protected string peopleNum;//人数
        protected string startCity;//始发地三字码ID
        protected string toCity;//目的地三字码ID
        protected string peopleType;//旅客类型(内宾,外宾)
        protected string startCity1;
        protected string toCity1;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Naviagtion = AirTicketNavigation.团队预定散拼;
            string method = Utils.GetQueryStringValue("method");
            if (method != "getInfo")
            {
                startCity1 = Utils.GetQueryStringValue("startCity1");
                toCity1 = Utils.GetQueryStringValue("toCity1");
                startCityName = Server.UrlDecode(startCity1);
                toCityName = Server.UrlDecode(toCity1);
                airType = Utils.GetQueryStringValue("airType");
                airCompany = Utils.GetQueryStringValue("airCompany");
                startDate = Utils.GetQueryStringValue("startDate");
                backDate = Utils.GetQueryStringValue("backDate");
                peopleNum = Utils.GetQueryStringValue("peopleNum");
                startCity = Utils.GetQueryStringValue("startCity");
                toCity = Utils.GetQueryStringValue("toCity");
                peopleType = Utils.GetQueryStringValue("peopleType");
            }
            else
            {
                if (!IsLogin)
                {
                    Utils.ResponseMeg(false, "你尚未登录");
                    return;
                }
                string sId = Utils.GetQueryStringValue("sId");
                //获取供应商信息
                //EyouSoft.Model.CompanyStructure.CompanyAttachInfo attachIno=EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().GetModel(sId);
             
                EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo info =EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance().GetSupplierInfo(sId);
                if (info == null)
                    info = new EyouSoft.Model.TicketStructure.TicketWholesalersAllInfo();
                string bronze = Domain.FileSystem+info.AttachInfo.Bronze;
                string tradeAward = Domain.FileSystem+ info.AttachInfo.TradeAward;
                string certif = Domain.FileSystem + info.AttachInfo.BusinessCertif.LicenceImg;
                Response.Clear();
                Response.Write("{");
                Response.Write(string.Format("success:\"1\",message:\"1\",contactName:\"{0}\",tel:\"{1}\",wTime:\"{2}\",webSite:\"{3}\",lev:\"{4}\",successRate:\"{5}\",tTime:\"{6}\",cerImg:\"{7}\",bronze:\"{8}\",trade:\"{9}\"", info.ContactName, info.ContactTel, info.WorkStartTime + "-" + info.WorkEndTime, info.WebSite, info.ProxyLev, (info.SuccessRate*100).ToString("F2"), info.RefundAvgTime + "/" + info.NoRefundAvgTime, certif, bronze, tradeAward));
                Response.Write("}");
                Response.End();
            }
            
        }
    }
}
