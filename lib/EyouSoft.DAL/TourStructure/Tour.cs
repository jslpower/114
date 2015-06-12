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

namespace EyouSoft.DAL.TourStructure
{
    /// <summary>
    /// 团队信息 数据访问
    /// </summary>
    /// 周文超 2010-05-11
    public class Tour : DALBase, EyouSoft.IDAL.TourStructure.ITour
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Tour() { }

        #region static constants
        //static constants
        //private const string SQL_UPDATE_DeleteByVirtual = "UPDATE  [tbl_TourList] SET [IsDelete]=@IsDelete WHERE ([Id]=@TourId OR [ParentTourID]=@TourId) AND CompanyId=@CompanyId AND NOT EXISTS(SELECT 1 FROM [tbl_TourOrder] AS B WHERE B.[TourId]=[tbl_TourList].[Id])";
        private const string SQL_DELETE_DeleteByActual = "DELETE FROM [tbl_TourList] WHERE ([Id]=@TourId OR [ParentTourID]=@TourId) AND CompanyId=@CompanyId AND NOT EXISTS(SELECT 1 FROM [tbl_TourOrder] AS B WHERE B.[TourId]=[tbl_TourList].[Id])";
        private const string SQL_SELECT_GetTourInfo_Basic = "SELECT * FROM [tbl_TourList] WHERE [Id]=@TourId;SELECT [CityId] FROM [tbl_TourAreaControl] WHERE [TourId]=@TourId;SELECT [CityId] FROM [tbl_TourCityControl] WHERE TourId=@TourId;SELECT ThemeId FROM [tbl_TourThemeControl] WHERE TourId=@TourId";
        //private const string SQL_SELECT_GetTourInfo_Price = "SELECT A.[PerosonalPrice],A.[ChildPrice],A.[CompanyPriceStandID],A.[CustomerLevelBasicType],B.[PriceStandName],A.[SysCustomLevel] FROM [tbl_TourBasicPriceDetail] AS A INNER JOIN [tbl_CompanyPriceStand] AS B ON A.[CompanyPriceStandID]=B.[Id] WHERE A.[TourBasicID]=@TourId ORDER BY A.RowMark ASC";
        private const string SQL_SELECT_GetTourInfo_Price = "SELECT A.[PerosonalPrice],A.[ChildPrice],A.[CompanyPriceStandID],A.[SysCustomLevel] FROM [tbl_TourBasicPriceDetail] AS A WHERE A.[TourBasicID]=@TourId ORDER BY A.RowMark ASC;SELECT CompanyId,ParentTourId FROM tbl_TourList WHERE Id=@TourId";
        private const string SQL_SELECT_GetTourInfo_StandardPlan = "SELECT [PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay] FROM [tbl_TourStandardPlan] WHERE [TourBasicID]=@TourId ORDER BY PlanDay";
        private const string SQL_SELECT_GetTourInfo_QuickPlan = "SELECT [RoutePlan] FROM [tbl_TourFastPlan] WHERE [TourBasicID]=@TourId ";
        private const string SQL_SELECT_GetTourInfo_ServiceStandard = "SELECT [ResideContent],[DinnerContent],[SightContent],[CarContent],[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice] FROM [tbl_TourServiceStandard] WHERE [TourBasicID]=@TourId";
        private const string SQL_SELECT_GetTourInfo_Children = "SELECT [Id],[TourNo],[LeaveDate],[TourState],[PersonalPrice],[ChildPrice],[RetailAdultPrice],[RetailChildrenPrice],[RealPrice],[MarketPrice],[RemnantNumber],OrderPeopleNumber,PlanPeopleCount FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' ORDER BY [LeaveDate] ASC";
        private const string SQL_SELECT_GetNotStartingChildrenTours = "SELECT [Id],[TourNo],[LeaveDate],[TourState],[PersonalPrice],[ChildPrice],[RetailAdultPrice],[RetailChildrenPrice],[RealPrice],[MarketPrice],[RemnantNumber],OrderPeopleNumber,PlanPeopleCount FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE() ORDER BY [LeaveDate] ASC";
        private const string SQL_UPDATE_SetChecked = "UPDATE [tbl_TourList] SET [IsChecked]=@Checked WHERE [Id]=@TourId";
        private const string SQL_UPDATE_SetTourState = "UPDATE [tbl_TourList] SET [TourState]=(CASE [TourState] WHEN 3 THEN 3 WHEN 4 THEN 4 ELSE @TourState END) WHERE [Id] IN(";
        private const string SQL_UPDATE_SetTourSpreadState = "UPDATE [tbl_TourList] SET [RouteState]=@TourSpreadState,TourDescription=@TourSpreadStateDescription WHERE [Id] IN(";
        private const string SQL_UPDATE_SetTemplateTourSpreadState = "UPDATE [tbl_TourList] SET [RouteState]=@TourSpreadState,TourDescription=@TourSpreadStateDescription WHERE ParentTourID=@TemplateTourId";
        private const string SQL_UPDATE_SetTourRemnantNumber = "UPDATE [tbl_TourList] SET [RemnantNumber]=(CASE WHEN @RemnantNumber>[PlanPeopleCount]-[OrderPeopleNumber] THEN [PlanPeopleCount]-[OrderPeopleNumber] ELSE @RemnantNumber END) WHERE [Id]=@TourId";
        private const string SQL_SELECT_GetTemplateTours = "SELECT A.[AreaId],'' AS AreaName,A.[RouteName],A.[ParentTourID],A.PersonalPrice,A.ChildPrice,A.RetailAdultPrice	,A.RetailChildrenPrice,A.PlanPeopleCount,A.RemnantNumber,A.RecentLeaveCount,A.LeaveDate,A.TourReleaseType FROM [tbl_TourList] AS A ";
        private const string SQL_INSERT_UPDATE_InsertTourVisitedInfo = "INSERT INTO [tbl_TourVisitInfo](Id,TourID,VisitedCompanyId,VisitedCompanyName,ClientIP,ClientUserID,ClientUserContactName,ComanyName,CompanyId,VisitType) VALUES(@Id,@TourId,@VisitedCompanyId,@VisitedCompanyName,@ClientIP,@ClientUserID,@ClientUserContactName,@ComanyName,@CompanyId,@VisitType);UPDATE tbl_TourList SET ShowCount=ShowCount+1 WHERE Id=@TourId";
        private const string SQL_SELECT_GetPlatformValidTourNumber = "SELECT COUNT(*) FROM tbl_TourList WHERE  LeaveDate>=GETDATE() AND IsDelete='0' AND IsChecked='1'";
        private const string SQL_SELECT_GetTourVisitedHistorysByUser = "SELECT TOP(@Expression) CompanyId AS ClientCompanyId,ComanyName AS ClientCompanyName,ClientUserID,ClientUserContactName tbl_TourVisitInfo WHERE VisitedCompanyId=@CompanyId ORDER BY IssueTime DESC";
        //private const string SQL_SELECT_GetFirstTourPriceDetail = "SELECT PerosonalPrice,ChildPrice,CompanyPriceStandID,CustomerLevelBasicType FROM tbl_TourBasicPriceDetail WHERE RowMark=0 AND TourBasicID=@TourId";
        private const string SQL_SELECT_GetVisitedHistorysByUser = "SELECT TOP(@Expression)  VisitedTourId, VisitedCompanyId, VisitedCompanyName, VisitedTourNo, VisitedRouteName, VisitedTime FROM view_TourVisitInfo_TourVisit WHERE ClientUserId=@UserId ORDER BY VisitedTime DESC";
        private const string SQL_SELECT_GetTourNumber = "SELECT COUNT(*) AS TourNumber FROM tbl_TourList WHERE CompanyId=@CompanyId";
        private const string SQL_SELECT_GetTourVisitedNumberByUser = "SELECT COUNT(*) AS VisitedNumber FROM tbl_TourVisitInfo WHERE VisitedCompanyId=@CompanyId";
        private const string SQL_SELECT_GetVisitedNumberByCompany = "SELECT COUNT(*) AS VisitedNumber FROM tbl_TourVisitInfo WHERE CompanyId=CompanyId GROUP BY VisitedCompanyId";
        private const string SQL_SELECT_GetCityTourNumber = "SELECT ParentTourCount,TourCount FROM tbl_SysCity WHERE Id=@CityId";
        private const string SQL_SELECT_GetTourInfo_AgencyInfo = "SELECT LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel FROM tbl_TourLocalityInfo WHERE TourId=@TourId";
        private const string SQL_SELECT_IsDeleted = "SELECT IsDelete FROM tbl_TourList WHERE Id=@TourId";
        private const string SQL_UPDATE_SetClicks = "UPDATE tbl_TourList SET ShowCount=ShowCount+@Expression WHERE Id=@TourId";
        #endregion

        #region protected member
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
                    xmlDoc.AppendFormat("<LeaveCityInfo CityId=\"{0}\" />", tmp.ToString());
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

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
        /// 创建团队主题XMLDocument 
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

