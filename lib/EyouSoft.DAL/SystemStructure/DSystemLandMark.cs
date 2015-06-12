using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;
using System.Data.Common;
using System.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 城市地标
    /// 创建者：郑付杰
    /// 创建时间：2011-11-28
    /// </summary>
    public class DSystemLandMark:DALBase,EyouSoft.IDAL.SystemStructure.ISystemLandMark
    {
        private readonly Database _db = null;

        public DSystemLandMark()
        {
            this._db = base.SystemStore;
        }

        #region ISystemLandMark 成员
        /// <summary>
        /// 获取所有地标
        /// </summary>
        /// <returns></returns>
        public virtual IList<MSystemLandMark> GetList()
        {
            string sql = "SELECT ID,Por,CityId,CityCode FROM tbl_SystemLandMark";
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            IList<MSystemLandMark> list = new List<MSystemLandMark>();

            MSystemLandMark item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MSystemLandMark()
                    {
                        Id = (int)reader["Id"],
                        Por = reader["Por"].ToString(),
                        CityId = (int)reader["CityId"],
                        CityCode = reader.IsDBNull(reader.GetOrdinal("CityCode")) ? string.Empty : reader["CityCode"].ToString()
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
