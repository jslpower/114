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
    /// 首页政策解读
    /// </summary>
    public partial class PolicyUnscramble : System.Web.UI.UserControl
    {
        protected string ImageServerPath = "";
        protected  int CityId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            FrontPage page = this.Page as FrontPage;
            CityId = page.CityId;
            ImageServerPath = page.ImageServerUrl;
            if (!Page.IsPostBack)
            {
                this.GetUnsrcambleList();
            }
        }
        /// <summary>
        /// 获的政策解读列表
        /// </summary>
        protected void GetUnsrcambleList()
        {
            IList<EyouSoft.Model.CommunityStructure.InfoArticle> ArticleList = EyouSoft.BLL.CommunityStructure.InfoArticle.CreateInstance().GetTopNumList(9,null,EyouSoft.Model.CommunityStructure.TopicAreas.政策解读,false,true,"");
            if (ArticleList != null && ArticleList.Count > 0)
            {
                this.repPolicyUnscramble.DataSource = ArticleList;
                this.repPolicyUnscramble.DataBind();
            }
            ArticleList = null;
        }
    }
}