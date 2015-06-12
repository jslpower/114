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

namespace SiteOperationsCenter.TyProductManage
{
    /// <summary>
    /// 机票采购商查询，张新兵，20100926
    /// </summary>
    public partial class PlanerBuyerQuery : YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            //暂时用 【统计分析_管理该栏目】权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_管理该栏目, false);
            }
        }
    }
}
