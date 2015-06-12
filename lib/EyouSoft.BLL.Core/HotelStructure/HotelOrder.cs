using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.HotelBI;
using System.Xml.Linq;

namespace EyouSoft.BLL.HotelStructure
{
    /// <summary>
    /// 酒店订单-业务逻辑层
    /// </summary>
    /// 鲁功源  2010-12-02
    public class HotelOrder : EyouSoft.IBLL.HotelStructure.IHotelOrder
    {
        private readonly EyouSoft.IDAL.HotelStructure.IHotelOrder dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.HotelStructure.IHotelOrder>();

        #region CreateInstance
        /// <summary>
        /// 创建酒店订单业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.HotelStructure.IHotelOrder CreateInstance()
        {
            EyouSoft.IBLL.HotelStructure.IHotelOrder op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.HotelStructure.IHotelOrder>();
            }
            return op;
        }
        #endregion

        #region private memebers
        /// <summary>
        /// 发送预订指令，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="info">订单信息业务实体</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功  负值或0失败</returns>
        private int CreateOrderRequest(EyouSoft.Model.HotelStructure.OrderInfo info, out string errorDesc)
        {
            errorDesc = string.Empty;

            #region 发送指令
            int createOrderRequestResult = 1;

            info.ResponseXML = Utils.CreateRequest(info.RequestXML);
            ErrorInfo errorInfo = Utils.ResponseErrorHandling(info.ResponseXML);
            switch (errorInfo.ErrorType)
            {
                case ErrorType.未知错误: createOrderRequestResult = -1000; break;
                case ErrorType.系统级错误: createOrderRequestResult = -1001; break;
                case ErrorType.业务级错误: createOrderRequestResult = -1002; errorDesc = errorInfo.ErrorDesc; break;
            }

            if (createOrderRequestResult < 1) return createOrderRequestResult;
            #endregion

            try
            {
                XElement xResponse = XElement.Parse(info.ResponseXML);
                XElement xHotelReservation = xResponse.Element("HotelResRS").Element("HotelReservations").Element("HotelReservation");// Utils.GetXElement(Utils.GetXElement(Utils.GetXElement(xResponse, "HotelResRS"), "HotelReservations"), "HotelReservation");
                info.ResOrderId = Utils.GetXAttributeValue(xHotelReservation, "ResOrderID");
            }
            catch
            {
                createOrderRequestResult = -1003;
            }

            if (createOrderRequestResult != -1003 && string.IsNullOrEmpty(info.ResOrderId))
            {
                createOrderRequestResult = -1004;
            }

            return createOrderRequestResult;
        }

        /// <summary>
        /// 发送取消订单指令，正值：成功  负值或0失败
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="cancelReason">取消原因</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功  负值或0失败</returns>
        private int CancelOrderRequest(string resOrderId, string cancelReason, out string errorDesc)
        {
            errorDesc = string.Empty;
            int cancelOrderRequestResult = 1;
            EyouSoft.HotelBI.HBECancelOrderInfo info = new HBECancelOrderInfo();

            info.ResOrderId = resOrderId;
            info.CancelReason = cancelReason;
            info.ResponseXML = Utils.CreateRequest(info.RequestXML);

            ErrorInfo errorInfo = Utils.ResponseErrorHandling(info.ResponseXML);
            switch (errorInfo.ErrorType)
            {
                case ErrorType.未知错误: cancelOrderRequestResult = -1000; break;
                case ErrorType.系统级错误: cancelOrderRequestResult = -1001; break;
                case ErrorType.业务级错误: cancelOrderRequestResult = -1002; errorDesc = errorInfo.ErrorDesc; break;
            }

            if (cancelOrderRequestResult < 1) return cancelOrderRequestResult;

            return cancelOrderRequestResult;
        }

