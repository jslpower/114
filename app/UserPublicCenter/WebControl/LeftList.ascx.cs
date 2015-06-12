using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace UserPublicCenter.WebControl
{
    /// <summary>
    /// 陈蝉鸣 
    /// 2011-5-12
    /// 寻找同行右边列表
    /// </summary>
    /// 
    public partial class LeftList : System.Web.UI.UserControl
    {

        protected string HotLists;//热点资讯拼接字符串
        protected string Imagepath;//图片服务器路径
        protected string newStr;//行业新闻变化拼接字符串
        protected int CityId;//用户所在的城市id
        public string RouteList;//最新发布线路
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Utils.GetQueryStringValue("CityId") != "")
                {
                    CityId = Convert.ToInt32(Utils.GetQueryStringValue("CityId"));
                }
                Imagepath = ImageManage.GetImagerServerUrl(1);
                Initialize();
            }


        }

        //初始化页面信息
        public void Initialize()
        {

            //获取热点资讯排行
            EyouSoft.IBLL.NewsStructure.INewsBll bll_News = EyouSoft.BLL.NewsStructure.NewsBll.CreateInstance();
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> HotList = bll_News.GetHotNews(10);
            StringBuilder strHot = new StringBuilder();

            if (HotList != null && HotList.Count > 0)
            {

                foreach (EyouSoft.Model.NewsStructure.WebSiteNews item in HotList)
                {
                    if (item.GotoUrl.Length > 0)
                    {//含有跳转的
                        strHot.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", item.GotoUrl, EyouSoft.Common.Utils.GetText2(item.AfficheTitle, 16, true), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                    else
                    {//不含跳转
                        strHot.AppendFormat("<li><a href='{0}' title='{3}' target=\"_blank\" style='{2}'>{1}</a></li>", EyouSoft.Common.URLREWRITE.Infomation.GetNewsDetailUrl(item.AfficheClass, item.Id), EyouSoft.Common.Utils.GetText2(item.AfficheTitle, 16, true), "color:" + item.TitleColor, item.AfficheTitle);
                    }
                }

            }
            else
            {
                strHot.Append("<li>暂无资讯排行信息</li>");
            }
            HotLists = strHot.ToString();

            //获取资讯信息类别
            EyouSoft.IBLL.NewsStructure.INewsType BLL_NewsType = EyouSoft.BLL.NewsStructure.NewsType.CreateInstance();
            IList<EyouSoft.Model.NewsStructure.NewsType> InfoTypeList = BLL_NewsType.GetInformationType();
            EyouSoft.Model.NewsStructure.NewsType only = InfoTypeList.Where(p => p.ClassName == "行业新闻").FirstOrDefault();
            //获取最新行业新闻                
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> Trades = bll_News.GetListByNewType(6, only.Id);
            this.news.DataSource = Trades;
            this.news.DataBind();
            bll_News = null;

            //获取供求信息
            EyouSoft.IBLL.CommunityStructure.IExchangeList Bchange = EyouSoft.BLL.CommunityStructure.ExchangeList.CreateInstance();
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> excList = Bchange.GetTopList(6);
            this.star.DataSource = excList;
            this.DataBind();

            //获取最新线路
            EyouSoft.IBLL.NewTourStructure.IRoute Btour = EyouSoft.BLL.NewTourStructure.BRoute.CreateInstance();
            IList<EyouSoft.Model.NewTourStructure.MRoute> TourList = Btour.GetNewRouteList(6);
            StringBuilder NewRoute = new StringBuilder();
            if (TourList != null)
            {
                foreach (EyouSoft.Model.NewTourStructure.MRoute item in TourList)
                {
                    NewRoute.Append(string.Format("<li><a title='{0}' target='_blank' href='{1}'>{2}</a></li>", item.RouteName, EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(item.Id, CityId), Utils.GetText2(item.RouteName, 16, true)));
                }
            } 
            RouteList = NewRoute.ToString();
            Btour = null;
            //获取旅游企业
            EyouSoft.IBLL.CompanyStructure.ICompanyInfo Bcompany = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance();
            IList<EyouSoft.Model.CompanyStructure.MLatestRegCompanyInfo> ComList = Bcompany.GetTopNumNewCompanys(9, true);
            this.company.DataSource = ComList;
            this.company.DataBind();
            Bcompany = null;
        }


    }
}