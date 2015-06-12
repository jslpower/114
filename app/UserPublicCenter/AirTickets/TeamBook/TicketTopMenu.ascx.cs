using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
namespace UserPublicCenter.AirTickets.TeamBook
{
    public partial class TicketTopMenu : System.Web.UI.UserControl
    {
        [Bindable(true)]
        public string TabIndex
        {
            set
            {
                tabIndex = value;
            }
            get
            {
                return tabIndex;
            }
        }
        private string tabIndex;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}