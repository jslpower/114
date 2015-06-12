using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.NewTourStructure;
using System.Text;
using EyouSoft.Common;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 出团通知书页面打印
    /// 创建时间：2011-12-23 方琪
    /// </summary>
    public partial class OutGroupNotices : EyouSoft.Common.Control.BasePage
    {
        /// <summary>
        /// 团队编号
        /// </summary>
        protected string TeamId = Utils.GetQueryStringValue("TeamId");
        /// <summary>
        /// 是否是标准日程
        /// </summary>
        protected bool isStandard = false;
        /// <summary>
        /// PageLoad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //页面标题
            this.Page.Title = "出团通知书";
            //团队信息加载
            InitPage();
        }

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void InitPage()
        {
            EyouSoft.Model.NewTourStructure.MPowderPrint PowderPrintModel =
                EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModelPrint(TeamId);
            if (PowderPrintModel != null)
            {
                #region 普通信息

                #region 联系信息2012-04-20  周文超 替换成当前登录用户的

                plhContact.Visible = IsLogin;
                if (IsLogin && SiteUserInfo != null)
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyDetailInfo =
                        EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);

                    //公司名称
                    this.zutuan.Text = companyDetailInfo.CompanyName;
                    //公司地址
                    this.Address.Text = companyDetailInfo.CompanyAddress;
                    //联系人
                    this.Contact.Text = companyDetailInfo.ContactInfo.ContactName;
                    //传真
                    this.Fax.Text = companyDetailInfo.ContactInfo.Fax;
                    //电话
                    this.Tel.Text = companyDetailInfo.ContactInfo.Tel;
                }

                #endregion

                //获取打印时间
                this.LabGetTime.Text = GetTime();
                //团号
                this.GroupNo.Text = PowderPrintModel.TourNo;
                //线路名次
                this.RouteName.Text = PowderPrintModel.RouteName;
                //成人价
                this.AdultPrice.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.RetailAdultPrice);
                //儿童价 
                this.ChildPrice.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.RetailChildrenPrice);
                //单房差
                this.danfangcha.Value = Utils.FilterEndOfTheZeroDecimal(PowderPrintModel.MarketPrice);
                //出发交通
                this.LeaveTraffic.Text = PowderPrintModel.StartTraffic.ToString() ==
                    "0" ? "" : PowderPrintModel.StartTraffic.ToString() + "  " + PowderPrintModel.StartDate;
                //返程交通
                this.BackTraffic.Text = PowderPrintModel.EndTraffic.ToString() ==
                    "0" ? "" : PowderPrintModel.EndTraffic.ToString() + "  " + PowderPrintModel.EndDate;
                //出发日期
                this.LeaveDate.Text = PowderPrintModel.LeaveDate.ToString("yyyy-MM-dd");
                //集合地点
                this.CollectionAddress.Text = PowderPrintModel.SetDec;
                //返回日期
                this.BackDate.Text = (PowderPrintModel.LeaveDate + new TimeSpan(PowderPrintModel.Day + 1, 0, 0, 0, 0)).ToString("yyyy-MM-dd");
                //全陪领队 
                this.DescriptionLeader.Text = Utils.TextToHtml(PowderPrintModel.TeamLeaderDec);
                #endregion

                #region 绑定日程
                if (PowderPrintModel.StandardPlan != null && PowderPrintModel.StandardPlan.Count > 0)
                {
                    isStandard = true;
                    //绑定日程
                    BindStandardPlan(PowderPrintModel.StandardPlan);
                }
                else
                {
                    //简易日程
                    this.FastisStandard.Text = PowderPrintModel.FastPlan;
                }
                #endregion

                #region 服务信息
                if (PowderPrintModel.FitQuotation != null && PowderPrintModel.FitQuotation != "")
                {
                    this.Containers.Text = PowderPrintModel.FitQuotation;
                }
                if (PowderPrintModel.ServiceStandard != null)
                {
                    //报价包含

                    if (PowderPrintModel.FitQuotation == null || PowderPrintModel.FitQuotation == "")
                    {
                        this.Containers.Text = Utils.TextToHtml(GetContainers(PowderPrintModel.ServiceStandard));
                    }
                    //不含
                    this.NoContainers.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.NotContainService);
                    //儿童
                    this.Children.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ChildrenInfo);
                    //赠送
                    this.Gift.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.GiftInfo);
                    //购物
                    this.Shopping.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ShoppingInfo);
                    //自费
                    this.OwnExpense.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.ExpenseItem);
                    //备注
                    this.Remark.Text = Utils.TextToHtml(PowderPrintModel.ServiceStandard.Notes);
                }
                #endregion
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

        #region 绑定日程
        /// <summary>
        /// 绑定日程
        /// </summary>
        /// <param name="StandardPlan"></param>
        protected void BindStandardPlan(IList<MStandardPlan> StandardPlan)
        {
            this.richeng.DataSource = StandardPlan.OrderBy(item=>(item.PlanDay));
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

    }
}
