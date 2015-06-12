-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-14
-- Description:	判断广告是否有效
-- =============================================
ALTER PROCEDURE proc_SysAdv_IsValid
	@AdvId INT=0,--广告编号
	@PositionId INT,--位置编号
	@StartDate DATETIME,--开始时间
	@EndDate DATETIME,--结束时间
	@Range TINYINT,--投放范围 0:单位类型 1:全国 2:省份 3:城市
	@Relation NVARCHAR(MAX)=NULL,--投放范围 XML:<ROOT><RelationInfo RelationId="[省份][城市][单位类型]编号"></ROOT> 投放范围为全国时无需设置 全省时关联省份编号 城市时关联城市编号 单位类型时关联单位类型
	@Result INT OUTPUT--结果 1:有效 0:无效
AS
BEGIN
	DECLARE @CurrentCount INT --当前广告数量
	DECLARE @MaxCount INT --最大数量
	DECLARE @DisplayType TINYINT--表现形式
	DECLARE @AdvType TINYINT--投放类型
	DECLARE @hdoc INT
	
	SELECT @MaxCount=ISNULL(AdvCount,0),@DisplayType=ISNULL(DisplayType,-1) FROM tbl_SysAdvArea WHERE AreaId=@PositionId

	IF(@DisplayType=-1)
	BEGIN
		SET @Result=0
		RETURN @Result
	END

	IF(@DisplayType=4)
	BEGIN
		SET @AdvType=2
	END
	ELSE
	BEGIN
		SET @AdvType=1
	END

	IF(@Range=1)--全国
	BEGIN
		IF(@AdvId=0)
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) AND A.AreaId=@PositionId
		ELSE
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) AND Id<>@AdvId AND A.AreaId=@PositionId
	END
	ELSE IF(@Range=2)--全省
	BEGIN		
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@Relation
		--SELECT ID AS CityId INTO #TmpCity FROM tbl_SysCity WHERE ProvinceId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT))
		--EXECUTE sp_xml_removedocument @hdoc
		IF(@AdvId=0)
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT ID FROM tbl_SysCity WHERE ProvinceId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT))))
				AND A.AreaId=@PositionId
		ELSE
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT ID FROM tbl_SysCity WHERE ProvinceId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)))) AND A.Id<>@AdvId
				AND A.AreaId=@PositionId
		EXECUTE sp_xml_removedocument @hdoc
		--DROP TABLE #TmpCity
	END
	ELSE IF(@Range=3)--城市
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@Relation
		IF(@AdvId=0)
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)) )
				AND A.AreaId=@PositionId
		ELSE
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)) ) AND A.Id<>@AdvId
				AND A.AreaId=@PositionId
		EXECUTE sp_xml_removedocument @hdoc
	END
	ELSE--单位类型
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@Relation
		IF(@AdvId=0)
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)) )
				AND A.AreaId=@PositionId
		ELSE
			SELECT @CurrentCount=COUNT(*) FROM tbl_SysAdv AS A WHERE ( (@StartDate BETWEEN A.StartDate AND A.EndDate) OR (@EndDate BETWEEN A.StartDate AND A.EndDate) ) 
				AND EXISTS(SELECT 1 FROM tbl_SysAdvAreaControl AS B WHERE B.AreaType=@AdvType AND B.AdvId=A.Id AND B.AreaId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)) ) AND A.Id<>@AdvId
				AND A.AreaId=@PositionId
		EXECUTE sp_xml_removedocument @hdoc
	END

	IF(@CurrentCount<=@MaxCount AND @AdvId>0)
		SET @Result=1
    ELSE IF(@CurrentCount<@MaxCount AND @AdvId=0)
		SET @Result=1
	ELSE
		SET @Result=0

	RETURN @Result
END
GO

