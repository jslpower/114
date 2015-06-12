using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using System.Text;
using System.Collections;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 线路行程单页面打印
    /// 创建时间：2011-12-22 方琪
    /// </summary>
    public partial class LineTourInfo : EyouSoft.Common.Control.BasePage
    {
        /// <summary>
        /// 旅行社logo
        /// </summary>
        protected string TravelLogo = string.Empty;
        /// <summary>
        /// 是否是标准日程
        /// </summary>
        protected bool isStandard = false;
        /// <summary>
        /// 是否是国际线路
        /// </summary>
        protected bool isInternational = false;
        /// <summary>
        /// 是否显示出团计划
        /// </summary>
        protected bool isShow = false;
        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //页面标题
            this.Page.Title = "线路行程单";
            //线路编号
            string RouteId = Utils.GetQueryStringValue("RouteId");
            if (RouteId != null && !string.IsNullOrEmpty(RouteId))
            {
                //加载页面信息
                InitPage(RouteId);
            }
        }
        #endregion


        #region 加载页面
        /// <summary>
        /// 加载页面信息
        /// </summary>
        protected void InitPage(string RouteId)
        {
            //根据线路编号获取相应的线路实体
            EyouSoft.Model.NewTourStructure.MRoutePrint RoutePrintModel =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetPrintModel(RouteId);
            if (RoutePrintModel != null)
            {
                if (RoutePrintModel.RouteType == AreaType.国际线)
                {
                    isInternational = true;
                }
                #region 普通信息

                //打印时间
                this.LabGetTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                #region 联系信息2012-04-20  周文超 替换成当前登录用户的

                plhContact.Visible = IsLogin;
                if (IsLogin && SiteUserInfo != null)
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyDetailInfo =
                        EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);

                    //公司logo
                    if (!string.IsNullOrEmpty(companyDetailInfo.AttachInfo.CompanyLogo.ImagePath))
                        this.TravelLogo = string.Format(
                            "<img src=\"{0}\" height=\"60\">", EyouSoft.Common.Domain.FileSystem + companyDetailInfo.AttachInfo.CompanyLogo.ImagePath);

                    //发布商名称
                    this.TravelName.Text = companyDetailInfo.CompanyName;
                    //地址
                    this.TravelAddress.Text = companyDetailInfo.CompanyAddress;
                    //传真
                    this.Fax.Text = companyDetailInfo.ContactInfo.Fax;
                    //电话
                    this.Phone.Text = companyDetailInfo.ContactInfo.Tel;
                    //手机
                    this.Mobile.Text = companyDetailInfo.ContactInfo.Mobile;
                    //联系人
                    this.Contact.Text = companyDetailInfo.ContactInfo.ContactName;
                }

                #endregion

                //线路名称
                this.RouteName.Text = RoutePrintModel.RouteName;
                //线路名称
                this.RouteName1.Text = RoutePrintModel.RouteName;
                //主题
                this.Themes.Text = RoutePrintModel.ThemeName;
                //出发城市和交通
                this.TrafficandCity.Text = RoutePrintModel.StartCityName + "  " +
                    RoutePrintModel.StartTraffic.ToString();
                //返程城市和交通
                this.TrafficandBackCity.Text = RoutePrintModel.EndCityName + "  " +
                    RoutePrintModel.EndTraffic.ToString();
                //旅游天数
                this.Dates.Text = RoutePrintModel.Day.ToString() + "天";
                //报名截至时间
                this.ApplyEndDate.Text = string.Format("提前{0}日报名", RoutePrintModel.AdvanceDayRegistration);
                //主要游览地区
                this.MainTourArea.Text = RoutePrintModel.BrowseCity;
                //签证地区
                this.VisaArea.Text = RoutePrintModel.VisaCity;
                var strAdultPrice = new StringBuilder();
                if (RoutePrintModel.AdultPrice <= 0)
                {
                    if (RoutePrintModel.AdultPrice == -1)
                        strAdultPrice.Append("成人：电询 &nbsp;");
                    if (RoutePrintModel.AdultPrice == 0)
                        strAdultPrice.Append("成人：无需定金 &nbsp;");
                }
                else
                {
                    strAdultPrice.AppendFormat("成人：{0} 元/人&nbsp;",
                                               Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.AdultPrice));
                }
                if (RoutePrintModel.ChildrenPrice <= 0)
                {
                    if (RoutePrintModel.AdultPrice == -1)
                        strAdultPrice.Append("儿童：电询 &nbsp;");
                    if (RoutePrintModel.AdultPrice == 0)
                        strAdultPrice.Append("儿童：无需定金 &nbsp;");
                }
                else
                {
                    strAdultPrice.AppendFormat(" 儿童：{0} 元/人&nbsp;&nbsp;&nbsp;",
                                               Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.ChildrenPrice));
                }
                ltrPrice.Text = strAdultPrice.ToString();

                //线路特色
                this.RouteFeatures.Text = Utils.TextToHtml(RoutePrintModel.Characteristic);
                #endregion

                #region 市场价
                if (RoutePrintModel.TeamPlanDes != null && RoutePrintModel.TeamPlanDes.Count > 0)
                {
                    this.Price.Value = Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.MinRetailAdultPrice);
                }
                else
                {
                    this.Price.Value = Utils.FilterEndOfTheZeroDecimal(RoutePrintModel.ReferencePrice);
                }
                #endregion

                #region 出团计划
                if (RoutePrintModel.TeamPlanDes != null && RoutePrintModel.TeamPlanDes.Count > 0)
                {
                    isShow = true;
                    this.GroupPlan.Text = GetTeamPlanDes(RoutePrintModel.TeamPlanDes);
                }
                #endregion

                #region 日程
                if (RoutePrintModel.StandardPlan != null && RoutePrintModel.StandardPlan.Count > 0)
                {
                    if (RoutePrintModel.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)
                    {
                        isStandard = true;
                        //绑定标准日程
                        BindStandardPlan(RoutePrintModel.StandardPlan);
                    }
                }
                else
                {
                    //简易行程
                    this.FastisStandard.Text = RoutePrintModel.FastPlan;
                }
                #endregion

                #region 服务项目
                if (RoutePrintModel.FitQuotation != null && RoutePrintModel.FitQuotation != "")
                {
                    //简易报价包含
                    this.Containers.Text = RoutePrintModel.FitQuotation;
                }
                if (RoutePrintModel.ServiceStandard != null)
                {
                    //服务包含
                    if (RoutePrintModel.FitQuotation == null || RoutePrintModel.FitQuotation == "")
                    {
                        this.Containers.Text = Utils.TextToHtml(GetContainers(RoutePrintModel.ServiceStandard));
                    }
                    //服务不含
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

                #region 银行信息公司账户
                this.CompanyAcount.Text = RoutePrintModel.CompanyAccount;
                //个人账户
                this.PersonerAcount.Text = GetPersonAccount(RoutePrintModel.PersonalAccount);
                // 支付宝
                this.Alipay.Text = RoutePrintModel.AlipayAccount;
                #endregion
            }
        }
        #endregion

        #region 绑定日程
        /// <summary>
        /// 绑定日程
        /// </summary>
        /// <param name="StandardPlan">行程</param>
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
        /// <returns>服务项目</returns>
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
            }
            return sb.ToString(); ;
        }
        #endregion

        #region 获取个人账户
        /// <summary>
        /// 获取个人账户
        /// </summary>
        /// <param name="PersonalAccount"></param>
        /// <returns></returns>
        protected string GetPersonAccount(ArrayList PersonalAccount)
        {
            StringBuilder sb = new StringBuilder();
            if (PersonalAccount != null && PersonalAccount.Count > 0)
            {
                foreach (var item in PersonalAccount)
                {
                    sb.AppendFormat("{0}</br>", item.ToString());
                }
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 去重复
        /// <summary>
        /// 去重复
        /// </summary>
        /// <param name="TeamPlanDes">出团计划</param>
        /// <returns>出团计划</returns>
        protected ArrayList NoAgGain(ArrayList TeamPlanDes)
        {
            if (TeamPlanDes == null)
            {
                return TeamPlanDes;
            }
            else
            {
                ArrayList Temp = new ArrayList();
                foreach (var item in TeamPlanDes)
                {
                    if (!Temp.Contains(item))
                    {
                        Temp.Add(item);
                    }
                }
                return Temp;
            }
        }
        #endregion

        #region 获取出团计划
        /// <summary>
        /// 获取出团计划
        /// </summary>
        /// <param name="TeamPlanDes">出团计划</param>
        /// <returns>出团计划</returns>
        protected string GetTeamPlanDes(ArrayList TeamPlanDes)
        {
            if (TeamPlanDes != null)
            {
                TeamPlanDes = NoAgGain(TeamPlanDes);
                StringBuilder OutPlan = new StringBuilder();
                string Month = string.Empty;
                string Day = string.Empty;
                ArrayList Months = new ArrayList();
                for (int i = 0; i < TeamPlanDes.Count; i++)
                {
                    Month = Convert.ToDateTime(TeamPlanDes[i]).ToString("MM");
                    if (!Months.Contains(Month))
                    {
                        Months.Add(Month);
                    }
                }
                for (int j = 0; j < Months.Count; j++)
                {
                    OutPlan.Append(Months[j].ToString() + "月");
                    for (int i = 0; i < TeamPlanDes.Count; i++)
                    {
                        Month = Convert.ToDateTime(TeamPlanDes[i]).ToString("MM");
                        Day = Convert.ToDateTime(TeamPlanDes[i]).ToString("dd");
                        if (Month == Months[j].ToString())
                        {
                            if (i != TeamPlanDes.Count - 1)
                            {
                                OutPlan.Append(Day + ",");
                            }
                            else
                            {
                                OutPlan.Append(Day);
                            }
                        }
                    }
                    if (j != Months.Count - 1)
                    {
                        OutPlan.Append("<br/>");
                    }
                }
                return OutPlan.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
