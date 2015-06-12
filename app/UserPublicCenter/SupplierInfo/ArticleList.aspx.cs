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
    /// 行业资讯列表页
    /// </summary>
    /// 周文超 2010-08-02
    public partial class ArticleList : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 大类ID
        /// </summary>
        private int TopicClassId = 0;
        /// <summary>
        /// 小类ID
        /// </summary>
        private int TopicAreasId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            TopicClassId = Utils.GetInt(Request.QueryString["TypeId"]);
            TopicAreasId = Utils.GetInt(Request.QueryString["AreaId"]);

            if (TopicClassId <= 0 && TopicAreasId <= 0)
            {
                return;
            }

            UserPublicCenter.SupplierInfo.Supplier site = (UserPublicCenter.SupplierInfo.Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 2;
        }
    }
}
