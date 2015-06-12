using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.SSOComponent;
using EyouSoft.SSOComponent.Entity;
using EyouSoft.Common.DAL;
using EyouSoft.Cache.Facade;
using EyouSoft.CacheTag;
using System.Web.Configuration;

namespace EyouSoft.SSOComponent
{
    /// <summary>
    /// 管理员登陆处理
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-6-1
    public class MaterLogin : EyouSoft.Common.DAL.DALBase, IMasterLogin
    {

        readonly string SQL_MASTERUSERLOGIN = "SELECT TOP 1 ID,UserName,ContactName,ContactTel,ContactFax,ContactMobile,PermissionList,IsDisable,IsAdmin," +
            "(SELECT AreaId FROM tbl_SysUserAreaControl WHERE UserId=a.Id for xml path,root('root')) AreaId FROM tbl_SystemUser a WHERE UserName=@UID AND MD5Password=@PWD";

        /// <summary>
        /// 查询用户所能查看的易诺用户池客户类型
        /// </summary>
        private readonly string SQL_MASTERUSERLOGIN_Expand = " SELECT [UserId],[CustomerId] FROM [tbl_UserCustomerType] where UserId = @ID; ";

        #region IMasterLogin 成员
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <returns>管理员信息</returns>
        public MasterUserInfo MasterLogin(string UserName, string PWD)
        {
            string AreaXml = "";
            string tmpPermissionList = "";
            MasterUserInfo User = null;
            #region 用户查询
            DbCommand dc = this.UserStore.GetSqlStringCommand(SQL_MASTERUSERLOGIN);
            this.UserStore.AddInParameter(dc, "UID", DbType.String, UserName);
            this.UserStore.AddInParameter(dc, "PWD", DbType.String, PWD);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                if (rdr.Read())
                {
                    User = new MasterUserInfo();
                    User.ID = rdr.GetInt32(rdr.GetOrdinal("ID"));
                    User.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
                    User.ContactFax = rdr.GetString(rdr.GetOrdinal("ContactFax"));
                    User.ContactMobile = rdr.GetString(rdr.GetOrdinal("ContactMobile"));
                    User.ContactName = rdr.GetString(rdr.GetOrdinal("ContactName"));                    
                    User.ContactTel = rdr.GetString(rdr.GetOrdinal("ContactTel"));
                    User.IsDisable = rdr.GetString(rdr.GetOrdinal("IsDisable")) == "1" ? true : false;
                    User.IsAdmin = rdr.GetString(rdr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    tmpPermissionList = rdr.IsDBNull(rdr.GetOrdinal("PermissionList")) ? "" : rdr.GetString(rdr.GetOrdinal("PermissionList"));
                    AreaXml = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? "" : rdr.GetString(rdr.GetOrdinal("AreaId"));
                }
            }
            if (User != null)
            {
                if (!String.IsNullOrEmpty(tmpPermissionList))
                {
                    string[] PermissionList = tmpPermissionList.Split(',');
                    User.PermissionList = new int[PermissionList.Length+1];
                    for (int i = 0; i < PermissionList.Length; i++)
                    {
                        User.PermissionList[i] = int.Parse(PermissionList[i]);
                    }
                    //给所有运营后台用户加上随便逛逛用户的管理权限
                    User.PermissionList[User.PermissionList.Length - 1] = 0;
                }
                if (!String.IsNullOrEmpty(AreaXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(AreaXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("AreaId");
                    User.AreaId = new int[NodeList.Count + 1];
                    User.AreaId[0] = 0;//随便逛逛身份没有城市
                    for (int i = 1; i <= NodeList.Count; i++)
                    {
                        User.AreaId[i] = int.Parse(NodeList[i - 1].FirstChild.Value);
                    }
                }

                #region 获取用户所能查看的易诺用户池客户类型

                //获取用户所能查看的易诺用户池客户类型
                GetMasterUserCustomerType(ref User);

                #endregion

                UpdateMasterInfo(User);
            }
            #endregion
            return User;
        }
        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <param name="UID">用户名</param>
        /// <returns></returns>
        public void MasterLogout(string UID)
        {
            //RemoveCache();
            EyouSoftSSOCache.Remove(CacheTag.System.SystemUser + UID);
            return;
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="UID">用户名</param>
        /// <returns>管理员信息</returns>
        public MasterUserInfo GetMasterInfo(string UID)
        {
            MasterUserInfo User = (MasterUserInfo)EyouSoftSSOCache.GetCache(CacheTag.System.SystemUser + UID); ;
            //GetCache();
            return User;
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="User">用户信息</param>
        public void UpdateMasterInfo(MasterUserInfo User)
        {
            //SetCache();
            EyouSoftSSOCache.Add(CacheTag.System.SystemUser + User.UserName, User, DateTime.Now.AddHours(12));
            return;
        }

        #endregion

        /// <summary>
        /// 获取用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="model">管理用户实体</param>
        private void GetMasterUserCustomerType(ref MasterUserInfo model)
        {
            if (model == null || model.ID <= 0)
                return;

            model.CustomerTypeIds = new List<int>();
            DbCommand dc = this.UserStore.GetSqlStringCommand(SQL_MASTERUSERLOGIN_Expand);
            this.PoolStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.PoolStore))
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CustomerId")))
                        model.CustomerTypeIds.Add(int.Parse(rdr["CustomerId"].ToString()));
                }
            }
        }
    }

