using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.Unity;
using EyouSoft.Common.Mail;

namespace EyouSoft.DAL.BackgroundServices
{
    public class MQServices : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.BackgroundServices.IMQServices
    {

        private EyouSoft.IDAL.ToolStructure.IMsgTipRecord idal ;
        private EyouSoft.IDAL.CompanyStructure.ICompanyInfo cIdal ;
        private EyouSoft.IDAL.CompanyStructure.ICompanyUser uIdal ;
        private EyouSoft.IDAL.MQStructure.IIMMember mIdal;
        SysMailMessage ESM = new SysMailMessage();
        EyouSoft.Config.EmailConfigInfo config = null;
        string EmailTemplate = string.Empty;

        #region IMQServices 成员

        private IUnityContainer container;

        public MQServices(IUnityContainer container)
        {
            this.container = container;
            idal = container.Resolve<EyouSoft.IDAL.ToolStructure.IMsgTipRecord>();
            cIdal = container.Resolve<EyouSoft.IDAL.CompanyStructure.ICompanyInfo>();
            uIdal = container.Resolve<EyouSoft.IDAL.CompanyStructure.ICompanyUser>();
            mIdal = container.Resolve<EyouSoft.IDAL.MQStructure.IIMMember>();
            config = EyouSoft.Config.EmailConfigs.GetConfig();
            EmailTemplate = System.IO.File.ReadAllText(EyouSoft.Common.Utility.GetMapPath("config/email.txt"), System.Text.Encoding.GetEncoding("gb2312"));
        }
        /// <summary>
        /// 发送短线
        /// </summary>
        /// <param name="srcMq">发送人</param>
        /// <param name="dstMq">接收人</param>
        public void SendMessage(int srcMq, int dstMq)
        {
            Model.CompanyStructure.CompanyUser ToCompany = uIdal.GetModel(dstMq);
            Model.CompanyStructure.CompanyAndUserInfo fromCompany = GetUserCompanyModel(srcMq);
            if (ToCompany != null && fromCompany != null)
            {
                if (IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType.MQNoReadMsg, EyouSoft.Model.ToolStructure.MsgSendWay.SMS, dstMq.ToString(), ToCompany.CityId))
                {
                    SendMail(ToCompany.ContactInfo.Email, ToCompany.UserName, dstMq.ToString(), ToCompany.PassWordInfo.NoEncryptPassword);
                    SendSms(ToCompany.ContactInfo.Mobile, fromCompany.Company.CompanyName, fromCompany.User.ContactInfo.ContactName, ToCompany.UserName);
                    EyouSoft.Model.ToolStructure.MsgTipRecord tipModel = new EyouSoft.Model.ToolStructure.MsgTipRecord();
                    tipModel.Email = string.Empty;
                    tipModel.FromMQID = srcMq.ToString();
                    tipModel.ToMQID = dstMq.ToString();
                    tipModel.Mobile = ToCompany.ContactInfo.Mobile;
                    tipModel.MsgType = EyouSoft.Model.ToolStructure.MsgType.MQNoReadMsg;
                    tipModel.SendWay = EyouSoft.Model.ToolStructure.MsgSendWay.SMS;
                    idal.Add(tipModel);
                }
            }
        }
        /// <summary>
        /// 获得当天最近的MQ消息
        /// </summary>
        /// <param name="count">要获取的MQ消息条数,若为0,则表示查询所有</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.MQStructure.IMMessage> GetTodayLastMessage(int count)
        {
            IList<EyouSoft.Model.MQStructure.IMMessage> list = new List<EyouSoft.Model.MQStructure.IMMessage>();
            string MYSQL_SELECT_TodayMsg = "SELECT num,dst,src,timenow FROM user_message where src>0 AND DATEDIFF(timenow,CURDATE())=0 group by dst";
            if (count > 0)
                MYSQL_SELECT_TodayMsg = " limit " + count;
            DbCommand dc = base.MySQLStore.GetSqlStringCommand(MYSQL_SELECT_TodayMsg);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.MQStructure.IMMessage model = new EyouSoft.Model.MQStructure.IMMessage();
                    model.num = dr.GetInt32(dr.GetOrdinal("num"));
                    model.dst = dr.GetInt32(dr.GetOrdinal("dst"));
                    model.src = dr.GetInt32(dr.GetOrdinal("src"));
                    //model.message = dr.IsDBNull(dr.GetOrdinal("content")) ? "" : dr.GetString(dr.GetOrdinal("content"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("timenow"));
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 私有
        /// <summary>
        /// 根据用户MQID获得公司以及当前用户明细信息实体类
        /// </summary>
        /// <param name="userMqId">用户MQID</param>
        /// <returns></returns>
        private EyouSoft.Model.CompanyStructure.CompanyAndUserInfo GetUserCompanyModel(int userMqId)
        {
            if (userMqId <= 0)
                return null;
            EyouSoft.Model.CompanyStructure.CompanyAndUserInfo modelAll = null;
            string companyid = "";
            EyouSoft.Model.CompanyStructure.CompanyUser user = uIdal.GetModel(userMqId);
            if (user != null)
            {
                modelAll = new EyouSoft.Model.CompanyStructure.CompanyAndUserInfo();
                companyid = user.CompanyID;
                modelAll.User = user;
            }
            user = null;
            if (string.IsNullOrEmpty(companyid))
                return null;

            EyouSoft.Model.CompanyStructure.CompanyDetailInfo company = cIdal.GetModel(companyid);
            if (company != null)
            {
                modelAll.Company = company;
            }
            company = null;

            return modelAll;
        }
        /// <summary>
        /// 是否发送消息提醒
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="sendWay">消息发送途径</param>
        /// <param name="mqID">接收消息用户MQID</param>
        /// <param name="cityId">当前用户所在城市</param>
        /// <returns></returns>
        private bool IsSendMsgTip(EyouSoft.Model.ToolStructure.MsgType msgType, EyouSoft.Model.ToolStructure.MsgSendWay sendWay, string mqID, int cityId)
        {
            bool issend = false;
            IList<int> cityidlist = new List<int>();
            DateTime? endtime = null;

            string MsgTipConfig = System.Configuration.ConfigurationManager.AppSettings["MsgTipConfig"];
            if (!string.IsNullOrEmpty(MsgTipConfig))
            {
                string[] valueArr = MsgTipConfig.Split("|".ToCharArray());
                if (valueArr != null && valueArr.Length > 0)
                {
                    foreach (string item in valueArr)
                    {
                        string[] val = item.Split(":".ToCharArray());
                        if (val != null && val.Length > 0)
                        {
                            switch (val[0])
                            {
                                case "city":
                                    string[] citys = val[1].Split(",".ToCharArray());
                                    foreach (string city in citys)
                                        cityidlist.Add(Convert.ToInt32(city));
                                    break;
                                case "endtime":
                                    endtime = Convert.ToDateTime(val[1]);
                                    break;
                            }
                        }
                    }
                }

                //判断时间有无到期,以及是否在要发送的城市内
                if (endtime.HasValue && endtime.Value.CompareTo(DateTime.Now) > 0)
                {
                    int smsSendCount = 0;   //短信发送次数
                    int smsSendCountMax = 1;   //短信当日可以发送的次数
                    if (sendWay == EyouSoft.Model.ToolStructure.MsgSendWay.SMS)
                    {
                        if (idal.GetSmsRemain() < 1) //当期发完
                            return false;
                        if (idal.GetTodayCount() > 800)//每天800
                            return false;
                    }
                    switch (msgType)
                    {
                        case EyouSoft.Model.ToolStructure.MsgType.RegPass:
                            switch (sendWay)
                            {
                                case EyouSoft.Model.ToolStructure.MsgSendWay.Email:
                                    issend = true;
                                    break;
                                case EyouSoft.Model.ToolStructure.MsgSendWay.SMS:
                                    if (cityidlist.Contains(cityId))
                                        issend = true;
                                    break;
                            }
                            break;
                        case EyouSoft.Model.ToolStructure.MsgType.MQNoReadMsg:
                        case EyouSoft.Model.ToolStructure.MsgType.AddFriend:
                        case EyouSoft.Model.ToolStructure.MsgType.NewOrder:
                            if (sendWay == EyouSoft.Model.ToolStructure.MsgSendWay.SMS && !cityidlist.Contains(cityId))
                                break;

                            //判断是否在线
                            Model.MQStructure.IMMember model_member = mIdal.GetModel(Convert.ToInt32(mqID));
                            bool isonline = false;
                            if (model_member.im_status > 11 && !model_member.IsAutoLogin)
                                isonline = true;
                            model_member = null;

                            //判断可发送的短信数量
                            //if (msgType == EyouSoft.Model.ToolStructure.MsgType.NewOrder)
                            //    smsSendCountMax = 2;
                            //else
                            //    smsSendCountMax = 1;

                            //判断最终结果
                            if (!isonline)
                            {
                                switch (sendWay)
                                {
                                    case EyouSoft.Model.ToolStructure.MsgSendWay.Email:
                                        issend = true;
                                        break;
                                    case EyouSoft.Model.ToolStructure.MsgSendWay.SMS:
                                        if (!idal.IsValid(mqID)) //MQ是否被连续发送冻结
                                        {
                                            if (idal.IsValid(mqID, 3))
                                            {
                                                //连续3天发送冻结
                                                idal.Add(mqID, DateTime.Now.AddDays(5));
                                                return false;
                                            }
                                            smsSendCount = idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.MQNoReadMsg, sendWay, mqID) +
                                                idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.AddFriend, sendWay, mqID) +
                                                idal.GetTodayCount(EyouSoft.Model.ToolStructure.MsgType.NewOrder, sendWay, mqID);
                                            if (smsSendCount < smsSendCountMax)
                                                issend = true;
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            return issend;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="fromCompanyName">发送公司名称</param>
        /// <param name="fromUserName">发送人</param>
        private void SendSms(string mobile, string fromCompanyName, string fromUserName, string toUsername)
        {
            string templateStr = "同业MQ提示:{0}" + fromUserName + "通过MQ给您留言啦,MQ上有商机,机会就在现在!留言为您保留7天,您的账号是" + toUsername;
            int MsgLength = templateStr.Length - 3;
            if (string.Format(templateStr, fromCompanyName).Length > 70)
            {
                templateStr = string.Format(templateStr, fromCompanyName.Substring(0, 70 - MsgLength - 1));
            }
            else
            {
                templateStr = string.Format(templateStr, fromCompanyName);
            }
            string EnterpriseId = string.Empty;
            EyouSoft.Model.SMSStructure.SMSChannel sendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[1];
            EyouSoft.BackgroundServices.VoSmsServices.Service sms = new EyouSoft.BackgroundServices.VoSmsServices.Service();
            sms.Timeout = 1000;
            sms.SendSms(EnterpriseId, mobile, templateStr, sendChannel.UserName, sendChannel.Pw);
        }

        private void SendMail(string email, string username, string mq, string password)
        {
            if (string.IsNullOrEmpty(email))
                return;
            //ESM..Priority = "Normal";
            ESM.AddRecipient(email);
            ESM.MailDomainPort = config.Port;
            ESM.From = config.Sysemail;//设定发件人地址(必须填写)
            ESM.FromName = config.FromName;
            ESM.Html = true;//设定正文是否HTML格式。
            ESM.Subject = "赶紧登陆同业MQ吧，您有一条留言消息，客户等着您报价呢！";
            ESM.Body = "" + EmailTemplate.Replace("{username}", username).Replace("{passwd}", password) + "";
            ESM.MailDomain = config.Smtp;
            //也可将将SMTP信息一次设置完成。写成
            ESM.MailServerUserName = config.Username;
            ESM.MailServerPassWord = config.Password;
            ESM.Send();
        }
        #endregion

    }
}
