using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;

namespace UserBackCenter.GeneralShop.SightShop
{
    /// <summary>
    /// 用户后台普通景区网店
    /// </summary>
    /// 周文超 2011-11-16
    public partial class SetSightShop : BasePage
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        private EyouSoft.Model.CompanyStructure.CompanyDetailInfo _companyinfo = null;

        /// <summary>
        /// 普通网店宣传图片地址
        /// </summary>
        protected string ImagePath = string.Empty;

        /// <summary>
        /// 公司log图片地址
        /// </summary>
        protected string LogoPath = string.Empty;

        /// <summary>
        /// 同业114log
        /// </summary>
        protected string UnionLogo = "";

        /// <summary>
        /// 经营许可证图片路径
        /// </summary>
        protected string BusinessCertImg = string.Empty;

        /// <summary>
        /// 营业执照图片路径
        /// </summary>
        protected string LicenceImg = string.Empty;

        /// <summary>
        /// 税务登记证图片路径
        /// </summary>
        protected string TaxRegImg = string.Empty;

        /// <summary>
        /// 版权
        /// </summary>
        protected string UnionRight = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage();
            }

            if (!IsPostBack)
            {
                //公司详细信息
                _companyinfo =
                    EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);

                InitCompanyInfo();
                InifCompanyInfoSun();
                GetUnionLogo();
            }
        }

        #region 公司信息

        private void InitCompanyInfo()
        {
            //公司详细信息
            if (_companyinfo != null)
            {
                bool isTravel = false;
                ltrBrandName.Text = Utils.GetText2(_companyinfo.CompanyBrand, 10, false);
                ltrCompanyName.Text = Utils.GetText2(_companyinfo.CompanyName, 19, false);
                ltrCompanyType.Text = Utils.GetText2(GetCompanyTypes(out isTravel), 12, false);
                ltrAddress.Text = Utils.GetText2(_companyinfo.CompanyAddress, 22, true);
                ltrContact.Text = Utils.GetText2(_companyinfo.ContactInfo.ContactName, 6, true);
                ltrFax.Text = Utils.GetText2(_companyinfo.ContactInfo.Fax, 22, false);
                ltrTel.Text = Utils.GetText2(_companyinfo.ContactInfo.Tel, 10, false);
                ImagePath = Utils.GetLineShopImgPath(_companyinfo.AttachInfo.CompanyImg.ImagePath, 6);
                LogoPath = Utils.GetLineShopImgPath(_companyinfo.AttachInfo.CompanyLogo.ImagePath, 2);

                ltrSaleArea.Visible = false;
                if (isTravel)
                {
                    ltrSaleArea.Visible = true;
                    ltrSaleArea.Text = Utils.GetText2(GetCompanySaleCity(), 12, false);
                }
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

        /// <summary>
        /// 获的联盟LOGO
        /// </summary>
        protected void GetUnionLogo()
        {
            EyouSoft.Model.SystemStructure.SystemInfo model = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSystemModel();
            if (model != null)
            {
                UnionLogo = Domain.FileSystem + model.UnionLog;
                UnionRight = model.AllRight;
            }
            else
            {
                UnionLogo = Domain.ServerComponents + "/images/UserPublicCenter/indexlogo.gif";
            }
            model = null;

        }

        #region 子页面初始化公司信息

        /// <summary>
        /// 初始化公司信息
        /// </summary>
        private void InifCompanyInfoSun()
        {
            ltrCompanyInfo.Text = _companyinfo.Remark;
            ltrEmail.Text = _companyinfo.ContactInfo.Email;
            ltrQQ.Text = Utils.GetNewQQ(_companyinfo.ContactInfo.QQ);
            ltrMSN.Text = Utils.GetMsn(_companyinfo.ContactInfo.MSN);
            ltrMQ.Text = Utils.GetBigImgMQ(_companyinfo.ContactInfo.MQ);
            ltrYWYS.Text = _companyinfo.ShortRemark;
            //同业联系方式在公司信息增加后开放
            ltrTYLXFS.Visible = false;
            if (_companyinfo.BankAccounts != null && _companyinfo.BankAccounts.Count > 0)
            {
                var model =
                    _companyinfo.BankAccounts.Find(
                        t => (t.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司));
                if (model != null)
                {
                    ltrCompanyBank.Text = model.BankAccountName;
                    ltrBankName.Text = model.BankName;
                    ltrAccount.Text = model.AccountNumber;
                }
            }

            if (_companyinfo.AttachInfo != null && _companyinfo.AttachInfo.BusinessCertif != null)
            {
                BusinessCertImg = Utils.GetNewImgUrl(_companyinfo.AttachInfo.BusinessCertif.BusinessCertImg, 2);
                LicenceImg = Utils.GetNewImgUrl(_companyinfo.AttachInfo.BusinessCertif.LicenceImg, 2);
                TaxRegImg = Utils.GetNewImgUrl(_companyinfo.AttachInfo.BusinessCertif.TaxRegImg, 2);
            }

            InitCompanyUser();
            InitPersonalAccount();
        }

        /// <summary>
        /// 初始化公司个人银行账户
        /// </summary>
        private void InitPersonalAccount()
        {
            if (_companyinfo.BankAccounts == null || _companyinfo.BankAccounts.Count < 1)
                return;

            var list = from t in _companyinfo.BankAccounts
                       where t.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.个人
                       select t;
            rptPersonalAccount.DataSource = list;
            rptPersonalAccount.DataBind();
        }

        /// <summary>
        /// 初始化公司用户
        /// </summary>
        private void InitCompanyUser()
        {
            var qmodel = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
            var userList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(SiteUserInfo.CompanyID, qmodel);
            rptCompanyUser.DataSource = userList;
            rptCompanyUser.DataBind();
        }

        /// <summary>
        /// 获取公司用户信息Html
        /// </summary>
        /// <param name="obj">公司用户信息实体</param>
        /// <returns></returns>
        protected string GetCompanyUserInfo(object obj)
        {
            if (obj == null)
                return "<li>暂无联系人</li>";

            var model = (EyouSoft.Model.CompanyStructure.ContactPersonInfo)obj;
            string strReturn =
                string.Format(
                    @" <li>{0}</li><li>{1}</li><li>{2}</li><li>{3}</li><li>{4}</li><li>{5}</li><li style=""width: 150px;"">{6}</li><li style=""width: 150px;"">{7}</li> ",
                    GetNbsp(model.ContactName), GetNbsp(model.MQ), GetNbsp(model.Tel), GetNbsp(model.Mobile),
                    GetNbsp(model.Fax), GetNbsp(model.QQ),
                    GetNbsp(model.MSN), GetNbsp(model.Email));

            return strReturn;
        }

        /// <summary>
        /// 如果字符串为空的话，将其替换为html空格（&nbsp;）
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        private string GetNbsp(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
                return "&nbsp;";

            return strInput;
        }

        #endregion
    }
}
