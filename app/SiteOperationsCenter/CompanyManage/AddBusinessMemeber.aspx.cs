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
using System.Collections.Generic;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Model.CompanyStructure;

namespace SiteOperationsCenter.CompanyManage
{
    /// <summary>
    /// 功能 商家会员修改
    /// 2011-12-16 蔡永辉
    /// </summary>
    public partial class AddBusinessMemeber : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 修改的公司ID
        /// </summary>
        protected string EditId = "";
        protected string OtherSaleCity = "";
        protected string returnUrl = "";
        protected StringBuilder strQualification = null;
        protected StringBuilder MySaleCity = null;
        #region 省份
        private int _setProvinceId = 0;
        /// <summary>
        /// 默认选中省份ID
        /// </summary>
        public int SetProvinceId
        {
            get { return _setProvinceId; }
            set { _setProvinceId = value; }
        }
        private int _setCityId = 0;
        /// <summary>
        /// 默认选中城市ID
        /// </summary>
        public int SetCityId
        {
            get { return _setCityId; }
            set { _setCityId = value; }
        }
        private int _setCountyId = 0;
        /// <summary>
        /// 默认选中县区ID
        /// </summary>
        public int SetCountyId
        {
            get { return _setCountyId; }
            set { _setCountyId = value; }
        }

