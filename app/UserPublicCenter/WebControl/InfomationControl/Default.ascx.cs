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
using EyouSoft.Common;
using System.Text;
using System.Collections.Generic;
using EyouSoft.Common.Control;

namespace UserPublicCenter.WebControl.InfomationControl
{
    /// <summary>
    /// 创建人：张新兵，创建时间：20110701
    /// 内容：用于显示 资讯首页
    /// </summary>
    public partial class Default : System.Web.UI.UserControl
    {
        /// <summary>
        /// 首页焦点图信息
        /// </summary>
        protected string HomePictureLists = "";

        /// <summary>
        /// 热点资讯[头两条]
        /// </summary>
        protected string HeaderNewsTop = "";

        /// <summary>
        /// 热点资讯
        /// </summary>
        protected string HeaderNews = "";

        /// <summary>
        /// 供求信息
        /// </summary>
        protected string strAllSupplyList = "";

        /// <summary>
        /// 资讯类别
        /// </summary>
        protected string ITypeLists = "";

        /// <summary>
        /// 同堂类别
        /// </summary>
        protected string STypeLists = "";

        /// <summary>
        /// 热点资讯排行
        /// </summary>
        protected string HotLists = "";

        /// <summary>
        /// 行业新闻
        /// </summary>
        protected string TradeNewsList = "";

        /// <summary>
        /// 行业新闻下[取资讯的前几条]
        /// </summary>
        protected string TradeBottenLists = "";

        /// <summary>
        /// 嘉宾访谈信息列表
        /// </summary>
        protected string InterviewLists = "";

        /// <summary>
        /// 展会大全
        /// </summary>
        protected string ExpositionLists = "";

        /// <summary>
        /// 最新线路信息
        /// </summary>
        protected string NewestRouteList = "";

        /// <summary>
        /// 旅游营销
        /// </summary>
        protected string TourMarketingLists = "";

        /// <summary>
        /// 企业经营与管理
        /// </summary>
        protected string CompanyManagementLists = "";

        /// <summary>
        /// 旅游政策法规
        /// </summary>
        protected string StartLists = "";

        /// <summary>
        /// 计调指南
        /// </summary>
        protected string TuningmeterLists = "";

        /// <summary>
        /// 导游带团
        /// </summary>
        protected string CiceroneLists = "";

        /// <summary>
        /// 最新加盟企业
        /// </summary>
        protected string JoininNews = "";

        /// <summary>
        /// 最新线路跳转页面
        /// </summary>
        private string ReturnUrl = "";

        protected int CityId;

        protected string ImageServerPath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //最新线路跳转页面
            ReturnUrl = Server.UrlEncode(Request.ServerVariables["SCRIPT_NAME"] + "?" + Request.QueryString);

            //初始化城市ID
            FrontPage frontPage = this.Page as FrontPage;
            if (frontPage != null)
            {
                CityId = frontPage.CityId;
                ImageServerPath = frontPage.ImageServerUrl;
            }

            GetHomePictureList();//首页图片切换信息
            GetHeaderList();//绑定热点资讯
            GetSupplyList();//实时供求
            GetNewsType();//类别和信息
            GetNewsOrdeyBy();//热点资讯排行
            GetTradeBottenList();//行业新闻下前几条
            GetTourList();//最新线路信息
            GetJoininNews();//最新加盟信息
        }

