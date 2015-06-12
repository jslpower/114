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
using EyouSoft.Common;

namespace UserPublicCenter
{
    public partial class ErrorPage : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string msg = Server.UrlDecode(Utils.InputText(Request.QueryString["msg"]));
            this.lblErrorText.Text = msg;

            string type = Utils.InputText(Request.QueryString["type"]);

            if (type.ToLower() == "hotel")
            {
                ltrCustom.Text = "或者<a href='/HotelManage/Default.aspx'>返回酒店首页</a>";
            }
        }
    }
}
