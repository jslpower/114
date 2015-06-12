using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 团队票数据操作
    /// </summary>
    /// Author:zhengfj 2011-5-18
    public class GroupTickets:DALBase,EyouSoft.IDAL.TicketStructure.IGroupTickets
    {
        #region 构造函数
        Database _database = null;
        public GroupTickets()
        {
            this._database = base.SystemStore;
        }

        #endregion

        #region sql
        const string SQL_GroupTickets_DeleteGroupTickets = "DELETE FROM tbl_GroupTickets WHERE CHARINDEX(','+LTRIM(ID)+',',','+@ids+',') > 0";
        const string SQL_GroupTickets_ModifyIsChecked = "UPDATE tbl_GroupTickets SET IsChecked = @IsChecked WHERE CHARINDEX(','+LTRIM(ID)+',',','+@ids+',') > 0";
        const string SQL_GroupTickets_GetGroupTicket = "SELECT ID,GroupType,StartCity,EndCity,PelopeCount,Phone,Airlines,FlightNumber,StartTime,TimeRange,RoundTripTime,Notes,Contact,ContactQQ,HopesPrice,Email,IsChecked,IssueTime FROM tbl_GroupTickets WHERE ID = @ID";
        const string SQL_PassengerInformation_Add = "INSERT INTO tbl_PassengerInformation(GroupTicketsID,UName,PassengerType,DocumentType,DocumentNo,Mobile) VALUES({0},'{1}',{2},{3},'{4}','{5}');";
        const string SQL_GroupTickets_AddGroupTickets = "INSERT INTO tbl_GroupTickets(GroupType,StartCity,EndCity,PelopeCount,Phone,Airlines,FlightNumber,StartTime,TimeRange,RoundTripTime,Notes,Contact,ContactQQ,HopesPrice,Email) VALUES(@GroupType,@StartCity,@EndCity,@PelopeCount,@Phone,@Airlines,@FlightNumber,@StartTime,@TimeRange,@RoundTripTime,@Notes,@Contact,@ContactQQ,@HopesPrice,@Email);SELECT @@IDENTITY";

        #endregion
        #region IGroupTickets 成员

        /// <summary>
        /// 申请团队票
        /// </summary>
        /// <param name="item">团队票实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool AddGroupTickets(EyouSoft.Model.TicketStructure.GroupTickets item)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_GroupTickets_AddGroupTickets);

            this._database.AddInParameter(comm, "@GroupType", DbType.Byte, (int)item.GroupType);
            this._database.AddInParameter(comm, "@StartCity", DbType.String, item.StartCity);
            this._database.AddInParameter(comm, "@EndCity", DbType.String, item.EndCity);
            this._database.AddInParameter(comm, "@PelopeCount", DbType.Int32, item.PelopeCount);
            this._database.AddInParameter(comm, "@Phone", DbType.String, item.Phone);
            this._database.AddInParameter(comm, "@Airlines", DbType.Byte, (int)item.AirlinesType);
            this._database.AddInParameter(comm, "@FlightNumber", DbType.String, item.FlightNumber);
            this._database.AddInParameter(comm, "@StartTime", DbType.String, item.StartTime);
            this._database.AddInParameter(comm, "@TimeRange", DbType.String, item.TimeRange);
            this._database.AddInParameter(comm, "@RoundTripTime", DbType.String, item.RoundTripTime);
            this._database.AddInParameter(comm, "@Notes", DbType.String, item.Notes);
            this._database.AddInParameter(comm, "@Contact", DbType.String, item.Contact);
            this._database.AddInParameter(comm, "@ContactQQ", DbType.String, item.ContactQQ);
            this._database.AddInParameter(comm, "@HopesPrice", DbType.Decimal, item.HopesPrice);
            this._database.AddInParameter(comm, "@Email", DbType.String, item.Email);

            int groupTicketsID = 0;  //记录添加成功后的团队票编号

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                if (reader.Read())
                {
                    groupTicketsID = int.Parse(reader[0].ToString());
                }
            }

            if (groupTicketsID > 0)  //添加成功
            {
                if (item.PassengerInformationList != null && item.PassengerInformationList.Count > 0)
                {
                    StringBuilder sql = new StringBuilder();
                    foreach (EyouSoft.Model.TicketStructure.PassengerInformation itemOne in item.PassengerInformationList)
                    {
                        sql.Append("INSERT INTO tbl_PassengerInformation(GroupTicketsID,UName,PassengerType,DocumentType,DocumentNo,Mobile)");
                        sql.Append("VALUES(");
                        sql.Append(groupTicketsID);
                        sql.Append(",'");
                        sql.Append(itemOne.UName);
                        sql.Append("',");
                        if (itemOne.PassengerType.HasValue)
                            sql.Append((int)itemOne.PassengerType);
                        else
                            sql.Append("nullif(1,1)");
                        sql.Append(",");
                        if (itemOne.DocumentType.HasValue)
                            sql.Append((int)itemOne.DocumentType);
                        else
                            sql.Append("nullif(1,1)");
                        sql.Append(",'");
                        sql.Append(itemOne.DocumentNo);
                        sql.Append("','");
                        sql.Append(itemOne.Mobile);
                        sql.Append("');");


                        //object[] parms = new object[]{
                        //     groupTicketsID,
                        //     itemOne.UName,
                        //     itemOne.PassengerType.HasValue?(int?)itemOne.PassengerType:null,
                        //     itemOne.DocumentType.HasValue?(int?)itemOne.DocumentType:null,
                        //     itemOne.DocumentNo,
                        //     itemOne.Mobile
                        //};
                        //sql.AppendFormat(SQL_PassengerInformation_Add, parms);
                    }

                    comm.CommandText = sql.ToString();

                    return DbHelper.ExecuteSqlTrans(comm, this._database) > 0 ? true : false;
                }
            }

            return true;
        }

        /// <summary>
        /// 单个/批量删除团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool DeleteGroupTickets(string ids)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_GroupTickets_DeleteGroupTickets);
            this._database.AddInParameter(comm, "@ids", DbType.String, ids);

            return DbHelper.ExecuteSqlTrans(comm, this._database) > 0 ? true : false;
        }

         /// <summary>
        /// 单个/批量审核团队票信息
        /// </summary>
        /// <param name="ids">团队票编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool ModifyIsChecked(string ids,bool isChecked)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_GroupTickets_ModifyIsChecked);
            this._database.AddInParameter(comm, "@IsChecked", DbType.String, isChecked ? "1" : "0");
            this._database.AddInParameter(comm, "@ids", DbType.String, ids);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 根据编号获取团队票信息
        /// </summary>
        /// <param name="id">团队票编号</param>
        /// <returns>团队票实体</returns>
        public virtual EyouSoft.Model.TicketStructure.GroupTickets GetGroupTicket(int id)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_GroupTickets_GetGroupTicket);
            this._database.AddInParameter(comm, "@ID", DbType.Int32, id);

            PassengerInformation dalPassengerInformation = new PassengerInformation();

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    return new EyouSoft.Model.TicketStructure.GroupTickets()
                    {
                        ID = (int)reader["ID"],
                        GroupType = (EyouSoft.Model.TicketStructure.GroupType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.GroupType), reader["GroupType"].ToString()),
                        StartCity = reader["StartCity"].ToString(),
                        EndCity = reader["EndCity"].ToString(),
                        PelopeCount = (int)reader["PelopeCount"],
                        Phone = reader["Phone"].ToString(),
                        AirlinesType = (EyouSoft.Model.TicketStructure.AirlinesType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.AirlinesType), reader["Airlines"].ToString()),
                        FlightNumber = reader.IsDBNull(reader.GetOrdinal("FlightNumber")) ? "" : reader["FlightNumber"].ToString(),
                        StartTime = reader["StartTime"].ToString(),
                        TimeRange = reader["TimeRange"].ToString(),
                        RoundTripTime = reader.IsDBNull(reader.GetOrdinal("RoundTripTime")) ? "" : reader["RoundTripTime"].ToString(),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader["Notes"].ToString(),
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? "" : reader["Contact"].ToString(),
                        ContactQQ = reader.IsDBNull(reader.GetOrdinal("ContactQQ")) ? "" : reader["ContactQQ"].ToString(),
                        HopesPrice = reader.IsDBNull(reader.GetOrdinal("HopesPrice")) ? 0 : decimal.Parse(reader["HopesPrice"].ToString()),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader["Email"].ToString(),
                        IsChecked = reader["IsChecked"].ToString().Equals("1") ? true : false,
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        //乘客信息
                        PassengerInformationList = dalPassengerInformation.GetPassengerInformation((int)reader["ID"])
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// 分页获取团队票信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>团队票集合</returns>
        public virtual IList<EyouSoft.Model.TicketStructure.GroupTickets> GetGroupTickets(int pageSize, int pageIndex, ref int recordCount)
        {
            string fileds = "ID,GroupType,StartCity,EndCity,PelopeCount,Phone,Airlines,FlightNumber,StartTime,TimeRange,RoundTripTime,Notes,Contact,ContactQQ,HopesPrice,Email,IsChecked,IssueTime";
            string tableName = "tbl_GroupTickets";
            string primaryKey = "ID";
            string orderBy = " IssueTime DESC";
            string query = string.Empty;

            PassengerInformation dalPassengerInformation = new PassengerInformation();
            IList<EyouSoft.Model.TicketStructure.GroupTickets> list = new List<EyouSoft.Model.TicketStructure.GroupTickets>();
            EyouSoft.Model.TicketStructure.GroupTickets item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,
                tableName,primaryKey,fileds,query,orderBy))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.TicketStructure.GroupTickets()
                    {
                        ID = (int)reader["ID"],
                        GroupType = (EyouSoft.Model.TicketStructure.GroupType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.GroupType), reader["GroupType"].ToString()),
                        StartCity = reader["StartCity"].ToString(),
                        EndCity = reader["EndCity"].ToString(),
                        PelopeCount = (int)reader["PelopeCount"],
                        Phone = reader["Phone"].ToString(),
                        AirlinesType = (EyouSoft.Model.TicketStructure.AirlinesType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.AirlinesType), reader["Airlines"].ToString()),
                        FlightNumber = reader.IsDBNull(reader.GetOrdinal("FlightNumber")) ? "" : reader["FlightNumber"].ToString(),
                        StartTime = reader["StartTime"].ToString(),
                        TimeRange = reader["TimeRange"].ToString(),
                        RoundTripTime = reader.IsDBNull(reader.GetOrdinal("RoundTripTime")) ? "" : reader["RoundTripTime"].ToString(),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader["Notes"].ToString(),
                        Contact = reader.IsDBNull(reader.GetOrdinal("Contact")) ? "" : reader["Contact"].ToString(),
                        ContactQQ = reader.IsDBNull(reader.GetOrdinal("ContactQQ")) ? "" : reader["ContactQQ"].ToString(),
                        HopesPrice = reader.IsDBNull(reader.GetOrdinal("HopesPrice")) ? 0 : decimal.Parse(reader["HopesPrice"].ToString()),
                        Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader["Email"].ToString(),
                        IsChecked = reader["IsChecked"].ToString().Equals("1") ? true : false,
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        //乘客信息
                        PassengerInformationList = dalPassengerInformation.GetPassengerInformation((int)reader["ID"])
                    };

                    list.Add(item);
                }
            }

            return list;
        }

        #endregion
    }
}
