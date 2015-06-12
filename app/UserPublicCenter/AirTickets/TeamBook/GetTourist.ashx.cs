using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserPublicCenter.AirTickets.TeamBook
{

    /// <summary>
    /// 页面功能：匹配姓名搜索相应旅客
    /// 开发人：xuty 开发时间：2010-10-21
    /// </summary>
    public class GetTourist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string companyId = Utils.GetQueryStringValue("companyid");
            string touristName = Utils.GetQueryStringValue("SeachKey");
            StringBuilder builder = new StringBuilder();
           
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> vistorList= EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetListByKeyWord(touristName,companyId);
            
            if (vistorList != null && vistorList.Count > 0)
           {   
               bool isLetter=IsLetter(touristName);
               if (isLetter)
               {
                   foreach (EyouSoft.Model.TicketStructure.TicketVistorInfo info in vistorList)
                   {
                       builder.AppendFormat("{0}~&&~{1}~&&~{2}~&&~{3}~&&~{4}\n", info.EnglishName, info.ContactSex.ToString(), info.VistorType.ToString(), info.CardType.ToString(), info.CardNo);
                   }
               }
               else
               {
                   foreach (EyouSoft.Model.TicketStructure.TicketVistorInfo info in vistorList)
                   {
                       builder.AppendFormat("{0}~&&~{1}~&&~{2}~&&~{3}~&&~{4}\n", info.ChinaName, info.ContactSex.ToString(), info.VistorType.ToString(), info.CardType.ToString(), info.CardNo);
                   }
               }
               
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
        /// <summary>
        /// 验证首字母是否为字母
        /// </summary>
        /// <param name="CheckString"></param>
        /// <returns></returns>
        private bool IsLetter(string CheckString)
        {
            bool IsTrue = false;
            if (!string.IsNullOrEmpty(CheckString))
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                IsTrue = rx.IsMatch(CheckString.Substring(0, 1));
                rx = null;
            }
            return !IsTrue;
        }
    }
}
