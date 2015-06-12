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

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 航空公司数据DAL
    /// </summary>
    /// 创建人：luofx 2010-10-28
    public class TicketFlightCompany : DALBase, IDAL.TicketStructure.ITicketFlightCompany
    {
        #region Public Methods
        /// <summary>
        /// 获取航空公司集合
        /// </summary>
        /// <param name="AirportName">航空公司名字（null或空时不做条件）</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> GetList(string AirportName)
        {
            IList<EyouSoft.Model.TicketStructure.TicketFlightCompany> ResultList = new List<EyouSoft.Model.TicketStructure.TicketFlightCompany>();
            string StrSql = " SELECT [id],[AirportCode],[AirportName] FROM tbl_TicketFlightCompany ";
            DbCommand dc = null;
            dc = base.TourStore.GetSqlStringCommand(StrSql);
            if (!string.IsNullOrEmpty(AirportName))
            {
                StrSql += " WHERE [AirportName] LIKE  @AirportName ";
                AirportName = "%" + AirportName + "%";
                dc = base.TourStore.GetSqlStringCommand(StrSql);
                base.TourStore.AddInParameter(dc, "AirportName", DbType.String, AirportName);
            }
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                EyouSoft.Model.TicketStructure.TicketFlightCompany model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketFlightCompany();
                    model.Id = dr.GetInt32(dr.GetOrdinal("id"));
                    model.AirportCode = dr.IsDBNull(dr.GetOrdinal("AirportCode")) ? string.Empty : dr.GetString(dr.GetOrdinal("AirportCode"));
                    model.AirportName = dr.IsDBNull(dr.GetOrdinal("AirportName")) ? string.Empty : dr.GetString(dr.GetOrdinal("AirportName"));
                    ResultList.Add(model);
                }
                model = null;
            }
            return ResultList;
        }
        /// <summary>
        /// 获取航空公司实体
        /// </summary>
        /// <param name="id">航空公司id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.TicketFlightCompany GetModel(int id)
        {
            string StrSql = " SELECT [id],[AirportCode],[AirportName] FROM tbl_TicketFlightCompany  WHERE [id]=@Id ";
            DbCommand dc = null;
            dc = base.TourStore.GetSqlStringCommand(StrSql);
            base.TourStore.AddInParameter(dc, "Id", DbType.Int32, id);
            EyouSoft.Model.TicketStructure.TicketFlightCompany model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketFlightCompany();
                    model.Id = dr.GetInt32(dr.GetOrdinal("id"));
                    model.AirportCode = dr.IsDBNull(dr.GetOrdinal("AirportCode")) ? string.Empty : dr.GetString(dr.GetOrdinal("AirportCode"));
                    model.AirportName = dr.IsDBNull(dr.GetOrdinal("AirportName")) ? string.Empty : dr.GetString(dr.GetOrdinal("AirportName"));
                }
            }
            return model;
        }
        #endregion
    }
}
