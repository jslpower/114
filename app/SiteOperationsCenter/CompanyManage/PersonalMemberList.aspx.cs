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

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 功能：运营后台中的个人会员管理列标以及搜索查询
    /// 2011-12-14 蔡永辉
    /// </summary>
    public partial class PersonalMemberList : EyouSoft.Common.Control.YunYingPage
    {
        protected int currentPage = 0;
        protected string type = "";
        protected string deleteid = "";
        protected string Companyid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            currentPage = Utils.GetInt(Request.QueryString["Page"]);
            type = Utils.GetQueryStringValue("type");
            deleteid = Utils.GetQueryStringValue("arg");
            Companyid = Utils.GetQueryStringValue("CompanyId");

            if (!IsPostBack)
            {
                GetPersonalMemeberList();
                if (type == "DeletePersonalMem")
                {
                    DelpersonalMem();
                }
            }
        }


        #region 删除个人会员
        protected void DelpersonalMem()
        {
            bool boolresult = false;
            string result = "";
            string[] deleteidlist = deleteid.Split('$');
            string[] newdeleteidlist = { "" };
            for (int i = 0; i < deleteidlist.Length; i++)
            {
                if (!string.IsNullOrEmpty(deleteidlist[i]))
                {
                    newdeleteidlist[i] = deleteidlist[i];
                }
            }
            if (newdeleteidlist.Length > 0)
            {
                boolresult = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().Delete(newdeleteidlist);
                if (boolresult)
                {
                    result = "1";
                }
                else
                    result = "0";
            }
            Response.Clear();
            Response.Write(result);
            Response.End();
        }
        #endregion

        #region 获取个人会员列表
        protected void GetPersonalMemeberList()
        {
            int ProvinceId = Utils.GetInt(Request.QueryString["ProvinceId"]);
            if (ProvinceId > 0)
            {
                this.ProvinceAndCityAndCounty1.SetProvinceId = ProvinceId;
                int CityId = Utils.GetInt(Request.QueryString["CityId"]);
                if (CityId > 0)
                {
                    this.ProvinceAndCityAndCounty1.SetCityId = CityId;
                    int CountyId = Utils.GetInt(Request.QueryString["CountyId"]);
                    if (CountyId > 0)
                    {
                        this.ProvinceAndCityAndCounty1.SetCountyId = CountyId;
                    }
                }
            }
            string PersonalMemName = Utils.GetQueryStringValue("PersonalMemName");
            if (!string.IsNullOrEmpty(PersonalMemName))
                txtPersonalMemName.Value = PersonalMemName;

            if (ProvinceId > 0 || txtPersonalMemName.Value != "单位名，地址，联系人，账户")
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "Parms.type=1;CompanyUserManage.OnSearch();");
            }
        }
        #endregion
    }
}
