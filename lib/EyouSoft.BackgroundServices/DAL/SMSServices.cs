using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.BackgroundServices
{
    /// <summary>
    /// 短信服务
    /// </summary>
    /// Author:张志瑜 2010-09-21
    public class SMSServices : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.BackgroundServices.ISMSServices
    {
        private Database _dataBase = null;
        private string SQL_SMS_SendPlan_SELECT = "SELECT [ID],[CompanyID],[CompanyName],[UserID],[ContactName],[SMSType],[SMSContent],[MobileList],[IssueTime],[SendTime],[SendChannel],[SendState],[EncryptMobiles] FROM SMS_SendPlan WHERE [SendState]='0' AND SendTime<=getdate() ORDER BY [SendTime]";
        private string SQL_SMS_SendPlan_UPDATE = "UPDATE SMS_SendPlan SET SendState=@SendState,StateText=@StateText,RealSendTime=getdate() WHERE ID=@ID";

        public SMSServices()
        {
            this._dataBase = base.SMSStore;
        }

        #region private members
        /// <summary>
        /// 根据接收短信手机号码字符串获取手机号码集合
        /// </summary>
        /// <param name="mobiles">手机号码字符串 ","间隔</param>
        /// <param name="isEncrypt">是否在显示时加密</param>
        /// <returns></returns>
        private List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> GetAcceptMobiles(string mobiles, bool isEncrypt)
        {           
            List<EyouSoft.Model.SMSStructure.AcceptMobileInfo> list = new List<EyouSoft.Model.SMSStructure.AcceptMobileInfo>();
            if (string.IsNullOrEmpty(mobiles)) return list;
            
            string[] arr = mobiles.Split(',');

            foreach (string mobile in arr)
                list.Add(new EyouSoft.Model.SMSStructure.AcceptMobileInfo() { IsEncrypt = isEncrypt, Mobile = mobile });

            return list;
        }
        #endregion

        #region SMSServices 成员

        /// <summary>
        /// 获得要发送的短信
        /// </summary>
        /// <returns></returns>
        public Queue<EyouSoft.Model.SMSStructure.SendPlanInfo> Get()
        {
            Queue<EyouSoft.Model.SMSStructure.SendPlanInfo> queue = new Queue<EyouSoft.Model.SMSStructure.SendPlanInfo>();

            DbCommand dc = this._dataBase.GetSqlStringCommand(SQL_SMS_SendPlan_SELECT);
            EyouSoft.Model.SMSStructure.SMSChannelList smschannel = new EyouSoft.Model.SMSStructure.SMSChannelList();
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._dataBase))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.SendPlanInfo item = new EyouSoft.Model.SMSStructure.SendPlanInfo();
                    item.PlanId = rdr.GetString(rdr.GetOrdinal("ID"));
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    item.CompanyName = rdr.IsDBNull(rdr.GetOrdinal("CompanyName")) ? "" : rdr.GetString(rdr.GetOrdinal("CompanyName"));
                    item.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                    item.ContactName = rdr.IsDBNull(rdr.GetOrdinal("ContactName")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactName"));
                    item.SMSType = rdr.GetInt32(rdr.GetOrdinal("SMSType"));
                    item.SMSContent = rdr.IsDBNull(rdr.GetOrdinal("SMSContent")) ? "" : rdr.GetString(rdr.GetOrdinal("SMSContent"));
                    string mobile = rdr.IsDBNull(rdr.GetOrdinal("MobileList")) ? "" : rdr.GetString(rdr.GetOrdinal("MobileList"));
                    string encryptMobiles = rdr.IsDBNull(rdr.GetOrdinal("EncryptMobiles")) ? "" : rdr.GetString(rdr.GetOrdinal("EncryptMobiles"));

                    //显示时不加密手机号
                    item.Mobiles = this.GetAcceptMobiles(mobile, false);
                    //显示时要加密手机号
                    item.Mobiles.AddRange(this.GetAcceptMobiles(encryptMobiles, true));

                    item.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    item.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    item.SendChannel = smschannel[rdr.GetInt32(rdr.GetOrdinal("SendChannel"))];

                    queue.Enqueue(item);
                }
            }

            return queue;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sms">待发送的短信实体</param>
        public void Send(EyouSoft.Model.SMSStructure.SendPlanInfo sms)
        {
            EyouSoft.BackgroundServices.smsSoap.SMSChannel smsChannel = new EyouSoft.BackgroundServices.smsSoap.SMSChannel();
            smsChannel.ChannelName = sms.SendChannel.ChannelName;
            smsChannel.Index = sms.SendChannel.Index;
            smsChannel.IsLong = sms.SendChannel.IsLong;
            smsChannel.PriceOne = sms.SendChannel.PriceOne;
            smsChannel.Pw = sms.SendChannel.Pw;
            smsChannel.UserName = sms.SendChannel.UserName;

            EyouSoft.BackgroundServices.smsSoap.SendMessageInfo sendmsg = new EyouSoft.BackgroundServices.smsSoap.SendMessageInfo();
            EyouSoft.BackgroundServices.smsSoap.AcceptMobileInfo[] MobilesArr = new EyouSoft.BackgroundServices.smsSoap.AcceptMobileInfo[sms.Mobiles.Count];
            for (int i = 0; i < sms.Mobiles.Count; i++)
            {
                MobilesArr[i] = new EyouSoft.BackgroundServices.smsSoap.AcceptMobileInfo();
                MobilesArr[i].Mobile = sms.Mobiles[i].Mobile;
                MobilesArr[i].IsEncrypt = sms.Mobiles[i].IsEncrypt;
            }
            sendmsg.Mobiles = MobilesArr;
            sendmsg.CompanyId = sms.CompanyId;
            sendmsg.CompanyName = sms.CompanyName;
            sendmsg.SendChannel = smsChannel;
            sendmsg.SendTime = sms.SendTime;
            sendmsg.SendType = EyouSoft.BackgroundServices.smsSoap.SendType.直接发送;
            sendmsg.SMSContent = sms.SMSContent;
            sendmsg.SMSType = sms.SMSType;
            sendmsg.UserFullName = sms.ContactName;
            sendmsg.UserId = sms.UserId;

            ////using (System.Transactions.TransactionScope tran = new System.Transactions.TransactionScope())
            ////{            
            //定义api
            EyouSoft.BackgroundServices.smsSoap.SmsAPI api = new EyouSoft.BackgroundServices.smsSoap.SmsAPI();
            //定义认证header      
            EyouSoft.BackgroundServices.smsSoap.APISoapHeader header = new EyouSoft.BackgroundServices.smsSoap.APISoapHeader();
            //header赋值,TMIS_APIKey为配置文件中配置的api调用密钥
            header.SecretKey = System.Configuration.ConfigurationManager.AppSettings.Get("TMIS_APIKey");
            api.APISoapHeaderValue = header;
            
            //发送短信
            EyouSoft.BackgroundServices.smsSoap.SendResultInfo result = api.Send(sendmsg);
            sendmsg = null;
            char SendState = '2';
            string error = "";
            if (result.IsSucceed)  //发送成功
                SendState = '1';
            else
                error = result.ErrorMessage;

            DbCommand dc = this._dataBase.GetSqlStringCommand(SQL_SMS_SendPlan_UPDATE);
            this._dataBase.AddInParameter(dc, "SendState", DbType.AnsiStringFixedLength, SendState);
            this._dataBase.AddInParameter(dc, "StateText", DbType.String, error);
            this._dataBase.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, sms.PlanId);

            //执行
            DbHelper.ExecuteSql(dc, this._dataBase);

            //    result = null;
            //    //tran.Complete();
            ////}            
        }

        #endregion SMSServices 成员
    }
}
