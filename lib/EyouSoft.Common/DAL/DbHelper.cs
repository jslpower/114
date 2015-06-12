using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace EyouSoft.Common.DAL
{
    /// <summary>
    /// 数据库访问方法类
    /// </summary>
    public static class DbHelper
    {
        #region 成员方法

        /// <summary>
        /// 执行SQL语句,返回数据是否存在
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns></returns>
        public static bool Exists(DbCommand dc, Database db)
        {
            object obj = GetSingle(dc, db);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(DbCommand dc, Database db)
        {           
            try
            {
                PrepareCommand(ref dc, db);
                int rows = db.ExecuteNonQuery(dc);
                return rows;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行SQL语句事务，返回影响的记录数
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlTrans(DbCommand dc, Database db)
        {
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    int rows = 0;
                    try
                    {
                        PrepareCommand(ref dc, db);
                        rows = db.ExecuteNonQuery(dc);
                        transaction.Commit();
                    }
                    catch (System.Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message);
                    }
                    connection.Close();
                    return rows;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回IDataReader
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>IDataReader</returns>
        public static IDataReader ExecuteReader(DbCommand dc, Database db)
        {
            IDataReader myReader = null; 
            try
            {
                PrepareCommand(ref dc, db);
                myReader = db.ExecuteReader(dc);
                return myReader;
            }
            catch (System.Exception e)
            {
                if(myReader!=null)
                    myReader.Close();  
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(DbCommand dc, Database db)
        {
            DataSet ds = new DataSet();
            try
            {
                PrepareCommand(ref dc, db);
                ds = db.ExecuteDataSet(dc);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return ds;
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>DataTable</returns>
        public static DataTable DataTableQuery(DbCommand dc, Database db)
        {           
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                PrepareCommand(ref dc, db);
                ds = db.ExecuteDataSet(dc);
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    ds.Dispose();
                    ds = null;
                }
                else
                {
                    ds.Dispose();
                    ds = null;
                    return null;
                }
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果(object)。
        /// </summary>
        /// <param name="dc">查询语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>查询结果(object)</returns>
        public static object GetSingle(DbCommand dc , Database db)
        {           
            try
            {
                PrepareCommand(ref dc, db);
                object obj = db.ExecuteScalar(dc);
                if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                {
                    return null;
                }
                else
                {
                    return obj;
                }
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="dc">存储过程语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns></returns>
        public static void RunProcedure(DbCommand dc, Database db)
        {
            try
            {
                PrepareCommand(ref dc, db);
                db.ExecuteNonQuery(dc);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 执行存储过程，获得存储过程的返回值[注:必须在目标存储过程中设置return返回值,否则始终返回0]
        /// </summary>
        /// <param name="dc">存储过程语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <param name="rowsAffected">执行后所影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(DbCommand dc, Database db, out int rowsAffected)
        {
            int result = 0;
            try
            {
                PrepareCommand(ref dc, db);
                db.AddParameter(dc, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue,string.Empty,DataRowVersion.Default,null);
                rowsAffected = db.ExecuteNonQuery(dc);
                result = (int)dc.Parameters[db.BuildParameterName("ReturnValue")].Value;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，获得存储过程的返回值[注:必须在目标存储过程中设置return返回值,否则始终返回0]
        /// </summary>
        /// <param name="dc">存储过程语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns></returns>
        public static int RunProcedureWithResult(DbCommand dc, Database db)
        {
            int result = 0;
            try
            {
                PrepareCommand(ref dc, db);
                db.AddParameter(dc, "ReturnValue", DbType.Int32, ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Default, null);
                db.ExecuteNonQuery(dc);
                result = (int)dc.Parameters[db.BuildParameterName("ReturnValue")].Value;
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，返回返回IDataReader		
        /// </summary>
        /// <param name="dc">存储过程语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>返回IDataReader</returns>
        public static IDataReader RunReaderProcedure(DbCommand dc, Database db)
        {
            IDataReader myReader = null;
            try
            {
                PrepareCommand(ref dc, db);
                myReader = db.ExecuteReader(dc);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return myReader;
        }

        /// <summary>
        /// 执行存储过程，返回返回DataSet
        /// </summary>
        /// <param name="dc">存储过程语句</param>
        /// <param name="db">操作目标数据库</param>
        /// <returns>DataSet</returns>
        public static DataSet RunDataSetProcedure(DbCommand dc, Database db)
        {            
            DataSet dataSet = new DataSet();
            try
            {
                PrepareCommand(ref dc, db);
                dataSet = db.ExecuteDataSet(dc);            
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
            return dataSet;
        }

        /*
        /// <summary>
        /// 非主键 存储过程分页函数(主键为GUID)
        /// </summary>
        /// <param name="db">操作目标数据库</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="tableName">查询的数据库表名</param>
        /// <param name="identityColumnName">主键字段</param>
        /// <param name="fieldsList">要查询的字段</param>
        /// <param name="fieldSearchKey">查询的条件 </param>
        /// <param name="orderString">排序内容</param>
        /// <param name="recordCount">返回总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <returns></returns>
        public static DataSet GetExportDataGuidList(Database db , int pageSize, int pageIndex, string tableName, string identityColumnName, string fieldsList, string fieldSearchKey, string orderString, out int recordCount)
        {
            DbCommand dc = db.GetStoredProcCommand("T_StocPage");
            db.AddOutParameter(dc, "@RecordCount", DbType.Int32 , 4);
            db.AddOutParameter(dc, "@PageCount", DbType.Int32, 4);
            db.AddInParameter(dc, "@PageIndex", DbType.Int32);
            db.AddInParameter(dc, "@TableName", DbType.Int32);
            db.AddInParameter(dc, "@IdentityColumnName", DbType.Int32);
            db.AddInParameter(dc, "@FieldsList", DbType.Int32);
            db.AddInParameter(dc, "@FieldSearchKey", DbType.Int32);
            db.AddInParameter(dc, "@OrderString", DbType.Int32);
            db.AddInParameter(dc, "@RecordCount", DbType.Int32);
            
            dc.Parameters[2].Value = pageSize;
            dc.Parameters[3].Value = pageIndex;
            dc.Parameters[4].Value = tableName;
            dc.Parameters[5].Value = identityColumnName;
            dc.Parameters[6].Value = fieldsList;
            dc.Parameters[7].Value = fieldSearchKey;
            dc.Parameters[8].Value = orderString;
            DataSet ds = RunDataSetProcedure(dc, db);
            recordCount = (int)dc.Parameters[0].Value;
            return ds;
        }*/

        /// <summary>
        /// 执行分页存储过程,返回IDataReader
        /// </summary>
        /// <param name="db">操作目标数据库</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tableName">查询的数据库表名</param>
        /// <param name="identityColumnName">主键字段</param>
        /// <param name="fields">要查询的字段</param>
        /// <param name="query">查询的条件</param>
        /// <param name="orderByString">排序</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(Database db, int pageSize, int pageIndex, ref int recordCount, string tableName, string identityColumnName, string fields, string query, string orderByString)
        {
            DbCommand cmd = db.GetStoredProcCommand("T_StocPage");
            db.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            db.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "TableName", DbType.String, tableName.Trim());
            //db.AddInParameter(cmd, "IdentityColumnName", DbType.String, identityColumnName);
            db.AddInParameter(cmd, "FieldsList", DbType.String, fields);
            db.AddInParameter(cmd, "FieldSearchKey", DbType.String, query);
            db.AddInParameter(cmd, "OrderString", DbType.String, orderByString);
            db.AddInParameter(cmd, "IsGroupBy", DbType.String, "0");

            IDataReader rdr = RunReaderProcedure(cmd, db);

            if (rdr.Read())
            {
                recordCount = rdr.GetInt32(0);
            }

            rdr.NextResult();

            return rdr;
        }

        /// <summary>
        /// 执行分页存储过程,返回IDataReader
        /// </summary>
        /// <param name="db">操作目标数据库</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tableName">查询的数据库表名</param>
        /// <param name="identityColumnName">主键字段</param>
        /// <param name="fields">要查询的字段</param>
        /// <param name="query">查询的条件</param>
        /// <param name="orderByString">排序</param>
        /// <param name="IsGroupBy">条件是否分组</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(Database db, int pageSize, int pageIndex, ref int recordCount, string tableName, string identityColumnName, string fields, string query, string orderByString, bool IsGroupBy)
        {
            DbCommand cmd = db.GetStoredProcCommand("T_StocPage");
            db.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            db.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "TableName", DbType.String, tableName.Trim());
            //db.AddInParameter(cmd, "IdentityColumnName", DbType.String, identityColumnName);
            db.AddInParameter(cmd, "FieldsList", DbType.String, fields);
            db.AddInParameter(cmd, "FieldSearchKey", DbType.String, query);
            db.AddInParameter(cmd, "OrderString", DbType.String, orderByString);
            db.AddInParameter(cmd, "IsGroupBy", DbType.String, IsGroupBy ? "1" : "0");

            IDataReader rdr = RunReaderProcedure(cmd, db);

            if (rdr.Read())
            {
                recordCount = rdr.GetInt32(0);
            }

            rdr.NextResult();

            return rdr;
        }

        /// <summary>
        /// 执行派生表分页存储过程,返回IDataReader
        /// </summary>
        /// <param name="db">操作目标数据库</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tableName">派生表</param>
        /// <param name="fields">要查询的字段</param>
        /// <param name="query">查询的条件</param>
        /// <param name="orderByString">排序条件</param>
        /// <param name="isGroupBy">是否分组</param>
        /// <param name="groupString">分组条件</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(Database db, int pageSize, int pageIndex, ref int recordCount, string tableName, string fields, string query, string orderByString, bool isGroupBy, string groupString)
        {
            DbCommand cmd = db.GetStoredProcCommand("T_StocPageForDerived");
            db.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            db.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "TableName", DbType.String, tableName.Trim());
            db.AddInParameter(cmd, "FieldsList", DbType.String, fields);
            db.AddInParameter(cmd, "FieldSearchKey", DbType.String, query);
            db.AddInParameter(cmd, "OrderString", DbType.String, orderByString);
            db.AddInParameter(cmd, "IsGroupBy", DbType.String, isGroupBy ? "1" : "0");
            db.AddInParameter(cmd, "GroupString", DbType.String, groupString);

            IDataReader rdr = RunReaderProcedure(cmd, db);

            if (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    recordCount = rdr.GetInt32(0);
            }

            rdr.NextResult();

            return rdr;
        }

        /// <summary>
        /// Executes the command and returns an System.Data.IDataReader
        /// </summary>
        /// <param name="db">database</param>
        /// <param name="tableName">table name</param>
        /// <param name="fields">fields</param>
        /// <param name="query">query</param>
        /// <param name="orderby">order by string</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(Database db, string tableName, string fields, string query, string orderby)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new System.Exception("dbhelper exception,table name is null or empty.");
            }

            string s = " SELECT ";
            if (string.IsNullOrEmpty(fields)) fields = "*";
            s += fields;
            s += " FROM ";
            s += tableName;

            if (!string.IsNullOrEmpty(query))
            {
                s += " WHERE "+query;
            }

            if (!string.IsNullOrEmpty(orderby))
            {
                s += " ORDER BY " + orderby;
            }

            DbCommand cmd = db.GetSqlStringCommand(s);

            return db.ExecuteReader(cmd);
        }

        #endregion

        #endregion

        #region 私有方法
        /// <summary>
        /// SQL查询参数转换
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static DbCommand PrepareCommand(ref DbCommand dc,Database db)
        {
            if (db is OracleDatabase)
            {
                if (dc.CommandType == CommandType.Text)
                {
                    dc.CommandText = dc.CommandText.Replace('@', ':');
                }
            }
            return dc;
        }
        #endregion
    }
}
