using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics.AgencyActionAnalysis
{
    /// <summary>
    /// 功能: 组团社行为分析 查看所有登录信息
    /// </summary>
    public partial class AllLoginList : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限控制
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, false);
            }
        }
    }
}
