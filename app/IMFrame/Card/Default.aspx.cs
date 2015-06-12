using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;

namespace IMFrame.Card
{
    public partial class Default : EyouSoft.ControlCommon.Control.MQPage
    {
        #region Attributes
        /// <summary>
        /// 对方的MQ编号
        /// </summary>
        private int OtherSideMQ;
        /// <summary>
        /// 自己的MQ编号
        /// </summary>
        private int SelfMQ;

        protected int FAdHeight = 0;
        protected int FLogoHeight = 0;

        /// <summary>
        /// 开通MQ收费功能介绍链接
        /// </summary>
        protected string IntroductionURL = string.Empty;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            this.OtherSideMQ = Utils.GetInt(Request.QueryString[Utils.ViewOtherMQUserKey], 0);
            this.SelfMQ = Utils.GetInt(Request.QueryString[Utils.MQLoginIdKey], 0);

            if (this.OtherSideMQ==0||this.SelfMQ==0)
            {
                Response.Clear();
                Response.Write("ERROR!");
                Response.End();
            }

            if (!this.Page.IsPostBack)
            {
                this.InitOtherSideCard();
                this.InitSelfCard();
            }
        }
        #endregion

        #region 初始化自己的名片信息
        /// <summary>
        /// 初始化自己的名片信息
        /// </summary>
        private void InitSelfCard()
        {
            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.SelfMQ);

            if (model == null || model.Company == null || model.User == null) return;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo cInfo = model.Company;
            EyouSoft.Model.CompanyStructure.CompanyUser uInfo = model.User;

