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
using EyouSoft.Common.Function;
namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 出游指南详细页
    /// </summary>
    /// 鲁功源 2010-11-12
    public partial class GuideBookInfo : EyouSoft.Common.Control.FrontPage
    {
        protected SeniorOnlineShop.master.T4 cMaster = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            cMaster = (SeniorOnlineShop.master.T4)this.Master;
            if (!IsPostBack)
            {
                if (cMaster == null)
                    return;
                #region 设置导航栏Tab索引
                cMaster.CTAB = master.T4TAB.出游指南;
                #endregion

                #region 初始化数据
                InitPageData();
                #endregion
            }
            Page.Title = lbTitle.InnerText + "_出游指南";
        }
        #region 初始化数据
        protected void InitPageData()
        {
            string KeyId=Utils.GetQueryStringValue("key");
            if(string.IsNullOrEmpty(KeyId))
                Utils.ShowAndRedirect("未能找到该条信息","/template4/GuideBooks.aspx");
            EyouSoft.Model.ShopStructure.HighShopTripGuide model = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetModel(KeyId);
            if(model==null)
                Utils.ShowAndRedirect("未能找到该条信息", "/template4/GuideBooks.aspx");
            lbTitle.InnerText = model.Title;
            sTypeName.InnerText = model.TypeID.ToString();
            lbAddTime.Text = model.IssueTime.ToString("yyyy-MM-dd");
            pContent.InnerHtml = model.ContentText;
            model = null;
        }
        #endregion
    }
}
