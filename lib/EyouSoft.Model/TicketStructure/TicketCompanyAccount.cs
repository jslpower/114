using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.TicketStructure
{
    /// <summary>
    /// 平台机票供应商（采购商）公司账户信息实体类
    /// </summary>
    /// 创建人：luofx 2010-10-21
    public class TicketCompanyAccount
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 公司账户
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// 支付接口类型（1：财付通，2：支付宝，3：工行，4：建行，5：农行，6：招行，7：其他银行）
        /// </summary>
        public TicketAccountType InterfaceType { get; set; }
        /// <summary>
        /// 是否加入支付圈电子协议签约
        /// </summary>
        public bool IsSign { get; set; }
    }
    /// <summary>
    /// 机票支付类别枚举
    /// 创建人：luofx 2010-10-21
    /// </summary>
    public enum TicketAccountType
    {
        /// <summary>
        /// 财付通
        /// </summary>
        财付通=1,
        /// <summary>
        /// 支付宝
        /// </summary>
        支付宝 = 2,
        /// <summary>
        /// 工行
        /// </summary>
        工行 = 3,
        /// <summary>
        /// 建行
        /// </summary>
        建行 = 4,
        /// <summary>
        /// 农行
        /// </summary>
        农行 = 5,
        /// <summary>
        /// 招行
        /// </summary>
        招行 = 6,
        /// <summary>
        /// 其他银行
        /// </summary>
        其他银行 = 7
    }
}
