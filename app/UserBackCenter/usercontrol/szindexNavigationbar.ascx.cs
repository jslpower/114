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
using System.Xml.Linq;
using System.ComponentModel;
using EyouSoft.Common;
namespace Demo1.usercontrol
{
    public partial class szindexNavigationbar : System.Web.UI.UserControl
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