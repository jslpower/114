using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using EyouSoft.Common;

using EyouSoft.Common.Control;
using System.Text;
namespace UserPublicCenter.HotelManage
{
    /// <summary>
    /// 页面功能:酒店预订
    /// 开发时间:2010/12/06 开发人:xuty
    /// --------------------------------
    /// 2011-1-20 查询单酒店不带供应商代码
    /// </summary>
    public partial class HotelBook : EyouSoft.Common.Control.FrontPage
    {
        protected string hotelName;//酒店名称
        protected string comeDate;//入住日期
        protected string leaveDate;//离店日期
        protected string payType;//付款方式
        protected int inDays;//入住天数
        protected string hotelRemark;
        protected IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> visistList;//酒店常旅客
        protected EyouSoft.Model.HotelStructure.RateInfo rateModel;//第一天价格明细
        protected EyouSoft.Model.HotelStructure.RoomTypeInfo roomTypeModel;//房型信息
        protected decimal backPrice;//总反佣价
        protected string hotelRateList;//酒店房费列表
        protected string roomCode;//房型代码
        protected string vendorCode;//供应商代码
        protected string ratePlanCode;//价格计划代码
        protected string hotelCode;//酒店编号
        protected string bookPolicy;//预定规则和要求
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsLogin)
            {
                EyouSoft.Security.Membership.UserProvider.RedirectLogin(this.Request.Url.ToString(), "请登录后预定");
                return;
            }
            this.CityAndMenu1.HeadMenuIndex = 4;

            #region 获取酒店查询条件
            hotelCode = Utils.GetQueryStringValue("hotelCode");//酒店编号
            comeDate = Utils.GetQueryStringValue("comeDate");//入住日期
            leaveDate = Utils.GetQueryStringValue("leaveDate");//离店日期

            DateTime cDate = Utils.GetDateTime(comeDate);
            DateTime lDate = Utils.GetDateTime(leaveDate);
            inDays = (lDate - cDate).Days;//获取入住天数

            roomCode = Utils.GetQueryStringValue("roomCode");//房型代码
            vendorCode = string.Empty; //Utils.GetQueryStringValue("vendorCode");//供应商代码
            ratePlanCode = Utils.GetQueryStringValue("ratePlanCode");//价格计划代码
            #endregion

            HotelSearch1.CityId = Utils.GetInt(Utils.GetQueryStringValue("CityID"));
            CommonUser1.CityId = HotelSearch1.CityId;
            SpecialHotel1.CityId = HotelSearch1.CityId;
            HotHotel1.CityId = HotelSearch1.CityId;
            string method = Utils.GetFormValue("method");//当前操作

            #region 查询酒店实体
            //设置查询条件
            EyouSoft.HotelBI.SingleSeach searchModel = new EyouSoft.HotelBI.SingleSeach();
            searchModel.HotelCode = hotelCode;//酒店代码
            searchModel.CheckInDate = comeDate;//入住时间
            searchModel.CheckOutDate = leaveDate;//离店时间
            searchModel.RoomTypeCode = roomCode;//房型代码
            searchModel.VendorCode = vendorCode;//供应商代码
            searchModel.RatePlanCode = ratePlanCode;//价格计划代码
            searchModel.AvailReqType = EyouSoft.HotelBI.AvailReqTypeEnum.includeStatic;//查询完整酒店信息
            EyouSoft.HotelBI.ErrorInfo errorInfo = null;//错误信息实体
            EyouSoft.Model.HotelStructure.HotelInfo hotelModel = 
                EyouSoft.BLL.HotelStructure.Hotel.CreateInstance().GetHotelModel(searchModel, out errorInfo);//酒店实体

            #endregion

            string themess = IsOk(errorInfo, hotelModel);//获取错误信息

