using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common;
namespace IMFram.TourAgency.TourManger
{
    /// <summary>
    /// 页面功能:ajax调用所关注的批发商列表
    /// 修改人:xuty 修改时间:2010-08-14
    /// </summary>
    public partial class MySettionCompany : EyouSoft.ControlCommon.Control.MQPage
    {
        protected string strSelect = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Response.Clear();
                Response.Write("对不起，你未开通组团服务！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
                InitCompany();//初始化我关注的批发商列表
            }
        }
        #region 初始化我关注的批发商列表
        private void InitCompany()
        {
            StringBuilder strSelHtml = new StringBuilder();//我关注批发商的拼接
            IList<EyouSoft.Model.CompanyStructure.CompanyInfo> companyList = EyouSoft.BLL.CompanyStructure.CompanyFavor.CreateInstance().GetListCompany(SiteUserInfo.CompanyID);
            strSelHtml.Append("<select id='ddlCompany' style='width:200px;' onchange=\"return SelectChange(this)\">");
            strSelHtml.Append("<option value='0'>我的供应商列表</option>");
            if (companyList != null && companyList.Count > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.Company model in companyList)
                {
                    string strUrl = GetDesPlatformUrl(Utils.GetShopUrl(model.ID));
                    strSelHtml.AppendFormat("<option value='{0}' href='{2}'title='{1}'>{1}</option>", model.ID, model.CompanyName, strUrl);
                }
            }
            strSelHtml.Append("</select>");
            companyList = null;
            strSelect = strSelHtml.ToString();//页面显示批发商下拉框
        }
        #endregion

    }
}
