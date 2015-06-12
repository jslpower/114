GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-17
-- Description:	虚拟删除用户
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROC [dbo].[proc_CompanyUser_Remove] 
	@XMLRoot NVARCHAR(MAX)  --用户ID,sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;
	SET @ReturnValue = 0;	-- 1:操作成功   0:操作失败
	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	--开始事务
	BEGIN TRAN User_Remove	
	UPDATE tbl_CompanyUser SET IsDeleted='1' WHERE ID IN( 
SELECT UserId FROM OPENXML(@hdoc, N'/ROOT/User') WITH (UserId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN User_Remove;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END

	--执行成功
	EXEC sp_xml_removedocument @hdoc;
	SET @ReturnValue = 1;
	COMMIT TRAN User_Remove;
	RETURN @ReturnValue;
END
GO