            //如果没有错误信息则获取酒店各种信息
            if (themess == "")
            {
                #region 获取酒店实体中的信息
                hotelName = hotelModel.HotelName;
                this.Page.Title = hotelName + "_酒店预订_同业114酒店频道";

                //根据房型代码获取酒店中的房型
                if (hotelModel.RoomTypeList != null && hotelModel.RoomTypeList.Count > 0)
                {
                    roomTypeModel = hotelModel.RoomTypeList.FirstOrDefault(r => r.RoomTypeCode == roomCode && /*r.VendorCode == vendorCode &&*/ r.RatePlanCode == ratePlanCode);//获取房型信息
                }

                visistList = 
                    EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance().GetHotelListByName("", SiteUserInfo.CompanyID);//获取酒店常旅客
                
                //获取房型明细中的第一条
                if (roomTypeModel != null && roomTypeModel.RoomRate.RateInfos.Count > 0)
                {
                    payType = roomTypeModel.RoomRate.Payment == EyouSoft.HotelBI.HBEPaymentType.T ? "前台现付" : "";
                    backPrice = roomTypeModel.RoomRate.RateInfos.Sum(r => r.CommissionAmount);//取得总反佣价

                    rateModel = roomTypeModel.RoomRate.RateInfos[0];//获取第一条明细
                }//如果房型信息不存在则显示房型已满
                else
                {
                    Utils.ShowError(string.Format("{0}至{1}房型已满", comeDate, leaveDate), "hotel");
                    roomTypeModel = new EyouSoft.Model.HotelStructure.RoomTypeInfo();
                    EyouSoft.Model.HotelStructure.RoomRateInfo roomRateModel = new EyouSoft.Model.HotelStructure.RoomRateInfo();
                    roomRateModel.RateInfos = new List<EyouSoft.Model.HotelStructure.RateInfo>();
                    roomTypeModel.RoomRate = roomRateModel;
                    rateModel = new EyouSoft.Model.HotelStructure.RateInfo();
                    return;
                }

                //获取预定规则和要求
                if (roomTypeModel.RoomRate.BookPolicy != null)
                {
                    bookPolicy = roomTypeModel.RoomRate.BookPolicy.LongDesc;
                }
                #endregion

                #region 下单操作
                if (method == "save")//下单操作
                {
                    if (hotelModel.HotelCode == "")//如果传入不存在的酒店则输出失败
                    {
                        Utils.ResponseMeg(false, string.Format("{0}至{1}房型已满", comeDate, leaveDate));
                        return;
                    }
                    string aEarlyTime = Utils.GetFormValue("hb_selETime");//最早到达时间
                    string aLateTime = Utils.GetFormValue("hb_selLTime");//最迟到达时间
                    int intLateTime = Utils.GetInt(aLateTime);
                    if (intLateTime > 24)
                        aLateTime = (intLateTime - 24).ToString();
                    EyouSoft.Model.HotelStructure.OrderInfo orderModel = new EyouSoft.Model.HotelStructure.OrderInfo();
                    orderModel.ArriveEarlyTime = (aEarlyTime.Length == 1 ? ("0" + aEarlyTime) : aEarlyTime) + "00";//到店最早时间
                    orderModel.ArriveLateTime = (aLateTime.Length == 1 ? ("0" + aLateTime) : aLateTime) + "00";//到店最晚时间
                    orderModel.BuyerCId = SiteUserInfo.CompanyID;//采购公司编号
                    orderModel.BuyerCName = SiteUserInfo.CompanyName;//采购公司名
                    orderModel.BuyerUFullName = SiteUserInfo.ContactInfo.ContactName;//采购用户姓名
                    orderModel.BuyerUId = SiteUserInfo.ID;//采购用户编号
                    orderModel.BuyerUName = SiteUserInfo.UserName;//采购用户名
                    orderModel.CheckInDate = cDate;//入住时间
                    orderModel.CheckOutDate = lDate;//离店时间
                    orderModel.CheckState = EyouSoft.Model.HotelStructure.CheckStateList.待审结;
                    orderModel.CityCode = hotelModel.CityCode;//城市代码
                    orderModel.CityName = hotelModel.CityCode;//城市名称
                    orderModel.Comments = bookPolicy;//备注
                    orderModel.CommissionFix = rateModel.Fix;//固定反佣
                    orderModel.CommissionPercent = rateModel.Percent;//反佣比例
                    orderModel.CommissionType = EyouSoft.HotelBI.HBECommissionType.FIX;
                    orderModel.ContacterFullname = Utils.GetFormValue("hb_txtContactName");//联系人姓名
                    orderModel.ContacterMobile = Utils.GetFormValue("hb_txtContactMoible");//联系人手机
                    orderModel.ContacterTelephone = Utils.GetFormValue("hb_txtContactArea") + "-" + Utils.GetFormValue("hb_txtContactTel") + (!string.IsNullOrEmpty(Utils.GetFormValue("hb_txtContactFen")) ? ("-" + Utils.GetFormValue("hb_txtContactFen")) : "");//联系人电话
                    if (!string.IsNullOrEmpty(hotelModel.CountryCode))
                    {
                        orderModel.CountryCode = hotelModel.CountryCode;//国家代码
                    }
                    orderModel.CreateDateTime = DateTime.Now;
                    orderModel.HotelCode = hotelModel.HotelCode;
                    orderModel.HotelName = hotelModel.HotelName;
                    orderModel.IsMobileContact = Utils.GetFormValue("hb_chkIsMoible") == "1";//是否短信通知客人
                    orderModel.OrderType = EyouSoft.Model.HotelStructure.OrderType.国内现付;
                    orderModel.PaymentType = EyouSoft.HotelBI.HBEPaymentType.T;
                    orderModel.Quantity = Utils.GetInt(Utils.GetFormValue("hb_selRoom"));
                    orderModel.RatePlanCode = roomTypeModel.RatePlanCode ?? "RatePlanCode";
                    orderModel.RoomTypeCode = roomTypeModel.RoomTypeCode ?? "RoomTypeCode";
                    orderModel.RoomTypeName = roomTypeModel.RoomTypeName;
                    decimal ExcessiveFee = 0;//额外收费
                    IList<EyouSoft.Model.HotelStructure.RateInfo> riList = roomTypeModel.RoomRate.RateInfos;
                    if (riList != null && riList.Count >= 0)
                    {
                        foreach (EyouSoft.Model.HotelStructure.RateInfo rate in riList)
                        {
                            if (rate.ExcessiveFees != null)
                            {
                                ExcessiveFee += rate.ExcessiveFees.Sum(ef => ef.Amount);//计算额外收费
                            }
                        }
                    }

                    orderModel.TotalAmount = roomTypeModel.RoomRate.AmountPrice * orderModel.Quantity + ExcessiveFee;//总房价（总销售+额外收费）
                    orderModel.TotalCommission = roomTypeModel.RoomRate.RateInfos.Sum(r => r.CommissionPrice) * orderModel.Quantity;//总佣金
                    orderModel.VendorCode = roomTypeModel.VendorCode;
                    orderModel.VendorName = roomTypeModel.VendorName;
                    //特殊要求
                    orderModel.SpecialRequest = string.Format("无烟要求：{0},早餐：{1},{2},{3}", Utils.GetFormValue("hb_selIsSmoke"), Utils.GetFormValue("hb_selIsBreakfast"), Utils.GetFormValue("hb_chkFloor"), Utils.GetFormValue("hb_chkRoom"));
                    IList<EyouSoft.HotelBI.HBEResGuestInfo> guestList = new List<EyouSoft.HotelBI.HBEResGuestInfo>();
                    string[] guestName = Utils.GetFormValues("hb_txtGuestName");//获取客户姓名
                    string[] guestType = Utils.GetFormValues("hb_selGuestType");//获取客户类型
                    string guestMoible = Utils.GetFormValue("hb_chkGuestMoible");//获取通知手机
                    //添加酒店旅客
                    EyouSoft.IBLL.TicketStructure.ITicketVisitor visistorBll = EyouSoft.BLL.TicketStructure.TicketVisitor.CreateInstance();
                    for (int i = 0; i < orderModel.Quantity; i++)
                    {
                        //添加常旅客
                        if (i == 0)
                        {
                            string cName = "";
                            string eName = "";
                            if (IsLetter(guestName[i]))//判断中文或英文
                                eName = guestName[i];
                            else
                                cName = guestName[i];
                            //判断常旅客是否存在
                            if (!visistorBll.HotelVistorIsExist(cName, eName, guestMoible, SiteUserInfo.CompanyID, null))
                            {
                                EyouSoft.Model.TicketStructure.TicketVistorInfo vInfo = new EyouSoft.Model.TicketStructure.TicketVistorInfo();
                                vInfo.ChinaName = cName;
                                vInfo.EnglishName = eName;
                                vInfo.CompanyId = SiteUserInfo.CompanyID;
                                vInfo.Id = Guid.NewGuid().ToString();
                                vInfo.ContactTel = guestMoible;
                                vInfo.CardNo = "";
                                vInfo.CardType = EyouSoft.Model.TicketStructure.TicketCardType.None;
                                vInfo.ContactSex = EyouSoft.Model.CompanyStructure.Sex.未知;
                                EyouSoft.Model.TicketStructure.TicketNationInfo nation = new EyouSoft.Model.TicketStructure.TicketNationInfo();
                                nation.CountryCode = "";
                                nation.CountryName = "";
                                vInfo.NationInfo = nation;
                                vInfo.VistorType = EyouSoft.Model.TicketStructure.TicketVistorType.成人;
                                vInfo.DataType = EyouSoft.Model.TicketStructure.TicketDataType.酒店常旅客;
                                visistorBll.AddTicketVisitorInfo(vInfo);//执行添加
                            }
                        }
                        EyouSoft.HotelBI.HBEResGuestInfo guest = new EyouSoft.HotelBI.HBEResGuestInfo();
                        guest.GuestTypeIndicator = (EyouSoft.HotelBI.HBEGuestTypeIndicator)(int.Parse(guestType[i]));
                        guest.IsMobileContact = orderModel.IsMobileContact;
                        guest.Mobile = guestMoible;
                        guest.PersonName = guestName[i];
                        guestList.Add(guest);
                    }
                    orderModel.ResGuests = guestList;//赋值旅客信息
                    string errorDesc = "";//错误描述
                    if (EyouSoft.BLL.HotelStructure.HotelOrder.CreateInstance().Add(orderModel, out errorDesc) > 0)
                        Utils.ResponseMeg(true, orderModel.ResOrderId);
                    else
                        Utils.ResponseMeg(false, errorDesc == "" ? "下单失败" : "下单失败：" + errorDesc);//下单失败
                }
                #endregion
                else
                {
                    #region 绑定酒店房费
                    HotelSearch1.ImageServerPath = ImageServerPath;
                    if (hotelModel.HotelCode == "")//如果出入不存在酒店则返回到查询列表
                    {
                        Utils.ShowError(string.Format("{0}至{1}房型已满", comeDate, leaveDate), "hotel");
                        return;
                    }
                    GetRateInfoList();//绑定酒店房费列表
                    #endregion
                }
            }
            else//如果错误信息不为空
            {
                //如果是保存操作则输出错误信息
                if (method == "save")
                {
                    Utils.ResponseMeg(false, themess);
                }
                else//如果是初始化页面时则跳转到错误页面
                {
                    Utils.ShowError(themess, "hotel");
                }
            }
        }

        #region 验证首字母是否为字母
        /// <summary>
        /// 验证首字母是否为字母
        /// </summary>
        /// <param name="CheckString"></param>
        /// <returns></returns>
        private bool IsLetter(string CheckString)
        {
            bool IsTrue = false;
            if (!string.IsNullOrEmpty(CheckString))
            {
                Regex rx = new Regex("^[\u4e00-\u9fa5]$");
                IsTrue = rx.IsMatch(CheckString.Substring(0, 1));
                rx = null;
            }
            return !IsTrue;
        }
        #endregion

        #region IsOK方法查询酒店是否有错误信息
        /// <summary>
        /// 查询酒店时判断是否有错误
        /// </summary>
        /// <param name="errorInfo">错误实体</param>
        /// <param name="model">酒店实体</param>
        /// <returns></returns>
        protected string IsOk(EyouSoft.HotelBI.ErrorInfo errorInfo, EyouSoft.Model.HotelStructure.HotelInfo model)
        {

            string mess = "";//错误消息
            //如果错误实体不为空
            if (errorInfo != null)
            {
                switch (errorInfo.ErrorType)//错误类别
                {
                    case EyouSoft.HotelBI.ErrorType.业务级错误:
                        mess = string.Format("{0}至{1}房型已满", comeDate, leaveDate);
                        break;
                    case EyouSoft.HotelBI.ErrorType.未知错误:
                        mess = "提交数据超时，请稍后在试";
                        break;
                    case EyouSoft.HotelBI.ErrorType.None:
                        mess = "";
                        break;
                    case EyouSoft.HotelBI.ErrorType.系统级错误:
                        mess = "查询数据时出错";
                        break;

                }
            }
            if (mess == "")//如果错误消息为空
            {  //判断酒店实体或酒店代码是否存在
                if (model == null || string.IsNullOrEmpty(model.HotelCode))
                {
                    mess = string.Format("{0}至{1}房型已满", comeDate, leaveDate);
                }
            }
            return mess;//返回错误消息

        }
        #endregion

        #region 绑定酒店房费列表
        /// <summary>
        /// 绑定酒店房费列表
        /// </summary>
        protected void GetRateInfoList()
        {
            //是否存在房费明细
            if (roomTypeModel != null && roomTypeModel.RoomRate != null && roomTypeModel.RoomRate.RateInfos != null)
            {
                IList<EyouSoft.Model.HotelStructure.RateInfo> rateList = roomTypeModel.RoomRate.RateInfos;//获取价格明细列表
                StringBuilder strBuilder1 = new StringBuilder();//结算价行
                StringBuilder strBuilder2 = new StringBuilder();//反佣价行
                StringBuilder strBuilder3 = new StringBuilder();//早餐行
                StringBuilder strBuilder = new StringBuilder();//整个房费构造
                //构造表标题
                strBuilder.Append("<table  width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                                    "<tr><th align=\"center\">&nbsp;</th>" +
                                    "<th align=\"center\">月/日</th>" +
                                    "<th align=\"center\">周一</th>" +
                                    "<th align=\"center\">周二</th>" +
                                    "<th align=\"center\">周三</th>" +
                                    "<th align=\"center\">周四</th>" +
                                    "<th align=\"center\">周五</th>" +
                                    "<th align=\"center\">周六</th>" +
                                    "<th align=\"center\">周日</th>" +
                                    "<th align=\"center\">总计</th>" +
                                    "</tr>");
                DateTime firstDate = roomTypeModel.RoomRate.StartDate;//入住日期
                DateTime endDate = roomTypeModel.RoomRate.EndDate;//离店日期
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
                            strBuilder1.AppendFormat(" <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{1}<br />返佣价：<br />￥{2}</td>", forNum * 3, Utils.GetMoney(roomTypeModel.RoomRate.AmountPrice), Utils.GetMoney(rateList.Sum(r => r.CommissionAmount)));
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
                                strBuilder1.AppendFormat(" <td class=\"orange strong\" rowspan=\"{0}\" align=\"center\">销售价：<br />￥{1}<br />返佣价：<br />￥{2}</td>", forNum * 3, EyouSoft.Common.Utils.GetMoney(roomTypeModel.RoomRate.AmountPrice), EyouSoft.Common.Utils.GetMoney(rateList.Sum(r => r.CommissionAmount)));
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
        #endregion

        #region 导航条下的图片路径
        /// <summary>
        /// 获取导航条图片的链接地址
        /// </summary>
        /// <returns></returns>
        protected string GetMainImgLink()
        {
            string linkHref = Domain.UserBackCenter + "/hotelcenter/hotelordermanage/teamonlinesubmit.aspx";
            if (!IsLogin)
            {
                linkHref = EyouSoft.Security.Membership.UserProvider.BuildLoginAndReturnUrl(linkHref, "登录");
            }
            return linkHref;
        }
        #endregion
    }
}
