using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.Oracle.DAL.Test
{
    /// <summary>
    /// 数据层测试类
    /// </summary>
    public class TestBLL2 : EyouSoft.DAL.TestBLL
    {
        public TestBLL2()
        {

        }

        ///// <summary>
        ///// 调用主库语句
        ///// </summary>
        ///// <returns></returns>
        //public override int TestConn()
        //{
        //    DbCommand dc = this.MasterStore.GetSqlStringCommand("SELECT COUNT(ID) FROM EyouSoft_CustomerList where SystemID=@SystemID");
        //    this.MasterStore.AddInParameter(dc, "SystemID", DbType.Int32);
        //    return Convert.ToInt32(DbHelper.GetSingle(dc, this.MasterStore));
        //}

        ///// <summary>
        ///// 调用从库存储过程
        ///// </summary>
        ///// <returns></returns>
        //public override int TestData()
        //{
        //    DbCommand dc = this.SlaveStore.GetStoredProcCommand("EyouSoft_GetSingleCount");
        //    this.SlaveStore.AddInParameter(dc, "OperationID", DbType.Int32, 329);
        //    this.SlaveStore.AddOutParameter(dc, "RowCount", DbType.Int32, 8);
        //    DbHelper.RunProcedure(dc, this.SlaveStore);
        //    return Convert.ToInt32(SlaveStore.GetParameterValue(dc, "RowCount"));
        //}
    }
}
