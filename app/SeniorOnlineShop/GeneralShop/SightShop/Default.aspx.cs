using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SeniorOnlineShop.GeneralShop.SightShop
{
    /// <summary>
    /// 普通景区网店首页
    /// </summary>
    /// 周文超 2011-11-14
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.HeadIndex = 5;
            Page.Title = string.Format(PageTitle.ScenicDetail_Title, CityModel.CityName, Master.CompanyInfo.CompanyName);
            AddMetaTag("description", string.Format(PageTitle.ScenicDetail_Des, CityModel.CityName, Master.CompanyInfo.CompanyName));
            AddMetaTag("keywords", string.Format(PageTitle.ScenicDetail_Keywords, Master.CompanyInfo.CompanyName));
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;

            if (!IsPostBack)
            {
                InifCompanyInfo();
                //当前查看用户没有登录，隐藏需要登录才能查看的内容
                if (!IsLogin)
                {
                    plnLoginUser.Visible = false;
                }
            }
        }

        /// <summary>
        /// 初始化公司信息
        /// </summary>
        private void InifCompanyInfo()
        {
            if (Master == null || Master.CompanyInfo == null || Master.CompanyInfo.ContactInfo == null)
                return;

            ltrCompanyInfo.Text = Master.CompanyInfo.Remark;
            ltrEmail.Text = Master.CompanyInfo.ContactInfo.Email;
            string contactQq = string.Empty;
            if (!string.IsNullOrEmpty(Master.CompanyInfo.ContactInfo.QQ))
            {
                string[] strQq = Master.CompanyInfo.ContactInfo.QQ.Split(',');
                if (strQq.Length > 0)
                {
                    foreach (var s in strQq)
                    {
                        if (string.IsNullOrEmpty(s))
                            continue;

                        contactQq += Utils.GetNewQQ(s) + "&nbsp;&nbsp;";
                    }
                }
            }
            ltrQQ.Text = contactQq;
            ltrMSN.Text = Utils.GetMsn(Master.CompanyInfo.ContactInfo.MSN);
            ltrMQ.Text = Utils.GetBigImgMQ(Master.CompanyInfo.ContactInfo.MQ);
            ltrYWYS.Text = Master.CompanyInfo.ShortRemark;
            //同业联系方式在公司信息增加后开放
            ltrTYLXFS.Visible = false;
            if (Master.CompanyInfo.BankAccounts != null && Master.CompanyInfo.BankAccounts.Count > 0)
            {
                var model =
                    Master.CompanyInfo.BankAccounts.Find(
                        t => (t.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司));
                if (model != null)
                {
                    ltrCompanyBank.Text = model.BankAccountName;
                    ltrBankName.Text = model.BankName;
                    ltrAccount.Text = model.AccountNumber;
                }
            }

            if (Master.CompanyInfo.AttachInfo != null && Master.CompanyInfo.AttachInfo.BusinessCertif != null)
            {
                BusinessCertImg = Utils.GetNewImgUrl(Master.CompanyInfo.AttachInfo.BusinessCertif.BusinessCertImg, 2);
                LicenceImg = Utils.GetNewImgUrl(Master.CompanyInfo.AttachInfo.BusinessCertif.LicenceImg, 2);
                TaxRegImg = Utils.GetNewImgUrl(Master.CompanyInfo.AttachInfo.BusinessCertif.TaxRegImg, 2);
            }

            InitCompanyUser();
            InitPersonalAccount();
        }

        /// <summary>
        /// 初始化公司个人银行账户
        /// </summary>
        private void InitPersonalAccount()
        {
            if (Master.CompanyInfo.BankAccounts == null || Master.CompanyInfo.BankAccounts.Count < 1)
                return;

            var list = from t in Master.CompanyInfo.BankAccounts
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
            var userList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(Master.CompanyId, qmodel);
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
    }
}
