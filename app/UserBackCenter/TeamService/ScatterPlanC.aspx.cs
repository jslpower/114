using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.NewTourStructure;

namespace UserBackCenter.TeamService
{
    /// <summary>
    /// 组团社-组团服务-国内散拼计划列表
    /// 李晓欢 2011-12-22
    /// </summary>
    public partial class ScatterPlanC : EyouSoft.Common.Control.BackPage
    {
        #region 参数
        protected int intPageIndex = 1;
        private int intPageSize = 15;
        private int RecordCount = 0;
        #endregion
        protected string Key = string.Empty;
        protected string litContactMQ = string.Empty;
        protected AreaType areaType = AreaType.国内长线;
        /// <summary>
        /// 出发地ID
        /// </summary>
        protected string startCityId = string.Empty;
        /// <summary>
        /// 出游天数
        /// </summary>
        protected int powderDay;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCompanyCheck)
            {
                Response.Clear();
                Response.Write("您的帐号尚未审核通过，暂时无法使用该功能!");
                Response.End();
                return;
            }
            if (!CheckGrant(TravelPermission.组团_线路散客订单管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
            if (!IsPostBack)
            {
                if (!IsLogin)
                {
                    EyouSoft.Security.Membership.UserProvider.RedirectLogin(Domain.UserBackCenter + "/Default.aspx");
                    return;
                }

                Key = "ScatterPlanC" + Guid.NewGuid().ToString();
                RouteUC.Key = Key;
                RouteUC.UserId = SiteUserInfo.ID;
                RouteUC.SelectFunctionName = "ScatterPlanC.Search";
                RouteUC.Title = "国内线路";
                RouteUC.ProvinceID = SiteUserInfo.ProvinceId;
                RouteUC.IsTongYe = true;
                //此代码设置国内长线
                RouteUC.SetAreaType = areaType;
                BindRecommendType();
                #region 初始化公司信息
                ////公司名称
                //this.litCompanyName.Text = SiteUserInfo.CompanyName;
                ////联系人
                //this.litContactName.Text = SiteUserInfo.ContactInfo.ContactName;
                ////电话
                //this.litContactPhone.Text = SiteUserInfo.ContactInfo.Mobile;
                ////传真
                //this.litContactFax.Text = SiteUserInfo.ContactInfo.Fax;
                ////在线QQ
                //this.litContactQQ.Text = EyouSoft.Common.Utils.GetQQ(SiteUserInfo.ContactInfo.QQ, "在线QQ");
                ////在线MQ
                //litContactMQ = EyouSoft.Common.Utils.GetMQ(SiteUserInfo.ContactInfo.MQ);
                #endregion

                #region 初始化查询条件
                //关键字
                string id = Utils.GetQueryStringValue("Id");
                if (id != null && !string.IsNullOrEmpty(id))
                {
                    this.SearchTxt.Value = id;
                }
                //出发地
                startCityId = Utils.GetQueryStringValue("StartPlaceId");
                string StartPlace = Utils.GetQueryStringValue("StartPlace");
                if (StartPlace != null && !string.IsNullOrEmpty(StartPlace))
                {
                    this.StartPlace.Value = StartPlace;
                }
                //出发时间

                this.StartDate.Value = Utils.GetDateTime(Request.QueryString["StartDate"]) == DateTime.MinValue ? string.Empty : Utils.GetDateTime(Request.QueryString["StartDate"]).ToString("yyyy-MM-dd");//(DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString()).ToString("yyyy-MM-dd");

                //返回时间
                DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
                if (EndDate != null && !string.IsNullOrEmpty(EndDate.ToString()))
                {
                    this.EndDate.Value = Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd");
                }
                #endregion

                PageInitList();
                BindCity();
            }
        }

        #region 初始化客户等级
        //protected string GetLevel(string levelId)
        //{
        //    string levelStr = string.Empty;
        //    switch (levelId)
        //    {
        //        case "1":
        //            levelStr = "<img src=\"" + ImageServerUrl + "/images/guan.gif\" title=\"皇冠推荐商户\" class=\"dengji_img\" />";
        //            break;
        //        case "2":
        //            levelStr = "<img src=\"" + ImageServerUrl + "/images/vip.gif\" title=\"钻石推荐商户\" class=\"dengji_img\" />";
        //            break;
        //        case "3":
        //            levelStr = "<img src=\"" + ImageServerUrl + "/images/renzhen.gif\" title=\"认证商户\" class=\"dengji_img\" />";
        //            break;
        //        default: break;
        //    }
        //    return levelStr;
        //}
        #endregion

        #region 初始化团队状态
        protected string INItTourState(string state, string tourID)
        {
            System.Text.StringBuilder StateStr = new System.Text.StringBuilder();
            switch (state)
            {
                //3 收客 2 停收 1 客满 4 成团
                case "2":
                    StateStr.Append("停收");
                    break;
                case "1":
                    StateStr.Append("客满");
                    break;
                default:
                    StateStr.Append("<a href=\"javascript:void(0);\" class=\"basic_btn\" value=\"" + tourID + "\"><span>预订</span></a><a href=\"/PrintPage/TeamTourInfo.aspx?TeamId=" + tourID + "\" target=\"_blank\" class=\"basic_btn\"><span>打印</span></a>");
                    break;
            }
            return StateStr.ToString();
        }
        #endregion

        #region 初始化线路状态
        //protected string InitRouteState(string state)
        //{
        //    //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
        //    string Routehtml = string.Empty;
        //    switch (state)
        //    {
        //        case "2": Routehtml = "推荐"; break;
        //        case "3": Routehtml = "特价"; break;
        //        case "4": Routehtml = "豪华"; break;
        //        case "5": Routehtml = "热门"; break;
        //        case "6": Routehtml = "新品"; break;
        //        case "8": Routehtml = "纯玩"; break;
        //        case "7": Routehtml = "经典"; break;
        //        default: break;
        //    }
        //    return Routehtml;
        //}
        #endregion

        #region 出发地点
        /// <summary>
        /// 绑定出发地点
        /// </summary>
        private void BindCity()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyCity bll = EyouSoft.BLL.CompanyStructure.CompanyCity.CreateInstance();
            IList<EyouSoft.Model.SystemStructure.CityBase> list = bll.GetCompanyPortCity(SiteUserInfo.CompanyID);
            if (list != null && list.Count > 0)
            {
                //城市
                rpt_DepartureCity.DataSource = list;
                rpt_DepartureCity.DataBind();
            }
        }
        #endregion

