

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-15
-- Description:	修改订单
-- Error：返回1表示传入参数错误（订单ID、订单来源编号、总人数、座位号）
-- Error：返回2表示此订单目前的状态不允许修改
-- Error：返回3该订单所在的团队的状态不允许增加订单人数
-- Error：返回4订单增加的人数大于团队的剩余人数不能修改
-- Error：返回5表示团队人数不足，不能继续留位或者完成交易
-- Error：返回6订单增加的人数大于团队的实际剩余人数
-- Error：返回7表示修改订单的过程中出现异常
-- Success：返回9表示所有操作已成功完成
-- =============================================
alter PROCEDURE proc_TourOrder_UpdateTourOrder
	@OrderId char(36),  --订单id
	@OrderType char(1),  --订单类型(1位数) 0:预定 1:购买
	@OrderState tinyint,  --订单状态
	@ContactName nvarchar(20),--联系人
    @ContactTel nvarchar(50),--联系电话
    @ContactFax nvarchar(50),--传真
    @ContactMQ nvarchar(250),--MQ
    @ContactQQ nvarchar(250),--QQ
	@PriceStandId char(36),  --价格等级编号
	@PersonalPrice money, --成人价
	@ChildPrice money, --儿童价
	@MarketPrice money, --单房差
	@AdultNumber int, --成人数
	@ChildNumber int, --儿童数
	@MarketNumber int, --单房差数
	@PeopleNumber int, --总人数
	@OtherPrice money, --其它费用
	@SaveSeatDate datetime, --留位时间
	@OperatorContent nvarchar(500), --操作留言
	@LeaveTraffic nvarchar(500),--出发交通（交通安排）
	@SpecialContent nvarchar(500), --特别要求
	@SumPrice money, --总金额
	@SeatList nvarchar(max), --座位号多个座位，号分隔
	@LastOperatorID char(36),--最后操作人ID
	@OrderSource tinyint, --订单来源编号 0: 组团下单；1：代客预定
	
	@OrderCustomerXML nvarchar(max),  --订单游客信息XML
    @Result int output -- 返回参数
