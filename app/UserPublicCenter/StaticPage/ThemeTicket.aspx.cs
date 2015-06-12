using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.StaticPage
{
    public partial class ThemeTicket : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("index2011"));
        }
    }
}