        #region  绑定列表
        protected void PageInitList()
        {
            intPageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.Model.NewTourStructure.MPowderSearch Search = new EyouSoft.Model.NewTourStructure.MPowderSearch();
            //线路区域编号
            string RouteID = Utils.GetQueryStringValue("RouteId");
            if (RouteID.Length > 0)
            {
                Search.AreaId = Utils.GetInt(RouteID);
                //专线用户控件选中
                RouteUC.CheckedId = RouteID;
            }
            //关键字
            string id = Utils.GetQueryStringValue("Id");
            if (id != null && id != "")
            {
                Search.TourKey = id;
            }
            //出发地
            Search.StartCityId = Utils.GetInt(startCityId);

            string StartPlace = Utils.GetQueryStringValue("StartPlace");
            if (StartPlace != null && StartPlace != "")
            {
                Search.StartCityName = StartPlace;
            }
            //出发时间
            Search.LeaveDate = Utils.GetDateTime(Request.QueryString["StartDate"] ?? (DateTime.Now + new TimeSpan(1, 0, 0, 0)).ToString());

            //返回时间
            DateTime? EndDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("EndDate"));
            if (EndDate != null)
            {
                Search.EndLeaveDate = Convert.ToDateTime(EndDate);
            }
            //出游天数
            powderDay = Utils.GetInt(Utils.GetQueryStringValue("travelDays"), 0);
            if (powderDay > 0)
            {
                Search.PowderDay = (PowderDay)powderDay;
            }

            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品 7 经典 8 纯玩
            string State = Utils.GetQueryStringValue("State");
            if (State.Length > 0)
            {
                Search.RecommendType = (RecommendType)Utils.GetInt(State);
            }
            //switch (State)
            //{
            //    case "1": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.无; break;
            //    case "2": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.推荐; break;
            //    case "3": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.特价; break;
            //    case "4": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.豪华; break;
            //    case "5": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.热门; break;
            //    case "6": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.新品; break;
            //    case "8": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.纯玩; break;
            //    case "7": Search.RecommendType = EyouSoft.Model.NewTourStructure.RecommendType.经典; break;
            //    default: break;
            //}

            IList<EyouSoft.Model.NewTourStructure.MPowderList> list = new EyouSoft.BLL.NewTourStructure.BPowderList().GetList(intPageSize, intPageIndex, ref RecordCount, AreaType.国内长线, SiteUserInfo.CityId, Search);
            if (list != null && list.Count > 0)
            {
                this.repPlanlist.DataSource = list;
                this.repPlanlist.DataBind();
                BindPage(Search);
            }
            else
            {
                pnlNodata.Visible = true;
                this.ExportPageInfo1.Visible = false;
            }
        }
        #endregion

        #region  分页
        protected void BindPage(MPowderSearch Search)
        {
            this.ExportPageInfo1.intPageSize = intPageSize;
            this.ExportPageInfo1.CurrencyPage = intPageIndex;
            this.ExportPageInfo1.intRecordCount = RecordCount;
            this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            //this.ExportPageInfo1.UrlParams = Request.QueryString;
            this.ExportPageInfo1.UrlParams.Add("travelDays", Utils.GetQueryStringValue("travelDays"));
            this.ExportPageInfo1.UrlParams.Add("lineId", Search.AreaId.ToString());
            this.ExportPageInfo1.UrlParams.Add("status", Utils.GetQueryStringValue("State"));
            this.ExportPageInfo1.UrlParams.Add("goTimeS", Search.LeaveDate.ToString());
            this.ExportPageInfo1.UrlParams.Add("goTimeE", Search.EndLeaveDate.ToString());
            this.ExportPageInfo1.UrlParams.Add("keyWord", Search.TourKey);
        }
        #endregion

        /// <summary>
        /// 绑定推荐类型
        /// </summary>
        private void BindRecommendType()
        {
            //获取推荐类型列表
            List<EnumObj> typeli = EnumObj.GetList(typeof(RecommendType));
            //去除无这个状态
            typeli.RemoveAt(0);
            rpt_type.DataSource = typeli;
            rpt_type.DataBind();
        }
    }
}
