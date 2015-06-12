using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SeniorOnlineShop.shop
{
    public partial class ShopDefault : EyouSoft.Common.Control.FrontPage
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
        /// <summary>
        /// 公司宣传图片
        /// </summary>
        protected string CompanyImg1 = string.Empty;
        protected string CompanyImg2 = string.Empty;
        protected string CompanyImg3 = string.Empty;
        /// <summary>
        /// 公司经度
        /// </summary>
        protected decimal jingdu;
        /// <summary>
        /// 公司纬度
        /// </summary>
        protected decimal weidu;
        protected string CompanyName = string.Empty;//公司名称
        protected SeniorOnlineShop.master.GeneralShop master;
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected void Page_Load(object sender, EventArgs e)
        {
            master = (SeniorOnlineShop.master.GeneralShop)this.Page.Master;
            if (master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) || master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团) || master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.地接))
                master.HeadIndex = 2;
            else if (master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
            {
                master.HeadIndex = 5;
            }
            else
            {
                master.HeadIndex = 1;
            }

            Page.Title = string.Format(PageTitle.EShop_Title, master.CompanyInfo.CompanyName, CityModel.CityName);
            string remark = EyouSoft.Common.Utils.LoseHtml(master.CompanyInfo.Remark);
            if (remark.Length > 50) remark = remark.Substring(0, 50);
            AddMetaTag(
                "description",
                string.Format(PageTitle.EShop_Des, master.CompanyInfo.CompanyName, CityModel.CityName, remark));
            AddMetaTag("keywords", string.Format(PageTitle.EShop_Keywords, master.CompanyInfo.CompanyName, CityModel.CityName));

            SecondMenu1.CompanyId = master.CompanyId;
            SecondMenu1.CurrCompanyType = master.CompanyInfo.CompanyRole;

            if (!IsPostBack)
            {
                EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().SetShopClickNum(master.CompanyId);
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
            if (Master == null || master.CompanyInfo == null || master.CompanyInfo.ContactInfo == null)
                return;
            ltrCompanyInfo.Text = master.CompanyInfo.Remark;
            CompanyName = master.CompanyInfo.CompanyName;
            ltrEmail.Text = master.CompanyInfo.ContactInfo.Email;
            string contactQq = string.Empty;
            if (!string.IsNullOrEmpty(master.CompanyInfo.ContactInfo.QQ))
            {
                string[] strQq = master.CompanyInfo.ContactInfo.QQ.Split(',');
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
            ltrMSN.Text = Utils.GetMsn(master.CompanyInfo.ContactInfo.MSN);
            ltrMQ.Text = Utils.GetBigImgMQ(master.CompanyInfo.ContactInfo.MQ);
            ltrYWYS.Text = master.CompanyInfo.ShortRemark;
            jingdu = master.CompanyInfo.Longitude;
            weidu = master.CompanyInfo.Latitude;
            if (master.CompanyInfo.AttachInfo != null && master.CompanyInfo.AttachInfo.CompanyPublicityPhoto != null)
            {
                if (master.CompanyInfo.AttachInfo.CompanyPublicityPhoto.Count == 3)
                {
                    CompanyImg1 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[0].ImagePath, 2);
                    CompanyImg2 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[1].ImagePath, 2);
                    CompanyImg3 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[2].ImagePath, 2);
                }
                else if (master.CompanyInfo.AttachInfo.CompanyPublicityPhoto.Count == 2)
                {
                    CompanyImg1 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[0].ImagePath, 2);
                    CompanyImg2 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[1].ImagePath, 2);
                    CompanyImg3 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                }
                else if (master.CompanyInfo.AttachInfo.CompanyPublicityPhoto.Count == 1)
                {
                    CompanyImg1 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyPublicityPhoto[0].ImagePath, 2);
                    CompanyImg2 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                    CompanyImg3 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                }
                else
                {
                    CompanyImg1 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                    CompanyImg2 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                    CompanyImg3 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                }
            }
            else
            {
                CompanyImg1 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                CompanyImg2 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
                CompanyImg3 = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.CompanyImg.ImagePath, 2);
            }
            //同业联系方式在公司信息增加后开放
            ltrTYLXFS.Visible = false;
            if (master.CompanyInfo.BankAccounts != null && master.CompanyInfo.BankAccounts.Count > 0)
            {
                var model =
                    master.CompanyInfo.BankAccounts.Find(
                        t => (t.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司));
                if (model != null)
                {
                    ltrCompanyBank.Text = model.BankAccountName;
                    ltrBankName.Text = model.BankName;
                    ltrAccount.Text = model.AccountNumber;
                }
            }

            if (master.CompanyInfo.AttachInfo != null && master.CompanyInfo.AttachInfo.BusinessCertif != null)
            {
                BusinessCertImg = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.BusinessCertif.BusinessCertImg, 2);
                LicenceImg = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.BusinessCertif.LicenceImg, 2);
                TaxRegImg = Utils.GetNewImgUrl(master.CompanyInfo.AttachInfo.BusinessCertif.TaxRegImg, 2);
            }

            InitCompanyUser();
            InitPersonalAccount();
        }

        /// <summary>
        /// 初始化公司个人银行账户
        /// </summary>
        private void InitPersonalAccount()
        {
            if (master.CompanyInfo.BankAccounts == null || master.CompanyInfo.BankAccounts.Count < 1)
                return;

            var list = from t in master.CompanyInfo.BankAccounts
                       where t.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.个人
                       select t;
            rptPersonalAccount.DataSource = list;
            rptPersonalAccount.DataBind();
        }
        /// <summary>
        /// 获取快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        /// <summary>
        /// 初始化公司用户
        /// </summary>
        private void InitCompanyUser()
        {
            var qmodel = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
            var userList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(master.CompanyId, qmodel);
            rptCompanyUser.DataSource = userList;
            rptCompanyUser.DataBind();
        }

        /// <summary>
        /// 获取公司用户信息Html
        /// </summary>
        /// <param name="obj">公司用户信息实体</param>
        /// <returns></returns>
        protected string GetCompanyUserInfo(object obj, object job)
        {
            if (obj == null)
                return "<td colspan=\"8\" class=\"lianxi_di2_right\">暂无联系人</td>";
            string post = Utils.GetString(Convert.ToString(job), "无");

            var model = (EyouSoft.Model.CompanyStructure.ContactPersonInfo)obj;
            string strReturn = string.Format(@" <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><a title='{4}' style='text-decoration:nonel;color:#555555' >{8}</a></td><td>{5}</td><td style=""width: 120px;"">{6}</td><td style=""width: 150px;"" class=""lianxi_di2_right"">{7}</td> ",
                 Utils.GetText2(GetNbsp(model.ContactName),6,false), post, GetNbsp(model.Tel), GetNbsp(model.Mobile),
                 GetNbsp(model.Fax),GetSplitQQ(GetNbsp(model.QQ)),
                  GetNbsp(model.MSN), GetNbsp(model.Email), Utils.GetText(GetNbsp(model.Fax), 13, false));

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

        private string GetSplitQQ(string QQ)
        {
            if (QQ.Split(',').Length > 0)
            {
                string Stringqq = string.Empty;
                for (int i = 0; i < QQ.Split(',').Length; i++)
                {
                    Stringqq += QQ.Split(',')[i].ToString() + "<br>";
                }
                return Stringqq.Remove(Stringqq.Length-4);
            }
            else
            {
                return QQ;
            }
        }
    }
}
