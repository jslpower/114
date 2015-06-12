go
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	新增公司
-- @ReturnValue  0:操作失败  1:操作成功   2:用户名数据已存在   3:Email数据已在用户表中存在
-- history:zhangzy   2010-9-2  将@License长度有10调整为50
-- =============================================
CREATE PROC [dbo].[proc_CompanyInfo_Insert]	
	@UserId CHAR(36),  --用户ID
	@CompanyId CHAR(36),   --公司ID
	@UserName NVARCHAR(100),  --用户名
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CityId INT,    --城市
	@CompanyAddress NVARCHAR(250),  --公司地址
	@CompanyBrand NVARCHAR(50),   --名牌
	@CompanyName NVARCHAR(250),   --公司名称
	@ContactName NVARCHAR(20),   --联系人
	@ContactEmail NVARCHAR(50),   --EMAIL
	@ContactFax NVARCHAR(50),  --传真
	@ContactMobile NVARCHAR(50),   --手机
	@ContactMSN NVARCHAR(50),   --MSN
	@ContactQQ NVARCHAR(250),   --QQ
	@ContactTel NVARCHAR(50),   --电话	
	@License NVARCHAR(50),  --许可证号
	@ProvinceId INT,   --省份ID
	@CommendPeople NVARCHAR(50),   --引荐人	
	@ShortRemark NVARCHAR(250),   --公司简介(业务优势)
	@XMLRootSaleCity NVARCHAR(MAX),  --公司销售城市的sqlxml脚本
	@XMLRootArea NVARCHAR(MAX),  --公司线路区域的sqlxml脚本
	@BusinessProperties TINYINT,  --企业性质  1:旅行社
	@XMLRootCompanyType NVARCHAR(MAX),  --公司类型的sqlxml脚本
	@ReturnMQID NVARCHAR(250) OUTPUT,   --操作成功返回MQID
	@ReturnValue INT OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN	
	DECLARE @MQID INT;  --MQID
	DECLARE @IsCheck CHAR(1);   --是否审核
	DECLARE @RoleId CHAR(36);   --管理员角色ID  
	DECLARE @RoleName NVARCHAR(20);  --管理员角色名称
	DECLARE @PermissionList NVARCHAR(3000);   --权限列表
	DECLARE @DepartId CHAR(36);   --部门ID
	DECLARE @DepartName NVARCHAR(50);   --部门名称
	DECLARE @ContactSex CHAR(1);   --性别
	DECLARE @IsAdmin CHAR(1);  --是否管理员	
	DECLARE @ErrorRate INT;  --操作诚信数据的事务结果	
	SET @IsCheck = '0';
	SET @RoleId = NEWID();
	SET @RoleName = '管理员';
	SET @DepartId = '00000000-0000-0000-0000-000000000000';
	SET @DepartName = '';
	SET @ContactSex = '2';
	SET @IsAdmin = '1';
	SET @PermissionList = '';
	SET @ReturnMQID = '';
	SET @ErrorRate = 0;

	--检查Email是否已存在
	IF((SELECT COUNT(1) FROM tbl_CompanyUser WHERE ContactEmail=@ContactEmail)>0)
	BEGIN
		SET @ReturnValue = 3;
		RETURN;
	END
	--检查用户名是否已存在
	IF((SELECT COUNT(1) FROM im_member WHERE im_username=@UserName)>0)
	BEGIN
		SET @ReturnValue = 2;
		RETURN;
	END	
	--开始事务
	BEGIN TRAN Company_Insert		
	--计算MQ的ID
	SELECT @MQID=MAX(im_uid)+1 FROM im_member;
	IF(@MQID IS NULL)
		SET @MQID = 1;
	--插入MQ
	INSERT INTO [im_member](
	[im_uid],[im_password],[im_displayname],[im_username],[bs_uid]
	)VALUES(
	@MQID,@MD5Password,@ContactName,@UserName,@UserId
	);
