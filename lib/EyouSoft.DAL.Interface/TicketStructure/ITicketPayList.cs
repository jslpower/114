using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 机票金额流水明细数据层接口
    /// </summary>
    /// 周文超 2010-10-25
    public interface ITicketPayList
    {
        /// <summary>
        /// 添加机票系统-支付明细记录
        /// </summary>
        /// <param name="model">添加机票系统支付明细实体</param>
        /// <returns>返回true表示成功</returns>
        bool AddTicketPay(EyouSoft.Model.TicketStructure.TicketPay model);

        /// <summary>
        /// 获取机票系统-支付明细记录
        /// </summary>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="TradeNo">提交给支付接口的交易号</param>
        /// <param name="BatchNo">提交给支付接口的批次号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketPay> GetTicketPay(string ItemId
            , EyouSoft.Model.TicketStructure.ItemType? ItemType, string TradeNo, string BatchNo);
        
        /// <summary>
        /// 支付回调函数
        /// </summary>
        /// <param name="PayNumber">支付接口返回的交易号</param>
        /// <param name="PayState">交易状态</param>
        /// <param name="Remark">备注（更新后的备注=原来的+现在的）</param>
        /// <param name="TradeNo">提交到支付接口的交易号</param>
        /// <param name="BatchNo">批次号（没有批次号null）</param>
        /// <param name="PayType">支付接口类型</param>
        /// <param name="ItemId">记录项的ID</param>
        /// <param name="ItemType">记录项的类型</param>
        /// <param name="CurrCompanyId">当前操作公司编号</param>
        /// <param name="CurrUserId">当前操作用户编号</param>
        /// <returns>返回true表示成功</returns>
        bool PayCallback(string PayNumber, EyouSoft.Model.TicketStructure.PayState PayState, string Remark,
            string TradeNo, string BatchNo, EyouSoft.Model.TicketStructure.TicketAccountType PayType,
            out string ItemId, out EyouSoft.Model.TicketStructure.ItemType? ItemType, out string CurrCompanyId, out string CurrUserId);
    }
}
