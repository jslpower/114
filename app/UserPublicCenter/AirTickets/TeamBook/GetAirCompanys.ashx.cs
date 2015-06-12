using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserPublicCenter.AirTickets.TeamBook
{
   
    /// <summary>
    /// 返回航空公司
    /// 开发人：xuty 2010-11-02
    /// </summary>
    public class GetAirCompanys : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string companyName = Utils.GetQueryStringValue("seachKey");
            StringBuilder builder = new StringBuilder();
            EyouSoft.IBLL.TicketStructure.ITicketFlightCompany ticketCompanyBll = EyouSoft.BLL.TicketStructure.TicketFlightCompany.CreateInstance();
            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> list=ticketCompanyBll.GetTicketFlightCompanyList(companyName);
            
            foreach(EyouSoft.Model.TicketStructure.TicketFlightCompany companyInfo in list)
            {
                builder.Append(companyInfo.AirportName+"("+companyInfo.AirportCode+")"+"~&&~"+companyInfo.Id+"\n");
            }
            context.Response.Write(builder.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
