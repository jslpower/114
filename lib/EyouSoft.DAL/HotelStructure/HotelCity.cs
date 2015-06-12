using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.HotelStructure
{
    /// <summary>
    /// 酒店系统-城市信息实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelCity : DALBase, EyouSoft.IDAL.HotelStructure.IHotelCity
    {
        #region SQL语句
        private const string Sql_Select_HotelCity = "SELECT [ID],[CityName],[Spelling],[SimpleSpelling],[CityCode],[IsHot] FROM [dbo].[tbl_HotelCity] ORDER BY [CityCode] ASC ";
        #endregion
        private Database _db = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelCity()
        {
            this._db = base.HotelStore;
        }
        /// <summary>
        /// 获取所有的城市信息集合
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelCity> GetList()
        {
            IList<EyouSoft.Model.HotelStructure.HotelCity> lists = null;
            EyouSoft.Model.HotelStructure.HotelCity model = null;
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Select_HotelCity);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                lists = new List<EyouSoft.Model.HotelStructure.HotelCity>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCity();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IsHot = dr.GetString(dr.GetOrdinal("IsHot")) == "0" ? false : true;
                    model.SimpleSpelling = dr.IsDBNull(dr.GetOrdinal("SimpleSpelling")) ? "" : dr.GetString(dr.GetOrdinal("SimpleSpelling"));
                    model.Spelling = dr.IsDBNull(dr.GetOrdinal("Spelling")) ? "" : dr.GetString(dr.GetOrdinal("Spelling"));
                    lists.Add(model);
                    model = null;
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="Id">城市主键Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelCity GetModel(int Id)
        {
            EyouSoft.Model.HotelStructure.HotelCity model = null;
            string Sql = "SELECT [ID],[CityName],[Spelling],[SimpleSpelling],[CityCode],[IsHot] FROM [dbo].[tbl_HotelCity] WHERE [ID]=@ID";
            DbCommand dc = this._db.GetSqlStringCommand(Sql);
            this._db.AddInParameter(dc, "ID", DbType.Int16, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCity();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IsHot = dr.GetString(dr.GetOrdinal("IsHot")) == "0" ? false : true;
                    model.SimpleSpelling = dr.IsDBNull(dr.GetOrdinal("SimpleSpelling")) ? "" : dr.GetString(dr.GetOrdinal("SimpleSpelling"));
                    model.Spelling = dr.IsDBNull(dr.GetOrdinal("Spelling")) ? "" : dr.GetString(dr.GetOrdinal("Spelling"));
                }
            }
            return model;
        }
        /// <summary>
        /// 获取酒店城市信息实体
        /// </summary>
        /// <param name="CityCode">城市三字码</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelCity GetModel(string CityCode)
        {
            EyouSoft.Model.HotelStructure.HotelCity model = null;
            string Sql = "SELECT [ID],[CityName],[Spelling],[SimpleSpelling],[CityCode],[IsHot] FROM [dbo].[tbl_HotelCity] WHERE [CityCode]=@CityCode";
            DbCommand dc = this._db.GetSqlStringCommand(Sql);
            this._db.AddInParameter(dc, "CityCode", DbType.AnsiString, CityCode);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCity();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.CityName = dr.GetString(dr.GetOrdinal("CityName"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    model.IsHot = dr.GetString(dr.GetOrdinal("IsHot")) == "0" ? false : true;
                    model.SimpleSpelling = dr.IsDBNull(dr.GetOrdinal("SimpleSpelling")) ? "" : dr.GetString(dr.GetOrdinal("SimpleSpelling"));
                    model.Spelling = dr.IsDBNull(dr.GetOrdinal("Spelling")) ? "" : dr.GetString(dr.GetOrdinal("Spelling"));
                }
            }
            return model;
        }
    }
}
