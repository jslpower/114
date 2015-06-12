using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.TicketStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TicketStructure
{
    /// <summary>
    /// 机票折扣申请
    /// </summary>
    /// Author:张志瑜  2010-10-11
    public class TicketApply : DALBase, ITicketApply
    {
        #region SQL变量定义
        private const string SQL_Ticket_Apply_SELECT = "SELECT ID,TicketArticleID,TicketArticleTitle,VoyageType,PeopleNum,TakeOffDate,ReturnDate,CountryType,Remark,CompanyName,ContactName,CompanyTel,CompanyAddress,IssueTime,UpdateTime,State FROM Ticket_Apply WHERE ID=@ID";
        private const string SQL_Ticket_Apply_INSERT = "INSERT INTO Ticket_Apply (ID,TicketArticleID,TicketArticleTitle,VoyageType,PeopleNum,TakeOffDate,ReturnDate,CountryType,Remark,CompanyName,ContactName,CompanyTel,CompanyAddress,IssueTime,UpdateTime,State) VALUES(NEWID(),@TicketArticleID,@TicketArticleTitle,@VoyageType,@PeopleNum,@TakeOffDate,@ReturnDate,@CountryType,@Remark,@CompanyName,@ContactName,@CompanyTel,@CompanyAddress,GETDATE(), GETDATE(), 0)";
        private const string SQL_Ticket_Apply_UPDATE = "UPDATE Ticket_Apply SET TakeOffDate=@TakeOffDate,ReturnDate=@ReturnDate,Remark=@Remark,CompanyName=@CompanyName,ContactName=@ContactName,CompanyTel=@CompanyTel,CompanyAddress=@CompanyAddress,UpdateTime=GETDATE() WHERE ID=@ID";
        #endregion SQL变量定义

        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public TicketApply()
        {
            this._database = base.SystemStore;
        }

        #region 成员方法
        /// <summary>
        /// 获得机票折扣申请实体
        /// </summary>
        /// <param name="id">申请ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TicketStructure.TicketApply GetModel(string id)
        {
            EyouSoft.Model.TicketStructure.TicketApply model = new EyouSoft.Model.TicketStructure.TicketApply();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Ticket_Apply_SELECT);
            this._database.AddInParameter(dc, "ID", DbType.AnsiString, id);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model.ApplyId = dr.GetString(dr.GetOrdinal("ID"));
                    model.TicketArticleID = dr.IsDBNull(dr.GetOrdinal("TicketArticleID")) ? "" : dr.GetString(dr.GetOrdinal("TicketArticleID"));
                    model.TicketArticleTitle = dr.IsDBNull(dr.GetOrdinal("TicketArticleTitle")) ? "" : dr.GetString(dr.GetOrdinal("TicketArticleTitle"));
                    model.TicketFlight.VoyageSet = (EyouSoft.Model.TicketStructure.VoyageType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.VoyageType), dr.GetByte(dr.GetOrdinal("VoyageType")).ToString());
                    model.TicketFlight.PeopleNumber = dr.GetInt32(dr.GetOrdinal("PeopleNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TakeOffDate")))
                        model.TicketFlight.TakeOffDate = dr.GetDateTime(dr.GetOrdinal("TakeOffDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ReturnDate")))
                        model.TicketFlight.ReturnDate = dr.GetDateTime(dr.GetOrdinal("ReturnDate"));
                    model.TicketFlight.PeopleCountryType = (EyouSoft.Model.TicketStructure.PeopleCountryType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.PeopleCountryType), dr.GetByte(dr.GetOrdinal("CountryType")).ToString());
                    model.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? "" : dr.GetString(dr.GetOrdinal("CompanyName"));
                    model.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.CompanyTel = dr.IsDBNull(dr.GetOrdinal("CompanyTel")) ? "" : dr.GetString(dr.GetOrdinal("CompanyTel"));
                    model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("CompanyAddress")) ? "" : dr.GetString(dr.GetOrdinal("CompanyAddress"));          
                }
            }
            return model;
        }

        /// <summary>
        /// 添加机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        public virtual bool Add(EyouSoft.Model.TicketStructure.TicketApply model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Ticket_Apply_INSERT);

            this._database.AddInParameter(dc, "TicketArticleID", DbType.AnsiString, model.TicketArticleID);
            this._database.AddInParameter(dc, "TicketArticleTitle", DbType.String, model.TicketArticleTitle);
            this._database.AddInParameter(dc, "VoyageType", DbType.Byte, Convert.ToByte(model.TicketFlight.VoyageSet));
            this._database.AddInParameter(dc, "PeopleNum", DbType.Int32, model.TicketFlight.PeopleNumber);
            this._database.AddInParameter(dc, "TakeOffDate", DbType.DateTime, model.TicketFlight.TakeOffDate);
            this._database.AddInParameter(dc, "ReturnDate", DbType.DateTime, model.TicketFlight.ReturnDate);
            this._database.AddInParameter(dc, "CountryType", DbType.Byte, Convert.ToByte(model.TicketFlight.PeopleCountryType));
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "CompanyTel", DbType.String, model.CompanyTel);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除机票折扣申请
        /// </summary>
        /// <param name="idList">申请的ID串</param>
        /// <returns></returns>
        public virtual bool Delete(params string[] idList)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_Ticket_Apply_Delete");
            StringBuilder XMLRoot = new StringBuilder("<ROOT>");
            foreach (string id in idList)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    XMLRoot.AppendFormat("<Apply ApplyID='{0}' ", id);
                    XMLRoot.Append(" />");
                }
            }
            XMLRoot.Append("</ROOT>");

            this._database.AddInParameter(dc, "XMLRoot", DbType.String, XMLRoot.ToString());
            if (DbHelper.RunProcedureWithResult(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 修改机票折扣申请
        /// </summary>
        /// <param name="model">机票折扣信息实体</param>
        /// <returns></returns>
        public virtual bool Update(EyouSoft.Model.TicketStructure.TicketApply model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_Ticket_Apply_UPDATE);

            this._database.AddInParameter(dc, "ID", DbType.StringFixedLength, model.ApplyId);
            this._database.AddInParameter(dc, "TakeOffDate", DbType.DateTime, model.TicketFlight.TakeOffDate);
            this._database.AddInParameter(dc, "ReturnDate", DbType.DateTime, model.TicketFlight.ReturnDate);
            this._database.AddInParameter(dc, "Remark", DbType.String, model.Remark);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "CompanyTel", DbType.String, model.CompanyTel);
            this._database.AddInParameter(dc, "CompanyAddress", DbType.String, model.CompanyAddress);
            if (DbHelper.ExecuteSql(dc, this._database) > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获得申请的列表
        /// </summary>
        /// <param name="query">查询条件实体</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总的记录数</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TicketStructure.TicketApply> GetList(EyouSoft.Model.TicketStructure.QueryTicketApply query, int pageSize, int pageIndex, ref int recordCount)
        {
            IList<EyouSoft.Model.TicketStructure.TicketApply> list = new List<EyouSoft.Model.TicketStructure.TicketApply>();

            string tableName = "Ticket_Apply";
            string fields = "ID,TicketArticleID,TicketArticleTitle,VoyageType,PeopleNum,TakeOffDate,ReturnDate,CountryType,Remark,CompanyName,ContactName,CompanyTel,CompanyAddress,IssueTime,UpdateTime,State";
            string primaryKey = "ID";
            string orderByString = "IssueTime desc";
            StringBuilder strWhere = new StringBuilder("1=1");

            #region 查询拼接
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Title))
                    strWhere.AppendFormat(" AND TicketArticleTitle like '%{0}%' ", query.Title);

                if (!string.IsNullOrEmpty(query.ContactName))
                    strWhere.AppendFormat(" AND ContactName like '%{0}%' ", query.ContactName);

                if (query.TakeOffDateStart.HasValue)
                    strWhere.AppendFormat(" AND DATEDIFF(dd, TakeOffDate, '{0}')=0", query.TakeOffDateStart.Value.ToString("yyyy-MM-dd"));

                /*
                if (query.TakeOffDateStart.HasValue)
                    strWhere.AppendFormat(" AND TakeOffDate>='{0}'", query.TakeOffDateStart.Value.ToString("yyyy-MM-dd"));

                if (query.TakeOffDateEnd.HasValue)
                    strWhere.AppendFormat(" AND TakeOffDate<='{0}'", query.TakeOffDateEnd.Value.AddDays(1).AddMilliseconds(-1));
                 */ 

                if (query.VoyageType != EyouSoft.Model.TicketStructure.VoyageType.所有)
                    strWhere.AppendFormat(" AND VoyageType={0}", Convert.ToByte(query.VoyageType));
            }

            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.TicketStructure.TicketApply model = new EyouSoft.Model.TicketStructure.TicketApply();
                    model.ApplyId = dr.GetString(dr.GetOrdinal("ID"));
                    model.TicketArticleID = dr.IsDBNull(dr.GetOrdinal("TicketArticleID")) ? "" : dr.GetString(dr.GetOrdinal("TicketArticleID"));
                    model.TicketArticleTitle = dr.IsDBNull(dr.GetOrdinal("TicketArticleTitle")) ? "" : dr.GetString(dr.GetOrdinal("TicketArticleTitle"));
                    model.TicketFlight.VoyageSet = (EyouSoft.Model.TicketStructure.VoyageType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.VoyageType), dr.GetByte(dr.GetOrdinal("VoyageType")).ToString());
                    model.TicketFlight.PeopleNumber = dr.GetInt32(dr.GetOrdinal("PeopleNum"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TakeOffDate")))
                        model.TicketFlight.TakeOffDate = dr.GetDateTime(dr.GetOrdinal("TakeOffDate"));
                    if (!dr.IsDBNull(dr.GetOrdinal("ReturnDate")))
                        model.TicketFlight.ReturnDate = dr.GetDateTime(dr.GetOrdinal("ReturnDate"));
                    model.TicketFlight.PeopleCountryType = (EyouSoft.Model.TicketStructure.PeopleCountryType)Enum.Parse(typeof(EyouSoft.Model.TicketStructure.PeopleCountryType), dr.GetByte(dr.GetOrdinal("CountryType")).ToString());
                    //model.Remark = dr.IsDBNull(dr.GetOrdinal("Remark")) ? "" : dr.GetString(dr.GetOrdinal("Remark"));
                    model.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? "" : dr.GetString(dr.GetOrdinal("CompanyName"));
                    model.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) ? "" : dr.GetString(dr.GetOrdinal("ContactName"));
                    model.CompanyTel = dr.IsDBNull(dr.GetOrdinal("CompanyTel")) ? "" : dr.GetString(dr.GetOrdinal("CompanyTel"));
                    //model.CompanyAddress = dr.IsDBNull(dr.GetOrdinal("CompanyAddress")) ? "" : dr.GetString(dr.GetOrdinal("CompanyAddress"));          
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion 成员方法
    }
}
