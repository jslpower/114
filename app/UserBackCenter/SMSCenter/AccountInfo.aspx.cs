using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 功能：短信中心帐户信息
    /// 开发人： 刘玉灵  时间：2010-8-4
    /// </summary>
    public partial class AccountInfo : EyouSoft.Common.Control.BackPage
    {
        protected string CompanyMoney = "0";
        protected int AccountSMSNumber = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.AccountInfo_SmsHeaderMenu.TabIndex = "tab5";
                EyouSoft.BLL.SMSStructure.Account.CreateInstance().SetAccountBaseInfo(SiteUserInfo.CompanyID);

                //余额 剩余短信条数
                EyouSoft.Model.SMSStructure.AccountInfo model = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetAccountInfo(SiteUserInfo.CompanyID);
                if (model != null)
                {
                    CompanyMoney = model.AccountMoney.ToString("f2");
                    if (model.AccountSMSNumber != null && model.AccountSMSNumber.Count > 0)
                    {
                        rpAccountList.DataSource = model.AccountSMSNumber;
                        rpAccountList.DataBind();
                    }
                }
                model = null;

            }
        }

    }
}
