using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 特价机票数据操作
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class SpecialFares:DALBase,EyouSoft.IDAL.TicketStructure.ISpecialFares
    {
        #region 构造函数
        Database _database = null;
        public SpecialFares()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region sql
        const string SQL_SpecialFares_AddSpecialFares = "INSERT INTO tbl_SpecialFares(Title,Category,IsJump,ContentText,Contact,ContactWay,QQ) VALUES(@Title,@Category,@IsJump,@ContentText,@Contact,@ContactWay,@QQ)";
        const string SQL_SpecialFares_ModifySpecialFares = "UPDATE tbl_SpecialFares SET Title = @Title,Category = @Category,IsJump=@IsJump,ContentText=@ContentText,Contact=@Contact,ContactWay=@ContactWay,QQ=@QQ WHERE ID = @ID";
        const string SQL_SpecialFares_DeleteSpecialFares = "DELETE FROM tbl_SpecialFares WHERE CHARINDEX(','+LTRIM(ID)+',',','+@ids+',') > 0";
        const string SQL_SpecialFares_GetSpecialFare = "SELECT ID,Title,Category,IsJump,ContentText,Contact,ContactWay,QQ,AddTime FROM tbl_SpecialFares WHERE ID = @ID";
        const string SQL_SpecialFares_GetTopSpecialFares = "SELECT TOP(@topNum) ID,Title,Category,IsJump,ContentText,Contact,ContactWay,QQ,AddTime FROM tbl_SpecialFares ORDER BY AddTime DESC";
        #endregion

        #region ISpecialFares 成员

        #region CUD
        /// <summary>
        /// 添加特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool AddSpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_SpecialFares_AddSpecialFares);
            this._database.AddInParameter(comm, "@Title", DbType.String, item.Title);
            this._database.AddInParameter(comm, "@Category", DbType.Byte, (int)item.SpecialFaresType);
            this._database.AddInParameter(comm, "@IsJump", DbType.String, item.IsJump ? "1" : "0");
            this._database.AddInParameter(comm, "@ContentText", DbType.String, item.ContentText);
            this._database.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._database.AddInParameter(comm, "@ContactWay", DbType.String, item.ContactWay);
            this._database.AddInParameter(comm, "@QQ", DbType.String, item.QQ);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;

        }

        /// <summary>
        /// 修改特价机票信息
        /// </summary>
        /// <param name="item">特价机票实体对象</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool ModifySpecialFares(EyouSoft.Model.TicketStructure.SpecialFares item)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_SpecialFares_ModifySpecialFares);
            this._database.AddInParameter(comm, "@Title", DbType.String, item.Title);
            this._database.AddInParameter(comm, "@Category", DbType.Byte, (int)item.SpecialFaresType);
            this._database.AddInParameter(comm, "@IsJump", DbType.String, item.IsJump ? "1" : "0");
            this._database.AddInParameter(comm, "@ContentText", DbType.String, item.ContentText);
            this._database.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._database.AddInParameter(comm, "@ContactWay", DbType.String, item.ContactWay);
            this._database.AddInParameter(comm, "@QQ", DbType.String, item.QQ);
            this._database.AddInParameter(comm, @"ID", DbType.Int32, item.ID);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;

        }

        /// <summary>
        /// 单个/批量删除特价机票信息
        /// </summary>
        /// <param name="ids">特价机票编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool DeleteSpecialFares(string ids)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_SpecialFares_DeleteSpecialFares);
            this._database.AddInParameter(comm, "@ids", DbType.String, ids);

            return DbHelper.ExecuteSqlTrans(comm, this._database) > 0 ? true : false;
        }
        #endregion

        #region R
        /// <summary>
        /// 根据编号获取特价机票信息
        /// </summary>
        /// <param name="id">特价机票编号</param>
        /// <returns>特价机票实体对象</returns>
        public virtual EyouSoft.Model.TicketStructure.SpecialFares GetSpecialFare(int id)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_SpecialFares_GetSpecialFare);
            this._database.AddInParameter(comm, "@ID", DbType.Int32, id);

            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._database))
            {
                if (reader.Read())
                {
                    return new EyouSoft.Model.TicketStructure.SpecialFares()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString(),
                        SpecialFaresType = (EyouSoft.Model.TicketStructure.SpecialFaresType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.SpecialFaresType), reader["Category"].ToString()),
                        IsJump = reader["IsJump"].ToString().Equals("1") ? true : false,
                        ContentText = reader.IsDBNull(reader.GetOrdinal("ContentText")) ? "" : reader["ContentText"].ToString(),
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? "" : reader["Contact"].ToString(),
                        ContactWay = reader.IsDBNull(reader.GetOrdinal("ContactWay")) ? "" : reader["ContactWay"].ToString(),
                        QQ = reader.IsDBNull(reader.GetOrdinal("QQ")) ? "" : reader["QQ"].ToString(),
                        AddTime = DateTime.Parse(reader["AddTime"].ToString())
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// 指定条数获取特价机票信息
        /// </summary>
        /// <param name="topNum">获取数量</param>
        /// <returns>特价机票集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.SpecialFares> GetTopSpecialFares(int topNum)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_SpecialFares_GetTopSpecialFares);
            this._database.AddInParameter(comm, "@topNum", DbType.Int32, topNum);

            IList<EyouSoft.Model.TicketStructure.SpecialFares> list = new List<EyouSoft.Model.TicketStructure.SpecialFares>();

            EyouSoft.Model.TicketStructure.SpecialFares item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.TicketStructure.SpecialFares()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString(),
                        SpecialFaresType = (EyouSoft.Model.TicketStructure.SpecialFaresType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.SpecialFaresType), reader["Category"].ToString()),
                        IsJump = reader["IsJump"].ToString().Equals("1") ? true : false,
                        ContentText = reader.IsDBNull(reader.GetOrdinal("ContentText")) ? "" : reader["ContentText"].ToString(),
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? "" : reader["Contact"].ToString(),
                        ContactWay = reader.IsDBNull(reader.GetOrdinal("ContactWay")) ? "" : reader["ContactWay"].ToString(),
                        QQ = reader.IsDBNull(reader.GetOrdinal("QQ")) ? "" : reader["QQ"].ToString(),
                        AddTime = DateTime.Parse(reader["AddTime"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取特价机票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>特价机票集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.SpecialFares> GetSpecialFares(int pageSize, int pageIndex, ref int recordCount)
        {
            string fileds = "ID,Title,Category,IsJump,ContentText,Contact,ContactWay,QQ,AddTime";
            string orderBy = " AddTime DESC";
            string query = string.Empty;
            string tableName = "tbl_SpecialFares";
            string primaryKey = "ID";
            IList<EyouSoft.Model.TicketStructure.SpecialFares> list = new List<EyouSoft.Model.TicketStructure.SpecialFares>();

            EyouSoft.Model.TicketStructure.SpecialFares item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.TicketStructure.SpecialFares()
                    {
                        ID = (int)reader["ID"],
                        Title = reader["Title"].ToString(),
                        SpecialFaresType = (EyouSoft.Model.TicketStructure.SpecialFaresType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.SpecialFaresType), reader["Category"].ToString()),
                        IsJump = reader["IsJump"].ToString().Equals("1") ? true : false,
                        ContentText = reader.IsDBNull(reader.GetOrdinal("ContentText")) ? "" : reader["ContentText"].ToString(),
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? "" : reader["Contact"].ToString(),
                        ContactWay = reader.IsDBNull(reader.GetOrdinal("ContactWay")) ? "" : reader["ContactWay"].ToString(),
                        QQ = reader.IsDBNull(reader.GetOrdinal("QQ")) ? "" : reader["QQ"].ToString(),
                        AddTime = DateTime.Parse(reader["AddTime"].ToString())
                    };
                    list.Add(item);
                }
            }
            return list;
        }
        #endregion
        #endregion
    }
}
