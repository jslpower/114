using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using System.Text;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.CompanyStructure;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    ///  团队详细打印页面
    ///  创建时间： 2011-12-22  方琪
    /// </summary>
    public partial class TeamRouteDetails : EyouSoft.Common.Control.BasePage
    {
        #region 页面变量
        /// <summary>
        /// 是否是专线
        /// </summary>
        protected bool IsRouteAgency = false;
        /// <summary>
        /// 是否是组团
        /// </summary>
        protected bool IsTourAgency = false;
        /// <summary>
        /// 是否显示预订和待订
        /// </summary>
        protected bool IsShow = true;
        /// <summary>
        /// 公司Mq
        /// </summary>
        protected string Mq = string.Empty;
        /// <summary>
        /// 团队编号
        /// </summary>
        protected string TeamId = Utils.GetQueryStringValue("TeamId");
        /// <summary>
        /// 网店链接
        /// </summary>
        protected string ShopURL = string.Empty;
        /// <summary>
        /// 是否是标准日程
        /// </summary>
        protected bool isStandard = false;
        /// <summary>
        /// 是否为国际线路
        /// </summary>
        protected bool isInternational = false;
        #endregion

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ShopURL = Utils.GetShopUrl(this.SiteUserInfo.CompanyID);
            this.Page.Title = "团队线路详细";
            InitPage();
        }
        #endregion

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>        
        protected void InitPage()
        {
            #region 判断公司身份
            CompanyType[] companyType = this.SiteUserInfo.CompanyRole.RoleItems;
            foreach (var item in companyType)
            {
                switch (item)
                {
                    case CompanyType.专线:
                        IsRouteAgency = true;
                        break;
                    case CompanyType.地接:
                        break;
                    case CompanyType.组团:
                        IsTourAgency = true;
                        break;
                    default:
                        break;
                }
            }
            #endregion

            //根据团号获取团队实体信息
            EyouSoft.Model.NewTourStructure.MPowderList PowderModel =
            EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(TeamId);
            if (PowderModel != null)
            {
                if (PowderModel.RouteType == AreaType.国际线)
                {
                    isInternational = true;
                }

                #region 判断散拼团队状态以及过期时间
                if (PowderModel.LeaveDate.Subtract(DateTime.Now.Date).Days <= 0 || PowderModel.PowderTourStatus == PowderTourStatus.客满 || PowderModel.PowderTourStatus == PowderTourStatus.停收)
                {
                    IsShow = false;
                }
                #endregion

                #region 普通信息
                //团号
                this.TourNo.Text = PowderModel.TourNo;
                //团队状态
                this.Statue.Text = PowderModel.PowderTourStatus.ToString();
                //出发时间
                this.BeginDate.Text = PowderModel.LeaveDate.ToString("yyyy-MM-dd");
                //报名截止时间
                this.EndDate.Text = PowderModel.RegistrationEndDate.ToString("yyyy-MM-dd");
                //成人市场价
                this.AdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(PowderModel.RetailAdultPrice);
                //儿童市场价
                this.ChildPrice.Text = Utils.FilterEndOfTheZeroDecimal(PowderModel.RetailChildrenPrice);
                //成人结算价
                this.AdultSettlementPrice.Text = Utils.FilterEndOfTheZeroDecimal(PowderModel.SettlementAudltPrice);
                //儿童结算价
                this.ChildSettlementPrice.Text = Utils.FilterEndOfTheZeroDecimal(PowderModel.SettlementChildrenPrice);
                //单房差
                this.danfangcha.Text = Utils.FilterEndOfTheZeroDecimal(PowderModel.MarketPrice);
                //团队
                this.Team.Text = "计划收客人数 " + PowderModel.TourNum.ToString() +
                                 "余位 " + PowderModel.MoreThan.ToString() +
                                 "留位 " + PowderModel.SaveNum.ToString();
                //出发班次时间
                this.GoTime.Text = PowderModel.StartDate;
                //反悔航班时间
                this.BackTime.Text = PowderModel.EndDate;
                //集合说明
                this.CollectionDescription.Text = PowderModel.SetDec;
                //领队全陪说明
                this.DescriptionLeader.Text = PowderModel.TeamLeaderDec;
                //销售商须知
                this.SellerNotice.Text = PowderModel.VendorsNotes;
                //团队备注
                this.TeamRemark.Text = PowderModel.TourNotes;
                //线路名称
                this.RouteName.Text = PowderModel.RouteName;
                //公司名称
                this.CompanyName.Text = PowderModel.PublishersName;
                Mq = Utils.GetMQ(SiteUserInfo.ContactInfo.MQ);
                //专线名称
                this.RouteType.Text = PowderModel.AreaName;
                //出发交通和城市
                this.StartTraffic.Text = PowderModel.StartTraffic + " " + PowderModel.StartCityName;
                //返回交通和城市
                this.EndTraffic.Text = PowderModel.EndTraffic + " " + PowderModel.EndCityName;
                //主题
                this.RouteTheme.Text = GetThemeList(PowderModel.Themes);
                //天数
                this.Day.Text = PowderModel.Day.ToString();
                //晚
                this.Night.Text = PowderModel.Late.ToString();
                //主要游览地区
                this.MainTourArea.Text = GetMainTourArea(PowderModel.RouteType, PowderModel.BrowseCitys, PowderModel.BrowseCountrys);
                //签证地区
                this.VisaArea.Text = GetVisaArea(PowderModel.BrowseCountrys);
                //成人定金
                this.Adult.Text = PowderModel.AdultPrice == 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(PowderModel.AdultPrice);
                //儿童定金
                this.Child.Text = PowderModel.ChildrenPrice == 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(PowderModel.ChildrenPrice);
                //线路特色
                this.RouteFeatures.Text = Utils.TextToHtml(PowderModel.Characteristic);
                #endregion

                #region 团队参考价格&最小成团人数
                if (PowderModel.RouteId != "" && PowderModel.RouteId != null)
                {
                    MRoute routeModel = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(PowderModel.RouteId);
                    //最小成团人数
                    this.Min.Text = routeModel.GroupNum.ToString();
                    //团队参考价格
                    if (routeModel.IndependentGroupPrice == 0)
                    {
                        this.TeamPrice.Text = "一团一议";
                    }
                    else
                    {
                        this.TeamPrice.Text = Utils.FilterEndOfTheZeroDecimal(routeModel.IndependentGroupPrice);
                    }
                }
                #endregion

                #region 日程信息
                if (PowderModel.StandardPlans != null && PowderModel.StandardPlans.Count > 0)
                {
                    isStandard = true;
                    //标准日程
                    BindStandardPlan(PowderModel.StandardPlans);

                }
                else
                {
                    //简易行程
                    this.FastStandard.Text = PowderModel.StandardStroke;
                }
                #endregion

                #region 服务信息
                if (PowderModel.FitQuotation != "" && PowderModel.FitQuotation != null)
                {
                    this.Containers.Text = PowderModel.FitQuotation;
                }
                if (PowderModel.ServiceStandard != null)
                {
                    if (PowderModel.FitQuotation == "" || PowderModel.FitQuotation == null)
                    {
                        //报价包含
                        this.Containers.Text = Utils.TextToHtml(GetContainers(PowderModel.ServiceStandard));
                    }
                    //不含
                    this.NoContainers.Text = Utils.TextToHtml(PowderModel.ServiceStandard.NotContainService);
                    //儿童
                    this.Children.Text = Utils.TextToHtml(PowderModel.ServiceStandard.ChildrenInfo);
                    //赠送
                    this.Gift.Text = Utils.TextToHtml(PowderModel.ServiceStandard.GiftInfo);
                    //购物
                    this.Shopping.Text = Utils.TextToHtml(PowderModel.ServiceStandard.ShoppingInfo);
                    //自费
                    this.OwnExpense.Text = Utils.TextToHtml(PowderModel.ServiceStandard.ExpenseItem);
                    //备注
                    this.Remark.Text = Utils.TextToHtml(PowderModel.ServiceStandard.Notes);
                }
                #endregion
            }
        }
        #endregion

        #region 获取大交通
        /// <summary>
        /// 获取大交通
        /// </summary>
        /// <param name="StartCityName">出发城市</param>
        /// <param name="trafficType">交通</param>
        /// <returns></returns>
        protected string GetBigTraffic(string StartCityName, TrafficType trafficType, string EndCityName, TrafficType endTraffic)
        {
            string traffic = string.Empty;
            traffic = StartCityName + trafficType.ToString() + "出发 " + endTraffic.ToString() + EndCityName + " 回";
            return traffic;
        }
        #endregion

        #region 绑定日程
        /// <summary>
        /// 绑定日程
        /// </summary>
        /// <param name="StandardPlan"></param>
        protected void BindStandardPlan(IList<MStandardPlan> StandardPlan)
        {
            this.richeng.DataSource = StandardPlan.OrderBy(p => p.PlanDay);
            this.richeng.DataBind();
        }
        #endregion

        #region 获取报价包含内容
        /// <summary>
        /// 获取报价包含内容
        /// </summary>
        /// <param name="ServiceStandard">服务项目</param>
        /// <returns></returns>
        protected string GetContainers(MServiceStandard ServiceStandard)
        {
            StringBuilder sb = new StringBuilder();
            if (ServiceStandard != null)
            {
                sb.AppendFormat("1. 交通：{0} <br/>", ServiceStandard.TrafficContent);
                sb.AppendFormat("2. 住宿：{0} <br/>", ServiceStandard.ResideContent);
                sb.AppendFormat("3. 餐饮：{0} <br/>", ServiceStandard.DinnerContent);
                sb.AppendFormat("4. 景点：{0} <br/>", ServiceStandard.SightContent);
                sb.AppendFormat("5. 导服：{0} <br/>", ServiceStandard.GuideContent);
                sb.AppendFormat("6. 用车：{0} <br/>", ServiceStandard.CarContent);
                sb.AppendFormat("7. 其他包含：{0} <br/>", ServiceStandard.IncludeOtherContent);
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }

        }
        #endregion

        #region 获取浏览地区
        /// <summary>
        /// 获取浏览地区
        /// </summary>
        /// <param name="areaType">线路类型</param>
        /// <param name="BrowseCity">浏览城市</param>
        /// <param name="BrowseCountry">浏览国家</param>
        /// <returns></returns>
        protected string GetMainTourArea(AreaType areaType, IList<MBrowseCityControl> BrowseCity, IList<MBrowseCountryControl> BrowseCountry)
        {
            string sList = string.Empty;
            if (areaType == AreaType.国际线)
            {
                if (BrowseCountry != null && BrowseCountry.Count > 0)
                {
                    foreach (var item in BrowseCountry)
                    {
                        sList += item.CountryName + "、";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (BrowseCity != null && BrowseCity.Count > 0)
                {
                    foreach (var item in BrowseCity)
                    {
                        if (item.CountyId == 0)
                        {
                            sList += item.CityName + "、";
                        }
                        else
                        {
                            sList += item.CityName+item.CountyName + "、";
                        }
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            return sList.Substring(0, sList.Length - 1);
        }
        #endregion

        #region 获取主题
        /// <summary>
        /// 获取主题
        /// </summary>
        /// <param name="ThemeList"></param>
        /// <returns></returns>
        protected string GetThemeList(IList<MThemeControl> ThemeList)
        {
            string themeList = string.Empty;
            if (ThemeList != null && ThemeList.Count > 0)
            {
                foreach (var item in ThemeList)
                {
                    themeList += item.ThemeName.ToString() + "、";
                }

                return themeList.Substring(0, themeList.Length - 1);
            }
            else
            {
                return themeList;
            }

        }
        #endregion

        #region 获取签证地区
        /// <summary>
        /// 获取签证地区
        /// </summary>
        /// <param name="BrowseCountry"></param>
        /// <returns></returns>
        protected string GetVisaArea(IList<MBrowseCountryControl> BrowseCountry)
        {
            string sList = string.Empty;
            if (BrowseCountry != null && BrowseCountry.Count > 0)
            {
                foreach (var item in BrowseCountry)
                {
                    sList += item.CountryName + "签证、";
                }
                return sList.Substring(0, sList.Length - 1);
            }
            else
            {
                return sList;
            }
        }
        #endregion

    }
}
