using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.CompanyManage
{
    public partial class OtherCompany : EyouSoft.Common.Control.YunYingPage
    {
        protected bool isUpdate = false;
        protected bool isDelete = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //CompanyType：代表不同的用户类型
                //1:景区,2:酒店,3:车队,4:旅游用品店,5:购物店
                int CompanyType = EyouSoft.Common.Utils.GetInt(Request.QueryString["CompanyType"]);

                //根据用户类型初始化对应用户的【管理栏目】【修改】【删除】权限
                EyouSoft.Common.YuYingPermission CompayTypeManage = YuYingPermission.景区汇总管理_管理该栏目;
                EyouSoft.Common.YuYingPermission CompanyTypeUpate = YuYingPermission.景区汇总管理_修改;
                EyouSoft.Common.YuYingPermission CompanyTypeDelete = YuYingPermission.景区汇总管理_删除;

                switch (CompanyType)
                {
                    case 2:
                        CompanyTypeUpate = YuYingPermission.酒店汇总管理_修改;
                        CompanyTypeDelete = YuYingPermission.酒店汇总管理_删除;
                        CompayTypeManage = YuYingPermission.酒店汇总管理_管理该栏目;
                        break;
                    case 3:
                        CompanyTypeUpate = YuYingPermission.车队汇总管理_修改;
                        CompanyTypeDelete = YuYingPermission.车队汇总管理_删除;
                        CompayTypeManage = YuYingPermission.车队汇总管理_管理该栏目;
                        break;
                    case 4:
                        CompanyTypeUpate = YuYingPermission.旅游用品店汇总管理_修改;
                        CompanyTypeDelete = YuYingPermission.旅游用品店汇总管理_删除;
                        CompayTypeManage = YuYingPermission.旅游用品店汇总管理_管理该栏目;
                        break;
                    case 5:
                        CompanyTypeUpate = YuYingPermission.购物店汇总管理_修改;
                        CompanyTypeDelete = YuYingPermission.购物店汇总管理_删除;
                        CompayTypeManage = YuYingPermission.购物店汇总管理_管理该栏目;
                        break;
                    case 6:
                        CompanyTypeUpate = YuYingPermission.机票供应商管理_修改;
                        CompanyTypeDelete = YuYingPermission.机票供应商管理_删除;
                        CompayTypeManage = YuYingPermission.机票供应商管理_管理该栏目;
                        break;
                    case 7:
                        CompanyTypeUpate = YuYingPermission.其他采购商管理_修改;
                        CompanyTypeDelete = YuYingPermission.其他采购商管理_删除;
                        CompayTypeManage = YuYingPermission.其他采购商管理_管理该栏目;
                        break;
                    case 11:
                        CompanyTypeUpate = YuYingPermission.随便逛逛_修改;
                        CompanyTypeDelete = YuYingPermission.随便逛逛_删除;
                        CompayTypeManage = YuYingPermission.随便逛逛_管理该栏目;
                        break;

                }

                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目,
                    CompayTypeManage);
                //是否有【修改】权限
                if (isManage && CheckMasterGrant(CompanyTypeUpate))
                {
                    isUpdate = true;
                }
                //是否有【删除】权限
                if (isManage && CheckMasterGrant(CompanyTypeDelete))
                {
                    isDelete = true;
                }
                //如果都没有【管理栏目】权限，则不允许查看页面。
                if (isManage == false)
                {
                    Utils.ResponseNoPermit();
                    return;
                }
                else
                {
                    this.CompanyList1.CompanyType = CompanyType;


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
                    string CompanyName = Server.UrlDecode(Utils.InputText(Request.QueryString["CompanyName"]));
                    if (!string.IsNullOrEmpty(CompanyName))
                    {
                        this.txtCompanyName.Value = CompanyName;
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
