go
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-24
-- Description:	修改公司明细资料
-- @ReturnValue  0:操作失败  1:操作成功
-- history:zhangzy   2010-9-2  将@License长度有10调整为50
-- =============================================
CREATE PROC [dbo].[proc_CompanyInfo_Update]	
	@UpdateType CHAR(1),   --修改类型:1:运营后台修改公司资料,2:单位信息管理修改自己公司资料,3:高级网店修改公司基本档案
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CompanyId CHAR(36),   --公司ID
	@CityId INT,    --城市
	@CompanyAddress NVARCHAR(250),  --地址
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
	@Remark NVARCHAR(MAX),   --公司介绍备注
	@ShortRemark NVARCHAR(250),   --公司简介(业务优势)	
	@CompanyImg  NVARCHAR(250),  --宣传图片	
	@CompanyLogo NVARCHAR(250),  --企业logo
	@CompanySignet  NVARCHAR(250),  --电子公章
	@BusinessCertImg NVARCHAR(250),  --经营许可证
	@LicenceImg NVARCHAR(250),  --营业执照
	@TaxRegImg NVARCHAR(250),  --税务登记证
	@XMLBankRoot NVARCHAR(MAX),  --银行帐号sqlxml脚本	
	@XMLRootSaleCity NVARCHAR(MAX),  --公司销售城市的sqlxml脚本
	@XMLRootArea NVARCHAR(MAX),  --公司线路区域的sqlxml脚本
	@BusinessProperties TINYINT,  --企业性质  1:旅行社
	@XMLRootCompanyType NVARCHAR(MAX),  --公司类型的sqlxml脚本	
	@UserId CHAR(36) OUTPUT,    --管理员的用户ID
	@ReturnValue CHAR(1) OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN	
	--DECLARE @UserId CHAR(36);  --管理员的用户ID	
	DECLARE @SQLCompanyUser NVARCHAR(MAX);
	DECLARE @SQLAttach NVARCHAR(MAX);
	DECLARE @PermissionList NVARCHAR(3000);   --权限列表
	DECLARE @CompanyTypeListOld NVARCHAR(20);  --原公司类型列表,无逗号分割,如:批发商,零售商,地接社身份,则为123,id值为升序排列 
	DECLARE @CompanyTypeListNew NVARCHAR(20);  --新的公司类型列表,无逗号分割,如:批发商,零售商,地接社身份,则为123,id值为升序排列
	DECLARE @ErrorAttach INT; --操作公司附件产生的错误信息
	SET @ErrorAttach = 0;
	SET @UserId = '';
	SET @SQLCompanyUser = '';
	SET @SQLAttach = '';
	SET @PermissionList = '';
	SET @CompanyTypeListOld = '';
	SET @CompanyTypeListNew = '';

	--开始事务
	BEGIN TRAN CompanyDetailInfo_Update
	----------------------修改公司管理员用户以及公司的资料-------------
	IF(@UpdateType='1')  --表示为运营后台的修改,则要修改公司总帐号的资料
	BEGIN
		--查询管理员的用户ID
		SELECT @UserId=ID FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsAdmin='1'	
		--修改管理员用户表
		SET @SQLCompanyUser = 'UPDATE [tbl_CompanyUser] SET [ProvinceId]=' + CAST(@ProvinceId AS NVARCHAR(10)) + ',[CityId]=' + CAST(@CityId AS NVARCHAR(10)) + ',[ContactName]=''' + @ContactName + ''',[ContactTel]=''' + @ContactTel + ''',[ContactFax]=''' + @ContactFax + ''',[ContactMobile]=''' + @ContactMobile + ''',[ContactEmail]=''' + @ContactEmail + ''',[QQ]=''' + @ContactQQ + ''',[MSN]=''' + @ContactMSN + '''';
		IF(@Password<>'')
		BEGIN
			SET @SQLCompanyUser = @SQLCompanyUser + ',[Password]=''' + @Password + ''',[EncryptPassword]=''' + @EncryptPassword + ''',[MD5Password]=''' + @MD5Password + '''';
			UPDATE im_member SET im_password=@MD5Password WHERE bs_uid=@UserId;
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN User_Update;
				RETURN;
			END	
		END
		SET @SQLCompanyUser = @SQLCompanyUser + ' WHERE Id=''' + @UserId + ''' AND CompanyId=''' + @CompanyId + '''';
		EXECUTE sp_executesql @SQLCompanyUser;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
		--修改公司表	
		UPDATE [tbl_CompanyInfo] SET [ProvinceId]=@ProvinceId,[CityId]=@CityId,[CompanyType]=@BusinessProperties,[CompanyName]=@CompanyName,[CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[License]=@License,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark,[ShortRemark]=@ShortRemark WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END		
		--修改公司身份类型
		--公司类型的sqlxml脚本
		DECLARE @hdocCompanyType int;
		EXEC sp_xml_preparedocument @hdocCompanyType OUTPUT, @XMLRootCompanyType;	
		--查询原来的公司类型
		SELECT @CompanyTypeListOld=@CompanyTypeListOld + CAST(TypeId AS NVARCHAR(5)) FROM [tbl_CompanyTypeList] WHERE CompanyId=@CompanyId ORDER BY TypeId;
		--查询新的公司类型
		SELECT @CompanyTypeListNew=@CompanyTypeListNew + CAST(TypeId AS NVARCHAR(5)) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) ORDER BY TypeId;
		IF(@CompanyTypeListOld<>@CompanyTypeListNew)  --表示公司身份有变动
		BEGIN
			--先删除原公司类型
			DELETE FROM [tbl_CompanyTypeList] WHERE [CompanyId]=@CompanyId;		
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
			--添加公司所有类型
			INSERT INTO [tbl_CompanyTypeList](
			[CompanyId],[TypeId]
			) SELECT @CompanyID,TypeId
			FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') 
			  WITH (TypeId TINYINT);
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
			--查询权限列表	
			--根据身份查询特有权限
			SELECT @PermissionList=@PermissionList + ',' + CAST(list.Id AS NVARCHAR(10)) FROM tbl_SysPermissionList list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;
			--查询通用服务的权限
			SELECT @PermissionList=@PermissionList + ',' + CAST(list.Id AS NVARCHAR(10)) FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (4,5,6)
			IF(LEN(@PermissionList)>0 AND CHARINDEX(',', @PermissionList)>0)
				SET @PermissionList = SUBSTRING(@PermissionList, 1, LEN(@PermissionList)-1);
			ELSE
				SET @PermissionList = '';
			--修改管理员角色权限
			UPDATE [tbl_CompanyRoles] SET [PermissionList]=@PermissionList WHERE [CompanyID]=@CompanyId AND IsAdminRole=1;
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
			--清除其他所有非管理员角色的权限
			UPDATE [tbl_CompanyRoles] SET [PermissionList]='' WHERE [CompanyID]=@CompanyId AND IsAdminRole=0;			
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
		END
		EXEC sp_xml_removedocument @hdocCompanyType; 
	END
	ELSE IF(@UpdateType='2')   --单位信息管理修改自己公司资料
	BEGIN
		UPDATE [tbl_CompanyInfo] SET [CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
	ELSE IF(@UpdateType='3')   --高级网店修改公司基本档案
	BEGIN
		UPDATE [tbl_CompanyInfo] SET [CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END	

	----------------------修改公司附件信息------------
	IF(@UpdateType='1')  --运营后台->证书管理(营业执照,经营许可证,税务登记证)
	BEGIN
		--先删除,再新增		
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('LicenceImg','BusinessCertImg','TaxRegImg');
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'LicenceImg',@LicenceImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'BusinessCertImg',@BusinessCertImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TaxRegImg',@TaxRegImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
	ELSE IF(@UpdateType='2')  --单位信息管理->宣传图片,企业LOGO,企业公章,营业执照,经营许可证,税务登记证
	BEGIN
		--先删除,再新增
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND [FieldValue]=@CompanyImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND [FieldValue]=@CompanyLogo)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('CompanyImg','CompanyLogo','CompanySignet','LicenceImg','BusinessCertImg','TaxRegImg');
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanyImg',@CompanyImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanyLogo',@CompanyLogo);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanySignet',@CompanySignet);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'LicenceImg',@LicenceImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'BusinessCertImg',@BusinessCertImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TaxRegImg',@TaxRegImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
--	ELSE IF(@UpdateType='2')  --修改高级网店公司档案
--	BEGIN
--		----高级网店公司档案,现在不对证书进行修改
--	END	
	----------------银行帐号信息------------	
	IF(@UpdateType='1' OR @UpdateType='2')  --运营后台,单位信息管理
	BEGIN
		--先删除所有银行帐号信息
		DELETE FROM [tbl_BankAccount] WHERE CompanyId=@CompanyId;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
		--添加银行帐号
		DECLARE @bankhdoc int;
		EXEC sp_xml_preparedocument @bankhdoc OUTPUT, @XMLBankRoot;
		INSERT INTO [tbl_BankAccount](
		[CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId]
		) SELECT @CompanyID,BankAccountName,AccountNumber,BankName,TypeId
		FROM OPENXML(@bankhdoc, N'/ROOT/BankAccountsUpdate') 
		  WITH (BankAccountName  NVARCHAR(100),AccountNumber NVARCHAR(250),BankName NVARCHAR(100),TypeId TINYINT);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @bankhdoc; 
			RETURN;
		END
		EXEC sp_xml_removedocument @bankhdoc; 
	END

	IF(@UpdateType='1')  --运营后台,修改公司以及所有用户销售城市,线路区域信息
	BEGIN
		-----------------------销售城市------------------
		DECLARE @hdocSaleCity int;
		EXEC sp_xml_preparedocument @hdocSaleCity OUTPUT, @XMLRootSaleCity;

		--先删除公司已不存在的销售城市
		DELETE FROM [tbl_CompanyCityControl] WHERE [CompanyId]=@CompanyID AND CityId NOT IN (SELECT CityId	FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') WITH (ProvinceId INT,CityId INT));
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocSaleCity; 
			RETURN;
		END
		--添加公司销售城市		
		INSERT INTO [tbl_CompanyCityControl](
			[CompanyId],[ProvinceId],[CityId]
			) SELECT @CompanyID,A.ProvinceId,A.CityId
		FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') WITH (ProvinceId INT,CityId INT) AS A
			WHERE A.CityId NOT IN (SELECT CityId FROM [tbl_CompanyCityControl] WHERE CompanyId=@CompanyID);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocSaleCity; 
			RETURN;
		END
		EXEC sp_xml_removedocument @hdocSaleCity; 
		
		-----------------------公司线路区域------------------
		DECLARE @hdocArea int;
		EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;
		---先删除公司线路区域,已不存在了的线路区域
		DELETE FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));	
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END
		--添加公司线路区域
		INSERT INTO [tbl_CompanyAreaConfig](
			[CompanyId],[AreaId]
			) SELECT @CompanyID,A.AreaId
		FROM OPENXML(@hdocArea, N'/ROOT/Area') 
		  WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyID);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END

		-----------------------用户线路区域------------------
		--删除已不存在了的所有帐号的线路区域
		DELETE FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));	
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END
		--添加管理员用户新的线路区域
		INSERT INTO [tbl_CompanyUserAreaControl](
			[UserId],[CompanyId],[AreaId]
			) SELECT @UserId,@CompanyID,A.AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND [UserId]=@UserId);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update; 
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END		
		EXEC sp_xml_removedocument @hdocArea; 
	END

	--执行成功		
	SET @ReturnValue = '1';
	COMMIT TRAN CompanyDetailInfo_Update;
END
go
