
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-26
-- Description: 批发商有效团队视图
-- =============================================
ALTER VIEW view_TourList_ValidTour
AS
SELECT	
	 A.ID
	,A.CompanyId
	,A.TourNo
	,A.ParentTourID
	,A.AreaId
	,A.TourState
	,A.LeaveDate
	,A.RouteState
	,A.TourDescription
	,A.TourDays
	,A.PersonalPrice
	,A.ChildPrice 
	,A.RealPrice
	,A.MarketPrice
	,A.PlanPeopleCount
	,A.RemnantNumber
	,A.ShowCount
	,A.CompanyName
	,A.UnitCompanyId
	,A.CreateTime
	,A.IsRecentLeave
	,A.RecentLeaveCount
	,A.TourContacMQ
	,A.RouteName
	,A.RouteType
	,A.RetailAdultPrice
	,A.RetailChildrenPrice
	,A.OrderPeopleNumber
FROM tbl_TourList AS A
WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsChecked='1'
GO
