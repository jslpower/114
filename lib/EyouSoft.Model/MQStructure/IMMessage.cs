using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.MQStructure
{
    #region MQ消息实体
    /// <summary>
    /// MQ消息实体
    /// </summary>
    /// 周文超 2010-05-11
    [Serializable]
    public class IMMessage
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMMessage()
		{}

		#region Model

		private int _num;
		private int _src;
		private int _dst;
		private string _message;
		private int _flag;
		private DateTime _issuetime;

		/// <summary>
		/// 编号
		/// </summary>
		public int num
		{
			set{ _num=value;}
			get{return _num;}
		}
		/// <summary>
		/// 发送人MQ编号
		/// </summary>
		public int src
		{
			set{ _src=value;}
			get{return _src;}
		}
		/// <summary>
		/// 接收人MQ编号
		/// </summary>
		public int dst
		{
			set{ _dst=value;}
			get{return _dst;}
		}
		/// <summary>
		/// 消息内容
		/// </summary>
		public string message
		{
			set{ _message=value;}
			get{return _message;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 消息发送时间
		/// </summary>
		public DateTime IssueTime
		{
			set{ _issuetime=value;}
			get{return _issuetime;}
		}

		#endregion Model

    }
    #endregion

    /*
    #region MQ消息一对一消息业务实体

    /// <summary>
    /// MQ消息一对一消息业务实体
    /// </summary>
    /// Author:汪奇志 2010-02-26
    [Serializable]
    public class OneToOneMessageInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OneToOneMessageInfo() { }

        /// <summary>
        /// constructor with specified initial values
        /// </summary>
        /// <param name="sendType">MQ消息的发送方式 1:以系统消息的方式发送 2:以聊天窗口的方式发送</param>
        /// <param name="sendMQId">发送消息人的MQ编号</param>
        /// <param name="acceptMQId">接收消息人的MQ编号</param>
        /// <param name="messageContent">消息内容</param>
        public OneToOneMessageInfo(int sendType, int sendMQId, int acceptMQId, string messageContent)
        {
            this.SendType = sendType;
            this.SendMQId = sendMQId;
            this.AcceptMQId = acceptMQId;
            this.MessageContent = messageContent;
        }

        /// <summary>
        /// MQ消息的发送方式 1:以系统消息的方式发送 2:以聊天窗口的方式发送
        /// </summary>
        public int SendType { get; set; }
        /// <summary>
        /// 发送消息人的MQ编号
        /// </summary>
        public int SendMQId { get; set; }
        /// <summary>
        /// 接收消息人的MQ编号
        /// </summary>
        public int AcceptMQId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
    }

    #endregion MQ消息一对一消息业务实体   

    #region MQ消息一对多(指定了接收人的MQ编号)消息业务实体

    /// <summary>
    /// MQ消息一对多(指定了接收人的MQ编号)消息业务实体
    /// </summary>
    public class OneToManySpecifiedMQMessageInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OneToManySpecifiedMQMessageInfo() { }
        /// <summary>
        /// MQ消息的发送方式 1:以系统消息的方式发送 2:以聊天窗口的方式发送
        /// </summary>
        public int SendType { get; set; }
        /// <summary>
        /// 发送消息人的MQ编号
        /// </summary>
        public int SendMQId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 接收消息人相关信息
        /// </summary>
        public IList<OneToManySpecifiedMQMessageAccepMQInfo> AcceptMQInfo { get; set; }
        /// <summary>
        /// 消息中是否包含有链接
        /// </summary>
        public bool IsLink { get; set; }
    }

    #endregion MQ消息一对多(指定了接收人的MQ编号)消息业务实体

    #region MQ消息一对多(指定了接收人的MQ编号)消息的接收人信息业务实体

    /// <summary>
    /// MQ消息一对多(指定了接收人的MQ编号)消息的接收人信息业务实体
    /// </summary>
    public class OneToManySpecifiedMQMessageAccepMQInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OneToManySpecifiedMQMessageAccepMQInfo() { }

        /// <summary>
        /// 接收人MQ编号
        /// </summary>
        public int MQId { get; set; }
        /// <summary>
        /// 消息链接的编号
        /// </summary>
        public string MessageUrlGuid { get; set; }
        /// <summary>
        /// 消息链接要跳转到的URL
        /// </summary>
        public string MessageUrlToUrl { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContent { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }
    }

    #endregion*/

    #region MQ消息发送方式
    /// <summary>
    /// MQ消息发送方式
    /// </summary>
    public enum SendType
    {
        /// <summary>
        /// 系统消息方式发送
        /// </summary>
        系统消息=1,
        /// <summary>
        /// 聊天窗口方式发送
        /// </summary>
        聊天窗口
    }
    #endregion

    #region 接收消息用户状态
    /// <summary>
    /// 接收消息用户状态
    /// </summary>
    public enum OnlineState
    {
        /// <summary>
        /// 所有用户
        /// </summary>
        所有用户=0,
        /// <summary>
        /// 在线用户
        /// </summary>
        在线用户,
        /// <summary>
        /// 登录过用户
        /// </summary>
        登录过用户
    }
    #endregion

    #region MQ消息一对多(未指定接收人的MQ编号)消息业务实体
    /// <summary>
    /// MQ消息一对多(未指定接收人的MQ编号)消息业务实体
    /// </summary>
    /// Author:汪奇志 2010-02-26
    public class OneToManyUnspecifiedMQMessageInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public OneToManyUnspecifiedMQMessageInfo() { }

        /// <summary>
        /// MQ消息发送方式
        /// </summary>
        public SendType SendType { get; set; }
        /// <summary>
        /// 发送消息人的MQ编号
        /// </summary>
        public int SendMQId { get; set; }
        /// <summary>
        /// 接收消息的省份 0:所有省份
        /// </summary>
        public int AcceptProvinceId { get; set; }
        /// <summary>
        /// 接收消息的城市 0:所有城市
        /// </summary>
        public int AcceptCityId { get; set; }
        /// <summary>
        /// 是否所有公司类型
        /// </summary>
        public bool IsAllCompanyType { get; set; }
        /// <summary>
        /// 接收消息公司类型 IsAllCompanyType=false时生效
        /// </summary>
        public IList<EyouSoft.Model.CompanyStructure.CompanyType> AcceptCompanyType { get; set; }
        /// <summary>
        /// 是否包含随便逛逛用户 IsAllCompanyType=false时生效
        /// </summary>
        public bool IsContainsGuest { get; set; }
        /// <summary>
        /// 接收消息用户状态
        /// </summary>
        public OnlineState OnlineState { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }

    #endregion
}