        /// <summary>
        /// 发送订单详细信息指令
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="errorCode">错误代码 正值：成功 负值或0：失败</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns></returns>
        private EyouSoft.Model.HotelStructure.OrderInfo OrderDetailRequest(string resOrderId, out int errorCode, out string errorDesc)
        {
            errorCode = 1;
            errorDesc = string.Empty;

            #region 发送指令
            EyouSoft.HotelBI.HBESingleOrderSearchInfo requestInfo = new HBESingleOrderSearchInfo();
            requestInfo.ResOrderId = resOrderId;
            requestInfo.ResponseXML = Utils.CreateRequest(requestInfo.RequestXML);

            ErrorInfo errorInfo = Utils.ResponseErrorHandling(requestInfo.ResponseXML);
            switch (errorInfo.ErrorType)
            {
                case ErrorType.未知错误: errorCode = -1000; break;
                case ErrorType.系统级错误: errorCode = -1001; break;
                case ErrorType.业务级错误: errorCode = -1002; errorDesc = errorInfo.ErrorDesc; break;
            }

            if (errorCode < 1) return null;
            #endregion

            var info = this.ParseOrderDetailResponseXML(requestInfo.ResponseXML, out errorCode);

            if (info != null)
            {
                info.ResponseXML = requestInfo.ResponseXML;
            }

            return info;
        }

        /// <summary>
        /// 解析订单详细信息返回指令
        /// </summary>
        /// <param name="xml">返回指令</param>
        /// <param name="errorCode">错误代码 正值：成功 负值或0：失败</param>
        /// <returns></returns>
        private EyouSoft.Model.HotelStructure.OrderInfo ParseOrderDetailResponseXML(string xml, out int errorCode)
        {
            errorCode = 1;
            EyouSoft.Model.HotelStructure.OrderInfo info = null;

            try
            {
                info = new EyouSoft.Model.HotelStructure.OrderInfo();

                XElement xResponse = XElement.Parse(xml);
                XElement xHotelResDetailSearchRS = xResponse.Element("HotelResDetailSearchRS");
                XElement xHotelReservations = xHotelResDetailSearchRS.Element("HotelReservations");
                XElement xHotelReservation = xHotelReservations.Element("HotelReservation");
                //XElement xHotelReservation = xResponse.Element("HotelResDetailSearchRS").Element("HotelReservations").Element("HotelReservation");

                info.CreateDateTime = EyouSoft.Common.Utility.GetDateTime112(Utils.GetXAttributeValue(xHotelReservation, "CreateDateTime"));
                info.ResOrderId = Utils.GetXAttributeValue(xHotelReservation, "ResOrderId");
                info.ResStatus = (HBEResStatus)Enum.Parse(typeof(HBEResStatus), Utils.GetXAttributeValue(xHotelReservation, "ResStatus"));
                info.TotalAmount = Common.Utility.GetDecimal(xHotelReservation.Element("TotalAmount").Value);

                XElement xRoomStays = xHotelReservation.Element("RoomStays");
                XElement xRoomStay = xRoomStays.Element("RoomStay");
                XElement xBasicProperty = xRoomStay.Element("BasicProperty");
                info.HotelCode = Utils.GetXAttributeValue(xBasicProperty, "HotelCode");
                info.HotelName = Utils.GetXAttributeValue(xBasicProperty, "HotelName");
                info.CityCode = Utils.GetXAttributeValue(xBasicProperty, "CityCode");
                info.CityName = Utils.GetXAttributeValue(xBasicProperty, "CityName");
                info.Quantity = Common.Utility.GetInt(xBasicProperty.Element("RoomQuantity").Value);
                info.SpecialRequest = Utils.GetXElement(xRoomStay, "SpecialRequest").Value;
                XElement xTimeSpan = Utils.GetXElement(xRoomStay, "TimeSpan");
                info.CheckInDate = EyouSoft.Common.Utility.GetDateTime(Utils.GetXAttributeValue(xTimeSpan, "StartDate"));
                info.CheckOutDate = EyouSoft.Common.Utility.GetDateTime(Utils.GetXAttributeValue(xTimeSpan, "EndDate"));

                XElement xRoomRates = xRoomStay.Element("RoomRates");
                XElement xRoomRate = xRoomRates.Element("RoomRate");
                info.RoomTypeCode = Utils.GetXAttributeValue(xRoomRate, "RoomTypeCode");
                info.RoomTypeName = Utils.GetXAttributeValue(xRoomRate, "RoomTypeName");

                info.Rates = this.ParseResRates(xRoomRate);

                #region 旅客信息
                XElement xResGuests = xHotelReservation.Element("ResGuests");
                info.ResGuests = this.ParseResGuests(xResGuests);
                #endregion

                #region 全局信息
                XElement xResGlobalInfo = xHotelReservation.Element("ResGlobalInfo");
                info.ArriveEarlyTime = Utils.GetXElement(xResGlobalInfo, "ArriveEarlyTime").Value;
                info.ArriveLateTime = Utils.GetXElement(xResGlobalInfo, "ArriveLateTime").Value;
                info.PaymentType = (HBEPaymentType)Enum.Parse(typeof(HBEPaymentType), Utils.GetXElement(xResGlobalInfo, "Payment").Value);
                #endregion

                #region 联系人信息
                XElement xContactorInfo = xHotelReservation.Element("ContactorInfo");
                XElement xProfile = xContactorInfo.Element("Profile");
                info.ContacterFullname = Utils.GetXElement(xProfile, "PersonName").Value;
                info.ContacterMobile = Utils.GetXElement(xProfile, "Mobile").Value;
                info.ContacterTelephone = Utils.GetXElement(xProfile, "Telephone").Value;
                #endregion
            }
            catch
            {
                errorCode = -1003;
                info = null;
            }

            return info;
        }

