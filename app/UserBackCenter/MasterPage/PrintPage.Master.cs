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

namespace UserBackCenter.MasterPage
{
    public partial class PrintPage : System.Web.UI.MasterPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnWordPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