        #region 首页焦点图信息
        /// <summary>
        /// 首页焦点图信息
        /// </summary>
        protected void GetHomePictureList()
        {
            EyouSoft.BLL.NewsStructure.NewsPicInfo bll_NewsPicInfo = new EyouSoft.BLL.NewsStructure.NewsPicInfo();

            StringBuilder strHomePictureList = new StringBuilder();

            if (bll_NewsPicInfo.GetModel() != null)
            {
                IList<EyouSoft.Model.NewsStructure.BasicNewsPic> homelist = bll_NewsPicInfo.GetModel().FocusPic;
                if (homelist != null && homelist.Count > 0)
                {

                    strHomePictureList.Append(" <div class=\"container\">");
                    strHomePictureList.Append("<ul class=\"slides\">");
                    for (int intCount = 0; intCount < homelist.Count; intCount++)
                    {
                        strHomePictureList.AppendFormat("<li><a href='{0}' target=\"_blank\"><IMG src='{1}'></a>", homelist[intCount].PicUrl, Domain.FileSystem + homelist[intCount].PicPath);
                        strHomePictureList.Append("<div class=\"bg\"></div>");
                        strHomePictureList.AppendFormat("<dl><dt><A href='{0}' target=\"_blank\">{1}</A></dt></dl></li>", homelist[intCount].PicUrl, EyouSoft.Common.Utils.GetText(homelist[intCount].PicTitle, 11));
                    }
                    strHomePictureList.Append("</ul></div>");
                    strHomePictureList.Append("<div class=\"validate_Slider\"></div><ul class=\"pagination\">");
                    for (int intSum = 0; intSum < homelist.Count; intSum++)
                    {
                        strHomePictureList.AppendFormat("<li><A href='{0}'>{1}</A></li>", homelist[intSum].PicUrl, intSum + 1);
                    }
                    strHomePictureList.Append("</ul>");
                }
                else
                {
                    strHomePictureList.Append("暂无图片切换信息");
                }
            }
            else
            {
                strHomePictureList.Append("暂无图片切换信息");
            }

            HomePictureLists = strHomePictureList.ToString();
        }

        #endregion

        #region 获取同行社区中间图片信息
        /// <summary>
        /// 获取同行社区中间图片信息
        /// </summary>
        /// <param name="intType">图片排序,0:左,1:右</param>
        /// <param name="intSumber">获取的排序,例如：0(图片路径).1(链接地址).2(标题)</param>
        /// <returns></returns>
        public string GetPictureMiddle(int intType, int intSumber)
        {
            string strPictureInfo = string.Empty;
            EyouSoft.BLL.NewsStructure.NewsPicInfo bll_NewsPicInfo = new EyouSoft.BLL.NewsStructure.NewsPicInfo();
            if (bll_NewsPicInfo.GetModel() != null)
            {//获取同行社区中间的图片
                IList<EyouSoft.Model.NewsStructure.BasicNewsPic> homelist = bll_NewsPicInfo.GetModel().CommunityMiddlePic;
                //判断
                if (homelist != null && homelist.Count > 0)
                {
                    if (intSumber == 0)
                    {//图片路径
                        strPictureInfo = Domain.FileSystem + homelist[intType].PicPath;
                    }
                    else if (intSumber == 1)
                    {//链接地址
                        strPictureInfo = homelist[intType].PicUrl;
                    }
                    else
                    {//标题[截取长度14]
                        //过滤HTML代码
                        string strPicTtite = homelist[intType].PicTitle.Replace("'", "");
                        strPictureInfo = EyouSoft.Common.Utils.LoseHtml(strPicTtite, 14);
                    }
                }
            }

            return strPictureInfo;
        }

        #endregion

