using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMFrame
{
    /// <summary>
    /// 获取MQ所属公司网店地址
    /// </summary>
    public partial class EShop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string shopUrl = string.Empty;
            string mq = Request.QueryString["mq"];
            if (!string.IsNullOrEmpty(mq) && EyouSoft.Common.Function.StringValidate.IsInteger(mq))
            {
                EyouSoft.Model.CompanyStructure.CompanyUser user = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetModel(int.Parse(mq));
                EyouSoft.Model.CompanyStructure.CompanyAndUserInfo company = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(int.Parse(mq));
                if (company!=null &&company.Company.CompanyRole.RoleItems.Length > 0)
                {
                    shopUrl = EyouSoft.Common.Utils.GetCompanyDomain(company.Company.ID, company.Company.CompanyRole.RoleItems[0]);
                }
                if (!string.IsNullOrEmpty(shopUrl))
                {
                    Response.Write("<script>location.href='" + shopUrl + "';</script>");
                }
                else
                {
                    Response.Write("<script>location.href='" + EyouSoft.Common.Domain.UserPublicCenter + "';</script>");
                }
            }
        }
    }
}
