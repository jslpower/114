using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.SupplierInfo.UserControl
{
    /// <summary>
    /// 嘉宾访谈左边（右边）用户控件
    /// </summary>
    /// 周文超 2010-08-03
    public partial class HonoredGuestLeftAndRight : EyouSoft.Common.Control.BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 访谈介绍
                CommonTopic1.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.访谈介绍;
                CommonTopic1.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.文字描述;
                #endregion

                #region 顾问团队
                CommonTopic2.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.顾问团队;
                CommonTopic2.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
                #endregion

                #region 近期访谈回顾
                CommonTopic3.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.嘉宾访谈;
                CommonTopic3.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.图文广告列表;
                #endregion

                #region 最新行业资讯
                CommonTopic4.PartType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.PartTypes.嘉宾访谈下资讯;
                CommonTopic4.TopicType = UserPublicCenter.SupplierInfo.UserControl.CommonTopicControl.TopicTypes.软文广告列表;
                CommonTopic4.TopicArea = null;
                CommonTopic4.TopicClass = EyouSoft.Model.CommunityStructure.TopicClass.行业资讯;
                #endregion
            }
        }
    }
}