using System;
using System.Collections;
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
using EyouSoft.Security.Membership;

namespace UserBackCenter.SupplyManage
{
    /// <summary>
    /// 供应商后台:资讯列表
    /// 罗伏先   2010-07-22
    /// </summary>
    public partial class NewsList : EyouSoft.Common.Control.BackPage
    {
        private int intPageSize = 15,intRecordCount = 0;
        private string CompanyID = string.Empty;
        protected int intPageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            CompanyID = this.SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.SupplierInfo model = EyouSoft.BLL.CompanyStructure.SupplierInfo.CreateInstance().GetModel(CompanyID);
            if (!model.State.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop))
            {
                Response.Clear();
                Response.Write("对不起，你没有权限操作供应商高级网店后台，如有需要请申请开通！");
                Response.End();
            }
            model = null;
            if (!string.IsNullOrEmpty(Request.QueryString["AfficheID"]))
            {
                if (Delete(Utils.GetInt(Request.QueryString["AfficheID"])))
                {
                    Response.Clear();
                    Response.Write("1");
                    Response.End();
                }
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
            EyouSoft.Model.CompanyStructure.CompanyAfficheType? ComAfficheType = null;
            CompanyID = this.SiteUserInfo.CompanyID;
            intPageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            if (this.SiteUserInfo.CompanyRole.RoleItems.Length > 0)
            {
                switch (this.SiteUserInfo.CompanyRole.RoleItems[0])
                {
                    case EyouSoft.Model.CompanyStructure.CompanyType.车队:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.车队新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.购物店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.购物点新闻;
                        break;

                    case EyouSoft.Model.CompanyStructure.CompanyType.景区:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.景区新闻;
                        break;

                    case EyouSoft.Model.CompanyStructure.CompanyType.酒店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.酒店新闻;
                        break;
                    case EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店:
                        ComAfficheType = EyouSoft.Model.CompanyStructure.CompanyAfficheType.旅游用品新闻;
                        break;
                }
                EyouSoft.IBLL.CompanyStructure.ICompanyAffiche Ibll = EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance();
                IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> AfficheList = Ibll.GetList(intPageSize, intPageIndex, ref intRecordCount, CompanyID, ComAfficheType, null, null, null);
                rptSupplyManage_NewsList.DataSource = AfficheList;
                rptSupplyManage_NewsList.DataBind();
                Ibll = null;
                AfficheList = null;               
                if (rptSupplyManage_NewsList.Items.Count < 1)
                {
                    NoData.Visible = true;
                }
            }
            this.ExportPageInfo1.intRecordCount = intRecordCount;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="AfficheID">新闻id</param>
        /// <returns></returns>
        private bool Delete(int AfficheID)
        {
           return EyouSoft.BLL.CompanyStructure.CompanyAffiche.CreateInstance().Delete(AfficheID);
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_NewsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
