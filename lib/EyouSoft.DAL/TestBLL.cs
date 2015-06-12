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

namespace EyouSoft.DAL
{
    /// <summary>
    /// 数据层测试类
    /// </summary>
    public class TestBLL : DALBase,IDALTest
    {
        public TestBLL()
        {
                      
        }

        /// <summary>
        /// 调用主库语句
        /// </summary>
        /// <returns></returns>
        public virtual int TestConn()
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT COUNT(*) FROM tbl_LogTour");
            this.SystemStore.AddInParameter(dc, "SystemID", DbType.Int32,0);
            return Convert.ToInt32(DbHelper.GetSingle(dc, this.SystemStore));        
        }

        /// <summary>
        /// 调用从库存储过程
        /// </summary>
        /// <returns></returns>
        public virtual int TestData()
        {
            DbCommand dc = this.SystemStore.GetStoredProcCommand("EyouSoft_GetSingleCount");
            this.SystemStore.AddInParameter(dc, "OperationID", DbType.Int32, 329);
            this.SystemStore.AddOutParameter(dc, "RowCount", DbType.Int32, 8);
            DbHelper.RunProcedure(dc, this.SystemStore); 
            return Convert.ToInt32(SystemStore.GetParameterValue(dc, "RowCount"));
        }

        public virtual object WT()
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT COUNT(Id) AS NUM FROM tbl_SysCity");

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this.SystemStore))
            {
                while (rdr.Read())
                {
                    int tmp1 = rdr.GetInt32(rdr.GetOrdinal("NUM"));
                    int tmp2 = rdr.GetInt32(0);
                }
            }

            return null;
        }
    }
}
