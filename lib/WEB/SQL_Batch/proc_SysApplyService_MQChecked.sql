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
-- Description:	收费MQ申请审核
-- =============================================
ALTER PROCEDURE proc_SysApplyService_MQChecked
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@SubAccountNumber INT,--子账号数量
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT--1:成功 0:失败
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CompanyId CHAR(36)--申请人公司编号

	DECLARE @OperatorLimit INT
	SET @OperatorLimit=0

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @OperatorLimit=@SubAccountNumber
	END

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorID) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanySetting(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'OperatorLimit',@OperatorLimit)
			SET @errorCount=@errorCount+@@ERROR
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,1,CASE @OperatorLimit WHEN 0 THEN '0' ELSE '1' END )
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
			--更新审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
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
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanySetting(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'OperatorLimit',@OperatorLimit)
			SET @errorCount=@errorCount+@@ERROR
			--公司已经开通的收费项目处理
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=1
			SET @errorCount=@errorCount+@@ERROR	
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,1,CASE @OperatorLimit WHEN 0 THEN '0' ELSE '1' END )
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
			--更新审核信息
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--删除续费信息
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=1
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
-- Description:	收费MQ申请审核
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysApplyService_MQChecked]
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@SubAccountNumber INT,--子账号数量
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT--1:成功 0:失败
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CompanyId CHAR(36)--申请人公司编号
	DECLARE @IsEnabled CHAR(1)
	DECLARE @OperatorLimit INT
	SET @OperatorLimit=0
	SET @IsEnabled='0'

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @OperatorLimit=@SubAccountNumber
		SET @IsEnabled='1'
	END

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorID) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanySetting(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'OperatorLimit',@OperatorLimit)
			SET @errorCount=@errorCount+@@ERROR
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,1,@IsEnabled)
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
			--更新审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
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
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanySetting(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'OperatorLimit',@OperatorLimit)
			SET @errorCount=@errorCount+@@ERROR
			--公司已经开通的收费项目处理
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=1
			SET @errorCount=@errorCount+@@ERROR	
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,1, @IsEnabled)
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
			--更新审核信息
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=CAST(@SubAccountNumber AS NVARCHAR(250)),CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--删除续费信息
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			--子账号数量处理
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=1
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