    /// <summary>
    /// 用户登陆处理(处理本地资源方式)
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-6-1
    public class UserLogin : EyouSoft.Common.DAL.DALBase, IUserLogin
    {
        readonly string SQL_USERLOGIN = "SELECT TOP 1 Id, OPUserId, CompanyId, ProvinceId, CityId, UserName, ContactName, ContactSex, ContactTel, ContactFax, ContactMobile, ContactEmail,"
            + " QQ, MQ, MSN, IsEnable, IsAdmin, DepartId, DepartName,Password,EncryptPassword,MD5Password,"
            + "(SELECT TOP 1 CompanyName FROM tbl_CompanyInfo WHERE ID=a.CompanyId) AS CompanyName,"
            + "(SELECT TOP 1 PermissionList FROM tbl_CompanyRoles WHERE ID=a.RoleID) AS PermissionList,"
            + "(SELECT AreaId FROM tbl_CompanyUserAreaControl WHERE UserId=a.Id for xml path,root('root')) AreaId,"
            + "(SELECT TypeId FROM tbl_CompanyTypeList WHERE CompanyId=a.CompanyId for xml path,root('root')) TypeId FROM tbl_CompanyUser a WHERE IsDeleted=0 ";
        readonly string SQL_ONLINELOGIN = "DELETE [tbl_SysOnlineUser] WHERE [UserId]=@UserId;INSERT INTO [tbl_SysOnlineUser]([ID], [UserId], [CompanyId], [LoginTime]) VALUES(@ID, @UserId, @CompanyId, GETDATE());";
        readonly string SQL_USERLOGIN_LOG = "UPDATE tbl_CompanyUser SET LastLoginIP=@LastLoginIP,LastLoginTime=GETDATE() WHERE [Id]=@UserId;UPDATE [tbl_CompanyInfo] SET [LoginCount]=[LoginCount]+1,LastLoginTime=GETDATE() WHERE [Id]=@CompanyId;";
        const string SQL_WRITE_LOG = "INSERT INTO tbl_LogUserLogin([EventID],[CompanyId],[OperatorId],[OperatorName],[ContactName],[EventArea],[EventTitle],[EventMessage],[EventCode],[EventUrl],[EventIP]) VALUES(" +
                                    "@EventID,@CompanyId,@OperatorId,@OperatorName,@ContactName,@EventArea,@EventTitle,@EventMessage,@EventCode,@EventUrl,@EventIP)";

