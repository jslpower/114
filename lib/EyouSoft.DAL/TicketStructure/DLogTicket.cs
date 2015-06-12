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
using System.Xml.Linq;
using EyouSoft.Common;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 机票接口访问日志数据访问类
    /// </summary>
    /// Author：汪奇志 2011-05-10
    public class DLogTicket:DALBase, EyouSoft.IDAL.TicketStructure.ILogTicket
    {
        #region static constants
        //static constants
        private const string SQL_INSERT_WLog = "INSERT INTO [tbl_LogTicket]([CompanyId],[UserId],[LCity],[RCity],[LDate],[RDate],[CDate]) VALUES (@CompanyId,@UserId,@LCity,@RCity,@LDate,@RDate,@CDate)";
        private const string SQL_SELECT_GetUserWeekLoginNumber = "SELECT COUNT(*) FROM [tbl_LogUserLogin] WHERE [OperatorId]=@UserId AND DATEDIFF(dd,[EventTime],GETDATE())<7";
        #endregion

        #region constructor
        /// <summary>
        /// database
        /// </summary>
        private Database _db = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public DLogTicket() { this._db = this.LogStore; }
        #endregion

        #region private members
        /// <summary>
        /// 获取用户一周内登录次数
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        private int GetUserWeekLoginNumber(string userId)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_SELECT_GetUserWeekLoginNumber);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }
        #endregion

        #region ILogTicket 成员
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log">日志信息业务实体</param>
        /// <returns></returns>
        public virtual bool WLog(EyouSoft.Model.TicketStructure.MLogTicketInfo log)
        {
            DbCommand cmd = this._db.GetSqlStringCommand(SQL_INSERT_WLog);
            this._db.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, log.CompanyId);
            this._db.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, log.UserId);
            this._db.AddInParameter(cmd, "LCity", DbType.AnsiStringFixedLength, log.LCity);
            this._db.AddInParameter(cmd, "RCity", DbType.AnsiStringFixedLength, log.RCity);
            this._db.AddInParameter(cmd, "LDate", DbType.AnsiStringFixedLength, log.LDate);
            this._db.AddInParameter(cmd, "RDate", DbType.AnsiStringFixedLength, log.RDate);
            this._db.AddInParameter(cmd, "CDate", DbType.AnsiStringFixedLength, DateTime.Now);

            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 获取日志信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.MLBLogInfo> GetLogs(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.TicketStructure.MLogTicketSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TicketStructure.MLBLogInfo> items = new List<EyouSoft.Model.TicketStructure.MLBLogInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_LogTicket";
            string primaryKey = "Id";
            string orderByString = "LatestDate DESC";
            StringBuilder fields = new StringBuilder();
            fields.Append("*");
            fields.Append(",(SELECT CompanyName,ContactName,ContactMobile,ContactTel,ContactQQ,LoginCount,LastLoginTime,IssueTime FROM tbl_CompanyInfo WHERE Id=view_LogTicket.CompanyId FOR XML RAW,ROOT('root')) AS Others");
            //fields.Append(",(SELECT COUNT(*) FROM [tbl_LogUserLogin] WHERE [OperatorId]=view_LogTicket.UserId AND DATEDIFF(dd,[EventTime],GETDATE())<7) AS WeekLoginNumber");

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TicketStructure.MLBLogInfo item = new EyouSoft.Model.TicketStructure.MLBLogInfo();

                    /*
                    item.CDate = rdr.GetDateTime(rdr.GetOrdinal("CDate"));
                    item.LCity = rdr["LCity"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("LDate")))
                    {
                        item.LDate = rdr.GetDateTime(rdr.GetOrdinal("LDate"));
                    }
                    item.RCity = rdr["RCity"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RDate")))
                    {
                        item.RDate = rdr.GetDateTime(rdr.GetOrdinal("RDate"));
                    }*/
                    item.CompanyId = rdr["CompanyId"].ToString();
                    item.UserId = rdr["UserId"].ToString();
                    item.LatestDate = rdr.GetDateTime(rdr.GetOrdinal("LatestDate"));
                    item.TotalTimes = rdr.GetInt32(rdr.GetOrdinal("TotalTimes"));
                    item.WeekTimes = rdr.GetInt32(rdr.GetOrdinal("WeekTimes"));

                    string xml = rdr["Others"].ToString();

                    if (!string.IsNullOrEmpty(xml))
                    {
                        XElement xRoot = XElement.Parse(xml);
                        var xRow = Utility.GetXElement(xRoot, "row");
                        item.CompanyName = Utility.GetXAttributeValue(xRow, "CompanyName");
                        item.ContactName = Utility.GetXAttributeValue(xRow, "ContactName");
                        item.ContactMobile = Utility.GetXAttributeValue(xRow, "ContactMobile");
                        item.ContactTelephone = Utility.GetXAttributeValue(xRow, "ContactTel");
                        item.ContactQQ = Utility.GetXAttributeValue(xRow, "ContactQQ");
                        item.LoginNumber = Utility.GetInt(Utility.GetXAttributeValue(xRow, "LoginCount"));
                        item.LastLoginTime = Utility.GetDateTime(Utility.GetXAttributeValue(xRow, "LastLoginTime"));
                        item.RegTime = Utility.GetDateTime(Utility.GetXAttributeValue(xRow, "IssueTime"));
                    }

                    //item.WeekLoginNumber = rdr.IsDBNull(rdr.GetOrdinal("WeekLoginNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("WeekLoginNumber"));

                    items.Add(item);
                }
            }

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    item.WeekLoginNumber = this.GetUserWeekLoginNumber(item.UserId);
                }
            }

            return items;
        }

        #endregion
    }
}
