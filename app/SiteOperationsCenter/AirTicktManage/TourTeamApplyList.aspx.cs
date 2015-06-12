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
    /// 页面功能：运营后台---机票管理---团队票申请
    /// CreateDate:2011-05-17
    /// </summary>
    /// Author:liuym
    public partial class TourTeamApplyList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量
        /// <summary>
        /// 每页显示记录
        /// </summary>
        protected int PageSize =10;
        /// <summary>
        /// 页码
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        protected int RecordCount = 0;
        /// <summary>
        /// 删除编号
        /// </summary>
        protected string DeleteId = string.Empty;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 删除操作
            if (!string.IsNullOrEmpty(Request.QueryString["DeleteId"]))
            {
                DeleteId = Request.QueryString["DeleteId"];
            }
           if(!string.IsNullOrEmpty(DeleteId))
           {
               //调用底层删除操作方法
               bool result = EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().DeleteGroupTickets(int.Parse(DeleteId));
               if(result)
               {
                   MessageBox.ShowAndRedirect(this, "删除成功", "/AirTicktManage/TourTeamApplyList.aspx");
               }
               else
                   MessageBox.ShowAndRedirect(this, "删除失败", "/AirTicktManage/TourTeamApplyList.aspx");

           }
            #endregion

           if (!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.机票首页管理_团队票申请管理))
                {
                    Utils.ResponseNoPermit(YuYingPermission.机票首页管理_团队票申请管理, true);
                    return;
                }
                //初始化团队申请列表
                InitTourApplyList();
            }
        }
        #endregion

        #region 绑定团队申请列表
        private void InitTourApplyList()
        {
            PageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);

            IList<EyouSoft.Model.TicketStructure.GroupTickets> list = EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().GetGroupTickets(PageSize, PageIndex, ref RecordCount);
            if (list != null && list.Count > 0)
            {
                this.crp_TourTeamApplyList.DataSource = list;
                this.crp_TourTeamApplyList.DataBind();
                BindPage();
            }
            else
                this.crp_TourTeamApplyList.EmptyText = "暂时没有数据";

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
            this.ExportPageInfo1.PageLinkURL = "TourTeamApplyList.aspx?";
            this.ExportPageInfo1.LinkType = 3;               
        }
        #endregion

        #region 标头批量删除操作
        protected void a_Del_Click(object sender, EventArgs e)
        {
            string[] Ids = Utils.GetFormValues("ckId");
            if (Ids == null || Ids.Length == 0)
            {
                MessageBox.Show(this, "请选择您要删除的数据！");
                return;
            }
            //调用批量删除方法
            bool Result = EyouSoft.BLL.TicketStructure.GroupTickets.CreateInstance().DeleteGroupTickets(Utils.StringArrToIntArr(Ids));
            if (Result)
                MessageBox.ShowAndRedirect(this, "删除成功", "/AirTicktManage/TourTeamApplyList.aspx");
            else
                MessageBox.ShowAndRedirect(this, "删除失败", "/AirTicktManage/TourTeamApplyList.aspx");

        }
        #endregion
    }
}
