using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTemplate.Engine;
using System.Text;

namespace WEB
{
    public partial class upload : System.Web.UI.Page
    {
        /// <summary>
        /// 行业新闻
        /// </summary>
        protected string TradeNewsList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //int RecordCount = 0;
            //TemplateDocument document = new TemplateDocument(Server.MapPath("page.html"), Encoding.UTF8);
            //IList<EyouSoft.Model.ToolStructure.Receivables> list = EyouSoft.BLL.ToolStructure.Receivables.CreateInstance().GetList(20, 1, ref RecordCount, 1, string.Empty, string.Empty, string.Empty,
            //    string.Empty, null, null, false, "983f0d2a-8255-4f9d-8727-7931dbe1007e");
            //document.Variables.SetValue("record", list.Count);
            //document.Variables.SetValue("list", list);
            //document.Variables.SetValue("sum", 0);
            //document.Render(Response.Output);
            if (!Page.IsPostBack)
            { 
               
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // TextBox2.Text = System.Web.HttpUtility.UrlDecode(TextBox1.Text, System.Text.Encoding.UTF8);

            EyouSoft.BLL.CommunityStructure.Remind bll_News = new EyouSoft.BLL.CommunityStructure.Remind();
            IList<EyouSoft.Model.CommunityStructure.Remind> TradeNews = bll_News.GetList();
            var c = from i in bll_News.GetList() orderby i.EventTime ascending select i;           
            //遍历
            StringBuilder strTrdeNewsList = new StringBuilder();
            if (TradeNews != null && TradeNews.Count > 0)
            {
                strTrdeNewsList.Append("<h3><span><a href='wwww' title='aaaa'>更多></a></span></h3><ul>");
                foreach (EyouSoft.Model.CommunityStructure.Remind item in c)
                {
                    strTrdeNewsList.AppendFormat("<li>{0}&nbsp;&nbsp;{1}&nbsp;&nbsp;{2}</li>", item.EventTime.ToString(), item.Operator, item.TypeLink);
              
                }
                strTrdeNewsList.Append("</ul>");
            }
            TradeNewsList = strTrdeNewsList.ToString();
        }
    }
}



//<OTRequest>
//        <TransactionName>TH_HotelAvailabilityCacheRQ</TransactionName>
//    <Header>
//        <SessionID></SessionID>
//        <Invoker></Invoker>
//        <Encoding></Encoding>
//        <Locale></Locale>
//        <SerialNo></SerialNo>
//        <TimeStamp>2010-12-28 12:00:00</TimeStamp>
//        <Application>availCache</Application>
//        <Language>CN</Language>
//    </Header>
//    <DestinationSystemCodes>
//        <UniqueID></UniqueID>
//    </DestinationSystemCodes>
//    <HotelAvailabilityCacheRQ>
//        <HotelAvailabilityCacheCriteria> 
//            <DateRange></DateRange>
//            <HotelCode>SOHOTO1222</HotelCode>
//            <RoomType></RoomType>
//            <Vendor></Vendor>
//            <RatePlan></RatePlan>
//</HotelAvailabilityCacheCriteria>
//    </HotelAvailabilityCacheRQ>
//    <IdentityInfo>
//        <OfficeID>ZJF114</OfficeID>
//        <UserID>ZJF11400C</UserID>
//        <Password>12345678</Password>
//        <Role></Role>
//    </IdentityInfo>
//    <Source>
//        <OfficeCode>ZJF114</OfficeCode>
//        <UniqueID></UniqueID>
//               <BookingChannel>HOTELBE</BookingChannel>
//    </Source>
//</OTRequest>
