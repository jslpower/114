GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-02
-- Description:	批量添加银行帐号
-- @ReturnValue  '1':操作成功   '0':操作失败
-- =============================================
CREATE PROCEDURE [dbo].[proc_BankAccount_InsertMore] 
	@XMLRoot nvarchar(max),  --sqlxml脚本
	@CompanyID  int,     --操作公司ID
	@ReturnValue char(1) output   --返回参数
AS
BEGIN
	IF len(@XMLRoot) <= 1
	BEGIN
		SET @ReturnValue = '0';
		RETURN
	END	
	
	IF @CompanyID <= 0
	BEGIN
		SET @ReturnValue = '0';
		RETURN
	END	

	/*
	<ROOT>
		<AddBankAccount AccountNumber='帐号' AccountType='帐号类型' BankAccountName='户名' BankName='开户行' />
	</ROOT>
	*/
	DECLARE @error INT;
	SET @error = 0;
	BEGIN TRAN 	
	DECLARE @hdoc int;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	INSERT INTO [tbl_BankAccount] ([CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId])
	SELECT @CompanyID,BankAccountName,AccountNumber,BankName,TypeId
	FROM OPENXML(@hdoc, N'/ROOT/AddBankAccount') 
	  WITH (BankAccountName NVARCHAR(100),AccountNumber NVARCHAR(250),[BankName] NVARCHAR(100),[TypeId] TINYINT) AS B;
	SET @error = @@ERROR;
	EXEC sp_xml_removedocument @hdoc;
	IF @error <> 0 
	BEGIN
		SET @ReturnValue = '0' 		
		ROLLBACK TRAN
		RETURN
	END	

	SET @ReturnValue = '1'
	COMMIT TRAN 
END
GO
