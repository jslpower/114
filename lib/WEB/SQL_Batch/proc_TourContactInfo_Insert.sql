GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-08
-- Description:	新增团队联系人信息
-- =============================================
CREATE PROC [dbo].[proc_TourContactInfo_Insert]	
	@CompanyId CHAR(36),   --公司ID
	@ContactName NVARCHAR(100),
	@ContactTel NVARCHAR(50),
	@ContactQQ NVARCHAR(30),
	@ContactMQId NVARCHAR(20),
	@UserName NVARCHAR(100),
	@ReturnValue INT OUTPUT   --返回值等于0失败,大于0成功
AS
BEGIN		
	SET @ReturnValue = 0
	IF (SELECT COUNT(*) FROM tbl_TourContactInfo WHERE CompanyId=@CompanyId AND UserName=@UserName)=0
		INSERT INTO tbl_TourContactInfo(CompanyId,ContactName,ContactTel,ContactQQ,ContactMQId,UserName) VALUES(@CompanyId,@ContactName,@ContactTel,@ContactQQ,@ContactMQId,@UserName)
	ELSE
		UPDATE tbl_TourContactInfo SET ContactName=@ContactName,ContactTel=@ContactTel,ContactQQ=@ContactQQ
	IF @@ERROR<>0
		SET @ReturnValue = 0
	ELSE
		SET @ReturnValue = 1
END
GO