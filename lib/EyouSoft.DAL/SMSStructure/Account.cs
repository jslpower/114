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
    /// 短信中心-用户账户信息数据访问类
    /// </summary>
    /// Author:汪奇志 2010-06-10 
    public class Account:DALBase,EyouSoft.IDAL.SMSStructure.IAccount
    {
        #region static constants
        //static constants
        private const string SQL_SELECT_GETACCOUNTINFO = "SELECT [ID],[CompanyID],[AccountMoney] FROM [SMS_Account] WHERE [CompanyId]=@COMPANYID";
        private const string SQL_SELECT_GETACCOUNTMONEY = "SELECT [AccountMoney] FROM [SMS_Account] WHERE [CompanyId]=@COMPANYID";
        private const string SQL_INSERT_PAYMONEY = "INSERT INTO [SMS_PayMoney] ([ID],[CompanyID] ,[CompanyName] ,[OperatorID] ,[OperatorName] ,[PayMoney] ,[PayTime], [PaySMSNumber], [OperatorMobile], [OperatorMQId], [OperatorTel]) VALUES(@ID,@COMPANYID ,@COMPANYNAME ,@USERID ,@USERFULLNAME ,@PAYMONEY ,@PAYTIME, @PaySMSNumber, @OperatorMobile, @OperatorMQId, @OperatorTel)";
        private const string SQL_SELECT_GETACCOUNTEXPENSECOLLECTINFO = "SELECT COUNT(*) AS [SentMessageCount],(SELECT SUM([UseMoeny]) FROM [SMS_SendTotal] WHERE [CompanyId]=@COMPANYID) AS [ExpenseAmount] FROM [SMS_SendDetail] WHERE [CompanyId]=@COMPANYID";
        private const string SQL_SELECT_GETACCOUNTSMSNUMBER = "SELECT [AccountSMSNumber] FROM [SMS_Account] WHERE [CompanyId]=@COMPANYID";
        private const string SQL_SELECT_HASNOCHECKPAY = "SELECT COUNT(*) FROM SMS_PayMoney WHERE CompanyID=@CompanyID AND IsChecked=0";
        private const string SQL_DELETE_NoPassCheckPayMoney = "DELETE FROM SMS_PayMoney WHERE ID=@PayMoneyId AND IsChecked<>1";
        private const string SQL_INSERT_SetAccountBaseInfo = "IF NOT EXISTS(SELECT 1 FROM [SMS_Account] WHERE [CompanyID]=@CompanyId) BEGIN INSERT INTO [SMS_Account](ID,CompanyID,AccountMoney) VALUES(@ID,@CompanyId,0) END";
        private const string SQL_SELECT_IsExistsAccount = "SELECT COUNT(*) FROM SMS_Account WHERE CompanyId=@CompanyId";
        #endregion static constants

        #region 成员方法
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual decimal GetAccountMoney(string companyId)
        {
            decimal accountMoney = 0;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETACCOUNTMONEY);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    accountMoney = rdr.GetDecimal(0);
                }
            }

            return accountMoney;
        }

        /// <summary>
        /// 获取账户剩余短信条数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetAccountSMSNumber(string companyId)
        {
            int number = 0;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETACCOUNTSMSNUMBER);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    number = rdr.GetInt32(0);
                }
            }

            return number;
        }
        /// <summary>
        /// 获取指定公司的账户信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns>账户信息</returns>
        public virtual EyouSoft.Model.SMSStructure.AccountInfo GetAccountInfo(string companyId)
        {
            EyouSoft.Model.SMSStructure.AccountInfo model = null;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETACCOUNTINFO);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    model = new EyouSoft.Model.SMSStructure.AccountInfo();
                    model.AccountId = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(0);
                    model.CompanyId = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1);
                    model.AccountMoney = rdr.IsDBNull(2) ? 0 : rdr.GetDecimal(2);
                }
            }
            return model;
        }

        /// <summary>
        /// 是否存在未审核的充值
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual bool HasNoCheckPay(string companyId)
        {
            bool tmp = false;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_HASNOCHECKPAY);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    tmp = rdr.GetInt32(0) > 0 ? true : false;
                }
            }

            return tmp;
        }

        /// <summary>
        /// 扣除账户余额
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="money">扣除金额</param>
        /// <param name="smscount">扣除短信条数</param>
        /// <param name="tempFeeTakeId">金额扣除临时表编号</param>
        /// <param name="sendTotalId">短信发送统计表编号</param>
        /// <returns></returns>
        public virtual bool DeductAccountMoney(string companyId, string userId, decimal money, int smscount, string tempFeeTakeId, string sendTotalId)
        {
            DbCommand cmd = base.SMSStore.GetStoredProcCommand("proc_SMS_DeductAccountMoney");
            base.SMSStore.AddInParameter(cmd, "TempFeeTakeId", DbType.String, tempFeeTakeId);
            base.SMSStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            base.SMSStore.AddInParameter(cmd, "UserId", DbType.String, userId);
            base.SMSStore.AddInParameter(cmd, "SendTotalId", DbType.String, sendTotalId);
            base.SMSStore.AddInParameter(cmd, "Money", DbType.Decimal, money);
            base.SMSStore.AddInParameter(cmd, "SMSNumber", DbType.Int32, smscount);
            base.SMSStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, base.SMSStore);

            return Convert.ToInt32(base.SMSStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 账户充值
        /// </summary>
        /// <param name="payMoneyInfo">充值支付信息业务实体</param>
        /// <returns></returns>
        public virtual bool InsertPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo payMoneyInfo)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_PAYMONEY);

            base.SMSStore.AddInParameter(cmd, "ID", DbType.String, payMoneyInfo.PayMoneyId);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.String, payMoneyInfo.CompanyId);
            base.SMSStore.AddInParameter(cmd, "COMPANYNAME", DbType.String, payMoneyInfo.CompanyName);
            base.SMSStore.AddInParameter(cmd, "USERID", DbType.String, payMoneyInfo.UserId);
            base.SMSStore.AddInParameter(cmd, "USERFULLNAME", DbType.String, payMoneyInfo.UserFullName);
            base.SMSStore.AddInParameter(cmd, "PAYMONEY", DbType.Decimal, payMoneyInfo.PayMoney);
            base.SMSStore.AddInParameter(cmd, "PAYTIME", DbType.DateTime, payMoneyInfo.PayTime);
            base.SMSStore.AddInParameter(cmd, "PaySMSNumber", DbType.Int32, payMoneyInfo.PaySMSNumber);
            base.SMSStore.AddInParameter(cmd, "OperatorMobile", DbType.String, payMoneyInfo.UserMobile);
            base.SMSStore.AddInParameter(cmd, "OperatorMQId", DbType.String, payMoneyInfo.UserMQId);
            base.SMSStore.AddInParameter(cmd, "OperatorTel", DbType.String, payMoneyInfo.UserTel);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取账户充值明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号 为null时取所有公司</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="payStartTime">充值开始时间 为null时不做为查询条件</param>
        /// <param name="payFinishTime">充值截止时间 为null时不做为查询条件</param>
        /// <param name="operatorStartTime">操作开始时间 为null时不做为查询条件</param>
        /// <param name="operatorFinishTime">操作截止时间 为null时不做为查询条件</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="checkStatus">审核状态 -1:所有状态 0:未审核 1:审核通过  2:审核未通过</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.PayMoneyInfo> GetPayMoneys(int pageSize, int pageIndex, ref int recordCount, string companyId, string companyName,
            DateTime? payStartTime, DateTime? payFinishTime, DateTime? operatorStartTime, DateTime? operatorFinishTime,
            int? provinceId, int? cityId, int checkStatus, string userAreas)
        {
            IList <EyouSoft.Model.SMSStructure.PayMoneyInfo> paymoneys = new List<EyouSoft.Model.SMSStructure.PayMoneyInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "SMS_PayMoney";
            string primaryKey = "Id";
            string orderByString = "PayTime DESC,OperatorTime DESC";
            StringBuilder fields = new StringBuilder();
            fields.Append(" [ID],[CompanyID],[CompanyName],[OperatorID],[OperatorName],[PayMoney],[PayTime],[OperatorTime],[IsChecked],[CheckTime],[CheckUserName],[CheckOperatorName],[OperatorTel],[OperatorMobile],[OperatorMQId],[PaySMSNumber],[UseMoney]");
            fields.Append(" ,(SELECT ProvinceId FROM tbl_CompanyInfo WHERE Id=SMS_PayMoney.CompanyID) AS ProvinceId ");
            fields.Append(" ,(SELECT CityId FROM tbl_CompanyInfo WHERE Id=SMS_PayMoney.CompanyID) AS CityId ");

            cmdQuery.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(companyId))
            {
                cmdQuery.AppendFormat(" AND CompanyID='{0}' ", companyId.ToString());
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (payStartTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND PayTime>='{0}' ", payStartTime.Value.ToString());
            }

            if (payFinishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND PayTime<'{0}' ", payFinishTime.Value.ToString());
            }

            if (operatorStartTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND OperatorTime>='{0}' ", operatorStartTime.Value.ToString());
            }

            if (operatorFinishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND OperatorTime<'{0}' ", operatorFinishTime.Value.ToString());
            }

            /*if (provinceId.HasValue)
            {
                cmdQuery.AppendFormat(" AND ProvinceId={0} ", provinceId.Value);
            }

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND CityId={0} ", cityId.Value);
            }*/

            if (checkStatus != -1)
            {
                cmdQuery.AppendFormat(" AND IsChecked={0} ", checkStatus);
            }
            
            /*if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
            }*/

            if (provinceId.HasValue || cityId.HasValue || !string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.Append(" AND EXISTS(SELECT 1 FROM tbl_CompanyInfo WHERE Id=SMS_PayMoney.CompanyID ");

                if (provinceId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND ProvinceId={0} ", provinceId.Value);
                }

                if (cityId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND CityId={0} ", cityId.Value);
                }

                if (!string.IsNullOrEmpty(userAreas))
                {
                    cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
                }

                cmdQuery.Append(" ) ");
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.PayMoneyInfo payMoneyInfo = new EyouSoft.Model.SMSStructure.PayMoneyInfo();

                    payMoneyInfo.PayMoneyId = rdr.GetString(rdr.GetOrdinal("ID"));
                    payMoneyInfo.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    payMoneyInfo.CompanyName = rdr["CompanyName"].ToString();
                    payMoneyInfo.UserId = rdr.GetString(rdr.GetOrdinal("OperatorID"));
                    payMoneyInfo.UserFullName = rdr["OperatorName"].ToString();
                    payMoneyInfo.PayMoney = rdr.GetDecimal(rdr.GetOrdinal("PayMoney"));
                    payMoneyInfo.PaySMSNumber = rdr.GetInt32(rdr.GetOrdinal("PaySMSNumber"));
                    payMoneyInfo.PayTime = rdr.GetDateTime(rdr.GetOrdinal("PayTime"));
                    payMoneyInfo.OperatorTime = rdr.GetDateTime(rdr.GetOrdinal("OperatorTime"));
                    payMoneyInfo.IsChecked = rdr.GetInt32(rdr.GetOrdinal("IsChecked"));

                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckTime")))
                    {
                        payMoneyInfo.CheckTime = rdr.GetDateTime(rdr.GetOrdinal("CheckTime"));
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckUserName")))
                    {
                        payMoneyInfo.CheckUserName = rdr["CheckUserName"].ToString();
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckOperatorName")))
                    {
                        payMoneyInfo.CheckUserFullName = rdr["CheckOperatorName"].ToString();
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("OperatorTel")))
                    {
                        payMoneyInfo.UserTel = rdr["OperatorTel"].ToString();
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("OperatorMobile")))
                    {
                        payMoneyInfo.UserMobile = rdr["OperatorMobile"].ToString();
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("OperatorMQId")))
                    {
                        payMoneyInfo.UserMQId = rdr.GetString(rdr.GetOrdinal("OperatorMQId"));
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("UseMoney")))
                    {
                        payMoneyInfo.UseMoney = rdr.GetDecimal(rdr.GetOrdinal("UseMoney"));
                    }

                    payMoneyInfo.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    payMoneyInfo.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));

                    paymoneys.Add(payMoneyInfo);
                }
            }

            return paymoneys;
        }

        /// <summary>
        /// 获得已充值过的公司汇总
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyName">公司名称 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> GetAllPayedCompanys(int pageSize, int pageIndex, ref int recordCount, string companyName)
        {
            IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> items = new List<EyouSoft.Model.SMSStructure.AccountDetailInfo>();
            StringBuilder strSql = new StringBuilder();
            string tableName = "view_SMS_AllPayedCompany";
            string fields = "CompanyID,CompanyName,ContactName,ContactTel,ContactMobile,ContactMQ,AccountMoney,AccountSMSNumber";
            string orderByString = "AccountSMSNumber,AccountMoney,CompanyID";
            string primaryKey = "CompanyID";
            StringBuilder cmdQuery = new StringBuilder();

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat("CompanyName LIKE '%{0}%'", companyName);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.AccountDetailInfo item = new EyouSoft.Model.SMSStructure.AccountDetailInfo();
                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    item.CompanyName = rdr["CompanyName"].ToString();
                    item.ContactName = rdr["ContactName"].ToString();
                    item.Tel = rdr["ContactTel"].ToString();
                    item.Mobile = rdr["ContactMobile"].ToString();                    
                    item.MQId = rdr["ContactMQ"].ToString();
                    item.AccountMoney = Convert.ToDecimal(rdr["AccountMoney"].ToString());
                    //item.AccountSMSNumber = Convert.ToInt32(rdr["AccountSMSNumber"].ToString());
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// 获取账户消费明细
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.SendTotalInfo> GetExpenseDetails(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            IList<EyouSoft.Model.SMSStructure.SendTotalInfo> totals = new List<EyouSoft.Model.SMSStructure.SendTotalInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "SMS_SendTotal";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " ID,   IssueTime, UseMoeny, SuccessCount, ErrorCount,SendChannel";

            cmdQuery.AppendFormat(" CompanyID='{0}' ", companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.SendTotalInfo sendTotalInfo = new EyouSoft.Model.SMSStructure.SendTotalInfo();

                    sendTotalInfo.TotalId = rdr.GetString(rdr.GetOrdinal("Id"));
                    sendTotalInfo.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    sendTotalInfo.SuccessCount = rdr.GetInt32(rdr.GetOrdinal("SuccessCount"));
                    sendTotalInfo.ErrorCount = rdr.GetInt32(rdr.GetOrdinal("ErrorCount"));
                    sendTotalInfo.UseMoeny = rdr.GetDecimal(rdr.GetOrdinal("UseMoeny"));
                    sendTotalInfo.SendChannel = new EyouSoft.Model.SMSStructure.SMSChannelList()[rdr.GetInt32(rdr.GetOrdinal("SendChannel"))];

                    totals.Add(sendTotalInfo);
                }
            }

            return totals;
        }

        /// <summary>
        /// 充值审核
        /// </summary>
        /// <param name="checkPayMoneyInfo">账户充值业务实体</param>
        /// <returns></returns>
        /// <remarks>
        /// 实体需要设置如下信息：
        /// PayMoneyId：充值支付编号
        /// IsChecked：审核状态 0:未审核 1:审核通过  2:审核未通过
        /// CheckTime：审核时间
        /// CheckUserName：审核人用户名
        /// CheckUserFullName：审核人姓名
        /// </remarks>
        public virtual bool CheckPayMoney(EyouSoft.Model.SMSStructure.PayMoneyInfo checkPayMoneyInfo)
        {
            DbCommand cmd = base.SMSStore.GetStoredProcCommand("proc_SMS_CheckPayMoney");

            base.SMSStore.AddInParameter(cmd, "PayMoneyId", DbType.String, checkPayMoneyInfo.PayMoneyId);
            base.SMSStore.AddInParameter(cmd, "CheckStatus", DbType.Int32, checkPayMoneyInfo.IsChecked);
            base.SMSStore.AddInParameter(cmd, "CheckTime", DbType.DateTime, checkPayMoneyInfo.CheckTime);
            base.SMSStore.AddInParameter(cmd, "CheckUserName", DbType.String, checkPayMoneyInfo.CheckUserName);
            base.SMSStore.AddInParameter(cmd, "CheckUserFullName", DbType.String, checkPayMoneyInfo.CheckUserFullName);
            base.SMSStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            base.SMSStore.AddInParameter(cmd, "UseMoney", DbType.Decimal, checkPayMoneyInfo.UseMoney);

            DbHelper.RunProcedure(cmd, base.SMSStore);

            return Convert.ToInt32(base.SMSStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 删除未通过审核[未审核/审核未通过]的充值记录
        /// </summary>
        /// <param name="PayMoneyId">充值记录ID</param>
        /// <returns></returns>
        public virtual bool DeleteNoPassCheckPayMoney(string PayMoneyId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_DELETE_NoPassCheckPayMoney);
            base.SMSStore.AddInParameter(cmd, "PayMoneyId", DbType.AnsiStringFixedLength, PayMoneyId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取消费明细汇总信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo GetAccountExpenseCollectInfo(string companyId)
        {
            EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo accountExpenseCollectInfo = new EyouSoft.Model.SMSStructure.AccountExpenseCollectInfo();
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_GETACCOUNTEXPENSECOLLECTINFO);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd,base.SMSStore))
            {
                if (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("SentMessageCount")))
                    {
                        accountExpenseCollectInfo.SentMessageCount = rdr.GetInt32(rdr.GetOrdinal("SentMessageCount"));
                    }

                    if (!rdr.IsDBNull(rdr.GetOrdinal("ExpenseAmount")))
                    {
                        accountExpenseCollectInfo.ExpenseAmount = rdr.GetDecimal(rdr.GetOrdinal("ExpenseAmount"));
                    }
                }
            }
            
            return accountExpenseCollectInfo;
        }

        /// <summary>
        /// 设置短信中心账户基础数据
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual bool SetAccountBaseInfo(string companyId)
        {
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_INSERT_SetAccountBaseInfo);

            base.SMSStore.AddInParameter(cmd, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            base.SMSStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, base.SMSStore) > 0 ? true : false;
        }

        /// <summary>
        /// 判断是否存在公司账户
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual bool IsExistsAccount(string companyId)
        {
            bool tmp = false;
            DbCommand cmd = base.SMSStore.GetSqlStringCommand(SQL_SELECT_IsExistsAccount);
            base.SMSStore.AddInParameter(cmd, "COMPANYID", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SMSStore))
            {
                if (rdr.Read())
                {
                    tmp = rdr.GetInt32(0) > 0 ? true : false;
                }
            }

            return tmp;
        }

        /// <summary>
        /// 短信剩余统计
        /// </summary>
        /// <param name="pageSize">每页显示记录数量</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="expression">剩余金额的数值表达式</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> RemnantStats(int pageSize, int pageIndex, ref int recordCount, double expression, int? provinceId, int? cityId, string companyName, string userAreas)
        {
            IList<EyouSoft.Model.SMSStructure.AccountDetailInfo> items = new List<EyouSoft.Model.SMSStructure.AccountDetailInfo>();
            StringBuilder strSql = new StringBuilder();
            string tableName = "view_SMS_AllPayedCompany";
            string fields = "CompanyID,CompanyName,ContactName,ContactTel,ContactMobile,ContactMQ,AccountMoney,AccountSMSNumber,ProvinceId,CityId,ContactQQ";
            string orderByString = "AccountSMSNumber,AccountMoney,CompanyID";
            string primaryKey = "CompanyID";
            StringBuilder cmdQuery = new StringBuilder();

            cmdQuery.AppendFormat(" AccountMoney<={0} ", expression);

            if (provinceId.HasValue)
            {
                cmdQuery.AppendFormat(" AND ProvinceId={0} ", provinceId.Value);
            }

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND CityId={0} ", cityId.Value);
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.SMSStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SMSStructure.AccountDetailInfo item = new EyouSoft.Model.SMSStructure.AccountDetailInfo();

                    item.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    item.CompanyName = rdr["CompanyName"].ToString();
                    item.ContactName = rdr["ContactName"].ToString();
                    item.Tel = rdr["ContactTel"].ToString();
                    item.Mobile = rdr["ContactMobile"].ToString();
                    item.MQId = rdr["ContactMQ"].ToString();
                    item.AccountMoney = Convert.ToDecimal(rdr["AccountMoney"].ToString());
                    //item.AccountSMSNumber = Convert.ToInt32(rdr["AccountSMSNumber"].ToString());
                    item.PrivinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    item.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    item.QQ = rdr["ContactQQ"].ToString();

                    items.Add(item);
                }
            }
            return items;
        }
        #endregion
    }
}
