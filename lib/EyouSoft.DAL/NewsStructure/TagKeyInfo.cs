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
    /// Tag标签与KeyWord关键字数据层
    /// </summary>
    /// 鲁功源 2011-03-31
    public class TagKeyInfo:DALBase, IDAL.NewsStructure.ITagKeyInfo
    {
        #region SQL变量定义
        private const string SQL_TagKeyInfo_Insert = "Insert into tbl_TagKeyList(Category,ItemName,ItemUrl,OperatorId) values(@Category,@ItemName,@ItemUrl,@OperatorId)";
        private const string SQL_TagKeyInfo_Update = "Update tbl_TagKeyList set ItemName=@ItemName,ItemUrl=@ItemUrl,OperatorId=@OperatorId where Id=@Id";
        private const string SQL_TagKeyInfo_Delete = "Delete tbl_TagKeyList where Id in({0})";
        private const string SQL_TagKeyInfo_Exists = "Select Count(1) from tbl_TagKeyList where 1=1 ";
        private const string SQL_TagKeyInfo_Select = "SELECT ID,Category,ItemName,ItemUrl,OperatorId,IssueTime FROM tbl_TagKeyList WHERE ID = @id AND Category = @cate";
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
        public TagKeyInfo() 
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region ITagKeyInfo 成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true：成功 false:失败</returns>
        public bool Add(EyouSoft.Model.NewsStructure.TagKeyInfo model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_TagKeyInfo_Insert);
            this._db.AddInParameter(dc, "Category", DbType.Byte, (int)model.Category);
            this._db.AddInParameter(dc, "ItemName", DbType.String, model.ItemName);
            this._db.AddInParameter(dc, "ItemUrl", DbType.String, model.ItemUrl);
            this._db.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns>true：成功 false:失败</returns>
        public bool Update(EyouSoft.Model.NewsStructure.TagKeyInfo model)
        {
            DbCommand dc = this._db.GetSqlStringCommand(SQL_TagKeyInfo_Update);
            this._db.AddInParameter(dc, "Id", DbType.Int32, model.Id);
            this._db.AddInParameter(dc, "ItemName", DbType.String, model.ItemName);
            this._db.AddInParameter(dc, "ItemUrl", DbType.String, model.ItemUrl);
            this._db.AddInParameter(dc, "OperatorId", DbType.Int32, model.OperatorId);
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns>true：成功 false:失败</returns>
        public bool Delete(params int[] Ids)
        {
            StringBuilder strQuery = new StringBuilder();
            foreach (var item in Ids)
            {
                strQuery.AppendFormat("{0},", item);
            }
            DbCommand dc = this._db.GetSqlStringCommand(string.Format(SQL_TagKeyInfo_Delete,strQuery.ToString().TrimEnd(',')));
            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 是否存在同名项目
        /// </summary>
        /// <param name="ItemName">项目名称</param>
        /// <param name="Id">主键编号</param>
        /// <param name="Category">类别</param>
        /// <returns>true:存在 false:不存在</returns>
        public bool IsExists(string ItemName, int Id, EyouSoft.Model.NewsStructure.ItemCategory Category)
        {
            StringBuilder strSql = new StringBuilder(SQL_TagKeyInfo_Exists);
            if (!string.IsNullOrEmpty(ItemName))
                strSql.AppendFormat(" and ItemName='{0}' ", ItemName);
            strSql.AppendFormat(" and Id<>{0} ", Id);
            strSql.AppendFormat(" and Category={0} ", (int)Category);
            DbCommand dc = this._db.GetSqlStringCommand(strSql.ToString());
            return DbHelper.Exists(dc, this._db);
        }


        /// <summary>
        /// 根据编号获取关键字/tag对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="cate">类别</param>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.TagKeyInfo GetModel(int id, EyouSoft.Model.NewsStructure.ItemCategory cate)
        {
            DbCommand comm = this._db.GetSqlStringCommand(SQL_TagKeyInfo_Select);

            this._db.AddInParameter(comm, "@id", DbType.Int32, id);
            this._db.AddInParameter(comm, "@cate", DbType.Int32, (int)cate);

            using (IDataReader dr = DbHelper.ExecuteReader(comm,this._db))
            {
                if (dr.Read())
                {
                    EyouSoft.Model.NewsStructure.TagKeyInfo model = new EyouSoft.Model.NewsStructure.TagKeyInfo();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.Category = (EyouSoft.Model.NewsStructure.ItemCategory)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.ItemName = dr.IsDBNull(dr.GetOrdinal("ItemName")) ? string.Empty : dr[dr.GetOrdinal("ItemName")].ToString();
                    model.ItemUrl = dr.IsDBNull(dr.GetOrdinal("ItemUrl")) ? string.Empty : dr[dr.GetOrdinal("ItemUrl")].ToString();
                    model.OperatorId = dr.GetInt32(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));

                    return model;
                }
            }

            return null;
        }
        /// <summary>
        /// 分页获取标签与关键字列表信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ItemName">项名称</param>
        /// <param name="ItemUrl">项链接</param>
        /// <param name="Category">类别 =null返回所有</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.TagKeyInfo> GetList(int pageSize, int pageIndex, ref int recordCount, string ItemName, string ItemUrl, EyouSoft.Model.NewsStructure.ItemCategory? Category)
        {
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = new List<EyouSoft.Model.NewsStructure.TagKeyInfo>();
            string tableName = "tbl_TagKeyList";
            string primaryKey = "Id";
            string fields = "Id,Category,ItemName,ItemUrl,OperatorId,IssueTime";
            string orderByStr = " IssueTime desc";
            StringBuilder strQuery = new StringBuilder(" 1=1 ");
            if(!string.IsNullOrEmpty(ItemName))
                strQuery.AppendFormat(" and ItemName like'%{0}%' ", ItemName);
            if (Category.HasValue)
            {
                strQuery.AppendFormat(" and Category={0} ", (int)Category.Value);
                if(Category.Value==EyouSoft.Model.NewsStructure.ItemCategory.KeyWord && !string.IsNullOrEmpty(ItemUrl))
                    strQuery.AppendFormat(" and ItemUrl='{0}' ", ItemUrl);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strQuery.ToString(), orderByStr))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.NewsStructure.TagKeyInfo model = new EyouSoft.Model.NewsStructure.TagKeyInfo();
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.Category = (EyouSoft.Model.NewsStructure.ItemCategory)int.Parse(dr[dr.GetOrdinal("Category")].ToString());
                    model.ItemName = dr.IsDBNull(dr.GetOrdinal("ItemName")) ? string.Empty : dr[dr.GetOrdinal("ItemName")].ToString();
                    model.ItemUrl = dr.IsDBNull(dr.GetOrdinal("ItemUrl")) ? string.Empty : dr[dr.GetOrdinal("ItemUrl")].ToString();
                    model.OperatorId = dr.GetInt32(dr.GetOrdinal("OperatorId"));
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        #endregion
    }

}
