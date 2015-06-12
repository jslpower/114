using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;

namespace SeniorOnlineShop.master
{
    /// <summary>
    /// 普通网店母版页
    /// </summary>
    /// 周文超 2011-11-14
    public partial class GeneralShop : System.Web.UI.MasterPage
    {
        public GeneralShop() { }

        #region

        /// <summary>
        /// 普通网店宣传图片地址
        /// </summary>
        protected string ImagePath = string.Empty;

        /// <summary>
        /// 公司log图片地址
        /// </summary>
        protected string LogoPath = string.Empty;
        /// <summary>
        /// 公司所在城市
        /// </summary>
        protected string CompanyCityId = string.Empty;
        /// <summary>
        /// 公司等级
        /// </summary>
        protected EyouSoft.Model.CompanyStructure.CompanyLev Companylev;
        protected bool IslevImg = false;//是否显示公司等级图标
        protected string LevImg = string.Empty;//公司等级图标路径
        protected string Contacttel = string.Empty;
        protected string ContactFax = string.Empty;
        protected string CompanyAddress = string.Empty;
        #endregion

        /// <summary>
        /// 公司编号
        /// </summary>
        private string _companyid;

        /// <summary>
        /// 公司信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyinfo = null;

        public GeneralShop(string companyid)
        {
            _companyid = companyid;
        }

        /// <summary>
        /// 公司信息
        /// </summary>
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo CompanyInfo
        {
            get { return _companyinfo; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get { return _companyid; }
        }

        /// <summary>
        /// 目录菜单索引
        /// 1：首页  2：线路 3：机票 4：酒店  5：景区 6：供求信息 7:旅游资讯
        /// </summary>
        public int HeadIndex
        {
            get { return PageMenu1.HeadMenuIndex; }
            set { PageMenu1.HeadMenuIndex = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            //初始化公司ID
            _companyid = Utils.GetQueryStringValue("cid");
            var detailCompanyInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(_companyid);
            if (detailCompanyInfo == null || detailCompanyInfo.StateMore.IsDelete)
            {
                Utils.ShowError("普通网店不存在!", "Shop");
                return;
            }
            _companyinfo = detailCompanyInfo;
            Companylev =_companyinfo.CompanyLev;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //添加头部样式脚本
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("index2011"));
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("xinmian"));
            if (!IsPostBack)
            {
                InitCompanyInfo();
            }
        }

        #region 公司信息

        private void InitCompanyInfo()
        {
            //公司详细信息
            if (_companyinfo != null)
            {
                bool isTravel = false;
                ltrBrandName.Text = Utils.GetText2(_companyinfo.CompanyBrand, 8, false);
                ltrCompanyName.Text = Utils.GetText2(_companyinfo.CompanyName, 19, false);
                ltrCompanyType.Text = GetCompanyTypes(out isTravel);
                CompanyAddress = _companyinfo.CompanyAddress;
                ltrAddress.Text = CompanyAddress;
                ltrContact.Text = Utils.GetText2(_companyinfo.ContactInfo.ContactName, 8, true);
                ltrFax.Text = Utils.GetText2(_companyinfo.ContactInfo.Fax, 15, false);
                ltrTel.Text = Utils.GetText2(_companyinfo.ContactInfo.Tel, 15, false);
                ContactFax = _companyinfo.ContactInfo.Fax;
                Contacttel = _companyinfo.ContactInfo.Tel;
                ImagePath = Utils.GetLineShopImgPath(_companyinfo.AttachInfo.CompanyImg.ImagePath, 6);
                LogoPath = Utils.GetLineShopImgPath(_companyinfo.AttachInfo.CompanyLogo.ImagePath, 2);
                ltrprovince.Text = GetProvinceName(_companyinfo.ProvinceId);
                ltrcity.Text = "<a target=\"_blank\" href=\"" + Domain.UserPublicCenter + "/TourManage/TourList.aspx?City=" +GetCityName(_companyinfo.CityId) + "&SearchType=More\">" + GetCityName(_companyinfo.CityId) + "</a>";
                ltrarea.Text = GetArea(_companyinfo.CountyId);
                CompanyCityId = _companyinfo.CityId.ToString();
                //if (isTravel)
                //{
                //    ltrSaleArea.Visible = true;
                //    ltrSaleArea.Text = Utils.GetText2(GetCompanySaleCity(), 12, false);
                //}
            }
        }
        /// <summary>
        /// 获取省份名称
        /// </summary>
        /// <param name="Provinceid"></param>
        /// <returns></returns>
        private string GetProvinceName(int Provinceid)
        {
            SysProvince sysProvince = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(Provinceid);
            if (sysProvince != null)
                return sysProvince.ProvinceName;
            else
                return "";
        }
        /// <summary>
        /// 获取城市名称
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        private string GetCityName(int cityid)
        {
            SysCity syscity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityid);
            if (syscity != null)
                return syscity.CityName;
            else
                return "";
        }
        /// <summary>
        /// 获取县区
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private string GetArea(int areaid)
        {
            SysArea sysarea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(areaid);
            if (sysarea != null)
                return sysarea.AreaName;
            else
                return "";
        }

        /// <summary>
        /// 获取公司等级图标
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected void GetCompanyLevImgSrc(int typeid)
        {
            switch (typeid)
            {
                case 3://签约 （皇冠）
                    IslevImg = true;
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/guan.gif";
                    break;
                case 4://收费商户（钻石）
                    IslevImg = true;
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/zuan.gif";
                    break;
                case 2://认证商户（认证标签） 
                    IslevImg = true;
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/renzheng.gif";
                    break;
                default:
                    LevImg = "";
                    break;
            }

        }
        /// <summary>
        /// 返回销售城市
        /// </summary>
        /// <returns></returns>
        private string GetCompanySaleCity()
        {
            string strReturn = string.Empty;
            if (_companyinfo != null && _companyinfo.SaleCity != null && _companyinfo.SaleCity.Count > 0)
            {
                foreach (var t in _companyinfo.SaleCity)
                {
                    strReturn += t.CityName + "&nbsp;";
                }
            }

            return strReturn;
        }

        /// <summary>
        /// 返回公司类型
        /// </summary>
        /// <returns></returns>
        private string GetCompanyTypes(out bool isTravel)
        {
            string strReturn = string.Empty;
            isTravel = false;
            //公司详细信息
            if (_companyinfo != null && _companyinfo.CompanyRole != null)
            {
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.专线 + "&nbsp;";
                    isTravel = true;
                }
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.组团 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.地接 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.其他采购商))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.其他采购商 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.景区 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.车队))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.车队 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.购物店))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.购物店 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.机票供应商))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.机票供应商 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.酒店 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛 + "&nbsp;";
                if (_companyinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.全部))
                    strReturn += EyouSoft.Model.CompanyStructure.CompanyType.全部 + "&nbsp;";
            }

            return strReturn;
        }

        #endregion
    }
}
