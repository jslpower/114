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

namespace SeniorOnlineShop.seniorshop
{
    /// <summary>
    /// 页面功能：最新动态详细页 
    /// 开发时间：2010-07-13  开发人：杜桂云
    /// 页面添加功能：同业动态详细页
    /// 李晓欢 2011-12-14
    /// </summary>
    public partial class New : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取新闻
            string id = Utils.InputText(Request.QueryString["newid"]);
            if (id != null)
            {
                this.litPageTitle.Text = "最新动态";
                EyouSoft.Model.ShopStructure.HighShopNews newModel = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetModel(id);
                if (newModel != null)
                {
                    //页面标题初始化
                    Page.Title = Utils.InputText(newModel.Title) + "_最新动态";
                    //新闻内容初始化
                    ltrTitle.Text = Utils.InputText(newModel.Title);
                    ltrContent.Text = Utils.InputText(newModel.ContentText);
                    ltrIssuetime.Text = newModel.IssueTime.ToString("yyyy-MM-dd HH:mm");
                }
                newModel = null;
            }
            //同业公告id
            string noticeid = Utils.GetQueryStringValue("noticeid");
            if (noticeid != null && !string.IsNullOrEmpty(noticeid))
            {
                this.litPageTitle.Text = "同业公告";

            }
           
        }
    }
}
