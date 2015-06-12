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

namespace EyouSoft.DAL.OnLineServer
{
    /// <summary>
    /// 在线客服数据访问
    /// </summary>
    public class OLServer : DALBase, IDAL.OnLineServer.IOLServer
    {
        #region constructor

        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public OLServer()
        {
            this._db = base.SystemStore;
        }

        #endregion

        #region IOLServer 成员

        /// <summary>
        /// 获取默认的客服信息(服务人数最少的客服为默认的客服) 返回null时没有在线客服
        /// </summary>
        /// <param name="CompanyId">公司Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.OnLineServer.OlServerUserInfo GetDefaultServiceInfo(string CompanyId)
        {
            EyouSoft.Model.OnLineServer.OlServerUserInfo model = null;

            System.Text.StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append(" SELECT TOP 1 A.Id,A.UserId,A.UserName,A.OlName,A.IsService,A.LoginTime,A.AcceptId,A.AcceptName,A.IsOnline,A.CompanyId,A.LastSendMessageTime FROM OL_ServerUsers ");
            sbQuery.Append(" AS A INNER JOIN( ");
            sbQuery.Append("    SELECT Id,");
            sbQuery.Append("        (SELECT COUNT(*) AS UserCount FROM OL_ServerUsers AS D WHERE IsService='0' AND IsOnline='1' AND D.AcceptId=C.Id GROUP BY D.AcceptId ) AS UserCount ");
            sbQuery.Append("    FROM OL_ServerUsers AS C ");
            sbQuery.Append("    WHERE C.IsService='1' AND C.Isonline='1' and C.CompanyId = @CompanyId )AS B ");
            sbQuery.Append(" ON A.Id=B.Id ");
            sbQuery.Append(" ORDER BY B.UserCount,A.Id ");
            DbCommand cmd = this._db.GetSqlStringCommand(sbQuery.ToString());
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    model.OId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? "" : rdr.GetString(rdr.GetOrdinal("UserId"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    model.OlName = rdr.IsDBNull(rdr.GetOrdinal("OlName")) ? "" : rdr.GetString(rdr.GetOrdinal("OlName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsService")))
                    {
                        if (rdr.GetString(rdr.GetOrdinal("IsService")) == "1" || rdr.GetString(rdr.GetOrdinal("IsService")).ToLower() == "true")
                            model.IsService = true;
                        else
                            model.IsService = false;
                    }
                    model.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("LoginTime"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.IsOnline = rdr.GetString(rdr.GetOrdinal("IsOnline")) == "1" ? true :false ;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LastSendMessageTime")))
                        model.LastSendMessageTime = rdr.GetDateTime(rdr.GetOrdinal("LastSendMessageTime"));
                    #endregion
                }
            }

            return model;
        }

        /// <summary>
        /// 根据指定的在线编号获取在线用户信息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.OnLineServer.OlServerUserInfo GetUserInfoByOlId(string olId)
        {
            EyouSoft.Model.OnLineServer.OlServerUserInfo model = null;
            string sql = "select Id,UserId,UserName,OlName,IsService,LoginTime,AcceptId,AcceptName,IsOnline,CompanyId,LastSendMessageTime from OL_ServerUsers where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    model.OId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? "" : rdr.GetString(rdr.GetOrdinal("UserId"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    model.OlName = rdr.IsDBNull(rdr.GetOrdinal("OlName")) ? "" : rdr.GetString(rdr.GetOrdinal("OlName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsService")))
                    {
                        if (rdr.GetString(rdr.GetOrdinal("IsService")) == "1" || rdr.GetString(rdr.GetOrdinal("IsService")).ToLower() == "true")
                            model.IsService = true;
                        else
                            model.IsService = false;
                    }
                    model.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("LoginTime"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.IsOnline = rdr.GetString(rdr.GetOrdinal("IsOnline")) == "1" ? true : false;
                    model.LastSendMessageTime = rdr.GetDateTime(rdr.GetOrdinal("LastSendMessageTime"));
                    #endregion
                }
            }

