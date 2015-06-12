using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace UserBackCenter.TicketsCenter.FreightManage
{
    public partial class AjaxMainte : EyouSoft.Common.Control.BasePage
    {
        #region 分页变量
        protected int pageSize = 15;
        public int pageIndex = 1;
        protected int recordCount;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                Response.Clear();
                Response.Write("Login");
                Response.End();
            }
            if (!IsPostBack)
            {
                //航空公司Id
                int airCompanyId = Utils.GetInt(Utils.GetQueryStringValue("aId"));
                //开始地址Id
                int beginId = Utils.GetInt(Utils.GetQueryStringValue("startId"));
                //结束地址的Id
                int endId = Utils.GetInt(Utils.GetQueryStringValue("endId"));
                //排序值
                int orderIndex = Utils.GetInt(Utils.GetQueryStringValue("orderIndex"));
                //设置当前页
                pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);

                DataInit(airCompanyId, beginId, endId, orderIndex);
            }

        }

        protected void DataInit(int airComPanyId, int beginId, int endId, int orderIndex)
        {
            EyouSoft.Model.TicketStructure.QueryTicketFreightInfo model = new EyouSoft.Model.TicketStructure.QueryTicketFreightInfo(SiteUserInfo.CompanyID, null, beginId, endId, 0, null, null, airComPanyId, null);

            IList<EyouSoft.Model.TicketStructure.TicketFreightInfo> list = EyouSoft.BLL.TicketStructure.TicketFreightInfo.CreateInstance().GetList(pageSize, pageIndex, ref recordCount, orderIndex, model);
            this.mai_rptList.DataSource = list;
            this.mai_rptList.DataBind();
            BindPage();
            if (list != null && list.Count == 0)
            {
                this.lblMsg.Text = "未找到相关数据";
                this.ExportPageInfo1.Visible = false;
            }
            list = null;
        }

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
            this.ExportPageInfo1.PageLinkURL = "#/FreightManage/AjaxMainte.aspx" + "?";
        }
        #endregion


        #region 运价状态
        /// <summary>
        /// 显示运价状态
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="isExpired"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected string FreTypeInit(int isEnabled, int isExpired, string Id,string FreightBuyId)
        {
            if (isExpired == 1)
            {
                return "过期关闭";
            }
            if (isEnabled == 1)
            {
                //return "<a href='javascript:void(0);' ref=\"0\" onclick=\"ThisPage.UpdateFreEn('" + Id + "',this,'" + FreightBuyId + "')\" title='点击关闭'>启用</a>";
                return "<a>启用</a>";
            }
            else
            {
                //return "<a href='javascript:void(0);' ref=\"1\" onclick=\"ThisPage.UpdateFreEn('" + Id + "',this,'" + FreightBuyId + "')\" title='点击启用'>关闭</a>";
                return "<a>关闭</a>";
            }

        }
        #endregion

        #region  显示购买运价的类型
        /// <summary>
        /// 运价购买类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string PackageType(int type)
        {
            switch (type)
            {
                case 1: return "常规购买";
                case 2: return "套餐购买";
                default: return "促销购买";
            }
        }
        #endregion 
    }
}
