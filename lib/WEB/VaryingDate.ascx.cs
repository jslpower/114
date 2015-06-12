using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB
{
    public class asdf
    {
        public const string a = "sessionid";
    }
    [PartialCaching(120, "none", null, asdf.a)]
    public partial class VaryingDate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (lstMode.SelectedIndex)
            {
                case 0:
                    TimeMsg.Font.Size = FontUnit.Large;
                    break;
                case 1:
                    TimeMsg.Font.Size = FontUnit.Small;
                    break;
                case 2:
                    TimeMsg.Font.Size = FontUnit.Medium;
                    break;
            }
            TimeMsg.Text = DateTime.Now.ToString("F");
        }
    }
}