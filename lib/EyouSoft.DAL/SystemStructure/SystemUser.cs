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
    /// 系统用户 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SystemUser : DALBase, IDAL.SystemStructure.ISystemUser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemUser()
        { }

        #region SqlString

        private const string Sql_SystemUser_Add = " INSERT INTO [tbl_SystemUser]([UserName],[PassWord],[MD5Password],[EncryptPassword],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[IsDisable],[PermissionList],[IssueTime]) VALUES (@UserName,@PassWord,@MD5Password,@EncryptPassword,@ContactName,@ContactTel ,@ContactFax,@ContactMobile,@IsDisable,@PermissionList,@IssueTime);SELECT @@IDENTITY; ";
        private const string Sql_SystemUser_Update = " UPDATE [tbl_SystemUser] SET [ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactFax] = @ContactFax,[ContactMobile] = @ContactMobile,[PermissionList] = @PermissionList WHERE ID = @ID ";
        private const string Sql_SystemUser_Del = " DELETE FROM [tbl_SystemUser] WHERE ID in ({0}) ";
        private const string Sql_SystemUser_Select = " SELECT [ID],[UserName],[PassWord],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[PermissionList],[IsDisable],[IssueTime] FROM [tbl_SystemUser] ";
        private const string Sql_SystemUser_Exists = " select count(1) from [tbl_SystemUser] ";

        private const string Sql_SysUserAreaControl_Del = " DELETE FROM [tbl_SysUserAreaControl] WHERE [UserId] in ({0}) ";
        private const string Sql_SysUserAreaControl_Add = " INSERT INTO [tbl_SysUserAreaControl]([AreaId],[UserId]) VALUES ({0},{1}); ";

        private const string Sql_UserCustomerType_Del = " DELETE FROM [tbl_UserCustomerType] WHERE UserId in ({0}); ";
        private const string Sql_UserCustomerType_Add = " INSERT INTO [tbl_UserCustomerType] ([UserId],[CustomerId]) VALUES ({0},{1}); ";

        #endregion

        #region 函数成员

        /// <summary>
        /// 新增系统管理员用户
        /// </summary>
        /// <param name="model">系统用户实体</param>
        /// <returns>返回新加用户的ID</returns>
        public virtual int AddSystemUser(EyouSoft.Model.SystemStructure.SystemUser model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SystemUser_Add);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "UserName", DbType.String, model.UserName);
            base.SystemStore.AddInParameter(dc, "PassWord", DbType.String, model.PassWordInfo.NoEncryptPassword);
            base.SystemStore.AddInParameter(dc, "MD5Password", DbType.String, model.PassWordInfo.MD5Password);
            base.SystemStore.AddInParameter(dc, "EncryptPassword", DbType.String, model.PassWordInfo.SHAPassword);
            base.SystemStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.SystemStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.SystemStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.SystemStore.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactMobile);
            base.SystemStore.AddInParameter(dc, "IsDisable", DbType.String, model.IsDisable ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "PermissionList", DbType.String, model.PermissionList);
            base.SystemStore.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);

            #endregion

            object obj = DbHelper.GetSingle(dc, base.SystemStore);
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
        /// 修改用户信息(只修改联系人、电话、传真、手机、权限)
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSystemUser(EyouSoft.Model.SystemStructure.SystemUser model)
        {
            if (model == null)
                return 0;

            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SystemUser_Update);

            base.SystemStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.SystemStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.SystemStore.AddInParameter(dc, "ContactFax", DbType.String, model.ContactFax);
            base.SystemStore.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactMobile);
            base.SystemStore.AddInParameter(dc, "PermissionList", DbType.String, model.PermissionList);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        ///  根据用户ID删除用户
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool DeleteSystemUser(IList<int> SystemUserIds)
        {
            if (SystemUserIds == null || SystemUserIds.Count <= 0)
                return false;

            string strIds = string.Empty;
            foreach (int SystemUserId in SystemUserIds)
            {
                if (SystemUserId > 0)
                    strIds += SystemUserId + ",";
            }
            strIds = strIds.TrimEnd(',');

            if (string.IsNullOrEmpty(strIds))
                return false;

            string strWhere = string.Format(Sql_SystemUser_Del, strIds);
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引，0添加时间升序，1添加时间降序</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <param name="UserName">用户名(模糊查询)</param>
        /// <param name="ContactName">联系人(模糊查询)</param>
        /// <returns>系统用户实体集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SystemUser> GetSystemUserList(int PageSize, int PageIndex, ref int RecordCount, int OrderIndex, int AreaId, string UserName, string ContactName)
        {
            IList<EyouSoft.Model.SystemStructure.SystemUser> list = new List<EyouSoft.Model.SystemStructure.SystemUser>();

            bool IsWhere = false;
            string strWhere = string.Empty;
            string strFiles = " [ID],[UserName],[PassWord],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[PermissionList],[IsDisable],[IssueTime] ";
            string strOrder = string.Empty;
            if (AreaId > 0)
            {
                strWhere += string.Format(" ID in (select distinct [UserId] from [tbl_SysUserAreaControl] where [AreaId] = {0}) ", AreaId);
                IsWhere = true;
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                if (IsWhere)
                    strWhere += " and ";

                strWhere += string.Format(" [UserName] like '%{0}%' ", UserName.Replace("'", ""));
                IsWhere = true;
            }
            if (!string.IsNullOrEmpty(ContactName))
            {
                if (IsWhere)
                    strWhere += " and ";

                strWhere += string.Format(" [ContactName] like '%{0}%' ", ContactName.Replace("'", ""));
                IsWhere = true;
            }
            switch (OrderIndex)
            {
                case 0: strOrder = " [IssueTime] asc "; break;
                case 1: strOrder = " [IssueTime] desc "; break;
                default: strOrder = " [IssueTime] desc "; break;
            }

            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_SystemUser", "[ID]", strFiles, strWhere, strOrder))
            {
                Model.SystemStructure.SystemUser model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.SystemStructure.SystemUser();
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                        model.ID = int.Parse(dr[0].ToString());
                    model.UserName = dr[1].ToString();
                    model.PassWordInfo.NoEncryptPassword = dr[2].ToString();
                    model.ContactName = dr[3].ToString();
                    model.ContactTel = dr[4].ToString();
                    model.ContactFax = dr[5].ToString();
                    model.ContactMobile = dr[6].ToString();
                    model.PermissionList = dr[7].ToString();
                    if (!string.IsNullOrEmpty(dr[8].ToString()))
                    {
                        if (dr[8].ToString() == "1")
                            model.IsDisable = true;
                        else
                            model.IsDisable = false;
                    }
                    if (!string.IsNullOrEmpty(dr[9].ToString()))
                        model.IssueTime = DateTime.Parse(dr[9].ToString());

                    list.Add(model);
                }
                model = null;
            }

            return list;
        }

        /// <summary>
        /// 根据用户名和密码修改密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="OldPassword">原来的密码</param>
        /// <param name="NewsPassword">新密码</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateUserPassWord(string UserName, EyouSoft.Model.CompanyStructure.PassWord OldPassword, EyouSoft.Model.CompanyStructure.PassWord NewsPassword)
        {
            if (string.IsNullOrEmpty(UserName) || OldPassword == null || NewsPassword == null)
                return 0;

            string strSql = " update [tbl_SystemUser] SET [PassWord] = @NewPassWord,[MD5Password]=@MD5Password,[EncryptPassword]=@EncryptPassword where [UserName] = @UserName and [EncryptPassword] = @OldPassWord ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            base.SystemStore.AddInParameter(dc, "OldPassWord", DbType.String, OldPassword.SHAPassword);
            base.SystemStore.AddInParameter(dc, "NewPassWord", DbType.String, NewsPassword.NoEncryptPassword);
            base.SystemStore.AddInParameter(dc, "MD5Password", DbType.String, NewsPassword.MD5Password);
            base.SystemStore.AddInParameter(dc, "EncryptPassword", DbType.String, NewsPassword.SHAPassword);
            base.SystemStore.AddInParameter(dc, "UserName", DbType.String, UserName);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 根据用户ID修改密码
        /// </summary>
        /// <param name="SystemUserId">用户id</param>
        /// <param name="password">密码</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateUserPassWord(int SystemUserId, EyouSoft.Model.CompanyStructure.PassWord password)
        {
            if (SystemUserId <= 0 || password == null)
                return 0;

            string strSql = " update [tbl_SystemUser] SET [PassWord] = @PassWord,[MD5Password]=@MD5Password,[EncryptPassword]=@EncryptPassword where [ID] = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql);
            base.SystemStore.AddInParameter(dc, "PassWord", DbType.String, password.NoEncryptPassword);
            base.SystemStore.AddInParameter(dc, "MD5Password", DbType.String, password.MD5Password);
            base.SystemStore.AddInParameter(dc, "EncryptPassword", DbType.String, password.SHAPassword);
            base.SystemStore.AddInParameter(dc, "ID", DbType.String, SystemUserId);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="SystemUserId">用户ID</param>
        /// <returns>返回用户实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SystemUser GetSystemUserModel(int SystemUserId)
        {
            Model.SystemStructure.SystemUser model = new EyouSoft.Model.SystemStructure.SystemUser();

            string strWhere = Sql_SystemUser_Select + " where ID = @ID; ";
            strWhere += " select AreaId from tbl_SysUserAreaControl where UserId = @ID; ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, SystemUserId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                if (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                        model.ID = int.Parse(dr[0].ToString());
                    model.UserName = dr[1].ToString();
                    model.PassWordInfo.NoEncryptPassword = dr[2].ToString();
                    model.ContactName = dr[3].ToString();
                    model.ContactTel = dr[4].ToString();
                    model.ContactFax = dr[5].ToString();
                    model.ContactMobile = dr[6].ToString();
                    model.PermissionList = dr[7].ToString();
                    if (!string.IsNullOrEmpty(dr[8].ToString()))
                    {
                        if (dr[8].ToString() == "1")
                            model.IsDisable = true;
                        else
                            model.IsDisable = false;
                    }
                    if (!string.IsNullOrEmpty(dr[9].ToString()))
                        model.IssueTime = DateTime.Parse(dr[9].ToString());
                }

                dr.NextResult();

                IList<int> AreaIds = new List<int>();
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        AreaIds.Add(dr.GetInt32(0));
                }
                model.AreaId = AreaIds;
            }

            return GetSysUserCustomerTypeIds(model);
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns>存在返回true，不存在返回false</returns>
        public virtual bool ExistsByUserName(string UserName)
        {
            string strWhere = Sql_SystemUser_Exists + " where [UserName] = @UserName ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            base.SystemStore.AddInParameter(dc, "UserName", DbType.String, UserName);

            return DbHelper.Exists(dc, base.SystemStore);
        }

        /// <summary>
        /// 验证用户是否存在(存在返回该用户实体，不存在返回null)
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>存在返回该用户实体，不存在返回null</returns>
        public virtual Model.SystemStructure.SystemUser ExistsByUserName(string UserName, string PassWord)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(PassWord))
                return null;

            Model.SystemStructure.SystemUser model = new EyouSoft.Model.SystemStructure.SystemUser();

            string strWhere = Sql_SystemUser_Select + " where [UserName] = @UserName and [PassWord] = @PassWord; ";
            strWhere += " select [AreaId] from [tbl_SysUserAreaControl] where [UserId] = (select top 1 ID from [tbl_SystemUser] where [UserName] = @UserName and [PassWord] = @PassWord) order by [AreaId] asc ; ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            base.SystemStore.AddInParameter(dc, "UserName", DbType.String, UserName);
            base.SystemStore.AddInParameter(dc, "PassWord", DbType.String, PassWord);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                while (dr.Read())
                {
                    if (!string.IsNullOrEmpty(dr[0].ToString()))
                        model.ID = int.Parse(dr[0].ToString());
                    model.UserName = dr[1].ToString();
                    model.PassWordInfo.NoEncryptPassword = dr[2].ToString();
                    model.ContactName = dr[3].ToString();
                    model.ContactTel = dr[4].ToString();
                    model.ContactFax = dr[5].ToString();
                    model.ContactMobile = dr[6].ToString();
                    model.PermissionList = dr[7].ToString();
                    if (!string.IsNullOrEmpty(dr[8].ToString()))
                    {
                        if (dr[8].ToString() == "1")
                            model.IsDisable = true;
                        else
                            model.IsDisable = false;
                    }
                    if (!string.IsNullOrEmpty(dr[9].ToString()))
                        model.IssueTime = DateTime.Parse(dr[9].ToString());

                    break;
                }

                dr.NextResult();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        model.AreaId.Add(dr.GetInt32(0));
                }
            }
            return model;
        }

        /// <summary>
        /// 设置该用户是否停用/启用
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <param name="IsDisable">停用(true)/启用(false)</param>
        /// <returns>返回操作是否成功</returns>
        public virtual bool SetIsDisable(int UserId, bool IsDisable)
        {
            if (UserId <= 0)
                return false;

            string strWhere = " Update [tbl_SystemUser] SET [IsDisable] = @IsDisable where [ID] = @ID ";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);
            base.SystemStore.AddInParameter(dc, "IsDisable", DbType.String, IsDisable ? "1" : "0");
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, UserId);

            int Result = DbHelper.ExecuteSql(dc, base.SystemStore);
            if (Result > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 系统管理员用户和线路区域关系函数

        /// <summary>
        /// 新增系统管理员用户和线路区域的关系
        /// </summary>
        /// <param name="UserId">管理员用户Id</param>
        /// <param name="AreaId">线路区域ID</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int AddSysUserAreaControl(int UserId, IList<int> AreaId)
        {
            if (UserId <= 0 || AreaId == null || AreaId.Count <= 0)
                return 0;

            StringBuilder strSql = new StringBuilder();
            foreach (int tmpId in AreaId)
            {
                strSql.AppendFormat(Sql_SysUserAreaControl_Add, tmpId, UserId);
            }

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 根据用户ID删除用户和线路区域的关系
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int DeleteSysUserAreaControl(IList<int> SystemUserIds)
        {
            if (SystemUserIds == null || SystemUserIds.Count <= 0)
                return 0;

            string strIds = string.Empty;
            foreach (int SystemUserId in SystemUserIds)
            {
                if (SystemUserId > 0)
                    strIds += SystemUserId + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (string.IsNullOrEmpty(strIds))
                return 0;

            string strWhere = string.Format(Sql_SysUserAreaControl_Del, strIds);
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        #endregion

        #region 系统管理员用户和用户所能查看的易诺用户池客户类型 方法

        /// <summary>
        /// 新增用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="UserId">系统用户ID</param>
        /// <param name="CustomerTypeIds">易诺用户池客户类型ID集合</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int AddSysUserCustomerType(int UserId, IList<int> CustomerTypeIds) 
        {
            if (UserId <= 0 || CustomerTypeIds == null || CustomerTypeIds.Count <= 0)
                return 0;

            StringBuilder strSql = new StringBuilder();
            foreach (int tmpId in CustomerTypeIds)
            {
                strSql.AppendFormat(Sql_UserCustomerType_Add, UserId, tmpId);
            }

            DbCommand dc = base.PoolStore.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, base.PoolStore);
        }

        /// <summary>
        /// 根据用户ID删除用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="SystemUserIds">用户ID集合</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int DeleteSysUserCustomerType(IList<int> SystemUserIds)
        {
            if (SystemUserIds == null || SystemUserIds.Count <= 0)
                return 0;

            string strIds = string.Empty;
            foreach (int SystemUserId in SystemUserIds)
            {
                if (SystemUserId > 0)
                    strIds += SystemUserId + ",";
            }
            strIds = strIds.TrimEnd(',');
            if (string.IsNullOrEmpty(strIds))
                return 0;

            string strWhere = string.Format(Sql_UserCustomerType_Del, strIds);
            DbCommand dc = base.PoolStore.GetSqlStringCommand(strWhere);

            return DbHelper.ExecuteSql(dc, base.PoolStore);
        }

        /// <summary>
        /// 获取用户所能查看的易诺用户池客户类型
        /// </summary>
        /// <param name="model">系统用户实体</param>
        private EyouSoft.Model.SystemStructure.SystemUser GetSysUserCustomerTypeIds(EyouSoft.Model.SystemStructure.SystemUser model)
        {
            if (model == null || model.ID <= 0)
                return model;

            IList<int> CustomerTypeIds = new List<int>();
            string strSql = " SELECT [CustomerId] FROM [tbl_UserCustomerType] where UserId = @ID; ";
            DbCommand dc = base.PoolStore.GetSqlStringCommand(strSql);
            base.PoolStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.PoolStore))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        CustomerTypeIds.Add(dr.GetInt32(0));
                }
            }
            model.CustomerTypeIds = CustomerTypeIds;
            return model;
        }

        #endregion
    }
}
