using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics.SMSRemnantStatistics
{
    /// <summary>
    /// 功能：短信余额统计首页
    /// 开发人:刘玉灵   时间：2010-9-17
    /// </summary>
    public partial class Default : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            if (!CheckMasterGrant(YuYingPermission.统计分析_管理该栏目, YuYingPermission.统计分析_短信余额统计))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_短信余额统计, false);
            }
        }
    }
}
