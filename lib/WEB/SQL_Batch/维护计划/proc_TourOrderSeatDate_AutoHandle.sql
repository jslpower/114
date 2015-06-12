
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-22
-- Description:	将待处理的已留位订单处理成留位过期(执行周期：2分钟)
-- =============================================
alter PROCEDURE proc_TourOrderSeatDate_AutoHandle
AS
BEGIN
	
	DECLARE @OrderId char(36)
	DECLARE @ChangeResult int 
	
	DECLARE cur_AutoHandle CURSOR FOR 
	SELECT OrderId FROM tbl_TourOrder_SeatDate WHERE datediff(dd,SaveSeatDate,getdate()) >= 0 AND datediff(mi,SaveSeatDate,getdate()) >= 0
	OPEN cur_AutoHandle
	WHILE 1 = 1
	BEGIN
		fetch next from cur_AutoHandle into @OrderId
	    if @@fetch_status<>0
		    break 
		    
		SET @ChangeResult = 0
		--将改订单的状态改为留位过期
		EXEC proc_TourOrder_SetOrderState @OrderId = @OrderId,@SaveSeatDate = NULL,@OrderState = 3,@Result = @ChangeResult output
		--改状态成功后删除此数据
		IF @ChangeResult = 9
			DELETE FROM tbl_TourOrder_SeatDate WHERE OrderId = @OrderId
	end		
	close cur_AutoHandle
    deallocate cur_AutoHandle
END
GO