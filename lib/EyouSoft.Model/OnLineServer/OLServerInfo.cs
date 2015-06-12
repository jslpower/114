using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.OnLineServer
{
    #region 在线客服-用户信息 

    /// <summary>
    /// 在线客服-用户信息
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-23
    [Serializable]
    public class OlServerUserInfo
    {
        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 在线编号
        /// </summary>
        public string OId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 在线名称
        /// </summary>
        public string OlName { get; set; }

        /// <summary>
        /// 是否客服
        /// </summary>
        public bool IsService
        {
            get;
            set;
        }
        /// <summary>
        /// 进入在线客服系统时间
        /// </summary>
        public DateTime LoginTime
        {
            get;
            set;
        }
        /// <summary>
        /// 接收消息人在线编号
        /// </summary>
        public string AcceptId
        {
            get;
            set;
        }
        /// <summary>
        /// 接收消息人在线名称
        /// </summary>
        public string AcceptName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline
        {
            get;
            set;
        }

        /// <summary>
        /// 最后发送消息时间
        /// </summary>
        public DateTime LastSendMessageTime { get; set; }
    }

    #endregion OlServerUserInfo class

    #region 在线客服-消息

    /// <summary>
    /// 在线客服-消息
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-23
    [Serializable]
    public class OlServerMessageInfo
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public string MessageId
        {
            get;
            set;
        }
        /// <summary>
        /// 发送人在线编号
        /// </summary>
        public string SendId
        {
            get;
            set;
        }
        /// <summary>
        /// 发送人在线名称
        /// </summary>
        public string SendName
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人在线编号
        /// </summary>
        public string AcceptId
        {
            get;
            set;
        }
        /// <summary>
        /// 接收人在线名称
        /// </summary>
        public string AcceptName
        {
            get;
            set;
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get;
            set;
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get;
            set;
        }
    }

    #endregion OlServerMessageInfo class

    #region 在线客服-配置信息

    /// <summary>
    /// 在线客服-配置信息
    /// </summary>
    [Serializable]
    public class OlServerConfig
    {
        private int getUsersInterval = 5;
        private int getMessageInterval = 3;
        private int stayTime = 10;
        private int clearUserOutInterVal = 1;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OlServerConfig() { }

        /// <summary>
        /// Constructor with specified initial values
        /// </summary>
        /// <param name="getUsersInterval">获取用户列表间隔时间 单位:秒</param>
        /// <param name="getMessageInterval">获取消息间隔时间 单位:秒</param>
        /// <param name="stayTime">未发送消息的客户在线停留时间 单位:分钟</param>
        /// <param name="clearUserOutInterVal">执行用户在线状态清理的间隔时间</param>
        public OlServerConfig(int getUsersInterval, int getMessageInterval, int stayTime, int clearUserOutInterVal)
        {
            this.getUsersInterval = getUsersInterval;
            this.getMessageInterval = getMessageInterval;
            this.stayTime = stayTime;
            this.clearUserOutInterVal = clearUserOutInterVal;
        }

        /// <summary>
        /// 获取用户列表间隔时间 单位:秒
        /// </summary>
        public int GetUsersInterval
        {
            get { return this.getUsersInterval; }
            set { this.getUsersInterval = value; }
        }
        /// <summary>
        /// 获取消息间隔时间 单位:秒
        /// </summary>
        public int GetMessageInterval
        {
            get { return this.getMessageInterval; }
            set { this.getMessageInterval = value; }
        }
        /// <summary>
        /// 未发送消息的客户在线停留时间 单位:分钟
        /// </summary>
        public int StayTime
        {
            get { return this.stayTime; }
            set { this.stayTime = value; }
        }
        /// <summary>
        /// 执行用户在线状态清理的间隔时间
        /// </summary>
        public int ClearUserOutInterVal
        {
            get { return this.clearUserOutInterVal; }
            set { this.clearUserOutInterVal = value; }
        }
    }
    #endregion OlServerConfig class

    #region 发送消息后的返回结果
    /// <summary>
    /// 发送消息后的返回结果
    /// </summary>
    /// Author:汪奇志 DateTime:2009-09-27
    [Serializable]
    public class OlServerSendMessageResultInfo
    {
        private string messageId;
        private DateTime sendTime;
        private bool isSuccess;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OlServerSendMessageResultInfo() { }

        /// <summary>
        /// Constructor with specified initial values
        /// </summary>
        /// <param name="messageId">发送的消息编号</param>
        /// <param name="sendTime">发送时间</param>
        /// <param name="isSuccess">是否发送成功</param>
        public OlServerSendMessageResultInfo(string messageId, DateTime sendTime, bool isSuccess)
        {
            this.messageId = messageId;
            this.sendTime = sendTime;
            this.isSuccess = isSuccess;
        }

        /// <summary>
        /// 发送的消息编号
        /// </summary>
        public string MessageId
        {
            get { return this.messageId; }
            set { this.messageId = value; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return this.sendTime; }
            set { this.sendTime = value; }
        }

        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool IsSuccess
        {
            get { return this.isSuccess; }
            set { this.isSuccess = value; }
        }
    }
    #endregion OlServerSendMessageResultInfo class
}
