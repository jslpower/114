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
    /// 创建人：鲁功源 2010-05-18
    /// 描述：包含项目数据层
    /// </summary>
    public class ServiceStandard:DALBase,IServiceStandard
    {
        #region 构造函数
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database=null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceStandard() 
        {
            this._database = base.CompanyStore;
        }
        #endregion

        #region SQL定义
        const string SQL_ServiceStandard_ADD = "INSERT INTO tbl_ServiceStandard(CompanyId,TypeId,OperatorID,Content) VALUES(@CompanyId,@TypeId,@OperatorID,@Content)";
        const string SQL_ServiceStandard_UPDATE = "UPDATE tbl_ServiceStandard set CompanyId=@CompanyId,TypeId=@TypeId,OperatorID=@OperatorID,Content=@Content where id=@id";
        const string SQL_ServiceStandard_DELETE = "DELETE tbl_ServiceStandard  WHERE ID=@ID ";
        const string SQL_ServiceStandard_SELECT = "SELECT id,CompanyId,TypeId,OperatorID,Content from tbl_ServiceStandard";
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加一条包含项目
        /// </summary>
        /// <param name="model">包含项目实体</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public virtual bool Add(EyouSoft.Model.CompanyStructure.ServiceStandard model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ServiceStandard_ADD);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc,"TypeId",DbType.Byte,(int)model.TypeID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc,"Content",DbType.String,model.Content);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 修改一条包含项目
        /// </summary>
        /// <param name="model">包含项目实体</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public virtual bool Update(EyouSoft.Model.CompanyStructure.ServiceStandard model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ServiceStandard_UPDATE);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyID);
            this._database.AddInParameter(dc, "TypeId", DbType.Byte, (int)model.TypeID);
            this._database.AddInParameter(dc, "OperatorID", DbType.AnsiStringFixedLength, model.OperatorID);
            this._database.AddInParameter(dc, "Content", DbType.String, model.Content);
            this._database.AddInParameter(dc, "id", DbType.Int32, model.ID);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 真实删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>true: 操作成功 false: 操作失败</returns>
        public virtual bool Delete(int id)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ServiceStandard_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 根据公司ID获取包含项目列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="TypeID">包含项目类型</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CompanyStructure.ServiceStandard> GetList(string companyId,EyouSoft.Model.CompanyStructure.ServiceTypes TypeID,int pageSize,int pageIndex,ref int recordCount)
        {
            IList<EyouSoft.Model.CompanyStructure.ServiceStandard> list = new List<EyouSoft.Model.CompanyStructure.ServiceStandard>();
            string tableName = "tbl_ServiceStandard";
            string fields = "ID,[Content],TypeID";
            string primaryKey = "ID";
            string orderByString = "ID DESC";

            #region 查询条件
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(companyId))
            {
                strWhere.AppendFormat(" companyId='{0}' ",companyId); 
            }
            if (strWhere.Length > 0)
                strWhere.AppendFormat(" and TypeId={0} ",(int)TypeID);
            else
                strWhere.AppendFormat(" TypeId={0} ", (int)TypeID);   
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,tableName,primaryKey,fields,strWhere.ToString(),orderByString))
            {
                EyouSoft.Model.CompanyStructure.ServiceStandard model=null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.ServiceStandard();
                    model.Content = dr.GetString(1);
                    model.ID = dr.GetInt32(0);
                    if(!dr.IsDBNull(2))
                        model.TypeID = (EyouSoft.Model.CompanyStructure.ServiceTypes)(int)dr.GetByte(2);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 获取一条包含项目实体
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns>包含项目实体</returns>
        public virtual EyouSoft.Model.CompanyStructure.ServiceStandard GetModel(int id)
        {
            EyouSoft.Model.CompanyStructure.ServiceStandard model = null;
            string SqlStr = SQL_ServiceStandard_SELECT + " WHERE ID=@ID ";
            DbCommand dc = this._database.GetSqlStringCommand(SqlStr);
            this._database.AddInParameter(dc, "ID", DbType.Int32, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CompanyStructure.ServiceStandard();
                    model.CompanyID = dr.GetString(1);
                    model.Content = dr.GetString(4);
                    model.ID = dr.GetInt32(0);
                    model.OperatorID = dr.GetString(3);
                    if(!dr.IsDBNull(2))
                        model.TypeID = (EyouSoft.Model.CompanyStructure.ServiceTypes)(int)dr.GetByte(2);
                }
            }
            return model;
        }
        #endregion
    }
}
