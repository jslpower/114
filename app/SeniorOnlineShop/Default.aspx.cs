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

namespace SeniorOnlineShop
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentHost = HttpContext.Current.Request.ServerVariables["Http_Host"].ToLower();

            EyouSoft.Model.SystemStructure.SysCompanyDomain cDomianModel = EyouSoft.BLL.SystemStructure.SysCompanyDomain.CreateInstance().
                    GetSysCompanyDomain(EyouSoft.Model.SystemStructure.DomainType.网店域名, currentHost);
            if (cDomianModel != null && !cDomianModel.IsDisabled)
            {
                string transferPath = "/seniorshop/default.aspx";

                if (!string.IsNullOrEmpty(cDomianModel.GoToUrl))
                {
                    transferPath = cDomianModel.GoToUrl;                    
                }

                Server.Transfer(transferPath);
            }
            else
            {
                Utils.ShowError("您当前查看的高级网店不存在", "SeniorShop");
            }
        }
    }
}