        #region 热点资讯
        /// <summary>
        /// 热点资讯
        /// </summary>
        protected void GetHeaderList()
        {
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> HeaderList = bll_News.GetHeadNews(7);
            //对取出的信息进行过滤
            int intHeader = 0;
            StringBuilder strHraderNewsTop = new StringBuilder();
            StringBuilder strHeaderNews = new StringBuilder();
            if (HeaderList != null && HeaderList.Count > 0)
            {
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in HeaderList)
                {
                    intHeader += 1;

                    if (intHeader == 1)
                    {
                        if (item.GotoUrl.Length > 0)
                        {
                            strHraderNewsTop.AppendFormat("<h2><a href='{0}' title='{4}' target=\"_blank\" style='{3}'>{1}</a></h2><p>{2}</p>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 16), EyouSoft.Common.Utils.GetText(item.AfficheDesc, 52, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strHraderNewsTop.AppendFormat("<h2><a href='{0}' title='{4}' target=\"_blank\" style='{3}'>{1}</a></h2><p>{2}</p>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 16), EyouSoft.Common.Utils.GetText(item.AfficheDesc, 52, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                    else if (intHeader == 2)
                    {
                        if (item.GotoUrl.Length > 0)
                        {
                            strHraderNewsTop.AppendFormat("<h2 class=\"too2\"><a href='{0}' title='{4}' target=\"_blank\" style='{3}'>{1}</a></h2><p>{2}</p>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 16), EyouSoft.Common.Utils.GetText(item.AfficheDesc, 52, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strHraderNewsTop.AppendFormat("<h2 class=\"too2\"><a href='{0}' title='{4}' target=\"_blank\" style='{3}'>{1}</a></h2><p>{2}</p>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 16), EyouSoft.Common.Utils.GetText(item.AfficheDesc, 52, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                    else
                    {
                        //计算截取的程度
                        int intNewClassLeach = item.ClassName.Length - 4;
                        int NewsTitleLeach = 20 - intNewClassLeach;
                        if (item.GotoUrl.Length > 0)
                        {
                            strHeaderNews.AppendFormat("<li><a href='{0}' title='{5}' class=\"ac\">{1}</a><a href='{2}' title='{3}' target=\"_blank\" style='{4}'>{3}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(item.AfficheClass), "[" + item.ClassName + "]" + "&nbsp;", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, NewsTitleLeach), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strHeaderNews.AppendFormat("<li><a href='{0}' title='{5}' class=\"ac\">{1}</a><a href='{2}' title='{3}' target=\"_blank\" style='{4}'>{3}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(item.AfficheClass), "[" + item.ClassName + "]" + "&nbsp;", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, NewsTitleLeach), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                }
            }
            else
            {
                strHraderNewsTop.Append("<li>暂无资讯头条信息</li>");
            }
            HeaderNewsTop = strHraderNewsTop.ToString();
            HeaderNews = strHeaderNews.ToString();
        }

        #endregion

        #region 获取图片广告相关信息
        /// <summary>
        /// 获取图片广告相关信息
        /// </summary>
        /// <param name="position">位置:0(焦点新闻右侧公告图片),1(旅游资讯焦点图片),2(同业学堂焦点图片),3(同业学堂右侧广告),4(同行社区)</param>
        /// <param name="parameter">参数:0(图片路径),1(链接地址),2(标题名称)</param>
        /// <returns></returns>
        public string GetAdvertPicture(int position, int parameter)
        {
            string strPictureInfo = string.Empty;
            //根据位置信息、参数信息查询
            EyouSoft.BLL.NewsStructure.NewsPicInfo bll_NewsPicInfo = new EyouSoft.BLL.NewsStructure.NewsPicInfo();
            if (bll_NewsPicInfo.GetModel() != null)
            {
                //绑定
                if (position == 0)
                {//焦点新闻右侧公告图片
                    if (parameter == 0)
                    {
                        strPictureInfo = Domain.FileSystem + bll_NewsPicInfo.GetModel().FocusRightPic.PicPath;
                    }
                    else if (parameter == 1)
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().FocusRightPic.PicUrl;
                    }
                    else
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().FocusRightPic.PicTitle;
                    }
                }
                else if (position == 1)
                {//旅游资讯焦点图片
                    if (parameter == 0)
                    {
                        strPictureInfo = Domain.FileSystem + bll_NewsPicInfo.GetModel().TravelFocusPic.PicPath;
                    }
                    else if (parameter == 1)
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().TravelFocusPic.PicUrl;
                    }
                    else
                    {
                        strPictureInfo = EyouSoft.Common.Utils.GetText(bll_NewsPicInfo.GetModel().TravelFocusPic.PicTitle, 20);
                    }
                }
                else if (position == 2)
                {//同业学堂焦点图片
                    if (parameter == 0)
                    {
                        strPictureInfo = Domain.FileSystem + bll_NewsPicInfo.GetModel().SchoolFocusPic.PicPath;
                    }
                    else if (parameter == 1)
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().SchoolFocusPic.PicUrl;
                    }
                    else
                    {
                        strPictureInfo = EyouSoft.Common.Utils.GetText(bll_NewsPicInfo.GetModel().SchoolFocusPic.PicTitle, 20);
                    }
                }
                else if (position == 3)
                {//同业学堂右侧广告
                    if (parameter == 0)
                    {
                        strPictureInfo = Domain.FileSystem + bll_NewsPicInfo.GetModel().SchoolRightPic.PicPath;
                    }
                    else if (parameter == 1)
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().SchoolRightPic.PicUrl;
                    }
                    else
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().SchoolRightPic.PicTitle;
                    }
                }
                else
                {//同行社区
                    if (parameter == 0)
                    {
                        strPictureInfo = Domain.FileSystem + bll_NewsPicInfo.GetModel().CommunityPic.PicPath;
                    }
                    else if (parameter == 1)
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().CommunityPic.PicUrl;
                    }
                    else
                    {
                        strPictureInfo = bll_NewsPicInfo.GetModel().CommunityPic.PicTitle;
                    }
                }
            }
            return strPictureInfo;
        }
        #endregion

        #region 实时供求
        /// <summary>
        /// 实时供求
        /// </summary>
        protected void GetSupplyList()
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> ExchangeList = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance().GetTopList(8, null, 0, null);
            if (ExchangeList != null && ExchangeList.Count > 0)
            {
                StringBuilder strAdvList = new StringBuilder();
                strAdvList.Append("<div class=\"mulitline\" style=\"height:85px; overflow:hidden; position:relative\">");
                //对数据进行过滤
                foreach (EyouSoft.Model.CommunityStructure.ExchangeList item in ExchangeList)
                {//将数据添加到列表集中去
                    strAdvList.AppendFormat("<a href='{0}' title='{2}' target=\"_blank\">{1}</a>", EyouSoft.Common.URLREWRITE.SupplierInfo.InfoUrlWrite(item.ID, CityId), "[" + item.IssueTime.ToString("MM-dd") + "]" + "&nbsp;&nbsp;" + EyouSoft.Common.Utils.GetText(item.ExchangeTitle, 13), item.ExchangeTitle);
                }
                strAdvList.Append("</div>");
                strAllSupplyList = strAdvList.ToString();
            }
            else
            {
                strAllSupplyList = "<div class=\"mulitline\" style=\"height:85px; overflow:hidden; position:relative\"><a></a><a></a><a>暂无供求信息</a><a></a><a></a></div>";
            }
        }
        #endregion

        #region 获取信息类别
        /// <summary>
        /// 获取信息类别
        /// </summary>
        protected void GetNewsType()
        {
            #region 获取资讯信息类别

            EyouSoft.BLL.NewsStructure.NewsType BLL_NewsType = new EyouSoft.BLL.NewsStructure.NewsType();
            //获取资讯信息类别
            IList<EyouSoft.Model.NewsStructure.NewsType> InfoTypeList = BLL_NewsType.GetInformationType();
            if (InfoTypeList != null && InfoTypeList.Count > 0)
            {
                int intInfos = 0;
                StringBuilder strTypeLists = new StringBuilder();
                foreach (EyouSoft.Model.NewsStructure.NewsType items in InfoTypeList)
                {
                    intInfos += 1;
                    if (intInfos == 1)
                    {
                        strTypeLists.AppendFormat("<a href='{0}' title='{1}'>{1}</a>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(items.Id), items.ClassName);
                    }
                    else
                    {
                        strTypeLists.AppendFormat(" ┊ <a href='{0}' title='{1}'>{1}</a>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(items.Id), items.ClassName);
                    }
                    //绑定绑定行业新闻
                    if (items.ClassName == "行业新闻")
                    {
                        GetTradeNewsList(items.Id, items.ClassName);
                    }
                    //绑定嘉宾访谈
                    if (items.ClassName == "嘉宾访谈")
                    {
                        GetInterviewList(items.Id, items.ClassName);
                    }
                    //绑定展会大全
                    if (items.ClassName == "旅游展会")
                    {
                        GetExpositionList(items.Id, items.ClassName);
                    }
                }
                ITypeLists = strTypeLists.ToString();
            }

            #endregion

            #region 获取同堂类别
            //获取同堂类别
            IList<EyouSoft.Model.NewsStructure.NewsType> SchoolTypeList = BLL_NewsType.GetSchoolType();
            if (SchoolTypeList != null && SchoolTypeList.Count > 0)
            {
                int intSchools = 0;
                StringBuilder strSchoolTypes = new StringBuilder();
                foreach (EyouSoft.Model.NewsStructure.NewsType items in SchoolTypeList)
                {
                    intSchools += 1;
                    if (intSchools == 1)
                    {
                        strSchoolTypes.AppendFormat("<a href='{0}' title='{1}'>{1}</a>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(items.Id), items.ClassName);
                    }
                    else
                    {
                        strSchoolTypes.AppendFormat(" ┊ <a href='{0}' title='{1}'>{1}</a>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(items.Id), items.ClassName);
                    }
                    //绑定旅游营销
                    if (items.ClassName == "旅游营销")
                    {
                        GetTourMarketingList(items.Id, items.ClassName);
                    }
                    //绑定企业经营与管理
                    if (items.ClassName == "旅行社经营管理")
                    {
                        GetCompanyManagementList(items.Id, items.ClassName);
                    }
                    //绑定政策法规
                    if (items.ClassName == "旅游政策法规")
                    {
                        GetStartList(items.Id, items.ClassName);
                    }
                    //计调指南
                    if (items.ClassName == "计调指南")
                    {
                        GetTuningmeterList(items.Id, items.ClassName);
                    }
                    if (items.ClassName == "导游带团")
                    {
                        GetCiceroneList(items.Id, items.ClassName);
                    }
                }
                STypeLists = strSchoolTypes.ToString();
            }
            #endregion
        }

        #endregion

        #region 获取热点资讯排行
        /// <summary>
        /// 获取热点资讯排行
        /// </summary>
        protected void GetNewsOrdeyBy()
        {
            //获取热点资讯排行[7条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> HotList = bll_News.GetHotNews(7);
            StringBuilder strHotList = new StringBuilder();
            if (HotList != null && HotList.Count > 0)
            {
                strHotList.Append("<h3>热点资讯排行</h3><ul>");
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in HotList)
                {
                    if (item.GotoUrl.Length > 0)
                    {//含有跳转的
                        strHotList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 20), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {//不含跳转
                        strHotList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 20), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
                strHotList.Append("</ul>");
            }
            else
            {
                strHotList.Append("<ul><li>暂无资讯排行信息</li></ul>");
            }
            HotLists = strHotList.ToString();
        }
        #endregion

        #region 行业新闻
        /// <summary>
        /// 行业新闻
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetTradeNewsList(int TypeID, string strTypeName)
        {
            //获取行业新闻[6条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> TradeNews = bll_News.GetListByNewType(6, TypeID);
            //遍历
            StringBuilder strTrdeNewsList = new StringBuilder();
            if (TradeNews != null && TradeNews.Count > 0)
            {
                strTrdeNewsList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in TradeNews)
                {
                    if (item.GotoUrl.Length > 0)
                    {//跳转
                        strTrdeNewsList.AppendFormat("<li><span>{0}</span><a href='{1}' title='{4}' target=\"_blank\" style='{3}'>{2}</a></li>", item.IssueTime.ToShortDateString(), item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {//正常
                        strTrdeNewsList.AppendFormat("<li><span>{0}</span><a href='{1}' title='{4}' target=\"_blank\" style='{3}'>{2}</a></li>", item.IssueTime.ToShortDateString(), EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
                strTrdeNewsList.Append("</ul>");
            }
            else
            {
                strTrdeNewsList.Append("<ul><li>暂无行业新闻信息</li></ul>");
            }
            TradeNewsList = strTrdeNewsList.ToString();
        }
        #endregion

        #region 行业新闻下前几条
        /// <summary>
        ///  行业新闻下前几条
        /// </summary>
        protected void GetTradeBottenList()
        {
            //过去行业信息列表[9条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> TradeBottenList = bll_News.GetListByNewCate(9);
            StringBuilder strTradeBotten = new StringBuilder();
            if (TradeBottenList != null && TradeBottenList.Count > 0)
            {
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in TradeBottenList)
                {
                    int intNewClassLeach = item.ClassName.Length - 2;
                    int NewsTitleLeach = 19 - intNewClassLeach;

                    if (item.GotoUrl.Length > 0)
                    {
                        strTradeBotten.AppendFormat("<li><span>{0}</span><a href='{1}' title='{2}' class=\"cl\">{2}</a> <a href='{3}' title='{6}' target=\"_blank\" style='{5}'>{4}</a></li>", item.IssueTime.ToShortDateString(), EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(item.AfficheClass), "[" + item.ClassName + "]", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, NewsTitleLeach), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {
                        strTradeBotten.AppendFormat("<li><span>{0}</span><a href='{1}' title='{2}' class=\"cl\">{2}</a> <a href='{3}' title='{6}' target=\"_blank\" style='{5}'>{4}</a></li>", item.IssueTime.ToShortDateString(), EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(item.AfficheClass), "[" + item.ClassName + "]", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, NewsTitleLeach), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
            }
            else
            {
                strTradeBotten.Append("<ul><li>暂无资讯信息</li></ul>");
            }
            TradeBottenLists = strTradeBotten.ToString();
        }
        #endregion

        #region 绑定嘉宾访谈
        /// <summary>
        /// 绑定嘉宾访谈
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetInterviewList(int TypeID, string strTypeName)
        {
            //获取嘉宾访谈数据集[4条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> InterviewList = bll_News.GetListByNewType(4, TypeID);
            //遍历
            int intInterView = 0;
            StringBuilder strInterviewList = new StringBuilder();
            if (InterviewList != null && InterviewList.Count > 0)
            {
                strInterviewList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListZhuanTiUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in InterviewList)
                {
                    intInterView += 1;
                    if (intInterView == 1)
                    {//含有图片
                        if (!string.IsNullOrEmpty(item.PicPath))
                        {
                            if (!string.IsNullOrEmpty(item.GotoUrl))
                            {
                                strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            }
                            else
                            {
                                strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            }
                            //if (item.GotoUrl.Length > 0)
                            //{//含有跳转

                            //    strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            //}
                            //else
                            //{
                            //    strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            //}
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item.GotoUrl))
                            {
                                strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.ServerComponents + "/images/nologo/nologo92_84.gif", EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            }
                            else
                            {
                                strInterviewList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"72px\" height=\"76px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.ServerComponents + "/images/nologo/nologo92_84.gif", EyouSoft.Common.Utils.GetText(item.AfficheDesc, 33, true), "color:" + item.TitleColor, item.AfficheTitle);
                            }
                        }

                    }
                    else
                    {
                        if (item.GotoUrl.Length > 0)
                        {//含有跳转数据
                            strInterviewList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strInterviewList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                }
                strInterviewList.Append("</ul>");
            }
            else
            {
                strInterviewList.Append("<ul><li>暂无嘉宾访谈信息</li></ul>");
            }
            InterviewLists = strInterviewList.ToString();
        }

        #endregion

        #region 绑定展会大全
        /// <summary>
        /// 绑定展会大全
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetExpositionList(int TypeID, string strTypeName)
        {
            //获取展会大全数据集[5条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> ExpositionList = bll_News.GetListByNewType(6, TypeID);
            //遍历
            int intExposition = 0;
            StringBuilder strExpositionList = new StringBuilder();
            if (ExpositionList != null && ExpositionList.Count > 0)
            {
                strExpositionList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3>\r<ul>\r", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in ExpositionList)
                {
                    intExposition += 1;
                    if (intExposition == 1)
                    {//图片1
                        if (item.GotoUrl.Length > 0)
                        {
                            strExpositionList.AppendFormat("<li class=\"noL\">\r<ul>\r<li>\r<a href='{0}' title='{4}' target=\"_blank\">\r<img src='{2}' width=\"117px\" height=\"58px\" />\r</a>\r<a href='{0}' title='{4}' style='{3}'>{1}</a>\r</li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strExpositionList.AppendFormat("<li class=\"noL\">\r<ul>\r<li>\r<a href='{0}' title='{4}' target=\"_blank\">\r<img src='{2}' width=\"117px\" height=\"58px\" />\r</a>\r<a href='{0}' title='{4}' style='{3}'>{1}</a>\r</li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                    else if (intExposition == 2)
                    {//图片2
                        if (item.GotoUrl.Length > 0)
                        {
                            strExpositionList.AppendFormat("<li>\r<a href='{0}' title='{4}' target=\"_blank\">\r<img src='{2}' width=\"117px\" height=\"58px\" />\r</a>\r<a href='{0}' title='{4}' style='{3}'>{1}</a>\r</li>\r</ul>\r</li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strExpositionList.AppendFormat("<li>\r<a href='{0}' title='{4}' target=\"_blank\">\r<img src='{2}' width=\"117px\" height=\"58px\" />\r</a>\r<a href='{0}' title='{4}' style='{3}'>{1}</a>\r</li>\r</ul>\r</li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, "color:" + item.TitleColor, item.AfficheTitle);
                        }

                    }
                    else
                    {
                        if (item.GotoUrl.Length > 0)
                        {//含有跳转的
                            strExpositionList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {//不含有跳转
                            strExpositionList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                }
                strExpositionList.Append("</ul>");
            }
            else
            {
                strExpositionList.Append("<ul><li>暂无展会大全信息</li></ul>");
            }
            ExpositionLists = strExpositionList.ToString();
        }

        #endregion

        #region 最新线路信息
        /// <summary>
        /// 最新线路信息
        /// </summary>
        private void GetTourList()
        {
            //获取最新线路信息[10条]
            IList<EyouSoft.Model.NewTourStructure.MRoute> list =
                EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance().GetNewRouteList(10);
            var strAdvList = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                strAdvList.AppendFormat("<h3><a href='{0}' title='{1}' class=\"publish\">{1}</a>最新发布旅游线路</h3><ul>",
                                        Domain.UserBackCenter + "/routeagency/routemanage/rmdefault.aspx", "发布线路");
                foreach (var item in list)
                {
                    strAdvList.AppendFormat(
                        "<li><span>{0}</span><a href='{1}' title='{3}' target=\"_blank\">{2}</a></li>",
                        item.RetailAdultPrice > 0 ? "<b>￥</b>" + item.RetailAdultPrice.ToString("F0") : string.Empty,
                        EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(item.Id, CityId) + "?ReturnUrl=" + ReturnUrl,
                        Utils.GetText2(item.RouteName, 16, false), item.RouteName);

                }
                strAdvList.AppendFormat("</ul><div class=\"line\"></div><span class=\"more\"><a href='{0}' title='更多'>更多></a></span>", Domain.UserPublicCenter + "/RouteManage/Default.aspx");
            }
            else
            {
                strAdvList.Append("<li>暂无最新线路信息</li>");
            }
            NewestRouteList = strAdvList.ToString();
        }
        #endregion

        #region 旅游营销
        /// <summary>
        /// 旅游营销
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetTourMarketingList(int TypeID, string strTypeName)
        {
            //获取旅游营销数据集[6条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> TourMarketingList = bll_News.GetListByNewType(6, TypeID);
            //遍历
            StringBuilder strTourMarketing = new StringBuilder();
            if (TourMarketingList != null && TourMarketingList.Count > 0)
            {
                strTourMarketing.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in TourMarketingList)
                {
                    if (item.GotoUrl.Length > 0)
                    {//带有跳转的数据集
                        strTourMarketing.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {
                        strTourMarketing.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
                strTourMarketing.Append("</ul>");
            }
            else
            {//无数据集
                strTourMarketing.Append("<ul><li>暂无旅游营销信息</li></ul>");
            }
            TourMarketingLists = strTourMarketing.ToString();
        }
        #endregion

        #region 企业经营与管理
        /// <summary>
        /// 企业经营与管理
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetCompanyManagementList(int TypeID, string strTypeName)
        {
            //获取企业经营与管理数据集[6条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> CompanyManagement = bll_News.GetListByNewType(6, TypeID);
            //遍历
            StringBuilder strCompanyManagement = new StringBuilder();
            if (CompanyManagement != null && CompanyManagement.Count > 0)
            {
                strCompanyManagement.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in CompanyManagement)
                {
                    if (item.GotoUrl.Length > 0)
                    {//跳转
                        strCompanyManagement.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {
                        strCompanyManagement.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
                strCompanyManagement.Append("</ul>");
            }
            else
            {//无数据集
                strCompanyManagement.Append("<ul><li>暂无企业经营与管理信息</li></ul>");
            }
            CompanyManagementLists = strCompanyManagement.ToString();
        }
        #endregion

        #region 旅游政策法规
        /// <summary>
        /// 旅游政策法规
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetStartList(int TypeID, string strTypeName)
        {
            //获取旅游政策法规数据集[6条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> StartList = bll_News.GetListByNewType(6, TypeID);
            //遍历
            StringBuilder strStartList = new StringBuilder();
            if (StartList != null && StartList.Count > 0)
            {
                strStartList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in StartList)
                {
                    if (item.GotoUrl.Length > 0)
                    {//跳转
                        strStartList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {//正常
                        strStartList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 22), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }
                strStartList.Append("</ul>");
            }
            else
            {//无数据集
                strStartList.Append("<ul><li>暂无旅游政策法规信息</li></ul>");
            }
            StartLists = strStartList.ToString();
        }
        #endregion

        #region 计调指南
        /// <summary>
        /// 计调指南
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetTuningmeterList(int TypeID, string strTypeName)
        {
            //获取计调指南程序集[6条]
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> TuningmeterList = bll_News.GetListByNewType(6, TypeID);
            //遍历
            int intTuning = 0;
            StringBuilder strTuningmeterList = new StringBuilder();
            if (TuningmeterList != null && TuningmeterList.Count > 0)
            {
                strTuningmeterList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in TuningmeterList)
                {
                    intTuning += 1;
                    if (intTuning == 1)
                    {//图片
                        if (item.GotoUrl.Length > 0)
                        {
                            strTuningmeterList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"92px\" height=\"67px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 25, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strTuningmeterList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{5}' width=\"92px\" height=\"67px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 25, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                    else
                    {
                        if (item.GotoUrl.Length > 0)
                        {//跳转
                            strTuningmeterList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {//正常
                            strTuningmeterList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                }
                strTuningmeterList.Append("</ul>");
            }
            else
            {//无数据集
                strTuningmeterList.Append("<ul>暂无计调指南信息</li></ul>");
            }
            TuningmeterLists = strTuningmeterList.ToString();
        }
        #endregion

        #region 导游带团
        /// <summary>
        /// 导游带团
        /// </summary>
        /// <param name="TypeID">类别ID</param>
        /// <param name="strTypeName">类别名称</param>
        protected void GetCiceroneList(int TypeID, string strTypeName)
        {
            //获取导游带团数据集
            EyouSoft.BLL.NewsStructure.NewsBll bll_News = new EyouSoft.BLL.NewsStructure.NewsBll();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> CiceroneList = bll_News.GetListByNewType(6, TypeID);
            //遍历
            int intCiceroneList = 0;
            StringBuilder strCiceroneList = new StringBuilder();
            if (CiceroneList != null && CiceroneList.Count > 0)
            {
                strCiceroneList.AppendFormat("<h3>{1}<span><a href='{0}' title='{1}'>更多></a></span></h3><ul>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsListUrl(TypeID), strTypeName);
                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in CiceroneList)
                {
                    intCiceroneList += 1;
                    if (intCiceroneList == 1)
                    {//图片
                        if (item.GotoUrl.Length > 0)
                        {
                            strCiceroneList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{1}'  width=\"93px\" height=\"70px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 25, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {
                            strCiceroneList.AppendFormat("<li class=\"noL\"><a href='{0}' title='{5}' target=\"_blank\"><img src='{2}' alt='{1}'  width=\"93px\" height=\"70px\" /><h3 class=\"m\" style='{4}'>{1}</h3><p>{3}</p></a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 9), Domain.FileSystem + item.PicPath, EyouSoft.Common.Utils.GetText(item.AfficheDesc, 25, true), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                    else
                    {
                        if (item.GotoUrl.Length > 0)
                        {//跳转
                            strCiceroneList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                        else
                        {//正常
                            strCiceroneList.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText(item.AfficheTitle, 15), "color:" + item.TitleColor, item.AfficheTitle);
                        }
                    }
                }
                strCiceroneList.Append("</ul>");
            }
            else
            {//无数据集
                strCiceroneList.Append("<ul><li>暂无导游带团信息</li></ul>");
            }
            CiceroneLists = strCiceroneList.ToString();
        }
        #endregion

        #region 最新加盟企业
        /// <summary>
        /// 最新加盟企业
        /// </summary>
        protected void GetJoininNews()
        {
            //获取最新加盟企业[9条]
            EyouSoft.BLL.CompanyStructure.CompanyInfo Joininnew = new EyouSoft.BLL.CompanyStructure.CompanyInfo();
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> JoininnewList = Joininnew.GetTopNumNewCompanys(9, null);
            StringBuilder strJoininnewslist = new StringBuilder();
            if (JoininnewList != null && JoininnewList.Count > 0)
            {
                strJoininnewslist.Append("<h3>最新加盟企业</h3><ul>");
                foreach (EyouSoft.Model.CompanyStructure.Company item in JoininnewList)
                {
                    strJoininnewslist.AppendFormat("<li><a href='{0}' title='{2}' target=\"_blank\">{1}</a></li>", EyouSoft.Common.Utils.GetDomainByCompanyId(item.ID, CityId), EyouSoft.Common.Utils.GetText(item.CompanyName, 17), item.CompanyName);
                }
                strJoininnewslist.Append("</ul>");
            }
            else
            {
                strJoininnewslist.Append("<ul><li>暂无最新加盟企业信息</li></ul>");
            }

            JoininNews = strJoininnewslist.ToString();
        }
        #endregion
    }
}