        /// <summary>
        /// 创建子团信息XMLDocument
        /// </summary>
        /// <param name="children">子团信息集合</param>
        /// <param name="isGenerateTourId">是否要生成团队编号</param>
        /// <returns>System.String</returns>
        protected string CreateChildrenTourXML(IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> children, bool isGenerateTourId)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (children != null && children.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.ChildrenTourInfo tmp in children)
                {
                    xmlDoc.AppendFormat("<TourInfo TourId=\"{0}\" TourCode=\"{1}\" LeaveDate=\"{2}\" />"
                        , isGenerateTourId ? Guid.NewGuid().ToString() : tmp.ChildrenId
                        , ""//tmp.TourCode
                        , tmp.LeaveDate);
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建团队报价信息XMLDocument
        /// </summary>
        /// <param name="price">报价信息集合</param>
        /// <returns>System.String</returns>
        protected string CreatePriceDetailXML(IList<EyouSoft.Model.TourStructure.TourPriceDetail> price)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (price != null && price.Count > 0)
            {
                int rowMark = 0;
                foreach (EyouSoft.Model.TourStructure.TourPriceDetail tmp in price)
                {
                    if (tmp.PriceDetail != null && tmp.PriceDetail.Count > 0)
                    {
                        foreach (EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail tmp1 in tmp.PriceDetail)
                        {
                            xmlDoc.AppendFormat("<PriceInfo AdultPrice=\"{0}\" ChildrenPrice=\"{1}\" PriceStandId=\"{2}\" CustomerLevelId=\"{3}\" RowMark=\"{4}\" />"
                                , tmp1.AdultPrice
                                , tmp1.ChildrenPrice
                                , tmp.PriceStandId
                                , tmp1.CustomerLevelId
                                , rowMark);
                        }

                        rowMark++;
                    }
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建标准发布团队的团队行程信息XMLDocument
        /// </summary>
        /// <param name="plan">行程集合</param>
        /// <returns>System.String</returns>
        /// <remarks>
        /// XML:<ROOT><PlanInfo PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
        /// </remarks>
        protected string CreateStandardPlanXML(IList<EyouSoft.Model.TourStructure.TourStandardPlan> plan)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (plan != null && plan.Count > 0)
            {
                int i = 1;
                foreach (EyouSoft.Model.TourStructure.TourStandardPlan tmp in plan)
                {
                    xmlDoc.AppendFormat("<PlanInfo PlanInterval=\"{0}\" Vehicle=\"{1}\" TrafficNumber=\"{2}\" House=\"{3}\" Dinner=\"{4}\" PlanContent=\"{5}\" PlanDay=\"{6}\" PlanId=\"{7}\"  />"
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.PlanInterval)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.Vehicle)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.TrafficNumber)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.House)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.Dinner)
                        , EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.PlanContent)
                        , i
                        , Guid.NewGuid().ToString());
                    i++;
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建自动生成团号出团日期XMLDocument
        /// </summary>
        /// <param name="leaveDate">出团日期</param>
        /// <returns></returns>
        protected string CreateAutoTourCodesXML(params DateTime[] leaveDate)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            foreach (DateTime tmp in leaveDate)
            {
                xmlDoc.AppendFormat("<LeaveDateInfo LeaveDate=\"{0}\" />", tmp);
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }

        /// <summary>
        /// 创建团队地接社信息XMLDocument
        /// </summary>
        /// <param name="agencyInfo">地接社信息集合</param>
        /// <returns></returns>
        protected string CreateLocalTravelAgencyXML(IList<EyouSoft.Model.TourStructure.TourLocalityInfo> agencyInfo)
        {
            StringBuilder xmlDoc = new StringBuilder();

            xmlDoc.Append("<ROOT>");

            if (agencyInfo != null && agencyInfo.Count > 0)
            {
                foreach (EyouSoft.Model.TourStructure.TourLocalityInfo tmp in agencyInfo)
                {
                    xmlDoc.AppendFormat("<AgencyInfo AgencyId=\"{0}\" AgencyName=\"{1}\" License=\"{2}\" Telephone=\"{3}\"  />",
                        tmp.LocalComoanyID,
                        EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.LocalCompanyName),
                        EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.LicenseNumber),
                        EyouSoft.Common.Utility.ReplaceXmlSpecialCharacter(tmp.ContactTel));
                }
            }

            xmlDoc.Append("</ROOT>");

            return xmlDoc.ToString();
        }
        #endregion

        #region 团队基本操作
        /*/// <summary>
        /// 新增团队信息(模板团标准发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int AddStandardTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_AddTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, Guid.NewGuid().ToString());
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                /*base.TourStore.AddInParameter(cmd, "ExpenseItem", DbType.String, model.ServiceStandard.ExpenseItem);
                base.TourStore.AddInParameter(cmd, "ChildrenInfo", DbType.String, model.ServiceStandard.ChildrenInfo);
                base.TourStore.AddInParameter(cmd, "ShoppingInfo", DbType.String, model.ServiceStandard.ShoppingInfo);
                base.TourStore.AddInParameter(cmd, "GiftInfo", DbType.String, model.ServiceStandard.GiftInfo);
                base.TourStore.AddInParameter(cmd, "NoticeProceeding", DbType.String, model.ServiceStandard.NoticeProceeding);* /
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(model.Childrens, true));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }*/

        /*/// <summary>
        /// 修改团队信息(模板团标准发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <param name="insertTours">要添加的子团信息集合</param>
        /// <param name="updateTours">要更新的子团信息集合</param>
        /// <param name="deleteTours">要删除的子团信息集合</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int UpdateStandardTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> insertTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> updateTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> deleteTours)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_UpdateTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, model.ID);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                /*base.TourStore.AddInParameter(cmd, "ExpenseItem", DbType.String, model.ServiceStandard.ExpenseItem);
                base.TourStore.AddInParameter(cmd, "ChildrenInfo", DbType.String, model.ServiceStandard.ChildrenInfo);
                base.TourStore.AddInParameter(cmd, "ShoppingInfo", DbType.String, model.ServiceStandard.ShoppingInfo);
                base.TourStore.AddInParameter(cmd, "GiftInfo", DbType.String, model.ServiceStandard.GiftInfo);
                base.TourStore.AddInParameter(cmd, "NoticeProceeding", DbType.String, model.ServiceStandard.NoticeProceeding);--* //
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(insertTours, true));
            base.TourStore.AddInParameter(cmd, "UpdateChildren", DbType.String, this.CreateChildrenTourXML(updateTours, false));
            base.TourStore.AddInParameter(cmd, "DeleteChildren", DbType.String, this.CreateChildrenTourXML(deleteTours, false));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }*/

        /*/// <summary>
        /// 新增团队信息(模板团快速发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int AddQuickTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_AddTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, Guid.NewGuid().ToString());
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);
            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(model.Childrens, true));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }*/


        /*/// <summary>
        /// 修改团队信息(模板团快速发布)
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <param name="insertTours">要添加的子团信息集合</param>
        /// <param name="updateTours">要更新的子团信息集合</param>
        /// <param name="deleteTours">要删除的子团信息集合</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int UpdateQuickTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> insertTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> updateTours
            , IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> deleteTours)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_UpdateTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, model.ID);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);
            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(insertTours, true));
            base.TourStore.AddInParameter(cmd, "UpdateChildren", DbType.String, this.CreateChildrenTourXML(updateTours, false));
            base.TourStore.AddInParameter(cmd, "DeleteChildren", DbType.String, this.CreateChildrenTourXML(deleteTours, false));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }*/

        /// <summary>
        /// 新增模板团信息
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int InsertTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_AddTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, Guid.NewGuid().ToString());
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                /*base.TourStore.AddInParameter(cmd, "ExpenseItem", DbType.String, model.ServiceStandard.ExpenseItem);
                base.TourStore.AddInParameter(cmd, "ChildrenInfo", DbType.String, model.ServiceStandard.ChildrenInfo);
                base.TourStore.AddInParameter(cmd, "ShoppingInfo", DbType.String, model.ServiceStandard.ShoppingInfo);
                base.TourStore.AddInParameter(cmd, "GiftInfo", DbType.String, model.ServiceStandard.GiftInfo);
                base.TourStore.AddInParameter(cmd, "NoticeProceeding", DbType.String, model.ServiceStandard.NoticeProceeding);*/
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(model.Childrens, true));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 修改模板团信息
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns></returns>
        public virtual int UpdateTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_UpdateTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, model.ID);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            //base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(insertTours, true));
            //base.TourStore.AddInParameter(cmd, "UpdateChildren", DbType.String, this.CreateChildrenTourXML(updateTours, false));
            //base.TourStore.AddInParameter(cmd, "DeleteChildren", DbType.String, this.CreateChildrenTourXML(deleteTours, false));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 追加模板团发团计划信息
        /// </summary>
        /// <param name="model">团队信息实体</param>
        /// <returns></returns>
        public virtual int AppendTemplateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_AppendTemplateTourInfo");

            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.String, model.ID);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourType", DbType.Byte, model.TourType);
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "InsertChildren", DbType.String, this.CreateChildrenTourXML(model.Childrens, true));
            //base.TourStore.AddInParameter(cmd, "UpdateChildren", DbType.String, this.CreateChildrenTourXML(updateTours, false));
            //base.TourStore.AddInParameter(cmd, "DeleteChildren", DbType.String, this.CreateChildrenTourXML(deleteTours, false));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }

        /// <summary>
        /// 删除团队(虚拟删除)
        /// </summary>
        /// <param name="companyId">团队所在公司编号</param>
        /// <param name="tourId">团队ID</param>
        /// <param name="isDelete">删除状态</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool DeleteByVirtual(string companyId, string tourId, bool isDelete)
        {
            //DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_UPDATE_DeleteByVirtual);
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_DeleteByVirtual");

            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            base.TourStore.AddInParameter(cmd, "IsDelete", DbType.String, isDelete ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 删除团队信息(真实删除)
        /// </summary>
        /// <param name="companyId">团队所在公司编号</param>
        /// <param name="tourId">团队ID</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool DeleteByActual(string companyId, string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_DELETE_DeleteByActual);

            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);

            return DbHelper.ExecuteSql(cmd, base.TourStore) > 0 ? true : false;
        }

        /// <summary>
        /// 获取标准发布计划行程信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourStandardPlan> GetTourInfoStandardPlans(string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_StandardPlan);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                IList<EyouSoft.Model.TourStructure.TourStandardPlan> plans = new List<EyouSoft.Model.TourStructure.TourStandardPlan>();
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourStandardPlan tmp = new EyouSoft.Model.TourStructure.TourStandardPlan();

                    tmp.PlanInterval = rdr[0].ToString();
                    tmp.Vehicle = rdr[1].ToString();
                    tmp.TrafficNumber = rdr[2].ToString();
                    tmp.House = rdr[3].ToString();
                    tmp.Dinner = rdr[4].ToString();
                    tmp.PlanContent = rdr[5].ToString();
                    tmp.PlanDay = rdr.GetInt32(6);

                    plans.Add(tmp);
                }
                return plans;
            }
        }

        /// <summary>
        /// 获取快速发布计划行程信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public virtual string GetTourInfoQuickPlan(string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_QuickPlan);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    return rdr[0].ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取标准发布计划服务标准信息
        /// </summary>
        /// <param name="tourId">计划编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TourStructure.TourServiceStandard GetTourServiceStandard(string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_ServiceStandard);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                EyouSoft.Model.TourStructure.TourServiceStandard serviceStandard = null;
                if (rdr.Read())
                {
                    serviceStandard = new EyouSoft.Model.TourStructure.TourServiceStandard();
                    serviceStandard.ResideContent = rdr[0].ToString();
                    serviceStandard.DinnerContent = rdr[1].ToString();
                    serviceStandard.SightContent = rdr[2].ToString();
                    serviceStandard.CarContent = rdr[3].ToString();
                    serviceStandard.GuideContent = rdr[4].ToString();
                    serviceStandard.TrafficContent = rdr[5].ToString();
                    serviceStandard.IncludeOtherContent = rdr[6].ToString();
                    serviceStandard.NotContainService = rdr[7].ToString();
                    /*serviceStandard.ExpenseItem = rdr[8].ToString();
                    serviceStandard.ChildrenInfo = rdr[9].ToString();
                    serviceStandard.ShoppingInfo = rdr[10].ToString();
                    serviceStandard.GiftInfo = rdr[11].ToString();
                    serviceStandard.NoticeProceeding = rdr[12].ToString();*/
                    serviceStandard.SpeciallyNotice = rdr[8].ToString();
                }

                return serviceStandard;
            }
        }

        /// <summary>
        /// 获取标准发布计划地接社信息集合
        /// </summary>
        /// <param name="tourId">计划编号</param>
        public virtual IList<EyouSoft.Model.TourStructure.TourLocalityInfo> GetTourLocalAgencys(string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_AgencyInfo);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            IList<EyouSoft.Model.TourStructure.TourLocalityInfo>  localTravelAgencys = new List<EyouSoft.Model.TourStructure.TourLocalityInfo>();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourLocalityInfo tmp = new EyouSoft.Model.TourStructure.TourLocalityInfo();

                    tmp.LocalComoanyID = rdr[0].ToString();
                    tmp.LocalCompanyName = rdr[1].ToString();
                    tmp.LicenseNumber = rdr[2].ToString();
                    tmp.ContactTel = rdr[3].ToString();

                    localTravelAgencys.Add(tmp);
                }
            }

            return localTravelAgencys;
        }

        /// <summary>
        /// 获取团队信息
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns>返回团队信息业务实体</returns>
        public virtual EyouSoft.Model.TourStructure.TourInfo GetTourInfo(string tourId)
        {
            EyouSoft.Model.TourStructure.TourInfo tourInfo = null;

            #region 团队基本信息
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_Basic);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                //团队基本信息
                if (rdr.Read())
                {
                    tourInfo = new EyouSoft.Model.TourStructure.TourInfo();

                    tourInfo.ID = tourId;
                    tourInfo.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tourInfo.CompanyName = rdr["CompanyName"].ToString();
                    tourInfo.RouteName = rdr["RouteName"].ToString();
                    tourInfo.TourNo = rdr["TourNo"].ToString();
                    tourInfo.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tourInfo.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tourInfo.ComeBackDate = rdr.GetDateTime(rdr.GetOrdinal("ComeBackDate"));
                    tourInfo.TourType = (EyouSoft.Model.TourStructure.TourType)rdr.GetByte(rdr.GetOrdinal("TourClassID"));
                    tourInfo.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tourInfo.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tourInfo.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    tourInfo.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tourInfo.ParentTourID = rdr["ParentTourID"].ToString();
                    tourInfo.LeaveTraffic = rdr["LeaveTraffic"].ToString();
                    //tourInfo.BackTraffic = rdr["BackTraffic"].ToString();
                    tourInfo.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tourInfo.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tourInfo.SendContactName = rdr["SendContactName"].ToString();
                    tourInfo.SendContactTel = rdr["SendContactTel"].ToString();
                    tourInfo.UrgentContactName = rdr["UrgentContactName"].ToString();
                    tourInfo.UrgentContactTel = rdr["UrgentContactTel"].ToString();
                    tourInfo.TourContact = rdr["TourContact"].ToString();
                    tourInfo.TourContactTel = rdr["TourContactTel"].ToString();
                    tourInfo.AutoOffDays = rdr.GetInt32(rdr.GetOrdinal("StopAcceptNum"));
                    tourInfo.TourContacMQ = rdr["TourContacMQ"].ToString();
                    tourInfo.TourContacUserName = rdr["TourContacUserName"].ToString();
                    tourInfo.MeetTourContect = rdr["MeetTourContect"].ToString();
                    tourInfo.CollectionContect = rdr["CollectionContect"].ToString();
                    tourInfo.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tourInfo.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tourInfo.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tourInfo.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tourInfo.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tourInfo.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tourInfo.OperatorID = rdr.GetString(rdr.GetOrdinal("OperatorID"));
                    tourInfo.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tourInfo.CreateTime = rdr.GetDateTime(rdr.GetOrdinal("CreateTime"));
                    tourInfo.OccupySeat = rdr["OccupySeat"].ToString();
                    tourInfo.StandardPlanError = rdr.GetInt32(rdr.GetOrdinal("StandardPlanError"));
                    tourInfo.TourPriceError = rdr.GetInt32(rdr.GetOrdinal("TourPriceError"));
                    tourInfo.IsCompanyCheck = rdr.GetString(rdr.GetOrdinal("IsCompanyCheck")) == "1" ? true : false;
                    tourInfo.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(rdr.GetString(rdr.GetOrdinal("TourReleaseType")));
                    tourInfo.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tourInfo.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                }

                if (tourInfo == null) { return tourInfo; }

                //出港城市
                if (rdr.NextResult())
                {
                    /*tourInfo.LeaveCity = new List<int>();
                    while (rdr.Read())
                    {
                        tourInfo.LeaveCity.Add(rdr.GetInt32(0));
                    }*/
                    if (rdr.Read())
                    {
                        tourInfo.LeaveCity = rdr.GetInt32(0);
                    }
                }

                //销售城市
                if (rdr.NextResult())
                {
                    tourInfo.SaleCity = new List<int>();
                    while (rdr.Read())
                    {
                        tourInfo.SaleCity.Add(rdr.GetInt32(0));
                    }
                }

                //线路主题
                if (rdr.NextResult())
                {
                    tourInfo.RouteTheme = new List<int>();
                    while (rdr.Read())
                    {
                        tourInfo.RouteTheme.Add(rdr.GetInt32(0));
                    }
                }


            }

            if (tourInfo == null) { return tourInfo; }
            #endregion
            
            if (tourInfo.ReleaseType == EyouSoft.Model.TourStructure.ReleaseType.Quick)
            {
                tourInfo.QuickPlan = this.GetTourInfoQuickPlan(tourId);
            }
            else
            {
                tourInfo.StandardPlan = this.GetTourInfoStandardPlans(tourId);
                tourInfo.ServiceStandard = this.GetTourServiceStandard(tourId);
                tourInfo.LocalTravelAgency = this.GetTourLocalAgencys(tourId);
            }
            
            //子团信息
            if (string.IsNullOrEmpty(tourInfo.ParentTourID))
            {
                tourInfo.Childrens = this.GetChildrenTours(tourInfo.ID);
            }

            return tourInfo;
        }

        /// <summary>
        /// 审核团队
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="isChecked">审核状态</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool SetChecked(string tourId, bool isChecked)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_UPDATE_SetChecked);
            base.TourStore.AddInParameter(cmd, "Checked", DbType.String, isChecked ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 1 ? true : false;
        }

        /// <summary>
        /// 设置团队收客状态
        /// </summary>
        /// <param name="tourState">团队状态</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool SetTourState(EyouSoft.Model.TourStructure.TourState tourState, params string[] tourId)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(SQL_UPDATE_SetTourState);

            for (int i = 0; i < tourId.Length; i++)
            {
                cmdText.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }

            cmdText.Append(")");

            cmdText.AppendFormat(";EXECUTE proc_TourList_RecentLeave @TourId='{0}',@IsParentTour=0", tourId[0]);

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());
            base.TourStore.AddInParameter(cmd, "TourState", DbType.Byte, tourState);

            for (int i = 0; i < tourId.Length; i++)
            {
                base.TourStore.AddInParameter(cmd, "PARM" + i, DbType.AnsiStringFixedLength, tourId[i]);
            }

            return DbHelper.ExecuteSql(cmd, base.TourStore) > 0 ? true : false;
        }

        /// <summary>
        /// 设置团队推广状态
        /// </summary>        
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool SetTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, params string[] tourId)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(SQL_UPDATE_SetTourSpreadState);

            for (int i = 0; i < tourId.Length; i++)
            {
                cmdText.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }

            cmdText.Append(")");

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());
            base.TourStore.AddInParameter(cmd, "TourSpreadState", DbType.Byte, tourSpreadState);
            base.TourStore.AddInParameter(cmd, "TourSpreadStateDescription", DbType.String, tourSpreadDescription);

            for (int i = 0; i < tourId.Length; i++)
            {
                base.TourStore.AddInParameter(cmd, "PARM" + i, DbType.AnsiStringFixedLength, tourId[i]);
            }

            return DbHelper.ExecuteSql(cmd, base.TourStore) > 0 ? true : false;
        }

        /// <summary>
        /// 设置模板团下所有子团推广状态
        /// </summary>
        /// <param name="tourSpreadState">团队推广状态</param>
        /// <param name="tourSpreadDescription">团队推广状态说明</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual bool SetTemplateTourSpreadState(EyouSoft.Model.TourStructure.TourSpreadState tourSpreadState, string tourSpreadDescription, string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_UPDATE_SetTemplateTourSpreadState);

            base.TourStore.AddInParameter(cmd, "TourSpreadState", DbType.Byte, tourSpreadState);
            base.TourStore.AddInParameter(cmd, "TourSpreadStateDescription", DbType.String, tourSpreadDescription);
            base.TourStore.AddInParameter(cmd, "TemplateTourId", DbType.AnsiStringFixedLength, tourId);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 0 ? false : true;
        }

        /// <summary>
        /// 获取团队报价信息集合
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="companyId">发布团队的公司</param>
        /// <param name="parentTourId">模板团编号</param>
        /// <returns>报价信息业务实体集合</returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourPriceDetail> GetTourPriceDetail(string tourId, out string companyId,out string parentTourId)
        {
            companyId = "";
            parentTourId = string.Empty;
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_Price);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            List<EyouSoft.Model.TourStructure.TourPriceDetail> prices = new List<EyouSoft.Model.TourStructure.TourPriceDetail>();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourPriceDetail price = prices.FindLast((EyouSoft.Model.TourStructure.TourPriceDetail tmp) =>
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

                    price = price ?? new EyouSoft.Model.TourStructure.TourPriceDetail();

                    EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail tmp1 = new EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail();
                    tmp1.CustomerLevelId = rdr.GetInt32(3);
                    tmp1.AdultPrice = rdr.GetDecimal(0);
                    tmp1.ChildrenPrice = rdr.GetDecimal(1);

                    if (string.IsNullOrEmpty(price.PriceStandId))
                    {
                        price.PriceStandId = rdr.GetString(2);
                        price.PriceStandName = "常规团";

                        price.PriceDetail = new List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>();

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
                    if (rdr.Read())
                    {
                        companyId = rdr.GetString(0);
                        parentTourId = rdr.GetString(1);
                    }
                }
            }

            return prices;
        }

        /// <summary>
        /// 设置团队剩余人数 当设置的剩余人数大于团队计划人数时将会把剩余人数设置成团队的计划人数
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="remnantNumber">剩余人数</param>
        /// <returns>System.Boolean true:success false:error</returns>
        public virtual bool SetTourRemnantNumber(string tourId, int remnantNumber)
        {
            /*DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_UPDATE_SetTourRemnantNumber);
            base.TourStore.AddInParameter(cmd, "RemnantNumber", DbType.Int32, remnantNumber);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.String, tourId);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 1 ? true : false;*/
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_SetTourRemnantNumber");
            base.TourStore.AddInParameter(cmd, "RemnantNumber", DbType.Int32, remnantNumber);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.RunProcedure(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result")) == 1 ? true : false;
        }

        /// <summary>
        /// 批量生成团号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <param name="leaveDate">出团日期</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.AutoTourCodeInfo> CreateAutoTourCodes(string companyId, int areaId, params DateTime[] leaveDate)
        {
            IList<EyouSoft.Model.TourStructure.AutoTourCodeInfo> tourCodes = new List<EyouSoft.Model.TourStructure.AutoTourCodeInfo>();

            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_CreateAutoTourCodes");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, areaId);
            base.TourStore.AddInParameter(cmd, "LeaveDate", DbType.String, CreateAutoTourCodesXML(leaveDate));

            using (IDataReader rdr = DbHelper.RunReaderProcedure(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    tourCodes.Add(new EyouSoft.Model.TourStructure.AutoTourCodeInfo(rdr.GetString(0), rdr.GetDateTime(1)));
                }
            }

            return tourCodes;
        }

        /// <summary>
        /// 修改团队信息(单个子团)
        /// </summary>
        /// <param name="model">团队信息业务实体</param>
        /// <returns>System.Int32 0:error 1:success</returns>
        public virtual int UpdateTourInfo(EyouSoft.Model.TourStructure.TourInfo model)
        {
            DbCommand cmd = base.TourStore.GetStoredProcCommand("proc_TourList_UpdateTourInfo");

            base.TourStore.AddInParameter(cmd, "TourId", DbType.String, model.ID);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, model.CompanyID);
            base.TourStore.AddInParameter(cmd, "CompanyName", DbType.String, model.CompanyName);
            base.TourStore.AddInParameter(cmd, "IsCompanyCheck", DbType.String, model.IsCompanyCheck ? "1" : "0");
            base.TourStore.AddInParameter(cmd, "RouteName", DbType.String, model.RouteName);
            base.TourStore.AddInParameter(cmd, "TourDays", DbType.Int32, model.TourDays);
            base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, model.AreaId);
            base.TourStore.AddInParameter(cmd, "AreaType", DbType.Byte, model.AreaType);
            base.TourStore.AddInParameter(cmd, "PlanPeopleCount", DbType.Int32, model.PlanPeopleCount);
            base.TourStore.AddInParameter(cmd, "ReleaseType", DbType.String, (int)model.ReleaseType);
            base.TourStore.AddInParameter(cmd, "AutoOffDays", DbType.Int32, model.AutoOffDays);
            base.TourStore.AddInParameter(cmd, "LeaveTraffic", DbType.String, model.LeaveTraffic);
            //base.TourStore.AddInParameter(cmd, "BackTraffic", DbType.String, model.BackTraffic);
            base.TourStore.AddInParameter(cmd, "SendContactName", DbType.String, model.SendContactName);
            base.TourStore.AddInParameter(cmd, "SendContactTel", DbType.String, model.SendContactTel);
            base.TourStore.AddInParameter(cmd, "UrgentContactName", DbType.String, model.UrgentContactName);
            base.TourStore.AddInParameter(cmd, "UrgentContactTel", DbType.String, model.UrgentContactTel);
            base.TourStore.AddInParameter(cmd, "OperatorID", DbType.String, model.OperatorID);
            base.TourStore.AddInParameter(cmd, "TourContact", DbType.String, model.TourContact);
            base.TourStore.AddInParameter(cmd, "TourContactTel", DbType.String, model.TourContactTel);
            base.TourStore.AddInParameter(cmd, "TourContactMQ", DbType.String, model.TourContacMQ);
            base.TourStore.AddInParameter(cmd, "TourContactUserName", DbType.String, model.TourContacUserName);
            base.TourStore.AddInParameter(cmd, "MeetTourContect", DbType.String, model.MeetTourContect);
            base.TourStore.AddInParameter(cmd, "CollectionContect", DbType.String, model.CollectionContect);
            base.TourStore.AddInParameter(cmd, "StandardPlanError", DbType.Int32, model.StandardPlanError);
            base.TourStore.AddInParameter(cmd, "TourPriceError", DbType.Int32, model.TourPriceError);

            if (model.ServiceStandard != null)
            {
                base.TourStore.AddInParameter(cmd, "ResideContent", DbType.String, model.ServiceStandard.ResideContent);
                base.TourStore.AddInParameter(cmd, "DinnerContent", DbType.String, model.ServiceStandard.DinnerContent);
                base.TourStore.AddInParameter(cmd, "SightContent", DbType.String, model.ServiceStandard.SightContent);
                base.TourStore.AddInParameter(cmd, "CarContent", DbType.String, model.ServiceStandard.CarContent);
                base.TourStore.AddInParameter(cmd, "GuideContent", DbType.String, model.ServiceStandard.GuideContent);
                base.TourStore.AddInParameter(cmd, "TrafficContent", DbType.String, model.ServiceStandard.TrafficContent);
                base.TourStore.AddInParameter(cmd, "IncludeOtherContent", DbType.String, model.ServiceStandard.IncludeOtherContent);
                base.TourStore.AddInParameter(cmd, "NotContainService", DbType.String, model.ServiceStandard.NotContainService);
                /*base.TourStore.AddInParameter(cmd, "ExpenseItem", DbType.String, model.ServiceStandard.ExpenseItem);
                base.TourStore.AddInParameter(cmd, "ChildrenInfo", DbType.String, model.ServiceStandard.ChildrenInfo);
                base.TourStore.AddInParameter(cmd, "ShoppingInfo", DbType.String, model.ServiceStandard.ShoppingInfo);
                base.TourStore.AddInParameter(cmd, "GiftInfo", DbType.String, model.ServiceStandard.GiftInfo);
                base.TourStore.AddInParameter(cmd, "NoticeProceeding", DbType.String, model.ServiceStandard.NoticeProceeding);*/
                base.TourStore.AddInParameter(cmd, "SpeciallyNotice", DbType.String, model.ServiceStandard.SpeciallyNotice);
            }

            //base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.String, this.CreateLeaveCityXML(model.LeaveCity));
            base.TourStore.AddInParameter(cmd, "LeaveCity", DbType.Int32, model.LeaveCity);
            base.TourStore.AddInParameter(cmd, "SaleCity", DbType.String, this.CreateSaleCityXML(model.SaleCity));
            base.TourStore.AddInParameter(cmd, "RouteTheme", DbType.String, this.CreateRouteThemeXML(model.RouteTheme));
            base.TourStore.AddInParameter(cmd, "PriceDetail", DbType.String, this.CreatePriceDetailXML(model.TourPriceDetail));
            base.TourStore.AddInParameter(cmd, "StandardPlan", DbType.String, this.CreateStandardPlanXML(model.StandardPlan));
            base.TourStore.AddInParameter(cmd, "LocalTravelAgency", DbType.String, this.CreateLocalTravelAgencyXML(model.LocalTravelAgency));
            base.TourStore.AddInParameter(cmd, "QuickPlan", DbType.String, model.QuickPlan);
            base.TourStore.AddOutParameter(cmd, "Result", DbType.Int32, 4);

            DbHelper.ExecuteSql(cmd, base.TourStore);

            return Convert.ToInt32(base.TourStore.GetParameterValue(cmd, "Result"));
        }

        /*/// <summary>
        /// 获取团队第一个报价等级的报价信息
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TourStructure.TourPriceDetail GetFirstTourPriceDetail(string tourId)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetFirstTourPriceDetail);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            EyouSoft.Model.TourStructure.TourPriceDetail firstPriceDetail = new EyouSoft.Model.TourStructure.TourPriceDetail();
            firstPriceDetail.PriceDetail = new List<EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail>();
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail tmp = new EyouSoft.Model.TourStructure.TourPriceCustomerLeaveDetail();

                    tmp.ChildrenPrice = rdr.GetDecimal(0);
                    tmp.AdultPrice = rdr.GetDecimal(1);
                    firstPriceDetail.PriceStandId = firstPriceDetail.PriceStandId ?? rdr.GetString(2);
                    //tmp.CustomerLevelType = (EyouSoft.Model.CompanyStructure.CustomerLevelType)rdr.GetByte(3);

                    firstPriceDetail.PriceDetail.Add(tmp);
                }
            }

            return firstPriceDetail;
        }*/

        /// <summary>
        /// 判断团队是否已删除
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual bool IsDeleted(string tourId)
        {
            bool isDeleted = true;
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_IsDeleted);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    isDeleted = rdr.GetString(0) == "1" ? true : false;
                }
            }

            return isDeleted;
        }
        #endregion

        #region 列表相关
        /// <summary>
        /// 按线路区域分组获取模板团列表
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="leaveCityId">出港城市编号 为null时所有出港城市</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TemplateTourInfo> GetTemplateTours(string companyId, string userId, string userAreas, int? leaveCityId)
        {
            List<EyouSoft.Model.TourStructure.TemplateTourInfo> tours = new List<EyouSoft.Model.TourStructure.TemplateTourInfo>();
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(SQL_SELECT_GetTemplateTours);

            cmdText.Append(" WHERE A.[CompanyId]=@CompanyId AND A.LeaveDate>=GETDATE() AND A.[IsRecentLeave]='1' ");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);

            if (leaveCityId.HasValue)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM [tbl_TourAreaControl] AS B WHERE B.[TourId]=A.Id AND B.[CityId]=@CityId ");
                base.TourStore.AddInParameter(cmd, "CityId", DbType.Int32, leaveCityId.Value);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND A.[AreaId] IN ({0}) ", userAreas);
            }

            if (!string.IsNullOrEmpty(userId))
            {
                cmdText.AppendFormat(" AND A.[OperatorID]='{0}' ", userId);
            }

            cmdText.Append(" ORDER BY A.[AreaId],A.[LeaveDate] DESC");

            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TemplateTourInfo tour = tours.FindLast((EyouSoft.Model.TourStructure.TemplateTourInfo tmp) =>
                    {
                        if (rdr.GetInt32(0) == tmp.AreaId)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });

                    tour = tour ?? new EyouSoft.Model.TourStructure.TemplateTourInfo();

                    EyouSoft.Model.TourStructure.TourBasicInfo tmp1 = new EyouSoft.Model.TourStructure.TourBasicInfo();
                    tmp1.ID = rdr.GetString(3);
                    tmp1.RouteName = rdr[2].ToString();
                    tmp1.TravelAdultPrice = rdr.GetDecimal(4);
                    tmp1.TravelChildrenPrice = rdr.GetDecimal(5);
                    tmp1.RetailAdultPrice = rdr.GetDecimal(6);
                    tmp1.RetailChildrenPrice = rdr.GetDecimal(7);
                    tmp1.PlanPeopleCount = rdr.GetInt32(8);
                    tmp1.RemnantNumber = rdr.GetInt32(9);
                    tmp1.RecentLeaveCount = rdr.GetInt32(10);
                    tmp1.LeaveDate = rdr.GetDateTime(11);
                    tmp1.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(rdr.GetString(12));

                    if (tour.AreaId > 0)
                    {
                        tour.TemplateTour.Add(tmp1);
                    }
                    else
                    {
                        tour.AreaId = rdr.GetInt32(0);
                        tour.AreaName = "";//rdr.GetString(1);
                        tour.TemplateTour = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
                        tour.TemplateTour.Add(tmp1);

                        tours.Add(tour);
                    }
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取未出发团队信息集合(模板团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount, string companyId, int? areaId)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_NotStartingTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "*";

            cmdQuery.Append(" IsRecentLeaveNoControl='1' ");
            cmdQuery.AppendFormat(" AND CompanyId='{0}' ", companyId);
            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourBasicInfo tmp = new EyouSoft.Model.TourStructure.TourBasicInfo();
                    tmp.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tmp.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tmp.RouteName = rdr["RouteName"].ToString();
                    tmp.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tmp.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tmp.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tmp.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tmp.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tmp.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tmp.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tmp.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tmp.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(rdr.GetString(rdr.GetOrdinal("TourReleaseType")));
                    tmp.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    tmp.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tmp.TourContact = rdr["TourContact"].ToString();
                    tmp.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tmp.TourSpreadDescription = rdr["TourDescription"].ToString();

                    tours.Add(tmp);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取未出发团队信息集合(子团)
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="templateTourId">模板团编号</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="searchTourState">团队状态</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetNotStartingTours(int pageSize, int pageIndex, ref int recordCount, string templateTourId, string tourCode, EyouSoft.Model.TourStructure.SearchTourState searchTourState, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_NotStartingTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" ParentTourID='{0}' ", templateTourId);

            if (!string.IsNullOrEmpty(tourCode))
            {
                cmdQuery.AppendFormat(" AND TourNo LIKE '%{0}%' ", tourCode);
            }

            /*if (searchTourState != EyouSoft.Model.TourStructure.SearchTourState.全部)
            {
                cmdQuery.AppendFormat(" AND  TourState={0}", (int)searchTourState);
            }*/

            switch (searchTourState)
            {
                case EyouSoft.Model.TourStructure.SearchTourState.客满:
                    cmdQuery.Append(" AND  TourState IN(2,4)");
                    break;
                case EyouSoft.Model.TourStructure.SearchTourState.收客:
                    cmdQuery.Append(" AND  TourState=1");
                    break;
                case EyouSoft.Model.TourStructure.SearchTourState.停收:
                    cmdQuery.Append(" AND  TourState IN(0,3)");
                    break;
                default:
                    break;
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourBasicInfo tour = new EyouSoft.Model.TourStructure.TourBasicInfo();
                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.TourNo = rdr["TourNo"].ToString();
                    tour.ParentTourID = rdr["ParentTourID"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CollectAdult")))
                        tour.CollectAdultNumber = rdr.GetInt32(rdr.GetOrdinal("CollectAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CollectChildren")))
                        tour.CollectChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("CollectChildren"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("AllowanceAdult")))
                        tour.AllowanceAdultNumber = rdr.GetInt32(rdr.GetOrdinal("AllowanceAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("AllowanceChildren")))
                        tour.AllowanceChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("AllowanceChildren"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("UntreatedAdult")))
                        tour.UntreatedAdultNumber = rdr.GetInt32(rdr.GetOrdinal("UntreatedAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("UntreatedChildren")))
                        tour.UntreatedChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("UntreatedChildren"));
                    tour.ReleaseType = (EyouSoft.Model.TourStructure.ReleaseType)int.Parse(rdr.GetString(rdr.GetOrdinal("TourReleaseType")));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));

                    tours.Add(tour);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取已出发团队信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetStartingTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string userAreas, string tourCode, string routeName
            , int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_StartingTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate DESC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' ", companyId);

            if (!string.IsNullOrEmpty(tourCode))
            {
                cmdQuery.AppendFormat(" AND TourNo LIKE '%{0}%' ", tourCode);
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (tourDays.HasValue)
            {
                cmdQuery.AppendFormat(" AND TourDays={0} ", tourDays);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND AreaId IN({0}) ", userAreas);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    //if (string.IsNullOrEmpty(rdr.GetString(rdr.GetOrdinal("ParentTourID")))) { continue; }
                    EyouSoft.Model.TourStructure.TourBasicInfo tour = new EyouSoft.Model.TourStructure.TourBasicInfo();
                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.TourNo = rdr["TourNo"].ToString();
                    tour.ParentTourID = rdr["ParentTourID"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CollectAdult")))
                        tour.CollectAdultNumber = rdr.GetInt32(rdr.GetOrdinal("CollectAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("CollectChildren")))
                        tour.CollectChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("CollectChildren"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("OverdueAdult")))
                        tour.OverdueAdultNumber = rdr.GetInt32(rdr.GetOrdinal("OverdueAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("OverdueChildren")))
                        tour.OverdueChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("OverdueChildren"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DismissAdult")))
                        tour.DismissAdultNumber = rdr.GetInt32(rdr.GetOrdinal("DismissAdult"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("DismissChildren")))
                        tour.DismissChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("DismissChildren"));
                    tour.ComeBackDate = rdr.GetDateTime(rdr.GetOrdinal("ComeBackDate"));

                    tours.Add(tour);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取快到期产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="expireTime">到期时间</param>
        /// <param name="userAreas">当前用户负责的区域(城市)编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetComingExpireTours(int pageSize, int pageIndex, ref int recordCount, DateTime expireTime
            , string userAreas, int? areaId, int? cityId, string companyName, string routeName)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_ComingExpireTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" LeaveDate<='{0}' ", expireTime);

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl AS A WHERE A.TourId=view_TourList_ComingExpireTour.Id AND A.CityId IN({0}) ) ", userAreas);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId ={0} ", areaId);
            }

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourCityControl WHERE TourId=view_TourList_ComingExpireTour.Id AND CityId={0}) ", cityId.Value);
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourBasicInfo tour = new EyouSoft.Model.TourStructure.TourBasicInfo();

                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourContact = rdr["TourContact"].ToString();
                    tour.TourContactTel = rdr["TourContactTel"].ToString();
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();

                    tours.Add(tour);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取关注批发商产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>        
        /// <param name="attentionCompanyId">关注的批发商编号 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <param name="areaType">线路区域类型 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetAttentionTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId
            , int? leaveCityId, int? areaId, string routeName
            , string attentionCompanyId, DateTime? startLeaveDate, DateTime? finishLeaveDate
            , EyouSoft.Model.SystemStructure.AreaType? areaType)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_ValidTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "*";

            #region 拼接查询条件
            cmdQuery.Append(" IsRecentLeave='1' ");
            cmdQuery.AppendFormat(" AND EXISTS (SELECT 1 FROM tbl_CompanyFavor WHERE CompanyID='{0}' AND FavorCompanyId=view_TourList_ValidTour.CompanyId) ", companyId);

            if (leaveCityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl WHERE TourId=view_TourList_ValidTour.Id AND CityId={0}) ", leaveCityId.Value);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (!string.IsNullOrEmpty(attentionCompanyId))
            {
                cmdQuery.AppendFormat(" AND CompanyId='{0}' ", attentionCompanyId);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }

            if (areaType.HasValue)
            {
                cmdQuery.AppendFormat(" AND RouteType={0} ", (int)areaType.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourInfo tour = new EyouSoft.Model.TourStructure.TourInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.RouteName = rdr["Routename"].ToString();
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.CreateTime = rdr.GetDateTime(rdr.GetOrdinal("CreateTime"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));

                    tours.Add(tour);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取有订单的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isHistory">是否是历史团队</param>
        /// <param name="userId">要查询的用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="tourCode">团队编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> GetHavingOrderTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, bool isHistory, string userId, string userAreas
            , string tourCode, string routeName, int? tourDays
            , DateTime? startLeaveDate, DateTime? finishLeaveDate, params EyouSoft.Model.TourStructure.OrderState[] orderState)
        {
            IList<EyouSoft.Model.TourStructure.HavingOrderTourInfo> tours = new List<EyouSoft.Model.TourStructure.HavingOrderTourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourList_HavingOrderTour";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "*";

            cmdQuery.AppendFormat(" CompanyId='{0}' ", companyId);

            if (isHistory)
            {
                cmdQuery.Append(" AND ComeBackDate <=GETDATE() ");
            }

            if (!string.IsNullOrEmpty(tourCode))
            {
                cmdQuery.AppendFormat(" AND TourNo LIKE '%{0}%' ", tourCode);
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (tourDays.HasValue)
            {
                cmdQuery.AppendFormat(" AND TourDays={0} ", tourDays.Value);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }

            if (!string.IsNullOrEmpty(userId) || orderState.Length > 0)
            {
                cmdQuery.Append(" AND EXISTS (SELECT 1 FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=view_TourList_HavingOrderTour.Id ");

                if (!string.IsNullOrEmpty(userId))
                {
                    cmdQuery.AppendFormat(" AND LastOperatorID='{0}' ", userId);
                }

                if (orderState != null && orderState.Length > 0)
                {
                    cmdQuery.Append(" AND OrderState IN( ");
                    cmdQuery.Append((int)orderState[0]);
                    for (int i = 1; i < orderState.Length; i++)
                    {
                        cmdQuery.AppendFormat(",{0}", (int)orderState[i]);
                    }

                    cmdQuery.Append(" ) ");
                }

                cmdQuery.Append(" ) ");
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND AreaId IN ({0}) ", userAreas);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.HavingOrderTourInfo tmp = new EyouSoft.Model.TourStructure.HavingOrderTourInfo();

                    tmp.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tmp.TourNo = rdr["TourNo"].ToString();
                    tmp.RouteName = rdr["RouteName"].ToString();
                    tmp.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tmp.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("BuyCompanyNumber")))
                        tmp.BuyCompanyNumber = rdr.GetInt32(rdr.GetOrdinal("BuyCompanyNumber"));
                    if (!rdr.IsDBNull(rdr.GetOrdinal("OrderPeopleNumber")))
                        tmp.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));

                    tours.Add(tmp);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取MQ订单提醒的团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="tourId">团队编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo> GetMQRemindHavingOrderTours(int pageSize, int pageIndex, ref int recordCount
            , string companyId, string userId, string userAreas, string tourId, params EyouSoft.Model.TourStructure.OrderState[] orderState)
        {
            IList<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo> tours = new List<EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            StringBuilder fields = new StringBuilder();
            StringBuilder tmpQuery = new StringBuilder();

            if (!string.IsNullOrEmpty(userId) || orderState != null || orderState.Length > 0)
            {

                if (!string.IsNullOrEmpty(userId))
                {
                    tmpQuery.AppendFormat(" AND LastOperatorID='{0}' ", userId);
                }

                if (orderState != null && orderState.Length > 0)
                {
                    tmpQuery.Append(" AND OrderState IN( ");
                    tmpQuery.Append((int)orderState[0]);
                    for (int i = 1; i < orderState.Length; i++)
                    {
                        tmpQuery.AppendFormat(",{0}", (int)orderState[i]);
                    }

                    tmpQuery.Append(" ) ");
                }
            }


            fields.Append("ID,TourNo,RouteName,LeaveDate");
            fields.Append(",(PlanPeopleCount-OrderPeopleNumber) AS RealRemnantNumber ");
            fields.AppendFormat(" ,(SELECT COUNT(*) FROM (SELECT BuyCompanyID FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0} GROUP BY BuyCompanyID) AS A) AS BuyCompanyNumber ", tmpQuery.ToString());
            fields.AppendFormat(" ,(SELECT COUNT(*) FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) AS OrderNumber ", tmpQuery.ToString());
            fields.AppendFormat(" ,(SELECT SUM(PeopleNumber) FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) AS BuySeatNumber ", tmpQuery.ToString());

            cmdQuery.AppendFormat(" CompanyId='{0}' ", companyId);
            cmdQuery.AppendFormat(" AND EXISTS (SELECT 1 FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) ", tmpQuery.ToString());

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND AreaId IN ({0}) ", userAreas);
            }

            if (!string.IsNullOrEmpty(tourId))
            {
                cmdQuery.AppendFormat(" AND Id='{0}' ", tourId);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    tours.Add(new EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo
                    {
                        TourId = rdr.GetString(rdr.GetOrdinal("ID")),
                        TourNo = rdr["TourNo"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate")),
                        BuyCompanyNumber = rdr.IsDBNull(rdr.GetOrdinal("BuyCompanyNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuyCompanyNumber")),
                        OrderNumber = rdr.IsDBNull(rdr.GetOrdinal("OrderNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNumber")),
                        BuySeatNumber = rdr.IsDBNull(rdr.GetOrdinal("BuySeatNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuySeatNumber")),
                        //PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount")),
                        //OrderPeopleNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"))  
                        RealRemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RealRemnantNumber"))
                    });
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取MQ订单提醒的历史团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual int GetMQRemindHistoryToursCount(string companyId, string userId, string userAreas)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.AppendFormat("SELECT COUNT(*) AS Number FROM tbl_TourList AS A WHERE A.CompanyId='{0}' AND ComeBackDate <=GETDATE() ", companyId);

            cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_TourOrder AS B WHERE B.TourId=A.Id AND B.IsDelete='0' ");
            if (!string.IsNullOrEmpty(userId))
            {
                cmdText.AppendFormat(" AND B.LastOperatorID='{0}' ", userId);
            }
            cmdText.Append(" ) ");

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND A.AreaId IN({0}) ", userAreas);
            }

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }

        /*/// <summary>
        /// 获取平台推荐产品信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="areaType">线路区域类型</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="siteId">分站编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <param name="companyId">公司(或经营单位)编号 为null时不做为查询条件</param>
        /// <param name="isUnit">是否是经营单位</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="companyName">公司名称 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetRecommendTours(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.SystemStructure.AreaType areaType, int? areaId, int siteId,
            EyouSoft.Model.TourStructure.TourDisplayType displayType, string companyId, bool isUnit,
            string routeName, string companyName, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,UnitCompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime";

            #region 拼接查询条件
            cmdQuery.Append(" LeaveDate>=GETDATE() AND IsChecked='1' AND IsDelete='0' AND ParentTourID>'' ");
            cmdQuery.AppendFormat(" AND RouteType={0} ", (int)areaType);
            cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_CompanySiteControl WHERE CompanyId=tbl_TourList.CompanyId  AND SiteId={0} ", siteId);

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            cmdQuery.Append("AND IsShow='1' ");

            cmdQuery.Append(" ) ");

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl WHERE TourId=tbl_TourList.Id AND CityId={0}) ", siteId);

            if (displayType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
            {
                cmdQuery.Append(" AND IsRecentLeave='1' ");
            }

            if (!string.IsNullOrEmpty(companyId))
            {
                if (!isUnit)
                {
                    cmdQuery.AppendFormat(" AND CompanyId='{0}' AND UnitCompanyId='' ", companyId);
                }
                else
                {
                    cmdQuery.AppendFormat(" AND UnitCompanyId='{0}' ", companyId);
                }
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", companyName);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourInfo tour = new EyouSoft.Model.TourStructure.TourInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.UnitCompanyId = rdr.GetString(rdr.GetOrdinal("UnitCompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));

                    tour.TourPriceDetail = this.GetTourPriceDetail(tour.ID);

                    tours.Add(tour);
                }
            }

            return tours;
        }*/

        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="tourDays">团队天数 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <param name="isDT">是否按模板团显示</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourSpreadState">团队推广状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(int pageSize, int pageIndex, ref int recordCount,
            string companyId, int? cityId,
            string routeName, int? tourDays, DateTime? startLeaveDate, DateTime? finishLeaveDate, bool isDT
            , int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice,ParentTourID,OrderPeopleNumber,PlanPeopleCount,RouteType,ShowCount,TourContacMQ";

            #region 拼接查询条件

            cmdQuery.Append(" LeaveDate>=GETDATE() AND IsChecked='1' AND IsDelete='0' AND ParentTourID>'' ");

            if (isDT)
            {
                cmdQuery.Append(" AND IsRecentLeave='1' ");
            }

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourCityControl WHERE TourId=tbl_TourList.Id AND CityId={0}) ", cityId);
            }

            cmdQuery.AppendFormat(" AND CompanyId='{0}' ", companyId);

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (tourDays.HasValue)
            {
                cmdQuery.AppendFormat(" AND TourDays={0} ", tourDays);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            if (tourSpreadState.HasValue)
            {
                cmdQuery.AppendFormat(" AND RouteState={0} ", (int)tourSpreadState.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourInfo tour = new EyouSoft.Model.TourStructure.TourInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();

                    tours.Add(tour);
                }
            }

            return tours;
        }
        /// <summary>
        /// 获取团队信息集合（未出发，已出发都包含）   
        /// 注意：这里并没有带出所有团队信息
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="TourNo">团号</param>
        /// <param name="RouteName">线路名称</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始日期 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止日期 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetTours(int pageSize, int pageIndex, ref int recordCount,
            string CompanyId,string TourNo,string RouteName, int? areaId, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate DESC";
            string fields = "Id,RouteName,TourNo,LeaveDate,CompanyId,CompanyName,(SELECT AreaName FROM [tbl_SysArea] WHERE [ID]=tbl_TourList.AreaId) AS AreaName,ParentTourID,AreaId";

            #region 拼接查询条件

            cmdQuery.Append("  IsChecked='1' AND IsDelete='0' AND ParentTourID>'' ");

            cmdQuery.AppendFormat(" AND CompanyId='{0}' ", CompanyId);

            if (!string.IsNullOrEmpty(RouteName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", RouteName);
            }
            if (!string.IsNullOrEmpty(TourNo))
            {
                cmdQuery.AppendFormat(" AND TourNo LIKE '%{0}%' ", TourNo);
            }
            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            #endregion
            EyouSoft.Model.TourStructure.TourInfo tour = null;
            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    tour = new EyouSoft.Model.TourStructure.TourInfo();
                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));                    
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.TourNo = rdr.GetString(rdr.GetOrdinal("TourNo"));
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));                    
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.AreaName = rdr.GetString(rdr.GetOrdinal("AreaName"));
                    tour.AreaId = rdr.GetInt32(rdr.GetOrdinal("AreaId"));   
                    tours.Add(tour);
                }
                tour = null;
            }            
            return tours;

        }
        /// <summary>
        /// 获取高级网店团队信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="tourSpreadState">团队推广状态 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetEshopTours(string companyId, int? cityId, int expression, int? areaId, EyouSoft.Model.TourStructure.TourSpreadState? tourSpreadState)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT TOP(@Expression) Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice,ParentTourID,OrderPeopleNumber,PlanPeopleCount,RouteType,TourContacMQ,ShowCount FROM tbl_TourList ");
            base.TourStore.AddInParameter(cmd, "Expression", DbType.Int32, expression);
            cmdText.Append(" WHERE CompanyId=@CompanyId AND IsDelete='0' AND LeaveDate>=GETDATE() AND IsChecked='1' ");
            cmdText.Append(" AND IsRecentLeave='1' ");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            if (cityId.HasValue)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_TourCityControl WHERE TourId=tbl_TourList.Id AND CityId=@CityId) ");
                base.TourStore.AddInParameter(cmd, "CityId", DbType.Int32, cityId.Value);
            }

            if (areaId.HasValue)
            {
                cmdText.Append(" AND AreaId=@AreaId ");
                base.TourStore.AddInParameter(cmd, "AreaId", DbType.Int32, areaId.Value);
            }

            if (tourSpreadState.HasValue)
            {
                cmdText.Append(" AND RouteState=@TourSpreadState ");
                base.TourStore.AddInParameter(cmd, "TourSpreadState", DbType.Byte, (int)tourSpreadState.Value);
            }

            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourInfo tour = new EyouSoft.Model.TourStructure.TourInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();

                    tours.Add(tour);
                }
            }

            return tours;
        }


        /// <summary>
        /// 获取所有子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetChildrenTours(string tourId)
        {
            IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> tours = new List<EyouSoft.Model.TourStructure.ChildrenTourInfo>();
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourInfo_Children);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.ChildrenTourInfo tmp = new EyouSoft.Model.TourStructure.ChildrenTourInfo();

                    tmp.ChildrenId = rdr.GetString(0);
                    tmp.TourCode = rdr[1].ToString();
                    tmp.LeaveDate = rdr.GetDateTime(2);
                    tmp.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(3);
                    tmp.TravelAdultPrice = rdr.GetDecimal(4);
                    tmp.TravelChildrenPrice = rdr.GetDecimal(5);
                    tmp.RetailAdultPrice = rdr.GetDecimal(6);
                    tmp.RetailChildrenPrice = rdr.GetDecimal(7);
                    tmp.RoomDiffSettlementPrice = rdr.GetDecimal(8);
                    tmp.RoomDiffRetailPrice = rdr.GetDecimal(9);
                    tmp.RemnantNumber = rdr.GetInt32(10);
                    tmp.BuySeatNumber = rdr.GetInt32(11);
                    tmp.PlanPeopleCount = rdr.GetInt32(12);

                    tours.Add(tmp);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取未出发子团信息集合
        /// </summary>
        /// <param name="tourId">模板团编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> GetNotStartingChildrenTours(string tourId)
        {
            IList<EyouSoft.Model.TourStructure.ChildrenTourInfo> tours = new List<EyouSoft.Model.TourStructure.ChildrenTourInfo>();
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetNotStartingChildrenTours);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.ChildrenTourInfo tmp = new EyouSoft.Model.TourStructure.ChildrenTourInfo();

                    tmp.ChildrenId = rdr.GetString(0);
                    tmp.TourCode = rdr[1].ToString();
                    tmp.LeaveDate = rdr.GetDateTime(2);
                    tmp.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(3);
                    tmp.TravelAdultPrice = rdr.GetDecimal(4);
                    tmp.TravelChildrenPrice = rdr.GetDecimal(5);
                    tmp.RetailAdultPrice = rdr.GetDecimal(6);
                    tmp.RetailChildrenPrice = rdr.GetDecimal(7);
                    tmp.RoomDiffSettlementPrice = rdr.GetDecimal(8);
                    tmp.RoomDiffRetailPrice = rdr.GetDecimal(9);
                    tmp.RemnantNumber = rdr.GetInt32(10);
                    tmp.BuySeatNumber = rdr.GetInt32(11);
                    tmp.PlanPeopleCount = rdr.GetInt32(12);

                    tours.Add(tmp);
                }
            }

            return tours;
        }

        /// <summary>
        /// 获取普通网店团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="cityId">城市编号 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourInfo> GetNormalEshopTours(int pageSize, int pageIndex, ref int recordCount,
            int? cityId, int? areaId, string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType)
        {
            IList<EyouSoft.Model.TourStructure.TourInfo> tours = new List<EyouSoft.Model.TourStructure.TourInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            string fields = "Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime,ParentTourID,OrderPeopleNumber,PlanPeopleCount,RouteType,ShowCount,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice,TourContacMQ";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" CompanyId='{0}' AND LeaveDate>=GETDATE() AND IsChecked='1' AND IsDelete='0' AND ParentTourID>'' ", companyId);

            if (cityId.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourCityControl WHERE TourId=tbl_TourList.Id AND CityId={0}) ", cityId);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", areaId.Value);
            }

            if (displayType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
            {
                cmdQuery.Append(" AND IsRecentLeave='1' ");
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourInfo tour = new EyouSoft.Model.TourStructure.TourInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();


                    tours.Add(tour);
                }
            }

            return tours;
        }

        /// <summary>
        /// 按照指定条件获取团队信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="isPopularize">是否是推广的</param>
        /// <param name="searchType">搜索类型</param>
        /// <param name="searchInfo">搜索条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetTours(int pageSize, int pageIndex, ref int recordCount, bool isPopularize
            , EyouSoft.Model.TourStructure.TourSearchType searchType, EyouSoft.Model.TourStructure.TourSearchInfo searchInfo)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC,CreateTime ASC";
            string fields = "Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice,RouteType,ParentTourID,OrderPeopleNumber,PlanPeopleCount,TourContacMQ,ShowCount";

            #region 拼接查询条件
            cmdQuery.AppendFormat(" EXISTS(SELECT 1 FROM tbl_TourCityControl WHERE CityId={0} AND TourId=tbl_TourList.Id)", searchInfo.CityId);
            cmdQuery.Append(" AND IsDelete='0' ");

            if (searchType == EyouSoft.Model.TourStructure.TourSearchType.Other && searchInfo != null && (searchInfo.StartLeaveDate.HasValue || searchInfo.FinishLeaveDate.HasValue))
            {
                //以子团方式展示计划信息
            }
            else
            {
                cmdQuery.Append(" AND IsRecentLeave='1' ");
            }

            if (searchInfo.IsAllHistory)
            {
                cmdQuery.Append(" AND LeaveDate<=GETDATE() ");
            }
            else
            {
                cmdQuery.Append(" AND LeaveDate>=GETDATE() ");
            }

            if (searchInfo.AreaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", searchInfo.AreaId.Value);
            }

            if (isPopularize)
            {
                if (searchInfo.AreaId.HasValue)
                {
                    cmdQuery.AppendFormat(" AND CompanyId IN(SELECT CompanyId FROM tbl_CompanyCityAd WHERE CityId={0} AND AreaId={1}) ", searchInfo.CityId, searchInfo.AreaId);
                }
                else
                {
                    cmdQuery.AppendFormat(" AND CompanyId IN(SELECT CompanyId FROM tbl_CompanyCityAd WHERE CityId={0}) ", searchInfo.CityId);
                }
            }

            switch (searchType)
            {
                case EyouSoft.Model.TourStructure.TourSearchType.Company:
                    cmdQuery.AppendFormat(" AND CompanyId='{0}' ", searchInfo.CompanyId);
                    break;
                case EyouSoft.Model.TourStructure.TourSearchType.None: break;
                case EyouSoft.Model.TourStructure.TourSearchType.Other:
                    if (!string.IsNullOrEmpty(searchInfo.RouteName))
                    {
                        cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", searchInfo.RouteName);
                    }

                    if (!string.IsNullOrEmpty(searchInfo.CompanyName))
                    {
                        cmdQuery.AppendFormat(" AND CompanyName LIKE '%{0}%' ", searchInfo.CompanyName);
                    }

                    if (searchInfo.TourDays.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND TourDays={0} ", searchInfo.TourDays.Value);
                    }

                    if (searchInfo.StartLeaveDate.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", searchInfo.StartLeaveDate.Value);
                    }

                    if (searchInfo.FinishLeaveDate.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", searchInfo.FinishLeaveDate.Value);
                    }

                    /*if (searchInfo.StartLeaveDate.HasValue || searchInfo.FinishLeaveDate.HasValue)
                    {
                        cmdQuery.Append(" AND EXISTS(SELECT 1 FROM tbl_TourList AS A WHERE A.ParentTourID=tbl_TourList.ParentTourID ");
                        if (searchInfo.StartLeaveDate.HasValue)
                        {
                            cmdQuery.AppendFormat(" AND A.LeaveDate>='{0}' ", searchInfo.StartLeaveDate.Value);
                        }

                        if (searchInfo.FinishLeaveDate.HasValue)
                        {
                            cmdQuery.AppendFormat(" AND A.LeaveDate<='{0}' ", searchInfo.FinishLeaveDate.Value);
                        }
                        cmdQuery.Append(")");
                    }*/

                    if (!string.IsNullOrEmpty(searchInfo.CompanyId))
                    {
                        cmdQuery.AppendFormat(" AND CompanyId='{0}' ", searchInfo.CompanyId);
                    }
                    break;
                case EyouSoft.Model.TourStructure.TourSearchType.Price:
                    /*if (searchInfo.MinPrice.HasValue||searchInfo.MaxPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourBasicPriceDetail WHERE TourBasicID=tbl_TourList.Id ");

                        if (searchInfo.MinPrice.HasValue && searchInfo.MaxPrice.HasValue)
                        {
                            cmdQuery.AppendFormat(" AND (PerosonalPrice BETWEEN {0} AND {1} OR ChildPrice BETWEEN {0} AND {1}) ", searchInfo.MinPrice.Value, searchInfo.MinPrice.Value);
                        }
                        else if (searchInfo.MinPrice.HasValue)
                        {
                            cmdQuery.AppendFormat(" AND (PerosonalPrice>={0} OR ChildPrice>={0}) ", searchInfo.MinPrice.Value);
                        }
                        else
                        {
                            cmdQuery.AppendFormat(" AND (PerosonalPrice<={0} OR ChildPrice<={0}) ", searchInfo.MaxPrice.Value);
                        }

                        cmdQuery.Append(" ) ");
                    }*/
                    /*if (searchInfo.MinPrice.HasValue && searchInfo.MaxPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND (PersonalPrice BETWEEN {0} AND {1} OR ChildPrice BETWEEN {0} AND {1} OR RetailAdultPrice BETWEEN {0} AND {1} OR RetailChildrenPrice BETWEEN {0} AND {1}) ", searchInfo.MinPrice.Value, searchInfo.MaxPrice.Value);
                    }
                    else if (searchInfo.MinPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND (PersonalPrice>={0} OR ChildPrice>={0} OR RetailAdultPrice>={0} OR RetailChildrenPrice>={0}) ", searchInfo.MinPrice.Value);
                    }
                    else if (searchInfo.MaxPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND (PersonalPrice<={0} OR ChildPrice<={0} OR RetailAdultPrice<={0} OR RetailChildrenPrice<={0}) ", searchInfo.MaxPrice.Value);
                    }*/

                    if (searchInfo.MinPrice.HasValue && searchInfo.MaxPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND ({0} BETWEEN {1} AND {2}) ", searchInfo.IsLogin ? "PersonalPrice" : "RetailAdultPrice", searchInfo.MinPrice.Value, searchInfo.MaxPrice.Value);
                    }
                    else if (searchInfo.MinPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND ({0}>={1}) ", searchInfo.IsLogin ? "PersonalPrice" : "RetailAdultPrice", searchInfo.MinPrice.Value);
                    }
                    else if (searchInfo.MaxPrice.HasValue)
                    {
                        cmdQuery.AppendFormat(" AND ({0}<={1}) ", searchInfo.IsLogin ? "PersonalPrice" : "RetailAdultPrice", searchInfo.MaxPrice.Value);
                    }
                    break;
                case EyouSoft.Model.TourStructure.TourSearchType.Theme:
                    cmdQuery.AppendFormat("AND Id IN(SELECT TourId FROM tbl_TourThemeControl WHERE ThemeId={0}) ", searchInfo.ThemeId);
                    break;
                case EyouSoft.Model.TourStructure.TourSearchType.TourDay:
                    if (searchInfo.TourDaysType < 0)
                    {
                        cmdQuery.AppendFormat(" AND TourDays<={0} ", searchInfo.TourDays.Value);
                    }
                    else if (searchInfo.TourDaysType == 0)
                    {
                        cmdQuery.AppendFormat(" AND TourDays={0} ", searchInfo.TourDays.Value);
                    }
                    else
                    {
                        cmdQuery.AppendFormat(" AND TourDays>={0} ", searchInfo.TourDays.Value);
                    }
                    break;
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourBasicInfo tour = new EyouSoft.Model.TourStructure.TourBasicInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();

                    tours.Add(tour);
                }
            }

            return tours;
        }
        #endregion

        #region 统计相关
        /// <summary>
        /// 获取平台下批发商有效团队数量
        /// </summary>
        /// <returns></returns>
        public virtual int GetPlatformValidTourNumber()
        {
            int tourNumber = 0;
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetPlatformValidTourNumber);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    tourNumber = rdr.GetInt32(0);
                }
            }

            return tourNumber;
        }

        /// <summary>
        /// 获取关注批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="days">即将出团所定义的天数</param>
        /// <returns></returns>
        public virtual int GetAttentionComingLeaveTourNumber(string companyId, int days)
        {
            int comingLeaveTourNumber = 0;

            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();

            cmdText.Append(" SELECT COUNT(*) AS ComingLeaveTourNumber FROM tbl_TourList AS A ");
            cmdText.Append(" INNER JOIN tbl_CompanyFavor AS B ON A.CompanyId=B.FavorCompanyId ");
            cmdText.Append(" AND B.CompanyId=@CompanyId ");
            cmdText.Append(" WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1' ");
            cmdText.Append(" AND A.LeaveDate<=@ComingLeaveDate ");

            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            base.TourStore.AddInParameter(cmd, "ComingLeaveDate", DbType.DateTime, DateTime.Today.AddDays(days));

            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    comingLeaveTourNumber = rdr.GetInt32(0);
                }
            }

            return comingLeaveTourNumber;
        }

        /// <summary>
        /// 获取关注批发商有效产品按线路区域类型统计信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号 为null时不做为查询条件</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TourStructure.AreaTypeStatInfo GetAttentionTourByAreaTypeStats(string companyId, string userId, int? leaveCityId)
        {
            StringBuilder cmdText = new StringBuilder();
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");

            #region 拼接查询语句
            cmdText.Append(" SELECT A.RouteType,COUNT(*) AS ValidTourNumber FROM tbl_TourList AS A ");
            cmdText.Append(" INNER JOIN tbl_CompanyFavor AS B ON A.CompanyId=B.FavorCompanyId ");
            cmdText.Append(" AND B.CompanyId=@CompanyId ");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, companyId);
            cmdText.Append(" WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1' AND IsRecentLeave='1' ");

            /*if (!string.IsNullOrEmpty(userId))
            {
                cmdText.Append(" AND A.AreaId IN(SELECT AreaId FROM tbl_CompanyUserAreaControl WHERE UserId=@UserId) ");
                base.TourStore.AddInParameter(cmd, "UserId", DbType.String, userId);
            }*/

            if (leaveCityId.HasValue)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl WHERE TourId=A.Id AND CityId=@CityId) ");
                base.TourStore.AddInParameter(cmd, "CityId", DbType.Int32, leaveCityId.Value);
            }

            cmdText.Append(" GROUP BY A.RouteType ");
            #endregion

            cmd.CommandText = cmdText.ToString();

            EyouSoft.Model.TourStructure.AreaTypeStatInfo stat = new EyouSoft.Model.TourStructure.AreaTypeStatInfo();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    switch ((EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(0))
                    {
                        case EyouSoft.Model.SystemStructure.AreaType.国内长线: stat.Long = rdr.GetInt32(1); break;
                        case EyouSoft.Model.SystemStructure.AreaType.国内短线: stat.Short = rdr.GetInt32(1); break;
                        case EyouSoft.Model.SystemStructure.AreaType.国际线: stat.Exit = rdr.GetInt32(1); break;
                    }
                }
            }

            return stat;
        }

        /// <summary>
        /// 获取关注批发商有效产品按线路区域统计信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="leaveCityId">出港城市编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetAttentionTourByAreaStats(string companyId, int? leaveCityId)
        {
            StringBuilder cmdText = new StringBuilder();
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");

            #region 拼接查询语句
            cmdText.Append(" SELECT A.AreaId,COUNT(*) AS ValidTourNumber FROM tbl_TourList AS A ");
            cmdText.Append(" INNER JOIN tbl_CompanyFavor AS B ON A.CompanyId=B.FavorCompanyId ");
            cmdText.Append(" AND B.CompanyId=@CompanyId ");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            cmdText.Append(" WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1' ");

            if (leaveCityId.HasValue)
            {
                cmdText.Append(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl WHERE TourId=A.Id AND CityId=@CityId) ");
                base.TourStore.AddInParameter(cmd, "CityId", DbType.Int32, leaveCityId.Value);
            }

            cmdText.Append(" GROUP BY A.AreaId ");
            #endregion

            cmd.CommandText = cmdText.ToString();

            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = new List<EyouSoft.Model.TourStructure.AreaStatInfo>();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TourStructure.AreaStatInfo(rdr.GetInt32(0), "", rdr.GetInt32(1)));
                }
            }

            return stats;
        }

        /// <summary>
        /// 获取批发商即将出团的团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="days">即将出团所定义的天数</param>
        /// <returns></returns>
        public virtual int GetComingLeaveTourNumber(string companyId, string userAreas, int days)
        {
            int comingLeaveTourNumber = 0;
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT COUNT(*) AS ComingLeaveTourNumber FROM tbl_TourList AS A ");
            cmdText.Append(" WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1' ");
            cmdText.Append(" AND A.CompanyId=@CompanyId ");
            cmdText.Append(" AND A.LeaveDate<=@ComingLeaveDate ");

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND A.AreaId IN({0}) ", userAreas);
            }

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());

            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            base.TourStore.AddInParameter(cmd, "ComingLeaveDate", DbType.DateTime, DateTime.Today.AddDays(days));

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    comingLeaveTourNumber = rdr.GetInt32(0);
                }
            }

            return comingLeaveTourNumber;
        }

        /// <summary>
        /// 获取批发商是否有发布过团队
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual bool IsReleaseTour(string companyId, string userAreas)
        {
            bool isRelease = false;
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(SQL_SELECT_GetTourNumber);

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND AreaId IN({0}) ", userAreas);
            }

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    isRelease = rdr.GetInt32(0) > 0 ? true : false;
                }
            }

            return isRelease;
        }

        /// <summary>
        /// 获取批发商有效团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="tourState">团队状态</param>
        /// <returns></returns>
        public virtual int GetTourNumber(string companyId, string userAreas, EyouSoft.Model.TourStructure.TourState? tourState)
        {
            int tourNumber = 0;
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();

            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            cmdText.Append(SQL_SELECT_GetTourNumber + " AND LeaveDate>=GETDATE() AND IsDelete='0' AND IsChecked='1' ");

            if (tourState.HasValue)
            {
                cmdText.Append(" AND TourState=@TourState ");
                base.TourStore.AddInParameter(cmd, "TourState", DbType.Byte, (byte)tourState);
            }

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND AreaId IN({0}) ", userAreas);
            }

            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    tourNumber = rdr.GetInt32(0);
                }
            }

            return tourNumber;
        }

        /// <summary>
        /// 获取团队按订单统计信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourCode">团号 为null时不做为查询条件</param>
        /// <param name="leaveCity">出港城市编号 为null时不做为查询条件</param>
        /// <param name="routeName">线路名称 为null时不做为查询条件</param>
        /// <param name="startLeaveDate">出团起始时间 为null时不做为查询条件</param>
        /// <param name="finishLeaveDate">出团截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourStatByOrderInfo> GetTourStatsByOrder(int pageSize, int pageIndex, ref int recordCount, string tourCode
            , int? leaveCity, string routeName, DateTime? startLeaveDate, DateTime? finishLeaveDate)
        {
            IList<EyouSoft.Model.TourStructure.TourStatByOrderInfo> stats = new List<EyouSoft.Model.TourStructure.TourStatByOrderInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC";
            StringBuilder fields = new StringBuilder();
            fields.Append("Id,RouteName,TourNo,LeaveDate,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice");
            fields.Append(" ,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=tbl_TourList.Id AND OrderState=5) AS AdultNumber ");
            fields.Append(" ,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=tbl_TourList.Id AND OrderState=5) AS ChildrenNumber ");
            fields.Append(" ,(SELECT COUNT(*) FROM tbl_TourOrder WHERE TourId=tbl_TourList.Id AND OrderState=5) AS OrderNumber ");
            fields.Append(" ,(SELECT SUM(SumPrice) FROM tbl_TourOrder WHERE TourId=tbl_TourList.Id AND OrderState=5) AS TotalIncome");

            #region 拼接查询条件
            cmdQuery.Append(" IsDelete='0' AND EXISTS(SELECT 1 FROM tbl_TourOrder WHERE TourId=tbl_TourList.Id) ");

            if (!string.IsNullOrEmpty(tourCode))
            {
                cmdQuery.AppendFormat(" AND TourNo LIKE '%{0}%' ", tourCode);
            }

            if (leaveCity.HasValue)
            {
                cmdQuery.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourAreaControl WHERE TourId=tbl_TourList.Id AND CityId={0}) ", leaveCity.Value);
            }

            if (!string.IsNullOrEmpty(routeName))
            {
                cmdQuery.AppendFormat(" AND RouteName LIKE '%{0}%' ", routeName);
            }

            if (startLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate>='{0}' ", startLeaveDate.Value);
            }

            if (finishLeaveDate.HasValue)
            {
                cmdQuery.AppendFormat(" AND LeaveDate<='{0}' ", finishLeaveDate.Value);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourStatByOrderInfo tmp = new EyouSoft.Model.TourStructure.TourStatByOrderInfo();

                    tmp.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tmp.RouteName = rdr["RouteName"].ToString();
                    tmp.TourNo = rdr["TourNo"].ToString();
                    tmp.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tmp.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tmp.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tmp.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tmp.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tmp.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tmp.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tmp.CollectAdultNumber = rdr.GetInt32(rdr.GetOrdinal("AdultNumber"));
                    tmp.CollectChildrenNumber = rdr.GetInt32(rdr.GetOrdinal("ChildrenNumber"));
                    tmp.OrderNumber = rdr.GetInt32(rdr.GetOrdinal("OrderNumber"));
                    tmp.TotalIncome = rdr.GetDecimal(rdr.GetOrdinal("TotalIncome"));

                    stats.Add(tmp);
                }
            }

            return stats;
        }

        /// <summary>
        /// 获取指定城市的模板团队和子团数量
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.SysCity GetCityTourNumber(int cityId)
        {
            EyouSoft.Model.SystemStructure.SysCity tmp = null;

            DbCommand cmd = base.SystemStore.GetSqlStringCommand(SQL_SELECT_GetCityTourNumber);
            base.SystemStore.AddInParameter(cmd, "CityId", DbType.Int32, cityId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.SystemStore))
            {
                if (rdr.Read())
                {
                    tmp = new EyouSoft.Model.SystemStructure.SysCity();

                    tmp.CityId = cityId;
                    tmp.ParentTourCount = rdr.GetInt32(0);
                    tmp.TourCount = rdr.GetInt32(1);
                }
            }

            return tmp;
        }

        /// <summary>
        /// 获取批发商有效产品按线路区域统计信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.AreaStatInfo> GetTourByAreaStats(string companyId, string userAreas)
        {
            IList<EyouSoft.Model.TourStructure.AreaStatInfo> stats = new List<EyouSoft.Model.TourStructure.AreaStatInfo>();

            StringBuilder cmdText = new StringBuilder();
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");

            #region 拼接查询语句
            cmdText.Append(" SELECT A.AreaId,COUNT(*) AS ValidTourNumber FROM tbl_TourList AS A ");
            cmdText.Append(" WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1' ");
            cmdText.Append(" AND A.CompanyId=@CompanyId ");
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND A.AreaId IN({0}) ", userAreas);
            }

            cmdText.Append(" GROUP BY A.AreaId ");
            #endregion

            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TourStructure.AreaStatInfo(rdr.GetInt32(0), "", rdr.GetInt32(1)));
                }
            }

            return stats;
        }

        /// <summary>
        /// 获取批发商快到期产品数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="expireTime">到期时间</param>
        /// <param name="userAreas">用户的线路区域 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual int GetComingExpireTourNumber(string companyId, DateTime expireTime, string userAreas)
        {
            int expireTourNumber = 0;

            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(" SELECT COUNT(*) AS ExpireTourNumber FROM view_TourList_ComingExpireTour ");
            cmdText.Append(" WHERE CompanyId=@CompanyId AND LeaveDate<=@ExpireTime ");

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND AreaId IN({0}) ", userAreas);
            }

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            base.TourStore.AddInParameter(cmd, "ExpireTime", DbType.DateTime, expireTime);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    expireTourNumber = rdr.GetInt32(0);
                }
            }

            return expireTourNumber;
        }

        /// <summary>
        /// 获取MQ订单提醒的团队信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourId">团队编号</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo GetMQRemindTourInfo(string companyId, string tourId, params EyouSoft.Model.TourStructure.OrderState[] orderState)
        {
            EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo tourInfo = null;

            StringBuilder cmdText = new StringBuilder();
            StringBuilder fields = new StringBuilder();
            StringBuilder tmpQuery = new StringBuilder();

            if (orderState != null && orderState.Length > 0)
            {
                if (orderState != null && orderState.Length > 0)
                {
                    tmpQuery.Append(" AND OrderState IN( ");
                    tmpQuery.Append((int)orderState[0]);
                    for (int i = 1; i < orderState.Length; i++)
                    {
                        tmpQuery.AppendFormat(",{0}", (int)orderState[i]);
                    }

                    tmpQuery.Append(" ) ");
                }
            }

            cmdText.Append("SELECT ");
            cmdText.Append(" ID,TourNo,RouteName,LeaveDate ");
            cmdText.Append(",(PlanPeopleCount-OrderPeopleNumber) AS RealRemnantNumber ");
            cmdText.AppendFormat(" ,(SELECT COUNT(*) FROM (SELECT BuyCompanyID FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0} GROUP BY BuyCompanyID) AS A) AS BuyCompanyNumber ", tmpQuery.ToString());
            cmdText.AppendFormat(" ,(SELECT COUNT(*) FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) AS OrderNumber ", tmpQuery.ToString());
            cmdText.AppendFormat(" ,(SELECT SUM(PeopleNumber) FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) AS BuySeatNumber ", tmpQuery.ToString());

            cmdText.AppendFormat(" FROM tbl_TourList WHERE Id='{0}' AND CompanyId='{1}' ", tourId, companyId);
            cmdText.AppendFormat(" AND EXISTS (SELECT 1 FROM tbl_TourOrder WHERE IsDelete='0' AND TourId=tbl_TourList.Id {0}) ", tmpQuery.ToString());

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    tourInfo = new EyouSoft.Model.TourStructure.MQRemindHavingOrderTourInfo
                    {
                        TourId = rdr.GetString(rdr.GetOrdinal("ID")),
                        TourNo = rdr["TourNo"].ToString(),
                        RouteName = rdr["RouteName"].ToString(),
                        LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate")),
                        BuyCompanyNumber = rdr.IsDBNull(rdr.GetOrdinal("BuyCompanyNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuyCompanyNumber")),
                        OrderNumber = rdr.IsDBNull(rdr.GetOrdinal("OrderNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("OrderNumber")),
                        BuySeatNumber = rdr.IsDBNull(rdr.GetOrdinal("BuySeatNumber")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("BuySeatNumber")),
                        RealRemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RealRemnantNumber"))
                    };
                }
            }

            return tourInfo;
        }

        /// <summary>
        /// 获取批发商有效团队数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="displayType">团队展示类型</param>
        /// <returns></returns>
        public virtual int GetTourNumber(string companyId, EyouSoft.Model.TourStructure.TourDisplayType displayType)
        {
            StringBuilder cmdText = new StringBuilder();
            cmdText.AppendFormat("SELECT COUNT(*) AS Number FROM tbl_TourList WHERE CompanyId='{0}' AND LeaveDate>=GETDATE() AND IsDelete='0' AND IsChecked='1' ", companyId);

            if (displayType == EyouSoft.Model.TourStructure.TourDisplayType.线路产品)
            {
                cmdText.Append(" AND IsRecentLeave='1' ");
            }

            DbCommand cmd = base.TourStore.GetSqlStringCommand(cmdText.ToString());

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    return rdr.GetInt32(0);
                }
            }

            return 0;
        }
        #endregion

        #region 团队访问记录
        /// <summary>
        /// 添加团队访问记录,并更新团队的浏览次数(浏览次数++)
        /// </summary>
        /// <param name="visitInfo">团队浏览记录信息业务实体</param>
        /// <returns></returns>
        public virtual bool InsertTourVisitedInfo(EyouSoft.Model.TourStructure.TourVisitInfo visitInfo)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_INSERT_UPDATE_InsertTourVisitedInfo);

            base.TourStore.AddInParameter(cmd, "Id", DbType.AnsiStringFixedLength, visitInfo.Id);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.String, visitInfo.TourId);
            base.TourStore.AddInParameter(cmd, "VisitedCompanyId", DbType.String, visitInfo.VisitedCompanyId);
            base.TourStore.AddInParameter(cmd, "VisitedCompanyName", DbType.String, visitInfo.VisitedCompanyName);
            base.TourStore.AddInParameter(cmd, "ClientIP", DbType.String, visitInfo.ClientIP);
            base.TourStore.AddInParameter(cmd, "ClientUserID", DbType.String, visitInfo.ClientUserId);
            base.TourStore.AddInParameter(cmd, "ClientUserContactName", DbType.String, visitInfo.ClientUserContactName);
            base.TourStore.AddInParameter(cmd, "ComanyName", DbType.String, visitInfo.ClientCompanyName);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.String, visitInfo.ClientCompanyId);
            base.TourStore.AddInParameter(cmd, "VisitType", DbType.Byte, visitInfo.VisitTourType);

            return DbHelper.ExecuteSql(cmd, base.TourStore) > 0 ? true : false;
        }

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)统计团队被访问的次数
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual int GetTourVisitedNumberByUser(string companyId, string userAreas)
        {
            int visitedNumber = 0;
            DbCommand cmd = base.TourStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();
            cmdText.Append(SQL_SELECT_GetTourVisitedNumberByUser);

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND EXISTS(SELECT 1 FROM tbl_TourList WEHRE Id=tbl_TourVisitInfo.TourId AND AreaId IN({0})) ", userAreas);
            }

            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);
            cmd.CommandText = cmdText.ToString();

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    visitedNumber = rdr.GetInt32(0);
                }
            }

            return visitedNumber;
        }

        /// <summary>
        /// 按团队分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="tourId">团队编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorys(int pageSize, int pageIndex, ref int recordCount, string tourId)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            string cmdQuery = string.Format(" VisitedTourId='{0}' ", tourId);
            string tableName = "view_TourVisitInfo_TourVisit";
            string primaryKey = "VisitedId";
            string orderByString = "VisitedTime DESC";
            string fields = "*";

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.Id = rdr.GetString(rdr.GetOrdinal("VisitedId"));
                    tmp.TourId = rdr.GetString(rdr.GetOrdinal("VisitedTourId"));
                    tmp.VisitedCompanyId = rdr.GetString(rdr.GetOrdinal("VisitedCompanyId"));
                    tmp.VisitedCompanyName = rdr["VisitedCompanyName"].ToString();
                    tmp.ClientIP = rdr["ClientIP"].ToString();
                    tmp.ClientUserId = rdr.GetString(rdr.GetOrdinal("ClientUserID"));
                    tmp.ClientUserContactName = rdr["ClientUserContactName"].ToString();
                    tmp.ClientCompanyName = rdr["ClientCompanyName"].ToString();
                    tmp.ClientCompanyId = rdr.GetString(rdr.GetOrdinal("ClientCompanyId"));
                    tmp.VisitedTime = rdr.GetDateTime(rdr.GetOrdinal("VisitedTime"));
                    tmp.VisitTourCode = rdr["VisitedTourNo"].ToString();
                    tmp.VisitTourRouteName = rdr["VisitedRouteName"].ToString();
                    tmp.ClientUserContactTelephone = rdr["ClientContactTelephone"].ToString();
                    tmp.ClientUserContactMobile = rdr["ClientContactMobile"].ToString();
                    tmp.ClinetUserContactQQ = rdr["ClientContactQQ"].ToString();
                    tmp.VisitTourType = (EyouSoft.Model.TourStructure.VisitTourType)rdr.GetByte(rdr.GetOrdinal("VisitType"));

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)、指定返回行数的数值表达式获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="compayId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(string compayId, string userAreas, int expression)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            StringBuilder cmdText = new StringBuilder();

            cmdText.Append("SELECT TOP(@Expression) CompanyId AS ClientCompanyId,ComanyName AS ClientCompanyName,ClientUserID,ClientUserContactName tbl_TourVisitInfo WHERE VisitedCompanyId=@CompanyId ");

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdText.AppendFormat(" AND VisitedTourAreaId IN({0}) ", userAreas);
            }

            cmdText.Append(" ORDER BY IssueTime DESC ");


            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetTourVisitedHistorysByUser);
            base.TourStore.AddInParameter(cmd, "Expression", DbType.Int32, expression);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, compayId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.ClientCompanyId = rdr.GetString(0);
                    tmp.ClientCompanyName = rdr[1].ToString();
                    tmp.ClientUserId = rdr.GetString(2);
                    tmp.ClientUserContactName = rdr[3].ToString();

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按批发商用户(用户分管的线路区域)分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="userAreas">当前用户分管的线路区域 多个线路区域用","间隔 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string companyId, string userAreas)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourVisitInfo_TourVisit";
            string primaryKey = "VisitedId";
            string orderByString = "VisitedTime DESC";
            string fields = "VisitedId, VisitedTourId, VisitedCompanyId, VisitedCompanyName, VisitedTourNo, VisitedRouteName, VisitedTime, ClientIP, ClientUserId, ClientUserContactName, ClientCompanyName, ClientCompanyId, ClientContactTelephone, ClientContactMobile, ClientContactQQ";

            cmdQuery.AppendFormat(" VisitedCompanyId='{0}' ", companyId);

            if (!string.IsNullOrEmpty(userAreas))
            {
                cmdQuery.AppendFormat(" AND VisitedTourAreaId IN({0}) ", userAreas);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.Id = rdr.GetString(rdr.GetOrdinal("VisitedId"));
                    tmp.TourId = rdr.GetString(rdr.GetOrdinal("VisitedTourId"));
                    tmp.VisitedCompanyId = rdr.GetString(rdr.GetOrdinal("VisitedCompanyId"));
                    tmp.VisitedCompanyName = rdr["VisitedCompanyName"].ToString();
                    tmp.ClientIP = rdr["ClientIP"].ToString();
                    tmp.ClientUserId = rdr.GetString(rdr.GetOrdinal("ClientUserID"));
                    tmp.ClientUserContactName = rdr["ClientUserContactName"].ToString();
                    tmp.ClientCompanyName = rdr["ClientCompanyName"].ToString();
                    tmp.ClientCompanyId = rdr.GetString(rdr.GetOrdinal("ClientCompanyId"));
                    tmp.VisitedTime = rdr.GetDateTime(rdr.GetOrdinal("VisitedTime"));
                    tmp.VisitTourCode = rdr["VisitedTourNo"].ToString();
                    tmp.VisitTourRouteName = rdr["VisitedRouteName"].ToString();
                    tmp.ClientUserContactTelephone = rdr["ClientContactTelephone"].ToString();
                    tmp.ClientUserContactMobile = rdr["ClientContactMobile"].ToString();
                    tmp.ClinetUserContactQQ = rdr["ClientContactQQ"].ToString();

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按批发商分页获取团队被访问的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="visitorCompanyName">访问者公司名称 为null时不做为查询条件</param>
        /// <param name="visitedRouteName">访问的线路名称 为null时不做为查询条件</param>
        /// <param name="startVisitedTime">访问开始时间 为null时不做为查询条件</param>
        /// <param name="finishVisitedTime">访问截止时间 为null时不做为查询条件</param>
        /// <param name="areaId">线路区域编号 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetTourVistedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount,
            string companyId, string visitorCompanyName, string visitedRouteName
            , DateTime? startVisitedTime, DateTime? finishVisitedTime, int? areaId)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourVisitInfo_TourVisit";
            string primaryKey = "VisitedId";
            string orderByString = "VisitedTime DESC";
            string fields = "*";

            cmdQuery.AppendFormat(" VisitedCompanyId='{0}' ", companyId);

            if (!string.IsNullOrEmpty(visitorCompanyName))
            {
                cmdQuery.AppendFormat(" AND ClientCompanyName LIKE '%{0}%' ", visitorCompanyName);
            }

            if (!string.IsNullOrEmpty(visitedRouteName))
            {
                cmdQuery.AppendFormat(" AND VisitedRouteName LIKE '%{0}%' ", visitedRouteName);
            }

            if (startVisitedTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND VisitedTime>='{0}' ", startVisitedTime.Value);
            }

            if (finishVisitedTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND VisitedTime<='{0}' ", finishVisitedTime.Value);
            }

            if (areaId.HasValue)
            {
                cmdQuery.AppendFormat(" AND VisitedTourAreaId={0} ", areaId.Value);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.Id = rdr.GetString(rdr.GetOrdinal("VisitedId"));
                    tmp.TourId = rdr.GetString(rdr.GetOrdinal("VisitedTourId"));
                    tmp.VisitedCompanyId = rdr.GetString(rdr.GetOrdinal("VisitedCompanyId"));
                    tmp.VisitedCompanyName = rdr["VisitedCompanyName"].ToString();
                    tmp.ClientIP = rdr["ClientIP"].ToString();
                    tmp.ClientUserId = rdr.GetString(rdr.GetOrdinal("ClientUserID"));
                    tmp.ClientUserContactName = rdr["ClientUserContactName"].ToString();
                    tmp.ClientCompanyName = rdr["ClientCompanyName"].ToString();
                    tmp.ClientCompanyId = rdr.GetString(rdr.GetOrdinal("ClientCompanyId"));
                    tmp.VisitedTime = rdr.GetDateTime(rdr.GetOrdinal("VisitedTime"));
                    tmp.VisitTourCode = rdr["VisitedTourNo"].ToString();
                    tmp.VisitTourRouteName = rdr["VisitedRouteName"].ToString();
                    tmp.ClientUserContactTelephone = rdr["ClientContactTelephone"].ToString();
                    tmp.ClientUserContactMobile = rdr["ClientContactMobile"].ToString();
                    tmp.ClinetUserContactQQ = rdr["ClientContactQQ"].ToString();
                    tmp.VisitTourType = (EyouSoft.Model.TourStructure.VisitTourType)rdr.GetByte(rdr.GetOrdinal("VisitType"));

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按访问团队的用户、指定返回行数的数值表达式获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="expression">指定返回行数的数值表达式</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(string userId, int expression)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetVisitedHistorysByUser);
            base.TourStore.AddInParameter(cmd, "Expression", DbType.Int32, expression);
            base.TourStore.AddInParameter(cmd, "UserId", DbType.AnsiStringFixedLength, userId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.TourId = rdr.GetString(0);
                    tmp.VisitedCompanyId = rdr.GetString(1);
                    tmp.VisitedCompanyName = rdr[2].ToString();
                    tmp.VisitTourCode = rdr[3].ToString();
                    tmp.VisitTourRouteName = rdr[4].ToString();
                    tmp.VisitedTime = rdr.GetDateTime(5);

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按访问团队的用户分页获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号 为空时不做为查询条件</param>
        /// <param name="userId">用户编号 为空时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByUser(int pageSize, int pageIndex, ref int recordCount, string companyId, string userId)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourVisitInfo_TourVisit";
            string primaryKey = "VisitedId";
            string orderByString = "VisitedTime DESC";
            string fields = "VisitedId, VisitedTourId, VisitedCompanyId, VisitedCompanyName, VisitedTourNo, VisitedRouteName, VisitedTime, ClientIP, ClientUserId, ClientUserContactName, ClientCompanyName, ClientCompanyId, ClientContactTelephone, ClientContactMobile, ClientContactQQ";

            cmdQuery.Append(" 1=1 ");

            if (!string.IsNullOrEmpty(companyId))
            {
                cmdQuery.AppendFormat(" AND ClientCompanyId='{0}' ", companyId);
            }

            if (!string.IsNullOrEmpty(userId))
            {
                cmdQuery.AppendFormat(" AND ClientUserId='{0}' ", userId);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.Id = rdr.GetString(rdr.GetOrdinal("VisitedId"));
                    tmp.TourId = rdr.GetString(rdr.GetOrdinal("VisitedTourId"));
                    tmp.VisitedCompanyId = rdr.GetString(rdr.GetOrdinal("VisitedCompanyId"));
                    tmp.VisitedCompanyName = rdr["VisitedCompanyName"].ToString();
                    tmp.ClientIP = rdr["ClientIP"].ToString();
                    tmp.ClientUserId = rdr.GetString(rdr.GetOrdinal("ClientUserID"));
                    tmp.ClientUserContactName = rdr["ClientUserContactName"].ToString();
                    tmp.ClientCompanyName = rdr["ClientCompanyName"].ToString();
                    tmp.ClientCompanyId = rdr.GetString(rdr.GetOrdinal("ClientCompanyId"));
                    tmp.VisitedTime = rdr.GetDateTime(rdr.GetOrdinal("VisitedTime"));
                    tmp.VisitTourCode = rdr["VisitedTourNo"].ToString();
                    tmp.VisitTourRouteName = rdr["VisitedRouteName"].ToString();
                    tmp.ClientUserContactTelephone = rdr["ClientContactTelephone"].ToString();
                    tmp.ClientUserContactMobile = rdr["ClientContactMobile"].ToString();
                    tmp.ClinetUserContactQQ = rdr["ClientContactQQ"].ToString();

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按访问团队的公司分页获取访问团队的历史记录信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="visitedCompanyName">被访问的批发商名称 为null时不做为查询条件</param>
        /// <param name="startVisitedTime">访问开始时间 为null时不做为查询条件</param>
        /// <param name="finishVisitedTime">访问截止时间 为null时不做为查询条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourVisitInfo> GetVisitedHistorysByCompany(int pageSize, int pageIndex, ref int recordCount, string companyId,
            string visitedCompanyName, DateTime? startVisitedTime, DateTime? finishVisitedTime)
        {
            IList<EyouSoft.Model.TourStructure.TourVisitInfo> visits = new List<EyouSoft.Model.TourStructure.TourVisitInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "view_TourVisitInfo_TourVisit";
            string primaryKey = "VisitedId";
            string orderByString = "VisitedTime DESC";
            string fields = "*";

            cmdQuery.AppendFormat(" ClientCompanyId='{0}' ", companyId);

            if (!string.IsNullOrEmpty(visitedCompanyName))
            {
                cmdQuery.AppendFormat(" AND VisitedCompanyName LIKE '%{0}%' ", visitedCompanyName);
            }

            if (startVisitedTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND VisitedTime>='{0}' ", startVisitedTime.Value);
            }

            if (finishVisitedTime.HasValue)
            {
                cmdQuery.AppendFormat(" AND VisitedTime<='{0}' ", finishVisitedTime.Value);
            }

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourVisitInfo tmp = new EyouSoft.Model.TourStructure.TourVisitInfo();

                    tmp.Id = rdr.GetString(rdr.GetOrdinal("VisitedId"));
                    tmp.TourId = rdr.GetString(rdr.GetOrdinal("VisitedTourId"));
                    tmp.VisitedCompanyId = rdr.GetString(rdr.GetOrdinal("VisitedCompanyId"));
                    tmp.VisitedCompanyName = rdr["VisitedCompanyName"].ToString();
                    tmp.ClientIP = rdr["ClientIP"].ToString();
                    tmp.ClientUserId = rdr.GetString(rdr.GetOrdinal("ClientUserID"));
                    tmp.ClientUserContactName = rdr["ClientUserContactName"].ToString();
                    tmp.ClientCompanyName = rdr["ClientCompanyName"].ToString();
                    tmp.ClientCompanyId = rdr.GetString(rdr.GetOrdinal("ClientCompanyId"));
                    tmp.VisitedTime = rdr.GetDateTime(rdr.GetOrdinal("VisitedTime"));
                    tmp.VisitTourCode = rdr["VisitedTourNo"].ToString();
                    tmp.VisitTourRouteName = rdr["VisitedRouteName"].ToString();
                    tmp.ClientUserContactTelephone = rdr["ClientContactTelephone"].ToString();
                    tmp.ClientUserContactMobile = rdr["ClientContactMobile"].ToString();
                    tmp.ClinetUserContactQQ = rdr["ClientContactQQ"].ToString();
                    tmp.VisitTourType = (EyouSoft.Model.TourStructure.VisitTourType)rdr.GetByte(rdr.GetOrdinal("VisitType"));

                    visits.Add(tmp);
                }
            }

            return visits;
        }

        /// <summary>
        /// 按公司统计访问批发商的数量
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual int GetVisitedNumberByCompany(string companyId)
        {
            int visitedNumber = 0;
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_SELECT_GetVisitedNumberByCompany);
            base.TourStore.AddInParameter(cmd, "CompanyId", DbType.AnsiStringFixedLength, companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, base.TourStore))
            {
                if (rdr.Read())
                {
                    visitedNumber = rdr.GetInt32(0);
                }
            }

            return visitedNumber;
        }

        /// <summary>
        /// 按公司分页获取访问批发商的统计信息集合
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.VisitedHistoryStatInfo> GetVisitedHistoryStats(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            IList<EyouSoft.Model.TourStructure.VisitedHistoryStatInfo> stats = new List<EyouSoft.Model.TourStructure.VisitedHistoryStatInfo>();

            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourVisitInfo";
            string primaryKey = "Id";
            string orderByString = "IssueTime DESC";
            StringBuilder fields = new StringBuilder();
            fields.Append("VisitedCompanyId");
            fields.Append(",(SELECT CompanyName FROM tbl_CompanyInfo WHERE Id=tbl_TourVisitInfo.VisitedCompanyId) AS VisitedCompanyName");
            fields.Append(",(SELECT COUNT(*) FROM tbl_TourVisitInfo AS A WHERE A.VisitedCompanyId=tbl_TourVisitInfo.VisitedCompanyId) AS VisitedTime");

            cmdQuery.AppendFormat(" CompanyId='{0}' GROUP BY VisitedCompanyId", companyId);

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields.ToString(), cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    stats.Add(new EyouSoft.Model.TourStructure.VisitedHistoryStatInfo(rdr.GetString(0), rdr[1].ToString(), rdr.GetInt32(2)));
                }
            }

            return stats;
        }

        /// <summary>
        /// 设置团队浏览数，按expression指定的数值增加或减少
        /// </summary>
        /// <param name="tourId">团队编号</param>
        /// <param name="expression">增量数值表达式</param>
        /// <returns></returns>
        public virtual bool SetClicks(string tourId, int expression)
        {
            DbCommand cmd = base.TourStore.GetSqlStringCommand(SQL_UPDATE_SetClicks);
            base.TourStore.AddInParameter(cmd, "Expression", DbType.Int32, expression);
            base.TourStore.AddInParameter(cmd, "TourId", DbType.AnsiStringFixedLength, tourId);

            return DbHelper.ExecuteSql(cmd, base.TourStore) == 1 ? true : false;
        }
        #endregion

        #region 新版资讯页面相关
        /// <summary>
        /// 根据线路区域获取相关的团队列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="AreaId">线路区域编号 =0时不做条件</param>
        /// <param name="LeaveCityId">出港城市编号 =0时不做条件</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.TourStructure.TourBasicInfo> GetToursByAreaId(int pageSize, int pageIndex, ref int recordCount, int AreaId,int LeaveCityId)
        {
            IList<EyouSoft.Model.TourStructure.TourBasicInfo> Tours = new List<EyouSoft.Model.TourStructure.TourBasicInfo>();
            StringBuilder cmdQuery = new StringBuilder();
            string tableName = "tbl_TourList";
            string primaryKey = "Id";
            string orderByString = "LeaveDate ASC,CreateTime ASC";
            string fields = "Id,RouteName,TourNo,TourDays,LeaveDate,CompanyId,CompanyName,TourState,RouteState,TourDescription,RecentLeaveCount,RemnantNumber,CreateTime,PersonalPrice,ChildPrice,RetailAdultPrice,RetailChildrenPrice,RealPrice,MarketPrice,RouteType,ParentTourID,OrderPeopleNumber,PlanPeopleCount,TourContacMQ,ShowCount,(select Cityid from tbl_TourAreaControl where tourid=tbl_TourList.Id) as CityId";

            #region 拼接查询条件
            cmdQuery.Append(" IsDelete='0' AND IsRecentLeave='1' ");
            cmdQuery.Append(" AND LeaveDate>=GETDATE() ");

            if (AreaId>0)
            {
                cmdQuery.AppendFormat(" AND AreaId={0} ", AreaId);
            }

            if (LeaveCityId > 0)
            {
                cmdQuery.AppendFormat(" AND Id in(select tourid from tbl_TourAreaControl where Cityid={0})", LeaveCityId);
            }
            #endregion

            using (IDataReader rdr = DbHelper.ExecuteReader(base.TourStore, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, cmdQuery.ToString(), orderByString))
            {
                while (rdr.Read())
                {
                    EyouSoft.Model.TourStructure.TourBasicInfo tour = new EyouSoft.Model.TourStructure.TourBasicInfo();

                    tour.ID = rdr.GetString(rdr.GetOrdinal("Id"));
                    tour.RouteName = rdr["RouteName"].ToString();
                    tour.LeaveDate = rdr.GetDateTime(rdr.GetOrdinal("LeaveDate"));
                    tour.TourDays = rdr.GetInt32(rdr.GetOrdinal("TourDays"));
                    tour.CompanyID = rdr.GetString(rdr.GetOrdinal("CompanyId"));
                    tour.CompanyName = rdr["CompanyName"].ToString();
                    tour.RemnantNumber = rdr.GetInt32(rdr.GetOrdinal("RemnantNumber"));
                    tour.TourSpreadState = (EyouSoft.Model.TourStructure.TourSpreadState)rdr.GetByte(rdr.GetOrdinal("RouteState"));
                    tour.TourSpreadDescription = rdr["TourDescription"].ToString();
                    tour.TourState = (EyouSoft.Model.TourStructure.TourState)rdr.GetByte(rdr.GetOrdinal("TourState"));
                    tour.RecentLeaveCount = rdr.GetInt32(rdr.GetOrdinal("RecentLeaveCount"));
                    tour.TravelAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("PersonalPrice"));
                    tour.TravelChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("ChildPrice"));
                    tour.RetailAdultPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailAdultPrice"));
                    tour.RetailChildrenPrice = rdr.GetDecimal(rdr.GetOrdinal("RetailChildrenPrice"));
                    tour.RoomDiffSettlementPrice = rdr.GetDecimal(rdr.GetOrdinal("RealPrice"));
                    tour.RoomDiffRetailPrice = rdr.GetDecimal(rdr.GetOrdinal("MarketPrice"));
                    tour.AreaType = (EyouSoft.Model.SystemStructure.AreaType)rdr.GetByte(rdr.GetOrdinal("RouteType"));
                    tour.ParentTourID = rdr.GetString(rdr.GetOrdinal("ParentTourID"));
                    tour.PlanPeopleCount = rdr.GetInt32(rdr.GetOrdinal("PlanPeopleCount"));
                    tour.BuySeatNumber = rdr.GetInt32(rdr.GetOrdinal("OrderPeopleNumber"));
                    tour.Clicks = rdr.GetInt32(rdr.GetOrdinal("ShowCount"));
                    tour.TourContacMQ = rdr["TourContacMQ"].ToString();
                    tour.LeaveCityId = rdr.IsDBNull(rdr.GetOrdinal("CityId")) ? 0 : rdr.GetInt32(rdr.GetOrdinal("CityId"));
                    Tours.Add(tour);
                }
            }

            return Tours;
        }
        #endregion
    }
}
