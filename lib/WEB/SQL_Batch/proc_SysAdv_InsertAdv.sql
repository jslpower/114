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
-- Description:	发布广告
-- =============================================
ALTER PROCEDURE proc_SysAdv_InsertAdv
	@PositionId INT,--位置编号
	@CategoryId TINYINT,--类别编号
	@Title NVARCHAR(250),--标题
	@Remark NVARCHAR(MAX),--内容
	@RedirectURL NVARCHAR(250),--链接地址
	@ImgPath NVARCHAR(250)=NULL,--图片路径
	@CompanyId CHAR(36),--购买单位编号
	@CompanyName NVARCHAR(250),--购买单位名称
	@ContactInfo NVARCHAR(250),--联系方式
	@StartDate DATETIME,--开始时间
	@EndDate DATETIME,--结束时间
	@OperatorId INT,--操作人编号
	@OperatorName NVARCHAR(50),--操作人名称
	@IssueTime DATETIME,--操作时间
	@Range TINYINT,--投放范围 0:单位类型 1:全国 2:省份 3:城市
	@Relation NVARCHAR(MAX)=NULL,--投放范围 XML:<ROOT><RelationInfo RelationId="[省份][城市][单位类型]编号"></ROOT> 投放范围为全国时无需设置 全省时关联省份编号 城市时关联城市编号 单位类型时关联单位类型
	@Result INT OUTPUT,--结果 0:失败 1:成功
	@AdvId INT OUTPUT--广告编号
AS
BEGIN
	--判断是否有效
	EXECUTE proc_SysAdv_IsValid @PositionId=@PositionId,@StartDate=@StartDate,@EndDate=@EndDate,@Range=@Range,@Relation=@Relation,@Result=@Result OUTPUT
	IF(@Result=0) RETURN @Result
	
	BEGIN TRANSACTION InsertAdv
	DECLARE @errorCount INT
	SET @errorCount=0

	SET @CompanyId=ISNULL(@CompanyId,'')

	--写入广告基本信息
	INSERT INTO tbl_SysAdv(AreaId,ClassId,AdvDescript,AdvRemark,AdvLink
		,CompanyId,CompanyName,ContactInfo,StartDate,EndDate
		,OperatorId,OperatorName,IssueTime,AdvArea)
	VALUES(@PositionId,@CategoryId,@Title,@Remark,@RedirectURL
		,@CompanyId,@CompanyName,@ContactInfo,@StartDate,@EndDate
		,@OperatorId,@OperatorName,@IssueTime,@Range)

	SET @AdvId=@@IDENTITY
	SET @errorCount=@errorCount+@@ERROR

	--广告图片信息
	IF(@ImgPath IS NOT NULL)
	BEGIN
		INSERT INTO tbl_SysAdvImg(AdvId,AdvImg) VALUES(@AdvId,@ImgPath)
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--广告关系
	DECLARE @DisplayType TINYINT--展现形式
	DECLARE @AdvType TINYINT--广告投放类型 1:城市 2:MQ
	SET @AdvType=1

	SELECT @DisplayType=ISNULL(DisplayType,-1) FROM tbl_SysAdvArea WHERE AreaId=@PositionId

	IF(@DisplayType=4)
	BEGIN
		SET @AdvType=2
	END
	ELSE
	BEGIN
		SET @AdvType=1
	END

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@Relation
	DECLARE @tmpRelation TABLE(RelationId INT)

	IF(@Range=1) --全国
	BEGIN
		INSERT INTO @tmpRelation(RelationId) SELECT Id FROM  tbl_SysCity
	END
	ELSE IF(@Range=2)--省份
	BEGIN
		INSERT INTO @tmpRelation(RelationId) SELECT ID FROM tbl_SysCity WHERE ProvinceId IN(SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT))
	END
	ELSE IF(@Range=3)--城市
	BEGIN
		INSERT INTO @tmpRelation(RelationId) SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)
	END
	ELSE--单位类型
	BEGIN
		INSERT INTO @tmpRelation(RelationId) SELECT RelationId FROM OPENXML(@hdoc,'/ROOT/RelationInfo') WITH(RelationId INT)
	END

	INSERT INTO tbl_SysAdvAreaControl(AdvId,AreaType,AreaId,SortId) SELECT @AdvId,@AdvType,A.RelationId,0 FROM @tmpRelation AS A
	SET @errorCount=@errorCount+@@ERROR
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION InsertAdv
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION InsertAdv
		SET @Result=1
	END

	RETURN @Result

END
GO
