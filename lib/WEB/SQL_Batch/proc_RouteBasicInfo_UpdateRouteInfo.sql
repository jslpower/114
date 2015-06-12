-- =============================================
-- Author:		鲁功源
-- Create date: 2010-05-26
-- Description:	更新线路信息
-- =============================================
CREATE PROCEDURE [dbo].[proc_RouteBasicInfo_UpdateRouteInfo]	
	@RouteId CHAR(36),--线路编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@UserId CHAR(36),--用户编号
	@ContactName NVARCHAR(20),--线路负责人
	@ContactTelephone NVARCHAR(20),--线路负责人电话
	@ContactUserName NVARCHAR(20),--线路负责人用户名
	@ContactMQId nvarchar(250),--线路负责人MQ编号
	@RouteName NVARCHAR(250),--线路名称
	@RouteDays INT,--线路天数
	@IssueTime DATETIME=NULL,--线路发布时间
	@AreaId INT=0,--线路区域
	@RouteType TINYINT,--线路区域类型
	-----原始版本出港城市多选------
	--@LeaveCity NVARCHAR(2000),--出港城市 XML:<ROOT><LeaveCityInfo CityId="" /></ROOT>
	----最新出港城市单选--------
	@LeaveCity int,
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT"></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT"></ROOT>
	@ReleaseType CHAR(1),--线路发布类型	
    ---------------去除客户等级类型----------------
	--@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级 0:同行 1:门市 2:单房差 3:其它" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PalnInfo PlanId="" PlanInterval="" Vehicle="" TrafficNumber="" House="" Dinner="" PlanContent="" PlanDay=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@ResideContent NVARCHAR(500)=NULL,--住宿(包含项目)
	@DinnerContent NVARCHAR(500)=NULL,--用餐(包含项目)
	@SightContent NVARCHAR(500)=NULL,--景点(包含项目)
	@CarContent NVARCHAR(500)=NULL,--用车(包含项目)
	@GuideContent NVARCHAR(500)=NULL,--导游(包含项目)
	@TrafficContent NVARCHAR(500)=NULL,--往返交通(包含项目)
	@IncludeOtherContent NVARCHAR(500)=NULL,--包含项目其它内容(包含项目)
	@NotContainService NVARCHAR(500)=NULL,--不包含项目
	/*@ExpenseItem NVARCHAR(500)=NULL,--自费项目
	@ChildrenInfo NVARCHAR(500)=NULL,--儿童安排
	@ShoppingInfo NVARCHAR(500)=NULL,--购物安排
	@GiftInfo NVARCHAR(500)=NULL,--赠送项目
	@NoticeProceeding NVARCHAR(500)=NULL,--注意事项*/
	@SpeciallyNotice NVARCHAR(500)=NULL,--温馨提醒
	@Result INT OUTPUT--操作结果 0:失败 1:成功	
