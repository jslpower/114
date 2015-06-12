-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-24
-- Description: 批发商-未出发团队视图
-- =============================================
ALTER VIEW view_TourList_NotStartingTour
AS
SELECT	
	 A.ID
	,A.CompanyId
	,A.TourNo
	,A.ParentTourID
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
	,A.RouteName
	,A.AreaId
	,A.RetailAdultPrice
	,A.RetailChildrenPrice
	,A.IsRecentLeave
	,A.RecentLeaveCount
	,A.TourReleaseType
	,A.OrderPeopleNumber
	,A.RouteType
	,A.TourContact
	--实收成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=5) AS CollectAdult
	--实收儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=5) AS CollectChildren
	--预留成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=2) AS AllowanceAdult
	--预留儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=2) AS AllowanceChildren
	--未处理成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState IN(0,1)) AS UntreatedAdult
	--未处理儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState IN(0,1)) AS UntreatedChildren
	,IsRecentLeaveNoControl
FROM tbl_TourList AS A
WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0'
GO
