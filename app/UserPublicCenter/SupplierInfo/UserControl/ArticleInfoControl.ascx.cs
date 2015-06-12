using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 资讯详细控件
    /// </summary>
    /// 周文超  2010-08-02
    public partial class ArticleInfoControl : EyouSoft.Common.Control.BaseUserControl
    {
        /// <summary>
        /// 大类ID
        /// </summary>
        private int TopicClassId = 0;
        /// <summary>
        /// 小类ID
        /// </summary>
        private int TopicAreasId = 0;
        /// <summary>
        /// 资讯ID
        /// </summary>
        private string ArticleId = string.Empty;

        /// <summary>
        /// 行业资讯小类别数组，其他小类别属于同业学堂
        /// </summary>
        private int[] TopicAreasIds = new int[] { 1, 2, 3, 4, 5, 6, 7, 9 };

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticleId = Utils.InputText(Request.QueryString["Id"]);

            if (string.IsNullOrEmpty(ArticleId))
            {
                return;
            }

            if (!IsPostBack)
            {
                InitPageData();
                SetClicks();
            }
        }

        /// <summary>
        /// 更新浏览次数
        /// </summary>
        private void SetClicks()
        {
            if (Request.Cookies["Article" + ArticleId] == null)
            {
                bool Result = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().SetClicks(ArticleId);
                if (Result)
                {
                    HttpCookie ExchangeInfoCookie = new HttpCookie("Article" + ArticleId);
                    ExchangeInfoCookie.Expires = DateTime.Now.AddMinutes(5);
                    ExchangeInfoCookie.Path = "/SupplierInfo";
                    Response.Cookies.Add(ExchangeInfoCookie);
                }
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
            EyouSoft.IBLL.CommunityStructure.IInfoArticle ibll = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance();
            EyouSoft.Model.CommunityStructure.InfoArticle model = ibll.GetModel(ArticleId);
            if (model == null)
            {
                Utils.ShowError("未能找到您要查看的资讯！", "InfoArticle");
            }
            TopicClassId = (int)model.TopicClassId;
            TopicAreasId = (int)model.AreaId;

            this.Page.Title = model.ArticleTitle;
            System.Text.StringBuilder strA = new System.Text.StringBuilder();
            if (TopicClassId == 1)
            {
                strA.AppendFormat("<a href=\"/SupplierInfo/InfoArticle.aspx\">{0}</a>", Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicClass), TopicClassId));
            }
            else if (TopicClassId > 0)
            {
                strA.AppendFormat("<a href=\"/SupplierInfo/SchoolIntroduction.aspx\">{0}</a>", Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicClass), TopicClassId));
            }
            ltrTopicClass.Text = strA.ToString();

            //小类别要判断
            if (TopicAreasId > 0)
            {
                if (TopicAreasIds.Contains(TopicAreasId))
                    ltrTopIcArea.Text = string.Format(" > <a href=\"/SupplierInfo/ArticleList.aspx?TypeId={0}&AreaId={1}\">{2}</a>", TopicClassId, TopicAreasId, Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicAreas), TopicAreasId));
                else
                    ltrTopIcArea.Text = string.Format(" > <a href=\"/SupplierInfo/SchoolIntroductionList.aspx?TypeId={0}&AreaId={1}\">{2}</a>", TopicClassId, TopicAreasId, Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicAreas), TopicAreasId));
            }

            if (string.IsNullOrEmpty(model.TitleColor))
                ltrTitle.Text = model.ArticleTitle;
            else
                ltrTitle.Text = string.Format("<font color=\"{0}\">", model.TitleColor) + model.ArticleTitle + "</font>";
            ltrTime.Text = model.IssueTime.ToString("yyyy年MM月dd日 hh:mm");
            if (!string.IsNullOrEmpty(model.Source))
                ltrSource.Text = "来源：" + model.Source;
            if (model.IsImage)
            {
                ltrImg.Text = string.Format("<img src=\"{0}\" alt=\"{1}\" width=\"600\" height=\"400\" />", Domain.FileSystem + model.ImgPath, model.ArticleTitle);
            }
            ltrInfo.Text = model.ArticleText;
            ltrTags.Text = string.IsNullOrEmpty(model.ArticleTag) ? string.Empty : model.ArticleTag.Replace(",", " ");
            ltrEditOR.Text = Utils.GetText(model.Editor, 6);
            if (model.PrevInfo != null)
            {
                ltrPrev.Text = string.Format("上一篇：<a href=\"/SupplierInfo/{0}?Id={1}\">{2}</a>", model.PrevInfo.TopicClassId == EyouSoft.Model.CommunityStructure.TopicClass.行业资讯 ? "ArticleInfo.aspx" : "SchoolIntroductionInfo.aspx", model.PrevInfo.ID, Utils.GetText(model.PrevInfo.ArticleTitle, 20));
            }
            if (model.NextInfo != null)
            {
                ltrNext.Text = string.Format("下一篇：<a href=\"/SupplierInfo/{0}?Id={1}\">{2}</a>", model.NextInfo.TopicClassId == EyouSoft.Model.CommunityStructure.TopicClass.行业资讯 ? "ArticleInfo.aspx" : "SchoolIntroductionInfo.aspx", model.NextInfo.ID, Utils.GetText(model.NextInfo.ArticleTitle, 20));
            }

            //绑定相关文章列表
            rptRelatedArticle.DataSource = ibll.GetTopNumTagList(8, (EyouSoft.Model.CommunityStructure.TopicClass)TopicClassId, (EyouSoft.Model.CommunityStructure.TopicAreas)TopicAreasId, ArticleId);
            rptRelatedArticle.DataBind();

            //绑定最新图文资讯
            rptArticle.DataSource = ibll.GetTopNumPicList(5, null, null, true, null);
            rptArticle.DataBind();

            ibll = null;
            model = null;
        }
    }
}