using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;
using EyouSoft.Model.VisaStructure;
using System.Text;
using EyouSoft.Model.CompanyStructure;

namespace SiteOperationsCenter.LineManage
{
    /// <summary>
    /// 运营后台 散拼计划管理 散拼修改
    /// 蔡永辉 2011-12-20
    /// </summary>
    public partial class UpdateScatteredFightPlan : YunYingPage
    {
        /// <summary>
        /// 图片路劲根目录
        /// </summary>
        protected string ImgURL = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
        /// <summary>
        /// 页面唯一标识
        /// </summary>
        protected string Key = string.Empty;
        /// <summary>
        /// 是否有行程信息
        /// </summary>
        protected bool isTravel = false, isServiceStandard = false;
        /// <summary>
        /// 团队编号,团队编号集合,线路Id
        /// </summary>
        protected string tourId = string.Empty, tourIds = string.Empty, routeId = string.Empty;
        /// <summary>
        /// 主要浏览国家或者城市,是否显示余位,是否批量修改
        /// </summary>
        protected bool citysOrCountrys, isShowMoreThan, isAllUpdata = false;
        protected string companyid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Key = "UpdateScatteredFightPlan" + Guid.NewGuid().ToString();
            isAllUpdata = Utils.GetQueryStringValue("isAllUpdata").ToLower() == "true";
            hd_tourIds.Value = Utils.GetQueryStringValue("tourIds");
            /* 此代码用于兼容批量修改散拼计划以及单个修改散拼计划时取团队编号
             * 批量修改散拼计划的时候线路信息取第一个团队编号的信息*/
            tourId = Request.QueryString["tourIds"] ?? Utils.GetQueryStringValue("tourId");
            tourId = tourId.Split(',')[0];
            /****************************************************************/
            BindDropList();
            string argument = Utils.GetQueryStringValue("argument");
            if (!string.IsNullOrEmpty(argument))
            {
                GetBussiness(argument);
            }


