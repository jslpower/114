GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-08
-- Description:	修改单位区域设置
-- =============================================
CREATE PROC [dbo].[proc_CompanyAreaSetting_Update]	
	@CompanyId CHAR(36),  --公司ID
	@XMLRoot NVARCHAR(MAX)  --sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;   --返回值等于0操作失败,大于0操作成功
	SET @ReturnValue = 0;
	DECLARE @error INT;
	SET @error = 0;
	BEGIN TRAN 
	--先删除所有数据	
	DELETE FROM tbl_CompanyAreaConfig WHERE CompanyId=@CompanyId;
	SET @error = @error + @@ERROR;

	--sqlxml脚本
	DECLARE @hdoc int;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;	
	--添加公司所有类型
	INSERT INTO [tbl_CompanyAreaConfig](
	[CompanyId],[AreaId],[PrefixText],[IsShowOrderSite]
	) SELECT @CompanyId,AreaId,PrefixText,IsShowOrderSite
	FROM OPENXML(@hdoc, N'/ROOT/CompanyAreaSetting') 
	  WITH (AreaId CHAR(36), PrefixText NVARCHAR(50), IsShowOrderSite CHAR(1));
	SET @error = @error + @@ERROR;
	--删除XML文件
	EXEC sp_xml_removedocument @hdoc; 

	IF @error <> 0 
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN
		RETURN @ReturnValue;
	END
	
	SET @ReturnValue = 1;	
	COMMIT TRAN 
	RETURN @ReturnValue;
END
GO