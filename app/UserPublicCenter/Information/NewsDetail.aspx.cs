using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace UserPublicCenter.Information
{
    /// <summary>
    /// 新闻详细页
    /// 作者：xuqh 2011-4-7
    /// </summary>
    public partial class NewsDetail : EyouSoft.Common.Control.FrontPage
    {
        private readonly EyouSoft.BLL.NewsStructure.NewsBll bll = new EyouSoft.BLL.NewsStructure.NewsBll();
        private EyouSoft.BLL.NewsStructure.NewsType newTypeBll = new EyouSoft.BLL.NewsStructure.NewsType();


        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageIndex = 1;
        protected int pageSize = 1;
        protected int recordCount = 1;

        protected EyouSoft.Model.NewsStructure.WebSiteNews model = null;
        protected EyouSoft.Model.NewsStructure.BasicNews preModel = null;
        protected EyouSoft.Model.NewsStructure.BasicNews nextModel = null;

        //大类名称
        protected string cateName = string.Empty;
        //小类名称
        protected string className = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                InitNewsDetail();
                InitRelatedNews();
                InitRecommendNews();
            }
        }

        /// <summary>
        /// 初始化详细资讯
        /// </summary>
        private void InitNewsDetail()
        {
            if (Request["TypeId"] != null)
            {
                //存在资讯大类
                if (newTypeBll.GetCategoryById(int.Parse(Request["TypeId"].ToString())) != null)
                    cateName = ((EyouSoft.Model.NewsStructure.NewsCategory)newTypeBll.GetCategoryById(int.Parse(Request["TypeId"].ToString()))).ToString();
                className = newTypeBll.GetNewsTypeName(int.Parse(Request["TypeId"].ToString()));
            }
            if(Request["NewsId"] != null)
            {
                bll.SetClicks(int.Parse(Request["NewsId"].ToString()));

                model = bll.GetModel(int.Parse(Request["NewsId"].ToString()), pageIndex);

                if (model != null)
                {
                    recordCount = model.AffichePageNum;

                    //新闻内容总数为1，则不分页
                    if (recordCount == 1)
                    {
                        ExportPageInfo.Visible = false;
                    }

                    preModel = model.PrevNewsInfo;
                    nextModel = model.NextNewsInfo;

                    AddPageMetaTag(model.NewsKeyWordItem,model.AfficheDesc);

                    this.Page.Title = String.Format(EyouSoft.Common.PageTitle.Information_Detail_Title, model.AfficheTitle, className);

                    BindPage();
                }
            }
        }

        /// <summary>
        /// 添加页面Meta关键字
        /// </summary>
        /// <param name="lsTags">tag集合</param>
        private void AddPageMetaTag(IList<EyouSoft.Model.NewsStructure.NewsSubItem> lsTags,string Des)
        {
            string keyValue = string.Empty;

            if (lsTags != null && lsTags.Count > 0)
            {
                for (int i = 0; i < lsTags.Count; i++)
                {
                    keyValue += lsTags[i].ItemName + ",";
                }

                keyValue = keyValue.Substring(0, keyValue.Length - 1);
            }

            AddMetaTag("keywords", keyValue);
            AddMetaTag("description", Des);
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPage()
        {
            this.ExportPageInfo.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ContentStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfo.IsUrlRewrite = true;
                this.ExportPageInfo.Placeholder = "#PageIndex#";
                this.ExportPageInfo.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfo.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfo.UrlParams = Request.QueryString;
            }
            this.ExportPageInfo.intPageSize = pageSize;
            this.ExportPageInfo.CurrencyPage = pageIndex;
            this.ExportPageInfo.intRecordCount = recordCount;
        }

        /// <summary>
        /// 初始化相关资讯
        /// </summary>
        private void InitRelatedNews()
        {
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> ls = new List<EyouSoft.Model.NewsStructure.WebSiteNews>();

            if(Request["NewsId"] != null)
            {
                ls = bll.GetRelatedList(7, int.Parse(Request["NewsId"].ToString()));

                if (ls != null && ls.Count > 0)
                {
                    rptRelatedNews.DataSource = ls;
                    rptRelatedNews.DataBind();
                }
                else
                {
                    NoInfo(true, false);
                }
            }
        }

        /// <summary>
        /// 初始化推荐资讯
        /// </summary>
        private void InitRecommendNews()
        {
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> ls = new List<EyouSoft.Model.NewsStructure.WebSiteNews>();

            if (Request["NewsId"] != null)
            {
                ls = bll.GetRecommendList(5, true);

                if (ls != null && ls.Count > 0)
                {
                    rptRecommendNews.DataSource = ls;
                    rptRecommendNews.DataBind();
                }
                else
                {
                    NoInfo(false, true);
                }
            }
        }

        /// <summary>
        /// 当没有消息时显示的内容
        /// </summary>
        /// <param name="hasRelateNews">true表示没有</param>
        /// <param name="hasRecommendNews">true表示没有</param>
        private void NoInfo(bool hasRelateNews, bool hasRecommendNews)
        {
            this.NoRelateNews.Visible = hasRelateNews;
            this.NoRelateNews.Text = "没有相关资讯...";
            this.NoRecommendNews.Visible = hasRecommendNews;
            this.NoRecommendNews.Text = "没有推荐资讯...";
        }
    }
}
