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
using System.Collections.Generic;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.Security.Membership;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：导入常用语言弹窗列表页
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// </summary>
    public partial class ImportCommonLanguage : EyouSoft.Common.Control.BasePage
    {
        #region 定义变量
        private static int intPageSize = 10;
        private int CurrencyPage = 1;
        private string companyid;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                UserProvider.RedirectLoginOpenTopPage("SendSMS.aspx");
            }
            //获取公司的编号
            companyid = this.SiteUserInfo.CompanyID;
            if (!Page.IsPostBack)
            {
                //绑定所有短语信息
                BindInfo();

            }
        }
        #endregion

        #region 绑定常用短语信息
        private void BindInfo()
        {
            if (Request.QueryString["IsSystem"] != null && Request.QueryString["IsSystem"].ToString() == "0")
            {
                rblTypes.SelectedValue = rblTypes.Items.FindByValue(Request.QueryString["IsSystem"].ToString()) != null ? Request.QueryString["IsSystem"].ToString() : "0";
            }
            else
            {
                companyid = "";
                rblTypes.SelectedValue = "1";
            }
            int intRecordCount = 0;
            CurrencyPage =Utils.GetInt(Request.QueryString["Page"],1);
            //获取所有短语信息列表
            IList<EyouSoft.Model.SMSStructure.TemplateInfo> listInfo = EyouSoft.BLL.SMSStructure.Template.CreateInstance().GetTemplates(intPageSize, CurrencyPage, ref intRecordCount, companyid, string.Empty, -1);//获取数据源

            if (listInfo!=null && listInfo.Count> 0)
            {
                this.NoDataInfo.Visible = false;
                //绑定短语信息
                Recommounse.DataSource = listInfo;
                Recommounse.DataBind();
                //绑定分页控件
                this.ExportPageInfo1.intPageSize = intPageSize;//每页显示记录数
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL =this.ExportPageInfo1.UrlParams.GetValues("IsSystem")==null?Request.ServerVariables["SCRIPT_NAME"].ToString() + "?IsSystem="+rblTypes.SelectedValue+"&":Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            }
            else
            {
                this.NoDataInfo.Visible = true;
                this.div_Expage.Visible = false;
            }
            listInfo = null;
        }
        #endregion

        #region 截取短信内容显示部分
        public string GetContent(string strContent)
        {
            return "<span title=\"" + strContent+ "\">" +Utils.GetText(strContent,40,true)+ "</span>";
        }
        #endregion

        #region 数据列表项绑定事件
        protected void Recommounse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //取得当前项的索引值加1,因为项的索引值是从0开始的.
            int intContentID = e.Item.ItemIndex + 1;
            if (e.Item.ItemIndex != -1)
            {
                System.Web.UI.WebControls.Label lblContentID = (Label)e.Item.FindControl("lblContentID");
                lblContentID.Text = Convert.ToString(((CurrencyPage - 1) * intPageSize) + intContentID);
            }
        }
        #endregion

        #region 切换数据源【自定义，系统默认】
        protected void rblTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("/SMSCenter/ImportCommonLanguage.aspx?IsSystem=" + rblTypes.SelectedValue + "&iframeId=" + Request.QueryString["iframeId"].ToString());
        }
        #endregion
    }
}
