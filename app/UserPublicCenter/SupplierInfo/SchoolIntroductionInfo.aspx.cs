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
    /// 资讯详细页
    /// </summary>
    /// 周文超 2010-08-31
    public partial class SchoolIntroductionInfo : EyouSoft.Common.Control.FrontPage
    {
        /// <summary>
        /// 资讯ID
        /// </summary>
        private string ArticleId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticleId = Utils.InputText(Request.QueryString["Id"]);

            if (string.IsNullOrEmpty(ArticleId))
            {
                return;
            }

            //zxb 20100907 加入JiaThis代码
            AddJavaScriptInclude("http://www.jiathis.com/code/jiathis_r.js?move=0", true, false);

            UserPublicCenter.SupplierInfo.Supplier site = (UserPublicCenter.SupplierInfo.Supplier)this.Master;
            if (site != null)
                site.MenuIndex = 4;
        }
    }
}
