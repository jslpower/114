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

namespace EyouSoft.DAL.OpenStructure
{
    /// <summary>
    /// 同步各平台数据异常信息数据访问
    /// </summary>
    /// 周文超 2011-04-02
    public class DException : DALBase, IDAL.OpenStructure.IDException
    {
        /// <summary>
        /// 数据库
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DException() { this._db = base.OpenStore; }

        #region Sql

        /// <summary>
        /// 写入数据Sql(异常处理结果和时间不写值)
        /// </summary>
        private const string Sql_RelationException_Insert = @" INSERT INTO [tbl_LogOpenRelationException] 
    ([ExceptionId],[SystemType],[InstructionCode],[ExceptionCode],[ExceptionTime],[ExceptionDesc]) 
    VALUES (@ExceptionId{0},@SystemType{0},@InstructionCode{0},@ExceptionCode{0},@ExceptionTime{0},@ExceptionDesc{0}); ";

        #endregion

        #region IMException 成员

        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="list">异常信息实体集合</param>
        /// <returns>返回1成功，其他失败</returns>
        public virtual int AddMException(IList<EyouSoft.Model.OpenStructure.MExceptionInfo> list)
        {
            if (list == null || list.Count <= 0)
                return 0;

            DbCommand dc = this._db.GetSqlStringCommand(" select 1;");
            StringBuilder strSql = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                    continue;

                list[i].ExecptionId = Guid.NewGuid().ToString();
                strSql.AppendFormat(Sql_RelationException_Insert, i);
                this._db.AddInParameter(dc, "ExceptionId" + i, DbType.AnsiStringFixedLength, list[i].ExecptionId);
                this._db.AddInParameter(dc, "SystemType" + i, DbType.Byte, list[i].SystemType);
                this._db.AddInParameter(dc, "InstructionCode" + i, DbType.String, list[i].InstructionCode);
                this._db.AddInParameter(dc, "ExceptionCode" + i, DbType.String, list[i].ExceptionCode);
                this._db.AddInParameter(dc, "ExceptionTime" + i, DbType.DateTime, list[i].ExceptionTime);
                this._db.AddInParameter(dc, "ExceptionDesc" + i, DbType.String, list[i].ExceptionDesc);
            }
            dc.CommandText = strSql.ToString();
            dc.CommandType = CommandType.Text;

            return DbHelper.ExecuteSql(dc, this._db) > 0 ? 1 : 0;
        }

        #endregion
    }
}
