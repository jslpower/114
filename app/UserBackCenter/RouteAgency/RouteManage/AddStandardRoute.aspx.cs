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
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 线路标准发布
    /// 罗丽娥   2010-06-28
    /// </summary>
    public partial class AddStandardRoute : EyouSoft.Common.Control.BackPage
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        private EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel = null;
        private string CompanyId = "0";
        protected string strRouteTheme = string.Empty, strSaleCity = string.Empty, strLeaveCity = string.Empty;
        protected string tblID = "AddStandardRoute";
        protected int MaxTourDays = 0;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            tblID = "AddStandardRoute" + Guid.NewGuid().ToString();
            this.AddStandardRoute_StandardPlan.ContainerID = tblID;
            this.AddStandardRoute_tourpricestand.ContainerID = tblID;
            this.AddStandardRoute_ServiceStandard.ContainerID = tblID;
            MaxTourDays = Utils.GetInt(System.Configuration.ConfigurationManager.AppSettings["MaxTourDays"]);
            if (SiteUserInfo != null)
            {
                UserInfoModel = SiteUserInfo;
                CompanyId = UserInfoModel.CompanyID;
                this.AddStandardRoute_tourpricestand.UserInfoModel = UserInfoModel;
                if (UserInfoModel.ContactInfo != null)
                {
                    this.AddStandardRoute_TourContactInfo.ContactName = UserInfoModel.ContactInfo.ContactName;
                    this.AddStandardRoute_TourContactInfo.ContactTel = UserInfoModel.ContactInfo.Tel;
                    this.AddStandardRoute_TourContactInfo.ContactMQID = UserInfoModel.ContactInfo.MQ;
                }
                this.AddStandardRoute_TourContactInfo.CompanyID = UserInfoModel.CompanyID;
                this.AddStandardRoute_TourContactInfo.ContainerID = tblID;
            }
            this.Page.Title = "标准版发布线路信息";
            if (!Page.IsPostBack)
            {
                InitRouteArea();
                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["RouteId"])))
                    InitRouteInfo(Utils.InputText(Request.QueryString["RouteId"]));
                else
                {
                    strRouteTheme = InitRouteTopic(null);
                    strSaleCity = InitSaleCity(null);
                    strLeaveCity = InitLeaveCity(0);
                }

                #region 权限处理
                if (!this.CheckGrant(EyouSoft.Common.TravelPermission.专线_线路管理))
                {
                    Utils.ResponseNoPermit("对不起，您没有专线_线路行程库权限");
                    return; 
                }
                #endregion
            }
            if (!String.IsNullOrEmpty(Request.QueryString["flag"]))
            {
                string flag = Request.QueryString["flag"].ToString();
                if (flag.Equals("add", StringComparison.OrdinalIgnoreCase))
                {
                    Response.Clear();
                    if (!IsCompanyCheck)
                    {
                        Response.Write("对不起，您的账号未审核，不能进行操作!");
                    }
                    else
                    {
                        Response.Write(InsertRouteInfo());
                    }
                    Response.End();
                }
                else if (flag.Equals("Exist", StringComparison.OrdinalIgnoreCase))
                {
                    string RouteName = Utils.InputText(Request.QueryString["RouteName"]);
                    Response.Clear();
                    if (!String.IsNullOrEmpty(RouteName))
                    {
                        Response.Write(IsExists(RouteName));
                    }
                    else
                    {
                        Response.Write(false);
                    }
                    Response.End();
                }
            }
        }
        #endregion 

        #region 绑定线路区域
        /// <summary>
        /// 绑定线路区域
        /// </summary>
        private void InitRouteArea()
        {
            // 用户信息
            if (UserInfoModel != null)
            {
                EyouSoft.IBLL.SystemStructure.ISysArea bll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
                IList<EyouSoft.Model.SystemStructure.SysArea> list = bll.GetSysAreaList(UserInfoModel.AreaId);
                if (list != null && list.Count > 0)
                {
                    foreach (EyouSoft.Model.SystemStructure.SysArea model in list)
                    {
                        this.AddStandardRoute_RouteArea.Items.Add(new ListItem(model.AreaName, model.AreaId.ToString()));
                    }
                }
                list = null;
                bll = null;
            }
        }
        #endregion

        #region 绑定线路主题
        /// <summary>
        /// 绑定线路主题
        /// </summary>
        private string InitRouteTopic(IList<int> RouteTopicIdList)
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.SystemStructure.ISysField bll = EyouSoft.BLL.SystemStructure.SysField.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.SysFieldBase> list = bll.GetRouteTheme();
            if (list != null && list.Count > 0)
            {
                int i = 0;     // 如果i=0加验证
                foreach (EyouSoft.Model.SystemStructure.SysFieldBase model in list)
                {
                    int tmpTopicID = 0;
                    if (RouteTopicIdList != null)   // 修改或复制初始化
                    {
                        foreach (int TopicID in RouteTopicIdList)
                        {
                            if (TopicID == model.FieldId)
                            {
                                tmpTopicID = model.FieldId;
                                break;
                            }
                        }
                    }
                    if (i == 0)
                    {
                        if (tmpTopicID == model.FieldId)
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkRouteTopic\" id=\"AddStandardRoute_chkRouteTopic{0}\" value=\"{0}\" valid=\"requireChecked\" checked errmsg=\"请选择线路主题!\" errmsgend=\"AddStandardRoute_chkRouteTopic\" />{1}</label>", model.FieldId, model.FieldName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkRouteTopic\" id=\"AddStandardRoute_chkRouteTopic{0}\" value=\"{0}\" valid=\"requireChecked\" errmsg=\"请选择线路主题!\" errmsgend=\"AddStandardRoute_chkRouteTopic\" />{1}</label>", model.FieldId, model.FieldName);
                        }
                    }
                    else
                    {
                        if (tmpTopicID == model.FieldId)
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkRouteTopic\" id=\"AddStandardRoute_chkRouteTopic{0}\" value=\"{0}\" checked />{1}</label>", model.FieldId, model.FieldName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkRouteTopic\" id=\"AddStandardRoute_chkRouteTopic{0}\" value=\"{0}\" />{1}</label>", model.FieldId, model.FieldName);
                        }
                    }
                    i++;
                }
            }
            list = null;
            bll = null;
            return str.ToString();
        }
        #endregion

        #region 绑定销售城市
        /// <summary>
        /// 绑定销售城市
        /// </summary>
        private string InitSaleCity(IList<int> SaleCityIdList)
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanySaleCity(CompanyId);
            if (list != null && list.Count > 0)
            {
                int i = 0;
                foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                {
                    int tmpSaleCityID = 0;
                    if (SaleCityIdList != null)
                    {
                        int j = 0;
                        foreach (int SaleCityId in SaleCityIdList)
                        {
                            if (SaleCityId == model.CityId)
                            {
                                tmpSaleCityID = model.CityId;
                                break;
                            }
                        }
                    }
                    if (i == 0)
                    {
                        if (tmpSaleCityID == model.CityId)
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkSaleCity{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkSaleCity\" value=\"{0}\" id=\"AddStandardRoute_chkSaleCity{0}\" checked valid=\"requireChecked\" errmsg=\"请选择销售城市!\" errmsgend=\"AddStandardRoute_chkSaleCity\" />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkSaleCity{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkSaleCity\" value=\"{0}\" id=\"AddStandardRoute_chkSaleCity{0}\" valid=\"requireChecked\" errmsg=\"请选择销售城市!\" errmsgend=\"AddStandardRoute_chkSaleCity\" />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                    }
                    else
                    {
                        if (tmpSaleCityID == model.CityId)
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkSaleCity{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkSaleCity\" value=\"{0}\" id=\"AddStandardRoute_chkSaleCity{0}\" checked />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardRoute_chkSaleCity{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardRoute_chkSaleCity\" value=\"{0}\" id=\"AddStandardRoute_chkSaleCity{0}\" />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                    }
                    i++;
                }
            }
            return str.ToString();
        }
        #endregion 

        #region 绑定出港城市
        private string InitLeaveCity(int LeaveCity)
        {
            StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
            StringBuilder str2 = new StringBuilder();  // 设置过的出港城市

            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);
            if (list != null && list.Count > 0)
            {
                int i = 0;
                foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                {
                    if (model.ProvinceId == UserInfoModel.ProvinceId)
                    {
                        if (LeaveCity == model.CityId)
                        {
                            if (i == 0)
                            {
                                str1.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" checked value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"AddStandardRoute_radPortCity\" />{1}</label>", model.CityId, model.CityName);
                            }
                            else
                            {
                                str1.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" checked value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                str1.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"AddStandardRoute_radPortCity\" />{1}</label>", model.CityId, model.CityName);
                            }
                            else
                            {
                                str1.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
                            }                            
                        }
                    }
                    else
                    {
                        if (LeaveCity == model.CityId)
                        {
                            if (i == 0)
                            {
                                str2.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" checked value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"AddStandardRoute_radPortCity\" />{1}</label>", model.CityId, model.CityName);
                            }
                            else
                            {
                                str2.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" checked value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                str2.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" value=\"{0}\" valid=\"requiredRadioed\" errmsg=\"请选择出港城市!\" errmsgend=\"AddStandardRoute_radPortCity\" />{1}</label>", model.CityId, model.CityName);
                            }
                            else
                            {
                                str2.AppendFormat("<label for=\"AddStandardRoute_radPortCity{0}\" hideFocus=\"false\"><input type=\"radio\" name=\"AddStandardRoute_radPortCity\" id=\"AddStandardRoute_radPortCity{0}\" value=\"{0}\" />{1}</label>", model.CityId, model.CityName);
                            }                            
                        }
                    }
                    i++;
                }
            }
            if (str1.ToString() != string.Empty)
            {
                if (str2.ToString() != string.Empty)
                {
                    return str1.ToString() + "|" + str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardRoute&ContainerID=" + tblID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
                }
                else
                {
                    return str1.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardRoute&ContainerID=" + tblID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
                }
            }
            else
            {
                if (str2.ToString() != string.Empty)
                {
                    return str2.ToString() + "| <a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardRoute&ContainerID=" + tblID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
                }
                else
                {
                    return "<a href=\"javascript:void(0)\" onclick=\"TourModule.OpenDialog('设置常用出港城市','/RouteAgency/SetLeaveCity.aspx?ReleaseType=AddStandardRoute&ContainerID=" + tblID + "&rnd='+ Math.random(),650,450);return false;\"><span class=\"huise\">更多</span></a>";
                }
            }
        }
        #endregion 

        #region 修改初始化线路信息
        /// <summary>
        /// 修改初始化线路信息
        /// </summary>
        /// <param name="TourID"></param>
        private void InitRouteInfo(string RouteID)
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            EyouSoft.Model.TourStructure.RouteBasicInfo model = bll.GetRouteInfo(RouteID);
            if (model != null)
            {
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
                {
                    this.TabAddQuickRoute.Visible = true;
                    this.TabAddStandardRoute.Visible = false;
                }
                else
                {
                    this.TabAddQuickRoute.Visible = false;
                    this.TabAddStandardRoute.Visible = true;
                }

                for (int i = 0; i < this.AddStandardRoute_RouteArea.Items.Count; i++)
                {
                    if (!String.IsNullOrEmpty(this.AddStandardRoute_RouteArea.Items[i].Value))
                    {
                        if (model.AreaId == int.Parse(this.AddStandardRoute_RouteArea.Items[i].Value.Split('|')[0].ToString()))
                        {
                            this.AddStandardRoute_RouteArea.Items[i].Selected = true;
                        }
                    }
                }
                if (!String.IsNullOrEmpty(Utils.InputText(Request.QueryString["type"])) && Utils.InputText(Request.QueryString["type"]) == "edit")
                {
                    this.AddStandardRoute_hidRouteID.Value = model.ID;
                }
                this.AddStandardRoute_RouteName.Value = model.RouteName;
                this.AddStandardRoute_txtTourDays.Value = model.TourDays.ToString();

                strRouteTheme = InitRouteTopic(model.RouteTheme);
                strSaleCity = InitSaleCity(model.SaleCity);
                strLeaveCity = InitLeaveCity(model.LeaveCityId);

                // 报价等级
                this.AddStandardRoute_tourpricestand.RoutePriceDetails = model.PriceDetails;
                // 行程安排
                this.AddStandardRoute_StandardPlan.RouteStandardPlanInfo = model.StandardPlans;
                // 包含项目
                //this.AddStandardRoute_ServiceStandard.RouteServiceStandardInfo = model.ServiceStandard;
                // 服务标准
                this.AddStandardRoute_Remark.Value = model.ServiceStandard.SpeciallyNotice;
                this.AddStandardRoute_Service.Value = model.ServiceStandard.NotContainService;

                // 线路负责人
                this.AddStandardRoute_TourContactInfo.ContactName = model.ContactName;
                this.AddStandardRoute_TourContactInfo.ContactTel = model.ContactTel;
                this.AddStandardRoute_TourContactInfo.ContactMQID = model.ContactMQID;
                this.AddStandardRoute_TourContactInfo.CompanyID = UserInfoModel.CompanyID;
            }
            model = null;
            bll = null;
        }
        #endregion

        #region 判断线路名称是否存在
        private bool IsExists(string RouteName)
        {
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            return bll.Exists(UserInfoModel.CompanyID, RouteName);
        }
        #endregion

        #region 添加线路信息
        /// <summary>
        /// 添加线路信息
        /// </summary>
        /// <returns></returns>
        private bool InsertRouteInfo()
        {
            bool IsResult = false;
            string strErr = string.Empty;
            // 线路基本信息
            string hidRouteID = Utils.GetFormValue(this.AddStandardRoute_hidRouteID.UniqueID);    // 用于判断是添加还是修改

            string RouteArea = Utils.GetFormValue(this.AddStandardRoute_RouteArea.UniqueID);
            string RouteName = Utils.GetFormValue(this.AddStandardRoute_RouteName.UniqueID);
            string TourDays = Utils.GetFormValue(this.AddStandardRoute_txtTourDays.UniqueID);
            string RouteTopic = Utils.GetFormValue("AddStandardRoute_chkRouteTopic");
            string LeaveCity = Utils.GetFormValue("AddStandardRoute_radPortCity");
            string SaleCity = Utils.GetFormValue("AddStandardRoute_chkSaleCity");
            string Service = Utils.GetFormValue(this.AddStandardRoute_Service.UniqueID);
            string Remark = Utils.GetFormValue(this.AddStandardRoute_Remark.UniqueID);

            // 标准发布线路行程信息
            string StandardPlan = Utils.GetFormValue(this.AddStandardRoute_Remark.UniqueID);

            // 服务标准
            string ResideContent = Utils.GetFormValue("AddStandardRouteResideContent");
            string DinnerContent = Utils.GetFormValue("AddStandardRouteDinnerContent");
            string SightContent = Utils.GetFormValue("AddStandardRouteSightContent");
            string CarContent = Utils.GetFormValue("AddStandardRouteCarContent");
            string GuideContent = Utils.GetFormValue("AddStandardRouteGuideContent");
            string TrafficContent = Utils.GetFormValue("AddStandardRouteTrafficContent");
            string IncludeOtherContent = Utils.GetFormValue("AddStandardRouteIncludeOtherContent");

            // 线路负责人
            string ContactName = Utils.GetFormValue("AddStandardRoute_TourContact");
            string ContactTel = Utils.GetFormValue("AddStandardRoute_TourContactTel");
            string ContactMQID = Utils.GetFormValue("AddStandardRoute_TourContacMQ");
            string ContactUserName = Utils.GetFormValue("AddStandardRoute_TourContactUserName");
            if (LeaveCity == null || LeaveCity == string.Empty)
                LeaveCity = "0";
            #region 线路区域处理
            string AreaName = string.Empty;
            int AreaID = 0;
            EyouSoft.Model.SystemStructure.AreaType Areatype = EyouSoft.Model.SystemStructure.AreaType.国内短线;
            if (!String.IsNullOrEmpty(RouteArea) && StringValidate.IsInteger(RouteArea))
            {
                EyouSoft.IBLL.SystemStructure.ISysArea AreaBll = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
                EyouSoft.Model.SystemStructure.SysArea AreaModel = AreaBll.GetSysAreaModel(int.Parse(RouteArea));
                if (AreaModel != null)
                {
                    AreaID = AreaModel.AreaId;
                    AreaName = AreaModel.AreaName;
                    Areatype = AreaModel.RouteType;
                }
                AreaModel = null;
                AreaBll = null;
            }
            #endregion

            #region 线路主题
            IList<int> RouteThemeList = new List<int>();
            if (!String.IsNullOrEmpty(RouteTopic))
            {
                string[] strRouteTopic = RouteTopic.Split(',');
                //string[] strRouteTopic = new string[] { "1","2","3"};
                foreach (string str in strRouteTopic)
                {
                    RouteThemeList.Add(int.Parse(str));
                }
            }
            #endregion

            #region 销售城市
            IList<int> SaleCityList = new List<int>();
            if (!String.IsNullOrEmpty(SaleCity))
            {
                string[] strSaleCity = SaleCity.Split(',');
                foreach (string str in strSaleCity)
                {
                    SaleCityList.Add(int.Parse(str));
                }
            }
            #endregion

            // 写入线路信息
            EyouSoft.IBLL.TourStructure.IRouteBasicInfo bll = EyouSoft.BLL.TourStructure.RouteBasicInfo.CreateInstance();
            EyouSoft.Model.TourStructure.RouteBasicInfo model = new EyouSoft.Model.TourStructure.RouteBasicInfo();
            model.AreaId = AreaID;
            model.AreaType = Areatype;
            model.CompanyID = UserInfoModel.CompanyID;
            model.CompanyName = UserInfoModel.CompanyName;
            model.ContactMQID = ContactMQID;
            model.ContactName = ContactName;
            model.ContactTel = ContactTel;
            model.ContactUserName = ContactUserName;
            model.IsAccept = false;
            model.IssueTime = DateTime.Now;
            model.LeaveCityId = int.Parse(LeaveCity);
            model.OperatorID = UserInfoModel.ID;
            model.PriceDetails = InsertRoutePriceDetail();
            model.QuickPlan = string.Empty;
            model.ReleaseType = EyouSoft.Model.TourStructure.ReleaseType.Standard;
            model.RouteName = RouteName;
            model.RouteTheme = RouteThemeList;
            model.SaleCity = SaleCityList;
            model.StandardPlans = InsertRouteStandardPlan();
            model.TourDays = int.Parse(TourDays);
            // 服务标准
            EyouSoft.Model.TourStructure.RouteServiceStandard RouteServiceModel = new EyouSoft.Model.TourStructure.RouteServiceStandard();
            if (!String.IsNullOrEmpty(ResideContent)) RouteServiceModel.ResideContent = ResideContent;
            if (!String.IsNullOrEmpty(DinnerContent)) RouteServiceModel.DinnerContent = DinnerContent;
            if (!String.IsNullOrEmpty(CarContent)) RouteServiceModel.CarContent = CarContent;
            if (!String.IsNullOrEmpty(SightContent)) RouteServiceModel.SightContent = SightContent;
            if (!String.IsNullOrEmpty(GuideContent)) RouteServiceModel.GuideContent = GuideContent;
            if (!String.IsNullOrEmpty(TrafficContent)) RouteServiceModel.TrafficContent = TrafficContent;
            if (!String.IsNullOrEmpty(IncludeOtherContent)) RouteServiceModel.IncludeOtherContent = IncludeOtherContent;
            if (!String.IsNullOrEmpty(Service)) RouteServiceModel.NotContainService = Service;
            if (!String.IsNullOrEmpty(Remark)) RouteServiceModel.SpeciallyNotice = Remark;
            model.ServiceStandard = RouteServiceModel;

            if (!String.IsNullOrEmpty(hidRouteID))   // 修改
            {
                model.ID = hidRouteID;
                IsResult = bll.UpdateRouteInfo(model);
            }
            else
            {
                model.ID = Guid.NewGuid().ToString();
                IsResult = bll.InsertRouteInfo(model);
            }

            return IsResult;
        }

        #region 写入线路报价信息
        /// <summary>
        /// 写入线路报价信息
        /// </summary>
        /// <returns></returns>
        private IList<EyouSoft.Model.TourStructure.RoutePriceDetail> InsertRoutePriceDetail()
        {
            string[] PriceStand = Utils.GetFormValues("drpPriceRank");
            string[] hidCustomerLevelID = Utils.GetFormValues("hidCustomerLevelID");

            IList<EyouSoft.Model.TourStructure.RoutePriceDetail> priceList = new List<EyouSoft.Model.TourStructure.RoutePriceDetail>();
            int i = 0;
            if (PriceStand != null)
            {
                foreach (string pricestand in PriceStand)
                {
                    IList<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail> list = new List<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail>();
                    EyouSoft.Model.TourStructure.RoutePriceDetail priceModel = new EyouSoft.Model.TourStructure.RoutePriceDetail();
                    priceModel.PriceStandId = pricestand;

                    foreach (string customerlevelid in hidCustomerLevelID)
                    {
                        string[] PeoplePrice = Utils.GetFormValues("PeoplePrice" + customerlevelid);
                        string[] ChildPrice = Utils.GetFormValues("ChildPrice" + customerlevelid);

                        EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail model = new EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail();
                        if (!String.IsNullOrEmpty(PeoplePrice[i]) && Utils.GetDecimal(PeoplePrice[i]) != 0)
                            model.AdultPrice = decimal.Parse(PeoplePrice[i]);
                        else
                            model.AdultPrice = 0;
                        if (!String.IsNullOrEmpty(ChildPrice[i]) && Utils.GetDecimal(ChildPrice[i]) != 0)
                            model.ChildrenPrice = decimal.Parse(ChildPrice[i]);
                        else
                            model.ChildrenPrice = 0;
                        model.CustomerLevelId = int.Parse(customerlevelid);
                        list.Add(model);
                        model = null;
                    }
                    priceModel.PriceDetail = list;
                    list = null;
                    priceList.Add(priceModel);
                    priceModel = null;
                    i++;
                }
            }
            return priceList;
        }
        #endregion

        #region 写入线路行程信息
        /// <summary>
        /// 写入线路行程信息
        /// </summary>
        private IList<EyouSoft.Model.TourStructure.RouteStandardPlan> InsertRouteStandardPlan()
        {
            string[] DayJourney = Utils.GetFormValues("DayJourney");
            IList<EyouSoft.Model.TourStructure.RouteStandardPlan> routeList = new List<EyouSoft.Model.TourStructure.RouteStandardPlan>();

            // 写入线路行程信息     
            if (DayJourney != null)
            {
                foreach (string dayplan in DayJourney)
                {
                    EyouSoft.Model.TourStructure.RouteStandardPlan model = new EyouSoft.Model.TourStructure.RouteStandardPlan();
                    //model.PlanID = Guid.NewGuid().ToString();
                    model.PlanDay = int.Parse(dayplan);
                    model.PlanInterval = Utils.GetFormValue("SightArea" + dayplan);
                    model.Vehicle = Utils.GetFormValue("Vehicle" + dayplan);
                    model.TrafficNumber = Utils.GetFormValue("TrafficNumber" + dayplan);
                    model.House = Utils.GetFormValue("Resideplan" + dayplan);
                    model.Dinner = Utils.GetFormValue("DinnerPlan" + dayplan);
                    model.PlanContent = Utils.GetFormValue("JourneyInfo" + dayplan);
                    routeList.Add(model);
                    model = null;
                }
            }
            return routeList;
        }
        #endregion

        #endregion
    }
}
