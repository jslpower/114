using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 县区数据层
    /// 创建人：郑付杰
    /// 创建时间:2011/10/24
    /// </summary>
    public class SysDistrictCounty:DALBase, EyouSoft.IDAL.SystemStructure.ISysDistrictCounty
    {
        #region private 
        private Database _db = null;

        private const string GetDistrictCounty_SQL = "SELECT Id,DistrictName,ProvinceId,CityId,HeaderLetter FROM tbl_sysDistrictCounty";
        private const string GetModel_SQL = "SELECT Id,DistrictName,ProvinceId,CityId,HeaderLetter FROM tbl_sysDistrictCounty WHERE Id = @Id";

        #endregion
        #region 构造函数
        public SysDistrictCounty()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region ISysDistrictCounty 成员
        /// <summary>
        /// 获取所有县区
        /// </summary>
        /// <returns>县区集合</returns>
        public virtual IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> GetDistrictCounty()
        {
            DbCommand comm = this._db.GetSqlStringCommand(GetDistrictCounty_SQL);
            IList<EyouSoft.Model.SystemStructure.SysDistrictCounty> list = new List<EyouSoft.Model.SystemStructure.SysDistrictCounty>();
            EyouSoft.Model.SystemStructure.SysDistrictCounty item = null;
          
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new EyouSoft.Model.SystemStructure.SysDistrictCounty()
                    {
                       Id = (int)reader["Id"],
                       ProvinceId = (int)reader["ProvinceId"],
                       CityId = (int)reader["CityId"],
                       DistrictName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                       HeaderLetter = reader.IsDBNull(reader.GetOrdinal("HeaderLetter")) ? string.Empty : reader["HeaderLetter"].ToString()
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// 获取县区实体
        /// </summary>
        /// <param name="districtId">县区编号</param>
        /// <returns>县区实体</returns>
        public virtual Model.SystemStructure.SysDistrictCounty GetModel(int districtId)
        {
            DbCommand comm = this._db.GetSqlStringCommand(GetModel_SQL);
            this._db.AddInParameter(comm, "@Id", DbType.Int32, districtId);
            EyouSoft.Model.SystemStructure.SysDistrictCounty item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    item = new EyouSoft.Model.SystemStructure.SysDistrictCounty()
                    {
                        Id = (int)reader["Id"],
                        ProvinceId = (int)reader["ProvinceId"],
                        CityId = (int)reader["CityId"],
                        DistrictName = reader.IsDBNull(reader.GetOrdinal("DistrictName")) ? string.Empty : reader["DistrictName"].ToString(),
                        HeaderLetter = reader.IsDBNull(reader.GetOrdinal("HeaderLetter")) ? string.Empty : reader["HeaderLetter"].ToString()
                    };
                }
            }

            return item;
        }
        #endregion
    }
}
