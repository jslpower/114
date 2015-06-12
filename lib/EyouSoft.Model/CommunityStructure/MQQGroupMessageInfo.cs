//汪奇志 2011-11-23
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.CommunityStructure
{
    /// <summary>
    /// qq group message info
    /// </summary>
    public class MQQGroupMessageInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MQQGroupMessageInfo() { }

        /// <summary>
        /// 消息编号
        /// </summary>
        public string MessageId { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 群号
        /// </summary>
        public string QGID { get; set; }
        /// <summary>
        /// Q号
        /// </summary>
        public string QUID { get; set; }
        /// <summary>
        /// Q消息发送时间
        /// </summary>
        public string QMTime { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public QQGroupMessageStatus Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 登录Q号
        /// </summary>
        public string FUID { get; set; }
    }

    /// <summary>
    /// QQ群消息状态
    /// </summary>
    public enum QQGroupMessageStatus
    {
        /// <summary>
        /// 未激活
        /// </summary>
        未激活,
        /// <summary>
        /// 已激活
        /// </summary>
        已激活
    }
}
