using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 公司详细信息
    /// luofx 2010-9-17
    /// </summary>
    public partial class CompanyInfo : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitPage()
        {
            string CompanyID = Utils.GetQueryStringValue("companyid");
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyID);
            if (model != null)
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
                IList<EyouSoft.Model.SystemStructure.AreaBase> AreaLists = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(CompanyID);
                string AreaName = string.Empty;
                ((List<EyouSoft.Model.SystemStructure.AreaBase>)AreaLists).ForEach(item =>
                {
                    AreaName += item.AreaName+"，";
                });
                ltrShortRemark.Text = AreaName.TrimEnd('，');
                #endregion
            }
            model = null;
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
