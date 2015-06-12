using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;

namespace UserBackCenter.RouteAgency
{
    public partial class CalendarIframePage : BasePage
    {

        protected string Key = string.Empty;
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected string AreaType = EyouSoft.Common.Utils.GetQueryStringValue("areaType");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Key = Guid.NewGuid().ToString();
            }
        }
    }
}
