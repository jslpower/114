using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 功能：短信中心充值
    /// 开发人：刘玉灵   时间：2010-8-4
    /// </summary>
    public partial class PayMoney : EyouSoft.Common.Control.BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PayMoney_txtCompanyName.Value = SiteUserInfo.CompanyName;
                this.PayMoney_txtContactName.Value = SiteUserInfo.ContactInfo.ContactName;
                this.PayMoney_txtPayTime.Value = DateTime.Today.ToShortDateString().ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["isPay"]))
            {
                Response.Write(this.CompanyPayMoney());
                Response.End();
            }
        }

        /// <summary>
        /// 充值
        /// </summary>
        protected bool CompanyPayMoney()
        {
            bool isTrue = false;
            EyouSoft.Model.SMSStructure.PayMoneyInfo Model=new EyouSoft.Model.SMSStructure.PayMoneyInfo ();
            Model.CompanyId = SiteUserInfo.CompanyID;
            Model.CompanyName = SiteUserInfo.CompanyName;
            Model.IsChecked = 0;
            Model.OperatorTime = DateTime.Now;

            Model.PayTime = Utils.GetDateTime(Request.QueryString["PayTime"], DateTime.Today);
            Model.PayMoney = Utils.GetDecimal(Request.QueryString["PayMoney"]);
           
            Model.UserFullName = SiteUserInfo.ContactInfo.ContactName;
            Model.UserId = SiteUserInfo.ID;
            Model.UserMobile = SiteUserInfo.ContactInfo.Mobile;
            Model.UserMQId = SiteUserInfo.ContactInfo.MQ;
            Model.UserTel = SiteUserInfo.ContactInfo.Tel;
            isTrue= EyouSoft.BLL.SMSStructure.Account.CreateInstance().InsertPayMoney(Model);
            Model = null;
            return isTrue;
        }
    }
}
