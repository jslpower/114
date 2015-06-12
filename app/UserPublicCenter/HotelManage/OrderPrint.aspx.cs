using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HotelManage
{    
    /// <summary>
    /// 酒店订单打印
    /// xuty 2010/11/07
    /// </summary>
    public partial class OrderPrint:System.Web.UI.Page
    {
        protected EyouSoft.Model.HotelStructure.OrderInfo orderModel;

        protected string guestNames;//旅客姓名
        protected string guestTypes;//旅客类型
        protected string guestMible;//旅客手机
        protected string resOrderId;//订单号(酒店返回)
        protected string userName;//预订人
      
        protected decimal backPrice;//总反佣价
        protected decimal totalAccount;//每间房的总销售价
        protected string hotelRateList;//酒店房费列表
        protected string ImageServerUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageServerUrl = EyouSoft.Common.ImageManage.GetImagerServerUrl(1);
            resOrderId = Utils.GetQueryStringValue("resOrderId");
            EyouSoft.IBLL.HotelStructure.IHotelOrder orderBll = EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance();
            string method = Utils.GetQueryStringValue("method");
            int errorCode = 0;// 错误代码
            string errorDesc = "";//错误描述
            orderModel = orderBll.GetInfo(resOrderId, out errorCode, out errorDesc);
            if (orderModel == null)
            {
                orderModel = new EyouSoft.Model.HotelStructure.OrderInfo();
                if (errorDesc == "")
                {
                    errorDesc = "订单不存在";
                }
                Utils.ShowError(errorDesc, "hotel");
                return;
            }
            userName = orderModel.BuyerUName;
            IList<EyouSoft.HotelBI.HBEResGuestInfo> guestList = orderModel.ResGuests;
            if (guestList != null && guestList.Count > 0)
            {
                foreach (EyouSoft.HotelBI.HBEResGuestInfo guest in guestList)
                {
                    guestNames += guest.PersonName + ",";//旅客姓名
                    guestTypes += guest.GuestTypeIndicator.ToString() + ",";//旅客类型
                    guestMible = guest.Mobile;//旅客手机
                }
            }
            //EyouSoft.HotelBI.SingleSeach searchModel = new EyouSoft.HotelBI.SingleSeach();
            //searchModel.HotelCode = orderModel.HotelCode;//酒店代码
            //searchModel.CheckInDate = orderModel.CheckInDate.ToString("yyyy-MM-dd");//入住日期
            //searchModel.CheckOutDate = orderModel.CheckOutDate.ToString("yyyy-MM-dd");//离店日期
            //searchModel.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;
            //EyouSoft.HotelBI.ErrorInfo errorInfo=null;
            //EyouSoft.Model.HotelStructure.HotelInfo hotelModel = EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(searchModel,out errorInfo);//获取酒店实体
            //string theMess = IsOk(errorInfo, hotelModel);
            //if (theMess != "")
            //{
            //    Utils.ShowError(theMess, "");
            //    return;
            //}
            GetRateInfoList();
        }

        /// <summary>
        /// 绑定酒店房费列表
        /// </summary>
        protected void GetRateInfoList()
        {
            //是否存在房费明细
            if (orderModel != null && orderModel.Rates != null && orderModel.Rates.Count>0)
            {   
                IList<EyouSoft.Model.HotelStructure.RateInfo> rateList = orderModel.Rates;//获取价格明细列表
              
                StringBuilder strBuilder1 = new StringBuilder();//结算价行
                StringBuilder strBuilder2 = new StringBuilder();//反佣价行
                StringBuilder strBuilder3 = new StringBuilder();//早餐行
                StringBuilder strBuilder = new StringBuilder();//整个房费构造
                //构造表标题
                strBuilder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr><th align=\"center\">&nbsp;</th>" +
                                    "<th align=\"center\">月/日</th>" +
                                    "<th align=\"center\">周一</th>" +
                                    "<th align=\"center\">周二</th>" +
                                    "<th align=\"center\">周三</th>" +
                                    "<th align=\"center\">周四</th>" +
                                    "<th align=\"center\">周五</th>" +
                                    "<th align=\"center\">周六</th>" +
                                    "<th align=\"center\">周日</th>" +
                                    "<th align=\"center\">每间总价</th>"+
                                    "<th align=\"center\">总计</th>" +
                                    "</tr>");
                DateTime firstDate = orderModel.CheckInDate;//入住日期
                DateTime endDate = orderModel.CheckOutDate;//离店日期
                //从入住日期到离店日期循环
                int forIndex = 1;//星期段循环index
                int forNum = 1;//总共能循环的星期数
                for (DateTime nowDate = firstDate; firstDate <= endDate; nowDate = nowDate.AddDays(1))
                {
                    //判断是否是入住日期

                    if (firstDate == nowDate)
                    {
                        int dayInt = (int)nowDate.DayOfWeek;//获取入住日当前星期(0,1,2…6)
                        int dayToMonday = dayInt == 0 ? 6 : dayInt - 1;//获取前补列数
                        int dayToSunday = dayInt == 0 ? 0 : 7 - dayInt;//获取候补列数

                        int dayInt2 = (int)endDate.DayOfWeek;//获取离店日当前星期(0,1,2…6)
                        int dayToSunday2 = dayInt2 == 0 ? 0 : 7 - dayInt2;//获取候补列数

                        forNum = ((endDate - firstDate).Days + dayToMonday + dayToSunday2 + 1) / 7;//星期获取循环次数(为了计算总计列的跨行数)

                        DateTime overDate = nowDate.AddDays(dayToSunday);//获取第一个星期的结束日
                        if (overDate > endDate)//如果结束日期大于离店日期则取离店日期
                        {
                            overDate = endDate;
                        }
                        strBuilder1.AppendFormat("<tr><td align=\"center\" class=\"orange\">售价</td><td rowspan=\"3\" align=\"center\">{0}<br />-<br />{1}</td>", nowDate.ToString("yyyy-MM-dd"), overDate.ToString("yyyy-MM-dd"));
                        strBuilder2.Append("<tr><td align=\"center\" class=\"orange\">返佣价</td>");
                        strBuilder3.Append("<tr><td align=\"center\" class=\"orange\">早餐</td>");
                        //前补列
                        for (var i = 1; i <= dayToMonday; i++)
                        {
                            strBuilder1.Append("<td align=\"center\"></td>");
                            strBuilder2.Append("<td align=\"center\"></td>");
                            strBuilder3.Append("<td align=\"center\"></td>");
                        }

                    }
                    //查找跟当前日期匹配的价格明细如果没有则输出空白列
                    EyouSoft.Model.HotelStructure.RateInfo rateInfo = rateList.FirstOrDefault(r => r.CurrData == nowDate);

                    strBuilder1.AppendFormat("<td align=\"center\">{0}</td>", rateInfo == null ? "" : "￥" + EyouSoft.Common.Utils.GetMoney(rateInfo.AmountPrice));
                    strBuilder2.AppendFormat("<td align=\"center\">{0}</td>", rateInfo == null ? "" : "￥" + EyouSoft.Common.Utils.GetMoney(rateInfo.CommissionAmount));
                    strBuilder3.AppendFormat("<td align=\"center\">{0}</td>", rateInfo == null ? "" : rateInfo.FreeMeal == 0 ? "无" : rateInfo.FreeMeal.ToString());
                    //总计列的跨行输出(如果是第一个星期段循环，如果是星期日则输出该列)
                    if (forIndex == 1)
                    {  
                        if (nowDate.DayOfWeek == 0)
                        {
                            decimal CommissionAmount1 = rateList.Sum(r => r.CommissionAmount);
                            strBuilder1.AppendFormat(" <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{1}<br />返佣价：<br />￥{2}</td> <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{3}<br />返佣价：<br />￥{4}</td>", forNum * 3, orderModel.TotalAmount / (orderModel.Quantity == 0 ? 1 : orderModel.Quantity), CommissionAmount1, EyouSoft.Common.Utils.GetMoney(orderModel.TotalAmount), EyouSoft.Common.Utils.GetMoney(CommissionAmount1 * orderModel.Quantity));
                            forIndex++;
                        }
                    }
                    //判断是否是最后一个日期
                    if (nowDate == endDate)
                    {
                        int dayInt = (int)nowDate.DayOfWeek;//获取当前星期(0,1,2…6)
                        int dayToSunday = dayInt == 0 ? 0 : 7 - dayInt;//获取候补列数
                        //进行补后列操作
                        for (var i = 1; i <= dayToSunday; i++)
                        {
                            strBuilder1.Append("<td align=\"center\"></td>");
                            strBuilder2.Append("<td align=\"center\"></td>");
                            strBuilder3.Append("<td align=\"center\"></td>");
                        }
                        if (forIndex == 1)
                        {
                            if (nowDate.DayOfWeek != 0)
                            {
                                 decimal CommissionAmount1 = rateList.Sum(r => r.CommissionAmount);
                                 strBuilder1.AppendFormat(" <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{1}<br />返佣价：<br />￥{2}</td> <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{3}<br />返佣价：<br />￥{4}</td>", forNum * 3, EyouSoft.Common.Utils.GetMoney(orderModel.TotalAmount / (orderModel.Quantity == 0 ? 1 : orderModel.Quantity)), EyouSoft.Common.Utils.GetMoney(CommissionAmount1), EyouSoft.Common.Utils.GetMoney(orderModel.TotalAmount), EyouSoft.Common.Utils.GetMoney(CommissionAmount1 * orderModel.Quantity));
                                 forIndex++;
                            }
                        }
                        //结尾输出
                        strBuilder1.Append("</tr>");//结算价行结束
                        strBuilder2.Append("</tr>");//反佣价行结束
                        strBuilder3.Append("</tr>");//早餐行结束
                        //整个房费结束
                        strBuilder.AppendFormat("{0}{1}{2}</table>", strBuilder1.ToString(), strBuilder2.ToString(), strBuilder3.ToString());
                        break;//方法返回
                    }
                    //判断是否是星期日
                    if (nowDate.DayOfWeek == 0)
                    {
                        strBuilder1.Append("</tr>");//结算价行结束
                        strBuilder2.Append("</tr>");//反佣价行结束
                        strBuilder3.Append("</tr>");//早餐行结束
                        DateTime lastDate1 = nowDate.AddDays(1);//下个开始日期
                        DateTime lastDate2 = nowDate.AddDays(7);//下个结束日期
                        if (lastDate2 > endDate)//如果下个结束日期超过离店日期则用离店日期
                        {
                            lastDate2 = endDate;
                        }
                        strBuilder.Append(strBuilder1.ToString() + strBuilder2.ToString() + strBuilder3.ToString());//星期段结束添加到strBuilder
                        //重新设置builder
                        strBuilder1 = new StringBuilder();
                        strBuilder2 = new StringBuilder();
                        strBuilder3 = new StringBuilder();

                        //开始换行操作进入下个星期段循环
                        strBuilder1.AppendFormat("<tr><td align=\"center\" class=\"orange\">售价</td><td rowspan=\"3\" align=\"center\">{0}<br />-<br />{1}</td>", lastDate1.ToString("yyyy-MM-dd"), lastDate2.ToString("yyyy-MM-dd"));
                        strBuilder2.Append("<tr><td align=\"center\" class=\"orange\">返佣价</td>");
                        strBuilder3.Append("<tr><td align=\"center\" class=\"orange\">早餐</td>");



                    }
                }
                hotelRateList = strBuilder.ToString();//酒店房费列表HTML
            }
        }

      
    }
}