AS
BEGIN
	SET @Result=0

	BEGIN TRANSACTION UpdateRouteInfoByStandard
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新线路基本信息
	UPDATE [tbl_RouteBasicInfo]
	SET [OperatorID] = @UserId
		  ,[ContactName] = @ContactName
		  ,[ContactTel] = @ContactTelephone
		  ,[ContactUserName] = @ContactUserName
		  ,[ContactMQID] = @ContactMQId
		  ,[AreaId]=@AreaId
		  ,[RouteName] = @RouteName
		  ,[RouteDays] = @RouteDays
		  ,[LeaveCityId]=@LeaveCity
		  ,[IssueTime] = GETDATE()
	WHERE [Id]=@RouteId
	SET @errorCount=@errorCount+@@ERROR

	DECLARE @hdoc INT	

	IF(@ReleaseType='0')
	BEGIN
		--更新线路详细信息(服务标准相关内容)
		UPDATE [tbl_RouteServiceStandard]
		SET [ResideContent] = @ResideContent
		  ,[DinnerContent] = @DinnerContent
		  ,[SightContent] = @SightContent
		  ,[CarContent] = @CarContent
		  ,[GuideContent] = @GuideContent
		  ,[TrafficContent] = @TrafficContent
		  ,[IncludeOtherContent] = @IncludeOtherContent
		  ,[NotContainService] = @NotContainService
		  /*,[ExpenseItem] = @ExpenseItem
		  ,[ChildrenInfo] = @ChildrenInfo
		  ,[ShoppingInfo] = @ShoppingInfo
		  ,[GiftInfo] = @GiftInfo
		  ,[NoticeProceeding] = @NoticeProceeding*/
		  ,[SpeciallyNotice] = @SpeciallyNotice
		WHERE [RouteBasicID]=@RouteId
		SET @errorCount=@errorCount+@@ERROR

		--更新线路行程信息	 操作方式:先删除原有的行程信息再新增
		DELETE FROM [tbl_RouteStandardPlan] WHERE [RouteBasicID]=@RouteId

		EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @StandardPlan
		INSERT INTO [tbl_RouteStandardPlan]([RouteBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])
		SELECT @RouteId,[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay]
		FROM OPENXML (@hdoc,'/ROOT/PalnInfo')
		WITH([PlanInterval] NVARCHAR(50),[Vehicle] NVARCHAR(50),[TrafficNumber] NVARCHAR(50),[House] NVARCHAR(50),[Dinner] NVARCHAR(50),[PlanContent] NVARCHAR(50),[PlanDay] INT,[PlanId] CHAR(36))

		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END
	ELSE
	BEGIN
		UPDATE [tbl_RouteFastPlan] SET [RoutePlan]=@QuickPlan
		WHERE [RouteBasicID]=@RouteId
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入价格明细 操作方式:先删除原有的价格信息再新增
	DELETE FROM [tbl_RouteBasicPriceDetail] WHERE [RouteBasicID]=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @PriceDetails
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT)
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	/*
	--出港城市 操作方式:先删除再新增
	DELETE FROM [tbl_RouteAreaControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @LeaveCity
	INSERT INTO [tbl_RouteAreaControl] (RouteId,CityId,AreaId)
	SELECT @RouteId,A.CityId,@AreaId FROM OPENXML(@hdoc,'/ROOT/LeaveCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	
	*/

	--销售城市 操作方式:先删除再新增
	DELETE FROM [tbl_RouteCityControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @SaleCity
	INSERT INTO [tbl_RouteCityControl] (RouteId,CityId)
	SELECT @RouteId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--线路主题 操作方式:先删除再新增
	DELETE FROM [tbl_RouteThemeControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @RouteTheme
	INSERT INTO [tbl_RouteThemeControl] (RouteId,ThemeId)
	SELECT @RouteId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--写入数据到tbl_TourContactInfo
	IF(NOT EXISTS(SELECT 1 FROM tbl_TourContactInfo WHERE CompanyId=@CompanyId AND ContactName=@ContactName))
	BEGIN
		DECLARE @username NVARCHAR(50)
		SELECT @username=UserName FROM tbl_CompanyUser WHERE Id=@UserId
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactMQID],[UserName])
		VALUES (@CompanyId,@ContactName,@ContactTelephone,@ContactMQId,@username)
		SET @errorCount=@errorCount+@@ERROR
	END

	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateRouteInfoByStandard
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateRouteInfoByStandard
		SET @Result=1
	END
	
	RETURN @Result
END
GO

-- =============================================
-- Author:		鲁功源
-- Create date: 2010-05-26
-- Description:	更新线路信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_RouteBasicInfo_UpdateRouteInfo]	
	@RouteId CHAR(36),--线路编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@UserId CHAR(36),--用户编号
	@ContactName NVARCHAR(20),--线路负责人
	@ContactTelephone NVARCHAR(20),--线路负责人电话
	@ContactUserName NVARCHAR(20),--线路负责人用户名
	@ContactMQId nvarchar(250),--线路负责人MQ编号
	@RouteName NVARCHAR(250),--线路名称
	@RouteDays INT,--线路天数
	@IssueTime DATETIME=NULL,--线路发布时间
	@AreaId INT=0,--线路区域
	@RouteType TINYINT,--线路区域类型
	-----原始版本出港城市多选------
	--@LeaveCity NVARCHAR(2000),--出港城市 XML:<ROOT><LeaveCityInfo CityId="" /></ROOT>
	----最新出港城市单选--------
	@LeaveCity int,
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT"></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT"></ROOT>
	@ReleaseType CHAR(1),--线路发布类型	
    ---------------去除客户等级类型----------------
	--@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级 0:同行 1:门市 2:单房差 3:其它" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PalnInfo PlanId="" PlanInterval="" Vehicle="" TrafficNumber="" House="" Dinner="" PlanContent="" PlanDay=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目
	/*@ExpenseItem NVARCHAR(500)=NULL,--自费项目
	@ChildrenInfo NVARCHAR(500)=NULL,--儿童安排
	@ShoppingInfo NVARCHAR(500)=NULL,--购物安排
	@GiftInfo NVARCHAR(500)=NULL,--赠送项目
	@NoticeProceeding NVARCHAR(500)=NULL,--注意事项*/
	@SpeciallyNotice NVARCHAR(500)=NULL,--温馨提醒
	@Result INT OUTPUT--操作结果 0:失败 1:成功	
