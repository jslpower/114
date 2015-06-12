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
using System.Collections.Generic;

namespace UserPublicCenter.AirTickets.VisitorManage
{
    /// <summary>
    /// 页面功能：机票常旅客——机票常旅客列表页
    /// 开发人：刘咏梅     
    /// 开发时间：2010-10-19
    /// </summary>
    public partial class AjaxVisitorList : EyouSoft.Common.Control.BasePage
    {
        #region 成员变量
        private int PageIndex = 1;   //当前页
        private int PageSize = 10;   //每页显示的记录数
        protected bool EditFlag = false; //修改权限
        protected bool DeleteFlag = false; //删除权限
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                //绑定常旅客列表数据
                InitVisitorList();

                //页面请求删除操作
                if (!string.IsNullOrEmpty(Utils.InputText(Context.Request.QueryString["DeleteId"])))
                {
                    //调用删除常旅客的方法
                    string deleteId = Utils.GetString(Utils.InputText(Context.Request.QueryString["DeleteId"]),"");
                    EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().DeleteTicketVisitorInfo(deleteId);
                    MessageBox.ShowAndRedirect(this, "删除成功", "VisitorList.aspx");
                }
               
            }
        }
        #endregion

        #region 绑定列表数据
        /// <summary>
        ///绑定列表数据
        /// </summary>
        private void InitVisitorList()
        {
            int RecordCount = 0;
            //获取数据集
            string companyId = this.SiteUserInfo.CompanyID;   //公司ID     
            string visitorName = Utils.InputText(Request.QueryString["Name"]);//旅客姓名
            string visitorType = Server.UrlDecode(Utils.InputText(Request.QueryString["Type"]));//旅客类型
            if (!string.IsNullOrEmpty(visitorName) || !string.IsNullOrEmpty(visitorType))
            {
                if (visitorName == "请输入姓名")
                    visitorName = "";
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "VisitorList.OnSearch();");
            }
            EyouSoft.Model.TicketStructure.TicketVistorType? Vtype = null;
            if (!string.IsNullOrEmpty(visitorType))
            {
                Vtype = (EyouSoft.Model.TicketStructure.TicketVistorType)EyouSoft.Model.TicketStructure.TicketVistorType.Parse(typeof(EyouSoft.Model.TicketStructure.TicketVistorType), visitorType);
            }
            IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> VisitorList = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetTicketVisitorList(companyId, visitorName, Vtype, PageSize, PageIndex, ref RecordCount);
            //判断集合是否为空 数量是否为零
            if (VisitorList != null && VisitorList.Count != 0)
            {
                //绑定分页控件               
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = RecordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.CurrencyPageCssClass = "RedFnt";
                this.ExporPageInfoSelect1.LinkType = 3;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "VisitorList.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "VisitorList.LoadData(this);", 0);
                //绑定数据源
                this.crpVisitorList.DataSource = VisitorList;
                this.crpVisitorList.DataBind();               

            }
            else
            {
                this.crpVisitorList.EmptyText = "抱歉，暂时没有数据！";
            }
            //释放资源 
            VisitorList = null;
        }
        #endregion
    }
}