        /// <summary>
        /// 解析旅客信息集合
        /// </summary>
        /// <param name="xResGuests">XElement</param>
        /// <returns></returns>
        private IList<EyouSoft.HotelBI.HBEResGuestInfo> ParseResGuests(XElement xResGuests)
        {
            IList<EyouSoft.HotelBI.HBEResGuestInfo> guests = new List<EyouSoft.HotelBI.HBEResGuestInfo>();
            var iEnumerableGuests = Utils.GetXElements(xResGuests, "ResGuest");

            foreach (var xResGuest in iEnumerableGuests)
            {
                XElement xGuestProfile = xResGuest.Element("GuestProfile");
                EyouSoft.HotelBI.HBEResGuestInfo guest = new HBEResGuestInfo();
                guest.PersonName = Utils.GetXElement(xGuestProfile, "PersonName").Value;
                guest.Mobile = Utils.GetXElement(xGuestProfile, "Mobile").Value;
                guest.Telephone = Utils.GetXElement(xGuestProfile, "Telephone").Value;

                guests.Add(guest);
            }

            return guests;
        }

        /// <summary>
        /// 解析价格明细
        /// </summary>
        /// <param name="xRoomRate">XElement</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.HotelStructure.RateInfo> ParseResRates(XElement xRoomRate)
        {
            IList<EyouSoft.Model.HotelStructure.RateInfo> rates = new List<EyouSoft.Model.HotelStructure.RateInfo>();
            var xRates = xRoomRate.Element("Rates");
            var iEnumerableRates = Utils.GetXElements(xRates, "Rate");

            foreach (var xRate in iEnumerableRates)
            {
                DateTime startDate = EyouSoft.Common.Utility.GetDateTime(Utils.GetXAttributeValue(xRate, "StartDate"), DateTime.Now);
                DateTime endDate = EyouSoft.Common.Utility.GetDateTime(Utils.GetXAttributeValue(xRate, "EndDate"), DateTime.Now);
                int days = (endDate - startDate).Days;

                for (int i = 0; i <= days; i++)
                {
                    var rate = new EyouSoft.Model.HotelStructure.RateInfo();
                    rate.CurrData = startDate.AddDays(i);
                    rate.AmountPrice = EyouSoft.Common.Utility.GetDecimal(Utils.GetXAttributeValue(xRate, "AmountPrice"));
                    rate.DisplayPrice = EyouSoft.Common.Utility.GetDecimal(Utils.GetXAttributeValue(xRate, "DisplayPrice"));
                    var xFreeMeal = Utils.GetXElement(xRate, "FreeMeal");
                    rate.FreeMeal = Common.Utility.GetInt(xFreeMeal.Value);

                    rates.Add(rate);
                }
            }

            return rates;
        }

