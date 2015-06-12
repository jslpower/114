using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using UserPublicCenter.MasterPage;
using EyouSoft.Model.NewTourStructure;
using Newtonsoft.Json.Converters;

namespace UserPublicCenter.TourManage
{
    /// <summary>
    /// ［线路］团队列表（从线路栏目查看详细信息）
    /// 开发人：刘玉灵  时间：2010-6-24
    /// </summary>
    public partial class TourList : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 线路区域ID
        /// </summary>
        protected int TourAreaId = 0;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int PageSize = 10;
        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;
        /// <summary>
        /// 日历当前月
        /// </summary>
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        /// <summary>
        /// 首页链接
        /// </summary>
        protected string strAllEmpty = "";
        //专线名称
        public string strRourListName = "所有线路";
        //日期搜索
        public bool IsDate = false;
        //网店地址(普通/高级)
        protected string RouteTypeName = string.Empty;//线路区域类型(国内\国际\周边)
        private IList<EyouSoft.Model.NewTourStructure.MPowderList> Powderlist;
        protected bool IsRoute = false;//非专线商登录
        protected bool IsTour = false;//非组团社登录
        protected void Page_Load(object sender, EventArgs e)
        {
            TourAreaId = EyouSoft.Common.Function.StringValidate.GetIntValue(Request.QueryString["TourAreaId"]);
            if (TourAreaId != 0 || Request.QueryString["RouteType"] != null)
            {
                EyouSoft.Model.SystemStructure.SysArea SysAreamodel = EyouSoft.BLL.SystemStructure.SysArea.CreateInstance().GetSysAreaModel(TourAreaId);
                int routetype = TourAreaId == 0 ? 3 : (int)SysAreamodel.RouteType;
                if (Request.QueryString["RouteType"] != null)
                {
                    routetype = Request.QueryString["RouteType"] == null ? 3 : Utils.GetInt(Request.QueryString["RouteType"].ToString());
                }
                switch (routetype)
                {
                    case 0:
                        RouteTypeName = "国内";
                        break;
                    case 1:
                        RouteTypeName = "国际";
                        break;
                    case 2:
                        RouteTypeName = "周边";
                        break;
                    default:
                        RouteTypeName = "";
                        break;
                }
                if (SysAreamodel != null)
                {
                    strRourListName = SysAreamodel.AreaName;
                }
                else
                {
                    strRourListName = RouteTypeName + "线路";
                }

            }
            UCRightList1.IsPinpai = true;
            UCRightList1.IsToolbar = false;
            UCRightList1.TourAreaId = TourAreaId.ToString();
            UCRightList1.CityID = CityId;
            this.TourSearchKeys1.IsDefault = false;
            //TourAreaId  KeyWord  Days   ThemeName  StartDate   EndDate   Price  City
            string startDate = EyouSoft.Common.Utils.GetQueryStringValue("StartDate");
            string endDate = EyouSoft.Common.Utils.GetQueryStringValue("EndDate");
            string keyword = EyouSoft.Common.Utils.GetQueryStringValue("keyWord");
            string Days = EyouSoft.Common.Utils.GetQueryStringValue("Days");
            string Price = EyouSoft.Common.Utils.GetQueryStringValue("Price");
            string City = EyouSoft.Common.Utils.GetQueryStringValue("City");
            if (startDate != "" || endDate != "")
            {
                IsDate = true;
            }

            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!Page.IsPostBack)
            {
                NewPublicCenter master = (this.Master as NewPublicCenter);
                master.HeadMenuIndex = 2;
                if (!string.IsNullOrEmpty(keyword) && keyword != null)
                {
                    master.SearchKeyWord = keyword;                    
                }               
                this.TourSearchKeys1.IsDefault = false;
                BindCompanyAndTour();
                GetUserRole();
            }

