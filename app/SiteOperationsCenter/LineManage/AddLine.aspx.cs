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
using EyouSoft.Common;
using System.Collections.Generic;
using System.Text;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.SystemStructure;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common.Function;
using EyouSoft.Model.CompanyStructure;
using EyouSoft.Model.VisaStructure;
using EyouSoft.Model.TourStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 线路管理 线路区域添加修改
    /// 蔡永辉 2011-12-19
    /// </summary>
    public partial class AddLine : EyouSoft.Common.Control.YunYingPage
    {
        protected string lineid = "";//线路区域id
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Utils.GetQueryStringValue("type");//ajax请求类型
            string argument = Utils.GetQueryStringValue("argument");//ajax处理参数
            string dropLineType = Utils.GetQueryStringValue("dropLineType"); //线路区域类型
            string drLine = Utils.GetQueryStringValue("dropLine");//线路区域
            lineid = Utils.GetQueryStringValue("LineId");
            hidOperatType.Value = Utils.GetQueryStringValue("OperatType");//判断是复制还是修改
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(type))
                {
                    //ajax 异步请求
                    ajax(type, argument);
                }
                else
                {

                    //初始化绑定主题
                    BindThemId(null);
                    BindDropDonwList();
                    //绑定销售城市
                    //InitSaleCity(null, dropBusinessLine.SelectedValue);
                    if (!string.IsNullOrEmpty(lineid))
                    {
                        initinfo();
                    }
                    else
                    {
                        //绑定下拉列表

                        //选中专线类型
                        dropWord.SelectedValue = dropLineType;
                        //选中专线
                        //dropLine.SelectedValue = drLine;
                        //if (Utils.GetInt(drLine) > 0)
                        //    MessageBox.ResponseScript(this.Page, "OnchangeWord('" + drLine + "','GetCompanyByLine',null);OnchangeWordByGey('" + drLine + "','listMBrowseCountryControl',null);");
                    }
                }
            }
        }

        #region 初始化数据
        protected void initinfo()
        {
            MRoute modelinfo = new MRoute();
            if (!string.IsNullOrEmpty(lineid))
            {
                modelinfo = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(lineid);
                if (modelinfo != null)
                {
                    if (modelinfo.RouteSource == RouteSource.地接社添加)
                    {
                        chkdj.Checked = true;
                        lblSSArea.Text = "全国";
                    }
                    else
                    {
                        chkzx.Checked = true;
                        //初始化绑定销售城市
                        hidsqlcity.Value = InitSaleCity(modelinfo.Citys, modelinfo.Publishers);
                    }
                    this.dropWord.Items.Clear();
                    this.dropWord.Items.Add(new ListItem(modelinfo.RouteType.ToString(), ((int)modelinfo.RouteType).ToString()));

                    MessageBox.ResponseScript(this.Page, "$(\"#btnSelectTravel\").css(\"display\",\"none\");");
                    MessageBox.ResponseScript(this.Page, "$(\"#a_AreaType\").css(\"display\",\"none\");");

                    this.hideAreaTypeID.Value = modelinfo.AreaId.ToString();
                    this.txtAreaType.Text = modelinfo.AreaName;
                    this.hideCompanyID.Value = modelinfo.Publishers;
                    this.txtCompany.Text = modelinfo.PublishersName;
                    InitSaleCity(null, modelinfo.Publishers);
                    this.dropPublisher.Items.Clear();
                    this.dropPublisher.Items.Add(new ListItem(modelinfo.OperatorName, modelinfo.OperatorId));

                    //this.dropPublisher.SelectedValue = modelinfo.OperatorId;
                    txt_D2BwebSite.Value = modelinfo.B2CRouteName;
                    txt_Adultdeposit.Value = modelinfo.AdultPrice < 0
                                                 ? string.Empty
                                                 : modelinfo.AdultPrice.ToString("0.00");
                    txt_AdvanceDayRegistration.Value = modelinfo.AdvanceDayRegistration.ToString();
                    txt_LineName.Value = modelinfo.RouteName;
                    dropB2B.SelectedValue = ((int)modelinfo.B2B).ToString();
                    dropB2C.SelectedValue = ((int)modelinfo.B2C).ToString();
                    txt_B2B.Value = modelinfo.B2BOrder.ToString();
                    txt_B2C.Value = modelinfo.B2COrder.ToString();
                    txt_D2BwebSite.Value = modelinfo.B2CRouteName;
                    dropRecommendType.SelectedValue = ((int)modelinfo.RecommendType).ToString();
                    txt_LineFeatures.Value = modelinfo.Characteristic;
                    txt_Tourminnumber.Value = modelinfo.GroupNum.ToString();
                    txt_TeamPrice.Value = modelinfo.IndependentGroupPrice.ToString("0.00");
                    txt_Childrendeposit.Value = modelinfo.ChildrenPrice < 0
                                                    ? string.Empty
                                                    : modelinfo.ChildrenPrice.ToString("0.00");
                    AddStandardTour_txtTourDays.Value = modelinfo.Day.ToString();
                    dropEndTraffic.SelectedValue = ((int)modelinfo.EndTraffic).ToString();
                    dropStartTraffic.SelectedValue = ((int)modelinfo.StartTraffic).ToString();
                    txt_Late.Value = modelinfo.Late.ToString();
                    if (modelinfo.IsCertain)
                    {
                        this.chkIsCertain.Checked = true;
                    }
                    else
                    {
                        this.chkIsCertain.Checked = false;
                    }
                    #region 线路配图
                    if (!String.IsNullOrEmpty(modelinfo.RouteImg))
                    {
                        this.lalUploadImg.Text += string.Format("<a href='{0}' target='_blank'>己上传</a>", EyouSoft.Common.Domain.FileSystem + modelinfo.RouteImg, modelinfo.RouteImg);
                        this.hidUploadImg.Value = modelinfo.RouteImg + "|" + modelinfo.RouteImg1;
                    }
                    #endregion
                    //初始化绑定主题
                    BindThemId(modelinfo.Themes);
                    #region 设置类型（行程，报价）
                    if ((int)modelinfo.ReleaseType == 0)
                        MessageBox.ResponseScript(Page, "x:HoverLi(2);");
                    else
                        MessageBox.ResponseScript(Page, "x:HoverLi(1);");
                    if (modelinfo.FitQuotation == "" || (modelinfo.FitQuotation != "" && modelinfo.ServiceStandard.CarContent != ""))
                        MessageBox.ResponseScript(Page, "HoverLi1(2);");
                    else if (modelinfo.ServiceStandard.CarContent == "")
                        MessageBox.ResponseScript(Page, "HoverLi1(1);");
                    #endregion
                    hidReleaseType.Value = ((int)modelinfo.ReleaseType).ToString();
                    //出发城市
                    InitLeaveCity(modelinfo.StartCity, modelinfo.Publishers);
                    //放回城市
                    InitBackCity(modelinfo.EndCity, modelinfo.Publishers);
                    //主要浏览城市
                    InitBrowseCity(modelinfo.BrowseCitys, modelinfo.AreaId);
                    //主要预览国家
                    InitBrowseCountry(modelinfo.BrowseCountrys, modelinfo.AreaId, modelinfo.IsNotVisa);
                    txt_VendorInformation.Value = modelinfo.VendorsNotes;
                    txtTravel.Text = modelinfo.FastPlan;
                    this.AddStandardTour_StandardPlan.RouteStandardPlanInfo = modelinfo.StandardPlans;//行程标准
                    txt_Priceincludes.Value = modelinfo.FitQuotation;
                    this.AddStandardRoute_ServiceStandard.RouteServiceStandardInfo = modelinfo.ServiceStandard;//服务项目
                    txt_Childrenandotherarrangements.Value = modelinfo.ServiceStandard.ChildrenInfo;
                    txt_Projectattheirownexpense.Value = modelinfo.ServiceStandard.ExpenseItem;
                    txt_Giftitems.Value = modelinfo.ServiceStandard.GiftInfo;
                    txt_PriceExcluding.Value = modelinfo.ServiceStandard.NotContainService;
                    txt_Remarks.Value = modelinfo.ServiceStandard.Notes;
                    txt_Shoppingarrangements.Value = modelinfo.ServiceStandard.ShoppingInfo;
                    #region 设置控件为只读
                    if (hidOperatType.Value == "")
                    {
                        this.dropWord.Enabled = false;
                        this.dropPublisher.Enabled = false;
                        this.chkdj.Attributes.Add("Disabled", "Disabled");
                        this.chkzx.Attributes.Add("Disabled", "Disabled");
                    }
                    #endregion
                }

            }

        }
        #endregion

        #region 初始化绑定主题
        private void BindThemId(IList<MThemeControl> thembid)
        {
            StringBuilder strchb = new StringBuilder("");
            IList<string> listthemb = new List<string>();
            if (thembid != null)
            {
                foreach (var model in thembid)
                {
                    if (model.ThemeId != "")
                        listthemb.Add(model.ThemeId);
                }
            }
            int i = 0;
            foreach (var item in EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetSysFieldList(EyouSoft.Model.SystemStructure.SysFieldType.线路主题))
            {
                if (listthemb != null)
                {
                    if (listthemb.Contains(item.FieldId.ToString()))
                        strchb.AppendFormat("<input type='checkbox' checked name='chbTheme' value='{0}' id='cbxt_{1}' /><label for='cbxt_{1}'>{2}</label>", item.FieldId, item.FieldId, item.FieldName);
                    else
                        strchb.AppendFormat("<input type='checkbox' name='chbTheme' value='{0}' id='cbxt_{1}' /><label for='cbxt_{1}'>{2}</label>", item.FieldId, item.FieldId, item.FieldName);
                }
                else
                {
                    strchb.AppendFormat("<input type='checkbox' name='chbTheme' value='{0}' id='cbxt_{1}' /><label for='cbxt_{1}'>{2}</label>", item.FieldId, item.FieldId, item.FieldName);
                }
                if (i % 8 == 0 && i > 0)
                {
                    strchb.Append("</br>");
                }
                i++;
            }
            MessageBox.ResponseScript(this.Page, "SetMThemeControl(\"" + strchb.ToString() + "\");");
        }
        #endregion

        #region  ajax请求
        protected void ajax(string t, string argument)
        {
            switch (t)
            {
                case "GetLineByType"://根据专线类型获取专线
                    #region 代码主体
                    ISysArea bllLineByType = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance();
                    int typeLine = Utils.GetInt(argument, 0);
                    IList<SysArea> listSysArea = bllLineByType.GetSysAreaList((AreaType)typeLine);
                    StringBuilder strbLineByType = new StringBuilder("{tolist:[");
                    foreach (SysArea item in listSysArea)
                    {
                        strbLineByType.Append("{\"AreaId\":\"" + item.AreaId + "\",\"AreaName\":\"" + item.AreaName + "\"},");
                    }
                    Response.Clear();
                    Response.Write(strbLineByType.ToString().TrimEnd(',') + "]}");
                    Response.End();
                    #endregion
                    break;

                case "GetCompanyByLine"://获取专线商根据线路
                    #region 代码主体
                    int areaid = Utils.GetInt(Utils.GetQueryStringValue("argument"));
                    QueryNewCompany Searchmodel = new QueryNewCompany();
                    if (areaid != 0)
                        Searchmodel.AreaId = areaid;
                    if (bool.Parse(Utils.GetQueryStringValue("IsChecked")))
                    {
                        CompanyType?[] listCompanyType = new CompanyType?[] { null };
                        listCompanyType[0] = CompanyType.地接;
                        Searchmodel.CompanyTypes = listCompanyType;
                    }
                    IList<CompanyDetailInfo> listCompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetAllCompany(0, Searchmodel);
                    StringBuilder strb = new StringBuilder("{tolist:[");
                    foreach (Company item in listCompany)
                    {
                        strb.Append("{\"ID\":\"" + item.ID + "\",\"CompanyName\":\"" + item.CompanyName + "\"},");
                    }
                    Response.Clear();
                    Response.Write(strb.ToString().TrimEnd(',') + "]}");
                    Response.End();
                    #endregion
                    break;

                case "GetContactByBusinessLineId"://获取联系人通过专线商
                    #region 代码主体
                    string companyId = Utils.GetQueryStringValue("argument");
                    if (!string.IsNullOrEmpty(companyId))
                    {
                        EyouSoft.Model.CompanyStructure.QueryParamsUser modelUser = new EyouSoft.Model.CompanyStructure.QueryParamsUser { IsShowAdmin = true };
                        IList<CompanyUserBase> CompanyUserList = EyouSoft.BLL.CompanyStructure.CompanyUser.CreateInstance().GetList(companyId, modelUser);
                        StringBuilder strbContact = new StringBuilder("{tolist:[");
                        foreach (var item in CompanyUserList)
                        {
                            strbContact.Append("{\"ID\":\"" + item.ID + "\",\"UserNameID\":\"" + item.UserName + "\"},");
                        }
                        Response.Clear();
                        Response.Write(strbContact.ToString().TrimEnd(',') + "]}");
                        Response.End();
                    }
                    #endregion
                    break;
                case "BindSaleCite"://获取销售城市
                    string companyIdSaleCite = Utils.GetQueryStringValue("argument");
                    Response.Clear();
                    Response.Write(InitSaleCity(null, companyIdSaleCite));
                    Response.End();
                    break;

                case "BindLeaveCity"://获取出港城市
                    string companyIdLeaveCity = Utils.GetQueryStringValue("argument");
                    Response.Clear();
                    Response.Write(InitLeaveCity(-1, companyIdLeaveCity));
                    Response.End();
                    break;

                case "BindBackCity"://获取返回城市
                    string companyIdBackCity = Utils.GetQueryStringValue("argument");
                    Response.Clear();
                    Response.Write(InitBackCity(-1, companyIdBackCity));
                    Response.End();
                    break;

                case "listMBrowseCountryControl"://获取主要预览国家
                    #region 代码主体
                    string companyIdBrowseCountry = Utils.GetQueryStringValue("argument");
                    StringBuilder strBrowseCountryControl = new StringBuilder();
                    SysArea Sysarea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(Utils.GetInt(companyIdBrowseCountry));
                    int index = 1;
                    if (Sysarea != null && Sysarea.VisitCity != null)
                    {
                        //主要浏览国家
                        if (Sysarea.VisitCity != null && Sysarea.VisitCity.Count > 0)
                        {
                            IList<MSysCountry> lsCountry = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountryList(
                                Sysarea.VisitCity.Select(ls => ls.CountryId).ToArray()
                                );
                            if (lsCountry != null && lsCountry.Count > 0)
                            {
                                foreach (MSysCountry item in lsCountry)
                                {
                                    strBrowseCountryControl.AppendFormat("<label for=\"AddStandardTour_radCountry{0}\" hideFocus=\"false\"><input type=\"checkbox\" onclick=\"IsViso(this)\" name=\"AddStandardTour_radCountry\" id=\"AddStandardTour_radCountry{0}\"  value=\"{0}\" />{1}</label>", item.CountryId, item.CName);
                                    if (index % 8 == 0)
                                    {
                                        strBrowseCountryControl.AppendFormat("</br>");
                                    }
                                    index++;
                                }
                            }
                        }
                    }
                    else
                    {
                        strBrowseCountryControl.Append("暂无");
                    }
                    Response.Clear();
                    Response.Write(strBrowseCountryControl.ToString());
                    Response.End();
                    #endregion
                    break;


                case "listMBrowseCity"://主要浏览城市
                    #region 代码主体
                    string companyIdMBrowseCity = Utils.GetQueryStringValue("argument");
                    StringBuilder strBrowseMBrowseCity = new StringBuilder();
                    SysArea modelSysArea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(Utils.GetInt(companyIdMBrowseCity));
                    int indexcity = 1;
                    if (modelSysArea != null && modelSysArea.VisitCity != null)
                    {
                        //主要浏览城市
                        if (modelSysArea.VisitCity != null && modelSysArea.VisitCity.Count > 0)
                        {
                            IList<SysCity> lsCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(
                            modelSysArea.VisitCity.Where(ls => ls.CityId != 0 && ls.CountyId == 0).Select(ls => ls.CityId).ToArray()
                            );

                            //主要浏览县区
                            IList<SysDistrictCounty> listcontry = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCountyList(
                                modelSysArea.VisitCity.Select(ls => ls.CountyId).ToArray()
                            );
                            #region
                            //int[] ids = new int[modelSysArea.VisitCity.Count]; int indexID = 0;
                            //foreach (SysAreaVisitCity item in modelSysArea.VisitCity)
                            //{
                            //    if (item.CityId != 0 && item.CountyId == 0)
                            //    {
                            //        ids[indexID] = item.CityId;
                            //    }
                            //    indexID++;
                            //}
                            //indexID = 0;
                            //IList<SysCity> lsCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(ids);

                            //int[] countyids = new int[modelSysArea.VisitCity.Count];
                            //foreach (SysAreaVisitCity item in modelSysArea.VisitCity)
                            //{
                            //    countyids[indexID] = item.CountyId;
                            //    indexID++;
                            //}
                            //IList<SysDistrictCounty> listcontry = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCountyList(countyids);
                            #endregion
                            if (lsCity != null && lsCity.Count > 0)
                            {
                                foreach (SysCity item in lsCity)
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for=\"AddStandardTour_radCity{0}\" hideFocus=\"false\"><input type=\"checkbox\" onclick=\"IsViso()\" name=\"AddStandardTour_radCity\" id=\"AddStandardTour_radCity{0}\"  value=\"{0}\" />{1}</label>", item.CityId, item.CityName);
                                    //每8个换行
                                    if (indexcity % 8 == 0)
                                    {
                                        strBrowseMBrowseCity.AppendFormat("</br>");
                                    }
                                    indexcity++;
                                }
                            }
                            indexcity = indexcity % 8 == 0 ? 1 : indexcity % 8;
                            #region 主要浏览县区
                            if (listcontry != null && listcontry.Count > 0)
                            {
                                foreach (SysDistrictCounty modelDistrictCounty in listcontry)
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for=\"AddStandardTour_radCountry{0}\" hideFocus=\"false\"><input type=\"checkbox\" onclick=\"IsViso()\" name=\"AddStandardTour_radCountry\" id=\"AddStandardTour_radCountry{0}\"  value=\"{0}\" />{1}</label>", modelDistrictCounty.Id, modelDistrictCounty.DistrictName);
                                    //每8个换行
                                    if (indexcity % 8 == 0)
                                    {
                                        strBrowseMBrowseCity.AppendFormat("</br>");
                                    }
                                    indexcity++;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        strBrowseMBrowseCity.Append("暂无");
                    }
                    Response.Clear();
                    Response.Write(strBrowseMBrowseCity.ToString());
                    Response.End();
                    #endregion
                    break;
            }
        }


        #endregion

        #region 绑定下拉列表
        /// <summary>
        /// 绑定下拉列表
        /// </summary>
        protected void BindDropDonwList()
        {
            //国内国际周边
            this.dropWord.DataSource = EnumObj.GetList(typeof(AreaType)).Where(p => p.Value != "3");
            this.dropWord.DataTextField = "Text";
            this.dropWord.DataValueField = "Value";
            this.dropWord.DataBind();
            this.dropWord.Items.Insert(0, new ListItem("请选择", "-1"));


            //类型（推荐，特价）
            this.dropRecommendType.DataSource = EnumObj.GetList(typeof(RecommendType));
            this.dropRecommendType.DataTextField = "Text";
            this.dropRecommendType.DataValueField = "Value";
            this.dropRecommendType.DataBind();


            //B2B
            this.dropB2B.DataSource = EnumObj.GetList(typeof(RouteB2BDisplay));
            this.dropB2B.DataTextField = "Text";
            this.dropB2B.DataValueField = "Value";
            this.dropB2B.DataBind();
            this.dropB2B.Items.Insert(0, new ListItem("请选择", "-1"));

            //B2C
            this.dropB2C.DataSource = EnumObj.GetList(typeof(RouteB2CDisplay));
            this.dropB2C.DataTextField = "Text";
            this.dropB2C.DataValueField = "Value";
            this.dropB2C.DataBind();
            this.dropB2C.Items.Insert(0, new ListItem("请选择", "-1"));

            //大交通(出发)
            this.dropStartTraffic.DataSource = EnumObj.GetList(typeof(TrafficType));
            this.dropStartTraffic.DataTextField = "Text";
            this.dropStartTraffic.DataValueField = "Value";
            this.dropStartTraffic.DataBind();
            this.dropStartTraffic.Items.Insert(0, new ListItem("请选择", "0"));

            //大交通（回来）
            this.dropEndTraffic.DataSource = EnumObj.GetList(typeof(TrafficType));
            this.dropEndTraffic.DataTextField = "Text";
            this.dropEndTraffic.DataValueField = "Value";
            this.dropEndTraffic.DataBind();
            this.dropEndTraffic.Items.Insert(0, new ListItem("请选择", "0"));
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
            IList<EyouSoft.Model.SystemStructure.SysField> list = bll.GetSysFieldList(EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
            if (list != null && list.Count > 0)
            {
                int i = 0;     // 如果i=0加验证
                foreach (EyouSoft.Model.SystemStructure.SysField model in list)
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
                            str.AppendFormat("<label for=\"AddStandardTour_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardTour_chkRouteTopic\" id=\"AddStandardTour_chkRouteTopic{0}\" value=\"{0}\" checked />{1}</label>", model.FieldId, model.FieldName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardTour_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardTour_chkRouteTopic\" id=\"AddStandardTour_chkRouteTopic{0}\" value=\"{0}\" />{1}</label>", model.FieldId, model.FieldName);
                        }
                    }
                    else
                    {
                        if (tmpTopicID == model.FieldId)
                        {
                            str.AppendFormat("<label for=\"AddStandardTour_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardTour_chkRouteTopic\" id=\"AddStandardTour_chkRouteTopic{0}\" value=\"{0}\" checked />{1}</label>", model.FieldId, model.FieldName);
                        }
                        else
                        {
                            str.AppendFormat("<label for=\"AddStandardTour_chkRouteTopic{0}\" hideFocus=\"false\"><input type=\"checkbox\" name=\"AddStandardTour_chkRouteTopic\" id=\"AddStandardTour_chkRouteTopic{0}\" value=\"{0}\" />{1}</label>", model.FieldId, model.FieldName);
                        }
                    }
                    if (i % 8 == 0 && i > 0)
                    {
                        str.AppendLine("</br>");
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
        private string InitSaleCity(IList<MCityControl> SaleCityIdList, string companyid)
        {
            StringBuilder str = new StringBuilder();
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanySaleCity(companyid);
            if (list != null && list.Count > 0)
            {
                int i = 0;
                foreach (EyouSoft.Model.SystemStructure.CityBase model in list)
                {
                    int tmpSaleCityID = 0;
                    if (SaleCityIdList != null)
                    {
                        foreach (var SaleCityId in SaleCityIdList)
                        {
                            if (SaleCityId.CityId == model.CityId)
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
                            str.AppendFormat("<label for='AddLine_chkSaleCity{0}' hideFocus='false'><input type='checkbox' name='AddLine_chkSaleCity' value='{0}' id='AddLine_chkSaleCity{0}' checked valid='requireChecked' errmsg='请选择销售城市!' errmsgend='AddLine_chkSaleCity' />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                        else
                        {
                            str.AppendFormat("<label for='AddLine_chkSaleCity{0}' hideFocus='false'><input type='checkbox' name='AddLine_chkSaleCity' value='{0}' id='AddLine_chkSaleCity{0}' valid='requireChecked' errmsg='请选择销售城市!' errmsgend='AddLine_chkSaleCity' />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                    }
                    else
                    {
                        if (tmpSaleCityID == model.CityId)
                        {
                            str.AppendFormat("<label for='AddLine_chkSaleCity{0}' hideFocus='false'><input type='checkbox' name='AddLine_chkSaleCity' value='{0}' id='AddLine_chkSaleCity{0}' checked />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                        else
                        {
                            str.AppendFormat("<label for='AddLine_chkSaleCity{0}' hideFocus='false'><input type='checkbox' name='AddLine_chkSaleCity' value='{0}' id='AddLine_chkSaleCity{0}' />{1}</label>", model.CityId.ToString(), model.CityName);
                        }
                    }
                    if (i > 1 && i % 8 == 0)
                        str.Append("</br>");
                    i++;
                }
            }

            if (SaleCityIdList != null)
                MessageBox.ResponseScript(this.Page, "SetSaleCite(\"" + str.ToString() + "\")");
            return str.ToString();
        }
        #endregion

        #region 绑定出港城市
        private string InitLeaveCity(int LeaveCity, string CompanyId)
        {
            string result = "";
            StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
            StringBuilder str2 = new StringBuilder();  // 固定城市

            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            var modelcompanyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);

            if (list != null && list.Count > 0)
            {
                string[] defaultcity = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("DefaultCity").Split(',');
                foreach (CityBase modelcity in list)
                {
                    if (!defaultcity.Contains(modelcity.CityId.ToString()))
                    {
                        if (modelcity.CityId == LeaveCity)
                            str1.AppendFormat("<label for='AddStandardTour_radPortCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radPortCity' id='AddStandardTour_radPortCity{0}' checked value='{0}' />{1}</label>", modelcity.CityId, modelcity.CityName);
                        else
                            str1.AppendFormat("<label for='AddStandardTour_radPortCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radPortCity' id='AddStandardTour_radPortCity{0}' value='{0}' />{1}</label>", modelcity.CityId, modelcity.CityName);
                    }
                }

                foreach (string str in defaultcity)
                {
                    SysCity item = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(Utils.GetInt(str));
                    if (str == LeaveCity.ToString())
                        str2.AppendFormat("<label for='AddStandardTour_radPortCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radPortCity' id='AddStandardTour_radPortCity{0}' checked value='{0}' />{1}</label>", item.CityId, item.CityName);
                    else
                        str2.AppendFormat("<label for='AddStandardTour_radPortCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radPortCity' id='AddStandardTour_radPortCity{0}' value='{0}' />{1}</label>", item.CityId, item.CityName);
                }

            }
            if (!string.IsNullOrEmpty(str1.ToString()))
            {
                result = str1.ToString() + "|" + str2.ToString() + "| <a href='javascript:void(0)' onclick=\"OpenDialoge('" + CompanyId + "','" + LeaveCity + "');return false;\"><span class='huise'>更多</span></a>";
            }
            else
                result = str2.ToString() + "| <a href='javascript:void(0)' onclick=\"OpenDialoge('" + CompanyId + "','" + LeaveCity + "');return false;\"><span class='huise'>更多</span></a>";

            if (LeaveCity >= 0)
                litLeaveCity.Text = result;
            //MessageBox.ResponseScript(this.Page, "SetLeaveCity('" + result + "')");
            return result;
        }
        #endregion

        #region 绑定返回城市
        private string InitBackCity(int LeaveCity, string CompanyId)
        {
            string result = "";
            StringBuilder str1 = new StringBuilder();  // 当前公司注册时所在省份的出港城市
            StringBuilder str2 = new StringBuilder();  // 固定城市

            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            var modelcompanyinfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(CompanyId);
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(CompanyId);

            if (list != null && list.Count > 0)
            {
                string[] defaultcity = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("DefaultCity").Split(',');
                foreach (CityBase modelcity in list)
                {
                    if (!defaultcity.Contains(modelcity.CityId.ToString()))
                    {
                        if (modelcity.CityId == LeaveCity)
                            str1.AppendFormat("<label for='AddStandardTour_radBackCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radBackCity' id='AddStandardTour_radBackCity{0}' checked value='{0}' />{1}</label>", modelcity.CityId, modelcity.CityName);
                        else
                            str1.AppendFormat("<label for='AddStandardTour_radBackCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radBackCity' id='AddStandardTour_radBackCity{0}' value='{0}' />{1}</label>", modelcity.CityId, modelcity.CityName);
                    }
                }

                foreach (string str in defaultcity)
                {
                    SysCity item = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(Utils.GetInt(str));
                    if (str == LeaveCity.ToString())
                        str2.AppendFormat("<label for='AddStandardTour_radBackCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radBackCity' id='AddStandardTour_radBackCity{0}' checked  value='{0}' />{1}</label>", item.CityId, item.CityName);
                    else
                        str2.AppendFormat("<label for='AddStandardTour_radBackCity{0}' hideFocus='false'><input type='radio' name='AddStandardTour_radBackCity' id='AddStandardTour_radBackCity{0}' value='{0}' />{1}</label>", item.CityId, item.CityName);
                }
            }
            if (!string.IsNullOrEmpty(str1.ToString()))
            {
                result = str1.ToString() + "|" + str2.ToString() + "| <a href='javascript:void(0)' onclick=\"OpenDialoge('" + CompanyId + "','" + LeaveCity + "');return false;\"><span class='huise'>更多</span></a>";
            }
            else
                result = str2.ToString() + "| <a href='javascript:void(0)' onclick=\"OpenDialoge('" + CompanyId + "','" + LeaveCity + "');return false;\"><span class='huise'>更多</span></a>";
            if (LeaveCity >= 0)
                litbackcity.Text = result;
            //MessageBox.ResponseScript(this.Page, "SetBackCity(\"" + result + "\")");
            return result;

        }
        #endregion

        #region 绑定主要预览城市
        /// <summary>
        /// 绑定主要预览城市
        /// </summary>
        private void InitBrowseCity(IList<MBrowseCityControl> BrowseCityList, int companyIdMBrowseCity)
        {
            StringBuilder strBrowseMBrowseCity = new StringBuilder();
            SysArea modelSysArea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(companyIdMBrowseCity);
            int indexcity = 1;
            if (modelSysArea != null && modelSysArea.VisitCity != null)
            {
                //主要浏览城市
                if (modelSysArea.VisitCity != null && modelSysArea.VisitCity.Count > 0)
                {
                    //主要浏览城市
                    IList<SysCity> lsCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(
                    modelSysArea.VisitCity.Where(ls => ls.CityId != 0 && ls.CountyId == 0).Select(ls => ls.CityId).ToArray()
                    );
                    //主要浏览县区
                    IList<SysDistrictCounty> listcontry = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCountyList(
                        modelSysArea.VisitCity.Select(ls => ls.CountyId).ToArray()
                    );
                    #region 主要浏览城市

                    if (lsCity != null && lsCity.Count > 0)
                    {
                        foreach (SysCity item in lsCity)
                        {
                            if (BrowseCityList != null && BrowseCityList.Count > 0)
                            {
                                if (BrowseCityList.Select(r => r.CityId).Contains(item.CityId))
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCity{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCity' Checked id='AddStandardTour_radCity{0}'  value='{0}' />{1}</label>", item.CityId, item.CityName);
                                }
                                else
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCity{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCity' id='AddStandardTour_radCity{0}'  value='{0}' />{1}</label>", item.CityId, item.CityName);
                                }
                            }
                            else
                            {
                                strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCity{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCity' id='AddStandardTour_radCity{0}'  value='{0}' />{1}</label>", item.CityId, item.CityName);

                            }
                            if (indexcity % 8 == 0)
                            {
                                strBrowseMBrowseCity.AppendFormat("</br>");
                            }
                            indexcity++;
                        }
                    #endregion
                    }
                    //合并城市和县区
                    indexcity = indexcity % 8 == 0 ? 1 : indexcity % 8;
                    #region 主要浏览县区
                    if (listcontry != null && listcontry.Count > 0)
                    {
                        foreach (SysDistrictCounty modelDistrictCounty in listcontry)
                        {
                            if (BrowseCityList != null && BrowseCityList.Count > 0)
                            {
                                if (BrowseCityList.Select(r => r.CountyId).Contains(modelDistrictCounty.Id))
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCountry' Checked id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", modelDistrictCounty.Id, modelDistrictCounty.DistrictName);
                                }
                                else
                                {
                                    strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCountry' id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", modelDistrictCounty.Id, modelDistrictCounty.DistrictName);
                                }
                            }
                            else
                            {
                                strBrowseMBrowseCity.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso()' name='AddStandardTour_radCountry' id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", modelDistrictCounty.Id, modelDistrictCounty.DistrictName);

                            }
                            if (indexcity % 8 == 0)
                            {
                                strBrowseMBrowseCity.AppendFormat("</br>");
                            }
                            indexcity++;
                        }
                    }
                    #endregion
                }
            }
            MessageBox.ResponseScript(this.Page, "SetBrowseCity(\"" + strBrowseMBrowseCity.ToString() + "\")");
        }
        #endregion

        #region 初始化主要预览国家（签证）
        /// <summary>
        /// 初始化主要预览国家（签证）
        /// </summary>
        private void InitBrowseCountry(IList<MBrowseCountryControl> BrowseCountryList, int MBrowseCountryAreaid, bool isNotVisa)
        {
            StringBuilder strBrowseMBrowseCountry = new StringBuilder();
            StringBuilder strBrowseMBrowseViso = new StringBuilder();
            //strBrowseMBrowseViso.Append("<label for=\"AddStandardTour_radIsviso0\" hideFocus=\"false\"><input type=\"checkbox\"  name=\"IsOrNoradIsviso\" onclick=\"IsorNoViso(this)\" id=\"AddStandardTour_radIsviso0\"  value=\"0\" />免签</label>");
            SysArea modelSysArea = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(MBrowseCountryAreaid);
            int indexcity = 1;
            int indexViso = 1;
            if (modelSysArea != null && modelSysArea.VisitCity != null)
            {
                //主要浏览国家
                if (modelSysArea.VisitCity != null && modelSysArea.VisitCity.Count > 0)
                {
                    IList<MSysCountry> lsCountry = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountryList(
                                modelSysArea.VisitCity.Select(ls => ls.CountryId).ToArray()
                                );
                    foreach (MSysCountry item in lsCountry)
                    {
                        if (BrowseCountryList != null && BrowseCountryList.Count > 0)
                        {
                            if (BrowseCountryList.Select(r => r.CountryId).Contains(item.CountryId))
                            {
                                MBrowseCountryControl modelMBrowseCountryControl = new MBrowseCountryControl();
                                modelMBrowseCountryControl = BrowseCountryList.Where(r => r.CountryId == item.CountryId).ToList()[0];
                                strBrowseMBrowseCountry.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso(this)' name='AddStandardTour_radCountry' Checked id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName);
                                if (modelMBrowseCountryControl.IsVisa)
                                    strBrowseMBrowseViso.AppendFormat("<label for='AddStandardTour_radIsviso{0}' hideFocus='false'><input type='checkbox'  name='AddStandardTour_radIsviso' Checked id='AddStandardTour_radIsviso{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName + "签证");
                                else
                                    strBrowseMBrowseViso.AppendFormat("<label for='AddStandardTour_radIsviso{0}' hideFocus='false'><input type='checkbox'  name='AddStandardTour_radIsviso' id='AddStandardTour_radIsviso{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName + "签证");

                                if (indexViso % 8 == 0)
                                {
                                    strBrowseMBrowseViso.AppendFormat("</br>");
                                }
                                indexViso++;
                            }
                            else
                            {
                                strBrowseMBrowseCountry.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso(this)' name='AddStandardTour_radCountry' id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName);
                                //strBrowseMBrowseViso.AppendFormat("<label for='AddStandardTour_radIsviso{0}' hideFocus='false'><input type='checkbox'  name='AddStandardTour_radIsviso' id='AddStandardTour_radIsviso{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName + "签证");
                            }
                        }
                        else
                        {
                            strBrowseMBrowseCountry.AppendFormat("<label for='AddStandardTour_radCountry{0}' hideFocus='false'><input type='checkbox' onclick='IsViso(this)' name='AddStandardTour_radCountry' id='AddStandardTour_radCountry{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName);
                            //strBrowseMBrowseViso.AppendFormat("<label for='AddStandardTour_radIsviso{0}' hideFocus='false'><input type='checkbox'  name='AddStandardTour_radIsviso' id='AddStandardTour_radIsviso{0}'  value='{0}' />{1}</label>", item.CountryId, item.CName + "签证");

                        }
                        if (indexcity % 8 == 0)
                        {
                            strBrowseMBrowseCountry.AppendFormat("</br>");
                        }
                        indexcity++;
                    }
                }
            }

            MessageBox.ResponseScript(this.Page, "SetBrowseCountryControl(\"" + strBrowseMBrowseCountry.ToString() + "\");SetAddStandardTour_radIsviso(\"" + strBrowseMBrowseViso.ToString() + "\")");
            //免签的话 执行客户端免签事件
            if (isNotVisa)
            {
                AddStandardTour_radIsviso0.Checked = true;
                MessageBox.ResponseScript(this.Page,
                                          string.Format("IsorNoViso(document.getElementById(\"{0}\"))",
                                                        AddStandardTour_radIsviso0.ClientID));
            }
        }
        #endregion

        #region 保存数据
        protected void btn_save_Click(object sender, EventArgs e)
        {
            MRoute modelMRoute = new MRoute();
            if (!string.IsNullOrEmpty(lineid))
            {
                modelMRoute = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(lineid);
                if (hidOperatType.Value != "")
                {
                    if (chkdj.Checked)
                        modelMRoute.RouteSource = RouteSource.地接社添加;
                    else
                        modelMRoute.RouteSource = RouteSource.专线商添加;
                    modelMRoute.RouteType = (AreaType)Utils.GetInt(Utils.GetFormValue(dropWord.UniqueID));
                    modelMRoute.AreaId = Utils.GetInt(Utils.GetFormValue(this.hideAreaTypeID.UniqueID));
                    modelMRoute.Publishers = Utils.GetFormValue(this.hideCompanyID.UniqueID);
                    modelMRoute.OperatorId = Utils.GetFormValue(dropPublisher.UniqueID);
                    modelMRoute.AreaName = Utils.GetFormValue(this.txtAreaType.UniqueID);
                    modelMRoute.PublishersName = Utils.GetFormValue(this.txtCompany.UniqueID);
                    modelMRoute.OperatorName = dropPublisher.Items[dropPublisher.SelectedIndex].Text;
                }

                #region 主要预览国家（城市）
                if (modelMRoute.RouteType == AreaType.国际线)
                {
                    //国家
                    IList<MBrowseCountryControl> listBrowseCountry = new List<MBrowseCountryControl>();
                    string[] strlist = Utils.GetFormValues("AddStandardTour_radIsviso");
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCountry"))
                    {
                        MBrowseCountryControl modelMBrowseCountryControl = new MBrowseCountryControl();
                        modelMBrowseCountryControl.CountryId = Utils.GetInt(str);
                        if (Utils.GetFormValues("AddStandardTour_radIsviso").Contains(str))
                            modelMBrowseCountryControl.IsVisa = true;
                        else
                            modelMBrowseCountryControl.IsVisa = false;
                        listBrowseCountry.Add(modelMBrowseCountryControl);
                    }
                    modelMRoute.BrowseCountrys = listBrowseCountry;

                    modelMRoute.IsNotVisa = AddStandardTour_radIsviso0.Checked;
                }
                else
                {
                    //城市
                    IList<MBrowseCityControl> listBrowseCity = new List<MBrowseCityControl>();
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCity"))
                    {
                        MBrowseCityControl modelMBrowseCityControl = new MBrowseCityControl();
                        modelMBrowseCityControl.Id = Guid.NewGuid().ToString();
                        modelMBrowseCityControl.CityId = Utils.GetInt(str);
                        listBrowseCity.Add(modelMBrowseCityControl);
                    }
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCountry"))
                    {
                        MBrowseCityControl modelMBrowseCityControl = new MBrowseCityControl();
                        modelMBrowseCityControl.Id = Guid.NewGuid().ToString();
                        modelMBrowseCityControl.CountyId = Utils.GetInt(str);
                        listBrowseCity.Add(modelMBrowseCityControl);
                    }
                    modelMRoute.BrowseCitys = listBrowseCity;
                }
                #endregion
            }
            else
            {
                if (chkdj.Checked)
                    modelMRoute.RouteSource = RouteSource.地接社添加;
                else
                    modelMRoute.RouteSource = RouteSource.专线商添加;
                modelMRoute.RouteType = (AreaType)Utils.GetInt(Utils.GetFormValue(dropWord.UniqueID));
                modelMRoute.AreaId = Utils.GetInt(Utils.GetFormValue(this.hideAreaTypeID.UniqueID));
                modelMRoute.Publishers = Utils.GetFormValue(this.hideCompanyID.UniqueID);
                modelMRoute.OperatorId = Utils.GetFormValue(dropPublisher.UniqueID);
                modelMRoute.AreaName = Utils.GetFormValue(this.txtAreaType.UniqueID);
                modelMRoute.PublishersName = Utils.GetFormValue(this.txtCompany.UniqueID);
                modelMRoute.OperatorName = dropPublisher.Items[dropPublisher.SelectedIndex].Text;

                #region 主要预览国家（城市）
                if (Utils.GetFormValue(dropWord.UniqueID) == "1")
                {
                    //国家
                    IList<MBrowseCountryControl> listBrowseCountry = new List<MBrowseCountryControl>();
                    string[] strlist = Utils.GetFormValues("AddStandardTour_radIsviso");
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCountry"))
                    {
                        MBrowseCountryControl modelMBrowseCountryControl = new MBrowseCountryControl();
                        modelMBrowseCountryControl.CountryId = Utils.GetInt(str);
                        if (Utils.GetFormValues("AddStandardTour_radIsviso").Contains(str))
                            modelMBrowseCountryControl.IsVisa = true;
                        else
                            modelMBrowseCountryControl.IsVisa = false;
                        listBrowseCountry.Add(modelMBrowseCountryControl);
                    }
                    modelMRoute.BrowseCountrys = listBrowseCountry;
                    modelMRoute.IsNotVisa = AddStandardTour_radIsviso0.Checked;
                }
                else
                {
                    //城市
                    IList<MBrowseCityControl> listBrowseCity = new List<MBrowseCityControl>();
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCity"))
                    {
                        MBrowseCityControl modelMBrowseCityControl = new MBrowseCityControl();
                        modelMBrowseCityControl.Id = Guid.NewGuid().ToString();
                        modelMBrowseCityControl.CityId = Utils.GetInt(str);
                        listBrowseCity.Add(modelMBrowseCityControl);
                    }
                    foreach (string str in Utils.GetFormValues("AddStandardTour_radCountry"))
                    {
                        MBrowseCityControl modelMBrowseCityControl = new MBrowseCityControl();
                        modelMBrowseCityControl.Id = Guid.NewGuid().ToString();
                        modelMBrowseCityControl.CountyId = Utils.GetInt(str);
                        listBrowseCity.Add(modelMBrowseCityControl);
                    }
                    modelMRoute.BrowseCitys = listBrowseCity;
                }
                #endregion
            }
            modelMRoute.AdultPrice = Utils.GetDecimal(Utils.GetFormValue(txt_Adultdeposit.UniqueID), -1);
            modelMRoute.AdvanceDayRegistration = Utils.GetInt(Utils.GetFormValue(txt_AdvanceDayRegistration.UniqueID));
            modelMRoute.RecommendType = (RecommendType)Utils.GetInt(dropRecommendType.SelectedValue);
            modelMRoute.RouteName = Utils.GetFormValue(txt_LineName.UniqueID);
            modelMRoute.B2B = (RouteB2BDisplay)Utils.GetInt(Utils.GetFormValue(dropB2B.UniqueID));
            modelMRoute.B2C = (RouteB2CDisplay)Utils.GetInt(Utils.GetFormValue(dropB2C.UniqueID));
            modelMRoute.B2BOrder = Utils.GetInt(Utils.GetFormValue(txt_B2B.UniqueID));
            modelMRoute.B2COrder = Utils.GetInt(Utils.GetFormValue(txt_B2C.UniqueID));
            modelMRoute.B2CRouteName = txt_D2BwebSite.Value;
            modelMRoute.Characteristic = txt_LineFeatures.Value;
            modelMRoute.ChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_Childrendeposit.UniqueID), -1);
            modelMRoute.ClickNum = 0;
            modelMRoute.Day = Utils.GetInt(Utils.GetFormValue(AddStandardTour_txtTourDays.UniqueID));
            modelMRoute.Late = Utils.GetInt(Utils.GetFormValue(txt_Late.UniqueID));
            modelMRoute.GroupNum = Utils.GetInt(Utils.GetFormValue(txt_Tourminnumber.UniqueID));
            modelMRoute.IndependentGroupPrice = Utils.GetDecimal(Utils.GetFormValue(txt_TeamPrice.UniqueID));
            modelMRoute.EndTraffic = (TrafficType)Utils.GetInt(dropEndTraffic.SelectedValue);
            modelMRoute.StartTraffic = (TrafficType)Utils.GetInt(dropStartTraffic.SelectedValue);
            modelMRoute.IsCertain = Utils.GetFormValue(chkIsCertain.UniqueID) == "" ? false : true;
            #region 线路主题
            IList<MThemeControl> RouteThemeList = new List<MThemeControl>();
            foreach (string item in Utils.GetFormValues("chbTheme"))
            {
                MThemeControl modelTheme = new MThemeControl();
                modelTheme.ThemeId = item;
                modelTheme.Id = Guid.NewGuid().ToString();
                RouteThemeList.Add(modelTheme);
            }
            #endregion

            #region 销售城市
            IList<MCityControl> SaleCityList = new List<MCityControl>();
            if (!chkdj.Checked)
            {
                foreach (string str in Utils.GetFormValues("AddLine_chkSaleCity"))
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        MCityControl modelMCityControl = new MCityControl();
                        modelMCityControl.Id = Guid.NewGuid().ToString();
                        modelMCityControl.CityId = Utils.GetInt(str);
                        SaleCityList.Add(modelMCityControl);
                    }
                }
            }
            #endregion

            #region 线路配图
            if (!string.IsNullOrEmpty(Utils.GetFormValue(SfUpload.FindControl("hidFileName").UniqueID)))
            {
                modelMRoute.RouteImg = Utils.GetFormValue(SfUpload.FindControl("hidFileName").UniqueID).Split('|')[1];
                modelMRoute.RouteImg1 = Utils.GetFormValue(SfUpload.FindControl("hidFileName").UniqueID).Split('|')[0];
                modelMRoute.RouteImg2 = Utils.GetFormValue(SfUpload.FindControl("hidFileName").UniqueID).Split('|')[1];
            }
            else
            {
                if (hidUploadImg.Value != "")
                {
                    modelMRoute.RouteImg = hidUploadImg.Value.Split('|')[0];
                    modelMRoute.RouteImg1 = hidUploadImg.Value.Split('|')[1];
                    modelMRoute.RouteImg2 = hidUploadImg.Value.Split('|')[1];
                }
            }
            #endregion

            #region 出发城市
            string[] StartCityid = Utils.GetFormValues("AddStandardTour_radPortCity");
            if (StartCityid.Length == 1)
                modelMRoute.StartCity = Utils.GetInt(StartCityid[0]);
            #endregion

            #region 返回城市
            string[] BackCityid = Utils.GetFormValues("AddStandardTour_radBackCity");
            if (BackCityid.Length == 1)
                modelMRoute.EndCity = Utils.GetInt(BackCityid[0]);
            #endregion
            modelMRoute.Citys = SaleCityList;
            modelMRoute.TeamPlanDes = "";
            modelMRoute.Themes = RouteThemeList;
            modelMRoute.VendorsNotes = txt_VendorInformation.Value;
            modelMRoute.ReleaseType = (ReleaseType)Utils.GetInt(Utils.GetFormValue(hidReleaseType.UniqueID));
            modelMRoute.FastPlan = Utils.EditInputText(Request.Form[txtTravel.UniqueID]);
            modelMRoute.StandardPlans = InsertRouteStandardPlan();
            modelMRoute.FitQuotation = Utils.GetFormValue(txt_Priceincludes.UniqueID);
            #region  服务
            MServiceStandard modelServiceStandard = new MServiceStandard();
            modelServiceStandard.CarContent = Utils.GetFormValue("AddStandardRouteCarContent");
            modelServiceStandard.ChildrenInfo = Utils.GetFormValue(txt_Childrenandotherarrangements.UniqueID);
            modelServiceStandard.DinnerContent = Utils.GetFormValue("AddStandardRouteDinnerContent");
            modelServiceStandard.ExpenseItem = Utils.GetFormValue(txt_Projectattheirownexpense.UniqueID);
            modelServiceStandard.GiftInfo = Utils.GetFormValue(txt_Giftitems.UniqueID);
            modelServiceStandard.GuideContent = Utils.GetFormValue("AddStandardRouteGuideContent");
            modelServiceStandard.IncludeOtherContent = Utils.GetFormValue("AddStandardRouteIncludeOtherContent");
            modelServiceStandard.NotContainService = Utils.GetFormValue(txt_PriceExcluding.UniqueID);
            modelServiceStandard.Notes = Utils.GetFormValue(txt_Remarks.UniqueID);
            modelServiceStandard.ResideContent = Utils.GetFormValue("AddStandardRouteResideContent");
            modelServiceStandard.ShoppingInfo = Utils.GetFormValue(txt_Shoppingarrangements.UniqueID);
            modelServiceStandard.SightContent = Utils.GetFormValue("AddStandardRouteSightContent");
            modelServiceStandard.TrafficContent = Utils.GetFormValue("AddStandardRouteTrafficContent");
            //modelMRoute.RouteStatus = (RouteStatus)Utils.GetInt(Utils.GetFormValue(dropRouteStatus.UniqueID));
            #endregion
            if (String.IsNullOrEmpty(lineid))
            {
                modelServiceStandard.Id = Guid.NewGuid().ToString();
                modelMRoute.RouteId = Guid.NewGuid().ToString();
                modelMRoute.ServiceStandard = modelServiceStandard;
                modelMRoute.IssueTime = DateTime.Now;
                if (chkdj.Checked)//地接社
                {
                    if (modelMRoute.ReleaseType == ReleaseType.Quick)
                    {

                        if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddGroundQuickRoute(modelMRoute) == modelMRoute.RouteId)
                        {
                            MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                        }
                        else
                            MessageBox.Show(Page, "添加失败");
                    }
                    else
                    {

                        if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddGroundStandardRoute(modelMRoute) == modelMRoute.RouteId)
                        {
                            MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                        }
                        else
                            MessageBox.Show(Page, "添加失败");
                    }
                }
                else
                {
                    if (modelMRoute.ReleaseType == ReleaseType.Quick)
                    {

                        if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddQuickRoute(modelMRoute) == modelMRoute.RouteId)
                        {
                            MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                        }
                        else
                            MessageBox.Show(Page, "添加失败");
                    }
                    else
                    {

                        if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddStandardRoute(modelMRoute) == modelMRoute.RouteId)
                        {
                            MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                        }
                        else
                            MessageBox.Show(Page, "添加失败");
                    }
                }
            }
            else
            {
                if (hidOperatType.Value == "copy")
                {
                    modelServiceStandard.Id = Guid.NewGuid().ToString();
                    modelMRoute.RouteId = Guid.NewGuid().ToString();
                    modelMRoute.ServiceStandard = modelServiceStandard;
                    modelMRoute.IssueTime = DateTime.Now;

                    #region 地接社
                    if (chkdj.Checked)
                    {
                        if (modelMRoute.ReleaseType == ReleaseType.Quick)
                        {
                            if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddGroundQuickRoute(modelMRoute) == modelMRoute.RouteId)
                            {
                                MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                            }
                            else
                                MessageBox.Show(Page, "添加失败");
                        }
                        else
                        {

                            if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddGroundStandardRoute(modelMRoute) == modelMRoute.RouteId)
                            {
                                MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                            }
                            else
                                MessageBox.Show(Page, "添加失败");
                        }

                    }
                    #endregion
                    #region 专线商
                    else
                    {
                        if (modelMRoute.ReleaseType == ReleaseType.Quick)
                        {
                            if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddQuickRoute(modelMRoute) == modelMRoute.RouteId)
                            {
                                MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                            }
                            else
                                MessageBox.Show(Page, "添加失败");
                        }
                        else
                        {
                            if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().AddStandardRoute(modelMRoute) == modelMRoute.RouteId)
                            {
                                MessageBox.ShowAndRedirect(Page, "添加成功", "LineList.aspx");
                            }
                            else
                                MessageBox.Show(Page, "添加失败");
                        }
                    }
                    #endregion
                }
                else
                {
                    modelMRoute.ServiceStandard = modelServiceStandard;
                    if (EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().UpdateRoute(modelMRoute))
                    {
                        MessageBox.ShowAndRedirect(Page, "修改成功", "LineList.aspx");
                    }
                    else
                        MessageBox.Show(Page, "修改失败");
                }
            }
        }
        #endregion

        #region 写入线路行程信息
        /// <summary>
        /// 写入线路行程信息
        /// </summary>
        private IList<MStandardPlan> InsertRouteStandardPlan()
        {
            string[] DayJourney = Utils.GetFormValues("DayJourney");
            IList<MStandardPlan> routeList = new List<MStandardPlan>();

            // 写入线路行程信息     
            if (DayJourney != null)
            {
                int index = 1;
                foreach (string dayplan in DayJourney)
                {
                    MStandardPlan model = new MStandardPlan();
                    model.Id = Guid.NewGuid().ToString();
                    model.PlanDay = index;
                    model.PlanInterval = Utils.GetFormValue("SightArea" + dayplan);
                    if (!string.IsNullOrEmpty(Utils.GetFormValue("Vehicle" + dayplan)))
                        model.Vehicle = (TrafficType)Enum.Parse(typeof(TrafficType), Utils.GetFormValue("Vehicle" + dayplan));
                    model.House = Utils.GetFormValue("Resideplan" + dayplan);
                    model.Early = Utils.GetFormValue("DinnerPlanEarly" + dayplan) == "早" ? true : false;
                    model.Center = Utils.GetFormValue("DinnerPlanCenter" + dayplan) == "中" ? true : false;
                    model.Late = Utils.GetFormValue("DinnerPlanLate" + dayplan) == "晚" ? true : false;
                    model.PlanContent = Utils.GetFormValue("JourneyInfo" + dayplan);
                    routeList.Add(model);
                    model = null;
                    index++;
                }
            }
            return routeList;
        }
        #endregion
    }
}