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
-- Create date: 2010-07-23
-- Description:	高级网店申请审核
-- =============================================
ALTER PROCEDURE proc_SysApplyService_EshopChecked
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@DomainName CHAR(255),--通过的域名
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT--1:成功 0:失败 2:域名重复
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CDomainName NVARCHAR(255)--原审核通过域名
	DECLARE @CompanyId CHAR(36)--申请人公司编号

	DECLARE @CompanyType TINYINT--公司类型
	DECLARE @CompanyName NVARCHAR(250)--公司名称
	DECLARE @IsDisabled CHAR(1)--域名是否禁用
	DECLARE @DomainType TINYINT--域名类型
	SET @IsDisabled='1'
	SET @DomainType=1

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @IsDisabled='0'
	END

	--PRINT @IsDisabled

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CDomainName=CheckText,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId
	SELECT @CompanyType=CompanyType,@CompanyName=CompanyName FROM tbl_CompanyInfo WHERE Id=@CompanyId	

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName)--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--写入域名信息				
			INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
			VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,'',GETDATE(),@IsDisabled)
			SET @errorCount=@errorCount+@@ERROR	
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END )
			SET @errorCount=@errorCount+@@ERROR	

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>@CDomainName AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--更新续费信息
			IF EXISTS(SELECT 1 FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime)
			BEGIN
				UPDATE  tbl_SysApplyServiceFee SET EnableTime=@EnableTime,ExpireTime=@ExpireTime WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
				SET @errorCount=@errorCount+@@ERROR
			END
			ELSE
			BEGIN
				INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
				SET @errorCount=@errorCount+@@ERROR
			END
			--域名信息
			IF EXISTS(SELECT 1 FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName)
			BEGIN
				UPDATE [tbl_SysCompanyDomain] SET [Domain]=@DomainName,[IsDisabled]=@IsDisabled WHERE CompanyId=@CompanyId AND Domain=@CDomainName
				SET @errorCount=@errorCount+@@ERROR
			END
			ELSE
			BEGIN				
				INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
				VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,'',GETDATE(),@IsDisabled)
				SET @errorCount=@errorCount+@@ERROR
			END
			--公司已经开通的收费项目
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END)
			SET @errorCount=@errorCount+@@ERROR	

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			BEGIN TRAN
			--审核信息处理
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--续费信息处理
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			SET @errorCount=@errorCount+@@ERROR
			--域名信息处理
			UPDATE [tbl_SysCompanyDomain] SET [IsDisabled]='1' WHERE CompanyId=@CompanyId AND [Domain]=@CDomainName
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END	
	END

	RETURN @Result	
END
GO


-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-23
-- Description:	高级网店申请审核
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysApplyService_EshopChecked]
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@DomainName NVARCHAR(250),--通过的域名
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT--1:成功 0:失败 2:域名重复
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CDomainName NVARCHAR(255)--原审核通过域名
	DECLARE @CompanyId CHAR(36)--申请人公司编号

	DECLARE @CompanyType TINYINT--公司类型
	DECLARE @CompanyName NVARCHAR(250)--公司名称
	DECLARE @IsDisabled CHAR(1)--域名是否禁用
	DECLARE @DomainType TINYINT--域名类型
	SET @IsDisabled='1'
	SET @DomainType=1

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @IsDisabled='0'
	END

	--PRINT @IsDisabled

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CDomainName=CheckText,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId
	SELECT @CompanyType=CompanyType,@CompanyName=CompanyName FROM tbl_CompanyInfo WHERE Id=@CompanyId	

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--写入域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)	
			BEGIN
				INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
				VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,'',GETDATE(),@IsDisabled)
				SET @errorCount=@errorCount+@@ERROR	
			END
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END )
			SET @errorCount=@errorCount+@@ERROR	

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND @DomainName<>@CDomainName AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--更新续费信息
			IF EXISTS(SELECT 1 FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime)
			BEGIN
				UPDATE  tbl_SysApplyServiceFee SET EnableTime=@EnableTime,ExpireTime=@ExpireTime WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
				SET @errorCount=@errorCount+@@ERROR
			END
			ELSE
			BEGIN
				INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
				SET @errorCount=@errorCount+@@ERROR
			END
			--域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)
			BEGIN
				IF EXISTS(SELECT 1 FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName)
				BEGIN
					UPDATE [tbl_SysCompanyDomain] SET [Domain]=@DomainName,[IsDisabled]=@IsDisabled WHERE CompanyId=@CompanyId AND Domain=@CDomainName
					SET @errorCount=@errorCount+@@ERROR
				END
				ELSE
				BEGIN				
					INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
					VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,'',GETDATE(),@IsDisabled)
					SET @errorCount=@errorCount+@@ERROR
				END
			END
			ELSE
			BEGIN
				DELETE FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName
			END
			--公司已经开通的收费项目
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END)
			SET @errorCount=@errorCount+@@ERROR	

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			BEGIN TRAN
			--审核信息处理
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--续费信息处理
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			SET @errorCount=@errorCount+@@ERROR
			--域名信息处理
			UPDATE [tbl_SysCompanyDomain] SET [IsDisabled]='1' WHERE CompanyId=@CompanyId AND [Domain]=@CDomainName
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END	
	END

	RETURN @Result	
END
GO
