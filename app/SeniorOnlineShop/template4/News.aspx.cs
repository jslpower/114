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

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 页面功能：最新动态详细页
    /// 开发时间：2010-11-16  开发人：罗伏先
    /// </summary>
    public partial class News : EyouSoft.Common.Control.FrontPage
    {
        protected string CompanyId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            CompanyId = this.Master.CompanyId;
            //获取新闻
            string id = Utils.InputText(Request.QueryString["newid"]);
            EyouSoft.Model.ShopStructure.HighShopNews newModel = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetModel(id);
            if (newModel != null)
            {
                //页面标题初始化
                Page.Title =Utils.InputText( newModel.Title);
                //新闻内容初始化
                ltrTitle.Text =Utils.InputText(  newModel.Title);
                ltrContent.Text =Utils.InputText( newModel.ContentText);
                ltrIssuetime.Text =newModel.IssueTime.ToString("yyyy-MM-dd HH:mm");
            }
            newModel = null;
            
        }
    }
}
