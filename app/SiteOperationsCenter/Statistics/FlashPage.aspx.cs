using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using OpenFlashChart;

namespace SiteOperationsCenter.Statistics
{
    /// <summary>
    /// Flash
    /// 功能：Flash
    /// 创建人：戴银柱
    /// 创建时间： 2010-09-26  
    /// </summary>
    /// 
    public partial class FlashPage : EyouSoft.Common.Control.YunYingPage
    {
        /// <summary>
        /// 要获得的数据月数默认为12个月
        /// </summary>
        #region 变量
        public static int MonthCount = 12;
        protected string strStartDate = "";
        protected string strEndDate = "";
        #endregion 

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

                #region 设置FLASH 大小 查看大图的时候 type =1
                if (Utils.GetQueryStringValue("type") == "1")
                {
                    this.hideType.Value = "1";
                    this.OpenFlashChartControl1.Width = "800px";
                    this.OpenFlashChartControl1.Height = "560px";
                }
                else
                {
                    this.hideType.Value = "0";
                    this.OpenFlashChartControl1.Width = "320px";
                    this.OpenFlashChartControl1.Height = "150px";
                }
                #endregion

                DataInit(startTime, endTime);
            }
        }

        #region 页面初始化
        protected void DataInit(DateTime startTime, DateTime endTime)
        {
            //Falsh 初始化
            DateTime X_StartDate = new DateTime();
            endTime = endTime.AddMonths(1);
            int monthPoor = endTime.Year * 12 + endTime.Month - startTime.Year * 12 - startTime.Month;
            //如果间隔时间小于12个月 那么就补成12个月
            if (monthPoor <= 12)
            {
                MonthCount = 12;
                X_StartDate = Convert.ToDateTime(endTime.AddMonths(-MonthCount));  //获得开始时间
            }
            else
            {
                MonthCount = monthPoor;
                X_StartDate = startTime;
            }

            IList<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth> dtList = EyouSoft.BLL.TourStructure.TourOrder.CreateInstance().GetTourOrderStatisticsByMonth(X_StartDate, endTime);

            //生成页面的JS 数组
            GetOrderPlotStartEndDate(X_StartDate, out strStartDate, out strEndDate);

            //为控件绑定数据
            OpenFlashChart.OpenFlashChart chart = InitOpenFlashChar(dtList, 5, X_StartDate);

            OpenFlashChartControl1.Chart = chart;
        }
        #endregion 

        #region 生成flash的数据源
        /// <summary>
        /// 初始化OpenFlashChar(从数据集中取出的就只要MonthCount个月的记录,若当前超过了MonthCount个月的,则从当前月开始往回推算,否则凑满MonthCount个月)
        /// </summary>
        /// <param name="dtList">数据集</param>
        /// <param name="minY_DotCount">最小的节点个数，默认为10个</param>
        public static OpenFlashChart.OpenFlashChart InitOpenFlashChar(IList<EyouSoft.Model.TourStructure.TourOrderStatisticsByMonth> dtList, int minY_DotCount, DateTime X_StarDate)
        {
            OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
            Double Y_minValue = 0;
            Double Y_maxValue = 0;

            if (minY_DotCount <= 0)
                minY_DotCount = 1;   //最小的节点个数

            string[] PointColor = { "#9650dc", "#FF9900", "#669900", "#000000" };
            string[] LineText = { "成交订单", "留位订单", "留位过期", "不受理订单" };
            string[] OnClickFun = { "LineClickBuy", "LineClickReserveTimeIn", "LineClickReserveTimeOut", "LineClickNoDeal" };
            string dotTipValueFormat = "#x_label#月：#val#笔，点击查看！";  //节点提示显示的格式

            //-------整个坐标的样式设置------
            OpenFlashChart.Title flashTitle = new Title("");
            flashTitle.Text = "";
            flashTitle.Style = "";
            chart.Title = flashTitle;  //设置标题               

            chart.Bgcolor = "#FFFFFF";  //设置背景色
            chart.X_Axis.GridColour = "#DDDDDD";
            chart.X_Axis.Colour = "#000000";  //y轴线条颜色
            chart.X_Axis.Labels.Color = "#000000";  //y轴文字颜色

            chart.Y_Axis.GridColour = "#DDDDDD";
            chart.Y_Axis.Colour = "#000000";  //y轴线条颜色
            chart.Y_Axis.Labels.Color = "#000000";  //y轴文字颜色

            chart.X_Axis.Offset = true;
            chart.X_Axis.TickHeight = "2";  //y坐标的坐标上刻度凸出的长度

            chart.Y_Axis.Offset = true;
            chart.Y_Axis.TickLength = 2;  //y坐标的坐标上刻度凸出的长度
            //-------整个坐标的样式设置------


            //-------------设置移动节点上后显示的提示样式---
            chart.Tooltip = new ToolTip(dotTipValueFormat);
            chart.Tooltip.Shadow = true;  //显示阴影
            chart.Tooltip.Colour = "#808080";  //边框颜色
            chart.Tooltip.MouseStyle = ToolTipStyle.CLOSEST;  //鼠标移动到节点上后提示出现的效果
            chart.Tooltip.BackgroundColor = "#F0F0F0";
            chart.Tooltip.Stroke = 2;   //边框宽度
            chart.Tooltip.Rounded = 10;  //边框菱角弯曲度            
            //-------------设置移动节点上后显示的提示样式---

            if (dtList != null && dtList.Count > 0)
            {
                int[] ColumnsCount = new[] { 5, 2, 3, 4 };


                //最大值和最小值都先取第1行，第1列的数据
                //Y_minValue = Convert.ToDouble(dtList[0].);
                //Y_maxValue = Convert.ToDouble(dtList[0]);

                List<Int32>[] dataList = new List<Int32>[ColumnsCount.Length];
                //new出每个list,有n个要绑定的字段，就要显示n条直线
                for (int index = 0; index < ColumnsCount.Length; index++)
                {
                    dataList[index] = new List<int>();
                }
                //通过数据集循环获得数据
                //找最大值
                Y_maxValue = dtList.Max(p => p.OrdainNum);
                if (dtList.Max(p => p.SaveSeatNum) > Y_maxValue)
                    Y_maxValue = dtList.Max(p => p.SaveSeatNum);
                if (dtList.Max(p => p.SaveSeatExpiredNum) > Y_maxValue)
                    Y_maxValue = dtList.Max(p => p.SaveSeatExpiredNum);
                if (dtList.Max(p => p.UntreatedNum) > Y_maxValue)
                    Y_maxValue = dtList.Max(p => p.UntreatedNum);

                //var ddd = (from item in dtList
                //           orderby (item.OrdainNum + item.SaveSeatNum + item.SaveSeatExpiredNum + item.UntreatedNum) descending
                //           select item).Take(1);

                //找最小值
                Y_minValue = 0;
                for (int i = 0; i < MonthCount; i++)
                {
                    dataList[0].Add(dtList[i].OrdainNum);
                    dataList[1].Add(dtList[i].SaveSeatNum);
                    dataList[2].Add(dtList[i].SaveSeatExpiredNum);
                    dataList[3].Add(dtList[i].UntreatedNum);
                }

                //将datalist的值绑定到控件中
                int k = 0;
                foreach (List<Int32> list in dataList)
                {
                    OpenFlashChart.Line line = new Line();
                    line.Values = list;
                    line.HaloSize = 0;
                    line.Width = 2;   //直线宽度

                    //---------设置节点颜色------
                    string strPointColor = PointColor[0];  //默认取第1个值
                    string text = LineText[0];
                    string clinkFun = OnClickFun[0];
                    if (k < PointColor.Length)  //若超过坐标，则取默认的颜色值
                    {
                        strPointColor = PointColor[k];
                        text = LineText[k];
                        clinkFun = OnClickFun[k];
                    }

                    //line.Text = "▇ " + text;
                    line.Text = text;

                    line.DotStyleType.Colour = strPointColor;   //节点的颜色
                    line.Colour = strPointColor;   //直线的颜色

                    //line.DotSize = 50;
                    line.DotStyleType.DotSize = 4;   //鼠标移动到节点上后节点显示的大小
                    //line.DotStyleType.Sides = 10;
                    line.DotStyleType.Type = DotType.SOLID_DOT;  //节点的形状
                    line.DotStyleType.Tip = dotTipValueFormat;  //设置移动到节点时要显示的值

                    line.DotStyleType.Alpha = 90;
                    //line1.DotStyleType.BackgroundColour = "#F0F0F0";

                    line.FontSize = 14;  //标题文字大小

                    //定义节点上的事件
                    //line.DotStyleType.OnClick = string.Format("OpenUnionOrderList(0,'this.value','',{0})", k);

                    line.SetOnClickFunction(clinkFun);

                    //加入线条
                    chart.AddElement(line);
                    k++;
                }
            }

            //---------设置坐标的值------------
            //chart.Y_Axis.SetRange(0, 50, 5);           //要求动态的计算
            chart.Y_Axis.Labels.FormatString = "#val#";

            //设置x轴坐标的文字表述
            for (int j = 0; j < MonthCount; j++)
            {
                DateTime tempDate = X_StarDate.AddMonths(j);
                AxisLabel xlabel = new AxisLabel(tempDate.Year.ToString() + "-" + tempDate.Month.ToString());
                if (j % 4 != 0)
                    xlabel.Visible = false;
                chart.X_Axis.Labels.Add(xlabel);
            }

            //chart.X_Axis.Labels.FormatString = "#val:time#";
            chart.X_Axis.Labels.VisibleSteps = 4;  //坐标刻度，显示的文字每个刻度的差值
            chart.X_Axis.Steps = 1;   //坐标刻度，每个刻度的差值
            //chart.X_Axis.Min = xMin;
            //chart.X_Axis.Max = xMax;
            //---------设置坐标的值------------

            //动态计算y轴坐标
            //若最大值与最小值之间的节点数量小于最小的节点数量
            if ((Y_maxValue - Y_minValue) / minY_DotCount < 1)
            {
                Y_maxValue = minY_DotCount + Y_minValue;  //则计算出最大值
            }
            chart.Y_Axis.SetRange(Y_minValue, Y_maxValue, Convert.ToInt32((Y_maxValue - Y_minValue) / minY_DotCount));

            return chart;
        }
        public static void GetOrderPlotStartEndDate(DateTime X_StartDate, out string strStartDate, out string strEndDate)
        {
            strStartDate = "";
            strEndDate = "";
            //TourUnion.Account.Model.Account operatorModel = TourUnion.Account.DAL.UserAuth.GetCompanyUserInfo();
            //TourUnion.BLL.TourUnion_TourOrder bll = new TourUnion.BLL.TourUnion_TourOrder();
            //DateTime startdate = bll.GetOrderListStartDate(operatorModel.UnionId);
            //bll = null;
            //operatorModel = null;

            for (int i = 0; i < MonthCount; i++)
            {
                //DateTime temp = startdate.AddMonths(i);
                DateTime temp = X_StartDate.AddMonths(i);
                DateTime tempStartDate = new DateTime(temp.Year, temp.Month, 1);  //月初
                DateTime tempEndDate = tempStartDate.AddMonths(1).AddDays(-1);  //月末

                strStartDate += tempStartDate.ToShortDateString() + ",";
                strEndDate += tempEndDate.ToShortDateString() + ",";
            }

            strStartDate = strStartDate.TrimEnd(",".ToCharArray());
            strEndDate = strEndDate.TrimEnd(",".ToCharArray());
        }
        #endregion

    }
}
