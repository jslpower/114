using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteOperationsCenter.CustomerManage
{
    public partial class AjaxContacters : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string CompanyId = EyouSoft.Common.Utils.GetQueryStringValue("CompanyId");
                if (CompanyId != null && CompanyId != "")
                {
                    GetContacters(CompanyId);
                }
                else
                {
                    this.divNodata.Visible = true;
                }
            }
        }

        /// <summary>
        /// 得到所有公司联系人的信息
        /// </summary>
        protected void GetContacters(string CompanyId)
        {
            EyouSoft.Model.PoolStructure.CompanyInfo ModelComapny = EyouSoft.BLL.PoolStructure.Company.CreateInstance().GetCompanyInfo(CompanyId);
            if (ModelComapny != null)
            {
                this.rptContacter.DataSource = ModelComapny.Contacters;
                this.rptContacter.DataBind();
            }
            else
                this.divNodata.Visible = true;
        }
    }
}
