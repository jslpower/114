using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.Test
{
    /// <summary>
    /// 数据层测试类
    /// </summary>
    public class TestBLL2 : DALBase,IDALTest
    {
        public TestBLL2()
        {

        }

        /// <summary>
        /// 调用主库语句
        /// </summary>
        /// <returns></returns>
        public virtual int TestConn()
        {
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT COUNT(ID) FROM EyouSoft_CustomerList where SystemID=@SystemID");
            this.SystemStore.AddInParameter(dc, "SystemID", DbType.Int32);
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
            return null;
        }
    }
}
