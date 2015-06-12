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
    /// 资讯带头条的详细列表
    /// 作者：xuqh 2011-4-2
    /// </summary>
    public partial class NewsList : EyouSoft.Common.Control.FrontPage
    {
        protected IList<EyouSoft.Model.NewsStructure.NewsModel> lsNews = null;
        private EyouSoft.BLL.NewsStructure.NewsBll bll = new EyouSoft.BLL.NewsStructure.NewsBll();
        private EyouSoft.BLL.NewsStructure.NewsType newTypeBll = new EyouSoft.BLL.NewsStructure.NewsType();

        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageSize = 30;
        protected int pageIndex = 1;
        int recordCount = 0;

        //大类名称
        protected string cateName = string.Empty;
        //小类名称
        protected string className = string.Empty;
        //查询实体
        EyouSoft.Model.NewsStructure.SearchOrderInfo queryModel = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }

            if (!IsPostBack)
            {

                if (Request["typeId"] != null || Request["TagId"] != null)
                {
                    if(pageIndex == 1)
                        InitTopNews(Request["typeId"] == null ? int.Parse(Request["TagId"].ToString()) : int.Parse(Request["typeId"].ToString()));

                    InitNewsList(Request["typeId"] == null ? int.Parse(Request["TagId"].ToString()) : int.Parse(Request["typeId"].ToString()));
                }
            }
        }

        /// <summary>
        /// 初始化头两条资讯
        /// </summary>
        /// <param name="typeId"></param>
        private void InitTopNews(int typeId)
        {
            IList<EyouSoft.Model.NewsStructure.NewsModel> lsTopNews = new List<EyouSoft.Model.NewsStructure.NewsModel>();

            //如果为关键字列表
            if (Request["TagId"] != null)
            {
                if (newTypeBll.GetCategoryById(typeId) != null)
                {
                    EyouSoft.Model.NewsStructure.NewsCategory cateType = (EyouSoft.Model.NewsStructure.NewsCategory)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.NewsCategory), newTypeBll.GetCategoryById(typeId).ToString());
                    lsTopNews = bll.GetList(2, cateType, EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯, typeId, null);
                }
            }
            else
            {
                if (newTypeBll.GetCategoryById(typeId) != null)
                {
                    EyouSoft.Model.NewsStructure.NewsCategory cateType = (EyouSoft.Model.NewsStructure.NewsCategory)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.NewsCategory), newTypeBll.GetCategoryById(typeId).ToString());
                    lsTopNews = bll.GetList(2, cateType, EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯,null,typeId);
                }
            }

            //如果有数据
            if (lsTopNews != null && lsTopNews.Count > 0)
            {
                rptTopNews.DataSource = lsTopNews;
                rptTopNews.DataBind();
            }
        }

        /// <summary>
        /// 初始化资讯列表
        /// </summary>
        private void InitNewsList(int typeId)
        {
            lsNews = new List<EyouSoft.Model.NewsStructure.NewsModel>();

            //如果是关键字列表
            if (Request["TagId"] != null)
            {
                string tagName = string.Empty;
                EyouSoft.BLL.NewsStructure.TagKeyInfo tagBll = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
                EyouSoft.Model.NewsStructure.TagKeyInfo tagModel = tagBll.GetModel(int.Parse(Request["TagId"].ToString()), EyouSoft.Model.NewsStructure.ItemCategory.Tag);

                if(tagModel!=null)
                    tagName = tagModel.ItemName;

                this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_List_Title, tagName + "-相关内容");

                //cateName = tagName == "" ? "行业资讯":tagName;
                cateName = "行业资讯";
                EyouSoft.Model.NewsStructure.SearchOrderInfo queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
                queryModel.Tag = typeId;
                lsNews = bll.GetList(pageSize, pageIndex, ref recordCount,queryModel);
            }
            else
            {
                //type<0表示是大类
                if (typeId < 0)
                {
                    queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
                    queryModel.Category = (EyouSoft.Model.NewsStructure.NewsCategory)(-typeId);
                    lsNews = bll.GetList(pageSize, pageIndex, ref recordCount,queryModel);
                    cateName = queryModel.Category.ToString();
                    this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_List_Title, cateName);
                }
                else
                {
                    cateName = newTypeBll.GetCategoryById(typeId) == null ? "" : ((EyouSoft.Model.NewsStructure.NewsCategory)newTypeBll.GetCategoryById(typeId)).ToString();
                    this.Page.Title = cateName;
                    className = newTypeBll.GetNewsTypeName(typeId).ToString();
                    this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_List_Title, className);

                    queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
                    queryModel.Type = typeId;
                    lsNews = bll.GetList(pageSize, pageIndex, ref recordCount,queryModel);
                }
            }

            AddMetaTag("keywords", string.Format(EyouSoft.Common.PageTitle.Information_List_Keywords, cateName == "" ? className : cateName));
            AddMetaTag("description", string.Format(EyouSoft.Common.PageTitle.Information_List_Des, cateName == "" ? className : cateName));

            //如果有数据
            if (lsNews != null && lsNews.Count > 0)
            {
                rptNewsList.DataSource = lsNews;
                rptNewsList.DataBind();

                BindPage();
                BindUpPage();
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPage()
        {
            this.ExportPageInfo.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

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
        /// 分页
        /// </summary>
        protected void BindUpPage()
        {
            this.ExportPageInfoUp.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfoUp.IsUrlRewrite = true;
                this.ExportPageInfoUp.Placeholder = "#PageIndex#";
                this.ExportPageInfoUp.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfoUp.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfoUp.UrlParams = Request.QueryString;
            }
            this.ExportPageInfoUp.intPageSize = pageSize;
            this.ExportPageInfoUp.CurrencyPage = pageIndex;
            this.ExportPageInfoUp.intRecordCount = recordCount;
        }

    }
}
