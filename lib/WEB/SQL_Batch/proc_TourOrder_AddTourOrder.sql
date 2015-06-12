



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-15
-- Description:	新增、修改订单后更新团队信息
-- Error：返回1表示传入参数有错误(总人数或者订单来源或者团队ID)
-- Error：返回2表示该团队的状态不能收客
-- Error：返回3、4表示订单人数超过剩余人数
-- Error：返回5表示写入订单信息有异常
-- Success：返回9表示所有操作成功
-- =============================================
alter PROCEDURE proc_TourOrder_AddTourOrder
	@OrderId char(36),  --订单ID
    @TourId char(36), --团队ID
    @RouteName nvarchar(250),--线路名称 
    @TourNo nvarchar(250), --团号
    @AreaId int,--线路区域ID
    @LeaveDate datetime,--出团时间
    @OrderType char(1),--订单类型
    @OrderState tinyint,--订单状态
    @BuyCompanyID char(36),--买家公司ID
    @BuyCompanyName nvarchar(250),--买家公司名称
    @ContactName nvarchar(20),--联系人
    @ContactTel nvarchar(50),--联系电话
    @ContactFax nvarchar(50),--传真
    @ContactMQ nvarchar(250),--MQ
    @ContactQQ nvarchar(250),--QQ
    @OperatorID char(36),--操作员ID
    @OperatorName nvarchar(50),--操作员名称
    @CompanyID char(36),--操作员所在公司ID
    @PriceStandId char(36),--价格等级ID
    @PersonalPrice money,--成人价
    @ChildPrice money,--儿童价
    @MarketPrice money,--单房差
    @AdultNumber int,--成人数
    @ChildNumber int,--儿童数
    @MarketNumber int,--单房差数
    @PeopleNumber int,--总人数
    @OtherPrice money,--其他费用
    @SaveSeatDate datetime,--留位时间
    @OperatorContent nvarchar(500),--操作留言
    @LeaveTraffic nvarchar(500),--出发交通（交通安排）
    @SpecialContent nvarchar(500),--特别要求
    @SumPrice money,--总金额
    @SeatList nvarchar(max),--座位号
    @TourCompanyName nvarchar(250),--卖家公司编号
    @TourCompanyId char(36),--卖家公司ID
    @TourClassId char(36),--团队类型ID
    @OrderSource tinyint, --订单来源编号 0: 组团下单；1：代客预定
    
    @OrderCustomerXML nvarchar(max),  --订单游客信息XML
    @Result int output -- 返回参数
