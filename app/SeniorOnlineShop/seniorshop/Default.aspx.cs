using System;
using System.Collections;
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
using System.Collections.Generic;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SeniorOnlineShop
{
    public partial class _Default : EyouSoft.Common.Control.FrontPage
    {
        protected string initFlashJs = string.Empty;
        protected bool IsCanViewTongHang = false;
        protected string thisDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected void Page_Load(object sender, EventArgs e)
        {

            //广告轮转初始化
            IList<EyouSoft.Model.ShopStructure.HighShopAdv> advList = EyouSoft.BLL.ShopStructure.HighShopAdv.CreateInstance().GetWebList(5,this.Master.CompanyId);
            int i = 1;
            string format = ";imag[{0}]=\"{1}\";link[{2}]=\"{3}\";text[{4}]=\"{5}\";";
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            string imagePath = null;
            foreach (EyouSoft.Model.ShopStructure.HighShopAdv adv in advList)
            {
                imagePath =Utils.GetLineShopImgPath(adv.ImagePath,3);
                strb.AppendFormat(format, i, imagePath, i, adv.LinkAddress!=string.Empty?adv.LinkAddress:"#", i, "图片"+i);
                i++;
            }
            initFlashJs = strb.ToString();
            advList = null;
            //最新旅游动态初始化
            linkNewsList.HRef = Utils.GenerateShopPageUrl2("/newslist", this.Master.CompanyId);
            IList<EyouSoft.Model.ShopStructure.HighShopNews> newsList = EyouSoft.BLL.ShopStructure.HighShopNews.CreateInstance().GetTopNumberList(6, this.Master.CompanyId);
            rptNewsList.DataSource = newsList;
            rptNewsList.DataBind();
            newsList = null;

            //出游指南初始化
            linkMuDiDi1.HRef = Utils.GenerateShopPageUrl2("/mudidis2_1", this.Master.CompanyId);
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> trip1List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(3, this.Master.CompanyId, 1,null);
            rptTrip1.DataSource = trip1List;
            rptTrip1.DataBind();
            rptTrip1 = null;

            linkMuDiDi2.HRef = Utils.GenerateShopPageUrl2("/mudidis2_2", this.Master.CompanyId);
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> trip2List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(3, this.Master.CompanyId, 2, null);
            rptTrip2.DataSource = trip2List;
            rptTrip2.DataBind();
            trip2List = null;

            linkMuDiDi3.HRef = Utils.GenerateShopPageUrl2("/mudidis2_3", this.Master.CompanyId);
            IList<EyouSoft.Model.ShopStructure.HighShopTripGuide> trip3List = EyouSoft.BLL.ShopStructure.HighShopTripGuide.CreateInstance().GetWebList(6, this.Master.CompanyId, 3, null);
            rptTrip3.DataSource = trip3List;
            rptTrip3.DataBind();
            trip3List = null;

            //旅游资源初始化
            linkZiYuans.HRef = Utils.GenerateShopPageUrl2("/ziyuan", this.Master.CompanyId);
            IList<EyouSoft.Model.ShopStructure.HighShopResource> resList = EyouSoft.BLL.ShopStructure.HighShopResource.CreateInstance().GetIndexList(5, this.Master.CompanyId);
            rptziyuans.DataSource = resList;
            rptziyuans.DataBind();
            resList = null;

            //绑定销售城市
            rptCitys.DataSource = this.Master.DetailCompanyInfo.SaleCity.Take(3) ;
            rptCitys.DataBind();

            if (this.Master.DetailCompanyInfo.SaleCity.Count > 4)
            {
                ddlCitys.Items.Clear();
                this.ddlCitys.Items.Insert(0, new ListItem("全部", "0"));
                for (int j = 0; j < this.Master.DetailCompanyInfo.SaleCity.Count; j++)
                {
                    ListItem item = new ListItem();
                    item.Text = this.Master.DetailCompanyInfo.SaleCity[j].CityName + "出港";
                    item.Value = this.Master.DetailCompanyInfo.SaleCity[j].CityId.ToString();
                    ddlCitys.Items.Add(item);
                }            
            }
            else
            {
                ddlCitys.Visible = false;
            }

            //初始化 IsCanViewTongHang
            if (IsLogin == true)//公司是否通过审核
            {
                IsCanViewTongHang = true;
            }
            else
            {
                IsCanViewTongHang = false;
            }

            //线路初始化           
            IList<EyouSoft.Model.NewTourStructure.MShopRoute> ShopRoute = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetShopList(7, 0, this.Master.CompanyId);
            if (ShopRoute != null && ShopRoute.Count > 0)
            {
                rptTourList.DataSource = ShopRoute;
                rptTourList.DataBind();
            }
            else
            {
                this.labShowMsg.Text = "暂无旅游线路信息！";
                this.rptTourList.Visible = false;
            }
            ShopRoute = null;

            InitOlServer();

            //设置Title.....
            IList<EyouSoft.Model.SystemStructure.AreaBase> arealist = EyouSoft.BLL.CompanyStructure.CompanyArea.CreateInstance().GetCompanyArea(this.Master.CompanyId);
            string strarea = string.Empty;
            this.Title = string.Format(EyouSoft.Common.PageTitle.SeniorShop_Title, string.Empty);
            foreach (EyouSoft.Model.SystemStructure.AreaBase area in arealist)
            {
                strarea = strarea + "、" + area.AreaName;
            }
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.SeniorShop_Des, lbCompanyName.Text, CityModel.ProvinceName + CityModel.CityName, strarea.TrimStart(new char[] { '、'})));
        }

        /// <summary>
        /// 1 线路来源于地接:  当前登录公司是组团，预定跳转到用户后台单团预定 其它身份就跳转到用户后台首页
        /// 2 线路来源于专线:  先判断线路下是否有计划的，如果有计划的，在判断公司身份的，如果公司身份中包含组团身份 到用户后台 单团预定
        ///                    如果公司身份中包含专线身份的，跳转到用户后台专线待定的 如果包含地接身份的，跳转到用户后台首页
        ///                    如果没有计划,也要判断公司身份的，如果公司身份包含组团身份的 到用户后台 单团预定 其它身份都跳转到用户后台首页的  
        /// </summary>
        /// <returns></returns>
        protected string GetOrderByRoute(string routeID, object o)
        {
            string GetOrderByRouteHtml = string.Empty;
            if (IsLogin)
            {
                EyouSoft.Model.NewTourStructure.RouteSource rSource = (EyouSoft.Model.NewTourStructure.RouteSource)o;
                if (rSource == EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                {
                    if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                    {
                        GetOrderByRouteHtml = "<a target=\"_blank\" href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID + "&isZT=true\" class=\"goumai0\">预定</a>";
                    }
                    else
                    {
                        GetOrderByRouteHtml = "<a  alt='该操作需要组团身份' href=\"" + EyouSoft.Common.Domain.UserBackCenter + "\" class=\"goumai0\">预定</a>";
                    }
                }
                else
                {
                    IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(routeID);
                    if (PowerList != null && PowerList.Count > 0)
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"   href=\"" + EyouSoft.Common.Domain.UserBackCenter + "/Order/OrderByTour.aspx?routeId=" + routeID.ToString() + "&isZT=true\">预定</a>";
                        }
                        else if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\" href='" + EyouSoft.Common.Domain.UserBackCenter + "/Order/RouteAgency/AddOrderByRoute.aspx?tourID=" + PowerList[0].TourId + "'>预定</a>";
                        }
                        else
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" href='" + EyouSoft.Common.Domain.UserBackCenter + "' class=\"goumai0\">预定</a>";
                        }
                    }
                    else
                    {
                        if (SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"  href='" + EyouSoft.Common.Domain.UserBackCenter + "/TeamService/SingleGroupPre.aspx?routeId=" + routeID + "'>预定</a>";
                        }
                        else
                        {
                            GetOrderByRouteHtml = "<a style=\"cursor:pointer\" target=\"_blank\" class=\"goumai0\"  href=\"" + EyouSoft.Common.Domain.UserBackCenter + "\">预定</a>";
                        }
                    }             
                }
            }
            else
            {                
                GetOrderByRouteHtml = "<a  target=\"_blank\" href=\"javascript:void(0);\"  class=\"goumai0\">预定</a>";
            }
            return GetOrderByRouteHtml;
        }


        protected string GetLoginUrl()
        {
            string url = Request.Url.ToString();
            return EyouSoft.Security.Membership.UserProvider.GetMinLoginPageUrl(url, "_parent", "");
        }

        protected void rptTourListItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.NewTourStructure.MShopRoute RouteModel = e.Item.DataItem as EyouSoft.Model.NewTourStructure.MShopRoute;
                if (RouteModel != null)
                {
                    Literal ltr = e.Item.FindControl("ltrTuiGuang") as Literal;
                    if (ltr != null)
                    {
                        ltr.Text = GetRecommendType(((int)RouteModel.RecommendType).ToString());
                    }

                    HtmlAnchor a = e.Item.FindControl("linkTour") as HtmlAnchor;
                    if (a != null)
                    {
                        a.HRef = Utils.GenerateShopPageUrl2("/TourDetail_" + RouteModel.RouteId, this.Master.CompanyId);
                    }

                    Literal litRouteName = e.Item.FindControl("LitRouteName") as Literal;
                    if (litRouteName != null)
                    {
                        litRouteName.Text = RouteModel.RouteName;
                    }

                    if (RouteModel.RouteSource == EyouSoft.Model.NewTourStructure.RouteSource.地接社添加)
                    {
                        // 班次
                        ltr = e.Item.FindControl("ltrCurrentTour") as Literal;
                        if (ltr != null)
                        {
                            ltr.Text = "暂无";
                        }
                        //市场价
                        ltr = e.Item.FindControl("ltrPrices") as Literal;
                        if (ltr != null)
                        {
                            ltr.Text = "电询";
                        }
                    }
                    else
                    {
                        IList<EyouSoft.Model.NewTourStructure.MPowderList> PowerList = EyouSoft.BLL.NewTourStructure.BPowderList.CreateInstance().GetList(RouteModel.RouteId);
                        if (PowerList != null && PowerList.Count > 0)
                        {
                            ltr = e.Item.FindControl("ltrCurrentTour") as Literal;
                            if (ltr != null)
                            {
                                ltr.Text = RouteModel.TeamPlanDes;
                            }
                            ltr = e.Item.FindControl("ltrPrices") as Literal;
                            if (ltr != null)
                            {
                                ltr.Text = string.Format("门市价:{0}起",
                                  RouteModel.RetailAdultPrice.ToString("F0"));
                            }
                        }
                        else
                        {
                            ltr = e.Item.FindControl("ltrCurrentTour") as Literal;
                            if (ltr != null)
                            {
                                ltr.Text = "暂无";
                            }
                            ltr = e.Item.FindControl("ltrPrices") as Literal;
                            if (ltr != null)
                            {
                                ltr.Text = "电询";
                            }
                        }
                    }
                    

                    //￥200/150 单房差
                    ltr = e.Item.FindControl("ltrDanFangCha") as Literal;
                    if (ltr != null)
                    {
                        ltr.Text = RouteModel.StartCityName;
                    }

                    ltr = e.Item.FindControl("linkMQ") as Literal;
                    if (ltr != null)
                    {
                        ltr.Text = Utils.GetMQ(this.Master.CompanyId);
                    }
                }
            }
        }

        protected string GetRecommendType(string state)
        {           
            //1 无  2 推荐 3 特价 4 豪华 5 热门 6 新品  7 经典 8 纯玩
            string stateHtml = string.Empty;
            switch (state)
            {
                case "1":stateHtml = "";break;
                case "2": stateHtml = "<img src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_30.gif\" width=\"25\" height=\"15\" alt=\"推荐\"/>"; break;
                case "3": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_35.gif\" width=\"25\" height=\"15\" alt=\"特价\"/>"; break;
                case "4": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_39.gif\" width=\"25\" height=\"15\" alt=\"豪华\"/>"; break;
                case "5": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_42.gif\" width=\"25\" height=\"15\" alt=\"热门\"/>"; break;
                case "6": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_44.gif\" width=\"25\" height=\"15\" alt=\"新品\"/>"; break;
                case "7": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_48.gif\" width=\"25\" height=\"15\" alt=\"经典\"/>"; break;
                case "8": stateHtml = "<img  src=\"" + ImageServerUrl + "/images/seniorshop/zhuangtai_46.gif\" width=\"25\" height=\"15\" alt=\"纯玩\"/>"; break;
                default: break;
            }
            return stateHtml;
        }


        protected void rptNewsList_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.ShopStructure.HighShopNews news = e.Item.DataItem as EyouSoft.Model.ShopStructure.HighShopNews;
                if (news != null)
                {
                    HtmlAnchor a = e.Item.FindControl("linkNew") as HtmlAnchor;
                    if (a != null)
                    {
                        a.HRef = Utils.GenerateShopPageUrl2("/new_" + news.ID, this.Master.CompanyId);
                        if (news.Title.Length >= 23)
                        {
                            a.InnerText = news.Title.Substring(0, 23);
                        }
                        else
                        {
                            a.InnerText = news.Title;
                        }
                    }
                }
            }
        }

        protected void rptziyuans_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.ShopStructure.HighShopResource res = e.Item.DataItem as EyouSoft.Model.ShopStructure.HighShopResource;
                if (res != null)
                {
                    string linkUrl = Utils.GenerateShopPageUrl2(string.Format("/ZiYuanShow_{0}", res.ID), this.Master.CompanyId);
                    HtmlAnchor a = e.Item.FindControl("linkZiYuanShow1") as HtmlAnchor;
                    if (a != null)
                    {
                        a.HRef = linkUrl;
                        a.Title = res.Title;
                        a.InnerHtml = string.Format("<img src=\"{0}\" border=\"0\" height=\"82\" width=\"123\">",Utils.GetLineShopImgPath(res.ImagePath,4));
                    }

                    HtmlAnchor b = e.Item.FindControl("linkZiYuanShow2") as HtmlAnchor;
                    if (b != null)
                    {
                        b.HRef = linkUrl;
                        b.Title = res.Title;
                        if (res.Title.Length >= 10)
                        {
                            b.InnerText = res.Title.Substring(0, 10);
                        }
                        else
                        {
                            b.InnerText = res.Title;
                        }
                    }
                }
            }
        }

        private const string TripFormat1 = 
            "<table class=\"maintop5\" style=\"margin-bottom: 5px;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tbody><tr><td><a href='{0}'><img alt='点击查看详细' src='{1}' border=\"0\" height=\"73\" width=\"97\"></a></td><td width=\"122px\" style=\"padding-left: 1px;padding-right:4px;\"><a href='{2}' class=\"huizi\">{3}</a></td></tr></tbody></table>";
        private const string TripFormat2 = 
            "·<a href=\"{0}\" class=\"huizi\">{1}</a>";
        protected void rptTrip1_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide trip = e.Item.DataItem as EyouSoft.Model.ShopStructure.HighShopTripGuide;
                if (trip != null)
                {
                    Literal ltr = e.Item.FindControl("ltrTrip") as Literal;
                    if (ltr != null)
                    {
                        string linkUrl = Utils.GenerateShopPageUrl2(string.Format("/MuDiDi_{0}", trip.ID), this.Master.CompanyId);
                        if (e.Item.ItemIndex == 0)
                        {
                            ltr.Text = string.Format(TripFormat1, linkUrl,Utils.GetLineShopImgPath(trip.ImagePath,5), linkUrl, Utils.GetText(Utils.InputText(trip.ContentText), 35, true));
                        }
                        else
                        {
                            ltr.Text = string.Format(TripFormat2,linkUrl,Utils.GetText(trip.Title,15));
                        }
                    }
                }
            }
        }

        protected void rptTrip3_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.ShopStructure.HighShopTripGuide trip = e.Item.DataItem as EyouSoft.Model.ShopStructure.HighShopTripGuide;
                if (trip != null)
                {
                    HtmlAnchor a = e.Item.FindControl("linkTrip") as HtmlAnchor;
                    if (a != null)
                    {
                        string linkUrl = Utils.GenerateShopPageUrl2(string.Format("/MuDiDi_{0}", trip.ID), this.Master.CompanyId);
                        a.InnerText = Utils.GetText(trip.Title, 15);
                        a.HRef = linkUrl;
                    }
                }
            }
        }

        protected void rptCitys_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                EyouSoft.Model.SystemStructure.CityBase city = e.Item.DataItem as EyouSoft.Model.SystemStructure.CityBase;
                if (city != null)
                {
                    HtmlAnchor a = e.Item.FindControl("linkCity") as HtmlAnchor;
                    if (a != null)
                    {
                        a.HRef = Utils.GenerateShopPageUrl2("/TourList_" + city.CityId, this.Master.CompanyId);
                        a.InnerText = city.CityName + "出港";
                    }
                }
            }
        }

        #region 初始化在线服务
        /// <summary>
        /// 初始化在线服务
        /// </summary>
        public void InitOlServer()
        {
            SeniorOnlineShop.master.SeniorShop cMaster = (SeniorOnlineShop.master.SeniorShop)this.Master;
            if (cMaster != null)
            {
                lbCompanyName.Text = cMaster.DetailCompanyInfo.CompanyName;
            }
            cMaster = null;

            string Remote_IP = StringValidate.GetRemoteIP();
            EyouSoft.Model.SystemStructure.CityBase cityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetClientCityByIp(Remote_IP);
            if (cityModel != null)
            {
                EyouSoft.Model.SystemStructure.SysCity thisCityModel = EyouSoft.BLL.SystemStructure.SysCity.CreateInstance().GetSysCityModel(cityModel.CityId);
                if (thisCityModel != null)
                {
                    lbGuestInfo.Text = string.Format("欢迎您来自{0}省{1}市的朋友有什么可以帮助您的吗？", thisCityModel.ProvinceName, thisCityModel.CityName);
                }
                thisCityModel = null;
            }
            cityModel = null;
        }
        #endregion

    }
}
