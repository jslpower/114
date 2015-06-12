using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SMSStructure
{
    #region 短信号码的类型 SMSNoType
    /// <summary>
    /// 短信号码的类型
    /// </summary>
    /// zhangzy
    public enum SMSNoType
    {
        /// <summary>
        /// 移动,联通手机号码
        /// </summary>
        Mobiel,

        /// <summary>
        /// 小灵通号码
        /// </summary>
        PHS
    }
    #endregion

    #region SendType 短信发送方式
    /// <summary>
    /// 短信发送方式
    /// </summary>
    public enum SendType
    {
        /// <summary>
        /// 直接发送
        /// </summary>
        直接发送=0,
        /// <summary>
        /// 定时发送
        /// </summary>
        定时发送
    }
    #endregion

    #region 发送短信提交的业务实体 SendMessageInfo
    /// <summary>
    /// 发送短信提交的业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    [Serializable]
    public class SendMessageInfo
    {
        private SendType sendType = SendType.直接发送;
        private DateTime sendTime = DateTime.Now;
        /// <summary>
        /// 发送短信前是否验证平台剩余的可发送短信条数，默认为false
        /// </summary>
        public bool IsValidatePlatform = false;

        /// <summary>
        /// default constructor
        /// </summary>
        public SendMessageInfo() { }

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
        public int SMSType { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }
        /// <summary>
        /// 获得短信发送的完整内容(有判断了是否存在发信人，若存在发信人，则会自动加上发信人)
        /// 格式：内容 ＋ ["发信人：" + 公司名称]  其中[ ]表示可选
        /// </summary>
        public string SMSContentSendComplete
        {
            get
            {
                if (!string.IsNullOrEmpty(UserFullName))
                    return SMSContent + "发信人：" + UserFullName;
                else
                    return SMSContent;
            }
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
        /// 接收手机号码集合
        /// </summary>
        public List<AcceptMobileInfo> Mobiles { get; set; }
        /// <summary>
        /// 发送方式
        /// </summary>
        public SendType SendType
        {
            get { return this.sendType;}
            set { this.sendType = value; }
        }
        /// <summary>
        /// 发送通道
        /// </summary>
        public SMSChannel SendChannel
        {
            get;
            set;
        }
    }
    #endregion

    #region 发送短信/验证发送结果业务实体 SendResultInfo
    /// <summary>
    /// 发送短信/验证发送结果业务实体
    /// </summary>
    /// Author:汪奇志 2010-03-25
    [Serializable]
    public class SendResultInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SendResultInfo() { }

        /// <summary>
        /// constructor with specified initial value
        /// </summary>
        /// <param name="isSucceed">是否发送/验证成功</param>
        /// <param name="errorMessage">错误信息</param>
        public SendResultInfo(bool isSucceed, string errorMessage)
        {
            this.IsSucceed = isSucceed;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 是否发送/验证成功
        /// </summary>
        public bool IsSucceed { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 应扣除账户金额(为预扣除金额)
        /// </summary>
        public decimal CountFee { get; set; }
        /// <summary>
        /// 实际消费的金额(预扣除金额中扣除了发送超时的金额,该值为短信发送结束后的消费金额)
        /// </summary>
        public decimal SendFee { get; set; }
        /// <summary>
        /// 金额扣除临时表编号
        /// </summary>
        public string TempFeeTakeId { get; set; }
        /// <summary>
        /// 短信发送统计表编号
        /// </summary>
        public string SendTotalId { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountMoney { get; set; }
        /*/// <summary>
        /// 帐号剩余短信条数
        /// </summary>
        public int AccountSMSNumber { get; set; }*/
        /// <summary>
        /// [移动、联通]待发送的总的号码个数
        /// </summary>
        public int MobileNumberCount { get; set; }
        /// <summary>
        /// [小灵通]待发送的总的号码个数
        /// </summary>
        public int PHSNumberCount { get; set; }
        /// <summary>
        /// [移动、联通]实际发送成功的总的号码个数
        /// </summary>
        public int SuccessCount { get; set; }
        /// <summary>
        /// [小灵通]实际发送成功的总的号码个数
        /// </summary>
        public int PHSSuccessCount { get; set; }
        /// <summary>
        /// [移动、联通]实际发送失败的总的号码个数
        /// </summary>
        public int ErrorCount { get; set; }
        /// <summary>
        /// [小灵通]实际发送失败的总的号码个数
        /// </summary>
        public int PHSErrorCount { get; set; }
        /// <summary>
        /// [移动、联通]实际发送超时的总的号码个数
        /// </summary>
        public int TimeoutCount { get; set; }
        /// <summary>
        /// [小灵通]实际发送超时的总的号码个数
        /// </summary>
        public int PHSTimeoutCount { get; set; }
        /// <summary>
        /// [移动、联通]实际发送成功与失败,总的号码个数(排除超时的)
        /// </summary>
        public int MobileSendNumberCount { get { return this.MobileNumberCount - this.TimeoutCount; } }
        /// <summary>
        /// [小灵通]实际发送成功与失败,总的号码个数(排除超时的)
        /// </summary>
        public int PHSSendNumberCount { get { return this.PHSNumberCount - this.PHSTimeoutCount; } }
        /// <summary>
        /// [移动、联通]1条短信内容实际产生的短信条数
        /// </summary>
        public int FactCount { get; set; }
        /// <summary>
        /// [小灵通]1条短信内容实际产生的短信条数
        /// </summary>
        public int PHSFactCount { get; set; }
        /// <summary>
        /// [移动，联通]待发送的总的短信条数，[移动，联通]待发送的总的号码个数*[移动、联通]1条短信内容实际产生的短信条数
        /// </summary>
        public int WaitSendMobileCount { get { return this.MobileNumberCount * this.FactCount; } }
        /// <summary>
        /// [小灵通]待发送的总的短信条数，[小灵通]待发送的总的号码个数*[小灵通]1条短信内容实际产生的短信条数
        /// </summary>
        public int WaitSendPHSCount { get { return this.PHSNumberCount * this.PHSFactCount; } }
        /// <summary>
        /// [移动，联通]实际发送成功与失败,总的短信条数(排除超时的)
        /// </summary>
        public int MobileSendCount { get { return this.MobileSendNumberCount * this.FactCount; } }
        /// <summary>
        /// [小灵通]实际发送成功与失败,总的短信条数(排除超时的)
        /// </summary>
        public int PHSSendCount { get { return this.PHSSendNumberCount * this.PHSFactCount; } }
    }
    #endregion

    #region 定时发送短信计划任务信息业务实体
    /// <summary>
    /// 定时发送短信计划任务信息业务实体
    /// </summary>
    /// 2010-09-21
    public class SendPlanInfo
    {
        /// <summary>
        /// 计划任务编号
        /// </summary>
        public string PlanId { get; set; }
        /// <summary>
        /// 发送短信公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 发送短信公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 发送短信用户编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 发送短信发送人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 短信类型
        /// </summary>
        public int SMSType { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public List<AcceptMobileInfo> Mobiles { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 短信发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 短信发送通道
        /// </summary>
        public SMSChannel SendChannel { get; set; }
    }
    #endregion

    #region 接收短信号码信息业务实体
    /// <summary>
    /// 接收短信号码信息业务实体
    /// </summary>
    [Serializable]
    public class AcceptMobileInfo
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public AcceptMobileInfo() { }

        /// <summary>
        /// 接收号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 是否在显示时进行加密
        /// </summary>
        public bool IsEncrypt { get; set; }
    }
    #endregion
}
