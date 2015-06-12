using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using OpenFlashChart;
using System.Data;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// 专线商订购情况
    /// 功能：订购情况信息
    /// 创建人：戴银柱
    /// 创建时间： 2010-09-19  
    /// </summary>
    
    public partial class AjaxLineOrder : EyouSoft.Common.Control.YunYingPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断权限
                if (!CheckMasterGrant(YuYingPermission.统计分析_专线商行为分析))
                {
                    Utils.ResponseNoPermit(YuYingPermission.统计分析_专线商行为分析, true);
                    return;
                }

                #region Request 参数
                //获得开始时间
                DateTime startTime = Utils.GetDateTime(Utils.GetQueryStringValue("startTime"), DateTime.Now);

                //获得结束时间
                DateTime endTime = Utils.GetDateTime(Utils.GetQueryStringValue("endTime"), DateTime.Now);
                #endregion 

                //页面数据初始化
                DataInit(startTime, endTime);
            }
        }

        #region 数据初始化方法
        protected void DataInit(DateTime startTime, DateTime endTime)
        {
            IList<int> list = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetOrderNumByState(startTime, endTime);
            if (list != null && list.Count > 0)
            {
                if (startTime.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd") == endTime.ToString("yyyy-MM-dd"))
                {
                    this.lblDateTime.Text = startTime.Month.ToString() + " 月";
                    this.lblExpiredTime.Text = startTime.Month.ToString() + " 月";
                    this.lblInvalidTime.Text = startTime.Month.ToString() + " 月";
                }
                else
                {
                    this.lblDateTime.Text = startTime.ToString("yyyy-MM-dd") + "至" + endTime.ToString("yyyy-MM-dd");
                    this.lblExpiredTime.Text = startTime.ToString("yyyy-MM-dd") + "至" + endTime.ToString("yyyy-MM-dd");
                    this.lblInvalidTime.Text = startTime.ToString("yyyy-MM-dd") + "至" + endTime.ToString("yyyy-MM-dd");
                }

                //确认成交订单数
                this.lblCertainCount.Text = list[((int)EyouSoft.Model.TourStructure.OrderState.已成交)].ToString();

                //留位过期订单数
                this.lblExpiredCount.Text = list[((int)EyouSoft.Model.TourStructure.OrderState.留位过期)].ToString();

                //无效订单数
                this.lblInvalidCount.Text = list[((int)EyouSoft.Model.TourStructure.OrderState.未处理)].ToString();
            }
        }
        #endregion 
    }
}
