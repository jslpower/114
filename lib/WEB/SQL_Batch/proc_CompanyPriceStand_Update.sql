GO
--=====================================
--修改人： 张志瑜
--修改时间：2010-07-12
--描述： 设置公司价格等级
--======================================
CREATE PROC [dbo].[proc_CompanyPriceStand_Update]
(
	@CompanyID CHAR(36),   --公司ID
	@XMLRoot NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @ReturnValue INT;   --返回值等于0操作失败,大于0操作成功
	SET @ReturnValue = 0;
	DECLARE @error INT;
	SET @error = 0;
	BEGIN TRAN 	
	DECLARE @hdoc INT;
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot
	--首先删除tbl_CompanyPriceStand中的记录
	DELETE tbl_CompanyPriceStand WHERE companyID=@CompanyID and CommonPriceStandID 
	NOT IN(SELECT CommonPriceStandID FROM OPENXML (@hdoc,'/ROOT/CommPriceInfo') WITH(CommonPriceStandID char(36), PriceStandName nvarCHAR(20)));
	SET @error = @error + @@ERROR;

	--执行添加
	INSERT INTO tbl_CompanyPriceStand (ID,CommonPriceStandID,PriceStandName,CompanyID,IsSystem)
	SELECT ID,CommonPriceStandID,PriceStandName,@CompanyID,'1'
	FROM OPENXML (@hdoc,'/ROOT/CommPriceInfo')
	WITH(ID char(36), CommonPriceStandID char(36), PriceStandName nvarCHAR(20)) AS A
	WHERE NOT EXISTS(select 1 from tbl_CompanyPriceStand as B where B.CommonPriceStandID=A.CommonPriceStandID And B.CompanyID=@CompanyID);
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