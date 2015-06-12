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
using EyouSoft.Common.Control;
using EyouSoft.Common.Function;
using EyouSoft.Common;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：ajax获取SMS客户信息列表
    /// 开发人：xuty 开发时间：2010-08-03
    /// </summary>
    public partial class AjaxCustomerList : EyouSoft.Common.Control.BasePage
    {
        protected int pageIndex;
        protected int pageSize = 10;
        protected int recordCount;
        EyouSoft.IBLL.SMSStructure.ICustomer customerBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            customerBll = EyouSoft.BLL.SMSStructure.Customer.CreateInstance();
            string method = Utils.GetFormValue("method");
            if (method == "delete")
            {
                if (!IsCompanyCheck)//是否审核通过
                {
                    Utils.ResponseMeg(false, "");
                    return;
                }
                DelCustomer();//删除客户
            }
            GetCustometList();
        }

        #region 删除客户
        protected void DelCustomer()
        {
            string custids = Utils.GetFormValue("custids");//获取要删除的客户的Id集合
            custids = custids.TrimEnd(',');
            string[] custArray = custids.Split(',');
            foreach (string custId in custArray)
            {
                customerBll.DeleteCustomer(custId);//执行删除操作
            }
        }
        #endregion

        #region 获取客户信息列表
        protected void GetCustometList()
        {   
            //查询参数
            pageIndex = Utils.GetInt(Request.Form["Page"], 1);
            string companyName = Utils.InputText(Server.UrlDecode(Request.Form["companyname"] ?? "")).Trim();
            string userName = Utils.InputText(Server.UrlDecode(Request.Form["username"] ?? "")).Trim();
            string moible = Utils.InputText(Server.UrlDecode(Request.Form["moible"] ?? "")).Trim();
            int userClass=Utils.GetInt(Server.UrlDecode(Request.Form["userclass"]??""),-1);
            //省份|城市
            int provinceId = Utils.GetInt(Request.Form["provinceId"], 0);
            int cityId = Utils.GetInt(Request.Form["cityId"], 0);
            //获取客户列表
            IList<EyouSoft.Model.SMSStructure.CustomerInfo> customerList = customerBll.GetCustomers(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyID, companyName, userName, moible, userClass, EyouSoft.Model.SMSStructure.CustomerInfo.CustomerSource.所有客户, provinceId, cityId);
            if (customerList != null && customerList.Count > 0)
            {
                acl_rptCustomerList.DataSource = customerList;
                acl_rptCustomerList.DataBind();
                BindPage();
            }
            else
            {
                acl_rptCustomerList.EmptyText = " <tr><td colspan=\"4\" height=\"30\" align=\"center\">暂无客户信息!</td></tr>";
                this.ExportPageInfo1.Visible = false;
            }
        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.Form;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
       }
        #endregion

        #region  获取选择框的Html（平台共享客户没有选择框）

        /// <summary>
        /// 获取选择框的Html（平台共享客户没有选择框）
        /// </summary>
        /// <param name="CompanyId">客户所属公司ID</param>
        /// <param name="CustomerId">客户Id</param>
        /// <returns>返回选择框的Html</returns>
        protected string GetCheckBox(string CompanyId, string CustomerId)
        {
            string strReturn = string.Format("<input type=\"checkbox\" name=\"cl_chk\" value='{0}' />", CustomerId);
            if (string.IsNullOrEmpty(CompanyId) || CompanyId == "0")
                strReturn = string.Empty;

            return strReturn;
        }

        #endregion

        #region 手机号码加密

        /// <summary>
        /// 获取加密的手机号码
        /// </summary>
        /// <param name="CompanyId">客户所属公司ID</param>
        /// <param name="Mobile">要加密的手机号码</param>
        /// <returns></returns>
        protected string GetEncryptMobileText(string CompanyId, string Mobile)
        {
            return Utils.GetEncryptMobile(Mobile, CompanyId == "0" ? true : false);
        }

        #endregion
    }
}
