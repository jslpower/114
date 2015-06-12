using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CompanyStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-14
    /// 描述：公司角色信息数据层
    /// </summary>
    public class CompanyUserRoles : DALBase, ICompanyUserRoles
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CompanyUserRoles() 
        {
            this._database = base.CompanyStore;
        }

        #region SQL 变量定义
        const string SQL_CompanyRoles_ADD = "IF(SELECT count(1) from tbl_CompanyRoles where CompanyId=@CompanyID and RoleName=@RoleName)=0 INSERT INTO tbl_CompanyRoles([id],[RoleName],[PermissionList],[IsAdminRole],[CompanyID],[OperatorID]) VALUES(@id,@RoleName,@PermissionList,@IsAdminRole,@CompanyID,@OperatorID)";
        const string SQL_CompanyRoles_SELECT = "SELECT [id],[RoleName],[IsAdminRole],[companyID],[operatorID],[issueTime],[permissionList] from tbl_CompanyRoles";
        const string SQL_CompanyRoles_UPDATE = "IF(SELECT count(1) from tbl_CompanyRoles where CompanyId=(SELECT CompanyId FROM tbl_CompanyRoles WHERE ID=@ID) and RoleName=@RoleName AND ID<>@ID)=0 UPDATE tbl_CompanyRoles set RoleName=@RoleName,PermissionList=@PermissionList,OperatorID=@OperatorID WHERE ID=@ID";
        const string SQL_CompanyRoles_DELETE = "DELETE tbl_CompanyRoles WHERE ID=@ID";
        const string SQL_CompanyRoles_EXISTS = "SELECT count(1) from tbl_CompanyRoles where CompanyId=@CompanyId and RoleName=@RoleName and ID<>@ID";
        const string SQL_SELECT_GetAdminRoleInfo = "SELECT [ID],[RoleName],[PermissionList],[IsAdminRole],[CompanyID],[OperatorID],[IssueTime] FROM [tbl_CompanyRoles] WHERE [CompanyID]=@CompanyId AND IsAdminRole='1'";
        #endregion

        /// <summary>
        /// 添加一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.CompanyUserRoles model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyRoles_ADD);
            this._database.AddInParameter(dc, "id", DbType.AnsiStringFixedLength, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "RoleName", DbType.String, model.RoleName);
            this._database.AddInParameter(dc, "PermissionList", DbType.String, model.PermissionList);
            this._database.AddInParameter(dc, "IsAdminRole", DbType.String, model.IsAdminRole ? "1" : "0");
            this._database.AddInParameter(dc, "CompanyID", DbType.String, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.String, model.OperatorID);
            //this._database.AddInParameter(dc, "AreaList", DbType.String, model.AreaList); 
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 修改一条公司角色
        /// </summary>
        /// <param name="model">公司角色实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.CompanyUserRoles model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyRoles_UPDATE);
            this._database.AddInParameter(dc, "RoleName", DbType.String, model.RoleName);
            this._database.AddInParameter(dc, "PermissionList", DbType.String, model.PermissionList);
            this._database.AddInParameter(dc, "OperatorID", DbType.String, model.OperatorID);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            //this._database.AddInParameter(dc, "AreaList", DbType.String, model.AreaList); 
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
            //需要更新该角色下的所有用户权限列表
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyRoles_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            return DbHelper.ExecuteSql(dc, this._database)>0?true:false;
        }
        /// <summary>
        /// 分页获取指定公司下的所有角色列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当期页码</param>
        /// <param name="recordCount">总记录</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyUserRoles> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyUserRoles> list = new List<EyouSoft.Model.CompanyStructure.CompanyUserRoles>();
            EyouSoft.Model.CompanyStructure.CompanyUserRoles model = null;
            string tableName = "tbl_CompanyRoles";
            string fields = "[id],[RoleName],[IsAdminRole],[IssueTime]";
            string primaryKey = "id";
            string orderByString = "IssueTime DESC";
            string strWhere = "IsDeleted=0";
            if (!string.IsNullOrEmpty(companyId))
            {
                strWhere = " companyId='"+companyId+"' ";
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,tableName,primaryKey,fields,strWhere,orderByString))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyUserRoles();
                    model.ID = dr.GetString(0);
                    model.RoleName = dr.GetString(1);
                    model.IsAdminRole = dr.GetString(2)=="1"?true:false;
                    model.IssueTime = dr.GetDateTime(3);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取角色信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyUserRoles GetModel(string id)
        {
            EyouSoft.Model.CompanyStructure.CompanyUserRoles model = null;
            string sqlStr = SQL_CompanyRoles_SELECT + " where Id=@Id ";
            DbCommand dc = this._database.GetSqlStringCommand(sqlStr);
            this._database.AddInParameter(dc, "Id", DbType.AnsiStringFixedLength, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyUserRoles();
                    model.CompanyID = dr.GetString(dr.GetOrdinal("companyID"));
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.IsAdminRole = dr.GetString(dr.GetOrdinal("isAdminRole")) == "1" ? true : false;
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("issueTime"));
                    model.OperatorID = dr.GetString(dr.GetOrdinal("operatorID"));
                    model.PermissionList = dr.IsDBNull(dr.GetOrdinal("permissionList")) ? "" : dr.GetString(dr.GetOrdinal("permissionList"));
                    model.RoleName = dr.IsDBNull(dr.GetOrdinal("roleName")) ? "" : dr.GetString(dr.GetOrdinal("roleName"));
                    //model.AreaList = dr.GetString(dr.GetOrdinal("AreaList"));
                }
            }
            return model;
        }
        /// <summary>
        /// 验证指定公司是否重名角色
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="rolename">角色名称</param>
        /// <param name="id">编号 新增时=""</param>
        /// <returns></returns>
        public virtual bool Exists(string companyid, string rolename, string id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_CompanyRoles_EXISTS);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyid);
            this._database.AddInParameter(dc, "RoleName", DbType.String, rolename);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            return DbHelper.Exists(dc, this._database);
        }

        /// <summary>
        /// 获取公司管理员角色信息业务实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyUserRoles GetAdminRoleInfo(string companyId)
        {
            DbCommand cmd = this._database.GetSqlStringCommand(SQL_SELECT_GetAdminRoleInfo);
            this._database.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._database))
            {
                if (rdr.Read())
                {
                    return new EyouSoft.Model.CompanyStructure.CompanyUserRoles()
                    {
                        CompanyID = rdr.GetString(rdr.GetOrdinal("companyID")),
                        ID = rdr.GetString(rdr.GetOrdinal("ID")),
                        IsAdminRole = rdr.GetString(rdr.GetOrdinal("isAdminRole")) == "1" ? true : false,
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("issueTime")),
                        OperatorID = rdr.GetString(rdr.GetOrdinal("operatorID")),
                        PermissionList = rdr.IsDBNull(rdr.GetOrdinal("permissionList")) ? "" : rdr.GetString(rdr.GetOrdinal("permissionList")),
                        RoleName = rdr.IsDBNull(rdr.GetOrdinal("roleName")) ? "" : rdr.GetString(rdr.GetOrdinal("roleName"))
                    };
                }
            }

            return null;
        }
    }
}
