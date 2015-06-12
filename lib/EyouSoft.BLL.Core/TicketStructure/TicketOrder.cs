using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.Model.TicketStructure;

namespace EyouSoft.BLL.TicketStructure
{   
    /// <summary>
    /// 机票订单操作
    /// </summary>
    /// Author:徐天勇 2010-10-29
    public class TicketOrder : EyouSoft.IBLL.TicketStructure.ITicketOrder
    {
        private readonly EyouSoft.IDAL.TicketStructure.ITicketOrder idalOrder = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketOrder>();
        private readonly EyouSoft.IDAL.TicketStructure.ITicketPayList idalTicketPayList = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TicketStructure.ITicketPayList>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.TicketStructure.ITicketOrder CreateInstance()
        {
            EyouSoft.IBLL.TicketStructure.ITicketOrder op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<EyouSoft.IBLL.TicketStructure.ITicketOrder>();
            }
            return op;
        }

        /// <summary>
        /// 修改订单信息的联系方式
        /// <summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="buyCName">联系公司</param>
        /// <param name="buyerContactName">联系人</param>
        /// <param name="buyerContactMoible">手机</param>
        /// <param name="buyerContactAddress">地址</param>
        public bool UpdateBuyerContact(string orderId, string buyCName, string buyerContactName, string buyerContactMoible, string buyerContactAddress)
        {
            return idalOrder.UpdateBuyerContact(orderId, buyCName, buyerContactName, buyerContactMoible, buyerContactAddress);
        }

        /// <summary>
        /// 采购商修改订单服务备注
        /// <summary>
        /// <param name="remark">服务备注</param>
        /// <param name="orderId">点单ID</param>
        /// <returns></returns>
        public bool UpdateServiceRemark(string orderId,string remark)
        {
            return idalOrder.UpdateServiceNote(orderId, remark);
        }
        /// <summary>
        /// 修改订单采购商备注(下订单时的特殊备注)
        /// <summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="buyerRemark">特殊备注</param>
        /// <returns></returns>
        public bool UpdateBuyerRemark(string orderId, string buyerRemark)
        {
            return idalOrder.UpdateBuyerRemark(orderId, buyerRemark);
        }