        /*/// <summary>
        /// 根据酒店接口(HBE)订单状态获取平台酒店订单状态
        /// </summary>
        /// <param name="orderHintState">酒店接口(HBE)订单状态</param>
        /// <returns></returns>
        private EyouSoft.Model.HotelStructure.OrderStateList GetOrderStateByResStatus(EyouSoft.HotelBI.HBEResStatus orderResState)
        {
            EyouSoft.Model.HotelStructure.OrderStateList state = EyouSoft.Model.HotelStructure.OrderStateList.处理中;

            return state;
        }*/
        #endregion

        #region IHotelOrder 成员
        /// <summary>
        /// 添加订单，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="model">酒店订单实体</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int Add(EyouSoft.Model.HotelStructure.OrderInfo model, out string errorDesc)
        {
            errorDesc = string.Empty;

            #region 验证
            if (model == null) return 0;
            if (string.IsNullOrEmpty(model.HotelCode)) return -1;
            if (string.IsNullOrEmpty(model.CityCode)) return -2;
            if (string.IsNullOrEmpty(model.CountryCode)) return -3;
            if (string.IsNullOrEmpty(model.RatePlanCode)) return -4;
            if (string.IsNullOrEmpty(model.RoomTypeCode)) return -5;
            if (model.CheckInDate == DateTime.MinValue) return -6;
            if (model.CheckOutDate == DateTime.MinValue) return -7;
            if (string.IsNullOrEmpty(model.VendorCode)) return -8;
            if (string.IsNullOrEmpty(model.VendorName)) return -9;
            if (model.Quantity <= 0) return -10;
            if (string.IsNullOrEmpty(model.ArriveEarlyTime)) return -11;
            if (string.IsNullOrEmpty(model.ArriveLateTime)) return -12;
            if (model.TotalAmount <= 0) return -13;
            if (model.ResGuests == null || model.ResGuests.Count < 1) return -14;
            if (string.IsNullOrEmpty(model.ResGuests[0].PersonName)) return -15;
            if (string.IsNullOrEmpty(model.ResGuests[0].Mobile)) return -16;
            if (string.IsNullOrEmpty(model.ContacterFullname)) return -17;
            if (string.IsNullOrEmpty(model.ContacterMobile)) return -18;
            if (string.IsNullOrEmpty(model.BuyerCId)) return -19;
            if (string.IsNullOrEmpty(model.BuyerUId)) return -20;
            if (model.CheckInDate < DateTime.Today) return -21;
            int datediff = (model.CheckOutDate - model.CheckInDate).Days;
            if (datediff < 1) return -22;
            if (datediff > 30) return -23;

            model.CreateDateTime = DateTime.Now;
            model.OrderId = Guid.NewGuid().ToString();
            model.CheckState = EyouSoft.Model.HotelStructure.CheckStateList.待审结;
            model.ResStatus = HBEResStatus.RES;
            #endregion

            int orderRequestResult = this.CreateOrderRequest(model, out errorDesc);

            if (orderRequestResult < 1) return orderRequestResult;

            return dal.Add(model) ? 1 : -101;
        }

        /// <summary>
        /// 分页获取酒店订单
        /// </summary>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="SearchInfo">查询实体</param>
        /// <returns>酒店订单列表</returns>
        public IList<EyouSoft.Model.HotelStructure.OrderInfo> GetList(int PageSize, int PageIndex, ref int RecordCount, EyouSoft.Model.HotelStructure.SearchOrderInfo SearchInfo)
        {
            return dal.GetList(PageSize, PageIndex, ref RecordCount, SearchInfo);
        }

