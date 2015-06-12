using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;

using System.Collections.Generic;

namespace SeniorOnlineShop.shop
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 创建人：刘飞
    /// 时间：2011年12月22日
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 每页显示数
        /// </summary>
        protected int PageSize = 7;

        /// <summary>
        /// 当前页
        /// </summary>
        protected int PageIndex = 1;

        /// <summary>
        /// 返回页地址
        /// </summary>
        protected string ReturnUrl = "";

        /// <summary>
        /// 公司编号
        /// </summary>
        ///  /// <summary>
        /// 日历当前月
        /// </summary>
        /// /// <summary>
        /// 线路区域ID
        /// </summary>
        protected int TourAreaId = 0;

        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);

        protected string CompanyId = string.Empty;

        private IList<EyouSoft.Model.NewTourStructure.MPowderList> Powderlist;

        protected SeniorOnlineShop.master.GeneralShop Master;

        protected bool IsRoute = false; //非专线商登录

        protected bool IsTour = false; //非组团社登录

        protected bool Route = false; //当前公司非专线

        protected bool Tour = false; //当前公司非组团

        protected void Page_Load(object sender, EventArgs e)
        {
            Master = (SeniorOnlineShop.master.GeneralShop)this.Page.Master;
            Master.HeadIndex = 2;

            Page.Title = string.Format(PageTitle.EShop_Product_Title, Master.CompanyInfo.CompanyName, CityModel.CityName);
            AddMetaTag("description", string.Format(PageTitle.EShop_Product_Des, Master.CompanyInfo.CompanyName, CityModel.CityName));
            AddMetaTag("keywords", string.Format(PageTitle.EShop_Keywords, Master.CompanyInfo.CompanyName, CityModel.CityName));

            CompanyId = Master.CompanyId;
            SecondMenu1.CompanyId = Master.CompanyId;
            SecondMenu1.CurrCompanyType = Master.CompanyInfo.CompanyRole;
            SecondMenu1.CurrMenuIndex = 2;
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                PageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                BindCompanyPro(Master.CompanyId);
                GetCompanyRole();
                GetUserRole();
            }
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
                    EyouSoft.Model.CompanyStructure.CompanyDetailInfo Userinfo =
                        new EyouSoft.BLL.CompanyStructure.CompanyInfo().GetModel(this.SiteUserInfo.CompanyID);
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
                //
            }
        }

        /// <summary>
        /// 获取当前公司(网店)身份
        /// </summary>
        protected void GetCompanyRole()
        {
            if (this.Master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
            {
                Tour = true;
            }
            if (this.Master.CompanyInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
            {
                Route = true;
            }
        }

        /// <summary>
        /// 绑定线路
        /// </summary>
        /// <param name="CompanyId"></param>
        private void BindCompanyPro(string CompanyId)
        {
            //总条数
            int recordCount = 0;
            EyouSoft.Model.NewTourStructure.MRouteSearch Msearch = new EyouSoft.Model.NewTourStructure.MRouteSearch();
            IList<EyouSoft.Model.NewTourStructure.MShopRoute> RouteList =
                new List<EyouSoft.Model.NewTourStructure.MShopRoute>();
            Msearch.Publishers = CompanyId;
            Msearch.RouteStatus = (EyouSoft.Model.NewTourStructure.RouteStatus)1;
            RouteList = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetShopList(
                PageSize, PageIndex, ref recordCount, CompanyId, Msearch);

            if (RouteList.Count > 0 && RouteList != null)
            {
                this.PageInfoSelect1.intPageSize = PageSize;
                this.PageInfoSelect1.intRecordCount = recordCount;
                this.PageInfoSelect1.CurrencyPage = PageIndex;
                this.RptProductList.DataSource = RouteList;
                this.RptProductList.DataBind();
            }
            else
            {
                this.PageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// 获取MQ号码
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <returns></returns>
        protected string GetMq(string companyid)
        {
            var companyinfo = GetCompanyName(companyid);
            if (companyinfo != null)
            {
                return companyinfo.ContactInfo.MQ;
            }
            else
            {
                return null;
            }
        }

        #region   获取公司名称

        public EyouSoft.Model.CompanyStructure.CompanyDetailInfo GetCompanyName(string id)
        {
            return EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(id);
        }

        #endregion

        /// <summary>
        /// 获取线路推荐类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetRecommendTypeImgSrc(object type)
        {
            int typeid = (int)type;
            string LevImg = "<img width=\"25px\" height=\"15px\" alt=\"\" src='{0}'/>";
            switch (typeid)
            {
                case 1: //无
                    LevImg = "";
                    break;
                case 2: //推荐
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_30.gif");
                    break;
                case 3: //特价
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_35.gif");
                    break;
                case 4: //豪华
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_39.gif");
                    break;
                case 5: //热门
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_42.gif");
                    break;
                case 6: //新品
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_44.gif");
                    break;
                case 7: //纯玩
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_46.gif");
                    break;
                case 8: //经典
                    LevImg = string.Format(LevImg, Domain.ServerComponents + "/images/new2011/xianlu/zhuangtai_48.gif");
                    break;
            }
            return LevImg;
        }

        /// <summary>
        /// 获取团号
        /// </summary>
        /// <param name="o">线路编号</param>
        /// <returns></returns>
        protected string GetTourId(object o)
        {
            string RouteId = o.ToString();
            Powderlist =
                EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteId).OrderBy(m => m.LeaveDate).
                    ToList();
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
        /// 生成线路价格
        /// </summary>
        /// <param name="tourPlan">团队数量</param>
        /// <param name="tourPrice">团队价格</param>
        /// <param name="tourCkPrice">线路参考价格</param>
        /// <returns>团队显示价格</returns>
        protected string GetTourPrice(object tourPlan, object tourPrice, object tourCkPrice)
        {
            if (tourPlan == null || string.IsNullOrEmpty(tourPlan.ToString())) return "电询";

            if (tourPrice != null && Utils.GetDecimal(tourPrice.ToString(), 0) > 0)
            {
                return Utils.GetDecimal(tourPrice.ToString(), 0).ToString("F0") + "起";
            }

            if (tourCkPrice != null && Utils.GetDecimal(tourCkPrice.ToString(), 0) > 0)
                return Utils.GetDecimal(tourCkPrice.ToString(), 0).ToString("F0") + "起";
                
            return "电询";
        }

        /// <summary>
        /// 设置线路价格(没有团的线路价格显示电询)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        protected string SetTourPrice(object obj, object price)
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

        #region 获得预定链接

        /// <summary>
        /// 根据线路和当前登录公司返回预定链接
        /// (如果是地接线路的话,先判断是否有组团身份,如果有的话跳转到后台单团预定,反之不做跳转(提示);
        ///  如果不是地接线路,有组团身份的话跳转到后台单团预定页面; 
        ///  如果只有专线身份的话判断是否有团队,如果有的话跳转到代订页面(团号取第一个),没有的话跳转到旅游线路库;
        ///  如果只是其他身份的话,直接跳转到用户后台首页(其他身份不能预定线路).)
        /// </summary>
        /// <returns></returns>
        protected string GetLinkByRoute(object routeID, object tourID, object o, object plan)
        {
            string str = "";
            if (IsLogin)
            {
                EyouSoft.Model.NewTourStructure.RouteSource rSource = (EyouSoft.Model.NewTourStructure.RouteSource)o;
                if (rSource == EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                {
                    if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                              + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID.ToString()
                              + "&isZT=true'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                              + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                    }
                    else
                    {
                        str = "<a alt='该操作需要组团身份' href='" + EyouSoft.Common.Domain.UserBackCenter
                              + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                              + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Convert.ToString(plan)))
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                                  + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID.ToString()
                                  + "&isZT=true'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                                  + EyouSoft.Common.Domain.ServerComponents
                                  + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                        }
                        else
                        {
                            str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                                  + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                                  + EyouSoft.Common.Domain.ServerComponents
                                  + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                        }
                    }
                    else
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                                  + "/Order/OrderByTour.aspx?tourID=" + GetTourId(routeID)
                                  + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                                  + EyouSoft.Common.Domain.ServerComponents
                                  + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                        }
                        else if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                        {
                            str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                                  + "/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + GetTourId(routeID)
                                  + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                                  + EyouSoft.Common.Domain.ServerComponents
                                  + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                        }
                        else
                        {
                            str = "<a href='" + EyouSoft.Common.Domain.UserBackCenter
                                  + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='"
                                  + EyouSoft.Common.Domain.ServerComponents
                                  + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
                        }
                    }
                }
            }
            else
            {
                str = "<a href='" + EyouSoft.Common.Domain.UserPublicCenter + "/Register/Login.aspx?isShow=1&CityId="
                      + CityId + "&returnurl=" + Request.Url.ToString()
                      + "'><img width=\"69\" height=\"23\" alt=\"预订\" src='" + EyouSoft.Common.Domain.ServerComponents
                      + "/images/new2011/xianlu/gscp_14.jpg' /></a>";
            }
            return str;
        }

        #endregion
    }
}