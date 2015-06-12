-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-20
-- Description:	修改团队信息(模板团)
-- =============================================
ALTER PROCEDURE proc_TourList_UpdateTemplateTourInfo
	@TemplateTourId CHAR(36),--模板团编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@TourType TINYINT,--团队类型
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT=0,--行程是否异常(百分制)
	@TourPriceError INT=0,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(500)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(500)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(500)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(500)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(500)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(500)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(500)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(500)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(500)=NULL,--温馨提醒
	@LeaveCity INT,--出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN	
	SET @Result=0
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号
	
	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc
	
	BEGIN TRANSACTION UpdateStandardTourInfo
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新模板团信息
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]=@PlanPeopleCount
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id=@TemplateTourId	

	--表变量存放此次要更新的子团信息
	DECLARE @tmptbl TABLE(TourId CHAR(36))
	INSERT INTO @tmptbl(TourId) SELECT Id FROM tbl_TourList WHERE ParentTourID=@TemplateTourId AND LeaveDate>=GETDATE()	

	--更新子团信息 订单人数(除留位过期、不受理的订单)>计划人数时 计划人数=订单人数
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays,[ComeBackDate]=DATEADD(DAY,@TourDays-1,[LeaveDate])
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]= CASE WHEN OrderPeopleNumber>@PlanPeopleCount THEN OrderPeopleNumber ELSE @PlanPeopleCount END
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[CreateTime]=GETDATE()
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR
	
	--表变量中加入模板团进行行程价格等信息的修改
	INSERT INTO @tmptbl(TourId) VALUES(@TemplateTourId)

	--写入行程信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT B.[TourId],A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(50),Vehicle NVARCHAR(50),TrafficNumber NVARCHAR(50),House NVARCHAR(50),Dinner NVARCHAR(50),PlanContent NVARCHAR(2000),PlanDay INT,PlanId CHAR(36)) AS A,@tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
		EXECUTE sp_xml_removedocument @hdoc		
	END
	ELSE--快速发布
	BEGIN
		DELETE FROM [tbl_TourFastPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) SELECT B.[TourId],@QuickPlan FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
	END

	--写入报价信息 先删除再新增
	DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT B.[TourId],A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT) AS A,@tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_removedocument @hdoc

	--写入服务标准信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourServiceStandard]
			([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		SELECT
			 B.TourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice
		FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--写入出港城市信息 先删除再新增	
	DELETE FROM [tbl_TourAreaControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	INSERT INTO [tbl_TourAreaControl](TourId,CityId) SELECT B.TourId,@LeaveCity FROM @tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR

	--写入销售城市信息
	DELETE FROM [tbl_TourCityControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT B.TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	DELETE FROM [tbl_TourThemeControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT B.TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT B.TourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A,@tmptbl AS B
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队自动停收状态 非手动停收手动客满→自动停收
	UPDATE [tbl_TourList] SET [TourState]=3 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE() AND TourState NOT IN(0,2)
	SET @errorCount=@errorCount+@@ERROR
	
	--标记团队状态 自动客满→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]>[OrderPeopleNumber] AND TourState=4
	SET @errorCount=@errorCount+@@ERROR
	--标记团队状态 正常→自动客满
	UPDATE [tbl_TourList] SET [TourState]=4 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]=[OrderPeopleNumber] AND TourState=1
	SET @errorCount=@errorCount+@@ERROR

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TemplateTourId,@IsParentTour=N'1'

	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--更新团队订单的线路区域信息
	UPDATE tbl_TourOrder SET AreaId=@AreaId WHERE TourId IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-20
-- Description:	修改团队信息(模板团)
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_UpdateTemplateTourInfo]
	@TemplateTourId CHAR(36),--模板团编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@TourType TINYINT,--团队类型
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT=0,--行程是否异常(百分制)
	@TourPriceError INT=0,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(MAX)=NULL,--温馨提醒
	@LeaveCity INT,--出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN	
	SET @Result=0
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号
	
	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc
	
	BEGIN TRANSACTION UpdateStandardTourInfo
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新模板团信息
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]=@PlanPeopleCount
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id=@TemplateTourId	

	--表变量存放此次要更新的子团信息
	DECLARE @tmptbl TABLE(TourId CHAR(36))
	INSERT INTO @tmptbl(TourId) SELECT Id FROM tbl_TourList WHERE ParentTourID=@TemplateTourId AND LeaveDate>=GETDATE()	

	--更新子团信息 订单人数(除留位过期、不受理的订单)>计划人数时 计划人数=订单人数
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays,[ComeBackDate]=DATEADD(DAY,@TourDays-1,[LeaveDate])
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]= CASE WHEN OrderPeopleNumber>@PlanPeopleCount THEN OrderPeopleNumber ELSE @PlanPeopleCount END
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[CreateTime]=GETDATE()
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR
	
	--表变量中加入模板团进行行程价格等信息的修改
	INSERT INTO @tmptbl(TourId) VALUES(@TemplateTourId)

	--写入行程信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT B.[TourId],A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(50),Vehicle NVARCHAR(50),TrafficNumber NVARCHAR(50),House NVARCHAR(50),Dinner NVARCHAR(50),PlanContent NVARCHAR(2000),PlanDay INT,PlanId CHAR(36)) AS A,@tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
		EXECUTE sp_xml_removedocument @hdoc		
	END
	ELSE--快速发布
	BEGIN
		DELETE FROM [tbl_TourFastPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) SELECT B.[TourId],@QuickPlan FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
	END

	--写入报价信息 先删除再新增
	DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT B.[TourId],A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT) AS A,@tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_removedocument @hdoc

	--写入服务标准信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourServiceStandard]
			([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		SELECT
			 B.TourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice
		FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--写入出港城市信息 先删除再新增	
	DELETE FROM [tbl_TourAreaControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	INSERT INTO [tbl_TourAreaControl](TourId,CityId) SELECT B.TourId,@LeaveCity FROM @tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR

	--写入销售城市信息
	DELETE FROM [tbl_TourCityControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT B.TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	DELETE FROM [tbl_TourThemeControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT B.TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT B.TourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A,@tmptbl AS B
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队自动停收状态 非手动停收手动客满→自动停收
	UPDATE [tbl_TourList] SET [TourState]=3 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE() AND TourState NOT IN(0,2)
	SET @errorCount=@errorCount+@@ERROR
	
	--标记团队状态 自动客满→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]>[OrderPeopleNumber] AND TourState=4
	SET @errorCount=@errorCount+@@ERROR
	--标记团队状态 正常→自动客满
	UPDATE [tbl_TourList] SET [TourState]=4 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]=[OrderPeopleNumber] AND TourState=1
	SET @errorCount=@errorCount+@@ERROR

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TemplateTourId,@IsParentTour=N'1'

	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--更新团队订单的线路区域信息
	UPDATE tbl_TourOrder SET AreaId=@AreaId WHERE TourId IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO

