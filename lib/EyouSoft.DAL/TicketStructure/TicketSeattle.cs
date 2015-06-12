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
    /// 机场信息数据DAL
    /// </summary>
    /// 创建人：luofx 2010-10-28
    public class TicketSeattle : DALBase, IDAL.TicketStructure.ITicketSeattle
    {
        #region Public Methods
        /// <summary>
        /// 获取机场信息集合
        /// </summary>
        /// <param name="InputString">输入的字符串</param>
        /// <param name="SeattleId">与运价关联的始发地编号</param>
        /// <param name="IsFreight">是否与运价关联</param>
        /// <returns>返回符合条件的机场信息集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketSeattle> GetList(string InputString, int SeattleId, bool IsFreight)
        {
            IList<EyouSoft.Model.TicketStructure.TicketSeattle> ResultList = new List<EyouSoft.Model.TicketStructure.TicketSeattle>();
            string StrSql = " SELECT [SeattleId],[LKE],[LongName],[Seattle],[ShortName],[IsHot] FROM tbl_TicketSeattle ";
            DbCommand dc = base.TourStore.GetSqlStringCommand(StrSql); ;
            if (!string.IsNullOrEmpty(InputString))
            {
                StrSql += " WHERE [Seattle] LIKE @Seattle ";
                InputString = "%" + InputString + "%";
                dc = base.TourStore.GetSqlStringCommand(StrSql);
                base.TicketStore.AddInParameter(dc, "Seattle", DbType.String, InputString);
            }
            else if (IsFreight)
            {
                StrSql += " WHERE 1=1 ";
                if (SeattleId==0)
                {
                    StrSql += " AND SeattleId IN (SELECT NoGadHomeCityId from dbo.tbl_TicketFreightInfo WHERE IsEnabled=1)";
                    dc = base.TicketStore.GetSqlStringCommand(StrSql);
                }
                else
                {
                    StrSql += " AND SeattleId IN (SELECT NoGadDestCityId from dbo.tbl_TicketFreightInfo WHERE NoGadHomeCityId = @SeattleId AND IsEnabled=1)";
                    dc = base.TicketStore.GetSqlStringCommand(StrSql);
                    base.TicketStore.AddInParameter(dc, "SeattleId", DbType.Int32, SeattleId);
                }
            }
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                EyouSoft.Model.TicketStructure.TicketSeattle model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketSeattle();
                    model.SeattleId = dr.GetInt32(dr.GetOrdinal("SeattleId"));
                    model.LKE = dr.IsDBNull(dr.GetOrdinal("LKE")) ? string.Empty : dr.GetString(dr.GetOrdinal("LKE"));
                    model.LongName = dr.IsDBNull(dr.GetOrdinal("LongName")) ? string.Empty : dr.GetString(dr.GetOrdinal("LongName"));
                    model.Seattle = dr.IsDBNull(dr.GetOrdinal("Seattle")) ? string.Empty : dr.GetString(dr.GetOrdinal("Seattle"));
                    model.ShortName = dr.IsDBNull(dr.GetOrdinal("ShortName")) ? string.Empty : dr.GetString(dr.GetOrdinal("ShortName"));
                    model.IsHot = dr.GetString(dr.GetOrdinal("IsHot")) != "1" ? false : true;
                    ResultList.Add(model);
                }
                model = null;
            }
            return ResultList;
        }
        /// <summary>
        /// 获取机场信息实体
        /// </summary>
        /// <param name="SeattleId">机场信息Id</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.TicketSeattle GetModel(int SeattleId)
        {
            string StrSql = " SELECT [SeattleId],[LKE],[LongName],[Seattle],[ShortName],[IsHot] FROM tbl_TicketSeattle  WHERE [SeattleId]=@SeattleId ";
            DbCommand dc = null;
            dc = base.TourStore.GetSqlStringCommand(StrSql);
            base.TicketStore.AddInParameter(dc, "SeattleId", DbType.Int32, SeattleId);
            EyouSoft.Model.TicketStructure.TicketSeattle model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketSeattle();
                    model.SeattleId = dr.GetInt32(dr.GetOrdinal("SeattleId"));
                    model.LKE = dr.IsDBNull(dr.GetOrdinal("LKE")) ? string.Empty : dr.GetString(dr.GetOrdinal("LKE"));
                    model.LongName = dr.IsDBNull(dr.GetOrdinal("LongName")) ? string.Empty : dr.GetString(dr.GetOrdinal("LongName"));
                    model.Seattle = dr.IsDBNull(dr.GetOrdinal("Seattle")) ? string.Empty : dr.GetString(dr.GetOrdinal("Seattle"));
                    model.ShortName = dr.IsDBNull(dr.GetOrdinal("ShortName")) ? string.Empty : dr.GetString(dr.GetOrdinal("ShortName"));
                    model.IsHot = dr.GetString(dr.GetOrdinal("IsHot")) != "1" ? false : true;
                }
            }
            return model;
        }
        /// <summary>
        /// 根据目的地获取机场信息ID主键
        /// </summary>
        /// <param name="SeattleName">机场名字</param>
        /// <returns>0:无该机场信息；大于0:有机场信息</returns>
        public virtual int GetSeattleIdByName(string SeattleName)
        {
            int SeattleId = 0;
            string StrSql = " SELECT [SeattleId] FROM tbl_TicketSeattle  WHERE [Seattle]=@Seattle ";
            DbCommand dc = null;
            dc = base.TourStore.GetSqlStringCommand(StrSql);
            base.TicketStore.AddInParameter(dc, "Seattle", DbType.String, SeattleName);           
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.TicketStore))
            {
                while (dr.Read())
                {
                    SeattleId = dr.GetInt32(dr.GetOrdinal("SeattleId"));                  
                }
            }
            return SeattleId;
        }
        // 添加机场信息
        //public int InsertInto(EyouSoft.Model.TicketStructure.TicketSeattle model)
        //{
        //    string sql = "IF NOT exists(SELECT 1 FROM  tbl_TicketSeattle WHERE  Seattle = @Seattle and LKE = @LKE AND LongName=@LongName AND ShortName=@ShortName) insert into tbl_TicketSeattle (Seattle,LKE,LongName,ShortName,IsHot) values(@Seattle,@LKE,@LongName,@ShortName,@IsHot)";
        //    DbCommand dc = base.TicketStore.GetSqlStringCommand(sql);
        //    base.TicketStore.AddInParameter(dc, "Seattle", DbType.String, model.Seattle);
        //    base.TicketStore.AddInParameter(dc, "LKE", DbType.String, model.LKE);
        //    base.TicketStore.AddInParameter(dc, "LongName", DbType.String, model.LongName);
        //    base.TicketStore.AddInParameter(dc, "ShortName", DbType.String, model.ShortName);
        //    base.TicketStore.AddInParameter(dc, "IsHot", DbType.Boolean, model.IsHot);
        //    return DbHelper.ExecuteSql(dc, base.TicketStore);
        //}

        #endregion
    }
}
