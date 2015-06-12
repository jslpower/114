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
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Common;
using System.Text;
using System.Collections.Generic;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 功能：运营后台中的个人会员管理列标以及搜索查询
    /// 2011-12-14 蔡永辉
    /// </summary>
    public partial class AjaxBusinessMemeberList : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 20;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 序号
        /// </summary>
        private int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"], 1);//获取分页序号
            if (!Page.IsPostBack)
            {
                BindPersonalMemberList();
            }
        }
        /// <summary>
        /// 绑定商家会员列表
        /// </summary>
        protected void BindPersonalMemberList()
        {
            int recordCount = 0;
            int ProvinceId = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("ProvinceId"));
            int CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("CityId"));
            int CountyId = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("CountyId"));
            string BusinessMemName = Utils.InputText(Utils.GetQueryStringValue("BusinessMemName"));
            string Lineid = Utils.InputText(Utils.GetQueryStringValue("Lineid"));

            //单位类型
            int intCompanyType = -1;
            CompanyType?[] listCompanyType = new CompanyType?[] { null };
            if (Utils.GetQueryStringValue("CompanyType") != "")
            {
                intCompanyType = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("CompanyType"));
                if (intCompanyType != 0)
                {
                    listCompanyType[0] = (CompanyType)intCompanyType;
                }
            }
            //公司等级
            int intCompanyLev = -1;
            if (Utils.GetQueryStringValue("CompanyLev") != "")
                intCompanyLev = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("CompanyLev"));

            int intB2B = -1;
            if (Utils.GetQueryStringValue("B2B") != "")
                intB2B = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("B2B"));

            int intB2C = -1;
            if (Utils.GetQueryStringValue("B2C") != "")
                intB2C = EyouSoft.Common.Function.StringValidate.GetIntValue(Utils.GetQueryStringValue("B2C"));

            EyouSoft.Model.CompanyStructure.QueryNewCompany SearchModel = new QueryNewCompany();
            if (ProvinceId > 0)
                SearchModel.PorvinceId = ProvinceId;
            if (CityId > 0)
                SearchModel.CityId = CityId;
            if (CountyId > 0)
                SearchModel.CountyId = CountyId;
            if (BusinessMemName != "公司名、地址、简称、品牌")
                SearchModel.KeyWord = BusinessMemName;

            if (listCompanyType[0] != null && listCompanyType.Length > 0)
                SearchModel.CompanyTypes = listCompanyType;

            if (intCompanyLev > -1)
                SearchModel.CompanyLev = (CompanyLev)intCompanyLev;

            if (intB2B > -1)
                SearchModel.B2BDisplay = (CompanyB2BDisplay)intB2B;

            if (intB2C > -1)
                SearchModel.B2CDisplay = (CompanyB2CDisplay)intB2C;

            if (Lineid != "0")
                SearchModel.AreaId = Utils.GetInt(Lineid);
            SearchModel.IsShowNoCheck = true;

            IList<CompanyAndUserInfo> listCompanyanduserinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(PageSize, PageIndex, ref recordCount, SearchModel);
            if (listCompanyanduserinfo.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;
                this.ExporPageInfoSelect1.HrefType = Adpost.Common.ExporPage.HrefTypeEnum.JsHref;
                this.ExporPageInfoSelect1.AttributesEventAdd("onclick", "BusinessMemManage.LoadData(this);", 1);
                this.ExporPageInfoSelect1.AttributesEventAdd("onchange", "BusinessMemManage.LoadData(this);", 0);
                this.repList.DataSource = listCompanyanduserinfo;
                this.repList.DataBind();
            }
            else
            {
                StringBuilder strEmptyText = new StringBuilder();
                strEmptyText.Append("<table width=\"100%\" border=\"1\" align=\"center\" cellpadding=\"1\" cellspacing=\"0\" bordercolor=\"#C7DEEB\" class=\"table_basic\">");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th nowrap=\"nowrap\">编号</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">经营范围</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">单位名称</th><th nowrap=\"nowrap\">地区</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">管理员</th><th nowrap=\"nowrap\">收费</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">注册</th nowrap=\"nowrap\"><th>最近</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">登录</th><th nowrap=\"nowrap\">会员等级</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">功能</th><th nowrap=\"nowrap\">查看</th>");
                strEmptyText.Append("<tr align='center'><td  align='center' colspan='20' height='100px'>暂无信息</td></tr>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("<tr>");
                strEmptyText.Append("<th nowrap=\"nowrap\">编号</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">经营范围</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">单位名称</th><th nowrap=\"nowrap\">地区</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">管理员</th><th nowrap=\"nowrap\">收费</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">注册</th nowrap=\"nowrap\"><th>最近</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">登录</th><th nowrap=\"nowrap\">会员等级</th>");
                strEmptyText.Append("<th nowrap=\"nowrap\">功能</th><th nowrap=\"nowrap\">查看</th>");
                strEmptyText.Append("</tr>");
                strEmptyText.Append("</table>");
                this.repList.EmptyText = strEmptyText.ToString();
            }
            listCompanyType = null;
            SearchModel = null;
            listCompanyanduserinfo = null;
        }


        /// <summary>
        /// 序号
        /// </summary>
        protected int GetCount()
        {
            return ++count + (PageIndex - 1) * PageSize;
        }


        #region 列表项

        //获取收费
        protected string GetCompanyServices(object services)
        {
            if (services.Equals(null)) return string.Empty;

            var model = (EyouSoft.Model.CompanyStructure.CompanyService)services;
            if (model.Length <= 0)
                return string.Empty;

            string strTmp = string.Empty;
            foreach (var t in model.ServiceItems)
            {
                strTmp += (t.IsEnabled ? t.Service.ToString() : string.Empty) + "<br />";
            }

            return strTmp;
        }

        /// <summary>
        /// 获的公司身份
        /// </summary>
        protected string GetCompanyRole(EyouSoft.Model.CompanyStructure.CompanyType[] TypeItems)
        {
            string strAllRole = "";
            for (int i = 0; i < TypeItems.Length; i++)
            {
                strAllRole += TypeItems[i].ToString() + ",";
            }
            if (strAllRole.EndsWith(","))
            {
                strAllRole = strAllRole.Substring(0, strAllRole.Length - 1);
            }
            return strAllRole;
        }

        //获取公司的地区
        protected string GetCompanyproCityCountry(int province, int city, int country)
        {
            string result = "";
            if (province > 0)
            {
                var modelprovince = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(province);
                if (modelprovince != null)
                {
                    result = modelprovince.ProvinceName + " ";
                }

            }
            if (city > 0)
            {
                var modelCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(city);
                if (modelCity != null)
                {
                    result += modelCity.CityName + " ";
                }
            }
            if (country > 0)
            {
                var modelcountry = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetModel(country);
                if (modelcountry != null)
                {
                    result += modelcountry.DistrictName;
                }
            }
            return result;
        }


        /// <summary>
        /// 获取公司的详细信息
        /// </summary>
        /// <returns></returns>
        public string GetCompanyInfo(object companyinfo)
        {
            CompanyDetailInfo modelcompany = (CompanyDetailInfo)companyinfo;
            string result = "";
            result = "<a href=" + "javascript:void(0)" + " onmouseout=" + "wsug(event,0)" + " onmouseover=" + "wsug(event,'联系人:" + modelcompany.ContactInfo.ContactName + "&lt;br/&gt;手机:" + modelcompany.ContactInfo.Mobile + "&lt;br/&gt;电话:" + modelcompany.ContactInfo.Tel + "&lt;br/&gt;传真:" + modelcompany.ContactInfo.Fax + "&lt;br/&gt;QQ:" + modelcompany.ContactInfo.QQ + "')" + ">"
                + "联系人：" + modelcompany.ContactInfo.ContactName + "<br />"
                + "联系电话：" + modelcompany.ContactInfo.Tel + "</a>";
            return result;
        }

        #endregion
    }
}
