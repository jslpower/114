using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.NewTourStructure;
using System.Data;
using System.Data.Common;
using EyouSoft.Model.CompanyStructure;

namespace EyouSoft.DAL.NewTourStructure
{
    /// <summary>
    /// 团队计划,订单接口
    /// 修改记录：
    /// 1、2011-12-20 郑付杰 创建
    /// 2、2011-12-28 曹胡生 增加方法实现
    /// </summary>
    public class DTourList : EyouSoft.Common.DAL.DALBase, EyouSoft.IDAL.NewTourStructure.ITourList
    {
        #region

        private Database _db = null;
        /// <summary>
        /// 
        /// </summary>
        public DTourList()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region ITourList 成员

        /// <summary>
        /// 添加团队订单(单团预订)
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddTourList(MTourList item)
        {
            DbCommand cmd = this._db.GetStoredProcCommand("proc_Tour_AddTourPlan");
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, item.TourId);
            this._db.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(cmd, "OrderNo", DbType.String, item.OrderNo);
            this._db.AddInParameter(cmd, "RouteName", DbType.String, item.RouteName);
            this._db.AddInParameter(cmd, "StartDate", DbType.DateTime, item.StartDate);
            this._db.AddInParameter(cmd, "Business", DbType.AnsiStringFixedLength, item.Business);
            this._db.AddInParameter(cmd, "LeaveDate", DbType.String, item.SLeaveDate);
            this._db.AddInParameter(cmd, "ComeBackDate", DbType.String, item.ComeBackDate);
            this._db.AddInParameter(cmd, "TravelContact", DbType.String, item.TravelContact);
            this._db.AddInParameter(cmd, "TravelTel", DbType.String, item.TravelTel);
            this._db.AddInParameter(cmd, "VisitorContact", DbType.String, item.VisitorContact);
            this._db.AddInParameter(cmd, "VisitorTel", DbType.String, item.VisitorTel);
            this._db.AddInParameter(cmd, "ScheduleNum", DbType.Int32, item.ScheduleNum);
            this._db.AddInParameter(cmd, "AdultNum", DbType.Int32, item.AdultNum);
            this._db.AddInParameter(cmd, "ChildrenNum", DbType.Int32, item.ChildrenNum);
            this._db.AddInParameter(cmd, "SingleRoomNum", DbType.Int32, item.SingleRoomNum);
            this._db.AddInParameter(cmd, "VisitorNotes", DbType.String, item.VisitorNotes);
            this._db.AddInParameter(cmd, "TravelNotes", DbType.String, item.TravelNotes);
            this._db.AddInParameter(cmd, "BusinessNotes", DbType.String, item.BusinessNotes);
            this._db.AddInParameter(cmd, "IssueTime", DbType.DateTime, item.IssueTime);
            this._db.AddInParameter(cmd, "OrderStatus", DbType.Byte, (int)EyouSoft.Model.NewTourStructure.TourOrderStatus.未确认);
            this._db.AddInParameter(cmd, "TourStatus", DbType.Byte, (int)EyouSoft.Model.NewTourStructure.TourStatus.收客);
            this._db.AddInParameter(cmd, "OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(cmd, "TourPrice", DbType.Decimal, item.TourPrice);
            this._db.AddInParameter(cmd, "Travel", DbType.AnsiStringFixedLength, item.Travel);
            _db.AddOutParameter(cmd, "Result", DbType.Int32, 4);
            DbHelper.RunProcedure(cmd, _db);
            return Convert.ToInt32(_db.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public MTourList GetModel(string TourId)
        {
            MTourList model = null;
            string sql = @"SELECT [TourId]
              ,[TourNo]
              ,[OrderNo]
              ,[RouteId]
              ,[RouteName]
              ,[StartDate]
              ,[Business]
              ,[LeaveDate]
              ,[ComeBackDate]
              ,[TravelContact]
              ,[TravelTel]
              ,[VisitorContact]
              ,[VisitorTel]
              ,[ScheduleNum]
              ,[AdultNum]
              ,[ChildrenNum]
              ,[SingleRoomNum]
              ,[VisitorNotes]
              ,[TravelNotes]
              ,[BusinessNotes]
              ,[IssueTime]
              ,[OrderStatus]
              ,[TourStatus]
              ,[OperatorId]
              ,[TourPrice]
              ,[Travel],(select StartCityName,EndCityName,GroupNum,DayNum,LateNum,AdultPrice,ChildrenPrice,VendorsNotes,StartTraffic,EndTraffic from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId for xml raw,root) as RouteInfo,(select CompanyName,ContactMQ,ContactQQ from tbl_CompanyInfo where Id=tbl_NewTourList.Travel for xml raw,root) as TravelInfo,(select CompanyName,ContactMQ,ContactQQ from tbl_CompanyInfo where Id=tbl_NewTourList.Business for xml raw,root) as BusinessInfo,(select ContactName from tbl_CompanyUser where Id=tbl_NewTourList.OperatorId) as OperatorName 
              FROM tbl_NewTourList WHERE TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, this._db))
            {
                if (rdr.Read())
                {
                    model = new MTourList();
                    model.TourId = rdr.IsDBNull(rdr.GetOrdinal("TourId")) ? "" : rdr.GetString(rdr.GetOrdinal("TourId"));
                    model.OrderNo = rdr.IsDBNull(rdr.GetOrdinal("OrderNo")) ? "" : rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    model.TourNo = rdr.IsDBNull(rdr.GetOrdinal("TourNo")) ? "" : rdr.GetString(rdr.GetOrdinal("TourNo"));
                    model.RouteId = rdr.IsDBNull(rdr.GetOrdinal("RouteId")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteId"));
                    model.RouteName = rdr.IsDBNull(rdr.GetOrdinal("RouteName")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteName"));
                    model.StartDate = rdr.IsDBNull(rdr.GetOrdinal("StartDate")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    model.Business = rdr.IsDBNull(rdr.GetOrdinal("Business")) ? "" : rdr.GetString(rdr.GetOrdinal("Business"));
                    model.SLeaveDate = rdr.IsDBNull(rdr.GetOrdinal("LeaveDate")) ? "" : rdr.GetString(rdr.GetOrdinal("LeaveDate"));
                    model.ComeBackDate = rdr.IsDBNull(rdr.GetOrdinal("ComeBackDate")) ? "" : rdr.GetString(rdr.GetOrdinal("ComeBackDate"));
                    model.TravelContact = rdr.IsDBNull(rdr.GetOrdinal("TravelContact")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelContact"));
                    model.TravelTel = rdr.IsDBNull(rdr.GetOrdinal("TravelTel")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelTel"));
                    model.VisitorContact = rdr.IsDBNull(rdr.GetOrdinal("VisitorContact")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorContact"));
                    model.VisitorTel = rdr.IsDBNull(rdr.GetOrdinal("VisitorTel")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorTel"));
                    model.ScheduleNum = rdr.IsDBNull(rdr.GetOrdinal("ScheduleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ScheduleNum"));
                    model.AdultNum = rdr.IsDBNull(rdr.GetOrdinal("AdultNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AdultNum"));
                    model.ChildrenNum = rdr.IsDBNull(rdr.GetOrdinal("ChildrenNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ChildrenNum"));
                    model.SingleRoomNum = rdr.IsDBNull(rdr.GetOrdinal("SingleRoomNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SingleRoomNum"));
                    model.VisitorNotes = rdr.IsDBNull(rdr.GetOrdinal("VisitorNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorNotes"));
                    model.TravelNotes = rdr.IsDBNull(rdr.GetOrdinal("TravelNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelNotes"));
                    model.BusinessNotes = rdr.IsDBNull(rdr.GetOrdinal("BusinessNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessNotes"));
                    model.IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    model.OrderStatus = (TourOrderStatus)rdr.GetByte(rdr.GetOrdinal("OrderStatus"));
                    model.TourStatus = (TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    model.OperatorId = rdr.IsDBNull(rdr.GetOrdinal("OperatorId")) ? "" : rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    model.TourPrice = rdr.IsDBNull(rdr.GetOrdinal("TourPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPrice"));
                    model.Travel = rdr.IsDBNull(rdr.GetOrdinal("Travel")) ? "" : rdr.GetString(rdr.GetOrdinal("Travel"));
                    model.OperatorName = rdr.IsDBNull(rdr.GetOrdinal("OperatorName")) ? "" : rdr.GetString(rdr.GetOrdinal("OperatorName"));

                    model.TravelName = rdr.IsDBNull(rdr.GetOrdinal("TravelInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("TravelInfo")), "CompanyName");
                    model.TravelMQ = rdr.IsDBNull(rdr.GetOrdinal("TravelInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("TravelInfo")), "ContactMQ");
                    model.TravelQQ = rdr.IsDBNull(rdr.GetOrdinal("TravelInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("TravelInfo")), "ContactQQ");
                    model.BusinessName = rdr.IsDBNull(rdr.GetOrdinal("BusinessInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("BusinessInfo")), "CompanyName");
                    model.BusinessMQ = rdr.IsDBNull(rdr.GetOrdinal("BusinessInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("BusinessInfo")), "ContactMQ");
                    model.BusinessQQ = rdr.IsDBNull(rdr.GetOrdinal("BusinessInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("BusinessInfo")), "ContactQQ");

                    model.VendorsNotes = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "VendorsNotes");
                    model.DayNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "DayNum"));
                    model.LateNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "LateNum"));
                    model.StartCityName = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "StartCityName");
                    model.EndCityName = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "EndCityName");
                    model.GroupNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "GroupNum"));
                    model.AdultPrice = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetDecimal(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "AdultPrice"));
                    model.ChildrenPrice = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetDecimal(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "ChildrenPrice"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")))
                    {
                        model.StartTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "StartTraffic"));
                        model.EndTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "EndTraffic"));
                    }

                }
            }
            return model;
        }

        /// <summary>
        /// 团队订单(专线，地接)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="OrderType">订单来源</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetList(int pageSize, int pageCurrent, ref int recordCount, string companyId, RouteSource OrderType, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            IList<MTourList> list = new List<MTourList>();
            string tableName = "tbl_NewTourList";
            string fields = "*,(select StartCityName,DayNum,LateNum from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId for xml raw,root) as RouteInfo,(select CompanyName from tbl_CompanyInfo where Id=tbl_NewTourList.Travel) as TravelName,(select CompanyType from tbl_CompanyInfo where Id=tbl_NewTourList.Travel) as CompanyType";
            string primaryKey = "TourId";
            string orderByString = "IssueTime DESC ";
            StringBuilder strWhere = new StringBuilder("1=1");
            strWhere.AppendFormat(" and Business='{0}' and OrderSource={1}", companyId, (int)OrderType);
            if (search != null)
            {
                if (search.SLeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)>=0", search.SLeaveDate);
                }
                if (search.ELeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)<=0", search.ELeaveDate);
                }
                if (search.TourOrderStatus != null)
                {
                    strWhere.AppendFormat(" and OrderStatus={0}", (int)search.TourOrderStatus);
                }
                if (!string.IsNullOrEmpty(search.TourKey))
                {
                    strWhere.AppendFormat(" and (RouteName like '%{0}%' or exists(select 1 from tbl_CompanyInfo where Id=tbl_NewTourList.Travel and CompanyName like '%{0}%'))", search.TourKey.Trim());
                }
                if (search.AreaType != null)
                {
                    strWhere.AppendFormat(" and exists(select 1 from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId and RouteType={0})", (int)search.AreaType);
                }
            }
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    MTourList model = new MTourList();
                    model.TourId = rdr.IsDBNull(rdr.GetOrdinal("TourId")) ? "" : rdr.GetString(rdr.GetOrdinal("TourId"));
                    model.OrderNo = rdr.IsDBNull(rdr.GetOrdinal("OrderNo")) ? "" : rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    model.TourNo = rdr.IsDBNull(rdr.GetOrdinal("TourNo")) ? "" : rdr.GetString(rdr.GetOrdinal("TourNo"));
                    model.RouteId = rdr.IsDBNull(rdr.GetOrdinal("RouteId")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteId"));
                    model.RouteName = rdr.IsDBNull(rdr.GetOrdinal("RouteName")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteName"));
                    model.StartDate = rdr.IsDBNull(rdr.GetOrdinal("StartDate")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    model.Business = rdr.IsDBNull(rdr.GetOrdinal("Business")) ? "" : rdr.GetString(rdr.GetOrdinal("Business"));
                    model.SLeaveDate = rdr.IsDBNull(rdr.GetOrdinal("LeaveDate")) ? "" : rdr.GetString(rdr.GetOrdinal("LeaveDate"));
                    model.ComeBackDate = rdr.IsDBNull(rdr.GetOrdinal("ComeBackDate")) ? "" : rdr.GetString(rdr.GetOrdinal("ComeBackDate"));
                    model.TravelContact = rdr.IsDBNull(rdr.GetOrdinal("TravelContact")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelContact"));
                    model.TravelTel = rdr.IsDBNull(rdr.GetOrdinal("TravelTel")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelTel"));
                    model.VisitorContact = rdr.IsDBNull(rdr.GetOrdinal("VisitorContact")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorContact"));
                    model.VisitorTel = rdr.IsDBNull(rdr.GetOrdinal("VisitorTel")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorTel"));
                    model.ScheduleNum = rdr.IsDBNull(rdr.GetOrdinal("ScheduleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ScheduleNum"));
                    model.AdultNum = rdr.IsDBNull(rdr.GetOrdinal("AdultNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AdultNum"));
                    model.ChildrenNum = rdr.IsDBNull(rdr.GetOrdinal("ChildrenNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ChildrenNum"));
                    model.SingleRoomNum = rdr.IsDBNull(rdr.GetOrdinal("SingleRoomNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SingleRoomNum"));
                    model.VisitorNotes = rdr.IsDBNull(rdr.GetOrdinal("VisitorNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorNotes"));
                    model.TravelNotes = rdr.IsDBNull(rdr.GetOrdinal("TravelNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelNotes"));
                    model.BusinessNotes = rdr.IsDBNull(rdr.GetOrdinal("BusinessNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessNotes"));
                    model.IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    model.OrderStatus = (TourOrderStatus)rdr.GetByte(rdr.GetOrdinal("OrderStatus"));
                    model.TourStatus = (TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    model.OperatorId = rdr.IsDBNull(rdr.GetOrdinal("OperatorId")) ? "" : rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    model.TourPrice = rdr.IsDBNull(rdr.GetOrdinal("TourPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPrice"));
                    model.Travel = rdr.IsDBNull(rdr.GetOrdinal("Travel")) ? "" : rdr.GetString(rdr.GetOrdinal("Travel"));
                    model.DayNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "DayNum"));
                    model.LateNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "LateNum"));
                    model.StartCityName = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "StartCityName");
                    model.TravelName = rdr.IsDBNull(rdr.GetOrdinal("TravelName")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelName"));
                    model.CompanyType = (CompanyType)Enum.Parse(typeof(CompanyType), rdr["CompanyType"].ToString());

                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 团队订单(组团社)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<EyouSoft.Model.NewTourStructure.MTourList> GetList(int pageSize, int pageCurrent, ref int recordCount, string companyId, EyouSoft.Model.NewTourStructure.MTourListSearch search)
        {
            IList<MTourList> list = new List<MTourList>();
            string tableName = "tbl_NewTourList";
            string fields = "*,(select StartCityName,DayNum,LateNum from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId for xml raw,root) as RouteInfo,(select CompanyName from tbl_CompanyInfo where Id=tbl_NewTourList.Business) as BusinessName,(select CompanyType from tbl_CompanyInfo where Id=tbl_NewTourList.Business) as CompanyType";
            string primaryKey = "TourId";
            string orderByString = "IssueTime DESC ";
            StringBuilder strWhere = new StringBuilder("1=1");
            strWhere.AppendFormat(" and Travel='{0}'", companyId);
            if (search != null)
            {

                if (search.SLeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)>=0", search.SLeaveDate);
                }
                if (search.ELeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)<=0", search.ELeaveDate);
                }
                if (search.TourOrderStatus != null)
                {
                    strWhere.AppendFormat(" and OrderStatus={0}", (int)search.TourOrderStatus);
                }
                if (search.OrderStatus != null && search.OrderStatus.Length > 0)
                {
                    if (search.OrderStatus.Length == 1 && search.OrderStatus[0].HasValue)
                        strWhere.AppendFormat(" and OrderStatus={0}", (int)search.OrderStatus[0].Value);
                    else
                    {
                        string strIds = string.Empty;
                        foreach (var t in search.OrderStatus)
                        {
                            if (!t.HasValue)
                                continue;

                            strIds += (int)t.Value + ",";
                        }
                        strIds = strIds.TrimEnd(',');
                        if (!string.IsNullOrEmpty(strIds))
                            strWhere.AppendFormat(" and OrderStatus in ({0}) ", strIds);
                    }
                }
                if (!string.IsNullOrEmpty(search.TourKey))
                {
                    strWhere.AppendFormat(" and (RouteName like '%{0}%' or VisitorContact like '%{0}%' or exists(select 1 from tbl_CompanyInfo where Id=tbl_NewTourList.Business and CompanyName like '%{0}%'))", search.TourKey.Trim());
                }
                if (search.AreaType != null)
                {
                    strWhere.AppendFormat(" and exists(select 1 from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId and RouteType={0})", (int)search.AreaType);
                }
            }
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    MTourList model = new MTourList();
                    model.TourId = rdr.IsDBNull(rdr.GetOrdinal("TourId")) ? "" : rdr.GetString(rdr.GetOrdinal("TourId"));
                    model.OrderNo = rdr.IsDBNull(rdr.GetOrdinal("OrderNo")) ? "" : rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    model.TourNo = rdr.IsDBNull(rdr.GetOrdinal("TourNo")) ? "" : rdr.GetString(rdr.GetOrdinal("TourNo"));
                    model.RouteId = rdr.IsDBNull(rdr.GetOrdinal("RouteId")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteId"));
                    model.RouteName = rdr.IsDBNull(rdr.GetOrdinal("RouteName")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteName"));
                    model.StartDate = rdr.IsDBNull(rdr.GetOrdinal("StartDate")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    model.Business = rdr.IsDBNull(rdr.GetOrdinal("Business")) ? "" : rdr.GetString(rdr.GetOrdinal("Business"));
                    model.SLeaveDate = rdr.IsDBNull(rdr.GetOrdinal("LeaveDate")) ? "" : rdr.GetString(rdr.GetOrdinal("LeaveDate"));
                    model.ComeBackDate = rdr.IsDBNull(rdr.GetOrdinal("ComeBackDate")) ? "" : rdr.GetString(rdr.GetOrdinal("ComeBackDate"));
                    model.TravelContact = rdr.IsDBNull(rdr.GetOrdinal("TravelContact")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelContact"));
                    model.TravelTel = rdr.IsDBNull(rdr.GetOrdinal("TravelTel")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelTel"));
                    model.VisitorContact = rdr.IsDBNull(rdr.GetOrdinal("VisitorContact")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorContact"));
                    model.VisitorTel = rdr.IsDBNull(rdr.GetOrdinal("VisitorTel")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorTel"));
                    model.ScheduleNum = rdr.IsDBNull(rdr.GetOrdinal("ScheduleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ScheduleNum"));
                    model.AdultNum = rdr.IsDBNull(rdr.GetOrdinal("AdultNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AdultNum"));
                    model.ChildrenNum = rdr.IsDBNull(rdr.GetOrdinal("ChildrenNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ChildrenNum"));
                    model.SingleRoomNum = rdr.IsDBNull(rdr.GetOrdinal("SingleRoomNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SingleRoomNum"));
                    model.VisitorNotes = rdr.IsDBNull(rdr.GetOrdinal("VisitorNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorNotes"));
                    model.TravelNotes = rdr.IsDBNull(rdr.GetOrdinal("TravelNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelNotes"));
                    model.BusinessNotes = rdr.IsDBNull(rdr.GetOrdinal("BusinessNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessNotes"));
                    model.IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    model.OrderStatus = (TourOrderStatus)rdr.GetByte(rdr.GetOrdinal("OrderStatus"));
                    model.TourStatus = (TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    model.OperatorId = rdr.IsDBNull(rdr.GetOrdinal("OperatorId")) ? "" : rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    model.TourPrice = rdr.IsDBNull(rdr.GetOrdinal("TourPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPrice"));
                    model.Travel = rdr.IsDBNull(rdr.GetOrdinal("Travel")) ? "" : rdr.GetString(rdr.GetOrdinal("Travel"));
                    model.DayNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "DayNum"));
                    model.LateNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "LateNum"));
                    model.StartCityName = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "StartCityName");
                    model.BusinessName = rdr.IsDBNull(rdr.GetOrdinal("BusinessName")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessName"));
                    model.CompanyType = (CompanyType)Enum.Parse(typeof(CompanyType), rdr["CompanyType"].ToString());
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 团队订单-运营后台
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">搜索实体</param>
        /// <returns>团队订单集合</returns>
        public IList<MTourList> GetList(int pageSize, int pageCurrent, ref int recordCount, MTourListSearch search)
        {
            IList<MTourList> list = new List<MTourList>();
            string tableName = "tbl_NewTourList";
            string fields = "*,(select StartCityName,DayNum,LateNum from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId for xml raw,root) as RouteInfo,(select CompanyName from tbl_CompanyInfo where Id=tbl_NewTourList.Business) as BusinessName,(select CompanyName from tbl_CompanyInfo where Id=tbl_NewTourList.Travel) as TravelName";
            string primaryKey = "TourId";
            string orderByString = "IssueTime DESC ";
            StringBuilder strWhere = new StringBuilder("1=1");
            if (search != null)
            {
                if (search.SLeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)>=0", search.SLeaveDate);
                }
                if (search.ELeaveDate.HasValue)
                {
                    strWhere.AppendFormat(" and datediff(day,'{0}',StartDate)<=0", search.ELeaveDate);
                }
                if (search.TourOrderStatus != null)
                {
                    strWhere.AppendFormat(" and OrderStatus={0}", (int)search.TourOrderStatus);
                }
                if (!string.IsNullOrEmpty(search.TourKey))
                {
                    strWhere.AppendFormat(" and (isnull(RouteName,'') + (select isnull(CompanyName,'') from tbl_CompanyInfo where Id=tbl_NewTourList.Business) + (select isnull(CompanyName,'') from tbl_CompanyInfo where Id=tbl_NewTourList.Travel)) like '%{0}%'", search.TourKey.Trim());
                }
                if (search.AreaType != null)
                {
                    strWhere.AppendFormat(" and exists(select 1 from tbl_NewRouteBasicInfo where RouteId=tbl_NewTourList.RouteId and RouteType={0})", (int)search.AreaType);
                }
            }
            using (IDataReader rdr = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    MTourList model = new MTourList();
                    model.TourId = rdr.IsDBNull(rdr.GetOrdinal("TourId")) ? "" : rdr.GetString(rdr.GetOrdinal("TourId"));
                    model.OrderNo = rdr.IsDBNull(rdr.GetOrdinal("OrderNo")) ? "" : rdr.GetString(rdr.GetOrdinal("OrderNo"));
                    model.TourNo = rdr.IsDBNull(rdr.GetOrdinal("TourNo")) ? "" : rdr.GetString(rdr.GetOrdinal("TourNo"));
                    model.RouteId = rdr.IsDBNull(rdr.GetOrdinal("RouteId")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteId"));
                    model.RouteName = rdr.IsDBNull(rdr.GetOrdinal("RouteName")) ? "" : rdr.GetString(rdr.GetOrdinal("RouteName"));
                    model.StartDate = rdr.IsDBNull(rdr.GetOrdinal("StartDate")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("StartDate"));
                    model.Business = rdr.IsDBNull(rdr.GetOrdinal("Business")) ? "" : rdr.GetString(rdr.GetOrdinal("Business"));
                    model.SLeaveDate = rdr.IsDBNull(rdr.GetOrdinal("LeaveDate")) ? "" : rdr.GetString(rdr.GetOrdinal("LeaveDate"));
                    model.ComeBackDate = rdr.IsDBNull(rdr.GetOrdinal("ComeBackDate")) ? "" : rdr.GetString(rdr.GetOrdinal("ComeBackDate"));
                    model.TravelContact = rdr.IsDBNull(rdr.GetOrdinal("TravelContact")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelContact"));
                    model.TravelTel = rdr.IsDBNull(rdr.GetOrdinal("TravelTel")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelTel"));
                    model.VisitorContact = rdr.IsDBNull(rdr.GetOrdinal("VisitorContact")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorContact"));
                    model.VisitorTel = rdr.IsDBNull(rdr.GetOrdinal("VisitorTel")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorTel"));
                    model.ScheduleNum = rdr.IsDBNull(rdr.GetOrdinal("ScheduleNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ScheduleNum"));
                    model.AdultNum = rdr.IsDBNull(rdr.GetOrdinal("AdultNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("AdultNum"));
                    model.ChildrenNum = rdr.IsDBNull(rdr.GetOrdinal("ChildrenNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("ChildrenNum"));
                    model.SingleRoomNum = rdr.IsDBNull(rdr.GetOrdinal("SingleRoomNum")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("SingleRoomNum"));
                    model.VisitorNotes = rdr.IsDBNull(rdr.GetOrdinal("VisitorNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("VisitorNotes"));
                    model.TravelNotes = rdr.IsDBNull(rdr.GetOrdinal("TravelNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelNotes"));
                    model.BusinessNotes = rdr.IsDBNull(rdr.GetOrdinal("BusinessNotes")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessNotes"));
                    model.IssueTime = rdr.IsDBNull(rdr.GetOrdinal("IssueTime")) ? DateTime.MinValue : rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    model.OrderStatus = (TourOrderStatus)rdr.GetByte(rdr.GetOrdinal("OrderStatus"));
                    model.TourStatus = (TourStatus)rdr.GetByte(rdr.GetOrdinal("TourStatus"));
                    model.OperatorId = rdr.IsDBNull(rdr.GetOrdinal("OperatorId")) ? "" : rdr.GetString(rdr.GetOrdinal("OperatorId"));
                    model.TourPrice = rdr.IsDBNull(rdr.GetOrdinal("TourPrice")) ? 0 : rdr.GetDecimal(rdr.GetOrdinal("TourPrice"));
                    model.Travel = rdr.IsDBNull(rdr.GetOrdinal("Travel")) ? "" : rdr.GetString(rdr.GetOrdinal("Travel"));
                    model.DayNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "DayNum"));
                    model.LateNum = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? 0 : Utility.GetInt(GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "LateNum"));
                    model.StartCityName = rdr.IsDBNull(rdr.GetOrdinal("RouteInfo")) ? "" : GetValueByXml(rdr.GetString(rdr.GetOrdinal("RouteInfo")), "StartCityName");
                    model.BusinessName = rdr.IsDBNull(rdr.GetOrdinal("BusinessName")) ? "" : rdr.GetString(rdr.GetOrdinal("BusinessName"));
                    model.TravelName = rdr.IsDBNull(rdr.GetOrdinal("TravelName")) ? "" : rdr.GetString(rdr.GetOrdinal("TravelName"));
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 团队订单状态更改
        /// </summary>
        /// <param name="Status">更改的状态</param>
        /// <param name="TourId"></param>
        /// <returns></returns>
        public bool TourOrderStatusChange(TourOrderStatus Status, string TourId)
        {
            string sql = "update tbl_NewTourList set OrderStatus=@OrderStatus where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "OrderStatus", DbType.Byte, (int)Status);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, TourId);
            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 运营前台 单团订单 专线或地接修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        public bool OrderModifyZXDJ(MTourList model)
        {
            string sql = "update tbl_NewTourList set ScheduleNum=@ScheduleNum,BusinessNotes=@BusinessNotes where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "ScheduleNum", DbType.Int32, model.ScheduleNum);
            this._db.AddInParameter(cmd, "BusinessNotes", DbType.String, model.BusinessNotes);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 运营前台 单团订单 组团修改
        /// </summary>
        /// <param name="model">修改实体</param>
        /// <returns></returns>
        public bool OrderModifyZT(MTourList model)
        {
            string sql = "update tbl_NewTourList set TravelContact=@TravelContact,TravelTel=@TravelTel,VisitorContact=@VisitorContact,VisitorTel=@VisitorTel,ScheduleNum=@ScheduleNum,TravelNotes=@TravelNotes where TourId=@TourId";
            DbCommand cmd = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(cmd, "ScheduleNum", DbType.Int32, model.ScheduleNum);
            this._db.AddInParameter(cmd, "TravelContact", DbType.String, model.TravelContact);
            this._db.AddInParameter(cmd, "TravelTel", DbType.String, model.TravelTel);
            this._db.AddInParameter(cmd, "VisitorContact", DbType.String, model.VisitorContact);
            this._db.AddInParameter(cmd, "VisitorTel", DbType.String, model.VisitorTel);
            this._db.AddInParameter(cmd, "TravelNotes", DbType.String, model.TravelNotes);
            this._db.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, model.TourId);
            return DbHelper.ExecuteSql(cmd, this._db) == 1 ? true : false;
        }

        /// <summary>
        /// 后台首页订单统计
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">类型(1：专线 2：地接 3：组团社)</param>
        /// <returns></returns>
        public virtual Dictionary<string, int> GetStatisticsCount(string companyId, int type)
        {
            string proc = string.Empty;
            switch (type)
            {
                case 1:
                    proc = "proc_OrderBusinessStatisticsCount";
                    break;
                case 2:
                    proc = "proc_OrderGroundStatisticsCount";
                    break;
                case 3:
                    proc = "proc_OrderTravelStatisticsCount";
                    break;
                default:
                    break;
            }
            DbCommand comm = this._db.GetStoredProcCommand(proc);
            this._db.AddInParameter(comm, "@companyId", DbType.AnsiStringFixedLength, companyId);

            Dictionary<string, int> dic = new Dictionary<string, int>();
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                switch (type)
                {
                    case 1:
                        #region 专线
                        if (reader.Read())
                            dic.Add("有效散客订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单未处理", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客预留待付款", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单已确认", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队订单未处理", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队订单已确认", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("历史订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic["历史订单"] += (int)reader[0];
                        if (reader.NextResult() && reader.Read())
                            dic.Add("成人数", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic["成人数"] += (int)reader[0];
                        if (reader.NextResult() && reader.Read())
                            dic.Add("儿童数", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic["儿童数"] += (int)reader[0];
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单已取消", (int) reader[0]);
                        #endregion
                        break;
                    case 2:
                        #region 地接
                        if (reader.Read())
                            dic.Add("有效团队订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队订单未处理", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队订单已处理", (int)reader[0]);
                        #endregion
                        break;
                    case 3:
                        #region 组团社
                        if (reader.Read())
                            dic.Add("未发团散客预订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单未处理", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单预留待付款", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("散客订单已确认", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("未发团团队预订单", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队未确认", (int)reader[0]);
                        if (reader.NextResult() && reader.Read())
                            dic.Add("团队已确认", (int)reader[0]);
                        #endregion
                        break;
                    default:
                        break;
                }
            }
            return dic;
        }
        /// <summary>
        /// 获取XML文档属性值
        /// </summary>
        /// <param name="xml"></param>
        ///  <param name="attribute">属性</param>
        /// <returns></returns>
        private string GetValueByXml(string xml, string attribute)
        {
            if (string.IsNullOrEmpty(xml)) return "";
            System.Xml.Linq.XElement xRoot = System.Xml.Linq.XElement.Parse(xml);
            var xRows = EyouSoft.Common.Utility.GetXElements(xRoot, "row");
            foreach (var xRow in xRows)
            {
                return EyouSoft.Common.Utility.GetXAttributeValue(xRow, attribute);
            }
            return "";
        }

        #endregion
    }
}
