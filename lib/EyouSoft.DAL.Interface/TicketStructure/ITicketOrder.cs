//Author:汪奇志 2010-10-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 机票系统订单信息数据访问接口
    /// </summary>
    public interface ITicketOrder
    {
        /// <summary>
        /// 创建订单信息
        /// </summary>
        /// <param name="orderInfo">订单信息业务实体</param>
        /// <returns>0:失败 1:成功</returns>
        int Create(EyouSoft.Model.TicketStructure.OrderInfo orderInfo);

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
        /// 更新订单旅客信息(票号)
        /// </summary>
        /// <param name="travellers">旅客信息集合</param>
        /// <returns></returns>
        bool UpdateTraveller(IList<EyouSoft.Model.TicketStructure.OrderTravellerInfo> travellers);

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
            , string handelRemark, DateTime? payTime, EyouSoft.Model.TicketStructure.TicketAccountType? payType, string payAccount, string payTradeNo);

        /// <summary>
        /// 按订单编号获取订单信息业务实体
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.OrderInfo GetInfo(string orderId);

        /// <summary>
        /// 按订单号获取订单信息业务实体
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.OrderInfo GetInfoByOrderNo(string orderNo);

        /// <summary>
        /// 根据指定条件获取订单信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrders(EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo, int pageSize, int pageIndex, ref int recordCount);

        /// <summary>
        /// 根据指定条件获取订单信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrders(EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo);

        /// <summary>
        /// 是否允许旅客状态变更申请(是否有未审核的旅客状态变更申请)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        bool IsAllowApply(string orderId);

        /// <summary>
        /// 订单旅客状态变更申请
        /// </summary>
        /// <param name="changeInfo">旅客状态变更信息业务实体</param>
        /// <returns></returns>
        bool ApplyChange(EyouSoft.Model.TicketStructure.OrderChangeInfo changeInfo);

        /// <summary>
        /// 订单旅客状态变更审核
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="orderChangeState">审核状态</param>
        /// <param name="checkUId">审核人用户编号</param>
        /// <param name="checkTime">审核时间</param>
        /// <param name="checkRemark">备注</param>
        /// <returns></returns>
        bool CheckChange(string changeId, EyouSoft.Model.TicketStructure.OrderChangeState orderChangeState
            , string checkUId, DateTime checkTime, string checkRemark);

        /// <summary>
        /// 获取订单操作信息集合
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderHandleInfo> GetHandels(string orderId);

        /// <summary>
        /// 获取订单旅客状态变更信息集合
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderChangeInfo> GetChanges(string orderId);

        /// <summary>
        /// 供应商获取采购分析信息集合
        /// </summary>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.BuyerAnalysisInfo> GetBuyerAnalysis(EyouSoft.Model.TicketStructure.BuyerAnalysisSearchInfo searchInfo);

        /// <summary>
        /// 获取供应商订单统计信息集合
        /// </summary>
        /// <param name="supplierCId">供应商编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetOrderStats(string supplierCId, EyouSoft.Model.TicketStructure.OrderSearchInfo searchInfo);

        /// <summary>
        /// 更新订单采购商联系方式
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="buyerCName">公司名称</param>
        /// <param name="buyerContactName">联系人</param>
        /// <param name="buyerContactMobile">联系手机</param>
        /// <param name="buyerContactAddress">联系地址</param>
        /// <returns></returns>
        bool UpdateBuyerContact(string orderId, string buyerCName, string buyerContactName, string buyerContactMobile, string buyerContactAddress);

        /// <summary>
        /// 获取供应商订单处理统计信息
        /// </summary>
        /// <param name="supplierCId">供应商编号</param>
        /// <returns></returns>
        IDictionary<EyouSoft.Model.TicketStructure.OrderStatType, IDictionary<EyouSoft.Model.TicketStructure.RateType, string>> GetSupplierHandelStats(string supplierCId);

        /// <summary>
        /// 更新订单服务备注
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="note">服务备注</param>
        /// <returns></returns>
        bool UpdateServiceNote(string orderId, string note);

        /// <summary>
        /// 设置供应商收款金额
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="amount">供应商收款金额</param>
        /// <returns></returns>
        bool SetBalanceAmount(string orderId, decimal amount);

        /// <summary>
        /// 修改订单采购商备注(下单时特殊要求备注)
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="buyerRemark">备注</param>
        /// <returns></returns>
        bool UpdateBuyerRemark(string orderId, string buyerRemark);

        /// <summary>
        /// 写入订单账户信息(注：采购商订单支付时一定调用)
        /// </summary>
        /// <param name="orderAccountInfo">订单账户信息业务实体</param>
        /// <returns></returns>
        bool InsertOrderAccount(EyouSoft.Model.TicketStructure.OrderAccountInfo orderAccountInfo);

        /// <summary>
        /// 获取订单账户信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.OrderAccountInfo GetOrderAccountInfo(string orderId);

        /// <summary>
        /// 获取最新的订单变更信息
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.OrderChangeInfo GetLatestChange(string orderId);

        /// <summary>
        /// 采购商获取订单报表
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.OrderInfo> GetBuyerOrderReports(string buyerCId, DateTime? startTime, DateTime? finishTime);

        /// <summary>
        /// 设置变更金额
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="handlingFee">手续费</param>
        /// <param name="totalAmount">供应商变更总金额</param>
        /// <param name="changeAmount">变更总金额</param>
        /// <returns></returns>
        bool SetChangeAmount(string changeId, decimal handlingFee, decimal changeAmount, decimal totalAmount);

        /// <summary>
        /// 采购商获取订单报表(合计)
        /// </summary>
        /// <param name="buyerCId">采购商编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="finishTime">结束时间</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.BuyerOrderReportsTotalInfo GetBuyerOrderReportsTotal(string buyerCId, DateTime? startTime, DateTime? finishTime);

        /*/// <summary>
        /// 设置订单旅客变更审核备注
        /// </summary>
        /// <param name="changeId">变更编号</param>
        /// <param name="remark">备注</param>
        /// <returns></returns>
        bool SetChangeCheckRemark(string changeId, string remark);*/
    }
}
