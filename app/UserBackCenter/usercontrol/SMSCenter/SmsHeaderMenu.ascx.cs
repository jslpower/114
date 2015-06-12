using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.ComponentModel;
using System.Xml.Linq;

namespace UserBackCenter.usercontrol.SMSCenter
{
    public partial class SmsHeaderMenu : System.Web.UI.UserControl
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