AS
BEGIN
	SET @Result=0

	BEGIN TRANSACTION UpdateRouteInfoByStandard
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新线路基本信息
	UPDATE [tbl_RouteBasicInfo]
	SET [OperatorID] = @UserId
		  ,[ContactName] = @ContactName
		  ,[ContactTel] = @ContactTelephone
		  ,[ContactUserName] = @ContactUserName
		  ,[ContactMQID] = @ContactMQId
		  ,[AreaId]=@AreaId
		  ,[RouteName] = @RouteName
		  ,[RouteDays] = @RouteDays
		  ,[LeaveCityId]=@LeaveCity
		  ,[IssueTime] = GETDATE()
	WHERE [Id]=@RouteId
	SET @errorCount=@errorCount+@@ERROR

	DECLARE @hdoc INT	

	IF(@ReleaseType='0')
	BEGIN
		--更新线路详细信息(服务标准相关内容)
		UPDATE [tbl_RouteServiceStandard]
		SET [ResideContent] = @ResideContent
		  ,[DinnerContent] = @DinnerContent
		  ,[SightContent] = @SightContent
		  ,[CarContent] = @CarContent
		  ,[GuideContent] = @GuideContent
		  ,[TrafficContent] = @TrafficContent
		  ,[IncludeOtherContent] = @IncludeOtherContent
		  ,[NotContainService] = @NotContainService
		  /*,[ExpenseItem] = @ExpenseItem
		  ,[ChildrenInfo] = @ChildrenInfo
		  ,[ShoppingInfo] = @ShoppingInfo
		  ,[GiftInfo] = @GiftInfo
		  ,[NoticeProceeding] = @NoticeProceeding*/
		  ,[SpeciallyNotice] = @SpeciallyNotice
		WHERE [RouteBasicID]=@RouteId
		SET @errorCount=@errorCount+@@ERROR

		--更新线路行程信息	 操作方式:先删除原有的行程信息再新增
		DELETE FROM [tbl_RouteStandardPlan] WHERE [RouteBasicID]=@RouteId

		EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @StandardPlan
		INSERT INTO [tbl_RouteStandardPlan]([RouteBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])
		SELECT @RouteId,[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay]
		FROM OPENXML (@hdoc,'/ROOT/PalnInfo')
		WITH([PlanInterval] NVARCHAR(50),[Vehicle] NVARCHAR(50),[TrafficNumber] NVARCHAR(50),[House] NVARCHAR(50),[Dinner] NVARCHAR(50),[PlanContent] NVARCHAR(50),[PlanDay] INT,[PlanId] CHAR(36))

		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END
	ELSE
	BEGIN
		UPDATE [tbl_RouteFastPlan] SET [RoutePlan]=@QuickPlan
		WHERE [RouteBasicID]=@RouteId
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入价格明细 操作方式:先删除原有的价格信息再新增
	DELETE FROM [tbl_RouteBasicPriceDetail] WHERE [RouteBasicID]=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @PriceDetails
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT)
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	/*
	--出港城市 操作方式:先删除再新增
	DELETE FROM [tbl_RouteAreaControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @LeaveCity
	INSERT INTO [tbl_RouteAreaControl] (RouteId,CityId,AreaId)
	SELECT @RouteId,A.CityId,@AreaId FROM OPENXML(@hdoc,'/ROOT/LeaveCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	
	*/

	--销售城市 操作方式:先删除再新增
	DELETE FROM [tbl_RouteCityControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @SaleCity
	INSERT INTO [tbl_RouteCityControl] (RouteId,CityId)
	SELECT @RouteId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--线路主题 操作方式:先删除再新增
	DELETE FROM [tbl_RouteThemeControl] WHERE RouteId=@RouteId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @RouteTheme
	INSERT INTO [tbl_RouteThemeControl] (RouteId,ThemeId)
	SELECT @RouteId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--写入数据到tbl_TourContactInfo
	IF(NOT EXISTS(SELECT 1 FROM tbl_TourContactInfo WHERE CompanyId=@CompanyId AND ContactName=@ContactName))
	BEGIN
		DECLARE @username NVARCHAR(50)
		SELECT @username=UserName FROM tbl_CompanyUser WHERE Id=@UserId
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactMQID],[UserName])
		VALUES (@CompanyId,@ContactName,@ContactTelephone,@ContactMQId,@username)
		SET @errorCount=@errorCount+@@ERROR
	END

	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateRouteInfoByStandard
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateRouteInfoByStandard
		SET @Result=1
	END
	
	RETURN @Result
END
GO

