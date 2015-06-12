
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-22
-- Description: 将今天留位过期的订单写入待处理的临时表
-- =============================================
alter PROCEDURE proc_TourOrderSeatDate_GetData
AS
BEGIN
	
	--清空临时表
	DELETE FROM tbl_TourOrder_SeatDate
	--写入数据
	INSERT INTO tbl_TourOrder_SeatDate SELECT Id,SaveSeatDate FROM tbl_TourOrder WHERE IsDelete = '0' and OrderType = '0' AND datediff(dd,SaveSeatDate,getdate()) >= 0 AND OrderState = 2
	
END
GO