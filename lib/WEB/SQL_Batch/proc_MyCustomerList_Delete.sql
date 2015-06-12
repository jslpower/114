GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-14
-- Description:	批量取消我的客户
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROC [dbo].[proc_MyCustomerList_Delete] 
	@XMLRoot nvarchar(max)  --sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;
	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot
	DELETE FROM tbl_CompanyCustomerList WHERE ID IN (Select B.ID FROM OPENXML(@hdoc, N'/ROOT/CancelMoreMyCustomer') WITH (ID CHAR(36)) AS B);
	IF @@ERROR <> 0 
		SET @ReturnValue = 0;
	ELSE
		SET @ReturnValue = 1;
	EXEC sp_xml_removedocument @hdoc;
	RETURN @ReturnValue;
END
GO