using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;

namespace EyouSoft.DAL.NewTourStructure
{
    using System.Data;
    using System.Data.Common;
    using System.Data.OleDb;
    using System.Xml.Linq;

    using EyouSoft.Common;
    using EyouSoft.Common.DAL;
    using EyouSoft.HotelBI;
    using EyouSoft.Model.CompanyStructure;
    using EyouSoft.Model.TicketStructure;

    using Microsoft.Practices.EnterpriseLibrary.Data;

    /// <summary>
    /// 散拼订单
    /// </summary>
    public class DTourOrder : DALBase, EyouSoft.IDAL.NewTourStructure.ITourOrder
    {
        private Database _db = null;

        /// <summary>
        /// 构造
        /// </summary>
        public DTourOrder()
        {
            this._db = base.SystemStore;
        }

        #region ITourOrder 成员
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public string CreateOrderNo(string mark)
        {
            var orderNo = string.Empty;
            var cmd = this._db.GetSqlStringCommand("SELECT dbo.fn_NewTourOrder_CreateOrderNo(@MARK) AS OrderNo");
            this._db.AddInParameter(cmd, "@MARK", DbType.String, mark);
            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rd.Read())
                {
                    orderNo = rd.GetString(rd.GetOrdinal("OrderNo"));
                }
            }
            return orderNo;
        }

        /// <summary>
        /// 添加、修改散拼订单
        /// </summary>
        /// <param name="item">散拼订单实体</param>
        /// <param name="isAdd">true:添加 false:修改</param>
        /// <returns>True：成功 False：失败</returns>
        public bool AddOrUpdTourOrder(bool isAdd, MTourOrder item)
        {
            var cmd = this._db.GetStoredProcCommand("proc_NewTourOrder_Add");

            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, item.OrderId);
            this._db.AddInParameter(cmd, "@OrderNo", DbType.String, item.OrderNo);
            this._db.AddInParameter(cmd, "@TourId", DbType.AnsiStringFixedLength, item.TourId);
            this._db.AddInParameter(cmd, "@TourNo", DbType.String, item.TourNo);
            this._db.AddInParameter(cmd, "@RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(cmd, "@RouteName", DbType.String, item.RouteName);
            this._db.AddInParameter(cmd, "@Travel", DbType.AnsiStringFixedLength, item.Travel);
            this._db.AddInParameter(cmd, "@TravelContact", DbType.String, item.TravelContact);
            this._db.AddInParameter(cmd, "@TravelTel", DbType.AnsiString, item.TravelTel);
            this._db.AddInParameter(cmd, "@VisitorContact", DbType.String, item.VisitorContact);
            this._db.AddInParameter(cmd, "@VisitorTel", DbType.AnsiString, item.VisitorTel);
            this._db.AddInParameter(cmd, "@VisitorNotes", DbType.String, item.VisitorNotes);
            this._db.AddInParameter(cmd, "@ScheduleNum", DbType.Int32, item.ScheduleNum);
            this._db.AddInParameter(cmd, "@TravelNotes", DbType.String, item.TravelNotes);
            this._db.AddInParameter(cmd, "@BusinessNotes", DbType.String, item.BusinessNotes);
            this._db.AddInParameter(cmd, "@TotalPrice", DbType.Currency, item.TotalPrice);
            this._db.AddInParameter(cmd, "@IssueTime", DbType.DateTime, item.IssueTime);
            this._db.AddInParameter(cmd, "@OrderStatus", DbType.Byte, (int)item.OrderStatus);
            this._db.AddInParameter(cmd, "@AdultNum", DbType.Int32, item.AdultNum);
            this._db.AddInParameter(cmd, "@ChildrenNum", DbType.Int32, item.ChildrenNum);
            this._db.AddInParameter(cmd, "@SingleRoomNum", DbType.Int32, item.SingleRoomNum);
            this._db.AddInParameter(cmd, "@PersonalPrice", DbType.Currency, item.PersonalPrice);
            this._db.AddInParameter(cmd, "@ChildPrice", DbType.Currency, item.ChildPrice);
            this._db.AddInParameter(cmd, "@MarketPrice", DbType.Currency, item.MarketPrice);
            this._db.AddInParameter(cmd, "@SettlementAudltPrice", DbType.Currency, item.SettlementAudltPrice);
            this._db.AddInParameter(cmd, "@SettlementChildrenPrice", DbType.Currency, item.SettlementChildrenPrice);
            this._db.AddInParameter(cmd, "@Add", DbType.Currency, item.Add);
            this._db.AddInParameter(cmd, "@Reduction", DbType.Currency, item.Reduction);
            this._db.AddInParameter(cmd, "@SaveDate", DbType.DateTime, item.SaveDate.HasValue ? item.SaveDate.Value.ToString() : null);
            this._db.AddInParameter(cmd, "@PaymentStatus", DbType.Byte, (int)item.PaymentStatus);
            this._db.AddInParameter(cmd, "@TotalSalePrice", DbType.Currency, item.TotalSalePrice);
            this._db.AddInParameter(cmd, "@TotalSettlementPrice", DbType.Currency, item.TotalSettlementPrice);
            this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(cmd, "@SpecialDes", DbType.String, item.SpecialDes);
            this._db.AddInParameter(cmd, "@OperationMsg", DbType.String, item.OperationMsg);
            this._db.AddInParameter(cmd, "@XMLCustomers", DbType.Xml, CreateCustomersXml(item.Customers));
            this._db.AddInParameter(cmd, "@isADD", DbType.AnsiStringFixedLength, isAdd ? "1" : "0");

            return DbHelper.ExecuteSqlTrans(cmd, this._db) > 0;
        }

        /// <summary>
        /// 根据订单编号修改订单状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="saveDate">留位时间</param>
        /// <returns></returns>
        public bool SetOrderStatus(string orderId, PowderOrderStatus status, DateTime? saveDate)
        {

            var sql = new StringBuilder();

            //sql.Append(" BEGIN TRAN");
            sql.Append(" update tbl_NewTourOrder");
            sql.Append(" set");
            sql.Append("    OrderStatus=@OrderStatus");
            if (saveDate.HasValue)
            {
                sql.Append("    ,SaveDate=@SaveDate");
            }
            //sql.Append("    ,AdultNum=@AdultNum");
            //sql.Append("    ,ChildrenNum=@ChildrenNum");
            sql.Append(" where OrderId=@OrderId;");
            //散拼订单状态如果是取消，则余位人数+订单人数,判断团队人数
            if (status == PowderOrderStatus.取消)
            {
                sql.Append(" UPDATE tbl_NewPowderList");
                sql.Append(" SET MoreThan = case when a.TourNum > (a.MoreThan + b.AdultNum + b.ChildrenNum)");
                sql.Append(" then a.MoreThan + b.AdultNum + b.ChildrenNum else a.TourNum end");
                sql.Append(" from tbl_NewTourOrder b inner join tbl_NewPowderList a on a.TourId = b.TourId");
                sql.Append(" WHERE b.OrderId = @OrderId");
            }
            //散拼订单状态如果是专线商已确认，则余位人数-订单人数
            else if (status == PowderOrderStatus.专线商已确定)
            {
                sql.Append(" UPDATE tbl_NewPowderList");
                sql.Append(" SET MoreThan = case when a.MoreThan >= (b.AdultNum + b.ChildrenNum)");
                sql.Append(" then a.MoreThan - (b.AdultNum + b.ChildrenNum) else 0 end");
                sql.Append(" from tbl_NewTourOrder b inner join tbl_NewPowderList a on a.TourId = b.TourId");
                sql.Append(" WHERE  b.OrderId = @OrderId;");
            }

            //sql.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");
            //if (status==PowderOrderStatus.专线商预留)
            //{
            //    sql.Append(" UPDATE tbl_NewPowderList");
            //    sql.Append(" SET");
            //    sql.Append("    OrderPeopleNum=OrderPeopleNum+@AdultNum+@ChildrenNum");
            //    sql.Append("    ,SaveNum=SaveNum+@AdultNum+@ChildrenNum");
            //    //sql.Append("    ,MoreThan=MoreThan-(@AdultNum+@ChildrenNum)");
            //    sql.Append(" WHERE TourId=@TourId");
            //    sql.Append(" IF @@ERROR <> 0 BEGIN ROLLBACK TRAN;RETURN; END ");
            //}
            //sql.Append(" COMMIT TRAN");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, orderId);
            this._db.AddInParameter(cmd, "@OrderStatus", DbType.Byte, (int)status);
            if (saveDate.HasValue)
            {
                this._db.AddInParameter(cmd, "@SaveDate", DbType.DateTime, saveDate.Value);
            }
            //this._db.AddInParameter(cmd, "@AdultNum", DbType.Int32, adultNum);
            //this._db.AddInParameter(cmd, "@ChildrenNum", DbType.Int32, childrenNum);

            return (DbHelper.ExecuteSql(cmd, this._db) > 0);
        }
        /// <summary>
        /// 根据订单编号修改支付状态
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <param name="status">支付编号</param>
        /// <returns></returns>
        public bool SetPaymentStatus(string orderId, PaymentStatus status)
        {
            var sql = new StringBuilder();
            sql.Append(" update tbl_NewTourOrder set PaymentStatus=@PaymentStatus where OrderId=@OrderId");
            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, orderId);
            this._db.AddInParameter(cmd, "@PaymentStatus", DbType.Byte, status);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }
        /// <summary>
        /// 根据订单编号删除订单
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns></returns>
        public bool DelTourOrder(string orderId)
        {
            var sql = new StringBuilder();
            sql.Append(" Update tbl_NewPowderList set OrderPeopleNum = a.OrderPeopleNum - b.AdultNum - b.ChildrenNum from tbl_NewPowderList a inner join tbl_NewTourOrder b on a.TourId = b.TourId where b.OrderId = @OrderId;");
            sql.Append(" delete from tbl_NewTourOrderCustomer where OrderId=@OrderId;delete tbl_NewTourOrder where OrderId=@OrderId;");
            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, orderId);

            return DbHelper.ExecuteSql(cmd, this._db) > 0;
        }

        /// <summary>
        /// 添加订单日志
        /// </summary>
        /// <param name="m">订单日志实体</param>
        public void AddOrderHandleLog(MOrderHandleLog m)
        {
            if (m == null)
                return;

            var sql = new StringBuilder();

            sql.Append(" declare @OrderNo nvarchar(255); ");
            switch (m.OrderType)
            {
                case OrderSource.线路散拼订单:
                    sql.Append(" select @OrderNo = [OrderNo] from tbl_NewTourOrder where OrderId = @OrderId; ");
                    break;
                case OrderSource.线路团队订单:
                    sql.Append(" select @OrderNo = [OrderNo] from tbl_NewTourList where TourId = @OrderId; ");
                    break;
            }
            sql.Append(" if @OrderNo is null set @OrderNo = '' ;  ");
            sql.Append(" INSERT INTO [dbo].[tbl_OrderHandleLog]");
            sql.Append("        ([LogId]");
            sql.Append("        ,[OrderId]");
            sql.Append("        ,[OrderType]");
            sql.Append("        ,[OrderNo]");
            sql.Append("        ,[OperatorId]");
            sql.Append("        ,[OperatorName]");
            sql.Append("        ,[CompanyId]");
            sql.Append("        ,[Remark]");
            sql.Append("        ,[IssueTime])");
            sql.Append(" VALUES");
            sql.Append("        (@LogId");
            sql.Append("        ,@OrderId");
            sql.Append("        ,@OrderType");
            sql.Append("        ,@OrderNo");
            sql.Append("        ,@OperatorId");
            sql.Append("        ,@OperatorName");
            sql.Append("        ,@CompanyId");
            sql.Append("        ,@Remark");
            sql.Append("        ,@IssueTime)");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@LogId", DbType.AnsiStringFixedLength, m.LogId);
            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, m.OrderId);
            this._db.AddInParameter(cmd, "@OrderType", DbType.Byte, (int)m.OrderType);
            //this._db.AddInParameter(cmd, "@OrderNo", DbType.String, m.OrderNo);
            this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, m.OperatorId);
            this._db.AddInParameter(cmd, "@OperatorName", DbType.String, m.OperatorName);
            this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, m.CompanyId);
            this._db.AddInParameter(cmd, "@Remark", DbType.String, m.Remark);
            this._db.AddInParameter(cmd, "@IssueTime", DbType.Date, m.IssueTime);

            DbHelper.ExecuteSql(cmd, this._db);
        }

        /// <summary>
        /// 获取订单实体
        /// </summary>
        /// <param name="orderId">订单编号</param>
        /// <returns>订单实体</returns>
        public MTourOrder GetModel(string orderId)
        {
            var mdl = new MTourOrder();
            var sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append(" 	O.[OrderId]");
            sql.Append(" 	,O.[OrderNo]");
            sql.Append(" 	,O.[TourId]");
            sql.Append(" 	,O.[TourNo]");
            sql.Append(" 	,O.[RouteId]");
            sql.Append(" 	,O.[RouteName]");
            sql.Append(" 	,O.[Travel]");
            sql.Append(" 	,O.[TravelContact]");
            sql.Append(" 	,O.[TravelTel]");
            sql.Append(" 	,O.[VisitorContact]");
            sql.Append(" 	,O.[VisitorTel]");
            sql.Append(" 	,O.[VisitorNotes]");
            sql.Append(" 	,O.[ScheduleNum]");
            sql.Append(" 	,O.[TravelNotes]");
            sql.Append(" 	,O.[BusinessNotes]");
            sql.Append(" 	,O.[TotalPrice]");
            sql.Append(" 	,O.[IssueTime]");
            sql.Append(" 	,O.[OrderStatus]");
            sql.Append(" 	,O.[AdultNum]");
            sql.Append(" 	,O.[ChildrenNum]");
            sql.Append(" 	,O.[SingleRoomNum]");
            sql.Append(" 	,O.[PersonalPrice]");
            sql.Append(" 	,O.[ChildPrice]");
            sql.Append(" 	,O.[MarketPrice]");
            sql.Append(" 	,O.[SettlementAudltPrice]");
            sql.Append(" 	,O.[SettlementChildrenPrice]");
            sql.Append(" 	,O.[Add]");
            sql.Append(" 	,O.[Reduction]");
            sql.Append(" 	,O.[SaveDate]");
            sql.Append(" 	,O.[PaymentStatus]");
            sql.Append(" 	,O.[TotalSalePrice]");
            sql.Append(" 	,O.[TotalSettlementPrice]");
            sql.Append(" 	,O.[OperatorId]");
            sql.Append(" 	,O.[SpecialDes]");
            sql.Append(" 	,O.[OperationMsg]");
            sql.Append(" 	,T.StartCityName");
            sql.Append(" 	,T.LeaveDate");
            sql.Append(" 	,T.StartDate");
            sql.Append(" 	,T.EndDate");
            sql.Append(" 	,T.MoreThan");
            sql.Append(" 	,T.StartTraffic");
            sql.Append(" 	,T.EndTraffic");
            sql.Append(" 	,T.TeamLeaderDec");
            sql.Append(" 	,T.SetDec");
            sql.Append("    ,T.Publishers");
            sql.Append(" 	,C.ContactMQ");
            sql.Append(" 	,C.ContactQQ");
            sql.Append("    ,C.CompanyName");
            sql.Append("    ,T.TourNotes");
            sql.Append(" 	,(SELECT ");
            sql.Append(" 		[Id]");
            sql.Append(" 		,[OrderId]");
            sql.Append(" 		,[VisitorName]");
            sql.Append(" 		,[ContactTel]");
            sql.Append(" 		,[Mobile]");
            sql.Append(" 		,[IdentityCard]");
            sql.Append(" 		,[Passport]");
            sql.Append(" 		,[OtherCard]");
            sql.Append(" 		,[Sex]");
            sql.Append(" 		,[CradType]");
            sql.Append(" 		,[SiteNo]");
            sql.Append(" 		,[Notes]");
            sql.Append(" 		,[IssueTime]");
            sql.Append(" 	FROM [dbo].[tbl_NewTourOrderCustomer]");
            sql.Append(" 	WHERE tbl_NewTourOrderCustomer.OrderId=O.OrderId");
            sql.Append(" 	FOR XML RAW,ROOT) AS XMLCustmers");
            sql.Append(" FROM [dbo].[tbl_NewTourOrder] O");
            sql.Append(" LEFT OUTER JOIN tbl_NewPowderList T ON T.TourId=O.TourId");
            sql.Append(" LEFT OUTER JOIN tbl_CompanyInfo C ON C.Id=O.Travel");
            sql.Append(" WHERE");
            sql.Append(" 	O.OrderId=@OrderId");
            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, orderId);

            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rd.Read())
                {
                    mdl.OrderId = rd.GetString(rd.GetOrdinal("OrderId"));
                    mdl.OrderNo = rd.GetString(rd.GetOrdinal("OrderNo"));
                    mdl.TourId = rd.GetString(rd.GetOrdinal("TourId"));
                    mdl.TourNo = rd.GetString(rd.GetOrdinal("TourNo"));
                    mdl.RouteId = rd.GetString(rd.GetOrdinal("RouteId"));
                    mdl.RouteName = rd.GetString(rd.GetOrdinal("RouteName"));
                    mdl.Travel = rd.GetString(rd.GetOrdinal("Travel"));
                    mdl.TravelName = rd.IsDBNull(rd.GetOrdinal("CompanyName")) ? string.Empty : rd["CompanyName"].ToString();
                    mdl.TravelContact = rd["TravelContact"].ToString();
                    mdl.TravelTel = rd["TravelTel"].ToString();
                    mdl.VisitorContact = rd["VisitorContact"].ToString();
                    mdl.VisitorTel = rd["VisitorTel"].ToString();
                    mdl.TourNotes = rd["TourNotes"].ToString();
                    mdl.VisitorNotes = rd["VisitorNotes"].ToString();
                    mdl.ScheduleNum = rd.GetInt32(rd.GetOrdinal("ScheduleNum"));
                    mdl.TravelNotes = rd.IsDBNull(rd.GetOrdinal("TravelNotes")) ? string.Empty : rd["TravelNotes"].ToString();
                    mdl.Publishers = rd.IsDBNull(rd.GetOrdinal("Publishers")) ? string.Empty : rd["Publishers"].ToString();
                    mdl.BusinessNotes = rd.IsDBNull(rd.GetOrdinal("BusinessNotes")) ? string.Empty : rd["BusinessNotes"].ToString();
                    mdl.TotalPrice = rd.GetDecimal(rd.GetOrdinal("TotalPrice"));
                    mdl.IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime"));
                    mdl.OrderStatus = (PowderOrderStatus)rd.GetByte(rd.GetOrdinal("OrderStatus"));
                    mdl.AdultNum = rd.GetInt32(rd.GetOrdinal("AdultNum"));
                    mdl.ChildrenNum = rd.GetInt32(rd.GetOrdinal("ChildrenNum"));
                    mdl.SingleRoomNum = rd.GetInt32(rd.GetOrdinal("SingleRoomNum"));
                    mdl.PersonalPrice = rd.GetDecimal(rd.GetOrdinal("PersonalPrice"));
                    mdl.ChildPrice = rd.GetDecimal(rd.GetOrdinal("ChildPrice"));
                    mdl.MarketPrice = rd.GetDecimal(rd.GetOrdinal("MarketPrice"));
                    mdl.SettlementAudltPrice = rd.GetDecimal(rd.GetOrdinal("SettlementAudltPrice"));
                    mdl.SettlementChildrenPrice = rd.GetDecimal(rd.GetOrdinal("SettlementChildrenPrice"));
                    mdl.Add = rd.GetDecimal(rd.GetOrdinal("Add"));
                    mdl.Reduction = rd.IsDBNull(rd.GetOrdinal("Reduction")) ? 0 : rd.GetDecimal(rd.GetOrdinal("Reduction"));
                    if (!rd.IsDBNull(rd.GetOrdinal("SaveDate")))
                    {
                        mdl.SaveDate = rd.GetDateTime(rd.GetOrdinal("SaveDate"));
                    }
                    mdl.PaymentStatus = (PaymentStatus)rd.GetByte(rd.GetOrdinal("PaymentStatus"));
                    mdl.TotalSalePrice = rd.GetDecimal(rd.GetOrdinal("TotalSalePrice"));
                    mdl.TotalSettlementPrice = rd.GetDecimal(rd.GetOrdinal("TotalSettlementPrice"));
                    mdl.OperatorId = rd.GetString(rd.GetOrdinal("OperatorId"));
                    mdl.SpecialDes = rd.IsDBNull(rd.GetOrdinal("SpecialDes")) ? string.Empty : rd["SpecialDes"].ToString();
                    mdl.OperationMsg = rd.IsDBNull(rd.GetOrdinal("OperationMsg")) ? string.Empty : rd["OperationMsg"].ToString();
                    mdl.Customers = GetCustomerLst(rd["XMLCustmers"].ToString());
                    if (!rd.IsDBNull(rd.GetOrdinal("LeaveDate")))
                    {
                        mdl.LeaveDate = rd.GetDateTime(rd.GetOrdinal("LeaveDate"));
                    }
                    mdl.StartDate = rd.IsDBNull(rd.GetOrdinal("StartDate")) ? string.Empty : rd["StartDate"].ToString();
                    mdl.EndDate = rd.IsDBNull(rd.GetOrdinal("EndDate")) ? string.Empty : rd["EndDate"].ToString();
                    mdl.MoreThan = rd.GetInt32(rd.GetOrdinal("MoreThan"));
                    mdl.TeamLeaderDec = rd.IsDBNull(rd.GetOrdinal("TeamLeaderDec")) ? string.Empty : rd["TeamLeaderDec"].ToString();
                    mdl.SetDec = rd.IsDBNull(rd.GetOrdinal("SetDec")) ? string.Empty : rd["SetDec"].ToString();
                    mdl.StartCityName = rd.IsDBNull(rd.GetOrdinal("StartCityName")) ? string.Empty : rd["StartCityName"].ToString();
                    mdl.ContactMQ = rd.IsDBNull(rd.GetOrdinal("ContactMQ")) ? string.Empty : rd["ContactMQ"].ToString();
                    mdl.ContactQQ = rd.IsDBNull(rd.GetOrdinal("ContactQQ")) ? string.Empty : rd["ContactQQ"].ToString();
                }
            }
            return mdl;

        }

        /// <summary>
        /// 专线商-最新散客订单(有效期内的散拼订单)
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <param name="orderNo">订单号</param>
        /// <returns>订单集合</returns>
        public IList<MTourOrder> GetPublishersNewList(string tourId, string orderNo)
        {
            var lst = new List<MTourOrder>();
            var sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append(" 	O.OrderId");
            sql.Append(" 	,O.OrderNo");
            sql.Append(" 	,C.CompanyName");
            sql.Append(" 	,O.TravelContact");
            sql.Append(" 	,O.TravelTel");
            sql.Append(" 	,O.IssueTime");
            sql.Append(" 	,O.AdultNum");
            sql.Append(" 	,O.ChildrenNum");
            sql.Append(" 	,O.OrderStatus");
            sql.Append(" 	,O.PaymentStatus");
            sql.Append("    ,O.SaveDate");
            sql.Append(" FROM tbl_NewTourOrder O");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo C ");
            sql.Append(" ON");
            sql.Append(" 	C.Id=O.Travel");
            sql.Append(" WHERE");
            if (!string.IsNullOrEmpty(orderNo))
            {
                sql.AppendFormat(" 	O.OrderNo + O.RouteName LIKE '%{0}%' AND", orderNo);
            }

            sql.AppendFormat(" 	O.TourId='{0}'", tourId);
            sql.Append(" ORDER BY O.IssueTime desc");

            var cmd = this._db.GetSqlStringCommand(sql.ToString());

            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrder()
                    {
                        OrderId = rd["OrderId"].ToString(),
                        OrderNo = rd["OrderNo"].ToString(),
                        TravelName = rd["CompanyName"].ToString(),
                        TravelContact = rd["TravelContact"].ToString(),
                        TravelTel = rd["TravelTel"].ToString(),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        AdultNum = (int)rd["AdultNum"],
                        ChildrenNum = (int)rd["ChildrenNum"],
                        OrderStatus = (PowderOrderStatus)rd.GetByte(rd.GetOrdinal("OrderStatus")),
                        SaveDate = rd.IsDBNull(rd.GetOrdinal("SaveDate")) ? null : (DateTime?)Utility.GetDateTime(rd["SaveDate"].ToString()),
                        PaymentStatus = (PaymentStatus)rd.GetByte(rd.GetOrdinal("PaymentStatus"))
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 专线商-所有散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">专线商</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        public IList<MTourOrder> GetPublishersAllList(int pageSize, int pageCurrent, ref int recordCount, string companyId, MTourOrderSearch search)
        {
            var lst = new List<MTourOrder>();
            var sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append(" 	O.OrderId");
            sql.Append(" 	,O.OrderNo");
            sql.Append("    ,O.TourId");
            sql.Append("    ,O.TourNo");
            sql.Append(" 	,C.CompanyName");
            sql.Append(" 	,O.TravelContact");
            sql.Append(" 	,O.TravelTel");
            sql.Append(" 	,O.IssueTime");
            sql.Append(" 	,O.AdultNum");
            sql.Append(" 	,O.ChildrenNum");
            sql.Append(" 	,O.OrderStatus");
            sql.Append(" 	,O.PaymentStatus");
            sql.Append(" 	,O.RouteName");
            sql.Append(" 	,T.LeaveDate");
            sql.Append(" 	,O.TotalSettlementPrice");
            sql.Append(" FROM tbl_NewTourOrder O");
            sql.Append(" INNER JOIN");
            sql.Append(" 	tbl_NewPowderList T ");
            sql.Append(" ON");
            sql.AppendFormat(" 	T.TourId=O.TourId AND T.IsDeleted='0' AND T.Publishers='{0}'", companyId);
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo C ");
            sql.Append(" ON");
            sql.Append(" 	C.Id=O.Travel");
            sql.Append(" WHERE");
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.OrderKey))
                {
                    sql.AppendFormat(" 	(O.OrderNo + O.RouteName LIKE '%{0}%' OR C.CompanyName LIKE '%{0}%') AND", search.OrderKey);
                }
                if (!string.IsNullOrEmpty(search.LeaveDateS))
                {
                    sql.AppendFormat(" 	T.LeaveDate >= '{0}' AND", search.LeaveDateS);
                }
                if (!string.IsNullOrEmpty(search.LeaveDateE))
                {
                    sql.AppendFormat(" 	T.LeaveDate <= '{0}' AND", search.LeaveDateE);
                }
                if (search.PowderOrderStatus != null && search.PowderOrderStatus.Count > 0)
                {
                    sql.AppendFormat(" 	O.OrderStatus IN ({0}) AND", GetOrderStatusForIn(search.PowderOrderStatus));
                }
                if (search.PaymentStatus != null && search.PaymentStatus.Count > 0)
                {
                    sql.AppendFormat(" 	 O.PaymentStatus in ({0}) AND", GetPaymentStatusForIn(search.PaymentStatus));
                }
                if (search.AreaId > 0)
                {
                    sql.AppendFormat(" 	T.AreaId={0} AND ", search.AreaId);
                }
                if (!string.IsNullOrEmpty(search.TourId))
                {
                    sql.AppendFormat(" O.TourId = '{0}' and ", search.TourId);
                }
            }
            sql.Append(" 1=1 ");

            using (var rd = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, sql.ToString(), "*", "", GetOrderBy(search.Order, search.IsDesc), false, ""))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrder()
                    {
                        OrderId = rd["OrderId"].ToString(),
                        OrderNo = rd["OrderNo"].ToString(),
                        TourId = rd["TourId"].ToString(),
                        TourNo = rd["TourNo"].ToString(),
                        TravelName = rd["CompanyName"].ToString(),
                        TravelContact = rd["TravelContact"].ToString(),
                        TravelTel = rd["TravelTel"].ToString(),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        AdultNum = (int)rd["AdultNum"],
                        ChildrenNum = (int)rd["ChildrenNum"],
                        OrderStatus = (PowderOrderStatus)rd.GetByte(rd.GetOrdinal("OrderStatus")),
                        PaymentStatus = (PaymentStatus)rd.GetByte(rd.GetOrdinal("PaymentStatus")),
                        RouteName = rd["RouteName"].ToString(),
                        LeaveDate = rd.GetDateTime(rd.GetOrdinal("LeaveDate")),
                        TotalSettlementPrice = rd.GetDecimal(rd.GetOrdinal("TotalSettlementPrice"))
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 组团社-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">组团社</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单集合</returns>
        public IList<MTourOrder> GetTravelList(int pageSize, int pageCurrent, ref int recordCount, string companyId, MTourOrderSearch search)
        {
            var lst = new List<MTourOrder>();
            var sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append(" O.OrderId");
            sql.Append(" ,O.IssueTime");
            sql.Append(" ,O.AdultNum");
            sql.Append(" ,O.ChildrenNum");
            sql.Append(" ,O.OrderStatus");
            sql.Append(" ,O.PaymentStatus");
            sql.Append(" ,O.RouteName");
            sql.Append(" ,T.LeaveDate");
            sql.Append(" ,O.VisitorContact");
            sql.Append(" ,O.VisitorTel");
            sql.Append(" ,T.TourNo");
            sql.Append(" ,T.TourId");
            sql.Append(" ,C.CompanyType");
            sql.Append(" ,T.Publishers");
            sql.Append(" ,T.RouteId");
            sql.Append(" ,M.CompanyName");
            sql.Append(" FROM tbl_NewTourOrder O");
            sql.Append(" INNER JOIN");
            sql.Append(" 	tbl_NewPowderList T ");
            sql.Append(" ON");
            sql.Append(" 	T.TourId=O.TourId AND T.IsDeleted='0' ");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo C ");
            sql.Append(" ON");
            sql.Append(" 	C.Id=O.Travel");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo M ");
            sql.Append(" ON");
            sql.Append(" 	M.Id=T.Publishers");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_NewRouteBasicInfo R");
            sql.Append(" ON");
            sql.Append(" 	R.RouteId = T.RouteId");
            sql.Append(" WHERE");
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.OrderKey))
                {
                    sql.AppendFormat(" 	(O.RouteName + O.VisitorContact LIKE '%{0}%' OR M.CompanyName LIKE '%{0}%' OR T.TourNo LIKE '%{0}%') AND", search.OrderKey);
                }
                if (!string.IsNullOrEmpty(search.LeaveDateS))
                {
                    sql.AppendFormat(" 	T.LeaveDate >= '{0}' AND", search.LeaveDateS);
                }
                if (!string.IsNullOrEmpty(search.LeaveDateE))
                {
                    sql.AppendFormat(" 	T.LeaveDate <= '{0}' AND", search.LeaveDateE);
                }
                if (search.PowderOrderStatus != null && search.PowderOrderStatus.Count > 0)
                {
                    sql.AppendFormat(" 	O.OrderStatus in ({0}) AND", GetOrderStatusForIn(search.PowderOrderStatus));
                }
                if (search.AreaType.HasValue)
                {
                    sql.AppendFormat(" 	R.RouteType={0} AND", (int)search.AreaType.Value);
                }
                if (!string.IsNullOrEmpty(search.RouteId))
                {
                    sql.AppendFormat(" O.RouteId = '{0}' AND", search.RouteId);
                }
            }
            sql.AppendFormat(" 	 O.Travel='{0}'", companyId);
            using (var rd = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, sql.ToString(), "*", "", "LeaveDate asc,IssueTime desc", false, ""))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrder()
                    {
                        OrderId = rd["OrderId"].ToString(),
                        TourId = rd["TourId"].ToString(),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        RouteId = rd.GetString(rd.GetOrdinal("RouteId")),
                        Publishers = rd.GetString(rd.GetOrdinal("Publishers")),
                        PublishersName = rd.GetString(rd.GetOrdinal("CompanyName")),
                        CompanyType = (CompanyType)Enum.Parse(typeof(CompanyType), rd["CompanyType"].ToString()),
                        AdultNum = (int)rd["AdultNum"],
                        ChildrenNum = (int)rd["ChildrenNum"],
                        OrderStatus = (PowderOrderStatus)rd.GetByte(rd.GetOrdinal("OrderStatus")),
                        PaymentStatus = (PaymentStatus)rd.GetByte(rd.GetOrdinal("PaymentStatus")),
                        RouteName = rd["RouteName"].ToString(),
                        LeaveDate = rd.GetDateTime(rd.GetOrdinal("LeaveDate")),
                        VisitorContact = rd["VisitorContact"].ToString(),
                        VisitorTel = rd["VisitorTel"].ToString(),
                        TourNo = rd["TourNo"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 运营后台-散客订单
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散客订单集合</returns>
        public IList<MTourOrder> GetList(int pageSize, int pageCurrent, ref int recordCount, MTourOrderSearch search)
        {
            var lst = new List<MTourOrder>();
            var sql = new StringBuilder();

            sql.Append(" SELECT");
            sql.Append("     O.RouteId");
            sql.Append(" 	,O.OrderId");
            sql.Append(" 	,O.OrderNo");
            sql.Append(" 	,C.CompanyName");
            sql.Append(" 	,O.IssueTime");
            sql.Append(" 	,O.AdultNum");
            sql.Append(" 	,O.ChildrenNum");
            sql.Append(" 	,O.OrderStatus");
            sql.Append(" 	,O.PaymentStatus");
            sql.Append(" 	,O.RouteName");
            sql.Append(" 	,T.LeaveDate");
            sql.Append(" 	,M.CompanyName Publisher");
            sql.Append(" 	,O.VisitorContact");
            sql.Append(" 	,O.VisitorTel");
            sql.Append(" FROM tbl_NewTourOrder O");
            sql.Append(" INNER JOIN");
            sql.Append(" 	tbl_NewPowderList T ");
            sql.Append(" ON");
            sql.Append(" 	T.TourId=O.TourId AND T.IsDeleted='0'");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo C ");
            sql.Append(" ON");
            sql.Append(" 	C.Id=O.Travel");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_CompanyInfo M ");
            sql.Append(" ON");
            sql.Append(" 	M.Id=T.Publishers");
            sql.Append(" LEFT OUTER JOIN");
            sql.Append(" 	tbl_NewRouteBasicInfo R");
            sql.Append(" ON");
            sql.Append(" 	R.RouteId = T.RouteId");
            sql.Append(" WHERE");
            if (!string.IsNullOrEmpty(search.OrderKey))
            {
                sql.AppendFormat(" 	(O.OrderNo + O.RouteName + O.VisitorContact LIKE '%{0}%' OR C.CompanyName LIKE '%{0}%' OR M.CompanyName LIKE '%{0}%') AND", search.OrderKey);
            }
            if (!string.IsNullOrEmpty(search.LeaveDateS))
            {
                sql.AppendFormat(" 	T.LeaveDate >= '{0}' AND", search.LeaveDateS);
            }
            if (!string.IsNullOrEmpty(search.LeaveDateE))
            {
                sql.AppendFormat(" 	T.LeaveDate <= '{0}' AND", search.LeaveDateE);
            }
            if (search.AreaType.HasValue)
            {
                sql.AppendFormat(" 	R.RouteType={0} AND", (int)search.AreaType.Value);
            }
            if (search.AreaId > 0)
            {
                sql.AppendFormat(" 	 R.AreaId={0} AND", search.AreaId);
            }
            if (!string.IsNullOrEmpty(search.Publishers))
            {
                sql.AppendFormat(" 	 R.Publishers='{0}' AND", search.Publishers);
            }
            if (search.PowderOrderStatus != null && search.PowderOrderStatus.Count > 0)
            {
                sql.AppendFormat(" 	 O.OrderStatus in ({0}) AND", GetOrderStatusForIn(search.PowderOrderStatus));
            }
            if (search.PaymentStatus != null && search.PaymentStatus.Count > 0)
            {
                sql.AppendFormat(" 	 O.PaymentStatus in ({0}) AND", GetPaymentStatusForIn(search.PaymentStatus));
            }
            if (!string.IsNullOrEmpty(search.TourId))
            {
                sql.AppendFormat(" O.TourId = '{0}' AND ", search.TourId);
            }
            sql.Append(" 	 1=1");
            using (var rd = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, sql.ToString(), "*", "", GetOrderBy(search.Order, search.IsDesc), false, ""))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrder()
                    {
                        RouteId = rd["RouteId"].ToString(),
                        OrderId = rd["OrderId"].ToString(),
                        OrderNo = rd["OrderNo"].ToString(),
                        TravelName = rd["CompanyName"].ToString(),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        AdultNum = (int)rd["AdultNum"],
                        ChildrenNum = (int)rd["ChildrenNum"],
                        OrderStatus = (PowderOrderStatus)rd.GetByte(rd.GetOrdinal("OrderStatus")),
                        PaymentStatus = (PaymentStatus)rd.GetByte(rd.GetOrdinal("PaymentStatus")),
                        RouteName = rd["RouteName"].ToString(),
                        LeaveDate = rd.GetDateTime(rd.GetOrdinal("LeaveDate")),
                        Publishers = rd["Publisher"].ToString(),
                        VisitorContact = rd["VisitorContact"].ToString(),
                        VisitorTel = rd["VisitorTel"].ToString()
                    });
                }
            }
            return lst;
        }


        /// <summary>
        /// 根据搜索实体获取订单统计列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单统计实体列表</returns>
        public IList<MOrderStatic> GetOrderStaticLst(int pageSize, int pageCurrent, ref int recordCount, MOrderStaticSearch search)
        {
            var sql = new StringBuilder();
            var strSql = new StringBuilder();
            var lst = new List<MOrderStatic>();

            sql.Append(" SELECT");
            if (search.IsDetail)
            {
                sql.Append(" 	T.RouteId");
                sql.Append(" 	,T.RouteName");
                sql.Append(search.CompanyTyp == CompanyType.组团 ? " 	,T.CompanyName" : "");
            }
            else
            {
                sql.Append(" 	A.ID");
                sql.Append(" 	,A.AreaName");
                sql.Append(" 	,MIN(T.LeaveDate) LeaveDateMin");
                sql.Append(" 	,MAX(T.LeaveDate) LeaveDateMax");
            }
            sql.Append(" 	,COUNT(T.OrderId) TotalOrder");
            sql.Append(" 	,ISNULL(SUM(T.AdultNum),0) TotalAdult");
            sql.Append(" 	,ISNULL(SUM(T.ChildrenNum),0) TotalChild");
            sql.Append(" 	,ISNULL(SUM(T.TotalSalePrice),0) TotalSale");
            sql.Append(" 	,ISNULL(SUM(T.TotalSettlementPrice),0) TotalSettle");
            sql.Append(" FROM");
            sql.Append(" 	tbl_SysArea A");
            sql.Append(" INNER JOIN");
            sql.Append(" 	(SELECT");
            sql.Append(" 		R.AreaId");
            sql.Append(" 		,P.RouteId");
            sql.Append(" 		,P.RouteName");
            sql.Append(" 		,P.LeaveDate");
            sql.Append(" 		,M.CompanyName");
            sql.Append(" 		,O.OrderId");
            sql.Append(" 		,O.AdultNum");
            sql.Append(" 		,O.ChildrenNum");
            sql.Append(" 		,O.TotalSalePrice");
            sql.Append(" 		,O.TotalSettlementPrice");
            sql.Append(" 		,R.Publishers");
            sql.Append(" 		,O.Travel");
            sql.Append(" 	FROM");
            sql.Append(" 		tbl_NewTourOrder O");
            sql.Append(" 	INNER JOIN");
            sql.Append(" 		tbl_NewPowderList P");
            sql.Append(" 	ON");
            sql.Append(" 		O.TourId=P.TourId");
            sql.Append(" 		AND O.RouteId=P.RouteId");
            sql.Append(" 	INNER JOIN");
            sql.Append(" 		tbl_NewRouteBasicInfo R");
            sql.Append(" 	ON");
            sql.Append(" 		O.RouteId=R.RouteId");
            sql.Append("    INNER JOIN");
            sql.Append(" 	    tbl_CompanyInfo M ");
            sql.Append("    ON");
            sql.Append(" 	    M.Id=P.Publishers");
            sql.Append(" 	WHERE");

            if (search.LeaveDateS.HasValue)
            {
                sql.AppendFormat(" 		P.LeaveDate >= '{0}' AND", search.LeaveDateS.Value);
            }
            if (search.LeaveDateE.HasValue)
            {
                sql.AppendFormat(" 		P.LeaveDate <= '{0}' AND", search.LeaveDateE.Value);
            }
            if (search.AreaType.HasValue)
            {
                sql.AppendFormat(" 		R.RouteType={0} AND", (int)search.AreaType.Value);
            }
            if (search.AreaId > 0)
            {
                sql.AppendFormat(" 		R.AreaId={0} AND", search.AreaId);
            }
            sql.AppendFormat("    R.IsDeleted='0'and o.orderstatus<>{0} and o.orderstatus<>{1}", (int)PowderOrderStatus.取消, (int)PowderOrderStatus.预留过期);
            sql.Append(" 	) T");
            sql.Append(" ON");
            sql.Append(" 	T.AreaId=A.ID");
            sql.Append(" WHERE");
            if (search.CompanyTyp == CompanyType.专线)
            {
                sql.AppendFormat(" 	T.Publishers='{0}' AND", search.CompanyId);
            }
            if (search.CompanyTyp == CompanyType.组团)
            {
                sql.AppendFormat(" 	T.Travel='{0}' AND", search.CompanyId);
            }
            if (search.IsDetail && search.AreaId > 0)
            {
                sql.AppendFormat(" 	A.ID={0} AND", search.AreaId);
            }
            sql.Append(" 	1=1");
            sql.Append(" GROUP BY");
            sql.Append(search.IsDetail ? " 	T.RouteId,T.RouteName" + (search.CompanyTyp == CompanyType.组团 ? ",T.CompanyName" : "") : " 	A.ID,A.AreaName");

            var orderStatus = new PowderOrderStatus?[]
                                  {
                                      PowderOrderStatus.专线商已确定,
                                      PowderOrderStatus.结单
                                  };
            int totalAdult;
            int totalChild;
            decimal totalSale;
            decimal totalSettle;
            using (var rd = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, sql.ToString(), "*", "", "TotalOrder DESC", false, ""))
            {
                while (rd.Read())
                {
                    totalAdult = 0;
                    totalChild = 0;
                    totalSale = 0M;
                    totalSettle = 0M;
                    var mdl = new MOrderStatic();
                    if (search.IsDetail)
                    {
                        mdl.RouteId = rd["RouteId"].ToString();
                        mdl.RouteName = rd["RouteName"].ToString();
                        if (search.CompanyTyp == CompanyType.组团)
                        {
                            mdl.Publisher = rd["CompanyName"].ToString();
                        }
                        GetCheckOrderPeopleNum(0, mdl.RouteId, orderStatus, search, ref totalAdult,
                                               ref totalChild, ref totalSale, ref totalSettle);
                    }
                    else
                    {
                        if (!rd.IsDBNull(rd.GetOrdinal("LeaveDateMin")))
                        {
                            mdl.LeaveDateMin = rd.GetDateTime(rd.GetOrdinal("LeaveDateMin"));
                        }
                        if (!rd.IsDBNull(rd.GetOrdinal("LeaveDateMax")))
                        {
                            mdl.LeaveDateMax = rd.GetDateTime(rd.GetOrdinal("LeaveDateMax"));
                        }
                        mdl.AreaId = rd.GetInt32(rd.GetOrdinal("ID"));
                        mdl.AreaName = rd["AreaName"].ToString();

                        GetCheckOrderPeopleNum(mdl.AreaId, string.Empty, orderStatus, search, ref totalAdult,
                                               ref totalChild, ref totalSale, ref totalSettle);
                    }
                    mdl.TotalOrder = rd.GetInt32(rd.GetOrdinal("TotalOrder"));
                    mdl.TotalAdult = totalAdult;
                    mdl.TotalChild = totalChild;
                    mdl.TotalSale = totalSale;
                    mdl.TotalSettle = totalSettle;
                    lst.Add(mdl);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <returns>订单游客列表</returns>
        public IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp)
        {
            var lst = new List<MTourOrderCustomer>();
            var sql = new StringBuilder();

            sql.Append(" select distinct");
            sql.Append("    c.Id");
            sql.Append("    ,c.VisitorName");
            sql.Append("    ,c.Sex");
            sql.Append("    ,c.CradType");
            sql.Append("    ,c.ContactTel");
            sql.Append("    ,c.IdentityCard");
            sql.Append("    ,c.SiteNo");
            sql.Append("    ,c.Notes");
            sql.Append("    ,c.Mobile");
            sql.Append("    ,c.OtherCard");
            sql.Append(" from");
            sql.Append("    tbl_NewTourOrderCustomer c");
            sql.Append(" inner join");
            sql.Append("    tbl_NewTourOrder o");
            sql.Append(" on");
            sql.Append("    o.orderId=c.orderid");
            sql.AppendFormat("    and o.orderstatus<>{0} and o.orderstatus<>{1}", (int)PowderOrderStatus.取消, (int)PowderOrderStatus.预留过期);
            sql.AppendFormat("    and o.tourid='{0}'", tourId);
            if (companyTyp == CompanyType.组团)
            {
                sql.AppendFormat("    and o.Travel='{0}'", companyId);
            }
            sql.Append(" inner join");
            sql.Append("    tbl_NewPowderList p");
            sql.Append(" on");
            sql.Append("    o.orderid=c.orderid");
            if (companyTyp == CompanyType.专线)
            {
                sql.AppendFormat("    and p.Publishers='{0}'", companyId);
            }
            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrderCustomer()
                    {
                        Id = (int)rd["Id"],
                        VisitorName = rd["VisitorName"].ToString(),
                        Sex = (Sex)Enum.Parse(typeof(Sex), rd["Sex"].ToString()),
                        CradType = (TicketVistorType)rd.GetByte(rd.GetOrdinal("CradType")),
                        OtherCard = rd.IsDBNull(rd.GetOrdinal("OtherCard")) ? string.Empty : rd["OtherCard"].ToString(),
                        ContactTel = rd["ContactTel"].ToString(),
                        IdentityCard = rd["IdentityCard"].ToString(),
                        SiteNo = rd["SiteNo"].ToString(),
                        Mobile = rd["Mobile"].ToString(),
                        Notes = rd["Notes"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据团队编号获取订单游客列表
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">专线商、组团社</param>
        /// <param name="companyTyp">公司类型</param>
        /// <param name="orderStatus">订单状态数组</param>
        /// <returns>订单游客列表</returns>
        public IList<MTourOrderCustomer> GetOrderCustomerByTourId(string tourId, string companyId, CompanyType companyTyp,
                                                           params PowderOrderStatus?[] orderStatus)
        {
            var lst = new List<MTourOrderCustomer>();
            var sql = new StringBuilder();

            sql.Append(" select distinct");
            sql.Append("    c.Id");
            sql.Append("    ,c.VisitorName");
            sql.Append("    ,c.Sex");
            sql.Append("    ,c.CradType");
            sql.Append("    ,c.ContactTel");
            sql.Append("    ,c.IdentityCard");
            sql.Append("    ,c.SiteNo");
            sql.Append("    ,c.Notes");
            sql.Append("    ,c.OtherCard");
            sql.Append("    ,c.Mobile");
            sql.Append(" from");
            sql.Append("    tbl_NewTourOrderCustomer c");
            sql.Append(" inner join");
            sql.Append("    tbl_NewTourOrder o");
            sql.Append(" on");
            sql.Append("    o.orderId=c.orderid ");
            if (orderStatus == null || orderStatus.Length <= 0)
                sql.AppendFormat(" and o.orderstatus<>{0} and o.orderstatus<>{1}", (int)PowderOrderStatus.取消, (int)PowderOrderStatus.预留过期);
            else
            {
                if (orderStatus.Length == 1)
                {
                    if (orderStatus[0].HasValue)
                        sql.AppendFormat(" and o.orderstatus = {0} ", (int)orderStatus[0].Value);
                }
                else
                {
                    string strTmp = string.Empty;
                    foreach (var t in orderStatus)
                    {
                        if (!t.HasValue)
                            continue;

                        strTmp += (int)t.Value + ",";
                    }
                    if (!string.IsNullOrEmpty(strTmp))
                    {
                        sql.AppendFormat(" and o.orderstatus in ({0}) ", strTmp.TrimEnd(','));
                    }
                }
            }
            sql.AppendFormat("    and o.tourid='{0}'", tourId);
            if (companyTyp == CompanyType.组团)
            {
                sql.AppendFormat("    and o.Travel='{0}'", companyId);
            }
            sql.Append(" inner join");
            sql.Append("    tbl_NewPowderList p");
            sql.Append(" on");
            sql.Append("    o.orderid=c.orderid");
            if (companyTyp == CompanyType.专线)
            {
                sql.AppendFormat("    and p.Publishers='{0}'", companyId);
            }
            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rd.Read())
                {
                    lst.Add(new MTourOrderCustomer()
                    {
                        Id = (int)rd["Id"],
                        VisitorName = rd["VisitorName"].ToString(),
                        Sex = (Sex)Enum.Parse(typeof(Sex), rd["Sex"].ToString()),
                        CradType = (TicketVistorType)rd.GetByte(rd.GetOrdinal("CradType")),
                        OtherCard = rd.IsDBNull(rd.GetOrdinal("OtherCard")) ? string.Empty : rd["OtherCard"].ToString(),
                        ContactTel = rd["ContactTel"].ToString(),
                        IdentityCard = rd["IdentityCard"].ToString(),
                        SiteNo = rd["SiteNo"].ToString(),
                        Mobile = rd["Mobile"].ToString(),
                        Notes = rd["Notes"].ToString()
                    });
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据搜索实体获取不带分页订单操作日志列表
        /// </summary>
        /// <param name="search">订单搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        public IList<MOrderHandleLog> GetOrderHandleLogLst(MOrderHandleLogSearch search)
        {
            var lst = new List<MOrderHandleLog>();
            var sql = new StringBuilder();
            sql.Append(" select");
            sql.Append("    o.OrderNo");
            sql.Append("    ,c.CompanyName");
            sql.Append("    ,o.OperatorName");
            sql.Append("    ,o.OrderType");
            sql.Append("    ,o.IssueTime");
            sql.Append("    ,o.Remark");
            sql.Append("    ,c.CompanyType");
            sql.Append("    ,o.CompanyId");
            sql.Append(" from");
            sql.Append("    tbl_OrderHandleLog o");
            sql.Append(" left outer join");
            sql.Append("    tbl_CompanyInfo c");
            sql.Append(" on");
            sql.Append("    o.CompanyId=c.Id");
            sql.Append(" where");
            if (search != null && !string.IsNullOrEmpty(search.OrderNo))
            {
                sql.AppendFormat("    o.OrderNo like '%{0}%' and", Utility.ToSqlLike(search.OrderNo));
            }
            if (search != null && !string.IsNullOrEmpty(search.OperatorName))
            {
                sql.AppendFormat("    o.OperatorName like '%{0}%' and", Utility.ToSqlLike(search.OperatorName));
            }
            if (search != null && !string.IsNullOrEmpty(search.OperatorId))
            {
                sql.Append("    o.OperatorId=@OperatorId and");
            }
            if (search != null && !string.IsNullOrEmpty(search.OrderId))
            {
                sql.Append("    o.OrderId=@OrderId and");
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                sql.Append("    o.CompanyId=@CompanyId and");
            }
            sql.Append("    1=1");
            sql.Append(" order by");
            sql.Append("    o.IssueTime");
            var cmd = this._db.GetSqlStringCommand(sql.ToString());
            if (search != null && !string.IsNullOrEmpty(search.OperatorId))
            {
                this._db.AddInParameter(cmd, "@OperatorId", DbType.AnsiStringFixedLength, search.OperatorId);
            }
            if (search != null && !string.IsNullOrEmpty(search.OrderId))
            {
                this._db.AddInParameter(cmd, "@OrderId", DbType.AnsiStringFixedLength, search.OrderId);
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                this._db.AddInParameter(cmd, "@CompanyId", DbType.AnsiStringFixedLength, search.CompanyId);
            }
            using (var rd = DbHelper.ExecuteReader(cmd, this._db))
            {
                while (rd.Read())
                {
                    lst.Add(new MOrderHandleLog
                    {
                        OrderNo = rd["OrderNo"].ToString(),
                        CompanyId = rd["CompanyId"].ToString(),
                        CompanyName = rd["CompanyName"].ToString(),
                        OperatorName = rd["OperatorName"].ToString(),
                        CompanyType = (CompanyType)Enum.Parse(typeof(CompanyType), rd["CompanyType"].ToString()),
                        OrderType = (OrderSource)rd.GetByte(rd.GetOrdinal("OrderType")),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        Remark = rd["Remark"].ToString()
                    });
                }
            }
            return lst;
        }
        /// <summary>
        /// 根据搜索实体获取带分页订单操作日志列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>订单操作日志列表</returns>
        public IList<MOrderHandleLog> GetOrderHandleLogLst(int pageSize, int pageCurrent, ref int recordCount, MOrderHandleLogSearch search)
        {
            var lst = new List<MOrderHandleLog>();
            const string field = "OrderNo,CompanyName=(select CompanyName from tbl_CompanyInfo where Id=tbl_OrderHandleLog.CompanyId),OperatorName,OrderType,IssueTime,Remark,CompanyId,CompanyType=(select CompanyType from tbl_CompanyInfo where Id=tbl_OrderHandleLog.CompanyId)";
            var query = new StringBuilder();

            if (search != null && !string.IsNullOrEmpty(search.OrderNo))
            {
                query.AppendFormat(" OrderNo like '%{0}%' and", Utility.ToSqlLike(search.OrderNo));
            }
            if (search != null && !string.IsNullOrEmpty(search.OperatorName))
            {
                query.AppendFormat(" OperatorName like '%{0}%' and", Utility.ToSqlLike(search.OperatorName));
            }
            if (search != null && !string.IsNullOrEmpty(search.OperatorId))
            {
                query.AppendFormat(" OperatorId='{0}' and", search.OperatorId);
            }
            if (search != null && !string.IsNullOrEmpty(search.OrderId))
            {
                query.AppendFormat(" OrderId='{0}' and", search.OrderId);
            }
            if (search != null && !string.IsNullOrEmpty(search.CompanyId))
            {
                query.AppendFormat(" CompanyId='{0}' and", search.CompanyId);
            }
            query.Append(" 1=1");

            using (var rd = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, "tbl_OrderHandleLog", "LogId", field, query.ToString(), "IssueTime DESC", false))
            {
                while (rd.Read())
                {
                    lst.Add(new MOrderHandleLog
                    {
                        OrderNo = rd["OrderNo"].ToString(),
                        CompanyName = rd["CompanyName"].ToString(),
                        CompanyId = rd["CompanyId"].ToString(),
                        OperatorName = rd["OperatorName"].ToString(),
                        OrderType = (OrderSource)rd.GetByte(rd.GetOrdinal("OrderType")),
                        CompanyType = (CompanyType)Enum.Parse(typeof(CompanyType), rd["CompanyType"].ToString()),
                        IssueTime = rd.GetDateTime(rd.GetOrdinal("IssueTime")),
                        Remark = rd["Remark"].ToString()
                    });
                }
            }
            return lst;
        }

        #endregion

        #region private

        /// <summary>
        /// 根据线路编号或者线路区域编号获取订单的人数和金额
        /// </summary>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="routeId">线路编号</param>
        /// <param name="orderStatus">订单状态数组</param>
        /// <param name="search">查询实体</param>
        /// <param name="totalAdult">成人数</param>
        /// <param name="totalChild">儿童数</param>
        /// <param name="totalSale">销售金额</param>
        /// <param name="totalSettle">结算金额</param>
        private void GetCheckOrderPeopleNum(int areaId, string routeId, PowderOrderStatus?[] orderStatus, MOrderStaticSearch search
            , ref int totalAdult, ref int totalChild, ref decimal totalSale, ref decimal totalSettle)
        {
            totalAdult = 0;
            totalChild = 0;
            totalSale = 0M;
            totalSettle = 0M;
            var strSql = new StringBuilder();

            #region Sql拼接

            strSql.Append(" select ");
            strSql.Append(" sum(nto.AdultNum) as TotalAdult ");
            strSql.Append(" ,sum(nto.ChildrenNum) as TotalChild ");
            strSql.Append(" ,sum(nto.TotalSalePrice) as TotalSale ");
            strSql.Append(" ,sum(nto.TotalSettlementPrice) as TotalSettle ");
            strSql.Append(" from tbl_NewTourOrder as nto where 1 = 1 ");
            if (areaId > 0)
            {
                strSql.AppendFormat(" and nto.TourId in (select TourId from tbl_NewPowderList as a where a.AreaId = {0}) ",
                                    areaId);
            }
            if (!string.IsNullOrEmpty(routeId))
            {
                strSql.AppendFormat(" and nto.RouteId = '{0}' ", routeId);
            }
            if (orderStatus != null && orderStatus.Length > 0)
            {
                if (orderStatus.Length == 1)
                {
                    if (orderStatus[0] != null && orderStatus[0].HasValue)
                        strSql.AppendFormat(" and nto.OrderStatus = {0} ", (int)orderStatus[0].Value);
                }
                else
                {
                    string strTmp = string.Empty;
                    foreach (var t in orderStatus)
                    {
                        if (!t.HasValue)
                            continue;

                        strTmp += (int)t.Value + ",";
                    }
                    if (!string.IsNullOrEmpty(strTmp.TrimEnd(',')))
                    {
                        strSql.AppendFormat(" and nto.OrderStatus in ({0}) ", strTmp.TrimEnd(','));
                    }
                }
            }
            if (search != null)
            {
                if (search.LeaveDateS.HasValue || search.LeaveDateE.HasValue || search.CompanyTyp == CompanyType.专线)
                {
                    strSql.Append(" and nto.TourId in (select TourId from tbl_NewPowderList as a where 1 = 1 ");
                    if (search.LeaveDateS.HasValue)
                    {
                        strSql.AppendFormat(" and a.LeaveDate >= '{0}' ", search.LeaveDateS.Value);
                    }
                    if (search.LeaveDateE.HasValue)
                    {
                        strSql.AppendFormat(" and a.LeaveDate <= '{0}' ", search.LeaveDateE.Value);
                    }
                    if (search.CompanyTyp == CompanyType.专线 && !string.IsNullOrEmpty(search.CompanyId))
                    {
                        strSql.AppendFormat(" and a.Publishers = '{0}' ", search.CompanyId);
                    }
                    strSql.Append(" ) ");
                }
                if (search.CompanyTyp == CompanyType.组团 && !string.IsNullOrEmpty(search.CompanyId))
                {
                    strSql.AppendFormat(" and nto.Travel = '{0}' ", search.CompanyId);
                }
                if (search.AreaType.HasValue)
                {
                    strSql.AppendFormat(
                        " and nto.RouteId in (select RouteId from tbl_NewRouteBasicInfo as nrb where nrb.IsDeleted = '0' and nrb.RouteType = {0}) ",
                        (int)search.AreaType.Value);
                }
            }

            #endregion

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                if (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("TotalAdult")))
                        totalAdult = dr.GetInt32(dr.GetOrdinal("TotalAdult"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TotalChild")))
                        totalChild = dr.GetInt32(dr.GetOrdinal("TotalChild"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TotalSale")))
                        totalSale = dr.GetDecimal(dr.GetOrdinal("TotalSale"));
                    if (!dr.IsDBNull(dr.GetOrdinal("TotalSettle")))
                        totalSettle = dr.GetDecimal(dr.GetOrdinal("TotalSettle"));
                }
            }
        }

        /// <summary>
        /// 生成订单游客XML
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        internal static string CreateCustomersXml(IList<MTourOrderCustomer> lst)
        {
            var xml = new StringBuilder();
            xml.Append("<Customers>");
            if (lst != null && lst.Count > 0)
            {
                foreach (var m in lst)
                {
                    xml.AppendFormat("<Customer OrderId=\"{0}\"", m.OrderId);
                    xml.AppendFormat(" VisitorName=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.VisitorName));
                    xml.AppendFormat(" ContactTel=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.ContactTel));
                    xml.AppendFormat(" Mobile = \"{0}\" ", Utility.ReplaceXmlSpecialCharacter(m.Mobile));
                    xml.AppendFormat(" IdentityCard=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.IdentityCard));
                    xml.AppendFormat(" Passport=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.Passport));
                    xml.AppendFormat(" CertificatesType=\"{0}\"", (int)m.CertificatesType);
                    xml.AppendFormat(" OtherCard=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.OtherCard));
                    xml.AppendFormat(" Sex=\"{0}\"", (int)m.Sex);
                    xml.AppendFormat(" CradType=\"{0}\"", (int)m.CradType);
                    xml.AppendFormat(" SiteNo=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.SiteNo));
                    xml.AppendFormat(" Notes=\"{0}\"", Utility.ReplaceXmlSpecialCharacter(m.Notes));
                    xml.AppendFormat(" IssueTime=\"{0}\"", m.IssueTime);
                    xml.AppendFormat(" IsSaveToTicketVistorInfo=\"{0}\"", m.IsSaveToTicketVistorInfo ? "1" : "0");
                    xml.AppendFormat(" CompanyId=\"{0}\" />", m.CompanyId);
                }
            }
            xml.Append("</Customers>");
            return xml.ToString();
        }

        /// <summary>
        /// 根据订单游客
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        internal static IList<MTourOrderCustomer> GetCustomerLst(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            var x = XElement.Parse(xml);
            var r = Utils.GetXElements(x, "row");
            return r.Select(i => new MTourOrderCustomer
            {
                Id = Utils.GetInt(Utils.GetXAttributeValue(i, "Id")),
                OrderId = Utils.GetXAttributeValue(i, "OrderId"),
                VisitorName = Utils.GetXAttributeValue(i, "VisitorName"),
                ContactTel = Utils.GetXAttributeValue(i, "ContactTel"),
                IdentityCard = Utils.GetXAttributeValue(i, "IdentityCard"),
                Passport = Utils.GetXAttributeValue(i, "Passport"),
                CertificatesType = (TicketCardType)Utils.GetInt(Utils.GetXAttributeValue(i, "CertificatesType")),
                OtherCard = Utils.GetXAttributeValue(i, "OtherCard"),
                Sex = (Sex)Utils.GetInt(Utils.GetXAttributeValue(i, "Sex")),
                CradType = (TicketVistorType)Utils.GetInt(Utils.GetXAttributeValue(i, "CradType")),
                SiteNo = Utils.GetXAttributeValue(i, "SiteNo"),
                Notes = Utils.GetXAttributeValue(i, "Notes"),
                IssueTime = Utils.GetDateTime(Utils.GetXAttributeValue(i, "IssueTime")),
                Mobile = Utils.GetXAttributeValue(i, "Mobile")
            }).ToList();
        }
        /// <summary>
        /// 排序规则
        /// 1：按出发时间排序
        /// 2：按下单时间排序
        /// 3：按订单状态排序
        /// </summary>
        /// <param name="i">排序字段</param>
        /// <param name="isDesc">True：降序 False：升序</param>
        /// <returns></returns>
        internal static string GetOrderBy(int i, bool isDesc)
        {
            string orderby;
            switch (i)
            {
                case 1:
                    orderby = "LeaveDate"; break;
                case 2:
                    orderby = "IssueTime"; break;
                case 3:
                    orderby = "OrderStatus"; break;
                default:
                    orderby = "LeaveDate"; break;
            }
            if (isDesc)
            {
                orderby += " DESC";
            }
            return orderby;
        }
        /// <summary>
        /// 获取散拼订单状态
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        internal static string GetOrderStatusForIn(IList<PowderOrderStatus> lst)
        {
            return lst.Aggregate(string.Empty, (current, en) => current + (int)en + ",").TrimEnd(',');
        }
        /// <summary>
        /// 获取支付状态
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        internal static string GetPaymentStatusForIn(IList<PaymentStatus> lst)
        {
            return lst.Aggregate(string.Empty, (current, en) => current + (int)en + ",").TrimEnd(',');
        }

        #endregion
    }
}
