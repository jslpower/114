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
using EyouSoft.Common;

namespace UserBackCenter.RouteAgency
{
    /// <summary>
    /// 选择团队获取团队联系方式
    /// 创建者：luofx 时间：2010-7-15
    /// </summary>
    public partial class AjaxBooking : System.Web.UI.Page
    {
        protected string ContactName = string.Empty;
        protected string CompanyName = string.Empty;
        protected string Fax = string.Empty;
        protected string Tel = string.Empty;
        protected string Phone = string.Empty;

        protected string MQ = string.Empty;
        protected string QQ = string.Empty;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 获取所属公司信息
                EyouSoft.IBLL.CompanyStructure.ICompanyInfo ICompanyInfobll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
                EyouSoft.Model.CompanyStructure.CompanyInfo companyInfoModel = ICompanyInfobll.GetModel(Utils.GetQueryStringValue("CompanyID"));
                ContactName = companyInfoModel.ContactInfo.ContactName;
                MQ = companyInfoModel.ContactInfo.MQ;
                QQ = companyInfoModel.ContactInfo.QQ;
                Phone = companyInfoModel.ContactInfo.Mobile;
                Fax = companyInfoModel.ContactInfo.Fax;
                Tel = companyInfoModel.ContactInfo.Tel;
                ICompanyInfobll = null;
                companyInfoModel = null;
                #endregion
            }
        }
    }
}