            if (Utils.GetQueryStringValue("Operating") == "UpSave")
            {
                Save();
            }
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 加载页面
        /// </summary>
        private void InitPage()
        {
            MPowderList model = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(tourId);
            companyid = model.Publishers;
            if (!isAllUpdata)
            {
                //绑定团队状态
                BindPowderTourStatus();
            }
            //绑定销售城市
            SalesArea();
            //绑定交通
            BindTraffic();
            //绑定线路区域主题
            BindTheme();

            if (model != null)
            {
                routeId = model.RouteId;
                if (!isAllUpdata)
                {
                    #region 团队报价标准
                    //出团时间
                    lbl_LeaveDate.Text = model.LeaveDate.ToString("yyyy-MM-dd");
                    //报名截止时间
                    txt_registrationEndDate.Text = model.RegistrationEndDate.ToString("yyyy-MM-dd");
                    //成人市场价
                    txt_retailAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.RetailAdultPrice);
                    //成人结算价
                    txt_settlementAudltPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementAudltPrice);
                    //儿童市场价
                    txt_retailChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.RetailChildrenPrice);
                    //儿童结算价
                    txt_settlementChildrenPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.SettlementChildrenPrice);
                    //单房差
                    txt_marketPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.MarketPrice);
                    //计划收客人数
                    txt_orderPeopleNum.Text = model.TourNum.ToString();
                    //是否显示余位
                    isShowMoreThan = model.IsLimit;
                    //余位
                    txt_moreThan.Text = model.MoreThan.ToString();
                    //留位
                    txt_saveNum.Text = model.SaveNum.ToString();
                    //航班出发时间
                    txt_startDate.Text = model.StartDate;
                    //航班返回时间
                    txt_endDate.Text = model.EndDate;
                    //集合说明时间
                    txt_setDec.Value = model.SetDec;
                    //领队全陪说明
                    txt_teamLeaderDec.Value = model.TeamLeaderDec;
                    //团队备注
                    txt_tourNotes.Value = model.TourNotes;
                    //团队状态
                    ddl_powderTourStatus.SelectedValue = ((int)model.PowderTourStatus).ToString();
                    //团队推荐
                    ddl_Status.SelectedValue = ((int)model.RecommendType).ToString();
                    //发布时间
                    litTime.Text = model.IssueTime.ToShortDateString();
                    //发布ip
                    litIP.Text = model.IP;
                    #endregion
                }
                #region 行程信息
                //绑定出发返回城市
                BindCity();
                //专线名称
                lbl_travelRangeName.Text = model.AreaName;
                //线路名称
                txt_LineName.Text = model.RouteName;
                //发布商
                txtPublishersName.Value = model.PublishersName;
                //发布人
                EyouSoft.Common.Function.MessageBox.ResponseScript(Page, "GetOperator('" + model.Publishers + "','" + model.OperatorId + "')");
                //B2B显示控制
                dropB2B.SelectedValue = ((int)model.B2B).ToString();
                //B2C显示控制
                dropB2C.SelectedValue = ((int)model.B2C).ToString();
                //B2B排序值
                txt_B2B.Value = model.B2BOrder.ToString();
                //B2B排序值
                txt_B2C.Value = model.B2COrder.ToString();
                //绑定推荐状态
                BindRecommendType(model.RecommendType == RecommendType.推荐);
                //推荐状态
                ddl_Status.SelectedValue = ((int)model.RecommendType).ToString();
                //出发交通
                ddl_DepartureTraffic.SelectedIndex = (int)model.StartTraffic - 1;
                //返回交通
                ddl_ReturnTraffic.SelectedIndex = (int)model.EndTraffic - 1;
                //出发城市
                hd_goCity.Value = model.StartCity + "|" + model.StartCityName;
                //返回城市
                hd_returnCity.Value = model.EndCity + "|" + model.EndCityName;
                //线路特色
                txt_LineFeatures.Value = model.Characteristic;
                //住宿天数
                txt_days.Text = model.Day.ToString();
                //住宿夜数
                txt_nights.Text = model.Late.ToString();
                //简易版行程
                txt_BriefnessTravel.Value = model.StandardStroke;
                //散客报价
                txt_FIT.Value = model.FitQuotation;
                //销售须知
                txt_Notes.Value = model.VendorsNotes;

                //主题
                hd_SubjectLine.Value = SetTheme(model.Themes);
                //销售区域
                hd_SellCity.Value = SetSalesArea(model.Citys);
                #region 主要浏览
                //主要浏览国家
                SysArea AreaModel = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(model.AreaId);
                if (AreaModel != null)
                {
                    if (model.RouteType == AreaType.国际线)
                    {
                        if (model.BrowseCountrys != null && model.BrowseCountrys.Count > 0)
                        {
                            //主要浏览国家
                            if (AreaModel.VisitCity != null && AreaModel.VisitCity.Count > 0)
                            {
                                IList<MSysCountry> lsCountry = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountryList(
                                    AreaModel.VisitCity.Select(ls => ls.CountryId).ToArray()
                                    );
                                if (lsCountry != null && lsCountry.Count > 0)
                                {
                                    rpt_VisitingNational.DataSource = lsCountry;
                                    rpt_VisitingNational.DataBind();
                                }
                            }

                            citysOrCountrys = true;
                            int i = model.BrowseCountrys.Count;
                            string str = string.Empty, qzstr = string.Empty;
                            while (i-- > 0)
                            {
                                str += model.BrowseCountrys[i].CountryId + ",";
                                //签证信息
                                if (model.BrowseCountrys[i].IsVisa)
                                {
                                    qzstr += model.BrowseCountrys[i].CountryId + ",";
                                }
                            }
                            if (qzstr.Length > 0)
                            {
                                hd_qz.Value = qzstr.Substring(0, qzstr.Length - 1);
                            }
                            hd_BrowseCountrys.Value = str.Substring(0, str.Length - 1);
                        }
                        //免签 直接显示免签
                        if (model.IsNotVisa)
                        {
                            chk_null.Checked = true;
                            hd_qz.Value = string.Empty;
                        }
                        //国际线路成人定金
                        txt_AdultDeposit.Text = model.AdultPrice < 0 ? string.Empty : Utils.FilterEndOfTheZeroDecimal(model.AdultPrice);
                        //国际线路儿童定金
                        txt_ChildrenDeposit.Text = model.ChildrenPrice < 0 ? string.Empty : Utils.FilterEndOfTheZeroDecimal(model.ChildrenPrice);

                    }
                    else
                    {

                        //主要浏览城市
                        if (AreaModel.VisitCity != null && AreaModel.VisitCity.Count > 0)
                        {
                            IList<SysCity> lsCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(
                                AreaModel.VisitCity.Where(ls => ls.CountyId == 0 && ls.CityId != 0).Select(ls => ls.CityId).ToArray()
                                );
                            IList<SysDistrictCounty> lsDistrictCounty = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCountyList(
                                  AreaModel.VisitCity.Select(ls => ls.CountyId).ToArray()
                                  );
                            if (lsDistrictCounty != null && lsDistrictCounty.Count > 0)
                            {
                                rpt_districtCounty.DataSource = lsDistrictCounty;
                                rpt_districtCounty.DataBind();
                            }
                            if (lsCity != null && lsCity.Count > 0)
                            {
                                rpt_browseCitys.DataSource = lsCity;
                                rpt_browseCitys.DataBind();
                            }
                        }
                        //主要浏览城市
                        if (model.BrowseCitys != null && model.BrowseCitys.Count > 0)
                        {
                            citysOrCountrys = false;
                            int i = model.BrowseCitys.Count;
                            string str = string.Empty;
                            while (i-- > 0)
                            {
                                if (model.BrowseCitys[i].CountyId > 0)
                                {
                                    str += model.BrowseCitys[i].CountyId + "|" + model.BrowseCitys[i].CityId + ",";
                                }
                                else
                                {
                                    str += model.BrowseCitys[i].CityId + ",";
                                }
                            }
                            hd_BrowseCitys.Value = str.Substring(0, str.Length - 1);

                        }
                    }
                }

                #endregion
                //行程
                if (model.StandardStroke.Length > 0)
                {
                    txt_BriefnessTravel.Value = model.StandardStroke;
                }
                else
                {
                    if (model.StandardPlans != null && model.StandardPlans.Count > 0)
                    {
                        isTravel = true;
                        rpt_standardPlans.DataSource = model.StandardPlans;
                        rpt_standardPlans.DataBind();
                    }
                }
                if (model.ServiceStandard != null)
                {
                    //报价不包含
                    txt_PriceExcluding.Value = model.ServiceStandard.NotContainService;
                    //赠送项目
                    txt_GiftItems.Value = model.ServiceStandard.GiftInfo;
                    //儿童及其他安排
                    txt_OtherArr.Value = model.ServiceStandard.ChildrenInfo;
                    //购物安排
                    txt_ShoppingArr.Value = model.ServiceStandard.ShoppingInfo;
                    //自费项目
                    txt_ThisConsumption.Value = model.ServiceStandard.ExpenseItem;
                    //备注
                    txt_Remarks.Value = model.ServiceStandard.Notes;
                    //判断是否为团队
                    isServiceStandard = model.ServiceStandard.CarContent.Length > 0 ||//用车
                        model.ServiceStandard.DinnerContent.Length > 0 ||//用餐
                        model.ServiceStandard.ResideContent.Length > 0 ||//住宿
                        model.ServiceStandard.SightContent.Length > 0 ||//景点
                        model.ServiceStandard.TrafficContent.Length > 0 ||//往返交通
                        model.ServiceStandard.GuideContent.Length > 0 ||//导游
                        model.ServiceStandard.IncludeOtherContent.Length > 0;//其他包含
                    if (isServiceStandard)
                        this.AddStandardRoute_ServiceStandard.RouteServiceStandardInfo = model.ServiceStandard;

                }
                #endregion
            }
        }
        /// <summary>
        /// 设置销售区域
        /// </summary>
        /// <returns></returns>
        private string SetSalesArea(IList<MCityControl> list)
        {
            string str = string.Empty;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    str += item.CityId + "," + item.CityName + "," + item.ProvinceId + "," + item.ProvinceName + "|";
                }
                return str.Substring(0, str.Length - 1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 设置以选择的主题
        /// </summary>
        /// <param name="list">主题列表</param>
        /// <returns></returns>
        private string SetTheme(IList<MThemeControl> list)
        {
            string retStr = string.Empty;
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    retStr += item.ThemeId + "," + item.ThemeName + "|";
                }
                return retStr.Substring(0, retStr.Length - 1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 绑定国家
        /// </summary>
        private void BindCountry()
        {
            IList<MSysCountry> list = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountryList();
            if (list != null && list.Count > 0)
            {
                rpt_VisitingNational.DataSource = list;
                rpt_VisitingNational.DataBind();
            }
        }
        /// <summary>
        /// 绑定销售区域
        /// </summary>
        private void SalesArea()
        {
            if (!String.IsNullOrEmpty(companyid))
            {
                EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
                IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanySaleCity(companyid);
                if (list != null && list.Count > 0)
                {
                    rpt_SalesArea.DataSource = list;
                    rpt_SalesArea.DataBind();
                    sp_SalesAreaMsg.Visible = false;
                }
            }
        }
        /// <summary>
        /// 绑定交通
        /// </summary>
        private void BindTraffic()
        {
            //获取交通列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(TrafficType));
            ddl_DepartureTraffic.DataTextField = "Text";
            ddl_DepartureTraffic.DataValueField = "Value";
            ddl_DepartureTraffic.DataSource = typeli;
            ddl_DepartureTraffic.DataBind();


            ddl_ReturnTraffic.DataTextField = "Text";
            ddl_ReturnTraffic.DataValueField = "Value";
            ddl_ReturnTraffic.DataSource = typeli;
            ddl_ReturnTraffic.DataBind();
        }
        /// <summary>
        /// 绑定推荐状态
        /// </summary>
        private void BindRecommendType(bool isdeltj)
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(RecommendType));
            ddl_Status.DataTextField = "Text";
            ddl_Status.DataValueField = "Value";
            if (isdeltj)
            {
                ddl_Status.DataSource = typeli;
            }
            else
            {
                ddl_Status.DataSource = typeli.Where(obj => obj.Value != ((int)RecommendType.推荐).ToString()).ToList();
            }
            ddl_Status.DataBind();

        }
        /// <summary>
        /// 绑定团队状态
        /// </summary>
        private void BindPowderTourStatus()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(PowderTourStatus));
            ddl_powderTourStatus.DataTextField = "Text";
            ddl_powderTourStatus.DataValueField = "Value";
            ddl_powderTourStatus.DataSource = typeli;
            ddl_powderTourStatus.DataBind();
        }
        /// <summary>
        /// 绑定出发返回城市
        /// </summary>
        private void BindCity()
        {
            IList<CityBase> list = new List<CityBase>();
            list = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanyPortCity(companyid)
                .Where(data =>
                    data.CityId != 48
                    && data.CityId != 292
                    && data.CityId != 295
                    && data.CityId != 362).ToList();
            /*
             * 48=广州 
             * 292=上海 
             * 295=成都
             * 362=杭州
             * 改此代码的哥们 恭喜你了
             */
            IList<SysCity> s = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(new int[] { 48, 292, 295, 362 });
            foreach (var ls in s)
            {
                list.Insert(0, (CityBase)ls);
            }
            if (list != null && list.Count > 0)
            {
                //出发城市
                rpt_DepartureCity.DataSource = list;
                rpt_DepartureCity.DataBind();
                //返回城市
                rpt_BackToCities.DataSource = list;
                rpt_BackToCities.DataBind();
            }
        }

        /// <summary>
        /// 获取线路联系人
        /// </summary>
        private void GetBussiness(string companyId)
        {
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
        }


        /// <summary>
        /// 绑定线路区域主题
        /// </summary>
        private void BindTheme()
        {
            IList<SysFieldBase> list = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetRouteTheme();
            if (list != null && list.Count > 0)
            {
                rpt_SubjectLine.DataSource = list;
                rpt_SubjectLine.DataBind();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            IPowderList bll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
            bool isSave = false;
            MPowderList model = bll.GetModel(tourId);
            if (model == null)
            {
                Response.Clear();
                Response.Write(isSave.ToString());
                Response.End();
                return;
            }
            string[] tourIds = Utils.GetFormValue(hd_tourIds.UniqueID).Split(',');
            #region 行程信息
            //主要浏览
            if (model.RouteType == AreaType.国际线)
            {
                //主要浏览国家
                model.BrowseCountrys = new List<MBrowseCountryControl>();
                string[] browseCountryControl = Utils.GetFormValue(hd_BrowseCountrys.UniqueID).Split(',');
                int i = browseCountryControl.Length;
                string[] qzArr = Utils.GetFormValue(hd_qz.UniqueID).Split(',');
                while (i-- > 0)
                {
                    MBrowseCountryControl browseCityControlModel = new MBrowseCountryControl();
                    browseCityControlModel.CountryId = Utils.GetInt(browseCountryControl[i]);
                    if (qzArr.Length > 0)
                    {
                        int j = qzArr.Length;
                        while (j-- > 0)
                        {
                            if (qzArr[j] == browseCountryControl[i])
                            {
                                browseCityControlModel.IsVisa = true;
                            }
                        }
                    }
                    model.BrowseCountrys.Add(browseCityControlModel);
                }
                model.IsNotVisa = chk_null.Checked;
                //国际线路成人定金
                model.AdultPrice = Utils.GetDecimal(Utils.GetFormValue(txt_AdultDeposit.UniqueID), -1);
                //国际线路儿童定金
                model.ChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_ChildrenDeposit.UniqueID), -1);
            }
            else
            {
                //主要浏览城市
                IList<MBrowseCityControl> browseCityControl = new List<MBrowseCityControl>();
                string[] browseCitys = Utils.GetFormValue(hd_BrowseCitys.UniqueID).Split(',');
                int i = browseCitys.Length;
                while (i-- > 0)
                {
                    MBrowseCityControl browseCityControlModel = new MBrowseCityControl();
                    //拆分城市县
                    int[] array = Utils.StringArrToIntArr(browseCitys[i].Split('|'));
                    if (array.Length > 1)
                    {
                        //县Id
                        browseCityControlModel.CountyId = array[0];
                        //城市Id
                        browseCityControlModel.CityId = array[1];
                    }
                    else
                    {
                        //城市Id
                        browseCityControlModel.CityId = Utils.GetInt(browseCitys[i]);
                    }

                    browseCityControl.Add(browseCityControlModel);
                }
                model.BrowseCitys = browseCityControl;
            }
            //线路名称
            model.RouteName = Utils.GetFormValue(txt_LineName.UniqueID);

            //B2B显示控制
            model.B2B = (RouteB2BDisplay)Utils.GetInt(Utils.GetFormValue(dropB2B.UniqueID));
            //B2C显示控制
            model.B2C = (RouteB2CDisplay)Utils.GetInt(Utils.GetFormValue(dropB2C.UniqueID));
            //B2B排序值
            model.B2BOrder = Utils.GetInt(Utils.GetFormValue(txt_B2B.UniqueID));
            //B2C排序值
            model.B2COrder = Utils.GetInt(Utils.GetFormValue(txt_B2C.UniqueID));
            //推荐类型
            model.RecommendType = (RecommendType)Utils.GetInt(Utils.GetFormValue(ddl_Status.UniqueID));
            //出发交通
            model.StartTraffic = (TrafficType)Utils.GetInt(Utils.GetFormValue(ddl_DepartureTraffic.UniqueID));
            //返回交通
            model.EndTraffic = (TrafficType)Utils.GetInt(Utils.GetFormValue(ddl_ReturnTraffic.UniqueID));
            //出发城市
            string[] sCity = Utils.GetFormValue(hd_goCity.UniqueID).Split('|');
            model.StartCity = Utils.GetInt(sCity[0]);
            model.StartCityName = sCity[1];
            //返回城市
            string[] eCity = Utils.GetFormValue(hd_returnCity.UniqueID).Split('|');
            model.EndCity = Utils.GetInt(eCity[0]);
            model.EndCityName = eCity[1];
            //线路特色
            model.Characteristic = Utils.GetFormValue(txt_LineFeatures.UniqueID);
            //天数
            model.Day = Utils.GetInt(Utils.GetFormValue(txt_days.UniqueID));
            //夜数
            model.Late = Utils.GetInt(Utils.GetFormValue(txt_nights.UniqueID));
            //线路主题
            model.Themes = GetTheme();
            //报价包含
            string fITOrTeam = Utils.GetFormValue("hd_FITOrTeam").Trim();
            //发布人账号
            model.OperatorId = Utils.GetFormValue(hiddropPublisher.UniqueID);
            //实例化
            model.ServiceStandard = new MServiceStandard();
            if (fITOrTeam == "FIT")
            {
                model.FitQuotation = Utils.GetFormValue(txt_FIT.UniqueID);
            }
            else
            {
                //用车
                model.ServiceStandard.CarContent = Utils.GetFormValue("txt_car");
                //其他包含
                model.ServiceStandard.IncludeOtherContent = Utils.GetFormValue("txt_qt");
                //用餐
                model.ServiceStandard.DinnerContent = Utils.GetFormValue("txt_yc");
                //导游
                model.ServiceStandard.GuideContent = Utils.GetFormValue("txt_dy");
                //住宿
                model.ServiceStandard.ResideContent = Utils.GetFormValue("txt_zs");
                //景点
                model.ServiceStandard.SightContent = Utils.GetFormValue("txt_jd");
                //往返交通
                model.ServiceStandard.TrafficContent = Utils.GetFormValue("txt_fhjt");
            }
            //报价不包含
            model.ServiceStandard.NotContainService = Utils.GetFormValue(txt_PriceExcluding.UniqueID);
            //赠送项目
            model.ServiceStandard.GiftInfo = Utils.GetFormValue(txt_GiftItems.UniqueID);
            //儿童及其他安排
            model.ServiceStandard.ChildrenInfo = Utils.GetFormValue(txt_OtherArr.UniqueID);
            //购物安排
            model.ServiceStandard.ShoppingInfo = Utils.GetFormValue(txt_ShoppingArr.UniqueID);
            //自费项目
            model.ServiceStandard.ExpenseItem = Utils.GetFormValue(txt_ThisConsumption.UniqueID);
            //销售商须知（只有组团社用到）
            model.VendorsNotes = Utils.GetFormValue(txt_Notes.UniqueID);
            //备注
            model.ServiceStandard.Notes = Utils.GetFormValue(txt_Remarks.UniqueID);
            //销售区域
            model.Citys = GetSalesArea();
            //行程类型
            string travelType = Utils.GetFormValue("hd_TravelContent").Trim();
            #endregion
            if (!isAllUpdata)
            {
                #region 散拼计划修改
                #region 团队信息
                //公司编号
                model.Publishers = companyid;
                //团队人数
                model.TourNum = Utils.GetInt(Utils.GetFormValue(txt_orderPeopleNum.UniqueID));
                //报名截止时间
                model.RegistrationEndDate = Utils.GetDateTime(Utils.GetFormValue(txt_registrationEndDate.UniqueID));
                if (!model.IsLimit)
                {
                    //余位
                    model.MoreThan = Utils.GetInt(Utils.GetFormValue(txt_moreThan.UniqueID));
                }
                //留位
                model.SaveNum = Utils.GetInt(Utils.GetFormValue(txt_saveNum.UniqueID));
                //成人市场价
                model.RetailAdultPrice = Utils.GetDecimal(Utils.GetFormValue(txt_retailAdultPrice.UniqueID));
                //成人结算价
                model.SettlementAudltPrice = Utils.GetDecimal(Utils.GetFormValue(txt_settlementAudltPrice.UniqueID));
                //儿童市场价
                model.RetailChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_retailChildrenPrice.UniqueID));
                //儿童结算价
                model.SettlementChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_settlementChildrenPrice.UniqueID));
                //单房差
                model.MarketPrice = Utils.GetDecimal(Utils.GetFormValue(txt_marketPrice.UniqueID));
                //集合说明
                model.SetDec = Utils.GetFormValue(txt_setDec.UniqueID);
                //线路销售备注
                model.TourNotes = Utils.GetFormValue(txt_tourNotes.UniqueID);
                //领队全陪
                model.TeamLeaderDec = Utils.GetFormValue(txt_teamLeaderDec.UniqueID);
                //航班出发时间
                model.StartDate = Utils.GetFormValue(txt_startDate.UniqueID);
                //航班返回时间
                model.EndDate = Utils.GetFormValue(txt_endDate.UniqueID);
                //收客状态
                model.PowderTourStatus = (PowderTourStatus)Utils.GetInt(Utils.GetFormValue(ddl_powderTourStatus.UniqueID), 3);
                //国际线路成人定金
                model.AdultPrice = Utils.GetDecimal(Utils.GetFormValue(txt_AdultDeposit.UniqueID), -1);
                //国际线路儿童定金
                model.ChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_ChildrenDeposit.UniqueID), -1);
                #endregion
                if (travelType == "SAE")
                {
                    //行程简易版
                    model.StandardStroke = Utils.EditInputText(
                    Request.Form[txt_BriefnessTravel.UniqueID] != null
                    &&
                    Request.Form[txt_BriefnessTravel.UniqueID].ToString().Length > 0 ?
                    Request.Form[txt_BriefnessTravel.UniqueID] :
                    string.Empty);
                    isSave = bll.UpdatePowder(model);
                }
                else
                {
                    //行程内容标准
                    model.StandardPlans = GetTravel();
                    isSave = bll.UpdatePowder(model);
                }
                #endregion
            }
            else
            {
                #region 批量修改散拼计划
                if (travelType == "SAE")
                {
                    //行程简易版
                    model.StandardStroke = Utils.EditInputText(
                    Request.Form[txt_BriefnessTravel.UniqueID] != null
                    &&
                    Request.Form[txt_BriefnessTravel.UniqueID].ToString().Length > 0 ?
                    Request.Form[txt_BriefnessTravel.UniqueID] :
                    string.Empty);
                }
                else
                {
                    //行程内容标准
                    model.StandardPlans = GetTravel();
                }
                #endregion
                isSave = bll.UpdateStandardPlan(model, tourIds);
            }
            Response.Clear();
            Response.Write(isSave.ToString());
            Response.End();

        }
        /// <summary>
        /// 获取标准行程信息
        /// </summary>
        /// <returns></returns>
        private IList<MStandardPlan> GetTravel()
        {
            //路径
            string[] way = Utils.GetFormValues("txt_Way");
            //交通
            string[] traffic = Utils.GetFormValues("sel_Traffic");
            //住宿
            string[] stay = Utils.GetFormValues("txt_Stay");
            //就餐
            string[] dining = Utils.GetFormValues("hd_Dining");
            //行程内容
            string[] travel = Utils.GetFormValues("txt_Travel");
            int i = way.Length;
            IList<MStandardPlan> travelModel = new List<MStandardPlan>();
            while (i-- > 0)
            {
                MStandardPlan model = new MStandardPlan();
                //住宿
                model.House = stay[i];
                //第几天行程
                model.PlanDay = i;
                //途径
                model.PlanInterval = way[i];
                //交通
                model.Vehicle = (TrafficType)Utils.GetInt(traffic[i]);
                //早
                model.Early = dining[i].IndexOf('1') >= 0;
                //中
                model.Center = dining[i].IndexOf('2') >= 0;
                //晚
                model.Late = dining[i].IndexOf('3') >= 0;
                travelModel.Add(model);
            }
            return travelModel;
        }
        /// <summary>
        /// 获取销售区域
        /// </summary>
        /// <returns></returns>
        private IList<MCityControl> GetSalesArea()
        {
            //前台隐藏域
            string salesAreaS = Utils.GetFormValue(hd_SellCity.UniqueID);
            string[] subSalesArea = salesAreaS.Split('|');
            int i = subSalesArea.Length;
            IList<MCityControl> list = new List<MCityControl>();
            while (i-- > 0)
            {
                string[] mod = subSalesArea[i].Split(',');
                MCityControl model = new MCityControl();
                //城市id
                model.CityId = Utils.GetInt(mod[0]);
                //省份id
                model.ProvinceId = Utils.GetInt(mod[2]);
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 获取线路主题
        /// </summary>
        /// <returns></returns>
        private IList<MThemeControl> GetTheme()
        {
            IList<MThemeControl> list = new List<MThemeControl>();
            string[] themeArr = Utils.GetFormValue(hd_SubjectLine.UniqueID).Split('|');
            int i = themeArr.Length;
            while (i-- > 0)
            {
                MThemeControl model = new MThemeControl();
                model.ThemeId = themeArr[i].Split(',')[0];
                model.ThemeName = themeArr[i].Split(',')[1];
                list.Add(model);
            }
            return list;

        }

        /// <summary>
        /// 类型（推荐，特价）
        /// </summary>
        private void BindDropList()
        {
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
        }
    }
}

