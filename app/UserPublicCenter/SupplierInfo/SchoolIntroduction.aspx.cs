using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.SupplierInfo
{
    /// <summary>
    /// 同业学堂
    /// </summary>
    /// 周文超 2010-08-01
    public partial class SchoolIntroduction : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Supplier site = (Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 4;

            if (!IsPostBack)
            {
                InitPageData();
            }
        }

        #region 初始化页面

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPageData()
        {
            #region 经验交流

            NewsControl1.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.经验交流;

            #endregion

            #region 新手计调

            NewsControl2.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.新手计调;
            NewsControl2.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.计调指南;
            
            #endregion

            #region 业务进阶

            NewsControl3.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.业务进阶;
            NewsControl3.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.计调指南;

            #endregion

            #region 同业集结号

            NewsControl4.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.同业集结号;
            NewsControl4.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.计调指南;

            #endregion

            #region 导游导购词

            NewsControl5.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.导游导购词;
            NewsControl5.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.导游之家;

            #endregion

            #region 导游考试

            NewsControl6.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.导游考试相关;
            NewsControl6.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.导游之家;

            #endregion

            #region 带团分享

            NewsControl7.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.带团经验分享;
            NewsControl7.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.导游之家;

            #endregion

            #region 景区案例

            NewsControl8.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.景区案例分析;
            NewsControl8.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.案例分析;

            #endregion

            #region 酒店案例

            NewsControl9.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.酒店案例分析;
            NewsControl9.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.案例分析;

            #endregion

            #region 旅行社案例

            NewsControl10.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.旅行社案例分析;
            NewsControl10.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.案例分析;

            #endregion

            #region 广告初始化

            InitAdvList();

            #endregion
        }

        /// <summary>
        /// 初始化广告位
        /// </summary>
        private void InitAdvList()
        {
            IList<EyouSoft.Model.AdvStructure.AdvInfo> AdvListBanner = null;

            AdvListBanner = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道同业学堂旗帜广告1);
            rptAdv1.DataSource = AdvListBanner;
            rptAdv1.DataBind();

            AdvListBanner = EyouSoft.BLL.AdvStructure.Adv.CreateInstance().GetAdvs(CityId, EyouSoft.Model.AdvStructure.AdvPosition.供求信息频道同业学堂旗帜广告2);
            rptAdv2.DataSource = AdvListBanner;
            rptAdv2.DataBind();

            if (AdvListBanner != null) AdvListBanner.Clear();
            AdvListBanner = null;
        }

        #endregion
    }
}
