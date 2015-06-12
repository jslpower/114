using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.DAL;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ScenicStructure
{
    /// <summary>
    /// 门票
    /// 创建者:郑付杰
    /// 创建时间:2011/10/28
    /// </summary>
    public class DScenicTickets:DALBase,EyouSoft.IDAL.ScenicStructure.IScenicTickets
    {
        private readonly Database _db = null;

        public DScenicTickets()
        {
            this._db = base.SystemStore;
        }

        #region IScenicTickets 成员
        /// <summary>
        /// 添加门票
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Add(MScenicTickets item)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO tbl_ScenicTickets(TicketsId,ScenicId,TypeName,RetailPrice,WebsitePrices,MarketPrice,DistributionPrice");
            sql.Append(",Limit,Payment,StartTime,EndTime,Description,SaleDescription,Status,CustomOrder,B2B,B2BOrder,B2C,B2COrder");
            sql.Append(",Operator,CompanyId,ExamineStatus,EnName,ExamineOperator) VALUES(@TicketsId,@ScenicId,@TypeName,@RetailPrice,@WebsitePrices,@MarketPrice,@DistributionPrice");
            sql.Append(",@Limit,@Payment,@StartTime,@EndTime,@Description,@SaleDescription,@Status,@CustomOrder,@B2B,@B2BOrder,@B2C");
            sql.Append(",@B2COrder,@Operator,@CompanyId,@ExamineStatus,@EnName,@ExamineOperator)");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(comm,"@TicketsId",DbType.AnsiStringFixedLength,item.TicketsId);
            this._db.AddInParameter(comm,"@ScenicId",DbType.AnsiStringFixedLength,item.ScenicId);
            this._db.AddInParameter(comm,"@TypeName",DbType.String,item.TypeName);
            this._db.AddInParameter(comm,"@RetailPrice",DbType.Currency,item.RetailPrice);
            this._db.AddInParameter(comm,"@WebsitePrices",DbType.Currency,item.WebsitePrices);
            this._db.AddInParameter(comm,"@MarketPrice",DbType.Currency,item.MarketPrice);
            this._db.AddInParameter(comm,"@DistributionPrice",DbType.Currency,item.DistributionPrice);
            this._db.AddInParameter(comm,"@Limit",DbType.Int32,item.Limit);
            this._db.AddInParameter(comm,"@Payment",DbType.Byte,(int)item.Payment);
            this._db.AddInParameter(comm,"@StartTime",DbType.DateTime,item.StartTime);
            
            this._db.AddInParameter(comm,"@EndTime",DbType.DateTime,item.EndTime);
            this._db.AddInParameter(comm,"@Description",DbType.String,item.Description);
            this._db.AddInParameter(comm,"@SaleDescription",DbType.String,item.SaleDescription);
            this._db.AddInParameter(comm,"@Status",DbType.Byte,(int)item.Status);
            this._db.AddInParameter(comm,"@CustomOrder",DbType.Int32,item.CustomOrder);
            this._db.AddInParameter(comm,"@B2B",DbType.Byte,(int)item.B2B);
            this._db.AddInParameter(comm,"@B2BOrder",DbType.Int32,item.B2BOrder);
            this._db.AddInParameter(comm,"@B2C",DbType.Byte,(int)item.B2C);
            this._db.AddInParameter(comm,"@B2COrder",DbType.Int32,item.B2COrder);
            this._db.AddInParameter(comm,"@Operator",DbType.AnsiStringFixedLength,item.Operator);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, item.CompanyId);
            this._db.AddInParameter(comm, "@ExamineStatus", DbType.Byte, (int)item.ExamineStatus);
            this._db.AddInParameter(comm, "@EnName", DbType.String, item.EnName);
            this._db.AddInParameter(comm, "@ExamineOperator", DbType.Int32,
                item.ExamineStatus == ExamineStatus.已审核 ? (int?)item.ExamineOperator : null);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改门票(价格一修改，则状态改为待审核)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isGn"></param>
        /// <returns></returns>
        public virtual bool Update(MScenicTickets item, bool isGn)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_ScenicTickets_Update");
            this._db.AddInParameter(comm, "@TicketsId", DbType.AnsiStringFixedLength, item.TicketsId);
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, item.ScenicId);
            this._db.AddInParameter(comm, "@TypeName", DbType.String, item.TypeName);
            this._db.AddInParameter(comm, "@RetailPrice", DbType.Currency, item.RetailPrice);
            this._db.AddInParameter(comm, "@WebsitePrices", DbType.Currency, item.WebsitePrices);
            this._db.AddInParameter(comm, "@MarketPrice", DbType.Currency, item.MarketPrice);
            this._db.AddInParameter(comm, "@DistributionPrice", DbType.Currency, item.DistributionPrice);
            this._db.AddInParameter(comm, "@Limit", DbType.Int32, item.Limit);
            this._db.AddInParameter(comm, "@Payment", DbType.Byte, (int)item.Payment);
            this._db.AddInParameter(comm, "@StartTime", DbType.DateTime, item.StartTime);
            this._db.AddInParameter(comm, "@EndTime", DbType.DateTime, item.EndTime);
            this._db.AddInParameter(comm, "@Description", DbType.String, item.Description);
            this._db.AddInParameter(comm, "@SaleDescription", DbType.String, item.SaleDescription);
            this._db.AddInParameter(comm, "@Status", DbType.Byte, (int)item.Status);
            this._db.AddInParameter(comm, "@CustomOrder", DbType.Int32, item.CustomOrder);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, item.B2COrder);
            this._db.AddInParameter(comm, "@EnName", DbType.String, item.EnName);
            this._db.AddInParameter(comm, "@ExamineStatus", DbType.Byte, (int)ExamineStatus.待审核);
            if (isGn)
            {
                this._db.AddInParameter(comm, "@ExamineStatus1", DbType.Byte, (int)item.ExamineStatus);
            }

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 删除门票(待审核状态才可以删除)
        /// </summary>
        /// <param name="ticketsId">门票编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual bool Delete(string ticketsId, string companyId)
        {
            string sql = "DELETE FROM tbl_ScenicTickets WHERE TicketsId = @Id AND CompanyId = @CompanyId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.AnsiStringFixedLength, ticketsId);
            this._db.AddInParameter(comm, "@CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改审核状态
        /// </summary>
        /// <param name="tickets">门票编号</param>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">审核状态</param>
        /// <returns></returns>
        public virtual bool UpdateExamineStatus(string tickets, int operatorId, ExamineStatus es)
        {
            string sql = "UPDATE tbl_ScenicTickets SET ExamineStatus = @ExamineStatus,ExamineOperator = @ExamineOperator WHERE TicketsId = @tickets";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ExamineStatus", DbType.Byte, (int)es);
            this._db.AddInParameter(comm, "@ExamineOperator", DbType.Int32, operatorId);
            this._db.AddInParameter(comm, "@tickets", DbType.AnsiStringFixedLength, tickets);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取门票实体
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ticketsId">门票编号</param>
        /// <returns>门票实体</returns>
        public virtual MScenicTickets GetModel(string ticketsId, string companyId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT [TicketsId],[ScenicId],[TypeName],[EnName],[RetailPrice]");
            sql.Append(",[WebsitePrices],[MarketPrice],[DistributionPrice]");
            sql.Append(",[Limit],[Payment],[StartTime],[EndTime],[Description],[SaleDescription],[Status]");
            sql.Append(",[ExamineStatus],[CustomOrder],[B2B],[B2BOrder],[B2C]");
            sql.Append(",[B2COrder],[IssueTime] ,[Operator],[CompanyId]");
            sql.Append(" FROM [dbo].[tbl_ScenicTickets] WHERE TicketsId = @id AND CompanyId = @cid");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, ticketsId);
            this._db.AddInParameter(comm, "@cid", DbType.AnsiStringFixedLength, companyId);

            MScenicTickets item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                if (reader.Read())
                {
                    item = new MScenicTickets()
                    {
                        TicketsId = reader["TicketsId"].ToString(),
                        ScenicId = reader["ScenicId"].ToString(),
                        TypeName = reader["TypeName"].ToString(),
                        EnName = reader.IsDBNull(reader.GetOrdinal("EnName")) ? string.Empty : reader["EnName"].ToString(),
                        RetailPrice = reader.IsDBNull(reader.GetOrdinal("RetailPrice")) ? 0 : (decimal)reader["RetailPrice"],
                        WebsitePrices = reader.IsDBNull(reader.GetOrdinal("WebsitePrices")) ? 0 : (decimal)reader["WebsitePrices"],
                        MarketPrice = reader.IsDBNull(reader.GetOrdinal("MarketPrice")) ? 0 : (decimal)reader["MarketPrice"],
                        DistributionPrice = reader.IsDBNull(reader.GetOrdinal("DistributionPrice")) ? 0 : (decimal)reader["DistributionPrice"],
                        Limit = (int)reader["Limit"],
                        Payment = (ScenicPayment)Enum.Parse(typeof(ScenicPayment), reader["Payment"].ToString()),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        SaleDescription = reader.IsDBNull(reader.GetOrdinal("SaleDescription")) ? string.Empty : reader["SaleDescription"].ToString(),
                        Status = (ScenicTicketsStatus)Enum.Parse(typeof(ScenicTicketsStatus), reader["Status"].ToString()),
                        ExamineStatus = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["ExamineStatus"].ToString()),
                        CustomOrder = (int)reader["CustomOrder"],
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2BOrder = (int)reader["B2BOrder"],
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        B2COrder = (int)reader["B2COrder"],
                        IssueTime = (DateTime)reader["IssueTime"],
                        Operator = reader["Operator"].ToString(),
                        CompanyId = reader["CompanyId"].ToString()
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("StartTime")))
                    {
                        item.StartTime = (DateTime)reader["StartTime"];
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EndTime")))
                    {
                        item.EndTime = (DateTime)reader["EndTime"];
                    }
                }
            }

            return item;
        }


        /// <summary>
        /// 获取特价门票
        /// </summary>
        /// <param name="topNum">指定获取数量</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MScenicTicketsSale> GetList(int topNum,MSearchScenicTicketsSale search)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top({0}) a.TicketsId,a.TypeName,a.ScenicId,a.RetailPrice,a.WebsitePrices,", topNum);
            sql.Append("c.ThumbAddress,c.Address,b.ScenicName,b.Id");
            sql.Append(" from tbl_ScenicTickets a");
            sql.AppendFormat(" left join tbl_ScenicArea b on a.ScenicId = b.ScenicId");
            sql.AppendFormat(" left join tbl_ScenicImg c on a.ScenicId = c.ScenicId and c.ImgType = {0}", (int)ScenicImgType.景区形象);
            sql.AppendFormat(" where a.Status = {0} and a.ExamineStatus = {1} and b.Status = {2}", (int)ScenicTicketsStatus.上架, (int)ExamineStatus.已审核, (int)ExamineStatus.已审核);
            
            if (search != null)
            {
                if (search.ProvinceId != null)
                {
                    sql.AppendFormat(" and b.ProvinceId = {0}", search.ProvinceId);
                }
                if (search.CityId != null)
                {
                    sql.AppendFormat(" and b.CityId = {0}", search.CityId);
                }
                if (search.CountyId != null)
                {
                    sql.AppendFormat(" and b.CountyId = {0}", search.CountyId);
                }
                if (search.B2B != null)
                {
                    sql.AppendFormat(" and a.B2B = {0}", (int)search.B2B);
                    sql.Append(" Order by a.B2BOrder,a.LastUpdateTime DESC");
                }
                else if (search.B2C != null)
                {
                    sql.AppendFormat(" and a.B2C = {0}", (int)search.B2C);
                    sql.Append(" Order by a.B2COrder,a.LastUpdateTime DESC");
                }
                else
                {
                    sql.Append(" Order by a.CustomOrder ASC,a.LastUpdateTime DESC");
                }
            }

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MScenicTicketsSale> list = new List<MScenicTicketsSale>();
            MScenicTicketsSale item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicTicketsSale()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        RetailPrice = reader.IsDBNull(reader.GetOrdinal("RetailPrice")) ? 0 : (decimal)reader["RetailPrice"],
                        WebsitePrices = reader.IsDBNull(reader.GetOrdinal("WebsitePrices")) ? 0 : (decimal)reader["WebsitePrices"],
                        ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? string.Empty : reader["Address"].ToString(),
                        TicketsId = reader.IsDBNull(reader.GetOrdinal("TicketsId")) ? string.Empty : reader["TicketsId"].ToString(),
                        TicketsName = reader.IsDBNull(reader.GetOrdinal("TypeName")) ? string.Empty : reader["TypeName"].ToString()
                    });
                }
            }

            return list;

        }
        /// <summary>
        /// 门票列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>门票列表</returns>
        public virtual IList<MScenicTickets> GetList(int pageSize, int pageIndex, ref int recordCount, MSearchScenicTickets search)
        {
            string tableName = "view_ScenicTickets_Select";
            string primaryKey = "TicketsId";
            string fields = "CompanyId,TicketsId,ScenicId,TypeName,RetailPrice,WebsitePrices,StartTime,EndTime,Payment,ExamineStatus,Status,B2B,B2C,ScenicName,IssueTime";
            string orderBy = "ExamineStatus ASC,LastUpdateTime Desc"; //按照待审核，已审核，最后修改时间排序
            #region 查询条件
            StringBuilder query = new StringBuilder();
            query.Append(" 1=1");
            if (search != null)
            {
                if (search.ExamineStatus != null)
                {
                    query.AppendFormat(" and ExamineStatus = {0}", (int)search.ExamineStatus);
                }
                if (search.Status != null)
                {
                    query.AppendFormat(" and Status = {0}", (int)search.Status);
                }
                if (!string.IsNullOrEmpty(search.ScenicName))
                {
                    query.AppendFormat(" and ScenicName like '%{0}%'", search.ScenicName);
                }
                if (!string.IsNullOrEmpty(search.TypeName))
                {
                    query.AppendFormat(" and TypeName like '%{0}%'", search.TypeName);
                }
                if (!string.IsNullOrEmpty(search.ScenicId))
                {
                    query.AppendFormat(" and ScenicId = '{0}'", search.ScenicId);
                }
                if (search.B2B != null)
                {
                    query.AppendFormat(" and B2B = {0}", (int)search.B2B);
                }
                if (search.B2C != null)
                {
                    query.AppendFormat(" and B2C = {0}", (int)search.B2C);
                }
                if (!string.IsNullOrEmpty(search.TicketsName))
                {
                    query.AppendFormat(" and TypeName = '{0}'", search.TicketsName);
                }
            }
            #endregion
            IList<MScenicTickets> list = new List<MScenicTickets>();
            MScenicTickets item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName,primaryKey,fields,query.ToString(),orderBy))
            {
                while (reader.Read())
                {
                    item = new MScenicTickets()
                    {
                        CompanyId = reader["CompanyId"].ToString(),
                        TicketsId = reader["TicketsId"].ToString(),
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        TypeName = reader["TypeName"].ToString(),
                        RetailPrice = reader.IsDBNull(reader.GetOrdinal("RetailPrice")) ? 0 : (decimal)reader["RetailPrice"],
                        WebsitePrices = reader.IsDBNull(reader.GetOrdinal("WebsitePrices")) ? 0 : (decimal)reader["WebsitePrices"],
                        Payment = (ScenicPayment)Enum.Parse(typeof(ScenicPayment), reader["Payment"].ToString()),
                        Status = (ScenicTicketsStatus)Enum.Parse(typeof(ScenicTicketsStatus), reader["Status"].ToString()),
                        ExamineStatus = (ExamineStatus)Enum.Parse(typeof(ExamineStatus), reader["ExamineStatus"].ToString()),
                        B2B = (ScenicB2BDisplay)Enum.Parse(typeof(ScenicB2BDisplay), reader["B2B"].ToString()),
                        B2C = (ScenicB2CDisplay)Enum.Parse(typeof(ScenicB2CDisplay), reader["B2C"].ToString()),
                        IssueTime = (DateTime)reader["IssueTime"],
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("StartTime")))
                    {
                        item.StartTime = (DateTime)reader["StartTime"];
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("EndTime")))
                    {
                        item.EndTime = (DateTime)reader["EndTime"];
                    }
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 到期时间还有1周
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetExpireTickets(string companyId)
        {
            string sql = string.Format("select count(*) from tbl_ScenicTickets where EndTime <= dateadd(day,7,getdate())  and ExamineStatus = {0} and CompanyId = '{1}'", (int)ExamineStatus.已审核, companyId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            return (int)DbHelper.GetSingle(comm, this._db);
        }

        #endregion
    }
}
