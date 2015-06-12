using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace UserBackCenter.usercontrol
{
    /// <summary>
    /// 用户后台首页右边用户控件（专线、组团、地接用）
    /// </summary>
    /// 周文超 2011-11-04  增加景区 IsSightAgency
    public partial class UserBackDefault : System.Web.UI.UserControl
    {
        protected string ImageServerPath = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);

        protected string MQID = "0";
        protected string FirstMenu = string.Empty;
        protected string HighShopURL = string.Empty;

        protected bool IsRouteAgency = false,
                       IsTourAgency = false,
                       IsLocalAgency = false,
                       IsOther = false,
                       IsOpenHighShop = false;
        protected bool IsSightAgency = false;  //是否景区

        protected string strStyle = string.Empty;

        private EyouSoft.SSOComponent.Entity.UserInfo _userinfomodel;
        public EyouSoft.SSOComponent.Entity.UserInfo UserInfoModel
        {
            set { _userinfomodel = value; }
            get { return _userinfomodel; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserInfoModel.ContactInfo != null)
            {
                MQID = UserInfoModel.ContactInfo.MQ;
            }

            if (!IsPostBack)
            {
                InitBasicInfo();

                HighShopURL = Utils.GetCompanyDomain(UserInfoModel.CompanyID, EyouSoft.Model.CompanyStructure.CompanyType.专线);

                //判断是否供应商
                if (UserInfoModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线) || UserInfoModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.酒店) || UserInfoModel.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.景区))
                {
                    this.spanOration.Visible = true;
                }

                //加载公告信息
                PageMsgInit();
            }
        }

        #region 初始化基础信息
        /// <summary>
        /// 初始化基础信息
        /// </summary>
        private void InitBasicInfo()
        {
            if (UserInfoModel != null)
            {
                EyouSoft.Model.CompanyStructure.CompanyInfo comModel = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(UserInfoModel.CompanyID);
                if (comModel != null)
                {
                    this.lblLoginUser.Text = "您好，" + UserInfoModel.UserName + Utils.GetCompanyLevImg(comModel.CompanyLev) + "　欢迎登录同业114。信息完整度:<a class=\"ff0000\" href='javascript:void(0);' onclick='topTab.open(\"/systemset/companyinfoset.aspx\",\"单位信息\");return false;' >" + (comModel.InfoFull * 100).ToString("00") + "%</a>";
                }
                else
                {
                    this.lblLoginUser.Text = "您好，" + UserInfoModel.UserName + "　欢迎登录同业114。";
                }
            }
            EyouSoft.IBLL.CompanyStructure.ICompanySetting CSetBll = EyouSoft.BLL.CompanyStructure.CompanySetting.CreateInstance();
            EyouSoft.Model.CompanyStructure.CompanySetting CSetModel = CSetBll.GetModel(UserInfoModel.CompanyID);
            if (CSetModel != null)
            {
                switch (CSetModel.FirstMenu)
                {
                    case EyouSoft.Model.CompanyStructure.MenuSection.专线服务:
                        FirstMenu = "0";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.组团服务:
                        FirstMenu = "1";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.地接服务:
                        FirstMenu = "2";
                        break;
                    case EyouSoft.Model.CompanyStructure.MenuSection.景区服务:
                        FirstMenu = "3";
                        break;
                    default:
                        FirstMenu = "1";
                        break;
                }
            }

            #region 公告区
            EyouSoft.IBLL.SystemStructure.ISummaryCount SummaryBll = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance();
            EyouSoft.Model.SystemStructure.SummaryCount SummaryModel = SummaryBll.GetSummary();
            if (SummaryModel != null)
            {
                this.lblRouteAgencyCount.Text = (SummaryModel.TravelAgency + SummaryModel.TravelAgencyVirtual).ToString();
                this.lblHotelCount.Text = (SummaryModel.Hotel + SummaryModel.HotelVirtual).ToString();
                this.lblSightCount.Text = (SummaryModel.Sight + SummaryModel.SightVirtual).ToString();
                this.lblCarCount.Text = (SummaryModel.Car + SummaryModel.CarVirtual).ToString();
                this.lblShoppingCount.Text = (SummaryModel.Shop + SummaryModel.ShopVirtual).ToString();
            }
            SummaryModel = null;
            SummaryBll = null;
            #endregion

            #region 同业114提醒
            EyouSoft.Model.CompanyStructure.CompanyRole RoleModel = UserInfoModel.CompanyRole;
            EyouSoft.Model.CompanyStructure.CompanyType[] enumType = RoleModel.RoleItems;

            #region 专线和组团显示控制
            if (enumType != null && enumType.Length > 0)
            {
                foreach (EyouSoft.Model.CompanyStructure.CompanyType type in enumType)
                {
                    if (type == EyouSoft.Model.CompanyStructure.CompanyType.专线)
                    {
                        IsRouteAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.组团 || type == EyouSoft.Model.CompanyStructure.CompanyType.其他采购商)
                    {
                        IsTourAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.地接)
                    {
                        IsLocalAgency = true;
                    }
                    else if (type == EyouSoft.Model.CompanyStructure.CompanyType.景区)
                    {
                        IsSightAgency = true;
                    }
                    else
                    {
                        IsOther = true;
                    }
                }

                if (IsRouteAgency)   // 仅为专线
                {
                    this.span_RouteAgency.Visible = true;
                    this.ul_RouteAgency.Visible = true;
                }
                if (IsTourAgency)     // 仅为组团
                {
                    this.span_TourAgency.Visible = true;
                    this.ul_TourAgency.Visible = true;
                }
                if (IsLocalAgency)   //仅为地接
                {
                    this.span_ErAgency.Visible = true;
                    this.ul_ErAgency.Visible = true;
                }
                if (IsSightAgency) //景区
                {
                    this.span_Scenic.Visible = true;
                    this.ul_Scenic.Visible = true;
                }
                //if (IsOther)
                //{
                //    Utils.ShowError("对不起，您不是旅行社账户!", "");

                //    this.span_RouteAgency.Visible = false;
                //    this.ul_RouteAgency.Visible = false;
                //    this.span_TourAgency.Visible = false;
                //    this.ul_TourAgency.Visible = false;
                //    this.span_ErAgency.Visible = false;
                //    this.ul_ErAgency.Visible = false;
                //    this.span_Scenic.Visible = true;
                //    this.ul_Scenic.Visible = true;
                //    divNoLocalAgency.Visible = false;
                //}
            }
            #endregion

            EyouSoft.IBLL.TourStructure.ITour TourBll = EyouSoft.BLL.TourStructure.Tour.CreateInstance();
            EyouSoft.IBLL.NewTourStructure.ITourList iorderBll = EyouSoft.BLL.NewTourStructure.BTourList.CreateInstance();
            EyouSoft.IBLL.NewTourStructure.IPowderList powderBll = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance();
            EyouSoft.IBLL.NewTourStructure.IRoute routeBll = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            Dictionary<string, int> dic = null;
            #region 组团提醒
            if (IsTourAgency)     // 组团提醒
            {
                //获得线路数和团队数
                EyouSoft.IBLL.SystemStructure.ISysCity CityBll = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance();
                EyouSoft.Model.SystemStructure.SysCity CityModel = CityBll.GetSysCityModel(UserInfoModel.CityId);
                if (CityModel != null)
                {
                    this.lblCity.Text = CityModel.CityName;
                    this.lblTemplateTourCount.Text = CityModel.ParentTourCount.ToString();
                    this.lblChildTourCount.Text = CityModel.TourCount.ToString();
                }
                CityModel = null;
                CityBll = null;

                //获得组团订单信息
                dic = iorderBll.GetOrderTravelCount(UserInfoModel.CompanyID);
                if (dic != null)
                {
                    if (dic.ContainsKey("未发团散客预订单") && dic.ContainsKey("散客订单未处理") && dic.ContainsKey("散客订单预留待付款") && dic.ContainsKey("散客订单已确认"))
                    {
                        this.lblTourOrderFrist.Text = string.Format(
                            " 未发团散客预订单：<a class=\"ff0000\" href=\"/teamservice/fitorders.aspx?goTimeS={0}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                            DateTime.Now.ToShortDateString()) + dic["未发团散客预订单"].ToString() + "单</b></a>，";
                        this.lblTourOrderFrist.Text +=
                            string.Format(
                                "其中未处理<a class=\"ff0000\" href=\"/teamservice/fitorders.aspx?goTimeS={3}&status={0},{1},{2}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                                (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社待处理,
                                (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.组团社已阅,
                                (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商待处理,
                                DateTime.Now.ToShortDateString()) +
                            dic["散客订单未处理"].ToString() + "单</b></a>，";
                        this.lblTourOrderFrist.Text += string.Format(
                            "预留待付款<a class=\"ff0000\" href=\"/teamservice/fitorders.aspx?goTimeS={0}&status={1}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                            DateTime.Now.ToShortDateString(),
                            (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商预留)
                                                       + dic["散客订单预留待付款"].ToString() + "单</b></a>，";
                        this.lblTourOrderFrist.Text +=
                            string.Format(
                                "已确认<a href=\"/teamservice/fitorders.aspx?goTimeS={0}&status={1},{2}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                                DateTime.Now.ToShortDateString(),
                                (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.专线商已确定,
                                (int)EyouSoft.Model.NewTourStructure.PowderOrderStatus.结单) +
                            dic["散客订单已确认"].ToString() + "单</b></a>";
                    }

                    if (dic.ContainsKey("未发团团队预订单") && dic.ContainsKey("团队未确认") && dic.ContainsKey("团队已确认"))
                    {
                        this.lblTourOrderSecond.Text = string.Format(
                            "未发团团队预订单：<a class=\"ff0000\" href=\"/teamservice/teamorders.aspx?ref=0&goTimeS={0}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                            DateTime.Now.ToShortDateString()) + dic["未发团团队预订单"].ToString() + "单</b></a>，";
                        this.lblTourOrderSecond.Text +=
                            string.Format(
                                "未确认订单<a class=\"ff0000\" href=\"/teamservice/teamorders.aspx?ref=0&goTimeS={1}&status={0}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                                (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认,
                                DateTime.Now.ToShortDateString()) +
                            dic["团队未确认"].ToString() + "单</b></a>，";
                        this.lblTourOrderSecond.Text +=
                            string.Format(
                                "已确认<a href=\"/teamservice/teamorders.aspx?ref=0&goTimeS={2}&status={0},{1}\" onclick=\"topTab.open($(this).attr('href'),'我的散拼订单');return false;\"><b>",
                                (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.已确认,
                                (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.结单,
                                DateTime.Now.ToShortDateString()) +
                            dic["团队已确认"].ToString() + "单</b></a>";
                    }
                }
            }
            #endregion

            #region 专线提醒
            if (IsRouteAgency)
            {
                int ComingLeaveTourNumber = TourBll.GetComingExpireTourNumber();
                if (ComingLeaveTourNumber > 0)
                {
                    this.lblComingExpireToursCount.Text = "<img src='" + ImageServerPath + "/images/gantanhao.gif' />您当前共有<a href='/routeagency/notstartingteams.aspx' onclick=\"topTab.open($(this).attr('href'),'未出发团队');return false;\">" + ComingLeaveTourNumber + "个团</a>将于1周后到期，请及时更新计划，以方便组团社查询";
                }
                //获得专线下线路区域和团的数量

                IList<EyouSoft.Model.TourStructure.AreaStatInfo> TourByAreaCount = powderBll.GetCurrentUserTourByAreaStats();
                if (TourByAreaCount != null && TourByAreaCount.Count > 0)
                {
                    StringBuilder strTour = new StringBuilder();
                    foreach (EyouSoft.Model.TourStructure.AreaStatInfo AreaStaModel in TourByAreaCount)
                    {
                        strTour.Append("<a class=\"lan14\" href=\"/routeagency/scatteredfightplan.aspx?lineId=" + AreaStaModel.AreaId.ToString() + "\" onclick=\"topTab.open($(this).attr('href'),'我的散拼计划');return false;\">" + AreaStaModel.AreaName + "(" + AreaStaModel.Number.ToString() + ")</a>");

                    }
                    this.lblRoutePlanCount.Text = strTour.ToString();
                }

                dic = iorderBll.GetOrderBusinessCount(UserInfoModel.CompanyID);
                if (dic != null)
                {
                    //获得专线订单信息
                    if (dic.ContainsKey("有效散客订单") && dic.ContainsKey("散客订单未处理") && dic.ContainsKey("散客预留待付款") && dic.ContainsKey("散客订单已确认"))
                    {
                        this.lblRouteOrderFrist.Text = "有效散客订单：<a href=\"/routeagency/allfitorders.aspx\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'所有散拼订单');return false;\"><b>" + dic["有效散客订单"].ToString() + "单</b></a>,";
                        this.lblRouteOrderFrist.Text += "其中未处理<a href=\"/routeagency/allfitorders.aspx?statue=2\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'所有散拼订单');return false;\"><b>" + dic["散客订单未处理"].ToString() + "单</b></a>,";
                        this.lblRouteOrderFrist.Text += "预留待付款<a href=\"/routeagency/allfitorders.aspx?statue=3\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'所有散拼订单');return false;\"><b>" + dic["散客预留待付款"].ToString() + "单</b></a>，";
                        this.lblRouteOrderFrist.Text += "已确认:<a href=\"/routeagency/allfitorders.aspx?statue=6\" onclick=\"topTab.open($(this).attr('href'),'所有散拼订单');return false;\"><b>" + dic["散客订单已确认"].ToString() + "单</b></a>";
                    }

                    if (dic.ContainsKey("团队订单") && dic.ContainsKey("团队订单未处理") && dic.ContainsKey("团队订单已确认"))
                    {
                        this.lblRouteOrderSecond.Text = "有效团队订单：共有<a href=\"/teamservice/teamorders.aspx?routeSource=1\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["团队订单"].ToString() + "单</b></a>，";
                        this.lblRouteOrderSecond.Text += "其中未处理<a href=\"/teamservice/teamorders.aspx?routeSource=1&status=0\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["团队订单未处理"].ToString() + "单</b></a>，";
                        this.lblRouteOrderSecond.Text += " 已确认:<a href=\"/teamservice/teamorders.aspx?routeSource=1&status=1\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["团队订单已确认"] + "单</b></a>";
                    }

                    if (dic.ContainsKey("历史订单") && dic.ContainsKey("成人数") && dic.ContainsKey("儿童数"))
                    {
                        this.lblRouteOrderThird.Text = "历史订单：共计<a class=\"ff0000\"><b>" + dic["历史订单"].ToString() + "单</b></a>，人数" + dic["成人数"].ToString() + "大" + dic["儿童数"].ToString() + "小";
                    }
                }

                //获得专线商的访问数量
                this.lblBrowseUserCount.Text = TourBll.GetVisitedNumberByCompany(UserInfoModel.CompanyID).ToString();
            }
            #endregion

            #region 地接提醒
            if (IsLocalAgency)
            {
                //获得地接线路区域信息

                IList<EyouSoft.Model.TourStructure.AreaStatInfo> routeByAreaCount = routeBll.GetCurrentUserRouteByAreaStats();
                if (routeByAreaCount != null && routeByAreaCount.Count > 0)
                {
                    StringBuilder strTour = new StringBuilder();
                    foreach (EyouSoft.Model.TourStructure.AreaStatInfo AreaStaModel in routeByAreaCount)
                    {
                        strTour.Append("<a href='/routeagency/routemanage/routeview.aspx?routeSource=2' onclick=\"topTab.open($(this).attr('href'),'我的线路库',{isRefresh:false,data:{AreaId:" + AreaStaModel.AreaId + "}});return false;\" class='lan14'>" + AreaStaModel.AreaName + "(" + AreaStaModel.Number + ")</a><br />");

                    }
                    this.lblLocalRoute.Text = strTour.ToString();
                }

                dic = iorderBll.GetOrderGroundCount(UserInfoModel.CompanyID);
                if (dic != null)
                {
                    //获得地接团队订单信息
                    if (dic.ContainsKey("有效团队订单") && dic.ContainsKey("团队订单未处理") && dic.ContainsKey("团队订单已处理"))
                    {
                        this.lblLocalOrderCount.Text = "有效团队订单：共有<a href=\"/teamservice/teamorders.aspx?routeSource=2\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["有效团队订单"].ToString() + "单</b></a>，";
                        this.lblLocalOrderCount.Text += "其中未处理<a href=\"/teamservice/teamorders.aspx?routeSource=2&status=0\" class=\"ff0000\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["团队订单未处理"].ToString() + "单</b></a>，";
                        this.lblLocalOrderCount.Text += "已确认:<a href=\"/teamservice/teamorders.aspx?routeSource=2&status=1\" onclick=\"topTab.open($(this).attr('href'),'团队订单管理');return false;\"><b>" + dic["团队订单已处理"].ToString() + "单</b></a>";
                    }
                }
            }
            #endregion

            #region 景区提醒
            if (IsSightAgency)
            {
                //获得将过期景点门票
                this.lblScenicCountOver.Text = "您当前共有<a href=\"/ScenicManage/MyScenice.aspx\" onclick=\"topTab.open($(this).attr('href'),'我的景区');return false;\">" + EyouSoft.BLL.ScenicStructure.BScenicTickets.CreateInstance().GetExpireTickets(UserInfoModel.CompanyID).ToString() + "个景区门票</a>将于1周后到期，请及时更新计划，以方便旅行社查询";
                int recordCount = 0;
                IList<EyouSoft.Model.ScenicStructure.MScenicArea> scenicList = EyouSoft.BLL.ScenicStructure.BScenicArea.CreateInstance().GetListAndTickets(3, 1, ref recordCount, null);

                if (scenicList != null && scenicList.Count > 0)
                {
                    string sbScenic = "";
                    for (int i = 0; i < scenicList.Count; i++)
                    {
                        sbScenic += "<a class=\"lan14\" href=\"/ScenicManage/MyScenice.aspx\" onclick=\"topTab.open($(this).attr('href'),'我的景区');return false;\">" + scenicList[i].ScenicName + "</a>&nbsp;&nbsp;";
                    }
                    this.lblScenicCount.Text = sbScenic;
                }
            }
            #endregion
            #endregion
        }
        #endregion

        #region 初始化运营广告
        //private void InitAdvInfo()
        //{
        //    IList<EyouSoft.Model.SystemStructure.Affiche> list = EyouSoft.BLL.SystemStructure.Affiche.CreateInstance().GetTopList(10, EyouSoft.Model.SystemStructure.AfficheType.旅行社后台广告);
        //    if (list != null && list.Count > 0)
        //    {
        //        this.dlAdv.DataSource = list;
        //        this.dlAdv.DataBind();
        //    }
        //    list = null;
        //}
        #endregion

        private void InitExchangeList()
        {
            //IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(10, null, 0);
            //if (list != null && list.Count > 0)
            //{
            //    this.dlTopicList.DataSource = list;
            //    this.dlTopicList.DataBind();
            //}
            //list = null;
        }

        #region 公告
        private void PageMsgInit()
        {
            EyouSoft.IBLL.NewsStructure.INewsBll newsBll = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance();
            EyouSoft.Model.NewsStructure.NewsCategory? cate = EyouSoft.Model.NewsStructure.NewsCategory.用户后台公告;
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> newsList = newsBll.GetList(6,
                                                                                       cate, null,
                                                                                       null, null);
            if (newsList != null && newsList.Count > 0)
            {
                this.rptGG.DataSource = newsList;
                this.rptGG.DataBind();
            }
            else
            {
                Label lb = new Label();
                lb.Text = "暂无公告";
                this.rptGG.Controls.Add(lb);
            }
        }
        #endregion

        #region 同业资讯
        protected string GetOrationByUser()
        {
            StringBuilder sb = new StringBuilder();
            EyouSoft.IBLL.NewsStructure.IPeerNews bll = EyouSoft.BLL.NewsStructure.BPeerNews.CreateInstance();
            int count = 0;
            EyouSoft.Model.NewsStructure.MQueryPeerNews searchModel = new EyouSoft.Model.NewsStructure.MQueryPeerNews();
            //searchModel.CompanyId = UserInfoModel.CompanyID;
            searchModel.KeyWord = "";
            searchModel.OrderIndex = 4;
            searchModel.TypeId = null;
            searchModel.IsShowHideNew = false;
            IList<EyouSoft.Model.NewsStructure.MPeerNews> list = bll.GetGetPeerNewsList(6, 1, ref count, searchModel);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("<li>");
                    switch (list[i].TypeId)
                    {
                        case EyouSoft.Model.NewsStructure.PeerNewType.活动资讯:
                            sb.Append("<span class=\"hailan\"><strong>[活动资讯]</strong></span>");
                            break;
                        case EyouSoft.Model.NewsStructure.PeerNewType.旅行动态:
                            sb.Append("<span class=\"chengse\"><strong>[旅行动态]</strong></span>");
                            break;
                        case EyouSoft.Model.NewsStructure.PeerNewType.特价促销:
                            sb.Append("<span class=\"caolv\"><strong>[特价促销]</strong></span>");
                            break;
                        case EyouSoft.Model.NewsStructure.PeerNewType.同业资料:
                            sb.Append("<span class=\"shenlan\"><strong>[同业资料]</strong></span>");
                            break;
                    }

                    //sb.Append("<a href='javascript:void(0)' onclick='return topTab.open('/TongYeInfo/InfoShow.aspx?infoId='" + list[i].NewId+",'我的同业资讯'); " + Utils.GetText(list[i].Title, 15, true) + "</a></li>");
                    sb.AppendFormat("<a href='javascript:void(0)' onclick=\"return topTab.open('/TongYeInfo/InfoShow.aspx?infoId={0}','查看同业资讯');\">{1}</a></li>", list[i].NewId, Utils.GetText(list[i].Title, 15, true));
                }
            }
            else
            {
                sb.Append("暂无资讯");
            }

            return sb.ToString();
        }
        #endregion
    }
}
