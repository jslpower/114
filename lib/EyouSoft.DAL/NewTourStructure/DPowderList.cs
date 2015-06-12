using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.CompanyStructure;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;
using System.Collections;

namespace EyouSoft.DAL.NewTourStructure
{
    /// <summary>
    /// 散拼计划
    /// </summary>
    public class DPowderList : DALBase, EyouSoft.IDAL.NewTourStructure.IPowderList
    {
        #region
        const string SQL_UpdatePowerList = "UPDATE tbl_NewPowderList SET RegistrationEndDate = '{0}',TourNum = {1},MoreThan = {2},SaveNum = {3},TourStatus = {4},RetailAdultPrice = {5},SettlementAudltPrice={6},RetailChildrenPrice={7},SettlementChildrenPrice={8},MarketPrice={9} WHERE TourId = '{10}';";
        private Database _db = null;
        /// <summary>
        /// 
        /// </summary>
        public DPowderList()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 增,删,改
        /// <summary>
        /// 添加散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        public virtual int AddPowder(MPowderList item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_NewPowderList_Add");
            #region 基本信息
            this._db.AddInParameter(comm, "@TourId", DbType.AnsiStringFixedLength, item.TourId);
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(comm, "@RouteName", DbType.String, item.RouteName);
            this._db.AddInParameter(comm, "@AreaId", DbType.Int32, item.AreaId);
            this._db.AddInParameter(comm, "@LeaveDate", DbType.DateTime, item.LeaveDate);
            this._db.AddInParameter(comm, "@ComeBackDate", DbType.DateTime, item.ComeBackDate);
            this._db.AddInParameter(comm, "@RegistrationEndDate", DbType.DateTime, item.RegistrationEndDate);
            this._db.AddInParameter(comm, "@TourNum", DbType.Int32, item.TourNum);
            this._db.AddInParameter(comm, "@SaveNum", DbType.Int32, item.SaveNum);
            this._db.AddInParameter(comm, "@MoreThan", DbType.Int32, item.MoreThan);
            this._db.AddInParameter(comm, "@TourStatus", DbType.Byte, (int)item.PowderTourStatus);
            this._db.AddInParameter(comm, "@RetailAdultPrice", DbType.Currency, item.RetailAdultPrice);
            this._db.AddInParameter(comm, "@SettlementAudltPrice", DbType.Currency, item.SettlementAudltPrice);
            this._db.AddInParameter(comm, "@RetailChildrenPrice", DbType.Currency, item.RetailChildrenPrice);
            this._db.AddInParameter(comm, "@SettlementChildrenPrice", DbType.Currency, item.SettlementChildrenPrice);
            this._db.AddInParameter(comm, "@MarketPrice", DbType.Currency, item.MarketPrice);
            this._db.AddInParameter(comm, "@SetDec", DbType.String, item.SetDec);
            this._db.AddInParameter(comm, "@TeamLeaderDec", DbType.String, item.TeamLeaderDec);
            this._db.AddInParameter(comm, "@TourNotes", DbType.String, item.TourNotes);
            this._db.AddInParameter(comm, "@IsLimit", DbType.AnsiStringFixedLength, Utility.GetBoolToString(item.IsLimit));
            this._db.AddInParameter(comm, "@TourType", DbType.Byte, (int)item.RecommendType);
            this._db.AddInParameter(comm, "@Publishers", DbType.AnsiStringFixedLength, item.Publishers);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@StandardStroke", DbType.String, item.StandardStroke);
            this._db.AddInParameter(comm, "@FitQuotation", DbType.String, item.FitQuotation);
            this._db.AddInParameter(comm, "@StartTraffic", DbType.Byte, (int)item.StartTraffic);
            this._db.AddInParameter(comm, "@EndTraffic", DbType.Byte, (int)item.EndTraffic);
            this._db.AddInParameter(comm, "@Characteristic", DbType.String, item.Characteristic);
            this._db.AddInParameter(comm, "@Day", DbType.Int32, item.Day);
            this._db.AddInParameter(comm, "@Late", DbType.Int32, item.Late);
            this._db.AddInParameter(comm, "@VendorsNotes", DbType.String, item.VendorsNotes);
            this._db.AddInParameter(comm, "@IP", DbType.AnsiStringFixedLength, item.IP);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, (int)item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, (int)item.B2COrder);
            this._db.AddInParameter(comm, "@StartCity", DbType.Int32, item.StartCity);
            this._db.AddInParameter(comm, "@StartCityName", DbType.String, item.StartCityName);
            this._db.AddInParameter(comm, "@EndCity", DbType.Int32, item.EndCity);
            this._db.AddInParameter(comm, "@EndCityName", DbType.String, item.EndCityName);
            this._db.AddInParameter(comm, "@StartDate", DbType.String, item.StartDate);
            this._db.AddInParameter(comm, "@EndDate", DbType.String, item.EndDate);
            _db.AddInParameter(comm, "IsNotVisa", DbType.AnsiString, Utility.GetBoolToString(item.IsNotVisa));

