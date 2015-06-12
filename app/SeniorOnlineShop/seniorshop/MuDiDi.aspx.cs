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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop.seniorshop
{
    public partial class MuDiDi :FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //获取出游指南
            string id = Utils.InputText(Request.QueryString["mudidiid"]);
            //string cid=Utils.InputText(Request.QueryString["cid"]);
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide guide = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetModel(id);
                if (guide != null)
                {
                    ltrTitle.Text = Utils.InputText( guide.Title);
                    ltrContent.Text = guide.ContentText;
                    ltrIssuetime.Text = guide.IssueTime.ToString("yyyy-MM-dd HH:mm");

                    //出游指南类型
                    ltrTripTypeName.Text = guide.TypeID.ToString();

                    Page.Title = ltrTitle.Text + "_出游指南";
                }
                guide = null;
            }
        }
    }
}