        private bool _isShowRequired = false;
        /// <summary>
        /// 是否显示必填（*）信息
        /// </summary>
        public bool IsShowRequired
        {
            get { return _isShowRequired; }
            set { _isShowRequired = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            EditId = Request.QueryString["EditId"];//要修改ID
            returnUrl = Request.QueryString["returnUrl"];//修改成功后，返回的地址
            #region 初始化省份
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.dropProvinceId.ClientID + "');");
            BindProvince();
            //BindCity();
            #endregion
            BindSearchDropdownlist();


            if (Request.QueryString["type"] != null)
            {
                string type = Request.QueryString["type"].ToString();
                //获的省份/城市下的线路区域
                if (type.Equals("GetTourArea", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    Response.Write(GetTourAreaList());
                    Response.End();
                }
            }
            if (!IsPostBack)
            {
                GetCompanyInfo();
            }
        }

        #region 绑定省份和城市
        //绑定省份
        private void BindProvince()
        {
            this.dropProvinceId.Items.Insert(0, new ListItem("请选择", "0"));
            this.dropProvinceId.Attributes.Add("onchange", string.Format("ChangeList('" + this.dropCityId.ClientID + "','" + this.dropCountyId.ClientID + "', this.options[this.selectedIndex].value)"));
            this.dropCityId.Items.Insert(0, new ListItem("请选择", "0"));
            //this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "',this.options[this.selectedIndex].value，'" + this.ddl_ProvinceList.ClientID + "')"));
            this.dropCityId.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.dropCountyId.ClientID + "', this.options[this.selectedIndex].value,'" + this.dropProvinceId.ClientID + "')"));
            this.dropCountyId.Items.Insert(0, new ListItem("请选择", "0"));
        }

        #endregion

        #region 初始化商家信息
        /// <summary>
        /// 初始化公司信息
        /// </summary>
        protected void GetCompanyInfo()
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(EditId);
            if (Model != null)
            {
                txtCompanyName.Value = Model.CompanyName;//单位名称
                txtCompanyWebsite.Value = Model.WebSite;//公司网站
                txtCompanyOtherName.Value = Model.Introduction;//公司简称
                txtCompanyBrand.Value = Model.CompanyBrand;//公司品牌
                dropCompanyScale.SelectedValue = ((int)Model.Scale).ToString();//公司规模
                txtLicenseNumber.Value = Model.License;// 许可证
                #region 公司联系人
                txtContactName.Value = Model.ContactInfo.ContactName;
                txtContactTel.Value = Model.ContactInfo.Tel;
                txtContactMobile.Value = Model.ContactInfo.Mobile;
                txtContactFax.Value = Model.ContactInfo.Fax;
                txtContactEmail.Value = Model.ContactInfo.Email;
                #endregion


                txtPeerContact.Value = Model.PeerContact;
                txtAlipayAccount.Value = Model.AlipayAccount;
                txtOfficeAddress.Value = Model.CompanyAddress;
                txt_Description.Value = Model.Remark;
                txt_txtBusinessSuperior.Value = Model.ShortRemark;
                BindQualification(Model.Qualification);
                txtPeerContact.Value = Model.PeerContact;
                dropUserGrade.SelectedValue = ((int)Model.CompanyLev).ToString();
                X.InnerText = Model.Latitude.ToString();
                Y.InnerText = Model.Longitude.ToString();

                #region 初始化省份
                //省份 城市
                if (Model.ProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.dropProvinceId.ClientID + "','" + Model.ProvinceId + "');", true);
                    if (Model.CityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.dropCityId.ClientID + "','" + Model.ProvinceId + "','" + Model.CityId + "');", true);
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.dropCountyId.ClientID + "','" + Model.ProvinceId + "','" + Model.CityId + "','" + Model.CountyId + "');", true);
                    }
                    else
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetList('" + this.dropCityId.ClientID + "','" + Model.ProvinceId + "','" + Model.CityId + "');", true);
                }
                #endregion

                //经营范围
                EyouSoft.Model.CompanyStructure.CompanyType[] TypeItems = Model.CompanyRole.RoleItems;
                bool isManage = CheckMasterGrant(EyouSoft.Common.YuYingPermission.会员管理_管理该栏目);
                string TypeId = "";
                #region 公司类型
                for (int i = 0; i < TypeItems.Length; i++)
                {
                    if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                    {

                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        TypeId = "1";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.组团)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        if (TypeId != "")
                        {
                            TypeId = TypeId + ",2";
                        }
                        else
                        {
                            TypeId = "2";
                        }
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅行社汇总管理_修改, true);
                            return;
                        }
                        if (TypeId != "")
                        {
                            TypeId = TypeId + ",3";
                        }
                        else
                        {
                            TypeId = "3";
                        }
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.景区汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.景区汇总管理_修改, true);
                            return;
                        }

                        TypeId = "4";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.酒店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.酒店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.酒店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "5";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.车队)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.车队汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.车队汇总管理_修改, true);
                            return;
                        }
                        TypeId = "6";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.旅游用品店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.旅游用品店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "7";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.购物店)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.购物店汇总管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.购物店汇总管理_修改, true);
                            return;
                        }
                        TypeId = "8";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.机票供应商)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.机票供应商管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.机票供应商管理_修改, true);
                            return;
                        }
                        TypeId = "9";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.其他采购商管理_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.其他采购商管理_修改, true);
                            return;
                        }
                        TypeId = "10";
                    }
                    else if (TypeItems[i] == EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛)
                    {
                        if (!isManage || !CheckMasterGrant(EyouSoft.Common.YuYingPermission.随便逛逛_修改))
                        {
                            Utils.ResponseNoPermit(EyouSoft.Common.YuYingPermission.随便逛逛_修改, true);
                            return;
                        }
                        TypeId = "11";
                    }
                }
                #endregion

                EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, "$(function(){Edit.SetCompanyType('" + TypeId + "');})");

                #region 销售区域
                if (TypeId.Contains("1")) //专线商
                {
                    MessageBox.ResponseScript(this.Page, "$(\"#tbCompanyInfo\").find(\"input[type='checkbox'][name='ckCompanyType'][value='1']\").attr(\"checked\",\"checked\")");
                    //销售城市
                    IList<EyouSoft.Model.SystemStructure.CityBase> SaleCityList = Model.SaleCity;
                    MySaleCity = new StringBuilder();
                    if (SaleCityList != null && SaleCityList.Count > 0)
                    {
                        int Count = 0;
                        foreach (EyouSoft.Model.SystemStructure.CityBase item in SaleCityList)
                        {
                            Count++;
                            if (Count % 13 == 0)
                            {
                                MySaleCity.Append("<br/>");
                            }
                            MySaleCity.AppendFormat("<input type='checkbox' id='SaleCity_{0}' value='{0}' checked='checked' name='ckSellCity'/><label for='SaleCity_{0}'>{1}</label>", item.CityId, item.CityName);
                        }
                    }
                    SaleCityList = null;
                    //输入的销售城市
                    string strSaleCity = EyouSoft.BLL.CompanyStructure.CompanyUnCheckedCity.CreateInstance().GetSaleCity(EditId);
                    if (!string.IsNullOrEmpty(strSaleCity))
                    {
                        OtherSaleCity = string.Format("<tr  class=\"lr_hangbg\"><td align=\"right\" class=\"lr_shangbg\">待增加的销售城市：</td><td colspan=\"2\">{0}</td></tr>", strSaleCity);
                    }

                }

                if (TypeId.Contains("3") || TypeId.Contains("1"))
                {
                    //经营线路区域
                    IList<EyouSoft.Model.SystemStructure.AreaBase> AreaList = Model.Area;
                    StringBuilder MyAreaList = new StringBuilder();
                    if (AreaList != null && AreaList.Count > 0)
                    {
                        foreach (EyouSoft.Model.SystemStructure.AreaBase item in AreaList)
                        {
                            MyAreaList.Append(item.AreaId + ",");
                        }

                    }
                    if (MySaleCity != null)
                        MessageBox.ResponseScript(this.Page, "$(function(){Edit.GetSellCity(\"" + MySaleCity.ToString() + "\",'" + MyAreaList.ToString() + "');})");
                    else
                        MessageBox.ResponseScript(this.Page, "$(function(){Edit.GetSellCity(\"\",'" + MyAreaList.ToString() + "');})");
                    AreaList = null;
                }
                #endregion

                //品牌logo
                if (Model.AttachInfo.CompanyLogo != null)
                {
                    string OldSfpBrandLogo = Model.AttachInfo.CompanyLogo.ImagePath;
                    if (!string.IsNullOrEmpty(OldSfpBrandLogo))
                    {
                        this.ltrBrandLogo.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldSfpBrandLogo, OldSfpBrandLogo);
                        this.hidCompanyLogoImg.Value = OldSfpBrandLogo + "|" + Model.AttachInfo.CompanyLogo.ThumbPath;
                    }
                }

                #region 证书
                EyouSoft.Model.CompanyStructure.BusinessCertif Buiness = Model.AttachInfo.BusinessCertif;
                if (Buiness != null)
                {

                    string OldLicenceImg = Buiness.LicenceImg;
                    string OldBusinessCertImg = Buiness.BusinessCertImg;
                    string OldTaxRegImg = Buiness.TaxRegImg;
                    string OldPersonCardImg = Buiness.PersonCardImg;
                    string OldWarrantImg = Buiness.WarrantImg;
                    if (!string.IsNullOrEmpty(OldLicenceImg))
                    {
                        this.ltrLicenceImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldLicenceImg, OldLicenceImg);
                        this.hidOldLicenceImg.Value = OldLicenceImg;
                    }
                    if (!string.IsNullOrEmpty(OldBusinessCertImg))
                    {
                        this.ltrBusinessCertImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldBusinessCertImg, OldBusinessCertImg);
                        this.hidOldBusinessCertImg.Value = OldBusinessCertImg;
                    }
                    if (!string.IsNullOrEmpty(OldTaxRegImg))
                    {
                        this.ltrTaxRegImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldTaxRegImg, OldTaxRegImg);
                        this.hidOldTaxRegImg.Value = OldTaxRegImg;
                    }

                    if (!string.IsNullOrEmpty(OldPersonCardImg))
                    {
                        this.ltrCardsImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldPersonCardImg, OldPersonCardImg);
                        this.hidOldAuthorizeImg.Value = OldTaxRegImg;
                    }
                    if (!string.IsNullOrEmpty(OldWarrantImg))
                    {
                        this.ltrAuthorizeImg.Text = string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + OldWarrantImg, OldWarrantImg);
                        this.hidOldCardsImg.Value = OldTaxRegImg;
                    }
                }
                Buiness = null;
                #endregion


                //公司照片

                if (Model.AttachInfo.CompanyPublicityPhoto != null && Model.AttachInfo.CompanyPublicityPhoto.Count > 0)
                {
                    foreach (CompanyPublicityPhoto modelimg in Model.AttachInfo.CompanyPublicityPhoto)
                    {
                        switch (modelimg.PhotoIndex)
                        {
                            case 1:
                                this.ltrcompanyimg1.Text += string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + modelimg.ImagePath, modelimg.ImagePath);
                                this.hidCompanyImg1.Value += modelimg.ImagePath + "|";
                                this.hidCompanyImg1.Value = this.hidCompanyImg1.Value.TrimEnd('|');
                                break;
                            case 2:
                                this.ltrcompanyimg2.Text += string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + modelimg.ImagePath, modelimg.ImagePath);
                                this.hidCompanyImg2.Value += modelimg.ImagePath + "|";
                                this.hidCompanyImg2.Value = this.hidCompanyImg2.Value.TrimEnd('|');
                                break;
                            case 3:
                                this.ltrcompanyimg3.Text += string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + modelimg.ImagePath, modelimg.ImagePath);
                                this.hidCompanyImg3.Value += modelimg.ImagePath + "|";
                                this.hidCompanyImg3.Value = this.hidCompanyImg3.Value.TrimEnd('|');
                                break;
                        }
                    }
                }

                #region 公司银行帐户
                List<EyouSoft.Model.CompanyStructure.BankAccount> BankList = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
                BankList = Model.BankAccounts;
                IList<EyouSoft.Model.CompanyStructure.BankAccount> oneselfBank = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
                if (BankList != null && BankList.Count > 0)
                {
                    foreach (EyouSoft.Model.CompanyStructure.BankAccount Bank in BankList)
                    {
                        if (Bank.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司)
                        {
                            this.txtCompanyBackName.Value = Bank.BankAccountName;
                            this.txtCompanyBack.Value = Bank.BankName;
                            this.txtCompanyBackNumber.Value = Bank.AccountNumber;
                        }
                        else
                        {
                            oneselfBank.Add(Bank);
                        }
                    }

                }
                if (oneselfBank != null && oneselfBank.Count > 0)
                {
                    this.RepeaterBank.DataSource = oneselfBank;
                    this.RepeaterBank.DataBind();
                }
                oneselfBank = null;
                BankList = null;
                #endregion

            }
            else
            {
                //EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "未找到该会员信息!", returnUrl);
                return;
            }



            txt_MQ.Value = Model.ContactInfo.MQ;
            txt_QQ.Value = Model.ContactInfo.QQ;
            txt_MSN.Value = Model.ContactInfo.MSN;
            DdlB2B.SelectedValue = ((int)Model.B2BDisplay).ToString();
            DdlB2C.SelectedValue = ((int)Model.B2CDisplay).ToString();
            txt_B2BOrder.Value = Model.B2BSort == 0 ? "50" : Model.B2BSort.ToString();
            txt_B2COrder.Value = Model.B2CSort == 0 ? "50" : Model.B2CSort.ToString();
            txtStartDate.Value = Model.ContractStart == null ? "" : Convert.ToDateTime(Model.ContractStart).ToShortDateString();
            txtEndDate.Value = Model.ContractEnd == null ? "" : Convert.ToDateTime(Model.ContractEnd).ToShortDateString();
            lal_ClickNum.Text = Model.ClickNum.ToString();
            lal_InfoFull.Text = Utils.FilterEndOfTheZeroDecimal(Model.InfoFull * 100) + "%";
            Model = null;
        }
        #endregion

        #region 获的省份/城市下的线路区域
        /// <summary>
        /// 获的省份/城市下的线路区域
        /// </summary>
        /// <returns></returns>
        protected string GetTourAreaList()
        {
            StringBuilder strTourAreaLis = new StringBuilder();
            string CityName = EyouSoft.Common.Utils.InputText(Request.QueryString["CityName"]);
            strTourAreaLis.AppendFormat("<table  cellspacing=\"0\" cellpadding=\"5\" border=\"0\" align=\"center\" width=\"900\" style=\"border: 1px solid rgb(197, 197, 197); background: none repeat scroll 0% 0% rgb(249, 249, 249);\" class=\"margin10\">");
            strTourAreaLis.AppendFormat("<tr><td align=\"left\"><img src=\"{0}\"></td></tr>", ImageServerUrl + "/images/yunying/jingyingquyu.gif");
            strTourAreaLis.Append("<tr><td>");
            //国内长线
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList1 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内长线);
            if (AreaList1 != null && AreaList1.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\" onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" > <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内长线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList1)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area_1\" /><label for=\"Area_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            AreaList1 = null;
            //国际线
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList2 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国际线);
            if (AreaList2 != null && AreaList2.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table  onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国际线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList2)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area_2\" /><label for=\"Area2_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            //国内短线
            IList<EyouSoft.Model.SystemStructure.SysArea> AreaList3 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaList(EyouSoft.Model.SystemStructure.AreaType.国内短线);
            if (AreaList3 != null && AreaList3.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\" cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr ><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内短线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr>");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList3)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input id=\"Area_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area_3\" /><label for=\"Area3_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr>");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            strTourAreaLis.Append("</td></tr></table>");
            AreaList3 = null;


            return strTourAreaLis.ToString();

        }
        #endregion

        #region 验证邮箱是否存在
        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        private bool IsExistEmail(string Email)
        {
            bool isTrue = false;
            isTrue = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().IsExistsEmail(Email);
            return isTrue;
        }
        #endregion

        #region 修改公司信息
        /// <summary>
        /// 修改公司信息
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            //省份ID
            int ProvinceId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropProvinceId.UniqueID));
            //城市ID
            int CityId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropCityId.UniqueID));
            //县区ID
            int CountyId = EyouSoft.Common.Utils.GetInt(Utils.GetFormValue(dropCountyId.UniqueID));
            //公司网址
            string txtWebSite = Utils.GetFormValue(txtCompanyWebsite.UniqueID);
            //公司名称
            string txtCompanyName = Utils.InputText(this.txtCompanyName.Value.Trim());
            //公司简介
            string CompanyOtherName = Utils.GetFormValue(txtCompanyOtherName.UniqueID);
            //品牌名
            string CompanyBrand = Utils.GetFormValue(txtCompanyBrand.UniqueID);

            //许可证号
            string txtLicenseNumber = Utils.InputText(this.txtLicenseNumber.Value.Trim());

            //支付宝账号
            string AlipayAccount = Utils.GetFormValue(txtAlipayAccount.UniqueID);

            //同业联系方式
            string PeerContact = Utils.GetFormValue(txtPeerContact.UniqueID);


            //经营范围
            string CompanyType = Utils.InputText(Request.Form["radManageArea"]);
            if (CompanyType == "")
            {
                //旅行社
                CompanyType = Utils.InputText(Request.Form["ckCompanyType"]);

                //专线商时有销售城市及经营线路区域
            }



            //公司介绍
            string txtCompanyInfo = Utils.EditInputText(this.txt_Description.Value.Trim());
            //业务优势
            string txtBusinessSuperior = Utils.InputText(this.txt_txtBusinessSuperior.Value.Trim());
            #region  公司银行账户
            string txtCompanyBackName = Utils.InputText(this.txtCompanyBackName.Value.Trim());
            string txtCompanyBack = Utils.InputText(this.txtCompanyBack.Value.Trim());
            string txtCompanyBackNumber = Utils.InputText(this.txtCompanyBackNumber.Value.Trim());
            #endregion

            #region 个人银行账户
            string[] txtBankAccountName = Utils.GetFormValues("txtBankAccountName");
            string[] txtBankName = Utils.GetFormValues("txtBankName");
            string[] txtAccountNumber = Utils.GetFormValues("txtAccountNumber");
            #endregion

            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();

            #region 公司信息
            /*公司信息*/
            model.ID = EditId;
            model.CompanyName = txtCompanyName;
            model.ProvinceId = ProvinceId;
            model.CityId = CityId;
            model.CountyId = CountyId;
            model.CompanyAddress = txtOfficeAddress.Value;
            model.CompanyBrand = txtCompanyBrand.Value;
            model.License = txtLicenseNumber;
            model.Remark = txtCompanyInfo;
            model.ShortRemark = txtBusinessSuperior;
            model.WebSite = txtWebSite;
            model.CompanyBrand = CompanyBrand;
            model.Introduction = CompanyOtherName;
            model.AlipayAccount = AlipayAccount;
            model.PeerContact = PeerContact;
            #endregion

            #region 品牌logo
            /*品牌logo*/
            CompanyLogo Logo = new CompanyLogo();
            string BrandLogo = Request.Form["SfpBrandLogo$hidFileName"];
            if (string.IsNullOrEmpty(BrandLogo) && this.hidCompanyLogoImg.Value != "")
            {
                BrandLogo = this.hidCompanyLogoImg.Value;
            }
            string[] listlogo = BrandLogo.Split('|');
            if (listlogo.Length > 1)
            {
                Logo.ImagePath = listlogo[0];
                Logo.ThumbPath = listlogo[1];
            }
            #endregion

            #region 证书管理
            EyouSoft.Model.CompanyStructure.CompanyAttachInfo AttachInfo = new EyouSoft.Model.CompanyStructure.CompanyAttachInfo();
            EyouSoft.Model.CompanyStructure.BusinessCertif Buiness = new EyouSoft.Model.CompanyStructure.BusinessCertif();
            string LicenceImg = Request.Form["SingleFilelLicence$hidFileName"];
            if (string.IsNullOrEmpty(LicenceImg) && this.hidOldLicenceImg.Value != "")
            {
                LicenceImg = this.hidOldLicenceImg.Value;
            }
            string BusinessCertImg = Request.Form["SingleFileBusinessCertImg$hidFileName"];
            if (string.IsNullOrEmpty(BusinessCertImg) && this.hidOldBusinessCertImg.Value != "")
            {
                BusinessCertImg = this.hidOldBusinessCertImg.Value;
            }
            string TaxRegImg = Request.Form["SingleFileTaxRegImg$hidFileName"];
            if (string.IsNullOrEmpty(TaxRegImg) && this.hidOldTaxRegImg.Value != "")
            {
                TaxRegImg = this.hidOldTaxRegImg.Value;
            }
            string AuthorizeImg = Request.Form["SingleFileAuthorizeImg$hidFileName"];
            if (string.IsNullOrEmpty(AuthorizeImg) && this.hidOldAuthorizeImg.Value != "")
            {
                AuthorizeImg = this.hidOldAuthorizeImg.Value;
            }
            string CardsImg = Request.Form["SingleFileCardsImg$hidFileName"];
            if (string.IsNullOrEmpty(CardsImg) && this.hidOldCardsImg.Value != "")
            {
                CardsImg = this.hidOldCardsImg.Value;
            }

            Buiness.LicenceImg = LicenceImg;
            Buiness.BusinessCertImg = BusinessCertImg;
            Buiness.TaxRegImg = TaxRegImg;
            Buiness.WarrantImg = AuthorizeImg;
            Buiness.PersonCardImg = CardsImg;
            #endregion

            #region 公司图片
            /*公司图片*/
            string Companyimg1 = Utils.GetFormValue(Sfpcompanyimg1.FindControl("hidFileName").UniqueID);
            string Companyimg2 = Utils.GetFormValue(Sfpcompanyimg2.FindControl("hidFileName").UniqueID);
            string Companyimg3 = Utils.GetFormValue(Sfpcompanyimg3.FindControl("hidFileName").UniqueID);
            List<CompanyPublicityPhoto> listPublicityPhoto = new List<CompanyPublicityPhoto>();
            if (string.IsNullOrEmpty(Companyimg1) && this.hidCompanyImg1.Value != "")
            {
                if (this.hidCompanyImg1.Value.Contains('|'))
                {
                    foreach (string strimg in this.hidCompanyImg1.Value.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(strimg))
                        {
                            string[] listPath = strimg.Split('$');
                            if (listPath.Length > 1)
                            {
                                CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                                modelPublicityPhoto.ImagePath = listPath[0];
                                modelPublicityPhoto.ThumbPath = listPath[1];
                                modelPublicityPhoto.PhotoIndex = 1;
                                listPublicityPhoto.Add(modelPublicityPhoto);
                                modelPublicityPhoto = null;
                            }
                        }
                    }
                }
                else
                {
                    string[] listPath = hidCompanyImg1.Value.Split('$');
                    if (listPath.Length > 1)
                    {
                        CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                        modelPublicityPhoto.ImagePath = listPath[0];
                        modelPublicityPhoto.ThumbPath = listPath[1];
                        modelPublicityPhoto.PhotoIndex = 1;
                        listPublicityPhoto.Add(modelPublicityPhoto);
                    }
                }
            }
            else
            {
                string[] listPath = Companyimg1.Split('|');
                if (listPath.Length > 1)
                {
                    CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                    modelPublicityPhoto.ImagePath = listPath[0];
                    modelPublicityPhoto.ThumbPath = listPath[1];
                    modelPublicityPhoto.PhotoIndex = 1;
                    listPublicityPhoto.Add(modelPublicityPhoto);
                    modelPublicityPhoto = null;
                }
            }

            if (string.IsNullOrEmpty(Companyimg2) && this.hidCompanyImg2.Value != "")
            {
                if (this.hidCompanyImg2.Value.Contains('|'))
                {
                    foreach (string strimg in this.hidCompanyImg2.Value.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(strimg))
                        {
                            string[] listPath = strimg.Split('$');
                            if (listPath.Length > 1)
                            {
                                CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                                modelPublicityPhoto.ImagePath = listPath[0];
                                modelPublicityPhoto.ThumbPath = listPath[1];
                                modelPublicityPhoto.PhotoIndex = 2;
                                listPublicityPhoto.Add(modelPublicityPhoto);
                                modelPublicityPhoto = null;
                            }
                        }
                    }
                }
                else
                {
                    string[] listPath = hidCompanyImg2.Value.Split('$');
                    if (listPath.Length > 1)
                    {
                        CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                        modelPublicityPhoto.ImagePath = listPath[0];
                        modelPublicityPhoto.ThumbPath = listPath[1];
                        modelPublicityPhoto.PhotoIndex = 2;
                        listPublicityPhoto.Add(modelPublicityPhoto);
                    }
                }
            }
            else
            {
                string[] listPath = Companyimg2.Split('|');
                if (listPath.Length > 1)
                {
                    CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                    modelPublicityPhoto.ImagePath = listPath[0];
                    modelPublicityPhoto.ThumbPath = listPath[1];
                    modelPublicityPhoto.PhotoIndex = 2;
                    listPublicityPhoto.Add(modelPublicityPhoto);
                    modelPublicityPhoto = null;
                }
            }

            if (string.IsNullOrEmpty(Companyimg3) && this.hidCompanyImg3.Value != "")
            {
                if (this.hidCompanyImg3.Value.Contains('|'))
                {
                    foreach (string strimg in this.hidCompanyImg3.Value.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(strimg))
                        {
                            string[] listPath = strimg.Split('$');
                            if (listPath.Length > 1)
                            {
                                CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                                modelPublicityPhoto.ImagePath = listPath[0];
                                modelPublicityPhoto.ThumbPath = listPath[1];
                                modelPublicityPhoto.PhotoIndex = 3;
                                listPublicityPhoto.Add(modelPublicityPhoto);
                                modelPublicityPhoto = null;
                            }
                        }
                    }
                }
                else
                {
                    string[] listPath = hidCompanyImg3.Value.Split('$');
                    if (listPath.Length > 1)
                    {
                        CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                        modelPublicityPhoto.ImagePath = listPath[0];
                        modelPublicityPhoto.ThumbPath = listPath[1];
                        modelPublicityPhoto.PhotoIndex = 3;
                        listPublicityPhoto.Add(modelPublicityPhoto);
                    }
                }
            }
            else
            {
                string[] listPath = Companyimg3.Split('|');
                if (listPath.Length > 1)
                {
                    CompanyPublicityPhoto modelPublicityPhoto = new CompanyPublicityPhoto();
                    modelPublicityPhoto.ImagePath = listPath[0];
                    modelPublicityPhoto.ThumbPath = listPath[1];
                    modelPublicityPhoto.PhotoIndex = 3;
                    listPublicityPhoto.Add(modelPublicityPhoto);
                    modelPublicityPhoto = null;
                }
            }

            #endregion
            AttachInfo.CompanyPublicityPhoto = listPublicityPhoto;
            AttachInfo.CompanyLogo = Logo;
            AttachInfo.BusinessCertif = Buiness;
            Buiness = null;
            Logo = null;
            listPublicityPhoto = null;
            model.AttachInfo = AttachInfo;
            AttachInfo = null;
            EyouSoft.Model.CompanyStructure.UserAccount UserModel = new EyouSoft.Model.CompanyStructure.UserAccount();
            #region 用户信息
            /*用户信息*/
            UserModel.UserName = "";
            UserModel.PassWordInfo.NoEncryptPassword = "";
            #endregion
            model.AdminAccount = UserModel;
            UserModel = null;

            EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactModel = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            #region 公司联系人信息
            /* 公司联系人信息 */
            ContactModel.ContactName = txtContactName.Value;
            ContactModel.Tel = txtContactTel.Value; ;
            ContactModel.Mobile = txtContactMobile.Value;
            ContactModel.Fax = txtContactFax.Value;
            ContactModel.QQ = txt_QQ.Value;
            ContactModel.MSN = txt_MSN.Value;
            ContactModel.Email = txtContactEmail.Value;
            #endregion
            model.ContactInfo = ContactModel;
            ContactModel = null;

            #region 公司身份
            /* 公司身份 */
            EyouSoft.Model.CompanyStructure.CompanyRole RoleMode = new EyouSoft.Model.CompanyStructure.CompanyRole();
            EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;
            //旅行社
            if (CompanyType == "0")
            {
                string[] strType = Request.Form.GetValues("ckCompanyType");

                //设置经营线路区域
                string[] strAreaListF = Utils.GetFormValues("checkbox_Area_1"); //国内长
                string[] strAreaListS = Utils.GetFormValues("checkbox_Area_2"); //国际
                string[] strAreaListT = Utils.GetFormValues("checkbox_Area_3"); //国内短
                List<EyouSoft.Model.SystemStructure.AreaBase> AreaList = new List<EyouSoft.Model.SystemStructure.AreaBase>();
                if (strAreaListF != null)
                {
                    for (int j = 0; j < strAreaListF.Length; j++)
                    {
                        EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                        item.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内长线;
                        item.AreaId = Convert.ToInt32(strAreaListF[j]);
                        AreaList.Add(item);
                        item = null;
                    }
                }
                if (strAreaListS != null)
                {
                    for (int j = 0; j < strAreaListS.Length; j++)
                    {
                        EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                        item.RouteType = EyouSoft.Model.SystemStructure.AreaType.国际线;
                        item.AreaId = Convert.ToInt32(strAreaListS[j]);
                        AreaList.Add(item);
                        item = null;
                    }
                }
                if (strAreaListT != null)
                {
                    for (int j = 0; j < strAreaListT.Length; j++)
                    {
                        EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                        item.RouteType = EyouSoft.Model.SystemStructure.AreaType.国内短线;
                        item.AreaId = Convert.ToInt32(strAreaListT[j]);
                        AreaList.Add(item);
                        item = null;
                    }
                }

                for (int i = 0; i < strType.Length; i++)
                {
                    if (strType[i] == "1")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.专线;

                        string[] strSalelist = Request.Form.GetValues("ckSellCity");
                        if (strSalelist != null)
                        {
                            //设置销售城市
                            List<EyouSoft.Model.SystemStructure.CityBase> SaleCity = new List<EyouSoft.Model.SystemStructure.CityBase>();
                            for (int j = 0; j < strSalelist.Length; j++)
                            {
                                EyouSoft.Model.SystemStructure.CityBase item = new EyouSoft.Model.SystemStructure.CityBase();
                                item.ProvinceId = ProvinceId;
                                item.CityId = EyouSoft.Common.Function.StringValidate.GetIntValue(strSalelist[j]);
                                SaleCity.Add(item);
                                item = null;
                            }
                            model.SaleCity = SaleCity;
                            SaleCity = null;
                        }
                    }
                    if (strType[i] == "2")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                    }
                    if (strType[i] == "3")
                    {
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                    }
                    RoleMode.SetRole(TypeEmnu);
                }
                model.Area = AreaList;
                AreaList = null;
            }
            else
            {
                switch (CompanyType)
                {
                    case "4":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.景区;
                        break;
                    case "5":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.酒店;
                        break;
                    case "6":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.车队;
                        break;
                    case "7":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.旅游用品店;
                        break;
                    case "8":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.购物店;
                        break;
                    case "9":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.机票供应商;
                        break;
                    case "10":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.其他采购商;
                        break;
                    case "11":
                        TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛;
                        break;
                }
                RoleMode.SetRole(TypeEmnu);
            }
            #endregion
            model.CompanyRole = RoleMode;
            RoleMode = null;

            #region 公司资质
            string[] list = Utils.GetFormValues("ckQualification");
            List<CompanyQualification> listQualification = new List<CompanyQualification>();
            foreach (string str in list)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    listQualification.Add((CompanyQualification)Enum.Parse(typeof(CompanyQualification), str));
                }
            }
            model.Qualification = listQualification;
            #endregion

            List<EyouSoft.Model.CompanyStructure.BankAccount> BankList = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
            #region 银行账号
            /* 公司银行账户信息 */
            EyouSoft.Model.CompanyStructure.BankAccount BankCompanyModel = new EyouSoft.Model.CompanyStructure.BankAccount();
            BankCompanyModel.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.公司;
            BankCompanyModel.BankAccountName = txtCompanyBackName;
            BankCompanyModel.BankName = txtCompanyBack;
            BankCompanyModel.AccountNumber = txtCompanyBackNumber;
            BankCompanyModel.CompanyID = EditId;
            BankList.Add(BankCompanyModel);
            BankCompanyModel = null;
            if (!string.IsNullOrEmpty(txtBankAccountName.ToString()))
            {
                /* 个人银行帐户信息 */
                for (int i = 0; i < txtBankAccountName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(txtBankAccountName[i]))
                    {
                        EyouSoft.Model.CompanyStructure.BankAccount BankModel = new EyouSoft.Model.CompanyStructure.BankAccount();
                        BankModel.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.个人;
                        BankModel.BankAccountName = txtBankAccountName[i];
                        BankModel.BankName = txtBankName[i];
                        BankModel.AccountNumber = txtAccountNumber[i];
                        BankModel.CompanyID = EditId;
                        BankList.Add(BankModel);
                        BankModel = null;
                    }
                }
            }
            #endregion
            model.BankAccounts = BankList;
            BankList = null;

            #region 销售城市
            List<EyouSoft.Model.SystemStructure.CityBase> SaleCityList = new List<EyouSoft.Model.SystemStructure.CityBase>();
            string CompanyType3 = Utils.GetFormValue("CompanyType3");
            if (!string.IsNullOrEmpty(CompanyType3))
            {
                foreach (string str in Utils.GetFormValues("ckSellCity"))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        EyouSoft.Model.SystemStructure.CityBase modelCityBase = new EyouSoft.Model.SystemStructure.CityBase();
                        modelCityBase.CityId = Utils.GetInt(str);
                        SaleCityList.Add(modelCityBase);
                    }
                }
                model.SaleCity = SaleCityList;
            }
            #endregion
            model.Longitude = Utils.GetDecimal(jingdu.Value);
            model.Latitude = Utils.GetDecimal(weidu.Value);
            model.B2BDisplay = (CompanyB2BDisplay)Enum.Parse(typeof(CompanyB2BDisplay), Utils.GetFormValue(DdlB2B.UniqueID));
            model.B2CDisplay = (CompanyB2CDisplay)Enum.Parse(typeof(CompanyB2CDisplay), Utils.GetFormValue(DdlB2C.UniqueID));
            model.B2BSort = Utils.GetInt(Utils.GetFormValue(txt_B2BOrder.UniqueID));
            model.B2CSort = Utils.GetInt(Utils.GetFormValue(txt_B2COrder.UniqueID));
            model.ContractStart = Utils.GetDateTimeNullable(Utils.GetFormValue(txtStartDate.UniqueID));

            model.ContractEnd = Utils.GetDateTimeNullable(Utils.GetFormValue(txtEndDate.UniqueID));
            model.CompanyLev = (CompanyLev)Utils.GetInt(Utils.GetFormValue(dropUserGrade.UniqueID));
            model.Scale = (CompanyScale)Utils.GetInt(Utils.GetFormValue(dropCompanyScale.UniqueID));
            //model.ClickNum = Utils.GetInt(Utils.GetFormValue(textfield.UniqueID));
            bool Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Update(model);
            if (Result)
            {
                if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "list")
                {
                    Response.Write("<script language='javascript'>alert('修改成功!');window.location.href='/CompanyManage/BusinessMemeberList.aspx';</script>");
                }
                else
                {
                    MessageBox.ResponseScript(Page, "alert('修改成功!');window.location.href='/CompanyManage/BusinessMemeberList.aspx';");
                    //EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this.Page, "修改成功!", "BusinessMemeberList.aspx");
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.Show(this.Page, "修改失败!");
            }
            model = null;

        }
        #endregion

        #region 公司资质
        protected void BindQualification(IList<CompanyQualification> list)
        {
            strQualification = new StringBuilder();
            //公司资质
            foreach (EnumObj modelenum in EnumObj.GetList(typeof(CompanyQualification)))
            {
                if (list != null && list.Count > 0)
                {
                    if (list.Contains((CompanyQualification)Enum.Parse(typeof(CompanyQualification), modelenum.Value)))
                    {
                        strQualification.AppendFormat("<input type='checkbox' id='Qualification_{0}' checked='checked' value='{0}' name='ckQualification'/><label for='Qualification_{0}'>{1}</label>", modelenum.Value, modelenum.Text);
                    }
                    else
                        strQualification.AppendFormat("<input type='checkbox' id='Qualification_{0}'  value='{0}' name='ckQualification'/><label for='Qualification_{0}'>{1}</label>", modelenum.Value, modelenum.Text);

                }
                else
                {
                    strQualification.AppendFormat("<input type='checkbox' id='Qualification_{0}'  value='{0}' name='ckQualification'/><label for='Qualification_{0}'>{1}</label>", modelenum.Value, modelenum.Text);
                }
            }
        }
        #endregion

        #region 绑定下拉列表
        protected void BindSearchDropdownlist()
        {
            //公司规模
            this.dropCompanyScale.DataSource = EnumObj.GetList(typeof(CompanyScale));
            this.dropCompanyScale.DataTextField = "Text";
            this.dropCompanyScale.DataValueField = "Value";
            this.dropCompanyScale.DataBind();

            //公司资质
            BindQualification(null);

            //公司等级
            this.dropUserGrade.DataSource = EnumObj.GetList(typeof(CompanyLev));
            this.dropUserGrade.DataTextField = "Text";
            this.dropUserGrade.DataValueField = "Value";
            this.dropUserGrade.DataBind();
            this.dropUserGrade.Items.Insert(0, new ListItem("请选择", "-1"));

            //B2B显示绑定
            this.DdlB2B.DataSource = EnumObj.GetList(typeof(CompanyB2BDisplay));
            this.DdlB2B.DataTextField = "Text";
            this.DdlB2B.DataValueField = "Value";
            this.DdlB2B.DataBind();


            //B2C显示绑定
            this.DdlB2C.DataSource = EnumObj.GetList(typeof(CompanyB2CDisplay));
            this.DdlB2C.DataTextField = "Text";
            this.DdlB2C.DataValueField = "Value";
            this.DdlB2C.DataBind();

        }
        #endregion
    }
}
