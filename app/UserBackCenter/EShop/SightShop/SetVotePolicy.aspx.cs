using System;
using System.Collections.Generic;
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

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 门票政策设置页面
    /// </summary>
    /// 鲁功源 2010-12-13
    public partial class SetVotePolicy : EyouSoft.Common.Control.BasePage
    {
        //门票政策
        private string staticHTML = "";

        #region 页面初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            if (!IsPostBack)
            {
                InitData();
            }
        }
        #endregion

        #region 初始化门票政策
        private void InitData()
        {
            this.staticHTML = this.hidDefaultHTML.Value;
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> list = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(1, SiteUserInfo.CompanyID, 14, string.Empty);
            if (list != null && list.Count > 0 && !string.IsNullOrEmpty(list[0].ContentText))
            {
                hId.Value = list[0].ID;
                staticHTML = list[0].ContentText;
            }
            editGuid.Value = staticHTML;
            list = null;
        }
        #endregion

        protected void Submit1_Click(object sender, EventArgs e)
        {
            bool Result = false;
            EyouSoft.Model.ShopStructure.HighShopTripGuide model = new EyouSoft.Model.ShopStructure.HighShopTripGuide();
            model.CompanyID = SiteUserInfo.CompanyID;
            model.ContentText = editGuid.Value;
            model.ImagePath = "";
            model.IssueTime = DateTime.Now;
            model.OperatorID = SiteUserInfo.ID;
            model.Title = "";
            model.TypeID = EyouSoft.Model.ShopStructure.HighShopTripGuide.TripGuideType.门票政策;
            if (!string.IsNullOrEmpty(hId.Value))
            {
                model.ID = hId.Value;
                Result = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().Update(model);
            }
            else
            {
                Result = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().Add(model);
            }
            model = null;
            if (Result)
                MessageBox.ResponseScript(this.Page, "alert(\"操作成功！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();");
            else
                MessageBox.ShowAndRedirect(this.Page, "alert(\"操作失败！\")", Request.RawUrl);
        }

    }
}
