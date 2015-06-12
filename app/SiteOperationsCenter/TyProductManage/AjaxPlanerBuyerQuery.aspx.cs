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
using EyouSoft.Common.Control;
using System.Collections.Generic;

namespace SiteOperationsCenter.TyProductManage
{
    /// <summary>
    /// 机票采购商查询Ajax请求页面，张新兵，20100926
    /// </summary>
    public partial class AjaxPlanerBuyerQuery : YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            //暂时用 【统计分析_管理该栏目】权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_管理该栏目, false);
            }

            BindList();
        }

        /// <summary>
        /// 获取 单位列表
        /// </summary>
        private void BindList()
        {
            int opuserId = Utils.GetInt(Request.QueryString["OPUserId"]);

            if (opuserId == 0)
            {
                Response.Clear();
                Response.Write("<span class='errmsg'>错误：[其他平台用户ID]须为有效数字或者您填写的值超出了最大值"+Int32.MaxValue+"</span>");
                Response.End();
            }

            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModelByOPUserID(opuserId);
            if (model != null && model.Company!=null&&model.User!=null)
            {
                InitCompanyInfo(model.Company);
                InitCompanyUserInfo(model.User);
            }
            else
            {
                Response.Clear();
                Response.Write("<span>抱歉！没有相关的公司和用户信息</span>");
                Response.End();
            }
        }

        /// <summary>
        /// 初始化公司信息
        /// </summary>
        /// <param name="model"></param>
        private void InitCompanyInfo(EyouSoft.Model.CompanyStructure.CompanyDetailInfo model)
        {
            ltrCompanyName.Text = model.CompanyName;
            ltrLicense.Text = model.License;
            ltrProvinceName.Text = this.GetProvinceName(model.ProvinceId);
            ltrCityName.Text = this.GetCityName(model.CityId);
            ltrUserName.Text = model.AdminAccount.UserName;
            ltrContactName.Text = model.ContactInfo.ContactName;
            ltrTel.Text = model.ContactInfo.Tel;
            ltrFax.Text = model.ContactInfo.Fax;
            ltrMobile.Text = model.ContactInfo.Mobile;
            ltrCommendPeople.Text = model.CommendPeople;
            ltrCompanyAddress.Text = model.CompanyAddress;
            ltrShortRemark.Text = model.ShortRemark;
            ltrRemark.Text = model.Remark;
            #region 经营范围
            IList<EyouSoft.Model.SystemStructure.AreaBase> AreaLists = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(model.ID);
            string AreaName = string.Empty;
            ((List<EyouSoft.Model.SystemStructure.AreaBase>)AreaLists).ForEach(item =>
            {
                AreaName += item.AreaName + "，";
            });
            ltrShortRemark.Text = AreaName.TrimEnd('，');
            #endregion
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        /// <param name="model"></param>
        private void InitCompanyUserInfo(EyouSoft.Model.CompanyStructure.CompanyUser model)
        {
            ltrUserCityName.Text = model.ContactInfo.ContactName;
            ltrUserSex.Text = model.ContactInfo.ContactSex != EyouSoft.Model.CompanyStructure.Sex.未知 ?
                model.ContactInfo.ContactSex.ToString() : "无性别信息";
            ltrIsAdmin.Text = model.IsAdmin ? "是" : "否";
            ltrUserName1.Text = model.UserName;
            ltrUserMQ.Text = model.ContactInfo.MQ;
            ltrUserMobile.Text = model.ContactInfo.Mobile;
            ltrUserTel.Text = model.ContactInfo.Tel;
            ltrUserFax.Text = model.ContactInfo.Fax;
            ltrUserEmail.Text = model.ContactInfo.Email;
            ltrUserMSN.Text = model.ContactInfo.MSN;
            ltrUserProvinceName.Text = this.GetProvinceName(model.ProvinceId);
            ltrUserCityName.Text = this.GetCityName(model.CityId);
        }

        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns></returns>
        private string GetProvinceName(int ProvinceId)
        {
            string Result = "";
            EyouSoft.Model.SystemStructure.SysProvince model = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(ProvinceId);
            if (model != null)
            {
                Result = model.ProvinceName;
            }
            return Result;
        }
        /// <summary>
        /// 获取城市名称
        /// </summary>
        /// <param name="ProvinceId"></param>
        /// <returns></returns>
        private string GetCityName(int CityId)
        {
            string Result = "";
            EyouSoft.Model.SystemStructure.SysCity model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(CityId);
            if (model != null)
            {
                Result = model.CityName;
            }
            return Result;
        }
    }
}
