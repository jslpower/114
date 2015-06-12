using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SiteOperationsCenter.HotelManagement
{
    /// <summary>
    /// 查看开户帐户信息
    /// 开发人：孙川  2010-12-9
    /// </summary>
    public partial class HotelAccountInfo : EyouSoft.Common.Control.YunYingPage
    {
        protected string HotelAccount = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 GetHotelAccountInfo(EyouSoft.Common.Utils.GetQueryStringValue("CompanyId"));
            }
        }

        /// <summary>
        /// 获得酒店账户信息
        /// </summary>
        protected void GetHotelAccountInfo(string CompanyId)
        {
            EyouSoft.Model.HotelStructure.HotelAccount ModelHotelAccount = EyouSoft.BLL.HotelStructure.HotelAccount.CreateInstance().GetModel(CompanyId);
            if (ModelHotelAccount != null)
            {
                StringBuilder strAccount = new StringBuilder();
                strAccount.Append(string.Format("<tr><td width=\"40%\" height=\"24\" align=\"right\" bgcolor=\"#F3F7FF\">开户行及支行名称：</td><td width=\"60%\" height=\"24\" bgcolor=\"#FFFFFF\">{0}</td></tr>", ModelHotelAccount.BankName));
                strAccount.Append(string.Format("<tr><td height=\"24\" align=\"right\" bgcolor=\"#F3F7FF\">开户姓名：</td><td height=\"24\" bgcolor=\"#FFFFFF\">{0}</td></tr>", ModelHotelAccount.AccountName));
                strAccount.Append(string.Format("<tr><td height=\"24\" align=\"right\" bgcolor=\"#F3F7FF\">卡号/财付通账号：</td><td height=\"24\" bgcolor=\"#FFFFFF\">{0}</td></tr>", ModelHotelAccount.BankNo));
                strAccount.Append(string.Format("<tr><td height=\"24\" align=\"right\" bgcolor=\"#F3F7FF\">结算方式：</td><td height=\"24\" bgcolor=\"#FFFFFF\">{0}</td></tr>", ModelHotelAccount.Settlement));
                strAccount.Append(string.Format("<tr><td height=\"24\" align=\"right\" bgcolor=\"#F3F7FF\">是否提供发票：</td><td height=\"24\" bgcolor=\"#FFFFFF\">{0}</td></tr>", ModelHotelAccount.IsMailInvoice == true ? "是" : "否"));
                HotelAccount = strAccount.ToString();
            }
            else
            {
                HotelAccount = "<tr><td height=\"24\" align=\"center\" bgcolor=\"#F3F7FF\" rowspan=\"2\" >该酒店暂未开户账户信息！</td></tr>";
            }
            ModelHotelAccount = null;
            this.labHotelAoccountList.Text = HotelAccount;
        }
    }
}
