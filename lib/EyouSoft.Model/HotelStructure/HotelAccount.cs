using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.HotelStructure
{
    /// <summary>
    /// 酒店系统-结算帐号实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelAccount
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
        /// 结算账户类型
        /// </summary>
        public HotelAccountType AccountType { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public HotelSettlement Settlement { get; set; }
        /// <summary>
        /// 开户姓名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 开户行及支行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行卡号（财付通帐号）
        /// </summary>
        public string BankNo { get; set; }
        /// <summary>
        /// 是否邮寄发票
        /// </summary>
        public bool IsMailInvoice { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime IssueTime { get; set; }
    }
    /// <summary>
    /// 结算账户类型
    /// </summary>
    public enum HotelAccountType
    {
        /// <summary>
        /// 银行卡
        /// </summary>
        银行卡 = 0,
        /// <summary>
        /// 中间账户财付通
        /// </summary>
        中间账户财付通 = 1
    }
    /// <summary>
    /// 结算账户类型
    /// </summary>
    public enum HotelSettlement
    {
        /// <summary>
        /// 一月一结
        /// </summary>
        一月一结= 0,
        /// <summary>
        /// 离店后5日结算
        /// </summary>
        离店后5日结算 = 1
    }

}
