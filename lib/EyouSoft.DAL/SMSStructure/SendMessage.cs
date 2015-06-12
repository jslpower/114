using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SMSStructure
{
    /// <summary>
    /// 短信中心-发送短信数据访问类
    /// </summary>
    /// Author:汪奇志 2010-06-10
    public class SendMessage:DALBase,EyouSoft.IDAL.SMSStructure.ISendMessage
    {
        #region static constants
        //static constants
        private const string SQL_SELECT_GETSENDCONTENT = "SELECT [SMSContent] FROM [SMS_SendTotal] WHERE [Id]=@SENDTOTALID AND [CompanyId]=@COMPANYID ";
        private const string SQL_INSERT_InsertSendPlan = "INSERT INTO [SMS_SendPlan]([ID],[CompanyID],[CompanyName],[UserID],[ContactName],[SMSType],[SMSContent],[MobileList],[IssueTime],[SendTime],[SendChannel],[EncryptMobiles]) VALUES(@ID,@CompanyID,@CompanyName,@UserID,@ContactName,@SMSType,@SMSContent,@MobileList,@IssueTime,@SendTime,@SendChannel,@EncryptMobiles)";
        /// <summary>
        /// 发送短信超时时异常编号
        /// </summary>
        private const int SendTimeOutEventCode = -2147483646;
        #endregion static constants

        #region private member
        /// <summary>
        /// 创建发送发送到的手机XML数据
        /// </summary>
        /// <param name="sendDetailsInfo"></param>
        /// <returns></returns>
        private string CreateSendMessageMobilesXML(IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendDetailsInfo)
        {
            StringBuilder xmlInfo = new StringBuilder();

            xmlInfo.Append("<ROOT>");

            foreach (EyouSoft.Model.SMSStructure.SendDetailInfo sendDetailInfo in sendDetailsInfo)
            {
                xmlInfo.AppendFormat("<MobileInfo Number=\"{0}\" ReturnResult=\"{1}\" ReturnMsg=\"{2}\" FactCount=\"{3}\" GUID=\"{4}\" IsEncrypt=\"{5}\" />"
                    , sendDetailInfo.Mobile
                    , sendDetailInfo.ReturnResult.ToString()
                    , sendDetailInfo.ReturnMsg
                    , sendDetailInfo.FactCount
                    , sendDetailInfo.SendId
                    , sendDetailInfo.IsEncrypt ? "1" : "0");
            }

            xmlInfo.Append("</ROOT>");

            return xmlInfo.ToString();
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 写入短信发送明细及统计信息，并更新账户余额
        /// </summary>
        /// <param name="sendMessageInfo">发送短信提交的业务实体</param>
        /// <param name="sendDetailsInfo">短信发送明细</param>
        /// <param name="sendResultInfo">发送短信/验证发送结果业务实体</param>
        /// <returns></returns>
        public virtual bool InsertSendInfo(EyouSoft.Model.SMSStructure.SendMessageInfo sendMessageInfo, IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendDetailsInfo, EyouSoft.Model.SMSStructure.SendResultInfo sendResultInfo)
        {
            DbCommand cmd = base.SMSStore.GetStoredProcCommand("proc_SMS_InsertSendMessageInfo");
            base.SMSStore.AddInParameter(cmd, "SendTotalId", DbType.String, sendResultInfo.SendTotalId);
            base.SMSStore.AddInParameter(cmd, "CompanyId", DbType.String, sendMessageInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "CompanyName", DbType.String, sendMessageInfo.CompanyName);
            base.SMSStore.AddInParameter(cmd, "UserId", DbType.String, sendMessageInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "UserFullName", DbType.String, sendMessageInfo.UserFullName);
            base.SMSStore.AddInParameter(cmd, "SMSType", DbType.Int32, sendMessageInfo.SMSType);
            base.SMSStore.AddInParameter(cmd, "SMSContent", DbType.String, sendMessageInfo.SMSContent);
            base.SMSStore.AddInParameter(cmd, "UseMoeny", DbType.Decimal, 0.01M * sendMessageInfo.SendChannel.PriceOne);
            base.SMSStore.AddInParameter(cmd, "SuccessCount", DbType.Int32, sendResultInfo.SuccessCount);
            base.SMSStore.AddInParameter(cmd, "ErrorCount", DbType.Int32, sendResultInfo.ErrorCount);
            base.SMSStore.AddInParameter(cmd, "TimeoutCount", DbType.Int32, sendResultInfo.TimeoutCount);
            base.SMSStore.AddInParameter(cmd, "FactCount", DbType.Int32, sendResultInfo.FactCount);
            base.SMSStore.AddInParameter(cmd, "TempFeeTakeId", DbType.String, sendResultInfo.TempFeeTakeId);
            base.SMSStore.AddInParameter(cmd, "SendMessageTime", DbType.DateTime, sendMessageInfo.SendTime);
            base.SMSStore.AddInParameter(cmd, "Mobiles", DbType.String, this.CreateSendMessageMobilesXML(sendDetailsInfo));
            base.SMSStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            base.SMSStore.AddInParameter(cmd, "PHSSuccessCount", DbType.Int32, sendResultInfo.PHSSuccessCount);
            base.SMSStore.AddInParameter(cmd, "PHSErrorCount", DbType.Int32, sendResultInfo.PHSErrorCount);
            base.SMSStore.AddInParameter(cmd, "PHSTimeoutCount", DbType.Int32, sendResultInfo.PHSTimeoutCount);
            base.SMSStore.AddInParameter(cmd, "PHSFactCount", DbType.Int32, sendResultInfo.PHSFactCount);
            base.SMSStore.AddInParameter(cmd, "SendChannel", DbType.Int32, sendMessageInfo.SendChannel.Index);

            DbHelper.RunProcedure(cmd, base.SMSStore);

            return Convert.ToInt32(base.SMSStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
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
        public virtual IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendHistorys(int pageSize, int pageIndex, ref int recordCount, string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime)
        {
            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendHistorys = new List<EyouSoft.Model.SMSStructure.SendDetailInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "SMS_SendDetail";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " ID, CompanyID, UserID, SendTotalID, SMSType, MobileNumber, SMSContent, SendTime, ReturnResult, ReturnMsg, UseMoeny, SMSSplitCount,  IssueTime,IsEncrypt";

            cmdQuery.AppendFormat(" CompanyID='{0}' ", companyId.ToString());

            if (!string.IsNullOrEmpty(keyword))
            {
                cmdQuery.AppendFormat(" AND (SMSContent LIKE '%{0}%') ", keyword);
            }

            switch (sendStatus)
            {
                case 0:
                    cmdQuery.AppendFormat(" AND(ReturnResult>{0}) ", SendTimeOutEventCode);
                    break;
                case 1:
                    cmdQuery.Append(" AND(ReturnResult=0) ");
                    break;
                case 2:
                    cmdQuery.AppendFormat(" AND(ReturnResult>{0}) AND (ReturnResult<0) ", SendTimeOutEventCode);
                    break;
                default:
                    cmdQuery.AppendFormat(" AND(ReturnResult>{0})  ", SendTimeOutEventCode);
                    break;
            }

            if (startTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND(SendTime>='{0}') ", startTime.Value);
            }

            if (finishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND(SendTime<'{0}') ", finishTime.Value);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.SendDetailInfo sendDetailInfo = new EyouSoft.Model.SMSStructure.SendDetailInfo();

                    sendDetailInfo.SendId = rdr.GetString(rdr.GetOrdinal("ID"));
                    sendDetailInfo.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    sendDetailInfo.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                    sendDetailInfo.SendTotalId = rdr.GetString(rdr.GetOrdinal("SendTotalID"));
                    sendDetailInfo.SMSType = rdr.GetInt32(rdr.GetOrdinal("SMSType"));
                    sendDetailInfo.Mobile = rdr["MobileNumber"].ToString();
                    sendDetailInfo.SMSContent = rdr["SMSContent"].ToString();
                    sendDetailInfo.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    sendDetailInfo.ReturnResult = rdr.GetInt32(rdr.GetOrdinal("ReturnResult"));
                    sendDetailInfo.ReturnMsg = rdr["ReturnMsg"].ToString();
                    sendDetailInfo.UseMoeny = rdr.GetDecimal(rdr.GetOrdinal("UseMoeny"));
                    sendDetailInfo.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    sendDetailInfo.IsEncrypt = rdr.GetString(rdr.GetOrdinal("IsEncrypt")) == "1" ? true : false;

                    sendHistorys.Add(sendDetailInfo);
                }
            }

            return sendHistorys;
        }

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送号码列表
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="sendStatus">发送状态 0:所有 1:成功 2:失败</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetSendDetails(string totalId, string companyId, int sendStatus)
        {
            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendDetails = new List<EyouSoft.Model.SMSStructure.SendDetailInfo>();
            StringBuilder cmdText = new StringBuilder();

            cmdText.Append(" SELECT [ID], [CompanyID], [UserID],[MobileNumber],[ReturnResult],[IsEncrypt] FROM  [SMS_SendDetail] WHERE [CompanyId]=@COMPANYID AND [SendTotalID]=@SENDTOTALID ");

            switch (sendStatus)
            {
                case 0:
                    cmdText.AppendFormat(" AND(ReturnResult>{0})  ", SendTimeOutEventCode);
                    break;
                case 1:
                    cmdText.Append(" AND(ReturnResult=0) ");
                    break;
                case 2:
                    cmdText.AppendFormat(" AND(ReturnResult>{0}) AND (ReturnResult<0) ", SendTimeOutEventCode);
                    break;
                default:
                    cmdText.AppendFormat(" AND(ReturnResult>{0})  ", SendTimeOutEventCode);
                    break;
            }

            DbCommand cmd = base.SMSStore.GetSqlStringCommand(cmdText.ToString());
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);
            base.SMSStore.AddInParameter(cmd, "SENDTOTALID", DbType.AnsiStringFixedLength, totalId);


            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,base.SMSStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.SendDetailInfo sendDetailInfo = new EyouSoft.Model.SMSStructure.SendDetailInfo();

                    sendDetailInfo.SendId = rdr.GetString(rdr.GetOrdinal("ID"));
                    sendDetailInfo.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    sendDetailInfo.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                    sendDetailInfo.Mobile = rdr["MobileNumber"].ToString();
                    sendDetailInfo.ReturnResult = rdr.GetInt32(rdr.GetOrdinal("ReturnResult"));
                    sendDetailInfo.IsEncrypt = rdr.GetString(rdr.GetOrdinal("IsEncrypt")) == "1" ? true : false;

                    sendDetails.Add(sendDetailInfo);
                }
            }

            return sendDetails;
        }

        /// <summary>
        /// 根据指定的短信发送统计编号获取发送短信的内容
        /// </summary>
        /// <param name="totalId">短信发送统计编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual string GetSendContent(string totalId, string companyId)
        {
            string content = string.Empty;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETSENDCONTENT);
            base.SMSStore.AddInParameter(cmd, "SENDTOTALID", DbType.AnsiStringFixedLength, totalId);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    content = rdr[0].ToString();
                }
            }

            return content;
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
        public virtual IList<EyouSoft.Model.SMSStructure.SendDetailInfo> GetAllSendHistorys(string companyId, string keyword, int sendStatus, DateTime? startTime, DateTime? finishTime)
        {
            IList<EyouSoft.Model.SMSStructure.SendDetailInfo> sendHistorys = new List<EyouSoft.Model.SMSStructure.SendDetailInfo>();

            StringBuilder cmdText = new StringBuilder();
            cmdText.Append("SELECT  ID, CompanyID, UserID, SendTotalID, SMSType, MobileNumber, SMSContent, SendTime, ReturnResult, ReturnMsg, UseMoeny, SMSSplitCount,  IssueTime,IsEncrypt FROM SMS_SendDetail ");
            cmdText.Append(" WHERE CompanyID=@CompanyId ");

            if (!string.IsNullOrEmpty(keyword))
            {
                cmdText.AppendFormat(" AND (SMSContent LIKE '%{0}%') ", keyword);
            }

            switch (sendStatus)
            {
                case 0:
                    cmdText.AppendFormat(" AND(ReturnResult>{0}) ", SendTimeOutEventCode);
                    break;
                case 1:
                    cmdText.Append(" AND(ReturnResult=0) ");
                    break;
                case 2:
                    cmdText.AppendFormat(" AND(ReturnResult>{0}) AND (ReturnResult<0) ",SendTimeOutEventCode);
                    break;
                default:
                    cmdText.AppendFormat(" AND(ReturnResult>{0}) ", SendTimeOutEventCode);
                    break;
            }

            if (startTime.HasValue)
            {
                cmdText.AppendFormat(" AND(SendTime>='{0}') ", startTime.Value);
            }

            if (finishTime.HasValue)
            {
                cmdText.AppendFormat(" AND(SendTime<'{0}') ", finishTime.Value);
            }

            cmdText.Append(" ORDER BY IssueTime DESC ");

            DbCommand cmd=base.SMSStore.GetSqlStringCommand(cmdText.ToString());
            base.SMSStore.AddInParameter(cmd,"CompanyId",DbType.String,companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,base.SMSStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.SendDetailInfo sendDetailInfo = new EyouSoft.Model.SMSStructure.SendDetailInfo();

                    sendDetailInfo.SendId = rdr.GetString(rdr.GetOrdinal("ID"));
                    sendDetailInfo.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    sendDetailInfo.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                    sendDetailInfo.SendTotalId = rdr.GetString(rdr.GetOrdinal("SendTotalID"));
                    sendDetailInfo.SMSType = rdr.GetInt32(rdr.GetOrdinal("SMSType"));
                    sendDetailInfo.Mobile = rdr["MobileNumber"].ToString();
                    sendDetailInfo.SMSContent = rdr["SMSContent"].ToString();
                    sendDetailInfo.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    sendDetailInfo.ReturnResult = rdr.GetInt32(rdr.GetOrdinal("ReturnResult"));
                    sendDetailInfo.ReturnMsg = rdr["ReturnMsg"].ToString();
                    sendDetailInfo.UseMoeny = rdr.GetDecimal(rdr.GetOrdinal("UseMoeny"));
                    sendDetailInfo.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    sendDetailInfo.IsEncrypt = rdr.GetString(rdr.GetOrdinal("IsEncrypt")) == "1" ? true : false;

                    sendHistorys.Add(sendDetailInfo);
                }
            }

            return sendHistorys;
        }

        /// <summary>
        /// 写入定时发送短信任务计划
        /// </summary>
        /// <param name="planInfo">任务计划信息业务实体</param>
        /// <returns></returns>
        public virtual bool InsertSendPlan(EyouSoft.Model.SMSStructure.SendPlanInfo planInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_InsertSendPlan);

            StringBuilder mobiles = new StringBuilder();
            StringBuilder encryptMobiles = new StringBuilder();

            if (planInfo.Mobiles != null && planInfo.Mobiles.Count > 0)
            {
                /*if (planInfo.Mobiles[0].IsEncrypt)
                {
                    encryptMobiles.Append(planInfo.Mobiles[0].Mobile);
                }
                else
                {
                    mobiles.Append(planInfo.Mobiles[0].Mobile);
                }
                
                for (int i = 1; i < planInfo.Mobiles.Count; i++)
                {
                    if (planInfo.Mobiles[i].IsEncrypt)
                    {
                        encryptMobiles.AppendFormat(",{0}", planInfo.Mobiles[i].Mobile);
                    }
                    else
                    {
                        mobiles.AppendFormat(",{0}", planInfo.Mobiles[i].Mobile);
                    }
                    
                }*/

                foreach (var mobile in planInfo.Mobiles)
                {
                    if (mobile.IsEncrypt)
                    {
                        encryptMobiles.AppendFormat("{0},", mobile.Mobile);
                    }
                    else
                    {
                        mobiles.AppendFormat("{0},", mobile.Mobile);
                    }
                }
            }

            base.SMSStore.AddInParameter(cmd, "ID", DbType.AnsiStringFixedLength, planInfo.PlanId);
            base.SMSStore.AddInParameter(cmd, "CompanyID", DbType.AnsiStringFixedLength, planInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "CompanyName", DbType.String, planInfo.CompanyName);
            base.SMSStore.AddInParameter(cmd, "UserID", DbType.AnsiStringFixedLength, planInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "ContactName", DbType.String, planInfo.ContactName);
            base.SMSStore.AddInParameter(cmd, "SMSType", DbType.Int32, planInfo.SMSType);
            base.SMSStore.AddInParameter(cmd, "SMSContent", DbType.String, planInfo.SMSContent);
            base.SMSStore.AddInParameter(cmd, "MobileList", DbType.String, mobiles.ToString().Trim(','));
            base.SMSStore.AddInParameter(cmd, "IssueTime", DbType.DateTime, planInfo.IssueTime);
            base.SMSStore.AddInParameter(cmd, "SendTime", DbType.DateTime, planInfo.SendTime);
            base.SMSStore.AddInParameter(cmd, "SendChannel", DbType.Int32, planInfo.SendChannel.Index);
            base.SMSStore.AddInParameter(cmd, "EncryptMobiles", DbType.String, encryptMobiles.ToString().Trim(','));

            return DbHelper.ExecuteSql(cmd, base.SMSStore) == 1 ? true : false;
        }
        #endregion
    }
}
