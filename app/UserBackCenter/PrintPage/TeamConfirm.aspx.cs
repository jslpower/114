using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.BLL.NewTourStructure;
using EyouSoft.Model.CompanyStructure;
using System.Text;
using EyouSoft.Model.TicketStructure;

namespace UserBackCenter.PrintPage
{
    /// <summary>
    /// 团队确认单页面打印
    /// 创建时间：2011-12-23 方琪
    /// </summary>
    public partial class TeamConfirm : EyouSoft.Common.Control.BasePage
    {
        #region 页面变量
        /// <summary>
        /// 序号
        /// </summary>
        protected int count = 0;
        /// <summary>
        /// 订单编号
        /// </summary>
        protected string OrderId = string.Empty;
        /// <summary>
        /// 散拼信息实体
        /// </summary>
        protected MPowderList PowderListModel = null;
        /// <summary>
        /// 散拼订单实体
        /// </summary>
        protected MTourOrder TOModel = null;
        /// <summary>
        /// 是否是标准行程
        /// </summary>
        protected bool isStandard = false;
        #endregion

        #region Page_Load
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderId = Utils.GetQueryStringValue("OrderId");
            //页面标题
            this.Page.Title = "团队确认单";
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
            //根据订单编号获取相应的实体
            var IBLL = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            TOModel = EyouSoft.BLL.NewTourStructure.BTourOrder.CreateInstance().GetModel(OrderId);
            if (TOModel != null)
            {
                #region 根据专线商编号获取专线商公司实体信息
                CompanyDetailInfo DetailInfoModelZX = IBLL.GetModel(TOModel.Publishers);
                if (DetailInfoModelZX != null)
                {
                    //专线商公司信息
                    this.Zhuanxian.Text = DetailInfoModelZX.CompanyName;
                    this.Zhuanxian1.Text = DetailInfoModelZX.CompanyName;
                    //专线商联系信息
                    this.ZhuanxianContact.Text = DetailInfoModelZX.ContactInfo.ContactName + " 手机 " + DetailInfoModelZX.ContactInfo.Mobile + " 传真号 " + DetailInfoModelZX.ContactInfo.Fax;
                    this.zxBankInfo.Text = GetBankList(DetailInfoModelZX.BankAccounts);
                }
                #endregion

                #region 根据组团社编号获取组团社公司实体信息
                CompanyDetailInfo DetailInfoModelZT = IBLL.GetModel(TOModel.Travel);
                if (DetailInfoModelZT != null)
                {
                    //组团社
                    this.TravelName.Text = DetailInfoModelZT.CompanyName;
                    //组团联系人
                    this.ZutuanContact.Text = DetailInfoModelZT.ContactInfo.ContactName + " 手机 " + DetailInfoModelZT.ContactInfo.Mobile + " 传真号 " + DetailInfoModelZT.ContactInfo.Fax;
                    this.ztBankInfo.Text = GetBankList(DetailInfoModelZT.BankAccounts);
                }
                #endregion

                #region 绑定游客信息
                if (TOModel.Customers != null && TOModel.Customers.Count > 0)
                {
                    this.TouristInfomation.DataSource = TOModel.Customers;
                    this.TouristInfomation.DataBind();
                }
                #endregion

                #region 根据团队编号获取散拼信息实体
                PowderListModel = BPowderList.CreateInstance().GetModel(TOModel.TourId);
                if (PowderListModel != null)
                {
                    //绑定日程
                    if (PowderListModel.StandardPlans != null && PowderListModel.StandardPlans.Count > 0)
                    {
                        isStandard = true;
                        BindStandardPlan(PowderListModel.StandardPlans);
                    }
                    else
                    {
                        this.FastStandard.Text = PowderListModel.StandardStroke;
                    }
                    if (PowderListModel.FitQuotation != null && PowderListModel.FitQuotation != "")
                    {
                        this.Containes.Text = PowderListModel.FitQuotation;
                    }
                    //报价包含
                    if (PowderListModel.ServiceStandard != null)
                    {
                        if (PowderListModel.FitQuotation == null || PowderListModel.FitQuotation == "")
                        {
                            this.Containes.Text = Utils.TextToHtml(GetContainers(PowderListModel.ServiceStandard));
                        }
                        //报价不含
                        this.NoContaines.Text = Utils.TextToHtml(PowderListModel.ServiceStandard.NotContainService);
                    }
                }
                #endregion

                #region 普通信息
                //团号
                this.TeamId.Text = TOModel.TourNo;
                //线路名称
                this.RouteName.Text = TOModel.RouteName;
                //出团时间
                this.LeaveDate.Text = TOModel.LeaveDate.ToString("yyyy-MM-dd");
                //人数
                this.PersonNum.Text = (TOModel.AdultNum + TOModel.ChildrenNum).ToString();
                //备注
                this.Remark.Text = Utils.TextToHtml(TOModel.TourNotes);
                //结算价合计
                this.SumSettlement.Value = Utils.FilterEndOfTheZeroDecimal(TOModel.TotalSettlementPrice);
                #endregion
            }
        }
        #endregion

        #region 获取公司个人银行信息
        /// <summary>
        /// 获取公司个人银行信息
        /// </summary>
        /// <param name="BankAccounts">银行信息</param>
        /// <returns>银行信息</returns>
        protected string GetBankList(List<BankAccount> BankAccounts)
        {
            StringBuilder sb = new StringBuilder();
            if (BankAccounts != null && BankAccounts.Count > 0)
            {
                foreach (BankAccount item in BankAccounts)
                {
                    if (item.AccountType == BankAccountType.公司)
                    {
                        sb.Append(item.BankName + ":" + item.AccountNumber + "<br/>");
                    }
                }
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
            return time.ToString("yyyy-MM-dd");
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

        #region 序号
        /// <summary>
        /// 序号
        /// </summary>
        /// <returns>序号</returns>
        protected int GetCount()
        {
            return ++count;
        }
        #endregion

        #region 获取市场价
        /// <summary>
        /// 获取市场价
        /// </summary>
        /// <param name="CradType">身份类型</param>
        /// <param name="RetailAdultPrice">成人市场价</param>
        /// <param name="RetailChildrenPrice">儿童市场价</param>
        /// <returns>市场价</returns>
        protected string GetRetailPrice(TicketVistorType CradType, Decimal RetailAdultPrice, Decimal RetailChildrenPrice)
        {
            if (CradType == TicketVistorType.成人)
            {
                return Utils.FilterEndOfTheZeroDecimal(RetailAdultPrice);
            }
            else
            {
                return Utils.FilterEndOfTheZeroDecimal(RetailChildrenPrice);
            }
        }
        #endregion

        #region 获取结算价
        /// <summary>
        /// 获取结算价
        /// </summary>
        /// <param name="CradType">身份类型</param>
        /// <param name="SettlementAdultPrice">成人结算价</param>
        /// <param name="SettlementChildrenPrice">儿童结算价</param>
        /// <returns>结算价</returns>
        protected string GetSettlementPrice(TicketVistorType CradType, Decimal SettlementAdultPrice, Decimal SettlementChildrenPrice)
        {
            if (CradType == TicketVistorType.成人)
            {
                return Utils.FilterEndOfTheZeroDecimal(SettlementAdultPrice);
            }
            else
            {
                return Utils.FilterEndOfTheZeroDecimal(SettlementChildrenPrice);
            }
        }
        #endregion
    }
}
