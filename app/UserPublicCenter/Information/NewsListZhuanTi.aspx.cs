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
    /// 专题列表
    /// 作者：xuqh 2011-4-7
    /// </summary>
    public partial class NewsListZhuanTi : EyouSoft.Common.Control.FrontPage
    {
        private EyouSoft.BLL.NewsStructure.NewsType newTypeBll = new EyouSoft.BLL.NewsStructure.NewsType();

        /// <summary>
        /// 分页设置
        /// </summary> 
        protected int pageSize = 30;
        protected int pageIndex = 1;
        protected int recordCount = 0;

        //大类名称
        protected string cateName = string.Empty;
        //小类名称
        protected string className = string.Empty;
        //查询实体
        EyouSoft.Model.NewsStructure.SearchOrderInfo queryModel = null;

        protected IList<EyouSoft.Model.NewsStructure.NewsModel> lsNews = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]) > 1)
            {
                pageIndex = EyouSoft.Common.Utils.GetInt(Request.QueryString["Page"]);
            }
            if (!IsPostBack)
            {
                if (Request["typeId"] != null)
                {
                    InitList(int.Parse(Request["typeId"].ToString()));
                }
            }
        }

        /// <summary>
        /// 初始化资讯列表
        /// </summary>
        private void InitList(int typeId)
        {
            EyouSoft.BLL.NewsStructure.NewsBll bll = new EyouSoft.BLL.NewsStructure.NewsBll();
            lsNews = new List<EyouSoft.Model.NewsStructure.NewsModel>();

            //typeId=0表示是推荐资讯列表
            if (typeId == 0)
            {
                cateName = "推荐资讯";
                queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
                queryModel.IsPic = true;
                lsNews = bll.GetList(pageSize, pageIndex, ref recordCount,queryModel);
            }
            //不为0表示专题列表
            else
            {
                if (newTypeBll.GetCategoryById(typeId) != null)
                {
                    cateName = ((EyouSoft.Model.NewsStructure.NewsCategory)newTypeBll.GetCategoryById(typeId)).ToString();
                    className = newTypeBll.GetNewsTypeName(typeId).ToString();
                }

                queryModel = new EyouSoft.Model.NewsStructure.SearchOrderInfo();
                queryModel.Type = typeId;
                lsNews = bll.GetList(pageSize, pageIndex, ref recordCount, queryModel);

            }

            if (lsNews != null && lsNews.Count > 0)
            {
                rptList.DataSource = lsNews;
                rptList.DataBind();

                BindPageDown();
                BindPageUp();
            }
            else
            {
                this.ExportPageInfoDown.Visible = false;
                this.ExportPageInfoUp.Visible = false;
            }

            this.Page.Title = string.Format(EyouSoft.Common.PageTitle.Information_List_Title,cateName==""?className:cateName);
            AddMetaTag("keywords",string.Format(EyouSoft.Common.PageTitle.Information_List_Keywords,cateName==""?className:cateName));
            AddMetaTag("description",string.Format(EyouSoft.Common.PageTitle.Information_List_Des,cateName==""?className:cateName));
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPageDown()
        {
            this.ExportPageInfoDown.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.ClassicStyle;

            if (EyouSoft.Common.URLREWRITE.UrlReWriteUtils.IsReWriteUrl(Request))
            {
                this.ExportPageInfoDown.IsUrlRewrite = true;
                this.ExportPageInfoDown.Placeholder = "#PageIndex#";
                this.ExportPageInfoDown.PageLinkURL = EyouSoft.Common.URLREWRITE.UrlReWriteUtils.GetUrlForPage(Request) + "_#PageIndex#";
            }
            else
            {
                this.ExportPageInfoDown.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExportPageInfoDown.UrlParams = Request.QueryString;
            }
            this.ExportPageInfoDown.intPageSize = pageSize;
            this.ExportPageInfoDown.CurrencyPage = pageIndex;
            this.ExportPageInfoDown.intRecordCount = recordCount;
        }

        /// <summary>
        /// 分页
        /// </summary>
        protected void BindPageUp()
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

    public class ZiXun
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Img { get; set; }

        public DateTime NewsTime { get; set; }

        public string FromWhere { get; set; }
    }
}
