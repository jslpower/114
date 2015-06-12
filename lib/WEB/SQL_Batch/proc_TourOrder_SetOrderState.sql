
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-19
-- Description:	设置订单状态
-- Error：返回1表示传入参数错误
-- Error：返回2表示修改订单状态时发生异常
-- Success：返回9表示所有操作成功完成
-- =============================================
alter PROCEDURE proc_TourOrder_SetOrderState
	@OrderId char(36),   --订单ID
	@SaveSeatDate datetime,    --留位过期时间
	@OrderState tinyint,  --订单状态
	
	@Result int output   --返回值
AS
BEGIN
	IF @OrderId IS NULL OR len(@OrderId) <= 0 OR @OrderState IS NULL OR @OrderState < 0 OR (@OrderState = 2 and @SaveSeatDate is null)
	BEGIN
		SET @Result = 1
		return
	end	
	
	DECLARE @ErrorCount int
	SET @ErrorCount = 0
	DECLARE @OldOrderState tinyint  --原来的订单状态
	DECLARE @SeatList nvarchar(500)   --订单的座位号
	DECLARE @AllSeatList varchar(500)  --保存所有的座位号
	DECLARE @PeopleNumber int      --订单人数
	DECLARE @TourId   char(36)    --团队ID
	DECLARE @TourState tinyint   --团队状态
	DECLARE @TourPlanPeopleNum int  --团队计划人数
	DECLARE @TourActualPeopleNum int  --团队实际人数
	
	DECLARE @UpdateSql nvarchar(max)  --更新团队Sql语句
	SET @UpdateSql = ''
	
	SELECT @TourId = TourId,@OldOrderState = OrderState,@PeopleNumber = PeopleNumber,@SeatList = SeatList FROM tbl_TourOrder WHERE ID = @OrderId
	SELECT @TourState = TourState, @TourPlanPeopleNum = PlanPeopleCount,@TourActualPeopleNum = OrderPeopleNumber FROM tbl_TourList WHERE ID = @TourId	
	
	IF (@OldOrderState = 3 OR @OldOrderState = 4) AND (@OrderState <> 3 AND @OrderState <> 4)
	BEGIN
		--留位过期或者不受理改为其他状态
		IF @PeopleNumber > (@TourPlanPeopleNum - @TourActualPeopleNum)
		BEGIN
			SET @Result = 2;   --团队剩余人数不足，不能继续留位或者成交
			RETURN 
		end
	end	
	
	BEGIN TRAN 
	
	update tbl_TourOrder SET OrderState = @OrderState WHERE ID = @OrderId
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	
	IF @OrderState = 5  --已成交  
	BEGIN
		DECLARE @SuccessTime datetime
		SET @SuccessTime = getdate()
	
		--更新订单的交易成功时间
		update tbl_TourOrder SET SuccessTime = @SuccessTime,ExpireTime = dateadd(dd,15,@SuccessTime),OrderType = '1' WHERE ID = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		
		--更新订单财务信息的交易成功时间
		UPDATE [tbl_TourOrderFinance] SET [SuccessTime] = @SuccessTime WHERE [OrderId] = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end	
	IF @OrderState = 2  --已留位
	BEGIN
		UPDATE tbl_TourOrder SET SaveSeatDate = @SaveSeatDate WHERE ID = @OrderId
		 --留位过期时间为今天，且大于现在，则写入处理留位过期的临时表
		DECLARE @TimeNow datetime
		SET @TimeNow = getdate()
		IF @SaveSeatDate is not null and datediff(dd,@SaveSeatDate,@TimeNow) = 0 AND @SaveSeatDate > @TimeNow
		BEGIN
			--删除已存在的，加入现在的
			DELETE FROM tbl_TourOrder_SeatDate WHERE OrderId = @OrderId  
			INSERT INTO tbl_TourOrder_SeatDate (OrderId,SaveSeatDate) VALUES (@OrderId,@SaveSeatDate) 
		end
	end
	
	--计算座位号
	SET @AllSeatList = ''
	set @AllSeatList = (SELECT cast(SeatList as nvarchar(max)) + ',' FROM tbl_TourOrder where TourId = @TourId FOR xml path(''))
	IF @SeatList IS not NULL OR len(@SeatList) > 0
		SET @AllSeatList = isnull(@AllSeatList,'') + @SeatList
	ELSE IF len(@AllSeatList) > 1
		SET @AllSeatList = substring(isnull(@AllSeatList,''),1,len(@AllSeatList) - 1)
	
	--其他状态改为留位过期或者不受理
	IF (@OldOrderState <> 3 AND @OldOrderState <> 4) AND (@OrderState = 3 OR @OrderState = 4)
	BEGIN
		--团队状态为自动客满的改为已收客
		--更新团队已占用座位号
		--更新团队实际订单人数
		--更新团队虚拟剩余人数
		SET @UpdateSql = ' UPDATE tbl_TourList SET OccupySeat = ''' + @AllSeatList + ''',OrderPeopleNumber = (CASE WHEN OrderPeopleNumber IS NULL THEN 0 ELSE OrderPeopleNumber end) - ' + cast(@PeopleNumber as nvarchar(20)) + ',RemnantNumber = (CASE WHEN RemnantNumber IS NULL THEN 0 ELSE RemnantNumber END) + ' + cast(@PeopleNumber AS nvarchar(20))
		IF @TourState = 4 AND @UpdateSql IS NOT NULL and len(@UpdateSql) > 1
		BEGIN
			SET @UpdateSql = @UpdateSql + ',' + ' TourState = 1 '
		end		
		IF @UpdateSql IS NOT NULL and len(@UpdateSql) > 1
			SET @UpdateSql = @UpdateSql + ' where ID = ''' + @TourId + ''''
	end	
	ELSE IF (@OldOrderState = 3 OR @OldOrderState = 4) AND (@OrderState <> 3 AND @OrderState <> 4)
	BEGIN
		--留位过期或者不受理改为其他状态
		--团队状态为自动客满的改为已收客
		--更新团队已占用座位号
		--更新团队实际订单人数
		--更新团队虚拟剩余人数
		SET @UpdateSql = ' UPDATE tbl_TourList SET OccupySeat = ''' + @AllSeatList + ''',OrderPeopleNumber = (CASE WHEN OrderPeopleNumber IS NULL THEN 0 ELSE OrderPeopleNumber end) + ' + cast(@PeopleNumber as nvarchar(20)) + ',RemnantNumber = (CASE WHEN RemnantNumber IS NULL THEN 0 ELSE RemnantNumber END) - ' + cast(@PeopleNumber as nvarchar(20))
		--团队状态为收客或者自动停收时，当新加的人数等于实际剩余人数时
		--更新团队状态为自动客满
		IF (@TourState = 1 OR @TourState = 3) AND (@PeopleNumber = @TourPlanPeopleNum - @TourActualPeopleNum) AND @UpdateSql IS NOT NULL and len(@UpdateSql) > 1
		begin
			SET @UpdateSql = @UpdateSql + ',' + ' TourState = 4 '
		end
		IF @UpdateSql IS NOT NULL and len(@UpdateSql) > 1
			SET @UpdateSql = @UpdateSql + ' where ID = ''' + @TourId + ''''
	end
	
	IF @UpdateSql IS NOT NULL and len(@UpdateSql) > 1
	BEGIN
		EXEC (@UpdateSql)
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		--标记最近出团的团、最近出团的数量等
		EXEC proc_TourList_RecentLeave @TourId=@TourId,@IsParentTour=N'0'
	end
	
	IF @ErrorCount > 0 
	BEGIN
		SET @Result = 2;
		ROLLBACK TRAN 
	end	
	ELSE
	BEGIN
		SET @Result = 9;
		COMMIT TRAN 
	end
END
GO
