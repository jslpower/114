using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    /// 运价套餐购买记录接口
    /// </summary>
    /// Author:罗丽娥  2010-10-29
    public interface IFreightBuyLog
    {
        /// <summary>
        /// 购买运价航线记录
        /// </summary>
        /// <param name="model">运价套餐购买记录实体</param>
        /// <returns>true:成功 false:失败</returns>
        bool Add(EyouSoft.Model.TicketStructure.TicketFreightBuyLog model);

        /// <summary>
        /// 修改支付状态
        /// </summary>
        /// <param name="TicketFreightId">主键编号</param>
        /// <param name="PayState">支付状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetPayInfo(string TicketFreightId, bool PayState);

        /// <summary>
        /// 获取可用运价条数
        /// </summary>
        /// <param name="CompanyID">供应商ID</param>
        /// <param name="RateType">业务类型</param>
        /// <returns></returns>
        int GetAvailableCount(string CompanyID, EyouSoft.Model.TicketStructure.RateType? RateType);
        /// <summary>
        /// 获取供应商的套餐购买记录统计信息
        /// </summary>
        /// <param name="StartDate">启用时间</param>
        /// <param name="CompanyId">购买公司编号</param>
        /// <param name="RateType">业务类型</param>
        /// <returns>套餐购买记录统计信息实体</returns>
        EyouSoft.Model.TicketStructure.PackBuyLogStatistics GetPackBuyLog(DateTime? StartDate, string CompanyId, EyouSoft.Model.TicketStructure.RateType? RateType);

        /// <summary>
        /// 获取套餐购买记录实体
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>套餐购买记录实体</returns>
        EyouSoft.Model.TicketStructure.TicketFreightBuyLog GetModel(string Id);

        /// <summary>
        /// 购买运价套餐支付前添加支付流水明细信息
        /// </summary>
        /// <param name="orderid">订单ID</param>
        /// <param name="orderno">订单号</param>
        /// <param name="userid">当前操作人ID</param>
        /// <param name="companyid">当前操作公司ID</param>
        /// <param name="payprice">支付金额</param>
        /// <param name="paytype">支付接口类型</param>
        /// <param name="batch_no">批次号</param>
        /// <returns></returns>
        bool AddTicketPay(string orderid,string orderno,string userid,string companyid,decimal payprice,EyouSoft.Model.TicketStructure.TicketAccountType paytype, out string batch_no);

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <param name="payNumber">支付接口返回的交易流水号</param>
        /// <param name="payprice">支付金额</param>
        /// <param name="paystate">支付状态</param>
        /// <param name="paytype">支付接口类型</param>
        /// <param name="payaccount">支付账号</param>
        /// <param name="paytradeno">提交到支付接口的交易号</param>
        /// <param name="paytime">支付时间</param>
        /// <param name="batchno">批次号</param>
        /// <returns></returns>
        bool PayAfter(string payNumber,decimal? payprice,EyouSoft.Model.TicketStructure.PayState paystate,EyouSoft.Model.TicketStructure.TicketAccountType paytype,string payaccount,string paytradeno,DateTime? paytime,string batchno);

         /// <summary>
        /// 根据运价启用状态与主键编号，设置购买记录可用数
        /// </summary>
        /// <param name="Id">主键编号[对应运价表中的，套餐购买编号]</param>
        /// <param name="FreightEnabled">运价启用状态</param>
        /// <returns>true:成功 false:失败</returns>
        bool SetAvailableByFreightState(string Id, bool FreightEnabled);
    }
}
