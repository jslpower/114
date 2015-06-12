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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using EyouSoft.HotelBI;
using EyouSoft.Common;

namespace UserBackCenter.HotelCenter.HotelOrderManage
{
    /// <summary>
    /// 页面功能；酒店管理--常旅客管理
    /// Author:liuym
    /// CreateTime:2010-12-06
    /// </summary>
    public partial class HotelVisitorManage : BackPage
    {
        #region Private Mebers
        protected int PageSize = 10;//每页显示的记录
        protected string EditId = string.Empty;//修改ID
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!Page.IsPostBack)
            {
                //权限判断
                if (this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商) && this.SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    Response.Clear();
                    Response.Write("对不起，您没有权限");
                    Response.End();
                    return;
                }
                //绑定常旅客列表
                BindHotelVisitors();
                //页面请求删除操作
                if (!string.IsNullOrEmpty((Context.Request.QueryString["DeleteId"])))
                {
                    //调用删除常旅客的方法
                    string deleteId = Context.Request.QueryString["DeleteId"];
                    if (deleteId != "")
                    {
                        EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().DeleteTicketVisitorInfo(deleteId);
                        MessageBox.ResponseScript(this, "alert('删除成功 ');topTab.url(topTab.activeTabIndex,'/HotelCenter/HotelOrderManage/HotelVisitorManage.aspx');");
                        return;
                    }
                }
            }
        }
        #endregion

        #region 绑定常旅客列表
        protected void BindHotelVisitors()
        {
            int RecordCount = 0;//总记录数
            int PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1); ;//页码
            string companyId = this.SiteUserInfo.CompanyID;//公司ID
            // 获取常旅客集合
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> VisitorList = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetHotelVistorList(companyId, "", null, PageSize, PageIndex, ref RecordCount);
            if (VisitorList != null && VisitorList.Count >0)
            {
                //绑定Repeater控件
                this.hvm_crp_HotelVisitorList.DataSource = VisitorList;
                this.hvm_crp_HotelVisitorList.DataBind();
               // 绑定分页控件 
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo1.intPageSize = PageSize;
                this.ExportPageInfo1.CurrencyPage = PageIndex;
                this.ExportPageInfo1.intRecordCount = RecordCount;
            }
            else
            {
                this.hvm_crp_HotelVisitorList.EmptyText = "<tr height=\"100px\"><td colspan=\"8\" style='text-align:center;'>暂无旅客信息</td></tr>";
                this.ExportPageInfo1.Visible = false;
                trPage.Visible = false;
            }
            //释放资源
            VisitorList = null;
        }
        #endregion

        #region 证件类型
        /// <summary>
        /// 转换证件类型
        /// </summary>
        /// <param name="cardType">证件类型：为None时，返回空；否则返回证件类型</param>
        /// <returns>空字符串</returns>
        protected string GetCardType(string cardType)
        {
            string CardType = cardType == "None" ? "" : cardType;
            return CardType;
        }
        #endregion
    }
}
