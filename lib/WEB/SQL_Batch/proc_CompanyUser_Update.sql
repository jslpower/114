go
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	修改公司帐号信息
-- @ReturnValue  0:操作失败  1:操作成功 
-- history:zhangzy   2010-9-2  将@ContactQQ长度有20调整为50
-- =============================================
CREATE PROC [dbo].[proc_CompanyUser_Update]	
	@Password varchar(50),    --未加密密码,若为空则不修改
	@EncryptPassword nvarchar(128),   --SHA加密密码,若为空则不修改
	@MD5Password nvarchar(50),   --MD5加密密码,若为空则不修改
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
	@UserId NVARCHAR(36),  --用户ID
	@CompanyID NVARCHAR(36),  --公司ID
	@XMLRootArea NVARCHAR(MAX), --用户线路区域的sqlxml脚本
	@ReturnValue CHAR(1) OUTPUT    --0:操作失败  1:操作成功
AS
BEGIN	
	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @SQLIM NVARCHAR(MAX);
	SET @SQLIM = '';
	SET @SQL = '';

	--开始事务
	BEGIN TRAN User_Update
	--修改用户表
	SET @SQL = 'UPDATE [tbl_CompanyUser] SET [ContactName]=''' + @ContactName + ''',[ContactSex]=''' + @ContactSex + ''',[ContactTel]=''' + @ContactTel + ''',[ContactFax]=''' + @ContactFax + ''',[ContactMobile]=''' + @ContactMobile + ''',[ContactEmail]=''' + @ContactEmail + ''',[QQ]=''' + @ContactQQ + ''',[MSN]=''' + @ContactMSN + '''';
	--判断是否要修改密码
	IF(@Password<>'')
	BEGIN
		SET @SQL = @SQL + ',[Password]=''' + @Password + ''',[EncryptPassword]=''' + @EncryptPassword + ''',[MD5Password]=''' + @MD5Password + '''';	
		UPDATE im_member SET im_password=@MD5Password WHERE bs_uid=@UserId;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN User_Update;
			RETURN;
		END		
	END
	--判断是否要修改角色,角色ID不能空,才进行修改
	IF(@RoleID<>'')
	BEGIN
		SET @SQL = @SQL + ',[RoleID]=''' + @RoleID + '''';
	END	
	--修改部门
	SET @SQL = @SQL + ',[DepartId]=''' + @DepartId + ''',[DepartName]=''' + @DepartName + '''';
	--设置修改条件
	SET @SQL = @SQL + ' WHERE Id=''' + @UserId + ''' AND CompanyID=''' + @CompanyID + '''';
print @SQL
	--执行sql语句
	execute sp_executesql @SQL
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN User_Update;
		RETURN;
	END	

	DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;	
	--先删除用户不存在了的线路区域关系
	DELETE tbl_CompanyUserAreaControl WHERE UserId=@UserId AND CompanyId=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 		
		ROLLBACK TRAN User_Update;		
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END	
	--添加用户新的线路区域	
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,A.AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND [UserId]=@UserId);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0';
		ROLLBACK TRAN User_Update;		
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END	
	EXEC sp_xml_removedocument @hdocArea; 

	SET @ReturnValue = '1';
	COMMIT TRAN User_Update;
END
go