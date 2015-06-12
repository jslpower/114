GO
--=====================================
--修改人： 张志瑜
--修改时间：2010-07-22
--描述： 判断指定公司,指定公共的价格等级ID有无在(团队/线路价格等级)使用
--======================================
CREATE PROC [dbo].[proc_CompanyPriceStand_Select_IsUsing]
(
	@CompanyID CHAR(36),   --公司ID
	@CommonPriceStandId NVARCHAR(36)
)
AS
BEGIN
	DECLARE @ReturnValue INT;   --返回值等于0数据未曾使用,1数据已在使用
	DECLARE @CompanyPriceStand NVARCHAR(36);  --公司价格等级ID
	DECLARE @error INT;
	DECLARE @count INT;
	SET @ReturnValue = 0;   --默认为0
	SET @CompanyPriceStand = '';
	SET @error = 0;
	SET @count = 0;
	--查询公司的报价等级ID
	SELECT @CompanyPriceStand=ID FROM tbl_CompanyPriceStand WHERE CommonPriceStandID=@CommonPriceStandId AND CompanyID=@CompanyID;
	IF(@CompanyPriceStand='' OR @CompanyPriceStand IS NULL)  --公司报价等级ID数据不存在,说明未选择过该公共价格等级
	BEGIN
		SET @ReturnValue = 0;	
		RETURN @ReturnValue;
	END
	--查询线路
	SELECT @count=COUNT(*) FROM tbl_RouteBasicInfo AS r,tbl_RouteBasicPriceDetail AS p WHERE r.CompanyID=@CompanyID AND r.ID=p.RouteBasicID AND p.CompanyPriceStandID=@CompanyPriceStand;
	IF(@count>0)
	BEGIN
		SET @ReturnValue = 1;	
		RETURN @ReturnValue;
	END
	--查询团队
	SELECT @count=COUNT(*) FROM tbl_TourList AS t,tbl_TourBasicPriceDetail AS p WHERE t.CompanyID=@CompanyID AND t.ID=p.TourBasicID AND p.CompanyPriceStandID=@CompanyPriceStand;
	IF(@count>0)
	BEGIN
		SET @ReturnValue = 1;	
		RETURN @ReturnValue;
	END
	RETURN @ReturnValue;
END
GO