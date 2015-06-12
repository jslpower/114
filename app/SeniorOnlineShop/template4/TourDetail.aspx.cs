
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SeniorOnlineShop.template4
{
    /// <summary>
    /// 散客预定
    /// </summary>
    /// 周文超 2010-11-12
    public partial class TourDetail : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        protected string ReturnUrl = "";
        /// <summary>
        /// 团队线路区域类型
        /// </summary>
        protected int AreaType = 1;
        /// <summary>
        /// 初始化日历当前月时期
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        /// <summary>
        /// 初始化日历下一个月时期
        /// </summary>
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));

        //mq衔接地址
        protected string MQUrlHtml = "";
        //浏览区域 签证
        protected System.Text.StringBuilder BrowSeAreaHtml = new StringBuilder();
        protected System.Text.StringBuilder qianzhengHtml = new StringBuilder();

        //铁定发团
        protected string IsCerTainHtml = string.Empty;
        //联系人 联系电话
        protected string Contect = string.Empty;
        protected string contecttel = string.Empty;
        //线路行程单
        protected string PrintUrl = string.Empty;
        //角色标识
        protected string Role = string.Empty;
        //旅游主题
        protected System.Text.StringBuilder themeList = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.CTAB = SeniorOnlineShop.master.T4TAB.散拼计划;
            string RouteID = Utils.GetQueryStringValue("routeId");
            if (!Page.IsPostBack)
            {
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo Userinfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.Master.CompanyId);
                if (Userinfo != null)
                {
                    if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                    {
                        Role = "1";
                    }
                    if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        Role = "2";
                    }
                    if (!Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) && !Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        Role = "3";
                    }
                }

                if (IsLogin)
                {
                    if (Userinfo != null)
                    {
                        if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            Contect = this.Master.CompanyInfo.ContactInfo.ContactName;
                            contecttel = this.Master.CompanyInfo.ContactInfo.Tel;
                        }
                    }
                }

                //根据团号取成人市场价
                string tourid = Utils.GetQueryStringValue("tourid");
                if (tourid != null && !string.IsNullOrEmpty(tourid))
                {
                    EyouSoft.Model.NewTourStructure.MPowderList PowderList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetModel(tourid);
                    if (PowderList != null)
                    {
                        Response.Clear();
                        Response.Write("" + Utils.FilterEndOfTheZeroDecimal(PowderList.RetailAdultPrice) + "");
                        Response.End();
                    }
                }

                if (RouteID != "")
                {
                    PrintUrl = "<a href=\"" + Domain.UserBackCenter + "/PrintPage/LineTourInfo.aspx?RouteId=" + RouteID + "\" target=\"_blank\"><img src=\"" + ImageServerPath + "/images/but03.gif\" border=\"0\" /></a>";
                    //初始化团队信息
                    this.GetTourInfo(RouteID);
                    GetPowerList(RouteID);
                }
                else
                {
                    Response.Clear();
                    Response.Redirect("" + Domain.SeniorOnlineShop + "/seniorshop/TourList.aspx");
                    Response.End();
                }
            }
        }

        protected string GetRecommendType(string state)
        {
            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品  7 经典 8 纯玩
            string stateHtml = string.Empty;
            switch (state)
            {
                case "1": stateHtml = ""; break;
                case "2": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
                default: break;
            }
            return stateHtml;
        }

        /// <summary>
        /// 获取计划信息
        /// </summary>
        /// <param name="RouteID"></param>
        protected void GetPowerList(string RouteID)
        {
            IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteID);
            if (PowerList != null && PowerList.Count > 0)
            {
                foreach (EyouSoft.Model.NewTourStructure.MPowderList ModelPower in PowerList)
                {
                    ListItem ltemlist = new ListItem(GetLeaveInfo(ModelPower.LeaveDate) + " " + "成人价" + ModelPower.RetailAdultPrice.ToString("F0") + " " + "儿童价" + ModelPower.RetailChildrenPrice.ToString("F0"), ModelPower.TourId);
                    this.DropPowerList.Items.Add(ltemlist);
                }
            }
            else
            {
                this.planInfo.Visible = false;
                this.panshowMsg.Visible = true;
            }
        }

        /// <summary>
        /// 初始化团队信息
        /// </summary>
        /// <param name="TourId">团队ID</param>
        protected void GetTourInfo(string RouteID)
        {
            EyouSoft.Model.NewTourStructure.MRoute RouteM = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(RouteID);
            if (RouteM != null)
            {
                this.labRouteName.Text = RouteM.RouteName;
                this.labTourSpreadState.Text = GetRecommendType(((int)RouteM.RecommendType).ToString());
                this.LitRetailPrices.Text = RouteM.PublicAuditPrice.ToString("F0");
                MQUrlHtml = EyouSoft.Common.Utils.GetMQ(this.Master.CompanyId);
                if (RouteM.RouteImg != null && !string.IsNullOrEmpty(RouteM.RouteImg))
                {
                    this.RouteImgUrl.ImageUrl = EyouSoft.Common.Domain.FileSystem + RouteM.RouteImg;
                }
                else
                {
                    this.RouteImgUrl.ImageUrl = Domain.ServerComponents + "/images/NoPicture.jpg";
                }

                if (RouteM.Themes != null && RouteM.Themes.Count > 0)
                {
                    for (int i = 0; i < RouteM.Themes.Count; i++)
                    {
                        themeList.Append("<span class=\"haohuayl\">" + RouteM.Themes[i].ThemeName + "</span>");
                    }
                }
                this.LitDays.Text = RouteM.Day.ToString();
                this.Litlateness.Text = RouteM.Late.ToString();
                this.litSigleUp.Text = RouteM.AdvanceDayRegistration.ToString();
                this.litStartTriffic.Text = "去程--交通:" + RouteM.StartTraffic.ToString() + ",出发地:" + RouteM.StartCityName + "<br/>";
                this.litEndTriffic.Text = "返程--交通:" + RouteM.EndTraffic.ToString() + ",目的地:" + RouteM.EndCityName;

                if (RouteM.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                {
                    this.liDjShowOrHiden.Visible = true;
                    this.LitAdultPriceDj.Text = RouteM.AdultPrice.ToString("F0");
                    this.litChildrenPriceDj.Text = RouteM.ChildrenPrice.ToString("F0");

                    if (RouteM.BrowseCountrys != null && RouteM.BrowseCountrys.Count > 0)
                    {
                        BrowSeAreaHtml.Append("<li>游览区域：");
                        qianzhengHtml.Append("<li class=\"qianzheng\">签证：");
                        for (int i = 0; i < RouteM.BrowseCountrys.Count; i++)
                        {
                            BrowSeAreaHtml.Append("" + RouteM.BrowseCountrys[i].CountryName + "&nbsp");
                            if (RouteM.BrowseCountrys[i].IsVisa == true)
                            {
                                qianzhengHtml.Append("" + RouteM.BrowseCountrys[i].CountryName + "签证&nbsp");
                            }
                        }
                        BrowSeAreaHtml.Append("</li>");
                        qianzhengHtml.Append("</li>");
                    }
                }
                else
                {
                    this.liDjShowOrHiden.Visible = false;
                    BrowSeAreaHtml.Append("<li>游览区域：");
                    if (RouteM.BrowseCitys != null && RouteM.BrowseCitys.Count > 0)
                    {
                        for (int i = 0; i < RouteM.BrowseCitys.Count; i++)
                        {
                            if (RouteM.BrowseCitys[i].CountyId == 0)
                            {
                                BrowSeAreaHtml.Append("" + RouteM.BrowseCitys[i].CityName + "&nbsp");
                            }
                            else
                            {
                                BrowSeAreaHtml.Append("" + RouteM.BrowseCitys[i].CountyName + "&nbsp");
                            }
                        }
                    }
                    BrowSeAreaHtml.Append("</li>");
                }

                if (RouteM.IsCertain == true)
                {
                    IsCerTainHtml = "无需成团，铁定发团";
                }

                //特色线路
                this.litHotRoute.Text = RouteM.Characteristic;

                //子团选项
                IList<EyouSoft.Model.NewTourStructure.MPowderList> Powderlist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteM.RouteId);
                if (Powderlist != null && Powderlist.Count > 0)
                {
                    //给日历赋子团信息
                    IList<EyouSoft.Model.NewTourStructure.MPowderList> CalendarChild = this.filterChildren(Powderlist);
                    IsoDateTimeConverter isoDate = new IsoDateTimeConverter();
                    isoDate.DateTimeFormat = "yyyy-MM-dd";
                    string scripts = string.Format("QGD.config.Childrens={0};", JsonConvert.SerializeObject(CalendarChild, isoDate));
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, scripts);
                }

                //行程安排
                if (RouteM.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Standard)
                {
                    if (RouteM.StandardPlans != null && RouteM.StandardPlans.Count > 0)
                    {
                        IList<EyouSoft.Model.NewTourStructure.MStandardPlan> PlanInfo = RouteM.StandardPlans.OrderBy(p => p.PlanDay).ToList();

                        StringBuilder strAllDateInfo = new StringBuilder();
                        for (int i = 0; i < PlanInfo.Count; i++)
                        {
                            string Dinner = string.Empty;
                            if (PlanInfo[i].Early == true) { Dinner += "早,"; };
                            if (PlanInfo[i].Center == true) { Dinner += "中,"; };
                            if (PlanInfo[i].Late == true) { Dinner += "晚"; };
                            strAllDateInfo.Append("<div class=\"m10\">");
                            strAllDateInfo.Append("<div class=\"trl_d_detail\"> <div class=\"trl_d_anpaiday\">第" +
                                                  (PlanInfo[i].PlanDay) + "天</div><div class=\"xing\"><span>行：</span>" +
                                                  (Convert.ToInt32(PlanInfo[i].Vehicle) <= 0
                                                       ? string.Empty
                                                       : PlanInfo[i].Vehicle.ToString()) +
                                                  "</div><div class=\"zhu\"><span>住：</span>" + PlanInfo[i].House +
                                                  "</div><div class=\"can\"><span>餐：</span>" + Dinner.TrimEnd(',') +
                                                  "</div></div>");
                            strAllDateInfo.Append("<div class=\"trl_d_detail1\"> <div class=\"trl_xcleft\">" +
                                                  Utils.TextToHtml(PlanInfo[i].PlanContent) + "</div></div>");
                            strAllDateInfo.Append("</div>");
                        }
                        this.LiteralTourPlan.Text = strAllDateInfo.ToString();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(RouteM.FastPlan))
                    {
                        this.LiteralTourPlan.Text = RouteM.FastPlan;
                    }
                }

                //服务项目
                if (RouteM.ServiceStandard != null)
                {
                    System.Text.StringBuilder ServiceHtml = new StringBuilder();
                    if (string.IsNullOrEmpty(RouteM.FitQuotation))
                    {
                        int index = 1;
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.ResideContent))
                            ServiceHtml.AppendFormat("{0}.住宿：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.ResideContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.DinnerContent))
                            ServiceHtml.AppendFormat("{0}.用餐：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.DinnerContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.SightContent))
                            ServiceHtml.AppendFormat("{0}.景点：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.SightContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.CarContent))
                            ServiceHtml.AppendFormat("{0}.用车：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.CarContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.GuideContent))
                            ServiceHtml.AppendFormat("{0}.导游：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.GuideContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.TrafficContent))
                            ServiceHtml.AppendFormat("{0}.往返交通：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.TrafficContent));
                        if (!string.IsNullOrEmpty(RouteM.ServiceStandard.IncludeOtherContent))
                            ServiceHtml.AppendFormat("{0}.其它包含：{1}<br/>", (index++),
                                                     Utils.TextToHtml(RouteM.ServiceStandard.IncludeOtherContent));

                        lblServiceStandard.Text = ServiceHtml.ToString();
                    }
                    else
                    {
                        this.lblServiceStandard.Text = Utils.TextToHtml(RouteM.FitQuotation);
                    }
                    //报价不含
                    this.lblServiceStandardNo.Text = Utils.TextToHtml(RouteM.ServiceStandard.NotContainService);
                    //儿童及其他安排
                    this.lblAdultAndOtherPlan.Text = Utils.TextToHtml(RouteM.ServiceStandard.ChildrenInfo);
                    //赠送项目
                    this.LitPresentedProject.Text = Utils.TextToHtml(RouteM.ServiceStandard.GiftInfo);
                    //自费项目
                    this.LitCopaymentsProject.Text = Utils.TextToHtml(RouteM.ServiceStandard.ExpenseItem);
                    //购物点
                    this.LitShoppingPlace.Text = Utils.TextToHtml(RouteM.ServiceStandard.ShoppingInfo);
                    //备注信息
                    this.LitRemarkInfo.Text = Utils.TextToHtml(RouteM.ServiceStandard.Notes);
                }
                //同业销售需知
                this.litSalesNotice.Text = Utils.TextToHtml(RouteM.VendorsNotes);
            }

        }
        /// <summary>
        /// 格式化出团日期
        /// </summary>
        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}-{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }


        /// <summary>
        /// 日历子团信息
        /// 取有用到的数据，没登录时，过滤掉同行价
        /// </summary>
        protected IList<EyouSoft.Model.NewTourStructure.MPowderList> filterChildren(IList<EyouSoft.Model.NewTourStructure.MPowderList> OldChildrens)
        {
            IList<EyouSoft.Model.NewTourStructure.MPowderList> NewChildrens = new List<EyouSoft.Model.NewTourStructure.MPowderList>();
            foreach (EyouSoft.Model.NewTourStructure.MPowderList Model in OldChildrens)
            {
                EyouSoft.Model.NewTourStructure.MPowderList item = new EyouSoft.Model.NewTourStructure.MPowderList();
                item.TourId = Model.TourId;
                item.TourNo = Model.TourNo;
                item.LeaveDate = Model.LeaveDate;
                item.RetailAdultPrice = Model.RetailAdultPrice;
                item.PowderTourStatus = Model.PowderTourStatus;
                item.MoreThan = Model.MoreThan;
                item.TourNum = Model.TourNum;
                item.IsLimit = Model.IsLimit;
                NewChildrens.Add(item);
                item = null;
            }
            return NewChildrens;
        }


        /// <summary>
        /// 散客预订快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl(string tourID, string adult, string child, string contact, string tel)
        {
            string url = string.Empty;
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo UserInfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.Master.CompanyId);
            if (UserInfo != null)
            {
                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + tourID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
                }

                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?tourID=" + tourID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
                }
                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) && UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?tourID=" + tourID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
                }
            }
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        /// <summary>
        /// 散客下订单
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl(string tourID)
        {
            string url = string.Empty;
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo UserInfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.Master.CompanyId);
            if (UserInfo != null)
            {
                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + tourID;
                }
                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?tourID=" + tourID;
                }
                if (UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) && UserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    url = EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?tourID=" + tourID;
                }
            }
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        /// <summary>
        /// 单团预订快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrlsingle(string routeID, string adult, string child, string contact, string tel)
        {
            string url = EyouSoft.Common.Domain.UserBackCenter + "/TeamService/SingleGroupPre.aspx?isZT=true&routeId=" + routeID + "&adult=" + adult + "&child=" + child + "&contact=" + contact + "&tel=" + tel;
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        /// <summary>
        /// 同业销售需知快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }


    }
}
