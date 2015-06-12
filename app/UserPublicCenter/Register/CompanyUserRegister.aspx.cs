using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Common.Function;
namespace UserPublicCenter.Register
{
    /// <summary>
    /// 平台注册页
    /// 开发人：刘玉灵  时间：2010-6-24
    /// －－－－－－－－－－－－－－－－
    /// 2013-12-03 注册时间间隔限制
    /// </summary>
    public partial class CompanyUserRegister : EyouSoft.Common.Control.FrontPage
    {


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
        private bool _isShowRequired = false;
        /// <summary>
        /// 是否显示必填（*）信息
        /// </summary>
        public bool IsShowRequired
        {
            get { return _isShowRequired; }
            set { _isShowRequired = value; }
        }



        protected string IsYkApplay = string.Empty;
        protected string UserName = string.Empty;
        protected string Email = string.Empty;
        protected string Mobile = string.Empty;
        protected string TureName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "GetCityList", JsManage.GetJsFilePath("GetCityList"));
            MessageBox.ResponseScript(this.Page, "BindProvinceList('" + this.ddl_ProvinceList.ClientID + "');BindProvinceCheckboxList('SaleProvince');");
            //MessageBox.ResponseScript(this.Page,"BindProvinceCheckboxList('SaleProvince');");
            BindProvince();
            //BindCity();
            if (!IsPostBack)
            {
                /* 默认选中项 */
                if (SetProvinceId > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetProvince(" + SetProvinceId + ");ChangeList('" + this.ddl_CityList.ClientID + "'," + SetProvinceId + ");", true);
                    if (SetCityId > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "SetCity(" + SetCityId + ");", true);
                    }
                }
                if (IsShowRequired)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "$('#ProvinceRequired').show();$('#CityRequired').show()", true);
                }
            }
            IsYkApplay = Request.QueryString["IsYkApplay"];
            if (!string.IsNullOrEmpty(IsYkApplay))
            {
                if (IsLogin && (IsYkApplay == "1" || IsYkApplay == "2"))
                {
                    if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛))//随便逛逛
                    {
                        UserName = SiteUserInfo.UserName;
                        Email = SiteUserInfo.ContactInfo.Email;
                        Mobile = SiteUserInfo.ContactInfo.Mobile;
                        TureName = SiteUserInfo.ContactInfo.ContactName;
                    }
                    else  //不是随便逛逛用户跳转到企业完善信息页
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                if (Request.QueryString["type"] != null)
                {
                    string type = Request.QueryString["type"].ToString();
                    //注册
                    if (type.Equals("add", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Clear();
                        Response.Write(RegisterCompany(null));
                        Response.End();
                    }
                    //随便逛逛用户申请
                    if (type.Equals("update", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Clear();
                        Response.Write(RegisterCompany(SiteUserInfo.CompanyID));
                        Response.End();
                    }
                    //获的省份下的销售城市
                    else if (type.Equals("GetSaleCity", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Clear();
                        Response.Write(GetSaleCity());
                        Response.End();
                    }
                    //获的省份/城市下的线路区域
                    else if (type.Equals("GetTourArea", StringComparison.OrdinalIgnoreCase))
                    {
                        Response.Clear();
                        Response.Write(GetTourAreaList());
                        Response.End();
                    }
                }
            }
        }


        /// <summary>
        /// 绑定省份
        /// </summary>
        private void BindProvince()
        {
            this.ddl_ProvinceList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_ProvinceList.Attributes.Add("onchange", string.Format("ChangeList('" + this.ddl_CityList.ClientID + "','" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value)"));
            this.ddl_CityList.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddl_CityList.Attributes.Add("onchange", string.Format("ChangeCountyList('" + this.ddl_CountyList.ClientID + "', this.options[this.selectedIndex].value,'" + this.ddl_ProvinceList.ClientID + "')"));
            this.ddl_CountyList.Items.Insert(0, new ListItem("请选择", "0"));
        }
        /// <summary>
        /// 获的销售城市
        /// </summary>
        protected string GetSaleCity()
        {
            StringBuilder strCityList = new StringBuilder();
            int ProvinceId = EyouSoft.Common.Utils.GetInt(Request.QueryString["ProvinceId"]);

            IList<EyouSoft.Model.SystemStructure.SysCity> SaleCityList = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(ProvinceId, null);
            if (SaleCityList != null)
            {
                strCityList.Append("<ul>");
                foreach (EyouSoft.Model.SystemStructure.SysCity item in SaleCityList)
                {
                    strCityList.AppendFormat("<li><input checked='checked' type='checkbox' id='SellCity_{0}' value='{0}' name='ckSellCity'/><label for='SellCity_{0}'>{1}</label></li>", item.CityId, item.CityName);
                }
            }
            strCityList.Append("</ul>");
            SaleCityList = null;
            return strCityList.ToString();
        }

        /// <summary>
        /// 获的省份/城市下的线路区域
        /// </summary>
        /// <returns></returns>
        protected string GetTourAreaList()
        {
            StringBuilder strTourAreaLis = new StringBuilder();

            int ProvinceId = EyouSoft.Common.Utils.GetInt(Request.QueryString["ProvinceId"]);
            int CityId = EyouSoft.Common.Utils.GetInt(Request.QueryString["CityId"]);
            string CityName = EyouSoft.Common.Utils.InputText(Request.QueryString["CityName"]);
            strTourAreaLis.AppendFormat("<table id=\"div135\" cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\" style=\"border:1px solid #D1E7F4; background:#F2FAFF;\">");
            strTourAreaLis.AppendFormat(
                "<tr><td align=\"left\"><strong>您所在的是{0}，请勾选专线区域（可多选）</strong><div id=\"errMsgckRouteArea\" class=\"redtist\" style=\"display: none\"><img src=\"{1}/images/UserPublicCenter/1000216.gif\" width=\"16\" height=\"16\" /> 必须选择专线区域</div></td></tr></table>",
                Server.UrlDecode(CityName), ImageServerPath);
            //国内长线
            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList1 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetLongAreaSiteControl(ProvinceId);
            if (AreaList1 != null && AreaList1.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\"><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内长线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysAreaSiteControl item in AreaList1)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input onclick=\"Register.IsShowAreaError();\" id=\"Area1_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area1_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
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
                strTourAreaLis.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\"><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国际线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysArea item in AreaList2)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input onclick=\"Register.IsShowAreaError();\" id=\"Area2_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area2_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            //国内短线
            IList<EyouSoft.Model.SystemStructure.SysAreaSiteControl> AreaList3 = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetShortAreaSiteControl(CityId);
            if (AreaList3 != null && AreaList3.Count > 0)
            {
                strTourAreaLis.AppendFormat("<table cellspacing=\"3\" cellpadding=\"2\" width=\"100%\" border=\"0\" style=\"border-bottom:1px dashed #cccccc;\"> <tr onmouseover=\"mouseovertr(this)\" onmouseout=\"mouseouttr(this)\"><td align=\"middle\" width=\"4%\" bgcolor=\"#D1E7F4\">国内短线</td><td width=\"96%\">");
                strTourAreaLis.Append("<table cellspacing=\"0\" width=\"100%\" border=\"0\">");
                strTourAreaLis.Append("<tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
                int Count = 0;
                foreach (EyouSoft.Model.SystemStructure.SysAreaSiteControl item in AreaList3)
                {
                    strTourAreaLis.AppendFormat("<td width=\"25%\" align=\"left\"><input onclick=\"Register.IsShowAreaError();\" id=\"Area3_{0}\"  type=\"checkbox\"  value=\"{0}\" name=\"checkbox_Area\" /><label for=\"Area3_{0}\">{1}</label></td>", item.AreaId, item.AreaName);
                    Count++;
                    if (Count != 0 && Count % 4 == 0)
                    {
                        strTourAreaLis.Append("</tr><tr onmouseover=\"mouseovertr(this)\"   onmouseout=\"mouseouttr(this)\">");
                    }
                }
                strTourAreaLis.Append("</tr>");
                strTourAreaLis.Append("</table>");
                strTourAreaLis.Append("</td></tr></table>");
            }
            AreaList3 = null;
            return strTourAreaLis.ToString();
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        protected int RegisterCompany(string companyId)
        {

            int AddisTrue = 0;

            if (EyouSoft.Cache.Facade.EyouSoftCache.GetCache("REG_" + Request.UserHostAddress) != null)
            {
                AddisTrue = 5; //注册太频繁
                return AddisTrue;
            }
             


            string CompanyName = Utils.InputText(Request.Form["txtCompanyName"]); //公司名
            string UserName = Utils.InputText(Request.Form["txtUserName"]); //用户名
            string PassWord = Utils.InputText(Request.Form["txtFristPassWord"]); //密码
            string LicenseNumber = Utils.InputText(Request.Form["txtLicenseNumber"]);  //许可证号
            string BrandName = Utils.InputText(Request.Form["txtBrandName"]);  //品牌名称
            //int ProvinceId = 0;//省份
            //if (Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_ProvinceList"] != null)
            //    ProvinceId = Convert.ToInt32(Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_ProvinceList"]);
            //else if (Request.Form["ProvinceAndCityList1$ddl_ProvinceList"] != null)
            //    ProvinceId = Convert.ToInt32(Request.Form["ProvinceAndCityList1$ddl_ProvinceList"]);

            //int CityId = 0;//城市
            //if (Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_CityList"] != null)
            //    CityId = Convert.ToInt32(Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_CityList"]);
            //else if (Request.Form["ProvinceAndCityList1$ddl_CityList"] != null)
            //    CityId = Convert.ToInt32(Request.Form["ProvinceAndCityList1$ddl_CityList"]); //城市

            //int CountyId = 0;//地区（县）
            //if (Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_CountyList"] != null)
            //    CountyId = Convert.ToInt32(Request.Form["ctl00$Main$ProvinceAndCityList1$ddl_CountyList"]);
            //else if (Request.Form["ProvinceAndCityList1$ddl_CityList"] != null)
            //    CountyId = Convert.ToInt32(Request.Form["ProvinceAndCityList1$ddl_CountyList"]); //城市
            int ProvinceId = 0;//省份
            if (Utils.GetFormValue(ddl_ProvinceList.UniqueID) != null)
                ProvinceId = Convert.ToInt32(Utils.GetFormValue(ddl_ProvinceList.UniqueID));

            int CityId = 0;//城市
            if (Utils.GetFormValue(ddl_CityList.UniqueID) != null)
                CityId = Convert.ToInt32(Utils.GetFormValue(ddl_CityList.UniqueID));
            int CountyId = 0;//地区（县）
            if (Utils.GetFormValue(ddl_CountyList.UniqueID) != null)
                CountyId = Convert.ToInt32(Utils.GetFormValue(ddl_CountyList.UniqueID));
            string OfficeAddress = Utils.InputText(Request.Form["txtOfficeAddress"]);   //办公地点
            //string CommendCompany = Utils.InputText(Request.Form["txtCommendCompany"]); 引荐单位
            string CompanyType = Utils.InputText(Request.QueryString["companytype"]);   //公司类型
            string ContactName = Utils.InputText(Request.Form["txtContactName"]);
            string ContactTel = Utils.InputText(Request.Form["txtContactTel"]);
            string ContactMobile = Utils.InputText(Request.Form["txtContactMobile"]);
            string ContactFax = Utils.InputText(Request.Form["txtContactFax"]);
            string ContactEmail = Utils.InputText(Request.Form["txtContactEmail"]);
            string ContactQQ = Utils.GetFormValue("qq", 100);  //QQ，注意可能多个用半角逗号分割的
            string ContactMSN = Utils.InputText(Request.Form["txtMsn"]);
            string CompanyInfo = Utils.InputText(Request.Form["txtCompanyInfo"]);
            string CompanyMainpro = Utils.InputText(Request.Form["txtMainProduct"]);
            string CompanySimpleName = Utils.InputText(Request.Form["txtCompanySimpleName"]);
            /*公司信息*/
            EyouSoft.Model.CompanyStructure.CompanyArchiveInfo model = new EyouSoft.Model.CompanyStructure.CompanyArchiveInfo();
            /*用户信息*/
            EyouSoft.Model.CompanyStructure.UserAccount UserModel = new EyouSoft.Model.CompanyStructure.UserAccount();
            /* 公司联系人信息 */
            EyouSoft.Model.CompanyStructure.ContactPersonInfo ContactModel = new EyouSoft.Model.CompanyStructure.ContactPersonInfo();
            string OtherSaleCity = "";
            if (string.IsNullOrEmpty(CompanyType))
            {
                AddisTrue = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(companyId)) //随便逛逛申请，修改
                {
                    UserModel.UserName = UserName; /*用户信息*/
                }
                UserModel.PassWordInfo.NoEncryptPassword = PassWord;
                ContactModel.ContactName = ContactName;
                ContactModel.Mobile = ContactMobile;
                ContactModel.Email = ContactEmail;
                ContactModel.QQ = ContactQQ;
                ContactModel.Fax = ContactFax;
                ContactModel.MSN = ContactMSN;
                ContactModel.Tel = ContactTel;
                if (CompanyType != "-1")  //游客               
                {
                    #region 非随便看看用户
                    /*公司信息*/
                    model.CompanyName = CompanyName;
                    model.ProvinceId = ProvinceId;
                    model.CityId = CityId;
                    model.CountyId = CountyId;
                    model.CompanyAddress = OfficeAddress;
                    model.CompanyBrand = BrandName;
                    model.License = LicenseNumber;
                    model.Introduction = CompanySimpleName;
                    model.Remark = CompanyInfo;
                    model.Scale = (EyouSoft.Model.CompanyStructure.CompanyScale)Utils.GetInt(this.sltCompanySize.Value);
                    model.ProvinceId = Utils.GetInt(Utils.GetFormValue(this.ddl_ProvinceList.UniqueID));
                    model.CityId = Utils.GetInt(Utils.GetFormValue(this.ddl_CityList.UniqueID));
                    model.CountyId = Utils.GetInt(Utils.GetFormValue(this.ddl_CountyList.UniqueID));
                    model.CompanyBrand = BrandName;
                    //model.Qualification
                    //model.AdminAccount = UserModel;

                    ContactModel.Tel = ContactTel;
                    ContactModel.Fax = ContactFax;
                    //model.ContactInfo = ContactModel;
                    // model.CommendPeople = CommendCompany;
                    //旅行社资质
                    string[] lxszz = Utils.Split(Utils.GetFormValue("chxzz"), ",");
                    if (lxszz != null && lxszz.Length > 0)
                    {
                        List<EyouSoft.Model.CompanyStructure.CompanyQualification> zizi = new List<EyouSoft.Model.CompanyStructure.CompanyQualification>();
                        for (int i = 0; i < lxszz.Length; i++)
                        {
                            if (lxszz[i] == "1")
                            {
                                zizi.Add(EyouSoft.Model.CompanyStructure.CompanyQualification.出境旅游);
                            }
                            if (lxszz[i] == "2")
                            {
                                zizi.Add(EyouSoft.Model.CompanyStructure.CompanyQualification.入境旅游);
                            }
                            if (lxszz[i] == "3")
                            {
                                zizi.Add(EyouSoft.Model.CompanyStructure.CompanyQualification.台湾旅游);
                            }
                        }
                        model.Qualification = zizi;
                    }
                    /* 公司身份 */
                    EyouSoft.Model.CompanyStructure.CompanyRole RoleMode = new EyouSoft.Model.CompanyStructure.CompanyRole();
                    EyouSoft.Model.CompanyStructure.CompanyType TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.全部;

                    if (CompanyType == "0") //线路供应商（专线，地接）
                    {
                        string[] strType = Utils.Split(Utils.GetFormValue("hdfZXandDj"), ",");
                        for (int i = 0; i < strType.Length; i++)
                        {
                            //设置经营线路区域
                            string[] strAreaList = Utils.Split(Utils.GetFormValue("checkbox_Area"), ",");
                            if (strAreaList != null)
                            {
                                List<EyouSoft.Model.SystemStructure.AreaBase> AreaList = new List<EyouSoft.Model.SystemStructure.AreaBase>();
                                for (int j = 0; j < strAreaList.Length; j++)
                                {
                                    EyouSoft.Model.SystemStructure.AreaBase item = new EyouSoft.Model.SystemStructure.AreaBase();
                                    item.AreaId = Convert.ToInt32(strAreaList[j]);
                                    AreaList.Add(item);
                                    item = null;
                                }
                                model.Area = AreaList;
                                AreaList = null;
                            }
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
                                        item.CityId = Convert.ToInt32(strSalelist[j]);
                                        SaleCity.Add(item);
                                        item = null;
                                    }
                                    model.SaleCity = SaleCity;
                                    SaleCity = null;
                                }
                                OtherSaleCity = Utils.InputText(Request.Form["inputOtherSaleCity"]);
                            }
                            if (strType[i] == "3")
                            {
                                TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.地接;
                            }
                            RoleMode.SetRole(TypeEmnu);
                            model.CompanyRole = RoleMode;
                        }

                    }
                    else
                    {
                        switch (CompanyType)
                        {
                            case "2":
                                TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.组团;
                                break;
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
                                //机票
                                ContactModel.QQ = Utils.InputText(Request.Form["txtContactQQ"]);
                                ContactModel.MSN = Utils.InputText(Request.Form["txtContactMSN"]);
                                EyouSoft.Model.TicketStructure.TicketWholesalersInfo ticketinfo = new EyouSoft.Model.TicketStructure.TicketWholesalersInfo();
                                ticketinfo.ProxyLev = Utils.InputText(Request.Form["txtDlNumber"]);
                                ticketinfo.WorkStartTime = Utils.InputText(Request.Form["WorkStartTime"]); //上下班时间
                                ticketinfo.WorkEndTime = Utils.InputText(Request.Form["WorkEndTime"]);
                                ticketinfo.OfficeNumber = Utils.InputText(Request.Form["txtOffice"]);
                                if (!string.IsNullOrEmpty(companyId)) //随便逛逛申请，修改
                                {
                                    ticketinfo.CompanyId = companyId;
                                }
                                model.TicketSupplierInfo = ticketinfo;
                                // model.License
                                break;
                            case "10":
                                TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.其他采购商;
                                break;

                            case "11":
                                TypeEmnu = EyouSoft.Model.CompanyStructure.CompanyType.随便逛逛;
                                break;
                        }
                        RoleMode.SetRole(TypeEmnu);
                        model.CompanyRole = RoleMode;
                    }
                    #endregion
                }
                model.AdminAccount = UserModel;
                model.ContactInfo = ContactModel;   
                if (!string.IsNullOrEmpty(companyId)) //随便逛逛申请，修改
                {
                    model.ID = companyId;
                    AddisTrue = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().ToCompany(model, OtherSaleCity) == true ? 1 : 0;
                }
                else    //注册添加
                {
                    EyouSoft.Model.ResultStructure.UserResultInfo Result = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().Add(model, OtherSaleCity);
                    switch (Result)
                    {
                        case EyouSoft.Model.ResultStructure.UserResultInfo.Succeed:
                            AddisTrue = 1;
                            EyouSoft.Cache.Facade.EyouSoftCache.Add("REG_" + Request.UserHostAddress,DateTime.Now,DateTime.Now.AddMinutes(5));
                            break;
                        case EyouSoft.Model.ResultStructure.UserResultInfo.ExistsEmail:
                            AddisTrue = 2;
                            break;
                        case EyouSoft.Model.ResultStructure.UserResultInfo.ExistsUserName:
                            AddisTrue = 3;
                            break;
                        case EyouSoft.Model.ResultStructure.UserResultInfo.Error:
                            AddisTrue = 0;
                            break;

                    }
                }
            }
            return AddisTrue;
        }
    }
}