            bool isHighShop = cInfo.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
            bool isPayMQ = cInfo.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ);

            #region 网店链接
            string selfSideEShop = "<div class=\"{0}\"><a href=\"{1}\" target=\"_blank\"></a></div>";
            EyouSoft.Model.CompanyStructure.CompanyType cType = this.GetCompanyType(cInfo.CompanyRole.RoleItems);

            switch (cType)
            {
                case EyouSoft.Model.CompanyStructure.CompanyType.全部:
                //case EyouSoft.Model.CompanyStructure.CompanyType.地接:
                case EyouSoft.Model.CompanyStructure.CompanyType.组团:
                    selfSideEShop = string.Empty;
                    break;
                default:
                    if (isHighShop)
                    {
                        selfSideEShop = string.Format(selfSideEShop, "cardshopgj", Utils.GetCompanyDomain(cInfo.ID, cType));
                    }
                    else
                    {
                        selfSideEShop = string.Format(selfSideEShop, "cardshop", Utils.GetCompanyDomain(cInfo.ID, cType));
                    }
                    break;
            }

            this.ltrSelfEshop.Text = selfSideEShop;
            #endregion

            #region 企业LOGO、MQ广告
            if (isPayMQ)
            {
                if (this.CheckGrant(TravelPermission.系统设置_单位信息))
                {
                    this.FSelfLogo.FType = IMFrame.WebControls.FType.Logo;
                   
                    if (cInfo.AttachInfo.CompanyLogo != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyLogo.ImagePath))
                    {
                        this.FSelfLogo.HavingImg = true;
                        this.FLogoHeight = 20;
                    }
                    else
                    {
                        this.FSelfLogo.FHeight = 60;
                        this.FSelfLogo.HavingImg = false;
                        this.FLogoHeight = 60;
                    }
                  
                    this.FSelfAd.FType = IMFrame.WebControls.FType.Ad;
                    
                    if (cInfo.AttachInfo.CompanyMQAdv != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyMQAdv.ImagePath))
                    {
                        this.FSelfAd.HavingImg = true;
                        this.FAdHeight = 20;
                    }
                    else
                    {
                        this.FSelfAd.FHeight = 105;
                        this.FSelfAd.HavingImg = false;
                        this.FAdHeight = 105;
                    }
                    if (cInfo.AttachInfo.CompanyMQAdv != null)
                    {
                        this.txtPayMQSelfAdUrl.Text = cInfo.AttachInfo.CompanyMQAdv.ImageLink;
                    }

                    this.phNotPayMQSelfAd.Visible = this.phNotPayMQSelfLogo.Visible = false;
                    this.phPayMQSelfAd.Visible = this.phPayMQSelfLogo.Visible = this.phFScript.Visible = true;
                }
                else
                {
                    this.phNotPayMQSelfAd.Visible = this.phNotPayMQSelfLogo.Visible = false;
                    this.phPayMQSelfAd.Visible = this.phPayMQSelfLogo.Visible = this.phFScript.Visible = false;
                }

                if (cInfo.AttachInfo.CompanyLogo != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyLogo.ImagePath))
                {
                    this.ltrSelfLogo.Text = string.Format("<img src=\"{0}\" alt=\"\" width=\"140\" height=\"60\" />", Domain.FileSystem + cInfo.AttachInfo.CompanyLogo.ImagePath);
                }

                if (cInfo.AttachInfo.CompanyMQAdv != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyMQAdv.ImagePath))
                {
                    this.txtSelfCurrentAdImgPath.Value = cInfo.AttachInfo.CompanyMQAdv.ImagePath;
                    this.ltrSelfAd.Text = string.Format("<img src=\"{0}\" alt=\"\" width=\"140\" height=\"85\" />", Domain.FileSystem + cInfo.AttachInfo.CompanyMQAdv.ImagePath);
                }
            }
            else
            {
                if (cInfo.BusinessProperties == EyouSoft.Model.CompanyStructure.BusinessProperties.旅游社)
                {
                    this.IntroductionURL = GetDesPlatformUrl(Domain.UserBackCenter + "/SystemSet/SonUserManage.aspx");

                   // this.phNotPayMQSelfAd.Visible = this.phNotPayMQSelfLogo.Visible = true;
                    this.phNotPayMQSelfAd.Visible = this.phNotPayMQSelfLogo.Visible = false;
                    this.phPayMQSelfAd.Visible = this.phPayMQSelfLogo.Visible = this.phFScript.Visible = false;
                    this.ltrSelfLogo.Visible = this.ltrSelfAd.Visible = false;
                }
                else
                {
                    this.FSelfLogo.FType = IMFrame.WebControls.FType.Logo;

                    if (cInfo.AttachInfo.CompanyLogo != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyLogo.ImagePath))
                    {
                        this.FSelfLogo.HavingImg = true;
                        this.FLogoHeight = 20;
                    }
                    else
                    {
                        this.FSelfLogo.FHeight = 60;
                        this.FSelfLogo.HavingImg = false;
                        this.FLogoHeight = 60;
                    }

                    if (cInfo.AttachInfo.CompanyLogo != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyLogo.ImagePath))
                    {
                        this.ltrSelfLogo.Text = string.Format("<img src=\"{0}\" alt=\"\" width=\"140\" height=\"60\" />", Domain.FileSystem + cInfo.AttachInfo.CompanyLogo.ImagePath);
                    }

                    this.phNotPayMQSelfAd.Visible = this.phNotPayMQSelfLogo.Visible = false;
                    this.phPayMQSelfLogo.Visible = true;
                    this.ltrSelfLogo.Visible =true;
                    this.phGYSFScript.Visible = true;

                    this.phFScript.Visible = false;
                    this.ltrSelfAd.Visible = false;
                    this.phPayMQSelfAd.Visible = false;
                }                
            }
            #endregion

            #region 基本信息
            this.txtSelfCompanyName.Value = cInfo.CompanyName;
            this.txtSelfContactMobile.Text = uInfo.ContactInfo.Mobile;
            this.txtSelfContactName.Text = uInfo.ContactInfo.ContactName;
            this.txtSelfContactTel.Text = uInfo.ContactInfo.Tel;
            this.rblSelfGender.SelectedIndex = this.rblSelfGender.Items.IndexOf(this.rblSelfGender.Items.FindByValue(((int)uInfo.ContactInfo.ContactSex).ToString()));
            #endregion
        }
        #endregion

        #region 初始化对方的名片信息
        /// <summary>
        /// 初始化对方的名片信息
        /// </summary>
        private void InitOtherSideCard()
        {
            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(this.OtherSideMQ);

            if (model == null||model.Company==null||model.User==null) return;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo cInfo=model.Company;
            EyouSoft.Model.CompanyStructure.CompanyUser uInfo=model.User;

            bool isHighShop=cInfo.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.HighShop);
            bool isPayMQ=cInfo.StateMore.CompanyService.IsServiceAvailable(EyouSoft.Model.CompanyStructure.SysService.MQ);

            #region 网店链接
            string otherSideEShop = "<div class=\"{0}\"><a href=\"{1}\" target=\"_blank\"></a></div>";
            EyouSoft.Model.CompanyStructure.CompanyType cType = this.GetCompanyType(cInfo.CompanyRole.RoleItems);

            switch (cType)
            {
                case EyouSoft.Model.CompanyStructure.CompanyType.全部:
                //case EyouSoft.Model.CompanyStructure.CompanyType.地接:
                case EyouSoft.Model.CompanyStructure.CompanyType.组团:
                    otherSideEShop = string.Empty;
                    break;
                default:
                    otherSideEShop = string.Format(otherSideEShop, isHighShop ? "cardshopgj" : "cardshop", Utils.GetCompanyDomain(cInfo.ID, cType));
                    break;
            }

            this.ltrOtherSideEShop.Text = otherSideEShop;
            #endregion

            #region 企业LOGO、主营、MQ广告
            if (isPayMQ)
            {
                string otherSideLogo = string.Empty;
                string otherSideArea =string.Empty;
                string otherSideAd = string.Empty;
                
                if (cInfo.AttachInfo.CompanyLogo!=null&&!string.IsNullOrEmpty(cInfo.AttachInfo.CompanyLogo.ImagePath))
                {
                    otherSideLogo = string.Format("<img src=\"{0}\" width=\"140\" height=\"62\" />", Domain.FileSystem + cInfo.AttachInfo.CompanyLogo.ImagePath);
                }

                if (cInfo.AttachInfo.CompanyMQAdv != null && !string.IsNullOrEmpty(cInfo.AttachInfo.CompanyMQAdv.ImagePath))
                {
                    string s1=@"<a href=""{0}"" target=""_blank"" id=""otherAdvLink_a""><img src=""{1}""  style=""width:140px; height:140px;""  border=""0"" /></a>";
                    string s2=@"<img src=""{0}""  style=""width:140px; height:140px;""  border=""0"" />";
                    string s=string.Empty;
                    if (string.IsNullOrEmpty(cInfo.AttachInfo.CompanyMQAdv.ImageLink))
                    {
                        s = string.Format(s2, Domain.FileSystem+cInfo.AttachInfo.CompanyMQAdv.ImagePath);
                    }
                    else
                    {
                        s = string.Format(s1, cInfo.AttachInfo.CompanyMQAdv.ImageLink, Domain.FileSystem+cInfo.AttachInfo.CompanyMQAdv.ImagePath);
                    }
                    otherSideAd = string.Format(@"<tr id=""other_advtr"" ><td style=""padding-top: 5px;"" align=""center"">{0}</td></tr>" , s);

                    string areas = this.GetAreaNames(uInfo.Area);
                    otherSideArea = string.Format("<span title=\"{0}\">{1}</span>", areas, Utils.GetText(areas, 30, true));
                }
                else
                {
                    string areas=this.GetAreaNames(uInfo.Area);
                    otherSideArea = string.Format("<span title=\"{0}\">{1}</span>", areas, Utils.GetText(areas, 60, true));
                }

                this.ltrOtherSideLogo.Text = otherSideLogo;
                this.ltrOtherSideArea.Text = otherSideArea;
                this.ltrOtherSideAd.Text = otherSideAd;

                this.phOtherSideAreas.Visible = this.phOtherSideLogo.Visible = true;
            }
            else
            {
                this.phOtherSideAreas.Visible = this.phOtherSideLogo.Visible = this.ltrOtherSideAd.Visible = false;
            }
            #endregion

            #region 基本信息
            this.lblOtherSideContactName.Text = uInfo.ContactInfo.ContactName;
            string gender = "";
            switch (uInfo.ContactInfo.ContactSex)
            {
                case EyouSoft.Model.CompanyStructure.Sex.男: gender = "先生"; break;
                case EyouSoft.Model.CompanyStructure.Sex.女: gender = "女士"; break;
            }
            this.lblOtherSideContactGender.Text = gender;
            this.txtOtherSideCompanyName.Value = cInfo.CompanyName;
            this.ltrOtherSideTelephone.Text = uInfo.ContactInfo.Tel;
            this.ltrOtherSideMobile.Text = uInfo.ContactInfo.Mobile;
            #endregion
        }

        /// <summary>
        /// 获取公司类型
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        private EyouSoft.Model.CompanyStructure.CompanyType GetCompanyType(EyouSoft.Model.CompanyStructure.CompanyType[] types)
        {
            
            EyouSoft.Model.CompanyStructure.CompanyType cType = EyouSoft.Model.CompanyStructure.CompanyType.全部;

            if (types.Length == 1)
            {
                cType = types[0];
            }
            else if (types.Contains(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                cType = EyouSoft.Model.CompanyStructure.CompanyType.专线;
            }
            else if (types.Contains(EyouSoft.Model.CompanyStructure.CompanyType.地接))
            {
                cType = EyouSoft.Model.CompanyStructure.CompanyType.地接;
            }

            return cType;
        }

        /// <summary>
        /// 获取经营区域字符串
        /// </summary>
        /// <param name="areas"></param>
        /// <returns></returns>
        private string GetAreaNames(IList<EyouSoft.Model.SystemStructure.AreaBase> areas)
        {
            StringBuilder s = new StringBuilder();

            if (areas != null && areas.Count > 0)
            {
                s.Append(areas[0].AreaName);

                for (int i = 1; i < areas.Count; i++)
                {
                    s.AppendFormat(",{0}",areas[i].AreaName);
                }
            }

            return s.ToString();
        }
        #endregion

        #region 保存按钮Click事件
        /// <summary>
        /// 保存按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.CompanyStructure.CompanyLogo logoInfo = new EyouSoft.Model.CompanyStructure.CompanyLogo();
            logoInfo.ImagePath = Utils.GetFormValue("FSelfLogo$hidFileName");

            EyouSoft.Model.CompanyStructure.CompanyMQAdv adInfo = new EyouSoft.Model.CompanyStructure.CompanyMQAdv();
            adInfo.ImagePath = Utils.GetFormValue("FSelfAd$hidFileName");
            adInfo.ImageLink = Utils.GetFormValue("txtPayMQSelfAdUrl").Trim();

            EyouSoft.IBLL.CompanyStructure.ICompanyUser ubll= EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanyUser uInfo=ubll.GetModel(this.SiteUserInfo.ID);

            string contactName = Utils.GetFormValue(this.txtSelfContactName.UniqueID); //Utils.InputText(this.txtSelfContactName.Text);
            int contactGender = Utils.GetInt(Utils.GetFormValue(this.rblSelfGender.UniqueID), 2); //Utils.GetInt(this.rblSelfGender.SelectedValue, 2);
            string contactTelephone = Utils.GetFormValue(this.txtSelfContactTel.UniqueID); //Utils.InputText(this.txtSelfContactTel.Text);
            string contactMobile = Utils.GetFormValue(this.txtSelfContactMobile.UniqueID); //Utils.InputText(this.txtSelfContactMobile.Text);

            uInfo.ContactInfo.ContactName = string.IsNullOrEmpty(contactName) ? uInfo.ContactInfo.ContactName : contactName;
            uInfo.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)contactGender;
            uInfo.ContactInfo.Tel = string.IsNullOrEmpty(contactTelephone) ? uInfo.ContactInfo.Tel : contactTelephone;
            uInfo.ContactInfo.Mobile = string.IsNullOrEmpty(contactMobile) ? uInfo.ContactInfo.Mobile : contactMobile;

            ubll.UpdatePersonal(uInfo);

            if (!string.IsNullOrEmpty(logoInfo.ImagePath))
            {
                EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyLogo(uInfo.CompanyID, logoInfo);
            }

            if (!string.IsNullOrEmpty(adInfo.ImagePath)||!string.IsNullOrEmpty(adInfo.ImageLink))
            {
                if (string.IsNullOrEmpty(adInfo.ImagePath))
                {
                    adInfo.ImagePath = this.txtSelfCurrentAdImgPath.Value;
                }

                if (!string.IsNullOrEmpty(adInfo.ImageLink) && !adInfo.ImageLink.StartsWith("http://"))
                {
                    adInfo.ImageLink = "http://" + adInfo.ImageLink;
                }

                EyouSoft.BLL.CompanyStructure.CompanyAttachInfo.CreateInstance().SetCompanyMQAdv(uInfo.CompanyID, adInfo);
            }

            Response.Redirect(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
        }
        #endregion
    }
}
