using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 短信发送统计信息业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class SendTotalInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SendTotalInfo() { }

        /// <summary>
        /// 统计编号
        /// </summary>
        public string TotalId { get; set; }
        /// <summary>
        /// 发送短信的公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 发送短信的公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 发送短信的用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 发送短信的用户姓名
        /// </summary>
        public string UserFullName { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public byte SMSType { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal UseMoeny { get; set; }
        /// <summary>
        /// 发送成功短信数量
        /// </summary>
        public int SuccessCount { get; set; }
        /// <summary>
        /// 发送失败短信数量
        /// </summary>
        public int ErrorCount { get; set; }
        /// <summary>
        /// 发送通道
        /// </summary>
        public SMSChannel SendChannel { get; set; }
    }
}
