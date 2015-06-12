using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    /// <summary>
    /// 短信发送明细业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    public class SendDetailInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SendDetailInfo() { }

        /// <summary>
        /// 发送编号
        /// </summary>
        public string SendId { get; set; }
        /// <summary>
        /// 发送短信的公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 发送短信的用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 短信统计编号
        /// </summary>
        public string SendTotalId { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public int SMSType { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /*
        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool IsSuccess { get; set; }*/
        /// <summary>
        /// 发送返回结果 -2147483646.超时 0.成功 其它均为失败 
        /// </summary>
        public int ReturnResult { get; set; }
        /// <summary>
        /// 发送返回的消息
        /// </summary>
        public string ReturnMsg { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal UseMoeny { get; set; }
        /// <summary>
        /// 数据入库时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 是否是小灵通
        /// </summary>
        public bool IsPHS { get; set; }
        /// <summary>
        /// 实际计算费用信息条数
        /// </summary>
        public int FactCount { get; set; }
        /// <summary>
        /// 是否在显示时进行加密
        /// </summary>
        public bool IsEncrypt { get; set; }
    }
}
