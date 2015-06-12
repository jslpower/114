using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.ToolStructure
{
    /// <summary>
    /// 消息提醒记录实体类
    /// </summary>
    /// Author:张志瑜  2010-10-21
    public class MsgTipRecord
    {
        /// <summary>
        /// 接收消息用户MQID
        /// </summary>
        public string ToMQID { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 消息类型编号
        /// </summary>
        public MsgType MsgType { get; set; }
        /// <summary>
        /// 消息发送途径
        /// </summary>
        public MsgSendWay SendWay { get; set; }
        /// <summary>
        /// 触发消息发送的用户MQID
        /// </summary>
        public string FromMQID { get; set; }

    }

    /// <summary>
    /// 用户操作提醒消息类型
    /// </summary>
    public enum MsgType
    { 
        /// <summary>
        /// 注册审核通过 = 1
        /// </summary>
        RegPass = 1,

        /// <summary>
        /// 被加为好友 = 2
        /// </summary>
        AddFriend = 2,

        /// <summary>
        /// 有MQ留言信息 = 3
        /// </summary>
        MQNoReadMsg = 3,

        /// <summary>
        /// 收到新订单 = 4
        /// </summary>
        NewOrder = 4,

        /// <summary>
        /// 长期未登录 = 5
        /// </summary>
        LongOffLine = 5
    }

    /// <summary>
    /// 消息发送的途径
    /// </summary>
    public enum MsgSendWay
    { 
        /// <summary>
        /// 短信 = 1
        /// </summary>
        SMS = 1,

        /// <summary>
        /// 邮件 = 2
        /// </summary>
        Email = 2
    }
}
