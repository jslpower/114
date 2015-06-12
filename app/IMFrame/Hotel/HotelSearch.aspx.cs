using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace IMFrame.Hotel
{
    public partial class HotelSearch : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string MQ = "";
        protected string CityId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                MQ = Utils.GetMQ(SiteUserInfo.ContactInfo.MQ);
                CityId = SiteUserInfo.CityId.ToString();
                
            }
        }
    }
}
 