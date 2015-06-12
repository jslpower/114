using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 同业学堂左右两侧用户控件
    /// </summary>
    /// 周文超 2010-08-01
    public partial class SchoolLeftAndRight : EyouSoft.Common.Control.BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            #region 学堂介绍

            CommonTopicControl1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.学堂介绍;
            CommonTopicControl1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.文字描述;

            #endregion

            #region 同业之星访谈

            CommonTopicControl2.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.同业之星访谈;
            CommonTopicControl2.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
            CommonTopicControl2.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.同业之星;
            CommonTopicControl2.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;

            EyouSoft.Common.Control.FrontPage CurrPage = (EyouSoft.Common.Control.FrontPage)this.Page;
            if (!CurrPage.Equals(null))
            {
                CommonTopicControl2.CurrCityId = CurrPage.CityId;
            }
            CurrPage = null;

            #endregion

            #region 新闻资讯

            CommonTopicControl3.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
            CommonTopicControl3.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
            CommonTopicControl3.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
            CommonTopicControl3.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.新闻资讯;

            #endregion

            #region 行业动态

            CommonTopicControl4.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.资讯;
            CommonTopicControl4.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
            CommonTopicControl4.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
            CommonTopicControl4.TopicArea = EyouSoft.Model.CommunityStructure.TopicAreas.行业动态;

            #endregion
        }
    }
}