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
using EyouSoft.Common.Control;
using EyouSoft.Common;
namespace UserBackCenter.SystemSet
{
    public partial class SystemIndex : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckGrant(TravelPermission.系统设置_权限管理))
            {
                Utils.ResponseNoPermit();
                return;
            }
        }
    }
}
