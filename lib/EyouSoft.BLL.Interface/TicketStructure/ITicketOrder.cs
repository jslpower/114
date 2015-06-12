using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TicketStructure;
namespace EyouSoft.IBLL.TicketStructure
{
    public interface ITicketOrder
    {

        /// <summary>
        /// 修改订单信息的联系方式
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="buyCName">公司名</param>
        /// <param name="buyerContactName">联系人</param>
        /// <param name="buyerContactMoible">手机</param>
        /// <param name="buyerContactAddress">地址</param>
        /// <returns></returns>
        bool UpdateBuyerContact(string orderId, string buyCName, string buyerContactName, string buyerContactMoible, string buyerContactAddress);

        /// <summary>
        /// 采购商修改订单服务备注
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="remark">服务备注</param>
        /// <returns></returns>
        bool UpdateServiceRemark(string orderId, string remark);

        /// <summary>
        /// 修改订单采购商备注(订单中的特殊备注)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="buyerRemark"></param>
        /// <returns></returns>
        bool UpdateBuyerRemark(string orderId, string buyerRemark);

        /// <summary>
        /// 更新订单信息(价格)
        /// </summary>
        /// <param name="orderInfo">订单信息业务实体</param>
        /// <returns></returns>
        bool UpdatePrice(EyouSoft.Model.TicketStructure.OrderInfo orderInfo);

        /// <summary>
        /// 更新订单信息(PNR)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="PNR">PNR</param>
        /// <returns></returns>
        bool UpdatePNR(string orderId, string PNR);

        /// <summary>
        /// 更新订单信息(航班号)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="lFlightCode">去程航班号</param>
        /// <param name="rFlightCode">回程航班号</param>
        /// <returns></returns>
        bool UpdateFlightCode(string orderId, string lFlightCode, string rFlightCode);

         /// <summary>
        /// 供应商订单精确查询(不分页)
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="ticketNumber">机票编号</param>
        /// <param name="travellName">游客名</param>
        /// <param name="pnrNo">PNR码</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> precisionSearch(string supplierId,string orderNo, string ticketNumber, string travellName, string pnrNo);
        
        /// <summary>
        /// 供应商订单精确查询(分页)
        /// </summary>
        /// <param name="supplierId">供应商Id</param>
        /// <param name="orderNo">订单号</param>
        /// <param name="ticketNumber">机票编号</param>
        /// <param name="travellName">游客名</param>
        /// <param name="pnrNo">PNR码</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
       IList<EyouSoft.Model.TicketStructure.OrderInfo> precisionSearch(string supplierId,string orderNo, string ticketNumber, string travellName, string pnrNo, int pageSize, int pageIndex, ref int recordCount);
        

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
       IList<EyouSoft.Model.TicketStructure.OrderInfo> ShouSuoSearch(string supplierId, int? flightId, RateType? rateType, int? startAddress, int? endAddress, DateTime? startDate, DateTime? endDate, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState);
        
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
        IList<EyouSoft.Model.TicketStructure.OrderInfo> ShouSuoSearch(string supplierId, int? flightId, RateType? rateType, int? startAddress, int? endAddress, DateTime? startDate, DateTime? endDate, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState, int pageSize, int pageIndex, ref int recordCount);