            return model;
        }


        /// <summary>
        /// 根据指定的用户编号获取在线用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="isService">是否客服</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.OnLineServer.OlServerUserInfo GetUserInfoByUserId(string userId, bool isService)
        {
            EyouSoft.Model.OnLineServer.OlServerUserInfo model = null;
            string sql = "select Id,UserId,UserName,OlName,IsService,LoginTime,AcceptId,AcceptName,IsOnline,CompanyId,LastSendMessageTime from OL_ServerUsers where UserId = @UserId and IsService = @IsService";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);
            this._db.AddInParameter(cmd, "IsService", DbType.AnsiStringFixedLength, isService ? "1" : "0");

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    model.OId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? "" : rdr.GetString(rdr.GetOrdinal("UserId"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    model.OlName = rdr.IsDBNull(rdr.GetOrdinal("OlName")) ? "" : rdr.GetString(rdr.GetOrdinal("OlName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsService")))
                    {
                        if (rdr.GetString(rdr.GetOrdinal("IsService")) == "1" || rdr.GetString(rdr.GetOrdinal("IsService")).ToLower() == "true")
                            model.IsService = true;
                        else
                            model.IsService = false;
                    }
                    model.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("LoginTime"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.IsOnline = rdr.GetString(rdr.GetOrdinal("IsOnline")) == "1" ? true : false;
                    model.LastSendMessageTime = rdr.IsDBNull(rdr.GetOrdinal("LastSendMessageTime")) ? DateTime.Now : rdr.GetDateTime(rdr.GetOrdinal("LastSendMessageTime"));
                    #endregion
                }
            }

