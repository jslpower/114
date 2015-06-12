using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace UserPublicCenter.AirTickets
{
    public partial class AirPlaneInfo : EyouSoft.Common.Control.FrontPage
    {
        protected string fareTitle;
        protected string fareContent;
        protected string fareDate;
        protected string fareAuthor;
        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 3;
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("body"));
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("index2011"));
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("InformationStyle"));
            
            int id=Utils.GetInt(Request.QueryString["TicketId"]);
            EyouSoft.Model.TicketStructure.SpecialFares model=EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetSpecialFare(id);
            if (model != null)
            {
                fareTitle = model.Title;
                fareContent = model.ContentText;
                fareDate = model.AddTime != null ? model.AddTime.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
                fareAuthor = model.Contact;
            }
        }
    }
}