AS
BEGIN
	IF @OrderId IS NULL OR len(@OrderId) <= 0 OR @OrderSource IS NULL OR @PeopleNumber IS NULL OR @PeopleNumber <= 0 OR (@OrderState = 2 and @SaveSeatDate IS NULL)
	begin
		SET @Result = 1;
		RETURN
	end	

	DECLARE @ErrorCount int
	SET @ErrorCount = 0
	
	DECLARE @TourCompanyId char(36)
	DECLARE @TourId char(36)
	DECLARE @OldPeopleNumber int  --要修改的订单修改前的总人数
	DECLARE @OldOrderState tinyint   --原来的订单状态
	DECLARE @TourState tinyint   --团队状态
	DECLARE @TourPlanPeopleNum int  --团队计划人数
	DECLARE @TourActualPeopleNum int  --团队实际人数
	DECLARE @TourVirtualSurplusPeopleNum int --团队虚拟剩余人数
	DECLARE @NewPeopleNumber int   --变动人数，保存修改前后的人数变动，人数不变动为0
	DECLARE @IsActualEqual char(1)    --变动人数是否和实际剩余人数相等
	DECLARE @IsVirtualEqual char(1)   --变动人数是否和虚拟剩余人数相等
	DECLARE @Sql nvarchar(max)       --保存更新团队表的Sql语句
	DECLARE @AllSeatList varchar(max)  --保存所有的座位号
	SET @NewPeopleNumber = 0;
	SET @IsActualEqual = '0';
	SET @IsVirtualEqual = '0';
	SET @Sql = '';
	
	SELECT @TourId = TourId,@OldOrderState = OrderState, @OldPeopleNumber = (case when PeopleNumber is null then 0 else PeopleNumber end), @TourCompanyId = TourCompanyId FROM tbl_TourOrder WHERE ID = @OrderId
	SELECT @TourState = TourState, @TourPlanPeopleNum = PlanPeopleCount,@TourActualPeopleNum = OrderPeopleNumber,@TourVirtualSurplusPeopleNum = RemnantNumber FROM tbl_TourList WHERE ID = @TourId	
	
	IF @OrderSource = 0   --组团修改订单（只能修改未处理和处理中的订单）
	BEGIN
		--变动人数 = 现在的人数 - 原来的人数
		SET @NewPeopleNumber = @PeopleNumber - @OldPeopleNumber 
		IF @OldOrderState <> 0 AND @OldOrderState <> 1
		BEGIN
			SET @Result = 2;    --订单状态不为未处理或者处理中时不能修改
			RETURN
		end		
		IF @PeopleNumber > @OldPeopleNumber   --订单人数小改大
		BEGIN
			IF @TourState <> 1  --团队状态不为可收客则不能增加订单人数
			BEGIN
				SET @Result = 3;    --该订单所在的团队的状态不允许增加订单人数
				RETURN
			end			
			IF @TourVirtualSurplusPeopleNum < @NewPeopleNumber
			BEGIN
				SET @Result = 4;    --订单增加的人数大于团队的虚拟剩余人数不能修改
				RETURN
			end			
			ELSE IF @TourVirtualSurplusPeopleNum = @NewPeopleNumber 
				SET @IsVirtualEqual = '1'; --变动人数和虚拟剩余人数相等
		end		
	end	
	ELSE IF @OrderSource = 1  --专线修改订单
	BEGIN
		IF (@OldOrderState = 3 OR @OldOrderState = 4) AND (@OrderState <> 3 and @OrderState <> 4)
		BEGIN
			--变动人数 = 现在的人数
		    SET @NewPeopleNumber = @PeopleNumber
			--原来订单状态为留位过期或者不受理时，修改成其他状态要验证实际剩余人数
			IF (@TourPlanPeopleNum - @TourActualPeopleNum) < @PeopleNumber
			BEGIN
				SET @Result = 5;    --团队人数不足，不能继续留位或者完成交易
				RETURN
			end			
			ELSE IF (@TourPlanPeopleNum - @TourActualPeopleNum) = @PeopleNumber
				SET @IsActualEqual = '1'  --变动人数和实际剩余人数相等
		end		
		ELSE IF (@OldOrderState <> 3 and @OldOrderState <> 4) AND (@OrderState = 3 OR @OrderState = 4)
		BEGIN
			--由占订单人数的订单（除留位过期和不受理外的订单）改为不占订单人数的订单（留位过期和不受理外的订单）
			--变动人数 = 0 - 现在的人数
		    SET @NewPeopleNumber = 0 - @PeopleNumber
		end
		ELSE
		BEGIN
			--变动人数 = 现在的人数 - 原来的人数
			SET @NewPeopleNumber = @PeopleNumber - @OldPeopleNumber
			IF @PeopleNumber > @OldPeopleNumber   --人数小 改  大
			BEGIN
				IF (@TourPlanPeopleNum - @TourActualPeopleNum) < @NewPeopleNumber
				BEGIN
					SET @Result = 6;    --订单增加的人数大于团队的实际剩余人数不能修改
					RETURN
				end			
				ELSE IF (@TourPlanPeopleNum - @TourActualPeopleNum) = @NewPeopleNumber
					SET @IsActualEqual = '1'  --变动人数和实际剩余人数相等
			end		
		end
	end
	
	BEGIN TRAN 
	
	--修改订单
	update tbl_TourOrder 
	set [OrderType] = @OrderType,[OrderState] = @OrderState,[ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactFax] = @ContactFax,[ContactMQ] = @ContactMQ,[ContactQQ] = @ContactQQ,[PriceStandId] = @PriceStandId,[PersonalPrice] = @PersonalPrice,[ChildPrice] = @ChildPrice,[MarketPrice] = @MarketPrice,[AdultNumber] = @AdultNumber,[ChildNumber] = @ChildNumber,[MarketNumber] = @MarketNumber,[PeopleNumber] = @PeopleNumber,[OtherPrice] = @OtherPrice,[SaveSeatDate] = @SaveSeatDate,[OperatorContent] = @OperatorContent,[LeaveTraffic] = @LeaveTraffic,[SpecialContent] = @SpecialContent,[SumPrice] = @SumPrice,[SeatList] = @SeatList,[LastDate] = getdate(),[LastOperatorID] = @LastOperatorID where [ID] = @OrderId
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	
	IF @OrderState = 2 --已留位
	BEGIN
		DECLARE @TimeNow datetime
		SET @TimeNow = getdate()
		--留位过期时间为今天，且大于现在，则写入处理留位过期的临时表
		IF @SaveSeatDate is not null and datediff(dd,@SaveSeatDate,@TimeNow) = 0 AND @SaveSeatDate > @TimeNow
		BEGIN
			--删除已存在的，加入现在的
			DELETE FROM tbl_TourOrder_SeatDate WHERE OrderId = @OrderId  
			INSERT INTO tbl_TourOrder_SeatDate (OrderId,SaveSeatDate) VALUES (@OrderId,@SaveSeatDate) 
		end
	end
	
	IF @OrderState = 5 --已成交
	BEGIN
		--更新订单的交易成功时间
		update tbl_TourOrder SET SuccessTime = getdate(),ExpireTime = dateadd(dd,15,getdate()),OrderType = '1' WHERE ID = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		--修改订单财务
		UPDATE [tbl_TourOrderFinance] SET [PeopleNumber] = @PeopleNumber,[ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactFax] = @ContactFax,[SumPrice] = @SumPrice,[SuccessTime] = getdate() WHERE [OrderId] = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end	
	ELSE
	BEGIN
		UPDATE [tbl_TourOrderFinance] SET [PeopleNumber] = @PeopleNumber,[ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactFax] = @ContactFax,[SumPrice] = @SumPrice WHERE [OrderId] = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end
	
	--修改订单游客信息
	IF @OrderCustomerXML IS NOT NULL and len(@OrderCustomerXML) > 1
	BEGIN
		set @OrderCustomerXML = replace(replace(@OrderCustomerXML,char(13),''),char(10),'')		

		--删除原有的游客信息
		DELETE FROM tbl_TourOrderCustomer WHERE OrderID = @OrderId
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		
		--写新的游客信息
		DECLARE @hdoc int
		EXEC sp_xml_preparedocument @hdoc OUTPUT, @OrderCustomerXML
		INSERT INTO [tbl_TourOrderCustomer]([ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo])
		SELECT newid(),@TourCompanyId,@OrderId,VisitorName,CradType,CradNumber,Sex,VisitorType,ContactTel,Remark,getdate(),SiteNo FROM OPENXML(@hdoc,N'/ROOT/OrderCustomer_Add')
		WITH (VisitorName nvarchar(100),CradType tinyint,CradNumber nvarchar(300),Sex char(1),VisitorType char(1),ContactTel nvarchar(100),Remark nvarchar(500),SiteNo varchar(3))
		EXEC sp_xml_removedocument @hdoc 
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end	
	
	--计算座位号
	SET @AllSeatList = ''
	set @AllSeatList = (SELECT cast(SeatList as nvarchar(max)) + ',' FROM tbl_TourOrder where TourId = @TourId FOR xml path(''))
	IF @SeatList IS not NULL OR len(@SeatList) > 0
		SET @AllSeatList = isnull(@AllSeatList,'') + @SeatList
	ELSE IF len(@AllSeatList) > 1
		SET @AllSeatList = substring(isnull(@AllSeatList,''),1,len(@AllSeatList) - 1)
	
	--更新团队已占用座位号
	--更新团队实际订单人数
	--更新团队虚拟剩余人数
	IF @NewPeopleNumber IS NOT NULL AND @NewPeopleNumber <> 0
	BEGIN
		SET @Sql = ' UPDATE tbl_TourList SET OccupySeat = ''' + @AllSeatList + ''',OrderPeopleNumber = (CASE WHEN OrderPeopleNumber IS NULL THEN 0 ELSE OrderPeopleNumber  end) + ' + cast(@NewPeopleNumber as nvarchar(20)) + ',RemnantNumber = (CASE WHEN RemnantNumber IS NULL THEN 0 ELSE RemnantNumber END) - ' + cast(@NewPeopleNumber as nvarchar(20))
	end	
	
	IF @NewPeopleNumber > 0  --小 改 大
	BEGIN
		IF @IsActualEqual = '1' OR @IsVirtualEqual = '1'
		BEGIN
			--团队状态为收客或者自动停收时，当新加的人数等于实际或者虚拟的剩余人数时
			--更新团队状态为自动客满
			IF @TourState = 1 OR @TourState = 3 AND @Sql IS NOT NULL and len(@Sql) > 1
				SET @Sql = @Sql + ',' + ' TourState = 4 '
		end
	end	
	ELSE IF @NewPeopleNumber < 0   --大 改 小
	BEGIN
		--订单人数由大改小时，原来团队的状态为自动客满的改为收客
		IF @TourState = 4 AND @Sql IS NOT NULL and len(@Sql) > 1
			SET @Sql = @Sql + ',' + ' TourState = 1 '
	end	

	IF @Sql IS NOT NULL and len(@Sql) > 1
		SET @Sql = @Sql + ' where ID = ''' + @TourId + ''''
	
	IF @Sql IS NOT NULL and len(@Sql) > 1
	BEGIN
		EXEC (@Sql)  --执行更新团队信息的Sql语句
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		--标记最近出团的团、最近出团的数量等
		EXEC proc_TourList_RecentLeave @TourId=@TourId,@IsParentTour=N'0'
	end
	
	IF @ErrorCount > 0 
	BEGIN
		SET @Result = 7;
		ROLLBACK TRAN 
	end	
	ELSE
	BEGIN
		SET @Result = 9;
		COMMIT TRAN 
	end
END
GO