            return model;
        }

        /// <summary>
        /// 获取所有客服信息（不区分是否在线）
        /// </summary>
        /// <param name="OlId">在线Id</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> GetServicesInfo(string OlId)
        {
            IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> lsService = new List<EyouSoft.Model.OnLineServer.OlServerUserInfo>();
            EyouSoft.Model.OnLineServer.OlServerUserInfo model = null;
            string sql = "select Id,UserId,UserName,OlName,IsService,LoginTime,AcceptId,AcceptName,IsOnline,CompanyId,LastSendMessageTime from OL_ServerUsers where IsService = '1' and CompanyId = (select olsu.CompanyId from OL_ServerUsers as olsu where Id = @Id) ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, OlId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    model.OId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? "" : rdr.GetString(rdr.GetOrdinal("UserId"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    model.OlName = rdr.IsDBNull(rdr.GetOrdinal("OlName")) ? "" : rdr.GetString(rdr.GetOrdinal("OlName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsService")))
                    {
                        if (rdr.GetString(rdr.GetOrdinal("IsService")) == "1" || rdr.GetString(rdr.GetOrdinal("IsService")).ToLower() == "true")
                            model.IsService = true;
                        else
                            model.IsService = false;
                    }
                    model.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("LoginTime"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.IsOnline = rdr.GetString(rdr.GetOrdinal("IsOnline")) == "1" ? true : false;
                    model.LastSendMessageTime = rdr.IsDBNull(rdr.GetOrdinal("LastSendMessageTime")) ? DateTime.Now : rdr.GetDateTime(rdr.GetOrdinal("LastSendMessageTime"));
                    #endregion
                    lsService.Add(model);
                }
            }

            #region 取出当前用户自己的信息
            model =GetUserInfoByOlId(OlId);
            if (model != null)
                lsService.Add(model);
            #endregion

            return lsService;
        }

        /// <summary>
        /// 根据指定的在线客服编号获取所有在线用户信息
        /// </summary>
        /// <param name="olServiceId">在线客服编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> GetOnlineUsersInfo(string olServiceId)
        {
            IList<EyouSoft.Model.OnLineServer.OlServerUserInfo> lsUser = new List<EyouSoft.Model.OnLineServer.OlServerUserInfo>();
            EyouSoft.Model.OnLineServer.OlServerUserInfo model = null;
            string sql = "select Id,UserId,UserName,OlName,IsService,LoginTime,AcceptId,AcceptName,IsOnline,CompanyId,LastSendMessageTime from OL_ServerUsers"
                        + " where AcceptId = @AcceptId and IsService = '0' and  IsOnline = '1' and CompanyId = (select olsu.CompanyId from OL_ServerUsers as olsu where olsu.Id = @AcceptId) ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, olServiceId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerUserInfo();
                    model.CompanyId = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    model.OId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.UserId = rdr.IsDBNull(rdr.GetOrdinal("UserId")) ? "" : rdr.GetString(rdr.GetOrdinal("UserId"));
                    model.UserName = rdr.IsDBNull(rdr.GetOrdinal("UserName")) ? "" : rdr.GetString(rdr.GetOrdinal("UserName"));
                    model.OlName = rdr.IsDBNull(rdr.GetOrdinal("OlName")) ? "" : rdr.GetString(rdr.GetOrdinal("OlName"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("IsService")))
                    {
                        if (rdr.GetString(rdr.GetOrdinal("IsService")) == "1" || rdr.GetString(rdr.GetOrdinal("IsService")).ToLower() == "true")
                            model.IsService = true;
                        else
                            model.IsService = false;
                    }
                    model.LoginTime = rdr.GetDateTime(rdr.GetOrdinal("LoginTime"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.IsOnline = rdr.GetString(rdr.GetOrdinal("IsOnline")) == "1" ? true : false;
                    model.LastSendMessageTime = rdr.IsDBNull(rdr.GetOrdinal("LastSendMessageTime")) ? DateTime.Now : rdr.GetDateTime(rdr.GetOrdinal("LastSendMessageTime"));
                    #endregion
                    lsUser.Add(model);
                }
            }

            return lsUser;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="info">用户信息</param>
        /// <returns></returns>
        public virtual bool UpdateUserInfo(EyouSoft.Model.OnLineServer.OlServerUserInfo info)
        {
            string sql = "update OL_ServerUsers set UserId = @UserId,UserName = @UserName,OlName = @OlName,IsService = @IsService,"
                        + " LoginTime = @LoginTime,AcceptId = @AcceptId,AcceptName = @AcceptName,IsOnline = @IsOnline,CompanyId = @CompanyId "
                        + " where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.OId);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, info.UserId);
            this._db.AddInParameter(cmd, "UserName", DbType.String, info.UserName);
            this._db.AddInParameter(cmd, "OlName", DbType.String, info.OlName);
            this._db.AddInParameter(cmd, "IsService", DbType.String, info.IsService ? "1":"0");
            this._db.AddInParameter(cmd, "LoginTime", DbType.DateTime, info.LoginTime);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, info.AcceptId);
            this._db.AddInParameter(cmd, "AcceptName", DbType.String, info.AcceptName);
            this._db.AddInParameter(cmd, "IsOnline", DbType.String, info.IsOnline ? "1":"0");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            //this._db.AddInParameter(cmd, "LastSendMessageTime", DbType.DateTime, info.LastSendMessageTime);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 插入用户信息，返回当前公司游客总数
        /// </summary>
        /// <param name="info">用户信息</param>
        public virtual int InserUserInfo(EyouSoft.Model.OnLineServer.OlServerUserInfo info)
        {
            if (info == null)
                return 0;

            info.OId = Guid.NewGuid().ToString();
            string sql = "insert into OL_ServerUsers (Id,UserId,UserName,OlName,IsService,LoginTime,AcceptId,AcceptName,IsOnline,CompanyId)"
                        + " values (@Id,@UserId,@UserName,@OlName,@IsService,@LoginTime,@AcceptId,@AcceptName,@IsOnline,@CompanyId);"
                        + " select count(*) from OL_ServerUsers where IsService = '0'";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, info.OId);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, info.UserId);
            this._db.AddInParameter(cmd, "UserName", DbType.String, info.UserName);
            this._db.AddInParameter(cmd, "OlName", DbType.String, info.OlName);
            this._db.AddInParameter(cmd, "IsService", DbType.String, info.IsService ? "1":"0");
            this._db.AddInParameter(cmd, "LoginTime", DbType.DateTime, info.LoginTime);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, info.AcceptId);
            this._db.AddInParameter(cmd, "AcceptName", DbType.String, info.AcceptName);
            this._db.AddInParameter(cmd, "IsOnline", DbType.String, info.IsOnline ? "1":"0");
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, info.CompanyId);
            //this._db.AddInParameter(cmd, "LastSendMessageTime", DbType.DateTime, info.LastSendMessageTime);
            object obj = DbHelper.GetSingle(cmd, this._db);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 设置客服
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="serviceId">客服在线编号</param>
        /// <param name="serviceName">客服在线名称</param>
        /// <returns></returns>
        public virtual bool SetService(string olId, string serviceId, string serviceName)
        {
            string sql = "update OL_ServerUsers set AcceptId = @AcceptId,AcceptName = @AcceptName where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, serviceId);
            this._db.AddInParameter(cmd, "AcceptName", DbType.String, serviceName);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 写入消息,返回写入的消息编号
        /// </summary>
        /// <param name="messageInfo">Model.OnLineServer.OlServerMessageInfo</param>
        /// <returns></returns>
        public virtual bool InsertMessage(EyouSoft.Model.OnLineServer.OlServerMessageInfo messageInfo)
        {
            if (messageInfo == null)
                return false;

            messageInfo.MessageId = Guid.NewGuid().ToString();
            string sql = "insert into OL_ServerMessage (Id,SendId,SendName,AcceptId,AcceptName,[Message],SendTime) values "
                        + " (@Id,@SendId,@SendName,@AcceptId,@AcceptName,@Message,@SendTime)";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, messageInfo.MessageId);
            this._db.AddInParameter(cmd, "SendId", DbType.AnsiStringFixedLength, messageInfo.SendId);
            this._db.AddInParameter(cmd, "SendName", DbType.String, messageInfo.SendName);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, messageInfo.AcceptId);
            this._db.AddInParameter(cmd, "AcceptName", DbType.String, messageInfo.AcceptName);
            this._db.AddInParameter(cmd, "Message", DbType.String, messageInfo.Message);
            this._db.AddInParameter(cmd, "SendTime", DbType.DateTime, messageInfo.SendTime);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据最后发送消息时间获取最后发送消息时间以后发给自己的所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="LastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId, DateTime? LastSendMessageTime)
        {
            IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> lsMsg = new List<EyouSoft.Model.OnLineServer.OlServerMessageInfo>();
            EyouSoft.Model.OnLineServer.OlServerMessageInfo model = null;
            string sql = "select Id,SendId,SendName,AcceptId,AcceptName,[Message],SendTime from OL_ServerMessage where AcceptId = @AcceptId {0}";
            if (LastSendMessageTime.HasValue)
            {
                sql = string.Format(sql, string.Format(" and datediff(ss,SendTime,'{0}')<0", LastSendMessageTime.Value.ToString()));
            }
            else
            {
                sql = string.Format(sql, "");
            }
            sql += " order by SendTime asc ";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, olId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerMessageInfo();
                    model.MessageId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.SendId = rdr.IsDBNull(rdr.GetOrdinal("SendId")) ? "" : rdr.GetString(rdr.GetOrdinal("SendId"));
                    model.SendName = rdr.IsDBNull(rdr.GetOrdinal("SendName")) ? "" : rdr.GetString(rdr.GetOrdinal("SendName"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.Message = rdr.IsDBNull(rdr.GetOrdinal("Message")) ? "" : rdr.GetString(rdr.GetOrdinal("Message"));
                    model.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    #endregion
                    lsMsg.Add(model);
                }
            }

            return lsMsg;
        }

        /// <summary>
        /// 获取登录后所有消息
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(string olId)
        {
            IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> lsMsg = new List<EyouSoft.Model.OnLineServer.OlServerMessageInfo>();
            EyouSoft.Model.OnLineServer.OlServerMessageInfo model = null;
            string sql = "select Id,SendId,SendName,AcceptId,AcceptName,[Message],SendTime from OL_ServerMessage where "
                        + " (AcceptId = @AcceptId or SendId = @SendId) and SendTime > (select LoginTime from OL_ServerUsers where Id = @Id)"
                        + " order by SendTime asc";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, olId);
            this._db.AddInParameter(cmd, "SendId", DbType.AnsiStringFixedLength, olId);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerMessageInfo();
                    model.MessageId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.SendId = rdr.IsDBNull(rdr.GetOrdinal("SendId")) ? "" : rdr.GetString(rdr.GetOrdinal("SendId"));
                    model.SendName = rdr.IsDBNull(rdr.GetOrdinal("SendName")) ? "" : rdr.GetString(rdr.GetOrdinal("SendName"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.Message = rdr.IsDBNull(rdr.GetOrdinal("Message")) ? "" : rdr.GetString(rdr.GetOrdinal("Message"));
                    model.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    #endregion
                    lsMsg.Add(model);
                }
            }

            return lsMsg;
        }

        /// <summary>
        /// 根据指定的在线编号设置在线状态为false[用户退出]
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public virtual bool Exit(string olId)
        {
            string sql = "update OL_ServerUsers set IsOnline = '0' where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 客服退出时设置与之会话的客户的默认服务客服信息为无
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public virtual bool ExitSetDefaultService(string olId)
        {
            string sql = "update OL_ServerUsers set AcceptId = '',AcceptName = '' where AcceptId = @AcceptId and IsOnline = '1' and IsService = '0'";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, olId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据指定在线编号设置用户最后发送消息时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <param name="lastSendMessageTime">最后发送消息时间</param>
        /// <returns></returns>
        public virtual bool SetLastSendMessageTime(string olId, DateTime lastSendMessageTime)
        {
            string sql = "update OL_ServerUsers set LastSendMessageTime = @LastSendMessageTime where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "LastSendMessageTime", DbType.DateTime, lastSendMessageTime);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 清理过了指定停留时间的用户在线状态为不在线（不含客服人员）
        /// </summary>
        /// <param name="stayTime">停留时间 单位：分钟</param>
        /// <returns></returns>
        public virtual bool ClearUserOut(int stayTime)
        {
            string sql = "update OL_ServerUsers set IsOnline = '0' where IsOnline = '1' and IsService = '0' and datediff(n,LastSendMessageTime,getdate()) > @stayTime";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "stayTime", DbType.Int32, stayTime);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 按条件分页获得聊天记录
        /// </summary>
        /// <param name="StartTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <param name="LikeInfo">消息内容包含的文字</param>
        /// <param name="pageSize">单页显示的数量</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordSum">总记录数量</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> GetMessages(DateTime? StartTime, DateTime? EndTime, string LikeInfo, int pageSize, int pageIndex, ref int recordSum)
        {
            IList<EyouSoft.Model.OnLineServer.OlServerMessageInfo> lsMsg = new List<EyouSoft.Model.OnLineServer.OlServerMessageInfo>();
            EyouSoft.Model.OnLineServer.OlServerMessageInfo model = null;

            string tableName = "OL_ServerMessage";
            string primaryKey = "Id";
            string orderByString = "SendTime asc";
            string fields = " Id,SendId,SendName,AcceptId,AcceptName,[Message],SendTime";

            StringBuilder cmdQuery = new StringBuilder();
            if (StartTime != null && StartTime != DateTime.MaxValue && StartTime != DateTime.MinValue)
                cmdQuery.AppendFormat(" and datediff(d,'{0}',SendTime) >= 0", StartTime);
            if (EndTime != null && EndTime != DateTime.MaxValue && EndTime != DateTime.MinValue)
                cmdQuery.AppendFormat(" and datediff(d,'{0}',SendTime) <= 0", EndTime);
            if (!string.IsNullOrEmpty(LikeInfo))
                cmdQuery.AppendFormat(" and [Message] like '%{0}%'", LikeInfo);

            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordSum, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    #region 基本信息
                    model = new EyouSoft.Model.OnLineServer.OlServerMessageInfo();
                    model.MessageId = rdr.GetString(rdr.GetOrdinal("Id"));
                    model.SendId = rdr.IsDBNull(rdr.GetOrdinal("Id")) ? "" : rdr.GetString(rdr.GetOrdinal("SnedId"));
                    model.SendName = rdr.IsDBNull(rdr.GetOrdinal("SendName")) ? "" : rdr.GetString(rdr.GetOrdinal("SendName"));
                    model.AcceptId = rdr.IsDBNull(rdr.GetOrdinal("AcceptId")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptId"));
                    model.AcceptName = rdr.IsDBNull(rdr.GetOrdinal("AcceptName")) ? "" : rdr.GetString(rdr.GetOrdinal("AcceptName"));
                    model.Message = rdr.IsDBNull(rdr.GetOrdinal("Message")) ? "" : rdr.GetString(rdr.GetOrdinal("Message"));
                    model.SendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                    #endregion
                    lsMsg.Add(model);
                }
            }

            return lsMsg;
        }

        /// <summary>
        /// 根据id删除聊天记录
        /// </summary>
        /// <param name="MessagesId">聊天记录ID</param>
        /// <returns></returns>
        public virtual bool DeleteMessages(string MessagesId)
        {
            string sql = "delete from OL_ServerMessage where Id = @Id";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, MessagesId);
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据UserId删除在线客服用户信息
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="IsService">是否为客服</param>
        /// <returns></returns>
        public virtual bool DeleteUserInfoByUserId(string UserId, bool IsService)
        {
            string sql = "delete from OL_ServerUsers where UserId = @UserId and IsService = @IsService";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, UserId);
            this._db.AddInParameter(cmd, "IsService", DbType.String, IsService ? "1":"0");
            return DbHelper.ExecuteSql(cmd, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据在线编号获取此在线用户最后发送消息的时间
        /// </summary>
        /// <param name="olId">在线编号</param>
        /// <returns></returns>
        public virtual DateTime? GetLastSendMessageTime(string olId)
        {
            DateTime lastSendTime = DateTime.Now;
            string sql = "select top 1 SendTime from OL_ServerMessage where SendTime >= (select LoginTime from OL_ServerUsers where Id = @Id) "
                        + " and (SendId = @SendId or AcceptId = @AcceptId) order by SendTime desc";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, olId);
            this._db.AddInParameter(cmd, "SendId", DbType.AnsiStringFixedLength, olId);
            this._db.AddInParameter(cmd, "AcceptId", DbType.AnsiStringFixedLength, olId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    lastSendTime = rdr.GetDateTime(rdr.GetOrdinal("SendTime"));
                }
            }
            return lastSendTime;
        }

        /// <summary>
        /// 根据用户Id获取该用户所有未读消息的记录数
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public virtual int GetMessageCountByUserId(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                return 0;

            string strSql = @" select count(*) from OL_ServerMessage inner join OL_ServerUsers on OL_ServerMessage.AcceptId = OL_ServerUsers.Id where OL_ServerUsers.UserId = @UserId and datediff(ss,OL_ServerMessage.SendTime,isnull(OL_ServerUsers.LastSendMessageTime,OL_ServerUsers.LoginTime)) < 0 ";

            DbCommand dc = this._db.GetSqlStringCommand(strSql);
            this._db.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, UserId);

            object obj = DbHelper.GetSingle(dc, this._db);
            if (obj != null)
                return Convert.ToInt32(obj);
            else
                return 0;
        }

        #endregion

    }
}
