using System;
using System.Collections.Generic;
using EyouSoft.Common.DAL;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统国家数据层
    /// </summary>
    public class DSysCountry : DALBase, IDAL.SystemStructure.ISysCountry
    {
        /// <summary>
        /// 数据库链接对象
        /// </summary>
        private readonly Database _db;

        /// <summary>
        /// 国家表查询sql
        /// </summary>
        private const string SqlCountrySelect = @"SELECT [Id]
      ,[EnName]
      ,[Zones]
      ,[CName]
      ,[Continent]
  FROM [dbo].[tbl_SysCountry]";

        /// <summary>
        /// 构造函数
        /// </summary>
        public DSysCountry()
        {
            _db = SystemStore;
        }

        /// <summary>
        /// 获取国家信息实体
        /// </summary>
        /// <param name="countryId">国家编号</param>
        /// <returns></returns>
        public Model.SystemStructure.MSysCountry GetCountry(int countryId)
        {
            Model.SystemStructure.MSysCountry model = null;
            if (countryId <= 0)
                return model;

            DbCommand dc = _db.GetSqlStringCommand(SqlCountrySelect + " where [Id] = @Id ");
            _db.AddInParameter(dc, "Id", DbType.Int32, countryId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    model = new Model.SystemStructure.MSysCountry();

                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.CountryId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EnName")))
                        model.EnName = dr.GetString(dr.GetOrdinal("EnName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Zones")))
                        model.Zones = dr.GetInt32(dr.GetOrdinal("Zones"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CName")))
                        model.CName = dr.GetString(dr.GetOrdinal("CName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Continent")))
                        model.Continent = (Model.SystemStructure.Continent)dr.GetByte(dr.GetOrdinal("Continent"));
                }
            }

            return model;
        }

        /// <summary>
        /// 获取所有的国家信息
        /// </summary>
        /// <returns></returns>
        public IList<Model.SystemStructure.MSysCountry> GetCountryList()
        {
            IList<Model.SystemStructure.MSysCountry> list;
            DbCommand dc = _db.GetSqlStringCommand(SqlCountrySelect);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<Model.SystemStructure.MSysCountry>();
                while (dr.Read())
                {
                    var model = new Model.SystemStructure.MSysCountry();

                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.CountryId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EnName")))
                        model.EnName = dr.GetString(dr.GetOrdinal("EnName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Zones")))
                        model.Zones = dr.GetInt32(dr.GetOrdinal("Zones"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CName")))
                        model.CName = dr.GetString(dr.GetOrdinal("CName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Continent")))
                        model.Continent = (Model.SystemStructure.Continent)dr.GetByte(dr.GetOrdinal("Continent"));

                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 根据地理洲际获取国家信息
        /// </summary>
        /// <param name="continent">地理洲际编号</param>
        /// <returns></returns>
        public IList<Model.SystemStructure.MSysCountry> GetCountryList(params Model.SystemStructure.Continent[] continent)
        {
            IList<Model.SystemStructure.MSysCountry> list;
            string strSql = SqlCountrySelect;
            if (continent != null && continent.Length > 0)
            {
                strSql += " where [Continent] ";
                if(continent.Length == 1)
                {
                    strSql += string.Format(" = {0}", (int)continent[0]);
                }
                else
                {
                    string strIds = string.Empty;
                    foreach (var t in strSql)
                    {
                        strIds += Convert.ToInt32(t) + ",";
                    }
                    if (!string.IsNullOrEmpty(strIds))
                        strIds = strIds.TrimEnd(',');

                    strSql += string.Format(" in ({0})", strIds);
                }
            }
            DbCommand dc = _db.GetSqlStringCommand(strSql);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                list = new List<Model.SystemStructure.MSysCountry>();
                while (dr.Read())
                {
                    var model = new Model.SystemStructure.MSysCountry();

                    if (!dr.IsDBNull(dr.GetOrdinal("Id")))
                        model.CountryId = dr.GetInt32(dr.GetOrdinal("Id"));
                    if (!dr.IsDBNull(dr.GetOrdinal("EnName")))
                        model.EnName = dr.GetString(dr.GetOrdinal("EnName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Zones")))
                        model.Zones = dr.GetInt32(dr.GetOrdinal("Zones"));
                    if (!dr.IsDBNull(dr.GetOrdinal("CName")))
                        model.CName = dr.GetString(dr.GetOrdinal("CName"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Continent")))
                        model.Continent = (Model.SystemStructure.Continent)dr.GetByte(dr.GetOrdinal("Continent"));

                    list.Add(model);
                }
            }

            return list;
        }
    }
}
