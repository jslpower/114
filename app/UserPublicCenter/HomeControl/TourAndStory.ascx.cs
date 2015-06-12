using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HomeControl
{
    /// <summary>
    /// 首页旅游动态/成功故事
    /// </summary>
    public partial class TourAndStory : System.Web.UI.UserControl
    {
        private bool _isGetStory = false;

        /// <summary>
        /// 是否是取成功故事 默认取旅游动态
        /// </summary>
        public bool IsGetStory
        {
            get { return _isGetStory; }
            set { _isGetStory = value; }
        }
        protected string strAllList = "";
        protected string ImageServerPath = "";
        protected int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        { 
            FrontPage page = this.Page as FrontPage;
            CityId = page.CityId;
            ImageServerPath = page.ImageServerUrl;
            if (!Page.IsPostBack)
            {
                GetTourAndStory();
            }
        }

        protected void GetTourAndStory()
        {
            EyouSoft.Model.CommunityStructure.TopicAreas TopicAreas = EyouSoft.Model.CommunityStructure.TopicAreas.行业动态;

            if (IsGetStory)
            {
                TopicAreas = EyouSoft.Model.CommunityStructure.TopicAreas.成功故事;
            }

            IList<EyouSoft.Model.CommunityStructure.InfoArticle> ArticleList = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetTopNumList(9, null, TopicAreas,false,true, "");
            if (ArticleList != null && ArticleList.Count > 0)
            {
                this.repTourAndStory.DataSource = ArticleList;
                this.repTourAndStory.DataBind();
            }
            ArticleList = null;

        }


    }
}