        /// <summary>
        /// 供应商订单精确查询(不分页)
        /// </summary>
        /// <param name="supplierId">供应商公司ID</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="ticketNumber">机票编号</param>
        /// <param name="travellName">游客名</param>
        /// <param name="pnrNo">PNR码</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> precisionSearch(string supplierId,string orderNo,string ticketNumber,string travellName,string pnrNo)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.SupplierCId = supplierId;
            if(orderNo!=string.Empty)
            searchInfo.OrderNo = orderNo;
            if (ticketNumber != string.Empty)
                searchInfo.TicketNumber = ticketNumber;
            if(travellName!=string.Empty)
            searchInfo.TravellerName = travellName;
            if(pnrNo!=string.Empty)
            searchInfo.PNR = pnrNo;
            IList<OrderInfo> orderList = idalOrder.GetOrders(searchInfo);
            SetSeattleName(orderList);
            return orderList;
        }

        /// <summary>
        /// 供应商订单精确查询(分页)
        /// </summary>
        /// <param name="supplierId">供应商公司ID</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="ticketNumber">机票编号</param>
        /// <param name="travellName">游客名</param>
        /// <param name="pnrNo">PNR码</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> precisionSearch(string supplierId,string orderNo, string ticketNumber, string travellName, string pnrNo,int pageSize,int pageIndex,ref int recordCount)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.SupplierCId = supplierId;
            if (orderNo != string.Empty)
                searchInfo.OrderNo = orderNo;
            if (ticketNumber != string.Empty)
                searchInfo.TicketNumber = ticketNumber;
            if (travellName != string.Empty)
                searchInfo.TravellerName = travellName;
            if (pnrNo != string.Empty)
                searchInfo.PNR = pnrNo;
            IList<OrderInfo> orderList= idalOrder.GetOrders(searchInfo,pageSize,pageIndex,ref recordCount);
            SetSeattleName(orderList);
            return orderList;
        }


        /// <summary>
        /// 供应商订单搜索查询(不分页)
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="flightId">航空公司ID</param>
        /// <param name="rateType">机票类型</param>
        /// <param name="startAddress">始发地</param>
        /// <param name="endAddress">目的地</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="changeState">变更状态</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> ShouSuoSearch(string supplierId,int? flightId,RateType?rateType, int? startAddress, int? endAddress, DateTime? startDate, DateTime? endDate, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.SupplierCId = supplierId;
            searchInfo.FlightId=flightId;
            searchInfo.RateType=rateType;
            searchInfo.HomeCityId = startAddress;
            searchInfo.DestCityId = endAddress;
            searchInfo.OrderStartTime = startDate;
            searchInfo.OrderFinishTime = endDate;
            searchInfo.OrderChangeState = changeState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.OrderState = orderState;
            IList<OrderInfo> orderList = idalOrder.GetOrders(searchInfo);
            SetSeattleName(orderList);
            return orderList;
        }

        /// <summary>
        /// 供应商订单搜索查询(分页)
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="flightId">航空公司ID</param>
        /// <param name="rateType">机票类型</param>
        /// <param name="startAddress">始发地</param>
        /// <param name="endAddress">目的地</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="changeState">变更状态</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="recordCount">记录数</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> ShouSuoSearch(string supplierId, int? flightId, RateType? rateType, int? startAddress, int? endAddress, DateTime? startDate, DateTime? endDate, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState,int pageSize,int pageIndex,ref int recordCount)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.SupplierCId = supplierId;
            searchInfo.FlightId = flightId;
            searchInfo.RateType = rateType;
            searchInfo.HomeCityId = startAddress;
            searchInfo.DestCityId = endAddress;
            searchInfo.OrderStartTime = startDate;
            searchInfo.OrderFinishTime = endDate;
            searchInfo.OrderChangeState = changeState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.OrderState = orderState;
            IList<OrderInfo> orderList= idalOrder.GetOrders(searchInfo,pageSize,pageIndex,ref recordCount);
            SetSeattleName(orderList);
            return orderList;
        }

        /// <summary>
        /// 设置订单信息中的始发地目的地名称
        /// </summary>
        /// <param name="orderInfo"></param>
        private void SetSeattleName(OrderInfo orderInfo)
        {   
            if (orderInfo != null)
            {
                Object seatCache = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketSeattle);
                EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo supplierBll = EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance();
                if (seatCache == null)
                {
                    IList<EyouSoft.Model.TicketStructure.TicketSeattle> seatList = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
                    Dictionary<int, string> seatDic = new Dictionary<int, string>();
                    foreach (EyouSoft.Model.TicketStructure.TicketSeattle seat in seatList)
                    {
                        seatDic.Add(seat.SeattleId, seat.Seattle);
                    }
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketSeattle, seatDic);
                    seatCache = seatDic;
                }
                orderInfo.SuccessRate = supplierBll.GetSuccessRate(orderInfo.SupplierCId);
                Dictionary<int, string> seatDic2 = (Dictionary<int, string>)seatCache;

                if (seatDic2.Where(i => i.Key == orderInfo.HomeCityId).Count() > 0)
                {
                    orderInfo.HomeCityName = seatDic2[orderInfo.HomeCityId];
                    
                }
                if (seatDic2.Where(i => i.Key == orderInfo.DestCityId).Count() > 0)
                {
                    orderInfo.DestCityName = seatDic2[orderInfo.DestCityId];
                }
            }
        }
        /// <summary>
        /// 设置订单集合信息中的始发地目的地名称
        /// </summary>
        /// <param name="infoList"></param>
        private void SetSeattleName(IList<OrderInfo> orderList)
        {  
            
            if (orderList != null)
            {   
                EyouSoft.IBLL.TicketStructure.ITicketSupplierInfo supplierBll= EyouSoft.BLL.TicketStructure.TicketSupplierInfo.CreateInstance();
                Object seatCache = EyouSoft.Cache.Facade.EyouSoftCache.GetCache(EyouSoft.CacheTag.AirTickets.TicketSeattle);
                if (seatCache == null)
                {   
                    IList<EyouSoft.Model.TicketStructure.TicketSeattle> seatList = EyouSoft.BLL.TicketStructure.TicketSeattle.CreateInstance().GetTicketSeattle(null);
                    Dictionary<int, string> seatDic = new Dictionary<int, string>();
                    foreach (EyouSoft.Model.TicketStructure.TicketSeattle seat in seatList)
                    {
                        seatDic.Add(seat.SeattleId, seat.Seattle);
                    }
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.CacheTag.AirTickets.TicketSeattle, seatDic);
                    seatCache = seatDic;
                }

                Dictionary<int, string> seatDic2 = (Dictionary<int, string>)seatCache;
                foreach (OrderInfo info in orderList)
                {
                    info.SuccessRate = supplierBll.GetSuccessRate(info.SupplierCId);
                    if (seatDic2.Where(i => i.Key == info.HomeCityId).Count() > 0)
                    {
                        info.HomeCityName = seatDic2[info.HomeCityId];
                    }
                    if (seatDic2.Where(i => i.Key == info.DestCityId).Count() > 0)
                    {
                        info.DestCityName = seatDic2[info.DestCityId];
                    }
                }
            }
        }

        /// <summary>
        /// 采购商订单查询(按选择类型)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="pnrNo">pnr号</param>
        /// <param name="travellerName">旅客姓名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId,string pnrNo,string travellerName,string orderNo)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.BuyerCId = buyerCId;
            if (pnrNo!=string.Empty)
                searchInfo.PNR= pnrNo;
            if (travellerName!=string.Empty)
                searchInfo.TravellerName = travellerName;
            if (orderNo!=string.Empty)
                searchInfo.OrderNo = orderNo;
            IList<OrderInfo> orderList=idalOrder.GetOrders(searchInfo);
            SetSeattleName(orderList);
            return orderList;
        }

         



        /// <summary>
        /// 采购商订单查询(按选择类型)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="pnrNo">pnr号</param>
        /// <param name="travellerName">旅客姓名</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, string pnrNo, string travellerName, string orderNo,int pageSize,int pageIndex,ref int recordCount)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.BuyerCId = buyerCId;
            if (pnrNo != string.Empty)
                searchInfo.PNR = pnrNo;
            if (travellerName != string.Empty)
                searchInfo.TravellerName = travellerName;
            if (orderNo != string.Empty)
                searchInfo.OrderNo = orderNo;
             IList<OrderInfo> orderList= idalOrder.GetOrders(searchInfo,pageSize,pageIndex,ref recordCount);
             SetSeattleName(orderList);
             return orderList;
        }

        /// <summary>
        /// 采购商订单查询(按状态查询)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="date">当前日期</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">改变类型</param>
        /// <param name="changeState">改变状态</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, DateTime? date, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.BuyerCId = buyerCId;
            searchInfo.FixedDate = date;
            searchInfo.OrderState = orderState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.OrderChangeState = changeState;
            IList<OrderInfo> orderList = idalOrder.GetOrders(searchInfo);
            SetSeattleName(orderList);
            return orderList;
        }

        /// <summary>
        /// 采购商订单查询(按状态查询)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="date">当前日期</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">改变类型</param>
        /// <param name="changeState">改变状态</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, DateTime? date, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState, int pageSize, int pageIndex, ref int recordCount)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.BuyerCId = buyerCId;
            searchInfo.FixedDate = date;
            searchInfo.OrderState = orderState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.OrderChangeState = changeState;
            IList<OrderInfo> orderList = idalOrder.GetOrders(searchInfo, pageSize, pageIndex, ref recordCount);
            SetSeattleName(orderList);
            return orderList;
        }
        

        /// <summary>
        /// 采购商供应商订单查询
        /// </summary>
        /// <param name="searchInfo">订单查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(OrderSearchInfo searchInfo)
        {
           IList<OrderInfo> orderList= idalOrder.GetOrders(searchInfo);
           SetSeattleName(orderList);
           return orderList;
        }

        /// <summary>
        /// 采购商供应商订单查询
        /// </summary>
        /// <param name="searchInfo">订单查询实体</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(OrderSearchInfo searchInfo,int pageSize,int pageIndex,ref int recordCount)
        {
            IList<OrderInfo> orderList = idalOrder.GetOrders(searchInfo, pageSize, pageIndex, ref recordCount);
            SetSeattleName(orderList);
            return orderList;
        }

        /// <summary>
        /// 供应商订单处理统计信息
       /// <summary>
        /// <param name="supplierId">供应商ID</param>
        /// <returns></returns>
        public IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> GetSupplierHandelStats(string supplierId)
        {
            return idalOrder.GetSupplierHandelStats(supplierId);
        }

        /// <summary>
        /// 获取供应商具体处理统计信息
        /// </summary>
        /// <param name="supplierId">供应商公司Id</param>
        /// <param name="type">业务类型</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">订单更改类型</param>
        /// <param name="changeState">订单状态</param>
        /// <returns></returns>
        public IList<OrderInfo> GetSupplierHandelStatsDetail(string supplierId,RateType? type,OrderState? orderState,OrderChangeType? changeType,OrderChangeState? changeState)
        {   
            OrderSearchInfo searchInfo=new OrderSearchInfo();
            searchInfo.SupplierCId=supplierId;
            searchInfo.OrderState=orderState;
            searchInfo.RateType = type;
            searchInfo.OrderChangeType=changeType;
            searchInfo.OrderChangeState = changeState;
            return idalOrder.GetOrders(searchInfo);
        }

        /// <summary>
        /// 获取供应商具体处理统计信息(分页)
        /// </summary>
        /// <param name="supplierId">供应商公司Id</param>
        /// <param name="type">业务类型</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">订单更改类型</param>
        /// <param name="changeState">订单状态</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IList<OrderInfo> GetSupplierHandelStatsDetail(string supplierId, RateType? type, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState, int pageSize, int pageIndex, ref int recordCount)
        {
            OrderSearchInfo searchInfo = new OrderSearchInfo();
            searchInfo.SupplierCId = supplierId;
            searchInfo.OrderState = orderState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.RateType = type;
            searchInfo.OrderChangeState = changeState;
            IList<OrderInfo> orderList=idalOrder.GetOrders(searchInfo, pageSize, pageIndex, ref recordCount);
            SetSeattleName(orderList);//设置始发目的地名称
            return orderList;
        }

        /// <summary>
        /// 供应商订单统计
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="supplierId">供应商ID</param>
        /// <param name="startAddress">出发地</param>
        /// <param name="endAddress">目的地</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">订单改变类型</param>
        /// <param name="changeState">订单改变状态</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderStats(string orderNo,string supplierId, int? startAddress,int? endAddress,DateTime? startDate,DateTime? endDate,OrderState? orderState,OrderChangeType? changeType,OrderChangeState? changeState)
        {   
            OrderSearchInfo searchInfo=new OrderSearchInfo();
            if (!string.IsNullOrEmpty(orderNo))
            {
                searchInfo.OrderNo = orderNo;
            }
            searchInfo.SupplierCId = supplierId;
            searchInfo.HomeCityId = startAddress;
            searchInfo.DestCityId = endAddress;
            searchInfo.OrderStartTime = startDate;
            searchInfo.OrderFinishTime = endDate;
            searchInfo.OrderChangeState = changeState;
            searchInfo.OrderChangeType = changeType;
            searchInfo.OrderState = orderState;
            IList<OrderInfo> orderList=idalOrder.GetOrderStats(supplierId, searchInfo);
            SetSeattleName(orderList);//设置始发目的地名称
            return orderList;
        }

        

        /// <summary>
        /// 采购商分析信息
        /// <summary>
        /// <param name="searchInfo">查询信息实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> GetBuyerAnalysis(BuyerAnalysisSearchInfo searchInfo)
        {
            return idalOrder.GetBuyerAnalysis(searchInfo);
        }

        /// <summary>
        /// 获取订单详细信息(根据订单ID)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoById(string orderId)
        {
            OrderInfo orderInfo= idalOrder.GetInfo(orderId);
            SetSeattleName(orderInfo);
             return orderInfo;
        }

        /// <summary>
        /// 获取订单详细信息(根据订单号)
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public OrderInfo GetOrderInfoByNo(string orderNo)
        {
            OrderInfo orderInfo= idalOrder.GetInfoByOrderNo(orderNo);
            SetSeattleName(orderInfo);
            return orderInfo;
        }

        /// <summary>
        /// 获取订单操作信息集合
        /// <summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TicketStructure.OrderHandleInfo> GetHandels(string orderId)
        {
            return idalOrder.GetHandels(orderId);
        }

        /// <summary>
        /// 获取订单游客状态变更信息集合
        /// <summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.TicketStructure.TicketOrderLog> GetChanges(string orderId)
        {

            List<EyouSoft.Model.TicketStructure.TicketOrderLog> infoList2 = new List<TicketOrderLog>();
             IList<EyouSoft.Model.TicketStructure.OrderChangeInfo> infoList= idalOrder.GetChanges(orderId);
             
             if (infoList != null)
             {
                 foreach (EyouSoft.Model.TicketStructure.OrderChangeInfo info in infoList)
                 {
                     if (info.ChangeState == EyouSoft.Model.TicketStructure.OrderChangeState.申请)
                     {
                         infoList2.Add(new TicketOrderLog { State = "申请" + info.ChangeType.ToString(), Time = info.ChangeTime, UserName = info.ChangeUFullName, Remark = (info.ChangeType == OrderChangeType.退票 || info.ChangeType == OrderChangeType .作废)?info.RefundTicketType.ToString():info.ChangeRemark});
                     }
                     else
                     {
                         infoList2.Add(new TicketOrderLog { State = "申请" + info.ChangeType.ToString(), Time = info.ChangeTime, UserName = info.ChangeUFullName, Remark = (info.ChangeType == OrderChangeType.退票 || info.ChangeType == OrderChangeType.作废) ? info.RefundTicketType.ToString(): info.ChangeRemark });
                         infoList2.Add(new TicketOrderLog { State = info.ChangeType.ToString()+info.ChangeState.ToString(), Time = info.CheckTime, UserName = info.CheckUFullName,Remark=info.CheckRemark });
                     }
                 }
             }
             return infoList2;
        }
        /// <summary>
        /// 获取订单状态变更记录
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        public IList<TicketOrderLog> GetTicketOrderState(string orderId)
        {  
             
            return GetHandels(orderId).Select(i => new TicketOrderLog { State = i.CurrState.ToString(), Time = i.HandelTime, UserName = i.HandelUFullName, Remark=i.HandleRemark}).
                Union(GetChanges(orderId)).ToList<TicketOrderLog>();
        }

        /// <summary>
        /// 根据订单获取游客信息(机票告知打印单)
        /// <summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public IList<OrderTravellerInfo> GetTravells(string orderNo)
        {
            IList<OrderTravellerInfo> travelList = null;
            OrderInfo orderInfo = idalOrder.GetInfoByOrderNo(orderNo);
            if (orderInfo != null)
            {
                travelList = orderInfo.Travellers;
            }
            return travelList;
        }

        /// <summary>
        /// 获取日报表
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IList<OrderInfo> GetDayReports(string buyerCId,DateTime? startDate,DateTime? endDate)
        {   
            return idalOrder.GetBuyerOrderReports(buyerCId, startDate, endDate);
        }

        /// <summary>
        /// 获取月报表
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public IList<OrderInfo> GetMonReports(string buyerCId, int year, int month)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate= startDate.AddDays(DateTime.DaysInMonth(year, month)-1);
            return idalOrder.GetBuyerOrderReports(buyerCId, startDate, endDate);
        }

        /// <summary>
        /// 采购商下订单
        /// <summary>
        /// <param name="orderInfo">订单信息实体</param>
        /// <returns></returns>
        public bool CreateOrder(OrderInfo orderInfo)
        {
          return idalOrder.Create(orderInfo) == 1 ? true : false;
        
        }

        /// <summary>
        /// 采购商取消订单
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyId">操作人公司Id</param>
        /// <returns></returns>
        public bool CancleOrder(string orderId, string userId,string companyId)
        {
            return idalOrder.SetState(orderId, OrderState.无效订单, companyId, userId, null, new Nullable<DateTime>(), new Nullable<TicketAccountType>(), null,null);
        }

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="handelCId">操作人公司编号</param>
        /// <param name="handelUId">操作人用户编号</param>
        /// <param name="handelRemark">备注信息</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payType">支付接口类型</param>
        /// <param name="payAccount">支付账户</param>
        /// <param name="payTradeNo">支付接口返回的交易号</param>
        /// <returns></returns>
        public bool SetState(string orderId, EyouSoft.Model.TicketStructure.OrderState orderState, string handelCId, string handelUId
            , string handelRemark, DateTime? payTime, EyouSoft.Model.TicketStructure.TicketAccountType? payType, string payAccount, string payTradeNo)
        {
            return idalOrder.SetState(orderId, orderState, handelCId, handelUId, handelRemark, payTime, payType, payAccount, payTradeNo);
        }

        

        /// <summary>
        /// 供应商订单拒绝审核
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="remark">拒绝理由</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyid">操作人公司Id</param>
        /// <returns></returns>
        public bool SupplierNotCheckOrder(string orderId, string remark, string userId, string companyid)
        {
          return idalOrder.SetState(orderId, OrderState.拒绝审核, companyid, userId, remark, new Nullable<DateTime>(), new Nullable<TicketAccountType>(), null,null);
        }

        /// <summary>
        /// 更新订单信息(价格)
        /// </summary>
        /// <param name="orderInfo">订单信息业务实体</param>
        /// <returns></returns>
        public bool UpdatePrice(EyouSoft.Model.TicketStructure.OrderInfo orderInfo)
        {
            return idalOrder.UpdatePrice(orderInfo);
        }

        /// <summary>
        /// 更新订单信息(PNR)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="PNR">PNR</param>
        /// <returns></returns>
        public bool UpdatePNR(string orderId, string PNR)
        {
            return idalOrder.UpdatePNR(orderId, PNR);
        }

        /// <summary>
        /// 更新订单信息(航班号)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="lFlightCode">去程航班号</param>
        /// <param name="rFlightCode">回程航班号</param>
        /// <returns></returns>
        public bool UpdateFlightCode(string orderId, string lFlightCode, string rFlightCode)
        {
            return idalOrder.UpdateFlightCode(orderId, lFlightCode, rFlightCode);
        }
        /// <summary>
        /// 供应商订单审核通过
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyid">操作人公司Id</param>
        /// <returns></returns>
        public bool SupplierCheckOrder(string orderId,string userId, string companyid)
        {
          return idalOrder.SetState(orderId, OrderState.审核通过, companyid, userId, null, null,null,null,null);
        }

        /// <summary>
        /// 判断是否已支付过
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        /// <param name="tradeNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool GetIsPay(string itemId,ItemType? itemType,string tradeNo,string batchNo)
        {
            return idalTicketPayList.GetTicketPay(itemId, itemType, tradeNo, batchNo).Where(i => i.PayState == PayState.交易失败 || i.PayState == PayState.交易完成).Count() == 1;
        }

        /// <summary>
        /// 获取机票系统-支付明细记录
        /// </summary>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="TradeNo">提交给支付接口的交易号</param>
        /// <param name="BatchNo">提交给支付接口的批次号</param>
        /// <returns></returns>
        public IList<TicketPay> GetPayList(string itemId, ItemType? itemType, string tradeNo, string batchNo)
        {
            return idalTicketPayList.GetTicketPay(itemId, itemType, tradeNo, batchNo);
        }


        /// <summary>
        /// 获取订单账户信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.OrderAccountInfo GetOrderAccountInfo(string orderId)
        {
            return idalOrder.GetOrderAccountInfo(orderId);
        }

       

        #region 支付后回调函数
        /// <summary>
        /// 支付后回调函数
        /// </summary>
        /// <param name="payNumber">支付接口返回的交易流水号</param>
        /// <param name="payPrice">支付金额</param>
        /// <param name="payState">支付状态</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="payAccount">支付账户</param>
        /// <param name="remark">备注</param>
        /// <param name="payTradeNo">提交到支付接口的交易号</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="batchNo">批次号</param>
        /// <returns></returns>
        public bool PayAfterCallBack(string payNumber,decimal?payPrice,PayState payState, TicketAccountType payType, string payAccount,string remark, string payTradeNo, DateTime? payTime,string batchNo)
        {
            bool result = false;
            string itemId = string.Empty;//项目编号
            string currCompanyId = string.Empty;//当前公司ID
            string currUserId = string.Empty;//当前用户ID
            ItemType? itemType;//流水明细记录项类型
            //回调后修改支付明细
            idalTicketPayList.PayCallback(payNumber, payState, remark, payTradeNo, batchNo, payType, out itemId, out itemType, out currCompanyId, out currUserId);
            
            if (payState == EyouSoft.Model.TicketStructure.PayState.交易完成)
            {    
                switch (itemType.Value)
                {
                    case ItemType.采购商付款到平台_订单://采购商支付后更新订单状态
                        result = PayAfter(itemId, currCompanyId, currUserId, payTime, payType, payAccount, remark,payNumber);
                        break;
                    case ItemType.平台到采购商_变更://(退票,作废)平台支付采购商后修改变更状态
                        result = BackOrDisableTicketAfter(itemId, payPrice, currUserId, payTime.Value, remark);
                        break;
                    case ItemType.平台到采购商_订单://供应商拒绝出票
                        result = NoOutputTicketAfter(itemId, currCompanyId, currUserId, payTime, payType, payAccount, remark,payNumber);
                        break;
                    case ItemType.平台到供应商_订单://供应商出票完成
                        result = OutPutTicketAfter(itemId, payPrice.Value, currCompanyId, currUserId, payTime, payType, payAccount, remark, payNumber);
                        break;
                    case ItemType.供应商到平台_变更://(退票,作废)
                        result = true;
                        break;
                }
            }
            return result;
        }
        #endregion

      

        #region 采购商调用支付接口前
        /// <summary>
        /// 采购商调用支付接口前
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="sellAccount">供应商公司账号</param>
        /// <param name="discount">供应商费率</param>
        /// <param name="currUserId">操作人ID</param>
        /// <param name="currCompanyId">操作人公司ID</param>
        /// <param name="payPrice">支付总金额</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="sellCompanyId">供应商公司编号</param>
        /// <param name="remark">备注</param>
        /// <param name="batchNo">返回的批次号</param>
        /// <returns></returns>
        public bool PayBefore(string orderId, string orderNo, string sellAccount, decimal discount, string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType, string sellCompanyId,string remark, out string batchNo)
        {
            //写支付账号信息
            OrderAccountInfo info=new OrderAccountInfo();
            info.Discount = discount;
            info.OrderId=orderId;
            info.OrderNo = orderNo;
            info.SellAccount=sellAccount;
            info.PayCompanyId = currCompanyId;
            info.PayPrice=payPrice;
            info.PayType=accountType;
            info.PayUserId = currUserId;
            info.SellCompanyId=sellCompanyId;
            info.PayAccount = "";
            idalOrder.InsertOrderAccount(info);
            //写支付明细
            TicketPay pay=new TicketPay();
            pay.TradeNo = orderNo;
            pay.CurrCompanyId = currCompanyId;
            pay.CurrUserId = currUserId;
            pay.IssueTime=DateTime.Now;
            pay.PayPrice=payPrice;
            pay.PayState = PayState.未提交到支付接口;
            pay.PayType=accountType;
            pay.ItemId = orderId;
            pay.Remark = remark;
            pay.ItemType = EyouSoft.Model.TicketStructure.ItemType.采购商付款到平台_订单;
            bool result=idalTicketPayList.AddTicketPay(pay);
            batchNo = pay.BatchNo;
            return result;
        }
        #endregion

        #region 采购商调用支付接口后
        /// <summary>
        /// 采购商调用支付接口后
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="currCompanyId">支付方公司ID</param>
        /// <param name="currUserId">支付人ID</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payType">支付接口类型</param>
        /// <param name="payAccount">支付账户</param>
        /// <param name="remark">备注</param>
        /// <param name="payTradeNo">支付接口返回的交易号</param>
        /// <returns></returns>
        private bool PayAfter(string orderId,string currCompanyId,string currUserId, DateTime? payTime,TicketAccountType? payType,string payAccount,string remark,string payTradeNo)
        {
            return idalOrder.SetState(orderId, OrderState.支付成功, currCompanyId, currUserId, remark, payTime, payType, payAccount,payTradeNo);
        }
        #endregion

        #region 供应商出票完成调用支付接口前
        /// <summary>
        /// 供应商出票完成调用支付接口前
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="orderId">订单ID</param>
        /// <param name="currUserId">操作人ID</param>
        /// <param name="currCompanyId">操作人公司ID</param>
        /// <param name="payPrice">支付金额</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="travellers">出票旅客</param>
        /// <param name="remark">备注</param>
        /// <param name="batchNo">返回的批次号</param>
       /// <returns></returns>
        public bool OutPutTicketBefore(string orderNo, string orderId,string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType, IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers,string remark,out string batchNo)
        {
            TicketPay pay = new TicketPay();
            pay.ItemId = orderId;
            pay.CurrCompanyId = currCompanyId;
            pay.CurrUserId = currUserId;
            pay.IssueTime = DateTime.Now;
            pay.PayPrice = payPrice;
            pay.ItemType = EyouSoft.Model.TicketStructure.ItemType.平台到供应商_订单;
            pay.PayState = PayState.支付接口正在处理;
            pay.PayType = accountType;
            pay.TradeNo = orderNo;
            pay.Remark = remark;
            bool result=idalTicketPayList.AddTicketPay(pay);
            batchNo = pay.BatchNo;//产生批次号
            idalOrder.UpdateTraveller(travellers);//更新票号
            return result;
        }
        #endregion

        #region 供应商出票完成调用支付接口后
        /// <summary>
        /// 供应商出票完成调用支付接口后
       /// </summary>
       /// <param name="orderId">订单ID</param>
       /// <param name="account">供应商收款金额</param>
       /// <param name="currCompanyId">操作人公司ID</param>
       /// <param name="currUserId">操作人ID</param>
       /// <param name="payTime">支付时间</param>
       /// <param name="payType">支付接口类型</param>
       /// <param name="payAccount">支付账户</param>
       /// <param name="remark">备注</param>
        /// <param name="payTradeNo">支付接口返回的交易号</param>
       /// <returns></returns>
        private bool OutPutTicketAfter(string orderId, decimal account, string currCompanyId, string currUserId, DateTime? payTime, TicketAccountType? payType, string payAccount, string remark, string payTradeNo)
        {
            idalOrder.SetState(orderId, OrderState.出票完成, currCompanyId, currUserId, remark, payTime, payType, payAccount,payTradeNo);
            return idalOrder.SetBalanceAmount(orderId, account);
        }
        #endregion

        #region 供应商拒绝出票调用支付接口前
        /// <summary>
        /// 供应商拒绝出票调用支付接口前
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="currUserId">操作人ID</param>
        /// <param name="currCompanyId">操作人公司ID</param>
        /// <param name="payPrice">支付金额</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="remark">备注</param>
        /// <param name="batchNo">返回批次号</param>
        /// <returns></returns>
        public bool NoOutputTicketBefore(string orderId, string orderNo, string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType,string remark,out string batchNo)
        {
            TicketPay pay = new TicketPay();
            pay.ItemId = orderId;
            pay.CurrCompanyId = currCompanyId;
            pay.CurrUserId = currUserId;
            pay.IssueTime = DateTime.Now;
            pay.PayPrice = payPrice;
            pay.PayState = PayState.支付接口正在处理;
            pay.PayType = accountType;
            pay.TradeNo = orderNo;
            pay.ItemType = ItemType.平台到采购商_订单;
            pay.Remark = remark;
            bool result= idalTicketPayList.AddTicketPay(pay);
            batchNo = pay.BatchNo;
            return result;
        }
        #endregion

        #region 供应商拒绝出票调用支付接口后
        /// <summary>
        /// 供应商拒绝出票调用支付接口后
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="currCompanyId">操作公司ID</param>
        /// <param name="currUserId">操作人ID</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payType">支付接口类型</param>
        /// <param name="payAccount">支付账户</param>
        /// <param name="remark">拒绝理由</param>
        /// <param name="payTradeNo">支付接口返回的交易号</param>
        /// <returns></returns>
        private bool NoOutputTicketAfter(string orderId, string currCompanyId, string currUserId, DateTime? payTime, TicketAccountType? payType, string payAccount, string remark, string payTradeNo)
        {   
            //设置订单状态
            return idalOrder.SetState(orderId, OrderState.拒绝出票, currCompanyId, currUserId, remark, payTime, payType, payAccount, payTradeNo);
        }
        #endregion

        #region 供应商退票、作废完成提交接口前（供应商到平台）
        /// <summary>
        /// 供应商退票、作废完成提交接口前（供应商到平台）
        /// </summary>
        /// <param name="changeId">变更ID</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="handlingfree">手续费</param>
        /// <param name="changeAccount">供应商变更总金额</param>
        /// <param name="totalAcount">变更总金额</param>
        /// <param name="currUserId">操作人ID</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="currCompanyId">操作公司ID</param>
        /// <param name="batchNo">返回给支付接口的批次号</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool BackOrDisableTicketBeforeGP(string changeId, string orderNo, decimal handlingfree, decimal changeAccount, decimal totalAcount, string currUserId, TicketAccountType accountType, string currCompanyId,out string batchNo,string remark)
        {   
            //设置订单变更金额
            idalOrder.SetChangeAmount(changeId, handlingfree, changeAccount,totalAcount);
            //写支付明细
            TicketPay pay = new TicketPay();
            pay.ItemId = changeId;
            pay.CurrCompanyId = currCompanyId;
            pay.CurrUserId = currUserId;
            pay.IssueTime = DateTime.Now;
            pay.PayPrice = totalAcount;
            pay.ItemType = ItemType.供应商到平台_变更;
            pay.PayState = PayState.支付接口正在处理;
            pay.PayType = accountType;
            pay.TradeNo = orderNo;
            pay.Remark = remark;
            bool result = idalTicketPayList.AddTicketPay(pay);
            batchNo = pay.BatchNo;
            return result;
        }
        #endregion

        #region 供应商退票、作废完成提交接口前（平台到采购商）
        /// <summary>
        /// 供应商退票作、废完成提交接口前（平台到采购商）
        /// </summary>
        /// <param name="changeId">变更ID</param>
        /// <param name="orderId">订单号</param>
        /// <param name="payPrice">支付金额</param>
        /// <param name="currUserId">当前操作用户ID</param>
        /// <param name="accountType">支付接口类型</param>
        /// <param name="currCompanyId">当前操作公司ID</param>
        /// <param name="remark">备注</param>
        /// <param name="batchNo">返回给支付接口的批次号</param>
        /// <returns></returns>
        public bool BackOrDisableTicketBeforePC(string changeId, string orderNo, decimal payPrice, string currUserId,TicketAccountType accountType, string currCompanyId,string remark,out string batchNo)
        {
            //写支付明细
            TicketPay pay = new TicketPay();
            pay.ItemId = changeId;
            pay.CurrCompanyId = currCompanyId;
            pay.CurrUserId = currUserId;
            pay.IssueTime = DateTime.Now;
            pay.PayPrice = payPrice;
            pay.PayState = PayState.支付接口正在处理;
            pay.PayType = accountType;
            pay.ItemType = ItemType.平台到采购商_变更;
            pay.TradeNo = orderNo;
            pay.Remark = remark;
            bool result=idalTicketPayList.AddTicketPay(pay);
            batchNo = pay.BatchNo;
            return result;
        }

        #endregion

        #region 供应商退票、作废完成后更新变更状态(平台到采购商)
        /// <summary>
        /// 供应商退票、作废完成后更新变更状态(平台到采购商)
        /// </summary>
        /// <param name="changeId">变更ID</param>
        /// <param name="changeTotalAccount">变更金额</param>
        /// <param name="checkUserId">审核人ID</param>
        /// <param name="checkTime">审核时间</param>
        /// <param name="checkRemark">审核备注</param>
        /// <returns></returns>
        private bool BackOrDisableTicketAfter(string changeId, decimal? changeTotalAccount, string checkUserId, DateTime checkTime,string checkRemark)
        {   
            //更新变更状态
            return idalOrder.CheckChange(changeId, OrderChangeState.接受, checkUserId, checkTime, checkRemark);
        }
        #endregion

        #region 供应商审核、改期、改签、拒绝作废
        /// <summary>
        /// 供应商审核、改期、改签、拒绝作废
        /// <summary>
        /// <param name="changeId">变更ID</param>
        /// <param name="userId">操作用户ID</param>
        /// <param name="remark">拒绝理由</param>
        /// <param name="changeState">变更类型</param>
        /// <returns></returns>
        public bool CheckOrderChange(string changeId, string userId, string remark,OrderChangeState changeState)
        {
            return idalOrder.CheckChange(changeId, changeState, userId, DateTime.Now, remark);
        }
        #endregion


        #region 采购商改变订单(退票、作废、改期、改签)
        /// <summary>
        /// 采购商改变订单(退票、作废、改期、改签)
        /// </summary>
        /// <param name="changeinfo">变更信息</param>
        /// <returns>0:成功,1:不能申请旅客状态变更,2:失败</returns>
        public int SetOrderChange(OrderChangeInfo changeinfo)
        {
            int result = 2;
            if (!idalOrder.IsAllowApply(changeinfo.OrderId))
            {
                result = 1;
            }
            else
            {
                changeinfo.ChangeId = Guid.NewGuid().ToString();
                result = idalOrder.ApplyChange(changeinfo) ? 0 : 2;
            }
            
            return result;
        }
        #endregion

        #region 获取最新的订单变更信息
        /// <summary>
        /// 获取最新的订单变更信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.OrderChangeInfo GetLatestChange(string orderId)
        {
            return idalOrder.GetLatestChange(orderId);
        }
        #endregion

        /*/// <summary>
        /// 设置订单旅客变更审核备注
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        public bool SetChangeCheckRemark(string changeId,string remark)
        {
            return idalOrder.SetChangeCheckRemark(changeId, remark);
        }*/


        /// <summary>
        /// 采购商获取订单报表(合计)
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        public EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo GetBuyerOrderReportsTotal(string buyerCId, DateTime? startTime, DateTime? finishTime)
        {
            return idalOrder.GetBuyerOrderReportsTotal(buyerCId, startTime, finishTime);
        }

        ///// <summary>
        ///// 供应商作废完成前1
        ///// </summary>
        ///// <param name="changeId">订单ID</param>
        ///// <param name="payAccount">支付账号</param>
        ///// <param name="payUserId">支付人ID</param>
        ///// <param name="payCompanyId">支付公司ID</param>
        ///// <param name="payPrice">支付金额</param>
        ///// <param name="accountType">支付类型</param>
        ///// <param name="sellCompanyId">收款公司ID</param>
        ///// <param name="id">支付金额流水号</param>
        ///// <returns></returns>
        //public bool DisableOrderBefore(string changeId, string sellAccount, string payUserId, string payCompanyId, decimal payPrice, TicketAccountType accountType, string sellCompanyId, out string id)
        //{
        //    //MoneyDetailList detail = new MoneyDetailList();
        //    //detail.CurrCompanyId = payCompanyId;
        //    //detail.CurrUserId = payUserId;
        //    //detail.ID = Guid.NewGuid().ToString();
        //    //detail.IssueTime = DateTime.Now;
        //    //detail.ItemId = changeId;
        //    //detail.ItemType = ItemType.订单变更;
        //    //detail.SellAccount = sellAccount;
        //    //detail.PayCompanyId = payCompanyId;
        //    //detail.PayFrom = PayFrom.不经平台转账;
        //    //detail.PayNumber = detail.ID;
        //    //detail.PayPrice = payPrice;
        //    //detail.PayTime = DateTime.Now;
        //    //detail.SellCompanyId = sellCompanyId;
        //    //detail.PayType = accountType;
        //    //id = detail.ID;
        //    //return idalTicketPayList.Pay(detail);
        //    id = "";
        //    return true;
        //}



        ///// 供应商作废完成后
        ///// <param name="payNumber">流水号</param>
        ///// <param name="payerAccount">支付账号</param>
        ///// <param name="payTradeNo">交易流水号</param>
        ///// <param name="payTime">支付时间</param>
        //public bool DisableOrderAfter(string changeId, decimal?changeTotalAccount, string checkUserId, DateTime checkTime)
        //{
        //    return idalOrder.CheckChange(changeId, OrderChangeState.接受, changeTotalAccount, checkUserId, checkTime.ToString(), null);
        //}

    }

}