            #endregion
            #region 服务
            if (item.ServiceStandard == null) { item.ServiceStandard = new MServiceStandard(); }
            this._db.AddInParameter(comm, "@ResideContent", DbType.String, item.ServiceStandard.ResideContent);
            this._db.AddInParameter(comm, "@DinnerContent", DbType.String, item.ServiceStandard.DinnerContent);
            this._db.AddInParameter(comm, "@SightContent", DbType.String, item.ServiceStandard.SightContent);
            this._db.AddInParameter(comm, "@CarContent", DbType.String, item.ServiceStandard.CarContent);
            this._db.AddInParameter(comm, "@GuideContent", DbType.String, item.ServiceStandard.GuideContent);
            this._db.AddInParameter(comm, "@TrafficContent", DbType.String, item.ServiceStandard.TrafficContent);
            this._db.AddInParameter(comm, "@IncludeOtherContent", DbType.String, item.ServiceStandard.IncludeOtherContent);
            this._db.AddInParameter(comm, "@NotContainService", DbType.String, item.ServiceStandard.NotContainService);
            this._db.AddInParameter(comm, "@GiftInfo", DbType.String, item.ServiceStandard.GiftInfo);
            this._db.AddInParameter(comm, "@ChildrenInfo", DbType.String, item.ServiceStandard.ChildrenInfo);
            this._db.AddInParameter(comm, "@ShoppingInfo", DbType.String, item.ServiceStandard.ShoppingInfo);
            this._db.AddInParameter(comm, "@ExpenseItem", DbType.String, item.ServiceStandard.ExpenseItem);
            this._db.AddInParameter(comm, "@Notes", DbType.String, item.ServiceStandard.Notes);
            #endregion
            #region 关系
            this._db.AddInParameter(comm, "@TourThemeControlXml", DbType.Xml, DRoute.ThemeXmlStr(item.Themes));
            this._db.AddInParameter(comm, "@TourCityControlXml", DbType.Xml, DRoute.CityXmlStr(item.Citys));
            this._db.AddInParameter(comm, "@TourBrowseCountryControlXml", DbType.Xml, DRoute.BrowseCountryXmlStr(item.BrowseCountrys));
            this._db.AddInParameter(comm, "@TourBrowseCityControlXml", DbType.Xml, DRoute.BrowseCityXmlStr(item.BrowseCitys));
            this._db.AddInParameter(comm, "@TourStandardPlanXml", DbType.Xml, DRoute.StandardPlanXmlStr(item.StandardPlans));
            #endregion

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 修改散拼计划
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <returns></returns>
        public virtual int UpdatePowder(MPowderList item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_NewPowderList_Update");
            #region 基本信息
            this._db.AddInParameter(comm, "@TourId", DbType.AnsiStringFixedLength, item.TourId);
            this._db.AddInParameter(comm, "@RouteName", DbType.String, item.RouteName);
            this._db.AddInParameter(comm, "@AreaId", DbType.Int32, item.AreaId);
            this._db.AddInParameter(comm, "@LeaveDate", DbType.DateTime, item.LeaveDate);
            this._db.AddInParameter(comm, "@ComeBackDate", DbType.DateTime, item.ComeBackDate);
            this._db.AddInParameter(comm, "@RegistrationEndDate", DbType.DateTime, item.RegistrationEndDate);
            this._db.AddInParameter(comm, "@TourNum", DbType.Int32, item.TourNum);
            this._db.AddInParameter(comm, "@SaveNum", DbType.Int32, item.SaveNum);
            this._db.AddInParameter(comm, "@MoreThan", DbType.Int32, item.MoreThan);
            this._db.AddInParameter(comm, "@TourStatus", DbType.Byte, (int)item.PowderTourStatus);
            this._db.AddInParameter(comm, "@RetailAdultPrice", DbType.Currency, item.RetailAdultPrice);
            this._db.AddInParameter(comm, "@SettlementAudltPrice", DbType.Currency, item.SettlementAudltPrice);
            this._db.AddInParameter(comm, "@RetailChildrenPrice", DbType.Currency, item.RetailChildrenPrice);
            this._db.AddInParameter(comm, "@SettlementChildrenPrice", DbType.Currency, item.SettlementChildrenPrice);
            this._db.AddInParameter(comm, "@MarketPrice", DbType.Currency, item.MarketPrice);
            this._db.AddInParameter(comm, "@SetDec", DbType.String, item.SetDec);
            this._db.AddInParameter(comm, "@TeamLeaderDec", DbType.String, item.TeamLeaderDec);
            this._db.AddInParameter(comm, "@TourNotes", DbType.String, item.TourNotes);
            this._db.AddInParameter(comm, "@IsLimit", DbType.AnsiStringFixedLength, Utility.GetBoolToString(item.IsLimit));
            this._db.AddInParameter(comm, "@TourType", DbType.Byte, (int)item.RecommendType);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@StandardStroke", DbType.String, item.StandardStroke);
            this._db.AddInParameter(comm, "@FitQuotation", DbType.String, item.FitQuotation);
            this._db.AddInParameter(comm, "@StartTraffic", DbType.Byte, (int)item.StartTraffic);
            this._db.AddInParameter(comm, "@EndTraffic", DbType.Byte, (int)item.EndTraffic);
            this._db.AddInParameter(comm, "@Characteristic", DbType.String, item.Characteristic);
            this._db.AddInParameter(comm, "@Day", DbType.Int32, item.Day);
            this._db.AddInParameter(comm, "@Late", DbType.Int32, item.Late);
            this._db.AddInParameter(comm, "@VendorsNotes", DbType.String, item.VendorsNotes);
            this._db.AddInParameter(comm, "@IP", DbType.AnsiStringFixedLength, item.IP);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, (int)item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, (int)item.B2COrder);
            this._db.AddInParameter(comm, "@StartCity", DbType.Int32, item.StartCity);
            this._db.AddInParameter(comm, "@StartCityName", DbType.String, item.StartCityName);
            this._db.AddInParameter(comm, "@EndCity", DbType.Int32, item.EndCity);
            this._db.AddInParameter(comm, "@EndCityName", DbType.String, item.EndCityName);
            this._db.AddInParameter(comm, "@StartDate", DbType.String, item.StartDate);
            this._db.AddInParameter(comm, "@EndDate", DbType.String, item.EndDate);
            this._db.AddInParameter(comm, "@AdultPrice", DbType.Currency, item.AdultPrice);
            this._db.AddInParameter(comm, "@ChildrenPrice", DbType.Currency, item.ChildrenPrice);
            _db.AddInParameter(comm, "IsNotVisa", DbType.AnsiString, Utility.GetBoolToString(item.IsNotVisa));
            #endregion
            #region 服务
            if (item.ServiceStandard == null) { item.ServiceStandard = new MServiceStandard(); }
            this._db.AddInParameter(comm, "@ResideContent", DbType.String, item.ServiceStandard.ResideContent);
            this._db.AddInParameter(comm, "@DinnerContent", DbType.String, item.ServiceStandard.DinnerContent);
            this._db.AddInParameter(comm, "@SightContent", DbType.String, item.ServiceStandard.SightContent);
            this._db.AddInParameter(comm, "@CarContent", DbType.String, item.ServiceStandard.CarContent);
            this._db.AddInParameter(comm, "@GuideContent", DbType.String, item.ServiceStandard.GuideContent);
            this._db.AddInParameter(comm, "@TrafficContent", DbType.String, item.ServiceStandard.TrafficContent);
            this._db.AddInParameter(comm, "@IncludeOtherContent", DbType.String, item.ServiceStandard.IncludeOtherContent);
            this._db.AddInParameter(comm, "@NotContainService", DbType.String, item.ServiceStandard.NotContainService);
            this._db.AddInParameter(comm, "@GiftInfo", DbType.String, item.ServiceStandard.GiftInfo);
            this._db.AddInParameter(comm, "@ChildrenInfo", DbType.String, item.ServiceStandard.ChildrenInfo);
            this._db.AddInParameter(comm, "@ShoppingInfo", DbType.String, item.ServiceStandard.ShoppingInfo);
            this._db.AddInParameter(comm, "@ExpenseItem", DbType.String, item.ServiceStandard.ExpenseItem);
            this._db.AddInParameter(comm, "@Notes", DbType.String, item.ServiceStandard.Notes);
            #endregion
            #region 关系
            this._db.AddInParameter(comm, "@TourThemeControlXml", DbType.Xml, DRoute.ThemeXmlStr(item.Themes));
            this._db.AddInParameter(comm, "@TourCityControlXml", DbType.Xml, DRoute.CityXmlStr(item.Citys));
            this._db.AddInParameter(comm, "@TourBrowseCountryControlXml", DbType.Xml, DRoute.BrowseCountryXmlStr(item.BrowseCountrys));
            this._db.AddInParameter(comm, "@TourBrowseCityControlXml", DbType.Xml, DRoute.BrowseCityXmlStr(item.BrowseCitys));
            this._db.AddInParameter(comm, "@TourStandardPlanXml", DbType.Xml, DRoute.StandardPlanXmlStr(item.StandardPlans));
            #endregion

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 删除散拼计划
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public virtual int DeletePowder(string tourId)
        {
            //没有订单的时候才可以删除
            string sql = string.Format("UPDATE tbl_NewPowderList SET IsDeleted = '1' WHERE OrderPeopleNum = 0 AND CHARINDEX(','+TourId+',',',{0},') > 0", tourId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@TourId", DbType.AnsiStringFixedLength, tourId);

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 批量修改散拼团状态
        /// </summary>
        /// <param name="status">散拼团状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual int UpdateStatus(PowderTourStatus status, string tourId)
        {
            string sql = string.Format("UPDATE tbl_NewPowderList SET TourStatus = {0} WHERE CHARINDEX(','+TourId+',',',{1},') > 0", (int)status, tourId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 批量修改散拼团推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual int UpdatePowderRecommend(RecommendType type, string tourId)
        {
            string sql = string.Format("UPDATE tbl_NewPowderList SET TourType = {0} WHERE CHARINDEX(','+TourId+',',',{1},') > 0", (int)type, tourId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 批量修改散拼计划
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual int UpdatePowerList(IList<MPowderList> list)
        {

            StringBuilder sql = new StringBuilder();
            foreach (MPowderList item in list)
            {
                sql.AppendFormat(SQL_UpdatePowerList, new object[] { 
                    item.RegistrationEndDate,
                    item.TourNum,
                    item.MoreThan,
                    item.SaveNum,
                    (int)item.PowderTourStatus,
                    item.RetailAdultPrice,
                    item.SettlementAudltPrice,
                    item.RetailChildrenPrice,
                    item.SettlementChildrenPrice,
                    item.MarketPrice,
                    item.TourId
                });
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            return DbHelper.ExecuteSql(comm, this._db);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="leaveDates">出团日期</param>
        /// <param name="registrationDate">报名截止日期</param>
        /// <returns></returns>
        public virtual int AddBatchPowder(MPowderList item, DateTime[] leaveDates, DateTime[] registrationDate)
        {
            StringBuilder leave = new StringBuilder();
            if (leaveDates.Length > 0)
            {
                leave.Append("<item>");
                for (int i = 0; i < leaveDates.Length; i++)
                {
                    leave.AppendFormat("<powder tourid=\"{0}\" leavedate=\"{1}\" regdate=\"{2}\">{3}</powder>", new object[] { Guid.NewGuid().ToString(), leaveDates[i], registrationDate[i], i + 1 });
                }
                leave.Append("</item>");
            }
            DbCommand comm = this._db.GetStoredProcCommand("proc_NewPowderList_Batch_Add");
            #region 基本信息
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(comm, "@TourNum", DbType.Int32, item.TourNum);
            this._db.AddInParameter(comm, "@MoreThan", DbType.Int32, item.MoreThan);
            this._db.AddInParameter(comm, "@TourStatus", DbType.Byte, (int)PowderTourStatus.收客);
            this._db.AddInParameter(comm, "@RetailAdultPrice", DbType.Currency, item.RetailAdultPrice);
            this._db.AddInParameter(comm, "@SettlementAudltPrice", DbType.Currency, item.SettlementAudltPrice);
            this._db.AddInParameter(comm, "@RetailChildrenPrice", DbType.Currency, item.RetailChildrenPrice);
            this._db.AddInParameter(comm, "@SettlementChildrenPrice", DbType.Currency, item.SettlementChildrenPrice);
            this._db.AddInParameter(comm, "@MarketPrice", DbType.Currency, item.MarketPrice);
            this._db.AddInParameter(comm, "@SetDec", DbType.String, item.SetDec);
            this._db.AddInParameter(comm, "@TeamLeaderDec", DbType.String, item.TeamLeaderDec);
            this._db.AddInParameter(comm, "@TourNotes", DbType.String, item.TourNotes);
            this._db.AddInParameter(comm, "@IsLimit", DbType.AnsiStringFixedLength, Utility.GetBoolToString(item.IsLimit));
            this._db.AddInParameter(comm, "@Publishers", DbType.AnsiStringFixedLength, item.Publishers);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@IP", DbType.AnsiStringFixedLength, item.IP);
            this._db.AddInParameter(comm, "@StartDate", DbType.String, item.StartDate);
            this._db.AddInParameter(comm, "@EndDate", DbType.String, item.EndDate);
            this._db.AddInParameter(comm, "@LeaveDate", DbType.Xml, leave.ToString());
            #endregion

            return DbHelper.ExecuteSqlTrans(comm, this._db);
        }
        /// <summary>
        /// 批量修改行程
        /// </summary>
        /// <param name="item">散拼实体</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual int UpdateStandardPlan(MPowderList item, params string[] tourId)
        {
            StringBuilder tour = new StringBuilder();
            tour.Append("<item>");
            foreach (string t in tourId)
            {
                tour.AppendFormat("<tourid>{0}</tourid>", t);
            }
            tour.Append("</item>");

            DbCommand comm = this._db.GetStoredProcCommand("proc_Powder_StandardPlan_Update");
            #region 基本信息
            this._db.AddInParameter(comm, "@RouteName", DbType.String, item.RouteName);
            this._db.AddInParameter(comm, "@TourNotes", DbType.String, item.TourNotes);
            this._db.AddInParameter(comm, "@TourType", DbType.Byte, (int)item.RecommendType);
            this._db.AddInParameter(comm, "@StandardStroke", DbType.String, item.StandardStroke);
            this._db.AddInParameter(comm, "@FitQuotation", DbType.String, item.FitQuotation);
            this._db.AddInParameter(comm, "@StartTraffic", DbType.Byte, (int)item.StartTraffic);
            this._db.AddInParameter(comm, "@EndTraffic", DbType.Byte, (int)item.EndTraffic);
            this._db.AddInParameter(comm, "@Characteristic", DbType.String, item.Characteristic);
            this._db.AddInParameter(comm, "@Day", DbType.Int32, item.Day);
            this._db.AddInParameter(comm, "@Late", DbType.Int32, item.Late);
            this._db.AddInParameter(comm, "@VendorsNotes", DbType.String, item.VendorsNotes);
            this._db.AddInParameter(comm, "@StartCity", DbType.Int32, item.StartCity);
            this._db.AddInParameter(comm, "@StartCityName", DbType.String, item.StartCityName);
            this._db.AddInParameter(comm, "@EndCity", DbType.Int32, item.EndCity);
            this._db.AddInParameter(comm, "@EndCityName", DbType.String, item.EndCityName);
            this._db.AddInParameter(comm, "@AdultPrice", DbType.Currency, item.AdultPrice);
            this._db.AddInParameter(comm, "@ChildrenPrice", DbType.Currency, item.ChildrenPrice);
            #endregion
            #region 服务
            if (item.ServiceStandard == null) { item.ServiceStandard = new MServiceStandard(); }
            this._db.AddInParameter(comm, "@ResideContent", DbType.String, item.ServiceStandard.ResideContent);
            this._db.AddInParameter(comm, "@DinnerContent", DbType.String, item.ServiceStandard.DinnerContent);
            this._db.AddInParameter(comm, "@SightContent", DbType.String, item.ServiceStandard.SightContent);
            this._db.AddInParameter(comm, "@CarContent", DbType.String, item.ServiceStandard.CarContent);
            this._db.AddInParameter(comm, "@GuideContent", DbType.String, item.ServiceStandard.GuideContent);
            this._db.AddInParameter(comm, "@TrafficContent", DbType.String, item.ServiceStandard.TrafficContent);
            this._db.AddInParameter(comm, "@IncludeOtherContent", DbType.String, item.ServiceStandard.IncludeOtherContent);
            this._db.AddInParameter(comm, "@NotContainService", DbType.String, item.ServiceStandard.NotContainService);
            this._db.AddInParameter(comm, "@GiftInfo", DbType.String, item.ServiceStandard.GiftInfo);
            this._db.AddInParameter(comm, "@ChildrenInfo", DbType.String, item.ServiceStandard.ChildrenInfo);
            this._db.AddInParameter(comm, "@ShoppingInfo", DbType.String, item.ServiceStandard.ShoppingInfo);
            this._db.AddInParameter(comm, "@ExpenseItem", DbType.String, item.ServiceStandard.ExpenseItem);
            this._db.AddInParameter(comm, "@Notes", DbType.String, item.ServiceStandard.Notes);
            #endregion
            #region 关系
            this._db.AddInParameter(comm, "@TourThemeControlXml", DbType.Xml, DRoute.ThemeXmlStr(item.Themes));
            this._db.AddInParameter(comm, "@TourCityControlXml", DbType.Xml, DRoute.CityXmlStr(item.Citys));
            this._db.AddInParameter(comm, "@TourBrowseCountryControlXml", DbType.Xml, DRoute.BrowseCountryXmlStr(item.BrowseCountrys));
            this._db.AddInParameter(comm, "@TourBrowseCityControlXml", DbType.Xml, DRoute.BrowseCityXmlStr(item.BrowseCitys));
            this._db.AddInParameter(comm, "@TourStandardPlanXml", DbType.Xml, DRoute.StandardPlanXmlStr(item.StandardPlans));
            #endregion
            //散拼团队编号
            this._db.AddInParameter(comm, "@tour", DbType.Xml, tour.ToString());
            //是否免签
            _db.AddInParameter(comm, "IsNotVisa", DbType.AnsiString, Utility.GetBoolToString(item.IsNotVisa));

            return DbHelper.ExecuteSql(comm, this._db);
        }
        #endregion

        #region 查询
        /// <summary>
        /// 线路区域对应的散拼计划数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetTourByAreaStats(string companyId, string userAreas)
        {
            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = new List<EyouSoft.Model.TourStructure.AreaStatInfo>();

            StringBuilder cmdText = new StringBuilder();
            cmdText.Append("select AreaId,count(*) as PowderNumber from tbl_NewRouteBasicInfo");
            cmdText.AppendFormat(" where B2B <> {0} and IsDeleted = '0' and Publishers = '{1}' and RouteSource = {2} ", (int)RouteB2BDisplay.隐藏, companyId, (int)RouteSource.专线商添加);
            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" and AreaId in ({0})", userAreas);
            }
            cmdText.Append(" group by AreaId");
            DbCommand comm = this._db.GetSqlStringCommand(cmdText.ToString());
            using (IDataReader rdr = DbHelper.ExecuteReader(comm, this._db))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TourStructure.AreaStatInfo(rdr.GetInt32(0), "", rdr.GetInt32(1)));
                }
            }

            return stats;
        }
        /// <summary>
        /// 到期时期还有1周
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetExpirePowder(string companyId)
        {
            string sql = string.Format("select count(TourId) from tbl_NewPowderList where IsDeleted = '0' and LeaveDate <= dateadd(day,7,getdate) and Publishers = '{0} ", companyId);

            DbCommand comm = this._db.GetSqlStringCommand(sql);

            return (int)DbHelper.GetSingle(comm, this._db);
        }
        /// <summary>
        /// 散拼计划行程单,出团通知书
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>行程单实体</returns>
        public virtual MPowderPrint GetModelPrint(string tourId)
        {
            string sql = string.Format("select * from view_PowderList_Select_Print where TourId  = '{0}'", tourId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    string xmlStr = string.Empty;
                    MPowderPrint item = new MPowderPrint();
                    item.TourNo = reader["TourNo"].ToString();
                    item.RouteName = reader["RouteName"].ToString();
                    item.StartTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["StartTraffic"].ToString());
                    item.EndTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["EndTraffic"].ToString());
                    item.Characteristic = reader.IsDBNull(reader.GetOrdinal("Characteristic")) ? string.Empty : reader["Characteristic"].ToString();
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.MarketPrice = (decimal)reader["MarketPrice"];
                    item.AdultPrice = (decimal)reader["AdultPrice"];
                    item.ChildrenPrice = (decimal)reader["ChildrenPrice"];
                    item.Day = (int)reader["Day"];
                    item.Late = (int)reader["Late"];
                    item.SetDec = reader.IsDBNull(reader.GetOrdinal("SetDec")) ? string.Empty : reader["SetDec"].ToString();
                    item.TeamLeaderDec = reader.IsDBNull(reader.GetOrdinal("TeamLeaderDec")) ? string.Empty : reader["TeamLeaderDec"].ToString();
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.ComeBackDate = reader.IsDBNull(reader.GetOrdinal("ComeBackDate")) ? DateTime.MinValue : DateTime.Parse(reader["ComeBackDate"].ToString());
                    item.StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? string.Empty : reader["StartDate"].ToString();
                    item.EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? string.Empty : reader["EndDate"].ToString();
                    item.RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString());
                    item.CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString();
                    item.CompanyAddress = reader.IsDBNull(reader.GetOrdinal("CompanyAddress")) ? string.Empty : reader["CompanyAddress"].ToString();
                    item.CompanyContact = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString();
                    item.Tel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString();
                    item.Mobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString();
                    item.Fax = reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? string.Empty : reader["ContactFax"].ToString();
                    item.AlipayAccount = reader.IsDBNull(reader.GetOrdinal("AlipayAccount")) ? string.Empty : reader["AlipayAccount"].ToString();
                    item.CompanyLogo = reader.IsDBNull(reader.GetOrdinal("CompanyLogo")) ? string.Empty : reader["CompanyLogo"].ToString();
                    item.FitQuotation = reader.IsDBNull(reader.GetOrdinal("FitQuotation")) ? string.Empty : reader["FitQuotation"].ToString();
                    item.GroupNum = (int)reader["GroupNum"];
                    item.ReferencePrice = (decimal)reader["IndependentGroupPrice"];
                    item.BrowseCity = reader.IsDBNull(reader.GetOrdinal("Brower")) ? string.Empty : reader["Brower"].ToString();
                    item.VisaCity = reader.IsDBNull(reader.GetOrdinal("IsVisa")) ? string.Empty : reader["IsVisa"].ToString();
                    item.FastPlan = reader.IsDBNull(reader.GetOrdinal("StandardStroke")) ? string.Empty : reader["StandardStroke"].ToString();

                    #region 公司帐号
                    if (!reader.IsDBNull(reader.GetOrdinal("Account")))
                    {
                        xmlStr = reader["Account"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRow = Utility.GetXElements(xRoot, "row");
                        if (xRow != null && xRow.Any())
                        {
                            foreach (var t in xRow)
                            {
                                //个人帐号
                                if (Utility.GetXAttributeValue(t, "TypeId").Equals(((int)Model.CompanyStructure.BankAccountType.个人).ToString()))
                                {
                                    item.PersonalAccount = new ArrayList();
                                    item.PersonalAccount.Add(Utility.GetXAttributeValue(t, "BankName") + ":" + Utility.GetXAttributeValue(t, "AccountNumber"));
                                }
                                else
                                {
                                    item.CompanyAccount = Utility.GetXAttributeValue(t, "BankName") + ":" + Utility.GetXAttributeValue(t, "AccountNumber");
                                }
                            }
                        }
                    }
                    #endregion
                    #region 行程
                    if (!reader.IsDBNull(reader.GetOrdinal("StandardPlan")))
                    {
                        xmlStr = reader["StandardPlan"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.StandardPlan = new List<MStandardPlan>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.StandardPlan.Add(new MStandardPlan()
                                {
                                    Vehicle = (TrafficType)Enum.Parse(typeof(TrafficType), Utility.GetXAttributeValue(t, "Vehicle")),
                                    PlanDay = int.Parse(Utility.GetXAttributeValue(t, "PlanDay")),
                                    PlanInterval = Utility.GetXAttributeValue(t, "PlanInterval"),
                                    House = Utility.GetXAttributeValue(t, "House"),
                                    Early = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Early")),
                                    Center = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Center")),
                                    Late = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Late")),
                                    PlanContent = Utility.GetXAttributeValue(t, "PlanContent")
                                });
                            }
                        }
                    }
                    #endregion
                    #region 服务标准
                    item.ServiceStandard = new MServiceStandard();
                    item.ServiceStandard.IncludeOtherContent = reader.IsDBNull(reader.GetOrdinal("IncludeOtherContent")) ? string.Empty : reader["IncludeOtherContent"].ToString();
                    item.ServiceStandard.NotContainService = reader.IsDBNull(reader.GetOrdinal("NotContainService")) ? string.Empty : reader["NotContainService"].ToString();
                    item.ServiceStandard.GiftInfo = reader.IsDBNull(reader.GetOrdinal("GiftInfo")) ? string.Empty : reader["GiftInfo"].ToString();
                    item.ServiceStandard.ChildrenInfo = reader.IsDBNull(reader.GetOrdinal("ChildrenInfo")) ? string.Empty : reader["ChildrenInfo"].ToString();
                    item.ServiceStandard.ShoppingInfo = reader.IsDBNull(reader.GetOrdinal("ShoppingInfo")) ? string.Empty : reader["ShoppingInfo"].ToString();
                    item.ServiceStandard.ExpenseItem = reader.IsDBNull(reader.GetOrdinal("ExpenseItem")) ? string.Empty : reader["ExpenseItem"].ToString();
                    item.ServiceStandard.Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader["Notes"].ToString();
                    item.ServiceStandard.CarContent = reader.IsDBNull(reader.GetOrdinal("CarContent")) ? string.Empty : reader["CarContent"].ToString();
                    item.ServiceStandard.DinnerContent = reader.IsDBNull(reader.GetOrdinal("DinnerContent")) ? string.Empty : reader["DinnerContent"].ToString();
                    item.ServiceStandard.GuideContent = reader.IsDBNull(reader.GetOrdinal("GuideContent")) ? string.Empty : reader["GuideContent"].ToString();
                    item.ServiceStandard.ResideContent = reader.IsDBNull(reader.GetOrdinal("ResideContent")) ? string.Empty : reader["ResideContent"].ToString();
                    item.ServiceStandard.SightContent = reader.IsDBNull(reader.GetOrdinal("SightContent")) ? string.Empty : reader["SightContent"].ToString();
                    item.ServiceStandard.TrafficContent = reader.IsDBNull(reader.GetOrdinal("TrafficContent")) ? string.Empty : reader["TrafficContent"].ToString();

                    #endregion

                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取散拼计划实体
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>散拼计划实体</returns>
        public virtual MPowderList GetModel(string tourId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.*,b.*,e.RouteType,Theme=(select tb.themeid,b.FieldName from tbl_NewTourThemeControl tb ");
            sql.Append(" inner join tbl_SysField b on tb.ThemeId = b.FieldId and b.FieldType = 2 ");
            sql.Append(" where tb.TourId = a.TourId for xml raw,root('item')),");
            sql.Append(" Country=(select tb.countryid,tb.isvisa,b.CName from tbl_NewTourBrowseCountryControl tb");
            sql.Append(" inner join tbl_SysCountry b on tb.CountryId = b.Id ");
            sql.Append(" where tb.TourId = a.TourId for xml raw,root('item')),");
            sql.Append(" City=(select tb.cityid,tb.provinceid,b.CityName from tbl_NewTourCityControl tb");
            sql.Append(" inner join tbl_SysCity b on tb.CityId = b.Id");
            sql.Append(" where tb.TourId = a.TourId for xml raw,root('item')),");
            sql.Append(" BrowseCity=(select tb.cityid,b.CityName,tb.CountyId,c.DistrictName from tbl_NewTourBrowseCityControl tb");
            sql.Append(" left join tbl_SysCity b on tb.Cityid = b.Id left join tbl_sysDistrictCounty c on tb.CountyId = c.Id ");
            sql.Append(" where tb.TourId = a.TourId for xml raw,root('item')),c.CompanyName,d.AreaName,");
            sql.Append(" StandardPlan =(select tb.* from tbl_NewTourStandardPlan tb where tb.TourId = a.TourId for xml raw,root('item'))");
            sql.Append(" FROM tbl_NewPowderList a left join tbl_NewTourServiceStandard b on a.TourId = b.TourId");
            sql.Append(" left join tbl_CompanyInfo c on a.Publishers = C.Id");
            sql.Append(" left join tbl_SysArea d on a.AreaId = d.ID");
            sql.Append(" left join tbl_NewRouteBasicInfo e on a.RouteId = e.RouteId");
            sql.Append(" WHERE a.TourId = @TourId");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@TourId", DbType.AnsiStringFixedLength, tourId);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    MPowderList item = new MPowderList();

                    #region 基本信息
                    item.RouteType = (AreaType)Enum.Parse(typeof(AreaType), reader["RouteType"].ToString());
                    item.TourId = reader["TourId"].ToString();
                    item.TourNo = reader["TourNo"].ToString();
                    item.RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? string.Empty : reader["RouteId"].ToString();
                    item.AreaId = (int)reader["AreaId"];
                    item.AreaName = reader.IsDBNull(reader.GetOrdinal("AreaName")) ? string.Empty : reader["AreaName"].ToString();
                    item.RouteName = reader["RouteName"].ToString();
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.ComeBackDate = reader.IsDBNull(reader.GetOrdinal("ComeBackDate")) ? DateTime.MinValue : DateTime.Parse(reader["ComeBackDate"].ToString());
                    item.RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString());
                    item.TourNum = (int)reader["TourNum"];
                    item.SaveNum = (int)reader["SaveNum"];
                    item.MoreThan = (int)reader["MoreThan"];
                    item.PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString());
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.MarketPrice = (decimal)reader["MarketPrice"];
                    item.SetDec = reader.IsDBNull(reader.GetOrdinal("SetDec")) ? string.Empty : reader["SetDec"].ToString();
                    item.TeamLeaderDec = reader.IsDBNull(reader.GetOrdinal("TeamLeaderDec")) ? string.Empty : reader["TeamLeaderDec"].ToString();
                    item.TourNotes = reader.IsDBNull(reader.GetOrdinal("TourNotes")) ? string.Empty : reader["TourNotes"].ToString();
                    item.IsLimit = Utility.GetStringToBool(reader["IsLimit"].ToString());
                    item.IssueTime = DateTime.Parse(reader["IssueTime"].ToString());
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString());
                    item.B2B = (RouteB2BDisplay)Enum.Parse(typeof(RouteB2BDisplay), reader["B2B"].ToString());
                    item.B2BOrder = (int)reader["B2BOrder"];
                    item.B2C = (RouteB2CDisplay)Enum.Parse(typeof(RouteB2CDisplay), reader["B2C"].ToString());
                    item.B2COrder = (int)reader["B2COrder"];
                    item.Publishers = reader.IsDBNull(reader.GetOrdinal("Publishers")) ? string.Empty : reader["Publishers"].ToString();
                    item.PublishersName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString();
                    item.OperatorId = reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? string.Empty : reader["OperatorId"].ToString();
                    item.StartTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["StartTraffic"].ToString());
                    item.EndTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["EndTraffic"].ToString());
                    item.Characteristic = reader.IsDBNull(reader.GetOrdinal("Characteristic")) ? string.Empty : reader["Characteristic"].ToString();
                    item.Day = (int)reader["Day"];
                    item.Late = (int)reader["Late"];
                    item.VendorsNotes = reader.IsDBNull(reader.GetOrdinal("VendorsNotes")) ? string.Empty : reader["VendorsNotes"].ToString();
                    item.IP = reader.IsDBNull(reader.GetOrdinal("IP")) ? string.Empty : reader["IP"].ToString();
                    item.OrderPeopleNum = (int)reader["OrderPeopleNum"];
                    item.StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? string.Empty : reader["StartDate"].ToString();
                    item.EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? string.Empty : reader["EndDate"].ToString();
                    item.StartCity = (int)reader["StartCity"];
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.EndCity = (int)reader["EndCity"];
                    item.EndCityName = reader.IsDBNull(reader.GetOrdinal("EndCityName")) ? string.Empty : reader["EndCityName"].ToString();
                    item.AdultPrice = (decimal)reader["AdultPrice"];
                    item.ChildrenPrice = (decimal)reader["ChildrenPrice"];
                    item.IsNotVisa = reader.IsDBNull(reader.GetOrdinal("IsNotVisa"))
                                         ? false
                                         : Utility.GetStringToBool(reader.GetString(reader.GetOrdinal("IsNotVisa")));

                    #endregion

                    #region 其他信息
                    item.ServiceStandard = new MServiceStandard();
                    item.ServiceStandard.ResideContent = reader.IsDBNull(reader.GetOrdinal("ResideContent")) ? string.Empty : reader["ResideContent"].ToString();
                    item.ServiceStandard.DinnerContent = reader.IsDBNull(reader.GetOrdinal("DinnerContent")) ? string.Empty : reader["DinnerContent"].ToString();
                    item.ServiceStandard.SightContent = reader.IsDBNull(reader.GetOrdinal("SightContent")) ? string.Empty : reader["SightContent"].ToString();
                    item.ServiceStandard.CarContent = reader.IsDBNull(reader.GetOrdinal("CarContent")) ? string.Empty : reader["CarContent"].ToString();
                    item.ServiceStandard.GuideContent = reader.IsDBNull(reader.GetOrdinal("GuideContent")) ? string.Empty : reader["GuideContent"].ToString();
                    item.ServiceStandard.TrafficContent = reader.IsDBNull(reader.GetOrdinal("TrafficContent")) ? string.Empty : reader["TrafficContent"].ToString();
                    item.ServiceStandard.IncludeOtherContent = reader.IsDBNull(reader.GetOrdinal("IncludeOtherContent")) ? string.Empty : reader["IncludeOtherContent"].ToString();
                    item.ServiceStandard.NotContainService = reader.IsDBNull(reader.GetOrdinal("NotContainService")) ? string.Empty : reader["NotContainService"].ToString();
                    item.ServiceStandard.GiftInfo = reader.IsDBNull(reader.GetOrdinal("GiftInfo")) ? string.Empty : reader["GiftInfo"].ToString();
                    item.ServiceStandard.ChildrenInfo = reader.IsDBNull(reader.GetOrdinal("ChildrenInfo")) ? string.Empty : reader["ChildrenInfo"].ToString();
                    item.ServiceStandard.ShoppingInfo = reader.IsDBNull(reader.GetOrdinal("ShoppingInfo")) ? string.Empty : reader["ShoppingInfo"].ToString();
                    item.ServiceStandard.ExpenseItem = reader.IsDBNull(reader.GetOrdinal("ExpenseItem")) ? string.Empty : reader["ExpenseItem"].ToString();
                    item.ServiceStandard.Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? string.Empty : reader["Notes"].ToString();

                    #endregion
                    #region 关系

                    item.StandardStroke = reader.IsDBNull(reader.GetOrdinal("StandardStroke")) ? string.Empty : reader["StandardStroke"].ToString();
                    item.FitQuotation = reader.IsDBNull(reader.GetOrdinal("FitQuotation")) ? string.Empty : reader["FitQuotation"].ToString();
                    string xmlStr = string.Empty;
                    #region 主题

                    if (!reader.IsDBNull(reader.GetOrdinal("Theme")))
                    {
                        xmlStr = reader["Theme"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.Themes = new List<MThemeControl>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.Themes.Add(new MThemeControl()
                                {
                                    ThemeId = Utility.GetXAttributeValue(t, "themeid"),
                                    ThemeName = Utility.GetXAttributeValue(t, "FieldName"),
                                    Id = item.RouteId
                                });
                            }
                        }
                    }
                    #endregion
                    #region 游览国家
                    if (!reader.IsDBNull(reader.GetOrdinal("Country")))
                    {
                        xmlStr = reader["Country"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.BrowseCountrys = new List<MBrowseCountryControl>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.BrowseCountrys.Add(new MBrowseCountryControl()
                                {
                                    Id = item.RouteId,
                                    CountryId = int.Parse(Utility.GetXAttributeValue(t, "countryid")),
                                    CountryName = Utility.GetXAttributeValue(t, "CName"),
                                    IsVisa = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "isvisa"))
                                });
                            }
                        }
                    }
                    #endregion
                    #region 销售城市
                    if (!reader.IsDBNull(reader.GetOrdinal("City")))
                    {
                        xmlStr = reader["City"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.Citys = new List<MCityControl>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.Citys.Add(new MCityControl()
                                {
                                    Id = item.RouteId,
                                    CityId = int.Parse(Utility.GetXAttributeValue(t, "cityid")),
                                    CityName = Utility.GetXAttributeValue(t, "CityName"),
                                    ProvinceId = int.Parse(Utility.GetXAttributeValue(t, "provinceid"))
                                });
                            }
                        }
                    }
                    #endregion
                    #region 游览城市
                    if (!reader.IsDBNull(reader.GetOrdinal("BrowseCity")))
                    {
                        xmlStr = reader["BrowseCity"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.BrowseCitys = new List<MBrowseCityControl>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.BrowseCitys.Add(new MBrowseCityControl()
                                {
                                    Id = item.RouteId,
                                    CityId = int.Parse(Utility.GetXAttributeValue(t, "cityid")),
                                    CityName = Utility.GetXAttributeValue(t, "CityName"),
                                    CountyId = Utility.GetInt(Utility.GetXAttributeValue(t, "CountyId")),
                                    CountyName = Utility.GetXAttributeValue(t, "DistrictName")
                                });
                            }
                        }

                    }
                    #endregion
                    #region 行程
                    if (!reader.IsDBNull(reader.GetOrdinal("StandardPlan")))
                    {
                        xmlStr = reader["StandardPlan"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.StandardPlans = new List<MStandardPlan>();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.StandardPlans.Add(new MStandardPlan()
                                {
                                    Id = item.RouteId,
                                    Vehicle = (TrafficType)Enum.Parse(typeof(TrafficType), Utility.GetXAttributeValue(t, "Vehicle")),
                                    PlanDay = int.Parse(Utility.GetXAttributeValue(t, "PlanDay")),
                                    PlanInterval = Utility.GetXAttributeValue(t, "PlanInterval"),
                                    House = Utility.GetXAttributeValue(t, "House"),
                                    Early = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Early")),
                                    Center = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Center")),
                                    Late = Utility.GetStringToBool(Utility.GetXAttributeValue(t, "Late")),
                                    PlanContent = Utility.GetXAttributeValue(t, "PlanContent")
                                });
                            }
                        }
                    }
                    #endregion

                    #endregion

                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取线路散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetList(string routeId)
        {
            IList<MPowderList> list = new List<MPowderList>();
            string sql = string.Format("SELECT TourId,TourNo,LeaveDate,RegistrationEndDate,IsLimit,TourNum,MoreThan,TourStatus,MarketPrice,RetailAdultPrice,RetailChildrenPrice FROM tbl_NewPowderList WHERE IsDeleted = '0' AND LeaveDate >= getdate() AND RouteId = '{0}'", routeId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        TourId = reader["TourId"].ToString(),
                        TourNo = reader["TourNo"].ToString(),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : Utility.GetDateTime(reader["LeaveDate"].ToString()),
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        MoreThan = (int)reader["MoreThan"],
                        MarketPrice = (decimal)reader["MarketPrice"],
                        IsLimit = Utility.GetStringToBool(reader["IsLimit"].ToString()),
                        RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString())
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取公司散拼计划
        /// </summary>
        /// <param name="top">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetList(int top, string companyId)
        {
            string sql = string.Format("SELECT TOP ({0}) TourId,TourType,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,RouteName,LeaveDate,MoreThan,MarketPrice FROM tbl_NewPowderList WHERE IsDeleted = '0' AND LeaveDate >= GETDATE() AND Publishers = '{1}'", top, companyId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        TourId = reader["TourId"].ToString(),
                        RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString()),
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"],
                        SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"],
                        RouteName = reader.IsDBNull(reader.GetOrdinal("RouteName")) ? string.Empty : reader["RouteName"].ToString(),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        MoreThan = (int)reader["MoreThan"],
                        MarketPrice = (decimal)reader["MarketPrice"]
                    });
                }
            }

            return list;
        }
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="tourNo">团号</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetList(string routeId, string tourNo, DateTime? startDate, DateTime? endDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select TourId,TourNo,LeaveDate,RegistrationEndDate,TourNum,SaveNum,MoreThan,TourStatus,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,MarketPrice,OrderPeopleNum from tbl_NewPowderList where IsDeleted = '0' AND LeaveDate >= getdate() AND  RouteId = '{0}'", routeId);
            if (startDate.HasValue)
            {
                sql.AppendFormat(" and LeaveDate >= '{0}'", startDate);
            }
            if (endDate.HasValue)
            {
                sql.AppendFormat(" and LeaveDate <= '{0}'", endDate);
            }
            if (!string.IsNullOrEmpty(tourNo))
            {
                sql.AppendFormat(" and TourNo like '%{0}%'", tourNo);
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MPowderList();

                    item.TourId = reader["TourId"].ToString();
                    item.TourNo = reader["TourNo"].ToString();
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString());
                    item.TourNum = (int)reader["TourNum"];
                    item.MoreThan = (int)reader["MoreThan"];
                    item.SaveNum = (int)reader["SaveNum"];
                    item.PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString());
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.MarketPrice = (decimal)reader["MarketPrice"];
                    item.OrderPeopleNum = (int)reader["OrderPeopleNum"];

                    list.Add(item);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号</param>
        /// <param name="tourNo">团号</param>
        /// <param name="endDate">开始出团时间</param>
        /// <param name="startDate">结束出团时间</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId, string tourNo,
            DateTime? startDate, DateTime? endDate)
        {
            StringBuilder query = new StringBuilder();
            string tableName = "tbl_NewPowderList";
            string fields = "TourId,TourNo,RouteName,TourType,LeaveDate,RegistrationEndDate,TourNum,SaveNum,MoreThan,TourStatus,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,MarketPrice,OrderPeopleNum";
            string primaryKey = "TourId";
            string orderBy = "LeaveDate DESC";
            query.AppendFormat(" RouteId = '{0}' and datediff(dd,getdate(),LeaveDate) >= 0 and IsDeleted = '0'", routeId);
            if (startDate.HasValue)
            {
                query.AppendFormat(" and LeaveDate >= '{0}'", startDate);
            }
            if (endDate.HasValue)
            {
                query.AppendFormat(" and LeaveDate <= '{0}'", endDate);
            }
            if (!string.IsNullOrEmpty(tourNo))
            {
                query.AppendFormat(" and TourNo like '%{0}%'", tourNo);
            }
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fields, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    item = new MPowderList();

                    item.TourId = reader["TourId"].ToString();
                    item.TourNo = reader["TourNo"].ToString();
                    item.RouteName = reader["RouteName"].ToString();
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString());
                    item.TourNum = (int)reader["TourNum"];
                    item.MoreThan = (int)reader["MoreThan"];
                    item.SaveNum = (int)reader["SaveNum"];
                    item.PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString());
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.MarketPrice = (decimal)reader["MarketPrice"];
                    item.OrderPeopleNum = (int)reader["OrderPeopleNum"];
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString());

                    list.Add(item);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取线路的散拼计划
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="routeId">线路编号</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, string routeId)
        {
            string tableName = "tbl_NewPowderList";
            string primaryKey = "TourId";
            string orderBy = "IssueTime DESC";
            string query = string.Format("IsDeleted = '0' AND RouteId = '{0}'", routeId);
            string fileds = "TourId,TourNo,LeaveDate,RegistrationEndDate,TourNum,MoreThan,RetailAdultPrice,RetailChildrenPrice,MarketPrice,TourStatus";
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        TourId = reader["TourId"].ToString(),
                        TourNo = reader["TourNo"].ToString(),
                        PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString()),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        MoreThan = (int)reader["MoreThan"],
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        MarketPrice = (decimal)reader["MarketPrice"]
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 分页获取散拼计划列表(用户后台-组团社,显示专线商销售区域包含 组团社销售区域的有效团队线路)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="AreaType">线路类型</param>
        /// <param name="cityId">商家所在城市</param>
        /// <param name="tourKeyType">关键字搜索类型</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            Model.SystemStructure.AreaType AreaType, int cityId, int tourKeyType, MPowderSearch search)
        {
            string tableName = "view_PowderList_Travel_Select";
            string query = string.Format(" RouteType = {0} AND cast(TourCity as xml).exist('/item/row[@CityId=\"{1}\"]') = 1 {2}", (int)AreaType, cityId, GetQuery(search, tourKeyType));
            string fileds = "RouteId,TourId,TourNo,StartCityName,RouteName,TourStatus,TourType,[Day],Late,LeaveDate,RegistrationEndDate,TourNum,MoreThan,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,CompanyLev,IsLimit";
            string primaryKey = "TourId";
            string orderBy = " LeaveDate asc ";
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                                        {
                                            RouteId = reader["RouteId"].ToString(),
                                            TourId = reader["TourId"].ToString(),
                                            TourNo =
                                                reader.IsDBNull(reader.GetOrdinal("TourNo"))
                                                    ? string.Empty
                                                    : reader["TourNo"].ToString(),
                                            StartCityName =
                                                reader.IsDBNull(reader.GetOrdinal("StartCityName"))
                                                    ? string.Empty
                                                    : reader["StartCityName"].ToString(),
                                            RouteName = reader["RouteName"].ToString(),
                                            LeaveDate =
                                                reader.IsDBNull(reader.GetOrdinal("LeaveDate"))
                                                    ? DateTime.MinValue
                                                    : DateTime.Parse(reader["LeaveDate"].ToString()),
                                            Day = (int)reader["Day"],
                                            Late = (int)reader["Late"],
                                            RegistrationEndDate =
                                                reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate"))
                                                    ? DateTime.MinValue
                                                    : DateTime.Parse(reader["RegistrationEndDate"].ToString()),
                                            TourNum = (int)reader["TourNum"],
                                            MoreThan = (int)reader["MoreThan"],
                                            PowderTourStatus =
                                                (PowderTourStatus)
                                                Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString()),
                                            RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                                            RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                                            SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"],
                                            SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"],
                                            CompanyLev =
                                                (CompanyLev)
                                                Enum.Parse(typeof(CompanyLev), reader["CompanyLev"].ToString()),
                                            RecommendType =
                                                (RecommendType)
                                                Enum.Parse(typeof(RecommendType), reader["TourType"].ToString()),
                                            IsLimit =
                                                reader.IsDBNull(reader.GetOrdinal("IsLimit"))
                                                    ? false
                                                    : Utility.GetStringToBool(reader["IsLimit"].ToString())
                                        });
                }
            }
            return list;
        }
        /// <summary>
        /// 分页获取散拼计划列表(用户后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourKeyType">关键字搜索类型</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, int tourKeyType, MPowderSearch search)
        {
            string tableName = "tbl_NewPowderList";
            string primaryKey = "TourId";
            string fileds = "TourId,TourNo,RouteId,RouteName,Day,Late,LeaveDate,RegistrationEndDate,TourNum,MoreThan,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,MarketPrice,TourStatus,TourType,OrderPeopleNum";
            string orderBy = "LeaveDate ASC";
            //有效散拼计划
            string query = string.Format("Publishers = '{0}' AND IsDeleted = '0' AND LeaveDate >= getdate() {1}", companyId, GetQuery(search, tourKeyType));
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        TourId = reader["TourId"].ToString(),
                        RouteId = reader.IsDBNull(reader.GetOrdinal("RouteId")) ? string.Empty : reader["RouteId"].ToString(),
                        TourNo = reader.IsDBNull(reader.GetOrdinal("TourNo")) ? string.Empty : reader["TourNo"].ToString(),
                        RouteName = reader["RouteName"].ToString(),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        MoreThan = (int)reader["MoreThan"],
                        OrderNum = (int)reader["OrderPeopleNum"],
                        Day = (int)reader["Day"],
                        Late = (int)reader["Late"],
                        RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString()),
                        PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString()),
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"],
                        SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"],
                        MarketPrice = (decimal)reader["MarketPrice"]
                    });
                }
            }

            return list;
        }
        /// <summary>
        /// 分页获取散拼计划列表(运营后台)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourKeyType">关键字搜索类型</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int pageSize, int pageCurrent, ref int recordCount, int tourKeyType, MPowderSearch search)
        {
            string tableName = "view_PowderList_Select";
            string fileds = "RouteId,TourId,TourNo,StartCityName,RouteName,CompanyName,TourType,LeaveDate,RegistrationEndDate,TourNum,MoreThan,TourStatus,RetailAdultPrice,RetailChildrenPrice,MarketPrice,SettlementAudltPrice,SettlementChildrenPrice";
            string query = string.Format(" 1=1 {0}", GetQuery(search, tourKeyType));
            string orderBy = "LeaveDate DESC,B2B DESC,B2BOrder DESC,CompanyLev DESC,LastUpdateTime DESC";
            string primaryKey = "TourId";
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        RouteId = reader["RouteId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        TourNo = reader.IsDBNull(reader.GetOrdinal("TourNo")) ? string.Empty : reader["TourNo"].ToString(),
                        StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString(),
                        RouteName = reader["RouteName"].ToString(),
                        PublishersName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString(),
                        RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString()),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        MoreThan = (int)reader["MoreThan"],
                        PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), reader["TourStatus"].ToString()),
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        MarketPrice = (decimal)reader["MarketPrice"],
                        SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"],
                        SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"]
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 专线商历史团队(过了出发时间的散拼团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourKeyType">关键字搜索类型</param>
        /// <param name="search">搜索实体</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetHistoryList(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, int tourKeyType, MPowderSearch search)
        {
            string tableName = "view_PowderList_Select";
            string fileds = "TourId,TourNo,RouteId,RouteName,LeaveDate,ComeBackDate,TourNum,OrderNum,OrderPeopleNum,RetailAdultPrice,RetailChildrenPrice";
            string primaryKey = "TourId";
            string orderBy = "LeaveDate ASC";
            //过了出发时间
            string query = string.Format(" Publishers = '{0}' AND IsDeleted = '0' AND LeaveDate < getdate() {1}", companyId, GetQuery(search, tourKeyType));
            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        RouteId = reader["RouteId"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        TourNo = reader.IsDBNull(reader.GetOrdinal("TourNo")) ? string.Empty : reader["TourNo"].ToString(),
                        RouteName = reader["RouteName"].ToString(),
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        ComeBackDate = reader.IsDBNull(reader.GetOrdinal("ComeBackDate")) ? DateTime.MinValue : DateTime.Parse(reader["ComeBackDate"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        OrderPeopleNum = (int)reader["OrderPeopleNum"],
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        OrderNum = (int)reader["OrderNum"]
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 最新散客订单
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeKey">关键字(可以为空)</param>
        /// <param name="areaId">专线(默认0查询全部)</param>
        /// <param name="startDate">开始日期(出团日期)</param>
        /// <param name="endDate">结束日期(出团日期)</param>
        /// <param name="tourId">散拼团队编号(可以为空)</param>
        /// <returns></returns>
        public virtual IList<MPowderOrder> GetNewPowderOrder(int pageSize, int pageCurrent, ref int recordCount, string companyId,
            string routeKey, int areaId, DateTime? startDate, DateTime? endDate, string tourId)
        {
            NewTourStructure.DTourOrder orderDal = new DTourOrder();

            string tableName = "view_PowderOrder_SelectList";
            string fields = "TourId,TourNo,MoreThan,RouteName,LeaveDate,OrderNum,Audlt,Children,Retailer,Publishers";
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" Publishers = '{0}'", companyId);
            string primaryKey = "TourId";
            string orderBy = "LeaveDate DESC";
            #region 条件
            if (startDate.HasValue)
            {
                query.AppendFormat(" and LeaveDate >= '{0}'", startDate);
            }
            if (endDate.HasValue)
            {
                query.AppendFormat(" and LeaveDate <= '{0}'", endDate);
            }
            if (areaId > 0)
            {
                query.AppendFormat(" and AreaId = {0}", areaId);
            }
            if (!string.IsNullOrEmpty(routeKey))
            {
                query.AppendFormat(" and OrderNos like '%{0}%'", routeKey);
            }
            if (!string.IsNullOrEmpty(tourId))
            {
                query.AppendFormat(" and TourId = '{0}'", tourId);
            }
            #endregion
            IList<MPowderOrder> list = new List<MPowderOrder>();
            MPowderOrder item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fields, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    item = new MPowderOrder();
                    item.Audit = (int)reader["Audlt"];
                    item.Children = (int)reader["Children"];
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.TourId = reader["TourId"].ToString();
                    item.TourNO = reader["TourNo"].ToString();
                    item.MoreThan = (int)reader["MoreThan"];
                    item.OrderNum = (int)reader["OrderNum"];
                    item.Retailer = (int)reader["Retailer"];
                    item.Orders = orderDal.GetPublishersNewList(item.TourId, routeKey);

                    list.Add(item);
                }
            }
            return list;
        }
        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourKeyType">关键字搜索类型</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MPowderList> GetCollectionPowder(int pageSize, int pageCurrent, ref int recordCount,
            string companyId, int tourKeyType, MPowderSearch search)
        {
            string tableName = "view_NewPowderListCollection";
            string fileds = "StartCityName,TourId,TourNo,RouteId,RouteName,[Day],LeaveDate,RegistrationEndDate,TourNum,MoreThan,RetailAdultPrice,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,CompanyLev,CompanyType,TourType";
            string orderBy = "LeaveDate ASC";
            string primaryKey = "TourId";
            string query = string.Format(" CompanyId = '{0}' {1}", companyId, GetQuery(search, tourKeyType));

            IList<MPowderList> list = new List<MPowderList>();
            MPowderList item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount, tableName,
                primaryKey, fileds, query, orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MPowderList()
                    {
                        StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString(),
                        TourId = reader["TourId"].ToString(),
                        TourNo = reader.IsDBNull(reader.GetOrdinal("TourNo")) ? string.Empty : reader["TourNo"].ToString(),
                        RouteId = reader["RouteId"].ToString(),
                        RouteName = reader.IsDBNull(reader.GetOrdinal("RouteName")) ? string.Empty : reader["RouteName"].ToString(),
                        Day = (int)reader["Day"],
                        LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString()),
                        RegistrationEndDate = reader.IsDBNull(reader.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(reader["RegistrationEndDate"].ToString()),
                        TourNum = (int)reader["TourNum"],
                        MoreThan = (int)reader["MoreThan"],
                        RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                        SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"],
                        SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"],
                        CompanyLev = (CompanyLev)Enum.Parse(typeof(CompanyLev), reader["CompanyLev"].ToString()),
                        RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString()),
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 网店散拼计划
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="areaId">专线</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="orderBy">排序规则 1:按推荐类型 2：按出发时间</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public virtual IList<MShopRoute> GetShopList(int topNum, int areaId, string companyId, int orderBy, MRouteSearch search)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top({0}) StartCityName,RouteId,TourType,RouteName,TourId,LeaveDate,MoreThan,RetailAdultPrice,RetailChildrenPrice,SettlementAudltPrice,SettlementChildrenPrice", topNum);
            sql.AppendFormat(" from tbl_NewPowderList where Publishers = '{0}' and IsDeleted = '0' ", companyId);
            if (orderBy == 4)
            {
                sql.AppendFormat(" AND TourType in({0})", (int)RecommendType.特价 + "," + (int)RecommendType.推荐);
            }
            if (areaId > 0)
            {
                sql.AppendFormat(" and AreaId = {0}", areaId);
            }
            if (search != null)
            {
                if (search.StartCity > 0)
                {
                    sql.AppendFormat(" and StartCity = {0}", search.StartCity);
                }
                if (!string.IsNullOrEmpty(search.RouteKey))
                {
                    sql.AppendFormat(" and RouteName like '%{0}%'", search.RouteKey);
                }
                if (search.DayNum > 0)
                {
                    sql.AppendFormat(" and [Day] = {0}", search.DayNum);
                }
                if (search.AreaId > 0)
                {
                    sql.AppendFormat(" and AreaId = {0}", search.AreaId);
                }
                if (search.StartDate != DateTime.MinValue)
                {
                    sql.AppendFormat(" and LeaveDate >= '{0}'", search.StartDate);
                }
                if (search.EndDate != DateTime.MinValue)
                {
                    sql.AppendFormat(" and LeaveDate <= '{0}'", search.EndDate);
                }
            }
            if (orderBy > 0)
            {
                sql.AppendFormat(" order by {0} ", DRoute.GetOrderBy(orderBy));
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MShopRoute> list = new List<MShopRoute>();
            MShopRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MShopRoute();
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString());
                    item.RouteId = reader["RouteId"].ToString();
                    item.RouteName = reader["RouteName"].ToString();
                    item.TourId = reader["TourId"].ToString();
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.MoreThan = reader.IsDBNull(reader.GetOrdinal("MoreThan")) ? 0 : (int)reader["MoreThan"];
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 网店散拼计划
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MShopRoute> GetShopList(int pageSize, int pageCurrent, ref int recordCount,
            int orderBy, string companyId, MRouteSearch search)
        {
            string tableName = "tbl_NewPowderList";
            string primaryKey = "TourId";
            string fileds = "StartCityName,RouteId,TourType,RouteName,TourId,SettlementAudltPrice,SettlementChildrenPrice,LeaveDate,MoreThan,RetailAdultPrice,RetailChildrenPrice";
            string order = DRoute.GetOrderBy(orderBy);
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" Publishers = '{0}' and IsDeleted = '0' ", companyId);
            #region
            if (search != null)
            {
                if (search.StartCity > 0)
                {
                    query.AppendFormat(" and StartCity = {0}", search.StartCity);
                }
                if (!string.IsNullOrEmpty(search.RouteKey))
                {
                    query.AppendFormat(" and RouteName like '%{0}%'", search.RouteKey);
                }
                if (search.DayNum > 0)
                {
                    query.AppendFormat(" and [Day] = {0}", search.DayNum);
                }
                if (search.AreaId > 0)
                {
                    query.AppendFormat(" and AreaId = {0}", search.AreaId);
                }
                if (search.StartDate != DateTime.MinValue)
                {
                    query.AppendFormat(" and LeaveDate >= '{0}'", search.StartDate);
                }
                if (search.EndDate != DateTime.MinValue)
                {
                    query.AppendFormat(" and LeaveDate <= '{0}'", search.EndDate);
                }
            }
            #endregion

            IList<MShopRoute> list = new List<MShopRoute>();
            MShopRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                tableName, primaryKey, fileds, query.ToString(), order))
            {
                while (reader.Read())
                {
                    item = new MShopRoute();
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["TourType"].ToString());
                    item.RouteId = reader["RouteId"].ToString();
                    item.RouteName = reader["RouteName"].ToString();
                    item.TourId = reader["TourId"].ToString();
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.MoreThan = reader.IsDBNull(reader.GetOrdinal("MoreThan")) ? 0 : (int)reader["MoreThan"];
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(reader["LeaveDate"].ToString());
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取散拼计划列表
        /// </summary>
        /// <param name="topNum">top数量（小于等于0取所有）</param>
        /// <param name="search">搜索实体</param>
        /// <param name="orderIndex">排序方式 0/1 出团时间降/升序</param>
        /// <returns>散拼计划列表</returns>
        public virtual IList<MPowderList> GetList(int topNum, MPowderSearch search, int orderIndex)
        {
            var strSql = new StringBuilder();
            strSql.Append(" select ");
            if (topNum > 0)
                strSql.AppendFormat(" top {0} ", topNum);
            strSql.Append(@" TourId,TourNo,RouteId,RouteName,Day,Late,LeaveDate,RegistrationEndDate,TourNum,MoreThan,RetailAdultPrice
                            ,SettlementAudltPrice,RetailChildrenPrice,SettlementChildrenPrice,MarketPrice,TourStatus,TourType
                            ,OrderPeopleNum ");
            strSql.Append(" from tbl_NewPowderList where IsDeleted = '0' ");
            strSql.Append(GetQuery(search, 1));
            strSql.Append(" order by ");
            switch (orderIndex)
            {
                case 0:
                    strSql.Append(" LeaveDate DESC ");
                    break;
                case 1:
                    strSql.Append(" LeaveDate asc ");
                    break;
                default:
                    strSql.Append(" LeaveDate DESC ");
                    break;
            }

            DbCommand dc = _db.GetSqlStringCommand(strSql.ToString());

            IList<MPowderList> list = new List<MPowderList>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, _db))
            {
                while (dr.Read())
                {
                    list.Add(new MPowderList
                    {
                        TourId = dr.IsDBNull(dr.GetOrdinal("TourId")) ? string.Empty : dr.GetString(dr.GetOrdinal("TourId")),
                        RouteId = dr.IsDBNull(dr.GetOrdinal("RouteId")) ? string.Empty : dr["RouteId"].ToString(),
                        TourNo = dr.IsDBNull(dr.GetOrdinal("TourNo")) ? string.Empty : dr["TourNo"].ToString(),
                        RouteName = dr["RouteName"].ToString(),
                        LeaveDate = dr.IsDBNull(dr.GetOrdinal("LeaveDate")) ? DateTime.MinValue : DateTime.Parse(dr["LeaveDate"].ToString()),
                        RegistrationEndDate = dr.IsDBNull(dr.GetOrdinal("RegistrationEndDate")) ? DateTime.MinValue : DateTime.Parse(dr["RegistrationEndDate"].ToString()),
                        TourNum = (int)dr["TourNum"],
                        MoreThan = (int)dr["MoreThan"],
                        OrderNum = (int)dr["OrderPeopleNum"],
                        Day = (int)dr["Day"],
                        Late = (int)dr["Late"],
                        RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), dr["TourType"].ToString()),
                        PowderTourStatus = (PowderTourStatus)Enum.Parse(typeof(PowderTourStatus), dr["TourStatus"].ToString()),
                        RetailAdultPrice = (decimal)dr["RetailAdultPrice"],
                        RetailChildrenPrice = (decimal)dr["RetailChildrenPrice"],
                        SettlementAudltPrice = (decimal)dr["SettlementAudltPrice"],
                        SettlementChildrenPrice = (decimal)dr["SettlementChildrenPrice"],
                        MarketPrice = (decimal)dr["MarketPrice"]
                    });
                }
            }

            return list;
        }

        #endregion

        #region private
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="search"></param>
        /// <param name="tourKeyType">1:团号,线路名 2:团队编号,团队线路名,线路特色 3:线路名称,线路特色,专线商名</param>
        /// <returns></returns>
        private string GetQuery(MPowderSearch search, int tourKeyType)
        {
            if (search != null)
            {
                StringBuilder query = new StringBuilder();
                if (search.AreaId > 0)
                {
                    query.AppendFormat(" AND AreaId = {0}", search.AreaId);
                }
                if (search.AreaType.HasValue)
                {
                    query.AppendFormat(" AND RouteType = {0}", (int?)search.AreaType);
                }
                if (search.LeaveDate != DateTime.MinValue)
                {
                    query.AppendFormat(" AND LeaveDate >= '{0}'", search.LeaveDate);
                }
                if (search.EndLeaveDate != DateTime.MinValue)
                {
                    query.AppendFormat(" AND LeaveDate <= '{0}'", search.EndLeaveDate);
                }
                if (search.StartPrice > 0)
                {
                    query.AppendFormat(" AND RetailAdultPrice >= {0}", search.StartPrice);
                }
                if (search.EndPrice > 0)
                {
                    query.AppendFormat(" AND RetailAdultPrice <= {0}", search.EndPrice);
                }
                if (!string.IsNullOrEmpty(search.Publishers))
                {
                    query.AppendFormat(" AND Publishers = '{0}'", search.Publishers);
                }
                if (search.RecommendType.HasValue)
                {
                    query.AppendFormat(" AND TourType = {0}", (int?)search.RecommendType);
                }
                if (!string.IsNullOrEmpty(search.RouteId))
                {
                    query.AppendFormat(" AND RouteId = '{0}'", search.RouteId);
                }
                if (search.StartCityId > 0)
                {
                    query.AppendFormat(" AND StartCity = {0}", search.StartCityId);
                }
                if (!string.IsNullOrEmpty(search.StartCityName))
                {
                    query.AppendFormat(" AND StartCityName like '%{0}%'", search.StartCityName);
                }
                if (search.ThemeId > 0)
                {
                    query.AppendFormat(" AND cast(ThemeId as xml).exist('/item/row[@themeid=\"{0}\"]') = 1", search.ThemeId);
                }
                if (!string.IsNullOrEmpty(search.TourKey))
                {
                    switch (tourKeyType)
                    {
                        case 1:
                            query.AppendFormat(" AND isnull(TourNo,'') + isnull(RouteName,'') like '%{0}%'", search.TourKey);
                            break;
                        case 2:
                            query.AppendFormat(" AND isnull(TourNo,'') + isnull(RouteName,'') + isnull(Characteristic,'') like '%{0}%' ", search.TourKey);
                            break;
                        case 3:
                            query.AppendFormat(" AND isnull(RouteName,'') + isnull(Characteristic,'') + isnull(CompanyName,'') like '%{0}%'", search.TourKey);
                            break;
                        default:
                            break;
                    }
                }
                if (search.PowderDay.HasValue)
                {
                    switch (search.PowderDay)
                    {
                        case PowderDay.一日游:
                            query.Append(" AND [Day] = 1");
                            break;
                        case PowderDay.两日游:
                            query.Append(" AND [Day] = 2");
                            break;
                        case PowderDay.三日游:
                            query.Append(" AND [Day] = 3");
                            break;
                        case PowderDay.四日游:
                            query.Append(" AND [Day] = 4");
                            break;
                        case PowderDay.五日游:
                            query.Append(" AND [Day] = 5");
                            break;
                        case PowderDay.六日游:
                            query.Append(" AND [Day] = 6");
                            break;
                        case PowderDay.七日游:
                            query.Append(" AND [Day] = 7");
                            break;
                        case PowderDay.八日游:
                            query.Append(" AND [Day] = 8");
                            break;
                        case PowderDay.九日游:
                            query.Append(" AND [Day] = 9");
                            break;
                        case PowderDay.十日游:
                            query.Append(" AND [Day] = 10");
                            break;
                        case PowderDay.四日游及以上:
                            query.Append(" AND [Day] >= 4");
                            break;
                        case PowderDay.五日游及以下:
                            query.Append(" AND [Day] <= 5");
                            break;
                        case PowderDay.七日游及以上:
                            query.Append(" AND [Day] >= 7");
                            break;
                        case PowderDay.八至十日游:
                            query.Append(" AND [Day] >= 8 and [Day] <= 10 ");
                            break;
                        case PowderDay.十日游及以上:
                            query.Append(" AND [Day] >= 10 ");
                            break;
                        case PowderDay.三日游及以上:
                            query.Append(" AND [Day] >= 3 ");
                            break;
                        default:
                            break;
                    }
                }
                return query.ToString();
            }
            return string.Empty;
        }
        #endregion
    }
}
