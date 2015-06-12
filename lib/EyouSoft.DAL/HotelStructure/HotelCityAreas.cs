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
    /// 酒店系统-城市区域实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelCityAreas : DALBase, EyouSoft.IDAL.HotelStructure.IHotelCityAreas
    {
        private Database _db = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelCityAreas()
        {
            this._db = base.HotelStore;
        }
        /// <summary>
        /// 获取所有的城市区域信息集合
        /// </summary>
        /// <param name="CityCode">城市三字码（空或null时，获取所有城市信息；不为空时，根据三字码获取相应城市的区域信息集合）</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelCityAreas> GetList(string CityCode)
        {
            IList<EyouSoft.Model.HotelStructure.HotelCityAreas> lists = null;
            EyouSoft.Model.HotelStructure.HotelCityAreas model = null;
            string StrSql = "SELECT [Id],[AreaName],[CityCode] FROM [dbo].[tbl_HotelCityAreas] ";
            if (!string.IsNullOrEmpty(CityCode))
            {
                CityCode = "%" + CityCode + "%";
                StrSql = StrSql + " WHERE [CityCode] like @CityCode";
            }
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            if (!string.IsNullOrEmpty(CityCode))
            {
                this._db.AddInParameter(dc, "CityCode", DbType.AnsiString, CityCode);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                lists = new List<EyouSoft.Model.HotelStructure.HotelCityAreas>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCityAreas();
                    model.AreaName = dr.GetString(dr.GetOrdinal("AreaName"));
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    lists.Add(model);
                    model = null;
                }
            }
            return lists;
        }
        /// <summary>
        /// 获取酒店城市区域信息实体
        /// </summary>
        /// <param name="Id">城市区域主键Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelCityAreas GetModel(int Id)
        {
            EyouSoft.Model.HotelStructure.HotelCityAreas model = null;
            string StrSql = "SELECT [Id],[AreaName],[CityCode] FROM [dbo].[tbl_HotelCityAreas] WHERE [ID]=@ID";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "ID", DbType.Int32, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelCityAreas();
                    model.AreaName = dr.GetString(dr.GetOrdinal("AreaName"));
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                }
            }
            return model;
        }
    }
}
