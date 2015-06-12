using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Net;

namespace SiteOperationsCenter.SupplierManage
{
    public partial class QInformationManager : EyouSoft.Common.Control.YunYingPage
    {
        #region Attributes
        protected int intPageSize = 15, CurrencyPage = 1;
        protected int intRecordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckMasterGrant(YuYingPermission.供求信息_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.供求信息_管理该栏目, true);
                return;
            }
            if (!IsPostBack)
            {
                //标题
                string title = Utils.GetQueryStringValue("KeyWord");
                if (title != "" && title != null)
                {
                    this.txtKeyWord.Value = title;
                }
                //供求信息编号
                string messageId = Utils.GetQueryStringValue("messageID");
                if (messageId != "" && messageId != null)
                {
                    this.txtinforID.Value = messageId;
                }
                BindInitPage();        
            }

        }

        #region 初始化信息
        protected void BindInitPage()
        {
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            EyouSoft.Model.CommunityStructure.SearchInfo Searchinfo = new EyouSoft.Model.CommunityStructure.SearchInfo();
            Searchinfo.ExchangeTitle = Utils.GetQueryStringValue("KeyWord");
            Searchinfo.OfferId = Utils.GetQueryStringValue("messageID");
            Searchinfo.ExchangeCategory = EyouSoft.Model.CommunityStructure.ExchangeCategory.QGroup;
            System.Collections.Generic.IList<EyouSoft.Model.CommunityStructure.ExchangeList> List = new EyouSoft.BLL.CommunityStructure.ExchangeList().GetList(intPageSize, CurrencyPage, ref  intRecordCount, Searchinfo);
            if (List != null && List.Count > 0)
            {
                this.rptSupplierList.DataSource = List;
                this.rptSupplierList.DataBind();
                //绑定分页控件
                this.ExportPageInfo.intPageSize = intPageSize;
                this.ExportPageInfo.intRecordCount = intRecordCount;
                this.ExportPageInfo.CurrencyPage = CurrencyPage;
                this.ExportPageInfo.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo.UrlParams = Request.QueryString;
                this.ExportPageInfo.PageLinkURL = "/SupplierManage/QInformationManager.aspx?";
                this.ExportPageInfo.LinkType = 3;
            }
            else
            {
                this.trNoData.Visible = true;
                this.ExportPageInfo.Visible = false;
            }

            if (List != null) List.Clear();
            List = null;
        }
        #endregion

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            bool Result = true;
            string[] ExchangeIds = Utils.GetFormValues("ckExchangID");
            if (ExchangeIds == null || ExchangeIds.Length == 0)
            {
                MessageBox.Show(this.Page, "请选择您要删除的项！");
                return;
            }
            foreach (string eid in ExchangeIds)
            {
                Result = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().Delete(eid);
            }
            if (Result)
            {
                MessageBox.ShowAndRedirect(this.Page, "删除成功！", Request.RawUrl);
            }
            else
            {
                MessageBox.ShowAndRedirect(this.Page, "删除失败！", Request.RawUrl);
            }
        }

        /// <summary>
        /// 命令行事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CommandName) || string.IsNullOrEmpty(e.CommandArgument.ToString()))
                return;          
            switch (e.CommandName.ToLower())
            {
                case "del":
                    if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().Delete(e.CommandArgument.ToString()))
                    {
                        MessageBox.ShowAndRedirect(this.Page, "删除成功！", Request.RawUrl);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "删除失败！", Request.RawUrl);
                    }
                    break;
                case "edit":
                    EyouSoft.Model.CommunityStructure.ExchangeList exchange = new EyouSoft.Model.CommunityStructure.ExchangeList();
                    System.Web.UI.WebControls.TextBox title = (System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtTitle");
                    if (EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().QG_SetQQGroupOfferTitle(e.CommandArgument.ToString(), title.Text))
                    {
                        MessageBox.ShowAndRedirect(this.Page, "保存成功！", Request.RawUrl);
                    }
                    else
                    {
                        MessageBox.ShowAndRedirect(this.Page, "保存失败！", Request.RawUrl);
                    }
                    break;
            }
        }

        /// <summary>
        /// 行绑定时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RepeaterList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Button btnDel = (Button)e.Item.FindControl("btnDel");
                if (btnDel != null)
                {
                    btnDel.Attributes.Add("onclick","return confirm('您确定要删除此条信息吗？');");
                }

            }
        }
    }
}
