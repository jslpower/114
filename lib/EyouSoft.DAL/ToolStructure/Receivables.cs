using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.ToolStructure
{
    /// <summary>
    /// 应收账款信息数据访问层
    /// </summary>
    /// 周文超  2010-11-9
    public class Receivables : DALBase, EyouSoft.IDAL.ToolStructure.IReceivables
    {
        private Database _db = null; 

        /// <summary>
        /// 构造函数
        /// </summary>
        public Receivables() 
        {
            this._db = SystemStore;
        }

        private const string Sql_Receivables_Insert = " INSERT INTO [tbl_Receivables] ([ID],[CompanyId],[OperatorId],[TourId],[TourNo],[RouteName],[LeaveDate],[OrderNo],[RetailersName],[PeopleNum],[OperatorName],[OperatorMQ],[SumPrice],[CheckendPrice],[NoCheckPrice],[ClearTime],[IssueTime]) VALUES (@ID,@CompanyId,@OperatorId,@TourId,@TourNo,@RouteName,@LeaveDate,@OrderNo,@RetailersName,@PeopleNum,@OperatorName,@OperatorMQ,@SumPrice,0,0,null,getdate()); ";
        private const string Sql_Receivables_Select = " SELECT [ID],[CompanyId],[OperatorId],[TourId],[TourNo],[RouteName],[LeaveDate],[OrderNo],[RetailersName],[PeopleNum],[OperatorName],[OperatorMQ],[SumPrice],[CheckendPrice],[NoCheckPrice],[ClearTime],[IssueTime] FROM [tbl_Receivables] ";

        #region IReceivables 成员

        /// <summary>
        /// 添加应收（已收）账款信息
        /// </summary>
        /// <param name="model">应收账款信息实体</param>
        /// <returns></returns>
        public virtual bool AddReceivables(EyouSoft.Model.ToolStructure.Receivables model)
        {
            if (model == null)
                return false;

            model.ID = Guid.NewGuid().ToString();

            DbCommand dc = this._db.GetSqlStringCommand(Sql_Receivables_Insert);

            #region 参数赋值

            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._db.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._db.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._db.AddInParameter(dc, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            this._db.AddInParameter(dc, "TourNo", DbType.String, model.TourNo);
            this._db.AddInParameter(dc, "RouteName", DbType.String, model.RouteName);
            this._db.AddInParameter(dc, "LeaveDate", DbType.DateTime, model.LeaveDate);
            this._db.AddInParameter(dc, "OrderNo", DbType.String, model.OrderNo);
            this._db.AddInParameter(dc, "RetailersName", DbType.String, model.RetailersName);
            this._db.AddInParameter(dc, "PeopleNum", DbType.Int32, model.PeopleNum);
            this._db.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._db.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            this._db.AddInParameter(dc, "SumPrice", DbType.Decimal, model.SumPrice);

            #endregion

            return DbHelper.ExecuteSql(dc, this._db) > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID获取对应的应收账款信息
        /// </summary>
        /// <param name="ReceivablesId">应收账款信息ID</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.ToolStructure.Receivables GetModel(string ReceivablesId)
        {
            if (string.IsNullOrEmpty(ReceivablesId))
                return null;

            EyouSoft.Model.ToolStructure.Receivables model = new EyouSoft.Model.ToolStructure.Receivables();
            DbCommand dc = this._db.GetSqlStringCommand(Sql_Receivables_Select + " where ID = @ID ");
            this._db.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ReceivablesId);

            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._db))
            {
                IList<EyouSoft.Model.ToolStructure.Receivables> list = GetSelectModel(dr);
                if (list == null || list.Count <= 0)
                    return model;

                model = list[0];
                list.Clear();
                list = null;
            }

            return model;
        }

        /// <summary>
        /// 获取应收（已收）账款信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="OrderIndex">排序索引</param>
        /// <param name="TourNo">団号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="OperatorName">下单人</param>
        /// <param name="RetailersName">组团社名称</param>
        /// <param name="StartLeaveDate">开始出团时间</param>
        /// <param name="EndLeaveDate">结束出团时间</param>
        /// <param name="IsCheck">是否只查询已收</param>
        /// <param name="CompanyId">所属供应商编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.ToolStructure.Receivables> GetList(int PageSize, int PageIndex, ref int RecordCount
            , int OrderIndex, string TourNo, string RouteName, string OperatorName, string RetailersName, DateTime? StartLeaveDate
            , DateTime? EndLeaveDate, bool IsCheck,string CompanyId)
        {
            IList<EyouSoft.Model.ToolStructure.Receivables> list = new List<EyouSoft.Model.ToolStructure.Receivables>();
            StringBuilder strWhere = new StringBuilder(" 1 = 1 ");
            string strFiles = " [ID],[CompanyId],[OperatorId],[TourId],[TourNo],[RouteName],[LeaveDate],[OrderNo],[RetailersName],[PeopleNum],[OperatorName],[OperatorMQ],[SumPrice],[CheckendPrice],[NoCheckPrice],[ClearTime] ";
            string strOrder = string.Empty;
            switch (OrderIndex)
            {
                case 0: strOrder = " IssueTime asc "; break;
                case 1: strOrder = " IssueTime desc "; break;
                case 2: strOrder = " SumPrice asc "; break;
                case 3: strOrder = " SumPrice desc "; break;
                case 4: strOrder = " CheckendPrice asc "; break;
                case 5: strOrder = " CheckendPrice desc "; break;
                case 6: strOrder = " NoCheckPrice asc "; break;
                case 7: strOrder = " NoCheckPrice desc "; break;
                case 8: strOrder = " ClearTime asc "; break;
                case 9: strOrder = " ClearTime desc "; break;
            }

            if (!string.IsNullOrEmpty(CompanyId))
                strWhere.AppendFormat(" and CompanyId = '{0}' ", CompanyId);
            if (!string.IsNullOrEmpty(TourNo))
                strWhere.AppendFormat(" and TourNo = '{0}' ", TourNo);
            if (!string.IsNullOrEmpty(RouteName))
                strWhere.AppendFormat(" and RouteName like '%{0}%' ", RouteName);
            if (!string.IsNullOrEmpty(OperatorName))
                strWhere.AppendFormat(" and OperatorName like '%{0}%' ", OperatorName);
            if (!string.IsNullOrEmpty(RetailersName))
                strWhere.AppendFormat(" and RetailersName like '%{0}%' ", RetailersName);
            if (StartLeaveDate.HasValue)
                strWhere.AppendFormat(" and datediff(dd,'{0}',LeaveDate) >= 0 ", StartLeaveDate.Value.ToShortDateString());
            if (EndLeaveDate.HasValue)
                strWhere.AppendFormat(" and datediff(dd,'{0}',LeaveDate) <= 0 ", EndLeaveDate.Value.ToShortDateString());
            if (IsCheck)
                strWhere.AppendFormat(" and SumPrice <= CheckendPrice ");
            else
                strWhere.AppendFormat(" and SumPrice <> CheckendPrice ");
            using (IDataReader dr = DbHelper.ExecuteReader(this._db, PageSize, PageIndex, ref RecordCount, "tbl_Receivables", "ID", strFiles, strWhere.ToString(), strOrder))
            {
                list = GetSelectModel(dr);
            }

            return list;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 应收信息实体赋值
        /// </summary>
        /// <param name="dr">IDataReader</param>
        /// <returns>返回应收信息实体集合</returns>
        private IList<EyouSoft.Model.ToolStructure.Receivables> GetSelectModel(IDataReader dr)
        {
            if (dr == null)
                return null;

            IList<EyouSoft.Model.ToolStructure.Receivables> list = new List<EyouSoft.Model.ToolStructure.Receivables>();
            EyouSoft.Model.ToolStructure.Receivables model = null;

            while (dr.Read())
            {
                model = new EyouSoft.Model.ToolStructure.Receivables();

                model.ID = dr["ID"].ToString();
                model.CompanyId = dr["CompanyId"].ToString();
                model.OperatorId = dr["OperatorId"].ToString();
                model.TourId = dr["TourId"].ToString();
                model.TourNo = dr["TourNo"].ToString();
                model.RouteName = dr["RouteName"].ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("LeaveDate")))
                    model.LeaveDate = DateTime.Parse(dr["LeaveDate"].ToString());
                model.OrderNo = dr["OrderNo"].ToString();
                model.RetailersName = dr["RetailersName"].ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("PeopleNum")))
                    model.PeopleNum = int.Parse(dr["PeopleNum"].ToString());
                model.OperatorName = dr["OperatorName"].ToString();
                model.OperatorMQ = dr["OperatorMQ"].ToString();
                if (!dr.IsDBNull(dr.GetOrdinal("SumPrice")))
                    model.SumPrice = decimal.Parse(dr["SumPrice"].ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("CheckendPrice")))
                    model.CheckendPrice = decimal.Parse(dr["CheckendPrice"].ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("NoCheckPrice")))
                    model.NoCheckPrice = decimal.Parse(dr["NoCheckPrice"].ToString());
                if (!dr.IsDBNull(dr.GetOrdinal("ClearTime")))
                    model.ClearTime = DateTime.Parse(dr["ClearTime"].ToString());

                list.Add(model);
            }

            return list;
        }

        #endregion
    }
}
