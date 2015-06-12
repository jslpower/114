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
    /// 酒店系统-地理位置（地标）实体
    /// </summary>
    /// 创建人：luofx 2010-12-1
    public class HotelLandMarks : DALBase, EyouSoft.IDAL.HotelStructure.IHotelLandMarks
    {
        private Database _db = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HotelLandMarks()
        {
            this._db = base.HotelStore;
        }
        /// <summary>
        /// 获取所有的城市地理位置（地标）信息集合
        /// </summary>
        /// <param name="CityCode">城市三字码（空或null时，获取所有城市信息；不为空时，根据三字码获取相应城市的地理位置信息集合）</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.HotelStructure.HotelLandMarks> GetList(string CityCode)
        {
            IList<EyouSoft.Model.HotelStructure.HotelLandMarks> lists = null;
            EyouSoft.Model.HotelStructure.HotelLandMarks model = null;
            string StrSql = "SELECT [Id],[Por],[CityCode] FROM [dbo].[tbl_HotelLandMarks]";
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
                lists = new List<EyouSoft.Model.HotelStructure.HotelLandMarks>();
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelLandMarks();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.Por = dr.GetString(dr.GetOrdinal("Por"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));
                    lists.Add(model);
                    model = null;
                }
            }
            return lists;
        }

        /// <summary>
        /// 获取城市的地理信息集合
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.HotelStructure.HotelLandMarks> GetList(int cityId)
        {
            return null;
        }

        /// <summary>
        /// 获取酒店地理位置信息实体
        /// </summary>
        /// <param name="Id">地理位置主键Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.HotelStructure.HotelLandMarks GetModel(int Id)
        {
            EyouSoft.Model.HotelStructure.HotelLandMarks model = null;
            string StrSql = "SELECT [Id],[Por],[CityCode] FROM [dbo].[tbl_HotelLandMarks] WHERE [ID]=@ID";
            DbCommand dc = this._db.GetSqlStringCommand(StrSql);
            this._db.AddInParameter(dc, "ID", DbType.Int32, Id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.HotelStructure.HotelLandMarks();
                    model.CityCode = dr.GetString(dr.GetOrdinal("CityCode"));
                    model.Por = dr.GetString(dr.GetOrdinal("Por"));
                    model.Id = dr.GetInt32(dr.GetOrdinal("Id"));                    
                }
            }
            return model;
        }
    }
}
