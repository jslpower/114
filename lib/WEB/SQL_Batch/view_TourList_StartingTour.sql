-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-24
-- Description: 批发商-已出发团队视图
-- =============================================
ALTER VIEW view_TourList_StartingTour
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
	,A.ComeBackDate
	--实收成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState IN(0,1,2,5)) AS CollectAdult
	--实收儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState IN(0,1,2,5)) AS CollectChildren
	--留位过期成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=3) AS OverdueAdult
	--留位过期儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=3) AS OverdueChildren
	--不受理成人数
	,(SELECT SUM(AdultNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=4 AND IsDelete='0') AS DismissAdult
	--不受理儿童数
	,(SELECT SUM(ChildNumber) FROM tbl_TourOrder WHERE TourId=A.Id AND  OrderState=4 AND IsDelete='0') AS DismissChildren
FROM tbl_TourList AS A
WHERE A.LeaveDate<GETDATE() AND IsDelete='0' AND A.ParentTourID>''
GO