--	INSERT INTO [im_member](
--		[im_password],[im_displayname],[im_username],[bs_uid]
--		)VALUES(
--		@MD5Password,@ContactName,@UserName,@UserId
--		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Insert;		
		RETURN;
	END	
	--SET @MQID = @@Identity
	--插入公司表	
	INSERT INTO [tbl_CompanyInfo](
		[Id],[ProvinceId],[CityId],[CompanyType],[CompanyName],[CompanyBrand],[CompanyAddress],[License],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[ContactMQ],[ContactQQ],[ContactMSN],[IsCheck],[CommendPeople],[ShortRemark]
		)VALUES(
		@CompanyId,@ProvinceId,@CityId,@BusinessProperties,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,CAST(@MQID AS VARCHAR(20)),@ContactQQ,@ContactMSN,@IsCheck,@CommendPeople,@ShortRemark
		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	--插入用户表
	INSERT INTO [tbl_CompanyUser](
		[Id],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],[ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],[RoleID],[IsAdmin],[DepartId],[DepartName]
		)VALUES(
		@UserId,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactQQ,@MQID,@ContactMSN,@RoleID,@IsAdmin,@DepartId,@DepartName
		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	--公司类型的sqlxml脚本
	DECLARE @hdocCompanyType int;
	EXEC sp_xml_preparedocument @hdocCompanyType OUTPUT, @XMLRootCompanyType;	
	--添加公司所有类型
	INSERT INTO [tbl_CompanyTypeList](
	[CompanyId],[TypeId]
	) SELECT @CompanyID,TypeId
	FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') 
	  WITH (TypeId TINYINT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocCompanyType; 
		RETURN;
	END
	--查询权限列表	
	--根据身份查询特有权限
	SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;
	--查询通用服务的权限
	SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (4,5,6)
	IF(LEN(@PermissionList)>0 AND CHARINDEX(',', @PermissionList)>0)
		SET @PermissionList = SUBSTRING(@PermissionList, 1, LEN(@PermissionList)-1);
	ELSE
		SET @PermissionList = '';
	--创建管理员角色	
	INSERT INTO [tbl_CompanyRoles]
           ([ID],[RoleName],[PermissionList],[IsAdminRole],[CompanyID],[OperatorID])
     VALUES (@RoleId, @RoleName, @PermissionList, @IsAdmin, @CompanyId,@UserId);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocCompanyType; 
		RETURN;
	END
	--以上执行正确,则删除公司类型的XML
	EXEC sp_xml_removedocument @hdocCompanyType; 

    --新增常规团价格等级
	INSERT INTO [tbl_CompanyPriceStand]
           ([ID],[CommonPriceStandID],[PriceStandName],[CompanyID],[IsSystem]) VALUES(NEWID(),'COMPRICE-0000-0000-0000-000000000026', '常规团', @CompanyId, 0);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	
	--添加公司附件信息
--	INSERT INTO [tbl_CompanyAttachInfo](
--		[CompanyId],[CompanyImg],[CompanyLogo],[LicenceImg],[BusinessCertImg],[TaxRegImg],[CommitmentImg]
--		)VALUES(
--		@CompanyId,'','','','','',''
--		);
    --添加公司设置
--	INSERT INTO [tbl_CompanySetting](
--		[CompanyId],[TourStopTime],[TourCity]
--		)VALUES(
--		@CompanyId,0,0
--		);
	--添加团队状态基础数据
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,1,'推荐');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,2,'促销');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,3,'最新');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,4,'品质');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,5,'纯玩');
	
	--添加诚信体系基础数据
	--信用分值信息 
	--[tbl_RateScoreTotal].[ScoreType] 1.交易总分值2.评价总分值 3:服务品质总星星数,4:性价比总星星数,5:旅游内容安排总星星数,6:活跃总分值,7:推荐总分值
	DECLARE @i TINYINT;
	SET @i=1;
	WHILE(@i>=1 AND @i<=7)
	BEGIN
		INSERT INTO tbl_RateScoreTotal(CompanyId,ScoreType,ScorePoint) VALUES(@CompanyId,@i,0);
		SET @ErrorRate = @ErrorRate + @@ERROR;
		SET @i=@i+1;
	END

	--次数信息
	--[tbl_RateCountTotal].[ScoreType] 1.交易总次数, 2:好评总次数,3:中评总次数,4:差评总次数,5:活跃总次数,6:推荐总次数
	SET @i=1;
	WHILE(@i>=1 AND @i<=6)
	BEGIN
		INSERT INTO tbl_RateCountTotal(CompanyId,ScoreType,ScoreNumber) VALUES(@CompanyId,@i,0);
		SET @ErrorRate = @ErrorRate + @@ERROR;
		SET @i=@i+1;
	END
	--判断插入诚信数据有无出错
	IF @ErrorRate <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;		 
		RETURN;
	END

	--添加公司销售城市
	DECLARE @hdocSaleCity int;
	EXEC sp_xml_preparedocument @hdocSaleCity OUTPUT, @XMLRootSaleCity;
	INSERT INTO [tbl_CompanyCityControl](
		[CompanyId],[ProvinceId],[CityId]
		) SELECT @CompanyID,ProvinceId,CityId
	FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') 
	  WITH (ProvinceId INT,CityId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocSaleCity; 
		RETURN;
	END
	EXEC sp_xml_removedocument @hdocSaleCity; 
	DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;
	--添加公司线路区域
	INSERT INTO [tbl_CompanyAreaConfig](
		[CompanyId],[AreaId]
		) SELECT @CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	--添加管理员用户线路区域
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	EXEC sp_xml_removedocument @hdocArea; 
	--写入公司常用出港城市[插入热门城市:北京 上海 广州和本省]
	--热门
	INSERT INTO [tbl_CompanySiteControl](
		[CompanyId],[CityId]
		) SELECT @CompanyID,ID FROM tbl_SysCity WHERE CityName IN ('北京', '上海', '广州');
	--本省
	INSERT INTO [tbl_CompanySiteControl](
		[CompanyId],[CityId]
		) SELECT @CompanyID,ID FROM tbl_SysCity WHERE ID NOT IN (SELECT DISTINCT CityId FROM [tbl_CompanySiteControl] WHERE CompanyId=@CompanyID) AND ProvinceId=@ProvinceId AND IsSite='1';	

	--执行成功
	SET @ReturnValue = 1;
	SET @ReturnMQID = @MQID;
	COMMIT TRAN Company_Insert;
END
go