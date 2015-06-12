using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.SMSStructure
{
    /// <summary>
    /// 短信中心-发送短信业务逻辑类
    /// </summary>
    /// Author:汪奇志 2010-06-10
    public class SendMessage:EyouSoft.IBLL.SMSStructure.ISendMessage
    {
        private readonly IDAL.SMSStructure.ISendMessage dal = ComponentFactory.CreateDAL<EyouSoft.IDAL.SMSStructure.ISendMessage>();

        /*发行版private readonly EyouSoft.BLL.SMSStructure.SmsServices.SMSWS sms = new EyouSoft.BLL.SMSStructure.SmsServices.SMSWS();*/
        /*测试版private readonly EyouSoft.BLL.SMSStructure.TSmsServices.SMSWS sms = new EyouSoft.BLL.SMSStructure.TSmsServices.SMSWS();*/
        /*新接口EyouSoft.BLL.SMSStructure.VoSmsServices.Service sms1 = new EyouSoft.BLL.SMSStructure.VoSmsServices.Service();*/
        
        /// <summary>
        /// 发送短信接口
        /// </summary>
        EyouSoft.BLL.SMSStructure.VoSmsServices.Service sms = new EyouSoft.BLL.SMSStructure.VoSmsServices.Service();
        /// <summary>
        /// send message enterprise id
        /// </summary>
        private string EnterpriseId = string.Empty;
        /// <summary>
        /// 发送短信超时时异常编号
        /// </summary>
        private readonly int SendTimeOutEventCode = -2147483646;

        #region CreateInstance
        /// <summary>
        /// 创建短信中心-常用短语及常用短语类型业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SMSStructure.ISendMessage CreateInstance()
        {
            EyouSoft.IBLL.SMSStructure.ISendMessage op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SMSStructure.ISendMessage>();
            }
            return op1;
        }
        #endregion

        #region private member
        /// <summary>
        /// 获取短信内容实际产生总的短信条数
        /// </summary>
        /// <param name="SMSContentSendComplete">要发送的完整的短信内容</param>
        /// <param name="smsType">短信号码类型</param>
        /// <param name="channel">发送通道</param>
        /// <returns></returns>
        private int GetSmsTotalCount(string SMSContentSendComplete, EyouSoft.Model.SMSStructure.SMSNoType smsType,EyouSoft.Model.SMSStructure.SMSChannel channel)
        {
            //1条短信所占的字符长度
            int oneSmsLength = 210;
            //总的实际短信条数
            int messageFaceCount = 1;

            if (!channel.IsLong)//非长短信
            {
                switch (smsType)
                {
                    case EyouSoft.Model.SMSStructure.SMSNoType.Mobiel: oneSmsLength = 70; break;
                    case EyouSoft.Model.SMSStructure.SMSNoType.PHS: oneSmsLength = 45; break;
                }
            }

            if (SMSContentSendComplete.Length > oneSmsLength)
            {
                messageFaceCount = (SMSContentSendComplete.Length + oneSmsLength - 1) / oneSmsLength;
            }

            return messageFaceCount;
        }

        /// <summary>
        /// 获取小灵通号码个数
        /// </summary>
        /// <param name="mobiles"></param>
        /// <returns></returns>
        private int GetPHSCount(IList<EyouSoft.Model.SMSStructure.AcceptMobileInfo> mobiles)
        {
            int phsCount = 0;

            foreach (var mobile in mobiles)
            {
                phsCount += this.IsPHS(mobile.Mobile) ? 1 : 0;
            }

            return phsCount;
        }

        /// <summary>
        /// 验证是否是小灵通号码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsPHS(string s)
        {
            string mobileRegexPattern = System.Configuration.ConfigurationManager.AppSettings["SMS_Mobile_Regex_Pattern"];//EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("appSettings", "SMS_Mobile_Regex_Pattern");
            System.Text.RegularExpressions.Regex regMobile = new System.Text.RegularExpressions.Regex(mobileRegexPattern);

            return !regMobile.IsMatch(s);
        }

        /// <summary>
        /// 获得短信服务接口状态
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        private string GetServicesState(int code)
        {
            string state = "";
            switch (code)
            { 
                case 0:
                    state = "成功";
                    break;
                case -1:
                    state = "登录错误";
                    break;
                case -2:
                    state = "数据库链接错误";
                    break;
                case -3:
                    state = "语句超长";
                    break;
                case -4:
                    state = "网络超时";
                    break;
                case -5:
                    state = "手机号码个数超过";
                    break;
                case -6:
                    state = "费用不足";
                    break;
                case -7:
                    state = "手机号码错误";
                    break;
                case -8:
                    state = "短信内容为空";
                    break;
                case -9:
                    state = "包含关键字";
                    break;
                case -11:
                    state = "通道错误";
                    break;
            }
            return state;
        }

        /// <summary>
        /// 是否包含关键字，包含时返回关键字内容，不包含时返回空字符串
        /// </summary>
        /// <param name="s">要验证的字符串</param>
        /// <returns></returns>
        public string IsIncludeKeyWord(string s)
        {
            string keyWord = string.Empty;

            try
            {
                sms.Timeout = 5000;
                while (true)
                {
                    string tmp = sms.IsIncludeKeyWord(s);

                    if (string.IsNullOrEmpty(tmp)) break;

                    keyWord += tmp + ",";

                    s = s.Replace(tmp, "");
                }
            }
            catch { }

            if (!string.IsNullOrEmpty(keyWord))
            {
                keyWord = keyWord.Substring(0, keyWord.Length - 1);
            }

            return keyWord;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 验证要发送的短信
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <returns></returns>
        public EyouSoft.Model.SMSStructure.SendResultInfo ValidateSend(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo)
        {
            EyouSoft.Model.SMSStructure.SendResultInfo validateResultInfo = new EyouSoft.Model.SMSStructure.SendResultInfo(true, null);

            if (string.IsNullOrEmpty(sendMessageInfo.CompanyId))
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "未填写发送短信的公司编号";

                return validateResultInfo;
            }

            if (sendMessageInfo.SendChannel.PriceOne <= 0)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "短信费用不能小于等于零";

                return validateResultInfo;
            }

            if (string.IsNullOrEmpty(sendMessageInfo.SMSContent))
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "短信内容不能为空";

                return validateResultInfo;
            }

            if (sendMessageInfo.Mobiles == null || sendMessageInfo.Mobiles.Count < 1)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "未填写任何接收短信人的手机号码";

                return validateResultInfo;
            }

            if (sendMessageInfo.SendType == EyouSoft.Model.SMSStructure.SendType.定时发送 && sendMessageInfo.SendTime <= DateTime.Now)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "定时发送时间不能小于当前时间";

                return validateResultInfo;
            }

            string keyWord = this.IsIncludeKeyWord(sendMessageInfo.SMSContentSendComplete);

            if (!string.IsNullOrEmpty(keyWord))
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = string.Format("您要发送的短信内容中包含:{0} 这些禁止发送的关键字，请重新编辑！", keyWord);

                return validateResultInfo;
            }

            //短信内容针对移动联通手机实际产生的短信条数
            validateResultInfo.FactCount = this.GetSmsTotalCount(sendMessageInfo.SMSContentSendComplete, EyouSoft.Model.SMSStructure.SMSNoType.Mobiel, sendMessageInfo.SendChannel);
            //短信内容针对小灵通实际产生的短信条数
            validateResultInfo.PHSFactCount = this.GetSmsTotalCount(sendMessageInfo.SMSContentSendComplete, EyouSoft.Model.SMSStructure.SMSNoType.PHS, sendMessageInfo.SendChannel);

            //小灵通号码个数
            validateResultInfo.PHSNumberCount = this.GetPHSCount(sendMessageInfo.Mobiles);
            //移动联通号码个数
            validateResultInfo.MobileNumberCount = sendMessageInfo.Mobiles.Count - validateResultInfo.PHSNumberCount;

            //应扣除的金额
            validateResultInfo.CountFee = 0.01M * sendMessageInfo.SendChannel.PriceOne * (validateResultInfo.WaitSendMobileCount + validateResultInfo.WaitSendPHSCount);

            #region 使用账户余额进行验证
            //用户账户余额
            validateResultInfo.AccountMoney = EyouSoft.BLL.SMSStructure.Account.CreateInstance().GetAccountMoney(sendMessageInfo.CompanyId);
            
            if (validateResultInfo.AccountMoney < validateResultInfo.CountFee)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = string.Format("您的账户余额不足,当前余额为:{0}，此次消费:{1}", validateResultInfo.AccountMoney.ToString("C2"), validateResultInfo.CountFee.ToString("C2"));

                return validateResultInfo;
            }
            #endregion

           #region 平台帐号余额验证
            if (sendMessageInfo.IsValidatePlatform)
            {
                //114平台剩余的短信条数
                int smsPlatformCount = 0;
                try
                {
                    smsPlatformCount = sms.QuerySMSLeft(this.EnterpriseId, sendMessageInfo.SendChannel.UserName, sendMessageInfo.SendChannel.Pw);
                    //smsPlatformCount = sms.QuerySMSLeft(this.EnterpriseId, "yn3", "298748");
                }
                catch { }
                if (smsPlatformCount < validateResultInfo.WaitSendMobileCount + validateResultInfo.WaitSendPHSCount)
                {
                    validateResultInfo.IsSucceed = false;
                    validateResultInfo.ErrorMessage = string.Format("对不起，当前114平台短信可发送条数不足，最大可发送数量为{0}条!", smsPlatformCount);

                    return validateResultInfo;
                }
            }
            #endregion 平台帐号余额验证

            return validateResultInfo;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <returns></returns>
        public EyouSoft.Model.SMSStructure.SendResultInfo Send(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo)
        {
            #region 发送短信验证
            //发送短信验证
            EyouSoft.Model.SMSStructure.SendResultInfo validateResultInfo = this.ValidateSend(sendMessageInfo);

            if (!validateResultInfo.IsSucceed)
            {
                return validateResultInfo;
            }
            #endregion

            #region 定时发送任务处理
            //定时发送任务处理
            if (sendMessageInfo.SendType == EyouSoft.Model.SMSStructure.SendType.定时发送)
            {
                EyouSoft.Model.SMSStructure.SendPlanInfo plan = new EyouSoft.Model.SMSStructure.SendPlanInfo();

                plan.CompanyId = sendMessageInfo.CompanyId;
                plan.CompanyName = sendMessageInfo.CompanyName;
                plan.ContactName = sendMessageInfo.UserFullName;
                plan.IssueTime = DateTime.Now;
                plan.Mobiles = sendMessageInfo.Mobiles;
                plan.PlanId = Guid.NewGuid().ToString();
                plan.SendChannel = sendMessageInfo.SendChannel;
                plan.SendTime = sendMessageInfo.SendTime;
                plan.SMSContent = sendMessageInfo.SMSContent;
                plan.SMSType = sendMessageInfo.SMSType;
                plan.UserId = sendMessageInfo.UserId;

                if (dal.InsertSendPlan(plan))
                {
                    validateResultInfo.IsSucceed = true;
                }
                else
                {
                    validateResultInfo.IsSucceed = false;
                    validateResultInfo.ErrorMessage = "写入定时发送计划任务时发生了错误。";
                }
                
                return validateResultInfo;
            }
            #endregion

            #region 发送短信处理
            validateResultInfo.TempFeeTakeId = Guid.NewGuid().ToString();
            validateResultInfo.SendTotalId = Guid.NewGuid().ToString();
            validateResultInfo.SuccessCount 
                = validateResultInfo.ErrorCount 
                = validateResultInfo.TimeoutCount 
                = 0;

            #region 扣除账户金额
            //扣除账户金额
            bool deductAccountMoneyResult = EyouSoft.BLL.SMSStructure.Account.CreateInstance().DeductAccountMoney(sendMessageInfo.CompanyId, sendMessageInfo.UserId, validateResultInfo.CountFee, 0, validateResultInfo.TempFeeTakeId, validateResultInfo.SendTotalId);

            if (!deductAccountMoneyResult)
            {
                validateResultInfo.IsSucceed = false;
                validateResultInfo.ErrorMessage = "扣除账户金额时产生了错误,请重试";

                return validateResultInfo;
            }
            #endregion

            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendDetails = new List<EyouSoft.Model.SMSStructure.SendDetailInfo>();

            //发送[移动、联通]内容实际计算费用短信条数
            int mobielFactCount = this.GetSmsTotalCount(sendMessageInfo.SMSContentSendComplete, EyouSoft.Model.SMSStructure.SMSNoType.Mobiel, sendMessageInfo.SendChannel);
            //发送[小灵通]内容实际计算费用短信条数
            int phsFactCount = this.GetSmsTotalCount(sendMessageInfo.SMSContentSendComplete, EyouSoft.Model.SMSStructure.SMSNoType.PHS, sendMessageInfo.SendChannel);            

            #region 调用发送短信接口
            //每次调用发送接口时待发送的手机号码
            StringBuilder waitMobiles = new StringBuilder();
            //每次调用发送接口时的最大发送号码个数
            int waitCanMobilesMax = 100;
            //总的要发送的手机号码个数
            int waitMobileLength = sendMessageInfo.Mobiles.Count;
            int indexStart = 0;
            for (int indexC = 0; indexC < waitMobileLength; indexC++)
            {
                EyouSoft.Model.SMSStructure.SendDetailInfo sendDetailInfo = new EyouSoft.Model.SMSStructure.SendDetailInfo();
                EyouSoft.Model.SMSStructure.AcceptMobileInfo mobile = sendMessageInfo.Mobiles[indexC];

                sendDetailInfo.SendId = Guid.NewGuid().ToString();
                sendDetailInfo.Mobile = mobile.Mobile;
                sendDetailInfo.IsPHS = this.IsPHS(mobile.Mobile);
                sendDetailInfo.IsEncrypt = mobile.IsEncrypt;

                if (!sendDetailInfo.IsPHS)
                {
                    sendDetailInfo.FactCount = mobielFactCount;
                }
                else
                {
                    sendDetailInfo.FactCount = phsFactCount;
                }

                //添加到集合中
                sendDetails.Add(sendDetailInfo);


                waitMobiles.AppendFormat("{0},", mobile.Mobile);


                #region 判断是否开始发送短信
                if ((indexC + 1) % waitCanMobilesMax == 0 || (indexC + 1) == waitMobileLength)
                {
                    int sendResult = 0;
                    string sendMsg = "";


                    try
                    {
                        string sendMessageContent = sendMessageInfo.SMSContentSendComplete;

                        //发送结果返回值 返回int类型的0时成功 返回对应的负数时失败
                        sendResult = sms.SendSms(this.EnterpriseId, waitMobiles.ToString().TrimEnd(",".ToCharArray()), sendMessageInfo.SMSContentSendComplete, sendMessageInfo.SendChannel.UserName, sendMessageInfo.SendChannel.Pw);
                        sendMsg = this.GetServicesState(sendResult);
                    }
                    catch
                    {
                        sendMsg = "超时";
                        sendResult = this.SendTimeOutEventCode;
                    }

                    //要发送的号码清空
                    waitMobiles.Remove(0, waitMobiles.Length);

                    #region 处理发送后的结果(处理当次在批量发送内的所有号码)
                    for (int j = indexStart; j <= indexC; j++)
                    {
                        sendDetails[j].ReturnMsg = sendMsg;
                        sendDetails[j].ReturnResult = sendResult;

                        if (!sendDetails[j].IsPHS)
                        {
                            if (sendResult == this.SendTimeOutEventCode)
                            {
                                validateResultInfo.TimeoutCount = validateResultInfo.TimeoutCount + 1;
                            }
                            else if (sendResult == 0)
                            {
                                validateResultInfo.SuccessCount = validateResultInfo.SuccessCount + 1;
                            }
                            else
                            {
                                validateResultInfo.ErrorCount = validateResultInfo.ErrorCount + 1;
                            }
                        }
                        else
                        {
                            if (sendResult == this.SendTimeOutEventCode)
                            {
                                validateResultInfo.PHSTimeoutCount = validateResultInfo.PHSTimeoutCount + 1;
                            }
                            else if (sendResult == 0)
                            {
                                validateResultInfo.PHSSuccessCount = validateResultInfo.PHSSuccessCount + 1;
                            }
                            else
                            {
                                validateResultInfo.PHSErrorCount = validateResultInfo.PHSErrorCount + 1;
                            }
                        }
                    }
                    #endregion 处理发送后的结果

                    //记录下一批的开始索引号
                    indexStart = indexC + 1;
                }
                #endregion 判断是否开始发送短信
            }
           
            #endregion

            //写入短信发送明细及统计信息，同时更新账户余额
            dal.InsertSendInfo(sendMessageInfo, sendDetails, validateResultInfo);

            //计算发送短信后实际扣除的消费金额(预扣除金额-发送超时的手机或者小灵通短信条数*1个短信的实际条数*单价)
            validateResultInfo.SendFee = validateResultInfo.CountFee - 0.01M * validateResultInfo.TimeoutCount * validateResultInfo.FactCount * sendMessageInfo.SendChannel.PriceOne - 0.01M * validateResultInfo.PHSTimeoutCount * validateResultInfo.PHSFactCount * sendMessageInfo.SendChannel.PriceOne;
            #endregion

            return validateResultInfo;
        }

        /// <summary>
        /// 根据指定条件获取发送短信历史记录
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <param name="startTime">发送开始时间 为空时不做为查询条件</param>
        /// <param name="finishTime">发送截止时间 为空时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendHistorys(int pageSize, int pageIndex, ref int recordCount, string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime)
        {
            return dal.GetSendHistorys(pageSize, pageIndex, ref recordCount, companyId, keyword, sendStatus, startTime, finishTime);
        }

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送号码列表
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendDetails(string totalId, string companyId, int sendStatus)
        {
            return dal.GetSendDetails(totalId, companyId, sendStatus);
        }

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送短信的内容
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public string GetSendContent(string totalId, string companyId)
        {
            return dal.GetSendContent(totalId, companyId);
        }

        /// <summary>
        /// 根据指定条件获取所有发送短信历史记录
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="keyword">关键字 为空时不做为查询条件</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <param name="startTime">发送开始时间 为空时不做为查询条件</param>
        /// <param name="finishTime">发送截止时间 为空时不做为查询条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetAllSendHistorys(string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime)
        {
            return dal.GetAllSendHistorys(companyId, keyword, sendStatus, startTime, finishTime);
        }

        /// <summary>
        /// 发送订单预定短信，返回1成功，其它失败
        /// </summary>
        /// <param name="info">订单短信信息业务实体</param>
        /// <returns></returns>
        public int SendOrderSMS(EyouSoft.Model.NewTourStructure.MOrderSmsInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DSTCompanyId)) return -1;
            if (string.IsNullOrEmpty(info.OrderId)) return -2;

            EyouSoft.Model.SystemStructure.MSysSettingInfo setting = EyouSoft.BLL.SystemStructure.SystemInfo.CreateInstance().GetSysSetting();
            if (setting == null || !setting.OrderSmsIsEnable) return -3;

            EyouSoft.Model.NewTourStructure.MOrderSmsDstInfo dstInfo = EyouSoft.BLL.CompanyStructure.CompanyInfo.CreateInstance().GetOrderSmsDstInfo(info.DSTCompanyId);

            if (dstInfo == null) return -4;
            if (dstInfo.DstPhones == null || dstInfo.DstPhones.Count < 1) return -5;

            if (setting.OrderSmsCompanyTypes == null || !setting.OrderSmsCompanyTypes.Contains(dstInfo.DstCompanyType)) return -6;

            if (info.OrderType == EyouSoft.Model.NewTourStructure.OrderSource.线路散拼订单)
            {
                string s = setting.OrderSmsTemplate;

                //信息替换
                s = s.Replace("[预定公司]", info.BookCompanyName);
                s = s.Replace("[预定联系电话]", info.BookPhone);
                s = s.Replace("[预定出发时间]", info.BookLTime.ToString("yyyy-MM-dd"));
                s = s.Replace("[预定产品]", info.BookName);
                s = s.Replace("[预定数量]", info.BookNumber.ToString());

                //收费和签约公司特有内容
                int startIndex = s.IndexOf('{');
                int endIndex = s.IndexOf('}');
                if (startIndex > -1 && endIndex> -1&&startIndex<endIndex)
                {
                    if (dstInfo.DstCompanyType == EyouSoft.Model.CompanyStructure.CompanyLev.签约商户
                        || dstInfo.DstCompanyType == EyouSoft.Model.CompanyStructure.CompanyLev.推荐商户)
                    {
                        s = s.Replace("{", string.Empty).Replace("}", string.Empty);
                    }
                    else
                    {
                        s = s.Substring(0, startIndex) + s.Substring(endIndex + 1);
                    }
                }

                EyouSoft.Model.SMSStructure.SendMessageInfo message = new EyouSoft.Model.SMSStructure.SendMessageInfo();
                List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> mobiles = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();

                foreach (var item in dstInfo.DstPhones)
                {
                    if (!string.IsNullOrEmpty(item) && EyouSoft.Common.Utility.IsMobile(item))
                    {
                        mobiles.Add(new EyouSoft.Model.SMSStructure.AcceptMobileInfo()
                        {
                            IsEncrypt = false,
                            Mobile = item
                        });
                        break;
                    }
                }

                message.CompanyId = setting.OrderSmsCompanyId;
                message.CompanyName = string.Empty;
                message.Mobiles = mobiles;
                message.SendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[setting.OrderSmsChannelIndex];
                message.SendTime = DateTime.Now;
                message.SendType = EyouSoft.Model.SMSStructure.SendType.直接发送;
                message.SMSContent = s;
                //message.SMSContentSendComplete
                message.SMSType = 0;
                message.UserFullName = string.Empty;
                message.UserId = setting.OrderSmsUserId;

                var sendRet = Send(message);
                if (sendRet.IsSucceed)
                {
                    return 1;
                }
                else
                {
                    return -7;
                }
            }

            return -8;
        }
        #endregion
    }
}
