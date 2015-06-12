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
using EyouSoft.Common.Control;
namespace UserBackCenter.SMSCenter
{
    /// <summary>
    /// 页面功能：从系统导入ajax获取客户信息列表
    /// 开发人：xuty 开发时间：2010-08-03
    /// </summary>
    public partial class AjaxSystemCustomer :  BasePage
    {
        protected int pageIndex;
        protected int pageSize = 10;
        protected int recordCount;
        EyouSoft.IBLL.SystemStructure.ISysCity cityBll;
        EyouSoft.IBLL.SystemStructure.ISysProvince provinceBll;
        protected void Page_Load(object sender, EventArgs e)
        {
            cityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
            provinceBll = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance();
            //获取当前页
            pageIndex = Utils.GetInt(Request.QueryString["Page"], 1);
            //获取查询条件
            int province = Utils.GetInt(Utils.GetQueryStringValue("province"));
            int city = Utils.GetInt(Utils.GetQueryStringValue("city"));
            string companyName = Utils.InputText(Server.UrlDecode(Request.QueryString["companyname"] ?? "")).Trim();
            string admin = Utils.InputText(Server.UrlDecode(Request.QueryString["admin"] ?? "")).Trim();
          
            string myCompanyId = SiteUserInfo.CompanyID;
            EyouSoft.Model.CompanyStructure.QueryParamsCompany query = new EyouSoft.Model.CompanyStructure.QueryParamsCompany();
            query.CityId = city;
            query.PorvinceId = province;
          
            query.CompanyName = companyName;
            query.ContactName = admin;
            //绑定我的客户
            IList<EyouSoft.Model.CompanyStructure.MyCustomer> myCustomerList = EyouSoft.BLL.CompanyStructure.MyCustomer.CreateInstance().GetList(myCompanyId, query, pageSize, pageIndex, ref recordCount);
            if (myCustomerList != null && myCustomerList.Count > 0)
            {
                asc_rptCustomerList.DataSource = myCustomerList;
                asc_rptCustomerList.DataBind();
                BindPage();
            }
            else
            {
                asc_rptCustomerList.EmptyText = "暂无客户信息";
                this.ExportPageInfo1.Visible = false;
            }
        }

        #region 获取省份城市
        protected string GetProAndCity(int pId, int cId)
        {
            string strCity = "";
            string strPro = "";
            EyouSoft.Model.SystemStructure.SysCity city = cityBll.GetSysCityModel(cId);
            EyouSoft.Model.SystemStructure.SysProvince province = provinceBll.GetProvinceModel(pId);
            if (city != null)
                strCity = city.CityName;
            if (province != null)
                strPro = province.ProvinceName;
            return strPro + "-" + strCity;

        }
        #endregion

        #region 设置分页
        protected void BindPage()
        {
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.intPageSize = pageSize;
            this.ExportPageInfo1.CurrencyPage = pageIndex;
            this.ExportPageInfo1.intRecordCount = recordCount;
        }
        #endregion
    }
}
