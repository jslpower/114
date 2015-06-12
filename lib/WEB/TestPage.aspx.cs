using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.BLL.HotelStructure;
using EyouSoft.Model.HotelStructure;

namespace WEB
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var item = new EyouSoft.Model.TicketStructure.GroupTickets();
            //item.AirlinesType = EyouSoft.Model.TicketStructure.AirlinesType.CA国际航空;
            //item.Contact = "1111";
            //item.ContactQQ = "401111";
            //item.Email = "121184679@qq.com";
            //item.EndCity = "成都";
            //item.FlightNumber = "灰机11111";
            //item.GroupType = EyouSoft.Model.TicketStructure.GroupType.单程;
            //item.HopesPrice = 1;
            //item.IsChecked = true;
            //item.IssueTime = DateTime.Now;
            //item.Notes = "备注";
            //item.PelopeCount = 10;
            //item.Phone = "58417520";
            //item.RoundTripTime = "1111111";
            //item.StartCity = "杭州";
            //item.StartTime = "222222";
            //item.TimeRange = "00000000";
            //item.ID = 4;
            //bool result = EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().DeleteGroupTickets(4);
            //Response.Write(result.ToString());
            Test();
        }

        private void Test()
        {
            BNewHotel bll = new BNewHotel();
            IList<MNewHotelInfo> list = bll.GetList(EyouSoft.Model.HotelStructure.CityAreaType.港澳, "", 6);
        }
    }
}
