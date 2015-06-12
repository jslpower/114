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
    /// 页面功能：从客户列表中导入手机号码
    /// 开发人：杜桂云  开发时间：2010-08-03
    /// </summary>
    public partial class ImportCustomerList : EyouSoft.Common.Control.BasePage
    {
        #region 成员变量
        private static int intPageSize = 10;
        private int CurrencyPage = 1;
        protected string CompanyID = "0";
        protected int intRecordCount = 0;
        #endregion

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                UserProvider.RedirectLoginOpenTopPage("SendSMS.aspx");
            }
            CompanyID = this.SiteUserInfo.CompanyID;
            if (!this.IsPostBack)
            {
                InitCustomerType();
                InitCustomerList();
            }
        }
        #endregion

        #region 初始化客户分类
        /// <summary>
        /// 初始化客户分类
        /// </summary>
        private void InitCustomerType()
        {
            //2010-12-16修改
            //客户类型来源 枚举型
            Type CusType = typeof(EyouSoft.Model.CompanyStructure.CompanyType);
            foreach (string s in Enum.GetNames(CusType))
            {
                ListItem item = new ListItem(s, Enum.Format(CusType, Enum.Parse(CusType, s), "d").Trim());
                ddl_CustomerType.Items.Add(item);

            }
            ddl_CustomerType.Items.Insert(0, new ListItem("选择类型", "-1"));

            //2010-12-16被替换部分
            //IList<EyouSoft.Model.SMSStructure.CustomerCategoryInfo> Cuslist =EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetCategorys(CompanyID);
            //this.ddl_CustomerType.Items.Add(new ListItem("请选择类型", "-1"));
            //if (Cuslist != null && Cuslist.Count > 0)
            //{
            //    foreach (EyouSoft.Model.SMSStructure.CustomerCategoryInfo cModel in Cuslist)
            //    {
            //        this.ddl_CustomerType.Items.Add(new ListItem(cModel.CategoryName, cModel.CategoryId.ToString()));
            //    }
            //}
            //Cuslist = null;
        }
        #endregion

        #region 绑定数据列表
        /// <summary>
        /// 绑定数据列表
        /// </summary>
        private void InitCustomerList()
        {
            string CompanyName = "";
            int CusType = -1, pid = 0, cid = 0, t = 1;//客户类型，省份编号，城市编号，数据来源[我的客户，平台客户]

            //获取查询条件
            if (!string.IsNullOrEmpty(Request.QueryString["CompanyName"]))
            {
                CompanyName = Utils.InputText(Server.UrlDecode(Request.QueryString["CompanyName"]));
                this.txt_CompanyName.Text = CompanyName;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CusType"]))
            {
                CusType = int.Parse(Utils.InputText(Server.UrlDecode(Request.QueryString["CusType"])));
                this.ddl_CustomerType.Items.FindByValue(CusType.ToString()).Selected = true;
            }
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"],1);
            pid = Utils.GetInt(Request.QueryString["pid"], 0);
            cid = Utils.GetInt(Request.QueryString["cid"], 0);
            t = Utils.GetInt(Request.QueryString["t"], 1);
            typeList.SelectedValue = t.ToString();
            CityList1.SetCityId = cid;
            CityList1.SetProvinceId = pid;
            if (typeList.SelectedValue == "1" && base.IsTravelUser && !Utils.IsOpenHighShop(this.SiteUserInfo.CompanyID))
            {
                rptCustomerList.Visible = false;
                NoData.Visible = true;
                TdNoData.InnerHtml = "<a href=\"javascript:;\" onclick=\"window.parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide();parent.topTab.open('/generalshop/geneshopmainpage.aspx','同业网店申请',{desUrl:'/GeneralShop/ApplicationEShop.aspx'});return false;\">马上开通高级网店</a>，即刻享受十万组团社资料进行精准短信营销";
                return;
            }
            //DateTime d1 = DateTime.Now;
            IList<EyouSoft.Model.SMSStructure.CustomerInfo> modelList = EyouSoft.BLL.SMSStructure.Customer.CreateInstance().GetCustomers(intPageSize, CurrencyPage, ref intRecordCount, CompanyID, CompanyName, "", "", CusType, t == 1 ? EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.平台组团社客户 : EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.我的客户, pid, cid);
            //DateTime d2 = DateTime.Now;
            //Response.Write(d1.Hour + ":" + d1.Minute + ":" + d1.Second + "." + d1.Millisecond + "<br />");
            //Response.Write(d2.Hour + ":" + d2.Minute + ":" + d2.Second + "." + d2.Millisecond);
            //Response.End();
            //return;
            if (modelList != null && modelList.Count > 0)
            {
                this.NoData.Visible = false;
                this.rptCustomerList.DataSource = modelList;
                this.rptCustomerList.DataBind();
                //绑定分页控件
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = CurrencyPage;
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            }
            else
            {
                this.NoData.Visible = true;
                this.div_Expage.Visible = false;
            }
            modelList = null;
        }
        #endregion

        #region 查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string CompanyName =Utils.InputText(Request.Form["txt_CompanyName"].Trim());
            string CusType = Request.Form["ddl_CustomerType"].Trim();
            Response.Redirect(Request.ServerVariables["SCRIPT_NAME"].ToString() + "?iframeId=" + Request.QueryString["iframeID"] + "&CompanyName=" + Server.UrlEncode(CompanyName) + "&CusType=" + Server.UrlEncode(CusType) + "&pid=" + Utils.GetFormValue("CityList1$ddl_ProvinceList") + "&cid=" + Utils.GetFormValue("CityList1$ddl_CityList") + "&t="+typeList.SelectedValue+"&");
            Response.End();
            return;
        }
        #endregion

        #region 数据列表项绑定事件
        protected void rptCustomerList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //取得当前项的索引值加1,因为项的索引值是从0开始的.
            int intCustomerId = e.Item.ItemIndex + 1;
            if (e.Item.ItemIndex != -1)
            {
                System.Web.UI.WebControls.Label lblCustomerID = (Label)e.Item.FindControl("lblCustomerID");
                lblCustomerID.Text = Convert.ToString(((CurrencyPage - 1) * intPageSize) + intCustomerId);
            }
        }
        #endregion
       

    }
}
