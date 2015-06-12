using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.HotelHomePageManage
{
    /// <summary>
    /// 页面功能：运营后台--酒店首页管理--国内酒店/港澳酒店/海外酒店管理列表
    /// 创建时间：2011-05-13
    /// </summary>
    /// Author:刘咏梅
    public partial class HotelManageList : EyouSoft.Common.Control.YunYingPage
    {
        #region 成员变量      
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        protected int RecordCount = 0;      
        /// <summary>
        /// 删除Id
        /// </summary>
        protected string DeleteId = string.Empty;
        #endregion

        #region 初始化页面
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["DeleteId"]))
            {
                DeleteId = Request.QueryString["DeleteId"];
            }
            
            if(!string.IsNullOrEmpty(DeleteId))
            {
               bool result=EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().Delete(int.Parse(DeleteId));
               if (result)
                   MessageBox.ShowAndRedirect(this, "删除成功", "/HotelHomePageManage/HotelManageList.aspx");
                else
                   MessageBox.ShowAndRedirect(this, "删除失败", "/HotelHomePageManage/HotelManageList.aspx");
            }
            
            if(!IsPostBack)
            {
                if (!CheckMasterGrant(YuYingPermission.酒店首页管理_特价酒店))
                {
                    Utils.ResponseNoPermit(YuYingPermission.酒店首页管理_特价酒店, true);
                    return;
                }
                //初始化酒店信息
                BindHotelList();
            }
        }
        #endregion

        #region 根据酒店类型绑定酒店列表
        protected void BindHotelList()
        {
            PageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            IList<EyouSoft.Model.HotelStructure.MNewHotelInfo> HotelList = EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().
                GetList(PageSize, PageIndex, ref RecordCount);
            if (HotelList != null && HotelList.Count > 0)
            {
                this.crp_HotelList.DataSource = HotelList;
                this.crp_HotelList.DataBind();
                BindPage();
            }
            else
                this.crp_HotelList.EmptyText = "暂时没有数据！";

            HotelList = null;
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
            this.ExportPageInfo1.PageLinkURL = "HotelManageList.aspx?";
            this.ExportPageInfo1.LinkType = 3;
        }
        #endregion


        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void a_Del_Click(object sender, EventArgs e)
        {
            string[] Ids = Utils.GetFormValues("ckId");
            if (Ids == null || Ids.Length == 0)
            {
                MessageBox.Show(this, "请选择您要删除的数据！");
                return;
            }
            bool Result=EyouSoft.BLL.HotelStructure.BNewHotel.CreateInstance().Delete(Utils.StringArrToIntArr(Ids));
            if (Result)
                MessageBox.ShowAndRedirect(this, "删除成功", "/HotelHomePageManage/HotelManageList.aspx");
            else
                MessageBox.ShowAndRedirect(this, "删除失败", "/HotelHomePageManage/HotelManageList.aspx");

        }
        #endregion
    }
}
