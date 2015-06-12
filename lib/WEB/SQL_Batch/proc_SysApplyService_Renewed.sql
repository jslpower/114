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
-- Description:	高级服务续费
-- =============================================
CREATE PROCEDURE proc_SysApplyService_Renewed
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@RenewTime DATETIME,--续费时间
	@OperatorId INT,--续费人
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT--1:成功 0:失败 2:续费时间不对
AS
BEGIN	
	SET @Result=0
	DECLARE @ServiceType TINYINT--高级服务类型 1:收费MQ 2:高级网店
	DECLARE @CompanyId CHAR(36)--申请服务公司编号
	DECLARE @CEnableTime DATETIME--原服务起始时间
	DECLARE @CExpireTime DATETIME--原服务到期时间
	DECLARE @CheckText NVARCHAR(250)--审核通过信息
	SELECT @CompanyId=CompanyId,@ServiceType=ServiceType,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CheckText=CheckText FROM tbl_SysApplyService WHERE Id=@ApplyId

	IF ((@EnableTime BETWEEN @CEnableTime AND @CExpireTime) OR (@ExpireTime BETWEEN @CEnableTime AND @CExpireTime) OR (@ExpireTime<=GETDATE()) OR (@EnableTime>@ExpireTime))
	BEGIN
		--续费时间填写不正确
		SET @Result=2
		RETURN @Result
	END

	DECLARE @IsRenewNow CHAR(1)--是否立即更新
	SET @IsRenewNow='0'

	IF(GETDATE() BETWEEN @EnableTime AND @ExpireTime)
	BEGIN
		SET @IsRenewNow='1'
	END

	DECLARE @errorCount INT
	SET @errorCount=0

	BEGIN TRAN

	IF(@IsRenewNow='1')
	BEGIN
		UPDATE tbl_SysApplyService SET EnableTime=@EnableTime,ExpireTime=@ExpireTime WHERE Id=@ApplyId
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入续费信息
	INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
	SET @errorCount=@errorCount+@@ERROR

	IF(@IsRenewNow='1')
	BEGIN
		IF(@ServiceType=1)--收费MQ续费
		BEGIN			
			DELETE FROM tbl_CompanySetting WHERE CompanyId=@CompanyId AND FieldName='OperatorLimit'
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanySetting(CompanyId,FieldName,FieldValue) VALUES(@CompanyId,'OperatorLimit',@CheckText)
			SET @errorCount=@errorCount+@@ERROR
		END
		ELSE IF(@ServiceType=2)--高级网店续费
		BEGIN
			UPDATE [tbl_SysCompanyDomain] SET [IsDisabled]='0' WHERE CompanyId=@CompanyId AND Domain=@CheckText
			SET @errorCount=@errorCount+@@ERROR
		END
	END

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

	RETURN @Result
END
GO