AS
BEGIN
	
	IF @PeopleNumber IS NULL OR @PeopleNumber <= 0 OR @OrderSource IS NULL OR @TourId IS NULL OR len(@TourId) <= 0 OR @OrderId IS NULL OR len(@OrderId) <= 0
	begin
		SET @Result = 1;
		RETURN
	end	

	DECLARE @ErrorCount int
	SET @ErrorCount = 0
	
	DECLARE @OrderNo nvarchar(250) --订单编号
	DECLARE @TourState tinyint   --团队状态
	DECLARE @TourPlanPeopleNum int  --团队计划人数
	DECLARE @TourActualPeopleNum int  --团队实际人数
	DECLARE @TourVirtualSurplusPeopleNum int --团队虚拟剩余人数
	declare @ParentTourID char(36)  --是否模板团
	SET @TourPlanPeopleNum = 0
	set @TourActualPeopleNum = 0
	set @TourVirtualSurplusPeopleNum = 0
	
	SELECT @TourState = TourState,@ParentTourID = ParentTourID,@TourPlanPeopleNum = PlanPeopleCount,@TourActualPeopleNum = OrderPeopleNumber,@TourVirtualSurplusPeopleNum = RemnantNumber FROM tbl_TourList WHERE ID = @TourId

	if @ParentTourID is null or len(@ParentTourID) <= 0  --不能对模板团下订单
	begin
		SET @Result = 1;
		RETURN
	end

	IF @OrderSource = 0  --组团下单验证团队状态、虚拟剩余人数
	BEGIN
		IF @TourState <> 1   --团队状态不为可收客则不能下单
		BEGIN
			SET @Result = 2;
			RETURN
		end		
		IF @TourVirtualSurplusPeopleNum < @PeopleNumber
		BEGIN
			SET @Result = 3;    --团队虚拟剩余人数小于当前总人数则不能下单
			RETURN
		end	
	end	
	ELSE IF @OrderSource = 1    --专线代客预定验证实际剩余人数
	BEGIN
		IF (@TourPlanPeopleNum - @TourActualPeopleNum) < @PeopleNumber
		BEGIN
			SET @Result = 4;    --团队实际剩余人数小于当前总人数则不能下单
			RETURN
		end
	end	
	
	SET @OrderNo = dbo.fn_TourOrder_CreateOrderNo()
	
	BEGIN TRAN 
	
	--写订单信息
	INSERT INTO [tbl_TourOrder]
	([ID],[OrderNo],[TourId],[RouteName],[TourNo],[LeaveDate],[SiteId],[AreaId],[OrderType],[OrderState],[BuyCompanyID],[BuyCompanyName],[ContactName],[ContactTel],[ContactFax],[ContactMQ],[ContactQQ],[OperatorID],[OperatorName],[CompanyID],[PriceStandId],[PersonalPrice],[ChildPrice],[MarketPrice],[AdultNumber],[ChildNumber],[MarketNumber],[PeopleNumber],[OtherPrice],[SaveSeatDate],[OperatorContent],[LeaveTraffic],[SpecialContent],[SumPrice],[SeatList],[LastDate],[LastOperatorID],[IsDelete],[IssueTime],[TourCompanyName],[TourCompanyId],[TourClassId],[SuccessTime],[OrderSource])
	VALUES (@OrderId,@OrderNo,@TourId,@RouteName,@TourNo,@LeaveDate,0,@AreaId,@OrderType,@OrderState,@BuyCompanyID,@BuyCompanyName,@ContactName,@ContactTel,@ContactFax,@ContactMQ,@ContactQQ,@OperatorID,@OperatorName,@CompanyID,@PriceStandId,@PersonalPrice,@ChildPrice,@MarketPrice,@AdultNumber,@ChildNumber,@MarketNumber,@PeopleNumber,@OtherPrice,@SaveSeatDate,@OperatorContent,@LeaveTraffic,@SpecialContent,@SumPrice,@SeatList,getdate(),@OperatorID,'0',getdate(),@TourCompanyName,@TourCompanyId,@TourClassId,getdate(),@OrderSource)
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	--写订单财务信息
	INSERT INTO [tbl_TourOrderFinance] ([Id],[OrderId],[OrderNo],[TourId],[TourNo],[TourClassId],[RouteName],[LeaveDate],[BuyCompanyID],[BuyCompanyName],[PeopleNumber],[ContactName],[ContactTel],[ContactFax],[OperatorID],[OperatorName],[CompanyID],[SumPrice])	VALUES (newid(),@OrderId,@OrderNo,@TourId,@TourNo,@TourClassId,@RouteName,@LeaveDate,@BuyCompanyID,@BuyCompanyName,@PeopleNumber,@ContactName,@ContactTel,@ContactFax,@OperatorID,@OperatorName,@CompanyID,@SumPrice)
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	
	--写订单游客信息
	IF @OrderCustomerXML IS NOT NULL and len(@OrderCustomerXML) > 0
	BEGIN
		set @OrderCustomerXML = replace(replace(@OrderCustomerXML,char(13),''),char(10),'')

		DECLARE @hdoc int
		EXEC sp_xml_preparedocument @hdoc OUTPUT, @OrderCustomerXML
		INSERT INTO [tbl_TourOrderCustomer]([ID],[CompanyID],[OrderID],[VisitorName],[CradType],[CradNumber],[Sex],[VisitorType],[ContactTel],[Remark],[IssueTime],[SiteNo])
		SELECT newid(),@TourCompanyId,@OrderId,VisitorName,CradType,CradNumber,Sex,VisitorType,ContactTel,Remark,getdate(),SiteNo FROM OPENXML(@hdoc,N'/ROOT/OrderCustomer_Add')
		WITH (VisitorName nvarchar(100),CradType tinyint,CradNumber nvarchar(300),Sex char(1),VisitorType char(1),ContactTel nvarchar(100),Remark nvarchar(500),SiteNo varchar(3))
		EXEC sp_xml_removedocument @hdoc 
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end	
	
	--更新团队已占用座位号
	--更新团队实际订单人数
	--更新团队虚拟剩余人数
	--当订单人数=实际剩余人数或者=虚拟剩余人数时更新团队状态为自动客满
	IF @PeopleNumber = @TourVirtualSurplusPeopleNum OR @PeopleNumber = (@TourPlanPeopleNum - @TourActualPeopleNum)
	begin
		UPDATE tbl_TourList SET OccupySeat = (CASE WHEN OccupySeat IS NULL THEN '' ELSE OccupySeat END) + ',' + @SeatList,OrderPeopleNumber = (CASE WHEN OrderPeopleNumber IS NULL THEN 0 ELSE OrderPeopleNumber end) + @PeopleNumber,RemnantNumber = (CASE WHEN RemnantNumber IS NULL THEN 0 ELSE RemnantNumber END) - @PeopleNumber,TourState = 4 WHERE ID = @TourId 
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
		--标记最近出团的团、最近出团的数量等
		EXECUTE proc_TourList_RecentLeave @TourId=@TourId,@IsParentTour=N'0'
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end
	ELSE
	begin
		UPDATE tbl_TourList SET OccupySeat = (CASE WHEN OccupySeat IS NULL THEN '' ELSE OccupySeat END) + ',' + @SeatList,OrderPeopleNumber = (CASE WHEN OrderPeopleNumber IS NULL THEN 0 ELSE OrderPeopleNumber end) + @PeopleNumber,RemnantNumber = (CASE WHEN RemnantNumber IS NULL THEN 0 ELSE RemnantNumber END) - @PeopleNumber WHERE ID = @TourId 
		--验证错误
		SET @ErrorCount = @ErrorCount + @@ERROR
	end
	
	IF @ErrorCount > 0 
	BEGIN
		SET @Result = 5;
		ROLLBACK TRAN 
	end	
	ELSE
	BEGIN
		SET @Result = 9;
		COMMIT TRAN 
	end
END
GO
