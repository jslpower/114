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
using EyouSoft.Common.Function;
using EyouSoft.Common;
using System.IO;

namespace UserBackCenter.FinanceManage
{
    /// <summary>
    /// 新增应收账款,或应付账款
    /// </summary>
    /// zhangzy   2010-11-11 
    public partial class AddAccount : EyouSoft.Common.Control.BasePage
    {
        protected string AddType = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.CheckGrant(EyouSoft.Common.TravelPermission.营销工具_财务管理))
            {
                Response.Clear();
                Response.Write("对不起，你正在使用的帐号没有操作该页面的权限！");
                Response.End();
            }
            if (!this.IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }
            AddType = Utils.InputText(Request.QueryString["AddType"]);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isTrue = false;
            if (AddType == "pay")　//新增支付
            {
                EyouSoft.Model.ToolStructure.Payments pay = new EyouSoft.Model.ToolStructure.Payments();
                pay.CompanyId = this.SiteUserInfo.CompanyID;
                pay.CompanyName = Utils.InputText(this.txtProviderName.Text.Trim());
                pay.CompanyType = this.txtProviderType.Text.Trim();
                pay.IssueTime = DateTime.Now;
                if (this.txtLeaveDate.Text.Trim() != string.Empty)
                    pay.LeaveDate = Utils.GetDateTime(this.txtLeaveDate.Text.Trim());
                pay.OperatorId = this.SiteUserInfo.ID;
                pay.PCount = Utils.GetInt(this.txtPeopleNumber.Text.Trim(), 0);
                pay.RouteName = Utils.InputText(this.txtRouteName.Text.Trim());
                pay.SumPrice = Utils.GetDecimal(this.txtMoney.Text.Trim(), 0.0m);
                pay.TourId = this.hidTourId.Value.Trim();
                pay.TourNo = Utils.InputText(this.txtTourNo.Text.Trim());
                isTrue = EyouSoft.BLL.ToolStructure.Payments.CreateInstance().AddPayments(pay);
                pay = null;
            }
            else
            {
                EyouSoft.Model.ToolStructure.Receivables collect = new EyouSoft.Model.ToolStructure.Receivables();                
                collect.CompanyId = this.SiteUserInfo.CompanyID;
                if (this.txtLeaveDate.Text.Trim() != string.Empty)
                    collect.LeaveDate = Utils.GetDateTime(this.txtLeaveDate.Text.Trim());
                collect.OperatorId = this.SiteUserInfo.ID;
                collect.OperatorMQ = Utils.InputText(this.txtBuyOrderUserMQ.Text.Trim());
                collect.OperatorName = Utils.InputText(this.txtBuyOrderUserName.Text.Trim());
                collect.OrderNo = Utils.InputText(this.txtOrderNo.Text.Trim());
                collect.PeopleNum = Utils.GetInt(this.txtPeopleNumber.Text.Trim(), 0); 
                collect.RetailersName = Utils.InputText(this.txtBuyOrderCompanyName.Text.Trim());
                collect.RouteName = Utils.InputText(this.txtRouteName.Text.Trim());
                collect.SumPrice = Utils.GetDecimal(this.txtMoney.Text.Trim(), 0.0m);
                collect.TourId = this.hidTourId.Value.Trim();
                collect.TourNo = Utils.InputText(this.txtTourNo.Text.Trim());
                isTrue = EyouSoft.BLL.ToolStructure.Receivables.CreateInstance().AddReceivables(collect);
                collect = null;
            }

            this.form1.InnerHtml = "";
            //string msg = "操作成功";
            //if (!isTrue)
            //    msg = "操作失败";

            //MessageBox.ResponseScript(this.Page, "alert(\"" + msg + "！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){window.parent.location.reload();});");
            MessageBox.ResponseScript(this.Page, string.Format("AddAccount.addCallback({0});", isTrue.ToString().ToLower()));
            //MessageBox.ResponseScript(this.Page, "alert(\"" + msg + "！\");parent.Boxy.getIframeDialog('" + Request.QueryString["iframeId"] + "').hide(function(){parent.topTab.remove(parent.topTab.activeTabIndex);parent.topTab.open('/FinanceManage/AccountPayable.aspx','账务管理',{isRefresh:true});});");

        }
    }
}
