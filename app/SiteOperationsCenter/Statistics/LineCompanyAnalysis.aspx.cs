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
    /// 专线商行为分析首页
    /// 功能：专线商信息显示
    /// 创建人：戴银柱
    /// 创建时间： 2010-09-17  
    /// </summary>
    public partial class LineCompanyAnalysis : EyouSoft.Common.Control.YunYingPage
    {
        
        public string fristDate ="";
        public string lastDate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //判断权限
            if (!CheckMasterGrant(YuYingPermission.统计分析_专线商行为分析))
            {
                Utils.ResponseNoPermit(YuYingPermission.统计分析_专线商行为分析, true);
                return;
            }

            if (!IsPostBack)
            {
               
                //设置时间控件的title
                this.StartAndEndDate1.SetTitle = "时间:";
                //获取当前月的第一天和最后一天
                DateTime now = DateTime.Now;
                DateTime frist = new DateTime(now.Year, now.Month, 1);
                DateTime last = frist.AddMonths(1).AddDays(-1);

                fristDate = frist.ToString("yyyy-MM-dd");
                lastDate = last.ToString("yyyy-MM-dd");

                //设置订购情况的初始化时间
                this.StartAndEndDate2.SetStartDate = fristDate;
                this.StartAndEndDate2.SetEndDate = lastDate;
            }
        }
    }
}
