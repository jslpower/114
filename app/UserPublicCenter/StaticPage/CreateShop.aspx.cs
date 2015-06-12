using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.StaticPage
{  
    /// <summary>
    /// 页面功能：开网店
    /// xuty 2011/05/16
    /// </summary>
    public partial class CreateShop : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserPublicCenter.MasterPage.NewPublicCenter masterPage = this.Master as UserPublicCenter.MasterPage.NewPublicCenter;
            if (masterPage != null)
            {
                masterPage.HeadMenuIndex = 1;//选中首页菜单项
            }
        }
    }
}
