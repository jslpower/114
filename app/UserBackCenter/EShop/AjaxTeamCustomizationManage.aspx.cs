using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.EShop
{
    /// <summary>
    /// 高级网店-团队定制管理
    ///Create: luofx  Date:2010-11-11
    /// </summary>
    public partial class AjaxTeamCustomizationManage : EyouSoft.Common.Control.BasePage
    {
        private int intPageSize = 10;
        protected int intPageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "del")
            {
                Delete();
                return;
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            int intRecordCount = 0;
            string CompanyId = this.SiteUserInfo.CompanyID;
            this.rptTeamCustomization.DataSource = EyouSoft.BLL.ShopStructure.RouteTeamCustomization.CreateInstance().GetList(intPageSize, intPageIndex, ref intRecordCount, CompanyId);
            this.rptTeamCustomization.DataBind();
            if (rptTeamCustomization.Items.Count <= 0) {
                this.NoData.Visible = true;
            }
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intPageSize = intPageSize;            
            this.ExportPageInfo1.PageLinkURL = "#";

        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            int StrId = Utils.GetInt(Request.QueryString["id"]);
            if (StrId > 0 && this.SiteUserInfo != null)
            {
                bool IsTrue = EyouSoft.BLL.ShopStructure.RouteTeamCustomization.CreateInstance().Delete(StrId, this.SiteUserInfo.CompanyID);
                if (IsTrue)
                {
                    Response.Clear();
                    Response.Write("[{isSuccess:true,Message:'删除成功！'}]");
                    Response.End();
                }
            }

        }
        /// <summary>
        /// repeater绑定数据时，赋值序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptTeamCustomization_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //序号赋值
                Literal ltr = (Literal)e.Item.FindControl("ltrXH");
                if (ltr != null)
                    ltr.Text = Convert.ToString(intPageSize * (intPageIndex - 1) + (e.Item.ItemIndex + 1));
            }
        }
    }
}
