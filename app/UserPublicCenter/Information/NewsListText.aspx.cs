using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.Information
{
    public partial class NewsListText : EyouSoft.Common.Control.FrontPage
    {
        IList<ZiXun> lsNews = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitNewsList();
            }
        }

        /// <summary>
        /// 初始化资讯列表
        /// </summary>
        private void InitNewsList()
        {
            
        }
    }
}
