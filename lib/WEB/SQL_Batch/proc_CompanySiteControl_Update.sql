GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-10
-- Description:	修改单位常用出港城市
-- =============================================
CREATE PROC [dbo].[proc_CompanySiteControl_Update]	
	@CompanyId CHAR(36),  --公司ID
	@XMLRoot NVARCHAR(MAX),  --sqlxml脚本
	@ReturnValue INT OUTPUT   --返回值  等于0失败,大于0成功
AS
BEGIN
	--先删除所有数据	
	DELETE FROM tbl_CompanySiteControl WHERE CompanyId=@CompanyId;
	--sqlxml脚本
	DECLARE @hdoc int;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;	
	--添加单位常用出港城市
	INSERT INTO tbl_CompanySiteControl(
	CompanyId,CityId
	) SELECT @CompanyId,CityId
	FROM OPENXML(@hdoc, N'/ROOT/CompanySiteControl') 
	  WITH (CityId INT);
	SET @ReturnValue = @@ROWCOUNT
	--删除XML文件
	EXEC sp_xml_removedocument @hdoc; 	
END
GO