        /// <summary>
        /// 取消订单，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="cancelReason">取消原因</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns>正值：成功  负值或0失败</returns>
        public int Cancel(string resOrderId, string cancelReason, out string errorDesc)
        {
            errorDesc = string.Empty;
            if (string.IsNullOrEmpty(resOrderId)) return 0;

            int cancelRequestResult = this.CancelOrderRequest(resOrderId, cancelReason, out errorDesc);

            if (cancelRequestResult < 1) return cancelRequestResult;

            return this.SetOrderState(resOrderId,  EyouSoft.HotelBI.HBEResStatus.XXX) ? 1 : -101;
        }

        /// <summary>
        /// 获取订单信息业务实体
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="errorCode">错误代码 正值：成功 负值或0：失败</param>
        /// <param name="errorDesc">错误描述</param>
        /// <returns></returns>
        public EyouSoft.Model.HotelStructure.OrderInfo GetInfo(string resOrderId, out int errorCode, out string errorDesc)
        {
            errorCode = 1;
            errorDesc = string.Empty;

            if (string.IsNullOrEmpty(resOrderId)) { errorCode = 0; return null; }

            var info = this.OrderDetailRequest(resOrderId, out errorCode, out errorDesc);

            if (info != null)
            {
                var localInfo = dal.GetOrderInfo(resOrderId);

                if (localInfo != null)
                {
                    info.ResGuests = localInfo.ResGuests;
                    info.Comments = localInfo.Comments;

                    info.ContacterFullname = localInfo.ContacterFullname;
                    info.ContacterMobile = localInfo.ContacterMobile;
                    info.ContacterTelephone = localInfo.ContacterTelephone;

                    #region 接口未返回的佣金及金额信息处理
                    //返佣信息
                    info.CommissionType = localInfo.CommissionType;
                    info.CommissionFix = localInfo.CommissionFix;
                    info.CommissionPercent = localInfo.CommissionPercent;
                    //总金额
                    info.TotalAmount = localInfo.TotalAmount;
                    #endregion

                    //状态
                    info.ResStatus = localInfo.ResStatus;
                    info.CheckState = localInfo.CheckState;
                }
            }

            return info;
        }

        /// <summary>
        /// 执行酒店订单异步通知指令，正值时成功，负值或0时失败
        /// </summary>
        /// <param name="hint">指令内容</param>
        /// <returns>正值：成功 负值或0：失败</returns>
        public int ExecOrderHint(string hint)
        {
            int execResult = 0;

            if (string.IsNullOrEmpty(hint)) return 0;

            try
            {
                XElement xHotelOrderStatusNotifRQ = XElement.Parse(hint);
                var xOrderStatusMessage = Utils.GetXElement(xHotelOrderStatusNotifRQ, "OrderStatusMessage");
                var xHotelReservationID = Utils.GetXElement(xOrderStatusMessage, "HotelReservationID");
                var xStatus = Utils.GetXElement(xOrderStatusMessage, "Status");

                string resOrderId = Utils.GetXAttributeValue(xHotelReservationID, "ResID_Value");
                string currentStatusCode = Utils.GetXAttributeValue(xStatus, "CurrentStatusCode");

                EyouSoft.HotelBI.HBEResStatus resStatus = (EyouSoft.HotelBI.HBEResStatus)Enum.Parse(typeof(EyouSoft.HotelBI.HBEResStatus), currentStatusCode);

                if (string.IsNullOrEmpty(resOrderId)) return -1;
                execResult = this.SetOrderState(resOrderId, resStatus) ? 1 : -2;
            }
            catch
            {
                execResult = -1003;
            }

            return execResult;
        }

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public bool SetOrderState(string resOrderId, EyouSoft.HotelBI.HBEResStatus orderState)
        {
            if (string.IsNullOrEmpty(resOrderId)) return false;

            return dal.SetOrderState(resOrderId, orderState);
        }

        /// <summary>
        /// 设置订单审核状态
        /// </summary>
        /// <param name="resOrderId">订单号(航旅通接口)</param>
        /// <param name="checkState">订单审核状态</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetCheckState(string resOrderId, EyouSoft.Model.HotelStructure.CheckStateList checkState)
        {
            if (string.IsNullOrEmpty(resOrderId)) return false;

            return dal.SetCheckState(resOrderId, checkState);
        }
        #endregion
    }
}
