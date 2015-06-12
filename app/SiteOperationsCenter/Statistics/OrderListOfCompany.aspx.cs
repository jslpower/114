using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 关于组团公司的订单
    /// luofx 2010-9-17
    /// </summary>
    public partial class OrderListOfCompany : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            if (!CheckMasterGrant(YuYingPermission.统计分析_组团社行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社行为分析, true);
                return;
            }
            this.StartAndEndDate1.SetTitle = "下单时间";
        }
    }
}
