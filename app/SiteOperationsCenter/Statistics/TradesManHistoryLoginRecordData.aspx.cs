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
using EyouSoft.Common.Function;
using EyouSoft.Common;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 零售商历史登录记录
    /// Lym 2010-09-20
    /// </summary>
    public partial class TradesManHistoryLoginRecordData : EyouSoft.Common.Control.YunYingPage
    {
        public string NowDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 权限判断
                //查询
                if (!this.CheckMasterGrant(YuYingPermission.统计分析_组团社历史登录记录))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_组团社历史登录记录, true);
                    return;
                }             
                #endregion

                StartAndEndDate1.SetTitle = "登录时间段：";
            }
        }
    }
}
