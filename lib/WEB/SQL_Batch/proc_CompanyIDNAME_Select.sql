GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-12
-- Description:	查询单位ID,名称,企业形象图片
-- =============================================
CREATE PROCEDURE [dbo].[proc_CompanyIDNAME_Select] 
	@XMLRoot NVARCHAR(MAX),  --sqlxml脚本
	@TypeId INT   --1:查询公司ID,名称  2:查询公司ID,名称,企业LOGO
AS
BEGIN	
	DECLARE @hdoc int;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	IF(@TypeId=1)
		SELECT Id,CompanyName FROM tbl_CompanyInfo WHERE ID IN (SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36))
		);
	ELSE	
		SELECT c.Id,c.CompanyName,attach.FieldValue AS CompanyLogo FROM tbl_CompanyInfo AS c LEFT JOIN tbl_CompanyAttachInfo AS attach ON c.ID=attach.CompanyId AND attach.FieldName='CompanyLogo' WHERE c.ID IN (SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36)));
	EXEC sp_xml_removedocument @hdoc;
END
GO