GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-24
-- Description:	修改单位设置
-- =============================================
CREATE PROC [dbo].[proc_CompanySetting_Update]	
	@CompanyId NVARCHAR(36),  --公司ID
	@TourStopTime NVARCHAR(250),  --自动停收时间
	@FirstMenu NVARCHAR(250),  --优先展示栏目(0:专线,1:组团)
	@OrderRefresh NVARCHAR(250)  --订单刷新时间(分钟)
AS
BEGIN
	DECLARE @ReturnValue INT;   --返回值等于0操作失败,大于0操作成功
	SET @ReturnValue = 0;
	DECLARE @error INT;
	SET @error = 0;
	BEGIN TRAN 
	--先删除
	DELETE FROM [tbl_CompanySetting] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('TourStopTime', 'FirstMenu', 'OrderRefresh');
	SET @error = @error + @@ERROR;
	INSERT INTO [tbl_CompanySetting]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TourStopTime',@TourStopTime);
	SET @error = @error + @@ERROR;
	INSERT INTO [tbl_CompanySetting]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'FirstMenu',@FirstMenu);
	SET @error = @error + @@ERROR;
	INSERT INTO [tbl_CompanySetting]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'OrderRefresh',@OrderRefresh);
	SET @error = @error + @@ERROR;

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