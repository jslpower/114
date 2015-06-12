using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace EyouSoft.DAL.NewsStructure
{
    /// <summary>
    /// 资讯类别数据层
    /// </summary>
    /// 鲁功源 2011-03-31
    public class NewsType:DALBase,IDAL.NewsStructure.INewsType
    {
        #region SQL变量定义
        private const string SQL_NewsType_Insert = "insert into tbl_NewsClass(Category,ClassName,IsSystem,OperatorId,IssueTime) values(@Category,@ClassName,'0',@OperatorId,getdate());";
        private const string SQL_NewsType_Update = "Update tbl_NewsClass set ClassName=@ClassName,OperatorId=@OperatorId where Id=@Id";
        private const string SQL_NewsType_Exists = "Select count(1) from tbl_NewsClass where 1=1 ";
        private const string SQL_NewsType_Delete = "Delete tbl_NewsClass where id in({0})";
        private const string SQL_NewsType_GetAll = "Select Id,Category,ClassName,IsSystem,OperatorId,IssueTime from tbl_NewsClass where 1=1 ";
        private const string SQL_NewsType_GetCategoryById = "Select Category from tbl_NewsClass where Id=@Id";
        private const string SQL_NewsType_GetNewsTypeName = "Select ClassName from tbl_NewsClass where Id=@Id";
        #endregion

        #region 数据库变量
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsType() 
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region INewsType 成员
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.NewsStructure.NewsType model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_NewsType_Insert);
            this._db.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._db.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
            this._db.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.NewsStructure.NewsType model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_NewsType_Update);
            this._db.AddInParameter(dc, "ClassName", DbType.String, model.ClassName);
            this._db.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            this._db.AddInParameter(dc, "Id", DbType.Int32, model.Id);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 验证是否存在同名新闻类别
        /// </summary>
        /// <param name="NewsTypeName">类别名称</param>
        /// <param name="Id">主键编号 新增时=0</param>
        /// <param name="Category">资讯类别 =null检索全部</param>
        /// <returns>true:存在 false：不存在</returns>
        public virtual bool IsExists(string NewsTypeName, int Id, EyouSoft.Model.NewsStructure.NewsCategory? Category)
        {
            StringBuilder strSql = new StringBuilder(SQL_NewsType_Exists);
            if (!string.IsNullOrEmpty(NewsTypeName))
                strSql.AppendFormat(" and ClassName='{0}' ", NewsTypeName);
            strSql.AppendFormat(" and Id<>{0} ", Id);
            if (Category.HasValue)
                strSql.AppendFormat(" and Category={0} ", (int)Category.Value);
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.Exists(dc, this._db);
        }
        /// <summary>
        /// 获取指定资讯类别的大类别
        /// </summary>
        /// <param name="Id">资讯类别编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.NewsStructure.NewsCategory? GetCategoryById(int Id)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_NewsType_GetCategoryById);
            this._db.AddInParameter(dc, "Id", DbType.Int32, Id);
            object obj= DbHelper.GetSingle(dc, this._db);
            if (obj == null || obj.ToString() == "0")
                return null;
            else
                return (EyouSoft.Model.NewsStructure.NewsCategory)int.Parse(obj.ToString());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(params int[] Ids)
        {
            StringBuilder strQuery = new StringBuilder();
            foreach (var item in Ids)
            {
                strQuery.AppendFormat("{0},", item);
            }
            DbCommand dc=this._db.GetSqlStringCommand(string.Format(SQL_NewsType_Delete,strQuery.ToString().TrimEnd(',')));
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取所有资讯类别列表
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.NewsStructure.NewsType> GetList(EyouSoft.Model.NewsStructure.NewsCategory? Category)
        {
            IList<EyouSoft.Model.NewsStructure.NewsType> list = new List<EyouSoft.Model.NewsStructure.NewsType>();
            StringBuilder strSql = new StringBuilder(SQL_NewsType_GetAll);
            if (Category.HasValue)
                strSql.AppendFormat(" and Category={0} ", (int)Category.Value);
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.NewsStructure.NewsType model = new EyouSoft.Model.NewsStructure.NewsType();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.ClassName = dr.IsDBNull(dr.GetOrdinal("ClassName")) ? string.Empty : dr[dr.GetOrdinal("ClassName")].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("Category")))
                        model.Category = (EyouSoft.Model.NewsStructure.NewsCategory)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.IsSystem = dr[dr.GetOrdinal("IsSystem")].ToString() == "1" ? true : false;
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 根据编号获取资讯类别名称
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns></returns>
        public virtual string GetNewsTypeName(int Id)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_NewsType_GetNewsTypeName);
            this._db.AddInParameter(dc, "Id", DbType.Int32, Id);
            object obj = DbHelper.GetSingle(dc, this._db);
            if (obj == null)
                return "";
            return obj.ToString();
        }
        #endregion
    }


}
