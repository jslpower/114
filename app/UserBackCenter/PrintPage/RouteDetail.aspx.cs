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
using System.Text.RegularExpressions;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 线路详细页面打印
    /// 创建时间：2011-12-22 方琪
    /// </summary>
    public partial class RouteDetail : EyouSoft.Common.Control.BasePage
    {
        #region 页面变量
        /// <summary>
        /// 联系人Mq
        /// </summary>
        protected string Mq = string.Empty;
        /// <summary>
        /// 页码
        /// </summary>
        protected int intPageIndex = Utils.GetInt(Utils.GetQueryStringValue("Page"), 1);
        /// <summary>
        /// 页大小
        /// </summary>
        protected int intPageSize = 5;
        /// <summary>
        /// 总页数
        /// </summary>
        protected int intRecordCount = 0;
        /// <summary>
        /// 线路编号
        /// </summary>
        protected string RouteId = Utils.GetQueryStringValue("RouteId");
        /// <summary>
        /// 网店链接
        /// </summary>
        protected string ShopURL = string.Empty;
        /// <summary>
        /// 是否是标准日程
        /// </summary>
        protected bool isStandard = false;
        /// <summary>
        /// 是否是国际线
        /// </summary>
        protected bool isInternational = false;
        /// <summary>
        /// 是否显示团队信息列表
        /// </summary>
        protected bool isShow = false;
        /// <summary>
        /// 是否是专线
        /// </summary>
        protected bool IsRouteAgency = false;
        /// <summary>
        /// 是否是组团
        /// </summary>
        protected bool IsTourAgency = false;
        #endregion

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //是否登录
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLoginOpenTopPage("/Default.aspx");
                return;
            }

            this.Page.Title = "线路详细";
            //加载页面
            InitPage();
            //绑定团队列表
            BindTeamList();
        }
        #endregion

        #region 加载页面信息
        /// <summary>
        /// 加载页面信息
        /// </summary>
        protected void InitPage()
        {
            #region 判断公司身份类型
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

            //根据线路编号获取相应的实体
            var IBll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            EyouSoft.Model.NewTourStructure.MRoutePrint RoutePrintModel = IBll.GetPrintModel(RouteId);
            //更新阅览记录
            IBll.UpdateClick(RouteId);
            if (RoutePrintModel != null)
            {
                if (RoutePrintModel.RouteType == AreaType.国际线)
                {
                    isInternational = true;
                }
                #region 普通信息
                //根据公司编号获取网店链接
                if (RoutePrintModel.RouteSource == RouteSource.专线商添加)
                    ShopURL = Utils.GetShopUrl(RoutePrintModel.CompanyId);

                //线路名称
                this.RouteName.Text = RoutePrintModel.RouteName;
                //公司名称
                this.CompanyName.Text = RoutePrintModel.CompanyName;
                Mq = Utils.GetMQ(RoutePrintModel.CompanyContactMq);
                //专线类型
                this.RouteType.Text = RoutePrintModel.AreaName;
                //出发交通和城市
                this.StartTraffic.Text = RoutePrintModel.StartTraffic + " " + RoutePrintModel.StartCityName;
                //返回交通和城市
                this.EndTraffic.Text = RoutePrintModel.EndTraffic + " " + RoutePrintModel.EndCityName;
                //主题
                this.RouteTheme.Text = RoutePrintModel.ThemeName;
                //天数
                this.Day.Text = RoutePrintModel.Day.ToString();
                //晚
                this.Night.Text = RoutePrintModel.Late.ToString();
                //最小成团人数
                this.Min.Text = RoutePrintModel.GroupNum.ToString();
                //主要游览地区
                this.MainTourArea.Text = RoutePrintModel.BrowseCity;
                //签证地区
                this.VisaArea.Text = RoutePrintModel.VisaCity;
                var strAdultPrice = new StringBuilder();
                if (RoutePrintModel.AdultPrice <= 0)
                {
                    if (RoutePrintModel.AdultPrice == -1)
                        strAdultPrice.Append("成人 <span class=\"ff0000\">电询</span> &nbsp;");
                    if (RoutePrintModel.AdultPrice == 0)
                        strAdultPrice.Append("成人 <span class=\"ff0000\">无需定金</span> &nbsp;");
                }
                else
                {
                    strAdultPrice.AppendFormat("成人 <span class=\"ff0000\">{0}</span> 元&nbsp;",
                                               Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.AdultPrice));
                }
                if (RoutePrintModel.ChildrenPrice <= 0)
                {
                    if (RoutePrintModel.AdultPrice == -1)
                        strAdultPrice.Append("儿童 <span class=\"ff0000\">电询</span> &nbsp;");
                    if (RoutePrintModel.AdultPrice == 0)
                        strAdultPrice.Append("儿童 <span class=\"ff0000\">无需定金</span> &nbsp;");
                }
                else
                {
                    strAdultPrice.AppendFormat(" 儿童 <span class=\"ff0000\">{0}</span> 元&nbsp;&nbsp;&nbsp;",
                                               Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.ChildrenPrice));
                }
                ltrPrice.Text = strAdultPrice.ToString();
                //线路特色
                this.RouteFeatures.Text = Utils.TextToHtml(RoutePrintModel.Characteristic);
                #endregion

                #region 团队参考价格
                if (RoutePrintModel.ReferencePrice == 0)
                {
                    this.TeamPrice.Text = "一团一议";
                }
                else
                {
                    this.TeamPrice.Text = Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.ReferencePrice);
                }
                #endregion

                #region 绑定日程
                if (RoutePrintModel.StandardPlan != null && RoutePrintModel.StandardPlan.Count > 0)
                {
                    isStandard = true;
                    BindStandardPlan(RoutePrintModel.StandardPlan);
                }
                else
                {
                    this.QuickStandard.Text = RoutePrintModel.FastPlan;
                }
                #endregion

                #region 服务信息
                if (RoutePrintModel.FitQuotation != null && RoutePrintModel.FitQuotation != "")
                {
                    this.Containers.Text = Utils.TextToHtml(RoutePrintModel.FitQuotation);
                }
                if (RoutePrintModel.ServiceStandard != null)
                {
                    //报价包含
                    if (RoutePrintModel.FitQuotation == null || RoutePrintModel.FitQuotation == "")
                    {
                        this.Containers.Text = Utils.TextToHtml(GetContainers(RoutePrintModel.ServiceStandard));
                    }
                    //不含
                    this.NoContainers.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.NotContainService);
                    //儿童
                    this.Children.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.ChildrenInfo);
                    //赠送
                    this.Gift.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.GiftInfo);
                    //购物
                    this.Shopping.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.ShoppingInfo);
                    //自费
                    this.OwnExpense.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.ExpenseItem);
                    //备注
                    this.Remark.Text = Utils.TextToHtml(RoutePrintModel.ServiceStandard.Notes);
                }
                #endregion
            }
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

        #region 绑定团队列表
        /// <summary>
        /// 绑定团队列表
        /// </summary>
        protected void BindTeamList()
        {
            //通过线路编号，绑定团队计划
            IList<EyouSoft.Model.NewTourStructure.MPowderList> list = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(intPageSize, intPageIndex, ref intRecordCount, RouteId);
            if (list != null && list.Count > 0)
            {
                isShow = true;
                this.TourList.DataSource = list;
                this.TourList.DataBind();
                this.ExportPageInfo1.intPageSize = intPageSize;
                this.ExportPageInfo1.intRecordCount = intRecordCount;
                this.ExportPageInfo1.CurrencyPage = intPageIndex;
                this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?RouteId=" + RouteId + "&";
            }

        }
        #endregion

        #region 获取URL链接
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="TourId">团队编号</param>
        /// <param name="LeaveDate">出团日期</param>
        /// <returns></returns>
        protected string GetUrl(string TourId, DateTime LeaveDate)
        {
            string url = string.Empty;
            if (IsTourAgency || (IsRouteAgency && IsTourAgency))
            {
                if (LeaveDate.Subtract(DateTime.Now.Date).Days > 0)
                {
                    url = "<a href='/Order/OrderByTour.aspx?tourID=" + TourId + "' class='basic_btn'><span>预订</span></a>";
                }
                else
                {
                    url = "已过期 ";
                }

            }
            if (!IsTourAgency && IsRouteAgency)
            {
                if (LeaveDate.Subtract(DateTime.Now.Date).Days > 0)
                {
                    url = "<a href='/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + TourId + "' class='basic_btn'><span>代订</span></a>";
                }
                else
                {
                    url = "已过期";
                }
            }
            return url;
        }
        #endregion
    }
}
