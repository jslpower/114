using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.TourStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-20
    /// 描述：线路信息数据层
    /// </summary>
    public class RouteBasicInfo:DALBase,IRouteBasicInfo
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region  default constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteBasicInfo() 
        {
            this._database = base.TourStore;
        }
        #endregion

        #region static constants
        private const string SQL_RouteBasicInfo_SELECT = "SELECT ID,CompanyID,CompanyName,OperatorID,ContactName,ContactTel,ContactUserName,ContactMQID,RouteName,RouteDays,LeaveCityId,IssueTime,RouteIssueTypeId,IsAccept,StandardPlanError,TourPriceError,RouteType,AreaID FROM tbl_RouteBasicInfo ";
        private const string SQL_SELECT_GetWholesalersAcceptRouteCount = "SELECT COUNT(*) AS AcceptRouteCount FROM tbl_RouteBasicInfo WHERE IsAccept='1' AND CompanyId=@CompanyId";
        private const string SQL_SELECT_GetWholesalerWaitAcceptRouteCount = "SELECT COUNT(*) AS WaitAcceptRouteCount FROM tbl_RouteSendList WHERE ToCompanyID=@CompanyId AND IsAccept='0' ";
        private const string SQL_SELECT_GetRoutePriceDetails = "SELECT A.PerosonalPrice, A.ChildPrice, A.CompanyPriceStandID,'常规团',A.SysCustomLevel FROM tbl_RouteBasicPriceDetail AS A   WHERE A.RouteBasicID=@RouteId ORDER BY RowMark;SELECT CompanyId FROM tbl_RouteBasicInfo WHERE Id=@RouteId";
        private const string SQL_SELECT_GetRotueInfo_QuickPlan = "SELECT RoutePlan FROM tbl_RouteFastPlan WHERE RouteBasicID=@RouteId";
        private const string SQL_SELECT_GetRouteInfo_StandardPlan = "SELECT  PlanInterval, Vehicle, TrafficNumber, House, Dinner, PlanContent, PlanDay FROM tbl_RouteStandardPlan WHERE RouteBasicID=@RouteId ORDER BY PlanDay";
        private const string SQL_SELECT_GetRouteInfo_ServiceStandard = "SELECT [ResideContent],[DinnerContent],[SightContent],[CarContent],[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice] FROM tbl_RouteServiceStandard WHERE RouteBasicID=@RouteId";
        private const string SQL_SELECT_GetRouteInfo_RouteLeaveCity = "SELECT SiteId FROM tbl_RouteAreaControl  WHERE RouteId=@RouteId";
        private const string SQL_SELECT_GetRouteInfo_RouteSaleCity = "SELECT CityId FROM tbl_RouteCityControl WHERE RouteId=@RouteId";
        private const string SQL_SELECT_GetRouteInfo_RouteTheme = "SELECT ThemeId FROM tbl_RouteThemeControl WHERE RouteId=@RouteId";
        #endregion

        #region private member
        /// <summary>
        /// 创建发送线路信息XML
        /// </summary>
        /// <param name="sendRoutes"></param>
        /// <returns></returns>
        private string CreateSendRoutesXML(IList<EyouSoft.Model.TourStructure.RouteSendList> sendRoutes)
        {
            StringBuilder xmlInfo = new StringBuilder();

            xmlInfo.Append("<ROOT>");

            foreach (EyouSoft.Model.TourStructure.RouteSendList sendRuoteInfo in sendRoutes)
            {
                xmlInfo.AppendFormat("<SendRouteInfo FromCompanyId=\"{0}\" FromOperatorID=\"{1}\" ToCompanyId=\"{2}\" RouteID=\"{3}\" />"
                    , sendRuoteInfo.FromCompanyID.ToString()
                    , sendRuoteInfo.FromOperatorID.ToString()
                    , sendRuoteInfo.ToCompanyID.ToString()
                    , sendRuoteInfo.RouteID);
            }

            xmlInfo.Append("</ROOT>");

            return xmlInfo.ToString();
        }
        /// <summary>
        /// 创建线路价格明细XML
        /// </summary>
        /// <param name="price">价格明细信息业务实体集合</param>
        /// <returns></returns>
        private string CreateRoutePriceDetailsXML(IList<EyouSoft.Model.TourStructure.RoutePriceDetail> price)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");
            
            if (price != null && price.Count > 0)
            {
                int rowMark = 0;
                foreach (EyouSoft.Model.TourStructure.RoutePriceDetail tmp in price)
                {
                    if (tmp.PriceDetail != null && tmp.PriceDetail.Count > 0)
                    {
                        foreach (EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail tmp1 in tmp.PriceDetail)
                        {
                           // xmlDoc.AppendFormat("<PriceInfo AdultPrice=\"{0}\" ChildrenPrice=\"{1}\" PriceStandId=\"{2}\" CustomerLevelType=\"{3}\" CustomerLevelId=\"{4}\" RowMark=\"{5}\" />"
                              xmlDoc.AppendFormat("<PriceInfo AdultPrice=\"{0}\" ChildrenPrice=\"{1}\" PriceStandId=\"{2}\"  CustomerLevelId=\"{3}\" RowMark=\"{4}\" />"
                                , tmp1.AdultPrice
                                , tmp1.ChildrenPrice
                                , tmp.PriceStandId
                                , tmp1.CustomerLevelId
                                , rowMark++);
                        }
                    }
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }
        /// <summary>
        /// 创建标准线路行程内容XML
        /// </summary>
        /// <param name="plans"></param>
        /// <returns></returns>
        private string CreateRouteStandardPlansXML(IList<EyouSoft.Model.TourStructure.RouteStandardPlan> plans)
        {
            StringBuilder xmlInfo = new StringBuilder();

            xmlInfo.Append("<ROOT>");

            foreach (EyouSoft.Model.TourStructure.RouteStandardPlan planInfo in plans)
            {
                xmlInfo.AppendFormat("<PalnInfo PlanInterval=\"{0}\" Vehicle=\"{1}\" TrafficNumber=\"{2}\" House=\"{3}\" Dinner=\"{4}\" PlanContent=\"{5}\" PlanDay=\"{6}\" PlanId=\"{7}\"  />"
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.PlanInterval)
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.Vehicle)
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.TrafficNumber)
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.House)
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.Dinner)
                    , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(planInfo.PlanContent)
                    , planInfo.PlanDay.ToString()
                    , Guid.NewGuid().ToString());
            }

            xmlInfo.Append("</ROOT>");

            return xmlInfo.ToString();
        }
        /*
        /// <summary>
        /// 创建出港城市XMLDocument 
        /// </summary>
        /// <param name="leaveCity">出港城市集合</param>
        /// <returns>System.String</returns>
        protected string CreateLeaveCityXML(IList<int> leaveCity)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (leaveCity != null && leaveCity.Count > 0)
            {
                foreach (int tmp in leaveCity)
                {
                    xmlDoc.AppendFormat("<LeaveCityInfo CityId=\"{0}\">", tmp.ToString());
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }
        */
        /// <summary>
        /// 创建销售城市XMLDocument 
        /// </summary>
        /// <param name="saleCity">销售城市集合</param>
        /// <returns>System.String</returns>
        protected string CreateSaleCityXML(IList<int> saleCity)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (saleCity != null && saleCity.Count > 0)
            {
                foreach (int tmp in saleCity)
                {
                    xmlDoc.AppendFormat("<SaleCityInfo CityId=\"{0}\" />", tmp.ToString());
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建线路主题XMLDocument 
        /// </summary>
        /// <param name="routeTheme">线路主题信息集合</param>
        /// <returns>System.String</returns>
        protected string CreateRouteThemeXML(IList<int> routeTheme)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (routeTheme != null && routeTheme.Count > 0)
            {
                foreach (int tmp in routeTheme)
                {
                    xmlDoc.AppendFormat("<ThemeInfo ThemeId=\"{0}\" />", tmp.ToString());
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 验证是否存在同名线路
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="RouteName">线路名称</param>
        /// <returns></returns>
        public virtual bool Exists(string CompanyId, string RouteName)
        {
            string strSql = "select count(0) from tbl_RouteBasicInfo WHERE CompanyId=@CompanyId AND RouteName=@RouteName";
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, CompanyId);
            this._database.AddInParameter(dc, "RouteName", DbType.String, RouteName);
            return DbHelper.Exists(dc, this._database);
        }
        /// <summary>
        /// 写入标准发布线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool InsertRouteInfoByStandard(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_RouteBasicInfo_AddRouteInfo");
            this._database.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeInfo.ID);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, routeInfo.CompanyID);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, routeInfo.CompanyName);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, routeInfo.OperatorID);
            this._database.AddInParameter(dc, "ContactName", DbType.String, routeInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTelephone", DbType.String, routeInfo.ContactTel);
            this._database.AddInParameter(dc, "ContactUserName", DbType.String, routeInfo.ContactUserName);
            this._database.AddInParameter(dc, "ContactMQId", DbType.String, routeInfo.ContactMQID);
            this._database.AddInParameter(dc, "RouteName", DbType.String, routeInfo.RouteName);
            this._database.AddInParameter(dc, "RouteDays", DbType.Int32, routeInfo.TourDays);
            this._database.AddInParameter(dc, "IssueTime", DbType.String, routeInfo.IssueTime);
            this._database.AddInParameter(dc, "AreaId", DbType.Int32, routeInfo.AreaId);
            this._database.AddInParameter(dc, "RouteType", DbType.Byte, (int)routeInfo.AreaType);
            this._database.AddInParameter(dc, "LeaveCity", DbType.Int32, routeInfo.LeaveCityId);
            this._database.AddInParameter(dc, "SaleCity", DbType.String, this.CreateSaleCityXML(routeInfo.SaleCity));
            this._database.AddInParameter(dc, "RouteTheme", DbType.String, this.CreateRouteThemeXML(routeInfo.RouteTheme));
            this._database.AddInParameter(dc, "ReleaseType", DbType.String, (int)routeInfo.ReleaseType);
            this._database.AddInParameter(dc, "PriceDetails", DbType.String, this.CreateRoutePriceDetailsXML(routeInfo.PriceDetails));
            this._database.AddInParameter(dc, "StandardPlan", DbType.String, this.CreateRouteStandardPlansXML(routeInfo.StandardPlans));
            this._database.AddInParameter(dc, "ResideContent", DbType.String, routeInfo.ServiceStandard.ResideContent);
            this._database.AddInParameter(dc, "DinnerContent", DbType.String, routeInfo.ServiceStandard.DinnerContent);
            this._database.AddInParameter(dc, "SightContent", DbType.String, routeInfo.ServiceStandard.SightContent);
            this._database.AddInParameter(dc, "CarContent", DbType.String, routeInfo.ServiceStandard.CarContent);
            this._database.AddInParameter(dc, "GuideContent", DbType.String, routeInfo.ServiceStandard.GuideContent);
            this._database.AddInParameter(dc, "TrafficContent", DbType.String, routeInfo.ServiceStandard.TrafficContent);
            this._database.AddInParameter(dc, "IncludeOtherContent", DbType.String, routeInfo.ServiceStandard.IncludeOtherContent);
            this._database.AddInParameter(dc, "NotContainService", DbType.String, routeInfo.ServiceStandard.NotContainService);
            /*this._database.AddInParameter(dc, "ExpenseItem", DbType.String, routeInfo.ServiceStandard.ExpenseItem);
            this._database.AddInParameter(dc, "ChildrenInfo", DbType.String, routeInfo.ServiceStandard.ChildrenInfo);
            this._database.AddInParameter(dc, "ShoppingInfo", DbType.String, routeInfo.ServiceStandard.ShoppingInfo);
            this._database.AddInParameter(dc, "GiftInfo", DbType.String, routeInfo.ServiceStandard.GiftInfo);
            this._database.AddInParameter(dc, "NoticeProceeding", DbType.String, routeInfo.ServiceStandard.NoticeProceeding);*/
            this._database.AddInParameter(dc, "SpeciallyNotice", DbType.String, routeInfo.ServiceStandard.SpeciallyNotice);
            this._database.AddOutParameter(dc, "Result", DbType.Int32, 50);

            DbHelper.ExecuteSql(dc, this._database);

            return Convert.ToInt32(this._database.GetParameterValue(dc, "Result"))>0?true:false;           
        }

        /// <summary>
        /// 写入快速发布线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool InsertRouteInfoByQuick(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_RouteBasicInfo_AddRouteInfo");
            this._database.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeInfo.ID);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, routeInfo.CompanyID);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, routeInfo.CompanyName);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, routeInfo.OperatorID);
            this._database.AddInParameter(dc, "ContactName", DbType.String, routeInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTelephone", DbType.String, routeInfo.ContactTel);
            this._database.AddInParameter(dc, "ContactUserName", DbType.String, routeInfo.ContactUserName);
            this._database.AddInParameter(dc, "ContactMQId", DbType.String, routeInfo.ContactMQID);
            this._database.AddInParameter(dc, "RouteName", DbType.String, routeInfo.RouteName);
            this._database.AddInParameter(dc, "RouteDays", DbType.Int32, routeInfo.TourDays);
            this._database.AddInParameter(dc, "IssueTime", DbType.String, routeInfo.IssueTime);
            this._database.AddInParameter(dc, "AreaId", DbType.Int32, routeInfo.AreaId);
            this._database.AddInParameter(dc, "RouteType", DbType.Byte, (int)routeInfo.AreaType);   
            this._database.AddInParameter(dc, "LeaveCity", DbType.Int32, routeInfo.LeaveCityId);
            this._database.AddInParameter(dc, "SaleCity", DbType.String, this.CreateSaleCityXML(routeInfo.SaleCity));
            this._database.AddInParameter(dc, "RouteTheme", DbType.String, this.CreateRouteThemeXML(routeInfo.RouteTheme));
            this._database.AddInParameter(dc, "ReleaseType", DbType.String, (int)routeInfo.ReleaseType);
            this._database.AddInParameter(dc, "PriceDetails", DbType.String, this.CreateRoutePriceDetailsXML(routeInfo.PriceDetails));
            this._database.AddInParameter(dc, "QuickPlan", DbType.String, routeInfo.QuickPlan);
            this._database.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(dc, this._database);

            return Convert.ToInt32(this._database.GetParameterValue(dc, "Result"))>0?true:false;
        }

        /// <summary>
        /// 更新标准发布的线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateRouteInfoByStandard(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_RouteBasicInfo_UpdateRouteInfo");
            this._database.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeInfo.ID);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, routeInfo.CompanyID);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, routeInfo.CompanyName);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, routeInfo.OperatorID);
            this._database.AddInParameter(dc, "ContactName", DbType.String, routeInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTelephone", DbType.String, routeInfo.ContactTel);
            this._database.AddInParameter(dc, "ContactUserName", DbType.String, routeInfo.ContactUserName);
            this._database.AddInParameter(dc, "ContactMQId", DbType.String, routeInfo.ContactMQID);
            this._database.AddInParameter(dc, "RouteName", DbType.String, routeInfo.RouteName);
            this._database.AddInParameter(dc, "RouteDays", DbType.Int32, routeInfo.TourDays);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, routeInfo.IssueTime);
            this._database.AddInParameter(dc, "AreaId", DbType.Int32, routeInfo.AreaId);
            this._database.AddInParameter(dc, "RouteType", DbType.Byte, (int)routeInfo.AreaType);
            this._database.AddInParameter(dc, "LeaveCity", DbType.Int32, routeInfo.LeaveCityId);
            this._database.AddInParameter(dc, "SaleCity", DbType.String, this.CreateSaleCityXML(routeInfo.SaleCity));
            this._database.AddInParameter(dc, "RouteTheme", DbType.String, this.CreateRouteThemeXML(routeInfo.RouteTheme));
            this._database.AddInParameter(dc, "ReleaseType", DbType.String, (int)routeInfo.ReleaseType);
            this._database.AddInParameter(dc, "PriceDetails", DbType.String, this.CreateRoutePriceDetailsXML(routeInfo.PriceDetails));
            this._database.AddInParameter(dc, "StandardPlan", DbType.String, this.CreateRouteStandardPlansXML(routeInfo.StandardPlans));
            this._database.AddInParameter(dc, "ResideContent", DbType.String, routeInfo.ServiceStandard.ResideContent);
            this._database.AddInParameter(dc, "DinnerContent", DbType.String, routeInfo.ServiceStandard.DinnerContent);
            this._database.AddInParameter(dc, "SightContent", DbType.String, routeInfo.ServiceStandard.SightContent);
            this._database.AddInParameter(dc, "CarContent", DbType.String, routeInfo.ServiceStandard.CarContent);
            this._database.AddInParameter(dc, "GuideContent", DbType.String, routeInfo.ServiceStandard.GuideContent);
            this._database.AddInParameter(dc, "TrafficContent", DbType.String, routeInfo.ServiceStandard.TrafficContent);
            this._database.AddInParameter(dc, "IncludeOtherContent", DbType.String, routeInfo.ServiceStandard.IncludeOtherContent);
            this._database.AddInParameter(dc, "NotContainService", DbType.String, routeInfo.ServiceStandard.NotContainService);
            /*this._database.AddInParameter(dc, "ExpenseItem", DbType.String, routeInfo.ServiceStandard.ExpenseItem);
            this._database.AddInParameter(dc, "ChildrenInfo", DbType.String, routeInfo.ServiceStandard.ChildrenInfo);
            this._database.AddInParameter(dc, "ShoppingInfo", DbType.String, routeInfo.ServiceStandard.ShoppingInfo);
            this._database.AddInParameter(dc, "GiftInfo", DbType.String, routeInfo.ServiceStandard.GiftInfo);
            this._database.AddInParameter(dc, "NoticeProceeding", DbType.String, routeInfo.ServiceStandard.NoticeProceeding);*/
            this._database.AddInParameter(dc, "SpeciallyNotice", DbType.String, routeInfo.ServiceStandard.SpeciallyNotice);
            this._database.AddOutParameter(dc, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(dc, this._database);

            return Convert.ToInt32(this._database.GetParameterValue(dc, "Result"))>0?true:false;
        }

        /// <summary>
        /// 更新快速发布的线路信息
        /// </summary>
        /// <param name="routeInfo">线路信息业务实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateRouteInfoByQuick(EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_RouteBasicInfo_UpdateRouteInfo");
            this._database.AddInParameter(dc, "RouteId", DbType.AnsiStringFixedLength, routeInfo.ID);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, routeInfo.CompanyID);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, routeInfo.CompanyName);
            this._database.AddInParameter(dc, "UserId", DbType.AnsiStringFixedLength, routeInfo.OperatorID);
            this._database.AddInParameter(dc, "ContactName", DbType.String, routeInfo.ContactName);
            this._database.AddInParameter(dc, "ContactTelephone", DbType.String, routeInfo.ContactTel);
            this._database.AddInParameter(dc, "ContactUserName", DbType.String, routeInfo.ContactUserName);
            this._database.AddInParameter(dc, "ContactMQId", DbType.String, routeInfo.ContactMQID);
            this._database.AddInParameter(dc, "RouteName", DbType.String, routeInfo.RouteName);
            this._database.AddInParameter(dc, "RouteDays", DbType.Int32, routeInfo.TourDays);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, routeInfo.IssueTime);
            this._database.AddInParameter(dc, "AreaId", DbType.Int32, routeInfo.AreaId);
            this._database.AddInParameter(dc, "RouteType", DbType.Byte, (int)routeInfo.AreaType);
            this._database.AddInParameter(dc, "LeaveCity", DbType.Int32, routeInfo.LeaveCityId);
            this._database.AddInParameter(dc, "SaleCity", DbType.String, this.CreateSaleCityXML(routeInfo.SaleCity));
            this._database.AddInParameter(dc, "RouteTheme", DbType.String, this.CreateRouteThemeXML(routeInfo.RouteTheme));
            this._database.AddInParameter(dc, "ReleaseType", DbType.String, (int)routeInfo.ReleaseType);
            this._database.AddInParameter(dc, "PriceDetails", DbType.String, this.CreateRoutePriceDetailsXML(routeInfo.PriceDetails));
            this._database.AddInParameter(dc, "QuickPlan", DbType.String, routeInfo.QuickPlan);
            this._database.AddOutParameter(dc, "Result", DbType.Int32, 50);

            DbHelper.ExecuteSql(dc, this._database);

            return Convert.ToInt32(this._database.GetParameterValue(dc, "Result"))>0?true:false;
        }

        /// <summary>
        /// 发送线路给专线商
        /// </summary>
        /// <param name="sendRoutes">发送线路信息业务实体</param>
        /// <returns>返回发送成功的总数</returns>
        public virtual int SendRouteToWholesalers(IList<EyouSoft.Model.TourStructure.RouteSendList> sendRoutes)
        {
             DbCommand  dc=this._database.GetStoredProcCommand("proc_RouteSendList_ADD"); 
             this._database.AddInParameter(dc,"SendRoutes",DbType.String,CreateSendRoutesXML(sendRoutes));
             this._database.AddOutParameter(dc,"Result",DbType.Int32,4);
             DbHelper.RunProcedure(dc,this._database);
             return int.Parse(this._database.GetParameterValue(dc,"Result").ToString());
        }

        /// <summary>
        /// 获取标准版线路信息实体
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TourStructure.RouteBasicInfo GetRouteInfo(string routeId)
        {
            EyouSoft.Model.TourStructure.RouteBasicInfo model=null;

            #region 线路基本信息
            DbCommand dc=this._database.GetSqlStringCommand(SQL_RouteBasicInfo_SELECT+" WHERE ID=@ID ");
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, routeId);
            using(IDataReader dr=DbHelper.ExecuteReader(dc,this._database))
            {
                if(dr.Read())
                {
                    model=new EyouSoft.Model.TourStructure.RouteBasicInfo();
                    model.CompanyID=dr.GetString(dr.GetOrdinal("CompanyID"));
                    model.CompanyName=dr.IsDBNull(dr.GetOrdinal("CompanyName"))?string.Empty:dr.GetString(dr.GetOrdinal("CompanyName"));
                    model.ContactMQID=dr.IsDBNull(dr.GetOrdinal("ContactMQID"))?string.Empty:dr.GetString(dr.GetOrdinal("ContactMQID"));
                    model.ContactName=dr.IsDBNull(dr.GetOrdinal("ContactName"))?string.Empty:dr.GetString(dr.GetOrdinal("ContactName"));
                    model.ContactTel=dr.IsDBNull(dr.GetOrdinal("ContactTel"))?string.Empty:dr.GetString(dr.GetOrdinal("ContactTel"));
                    model.ContactUserName=dr.IsDBNull(dr.GetOrdinal("ContactUserName"))?string.Empty:dr.GetString(dr.GetOrdinal("ContactUserName"));
                    model.ID=dr.GetString(dr.GetOrdinal("ID"));
                    model.IsAccept = dr.GetString(dr.GetOrdinal("IsAccept")) == "1" ? true : false;
                    model.IssueTime=dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.OperatorID=dr.GetString(dr.GetOrdinal("OperatorID"));
                    model.TourDays = dr.GetInt32(dr.GetOrdinal("RouteDays"));
                    model.LeaveCityId = dr.GetInt32(dr.GetOrdinal("LeaveCityId"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RouteIssueTypeId")))
                        model.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)dr.GetInt32(dr.GetOrdinal("RouteIssueTypeId"));
                    model.RouteName=dr.GetString(dr.GetOrdinal("RouteName"));
                    model.StandardPlanError=dr.GetInt32(dr.GetOrdinal("StandardPlanError"));
                    model.TourPriceError=dr.GetInt32(dr.GetOrdinal("TourPriceError"));
                    model.AreaId = dr.GetInt32(dr.GetOrdinal("AreaID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("RouteType")))
                        model.AreaType = (EyouSoft.Model.SystemStructure.AreaType)int.Parse(dr.GetByte(dr.GetOrdinal("RouteType")).ToString());
                }
            }

            if (model == null) { return null; }
            #endregion

            #region 线路报价信息
            string companyId = string.Empty;
            model.PriceDetails = this.GetRoutePriceDetails(model.ID,out companyId);
            #endregion

            #region 线路行程及服务标准信息
            if (model.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
            {
                dc.Parameters.Clear();
                dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRotueInfo_QuickPlan);
                this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

                using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
                {
                    if (rdr.Read())
                    {
                        model.QuickPlan = rdr[0].ToString();
                    }
                }
            }
            else
            {
                model.ServiceStandard = new EyouSoft.Model.TourStructure.RouteServiceStandard();
                dc.Parameters.Clear();
                dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRouteInfo_ServiceStandard);
                this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

                using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
                {
                    if (rdr.Read())
                    {
                        model.ServiceStandard.ResideContent = rdr[0].ToString();
                        model.ServiceStandard.DinnerContent = rdr[1].ToString();
                        model.ServiceStandard.SightContent = rdr[2].ToString();
                        model.ServiceStandard.CarContent = rdr[3].ToString();
                        model.ServiceStandard.GuideContent = rdr[4].ToString();
                        model.ServiceStandard.TrafficContent = rdr[5].ToString();
                        model.ServiceStandard.IncludeOtherContent = rdr[6].ToString();
                        model.ServiceStandard.NotContainService = rdr[7].ToString();
                        /*model.ServiceStandard.ExpenseItem = rdr[8].ToString();
                        model.ServiceStandard.ChildrenInfo = rdr[9].ToString();
                        model.ServiceStandard.ShoppingInfo = rdr[10].ToString();
                        model.ServiceStandard.GiftInfo = rdr[11].ToString();
                        model.ServiceStandard.NoticeProceeding = rdr[12].ToString();*/
                        model.ServiceStandard.SpeciallyNotice = rdr[8].ToString();
                    }
                }


                model.StandardPlans = new List<EyouSoft.Model.TourStructure.RouteStandardPlan>();

                dc.Parameters.Clear();
                dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRouteInfo_StandardPlan);
                this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

                using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
                {
                    while (rdr.Read())
                    {
                        EyouSoft.Model.TourStructure.RouteStandardPlan tmp = new EyouSoft.Model.TourStructure.RouteStandardPlan();

                        tmp.PlanInterval = rdr[0].ToString();
                        tmp.Vehicle = rdr[1].ToString();
                        tmp.TrafficNumber = rdr[2].ToString();
                        tmp.House = rdr[3].ToString();
                        tmp.Dinner = rdr[4].ToString();
                        tmp.PlanContent = rdr[5].ToString();
                        tmp.PlanDay = rdr.GetInt32(6);

                        model.StandardPlans.Add(tmp);
                    }
                }
            }
            #endregion

            /*#region 出港城市
            dc.Parameters.Clear();
            dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRouteInfo_RouteLeaveCity);
            this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                model.LeaveCity = new List<int>();
                while (rdr.Read())
                {
                    model.LeaveCity.Add(rdr.GetInt32(0));
                }
            }
            #endregion*/

            #region 销售城市
            dc.Parameters.Clear();
            dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRouteInfo_RouteSaleCity);
            this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                model.SaleCity = new List<int>();
                while (rdr.Read())
                {
                    model.SaleCity.Add(rdr.GetInt32(0));
                }
            }
            #endregion

            #region 线路主题
            dc.Parameters.Clear();
            dc = this._database.GetSqlStringCommand(SQL_SELECT_GetRouteInfo_RouteTheme);
            this._database.AddInParameter(dc, "RouteId", DbType.String, model.ID);

            using (IDataReader rdr = DbHelper.ExecuteReader(dc, this._database))
            {
                model.RouteTheme = new List<int>();
                while (rdr.Read())
                {
                    model.RouteTheme.Add(rdr.GetInt32(0));
                }
            }
            #endregion

            return model;
        }

        /// <summary>
        /// 根据指定条件获取线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =0时返回公司所有用户发布的线路 >0时返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <param name="ReleaseType">线路发布类型 =null返回全部</param>
        /// <param name="AreaType">线路区域类型 =null返回全部</param>
        /// <returns></returns> 
        public virtual IList<EyouSoft.Model.TourStructure.RouteBasicInfo> GetRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName, int[] AreaId, EyouSoft.Model.TourStructure.ReleaseType? ReleaseType,EyouSoft.Model.SystemStructure.AreaType? AreaType)
        {
           IList<EyouSoft.Model.TourStructure.RouteBasicInfo> list=new List<EyouSoft.Model.TourStructure.RouteBasicInfo>();
           StringBuilder cmdQuery = new StringBuilder();
           string tableName = "tbl_RouteBasicInfo";
           string primaryKey = "Id";
           string orderByString = "IssueTime DESC";
           string fields = " ID, CompanyID, CompanyName, OperatorID, ContactName, ContactTel, ContactUserName, ContactMQID, RouteName, RouteDays, IssueTime,  RouteIssueTypeId, IsAccept";

           cmdQuery.AppendFormat("CompanyId='{0}' AND IsDelete='0' ", companyId);

           if(!string.IsNullOrEmpty(userId))  //获取某个用户的所有
           {
               cmdQuery.AppendFormat(" AND (OperatorID='{0}') ", userId);
           }

           if (!string.IsNullOrEmpty(routeName))
           {
               cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
           }

           if (routeDays > 0)
           {
               cmdQuery.AppendFormat(" AND RouteDays={0} ", routeDays);
           }

           if (!string.IsNullOrEmpty(contactName))
           {
               cmdQuery.AppendFormat(" AND ContactName LIKE '%{0}%' ", contactName);
           }
           if (AreaId != null && AreaId.Length > 0)
           {    
                string AreaIDs=string.Empty;
                foreach(int aid in AreaId)
                {
                    AreaIDs+=aid.ToString()+",";
                }
                AreaIDs=AreaIDs.Substring(0,AreaIDs.Length-1);
                cmdQuery.AppendFormat(" and (areaid=0 or areaid in({0}))", AreaIDs);
           }
           if (ReleaseType != null)
           {
               cmdQuery.AppendFormat(" and RouteIssueTypeId='{0}' ", ((int)ReleaseType).ToString());
           }
           if (AreaType.HasValue)
           {
               cmdQuery.AppendFormat(" and RouteType={0} ", (int)AreaType.Value);
           }
           else
           {
               cmdQuery.AppendFormat(" and RouteType<>{0} ", (int)EyouSoft.Model.SystemStructure.AreaType.地接线路);
           }
           using (IDataReader dr = DbHelper.ExecuteReader(this._database,pageSize,pageIndex,ref recordCount,tableName,primaryKey,fields,cmdQuery.ToString(),orderByString))
           {
               while(dr.Read())
               {
                   EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo = new EyouSoft.Model.TourStructure.RouteBasicInfo();

                   routeInfo.ID = dr.IsDBNull(dr.GetOrdinal("Id")) ? "" : dr["Id"].ToString();
                   routeInfo.CompanyID = dr.IsDBNull(dr.GetOrdinal("CompanyID")) ? "" : dr["CompanyID"].ToString();
                   routeInfo.CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? "" : dr["CompanyName"].ToString();
                   routeInfo.OperatorID = dr.IsDBNull(dr.GetOrdinal("OperatorID")) ? "" : dr["OperatorID"].ToString();
                   routeInfo.ContactName = dr.IsDBNull(dr.GetOrdinal("ContactName")) ? "" : dr["ContactName"].ToString();
                   routeInfo.ContactTel = dr.IsDBNull(dr.GetOrdinal("ContactTel")) ? "" : dr["ContactTel"].ToString();
                   routeInfo.ContactUserName = dr.IsDBNull(dr.GetOrdinal("ContactUserName")) ? "" : dr["ContactUserName"].ToString();
                   routeInfo.ContactMQID = dr.IsDBNull(dr.GetOrdinal("ContactMQID")) ? "" : dr["ContactMQID"].ToString();
                   routeInfo.RouteName = dr.IsDBNull(dr.GetOrdinal("RouteName")) ? "" : dr["RouteName"].ToString();
                   routeInfo.TourDays = dr.IsDBNull(dr.GetOrdinal("RouteDays")) ? 0 : int.Parse(dr["RouteDays"].ToString());
                   routeInfo.IssueTime = dr.IsDBNull(dr.GetOrdinal("IssueTime")) ? DateTime.MinValue : DateTime.Parse(dr["IssueTime"].ToString());
                   if (!dr.IsDBNull(dr.GetOrdinal("RouteIssueTypeId")))
                     routeInfo.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(dr["RouteIssueTypeId"].ToString());
                   #region 线路报价信息
                   string cId = string.Empty;
                   routeInfo.PriceDetails = this.GetRoutePriceDetails(routeInfo.ID,out cId);
                   #endregion

                   list.Add(routeInfo);
                   routeInfo = null;
               }
           }
           return list;
        }

        /// <summary>
        /// 获取专线已接收线路数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetWholesalersAcceptRouteCount(string companyId)
        {
            int acceptRouteCount = 0;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_SELECT_GetWholesalersAcceptRouteCount);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(dc,this._database))
            {
                if (rdr.Read())
                {
                    acceptRouteCount = rdr.GetInt32(0);
                }
            }

            return acceptRouteCount;
        }

        /// <summary>
        /// 根据指定条件获取专线已接收的线路信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当面页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 =0时返回公司所有用户发布的线路 >0时返回指定用户发布的线路</param>
        /// <param name="routeName">线路名称 [为空时不做为查询条件]</param>
        /// <param name="routeDays">线路天数 [为0时不做为查询条件]</param>
        /// <param name="contactName">线路联系人 [为空时不做为查询条件]</param>
        /// <param name="AreaId">线路区域ID集合</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.RouteBasicInfo> GetWholesalersAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId, string routeName, int routeDays, string contactName,int[] AreaId)
        {
            IList<EyouSoft.Model.TourStructure.RouteBasicInfo> routes = new List<EyouSoft.Model.TourStructure.RouteBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_RouteBasicInfo";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            string fields = " ID, CompanyID, CompanyName, OperatorID, ContactName, ContactTel, ContactUserName, ContactMQID, RouteName, RouteDays, IssueTime,  RouteIssueTypeId, IsAccept";

            cmdQuery.AppendFormat("CompanyId='{0}'", companyId);
            cmdQuery.Append(" AND IsAccept='1' ");

            if (!string.IsNullOrEmpty(userId))  //获取某个用户的所有话题
            {
                cmdQuery.AppendFormat(" AND (OperatorID='{0}') ", userId.ToString());
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (routeDays > 0)
            {
                cmdQuery.AppendFormat(" AND RotueDays={0} ", routeDays);
            }

            if (!string.IsNullOrEmpty(contactName))
            {
                cmdQuery.AppendFormat(" AND ContactName LIKE '%{0}%' ", contactName);
            }
            if (AreaId.Length > 0)
            {
                string AreaIDs = string.Empty;
                foreach (int aid in AreaId)
                {
                    AreaIDs += aid.ToString() + ",";
                }
                AreaIDs = AreaIDs.Substring(0, AreaIDs.Length - 1);
                cmdQuery.AppendFormat(" and (areaid=0 or areaid in({0}))", AreaIDs);
            }
            using (IDataReader rdr = DbHelper.ExecuteReader(this._database,pageSize, pageIndex,ref recordCount,tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.RouteBasicInfo routeInfo = new EyouSoft.Model.TourStructure.RouteBasicInfo();

                    routeInfo.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    routeInfo.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    routeInfo.CompanyName = rdr["CompanyName"].ToString();
                    routeInfo.OperatorID = rdr.GetString(rdr.GetOrdinal("OperatorID"));
                    routeInfo.ContactName = rdr["ContactName"].ToString();
                    routeInfo.ContactTel = rdr["ContactTel"].ToString();
                    routeInfo.ContactUserName = rdr["ContactUserName"].ToString();
                    routeInfo.ContactMQID = rdr.GetString(rdr.GetOrdinal("ContactMQID"));
                    routeInfo.RouteName = rdr["RouteName"].ToString();
                    routeInfo.TourDays = rdr.GetInt32(rdr.GetOrdinal("RouteDays"));
                    routeInfo.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RouteIssueTypeId")))
                        routeInfo.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(rdr.GetString(rdr.GetOrdinal("RouteIssueTypeId")));

                    routes.Add(routeInfo);
                }
            }

            return routes;
        }

        /// <summary>
        /// 获取线路的报价详细信息集合
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="CompanyId">发布线路的公司</param>
        /// <returns></returns>
        public virtual  IList<EyouSoft.Model.TourStructure.RoutePriceDetail> GetRoutePriceDetails(string routeId,out string CompanyId)
        {
            CompanyId = "";
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetRoutePriceDetails);
            base.TourStore.AddInParameter(cmd, "RouteId", DbType.AnsiStringFixedLength, routeId);
            List<EyouSoft.Model.TourStructure.RoutePriceDetail> prices = new List<EyouSoft.Model.TourStructure.RoutePriceDetail>();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.RoutePriceDetail price = prices.FindLast((EyouSoft.Model.TourStructure.RoutePriceDetail tmp) =>
                    {
                        if (rdr.GetString(2) == tmp.PriceStandId)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                    
                    price = price ?? new EyouSoft.Model.TourStructure.RoutePriceDetail();

                    EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail tmp1 = new EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail();
                    tmp1.CustomerLevelId = rdr.GetInt32(4);
                    //tmp1.CustomerLevelType = (EyouSoft.Model.CompanyStructure.CustomerLevelType)rdr.GetByte(3);
                    tmp1.AdultPrice = rdr.GetDecimal(0);
                    tmp1.ChildrenPrice = rdr.GetDecimal(1);

                    if (string.IsNullOrEmpty(price.PriceStandId))
                    {
                        price.PriceStandId = rdr.GetString(2);
                        price.PriceStandName = rdr[3].ToString();

                        price.PriceDetail = new List<EyouSoft.Model.TourStructure.RoutePriceCustomerLeaveDetail>();

                        price.PriceDetail.Add(tmp1);

                        prices.Add(price);
                    }
                    else
                    {
                        price.PriceDetail.Add(tmp1);
                    }
                }
                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        CompanyId = rdr[0].ToString();
                    }
                }
            }

            return prices;
        }

        /// <summary>
        /// 获取批发商待接收线路集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeName">线路名称</param>
        /// <param name="routeDays">行程天数</param>
        /// <param name="contactName">联系人</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo> GetWholesalerWaitAcceptRoutes(int pageSize, int pageIndex, ref int recordCount, string companyId, string routeName, int routeDays, string contactName)
        {
            IList<EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo> routes = new List<EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "dt_WholesalerWaitAcceptRoutes";
            string primaryKey = "Id";
            string orderByString = "SendRouteTime DESC";
            string fields = "WaitAcceptId, SendRouteTime, ID, CompanyID, CompanyName, OperatorID,  ContactName, ContactTel, ContactUserName, ContactMQID, RouteName, RouteDays, IssueTime, RouteIssueTypeId, IsAccept";

            cmdQuery.AppendFormat("ToCompanyID='{0}'", companyId);

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (routeDays > 0)
            {
                cmdQuery.AppendFormat(" AND RouteDays={0} ", routeDays);
            }

            if (!string.IsNullOrEmpty(contactName))
            {
                cmdQuery.AppendFormat(" AND ContactName LIKE '%{0}%' ", contactName);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(this._database,pageSize, pageIndex,ref recordCount,tableName,primaryKey,fields,cmdQuery.ToString(),orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo routeInfo = new EyouSoft.Model.TourStructure.WholesalerWaitAcceptRouteInfo();

                    routeInfo.WaitAcceptId = rdr.GetString(rdr.GetOrdinal("WaitAcceptId"));
                    routeInfo.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    routeInfo.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyID"));
                    routeInfo.CompanyName = rdr["CompanyName"].ToString();
                    routeInfo.OperatorID = rdr.GetString(rdr.GetOrdinal("OperatorID"));
                    routeInfo.ContactName = rdr["ContactName"].ToString();
                    routeInfo.ContactTel = rdr["ContactTel"].ToString();
                    routeInfo.ContactUserName = rdr["ContactUserName"].ToString();
                    routeInfo.ContactMQID = rdr.GetString(rdr.GetOrdinal("ContactMQID"));
                    routeInfo.RouteName = rdr["RouteName"].ToString();
                    routeInfo.TourDays = rdr.GetInt32(rdr.GetOrdinal("RouteDays"));
                    routeInfo.IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("RouteIssueTypeId")))
                        routeInfo.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse( rdr.GetString(rdr.GetOrdinal("RouteIssueTypeId")));
                    routes.Add(routeInfo);
                    routeInfo = null;
                }
            }

            return routes;
        }

        /// <summary>
        /// 批发商确认接收线路
        /// </summary>
        /// <param name="userId">接收线路的用户编号</param>
        /// <param name="waitAcceptIds">待接收信息编号(即发送线路给批发商的发送编号)</param>
        /// <returns></returns>
        public virtual int AcceptRoutes(int userId, string[] waitAcceptIds)
        {
            return int.MaxValue;
        }

        /// <summary>
        /// 获取批发商等待接收的线路数量
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public virtual int GetWholesalerWaitAcceptRouteCount(string companyId)
        {
            int waitAcceptRouteCount = 0;
            DbCommand dc=this._database.GetSqlStringCommand(SQL_SELECT_GetWholesalerWaitAcceptRouteCount);
            this._database.AddInParameter(dc, "CompanyID", DbType.AnsiStringFixedLength, companyId);
            using (IDataReader rdr = DbHelper.ExecuteReader(dc,this._database))
            {
                if (rdr.Read())
                {
                    waitAcceptRouteCount = rdr.GetInt32(0);
                }
            }

            return waitAcceptRouteCount;
        }

        /// <summary>
        /// 删除线路(虚拟删除)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="isDelete">删除状态</param>
        /// <param name="routeId">要删除的线路编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool DeleteByVirtual(string companyId, bool isDelete, params string[] routeId)
        {
            StringBuilder cmdText = new StringBuilder();

            cmdText.Append("UPDATE tbl_RouteBasicInfo SET IsDelete=@IsDelete WHERE CompanyId=@CompanyId AND Id IN(");

            for (int i=0;i<routeId.Length;i++)
            {
                cmdText.AppendFormat("{0}'{1}'", i == 0 ? "" : ",", routeId[i]);
            }

            cmdText.Append(")");

            DbCommand cmd = this._database.GetSqlStringCommand(cmdText.ToString());
            this._database.AddInParameter(cmd, "IsDelete", DbType.AnsiStringFixedLength, isDelete ? "1" : "0");
            this._database.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 删除线路信息(物理删除)
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="routeId">要删除的线路编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool DeleteByActual(string companyId, params string[] routeId)
        {
            StringBuilder cmdText = new StringBuilder();

            cmdText.Append("DELETE FROM tbl_RouteBasicInfo WHERE CompanyId=@CompanyId AND Id IN(");

            for (int i = 0; i < routeId.Length; i++)
            {
                cmdText.AppendFormat("{0}'{1}'", i == 0 ? "" : ",", routeId[i]);
            }

            cmdText.Append(")");

            DbCommand cmd = this._database.GetSqlStringCommand(cmdText.ToString());
            this._database.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            return DbHelper.ExecuteSql(cmd, this._database) > 0 ? true : false;
        }
        #endregion 
    }
} 
