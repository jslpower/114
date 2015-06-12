go
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	新增公司子帐号信息
-- @ReturnValue  0:操作失败  1:操作成功   2:用户名数据已存在
-- history:zhangzy   2010-9-2  将@ContactQQ长度有20调整为50
-- =============================================
CREATE PROC [dbo].[proc_CompanyUserChild_Insert]	
	@UserId char(36),   --用户ID	
	@CompanyId char(36),   --公司ID	
	@UserName nvarchar(100),   --用户名
	@Password varchar(50),    --未加密密码
	@EncryptPassword nvarchar(128),   --SHA加密密码
	@MD5Password nvarchar(50),   --MD5加密密码
	@ContactName nvarchar(100),   --联系人
	@ContactSex char(1),   --性别
	@ContactTel nvarchar(50),   --电话
	@ContactFax nvarchar(50),   --传真
	@ContactMobile nvarchar(50),   --手机
	@ContactEmail nvarchar(50),   --EMAIL
	@ContactQQ varchar(50),  
	@ContactMSN nvarchar(50),
	@RoleID char(36),   --角色ID
	@DepartId char(36),  --部门ID
	@DepartName nvarchar(50),   --部门名称
    @XMLRootArea NVARCHAR(MAX), --用户线路区域的sqlxml脚本
	@ReturnMQID NVARCHAR(250) OUTPUT,   --操作成功返回MQID
	@ReturnProvinceId INT OUTPUT,   --操作成功返回省份ID
	@ReturnValue CHAR(1) OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN		
	DECLARE @MQID INT;  --MQID 
	DECLARE @ProvinceId int;   --省份ID
	DECLARE @CityId int;   --城市ID
	DECLARE @IsAdmin CHAR(1);  --是否管理员
	SET @IsAdmin = '0';
	SET @ReturnMQID = '';
	SET @ReturnProvinceId = 0;

	--检查用户名是否已存在
	IF((SELECT COUNT(1) FROM im_member WHERE im_username=@UserName)>0)
	BEGIN
print @UserName
		SET @ReturnValue = '2';
		RETURN;
	END
	--开始事务
	BEGIN TRAN UserChild_Insert

	--计算MQ的ID
	SELECT @MQID=MAX(im_uid)+1 FROM im_member;
	--插入MQ
	INSERT INTO [im_member](
	[im_uid],[im_password],[im_displayname],[im_username],[bs_uid]
	)VALUES(
	@MQID,@MD5Password,@ContactName,@UserName,@UserId
	);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0';
		ROLLBACK TRAN UserChild_Insert;
		RETURN;
	END
	
	--查询公司所在的省份,城市ID
	SELECT @ProvinceId=ProvinceId,@CityId=CityId FROM tbl_CompanyInfo WHERE Id=@CompanyId

	--插入用户表
	INSERT INTO [tbl_CompanyUser](
		[Id],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],[ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],[RoleID],[IsAdmin],[DepartId],[DepartName])VALUES(
		@UserId,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactQQ,@MQID,@ContactMSN,@RoleID,@IsAdmin,@DepartId,@DepartName);

	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN UserChild_Insert;
		RETURN;
	END	
	
	--添加用户线路区域
    DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;	
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN UserChild_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	
	--执行成功
	EXEC sp_xml_removedocument @hdocArea; 
	SET @ReturnValue = '1';
	SET @ReturnMQID = @MQID;
	SET @ReturnProvinceId = @ProvinceId;
	COMMIT TRAN UserChild_Insert;
END
go