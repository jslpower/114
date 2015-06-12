using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using Newtonsoft.Json;
namespace UserPublicCenter.AirTickets.TeamBook
{   
    /// <summary>
    /// 页面功能:机票查询
    /// 开发人:xuty 开发时间:2010-10-21
    /// </summary>
    public partial class TicketSearch : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.Naviagtion = AirTicketNavigation.团队预定散拼;
            this.Title = "查询-组团预定/散拼-机票";
           

            
        }
    }
}
