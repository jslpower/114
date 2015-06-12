using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.NewTourStructure;
using EyouSoft.Model.SystemStructure;
using EyouSoft.Model.TourStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common;
using EyouSoft.Common.DAL;
using System.Data.Common;
using System.Data;
using System.Xml.Linq;
using System.Collections;

namespace EyouSoft.DAL.NewTourStructure
{
    /// <summary>
    /// 线路
    /// 创建者：郑付杰
    /// 创建时间：2011-12-20
    /// </summary>
    public class DRoute : DALBase, EyouSoft.IDAL.NewTourStructure.IRoute
    {
        #region
        private Database _db = null;
        /// <summary>
        /// 
        /// </summary>
        public DRoute()
        {
            this._db = base.SystemStore;
        }
        #endregion

        #region 增,删,改
        /// <summary>
        /// 添加线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>受影响行数</returns>
        public virtual int AddRoute(MRoute item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_Route_AddStandardRoute");
            #region 线路基本信息
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(comm, "@RouteType", DbType.Byte, (int)item.RouteType);
            this._db.AddInParameter(comm, "@AreaId", DbType.Int32, item.AreaId);
            this._db.AddInParameter(comm, "@ReleaseType", DbType.Byte, (int)item.ReleaseType);
            this._db.AddInParameter(comm, "@FastPlan", DbType.String, item.FastPlan);
            this._db.AddInParameter(comm, "@B2BName", DbType.String, item.RouteName);
            this._db.AddInParameter(comm, "@B2CName", DbType.String, item.B2CRouteName);
            this._db.AddInParameter(comm, "@Status", DbType.Byte, (int)RouteStatus.上架);
            this._db.AddInParameter(comm, "@Characteristic", DbType.String, item.Characteristic);
            this._db.AddInParameter(comm, "@StartCity", DbType.Int32, item.StartCity);
            this._db.AddInParameter(comm, "@StartCityName", DbType.String, item.StartCityName);
            this._db.AddInParameter(comm, "@EndCity", DbType.Int32, item.EndCity);
            this._db.AddInParameter(comm, "@EndCityName", DbType.String, item.EndCityName);
            this._db.AddInParameter(comm, "@RecommendType", DbType.Byte, (int)item.RecommendType);
            this._db.AddInParameter(comm, "@FitQuoation", DbType.String, item.FitQuotation);
            this._db.AddInParameter(comm, "@StartTraffic", DbType.Byte, (int)item.StartTraffic);
            this._db.AddInParameter(comm, "@EndTraffic", DbType.Byte, (int)item.EndTraffic);
            this._db.AddInParameter(comm, "@IndependentGroupPrice", DbType.Currency, item.IndependentGroupPrice);
            this._db.AddInParameter(comm, "@GroupNum", DbType.Int32, item.GroupNum);
            this._db.AddInParameter(comm, "@DayNum", DbType.Int32, item.Day);
            this._db.AddInParameter(comm, "@LateNum", DbType.Int32, item.Late);
            this._db.AddInParameter(comm, "@VendorsNotes", DbType.String, item.VendorsNotes);
            this._db.AddInParameter(comm, "@AdvanceDayReg", DbType.Int32, item.AdvanceDayRegistration);
            this._db.AddInParameter(comm, "@IsCertain", DbType.AnsiString, Utility.GetBoolToString(item.IsCertain));
            this._db.AddInParameter(comm, "@AdultPrice", DbType.Currency, item.AdultPrice);
            this._db.AddInParameter(comm, "@ChildrenPrice", DbType.Currency, item.ChildrenPrice);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, item.B2COrder);
            this._db.AddInParameter(comm, "@Publishers", DbType.AnsiStringFixedLength, item.Publishers);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            this._db.AddInParameter(comm, "@RouteSource", DbType.Byte, (int)item.RouteSource);
            this._db.AddInParameter(comm, "@RouteImg", DbType.AnsiString, item.RouteImg);
            this._db.AddInParameter(comm, "@RouteImg1", DbType.AnsiString, item.RouteImg1);
            this._db.AddInParameter(comm, "@RouteImg2", DbType.AnsiString, item.RouteImg2);
            _db.AddInParameter(comm, "IsNotVisa", DbType.AnsiString, Utility.GetBoolToString(item.IsNotVisa));

            #endregion
            #region 服务项目
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
            this._db.AddInParameter(comm, "@RouteThemeControlXml", DbType.Xml, ThemeXmlStr(item.Themes));
            this._db.AddInParameter(comm, "@NewRouteCityControlXml", DbType.Xml, CityXmlStr(item.Citys));
            this._db.AddInParameter(comm, "@RouteBrowseCountryControlXml", DbType.Xml, BrowseCountryXmlStr(item.BrowseCountrys));
            this._db.AddInParameter(comm, "@RouteBrowseCityControlXml", DbType.Xml, BrowseCityXmlStr(item.BrowseCitys));
            this._db.AddInParameter(comm, "@RouteStandardPlanXml", DbType.Xml, StandardPlanXmlStr(item.StandardPlans));
            #endregion

