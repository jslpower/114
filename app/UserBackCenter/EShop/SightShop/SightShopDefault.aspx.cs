using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;

namespace UserBackCenter.EShop.SightShop
{
    /// <summary>
    /// 景区后台
    /// </summary>
    /// 罗丽娥   2010-12-09
    public partial class SightShopDefault : EyouSoft.Common.Control.BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hrefSightShop.HRef = string.Format("{0}/jingqu_2_{1}", Domain.SeniorOnlineShop, SiteUserInfo.CompanyID);
        }
    }
}
