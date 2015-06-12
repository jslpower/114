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
    /// 创建人：鲁功源  2010-05-11
    /// 描述：公司部门数据层
    /// </summary>
    public class CompanyDepartment : DALBase, ICompanyDepartment
    {
        #region SQL变量
        const string SQL_INSERT_CompanyDepartment = " INSERT INTO tbl_CompanyDepartment([ID],[CompanyId],[OperatorId],[DepartName]) VALUES(@ID,@CompanyID,@OperatorID,@DepartName) ";
        const string SQL_UPDATE_CompanyDepartment = "UPDATE tbl_CompanyDepartment SET [DepartName]=@DepartName where id=@ID";
        const string SQL_DELETE_CompanyDepartment = "DELETE tbl_CompanyDepartment WHERE id=@ID";
        const string SQL_SELECT_CompanyDepartmentModel = "SELECT [id],[CompanyId],[OperatorId],[DepartName] FROM tbl_CompanyDepartment WHERE Id=@ID";
        const string SQL_Exists_SQL_CompanyDepartment = "SELECT COUNT(0) from tbl_CompanyDepartment WHERE CompanyID=@CompanyID and DepartName=@DepartName";
        #endregion

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public CompanyDepartment()
        {
            this._database = base.CompanyStore;
        }

        #region 成员方法
        /// <summary>
        /// 验证是否存在同名部门
        /// </summary>
        /// <param name="CompanyID">公司编号</param>
        /// <param name="DepartmentName">部门名称</param>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public virtual bool Exists(string CompanyID, string DepartmentName, string ID)
        {
            string strWhere = SQL_Exists_SQL_CompanyDepartment;
            if (!string.IsNullOrEmpty(ID))
                strWhere += " AND ID<>@ID";

            DbCommand dc = this._database.GetSqlStringCommand(strWhere);
            if (!string.IsNullOrEmpty(ID))
                this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);

            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, CompanyID);
            this._database.AddInParameter(dc, "DepartName", DbType.String, DepartmentName);
            return DbHelper.Exists(dc, this._database);
        }
        /// <summary>
        /// 添加一个部门
        /// </summary>
        /// <param name="model">部门实体信息</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.CompanyDepartment model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_INSERT_CompanyDepartment);
            this._database.AddInParameter(dc, "ID", DbType.String, Guid.NewGuid().ToString());
            this._database.AddInParameter(dc, "CompanyID", DbType.String, model.CompanyID);
            this._database.AddInParameter(dc, "OperatorID", DbType.String, model.OperatorID);
            this._database.AddInParameter(dc, "DepartName", DbType.String, model.DepartName);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改一个部门信息
        /// </summary>
        /// <param name="model">部门实体信息</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.CompanyDepartment model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_UPDATE_CompanyDepartment);
            this._database.AddInParameter(dc, "DepartName", DbType.String, model.DepartName);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 删除一个部门信息
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool Delete(string id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETE_CompanyDepartment);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 根据公司编号获取部门列表
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> GetList(string companyId, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.CompanyDepartment> list = new List<EyouSoft.Model.CompanyStructure.CompanyDepartment>();
            EyouSoft.Model.CompanyStructure.CompanyDepartment model = null;
            string tableName = "tbl_CompanyDepartment";
            string fields = "id,DepartName,IssueTime";
            string primaryKey = "ID";
            string orderByString = "IssueTime DESC";
            string strWhere = string.Format(" companyid='{0}' ", companyId);

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere, orderByString))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyDepartment();
                    model.DepartName = dr[1].ToString();
                    model.ID = dr[0].ToString();
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取一个部门信息实体
        /// </summary>
        /// <param name="id">部门编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CompanyStructure.CompanyDepartment GetModel(string id)
        {
            EyouSoft.Model.CompanyStructure.CompanyDepartment model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SELECT_CompanyDepartmentModel);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.CompanyDepartment();
                    model.CompanyID = dr.GetString(dr.GetOrdinal("CompanyID"));
                    model.DepartName = dr.GetString(dr.GetOrdinal("DepartName"));
                    model.ID = dr.GetString(dr.GetOrdinal("ID"));
                    model.OperatorID = dr.GetString(dr.GetOrdinal("OperatorID"));
                }
            }
            return model;
        }

        /// <summary>
        /// 根据公司编号分页获取部门列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<Model.CompanyStructure.CompanyDepartment> GetList(string companyId)
        {
            IList<Model.CompanyStructure.CompanyDepartment> list = null;
            if (string.IsNullOrEmpty(companyId))
                return list;

            var strSql = new StringBuilder();
            strSql.Append(" select id,DepartName,IssueTime ");
            strSql.Append(" from tbl_CompanyDepartment ");
            strSql.Append(" where CompanyID = @CompanyID ");
            strSql.Append(" order by IssueTime DESC; ");

            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());
            _database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _database))
            {
                list = new List<Model.CompanyStructure.CompanyDepartment>();
                while (dr.Read())
                {
                    var model = new Model.CompanyStructure.CompanyDepartment
                                    {
                                        DepartName = dr[1].ToString(),
                                        ID = dr[0].ToString(),
                                        CompanyID = companyId
                                    };
                    list.Add(model);
                }
            }
            return list;
        }

        #endregion
    }
}