            return DbHelper.ExecuteSqlTrans(comm, this._db);
        }
        /// <summary>
        /// 批量修改线路上下架状态
        /// </summary>
        /// <param name="status">线路上下架状态</param>
        /// <param name="routes">线路编号</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateRouteStatus(RouteStatus status, string routes)
        {
            string sql = string.Format("UPDATE tbl_NewRouteBasicInfo SET Status = {0} WHERE CHARINDEX(','+RouteId+',',',{1},') > 0", (int)status, routes);
            DbCommand comm = this._db.GetSqlStringCommand(sql);

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 批量修改线路推荐类型
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="routes">线路编号</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateRouteRecommend(RecommendType type, string routes)
        {
            string sql = string.Format("UPDATE tbl_NewRouteBasicInfo SET RecommendType = {0} WHERE CHARINDEX(','+RouteId+',',',{1},') > 0", (int)type, routes);

            DbCommand comm = this._db.GetSqlStringCommand(sql);
            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 修改点击量
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateClick(string routeId)
        {
            string sql = "UPDATE tbl_NewRouteBasicInfo SET ClickNum = ClickNum + 1 WHERE RouteId = @RouteId";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, routeId);

            return DbHelper.ExecuteSql(comm, this._db);
        }
        /// <summary>
        /// 修改线路
        /// </summary>
        /// <param name="item">线路实体</param>
        /// <returns>受影响行数</returns>
        public virtual int UpdateRoute(MRoute item)
        {
            DbCommand comm = this._db.GetStoredProcCommand("proc_Route_UpdateStandardRoute");
            #region 线路基本信息
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, item.RouteId);
            this._db.AddInParameter(comm, "@RouteType", DbType.Byte, (int)item.RouteType);
            this._db.AddInParameter(comm, "@AreaId", DbType.Int32, item.AreaId);
            this._db.AddInParameter(comm, "@RouteImg", DbType.AnsiString, item.RouteImg);
            this._db.AddInParameter(comm, "@RouteImg1", DbType.AnsiString, item.RouteImg1);
            this._db.AddInParameter(comm, "@RouteImg2", DbType.AnsiString, item.RouteImg2);
            this._db.AddInParameter(comm, "@FastPlan", DbType.String, item.FastPlan);
            this._db.AddInParameter(comm, "@B2BName", DbType.String, item.RouteName);
            this._db.AddInParameter(comm, "@B2CName", DbType.String, item.B2CRouteName);
            this._db.AddInParameter(comm, "@Characteristic", DbType.String, item.Characteristic);
            this._db.AddInParameter(comm, "@StartCity", DbType.Int32, item.StartCity);
            this._db.AddInParameter(comm, "@ReleaseType", DbType.Byte, (int)item.ReleaseType);
            this._db.AddInParameter(comm, "@StartCityName", DbType.String, item.StartCityName);
            this._db.AddInParameter(comm, "@EndCity", DbType.Int32, item.EndCity);
            this._db.AddInParameter(comm, "@EndCityName", DbType.String, item.EndCityName);
            this._db.AddInParameter(comm, "@RecommendType", DbType.Byte, (int)item.RecommendType);
            this._db.AddInParameter(comm, "@FitQuoation", DbType.String, item.FitQuotation);
            this._db.AddInParameter(comm, "@StartTraffic", DbType.Byte, (int)item.StartTraffic);
            this._db.AddInParameter(comm, "@EndTraffic", DbType.Byte, (int)item.EndTraffic);
            this._db.AddInParameter(comm, "@IndependentGroupPrice", DbType.Currency, item.IndependentGroupPrice);
            this._db.AddInParameter(comm, "@GroupNum", DbType.Int32, item.GroupNum);
            this._db.AddInParameter(comm, "@DayNum", DbType.Int32, item.Day);
            this._db.AddInParameter(comm, "@LateNum", DbType.Int32, item.Late);
            this._db.AddInParameter(comm, "@VendorsNotes", DbType.String, item.VendorsNotes);
            this._db.AddInParameter(comm, "@AdvanceDayReg", DbType.Int32, item.AdvanceDayRegistration);
            this._db.AddInParameter(comm, "@IsCertain", DbType.AnsiString, Utility.GetBoolToString(item.IsCertain));
            this._db.AddInParameter(comm, "@AdultPrice", DbType.Currency, item.AdultPrice);
            this._db.AddInParameter(comm, "@ChildrenPrice", DbType.Currency, item.ChildrenPrice);
            this._db.AddInParameter(comm, "@B2B", DbType.Byte, (int)item.B2B);
            this._db.AddInParameter(comm, "@B2BOrder", DbType.Int32, item.B2BOrder);
            this._db.AddInParameter(comm, "@B2C", DbType.Byte, (int)item.B2C);
            this._db.AddInParameter(comm, "@B2COrder", DbType.Int32, item.B2COrder);
            this._db.AddInParameter(comm, "@OperatorId", DbType.AnsiStringFixedLength, item.OperatorId);
            _db.AddInParameter(comm, "IsNotVisa", DbType.AnsiString, Utility.GetBoolToString(item.IsNotVisa));
            #endregion
            #region 服务项目
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
            this._db.AddInParameter(comm, "@RouteThemeControlXml", DbType.Xml, ThemeXmlStr(item.Themes));
            this._db.AddInParameter(comm, "@NewRouteCityControlXml", DbType.Xml, CityXmlStr(item.Citys));
            this._db.AddInParameter(comm, "@RouteBrowseCountryControlXml", DbType.Xml, BrowseCountryXmlStr(item.BrowseCountrys));
            this._db.AddInParameter(comm, "@RouteBrowseCityControlXml", DbType.Xml, BrowseCityXmlStr(item.BrowseCitys));
            this._db.AddInParameter(comm, "@RouteStandardPlanXml", DbType.Xml, StandardPlanXmlStr(item.StandardPlans));
            #endregion

            return DbHelper.ExecuteSqlTrans(comm, this._db);
        }
        /// <summary>
        /// 删除线路
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="type">1：更新B2B 2：逻辑删除更改isdelete</param>
        /// <returns>受影响行数</returns>
        public virtual int DeleteRoute(string routeId, int type)
        {
            string sql = string.Empty;
            if (type == 1)
            {
                sql = string.Format("UPDATE tbl_NewRouteBasicInfo SET B2B = {0} WHERE RouteId = @RouteId", (int)RouteB2BDisplay.隐藏);
            }
            else if (type == 2)
            {
                sql = "UPDATE tbl_NewRouteBasicInfo SET IsDeleted = '1' WHERE RouteId = @RouteId";
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@RouteId", DbType.AnsiStringFixedLength, routeId);

            return DbHelper.ExecuteSql(comm, this._db);
        }

        #endregion

        #region 查询
        /// <summary>
        /// 获取打印线路行程单
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public virtual MRoutePrint GetPrintModel(string routeId)
        {
            string sql = string.Format("select * from view_Route_Select_Print where RouteId = '{0}'", routeId);
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    string xmlStr = string.Empty;
                    MRoutePrint item = new MRoutePrint();
                    #region

                    item.CompanyId = reader.IsDBNull(reader.GetOrdinal("Publishers"))
                                         ? string.Empty
                                         : reader.GetString(reader.GetOrdinal("Publishers"));
                    item.CompanyContactMq = reader.IsDBNull(reader.GetOrdinal("ContactMQ"))
                                         ? string.Empty
                                         : reader.GetString(reader.GetOrdinal("ContactMQ"));
                    item.AdvanceDayRegistration = (int)reader["AdvanceDayRegistration"];
                    item.RouteName = reader["B2BName"].ToString();
                    item.AreaName = reader.IsDBNull(reader.GetOrdinal("AreaName")) ? string.Empty : reader["AreaName"].ToString();
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.EndCityName = reader.IsDBNull(reader.GetOrdinal("EndCityName")) ? string.Empty : reader["EndCityName"].ToString();

                    if (reader.IsDBNull(reader.GetOrdinal("StartTraffic")) || reader.GetByte(reader.GetOrdinal("StartTraffic")) > 0)
                        item.StartTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["StartTraffic"].ToString());
                    else 
                        item.StartTraffic = TrafficType.其它;

                    if (reader.IsDBNull(reader.GetOrdinal("EndTraffic")) || reader.GetByte(reader.GetOrdinal("EndTraffic")) > 0)
                        item.EndTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["EndTraffic"].ToString());
                    else 
                        item.EndTraffic = TrafficType.其它;