        /// <summary>
        /// 采购商订单查询(按选择类型)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="pnrNo">pnr号</param>
        /// <param name="travellerName">旅客姓名</param>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
         IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, string pnrNo, string travellerName, string orderNo);


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
       IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, string pnrNo, string travellerName, string orderNo, int pageSize, int pageIndex, ref int recordCount);


         /// <summary>
        /// 采购商订单查询(按状态查询)
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="date">当前日期</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">改变类型</param>
        /// <param name="changeState">改变状态</param>
        /// <returns></returns>
       IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, DateTime? date, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState);


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
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(string buyerCId, DateTime? date, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState, int pageSize, int pageIndex, ref int recordCount);

       /// <summary>
       /// 采购商供应商订单查询
       /// </summary>
       /// <param name="searchInfo">查询实体</param>
       /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(OrderSearchInfo searchInfo);


         /// <summary>
        /// 采购商供应商订单查询
        /// </summary>
        /// <param name="searchInfo">订单查询实体</param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderList(OrderSearchInfo searchInfo, int pageSize, int pageIndex, ref int recordCount);

       /// <summary>
        /// 供应商订单处理统计信息
       /// </summary>
       /// <param name="supplierId">供应商ID</param>
       /// <returns></returns>
        IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> GetSupplierHandelStats(string supplierId);

        /// <summary>
        /// 获取供应商具体处理统计信息
        /// </summary>
        /// <param name="supplierId">供应商公司Id</param>
        /// <param name="type">业务类型</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">订单更改类型</param>
        /// <param name="changeState">订单状态</param>
        /// <returns></returns>
        IList<OrderInfo> GetSupplierHandelStatsDetail(string supplierId, RateType? type, OrderState? orderState, OrderChangeType? changeType,OrderChangeState? changeState);

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
        IList<OrderInfo> GetSupplierHandelStatsDetail(string supplierId, RateType? type, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState, int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 供应商订单统计
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <param name="supplierId">供应商公司Id</param>
        /// <param name="startAddress">始发地</param>
        /// <param name="endAddress">目的地</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="changeType">订单更改类型</param>
        /// <param name="changeState">订单更改状态</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderStats(string orderNo,string supplierId, int? startAddress, int? endAddress, DateTime? startDate, DateTime? endDate, OrderState? orderState, OrderChangeType? changeType, OrderChangeState? changeState);

       /// <summary>
        /// 采购商分析信息
       /// </summary>
       /// <param name="searchInfo">订单查询实体</param>
       /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> GetBuyerAnalysis(BuyerAnalysisSearchInfo searchInfo);

        /// <summary>
        /// 获取订单详细信息(根据订单ID)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        OrderInfo GetOrderInfoById(string orderId);

         /// <summary>
        /// 获取订单详细信息(根据订单号)
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        OrderInfo GetOrderInfoByNo(string orderNo);


         /// <summary>
        /// 获取机票系统-支付明细记录
        /// </summary>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="TradeNo">提交给支付接口的交易号</param>
        /// <param name="BatchNo">提交给支付接口的批次号</param>
        /// <returns></returns>
        IList<TicketPay> GetPayList(string itemId, ItemType? itemType, string tradeNo, string batchNo);


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
        bool SetState(string orderId, EyouSoft.Model.TicketStructure.OrderState orderState, string handelCId, string handelUId
            , string handelRemark, DateTime? payTime, EyouSoft.Model.TicketStructure.TicketAccountType? payType, string payAccount,string payTradeNo);

        /// <summary>
        /// 获取订单状态记录
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        IList<TicketOrderLog> GetTicketOrderState(string orderId);

        /// <summary>
        /// 根据订单获取游客信息(机票告知打印单)
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        IList<OrderTravellerInfo> GetTravells(string orderNo);

        

       /// <summary>
        /// 获取日报表
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        IList<OrderInfo> GetDayReports(string buyerCId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 获取月报表
        /// </summary>
        /// <param name="buyerCId">采购商ID</param>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        IList<OrderInfo> GetMonReports(string buyerCId, int year, int month);

        /// <summary>
        /// 采购商获取订单报表(合计)
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo GetBuyerOrderReportsTotal(string buyerCId, DateTime? startTime, DateTime? finishTime);

        /// <summary>
        /// 采购商下订单
        /// </summary>
        /// <param name="model">订单信息实体</param>
        /// <returns></returns>
        bool CreateOrder(OrderInfo model);

        /// <summary>
        /// 采购商取消订单
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyId">操作人公司Id</param>
        /// <returns></returns>
        bool CancleOrder(string orderId, string userId, string companyId);


        /// <summary>
        /// 获取订单账户信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.OrderAccountInfo GetOrderAccountInfo(string orderId);


        /// <summary>
        /// 供应商订单拒绝审核
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="remark">拒绝理由</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyid">操作人公司Id</param>
        /// <returns></returns>
        bool SupplierNotCheckOrder(string orderId, string remark, string userId, string companyid);

        /// <summary>
        /// 供应商订单审核通过
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="userId">操作人Id</param>
        /// <param name="companyid">操作人公司Id</param>
        /// <returns></returns>
        bool SupplierCheckOrder(string orderId,string userId, string companyid);
        

        
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
        bool PayAfterCallBack(string payNumber, decimal? payPrice, PayState payState, TicketAccountType payType, string payAccount, string remark, string payTradeNo, DateTime? payTime, string batchNo);


      

       
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
        bool PayBefore(string orderId, string orderNo, string sellAccount, decimal discount, string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType, string sellCompanyId, string remark, out string batchNo);



        
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
        bool OutPutTicketBefore(string orderNo, string orderId, string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType, IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers, string remark, out string batchNo);

         /// <summary>
        /// 判断是否已支付过
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        /// <param name="tradeNo"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        bool GetIsPay(string itemId, ItemType? itemType, string tradeNo, string batchNo);
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
       bool NoOutputTicketBefore(string orderId, string orderNo, string currUserId, string currCompanyId, decimal payPrice, TicketAccountType accountType, string remark, out string batchNo);

        
    
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
       bool BackOrDisableTicketBeforeGP(string changeId, string orderNo, decimal handlingfree, decimal changeAccount, decimal totalAcount, string currUserId, TicketAccountType accountType, string currCompanyId, out string batchNo, string remark);

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
      bool BackOrDisableTicketBeforePC(string changeId, string orderNo, decimal payPrice, string currUserId, TicketAccountType accountType, string currCompanyId, string remark, out string batchNo);






        /// 供应商审核改期或改签或拒绝作废
        /// <param name="changeId">订单变更ID</param>
        /// <param name="userId">操作用户ID</param>
        /// <param name="remark">拒绝理由</param>
        /// <param name="changeState">变更状态</param>
        bool CheckOrderChange(string changeId, string userId, string remark, OrderChangeState changeState);

        /// <summary>
        /// 采购商改变订单(退票，作废，改期，改签)
        /// <summary>
        /// <param name="changeinfo">订单变更信息</param>
        /// <returns>0:成功,1:不允许旅客状态变更申请,2:失败</returns>
        int  SetOrderChange(OrderChangeInfo changeinfo);


        /// <summary>
        /// 获取最新的订单变更信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        OrderChangeInfo GetLatestChange(string orderId);

        /* /// <summary>
        /// 设置订单旅客变更审核备注
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        bool SetChangeCheckRemark(string changeId, string remark);*/


    }
}
