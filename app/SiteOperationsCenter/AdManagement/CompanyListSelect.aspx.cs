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
using EyouSoft.Security.Membership;
using EyouSoft.Common;
using System.Collections.Generic;

namespace SiteOperationsCenter.AdManagement
{
    /// <summary>
    /// 广告管理：公司列表查询
    /// 功能：查询
    /// 创建人：袁惠
    /// 创建时间： 2010-7-22  
    /// </summary>
    public partial class CompanyListSelect : EyouSoft.Common.Control.YunYingPage
    {
        private int intPageSize = 21, intPageIndex = 1;
        protected string CompanyName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                if (!this.CheckMasterGrant(YuYingPermission.同业114广告_管理该栏目))
                {
                    Utils.ResponseNoPermit(YuYingPermission.同业114广告_管理该栏目, true);
                    return;
                }
                int provinceid = Utils.GetInt(Request.QueryString["Province"],0);
                int citypid = Utils.GetInt(Request.QueryString["City"],0);
                string companyname = Utils.GetString(Server.UrlDecode(Request.QueryString["CompanyName"]), "");
                string companyType = Utils.GetString(Server.UrlDecode(Request.QueryString["companyType"]), "");
                BindRadioList(companyType);
                InitPage(provinceid, citypid, companyname, companyType);

            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage(int provinceid,int cityid,string comname, string companytype)
        {   
            int intRecordCount = 0;
            intPageIndex = Utils.GetInt(Utils.InputText(Request.QueryString["Page"]), 1);
            EyouSoft.Model.CompanyStructure.QueryParamsAllCompany QueryModel = new EyouSoft.Model.CompanyStructure.QueryParamsAllCompany();
            QueryModel.PorvinceId = provinceid;
            ProvinceAndCityList1.SetProvinceId = QueryModel.PorvinceId;
       
            QueryModel.CityId = cityid;
            ProvinceAndCityList1.SetCityId = QueryModel.CityId;

            if (!string.IsNullOrEmpty(companytype))
            {
               // intPageIndex = 1;
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CompanyType"]))
                {
                    companytype = Request.QueryString["CompanyType"];
                }
                else
                {
                    companytype = "全部";
                }
            }
            QueryModel.CompanyType = (EyouSoft.Model.CompanyStructure.CompanyType)Enum.Parse(typeof(EyouSoft.Model.CompanyStructure.CompanyType), companytype);
            
            QueryModel.CompanyName = comname;
            CompanyName = comname;
            this.ddlCompanyType.Text = companytype;
            IList<EyouSoft.Model.CompanyStructure.CompanyDetailInfo> lists = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetListChecked(QueryModel,intPageSize, intPageIndex, ref intRecordCount);
            if (lists != null && lists.Count > 0)
            {
                rptQueryTour.DataSource = lists;
                rptQueryTour.DataBind();
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = intPageIndex;
                this.ExportPageInfo1.CurrencyPageCssClass = "RedFnt";
                this.ExportPageInfo1.UrlParams = Request.QueryString;
                this.ExportPageInfo1.PageLinkURL = "CompanyListSelect.aspx?";
                this.ExportPageInfo1.LinkType = 3;   
            }
            ProvinceAndCityList1.SetCityId = QueryModel.CityId;
            ProvinceAndCityList1.SetProvinceId = QueryModel.PorvinceId;
            lists = null;
            QueryModel = null;
            if (rptQueryTour.Items.Count < 1)
            {
                Literal1.Text = "暂无公司可以选择！";
            }
        }

        private void BindRadioList(string selectValue)
        {
            #region 绑定公司类型
            string[] typeList = Enum.GetNames(typeof(EyouSoft.Model.CompanyStructure.CompanyType));
            if (typeList.Length > 0)
            {
                for (int i = 0; i < typeList.Length; i++)
                {
                    ListItem listItem = new ListItem(typeList[i], typeList[i]);
                    if(listItem.Value ==selectValue)
                    {
                        listItem.Selected = true;
                    }
                    this.ddlCompanyType.Items.Add(listItem);
                }
            }
            #endregion
        }
    }
}
