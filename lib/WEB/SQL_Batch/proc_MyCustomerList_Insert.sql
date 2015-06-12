GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-01
-- Description:	批量设置我的客户
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROCEDURE [dbo].[proc_MyCustomerList_Insert] 
	@XMLRoot NVARCHAR(MAX),  --sqlxml脚本
	@OperatorID  CHAR(36)          --操作用户ID
AS
BEGIN
	DECLARE @CompanyID  CHAR(36);    --操作公司ID
	DECLARE @ReturnValue INT;  --返回值
	SET @CompanyID = '';
	SET @ReturnValue = 0;
	SELECT @CompanyID = CompanyId FROM tbl_CompanyUser WHERE id = @OperatorID;	
print @CompanyID
	IF @CompanyID IS NULL OR @CompanyID=''
	BEGIN
		RETURN @ReturnValue;
	END	

	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	INSERT INTO tbl_CompanyCustomerList ([ID],[CompanyID],[OperatorID],[CustomerCompanyID])
	SELECT B.ID,@CompanyID,@OperatorID,B.CustomerCompanyID
	FROM OPENXML(@hdoc, N'/ROOT/SetMoreMyCustomer') 
	  WITH (ID CHAR(36),CustomerCompanyID CHAR(36)) AS B WHERE NOT EXISTS (SELECT 1 FROM tbl_CompanyCustomerList AS M WHERE M.CompanyID = @CompanyID AND M.CustomerCompanyID = B.CustomerCompanyID); 

	IF @@ERROR <> 0 
		SET @ReturnValue = 0;
	ELSE
		SET @ReturnValue = 1;
	EXEC sp_xml_removedocument @hdoc;

	RETURN @ReturnValue;		
END
GO