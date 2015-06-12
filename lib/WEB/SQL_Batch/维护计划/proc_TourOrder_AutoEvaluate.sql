
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		周文超
-- Create date: 2010-07-21
-- Description:	订单自动评价
-- =============================================
alter PROCEDURE proc_TourOrder_AutoEvaluate
AS
BEGIN
	DECLARE @OrderId char(36)   --订单ID
	--买家公司编号
	DECLARE @BuyCompanyId char(36)
	--买家公司名称
	DECLARE @BuyCompanyName NVARCHAR(250)
	--买家用户编号
	DECLARE @BuyUserId char(36)
	--买家用户联系人名称
	DECLARE @BuyUserContactName NVARCHAR(50)
	--评价类型 1.好评 2.中评 3.差评
	DECLARE @RateType TINYINT
	--服务品质
	DECLARE @ServiceQuality FLOAT
	--性价比
	DECLARE @PriceQuality FLOAT
	--旅游内容安排
	DECLARE @TravelPlan FLOAT
	--满意度
	DECLARE @AgreeLevel FLOAT
	--评价增加卖家积分值
	DECLARE @RateScore FLOAT
	--评价内容
	DECLARE @RateContent NVARCHAR(2000)
	--卖家公司编号
	DECLARE @SellCompanyId char(36)
	--线路名称
	DECLARE @RouteName NVARCHAR(250)
	--成人价
	DECLARE @SettlePeoplePrice MONEY
	--儿童价
	DECLARE @SettleChildPrice MONEY
	--团队编号
	DECLARE @TourId char(36)
	--评价到期时间
	DECLARE @ExpireTime DATETIME
	--是否是系统默认评价
	DECLARE @IsSystemRate char(1)
	
	DECLARE @ErrorCount int
	SET @ErrorCount = 0;
		
	SET @RateType=1
	SET @ServiceQuality=5
	SET @PriceQuality=5
	SET @TravelPlan=5
	SET @AgreeLevel=100
	SET @RateContent='系统默认好评!'
	SET @IsSystemRate='1'
	SET @RateScore=3

	begin tran 	

	DECLARE cur_AutoEvaluate CURSOR FOR 
	SELECT Id,ExpireTime,BuyCompanyId,BuyCompanyName,OperatorID,OperatorName,TourCompanyId,RouteName,PersonalPrice,ChildPrice,TourId FROM tbl_TourOrder WHERE RateType = 0 AND datediff(dd,ExpireTime,getdate()) > 0 and IsDelete = '0' and OrderSource = 0 and TourClassId = 2 and OrderType = '1'
	OPEN cur_AutoEvaluate
	WHILE 1 = 1
	BEGIN
		fetch next from cur_AutoEvaluate into @OrderId,@ExpireTime,@BuyCompanyId,@BuyCompanyName,@BuyUserId,@BuyUserContactName,@SellCompanyId,@RouteName,@SettlePeoplePrice,@SettleChildPrice,@TourId
	    if @@fetch_status<>0
		    break 
		
		set @ErrorCount = 0
		
		--写评论表信息
		INSERT INTO [tbl_RateOrder]([ID],[BuyCompanyId],[BuyCompanyName],[BuyUserId],[BuyUserContactName],[SellCompanyId],[RateType],[ServiceQuality],[PriceQuality],[TravelPlan],[AgreeLevel],[RateScore],[RateContent],[OrderId],[TourId],[RouteName],[SettlePeoplePrice],[SettleChildPrice],[IssueTime],[ExpireTime],[IsSystemRate]) VALUES (newid(),@BuyCompanyId,@BuyCompanyName,@BuyUserId,@BuyUserContactName,@SellCompanyId,@RateType,@ServiceQuality,@PriceQuality,@TravelPlan,@AgreeLevel,@RateScore,@RateContent,@OrderId,@TourId,@RouteName,@SettlePeoplePrice,@SettleChildPrice,getdate(),@ExpireTime,@IsSystemRate)
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		
		UPDATE tbl_RateCountTotal SET ScoreNumber=ScoreNumber+1 WHERE CompanyId=@SellCompanyId AND ScoreType=(CASE @RateType WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 4 ELSE -1 END)
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		--tbl_RateScoreTotal.ScoreType 3:服务品质总星星数
		UPDATE tbl_RateScoreTotal SET ScorePoint=ScorePoint+@ServiceQuality WHERE CompanyId=@SellCompanyId AND ScoreType=3
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		--tbl_RateScoreTotal.ScoreType 4:性价比总星星数
		UPDATE tbl_RateScoreTotal SET ScorePoint=ScorePoint+@PriceQuality WHERE CompanyId=@SellCompanyId AND ScoreType=4
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		--tbl_RateScoreTotal.ScoreType 5:旅游内容安排总星星数
		UPDATE tbl_RateScoreTotal SET ScorePoint=ScorePoint+@TravelPlan WHERE CompanyId=@SellCompanyId AND ScoreType=5
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		--tbl_RateScoreTotal.ScoreType 2:评价总分值
		UPDATE tbl_RateScoreTotal SET ScorePoint=ScorePoint+@RateScore WHERE CompanyId=@SellCompanyId AND ScoreType=2
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		--tbl_TourOrder.RateType 同步更新订单表评价类型
		UPDATE tbl_TourOrder SET RateType=@RateType WHERE Id=@OrderId
		if @@Error <> 0
			set @ErrorCount = @ErrorCount + @@Error
		
		--将处理过的公司ID写入tbl_AutoJudgeCompany表(现版本没有，暂时注释)
		--INSERT INTO tbl_AutoJudgeCompany (CompanyId,RateScore) values (@sellCompanyId,@RateScore)
		
		if @ErrorCount <> 0
			rollback tran
	end		
	close cur_AutoEvaluate
    deallocate cur_AutoEvaluate
	
	commit tran
END
GO