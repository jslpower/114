-- =============================================
-- Author:		汪奇志
-- Create date: 2010-04-19
-- Description:	写入线路信息
-- =============================================
CREATE PROCEDURE [dbo].[proc_RouteBasicInfo_AddRouteInfo]
	@RouteId CHAR(36),--线路编号	
	@CompanyId CHAR(36),--公司编号	
	@CompanyName NVARCHAR(250),--公司名称	
	@UserId CHAR(36),--用户编号	
	@ContactName NVARCHAR(20),--线路负责人	
	@ContactTelephone NVARCHAR(20),--线路负责人电话	
	@ContactUserName NVARCHAR(20),--线路负责人用户名	
	@ContactMQId NVARCHAR(250),--线路负责人MQ编号	
	@RouteName NVARCHAR(250),--线路名称	
	@RouteDays INT,--线路天数	
	@IssueTime DATETIME=NULL,--线路发布时间
	@AreaId INT=0,--线路区域
	@RouteType TINYINT,--线路区域类型
	-----------------原始版出港城市可多选----------------------
	--@LeaveCity NVARCHAR(2000),--出港城市 XML:<ROOT><LeaveCityInfo CityId="" /></ROOT>
	-----------------最新版出港城市单选------------------
	@LeaveCity int,
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT"></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT"></ROOT>
	@ReleaseType CHAR(1),--线路发布类型	
	--@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级 0:同行 1:门市 2:单房差 3:其它" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)"  CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PalnInfo PlanInterval="" Vehicle="" TrafficNumber="" House="" Dinner="" PlanContent="" PlanDay=""  /></ROOT>
	@ResideContent NVARCHAR(500)=NULL,--住宿(包含项目)
	@DinnerContent NVARCHAR(500)=NULL,--用餐(包含项目)
	@SightContent NVARCHAR(500)=NULL,--景点(包含项目)
	@CarContent NVARCHAR(500)=NULL,--用车(包含项目)
	@GuideContent NVARCHAR(500)=NULL,--导游(包含项目)
	@TrafficContent NVARCHAR(500)=NULL,--往返交通(包含项目)
	@IncludeOtherContent NVARCHAR(500)=NULL,--包含项目其它内容(包含项目)
	@NotContainService NVARCHAR(500)=NULL,--不包含项目(合并)
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

	BEGIN TRANSACTION InsertRouteInfoByStandard
	DECLARE @errorCount INT
	SET @errorCount=0

	--写入线路基本信息
	INSERT INTO [tbl_RouteBasicInfo]([ID],[CompanyID],[AreaId],[RouteType],[CompanyName]
           ,[OperatorID],[ContactName],[ContactTel],[ContactUserName],[ContactMQID]
           ,[RouteName],[RouteDays],[LeaveCityId],[IssueTime],[RouteIssueTypeId],[IsAccept])
     VALUES(@RouteId,@CompanyId,@AreaId,@RouteType,@CompanyName
			,@UserId,@ContactName,@ContactTelephone,@ContactUserName,@ContactMQId
			,@RouteName,@RouteDays,@LeaveCity,@IssueTime,@ReleaseType,0)
	SET @errorCount=@errorCount+@@ERROR

	DECLARE @hdoc INT
	IF(@ReleaseType='0') --标准发布
	BEGIN
		--写入线路详细信息(服务标准相关内容)
		INSERT INTO [tbl_RouteServiceStandard]([RouteBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
				,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		 VALUES(@RouteId,@ResideContent,@DinnerContent,@SightContent,@CarContent
				,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice)
		SET @errorCount=@errorCount+@@ERROR
		
		--写入线路行程信息	
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @StandardPlan
		INSERT INTO [tbl_RouteStandardPlan]([RouteBasicID],[PlanInterval],[Vehicle] ,[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])
		SELECT @RouteId,[PlanInterval] ,[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay]
		FROM OPENXML (@hdoc,'/ROOT/PalnInfo')
		WITH([PlanInterval] NVARCHAR(50),[Vehicle] NVARCHAR(50),[TrafficNumber] NVARCHAR(50),[House] NVARCHAR(50),[Dinner] NVARCHAR(50),[PlanContent] NVARCHAR(50),[PlanDay] INT,[PlanId] CHAR(36))

		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END
	ELSE
	BEGIN
		INSERT INTO [tbl_RouteFastPlan] ([RouteBasicID],[RoutePlan]) VALUES (@RouteId,@QuickPlan)
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入价格明细	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @PriceDetails
	/*----------原始存在客户等级类型模式----------------
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[CustomerLevelBasicType],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelType,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelType TINYINT,CustomerLevelId INT,RowMark TINYINT)
	*/
	------------新版不存在客户等级类型模式-----------
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT)
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	/*
	--写入出港城市
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @LeaveCity
	INSERT INTO [tbl_RouteAreaControl] (RouteId,CityId,AreaId)
	SELECT @RouteId,A.CityId,@AreaId FROM OPENXML(@hdoc,'/ROOT/LeaveCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	
	*/

	--写入销售城市信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_RouteCityControl] (RouteId,CityId)
	SELECT @RouteId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
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
		ROLLBACK TRANSACTION InsertRouteInfoByStandard
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION InsertRouteInfoByStandard
		SET @Result=1
	END
	
	RETURN @Result
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-04-19
-- Description:	写入线路信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_RouteBasicInfo_AddRouteInfo]
	@RouteId CHAR(36),--线路编号	
	@CompanyId CHAR(36),--公司编号	
	@CompanyName NVARCHAR(250),--公司名称	
	@UserId CHAR(36),--用户编号	
	@ContactName NVARCHAR(20),--线路负责人	
	@ContactTelephone NVARCHAR(20),--线路负责人电话	
	@ContactUserName NVARCHAR(20),--线路负责人用户名	
	@ContactMQId NVARCHAR(250),--线路负责人MQ编号	
	@RouteName NVARCHAR(250),--线路名称	
	@RouteDays INT,--线路天数	
	@IssueTime DATETIME=NULL,--线路发布时间
	@AreaId INT=0,--线路区域
	@RouteType TINYINT,--线路区域类型
	-----------------原始版出港城市可多选----------------------
	--@LeaveCity NVARCHAR(2000),--出港城市 XML:<ROOT><LeaveCityInfo CityId="" /></ROOT>
	-----------------最新版出港城市单选------------------
	@LeaveCity int,
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT"></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT"></ROOT>
	@ReleaseType CHAR(1),--线路发布类型	
	--@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级 0:同行 1:门市 2:单房差 3:其它" CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@PriceDetails NVARCHAR(MAX),--价格明细信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)"  CustomerLevelId="客户等级编号" RowMark="报价等级行标识 TINYINT" /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PalnInfo PlanInterval="" Vehicle="" TrafficNumber="" House="" Dinner="" PlanContent="" PlanDay=""  /></ROOT>
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目(合并)
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

	BEGIN TRANSACTION InsertRouteInfoByStandard
	DECLARE @errorCount INT
	SET @errorCount=0

	--写入线路基本信息
	INSERT INTO [tbl_RouteBasicInfo]([ID],[CompanyID],[AreaId],[RouteType],[CompanyName]
           ,[OperatorID],[ContactName],[ContactTel],[ContactUserName],[ContactMQID]
           ,[RouteName],[RouteDays],[LeaveCityId],[IssueTime],[RouteIssueTypeId],[IsAccept])
     VALUES(@RouteId,@CompanyId,@AreaId,@RouteType,@CompanyName
			,@UserId,@ContactName,@ContactTelephone,@ContactUserName,@ContactMQId
			,@RouteName,@RouteDays,@LeaveCity,@IssueTime,@ReleaseType,0)
	SET @errorCount=@errorCount+@@ERROR

	DECLARE @hdoc INT
	IF(@ReleaseType='0') --标准发布
	BEGIN
		--写入线路详细信息(服务标准相关内容)
		INSERT INTO [tbl_RouteServiceStandard]([RouteBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
				,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		 VALUES(@RouteId,@ResideContent,@DinnerContent,@SightContent,@CarContent
				,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice)
		SET @errorCount=@errorCount+@@ERROR
		
		--写入线路行程信息	
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @StandardPlan
		INSERT INTO [tbl_RouteStandardPlan]([RouteBasicID],[PlanInterval],[Vehicle] ,[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])
		SELECT @RouteId,[PlanInterval] ,[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay]
		FROM OPENXML (@hdoc,'/ROOT/PalnInfo')
		WITH([PlanInterval] NVARCHAR(50),[Vehicle] NVARCHAR(50),[TrafficNumber] NVARCHAR(50),[House] NVARCHAR(50),[Dinner] NVARCHAR(50),[PlanContent] NVARCHAR(50),[PlanDay] INT,[PlanId] CHAR(36))

		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END
	ELSE
	BEGIN
		INSERT INTO [tbl_RouteFastPlan] ([RouteBasicID],[RoutePlan]) VALUES (@RouteId,@QuickPlan)
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入价格明细	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @PriceDetails
	/*----------原始存在客户等级类型模式----------------
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[CustomerLevelBasicType],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelType,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelType TINYINT,CustomerLevelId INT,RowMark TINYINT)
	*/
	------------新版不存在客户等级类型模式-----------
	INSERT INTO [tbl_RouteBasicPriceDetail]([RouteBasicID] ,[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @RouteId,AdultPrice,ChildrenPrice,PriceStandId,CustomerLevelId,RowMark
	FROM OPENXML (@hdoc,'/ROOT/PriceInfo')
	WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT)
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	/*
	--写入出港城市
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @LeaveCity
	INSERT INTO [tbl_RouteAreaControl] (RouteId,CityId,AreaId)
	SELECT @RouteId,A.CityId,@AreaId FROM OPENXML(@hdoc,'/ROOT/LeaveCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	
	*/

	--写入销售城市信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_RouteCityControl] (RouteId,CityId)
	SELECT @RouteId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
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
		ROLLBACK TRANSACTION InsertRouteInfoByStandard
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION InsertRouteInfoByStandard
		SET @Result=1
	END
	
	RETURN @Result
END
GO
