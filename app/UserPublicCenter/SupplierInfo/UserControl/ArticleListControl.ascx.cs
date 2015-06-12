using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 资讯列表控件
    /// </summary>
    /// 周文超 2010-08-02
    public partial class ArticleListControl : EyouSoft.Common.Control.BaseUserControl
    {
        /// <summary>
        /// 大类ID
        /// </summary>
        private int TopicClassId = 0;
        /// <summary>
        /// 小类ID
        /// </summary>
        private int TopicAreasId = 0;
        protected int intPageSize = 30;
        private int CurrencyPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            TopicClassId = Utils.GetInt(Request.QueryString["TypeId"]);
            TopicAreasId = Utils.GetInt(Request.QueryString["AreaId"]);

            if (TopicClassId <= 0 && TopicAreasId <= 0)
            {
                return;
            }

            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
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
            if (TopicAreasId > 0)
            {
                ltrTopIcArea.Text = " > " + Enum.GetName(typeof(EyouSoft.Model.CommunityStructure.TopicAreas), TopicAreasId);
            }

            int intRecordCount = 0;
            CurrencyPage = Utils.GetInt(Request.QueryString["Page"], 1);
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> list = new List<EyouSoft.Model.CommunityStructure.InfoArticle>();
            if (TopicAreasId > 0)
                list = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetPageList(intPageSize, CurrencyPage, ref  intRecordCount, null, (EyouSoft.Model.CommunityStructure.TopicAreas)TopicAreasId, null, null, null);
            else if (TopicClassId > 0)
                list = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetPageList(intPageSize, CurrencyPage, ref  intRecordCount, (EyouSoft.Model.CommunityStructure.TopicClass)TopicClassId, null, null, null, null);
            rptArticleList.DataSource = list;
            rptArticleList.DataBind();
            this.ExporPageInfoSelect1.intPageSize = intPageSize;
            this.ExporPageInfoSelect1.intRecordCount = intRecordCount;
            this.ExporPageInfoSelect1.CurrencyPage = CurrencyPage;
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"] + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.PageStyleType = Adpost.Common.ExporPage.PageStyleTypeEnum.NewButton;
        }
    }
}