                    item.RetailAdultPrice = reader.IsDBNull(reader.GetOrdinal("RetailAdultPrice")) ? 0 : (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = reader.IsDBNull(reader.GetOrdinal("RetailChildrenPrice")) ? 0 : (decimal)reader["RetailChildrenPrice"];
                    item.MinRetailAdultPrice = reader.IsDBNull(reader.GetOrdinal("minRetailAdultPrice"))
                                                   ? 0M
                                                   : reader.GetDecimal(reader.GetOrdinal("minRetailAdultPrice"));
                    item.MinRetailChildrenPrice = reader.IsDBNull(reader.GetOrdinal("minRetailChildrenPrice"))
                                                   ? 0M
                                                   : reader.GetDecimal(reader.GetOrdinal("minRetailChildrenPrice"));
                    item.AdultPrice = reader.IsDBNull(reader.GetOrdinal("AdultPrice")) ? 0 : (decimal)reader["AdultPrice"];
                    item.ChildrenPrice = reader.IsDBNull(reader.GetOrdinal("ChildrenPrice")) ? 0 : (decimal)reader["ChildrenPrice"];
                    item.Characteristic = reader.IsDBNull(reader.GetOrdinal("Characteristic")) ? string.Empty : reader["Characteristic"].ToString();
                    item.CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString();
                    item.CompanyAddress = reader.IsDBNull(reader.GetOrdinal("CompanyAddress")) ? string.Empty : reader["CompanyAddress"].ToString();
                    item.CompanyContact = reader.IsDBNull(reader.GetOrdinal("ContactName")) ? string.Empty : reader["ContactName"].ToString();
                    item.Tel = reader.IsDBNull(reader.GetOrdinal("ContactTel")) ? string.Empty : reader["ContactTel"].ToString();
                    item.Mobile = reader.IsDBNull(reader.GetOrdinal("ContactMobile")) ? string.Empty : reader["ContactMobile"].ToString();
                    item.Fax = reader.IsDBNull(reader.GetOrdinal("ContactFax")) ? string.Empty : reader["ContactFax"].ToString();
                    item.AlipayAccount = reader.IsDBNull(reader.GetOrdinal("AlipayAccount")) ? string.Empty : reader["AlipayAccount"].ToString();
                    item.RouteSource = (RouteSource)Enum.Parse(typeof(RouteSource), reader["RouteSource"].ToString());
                    item.Day = (int)reader["DayNum"];
                    item.Late = (int)reader["LateNum"];
                    item.CompanyLogo = reader.IsDBNull(reader.GetOrdinal("CompanyLogo")) ? string.Empty : reader["CompanyLogo"].ToString();
                    item.FitQuotation = reader.IsDBNull(reader.GetOrdinal("FitQuotation")) ? string.Empty : reader["FitQuotation"].ToString();
                    item.GroupNum = (int)reader["GroupNum"];
                    item.ReferencePrice = (decimal)reader["IndependentGroupPrice"];
                    #endregion
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
                                    if (item.PersonalAccount == null)
                                    {
                                        item.PersonalAccount = new ArrayList();
                                    }
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
                    item.ReleaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), reader["ReleaseType"].ToString());
                    if (item.ReleaseType == ReleaseType.Quick)
                    {
                        item.FastPlan = reader.IsDBNull(reader.GetOrdinal("FastPlan")) ? string.Empty : reader["FastPlan"].ToString();
                    }
                    else
                    {
                        #region 标准行程
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
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("TeamPlans")))
                    {
                        xmlStr = reader["TeamPlans"].ToString();
                        XElement xRoot = XElement.Parse(xmlStr);
                        var xRows = Utility.GetXElements(xRoot, "row");
                        item.TeamPlanDes = new ArrayList();
                        if (xRows != null && xRows.Any())
                        {
                            foreach (var t in xRows)
                            {
                                item.TeamPlanDes.Add(Utility.GetDateTime(Utility.GetXAttributeValue(t, "LeaveDate")));
                            }
                        }
                    }
                    item.ThemeName = reader.IsDBNull(reader.GetOrdinal("Theme")) ? string.Empty : reader["Theme"].ToString();
                    item.BrowseCity = reader.IsDBNull(reader.GetOrdinal("Brower")) ? string.Empty : reader["Brower"].ToString();
                    item.RouteType = (AreaType)Enum.Parse(typeof(AreaType), reader["RouteType"].ToString());
                    if (item.RouteType == AreaType.国际线)
                    {
                        item.VisaCity = reader.IsDBNull(reader.GetOrdinal("IsVisa")) ? string.Empty : reader["IsVisa"].ToString();
                    }
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

                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// 验证公司下线路名称是否存在
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称</param>
        /// <returns></returns>
        public virtual bool IsExits(string companyId, string routeName)
        {
            string sql = "SELECT COUNT(*) FROM tbl_NewRouteBasicInfo WHERE B2BName = @B2BName AND Publishers = @Publishers";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@B2BName", DbType.String, routeName);
            this._db.AddInParameter(comm, "@Publishers", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.Exists(comm, this._db);
        }
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns>线路实体</returns>
        public virtual MRoute GetModel(string routeId)
        {
            return GetModelData(routeId, 0);
        }
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="Id">线路自增编号</param>
        /// <returns>线路实体</returns>
        public virtual MRoute GetModel(long Id)
        {
            return GetModelData(string.Empty, Id);
        }
        /// <summary>
        /// 线路首页-最新旅游线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <returns>线路集合</returns>
        public virtual IList<MRoute> GetNewRouteList(int topNum)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top {0} a.RouteId,a.Id,a.B2BName from tbl_NewRouteBasicInfo a", topNum);
            sql.AppendFormat(" left join (select count(TourId) c,RouteId from tbl_NewPowderList where IsDeleted = '0' and LeaveDate >= getdate() group by RouteId) b on a.RouteId = b.RouteId");
            sql.Append(" left join tbl_CompanyInfo c on a.Publishers = c.Id");
            //签约的专线商用户,线路必须为上架，有散拼团队,
            //周文超 2012-07-19  去掉有散拼团队 条件 and b.c > 0 
            sql.AppendFormat(
                " where a.IsDeleted = '0' and c.CompanyLev = {0} and a.Status = {1} and a.RouteSource = {2} and a.B2B <> {3} ",
                (int)Model.CompanyStructure.CompanyLev.签约商户, (int)RouteStatus.上架, (int)RouteSource.专线商添加,
                (int)RouteB2BDisplay.隐藏);
            sql.Append(" order by a.IssueTime desc");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            IList<MRoute> list = new List<MRoute>();
            MRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MRoute()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        RouteId = reader["RouteId"].ToString(),
                        RouteName = reader["B2BName"].ToString()
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        public virtual IList<MRoute> GetRecommendList(int topNum, RecommendType? type, RouteB2BDisplay? b2BDisplay)
        {
            string sql = " SELECT ";
            if (topNum > 0)
                sql += string.Format(" top {0} ", topNum);
            sql +=
                string.Format(" Id,RouteId,B2BName FROM tbl_NewRouteBasicInfo WHERE IsDeleted = '0' AND Status = {0} ",
                              (int)RouteStatus.上架);
            if (b2BDisplay.HasValue)
                sql += string.Format(" AND B2B = {0} ", (int)b2BDisplay.Value);
            if (type.HasValue)
                sql += string.Format(" AND RecommendType = {0} ", (int)type.Value);
            sql += " order by B2BOrder asc,LastUpdateTime desc ";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            IList<MRoute> list = new List<MRoute>();
            MRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MRoute()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        RouteId = reader["RouteId"].ToString(),
                        RouteName = reader["B2BName"].ToString()
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="type">推荐类型</param>
        /// <param name="b2BDisplay">B2B显示控制</param>
        /// <returns></returns>
        public virtual IList<MRoute> GetRecommendList(int topNum, string companyId, RecommendType? type, RouteB2BDisplay? b2BDisplay)
        {
            string sql = " SELECT ";
            if (topNum > 0)
                sql += string.Format(" top {0} ", topNum);
            sql +=
                string.Format(" Id,RouteId,B2BName FROM tbl_NewRouteBasicInfo WHERE IsDeleted = '0' AND Status = {0} ",
                              (int)RouteStatus.上架);
            if (b2BDisplay.HasValue)
                sql += string.Format(" AND B2B = {0} ", (int)b2BDisplay.Value);
            if (type.HasValue)
                sql += string.Format(" AND RecommendType = {0} ", (int)type.Value);
            if (!string.IsNullOrEmpty(companyId))
                sql += string.Format(" AND Publishers = '{0}' ", companyId);
            sql += " order by B2BOrder asc,LastUpdateTime desc ";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            IList<MRoute> list = new List<MRoute>();
            MRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MRoute()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        RouteId = reader["RouteId"].ToString(),
                        RouteName = reader["B2BName"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 根据推荐类型获取线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="search">查询实体</param>
        /// <returns></returns>
        public IList<MRoute> GetRecommendList(int topNum, MTuiJianRouteSearch search)
        {
            string sql = " SELECT ";
            if (topNum > 0)
                sql += string.Format(" top {0} ", topNum);
            sql +=
                string.Format(" Id,RouteId,B2BName FROM tbl_NewRouteBasicInfo WHERE IsDeleted = '0' AND Status = {0} ",
                              (int)RouteStatus.上架);
            if (search != null)
            {
                if (search.B2BDisplay.HasValue)
                    sql += string.Format(" AND B2B = {0} ", (int)search.B2BDisplay.Value);
                if (search.Type.HasValue)
                    sql += string.Format(" AND RecommendType = {0} ", (int)search.Type.Value);
                if (!string.IsNullOrEmpty(search.CompanyId))
                    sql += string.Format(" AND Publishers = '{0}' ", search.CompanyId);

                string strTmp = string.Empty;
                if (search.AreaIds != null && search.AreaIds.Any())
                {
                    if (search.AreaIds.Length == 1 && search.AreaIds[0] > 0)
                    {
                        sql += string.Format(" AND AreaId = {0} ", search.AreaIds[0]);
                    }
                    else
                    {
                        strTmp = string.Empty;
                        foreach (var t in search.AreaIds)
                        {
                            if (t <= 0)
                                continue;

                            strTmp += t + ",";
                        }
                        if (!string.IsNullOrEmpty(strTmp))
                            sql += string.Format(" AND AreaId in ({0}) ", strTmp.TrimEnd(','));
                    }
                }
                if (search.SellCityIds != null && search.SellCityIds.Any())
                {
                    sql +=
                        " and exists (select 1 from tbl_NewRouteCityControl where tbl_NewRouteCityControl.RouteId = tbl_NewRouteBasicInfo.RouteId ";
                    if (search.SellCityIds.Length == 1 && search.SellCityIds[0] > 0)
                    {
                        sql += string.Format(" AND tbl_NewRouteCityControl.CityId = {0} ", search.SellCityIds[0]);
                    }
                    else
                    {
                        strTmp = string.Empty;
                        foreach (var t in search.SellCityIds)
                        {
                            if (t <= 0)
                                continue;

                            strTmp += t + ",";
                        }
                        if (!string.IsNullOrEmpty(strTmp))
                            sql += string.Format(" AND tbl_NewRouteCityControl.CityId in ({0}) ", strTmp.TrimEnd(','));
                    }
                    sql += " ) ";
                }
                sql += " order by ";
                switch (search.OrderIndex)
                {
                    case 0:
                        sql += " B2BOrder asc,LastUpdateTime desc ";
                        break;
                    case 1:
                        sql += " B2BOrder desc,LastUpdateTime asc ";
                        break;
                    default:
                        sql += " B2BOrder asc,LastUpdateTime desc ";
                        break;
                }
            }
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            IList<MRoute> list = new List<MRoute>();
            MRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MRoute()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        RouteId = reader["RouteId"].ToString(),
                        RouteName = reader["B2BName"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 线路列表-组团社
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MRoute> GetList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, MRouteSearch search)
        {
            string fields = "Id,RouteId,StartCityName,B2BName,RecommendType,DayNum,LateNum,CompanyName,Publishers,[Count],RetailAdultPrice,"
            + "RetailChildrenPrice,MaxDate,MinDate,B2B,B2C,ClickNum,Status,RouteType,AreaId,Characteristic,CompanyBrand,Introduction,CompanyLev,CompanyType,IndependentGroupPrice,IssueTime";
            string tableName = GetTableName(search, false);
            string orderByStr = GetOrderBy(orderBy);
            string query = string.Format(" IsDeleted = '0' AND B2B <> {0} {1}", (int)RouteB2BDisplay.隐藏, GetSQL(search, routeKeyType));

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref RecordCount,
                tableName, fields, query, orderByStr, false, string.Empty))
            {
                return GetListData(reader);
            }
        }
        /// <summary>
        /// 大平台线路列表(只显示签约，收费和认证的专线商线路)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public virtual IList<MRoute> GetPublicCenterList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, MRouteSearch search)
        {
            string fields = "Id,RouteId,StartCityName,B2BName,RecommendType,DayNum,LateNum,CompanyName,Publishers,[Count],RetailAdultPrice,"
                + "RetailChildrenPrice,MaxDate,MinDate,B2B,B2C,ClickNum,Status,RouteType,AreaId,Characteristic,CompanyBrand,Introduction,CompanyLev,CompanyType,IndependentGroupPrice,IssueTime";
            string tableName = GetTableName(search, false);
            string orderByStr = GetOrderBy(orderBy);
            string query = string.Format(" IsDeleted = '0' AND B2B <> {0} {1}", (int)RouteB2BDisplay.隐藏, GetSQL(search, routeKeyType));
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref RecordCount,
                tableName, fields, query, orderByStr, false, string.Empty))
            {
                return GetListData(reader);
            }
        }
        /// <summary>
        /// 运营后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public virtual IList<MRoute> GetOperationsCenterList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, MRouteSearch search)
        {
            string fields = "Id,RouteId,StartCityName,B2BName,RecommendType,DayNum,LateNum,CompanyName,Publishers,[Count],RetailAdultPrice,"
                + "RetailChildrenPrice,MaxDate,MinDate,B2B,B2C,ClickNum,Status,RouteType,AreaId,Characteristic,CompanyBrand,Introduction,CompanyLev,CompanyType,IndependentGroupPrice,IssueTime";
            string tableName = GetTableName(search, false);
            string orderByStr = GetOrderBy(orderBy);
            string query = string.Format(" IsDeleted = '0' {0}", GetSQL(search, routeKeyType));
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref RecordCount,
                tableName, fields, query, orderByStr, false, string.Empty))
            {
                return GetListData(reader);
            }
        }
        /// <summary>
        /// 用户后台线路列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageCurrent">当前页</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="orderBy">排序方式</param>
        /// <param name="routeKeyType">1：标题，线路特色 2标题，途径，特色 3标题</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns>线路集合</returns>
        public virtual IList<MRoute> GetBackCenterList(int pageSize, int pageCurrent, ref int RecordCount,
            int orderBy, int routeKeyType, string companyId, MRouteSearch search)
        {
            string fields = "Id,RouteId,StartCityName,B2BName,RecommendType,DayNum,LateNum,CompanyName,Publishers,[Count],RetailAdultPrice,"
                + "RetailChildrenPrice,MaxDate,MinDate,B2B,B2C,ClickNum,Status,RouteType,AreaId,Characteristic,CompanyBrand,Introduction,CompanyLev,CompanyType,IndependentGroupPrice,IssueTime";
            string tableName = GetTableName(search, false);
            string orderByStr = GetOrderBy(orderBy);
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" IsDeleted = '0' AND B2B <> {0} AND Publishers = '{1}' {2}", (int)RouteB2BDisplay.隐藏, companyId, GetSQL(search, routeKeyType));
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref RecordCount,
                tableName, fields, query.ToString(), orderByStr, false, string.Empty))
            {
                return GetListData(reader);
            }
        }

        /// <summary>
        /// 网店线路
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cityId">城市</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="orderBy">排序规则 1:按推荐类型 2：按出发时间</param>
        /// <returns></returns>
        public virtual IList<MShopRoute> GetShopList(int topNum, int cityId, string companyId, int orderBy)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT top({0}) a.RecommendType,a.StartCityName,a.RouteId,a.B2BName,isnull(b.RetailAdultPrice,0)RetailAdultPrice,", topNum);
            sql.Append(" isnull(b.RetailChildrenPrice,0)RetailChildrenPrice,isnull(b.SettlementAudltPrice,0)SettlementAudltPrice,");
            sql.Append(" isnull(b.SettlementChildrenPrice,0)SettlementChildrenPrice,b.MaxDate,b.MinDate,b.MarketPrice,b.MoreThan,a.ClickNum,isnull(b.[Count],0)[Count],");
            sql.Append(" a.RouteSource, ");
            sql.Append(" b.LeaveDate from tbl_NewRouteBasicInfo a");
            sql.Append(" left join(");
            sql.Append(" select count(*) [Count],min(RetailAdultPrice) RetailAdultPrice,min(SettlementAudltPrice)SettlementAudltPrice,");
            sql.Append(" max(LeaveDate) MaxDate,min(SettlementChildrenPrice)SettlementChildrenPrice,min(MarketPrice)MarketPrice,max(MoreThan)MoreThan,");
            sql.Append(" min(RetailChildrenPrice)RetailChildrenPrice,min(LeaveDate) MinDate,RouteId,min(LeaveDate)LeaveDate ");
            sql.Append(" from tbl_NewPowderList where IsDeleted = '0' and LeaveDate >= getdate() group by RouteId");
            sql.Append(" ) b on a.RouteId = b.RouteId");
            sql.AppendFormat(" where a.IsDeleted = '0' and a.B2B <> {0}", (int)RouteB2BDisplay.隐藏);
            //网店线路需要显示下架的线路
            sql.AppendFormat(" and a.Status = {0} ", (int) RouteStatus.上架);

            if (cityId > 0) //城市
            {
                sql.AppendFormat(" and a.StartCity = {0}", cityId);
            }
            sql.AppendFormat("and a.Publishers = '{0}' ", companyId);
            if (orderBy != 0)
                sql.AppendFormat(" order by {0} ", GetOrderBy(orderBy));
            else
                sql.Append(" order by [Count] desc, LastUpdateTime desc ");

            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            IList<MShopRoute> list = new List<MShopRoute>();
            MShopRoute item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    item = new MShopRoute();
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["RecommendType"].ToString());
                    item.RouteId = reader["RouteId"].ToString();
                    item.RouteName = reader["B2BName"].ToString();
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.MarketPrice = reader.IsDBNull(reader.GetOrdinal("MarketPrice")) ? 0 : (decimal)reader["MarketPrice"];
                    item.ClickNum = (int)reader["ClickNum"];
                    item.MoreThan = reader.IsDBNull(reader.GetOrdinal("MoreThan")) ? 0 : (int)reader["MoreThan"];
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("MinDate")) ? DateTime.MinValue : DateTime.Parse(reader["MinDate"].ToString());
                    item.TeamPlanDes = GetSpjh((int)reader["Count"], reader.IsDBNull(reader.GetOrdinal("MaxDate")) ? DateTime.MinValue : DateTime.Parse(reader["MaxDate"].ToString()),
                                             reader.IsDBNull(reader.GetOrdinal("MinDate")) ? DateTime.MinValue : DateTime.Parse(reader["MinDate"].ToString()));
                    if (!reader.IsDBNull(reader.GetOrdinal("RouteSource")))
                        item.RouteSource = (RouteSource)reader.GetByte(reader.GetOrdinal("RouteSource"));

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 网店线路
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
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.RecommendType,a.RouteId,a.StartCityName,a.B2BName,isnull(b.RetailAdultPrice,0)RetailAdultPrice,a.Status,a.IssueTime,a.LastUpdateTime,");
            sql.Append(" isnull(b.RetailChildrenPrice,0)RetailChildrenPrice,isnull(b.SettlementAudltPrice,0)SettlementAudltPrice,");
            sql.Append(" isnull(b.SettlementChildrenPrice,0)SettlementChildrenPrice,b.MaxDate,b.MinDate,b.MarketPrice,b.MoreThan,a.ClickNum,isnull(b.[Count],0)[Count]");
            sql.Append(" ,a.RouteSource,a.IndependentGroupPrice ");
            sql.Append(" ,b.LeaveDate from tbl_NewRouteBasicInfo a");
            sql.Append(" left join(");
            sql.Append(" select count(*) [Count],min(RetailAdultPrice) RetailAdultPrice,min(SettlementAudltPrice)SettlementAudltPrice,");
            sql.Append(" max(LeaveDate) MaxDate,min(SettlementChildrenPrice)SettlementChildrenPrice,min(MarketPrice)MarketPrice,max(MoreThan)MoreThan,");
            sql.Append(" min(RetailChildrenPrice)RetailChildrenPrice,min(LeaveDate) MinDate,RouteId,min(LeaveDate)LeaveDate ");
            sql.Append(" from tbl_NewPowderList where IsDeleted = '0' and LeaveDate >= getdate() ");
            if (search != null)
            {
                if (search.StartDate != DateTime.MinValue)
                {
                    sql.AppendFormat(" and LeaveDate >= '{0}'", search.StartDate);
                }
                if (search.EndDate != DateTime.MinValue)
                {
                    sql.AppendFormat(" and LeaveDate <= '{0}'", search.EndDate);
                }
            }
            sql.Append(" group by RouteId");
            sql.Append(" ) b on a.RouteId = b.RouteId");
            sql.AppendFormat(" where a.IsDeleted = '0' and a.B2B <> {0} ", (int)RouteB2BDisplay.隐藏);
            //网店线路需要显示下架的线路
            sql.AppendFormat(" and a.Status = {0} ", (int) RouteStatus.上架);
            if (search != null)
            {
                if (search.StartCity > 0)
                {
                    sql.AppendFormat(" and a.StartCity = {0}", search.StartCity);
                }
                if (search.DayNum > 0)
                {
                    sql.AppendFormat(" and a.DayNum = {0}", search.DayNum);
                }
                if (!string.IsNullOrEmpty(search.RouteKey))
                {
                    sql.AppendFormat(" and a.B2BName like '%{0}%'", search.RouteKey);
                }
            }
            sql.AppendFormat("and a.Publishers = '{0}'", companyId);

            string order = " [Count] desc, " + GetOrderBy(orderBy);
            string fileds = "RecommendType,RouteId,B2BName,LeaveDate,StartCityName,RetailAdultPrice,RetailChildrenPrice,SettlementAudltPrice,SettlementChildrenPrice,MaxDate,MinDate,MarketPrice,MoreThan,ClickNum,Count,RouteSource,IndependentGroupPrice";
            IList<MShopRoute> list = new List<MShopRoute>();
            MShopRoute item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageCurrent, ref recordCount,
                sql.ToString(), fileds, string.Empty, order, false, string.Empty))
            {
                while (reader.Read())
                {
                    item = new MShopRoute();
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["RecommendType"].ToString());
                    item.RouteId = reader["RouteId"].ToString();
                    item.RouteName = reader["B2BName"].ToString();
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.SettlementAudltPrice = (decimal)reader["SettlementAudltPrice"];
                    item.SettlementChildrenPrice = (decimal)reader["SettlementChildrenPrice"];
                    item.RetailAdultPrice = (decimal)reader["RetailAdultPrice"];
                    item.RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"];
                    item.MarketPrice = reader.IsDBNull(reader.GetOrdinal("MarketPrice")) ? 0 : (decimal)reader["MarketPrice"];
                    item.ClickNum = (int)reader["ClickNum"];
                    item.MoreThan = reader.IsDBNull(reader.GetOrdinal("MoreThan")) ? 0 : (int)reader["MoreThan"];
                    item.LeaveDate = reader.IsDBNull(reader.GetOrdinal("MinDate")) ? DateTime.MinValue : DateTime.Parse(reader["MinDate"].ToString());
                    item.TeamPlanDes = GetSpjh((int)reader["Count"], reader.IsDBNull(reader.GetOrdinal("MaxDate")) ? DateTime.MinValue : DateTime.Parse(reader["MaxDate"].ToString()),
                                             reader.IsDBNull(reader.GetOrdinal("MinDate")) ? DateTime.MinValue : DateTime.Parse(reader["MinDate"].ToString()));
                    if (!reader.IsDBNull(reader.GetOrdinal("RouteSource")))
                        item.RouteSource = (RouteSource)reader.GetByte(reader.GetOrdinal("RouteSource"));
                    item.IndependentGroupPrice = reader.IsDBNull(reader.GetOrdinal("IndependentGroupPrice"))
                                                     ? 0
                                                     : reader.GetDecimal(reader.GetOrdinal("IndependentGroupPrice"));

                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 线路区域对应的有效线路（地接社）
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetRouteByAreaStats(string companyId, string userAreas)
        {
            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = new List<EyouSoft.Model.TourStructure.AreaStatInfo>();

            StringBuilder cmdText = new StringBuilder();
            cmdText.Append("select AreaId,count(*) as RouteNumber from tbl_NewRouteBasicInfo");
            cmdText.AppendFormat(" where IsDeleted = '0' and B2B <> {0} and Publishers = '{1}' and RouteSource = {2}", (int)RouteB2BDisplay.隐藏, companyId, (int)RouteSource.地接社添加);
            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" and AreaId in ({0})", userAreas);
            }
            cmdText.Append(" group by AreaId");
            DbCommand comm = this._db.GetSqlStringCommand(cmdText.ToString());
            using (IDataReader rdr = DbHelper.ExecuteReader(comm, base.TourStore))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TourStructure.AreaStatInfo(rdr.GetInt32(0), "", rdr.GetInt32(1)));
                }
            }

            return stats;
        }


        #endregion

        #region private
        /// <summary>
        /// 获取线路实体
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private MRoute GetModelData(string routeId, long Id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select (select tb.themeid,b.FieldName from tbl_NewRouteThemeControl tb ");
            sql.Append(" inner join tbl_SysField b on tb.ThemeId = b.FieldId and b.FieldType = 2 ");
            sql.Append(" where tb.RouteId = a.RouteId for xml raw,root('item')) Theme,");
            sql.Append(" (select tb.cityid,tb.CountyId,b.CityName,c.DistrictName from tbl_NewRouteBrowseCityControl tb");
            sql.Append(" left join tbl_SysCity b on tb.Cityid = b.Id ");
            sql.Append(" left join tbl_sysDistrictCounty c on tb.CountyId = c.Id ");
            sql.Append(" where tb.RouteId = a.RouteId for xml raw,root('item')) BrowseCity,");
            sql.Append(" (select tb.countryid,tb.isvisa,b.CName from tbl_NewRouteBrowseCountryControl tb");
            sql.Append(" inner join tbl_SysCountry b on tb.CountryId = b.Id ");
            sql.Append(" where tb.RouteId = a.RouteId for xml raw,root('item')) Country,");
            sql.Append(" (select tb.cityid,tb.provinceid,b.CityName from tbl_NewRouteCityControl tb");
            sql.Append(" inner join tbl_SysCity b on tb.CityId = b.Id");
            sql.Append(" where tb.RouteId = a.RouteId for xml raw,root('item')) City,");
            sql.Append(" (select * from tbl_NewRouteStandardPlan where RouteId = a.RouteId for xml raw,root('item')) StandardPlan,");
            sql.Append(" (select isnull(min(RetailAdultPrice),0) from tbl_NewPowderList where IsDeleted = '0' and LeaveDate >= getdate() and RouteId = a.RouteId) PublicAuditPrice,");
            sql.Append(" a.*,b.*,c.RouteImg,c.RouteImg1,c.RouteImg2,d.AreaName,e.CompanyName,f.ContactName from tbl_NewRouteBasicInfo a");
            sql.Append(" left join tbl_NewRouteServiceStandard b on a.RouteId = b.RouteId");
            sql.Append(" left join tbl_NewRouteImg c on a.RouteId = c.RouteId");
            sql.Append(" left join tbl_SysArea d on a.AreaId = d.ID");
            sql.Append(" left join tbl_CompanyInfo e on a.Publishers = e.Id ");
            sql.Append(" left join tbl_CompanyUser f on a.OperatorId = f.Id ");

            if (!string.IsNullOrEmpty(routeId))
            {
                sql.AppendFormat(" where a.RouteId = '{0}'", routeId);
            }
            else if (Id > 0)
            {
                sql.AppendFormat(" where a.Id = {0}", Id);
            }
            string xmlStr = string.Empty;
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                if (reader.Read())
                {
                    MRoute item = new MRoute();
                    #region 基本信息
                    item.RouteId = reader["RouteId"].ToString();
                    item.Id = long.Parse(reader["Id"].ToString());
                    item.RouteType = (AreaType)Enum.Parse(typeof(AreaType), reader["RouteType"].ToString());
                    item.ReleaseType = (ReleaseType)Enum.Parse(typeof(ReleaseType), reader["ReleaseType"].ToString());
                    item.AreaId = (int)reader["AreaId"];
                    item.AreaName = reader.IsDBNull(reader.GetOrdinal("AreaName")) ? string.Empty : reader["AreaName"].ToString();
                    item.RouteName = reader["B2BName"].ToString();
                    item.B2CRouteName = reader.IsDBNull(reader.GetOrdinal("B2CName")) ? string.Empty : reader["B2CName"].ToString();
                    item.RouteStatus = (RouteStatus)Enum.Parse(typeof(RouteStatus), reader["Status"].ToString());
                    item.Characteristic = reader.IsDBNull(reader.GetOrdinal("Characteristic")) ? string.Empty : reader["Characteristic"].ToString();
                    item.StartCity = (int)reader["StartCity"];
                    item.EndCityName = reader.IsDBNull(reader.GetOrdinal("EndCityName")) ? string.Empty : reader["EndCityName"].ToString();
                    item.EndCity = (int)reader["EndCity"];
                    item.StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString();
                    item.StartTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["StartTraffic"].ToString());
                    item.EndTraffic = (TrafficType)Enum.Parse(typeof(TrafficType), reader["EndTraffic"].ToString());
                    item.IndependentGroupPrice = (decimal)reader["IndependentGroupPrice"];
                    item.GroupNum = (int)reader["GroupNum"];
                    item.Day = (int)reader["DayNum"];
                    item.Late = (int)reader["LateNum"];
                    item.VendorsNotes = reader.IsDBNull(reader.GetOrdinal("VendorsNotes")) ? string.Empty : reader["VendorsNotes"].ToString();
                    item.AdvanceDayRegistration = (int)reader["AdvanceDayRegistration"];
                    item.IsCertain = Utility.GetStringToBool(reader["IsCertain"].ToString());
                    item.AdultPrice = (decimal)reader["AdultPrice"];
                    item.ChildrenPrice = (decimal)reader["ChildrenPrice"];
                    item.B2B = (RouteB2BDisplay)Enum.Parse(typeof(RouteB2BDisplay), reader["B2B"].ToString());
                    item.B2BOrder = (int)reader["B2BOrder"];
                    item.B2C = (RouteB2CDisplay)Enum.Parse(typeof(RouteB2CDisplay), reader["B2C"].ToString());
                    item.B2COrder = (int)reader["B2COrder"];
                    item.Publishers = reader.IsDBNull(reader.GetOrdinal("Publishers")) ? string.Empty : reader["Publishers"].ToString();
                    item.OperatorId = reader.IsDBNull(reader.GetOrdinal("OperatorId")) ? string.Empty : reader["OperatorId"].ToString();
                    item.IssueTime = DateTime.Parse(reader["IssueTime"].ToString());
                    item.ClickNum = (int)reader["ClickNum"];
                    item.IsDeleted = Utility.GetStringToBool(reader["IsDeleted"].ToString());
                    item.RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["RecommendType"].ToString());
                    item.RouteImg = reader.IsDBNull(reader.GetOrdinal("RouteImg")) ? string.Empty : reader["RouteImg"].ToString();
                    item.RouteImg1 = reader.IsDBNull(reader.GetOrdinal("RouteImg1")) ? string.Empty : reader["RouteImg1"].ToString();
                    item.RouteImg2 = reader.IsDBNull(reader.GetOrdinal("RouteImg2")) ? string.Empty : reader["RouteImg2"].ToString();
                    item.RouteSource = (RouteSource)Enum.Parse(typeof(RouteSource), reader["RouteSource"].ToString());
                    item.PublishersName = reader.IsDBNull(reader.GetOrdinal("CompanyName"))
                                              ? string.Empty
                                              : reader.GetString(reader.GetOrdinal("CompanyName"));
                    item.OperatorName = reader.IsDBNull(reader.GetOrdinal("ContactName"))
                                              ? string.Empty
                                              : reader.GetString(reader.GetOrdinal("ContactName"));

                    #endregion
                    item.PublicAuditPrice = (decimal)reader["PublicAuditPrice"];
                    item.IsNotVisa = reader.IsDBNull(reader.GetOrdinal("IsNotVisa"))
                                         ? false
                                         : Utility.GetStringToBool(reader.GetString(reader.GetOrdinal("IsNotVisa")));

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

                    item.FastPlan = reader.IsDBNull(reader.GetOrdinal("FastPlan")) ? string.Empty : reader["FastPlan"].ToString();
                    item.FitQuotation = reader.IsDBNull(reader.GetOrdinal("FitQuotation")) ? string.Empty : reader["FitQuotation"].ToString();
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
        /// 读取数据
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private IList<MRoute> GetListData(IDataReader reader)
        {
            IList<MRoute> list = new List<MRoute>();
            MRoute item = null;
            while (reader.Read())
            {
                list.Add(item = new MRoute()
                {
                    Id = long.Parse(reader["Id"].ToString()),
                    RouteId = reader["RouteId"].ToString(),
                    StartCityName = reader.IsDBNull(reader.GetOrdinal("StartCityName")) ? string.Empty : reader["StartCityName"].ToString(),
                    RouteName = reader["B2BName"].ToString(),
                    RecommendType = (RecommendType)Enum.Parse(typeof(RecommendType), reader["RecommendType"].ToString()),
                    Day = (int)reader["DayNum"],
                    Late = (int)reader["LateNum"],
                    PublishersName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? string.Empty : reader["CompanyName"].ToString(),
                    CompanyBrand = reader.IsDBNull(reader.GetOrdinal("CompanyBrand")) ? string.Empty : reader["CompanyBrand"].ToString(),
                    Introduction = reader.IsDBNull(reader.GetOrdinal("Introduction")) ? string.Empty : reader["Introduction"].ToString(),
                    CompanyLev = (Model.CompanyStructure.CompanyLev)Enum.Parse(typeof(Model.CompanyStructure.CompanyLev), reader["CompanyLev"].ToString()),
                    Publishers = reader.IsDBNull(reader.GetOrdinal("Publishers")) ? string.Empty : reader["Publishers"].ToString(),
                    RetailAdultPrice = (decimal)reader["RetailAdultPrice"],
                    RetailChildrenPrice = (decimal)reader["RetailChildrenPrice"],
                    TeamPlanDes = GetSpjh((int)reader["Count"], reader.IsDBNull(reader.GetOrdinal("MaxDate")) ? DateTime.MinValue : DateTime.Parse(reader["MaxDate"].ToString()),
                                             reader.IsDBNull(reader.GetOrdinal("MinDate")) ? DateTime.MinValue : DateTime.Parse(reader["MinDate"].ToString())),

                    B2B = (RouteB2BDisplay)Enum.Parse(typeof(RouteB2BDisplay), reader["B2B"].ToString()),
                    B2C = (RouteB2CDisplay)Enum.Parse(typeof(RouteB2CDisplay), reader["B2C"].ToString()),
                    ClickNum = (int)reader["ClickNum"],
                    RouteStatus = (RouteStatus)Enum.Parse(typeof(RouteStatus), reader["Status"].ToString()),
                    AreaId = (int)reader["AreaId"],
                    Characteristic = reader.IsDBNull(reader.GetOrdinal("Characteristic")) ? string.Empty : reader["Characteristic"].ToString(),
                    CompanyType = (Model.CompanyStructure.CompanyType)Enum.Parse(typeof(Model.CompanyStructure.CompanyType), reader["CompanyType"].ToString()),
                    IndependentGroupPrice = reader.IsDBNull(reader.GetOrdinal("IndependentGroupPrice")) ? 0M : reader.GetDecimal(reader.GetOrdinal("IndependentGroupPrice")),
                    IssueTime = reader.GetDateTime(reader.GetOrdinal("IssueTime"))
                });
            }
            return list;
        }
        /// <summary>
        /// 生成计划文字(如:2011.9-2011.12 共有4个计划)
        /// </summary>
        /// <param name="count"></param>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        private string GetSpjh(int count, DateTime dt1, DateTime dt2)
        {
            if (dt1 != DateTime.MinValue && dt2 != DateTime.MinValue && count > 0)
            {
                object[] objParms = new object[] { 
                    dt2.Year,
                    dt2.Month,
                    dt1.Year,
                    dt1.Month,
                    count
                };
                return string.Format("{0}.{1}-{2}.{3} <br /> 共有{4}个计划", objParms);
            }
            return string.Empty;
        }
        /// <summary>
        /// 生成sql语句
        /// </summary>
        /// <param name="search"></param>
        /// <param name="IsPublic">(只显示签约，收费和认证的专线商线路)</param>
        /// <returns></returns>
        private string GetTableName(MRouteSearch search, bool IsPublic)
        {
            StringBuilder tableName = new StringBuilder();
            tableName.Append("select a.Id,a.RouteId,a.IssueTime,a.StartCity,a.StartCityName,a.B2BName,a.DayNum,a.LateNum,a.IndependentGroupPrice,a.LastUpdateTime,");
            tableName.Append(" b.CompanyName,isnull(b.CompanyLev,0)CompanyLev,b.InfoFull,b.CompanyBrand,b.Introduction,b.ProvinceId as PublishersProvinceId,b.CityId as PublishersCityId,a.Publishers,isnull(c.[Count],0)[Count],isnull(c.RetailAdultPrice,0)RetailAdultPrice,");
            tableName.Append(" isnull(c.RetailChildrenPrice,0)RetailChildrenPrice,c.MaxDate,c.MinDate,a.IsDeleted,");
            tableName.Append(" a.B2B,a.B2BOrder,a.B2C,a.ClickNum,a.RouteSource,a.Status,a.RouteType,a.AreaId,a.Characteristic,isnull(b.CompanyType,0)CompanyType,");
            tableName.Append(" a.RecommendType,(select themeid from tbl_NewRouteThemeControl where RouteId = a.RouteId for xml raw,root('item')) Theme,");
            tableName.Append(" (select CityId from tbl_NewRouteCityControl where RouteId = a.RouteId for xml raw,root('item')) RouteCity,");
            tableName.Append(" PlanInterval = (select PlanInterval + ',' from tbl_NewRouteStandardPlan tb where tb.RouteId = a.RouteId for xml path('')) ");
            tableName.Append(" from tbl_NewRouteBasicInfo a");
            tableName.Append(" left join tbl_CompanyInfo b on a.Publishers = b.Id ");
            tableName.Append(" left join (");
            tableName.Append(" select count(*) [Count],min(RetailAdultPrice) RetailAdultPrice,max(LeaveDate) MaxDate,");
            tableName.Append(" min(RetailChildrenPrice)RetailChildrenPrice,min(LeaveDate) MinDate,RouteId ");
            tableName.Append(" from tbl_NewPowderList where IsDeleted = '0' and LeaveDate >= getdate() ");  //未删除,报名截止日期大于当前日期
            if (search != null)
            {
                if (search.StartDate != DateTime.MinValue)
                {
                    tableName.AppendFormat(" and LeaveDate >= '{0}'", search.StartDate);
                }
                if (search.EndDate != DateTime.MinValue)
                {
                    tableName.AppendFormat(" and LeaveDate <= '{0}'", search.EndDate);
                }
                if (search.LeaveMonth == 3)
                {
                    tableName.AppendFormat(" and YEAR(LeaveDate) = {0} and MONTH(LeaveDate) = {1}", search.Year, search.Month);
                }
            }
            tableName.Append(" group by RouteId ");
            tableName.Append(" ) c on a.RouteId = c.RouteId ");
            if (IsPublic)
            {
                tableName.AppendFormat(" where c.[Count] > 0 AND b.CompanyLev in({0},{1},{2})", (int)Model.CompanyStructure.CompanyLev.认证商户,
                    (int)Model.CompanyStructure.CompanyLev.推荐商户, (int)Model.CompanyStructure.CompanyLev.签约商户);
            }

            return tableName.ToString();
        }

        /// <summary>
        /// 构造搜索条件
        /// </summary>
        /// <param name="search"></param>
        /// <param name="RouteKeyType">搜搜关键字类型</param>
        /// <returns></returns>
        private string GetSQL(MRouteSearch search, int RouteKeyType)
        {
            StringBuilder query = new StringBuilder();
            if (search != null)
            {
                if (search.AreaId > 0)
                {
                    query.AppendFormat(" AND AreaId = {0}", search.AreaId);
                }
                if (search.StartCity > 0)
                {
                    query.AppendFormat(" AND StartCity = {0}", search.StartCity);
                }
                if (!string.IsNullOrEmpty(search.StartCityName))
                {
                    query.AppendFormat(" AND StartCityName like '%{0}%'", search.StartCityName);
                }
                if (search.RouteType.HasValue)
                {
                    query.AppendFormat(" AND RouteType = {0}", (int)search.RouteType);
                }
                if (RouteKeyType > 0 && !string.IsNullOrEmpty(search.RouteKey))
                {
                    #region
                    switch (RouteKeyType)
                    {
                        case 1: //标题+线路特色
                            query.AppendFormat(" AND isnull(B2BName,'') + isnull(Characteristic,'') + isnull(CompanyName,'') LIKE '%{0}%'", search.RouteKey);
                            break;
                        case 2: //标题+途径区域+特色
                            query.AppendFormat(" AND isnull(B2BName,'') + isnull(PlanInterval,'') + isnull(Characteristic,'') LIKE '%{0}%'", search.RouteKey);
                            break;
                        case 3: //标题
                            query.AppendFormat(" AND B2BName like '%{0}%'", search.RouteKey);
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                if (search.RouteStatus.HasValue)
                {
                    query.AppendFormat(" AND Status = {0}", (int)search.RouteStatus);
                }
                if (search.ThemeId > 0)
                {
                    //query.AppendFormat(" AND cast(Theme as xml).exist('/item/row[@themeid=\"{0}\"]') = 1", search.ThemeId);
                    query.AppendFormat(
                        " and exists (select 1 from tbl_NewRouteThemeControl as nrtc where nrtc.RouteId = RouteId and nrtc.themeid = {0}) ",
                        search.ThemeId);
                }
                if (search.RecommendType.HasValue)
                {
                    query.AppendFormat(" AND RecommendType = {0}", (int)search.RecommendType);
                }
                if (!string.IsNullOrEmpty(search.Publishers))
                {
                    query.AppendFormat(" AND Publishers = '{0}'", search.Publishers);
                }
                #region
                switch (search.Price)
                {
                    case PublicCenterPrice._100元以下:
                        query.Append(" AND RetailAdultPrice < 100");
                        break;
                    case PublicCenterPrice._100到300元:
                        query.Append(" AND RetailAdultPrice BETWEEN 100 AND 300");
                        break;
                    case PublicCenterPrice._300到1000元:
                        query.Append("AND RetailAdultPrice BETWEEN 300 AND 1000");
                        break;
                    case PublicCenterPrice._1000到3000元:
                        query.Append(" AND RetailAdultPrice BETWEEN 1000 AND 3000");
                        break;
                    case PublicCenterPrice._3000到10000元:
                        query.Append(" AND RetailAdultPrice BETWEEN 3000 AND 10000");
                        break;
                    case PublicCenterPrice._10000以上:
                        query.AppendFormat(" AND RetailAdultPrice > 10000");
                        break;
                    default:
                        break;
                }
                #endregion
                #region
                switch (search.RouteDay)
                {
                    case PublicCenterRouteDay._1日:
                        query.Append(" AND DayNum = 1");
                        break;
                    case PublicCenterRouteDay._2日:
                        query.Append(" AND DayNum = 2");
                        break;
                    case PublicCenterRouteDay._3日:
                        query.Append(" AND DayNum = 3");
                        break;
                    case PublicCenterRouteDay._4日:
                        query.Append(" AND DayNum = 4");
                        break;
                    case PublicCenterRouteDay._5日:
                        query.Append(" AND DayNum = 5");
                        break;
                    case PublicCenterRouteDay._6日:
                        query.Append(" AND DayNum = 6");
                        break;
                    case PublicCenterRouteDay._7日:
                        query.Append(" AND DayNum = 7");
                        break;
                    case PublicCenterRouteDay._7日以上:
                        query.Append(" AND DayNum > 7");
                        break;
                    default:
                        break;
                }
                #endregion
                if (search.LeaveMonth == 1)
                {
                    query.Append(" AND [Count] = 0");
                }
                if (search.RouteSource.HasValue)
                {
                    query.AppendFormat(" AND RouteSource = {0}", (int?)search.RouteSource);
                }
                if (search.CityId > 0)
                {
                    //地接社的线路销售区域默认是全国，所有只要是地接社添加的线路都显示在前台
                    //query.AppendFormat(" and cast(RouteCity as xml).exist('/item/row[@CityId=\"{0}\"]') = 1 ", search.CityId);
                    query.AppendFormat(
                        " and exists (select 1 from tbl_NewRouteCityControl as nrcc where nrcc.RouteId = RouteId and nrcc.CityId = {0}) ",
                        search.CityId);
                }
                if (search.PublishersProvinceId > 0)
                {
                    query.AppendFormat(" and PublishersProvinceId = {0} ", search.PublishersProvinceId);
                }
                if (search.PublishersCityId > 0)
                {
                    query.AppendFormat(" and PublishersCityId = {0} ", search.PublishersCityId);
                }
            }
            return query.ToString();
        }

        #endregion

        #region internal  生成xml(节点名称跟存储过程处理的名字一样)

        /// <summary>
        /// 返回排序字段
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        internal static string GetOrderBy(int orderBy)
        {
            string order = string.Empty;
            switch (orderBy)
            {
                case 1:
                    order = "CompanyLev DESC,B2B ASC,B2BOrder ASC,InfoFull DESC,LastUpdateTime DESC";
                    break;
                case 2:
                    order = "Status ASC,LastUpdateTime DESC";
                    break;
                case 3:
                    order = "CompanyLev DESC,B2B ASC,B2BOrder ASC,LastUpdateTime DESC";
                    break;
                case 4:
                    order = "TourType DESC"; //散拼计划推荐类型
                    break;
                case 5:
                    order = "LeaveDate DESC";  //散拼计划出团时间
                    break;
                default:
                    break;
            }
            return order;
        }
        /// <summary>
        /// 主题关系
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string ThemeXmlStr(IList<MThemeControl> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder theme = new StringBuilder();
                theme.Append("<item>");
                foreach (MThemeControl item in list)
                {
                    theme.AppendFormat("<themeid>{0}</themeid>", item.ThemeId);
                }
                theme.Append("</item>");
                return theme.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 销售城市
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string CityXmlStr(IList<MCityControl> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder city = new StringBuilder();
                city.Append("<item>");
                foreach (MCityControl item in list)
                {
                    city.AppendFormat("<cityid id=\"{0}\">{1}</cityid>", item.CityId, item.ProvinceId);
                }
                city.Append("</item>");
                return city.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 游览国家
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string BrowseCountryXmlStr(IList<MBrowseCountryControl> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder browseCountry = new StringBuilder();
                browseCountry.Append("<item>");
                foreach (MBrowseCountryControl item in list)
                {
                    browseCountry.AppendFormat("<countryid id=\"{0}\">{1}</countryid>", item.CountryId, Utility.GetBoolToString(item.IsVisa));
                }
                browseCountry.Append("</item>");

                return browseCountry.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 游览城市
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string BrowseCityXmlStr(IList<MBrowseCityControl> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder browseCity = new StringBuilder();
                browseCity.Append("<item>");
                foreach (MBrowseCityControl item in list)
                {
                    browseCity.AppendFormat("<cityid id=\"{0}\">{1}</cityid>", item.CityId, item.CountyId);
                }
                browseCity.Append("</item>");
                return browseCity.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 标准行程
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static string StandardPlanXmlStr(IList<MStandardPlan> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder standardPlan = new StringBuilder();
                standardPlan.Append("<item>");
                foreach (MStandardPlan item in list)
                {
                    standardPlan.Append("<value");
                    standardPlan.AppendFormat(" PlanInterval = \"{0}\"", item.PlanInterval);
                    standardPlan.AppendFormat(" Vehicle = \"{0}\"", (int)item.Vehicle);
                    standardPlan.AppendFormat(" House = \"{0}\"", item.House);
                    standardPlan.AppendFormat(" Early = \"{0}\"", Utility.GetBoolToString(item.Early));
                    standardPlan.AppendFormat(" Center = \"{0}\"", Utility.GetBoolToString(item.Center));
                    standardPlan.AppendFormat(" Late = \"{0}\"", Utility.GetBoolToString(item.Late));
                    standardPlan.AppendFormat(" PlanDay = \"{0}\"", item.PlanDay);
                    standardPlan.AppendFormat(">{0}</value>", Utility.ReplaceXmlSpecialCharacter(item.PlanContent));
                }
                standardPlan.Append("</item>");
                return standardPlan.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
