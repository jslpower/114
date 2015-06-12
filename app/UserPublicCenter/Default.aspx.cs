using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
namespace UserPublicCenter
{
    /// <summary>
    /// 页面功能：大平台首页
    /// 开发人：xuty
    /// 开发时间：2011-5-12
    /// </summary>
    public partial class Index : EyouSoft.Common.Control.FrontPage
    {

        protected string partnerlinks;//合作伙伴与友情链接字符串
        protected string bannerAdv;//通栏广告
        protected string supplysHtml;//供求列表
        protected string strLoginMessage;//登录信息
        EyouSoft.IBLL.NewsStructure.INewsBll newBll;//资讯Bll
        EyouSoft.IBLL.AdvStructure.IAdv advBll;//广告Bll
        protected bool isExtendSite;//是否为二类分站
        protected string noticeHtml = string.Empty;//网站公告

        protected void Page_Load(object sender, EventArgs e)
        {
            PageMenu1.HeadMenuIndex = 1;
            this.Page.Title = string.Format(PageTitle.Home_Title, CityModel.CityName);//首页Page Title初始化
            AddMetaTag("description", string.Format(PageTitle.Home_Des, CityModel.CityName));//添加 Description Meta 标记
            AddMetaTag("keywords", string.Format(PageTitle.Home_Keywords, CityModel.CityName));//添加 Keywords Meta 标记

            //添加头部样式脚本
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("index2011"));
            Utils.AddStylesheetInclude(CssManage.GetCssFilePath("517autocomplete"));

            //绑定供求
            BindSupply();
            //创建广告类Bll
            advBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            //绑定焦点轮轮换广告

