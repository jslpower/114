using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.IBLL.NewTourStructure;
using System.Text;
using EyouSoft.Model.VisaStructure;
using EyouSoft.Model.TourStructure;

namespace UserBackCenter.RouteAgency.RouteManage
{
    /// <summary>
    /// 添加线路,修改,复制
    /// </summary>
    /// 创建人：柴逸宁  
    /// 2011-12-15
    public partial class AddTourism : BackPage
    {
        /// <summary>
        /// 旅游范围控制变量
        /// 国内长线 = 0,
        /// 国际线 = 1,
        /// 国内短线 = 2,
        /// 地接线路 = 3,
        /// </summary>
        protected int travelRangeType = 0;
        /// <summary>
        /// 专线类型
        /// </summary>
        protected int travelRangeId = 0;
        /// <summary>
        /// isTravel=是否有行程信息,isServiceStandard=是否标准报价信息
        /// </summary>
        protected bool isTravel = false, isServiceStandard = false;
        /// <summary>
        /// Key=页面唯一标识,routeId=线路Id
        /// </summary>
        protected string Key = string.Empty, routeId = string.Empty;
        /// <summary>
        /// 线路来源
        /// </summary>
        protected RouteSource routeSource;
        /// <summary>
        /// 保存类型add=添加,update=修改,copy=复制
        /// </summary>
        protected string SaveType = "add";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 权限判断
            if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.专线商添加)
            {
                if (!CheckGrant(TravelPermission.专线_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            else if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.地接社添加)
            {
                if (!CheckGrant(TravelPermission.地接_线路管理))
                {
                    Utils.ResponseNoPermit();
                    return;
                }
            }
            #endregion
            //线路来源
            routeSource = (RouteSource)Utils.GetInt(Utils.GetQueryStringValue("routeSource"), 1);
            //操作类型
            string Operating = Utils.GetQueryStringValue("Operating");
            /*
             * 存在操作,目前该页面只有保存操作
             * 保存操作包含修改和添加
             */
            if (Operating.Length > 0) Save(Operating);
            //生成页面唯一标识
            Key = "AddTourism" + Guid.NewGuid().ToString();
            if (!IsPostBack)
            {
                /*线路区域类型
                 * 国内长线 = 0,
                 * 国际线 = 1,
                 * 国内短线 = 2,
                 * 地接线路 = 3,
                 */
                travelRangeType = Utils.GetInt(Utils.GetQueryStringValue("travelRangeType"));
                //专线类型
                travelRangeId = Utils.GetInt(Utils.GetQueryStringValue("travelRangeId"));
                //初始化页面
                InitPage();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            #region 配置用户控件
            //配置上传用户控件
            files.IsGenerateThumbnail = true;
            //标准报价包含用户控件配置
            AddStandardRoute_ServiceStandard.ContainerID = Key;
            #endregion
            //线路区域类型名称
            lbl_travelRangeName.Text = EyouSoft.Common.Utils.GetQueryStringValue("travelRangeName");
            //绑定销售城市
            SalesArea();
            //绑定交通
            BindTraffic();
            //绑定推荐状态
            BindRecommendType(false);
            //绑定线路区域主题
            BindTheme();
            //线路Id
            routeId = Utils.GetQueryStringValue("RouteId");
            #region 修改-复制加载原有数据
            if (routeId.Length > 0)
            {
                #region 页面赋值
                //线路实体
                MRoute model = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(routeId);
                //线路来源
                routeSource = model.RouteSource;
                //专线类型
                travelRangeId = model.AreaId;
                if (model != null)
                {
                    //线路配图
                    if (model.RouteImg.Length > 0)
                    {
                        img_showImg.Src = EyouSoft.Common.Domain.FileSystem + model.RouteImg1;
                        img_showImg.Visible = true;
                        //a_showImg.HRef = EyouSoft.Common.Domain.FileSystem + model.RouteImg;
                        //a_showImg.Visible = true;
                    }
                    #region 线路类型
                    //线路类型
                    travelRangeType = (int)model.RouteType;
                    if (model.RouteType == AreaType.国际线)
                    {
                        #region 主要浏览国家
                        if (model.BrowseCountrys != null && model.BrowseCountrys.Count > 0)
                        {
                            //主要浏览国家个数
                            int i = model.BrowseCountrys.Count;
                            //变量
                            string str = string.Empty, qzstr = string.Empty;
                            //递减遍历主要浏览国家
                            while (i-- > 0)
                            {
                                //国家信息
                                str += model.BrowseCountrys[i].CountryId + ",";
                                //签证信息
                                if (model.BrowseCountrys[i].IsVisa)
                                {
                                    qzstr += model.BrowseCountrys[i].CountryId + ",";
                                }
                            }
                            //国家信息
                            hd_BrowseCountrys.Value = str.Substring(0, str.Length - 1);
                            //签证信息
                            if (qzstr.Length > 0)
                            {
                                hd_qz.Value = qzstr.Substring(0, qzstr.Length - 1);
                            }


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
                        #endregion
                    }
                    else
                    {
                        #region 主要浏览城市
                        if (model.BrowseCitys != null && model.BrowseCitys.Count > 0)
                        {
                            //主要浏览城市个数
                            int i = model.BrowseCitys.Count;
                            //变量
                            string str = string.Empty;
                            //递减遍历主要浏览城市
                            while (i-- > 0)
                            {
                                /*
                                 * 保存主要浏览城市信息
                                 * 存在县保存:县id|城市id,
                                 * 不存在县(城市)保存:城市id,
                                 * 最终用js拆分
                                 */
                                if (model.BrowseCitys[i].CountyId > 0)
                                {
                                    str += model.BrowseCitys[i].CountyId + "|" + model.BrowseCitys[i].CityId + ",";
                                }
                                else
                                {
                                    str += model.BrowseCitys[i].CityId + ",";
                                }
                            }
                            //主要浏览城市信息
                            hd_BrowseCitys.Value = str.Substring(0, str.Length - 1);

                        }
                        #endregion
                    }
                    #endregion
                    #region 普通数据
                    //专线名称
                    lbl_travelRangeName.Text = model.AreaName;
                    //线路名称
                    txt_LineName.Text = model.RouteName;
                    //如果是推荐状态为  推荐 重新绑定推荐状态
                    if (model.RecommendType == RecommendType.推荐)
                    {
                        BindRecommendType(true);
                    }
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
                    //最小成团人数
                    txt_MinNumberPeople.Text = model.GroupNum.ToString();
                    //参考价
                    txt_ReferencePrice.Text = Utils.FilterEndOfTheZeroDecimal(model.IndependentGroupPrice);
                    //住宿天数
                    txt_days.Text = model.Day.ToString();
                    //住宿夜数
                    txt_nights.Text = model.Late.ToString();
                    //提前几天报名
                    txt_EarlyDays.Text = model.AdvanceDayRegistration.ToString();
                    //散客报名无需成团，铁定发团 
                    chk_FITRegistration.Checked = model.IsCertain;
                    //简易版行程
                    txt_AddTourismBriefnessTravel.Value = model.FastPlan;
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
                    //销售须知
                    txt_Notes.Value = model.VendorsNotes;
                    //备注
                    txt_Remarks.Value = model.ServiceStandard.Notes;
                    //主题
                    hd_SubjectLine.Value = SetTheme(model.Themes);
                    //销售区域
                    hd_SellCity.Value = SetSalesArea(model.Citys);
                    #endregion
                    #region 行程
                    //行程
                    if (model.ReleaseType == ReleaseType.Quick)
                    {
                        txt_AddTourismBriefnessTravel.Value = model.FastPlan;
                    }
                    else
                    {
                        if (model.StandardPlans != null && model.StandardPlans.Count > 0)
                        {
                            isTravel = true;
                            rpt_standardPlans.DataSource = model.StandardPlans.OrderBy(r => r.PlanDay).ToList();
                            rpt_standardPlans.DataBind();
                        }
                    }
                    #endregion
                    #region 报价
                    txt_FIT.Value = model.FitQuotation;
                    AddStandardRoute_ServiceStandard.RouteServiceStandardInfo = model.ServiceStandard;
                    //判断是否为团队
                    isServiceStandard = model.ServiceStandard.CarContent.Length > 0 ||//用车
                        model.ServiceStandard.DinnerContent.Length > 0 ||//用餐
                        model.ServiceStandard.ResideContent.Length > 0 ||//住宿
                        model.ServiceStandard.SightContent.Length > 0 ||//景点
                        model.ServiceStandard.TrafficContent.Length > 0 ||//往返交通
                        model.ServiceStandard.GuideContent.Length > 0 ||//导游
                        model.ServiceStandard.IncludeOtherContent.Length > 0;//其他包含
                    #endregion
                }
                #endregion
                //保存类型
                SaveType = Utils.GetQueryStringValue("type");
                if (SaveType == "copy")
                {
                    //复制
                    //清空线路ID
                    routeId = string.Empty;
                    //清空线路名称
                    txt_LineName.Text = string.Empty;
                }
            }
            #endregion
            /*
             * 绑定出发返回城市
             * 该方法需要根据线路区域类型显示不同的返回城市
             * 为兼容添加与修改
             * 该方法只能写在线路数据加载之后
             */
            BindCity();
            /*
             * 绑定主要浏览国家或城市
             * 该方法需要根据线路区域类型显示不同的返回城市
             * 为兼容添加与修改
             * 该方法只能写在线路数据加载之后
             * 此方法与修改-复制原有数据中的代码无冲突
             */
            BindCountrysOrCity();
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save(string Operating)
        {
            /*
             * 初始化保存Model
             * 当存在线路Id获取原线路实体
             * 否则实例化一个新的线路实体
             */
            MRoute model = Utils.GetQueryStringValue("routeId").Length > 0 ?
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(Utils.GetQueryStringValue("routeId"))
                :
                new MRoute();
            //确保原线路实体不为空
            if (model != null)
            {
                #region 实体赋值
                //写死
                model.B2BOrder = 50;
                model.B2COrder = 50;
                //线路类型
                model.RouteType = (AreaType)Utils.GetInt(Utils.GetQueryStringValue("travelRangeType"));
                #region 主要浏览
                if (model.RouteType == AreaType.国际线)
                {
                    #region 主要浏览国家
                    //主要浏览国家信息
                    string[] browseCountryControl = Utils.GetFormValue(hd_BrowseCountrys.UniqueID).Split(',');
                    if (browseCountryControl.Length > 0)
                    {
                        //主要浏览国家
                        model.BrowseCountrys = new List<MBrowseCountryControl>();
                        //主要浏览国家个数
                        int i = browseCountryControl.Length;
                        //主要浏览国家签证信息
                        string qzArr = Utils.GetFormValue(hd_qz.UniqueID);
                        //递减遍历
                        while (i-- > 0)
                        {
                            //国家实体
                            MBrowseCountryControl browseCityControlModel = new MBrowseCountryControl();
                            //国家id
                            browseCityControlModel.CountryId = Utils.GetInt(browseCountryControl[i]);

                            //免签没有选择，才给其他赋值,否则直接不赋值
                            if (!chk_null.Checked)
                            {
                                //判断是否签证
                                if (qzArr.IndexOf(browseCountryControl[i] + ",") >= 0)
                                {
                                    browseCityControlModel.IsVisa = true;
                                }
                            }
                            
                            model.BrowseCountrys.Add(browseCityControlModel);
                        }
                    }
                    else
                    {
                        model.BrowseCountrys = null;
                    }
                    //是否免签
                    if (chk_null.Checked)
                    {
                        model.IsNotVisa = true;
                    }
                    else
                    {
                        model.IsNotVisa = false;
                    }
                    //国际线路成人定金
                    model.AdultPrice = Utils.GetDecimal(Utils.GetFormValue(txt_AdultDeposit.UniqueID), -1);
                    //国际线路儿童定金
                    model.ChildrenPrice = Utils.GetDecimal(Utils.GetFormValue(txt_ChildrenDeposit.UniqueID), -1);
                    #endregion
                }
                else
                {
                    #region 主要浏览城市
                    string[] browseCitys = Utils.GetFormValue(hd_BrowseCitys.UniqueID).Split(',');
                    int i = browseCitys.Length;
                    if (i > 0)
                    {
                        //主要浏览城市
                        IList<MBrowseCityControl> browseCityControl = new List<MBrowseCityControl>();


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
                    else
                    {
                        model.BrowseCitys = null;
                    }
                    #endregion
                }
                #endregion
                //公司编号
                model.Publishers = SiteUserInfo.CompanyID;
                //发布人ID
                model.OperatorId = SiteUserInfo.ID;
                //发布人名称
                model.OperatorName = SiteUserInfo.CompanyName;
                //专线类型
                model.AreaId = Utils.GetInt(Utils.GetQueryStringValue("travelRangeId"));
                //线路名称
                model.RouteName = Utils.GetFormValue(txt_LineName.UniqueID);
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
                //最小成团人数
                model.GroupNum = Utils.GetInt(Utils.GetFormValue(txt_MinNumberPeople.UniqueID));
                //团队参考价格
                model.IndependentGroupPrice = Utils.GetDecimal(Utils.GetFormValue(txt_ReferencePrice.UniqueID));
                //天数
                model.Day = Utils.GetInt(Utils.GetFormValue(txt_days.UniqueID));
                //夜数
                model.Late = Utils.GetInt(Utils.GetFormValue(txt_nights.UniqueID));
                //线路主题
                model.Themes = GetTheme();
                //提前几天报名
                model.AdvanceDayRegistration = Utils.GetInt(Utils.GetFormValue(txt_EarlyDays.UniqueID));
                //散客报名无需成团，铁定发团 
                model.IsCertain = Utils.GetFormValue(chk_FITRegistration.UniqueID) == "true";
                //报价包含
                string fITOrTeam = Utils.GetFormValue("hd_FITOrTeam").Trim();
                //线路配图
                string[] imgs = Utils.GetFormValue(this.files.UniqueID + "$hidFileName").Split('|');
                if (imgs != null && imgs.Count() > 1)
                {
                    model.RouteImg = imgs[0];
                    model.RouteImg1 = imgs[1];
                    model.RouteImg2 = imgs[1];
                }
                //model.RouteImg = Utils.GetFormValue(this.files.UniqueID + "$hidFileName").Length > 0 ? Utils.GetFormValue(this.files.UniqueID + "$hidFileName") : model.RouteImg == null ? string.Empty : model.RouteImg;
                //实例化
                model.ServiceStandard = new MServiceStandard();
                #region 行程
                if (fITOrTeam == "FIT")
                {
                    string strTmp = Utils.GetFormValue(txt_FIT.UniqueID);
                    model.FitQuotation = string.IsNullOrEmpty(strTmp)
                                             ? string.Empty
                                             : strTmp;
                    /*清空标准版原有信息*/
                    //用车
                    model.ServiceStandard.CarContent = string.Empty;
                    //其他包含
                    model.ServiceStandard.IncludeOtherContent = string.Empty;
                    //用餐
                    model.ServiceStandard.DinnerContent = string.Empty;
                    //导游
                    model.ServiceStandard.GuideContent = string.Empty;
                    //住宿
                    model.ServiceStandard.ResideContent = string.Empty;
                    //景点
                    model.ServiceStandard.SightContent = string.Empty;
                    //往返交通
                    model.ServiceStandard.TrafficContent = string.Empty;
                    /***********************************************************/
                }
                else
                {
                    /*清空简易版原有信息*/
                    model.FitQuotation = string.Empty;
                    /************************************************************/
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
                #endregion
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
                if (routeSource == RouteSource.专线商添加 || model.RouteSource == RouteSource.专线商添加)
                {
                    model.Citys = GetSalesArea();
                }
                #endregion
                IRoute bll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
                //行程类型
                string travelType = Utils.GetFormValue("hd_TravelContent").Trim();
                /*
                 * 操作结果变量
                 * >0=保存成功的线路Id，空字符串=保存失败，-2线路名已存在
                 */
                string isSave = "-1";
                //添加
                if (Operating == "AddSave")
                {
                    #region 添加
                    if (travelType == "SAE")
                    {
                        #region 行程简易版
                        model.FastPlan = Utils.EditInputText(Request.Form[txt_AddTourismBriefnessTravel.UniqueID] ?? string.Empty);
                        if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.专线商添加)
                        {
                            //专线添加
                            isSave = (!bll.IsExits(SiteUserInfo.CompanyID, model.RouteName)) ? bll.AddQuickRoute(model) : "-2";
                        }
                        else
                        {
                            //地接添加
                            isSave = (!bll.IsExits(SiteUserInfo.CompanyID, model.RouteName)) ? bll.AddGroundQuickRoute(model) : "-2";
                        }
                        #endregion

                    }
                    else
                    {
                        #region 行程内容标准
                        model.StandardPlans = GetTravel();
                        if ((RouteSource)Utils.GetInt(Utils.GetQueryStringValue("RouteSource"), 1) == RouteSource.专线商添加)
                        {
                            //专线添加
                            isSave = (!bll.IsExits(SiteUserInfo.CompanyID, model.RouteName)) ? bll.AddStandardRoute(model) : "-2";
                        }
                        else
                        {
                            //地接添加
                            isSave = (!bll.IsExits(SiteUserInfo.CompanyID, model.RouteName)) ? bll.AddGroundStandardRoute(model) : "-2";
                        }
                        #endregion
                    }
                    #endregion

                }
                else
                {
                    #region 修改
                    if (travelType == "SAE")
                    {
                        #region 行程简易版
                        model.ReleaseType = ReleaseType.Quick;
                        //清除原先标准版数据
                        model.StandardPlans = null;
                        model.FastPlan = Utils.EditInputText(
                            Request.Form[txt_AddTourismBriefnessTravel.UniqueID] != null
                            &&
                            Request.Form[txt_AddTourismBriefnessTravel.UniqueID].ToString().Length > 0 ?
                            Request.Form[txt_AddTourismBriefnessTravel.UniqueID] :
                            string.Empty);
                        #endregion
                    }
                    else
                    {
                        #region 行程标准版
                        model.ReleaseType = ReleaseType.Standard;
                        //清除原先简易版数据
                        model.FastPlan = string.Empty;
                        model.StandardPlans = GetTravel();
                        #endregion
                    }
                    #endregion
                    isSave = bll.UpdateRoute(model) ? model.RouteId : "-2";
                }
                Response.Clear();
                Response.Write(isSave.ToString());
                Response.End();
            }
            Response.Clear();
            Response.Write("");
            Response.End();

        }
        /// <summary>
        /// 获取标准行程信息
        /// </summary>
        /// <returns>标准行程Ilist</returns>
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
                model.PlanDay = i + 1;
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
                //行程内容
                model.PlanContent = travel[i];
                travelModel.Add(model);
            }
            return travelModel;
        }
        /// <summary>
        /// 获取销售区域
        /// </summary>
        /// <returns>销售城市Ilist</returns>
        private IList<MCityControl> GetSalesArea()
        {
            //前台隐藏域
            string[] subSalesArea = Utils.GetFormValue(hd_SellCity.UniqueID).Split('|');
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
        /// 设置以选择的主题
        /// </summary>
        /// <param name="list">主题列表</param>
        /// <returns>主题Ilist</returns>
        private string SetTheme(IList<MThemeControl> list)
        {
            if (list != null && list.Count > 0)
            {
                string retStr = string.Empty;
                foreach (var item in list)
                {
                    retStr += item.ThemeId + "," + item.ThemeName + "|";
                }
                return retStr.Substring(0, retStr.Length - 1);
            }
            return string.Empty;
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
        /// 设置销售区域
        /// </summary>
        /// <returns></returns>
        private string SetSalesArea(IList<MCityControl> list)
        {
            if (list != null && list.Count > 0)
            {
                string str = string.Empty;
                foreach (var item in list)
                {
                    str += item.CityId + "," + item.CityName + "," + item.ProvinceId + "," + item.ProvinceName + "|";
                }
                return str.Substring(0, str.Length - 1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 绑定销售区域
        /// </summary>
        private void SalesArea()
        {
            if (SiteUserInfo != null)
            {
                EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
                IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanySaleCity(SiteUserInfo.CompanyID);
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
            if (travelRangeType == (int)AreaType.地接线路 || travelRangeType == (int)AreaType.国内短线)
            {
                //周边
                ddl_DepartureTraffic.SelectedIndex = 1;
                ddl_ReturnTraffic.SelectedIndex = 1;
            }
            else
            {
                //国际国内
                ddl_DepartureTraffic.SelectedIndex = 0;
                ddl_ReturnTraffic.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定推荐状态
        /// </summary>
        /// <param name="isdeltj">是否需要推荐</param>
        /// 条件:
        /// 1、当添加线路时推荐状态不能有"推荐"这个选项
        /// 2、当修改线路时该线路原数据中推荐状态为"推荐",则下拉框中存在"推荐",否则就不能有"推荐"
        private void BindRecommendType(bool isdeltj)
        {
            //清空下拉框
            ddl_Status.Items.Clear();
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(RecommendType));
            //用户后台不能添加修改推荐类型为推荐的线路
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
        /// 绑定出发返回城市
        /// </summary>
        /// 条件:
        /// 1、国际线=广州,上海,成都,杭州+常用出港城市列表
        /// 2、国内线=常用出港城市列表
        /// 3、周边线=常用出港城市列表+公司所在城市
        private void BindCity()
        {
            IList<CityBase> list = new List<CityBase>();
            switch (travelRangeType)
            {
                case (int)AreaType.国际线:
                    list = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanyPortCity(SiteUserInfo.CompanyID)
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
                    //list.Distinct();
                    break;
                case (int)AreaType.国内长线:
                    list = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance().GetCompanyPortCity(SiteUserInfo.CompanyID);
                    break;
                default:
                    //周边线
                    SysCity model = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(SiteUserInfo.CityId);
                    if (model != null)
                    {
                        list.Add((CityBase)model);
                    }
                    break;

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
        /// 绑定主要浏览国家或城市
        /// </summary>
        private void BindCountrysOrCity()
        {
            //根据线路区域id获取线路区域实体
            SysArea AreaModel = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(travelRangeId);
            if (AreaModel != null)
            {
                //国际线,绑定主要浏览国家
                if (travelRangeType == 1)
                {
                    //主要浏览国家
                    if (AreaModel.VisitCity != null && AreaModel.VisitCity.Count > 0)
                    {

                        //根据主要浏览国家ID获取国家集合
                        IList<MSysCountry> lsCountry = EyouSoft.BLL.SystemStructure.BSysCountry.CreateInstance().GetCountryList(
                            AreaModel.VisitCity.Select(ls => ls.CountryId).ToArray()
                            );
                        if (lsCountry != null && lsCountry.Count > 0)
                        {
                            rpt_VisitingNational.DataSource = lsCountry;
                            rpt_VisitingNational.DataBind();
                        }
                    }
                }
                else
                {
                    //主要浏览城市
                    if (AreaModel.VisitCity != null && AreaModel.VisitCity.Count > 0)
                    {
                        //城市信息
                        IList<SysCity> lsCity = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityList(
                            AreaModel.VisitCity.Where(ls => ls.CountyId == 0 && ls.CityId != 0).Select(ls => ls.CityId).ToArray()
                            );
                        //县的信息
                        IList<SysDistrictCounty> lsDistrictCounty = EyouSoft.BLL.SystemStructure.SysDistrictCounty.CreateInstance().GetDistrictCountyList(
                              AreaModel.VisitCity.Select(ls => ls.CountyId).ToArray()
                              );
                        //分别绑定
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

                }
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
    }
}
