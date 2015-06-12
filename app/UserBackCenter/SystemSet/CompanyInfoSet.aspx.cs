using System;
using System.Collections;
using System.Collections.Generic;
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
using EyouSoft.Common.Control;
using EyouSoft.Common;
using System.IO;
using System.Text.RegularExpressions;
using EyouSoft.Common.Function;
using System.Text;
namespace UserBackCenter.SystemSet
{
    /// <summary>
    /// 页面功能：单位信息管理
    /// 开发人：xuty 开发时间：2010-07-06
    /// 修改人：徐从栎
    /// </summary>
    public partial class CompanyInfoSet : BackPage
    {
        protected string cityName;//所属城市
        protected string companyName;//单位名称 
        protected string licenese;//许可证号
        protected int columnIndex = 1;
        protected int personNo = 1;//个人账户控件编号
        protected EyouSoft.IBLL.CompanyStructure.ICompanyInfo companyBll;
        protected EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyModel;
        protected bool haveArea = true;
        protected IList<EyouSoft.Model.SystemStructure.AreaBase> checkedAreaList;
        protected bool haveUpdate = true;
        protected string companyDetail = "";
        protected string saleCityName = "";
        protected bool isCompanyCheck = false;
        protected string GoogleMapKey = string.Empty;
        protected int longCount;
        protected int shortCount;
        protected int exitCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (SiteUserInfo.CompanyRole.RoleItems.Where(i => i == EyouSoft.Model.CompanyStructure.CompanyType.全部 || i == EyouSoft.Model.CompanyStructure.CompanyType.专线 || i == EyouSoft.Model.CompanyStructure.CompanyType.组团 || i == EyouSoft.Model.CompanyStructure.CompanyType.地接 || i==EyouSoft.Model.CompanyStructure.CompanyType.其他采购商).Count() < 1)
                //{
                //    Response.Clear();
                //    Response.Write("<script type='text/javascript'>alert('对不起，你不是旅行社用户！');</script>");
                //    Response.End();
                //    return;
                //}
                this.GoogleMapKey = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "GoogleMapsAPIKEY");
                this.initData();
            }
            if (IsCompanyCheck)
            {
                isCompanyCheck = true;
            }

            //if (!CheckGrant(TravelPermission.系统设置_单位信息))
            //{
            //    haveUpdate = false;
            //}
            companyBll = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            companyModel = companyBll.GetModel(SiteUserInfo.CompanyID);//获取公司信息
            string method = Utils.GetFormValue("method");
            if (method == "save")
            {
                if (!haveUpdate)
                {
                    Utils.ResponseMeg(false, "对不起，你没有该权限!");
                    return;
                }
                UpdateCompanyInfo();//更新单位信息
                return;
            }
            else
            {
                LoadCompanyInfo();//初始化单位信息
            }
        }
        protected void initData()
        {
            //this.InitProvince();
            List<EnumObj> lstComScale = EnumObj.GetList(typeof(EyouSoft.Model.CompanyStructure.CompanyScale));
            if (null != lstComScale && lstComScale.Count > 0)
            {
                this.ddlScale.DataSource = lstComScale;
                this.ddlScale.DataTextField = "text";
                this.ddlScale.DataValueField = "value";
                this.ddlScale.DataBind();
            }
        }
        #region 绑定个人银行账户时返回控件编号
        protected string GetPersonBankNo()
        {
            return personNo.ToString();
        }
        #endregion

        #region 绑定个人账号时递增控件编号
        protected void cis_rpt_personBank_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            personNo++;
        }
        #endregion

        #region 修改单位信息
        protected void UpdateCompanyInfo()
        {
            if (!IsCompanyCheck)//是否审核
            {
                Utils.ResponseMeg(false, "对不起,你尚未审核通过!");
                return;
            }
            string userAdmin = Utils.GetFormValue(cis_txtAdmin.UniqueID, 20);
            if (userAdmin == "")
            {
                Utils.ResponseMegNoComplete();
            }
            else
            {
                EyouSoft.Model.CompanyStructure.CompanyArchiveInfo companyArchModel = companyModel as EyouSoft.Model.CompanyStructure.CompanyArchiveInfo;
                //账户信息
                List<EyouSoft.Model.CompanyStructure.BankAccount> bankList = new List<EyouSoft.Model.CompanyStructure.BankAccount>();
                //获得公司账户
                EyouSoft.Model.CompanyStructure.BankAccount companyBankAccount = new EyouSoft.Model.CompanyStructure.BankAccount();
                companyBankAccount.BankAccountName = Utils.GetFormValue(cis_Company1.UniqueID, 100); //账户名
                companyBankAccount.BankName = Utils.GetFormValue(cis_CompanyBank1.UniqueID, 100);//开户行
                companyBankAccount.AccountNumber = Utils.GetFormValue(cis_CompanyAccount1.UniqueID, 100); //账号
                //判断公司账户是否全为空
                if (companyBankAccount.BankAccountName == "" && companyBankAccount.BankName == "" && companyBankAccount.AccountNumber == "")
                {

                }
                else
                {
                    if (companyBankAccount.BankAccountName == "" || companyBankAccount.BankName == "" || companyBankAccount.AccountNumber == "")
                    {
                        Utils.ResponseMeg(false, "请填写完整账户信息!");
                        return;
                    }
                    companyBankAccount.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.公司;
                    bankList.Add(companyBankAccount);
                }

                //获得个人账户集合
                var strNoList = Request.Form.AllKeys.Where(i => i.Contains("cis_PeosonName")).Select(i => i.Substring(14, i.Length - 14));
                foreach (string no in strNoList)
                {
                    EyouSoft.Model.CompanyStructure.BankAccount peosonBankModel = new EyouSoft.Model.CompanyStructure.BankAccount();
                    peosonBankModel.AccountNumber = Utils.GetFormValue("cis_PeosonAccount" + no, 100);//账号
                    peosonBankModel.BankAccountName = Utils.GetFormValue("cis_PeosonName" + no, 100);//账户名
                    peosonBankModel.BankName = Utils.GetFormValue("cis_PeosonBank" + no, 100);//开户行
                    //如果账户全为空则继续
                    if (peosonBankModel.AccountNumber == "" && peosonBankModel.BankAccountName == "" && peosonBankModel.BankName == "")
                    {
                        continue;
                    }
                    else
                    {   //如果不全为空但有空存在
                        if (peosonBankModel.AccountNumber == "" || peosonBankModel.BankAccountName == "" || peosonBankModel.BankName == "")
                        {
                            Utils.ResponseMeg(false, "请填写完整账户信息!");
                            return;
                        }
                        peosonBankModel.AccountType = EyouSoft.Model.CompanyStructure.BankAccountType.个人;
                    }

                    bankList.Add(peosonBankModel);
                }
                companyArchModel.BankAccounts = bankList;
                companyArchModel.ContactInfo.ContactName = userAdmin;
                companyArchModel.CompanyBrand = Utils.GetFormValue(this.txtBrandName.UniqueID, 50);
                companyArchModel.CompanyAddress = Utils.GetFormValue(cis_txtCompanyAddress.UniqueID, 250);
                companyArchModel.Remark = Utils.EditInputText(Request.Form["cis_CompanyDetail"]);
                companyArchModel.ContactInfo.Fax = Utils.GetFormValue(cis_txtFax.UniqueID, 50);
                companyArchModel.ContactInfo.Mobile = Utils.GetFormValue(cis_txtMobile.UniqueID, 20);
                //companyArchModel.ContactInfo.MSN = Utils.GetFormValue(cis_txtMSN.UniqueID, 50);
                //companyArchModel.ContactInfo.QQ = Utils.GetFormValue(cis_txtQQ.UniqueID, 20);
                companyArchModel.ContactInfo.Tel = Utils.GetFormValue(cis_txtTel.UniqueID, 45);
                string logoPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$uc_logo_fileUp$hidFileName");
                if (logoPath != "")
                    companyArchModel.AttachInfo.CompanyLogo.ImagePath = logoPath;
                string cerPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$uc_Cert_fileUp$hidFileName");
                if (cerPath != "")
                    companyArchModel.AttachInfo.BusinessCertif.BusinessCertImg = cerPath;
                string licePath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$uc_Licence_fileUp$hidFileName");
                if (licePath != "")
                    companyArchModel.AttachInfo.BusinessCertif.LicenceImg = licePath;
                string taxPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$uc_Tax_fileUp$hidFileName");
                if (taxPath != "")
                    companyArchModel.AttachInfo.BusinessCertif.TaxRegImg = taxPath;
                //string signPath = Utils.GetFormValue("ctl00$ContentPlaceHolder1$uc_Sign_fileUp$hidFileName");
                //if (signPath != "")
                //    companyArchModel.AttachInfo.CompanySignet = signPath;
                /*省市区不可修改  2012-02-10 信息来源：周
                //公司省市区
                companyArchModel.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ProvinceList.UniqueID),-1);
                companyArchModel.CityId = Utils.GetInt(Utils.GetFormValue(this.CityList.UniqueID), -1);
                companyArchModel.CountyId = Utils.GetInt(Utils.GetFormValue(this.CountyList.UniqueID), -1);
                 */
                //公司简称
                companyArchModel.Introduction = this.txtSimpleName.Value;
                //客服邮箱
                companyArchModel.ContactInfo.Email = this.txtEmail.Value;
                //同业联系方式
                companyArchModel.PeerContact = this.txtTongYeContact.Value;
                //支付宝账户
                companyArchModel.AlipayAccount = this.txtAliPay.Value.Trim();
                //签约时间
                companyArchModel.ContractStart = Utils.GetDateTimeNullable(this.txtStartTime.Value);
                companyArchModel.ContractEnd = Utils.GetDateTimeNullable(this.txtEndTime.Value);
                //业务优势//只有运营后台可改
                //companyArchModel.ShortRemark = this.txtOperation.Value;
                //地图信息
                string[] strMap = this.hiddenMapXY.Value.Split(',');
                if (strMap.Length == 2)
                {
                    decimal x = 0;
                    decimal y = 0;
                    decimal.TryParse(strMap[0], out x);
                    decimal.TryParse(strMap[1], out y);
                    if (x != 0 && y != 0)
                    {
                        companyArchModel.Longitude = x;
                        companyArchModel.Latitude = y;
                    }
                }
                //公司网址
                companyArchModel.WebSite = this.txtWebUrl.Value;
                //公司规模
                companyArchModel.Scale = (EyouSoft.Model.CompanyStructure.CompanyScale)Utils.GetInt(Utils.GetFormValue(this.ddlScale.UniqueID));
                //公司资质
                string[] qualiList = Utils.GetFormValues("cbxQualification");
                if (qualiList.Length > 0)
                {
                    for (int i = 0; i < qualiList.Length; i++)
                    {
                        companyArchModel.Qualification.Add((EyouSoft.Model.CompanyStructure.CompanyQualification)Utils.GetInt(qualiList[i]));
                    }
                }
                //公司县区
                int districtID = Utils.GetInt(Utils.GetFormValue("sltDistrictName"));
                companyArchModel.CountyId = districtID;
                //公司照片开始
                string companyPic1 = Utils.GetFormValue(this.CompanyPic1.UniqueID + "$hidFileName");
                string companyPic2 = Utils.GetFormValue(this.CompanyPic2.UniqueID + "$hidFileName");
                string companyPic3 = Utils.GetFormValue(this.CompanyPic3.UniqueID + "$hidFileName");
                List<EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto> lstCompanyPic = new List<EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto>();
                this.setComPicInfo(companyPic1, lstCompanyPic, 1);
                this.setComPicInfo(companyPic2, lstCompanyPic, 2);
                this.setComPicInfo(companyPic3, lstCompanyPic, 3);
                companyArchModel.AttachInfo.CompanyPublicityPhoto = lstCompanyPic;
                //公司照片结束
                //授权证书
                string rightPic = Utils.GetFormValue(this.setRightPic.UniqueID + "$hidFileName");
                if (!string.IsNullOrEmpty(rightPic))
                {
                    companyArchModel.AttachInfo.BusinessCertif.WarrantImg = rightPic;
                }
                //负责人身份证
                string idCardPic = Utils.GetFormValue(this.setIDCard.UniqueID + "$hidFileName");
                if (!string.IsNullOrEmpty(idCardPic))
                {
                    companyArchModel.AttachInfo.BusinessCertif.PersonCardImg = idCardPic;
                }
                if (companyBll.UpdateSelf(companyArchModel))//执行修改
                {
                    Utils.ResponseMegSuccess();
                }
                else
                {
                    Utils.ResponseMegError();
                }
            }
        }
        #endregion

        #region 初始化单位信息
        protected void LoadCompanyInfo()
        {
            if (companyModel != null)
            {
                haveArea = companyModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);
                EyouSoft.Model.SystemStructure.SysProvince proviceModel = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceModel(companyModel.ProvinceId);
                EyouSoft.Model.SystemStructure.SysCity cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(companyModel.CityId);

                EyouSoft.IBLL.SystemStructure.ISysDistrictCounty disBll = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance();
                EyouSoft.Model.SystemStructure.SysDistrictCounty countyModel = disBll.GetModel(companyModel.CountyId);
                if (proviceModel != null)
                {
                    cityName = proviceModel.ProvinceName;
                }
                if (cityModel != null)
                {
                    cityName += "-" + cityModel.CityName;
                }
                if (countyModel != null)
                {
                    if (countyModel.DistrictName.Trim() != "")
                    {
                        cityName += "-" + countyModel.DistrictName;
                    }
                }
                else
                {
                    string districtName = "";
                    IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> disList = disBll.GetDistrictCounty(companyModel.CityId);
                    if (disList != null && disList.Count > 0)
                    {
                        for (int i = 0; i < disList.Count; i++)
                        {
                            districtName += "<option value='" + disList[i].Id.ToString() + "'>" + disList[i].DistrictName + "</option>";
                        }
                    }
                    cityName += "-&nbsp;<select name='sltDistrictName'>" + districtName + "</select>";
                }

                if (companyModel.SaleCity != null)
                {
                    foreach (EyouSoft.Model.SystemStructure.CityBase city in companyModel.SaleCity)
                    {
                        saleCityName += city.CityName + ",";
                    }
                    saleCityName = saleCityName.TrimEnd(',');
                }
                companyName = companyModel.CompanyName;
                licenese = string.IsNullOrEmpty(companyModel.License) ? "暂无" : companyModel.License;
                cis_txtAdmin.Value = companyModel.ContactInfo.ContactName;
                this.txtBrandName.Value = companyModel.CompanyBrand;
                cis_txtCompanyAddress.Value = companyModel.CompanyAddress;
                companyDetail = companyModel.Remark;
                cis_txtFax.Value = companyModel.ContactInfo.Fax;
                cis_txtMobile.Value = companyModel.ContactInfo.Mobile;
                //cis_txtMSN.Value = companyModel.ContactInfo.MSN;
                //cis_txtQQ.Value = companyModel.ContactInfo.QQ;
                cis_txtTel.Value = companyModel.ContactInfo.Tel;
                //初始化账户信息
                List<EyouSoft.Model.CompanyStructure.BankAccount> BankAccountList = companyModel.BankAccounts;
                if (BankAccountList != null)
                {
                    if (BankAccountList.Where(i => i.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司).Count() > 0)
                    {
                        EyouSoft.Model.CompanyStructure.BankAccount companyBankAccount = BankAccountList.Where(i => i.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.公司).First();
                        cis_Company1.Value = companyBankAccount.BankAccountName;
                        cis_CompanyAccount1.Value = companyBankAccount.AccountNumber;
                        cis_CompanyBank1.Value = companyBankAccount.BankName;
                    }
                    cis_rpt_personBank.DataSource = companyModel.BankAccounts.Where(i => i.AccountType == EyouSoft.Model.CompanyStructure.BankAccountType.个人);
                    cis_rpt_personBank.DataBind();
                }
                //地区，省、市、区
                //this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), string.Format("CompanyInfoSet.InitPlace('{0}','{1}','{2}');",companyModel.ProvinceId,companyModel.CityId,companyModel.CountyId), true);
                this.spanProvinceInfo.InnerHtml = cityName;// string.Format("{0}&nbsp;&nbsp;{1}&nbsp;&nbsp;{2}", proviceModel.ProvinceName, cityModel.CityName, countyModel.DistrictName);
                //管理员账户
                this.txtAdminCode.Value = companyModel.AdminAccount.UserName;
                //同业联系方式
                this.txtTongYeContact.Value = companyModel.PeerContact;
                //销售城市
                StringBuilder strSaleCity = new StringBuilder();
                List<EyouSoft.Model.SystemStructure.CityBase> lstSaleCity = companyModel.SaleCity;
                if (null != lstSaleCity && lstSaleCity.Count > 0)
                {
                    for (int i = 0; i < lstSaleCity.Count; i++)
                    {
                        strSaleCity.Append(lstSaleCity[i].CityName + ";");
                    }
                }
                this.txtSaleCity.InnerHtml = strSaleCity.Length > 0 ? strSaleCity.ToString() : "暂无销售城市";
                //公司简称
                this.txtSimpleName.Value = companyModel.Introduction;
                //客服邮箱
                this.txtEmail.Value = companyModel.ContactInfo.Email;
                this.ddlScale.SelectedValue = Convert.ToString((int)companyModel.Scale);
                //string strUrl = "<a href=\"" + Domain.FileSystem + "{0}\" target='_blank' style='color:#f00;'>查看</a><a href=\"javascript:void(0);\" onclick=\"CompanyInfoSet.delFile(this)\" title='删除'><img src=\"" + ImageServerUrl + "/images/fujian_x.gif\"/></a>";
                string strUrl = "<a href=\"" + Domain.FileSystem + "{0}\" target='_blank'>查看</a>";
                //支付宝账户
                this.txtAliPay.Value = companyModel.AlipayAccount;
                //用户等级
                this.spanUserLevel.InnerHtml = Convert.ToString(companyModel.CompanyLev);
                //资料完整度
                this.spanInfoLevel.InnerHtml = (int)(companyModel.InfoFull * 100) + "%";
                //签约时间
                this.txtStartTime.Value = string.Format("{0:yyyy-MM-dd}", companyModel.ContractStart);
                this.txtEndTime.Value = string.Format("{0:yyyy-MM-dd}", companyModel.ContractEnd);
                //业务优势
                this.txtOperation.Value = companyModel.ShortRemark;
                //公司网址
                this.txtWebUrl.Value = companyModel.WebSite;
                //公司经纬度
                if (companyModel.Longitude == 0 && companyModel.Latitude == 0)
                {
                    this.spanMapXY.InnerHtml = "<font style='color:#f00;font-size:12px;'>暂未设置地理位置！</font>";
                }
                else
                {
                    this.hiddenMapXY.Value = companyModel.Longitude + "," + companyModel.Latitude;
                }
                //公司图片开始
                IList<EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto> lstCompanyPic = companyModel.AttachInfo.CompanyPublicityPhoto;
                if (null != lstCompanyPic && lstCompanyPic.Count > 0)
                {
                    for (int i = 0; i < lstCompanyPic.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                this.LtComPic1.Text = string.IsNullOrEmpty(lstCompanyPic[i].ImagePath.Trim()) ? "暂无图片" : string.Format(strUrl, lstCompanyPic[i].ImagePath);
                                MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setCompPic(0,'{0}');", lstCompanyPic[i].ImagePath));
                                break;
                            case 1:
                                this.LtComPic2.Text = string.IsNullOrEmpty(lstCompanyPic[i].ImagePath.Trim()) ? "暂无图片" : string.Format(strUrl, lstCompanyPic[i].ImagePath);
                                MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setCompPic(1,'{0}');", lstCompanyPic[i].ImagePath));
                                break;
                            case 2:
                                this.LtComPic3.Text = string.IsNullOrEmpty(lstCompanyPic[i].ImagePath.Trim()) ? "暂无图片" : string.Format(strUrl, lstCompanyPic[i].ImagePath);
                                MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setCompPic(2,'{0}');", lstCompanyPic[i].ImagePath));
                                break;
                            default:
                                break;
                        }
                    }
                }
                //公司图片结束
                //授权证书//负责人身份证
                this.spanSetRightPic.InnerHtml = string.IsNullOrEmpty(companyModel.AttachInfo.BusinessCertif.WarrantImg) ? "暂无授权证书图片" : string.Format(strUrl, companyModel.AttachInfo.BusinessCertif.WarrantImg);
                this.spanSetIDCard.InnerHtml = string.IsNullOrEmpty(companyModel.AttachInfo.BusinessCertif.PersonCardImg) ? "暂无负责人身份证图片" : string.Format(strUrl, companyModel.AttachInfo.BusinessCertif.PersonCardImg);
                MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setRightPicAndIDCard(0,'{0}');CompanyInfoSet.setRightPicAndIDCard(1,'{1}');", companyModel.AttachInfo.BusinessCertif.WarrantImg, companyModel.AttachInfo.BusinessCertif.PersonCardImg));
                //旅行社资质
                IList<EyouSoft.Model.CompanyStructure.CompanyQualification> lstCompetence = companyModel.Qualification;
                StringBuilder strCompetence = new StringBuilder();
                if (null != lstCompetence && lstCompetence.Count > 0)
                {
                    for (int i = 0; i < lstCompetence.Count; i++)
                    {
                        strCompetence.Append(lstCompetence[i] + ";");
                    }
                }
                if (strCompetence.Length == 0)
                {
                    IList<EyouSoft.Common.EnumObj> enQualification = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.CompanyStructure.CompanyQualification));
                    if (enQualification != null && enQualification.Count > 0)
                    {
                        for (int i = 0; i < enQualification.Count; i++)
                        {
                            strCompetence.Append("<input type='checkbox' name='cbxQualification'  value='" + enQualification[i].Value + "' />" + enQualification[i].Text + "&nbsp;&nbsp;");
                        }
                    }
                }
                this.spanCompetence.InnerHtml = strCompetence.ToString();
                //经营区域
                StringBuilder strArea = new StringBuilder();
                List<EyouSoft.Model.SystemStructure.AreaBase> areaList = companyModel.Area;
                if (null != areaList && areaList.Count > 0)
                {
                    for (int i = 0; i < areaList.Count; i++)
                    {
                        strArea.AppendFormat("{0}&nbsp;&nbsp;", areaList[i].AreaName);
                    }
                }
                if (strArea.Length > 0)
                {
                    this.trAreas.Visible = true;
                    this.spanAreas.InnerHtml = strArea.ToString();
                }
                else
                {
                    this.trAreas.Visible = false;
                }
            }
            //加载经营范围
            if (haveArea)
            {
                IList<EyouSoft.Model.SystemStructure.AreaBase> areaList = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(SiteUserInfo.CompanyID);
                if (areaList != null && areaList.Count > 0)
                {
                    IList<EyouSoft.Model.SystemStructure.AreaBase> longAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内长线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    longCount = longAreaList.Count;
                    pis_rpt_LongAreaList.DataSource = longAreaList;
                    pis_rpt_LongAreaList.DataBind();
                    IList<EyouSoft.Model.SystemStructure.AreaBase> shortAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国内短线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    shortCount = shortAreaList.Count;
                    pis_rpt_ShortAreaList.DataSource = shortAreaList;
                    pis_rpt_ShortAreaList.DataBind();
                    IList<EyouSoft.Model.SystemStructure.AreaBase> exitAreaList = areaList.Where(i => i.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线).ToList<EyouSoft.Model.SystemStructure.AreaBase>();
                    exitCount = exitAreaList.Count;
                    pis_rpt_ExitAreaList.DataSource = exitAreaList;
                    pis_rpt_ExitAreaList.DataBind();
                }
            }
        }
        #endregion

        #region 绑定经营范围项操作
        protected string GetItem()
        {
            string strHTML = "";
            if (columnIndex % 4 == 0)//如果已显示四列则换行
            {
                strHTML = "</tr><tr>";
            }
            columnIndex++;
            return strHTML;
        }
        #endregion

        #region 绑定项时判断是否经营该专线
        protected string GetChecked(int areaId)
        {
            string checkedHTML = "";
            if (companyModel.Area != null)
            {
                if (companyModel.Area.Where(i => i.AreaId == areaId).Count() > 0)
                {
                    checkedHTML = "checked='checked'";
                }
            }
            return checkedHTML;
        }
        #endregion

        #region 获取各种证件,logo
        protected string GetLogo()
        {
            string strLogo = "暂无logo";
            if (companyModel != null)
            {
                string path = companyModel.AttachInfo.CompanyLogo.ImagePath;
                if (!string.IsNullOrEmpty(path))
                {
                    strLogo = "<a href='" + EyouSoft.Common.Domain.FileSystem + path + "' target='blank' id='cis_logo_url'>查看</a>";
                    MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setOtherPicHidden(0,'{0}');", companyModel.AttachInfo.CompanyLogo.ImagePath));
                }
            }
            return strLogo;
        }
        protected string GetLicence()
        {
            string strLogo = "暂无营业执照";
            if (companyModel != null)
            {
                string path = companyModel.AttachInfo.BusinessCertif.LicenceImg;
                if (!string.IsNullOrEmpty(path))
                {
                    strLogo = "<a href='" + EyouSoft.Common.Domain.FileSystem + path + "' target='blank'>查看</a>";
                    MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setOtherPicHidden(1,'{0}');", companyModel.AttachInfo.BusinessCertif.LicenceImg));
                }
            }
            return strLogo;
        }
        protected string GetCer()
        {
            string strLogo = "暂无经营许可证";
            if (companyModel != null)
            {
                string path = companyModel.AttachInfo.BusinessCertif.BusinessCertImg;
                if (!string.IsNullOrEmpty(path))
                {
                    strLogo = "<a href='" + EyouSoft.Common.Domain.FileSystem + path + "' target='blank'>查看</a>";
                    MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setOtherPicHidden(2,'{0}');", companyModel.AttachInfo.BusinessCertif.BusinessCertImg));
                }
            }
            return strLogo;
        }
        protected string GetTax()
        {
            string strLogo = "暂无税务登记证";
            if (companyModel != null)
            {
                string path = companyModel.AttachInfo.BusinessCertif.TaxRegImg;
                if (!string.IsNullOrEmpty(path))
                {
                    strLogo = "<a href='" + EyouSoft.Common.Domain.FileSystem + path + "' target='blank'>查看</a>";
                    MessageBox.ResponseScript(this, string.Format("CompanyInfoSet.setOtherPicHidden(3,'{0}');", companyModel.AttachInfo.BusinessCertif.TaxRegImg));
                }
            }
            return strLogo;
        }
        //protected string GetSign()
        //{
        //    string strLogo = "暂无电子印章";
        //    if (companyModel != null)
        //    {
        //        string path = companyModel.AttachInfo.CompanySignet;
        //        if (!string.IsNullOrEmpty(path))
        //        {
        //            strLogo = "<a href='" + EyouSoft.Common.Domain.FileSystem + path + "' target='blank'>查看</a>";
        //        }
        //    }
        //    return strLogo;
        //}
        #endregion
        /*
        /// <summary>
        /// 绑定省份
        /// </summary>
        protected void InitProvince()
        {
            //获取省份列表
            IList<EyouSoft.Model.SystemStructure.SysProvince> ProvinceList = EyouSoft.BLL.SystemStructure.SysProvince.CreateInstance().GetProvinceList();
            this.ProvinceList.Items.Clear();
            if (ProvinceList != null && ProvinceList.Count > 0)
            {
                this.ProvinceList.DataSource = ProvinceList;
                this.ProvinceList.DataTextField = "ProvinceName";
                this.ProvinceList.DataValueField = "ProvinceId";
                this.ProvinceList.DataBind();
            }
            this.ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CityList.Items.Insert(0, new ListItem("请选择", "0"));
            this.CountyList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ProvinceList.Attributes.Add("onchange", string.Format("SetList('{0}',$(this).val(),'0');", this.CityList.ClientID) + string.Format("SetList('{0}',1,$('#{1}').val(),'0');", this.CountyList.ClientID, this.CityList.ClientID));
            this.CityList.Attributes.Add("onchange", string.Format("SetList('{0}',1,$(this).val(),'0');", this.CountyList.ClientID));
        }
         */
        /// <summary>
        /// 公司图片添加
        /// </summary>
        /// <param name="lstPic">图片list</param>
        /// <param name="companyPic">上传的源图路径</param>
        protected void setComPicInfo(string companyPic, List<EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto> lstPic, int index)
        {
            if (companyPic.Length == 0)
                return;
            EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto picModel = new EyouSoft.Model.CompanyStructure.CompanyPublicityPhoto();
            picModel.ImagePath = companyPic;//1024*768
            picModel.ThumbPath = companyPic.Replace("1024_768_", "400_300_");//400*300
            picModel.PhotoIndex = index;
            lstPic.Add(picModel);
        }
        /// <summary>
        /// 返回经营范围
        /// </summary>
        /// <returns></returns>
        protected string GetRoundInfo()
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.SystemStructure.ISysArea areaBLL = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();

            return str.ToString();
        }
    }
}