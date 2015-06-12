using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 机票平台-国籍基础信息DAL
    /// </summary>
    /// 创建人：luofx 2010-11-8
    public class TicketNationInfo : DALBase, IDAL.TicketStructure.ITicketNationInfo
    {
        /// <summary>
        /// 获取国家信息列表
        /// </summary>
        /// <param name="CountryName">国家名称（为空时：查询所有；不为空时，按国家名称筛选）</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.TicketStructure.TicketNationInfo> GetList(string CountryName)
        {
            IList<EyouSoft.Model.TicketStructure.TicketNationInfo> ResultList = new List<EyouSoft.Model.TicketStructure.TicketNationInfo>();
            string StrSql = "SELECT [Id] AS NationId,[CountryCode],[CountryName] FROM [tbl_TicketNationInfo] ";
            DbCommand dc = null;
            if (!string.IsNullOrEmpty(CountryName))
            {
                CountryName = "%" + CountryName + "%";
                StrSql += " WHERE [CountryName] LIKE @CountryName";
                dc = this.TicketStore.GetSqlStringCommand(StrSql);
                this.TicketStore.AddInParameter(dc, "CountryName", DbType.String, CountryName);
            }
            StrSql += " ORDER BY [CountryCode] ASC";
            if (dc == null) dc = this.TicketStore.GetSqlStringCommand(StrSql);
            EyouSoft.Model.TicketStructure.TicketNationInfo model = null;
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this.TicketStore))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.TicketStructure.TicketNationInfo();
                    model.NationId = dr.GetInt32(dr.GetOrdinal("NationId"));
                    model.CountryCode = dr.IsDBNull(dr.GetOrdinal("CountryCode")) ? "" : dr.GetString(dr.GetOrdinal("CountryCode"));
                    model.CountryName = dr.IsDBNull(dr.GetOrdinal("CountryName")) ? "" : dr.GetString(dr.GetOrdinal("CountryName"));
                    ResultList.Add(model);
                }
            }
            model = null;
            return ResultList;
        }
    }
}
