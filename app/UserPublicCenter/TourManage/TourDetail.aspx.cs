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
using EyouSoft.Common.Function;
using EyouSoft.Model.NewTourStructure;
namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// 团队详细信息页
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    /// 修改人：刘飞  
    public partial class TourDetail : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 当前团队线路区域类型
        /// </summary>
        protected int RouteType = 1;
        /// <summary>
        /// 初始化日历当前月
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        /// <summary>
        /// 初始化日历下一个月
        /// </summary>
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected string CompanyMQ = string.Empty;//公司MQ
        protected string RouteImgHref = string.Empty;
        protected bool IsRoute = false;//非专线商登录
        protected bool IsTour = false;//非组团社登录
        protected bool IsVisa = false;//签证
        protected bool IsDijieRoute = false;//是否是地接线路
        protected string IsTeam = string.Empty;
        protected bool IsPlan = false;//线路是否有团
        /// <summary>
        /// 线路编号
        /// </summary>
        protected string RouteId = string.Empty;
        /// <summary>
        /// 线路数字编号
        /// </summary>
        private long _intRouteId = 0;
        IList<EyouSoft.Model.NewTourStructure.MPowderList> Powderlist = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            #region 页面初始化控件赋值
            this.UCRightList1.IsSearch = true;
            this.UCRightList1.IsToolbar = true;
            this.UCRightList1.CityID = CityId;
            this.UCRightList1.IsPinpai = false;
            this.CityAndMenu1.HeadMenuIndex = 2;
            #endregion

            RouteId = Utils.GetQueryStringValue("RouteID");
            if (!IsPostBack)
            {
                if (RouteId != "")
                {
                    GetTourInfo();
                }
                else
                {

                    _intRouteId = StringValidate.IsDecimal(Utils.GetQueryStringValue("intRouteId"))
                                      ? long.Parse(Utils.GetQueryStringValue("intRouteId"))
                                      : 0;
                    if (_intRouteId > 0)
                    {
                        GetTourInfo();
                    }
                    else
                    {
                        Utils.ShowError("暂无线路信息!", "Tour");
                        return;
                    }
                }
                if (IsLogin)
                {
                    if (this.SiteUserInfo != null && !string.IsNullOrEmpty(this.SiteUserInfo.CompanyID))
                    {
                        EyouSoft.Model.CompanyStructure.CompanyDetailInfo Userinfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.SiteUserInfo.CompanyID);

                        if (Userinfo != null && Userinfo.CompanyRole != null)
                        {
                            if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                            {
                                this.txtContactName.Text = Userinfo.ContactInfo.ContactName;
                                this.txtContactTel.Text = Userinfo.ContactInfo.Tel;
                                IsTour = true;
                            }
                            if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                            {
                                IsRoute = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取快速登录链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }
        /// <summary>
        /// 初始化线路信息
        /// </summary>
        protected void GetTourInfo()
        {
            MRoute model;
            model = string.IsNullOrEmpty(RouteId)
                        ? EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(_intRouteId)
                        : EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetModel(RouteId);

            if (model != null)
            {
                //线路字符编号为空时 赋值 避免前台代码错误
                RouteId = string.IsNullOrEmpty(RouteId) ? model.RouteId : RouteId;
                //线路名称
                this.lbRouteName.Text = GetRecommendTypeImgSrc((int)model.RecommendType) + model.RouteName;
                this.lbdays.Text = model.Day.ToString();

                if (model.Late > 0)
                    this.lbnight.Text = string.Format("<span class=\"hong123\">{0}</span>晚", model.Late);
                else
                    this.lbnight.Text = string.Empty;

                GetCompanyinfo(model.Publishers);

                if (model.RouteSource == RouteSource.地接社添加)
                {
                    this.IsDijieRoute = true;
                    this.lbPrice_c.Text = "电询";
                    this.lbContactName.Visible = false;
                    this.lbContactTel.Visible = false;
                    this.lbadultCount.Visible = false;
                    this.lbchildCount.Visible = false;
                    this.tourDate.Visible = false;
                    this.dropTravelAllTourList.Visible = false;
                }

                if (model.RouteSource == RouteSource.专线商添加)
                {
                    Powderlist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteId).OrderBy(m => m.LeaveDate).ToList();
                    //团队信息
                    if (Powderlist != null && Powderlist.Count > 0)
                    {
                        this.tourDate.Visible = true;
                        IsPlan = true;
                        string strTmp = string.Empty;
                        foreach (EyouSoft.Model.NewTourStructure.MPowderList child in Powderlist)
                        {
                            if (child.PowderTourStatus == EyouSoft.Model.NewTourStructure.PowderTourStatus.收客 ||
                                child.PowderTourStatus == EyouSoft.Model.NewTourStructure.PowderTourStatus.成团 ||
                                child.PowderTourStatus == EyouSoft.Model.NewTourStructure.PowderTourStatus.客满)
                            {
                                strTmp = GetLeaveInfo(child.LeaveDate);
                                if (child.RetailAdultPrice > 0)
                                    strTmp += "  成人价" + child.RetailAdultPrice.ToString("F0");
                                if (child.RetailChildrenPrice > 0)
                                    strTmp += "  儿童价" + child.RetailChildrenPrice.ToString("F0");
                                if (child.MarketPrice > 0)
                                    strTmp += "  单房差" + child.MarketPrice.ToString("F0");
                                ListItem list = new ListItem(strTmp,
                                                             child.TourId + "|" + child.RetailAdultPrice.ToString("F0"));

                                this.dropTravelAllTourList.Items.Add(list);
                            }
                        }
                        //给日历赋子团信息
                        SetCalendar();
                    }
                    else
                    {
                        this.lbPrice_c.Text = "电询";
                        this.lbContactName.Visible = false;
                        this.lbContactTel.Visible = false;
                        this.lbadultCount.Visible = false;
                        this.lbchildCount.Visible = false;
                        this.tourDate.Visible = false;
                        this.dropTravelAllTourList.Visible = false;
                        this.IsPlan = false;
                    }
                }
                //提前几天报名
                this.lbtqdays.Text = model.AdvanceDayRegistration.ToString();
                //去程:交通+出发地
                this.lbgo.Text = "去程:交通-" + model.StartTraffic + " 出发地-" + model.StartCityName;
                this.lbBack.Text = "返程:交通-" + model.EndTraffic + " 目的地-" + model.EndCityName;

                this.lbsaleinfo.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.VendorsNotes);
                RouteImgHref = string.IsNullOrEmpty(model.RouteImg) ? Domain.ServerComponents + "/images/NoPicture.jpg" : EyouSoft.Common.Domain.FileSystem + model.RouteImg;
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().UpdateClick(RouteId);
                string company = model.Publishers;
                UCRightList1.TourAreaId = model.AreaId.ToString();
                IList<MThemeControl> themelist = model.Themes;
                string theme = string.Empty;//线路主题
                if (themelist != null && themelist.Count > 0)
                {
                    for (int i = 0; i < themelist.Count; i++)
                    {
                        theme += themelist[i].ThemeName + " ";
                    }
                    this.lbtheme.Text = theme;
                }
                if (!string.IsNullOrEmpty(model.Characteristic))
                {
                    this.lbCharacteristic.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(model.Characteristic);
                }
                else
                {
                    this.divCharacteristic.Visible = false;
                }
                if (model.IsCertain)
                {
                    IsTeam = "<li class=\"tuan\">无须成团,铁定发团!</li>";
                }
                //设置keywords、title、dec
                this.Page.Title = string.Format("{0}{1}_同业114",
                                                string.IsNullOrEmpty(model.StartCityName)
                                                    ? string.Empty
                                                    : model.StartCityName + "出发", model.RouteName);
                AddMetaTag("description", string.Format(PageTitle.RouteDetails_Des, CityModel.CityName, model.AreaName, model.RouteName));
                AddMetaTag("keywords", string.Format(PageTitle.RouteDetails_Keywords, model.AreaName));

                //线路区域类型
                RouteType = Convert.ToInt32(model.RouteType);
                if (model.RouteType == EyouSoft.Model.SystemStructure.AreaType.国际线)
                {
                    //成人定金
                    if (model.AdultPrice <= 0)
                        lb_d_Price.Text = model.AdultPrice == -1 ? "电询" : "无需定金";
                    else
                        lb_d_Price.Text = model.AdultPrice.ToString("F0") + "元";

                    //儿童定金
                    if (model.ChildrenPrice <= 0)
                        lbPrice_e.Text = model.ChildrenPrice == -1 ? "电询" : "无需定金";
                    else
                        lbPrice_e.Text = model.ChildrenPrice.ToString("F0") + "元";

                    if (model.BrowseCountrys != null && model.BrowseCountrys.Count > 0)
                    {
                        //浏览国家
                        string BrowseCountry = string.Empty;
                        //签证
                        string Visa = string.Empty;
                        string qianzheng = string.Empty;
                        for (int i = 0; i < model.BrowseCountrys.Count; i++)
                        {
                            BrowseCountry += model.BrowseCountrys[i].CountryName + " ";
                            if (model.BrowseCountrys[i].IsVisa)
                            {
                                IsVisa = true;
                                Visa += model.BrowseCountrys[i].CountryName + "签证 ";
                            }
                        }
                        this.lbarea.Text = BrowseCountry;
                        this.lbqianzheng.Text = Visa;

                    }
                    //免签
                    if (!IsVisa && model.IsNotVisa)
                    {
                        IsVisa = true;
                        lbqianzheng.Text = "免签";
                    }
                }
                else
                {
                    if (model.BrowseCitys != null && model.BrowseCitys.Count > 0)
                    {
                        string BrowseCity = string.Empty;
                        for (int i = 0; i < model.BrowseCitys.Count; i++)
                        {
                            if (string.IsNullOrEmpty(model.BrowseCitys[i].CountyName))
                            {
                                BrowseCity += model.BrowseCitys[i].CityName + " ";
                            }
                            else
                            {
                                BrowseCity += model.BrowseCitys[i].CountyName + " ";
                            }
                        }
                        lbarea.Text = BrowseCity;
                    }
                }
                //快速发布 
                if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
                {
                    if (!string.IsNullOrEmpty(model.FastPlan))
                    {
                        this.LiteralTourPlan.Text = model.FastPlan;
                    }
                    else
                    {
                        this.divTourPlanHead.Visible = false;
                    }
                }
                else
                {
                    #region 标准行程
                    IList<EyouSoft.Model.NewTourStructure.MStandardPlan> PlanInfo = model.StandardPlans.OrderBy(m => m.PlanDay).ToList();
                    if (PlanInfo != null && PlanInfo.Count > 0)
                    {
                        StringBuilder strAllDateInfo = new StringBuilder();
                        string Dinner = string.Empty;//包餐(早、中、晚)
                        foreach (EyouSoft.Model.NewTourStructure.MStandardPlan Plan in PlanInfo)
                        {
                            if (Plan.Early) { Dinner += "早、"; }
                            if (Plan.Center) { Dinner += "中、"; }
                            if (Plan.Late) { Dinner += "晚、"; }
                            strAllDateInfo.Append("<li>");
                            strAllDateInfo.AppendFormat("<div class=\"daty\">第{0}天</div> <div class=\"zhu\"><span class=\"STYLE3\">住：</span>{1}</div><div class=\"can\"><span class=\"STYLE4\">餐：</span>{2}</div>", Plan.PlanDay, Plan.House, string.IsNullOrEmpty(Dinner) ? "" : Dinner.Remove(Dinner.Length - 1));
                            strAllDateInfo.AppendFormat("<div class=\"xing\"> <span class=\"STYLE5\">行：</span>{0}</div></li>", Plan.PlanContent);

                            Dinner = string.Empty;
                        }
                        this.LiteralTourPlan.Text = strAllDateInfo.ToString();
                    }
                    else
                    {
                        this.divTourPlanHead.Visible = false;
                    }
                    PlanInfo = null;

                    #endregion
                }

                int _index = 1;
                EyouSoft.Model.NewTourStructure.MServiceStandard ServiceModel = model.ServiceStandard;

                if (ServiceModel != null)
                {
                    #region 包含项目
                    StringBuilder tmpString = new StringBuilder();
                    if (string.IsNullOrEmpty(ServiceModel.ResideContent + ServiceModel.DinnerContent + ServiceModel.SightContent + ServiceModel.CarContent + ServiceModel.GuideContent + ServiceModel.TrafficContent + ServiceModel.IncludeOtherContent))
                    {
                        //tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".散客:" + "</span>");
                        tmpString.Append(EyouSoft.Common.Function.StringValidate.TextToHtml(model.FitQuotation) + "<br>");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ServiceModel.ResideContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".住宿:" + "</span>");
                            tmpString.Append(ServiceModel.ResideContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.DinnerContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".用餐:" + "</span>");
                            tmpString.Append(ServiceModel.DinnerContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.SightContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".景点:" + "</span>");
                            tmpString.Append(ServiceModel.SightContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.CarContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".用车:" + "</span>");
                            tmpString.Append(ServiceModel.CarContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.GuideContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".导游:" + "</span>");
                            tmpString.Append(ServiceModel.GuideContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.TrafficContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".往返交通:" + "</span>");
                            tmpString.Append(ServiceModel.TrafficContent + "<br>");
                        }
                        if (!string.IsNullOrEmpty(ServiceModel.IncludeOtherContent))
                        {
                            tmpString.Append("<span class=\"STYLE2\">" + (_index++) + ".其它:" + "</span>");
                            tmpString.Append(ServiceModel.IncludeOtherContent + "<br>");
                        }
                    }
                    lblServiceStandard.Text = tmpString.ToString();

                    if (ServiceModel == null)
                    {
                        this.divServiceStandard.Visible = false;
                    }
                    #endregion

                    #region 不包含项目
                    if (!string.IsNullOrEmpty(ServiceModel.NotContainService))
                    {
                        this.lblNotContainService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.NotContainService);
                    }
                    else
                    {
                        this.divNotContainService.Visible = false;
                    }
                    #endregion

                    #region 儿童及其他安排
                    if (!string.IsNullOrEmpty(ServiceModel.ChildrenInfo))
                    {
                        this.LbChildandplan.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.ChildrenInfo);
                    }
                    else
                    {
                        this.divChildandplan.Visible = false;
                    }
                    #endregion

                    #region 自费项目
                    if (!string.IsNullOrEmpty(ServiceModel.ExpenseItem))
                    {
                        this.lbSelfService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.ExpenseItem);
                    }
                    else
                    {
                        this.divSelfService.Visible = false;
                    }
                    #endregion

                    #region 赠送项目
                    if (!string.IsNullOrEmpty(ServiceModel.GiftInfo))
                    {
                        this.lbZsService.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.GiftInfo);
                    }
                    else
                    {
                        this.divZsService.Visible = false;
                    }
                    #endregion

                    #region 购物点
                    if (!string.IsNullOrEmpty(ServiceModel.ShoppingInfo))
                    {
                        this.lbShopping.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.ShoppingInfo);
                    }
                    else
                    {
                        this.divShopping.Visible = false;
                    }
                    #endregion

                    #region 备注信息
                    if (!string.IsNullOrEmpty(ServiceModel.Notes))
                    {
                        this.lbRemarks.Text = EyouSoft.Common.Function.StringValidate.TextToHtml(ServiceModel.Notes);
                    }
                    else
                    {
                        this.divRemarks.Visible = false;
                    }
                    #endregion
                }
                else
                {
                    this.divServiceStandardInfo.Visible = false;
                }
                ServiceModel = null;
            }
            else
            {
                Utils.ShowError("暂无该线路信息!", "Tour");
                return;
            }
            model = null;
        }
        /// <summary>
        /// 给日历赋值
        /// </summary>
        private void SetCalendar()
        {
            IList<EyouSoft.Model.NewTourStructure.MPowderList> CalendarChild = this.filterChildren(Powderlist);
            IsoDateTimeConverter isoDate = new IsoDateTimeConverter();
            isoDate.DateTimeFormat = "yyyy-MM-dd";
            string scripts = string.Format("QGD.config.Childrens={0};", JsonConvert.SerializeObject(CalendarChild, isoDate));
            EyouSoft.Common.Function.MessageBox.ResponseScript(this.Page, scripts);
        }

        /// <summary>
        /// 获取线路所属公司信息
        /// </summary>
        /// <param name="companyid"></param>
        private void GetCompanyinfo(string companyid)
        {
            EyouSoft.Model.CompanyStructure.CompanyDetailInfo companyinfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(companyid);
            if (companyinfo != null)
            {
                if (companyinfo.ContactInfo != null && companyinfo.ContactInfo.MQ != null)
                {
                    CompanyMQ = companyinfo.ContactInfo.MQ;
                }
            }

            //如果品牌名称为空的话，取公司简称，如果再为空则截取公司名称前6个字
            if (companyinfo != null)
            {
                if (string.IsNullOrEmpty(companyinfo.CompanyBrand))
                {
                    if (string.IsNullOrEmpty(companyinfo.Introduction))
                    {
                        if (!string.IsNullOrEmpty(companyinfo.CompanyName))
                        {
                            lbpinpai.Text = Utils.GetCompanyLevImg(companyinfo.CompanyLev) + Utils.GetText2(companyinfo.CompanyName, 6, false);
                        }
                    }
                    else
                    {
                        lbpinpai.Text = Utils.GetCompanyLevImg(companyinfo.CompanyLev) + companyinfo.Introduction;
                    }
                }
                else
                {
                    lbpinpai.Text = Utils.GetCompanyLevImg(companyinfo.CompanyLev) + companyinfo.CompanyBrand;
                }
            }
            else
            {
                lbpinpai.Text = "无品牌名称";
            }
        }
        #region   获取公司名称
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetCompanyName(string id)
        {
            return EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(id);
        }
        #endregion

        /// <summary>
        /// 格式化出团日期
        /// </summary>
        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
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
                item.TourNo = Model.TourNo;
                item.TourId = Model.TourId;
                item.LeaveDate = Model.LeaveDate;
                item.RetailAdultPrice = Model.RetailAdultPrice;
                item.SaveNum = Model.SaveNum;
                item.MoreThan = Model.MoreThan;
                item.IsLimit = Model.IsLimit;
                item.TourNum = Model.TourNum;
                item.PowderTourStatus = Model.PowderTourStatus;
                NewChildrens.Add(item);
                item = null;
            }
            return NewChildrens;
        }
        /// <summary>
        /// 获取线路推荐类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetRecommendTypeImgSrc(int type)
        {
            string LevImg = string.Empty;
            switch (type)
            {
                case 1://无
                    LevImg = "";
                    break;
                case 2://推荐
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_30.gif\"/>";
                    break;
                case 3://特价
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_35.gif\"/>";
                    break;
                case 4://豪华
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_39.gif\"/>";
                    break;
                case 5://热门
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_42.gif\"/>";
                    break;
                case 6://新品
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_44.gif\"/>";
                    break;
                case 7://纯玩
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_46.gif\"/>";
                    break;
                case 8://经典
                    LevImg = "<img src=\"" + Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_48.gif\"/>";
                    break;
            }
            return LevImg;
        }

    }
}