        readonly string CustomServicePwd = WebConfigurationManager.AppSettings["CustomerServicePwd"] != null ? WebConfigurationManager.AppSettings["CustomerServicePwd"] : "";
        #region IUserLogin 成员
        private UserInfo ReadUserInfo(DbCommand dc)
        {
            #region 用户查询
            string AreaXml = "";
            string CompanyRoleXml = "";
            string tmpPermissionList = "";
            UserInfo User = null;

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.UserStore))
            {
                if (rdr.Read())
                {
                    User = new UserInfo();
                    User.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
                    User.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    User.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    User.CompanyName = rdr.IsDBNull(rdr.GetOrdinal("CompanyName")) ? "" : rdr.GetString(rdr.GetOrdinal("CompanyName"));
                    User.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    User.ContactInfo.Email = rdr.IsDBNull(rdr.GetOrdinal("ContactEmail")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactEmail"));
                    User.ContactInfo.Fax = rdr.IsDBNull(rdr.GetOrdinal("ContactFax")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactFax"));
                    User.ContactInfo.Mobile = rdr.IsDBNull(rdr.GetOrdinal("ContactMobile")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactMobile"));
                    User.ContactInfo.ContactName = rdr.IsDBNull(rdr.GetOrdinal("ContactName")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactName"));
                    User.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(rdr.GetString(rdr.GetOrdinal("ContactSex")));
                    User.ContactInfo.Tel = rdr.IsDBNull(rdr.GetOrdinal("ContactTel")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactTel"));
                    User.ContactInfo.QQ = rdr.IsDBNull(rdr.GetOrdinal("QQ")) ? "" : rdr.GetString(rdr.GetOrdinal("QQ"));
                    User.ContactInfo.MQ = rdr.IsDBNull(rdr.GetOrdinal("MQ")) ? "" : rdr.GetString(rdr.GetOrdinal("MQ"));
                    User.DepartId = rdr.IsDBNull(rdr.GetOrdinal("DepartId")) ? "" : rdr.GetString(rdr.GetOrdinal("DepartId"));
                    User.DepartName = rdr.IsDBNull(rdr.GetOrdinal("DepartName")) ? "" : rdr.GetString(rdr.GetOrdinal("DepartName"));
                    User.ID = rdr.GetString(rdr.GetOrdinal("ID"));
                    User.IsAdmin = rdr.GetString(rdr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    User.IsEnable = rdr.GetString(rdr.GetOrdinal("IsEnable")) == "1" ? true : false;
                    User.ContactInfo.MQ = rdr.IsDBNull(rdr.GetOrdinal("MQ")) ? "" : rdr.GetString(rdr.GetOrdinal("MQ"));
                    User.ContactInfo.MSN = rdr.IsDBNull(rdr.GetOrdinal("MSN")) ? "" : rdr.GetString(rdr.GetOrdinal("MSN"));
                    User.OpUserId = rdr.GetInt32(rdr.GetOrdinal("OpUserId"));
                    User.PassWordInfo.SetEncryptPassWord(rdr.IsDBNull(rdr.GetOrdinal("Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("Password")), rdr.IsDBNull(rdr.GetOrdinal("EncryptPassword")) == true ? "" : rdr.GetString(rdr.GetOrdinal("EncryptPassword")), rdr.IsDBNull(rdr.GetOrdinal("MD5Password")) == true ? "" : rdr.GetString(rdr.GetOrdinal("MD5Password")));
                    //User.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.地接);
                    tmpPermissionList = rdr.IsDBNull(rdr.GetOrdinal("PermissionList")) ? "" : rdr.GetString(rdr.GetOrdinal("PermissionList"));
                    AreaXml = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? "" : rdr.GetString(rdr.GetOrdinal("AreaId"));
                    CompanyRoleXml = rdr.IsDBNull(rdr.GetOrdinal("TypeId")) ? "" : rdr.GetString(rdr.GetOrdinal("TypeId"));
                }
            }
            if (User != null)
            {
                if (!String.IsNullOrEmpty(tmpPermissionList))
                {
                    string[] PermissionList = tmpPermissionList.Split(',');
                    User.PermissionList = new int[PermissionList.Length];
                    for (int i = 0; i < PermissionList.Length; i++)
                    {
                        if (EyouSoft.Common.Function.StringValidate.IsInteger(PermissionList[i]))
                        {
                            User.PermissionList[i] = int.Parse(PermissionList[i]);
                        }
                    }
                }
                else
                {
                    User.PermissionList = new int[1] { -1 };
                }
                if (!String.IsNullOrEmpty(AreaXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(AreaXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("AreaId");
                    User.AreaId = new int[NodeList.Count];
                    for (int i = 0; i < NodeList.Count; i++)
                    {
                        if (EyouSoft.Common.Function.StringValidate.IsInteger(NodeList[i].FirstChild.Value))
                        {
                            User.AreaId[i] = int.Parse(NodeList[i].FirstChild.Value);
                        }
                    }
                }

                else
                {
                    User.AreaId = new int[1] { -1 };
                }
                if (!String.IsNullOrEmpty(CompanyRoleXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(CompanyRoleXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("TypeId");

                    for (int i = 0; i < NodeList.Count; i++)
                    {
                        if (EyouSoft.Common.Function.StringValidate.IsInteger(NodeList[i].FirstChild.Value))
                        {
                            User.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(NodeList[i].FirstChild.Value));
                        }
                    }
                }

                #region 写登录日志

                if (!dc.Parameters.Contains("MQID"))
                {
                    dc.Parameters.Clear();
                    string GetRemoteIP = EyouSoft.Common.Utility.GetRemoteIP();
                    string GetRemoteArea = this.GetClientArea(GetRemoteIP);
                    string RequestUrl = EyouSoft.Common.Utility.GetRequestUrl();

                    dc = this.SystemStore.GetSqlStringCommand(SQL_USERLOGIN_LOG);
                    this.SystemStore.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, User.ID);
                    this.SystemStore.AddInParameter(dc, "LastLoginIP", DbType.String, EyouSoft.Common.Utility.GetRemoteIP());
                    this.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, User.CompanyID);
                    DbHelper.ExecuteSql(dc, this.SystemStore);

                    WriteLog(User.CompanyID, User.ID, User.UserName, User.ContactInfo.ContactName, GetRemoteArea,
                        "用户登录", "用户" + User.UserName + "于" + DateTime.Now.ToString() + "登陆系统", RequestUrl, GetRemoteIP);

                }
                #endregion

                #region 写入组团在线用户
                if (User.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    dc.Parameters.Clear();
                    dc = this.SystemStore.GetSqlStringCommand(SQL_ONLINELOGIN);
                    this.SystemStore.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
                    this.SystemStore.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, User.ID);
                    this.SystemStore.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, User.CompanyID);
                    DbHelper.ExecuteSql(dc, this.SystemStore);
                }
                #endregion
            }
            return User;
            #endregion
        }
        /// <summary>
        /// 平台用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns>用户信息</returns> 
        private UserInfo UserLoginFunc(string UserName, string PWD, PasswordType PwdType)
        {           
            #region 用户查询
            string strWhere = "";
            if (PwdType == PasswordType.MD5 && PWD == CustomServicePwd && !String.IsNullOrEmpty(CustomServicePwd))/*客服密码*/
            {
                strWhere = SQL_USERLOGIN + " AND UserName=@UID";
            }
            else if (PwdType == PasswordType.SHA)
            {
                strWhere = SQL_USERLOGIN + " AND UserName=@UID AND EncryptPassword=@PWD";
            }
            else if (PwdType == PasswordType.MD5)
            {
                strWhere = SQL_USERLOGIN + " AND UserName=@UID AND MD5Password=@PWD";
            }
            DbCommand dc = this.UserStore.GetSqlStringCommand(strWhere);
            this.UserStore.AddInParameter(dc, "UID", DbType.AnsiStringFixedLength, UserName);
            this.UserStore.AddInParameter(dc, "PWD", DbType.String, PWD);            
            #endregion
            return ReadUserInfo(dc);
        }
        /// <summary>
        /// 平台用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <returns>用户信息</returns>        
        public UserInfo UserLoginAct(string UserName, string PWD, string LoginTicket)
        {
            UserInfo User = null;
            if (PWD == string.Empty)
            {
                User = GetUserInfo(UserName);
                if (User == null || User.LoginTicket != LoginTicket)
                {
                    User = null;
                }
            }
            else
            {
                User = UserLoginFunc(UserName, PWD, PasswordType.SHA);
                if (User != null)
                {
                    User.LoginTicket = LoginTicket;
                    if(User.IsEnable)
                        UpdateUserInfo(User);
                }
            }
            return User;
        }
        /// <summary>
        /// 平台用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns>用户信息</returns> 
        public UserInfo UserLoginAct(string UserName, string PWD, string LoginTicket, PasswordType PwdType)
        {
            UserInfo User = UserLoginFunc(UserName, PWD, PwdType);
            if (User != null)
            {
                User.LoginTicket = LoginTicket;
                if(User.IsEnable)
                    UpdateUserInfo(User);
            }
            return User;
        }
        /// <summary>
        /// MQ用户登录
        /// </summary>
        /// <param name="MQID">MQID</param>
        /// <param name="PWD">用户密码</param>
        /// <returns></returns>
        public UserInfo UserLoginAct(int MQID, string PWD)
        {
            #region 用户查询
            string strWhere = "";
            strWhere = SQL_USERLOGIN + " AND MQ=@MQID ";
            if (!string.IsNullOrEmpty(PWD))
                strWhere += " AND MD5Password=@PWD";
            DbCommand dc = this.UserStore.GetSqlStringCommand(strWhere);
            this.UserStore.AddInParameter(dc, "MQID", DbType.AnsiString, MQID.ToString());
            if (!string.IsNullOrEmpty(PWD))
                this.UserStore.AddInParameter(dc, "PWD", DbType.String, PWD);
            #endregion
            UserInfo User = ReadUserInfo(dc);
            if (User != null)
                EyouSoft.Cache.Facade.EyouSoftSSOCache.Add(EyouSoft.CacheTag.MQ.MQUser + MQID.ToString(), User, DateTime.Now.AddHours(12));
            return User;

        }
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="model"></param>
        private void WriteLog(string CompanyId, string OperatorId, string OperatorName, string ContactName
            , string EventArea, string EventTitle, string EventMessage, string EventUrl, string EventIP
            )
        {
            DbCommand dc = this.LogStore.GetSqlStringCommand(SQL_WRITE_LOG);
            this.LogStore.AddInParameter(dc, "EventID", DbType.String, Guid.NewGuid().ToString());
            this.LogStore.AddInParameter(dc, "CompanyId", DbType.String, CompanyId);
            this.LogStore.AddInParameter(dc, "OperatorId", DbType.String, OperatorId);
            this.LogStore.AddInParameter(dc, "OperatorName", DbType.String, OperatorName);
            this.LogStore.AddInParameter(dc, "ContactName", DbType.String, ContactName);
            this.LogStore.AddInParameter(dc, "EventArea", DbType.String, EventArea);
            this.LogStore.AddInParameter(dc, "EventTitle", DbType.String, EventTitle);
            this.LogStore.AddInParameter(dc, "EventMessage", DbType.String, EventMessage);
            this.LogStore.AddInParameter(dc, "EventCode", DbType.Int32, 0);
            this.LogStore.AddInParameter(dc, "EventUrl", DbType.String, EventUrl);
            this.LogStore.AddInParameter(dc, "EventIP", DbType.String, EventIP);
            DbHelper.ExecuteSql(dc, this.LogStore);
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>是否成功</returns>
        public bool UserLogout(string UID)
        {
            //RemoveCache();
            EyouSoftSSOCache.Remove(CacheTag.Company.CompanyUser + UID.ToLower());
            return true;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>用户信息</returns>
        public UserInfo GetUserInfo(string UID)
        {
            //GetCache();
            UserInfo User = (UserInfo)EyouSoftSSOCache.GetCache(CacheTag.Company.CompanyUser + UID.ToLower());
            return User;
        }
        /// <summary>
        /// 获取MQ用户信息
        /// </summary>
        /// <param name="UID">MQID</param>
        /// <returns>用户信息</returns>
        public UserInfo GetMQUserInfo(int MQID)
        {
            //GetCache();
            UserInfo User = (UserInfo)EyouSoftSSOCache.GetCache(CacheTag.MQ.MQUser + MQID.ToString());
            return User;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        public void UpdateUserInfo(UserInfo User)
        {
            //SetCache();
            EyouSoftSSOCache.Remove(CacheTag.Company.CompanyUser + User.UserName.ToLower());
            EyouSoftSSOCache.Add(CacheTag.Company.CompanyUser + User.UserName.ToLower(), User, DateTime.Now.AddHours(12));
            return;
        }
        /// <summary>
        /// 根据客户端Ip地址返回地区
        /// </summary>
        /// <param name="IpAddress">客户端Ip地址</param>
        /// <returns>城市基类实体</returns>
        private string GetClientArea(string IpAddress)
        {
            if (string.IsNullOrEmpty(IpAddress))
                return null;

            long Ip = EyouSoft.Common.Utility.IpToLong(IpAddress);
            if (Ip <= 0)
                return null;

            string strSql = " SELECT IpAddress+Remark FROM tbl_SystemIP WHERE @Ip BETWEEN IpStartNum AND IpEndNum ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            base.SystemStore.AddInParameter(dc, "Ip", DbType.Decimal, Ip);
            object returnValue = DbHelper.GetSingle(dc, base.SystemStore);
            if (returnValue != null && returnValue != DBNull.Value)
            {
                return returnValue.ToString();
            }
            return null;
        }
        #endregion
    }
}

