using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
namespace UserPublicCenter.SupplierInfo.Ashx
{
    public abstract class PageProxy :System.Web.UI.Page
    {
        /// <summary>
        /// 输出用户控件的Html片段
        /// </summary>
        /// <param name="control">控件的相对路径</param>
        /// <returns></returns>
        public static string RenderUserControl(string control)
        {
            Page page = new Page();
            UserPublicCenter.WebControl.PageHead ctl1 = new UserPublicCenter.WebControl.PageHead();         
            System.Web.UI.UserControl ctl = (System.Web.UI.UserControl)page.LoadControl("~/" + control);
            page.Controls.Add(ctl);
            StringWriter writer = new StringWriter();
            HttpContext.Current.Server.Execute(page, writer, false);
            return writer.ToString();
        }
        public enum st
        { 
            eyou
        }
    }
}
