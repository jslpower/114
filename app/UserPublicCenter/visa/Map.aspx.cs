using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserPublicCenter.visa
{
    public partial class Map : EyouSoft.Common.Control.FrontPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            (this.Master as UserPublicCenter.MasterPage.NewPublicCenter).HeadMenuIndex = 1;//设置导航条
            this.Title = "电子地图_同业114电子地图频道";           
        }
    }
}
