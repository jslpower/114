using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.BackgroundServices
{
    public class AdvServices : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.BackgroundServices.IAdvServices
    {
        /// <summary>
        /// 获取广告位置
        /// </summary>
        /// <returns></returns>
        public IList<string> GetPostion()
        {
            IList<string> list = new List<string>();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT AreaName FROM tbl_SysAdvArea");
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.MySQLStore))
            {
                while (dr.Read())
                {
                    list.Add(dr.GetString(0));
                }
            }
            return list;
        }
        /// <summary>
        /// 同步数据
        /// </summary>
        public void RunUpdate()
        {
            DbCommand dc = this.SystemStore.GetStoredProcCommand("");
            this.SystemStore.ExecuteNonQuery(dc);
        }
    }
}
