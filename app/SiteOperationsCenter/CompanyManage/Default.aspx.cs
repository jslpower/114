using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 旅行社汇总管理
    /// 开发人：刘玉灵  时间：2010-7-5
    /// </summary>
    public partial class Default :EyouSoft.Common.Control.YunYingPage
    {
        protected int intPage = 0;
        protected bool isUpdate = false;
        protected bool isDelete = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            intPage = Utils.GetInt(Request.QueryString["Page"]);
            if (!Page.IsPostBack)
            {
                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目,
                    EyouSoft.Common.YuYingPermission.旅行社汇总管理_管理该栏目);
                if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                {
                    isUpdate = true;
                }
                if (isManage && CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_删除))
                {
                    isDelete = true;
                }
                if (isManage==false)
                {
                    Utils.ResponseNoPermit();
                    return;
                }
                else
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
                    int CompanyType = Utils.GetInt(Request.QueryString["CompanyType"]);
                    if (CompanyType > 0)
                    {
                        this.dropCompanyType.SelectedValue = CompanyType.ToString();
                    }

                    string CompanyName = Server.UrlDecode(Utils.InputText(Request.QueryString["CompanyName"]));
                    if (!string.IsNullOrEmpty(CompanyName))
                    {
                        this.txtCompanyName.Value = CompanyName;
                    }
                    string RecommendPerson = Server.UrlDecode(Utils.InputText(Request.QueryString["RecommendPerson"]));
                    if (!string.IsNullOrEmpty(RecommendPerson))
                    {
                        this.txtRecommendPerson.Value = RecommendPerson;
                    }
                    if (ProvinceId > 0 || CompanyType > 0 || !string.IsNullOrEmpty(CompanyName) || !string.IsNullOrEmpty(RecommendPerson))
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "CompanyManage.OnSearch();");
                    }
                }

            }

            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                string type = Request.QueryString["Type"].ToString();
                if (type.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Write(this.DeletCompany());
                    Response.End();
                }
            }

        }

        /// <summary>
        /// 删除公司
        /// </summary>
        protected bool DeletCompany()
        {
            bool Result = false;
            string[] strCompanyId = Request.Form.GetValues("ckCompanyId");
            if (strCompanyId != null)
            {
                Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Remove(strCompanyId);
            }
            return Result;
        }
    }
}
