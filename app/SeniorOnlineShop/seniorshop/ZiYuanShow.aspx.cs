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
    public partial class ZiYuanShow : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取旅游资源
            string id = Utils.InputText(Request.QueryString["ziyuanid"]);
            //string cid = Utils.InputText(Request.QueryString["cid"]);
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.Model.ShopStructure.HighShopResource reso = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetModel(id);
                if (reso != null)
                {
                    //旅游资源内容初始化
                    ltrTitle.Text = Utils.InputText( reso.Title);
                    //zxb update 20110413
                    ltrContent.Text = reso.ContentText; //Utils.InputText(  reso.ContentText);
                    ltrIssuetime.Text = reso.IssueTime.ToString("yyyy-MM-dd HH:mm");
                    //页面标题初始化
                    Page.Title = ltrTitle.Text + "_旅游资源推荐";
                }
                reso = null;             
            }
        }
    }
}