            string tourAreaName = this.hidRourName.Value;
            this.Page.Title = string.Format(PageTitle.RouteList_Title, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName);
            AddMetaTag("description", string.Format(PageTitle.RouteList_Des, CityModel.CityName, tourAreaName, tourAreaName, tourAreaName));
            AddMetaTag("keywords", string.Format(PageTitle.RouteList_Keywords, tourAreaName, tourAreaName, tourAreaName));

        }
        /// <summary>
        /// 获取当前登录用户的身份
        /// </summary>
        protected void GetUserRole()
        {
            if (IsLogin)
            {
                if (this.SiteUserInfo != null && !string.IsNullOrEmpty(this.SiteUserInfo.CompanyID))
                {
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo Userinfo = new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.SiteUserInfo.CompanyID);

                    if (Userinfo != null && Userinfo.CompanyRole != null)
                    {
                        if (Userinfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
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

        /// <summary>
        /// 绑定线路区域下的线路信息
        /// </summary>
        protected void BindCompanyAndTour()
        {
            if (IsLogin)
            {
 
            }
            int recordCount = 0;
            string SearchType = Request.QueryString["SearchType"];
            int intSearchId = 0;
            string SearchId = Request.QueryString["Id"];

            intSearchId = EyouSoft.Common.Function.StringValidate.GetIntValue(SearchId);

            StringBuilder strEmpty = new StringBuilder();
            string ErrorInfo = "";
            EyouSoft.Model.NewTourStructure.MRouteSearch routeSearch = new EyouSoft.Model.NewTourStructure.MRouteSearch();

            routeSearch.AreaId = TourAreaId;
            routeSearch.CityId = CityId;
            routeSearch.RouteStatus = (RouteStatus)1;
            if (Request.QueryString["RouteType"] != null)
            {
                routeSearch.RouteType = (EyouSoft.Model.SystemStructure.AreaType)Utils.GetInt(Request.QueryString["RouteType"].ToString());
            }
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> Tour = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();


            IList<EyouSoft.Model.NewTourStructure.MRoute> TourList = new List<EyouSoft.Model.NewTourStructure.MRoute>();
            if (!string.IsNullOrEmpty(SearchType))
            {
                //按主题类型
                if (SearchType.Equals("Theme", StringComparison.OrdinalIgnoreCase))
                {
                    routeSearch.ThemeId = intSearchId;
                }
                //按价格区间
                else if (SearchType.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    int? MinPrice = null;
                    int? MaxPrice = null;
                    string PriceType = "之间";
                    switch (SearchId)
                    {
                        case "1":
                            MinPrice = 1;
                            MaxPrice = 100;
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._100元以下;
                            break;
                        case "2":
                            MinPrice = 100;
                            MaxPrice = 300;
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._100到300元;
                            break;
                        case "3":
                            MinPrice = 300;
                            MaxPrice = 1000;
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._300到1000元;
                            break;
                        case "4":
                            MinPrice = 1000;
                            MaxPrice = 3000;
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._1000到3000元;
                            break;
                        case "5":
                            MinPrice = 3000;
                            MaxPrice = 10000;
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._3000到10000元;
                            break;
                        case "6":
                            MinPrice = 10000;
                            PriceType = "以上";
                            routeSearch.Price = EyouSoft.Model.NewTourStructure.PublicCenterPrice._10000以上;
                            break;
                    }
                    if (MinPrice != null || MaxPrice != null)
                    {
                        StringBuilder strPrice = new StringBuilder();
                        strPrice.AppendFormat("“价格在{0}{3}{1}{2}”", MinPrice == null ? "" : MinPrice.ToString(), MaxPrice == null ? "" : MaxPrice.ToString(), PriceType, PriceType == "之间" ? "-" : "");
                        ErrorInfo += strPrice.ToString();
                        strPrice = null;
                    }
                }
                //按行程天数
                else if (SearchType.Equals("Day", StringComparison.OrdinalIgnoreCase))
                {
                    routeSearch.RouteDay = (EyouSoft.Model.NewTourStructure.PublicCenterRouteDay)intSearchId;
                    if (intSearchId > 0)
                    {
                        StringBuilder strDays = new StringBuilder();
                        strDays.AppendFormat("“{0}”", intSearchId == 8 ? "7日游及以上" : intSearchId.ToString() + "日游");
                        ErrorInfo += strDays.ToString();
                        strDays = null;
                    }
                }
                //按月份搜索
                else if (SearchType.Equals("Month", StringComparison.OrdinalIgnoreCase))
                {
                    routeSearch.LeaveMonth = 3;
                    for (int i = 0; i < 6; i++)
                    {
                        if (Utils.GetInt(SearchId) == i)
                        {
                            routeSearch.Month =DateTime.Now.AddMonths(i).Month;
                        }
                    }
                    
                    routeSearch.Year = DateTime.Now.Year;
                }
                else if (SearchType.Equals("More", StringComparison.OrdinalIgnoreCase))
                {
                    string keyword = Utils.InputText(Utils.GetQueryStringValue("keyWord"));
                    string Days = EyouSoft.Common.Utils.GetQueryStringValue("Days");
                    string Price = EyouSoft.Common.Utils.GetQueryStringValue("Price");
                    string City = Utils.InputText(Utils.GetQueryStringValue("City"));
                    string ThemeId = Utils.GetQueryStringValue("ThemeName");
                    DateTime? StartDate = null;
                    string strStartDate = Utils.InputText(Request.QueryString["StartDate"]);

                    if (!string.IsNullOrEmpty(strStartDate) && EyouSoft.Common.Function.StringValidate.IsDateTime(strStartDate))
                    {
                        StartDate = Convert.ToDateTime(strStartDate);
                    }
                    DateTime? EndDate = null;
                    string strEndDate = Utils.InputText(Request.QueryString["EndDate"]);
                    if (!string.IsNullOrEmpty(strEndDate) && EyouSoft.Common.Function.StringValidate.IsDateTime(strEndDate))
                    {
                        EndDate = Convert.ToDateTime(strEndDate);
                    }
                    routeSearch.EndDate = Utils.GetDateTime(EndDate.ToString());
                    routeSearch.StartCityName = City;
                    if (!string.IsNullOrEmpty(City))
                    {
                        routeSearch.StartCity = 0;
                    }
                    routeSearch.StartDate = Utils.GetDateTime(StartDate.ToString());
                    routeSearch.RouteKey = keyword;
                    routeSearch.Price = (PublicCenterPrice)Utils.GetInt(Price);
                    routeSearch.RouteDay = (PublicCenterRouteDay)Utils.GetInt(Days);
                    routeSearch.ThemeId = Utils.GetInt(ThemeId);
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        ErrorInfo += "“" + keyword + "”";
                    }
                    if (StartDate != null)
                    {
                        ErrorInfo += "“" + Convert.ToDateTime(StartDate).ToShortDateString() + "”";

                    }
                    if (EndDate != null)
                    {
                        ErrorInfo += "“" + Convert.ToDateTime(EndDate).ToShortDateString() + "”";

                    }
                }
            }
            TourList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetPublicCenterList(PageSize, PageIndex, ref recordCount, routeSearch);
            //EyouSoft.Model.NewTourStructure.MPowderSearch ms=new MPowderSearch();
            //IList<MPowderList> plist = new List<MPowderList>();
            // plist=EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(PageSize, PageIndex, ref recordCount, ms);
            
            if (TourList != null && TourList.Count > 0)
            {
                this.ExporPageInfoSelect1.intPageSize = PageSize;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
                this.ExporPageInfoSelect1.CurrencyPage = PageIndex;

                this.ExporPageInfoSelect2.intPageSize = PageSize;
                this.ExporPageInfoSelect2.intRecordCount = recordCount;
                this.ExporPageInfoSelect2.CurrencyPage = PageIndex;


                if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
                {
                    //是否重写 分页的链接
                    this.ExporPageInfoSelect1.IsUrlRewrite = true;
                    //设置需要替换的值
                    this.ExporPageInfoSelect1.Placeholder = "#PageIndex#";
                    //获得线路的url 赋值给分页控件
                    this.ExporPageInfoSelect1.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
                    this.ExporPageInfoSelect2.IsUrlRewrite = true;
                    //设置需要替换的值
                    this.ExporPageInfoSelect2.Placeholder = "#PageIndex#";
                    //获得线路的url 赋值给分页控件
                    this.ExporPageInfoSelect2.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
                }
                else
                {
                    this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                    this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                    this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
                }

                // 推荐类型(RecommendType)，公司等级，公司名称(CompanyBrand)  StartCity
                this.repTourList.DataSource = TourList;
                this.repTourList.DataBind();
            }
            else
            {
                strEmpty.Append("<div class=\"noresult\" >");
                if (!string.IsNullOrEmpty(SearchType))
                {
                    //按主题类型
                    if (SearchType.Equals("Theme", StringComparison.OrdinalIgnoreCase))
                    {
                        string RouteTypeName = EyouSoft.BLL.SystemStructure.SysField.CreateInstance().GetFieldNameById(intSearchId, EyouSoft.Model.SystemStructure.SysFieldType.线路主题);
                        if (!string.IsNullOrEmpty(RouteTypeName))
                        {
                            ErrorInfo += "“" + RouteTypeName + "”";

                        }
                    }
                }
                strEmpty.Append("<div class=\"p1\"><h3><span class='timg'><img src='" + ImageServerUrl + "/images/UserPublicCenter/paoqian.gif' /></span><span>抱歉，" + ErrorInfo + "没有找到相关的结果!</span></h3></div>");
                strEmpty.Append("<div class=\"p2\"><a href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/routeagency/routemanage/rmdefault.aspx\"><img src=\"" + ImageServerUrl + "/images/new2011/xianlu/nobg_07.gif\" alt=\"我要添加产品\"/></a>");
                strEmpty.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + EyouSoft.Common.URLREWRITE.Tour.GetOldTourUrl(TourAreaId.ToString(), CityId) + "\"><img src=\"" + ImageServerUrl + "/images/new2011/xianlu/nobg_09.gif\" alt=\"查看往期产品\"/></a></div>");
                strEmpty.Append("<div class=\"p3\"><b>建议你：</b><br />");
                strEmpty.Append("·适当删减或更改关键词试试<br /> ·看看输入的文字是否有错别字<br />·如关键词中含有城市名，去掉城市名试试 <br />·换个分类搜索试试");
                strEmpty.Append("</div>");
                strAllEmpty = strEmpty.ToString();
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
                this.repTourList.EmptyText = strAllEmpty;

            }
            TourList = null;

        }


        /// <summary>
        /// 获取公司网店地址
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        protected string GetCompanyShopUrl(string CompanyId)
        {
            string GoToUrl = string.Empty;
            GoToUrl = EyouSoft.Common.Utils.GetDomainByCompanyId(CompanyId, CityId);
            return GoToUrl;
        }


        /// <summary>
        /// 推广说明/团队状态
        /// </summary>
        /// <param name="TourId">团队ID</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="TourState">团队状态</param>
        /// <param name="TourSpreadStateName">推广名称</param>
        /// <returns></returns>
        protected string TourSpreadState(string TourState, string TourSpreadStateName)
        {
            string strTourMarkerNote = TourSpreadStateName;
            //团队推广说明
            if (TourState == "手动客满" || TourState == "自动客满")
            {
                strTourMarkerNote = "<span class=\"keman\">客满</span>";
            }
            if (TourState == "手动停收" || TourState == "自动停收")
            {
                strTourMarkerNote = "<span class=\"tings\">停收</span>";
            }
            return strTourMarkerNote;
        }


        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="time">时间</param>
        protected string GetLeaveInfo(DateTime time)
        {
            return string.Format("{0}/{1}({2})", time.Month, time.Day, EyouSoft.Common.Utils.ConvertWeekDayToChinese(time));
        }
        /// <summary>
        /// 获取MQ号码
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <returns></returns>
        protected string GetMq(string companyid)
        {
            var companyinfo = GetCompanyName(companyid);
            if (!string.IsNullOrEmpty(companyinfo.ToString()))
            {
                return companyinfo.ContactInfo.MQ;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取公司等级图标
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        protected string GetCompanyLevImgSrc(object Type)
        {
            int typeid = (int)Type;
            string LevImg = string.Empty;
            switch (typeid)
            {
                case 4://签约 （皇冠）
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/guan.gif";
                    break;
                case 3://收费商户（钻石）
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/zuan.gif";
                    break;
                case 2://认证商户（认证标签） 
                    LevImg = Domain.ServerComponents + "/images/new2011/xianlu/renzheng.gif";
                    break;
                default:
                    LevImg = "";
                    break;
            }
            return LevImg;

        }
        /// <summary>
        /// 获取线路推荐类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetRecommendTypeImgSrc(object type)
        {
            int typeid = (int)type;
            string LevImg = string.Empty;
            switch (typeid)
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

        #region   获取公司名称
        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetCompanyName(string id)
        {
            return EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(id);
        }
        #endregion

        /// <summary>
        /// 根据线路编号获取团号
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetTourId(object o)
        {
            string RouteId = o.ToString();
             Powderlist = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteId).OrderBy(m => m.LeaveDate).ToList();
            if (Powderlist != null && Powderlist.Count > 0)
            {
                return Powderlist[0].TourId;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 设置线路价格(没有团的线路价格显示电询)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        protected string SetTourPrice(object obj,object price)
        {
            string TourPlan = obj.ToString();
            if (string.IsNullOrEmpty(TourPlan))
            {
                return "电询";
            }
            else
            {
                return Convert.ToDecimal(price).ToString("F0") + "起";
            }
        }
       
    }
}
