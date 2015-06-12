using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
namespace UserPublicCenter.HomeControl
{   
    /// <summary>
    /// 首页机票信息
    /// 刘飞 2012/2/24
    /// </summary>
    public partial class Tickets : System.Web.UI.UserControl
    {
        protected string ticketsHtml;//机票信息
        protected string ImageServerPath;
        protected int ticketCount;//特价机票数
        protected void Page_Load(object sender, EventArgs e)
        {
            EyouSoft.Common.Control.FrontPage thePage = (this.Page as EyouSoft.Common.Control.FrontPage);
            IsLogin = thePage.IsLogin;
            CityId = thePage.CityId;
            ImageServerPath = (Page as EyouSoft.Common.Control.FrontPage).ImageServerUrl;
            IList<EyouSoft.Model.TicketStructure.SpecialFares> ticketList= EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetTopSpecialFares(30);
            StringBuilder strBuilder = new StringBuilder();
            if (ticketList != null && ticketList.Count > 0)
            {
                ticketCount = ticketList.Count;
                foreach (EyouSoft.Model.TicketStructure.SpecialFares model in ticketList)
                {
                    strBuilder.AppendFormat("<li><span class=\"wenzilanse\">[{2}]</span> <a href=\"{0}\" title=\"{3}\" target=\"_blank\">{1}</a> </li>", 
                        model.IsJump ? (IsLogin ? "/PlaneInfo/PlaneListPage.aspx" : "/AirTickets/Login.aspx?return=PlaneListPage") : EyouSoft.Common.URLREWRITE.Plane.SpecialFaresUrl(model.ID, CityId), 
                        EyouSoft.Common.Utils.GetText(model.Title, 22, true), 
                        model.SpecialFaresType, 
                        model.Title); 
                }
            }
            ticketsHtml=strBuilder.ToString();
        }

        protected int CityId;
        protected bool IsLogin;

    }
}