            isExtendSite = EyouSoft.BLL.AdvStructure.SiteExtend.CreateInstance().IsExtendSite(CityId);
            //如果是重点分站显示散拼中心,否则显示旅行社
            if (!isExtendSite)
            {
                if (IsLogin) { guest1.isLogin = true; }
                routeArea1.IsShow = true;
                goldc1.IsShow = true;
                RecomP1.IsShow = true;
                RecomP1.CityId = CityId;
                goldc1.CityId = CityId;
                routeArea1.CityId = CityId;
                Comp1.IsShow = false;
            }
            else
            {
                Comp1.IsShow = true;
                Comp1.CityName = CityModel.CityName;
                Comp1.CityId = CityId;
            }
            BindPartnerLinks();//友情链接
            newBll = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance();
            GetLoginMessage();
            GetAdvImg();
            GetDataCount();
            BindNotice();
            BindTurnPic();

        }
        /// <summary>
        /// 获取首页广告banner(3)
        /// </summary>
        private void GetAdvImg()
        {
            EyouSoft.IBLL.AdvStructure.IAdv advBll = EyouSoft.BLL.AdvStructure.Adv.CreateInstance();
            //获取通栏广告
            EyouSoft.Model.AdvStructure.AdvInfo advModel = advBll.GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页资讯通栏广告).FirstOrDefault();
            if (advModel != null)
            {
                bannerAdv = EyouSoft.Common.Utils.GetImgOrFalash(advModel.ImgPath, advModel.RedirectURL);
            }
        }

        #region 获取登录信息
        /// <summary>
        /// 获的登陆信息
        /// </summary>
        private void GetLoginMessage()
        {
            if (IsLogin)
            {
                this.divLogin.Visible = false;//隐藏DivNoLogin，用于显示登录入口
                this.divYesLogin.Visible = true;//显示div_YesLogin,用于显示登录的用户信息

                string URL = Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString;//当前请求URL

                /*
                 * 根据用户的类型，生成不同的用户信息 
                 */
                strLoginMessage = "<div class='loginedbox'><div>";
                EyouSoft.Model.CompanyStructure.CompanyDetailInfo Model = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetModel(SiteUserInfo.CompanyID);
                if (Model != null)
                {
                    //EyouSoft.Model.CompanyStructure.CompanyType[] CompanyTypeItems = Model.CompanyRole.RoleItems;
                    //判断当前用户是否是专线用户
                    bool isRouteAgency = SiteUserInfo.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.专线);

                    //显示用户所在的企业LOGO
                    if (!string.IsNullOrEmpty(Model.AttachInfo.CompanyLogo.ImagePath))
                    {
                        strLoginMessage += string.Format("<div style=\"line-height:30px; width:200px;\"><img height=\"30\" width=\"60\" src=\"{0}\"/>", EyouSoft.Common.Domain.FileSystem + Model.AttachInfo.CompanyLogo.ImagePath);
                        //显示用户名信息
                        strLoginMessage += "<span title=\"" + SiteUserInfo.UserName + "\"> 欢迎您!" + string.Format("&nbsp;{0}</span></div>", Utils.GetText(SiteUserInfo.UserName, 10, true));
                    }
                    else
                    {
                        //显示用户名信息
                        strLoginMessage += "<span title=\"" + SiteUserInfo.UserName + "\"> 欢迎您!" + string.Format("&nbsp;{0}</span></div>", Utils.GetText(SiteUserInfo.UserName, 19, true));
                    }
                    strLoginMessage += "<div class='loginedboxdiv'>";

                    string userManageUrl = null;//用户后台URL
                    string shopUrl = null;//网店URL


                    //判断是否开通了高级网店
                    if (Utils.IsOpenHighShop(SiteUserInfo.CompanyID))//开通了高级网店
                    {
                        shopUrl = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/MyOwenerShop.aspx";
                    }
                    else//没有开通
                    {
                        shopUrl = EyouSoft.Common.Domain.UserBackCenter + "/supplymanage/freeshop.aspx";
                    }
                    userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/SupplyManage/Default.aspx";

                    if (IsAirTicketSupplyUser)//如果是机票供应商用户
                    {
                        userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/TicketsCenter/";
                    }
                    else if (IsTemporaryUser)  //当前用户为随便逛逛
                    {
                        //zwc 20101122 增加随便逛逛
                        userManageUrl = EyouSoft.Common.Domain.UserClub + "/usercp.aspx";
                    }
                    else//如果是旅行社用户
                    {
                        userManageUrl = EyouSoft.Common.Domain.UserBackCenter + "/";
                        if (isRouteAgency)
                        {
                            shopUrl = Utils.GetCompanyDomain(SiteUserInfo.CompanyID, EyouSoft.Model.CompanyStructure.CompanyType.专线, CityId);
                        }
                    }

                    //显示 用户 进入后台的链接
                    strLoginMessage += string.Format("<a href='{0}' ><strong>进入后台</strong></a>", userManageUrl);

                    //专线用户 显示 【网店】，发布产品，管理产品 链接
                    if (isRouteAgency)
                    {
                        strLoginMessage += string.Format(" | <a href='{0}' >查看我的网店</a><br/>", shopUrl);
                        strLoginMessage += string.Format("<a href='{0}' >发布产品</a> | <a href='{1}' >管理</a><br/>", EyouSoft.Common.Domain.UserBackCenter + "/routeagency/routemanage/rmdefault.aspx", EyouSoft.Common.Domain.UserBackCenter + "/routeagency/routemanage/routeview.aspx");
                    }
                    else
                    {
                        strLoginMessage += "<br/>";
                    }

                    if (IsTemporaryUser)
                    {
                        //zwc 20101122 增加随便逛逛
                        strLoginMessage += string.Format("<a style=\"clear:both;\" href=\"{0}\" ><img style=\"vertical-align:middle\" alt=\"{1}\" src=\"{2}\" /></a><br /><a style=\"clear:both;\" href=\"{3}\" ><img alt=\"{4}\" src=\"{5}\" /></a><br/>", EyouSoft.Common.Domain.UserPublicCenter + "/Register/CompanyUserRegister.aspx?IsYkApplay=1", "点击申请成为采购商", EyouSoft.Common.Domain.ServerComponents + "/Images/UserPublicCenter/sqico_1.gif", EyouSoft.Common.Domain.UserPublicCenter + "/Register/CompanyUserRegister.aspx?IsYkApplay=2", "点击申请成为供应商", EyouSoft.Common.Domain.ServerComponents + "/Images/UserPublicCenter/sqico_2.gif");
                    }
                    else
                    {
                        //zxb 20100902 所有用户后台都有发布和管理供求信息的链接
                        strLoginMessage += string.Format("<a href='{0}' >发布供求信息</a> | <a href='{1}' >管理</a><br/>", Domain.UserBackCenter + "/supplyinformation/addsupplyinfo.aspx", EyouSoft.Common.Domain.UserBackCenter + "/supplyinformation/allsupplymanage.aspx");
                        //旅行社用户和供应商用户 显示 修改企业信息链接
                        strLoginMessage += string.Format("<a href='{0}' >修改企业信息</a> | ", EyouSoft.Common.Domain.UserBackCenter + "/SystemSet/CompanyInfoSet.aspx");

                    }

                    strLoginMessage += string.Format("<a style=\"clear:both;\" href='{0}'>退出</a><br/>", EyouSoft.Common.Utils.GetLogoutUrl(Request.Url.ToString()));
                    strLoginMessage += "</div>";
                    strLoginMessage += "</div>";
                }

            }
        }
        #endregion

        #region 绑定供求列表
        /// <summary>
        /// 绑定供求列表
        /// </summary>
        private void BindSupply()
        {
            StringBuilder strBuilder = new StringBuilder();
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> excList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(20, null, -1, true);
            if (excList != null && excList.Count > 0)
            {
                for (int i = 0; i < excList.Count; i++)
                {
                    if (excList[i].ExchangeCategory == EyouSoft.Model.CommunityStructure.ExchangeCategory.供)
                    {
                        if (i + 1 == excList.Count)
                        {
                            supplysHtml = strBuilder.ToString();
                            return;
                        }
                        strBuilder.AppendFormat("<li><span><img width=\"15px\" height=\"16px\" src=\"" + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/index/home_54.gif\"></span><a href=\"{0}\">{1}</a><span><img width=\"15px\" height=\"16px\" src=\"" + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/index/home_54.gif\"></span><a href=\"{2}\">{3}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i].ID, CityId), Utils.GetText(excList[i].ExchangeTitle, 13), EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i + 1].ID, CityId), Utils.GetText(excList[i + 1].ExchangeTitle, 13));
                    }
                    else
                    {
                        if (i + 1 == excList.Count)
                        {
                            supplysHtml = strBuilder.ToString();
                            return;
                        }
                        strBuilder.AppendFormat("<li><span><img width=\"15px\" height=\"16px\" src=\"" + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/index/home_57.gif\"></span><a href=\"{0}\">{1}</a><span><img width=\"15px\" height=\"16px\" src=\"" + EyouSoft.Common.Domain.ServerComponents + "/images/new2011/index/home_57.gif\"></span><a href=\"{2}\">{3}</a></li>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i].ID, CityId), Utils.GetText(excList[i].ExchangeTitle, 13), EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(excList[i + 1].ID, CityId), Utils.GetText(excList[i + 1].ExchangeTitle, 13));
                    }
                }
            }
            supplysHtml = strBuilder.ToString();
        }
        #endregion

        /// <summary>
        /// 获取网站统计数量
        /// </summary>
        private void GetDataCount()
        {
            EyouSoft.Model.SystemStructure.SummaryCount model = EyouSoft.BLL.SystemStructure.SummaryCount.CreateInstance().GetSummary();
            if (model != null)
            {
                //实际 + 虚拟
                //注册公司 = 旅行社数量 + 酒店数量 + 景区管理公司数量 + 车队数量 + 购物店数量
                lbregCount.Text =
                    (model.TravelAgency + model.TravelAgencyVirtual + model.Hotel + model.HotelVirtual + model.Sight +
                     model.SightVirtual + model.Car + model.CarVirtual + model.Shop + model.ShopVirtual).ToString();
                lbrouteCount.Text = (model.User + model.UserVirtual).ToString();
                lbMqCount.Text = (model.Route + model.RouteVirtual).ToString();
            }

        }
        /// <summary>
        /// 绑定公告
        /// </summary>
        private void BindNotice()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.首页广告公告);
            StringBuilder strBuilder = new StringBuilder();
            if (advList != null && advList.Count > 0)
            {
                for (int i = 0; i < advList.Count; i++)
                {
                    if (i == 3) { break; }
                    strBuilder.AppendFormat("<li><a target=\"_blank\" href=\"{3}/PlaneInfo/NewsDetailInfo.aspx?NewsID={0}&CityId={1}\">·{2}</a></li>", advList[i].AdvId, CityId, Utils.GetText(advList[i].Title, 15, false), Domain.UserPublicCenter);
                }
            }
            noticeHtml = strBuilder.ToString();
        }

        /// <summary>
        /// 绑定滚动图片
        /// </summary>
        private void BindTurnPic()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> advList = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetNotFillAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.焦点图片);
            if (advList != null && advList.Count > 0)
            {
                this.rptHotAdv.DataSource = advList;
                this.rptHotAdv.DataBind();
                this.rptHotAdvForLi.DataSource = advList;
                this.rptHotAdvForLi.DataBind();
            }
        }


        #region 绑定战略合作伙伴与友情链接
        /// <summary>
        /// 绑定战略合作伙伴与友情链接
        /// </summary>
        private void BindPartnerLinks()
        {
            StringBuilder strBuilder = new StringBuilder();//字符串拼接
            strBuilder.Append("<div class=\"links\"><s class=\"linksL\"></s><div class=\"links-cont\">");
            //操作BLL
            EyouSoft.IBLL.SystemStructure.ISysFriendLink linkBll = EyouSoft.BLL.SystemStructure.SysFriendLink.CreateInstance();
            //获取友情链接集合
            int recordCount = 1;
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> linkList = linkBll.GetSysFriendLinkList(8, 1, ref recordCount, 1, EyouSoft.Model.SystemStructure.FriendLinkType.战略合作);
            if (linkList != null && linkList.Count > 0)
            {
                strBuilder.Append("<ul class=\"links-pic fixed\">");
                foreach (EyouSoft.Model.SystemStructure.SysFriendLink linkModel in linkList)
                {
                    strBuilder.AppendFormat("<li><a target=\"blank\" href=\"{0}\"><img src=\"{1}\" alt=\"{2}\" /></a></li>", linkModel.LinkAddress, EyouSoft.Common.Domain.FileSystem + linkModel.ImgPath, linkModel.LinkName);
                }
                strBuilder.Append("</ul><div class=\"hr_5\"></div>");
            }
            //获取合作伙伴集合
            IList<EyouSoft.Model.SystemStructure.SysFriendLink> partnerList = linkBll.GetSysFriendLinkList(EyouSoft.Model.SystemStructure.FriendLinkType.文字);
            if (partnerList != null && partnerList.Count > 0)
            {
                strBuilder.Append("<p>");
                int partnerCount = partnerList.Count;
                foreach (EyouSoft.Model.SystemStructure.SysFriendLink linkModel in partnerList)
                {
                    partnerCount--;
                    //判断是否拼接最后一个有情链接(不加分隔符)
                    if (partnerCount > 0)
                        strBuilder.AppendFormat("<a target=\"blank\" href=\"{0}\">{1}</a> |", linkModel.LinkAddress, linkModel.LinkName);
                    else
                        strBuilder.AppendFormat("<a target=\"blank\" href=\"{0}\">{1}</a></p>", linkModel.LinkAddress, linkModel.LinkName);

                }
            }
            strBuilder.Append("</div><s class=\"linksR\"></s></div>");
            partnerlinks = strBuilder.ToString();//输出拼接结果
        }
        #endregion


    }
}
