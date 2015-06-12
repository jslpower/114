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

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 高级服务申请数据访问类
    /// </summary>
    /// Author:汪奇志 2010-07-22
    public class SysApplyService : DALBase, EyouSoft.IDAL.SystemStructure.ISysApplyService
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_Apply = "INSERT INTO tbl_SysApplyService(ID,ServiceType,CompanyID,CompanyName,ProvinceId,CityId,ProvinceName,CityName,CompanyAdress,UserID,ContactName,ContactTel,ContactMobile,ContactMQ,ContactQQ,ApplyText,ApplyTime) VALUES(@ID,@ServiceType,@CompanyID,@CompanyName,@ProvinceId,@CityId ,@ProvinceName,@CityName,@CompanyAdress,@UserID,@ContactName,@ContactTel,@ContactMobile,@ContactMQ,@ContactQQ,@ApplyText,@ApplyTime)";
        private const string SQL_SELECT_IsApply = "SELECT COUNT(*) AS CountNumber FROM tbl_SysApplyService WHERE CompanyId=@CompanyId AND ServiceType=@ServiceType";
        private const string SQL_SELECT_GetApplyState = "SELECT ApplyState FROM tbl_SysApplyService WHERE CompanyId=@CompanyId AND ServiceType=@ServiceType";
        private const string SQL_SELECT_GetApplyInfo = "SELECT top 1 * FROM tbl_SysApplyService WHERE 1=1 ";
        private const string SQL_SELECT_GetServiceReneweds = "SELECT * FROM tbl_SysApplyServiceFee WHERE ApplyServiceId=@ApplyId ORDER BY IssueTime DESC";
        #endregion

        #region 成员方法
        /// <summary>
        /// 高级服务申请
        /// </summary>
        /// <param name="info">高级服务申请信息业务实体</param>
        /// <returns></returns>
        public virtual bool Apply(EyouSoft.Model.SystemStructure.SysApplyServiceInfo info)
        {
            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_INSERT_Apply);

            this.SystemStore.AddInParameter(cmd, "ID", DbType.String, info.ApplyId);
            this.SystemStore.AddInParameter(cmd, "ServiceType", DbType.Byte, info.ApplyServiceType);
            this.SystemStore.AddInParameter(cmd, "CompanyID", DbType.String, info.CompanyId);
            this.SystemStore.AddInParameter(cmd, "CompanyName", DbType.String, info.CompanyName);
            this.SystemStore.AddInParameter(cmd, "ProvinceId", DbType.Int32, info.ProvinceId);
            this.SystemStore.AddInParameter(cmd, "CityId", DbType.Int32, info.CityId);
            this.SystemStore.AddInParameter(cmd, "ProvinceName", DbType.String, info.ProvinceName);
            this.SystemStore.AddInParameter(cmd, "CityName", DbType.String, info.CityName);
            this.SystemStore.AddInParameter(cmd, "CompanyAdress", DbType.String, info.ContactAddress);
            this.SystemStore.AddInParameter(cmd, "UserID", DbType.String, info.UserId);
            this.SystemStore.AddInParameter(cmd, "ContactName", DbType.String, info.ContactName);
            this.SystemStore.AddInParameter(cmd, "ContactTel", DbType.String, info.ContactTel);
            this.SystemStore.AddInParameter(cmd, "ContactMobile", DbType.String, info.ContactMobile);
            this.SystemStore.AddInParameter(cmd, "ContactMQ", DbType.String, info.ContactMQ);
            this.SystemStore.AddInParameter(cmd, "ContactQQ", DbType.String, info.ContactQQ);
            this.SystemStore.AddInParameter(cmd, "ApplyText", DbType.String, info.ApplyText);
            this.SystemStore.AddInParameter(cmd, "ApplyTime", DbType.String, info.ApplyTime);
            //this.SystemStore.AddInParameter(cmd, "ApplyState", DbType.Byte, info.ApplyState);

            return DbHelper.ExecuteSql(cmd, this.SystemStore) == 1 ? true : false;
        }

        /// <summary>
        /// 判断是否申请高级服务
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public virtual bool IsApply(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            bool isApply = false;
            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_SELECT_IsApply);

            this.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            this.SystemStore.AddInParameter(cmd, "ServiceType", DbType.Byte, serviceType);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this.SystemStore))
            {
                if (rdr.Read())
                {
                    isApply = rdr.GetInt32(0) == 0 ? false : true;
                }
            }

            return isApply;
        }

        /// <summary>
        /// 获取高级服务申请审核状态,返回null未申请
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.ApplyServiceState? GetApplyState(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            EyouSoft.Model.SystemStructure.ApplyServiceState? state = null;
            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_SELECT_GetApplyState);

            this.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            this.SystemStore.AddInParameter(cmd, "ServiceType", DbType.Byte, serviceType);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this.SystemStore))
            {
                if (rdr.Read())
                {
                    state = (EyouSoft.Model.SystemStructure.ApplyServiceState)rdr.GetByte(0);
                }
            }

            return state;
        }

        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string applyId)
        {
            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_SELECT_GetApplyInfo + " AND Id=@Id");
            this.SystemStore.AddInParameter(cmd, "Id", DbType.String, applyId);
            return GetApplyInfo(cmd);
        }

        /// <summary>
        /// 获取高级服务申请信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(string companyId, EyouSoft.Model.CompanyStructure.SysService serviceType)
        {
            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_SELECT_GetApplyInfo + " AND CompanyId=@CompanyId AND ServiceType=@ServiceType");
            this.SystemStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            this.SystemStore.AddInParameter(cmd, "ServiceType", DbType.Byte, serviceType);
            return GetApplyInfo(cmd);
        }
        /// <summary>
        /// 处理实体信息
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        private EyouSoft.Model.SystemStructure.SysApplyServiceInfo GetApplyInfo(DbCommand dc)
        {
            EyouSoft.Model.SystemStructure.SysApplyServiceInfo info = null;

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();

                    info.ApplyId = rdr.GetString(rdr.GetOrdinal("Id"));
                    info.ApplyServiceType = (EyouSoft.Model.CompanyStructure.SysService)rdr.GetByte(rdr.GetOrdinal("ServiceType"));
                    info.ApplyState = (EyouSoft.Model.SystemStructure.ApplyServiceState)rdr.GetByte(rdr.GetOrdinal("ApplyState"));
                    info.ApplyText = rdr["ApplyText"].ToString();
                    info.ApplyTime = rdr.GetDateTime(rdr.GetOrdinal("ApplyTime"));
                    info.CheckText = rdr["CheckText"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckTime")))
                        info.CheckTime = rdr.GetDateTime(rdr.GetOrdinal("CheckTime"));
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CityName = rdr["CityName"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CompanyName = rdr["CompanyName"].ToString();
                    info.ContactAddress = rdr["CompanyAdress"].ToString();
                    info.ContactMobile = rdr["ContactMobile"].ToString();
                    info.ContactMQ = rdr["ContactMQ"].ToString();
                    info.ContactName = rdr["ContactName"].ToString();
                    info.ContactQQ = rdr["ContactQQ"].ToString();
                    info.ContactTel = rdr["ContactTel"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EnableTime")))
                        info.EnableTime = rdr.GetDateTime(rdr.GetOrdinal("EnableTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ExpireTime")))
                        info.ExpireTime = rdr.GetDateTime(rdr.GetOrdinal("ExpireTime"));
                    info.OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorID"));
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.ProvinceName = rdr["ProvinceName"].ToString();
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));
                }
            }

            return info;
        }

        /// <summary>
        /// 获取高级服务申请信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="applyStartTime">申请起始时间 为null时不做为查询条件</param>
        /// <param name="applyFinishTime">申请截止时间 为null时不做为查询条件</param>
        /// <param name="applyState">审核状态 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType, int? provinceId, int? cityId, string companyName, DateTime? applyStartTime, DateTime? applyFinishTime
            , EyouSoft.Model.SystemStructure.ApplyServiceState? applyState,string userAreas)
        {
            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> services = new List<EyouSoft.Model.SystemStructure.SysApplyServiceInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_SysApplyService";
            string primaryKey = "Id";
            string orderByString = "ApplyState ASC,CheckTime DESC,ApplyTime DESC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" ServiceType={0} ", (int)serviceType);

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

            if (applyStartTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ApplyTime>='{0}' ", applyStartTime.Value);
            }

            if (applyFinishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ApplyTime<='{0}' ", applyFinishTime.Value);
            }

            if (applyState.HasValue)
            {
                cmdQuery.AppendFormat(" AND ApplyState={0} ", (int)applyState.Value);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(this.SystemStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysApplyServiceInfo info = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();

                    info.ApplyId = rdr.GetString(rdr.GetOrdinal("Id"));
                    info.ApplyServiceType = (EyouSoft.Model.CompanyStructure.SysService)rdr.GetByte(rdr.GetOrdinal("ServiceType"));
                    info.ApplyState = (EyouSoft.Model.SystemStructure.ApplyServiceState)rdr.GetByte(rdr.GetOrdinal("ApplyState"));
                    info.ApplyText = rdr["ApplyText"].ToString();
                    info.ApplyTime = rdr.GetDateTime(rdr.GetOrdinal("ApplyTime"));
                    info.CheckText = rdr["CheckText"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckTime")))
                        info.CheckTime = rdr.GetDateTime(rdr.GetOrdinal("CheckTime"));
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CityName = rdr["CityName"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CompanyName = rdr["CompanyName"].ToString();
                    info.ContactAddress = rdr["CompanyAdress"].ToString();
                    info.ContactMobile = rdr["ContactMobile"].ToString();
                    info.ContactMQ = rdr["ContactMQ"].ToString();
                    info.ContactName = rdr["ContactName"].ToString();
                    info.ContactQQ = rdr["ContactQQ"].ToString();
                    info.ContactTel = rdr["ContactTel"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EnableTime")))
                        info.EnableTime = rdr.GetDateTime(rdr.GetOrdinal("EnableTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ExpireTime")))
                        info.ExpireTime = rdr.GetDateTime(rdr.GetOrdinal("ExpireTime"));
                    info.OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorID"));
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.ProvinceName = rdr["ProvinceName"].ToString();
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));

                    services.Add(info);
                }
            }

            return services;
        }

        /// <summary>
        /// 获取快到期服务信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="serviceType">高级服务项目</param>
        /// <param name="expireStartTime">到期起始时间 为null时不做为查询条件</param>
        /// <param name="expireFinishTime">到期截止时间 为null时不做为查询条件</param>
        /// <param name="userAreas">用户分管的区域范围 为null时不做为查询条件</param>
        /// <param name="provinceId">省份编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="searchStartTime">搜索起始时间 为null时不做为查询条件</param>
        /// <param name="searchFinishTime">搜索截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> GetComingExpireApplys(int pageSize, int pageIndex, ref int recordCount
            , EyouSoft.Model.CompanyStructure.SysService serviceType, DateTime? expireStartTime, DateTime? expireFinishTime
            , string userAreas
            , int? provinceId, int? cityId, string companyName
            , DateTime? searchStartTime, DateTime? searchFinishTime)
        {
            IList<EyouSoft.Model.SystemStructure.SysApplyServiceInfo> services = new List<EyouSoft.Model.SystemStructure.SysApplyServiceInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_SysApplyService";
            string primaryKey = "Id";
            string orderByString = "ApplyState ASC,CheckTime DESC,ApplyTime DESC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" ApplyState=1 AND ServiceType={0} ", (int)serviceType);

            if (expireStartTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ExpireTime>='{0}' ", expireStartTime.Value);
            }

            if (expireFinishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ExpireTime<='{0}' ", expireFinishTime.Value);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND CityId IN({0}) ", userAreas);
            }

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

            if (searchStartTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ExpireTime>='{0}' ", searchStartTime.Value);
            }

            if (searchFinishTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND ExpireTime<='{0}' ", searchFinishTime.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(this.SystemStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.SystemStructure.SysApplyServiceInfo info = new EyouSoft.Model.SystemStructure.SysApplyServiceInfo();

                    info.ApplyId = rdr.GetString(rdr.GetOrdinal("Id"));
                    info.ApplyServiceType = (EyouSoft.Model.CompanyStructure.SysService)rdr.GetByte(rdr.GetOrdinal("ServiceType"));
                    info.ApplyState = (EyouSoft.Model.SystemStructure.ApplyServiceState)rdr.GetByte(rdr.GetOrdinal("ApplyState"));
                    info.ApplyText = rdr["ApplyText"].ToString();
                    info.ApplyTime = rdr.GetDateTime(rdr.GetOrdinal("ApplyTime"));
                    info.CheckText = rdr["CheckText"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CheckTime")))
                        info.CheckTime = rdr.GetDateTime(rdr.GetOrdinal("CheckTime"));
                    info.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    info.CityName = rdr["CityName"].ToString();
                    info.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    info.CompanyName = rdr["CompanyName"].ToString();
                    info.ContactAddress = rdr["CompanyAdress"].ToString();
                    info.ContactMobile = rdr["ContactMobile"].ToString();
                    info.ContactMQ = rdr["ContactMQ"].ToString();
                    info.ContactName = rdr["ContactName"].ToString();
                    info.ContactQQ = rdr["ContactQQ"].ToString();
                    info.ContactTel = rdr["ContactTel"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("EnableTime")))
                        info.EnableTime = rdr.GetDateTime(rdr.GetOrdinal("EnableTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("ExpireTime")))
                        info.ExpireTime = rdr.GetDateTime(rdr.GetOrdinal("ExpireTime"));
                    info.OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorID"));
                    info.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    info.ProvinceName = rdr["ProvinceName"].ToString();
                    info.UserId = rdr.GetString(rdr.GetOrdinal("UserID"));

                    services.Add(info);
                }
            }

            return services;
        }

        /// <summary>
        /// 高级网店审核
        /// </summary>
        /// <param name="info">高级网店申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败 2:域名重复</returns>
        public virtual int EshopChecked(EyouSoft.Model.SystemStructure.EshopCheckInfo info)
        {
            DbCommand cmd = this.SystemStore.GetStoredProcCommand("proc_SysApplyService_EshopChecked");

            this.SystemStore.AddInParameter(cmd, "ApplyId", DbType.String, info.ApplyId);
            this.SystemStore.AddInParameter(cmd, "EnableTime", DbType.DateTime, info.EnableTime);
            this.SystemStore.AddInParameter(cmd, "ExpireTime", DbType.DateTime, info.ExpireTime);
            this.SystemStore.AddInParameter(cmd, "ApplyState", DbType.Byte, info.ApplyState);
            this.SystemStore.AddInParameter(cmd, "CheckTime", DbType.DateTime, info.CheckTime);
            this.SystemStore.AddInParameter(cmd, "OperatorId", DbType.Int32, info.OperatorId);
            this.SystemStore.AddInParameter(cmd, "DomainName", DbType.String, info.DomainName);
            this.SystemStore.AddInParameter(cmd, "RenewalId", DbType.String, Guid.NewGuid().ToString());
            this.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            this.SystemStore.AddInParameter(cmd, "TemplatePath", DbType.String, info.TemplatePath);
            this.SystemStore.AddInParameter(cmd, "GoogleMapKey", DbType.String, info.GoogleMapKey);

            DbHelper.RunProcedure(cmd, this.SystemStore);

            return Convert.ToInt32(this.SystemStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 收费MQ审核
        /// </summary>
        /// <param name="info">收费MQ申请审核信息业务实体</param>
        /// <returns>1:成功 0:失败</returns>
        public virtual int MQChecked(EyouSoft.Model.SystemStructure.MQCheckInfo info)
        {
            DbCommand cmd = this.SystemStore.GetStoredProcCommand("proc_SysApplyService_MQChecked");

            this.SystemStore.AddInParameter(cmd, "ApplyId", DbType.String, info.ApplyId);
            this.SystemStore.AddInParameter(cmd, "EnableTime", DbType.DateTime, info.EnableTime);
            this.SystemStore.AddInParameter(cmd, "ExpireTime", DbType.DateTime, info.ExpireTime);
            this.SystemStore.AddInParameter(cmd, "ApplyState", DbType.Byte, info.ApplyState);
            this.SystemStore.AddInParameter(cmd, "CheckTime", DbType.DateTime, info.CheckTime);
            this.SystemStore.AddInParameter(cmd, "OperatorId", DbType.Int32, info.OperatorId);
            this.SystemStore.AddInParameter(cmd, "SubAccountNumber", DbType.String, info.SubAccountNumber);
            this.SystemStore.AddInParameter(cmd, "RenewalId", DbType.String, Guid.NewGuid().ToString());
            this.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, this.SystemStore);

            return Convert.ToInt32(this.SystemStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 高级服务续费
        /// </summary>
        /// <param name="applyId">申请编号</param>
        /// <param name="enableTime">续费起始时间</param>
        /// <param name="expireTime">续费到期时间</param>
        /// <param name="operatorId">续费人编号</param>
        /// <param name="renewTime">续费时间</param>
        /// <param name="subAccountNumber">MQ续费时子账号数量 高级网店续费时设置0</param>
        /// <returns>1:成功 0:失败 2:续费时间不对</returns>
        public virtual int Renewed(string applyId, DateTime enableTime, DateTime expireTime, int operatorId, DateTime renewTime, int subAccountNumber)
        {
            DbCommand cmd = this.SystemStore.GetStoredProcCommand("proc_SysApplyService_Renewed");

            this.SystemStore.AddInParameter(cmd, "ApplyId", DbType.String, applyId);
            this.SystemStore.AddInParameter(cmd, "EnableTime", DbType.DateTime, enableTime);
            this.SystemStore.AddInParameter(cmd, "ExpireTime", DbType.DateTime, expireTime);
            this.SystemStore.AddInParameter(cmd, "RenewTime", DbType.DateTime, renewTime);
            this.SystemStore.AddInParameter(cmd, "OperatorId", DbType.Int32, operatorId);
            this.SystemStore.AddInParameter(cmd, "RenewalId", DbType.String, Guid.NewGuid().ToString());
            this.SystemStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            this.SystemStore.AddInParameter(cmd, "SubAccountNumber", DbType.Int32, subAccountNumber);

            DbHelper.RunProcedure(cmd, this.SystemStore);

            return Convert.ToInt32(this.SystemStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 获取高级服务续费信息集合
        /// </summary>
        /// <param name="applyId">高级服务申请编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.SystemStructure.ServiceRenewInfo> GetServiceReneweds(string applyId)
        {
            IList<EyouSoft.Model.SystemStructure.ServiceRenewInfo> reneweds = new List<EyouSoft.Model.SystemStructure.ServiceRenewInfo>();

            DbCommand cmd = this.SystemStore.GetSqlStringCommand(SQL_SELECT_GetServiceReneweds);
            this.SystemStore.AddInParameter(cmd, "ApplyId", DbType.AnsiStringFixedLength, applyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this.SystemStore))
            {
                while (rdr.Read())
                {
                    reneweds.Add(new EyouSoft.Model.SystemStructure.ServiceRenewInfo
                    {
                        ApplyServiceId=applyId,
                        CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyID")),
                        EnableTime = rdr.GetDateTime(rdr.GetOrdinal("EnableTime")),
                        ExpireTime = rdr.GetDateTime(rdr.GetOrdinal("ExpireTime")),
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        OperatorId = rdr.GetInt32(rdr.GetOrdinal("OperatorId")),
                        RenewId = rdr.GetString(rdr.GetOrdinal("OperatorId")),
                        SubAccountNumber = rdr.IsDBNull(rdr.GetOrdinal("SubAccountNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SubAccountNumber"))
                    });
                }
            }

            return reneweds;
        }
        #endregion 成员方法
    }
}
