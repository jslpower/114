GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-17
-- Description:	真实删除用户记录,并同时删除用户所有的关联表
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROC [dbo].[proc_CompanyUser_Delete] 
	@XMLRoot NVARCHAR(MAX)  --用户ID,sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;
	SET @ReturnValue = 0;	-- 1:操作成功   0:操作失败
	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	--开始事务
	BEGIN TRAN User_Delete
	DELETE im_member WHERE bs_uid IN( 
SELECT UserId FROM OPENXML(@hdoc, N'/ROOT/User') WITH (UserId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN User_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE tbl_CompanyUser WHERE ID IN( 
SELECT UserId FROM OPENXML(@hdoc, N'/ROOT/User') WITH (UserId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN User_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	--执行成功
	EXEC sp_xml_removedocument @hdoc;
	SET @ReturnValue = 1;
	COMMIT TRAN User_Delete;
	RETURN @ReturnValue;
END
GO