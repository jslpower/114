using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using System.Text;
using System.Collections;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    ///  团队行程单打印页面
    ///  创建时间： 2011-12-22   方琪
    /// </summary>
    public partial class TeamTourInfo : EyouSoft.Common.Control.BasePage
    {
        #region 页面变量
        /// <summary>
        /// 公司logo
        /// </summary>
        protected string TravelLogo = string.Empty;
        /// <summary>
        /// 团队编号
        /// </summary>
        protected string TeamId = Utils.GetQueryStringValue("TeamId");
        /// <summary>
        /// 是否为标准日程
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
            //页面标题
            this.Page.Title = "团队行程单";
            //页面加载
            InitPage();
        }
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage()
        {
            //通过团队编号获取相关实体
            EyouSoft.Model.NewTourStructure.MPowderPrint PowderPrintModel =
                EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModelPrint(TeamId);
            this.LabGetTime.Text = GetTime();
            if (PowderPrintModel != null)
            {
                if (PowderPrintModel.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                {
                    isInternational = true;
                }
                #region 普通信息

                #region 联系信息2012-04-20  周文超 替换成当前登录用户的

                plhContact.Visible = IsLogin;
                if (IsLogin && SiteUserInfo != null)
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyDetailInfo =
                        EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                    //公司logo
                    if (!string.IsNullOrEmpty(companyDetailInfo.AttachInfo.CompanyLogo.ImagePath))
                        this.TravelLogo = string.Format("<img src=\"{0}\" height=\"60\">", EyouSoft.Common.Domain.FileSystem + companyDetailInfo.AttachInfo.CompanyLogo.ImagePath);

                    //公司名称
                    this.TravelName.Text = companyDetailInfo.CompanyName;
                    //公司地址
                    this.TravelAddress.Text = companyDetailInfo.CompanyAddress;
                    //联系人
                    this.Contact.Text = companyDetailInfo.ContactInfo.ContactName;
                    //传真
                    this.Fax.Text = companyDetailInfo.ContactInfo.Fax;
                    //电话
                    this.Phone.Text = companyDetailInfo.ContactInfo.Tel;
                    //手机
                    this.Mobile.Text = companyDetailInfo.ContactInfo.Mobile;
                }

                #endregion

                //线路名称
                this.RouteName.Text = PowderPrintModel.RouteName;
                //线路名称
                this.RouteName1.Text = PowderPrintModel.RouteName;
                //团号
                this.TourNo.Text = PowderPrintModel.TourNo;
                //报名截止日期 
                this.RegistrationEndDate.Text = PowderPrintModel.RegistrationEndDate.ToString("yyyy-MM-dd");
                //天数
                this.Dates.Text = PowderPrintModel.Day.ToString();
                //出发交通
                this.TrafficandCity.Text = PowderPrintModel.StartCityName + "  " +
                    PowderPrintModel.StartTraffic + "  " + PowderPrintModel.StartDate;
                //出发时间
                this.LeaveDate.Text = PowderPrintModel.LeaveDate.ToString("yyyy-MM-dd");
                //返回交通
                this.TrafficandBackCity.Text = PowderPrintModel.EndCityName + "  " +
                    PowderPrintModel.EndTraffic + "  " + PowderPrintModel.EndDate;
                //返抵达日
                this.ComeBackDate.Text = PowderPrintModel.ComeBackDate.ToString("yyyy-MM-dd");
                //成人市场价
                this.AdultPrice.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.RetailAdultPrice);
                //儿童市场价
                this.ChildPrice.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.RetailChildrenPrice);
                //单房差
                this.danfangcha.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.MarketPrice);
                //成人定金
                this.AdaultDeposit.Text = PowderPrintModel.AdultPrice == 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.AdultPrice);
                //儿童定金
                this.ChildDeposit.Text = PowderPrintModel.ChildrenPrice == 0 ? "电询" : Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.ChildrenPrice);
                //主要游览地区
                this.MainArea.Text = PowderPrintModel.BrowseCity;
                //签证地区
                this.VisaArea.Text = PowderPrintModel.VisaCity;
                //线路特色
                this.RouteFeatures.Text = Utils.TextToHtml(PowderPrintModel.Characteristic);
                #endregion

                #region 日程信息
                if (PowderPrintModel.StandardPlan != null && PowderPrintModel.StandardPlan.Count > 0)
                {
                    isStandard = true;
                    //绑定标准日程
                    BindStandardPlan(PowderPrintModel.StandardPlan);
                }
                else
                {
                    //简易行程
                    this.FastStandard.Text = PowderPrintModel.FastPlan;
                }
                #endregion

                #region 服务信息
                if (PowderPrintModel.FitQuotation != null && PowderPrintModel.FitQuotation != "")
                {
                    this.Containers.Text = PowderPrintModel.FitQuotation;
                }
                if (PowderPrintModel.ServiceStandard != null)
                {
                    if (PowderPrintModel.FitQuotation == null || PowderPrintModel.FitQuotation == "")
                    {
                        this.Containers.Text = Utils.TextToHtml(GetContainers(PowderPrintModel.ServiceStandard));//服务包含
                    }
                    this.NoContainers.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.NotContainService);//服务不含
                    this.Children.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ChildrenInfo);//儿童
                    this.Gift.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.GiftInfo);//赠送
                    this.Shopping.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ShoppingInfo);//购物
                    this.OwnExpense.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ExpenseItem);//自费
                    this.Remark.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.Notes);//备注
                }
                #endregion

                #region 银行信息
                this.CompanyAcount.Text = PowderPrintModel.CompanyAccount;
                if (PowderPrintModel.PersonalAccount != null && PowderPrintModel.PersonalAccount.Count > 0)
                {
                    this.PersonerAcount.Text = GetPersonAccount(PowderPrintModel.PersonalAccount);
                }
                this.Alipay.Text = PowderPrintModel.AlipayAccount;
                #endregion
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

        #region 获取当前系统时间
        /// <summary>
        /// 获取当前系统时间
        /// </summary>
        /// <returns></returns>
        protected string GetTime()
        {
            DateTime time = DateTime.Now;
            return time.ToString("yyyy-MM-dd HH:mm");
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
                    sb.AppendFormat("{0}<br/>", item.ToString());
                } return sb.ToString();
            }
            else
            {
                return string.Empty;
            }


        }
        #endregion

    }
}
