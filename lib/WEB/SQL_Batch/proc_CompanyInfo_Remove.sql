GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-16
-- Description:	虚拟删除公司
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROC [dbo].[proc_CompanyInfo_Remove] 
	@XMLRoot NVARCHAR(MAX)  --公司ID,sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;
	SET @ReturnValue = 0;	-- 1:操作成功   0:操作失败
	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	--开始事务
	BEGIN TRAN Company_Remove	
	UPDATE tbl_CompanyInfo SET IsDeleted='1' WHERE Id IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Remove;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	UPDATE tbl_CompanyUser SET IsDeleted='1' WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Remove;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	--执行成功
	EXEC sp_xml_removedocument @hdoc;
	SET @ReturnValue = 1;
	COMMIT TRAN Company_Remove;
	RETURN @ReturnValue;
END
GO