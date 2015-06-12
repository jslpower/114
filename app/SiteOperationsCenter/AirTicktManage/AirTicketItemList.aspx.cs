using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.AirTicktManage
{
    /// <summary>
    /// 页面功能：运营后台--机票管理---特价/免票/K位管理
    /// BuildDate:2011-05-17
    /// </summary>
    /// Author:liuym
    public partial class AirTicketItemList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        protected int PageSize = 20;
        protected int PageIndex = 1;
        protected int RecordCount = 0;
        protected string DeleteId = string.Empty;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["DeleteId"]))
            {
                DeleteId = Request.QueryString["DeleteId"];
            }
            if (!string.IsNullOrEmpty(DeleteId))
            {
                //调用删除操作
                bool result = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().DeleteSpecialFares(int.Parse(DeleteId));
                if (result)
                    MessageBox.ShowAndRedirect(this, "删除成功", "/AirTicktManage/AirTicketItemList.aspx");
                else
                    MessageBox.ShowAndRedirect(this, "删除失败", "/AirTicktManage/AirTicketItemList.aspx");
            }

            if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.机票首页管理_特价机票管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.机票首页管理_特价机票管理, true);
                    return;
                }
                BindItemList();
            }
        }
        #endregion

        #region 绑定列表
        private void BindItemList()
        {
            PageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);

            IList<EyouSoft.Model.TicketStructure.SpecialFares> list = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().GetSpecialFares(PageSize, PageIndex, ref RecordCount);
            if (list != null && list.Count > 0)
            {
                this.crp_AirTicketList.DataSource = list;
                this.crp_AirTicketList.DataBind();
                BindPage();
            }
            else
                this.crp_AirTicketList.EmptyText = "暂时没有数据";
            list = null;
        }
        #endregion

        #region 绑定分页
        private void BindPage()
        {

            this.ExportPageInfo1.intPageSize = PageSize;
            this.ExportPageInfo1.CurrencyPage = PageIndex;
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.PageLinkURL = "AirTicketItemList.aspx?";
            this.ExportPageInfo1.LinkType = 3;
        }
        #endregion

        #region 删除
        protected void a_Del_Click(object sender, EventArgs e)
        {
            string[] Ids = Utils.GetFormValues("ckId");
            if (Ids == null || Ids.Length == 0)
            {
                MessageBox.Show(this, "请选择您要删除的数据！");
                return;
            }
            //调用删除操作
            bool Result = EyouSoft.BLL.TicketStructure.SpecialFares.CreateInstance().DeleteSpecialFares(Utils.StringArrToIntArr(Ids));
            if (Result)
                MessageBox.ShowAndRedirect(this, "删除成功", "/AirTicktManage/AirTicketItemList.aspx");
            else
                MessageBox.ShowAndRedirect(this, "删除失败", "/AirTicktManage/AirTicketItemList.aspx");

        }
        #endregion
    }
}
