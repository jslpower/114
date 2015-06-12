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

namespace EyouSoft.SSOComponent
{
    /// <summary>
    /// 管理员登陆处理
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-6-1
    public class MaterLogin : EyouSoft.Common.DAL.DALBase, IMasterLogin
    {

        readonly string SQL_MASTERUSERLOGIN = "SELECT TOP 1 ID,UserName,ContactName,ContactTel,ContactFax,ContactMobile,PermissionList,IsDisable," +
            "(SELECT AreaId FROM tbl_SysUserAreaControl WHERE UserId=a.Id for xml path,root('root')) AreaId FROM tbl_SystemUser a WHERE UserName=@UID AND EncryptPassword=@PWD";

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
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.UserStore))
            {
                if (rdr.Read())
                {
                    User = new MasterUserInfo();
                    User.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
                    User.ContactFax = rdr.GetString(rdr.GetOrdinal("ContactFax"));
                    User.ContactMobile = rdr.GetString(rdr.GetOrdinal("ContactMobile"));
                    User.ContactName = rdr.GetString(rdr.GetOrdinal("ContactName"));                    
                    User.ContactTel = rdr.GetString(rdr.GetOrdinal("ContactTel"));
                    User.IsDisable = rdr.GetString(rdr.GetOrdinal("IsDisable")) == "1" ? true : false;
                    tmpPermissionList = rdr.GetString(rdr.GetOrdinal("PermissionList")).ToString();
                    AreaXml = rdr.GetString(rdr.GetOrdinal("AreaId")).ToString();
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
                        User.PermissionList[i] = int.Parse(PermissionList[i]);
                    }
                }
                if (!String.IsNullOrEmpty(AreaXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(AreaXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("AreaId");
                    User.AreaId = new int[NodeList.Count];
                    for (int i = 0; i < NodeList.Count; i++)
                    {
                        User.AreaId[i] = int.Parse(NodeList[i].FirstChild.Value);
                    }
                }
                UpdateMasterInfo(User);
            }
            #endregion
            return User;
        }
        /// <summary>
        /// 管理员退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <param name="PWD">用户密码</param>
        /// <returns></returns>
        public void MasterLogout(string UID)
        {
            //RemoveCache();
            EyouSoftCache.Remove(CacheTag.System.SystemUser + UID);
            return;
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>管理员信息</returns>
        public MasterUserInfo GetMasterInfo(string UID)
        {
            MasterUserInfo User = (MasterUserInfo)EyouSoftCache.GetCache(CacheTag.System.SystemUser + UID); ;
            //GetCache();
            return User;
        }
        /// <summary>
        /// 更新管理员信息
        /// </summary>
        /// <param name="User">用户编号</param>
        public void UpdateMasterInfo(MasterUserInfo User)
        {
            //SetCache();
            EyouSoftCache.Add(CacheTag.System.SystemUser + User.ID, User);
            return;
        }

        #endregion
    }

    /// <summary>
    /// 用户登陆处理(处理本地资源方式)
    /// </summary>
    /// 开发人：蒋胜蓝  开发时间：2010-6-1
    public class UserLogin : EyouSoft.Common.DAL.DALBase, IUserLogin
    {
        readonly string SQL_USERLOGIN = "SELECT TOP 1 Id, OPUserId, CompanyId, ProvinceId, CityId, UserName, ContactName, ContactSex, ContactTel, ContactFax, ContactMobile, ContactEmail,"            
            + " QQ, MQ, MSN, IsEnable, IsAdmin, DepartId, DepartName,"
            + "(SELECT TOP 1 PermissionList FROM tbl_CompanyRoles WHERE ID=a.RoleID) AS PermissionList,"
            + "(SELECT AreaId FROM tbl_CompanyUserAreaControl WHERE UserId=a.Id for xml path,root('root')) AreaId,"
            + "(SELECT TypeId FROM tbl_CompanyTypeList WHERE CompanyId=a.CompanyId for xml path,root('root')) TypeId FROM tbl_CompanyUser a WHERE UserName=@UID ";
        readonly string SQL_ONLINELOGIN = "DELETE [tbl_SysOnlineUser] WHERE [UserId]=@UserId;INSERT INTO [tbl_SysOnlineUser]([ID], [UserId], [CompanyId], [LoginTime]) VALUES(@ID, @UserId, @CompanyId, GETDATE());";        
        readonly string SQL_USERLOGIN_LOG = "UPDATE tbl_CompanyUser SET LastLoginIP=@LastLoginIP,LastLoginTime=GETDATE() WHERE [Id]=@UserId";            

        #region IUserLogin 成员
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="PwdType">密码类型</param>
        /// <returns>用户信息</returns> 
        private UserInfo UserLoginFunc(string UserName, string PWD, PasswordType PwdType)
        {
            string AreaXml = "";
            string CompanyRoleXml = "";
            string tmpPermissionList = "";
            UserInfo User = null;
            #region 用户查询
            string strWhere = "";
            if (PwdType == PasswordType.SHA)
            {
                strWhere = SQL_USERLOGIN + " AND EncryptPassword=@PWD";
            }
            else if (PwdType == PasswordType.MD5)
            {
                strWhere = SQL_USERLOGIN + " AND MD5Password=@PWD";
            }
            DbCommand dc = this.UserStore.GetSqlStringCommand(SQL_USERLOGIN);
            this.UserStore.AddInParameter(dc, "UID", DbType.String, UserName);
            this.UserStore.AddInParameter(dc, "PWD", DbType.String, PWD);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.UserStore))
            {
                if (rdr.Read())
                {
                    User = new UserInfo();
                    User.UserName = rdr.GetString(rdr.GetOrdinal("UserName"));
                    User.ProvinceId = rdr.GetInt32(rdr.GetOrdinal("ProvinceId"));
                    User.CityId = rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    User.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    User.ContactInfo.Email = rdr.IsDBNull(rdr.GetOrdinal("ContactEmail")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactEmail"));
                    User.ContactInfo.Fax = rdr.IsDBNull(rdr.GetOrdinal("ContactFax")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactFax"));
                    User.ContactInfo.Mobile = rdr.IsDBNull(rdr.GetOrdinal("ContactMobile")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactMobile"));
                    User.ContactInfo.ContactName = rdr.IsDBNull(rdr.GetOrdinal("ContactName")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactName"));
                    User.ContactInfo.ContactSex = (EyouSoft.Model.CompanyStructure.Sex)int.Parse(rdr.GetString(rdr.GetOrdinal("ContactSex")));
                    User.ContactInfo.Tel = rdr.IsDBNull(rdr.GetOrdinal("ContactTel")) ? "" : rdr.GetString(rdr.GetOrdinal("ContactTel"));
                    User.DepartId = rdr.IsDBNull(rdr.GetOrdinal("DepartId")) ? "" : rdr.GetString(rdr.GetOrdinal("DepartId"));
                    User.DepartName = rdr.IsDBNull(rdr.GetOrdinal("DepartName"))? "" : rdr.GetString(rdr.GetOrdinal("DepartName"));
                    User.ID = rdr.GetString(rdr.GetOrdinal("ID"));
                    User.IsAdmin = rdr.GetString(rdr.GetOrdinal("IsAdmin")) == "1" ? true : false;
                    User.IsEnable = rdr.GetString(rdr.GetOrdinal("IsEnable")) == "1" ? true : false;
                    User.ContactInfo.MQ = rdr.IsDBNull(rdr.GetOrdinal("MQ")) ? "" : rdr.GetString(rdr.GetOrdinal("MQ"));
                    User.ContactInfo.MSN = rdr.IsDBNull(rdr.GetOrdinal("MSN")) ? "" : rdr.GetString(rdr.GetOrdinal("MSN"));
                    User.OpUserId = rdr.GetInt32(rdr.GetOrdinal("OpUserId"));
                    //User.CompanyRole.SetRole(EyouSoft.Model.CompanyStructure.CompanyType.地接);
                    tmpPermissionList = rdr.IsDBNull(rdr.GetOrdinal("PermissionList")) ? "" : rdr.GetString(rdr.GetOrdinal("PermissionList"));
                    AreaXml = rdr.IsDBNull(rdr.GetOrdinal("AreaId")) ? "" : rdr.GetString(rdr.GetOrdinal("AreaId")).ToString();
                    CompanyRoleXml = rdr.IsDBNull(rdr.GetOrdinal("TypeId")) ? "" : rdr.GetString(rdr.GetOrdinal("TypeId")).ToString();
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
                        User.PermissionList[i] = int.Parse(PermissionList[i]);
                    }
                }
                if (!String.IsNullOrEmpty(AreaXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(AreaXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("AreaId");
                    User.AreaId = new int[NodeList.Count];
                    for (int i = 0; i < NodeList.Count; i++)
                    {
                        User.AreaId[i] = int.Parse(NodeList[i].FirstChild.Value);
                    }
                }
                if (!String.IsNullOrEmpty(CompanyRoleXml))
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(CompanyRoleXml);
                    System.Xml.XmlNodeList NodeList = xmlDoc.GetElementsByTagName("TypeId");
                    
                    for (int i = 0; i < NodeList.Count; i++)
                    {
                        User.CompanyRole.SetRole((EyouSoft.Model.CompanyStructure.CompanyType)int.Parse(NodeList[i].FirstChild.Value));
                    }
                }

                #region 写登录日志
                dc.Parameters.Clear();
                dc = this.SystemStore.GetSqlStringCommand(SQL_USERLOGIN_LOG);
                this.SystemStore.AddInParameter(dc, "UserId", DbType.String, User.ID);
                this.SystemStore.AddInParameter(dc, "LastLoginIP", DbType.String, EyouSoft.Common.Utility.GetRemoteIP());
                DbHelper.ExecuteSql(dc, this.SystemStore);

                //EyouSoft.BusinessLogWriter.Model.LogWriter Log = new EyouSoft.BusinessLogWriter.Model.LogWriter();
                //Log.EventID = Guid.NewGuid().ToString();
                //Log.CompanyId = User.CompanyID;
                //Log.OperatorId = User.ID;
                //Log.OperatorName = User.UserName;
                //Log.EventTitle = EyouSoft.BusinessLogWriter.UserLog.LOG_COMPANY_UPDATE_TITLE;
                //Log.EventMessage = EyouSoft.BusinessLogWriter.UserLog.LOG_COMPANY_UPDATE;
                //Log.EventIP = EyouSoft.Common.Utility.GetRemoteIP();
                //Log.EventUrl = EyouSoft.Common.Utility.GetRequestUrl();
                //new EyouSoft.BusinessLogWriter.UserLog().WriteLog(Log);

                #endregion

                #region 写入组团在线用户
                if (User.CompanyRole.HasRole(EyouSoft.Model.CompanyStructure.CompanyType.组团))
                {
                    dc.Parameters.Clear();
                    dc = this.SystemStore.GetSqlStringCommand(SQL_ONLINELOGIN);
                    this.SystemStore.AddInParameter(dc, "ID", DbType.String, Guid.NewGuid().ToString());
                    this.SystemStore.AddInParameter(dc, "UserId", DbType.String, User.ID);
                    this.SystemStore.AddInParameter(dc, "CompanyId", DbType.String, User.CompanyID);
                    DbHelper.ExecuteSql(dc, this.SystemStore);
                }
                #endregion

                UpdateUserInfo(User);
            }
            #endregion
            return User;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PWD">用户密码</param>
        /// <param name="LoginTicket">登录凭据值</param>
        /// <returns>用户信息</returns>        
        public UserInfo UserLoginAct(string UserName, string PWD, string LoginTicket)
        {
            UserInfo User = UserLoginFunc(UserName, PWD, PasswordType.SHA);            
            if (User != null)
            {
                User.LoginTicket = LoginTicket;
                UpdateUserInfo(User);
            }
            return User;
        }
        /// <summary>
        /// 用户登录
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
            }
            return User;
        }
        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="UID">用户编号</param>
        /// <returns>是否成功</returns>
        public bool UserLogout(string UID)
        {
            //RemoveCache();
            EyouSoftCache.Remove(CacheTag.Company.CompanyUser + UID);
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
            UserInfo User = (UserInfo)EyouSoftCache.GetCache(CacheTag.Company.CompanyUser + UID);
            return User;
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        public void UpdateUserInfo(UserInfo User)
        {
            //SetCache();
            EyouSoftCache.Remove(CacheTag.Company.CompanyUser + User.UserName);
            EyouSoftCache.Add(CacheTag.Company.CompanyUser + User.UserName, User);
            return;
        }

        #endregion
    }
}

