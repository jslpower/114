--城市urlRewrite(还要将254库中tbl_SysCity数据复制到服务器上)--
alter TABLE [dbo].[tbl_SysCity] add [RewriteCode] [varchar](50)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'urlrewriter名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tbl_SysCity', @level2type=N'COLUMN', @level2name=N'RewriteCode'

--客户资料管理权限--

INSERT [tbl_SysPermissionCategory] ([Id],[TypeId],[CategoryName],[SortId],[IsEnable]) VALUES ( 11,1,'客户资料管理',0,'1')
INSERT [tbl_SysPermissionClass] ([Id],[CategoryId],[ClassName],[SortId],[IsEnable]) VALUES ( 37,11,'客户资料管理',0,'1')
INSERT [tbl_SysPermissionClass] ([Id],[CategoryId],[ClassName],[SortId],[IsEnable]) VALUES ( 38,11,'系统维护',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 127,11,37,'管理该栏目',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 128,11,37,'新增',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 129,11,37,'修改',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 130,11,37,'删除',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 131,11,38,'管理该栏目',0,'1')

INSERT [tbl_SysPermissionCategory] ([Id],[TypeId],[CategoryName],[SortId],[IsEnable]) VALUES ( 12,1,'酒店管理',0,'1')
INSERT [tbl_SysPermissionClass] ([Id],[CategoryId],[ClassName],[SortId],[IsEnable]) VALUES ( 39,12,'订单管理',0,'1')
INSERT [tbl_SysPermissionClass] ([Id],[CategoryId],[ClassName],[SortId],[IsEnable]) VALUES ( 40,12,'团队订单管理',0,'1')
INSERT [tbl_SysPermissionClass] ([Id],[CategoryId],[ClassName],[SortId],[IsEnable]) VALUES ( 41,12,'首页板块数据管理',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 132,12,39,'管理该栏目',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 133,12,40,'管理该栏目',0,'1')
INSERT [tbl_SysPermissionList] ([Id],[CategoryId],[ClassId],[PermissionName],[SortId],[IsEnable]) VALUES ( 134,12,41,'管理该栏目',0,'1')

UPDATE [tbl_SystemUser]
   SET 
      [PermissionList] = PermissionList + ',127,128,129,130,131,132,133,134'
 WHERE [UserName]='admin'

--================
--MQ不在线提醒(不在机票更新范围内)
--================

--计划任务（删除启用记录）--

delete tbl_MsgTip_DisabledList
where datediff(dd,EnableTime,getdate())=0


--数据导出---

SELECT IssueTime as 短信发送时间
,Mobile as 手机号,
(SELECT CompanyName from tbl_CompanyInfo where ID=(select top 1 companyid from tbl_companyuser where mq=l.tomqid)) as 公司名称,
(select top 1 username from tbl_companyuser where mq=l.tomqid) as 用户名,
(select top 1 contactname from tbl_companyuser where mq=l.tomqid) as 姓名,
(select count(*)  from tbl_LogUserLogin where OperatorId =
(select top 1 id from tbl_companyuser where mq=l.tomqid)) as 登录次数,
(select top 1 eventtime from tbl_LogUserLogin where OperatorId =
(select top 1 id from tbl_companyuser where mq=l.tomqid) 
and Eventtime>l.issuetime order by Eventtime) as 发送后第一次登录时间

 FROM tbl_MsgTipList l
 
 
-- =============================================
-- Author:luofx
-- Create date: 2010-12-3
-- Description:批量添加酒店系统首页板块信息	
-- =============================================
CREATE PROCEDURE [dbo].[proc_HotelLocalInfo_Insert]
	@LocalHotelInfoXML NVARCHAR(MAX), -- 酒店信息XML字符串:<ROOT><LocalHotelInfo CityCode="" HotelCode="" HotelImg="" HotelName="" IsTop="" MarketingPrice="" Rank=""  ShortDesc="" ShowType="" SortId=""  /></ROOT>
	@Result int output -- 返回参数	
AS
BEGIN
	SET @Result=0
	DECLARE @ErrorCount int --验证错误
	DECLARE @hdoc int       --XML使用参数
	DECLARE @MAXID int      --暂存最大id
	DECLARE @i CHAR(36)		--计数
	DECLARE @HotelCode VARCHAR(50)  --酒店代号
	SET @ErrorCount=0
    SET @i=1
	--使用表暂存XML信息
	CREATE  Table #tmpLocalHotelInfoTbl(
		Vid INT ,                   --标识ID
		CityCode varchar(5),       --城市三字码
		HotelCode varchar(50),		--酒店代号
		HotelImg nvarchar(250),		--酒店图片路径
		HotelName nvarchar(250),	--酒店名称
		IsTop char(1),				--是否置顶
		MarketingPrice money,		--促销价
		Rank tinyint,				--星级
		ShortDesc nvarchar(250),	--酒店介绍
		ShowType tinyint,			--显示类型
		SortId int					--排序
	)    
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @LocalHotelInfoXML	
		INSERT INTO #tmpLocalHotelInfoTbl([Vid],[CityCode],[HotelCode],[HotelImg],[HotelName],[IsTop],[MarketingPrice],[Rank],[ShortDesc],[ShowType],[SortId])
				SELECT row_number() OVER(ORDER BY [CityCode] DESC) AS [row_id],[CityCode],[HotelCode],
						[HotelImg],[HotelName],[IsTop],[MarketingPrice],[Rank],[ShortDesc],[ShowType],[SortId]
					FROM OPENXML(@hdoc,N'/ROOT/LocalHotelInfo') 
						 WITH([ID] INT,[CityCode] VARCHAR(5),[HotelCode] VARCHAR(50)
							,[HotelImg] NVARCHAR(250),[HotelName] NVARCHAR(250),
							[IsTop] CHAR(1),[MarketingPrice] money,[Rank] tinyint,[ShortDesc] nvarchar(250),
							[ShowType] tinyint,[SortId] INT)																
	EXEC sp_xml_removedocument @hdoc 
    SELECT @MAXID=MAX(Vid) FROM #tmpLocalHotelInfoTbl
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	BEGIN TRANSACTION LocalHotelInfoListInsert		
			WHILE(@i<=@MAXID )
				BEGIN
					SELECT @HotelCode=[HotelCode] FROM #tmpLocalHotelInfoTbl WHERE [Vid]=@i    
					--写入酒店信息
					if NOT EXISTS(SELECT 1 FROM [tbl_HotelLocalInfo] WHERE [HotelCode]=@HotelCode)  
						BEGIN
							INSERT INTO [tbl_HotelLocalInfo] ([id],[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice]
											   ,[CityCode],[HotelImg],[ShowType],[SortId],[IsTop],[IssueTime])
								  SELECT newid(),[HotelCode],[HotelName],[ShortDesc],[Rank],[MarketingPrice]
										   ,[CityCode],[HotelImg],[ShowType],[SortId],[IsTop],getdate() 
										FROM #tmpLocalHotelInfoTbl WHERE [Vid]=@i 
							SET @Result=@Result+1
						END	
					SET @i=@i+1			
				END		
		IF @ErrorCount > 0 
			BEGIN
				SET @Result=0
				ROLLBACK TRANSACTION LocalHotelInfoListInsert 
			END	
	COMMIT TRANSACTION LocalHotelInfoListInsert
	DROP TABLE #tmpLocalHotelInfoTbl	
	RETURN @Result
END


-- =============================================
-- Author:luofx
-- Create date: 2010-10-28
-- Description:	批量添加常旅客,	
-- =============================================
ALTER PROCEDURE [dbo].[proc_TicketVistorInfo_Insert]
	@VistorInfoXML NVARCHAR(MAX), -- 常旅客信息XML字符串:<ROOT><TicketVistorInfo CardNo="" CardType=""  ChinaName=""  CompanyId=""  ContactSex=""  ContactTel=""  EnglishName=""  NationCode=""  Remark=""  VistorType="" /></ROOT>
	@Result int output -- 返回参数	
AS
BEGIN
	SET @Result=0
	DECLARE @ErrorCount int --验证错误
	DECLARE @hdoc int       --XML使用参数
	DECLARE @MAXID int      --暂存最大id
	DECLARE @i CHAR(36)		--计数
	DECLARE @CardNo NVARCHAR(50) --证件号
	DECLARE @CompanyId CHAR(36)  --公司编号
	--DECLARE @ChinaName NVARCHAR(50)  --中文名称
	--DECLARE @EnglishName  NVARCHAR(50) --英文名称
	SET @ErrorCount=0
    SET @i=1
	--使用表变量暂存XML信息
	CREATE  Table #tmpVistorTbl(
		Vid INT ,                   --标识ID
		VistorGuid CHAR(36),        --旅客的GUID,存入数据库的主键
		CompanyId CHAR(36),			--公司id
		CardType INT,			--证件编号
		CardNo NVARCHAR(50),		--证件类型
		ChinaName NVARCHAR(50),		--中文名字
		EnglishName NVARCHAR(50),	--英文名字
		ContactSex CHAR(1),			--性别
		ContactTel NVARCHAR(100),	--联系电话
		Remark NVARCHAR(500),		--备注
		NationCode NVARCHAR(50),	--国籍代号
		VistorType TINYINT			--旅客类型
	)    
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @VistorInfoXML	
		INSERT INTO #tmpVistorTbl([Vid],[VistorGuid],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType])
				SELECT row_number() OVER(ORDER BY CompanyId DESC) AS [row_id],[Id],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType]
					FROM OPENXML(@hdoc,N'/ROOT/TicketVistorInfo') WITH([ID] CHAR(36),[CompanyId] CHAR(36),[CardType] INT,[CardNo] NVARCHAR(50),[ChinaName] NVARCHAR(50),
						[EnglishName] NVARCHAR(50),[ContactSex] CHAR(1),[ContactTel] NVARCHAR(100),[Remark] NVARCHAR(500),[NationCode] NVARCHAR(50),[VistorType] TINYINT)																
	EXEC sp_xml_removedocument @hdoc 
    SELECT @MAXID=MAX(Vid) FROM #tmpVistorTbl
	--验证错误
	SET @ErrorCount = @ErrorCount + @@ERROR
	BEGIN TRANSACTION VistorListInsert		
			WHILE(@i<=@MAXID )
				BEGIN
					SELECT @CardNo=[CardNo],@CompanyId=[CompanyId] FROM #tmpVistorTbl WHERE [Vid]=@i    --,@ChinaName=[ChinaName],@EnglishName=[EnglishName]
					--写入常旅客信息
					if NOT EXISTS(SELECT 1 FROM [tbl_TicketVistorInfo] WHERE [CompanyId]=@CompanyId AND [CardNo]=@CardNo)  --AND [EnglishName]=@EnglishName AND [ChinaName]=@ChinaName
						BEGIN
							INSERT INTO [tbl_TicketVistorInfo] ([ID],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType]) SELECT [VistorGuid],[CompanyId],[CardType],[CardNo],[ChinaName],[EnglishName],[ContactSex],[ContactTel],[Remark],[NationCode],[VistorType] FROM #tmpVistorTbl WHERE [Vid]=@i
							SET @Result=@Result+1
						END	
					SET @i=@i+1			
				END		
		IF @ErrorCount > 0 
			BEGIN
				SET @Result=0
				ROLLBACK TRANSACTION VistorListInsert 
			END	
	COMMIT TRANSACTION VistorListInsert
	DROP TABLE #tmpVistorTbl	
	RETURN @Result
END
GO
-- =============================================
-- Author:luofx
-- Create date: 2010-10-29
-- Description: 机票(酒店)系统-常旅客信息列表
-- update:增加了常旅客数据来源(DataType)，方便区分查询
-- =============================================
ALTER VIEW [dbo].[view_TicketVistorInfo]
AS
SELECT     dbo.tbl_TicketVistorInfo.ID, dbo.tbl_TicketVistorInfo.CompanyId, dbo.tbl_TicketVistorInfo.CardType, dbo.tbl_TicketVistorInfo.CardNo, 
                      dbo.tbl_TicketVistorInfo.ChinaName, dbo.tbl_TicketVistorInfo.EnglishName, dbo.tbl_TicketVistorInfo.ContactSex, dbo.tbl_TicketVistorInfo.ContactTel, 
                      dbo.tbl_TicketVistorInfo.Remark, dbo.tbl_TicketVistorInfo.VistorType, dbo.tbl_TicketVistorInfo.IssueTime, dbo.tbl_TicketNationInfo.Id AS NationId, 
                      dbo.tbl_TicketNationInfo.CountryCode, dbo.tbl_TicketNationInfo.CountryName, dbo.tbl_TicketVistorInfo.DataType
FROM         dbo.tbl_TicketVistorInfo LEFT OUTER JOIN
                      dbo.tbl_TicketNationInfo ON dbo.tbl_TicketVistorInfo.NationCode = dbo.tbl_TicketNationInfo.Id
GO

-- =============================================
---- Author:luofx
-- Update date: 2010-12-13
-- 修改表【tbl_TicketVistorInfo】的字段（CardType）tinyint修改为int
-- =============================================
ALTER TABLE tbl_TicketVistorInfo
ALTER COLUMN CardType int



GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-23
-- Description:	高级网店申请审核
-- History:
-- 1.2010-12-13 汪奇志 增加模板路径,审核通过后默认写入tbl_HighShopCompanyInfo
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysApplyService_EshopChecked]
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@DomainName NVARCHAR(250),--通过的域名
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT,--1:成功 0:失败 2:域名重复
	@TemplatePath NVARCHAR(255)=''--网店模板路径
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CDomainName NVARCHAR(255)--原审核通过域名
	DECLARE @CompanyId CHAR(36)--申请人公司编号

	DECLARE @CompanyType TINYINT--公司类型
	DECLARE @CompanyName NVARCHAR(250)--公司名称
	DECLARE @IsDisabled CHAR(1)--域名是否禁用
	DECLARE @DomainType TINYINT--域名类型
	SET @IsDisabled='1'
	SET @DomainType=1

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @IsDisabled='0'
	END

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CDomainName=CheckText,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId
	SELECT @CompanyType=CompanyType,@CompanyName=CompanyName FROM tbl_CompanyInfo WHERE Id=@CompanyId	

	IF(@CompanyType<>1)
	BEGIN
		SELECT TOP (1) @CompanyType=TypeId FROM tbl_CompanyTypeList WHERE CompanyId=@CompanyId
	END

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--写入域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)	
			BEGIN
				INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
				VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,@TemplatePath,GETDATE(),@IsDisabled)
				SET @errorCount=@errorCount+@@ERROR	
			END
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END )
			SET @errorCount=@errorCount+@@ERROR	
			--写入HighShopCompanyInfo
			IF NOT EXISTS(SELECT 1 FROM tbl_HighShopCompanyInfo WHERE CompanyID=@CompanyId)
			BEGIN
				INSERT INTO tbl_HighShopCompanyInfo(CompanyID,TemplateId) VALUES(@CompanyId,1)
			END

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND @DomainName<>@CDomainName AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--更新续费信息
			IF EXISTS(SELECT 1 FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime)
			BEGIN
				UPDATE  tbl_SysApplyServiceFee SET EnableTime=@EnableTime,ExpireTime=@ExpireTime WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
				SET @errorCount=@errorCount+@@ERROR
			END
			ELSE
			BEGIN
				INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
				SET @errorCount=@errorCount+@@ERROR
			END
			--域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)
			BEGIN
				IF EXISTS(SELECT 1 FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName)
				BEGIN
					UPDATE [tbl_SysCompanyDomain] SET [Domain]=@DomainName,[IsDisabled]=@IsDisabled,GoToUrl=@TemplatePath WHERE CompanyId=@CompanyId AND Domain=@CDomainName
					SET @errorCount=@errorCount+@@ERROR
				END
				ELSE
				BEGIN				
					INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
					VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,@TemplatePath,GETDATE(),@IsDisabled)
					SET @errorCount=@errorCount+@@ERROR
				END
			END
			ELSE
			BEGIN
				DELETE FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName
			END
			--公司已经开通的收费项目
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END)
			SET @errorCount=@errorCount+@@ERROR	
			--写入HighShopCompanyInfo
			IF NOT EXISTS(SELECT 1 FROM tbl_HighShopCompanyInfo WHERE CompanyID=@CompanyId)
			BEGIN
				INSERT INTO tbl_HighShopCompanyInfo(CompanyID,TemplateId) VALUES(@CompanyId,1)
			END

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			BEGIN TRAN
			--审核信息处理
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--续费信息处理
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			SET @errorCount=@errorCount+@@ERROR
			--域名信息处理
			UPDATE [tbl_SysCompanyDomain] SET [IsDisabled]='1' WHERE CompanyId=@CompanyId AND [Domain]=@CDomainName
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END	
	END

	RETURN @Result	
END
GO


--------URLREWRITER代码------------

ALTER table [tbl_SysCity] add [RewriteCode] [varchar](50)

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'urlrewriter名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'tbl_SysCity', @level2type=N'COLUMN', @level2name=N'RewriteCode'

GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-23
-- Description:	高级网店申请审核
-- History:
-- 1.2010-12-13 汪奇志 增加模板路径,审核通过后默认写入tbl_HighShopCompanyInfo
-- 2.2010-12-15 汪奇志 增加google map key配置
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysApplyService_EshopChecked]
	@ApplyId CHAR(36),--申请编号
	@EnableTime DATETIME,--启用时间
	@ExpireTime DATETIME,--到期时间
	@ApplyState TINYINT,--审核状态 1:通过 2:不通过
	@CheckTime DATETIME,--审核时间
	@OperatorId INT,--审核人
	@DomainName NVARCHAR(250),--通过的域名
	@RenewalId CHAR(36),--续费编号
	@Result INT OUTPUT,--1:成功 0:失败 2:域名重复
	@TemplatePath NVARCHAR(255)='',--网店模板路径
	@GoogleMapKey VARCHAR(200)=''--google map key
AS
BEGIN	
	DECLARE @CEnableTime DATETIME--原启用时间
	DECLARE @CExpireTime DATETIME--原到期时间
	DECLARE @CApplyState TINYINT--原审核状态
	DECLARE @CDomainName NVARCHAR(255)--原审核通过域名
	DECLARE @CompanyId CHAR(36)--申请人公司编号

	DECLARE @CompanyType TINYINT--公司类型
	DECLARE @CompanyName NVARCHAR(250)--公司名称
	DECLARE @IsDisabled CHAR(1)--域名是否禁用
	DECLARE @DomainType TINYINT--域名类型
	SET @IsDisabled='1'
	SET @DomainType=1

	IF(GETDATE()  BETWEEN @EnableTime AND @ExpireTime )	
	BEGIN
		SET @IsDisabled='0'
	END

	SELECT @CApplyState=ApplyState,@CEnableTime=EnableTime,@CExpireTime=ExpireTime,@CDomainName=CheckText,@CompanyId=CompanyId FROM tbl_SysApplyService WHERE Id=@ApplyId
	SELECT @CompanyType=CompanyType,@CompanyName=CompanyName FROM tbl_CompanyInfo WHERE Id=@CompanyId	

	IF(@CompanyType<>1)
	BEGIN
		SELECT TOP (1) @CompanyType=TypeId FROM tbl_CompanyTypeList WHERE CompanyId=@CompanyId
	END

	DECLARE @errorCount INT	
	SET @errorCount=0

	IF(@CApplyState=0)--审核操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--写入续费信息
			INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
			SET @errorCount=@errorCount+@@ERROR
			--写入域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)	
			BEGIN
				INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
				VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,@TemplatePath,GETDATE(),@IsDisabled)
				SET @errorCount=@errorCount+@@ERROR	
			END
			--写入公司已经开通的收费项目
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END )
			SET @errorCount=@errorCount+@@ERROR	
			--写入HighShopCompanyInfo
			IF NOT EXISTS(SELECT 1 FROM tbl_HighShopCompanyInfo WHERE CompanyID=@CompanyId)
			BEGIN
				INSERT INTO tbl_HighShopCompanyInfo(CompanyID,TemplateId,GoogleMapKey) VALUES(@CompanyId,1,@GoogleMapKey)
				SET @errorCount=@errorCount+@@ERROR	
			END
			ELSE
			BEGIN
				UPDATE tbl_HighShopCompanyInfo SET GoogleMapKey=@GoogleMapKey WHERE CompanyId=@CompanyId
				SET @errorCount=@errorCount+@@ERROR	
			END

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @Result=1
		END	
	END
	ELSE--审核修改操作
	BEGIN
		IF(@ApplyState=1)--通过审核
		BEGIN			
			IF (@DomainName<>'' AND @DomainName IS NOT NULL AND @DomainName<>@CDomainName AND EXISTS(SELECT 1 FROM tbl_SysCompanyDomain WHERE Domain=@DomainName))--域名重复判断
			BEGIN
				SET @Result=2
				RETURN @Result
			END

			BEGIN TRAN
			--写入审核信息
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--更新续费信息
			IF EXISTS(SELECT 1 FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime)
			BEGIN
				UPDATE  tbl_SysApplyServiceFee SET EnableTime=@EnableTime,ExpireTime=@ExpireTime WHERE CompanyId=@CompanyId AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
				SET @errorCount=@errorCount+@@ERROR
			END
			ELSE
			BEGIN
				INSERT INTO tbl_SysApplyServiceFee(Id,CompanyID,ApplyServiceId,EnableTime,ExpireTime,OperatorId) VALUES(@RenewalId,@CompanyId,@ApplyId,@EnableTime,@ExpireTime,@OperatorId)
				SET @errorCount=@errorCount+@@ERROR
			END
			--域名信息
			IF(@DomainName<>'' AND @DomainName IS NOT NULL)
			BEGIN
				IF EXISTS(SELECT 1 FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName)
				BEGIN
					UPDATE [tbl_SysCompanyDomain] SET [Domain]=@DomainName,[IsDisabled]=@IsDisabled,GoToUrl=@TemplatePath WHERE CompanyId=@CompanyId AND Domain=@CDomainName
					SET @errorCount=@errorCount+@@ERROR
				END
				ELSE
				BEGIN				
					INSERT INTO [tbl_SysCompanyDomain] ([CompanyId],[CompanyType],[CompanyName],[Domain],[DomainType],[GoToUrl],[IssueTime],[IsDisabled])
					VALUES(@CompanyId,@CompanyType,@CompanyName,@DomainName,@DomainType,@TemplatePath,GETDATE(),@IsDisabled)
					SET @errorCount=@errorCount+@@ERROR
				END
			END
			ELSE
			BEGIN
				DELETE FROM [tbl_SysCompanyDomain] WHERE CompanyId=@CompanyId AND Domain=@CDomainName
			END
			--公司已经开通的收费项目
			DELETE FROM tbl_CompanyPayService WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR
			INSERT INTO tbl_CompanyPayService(CompanyId,ServiceId,IsEnabled) VALUES(@CompanyId,2,CASE @IsDisabled WHEN '0' THEN '1' ELSE '0' END)
			SET @errorCount=@errorCount+@@ERROR	
			--写入HighShopCompanyInfo
			IF NOT EXISTS(SELECT 1 FROM tbl_HighShopCompanyInfo WHERE CompanyID=@CompanyId)
			BEGIN
				INSERT INTO tbl_HighShopCompanyInfo(CompanyID,TemplateId,GoogleMapKey) VALUES(@CompanyId,1,@GoogleMapKey)
				SET @errorCount=@errorCount+@@ERROR	
			END
			ELSE
			BEGIN
				UPDATE tbl_HighShopCompanyInfo SET GoogleMapKey=@GoogleMapKey WHERE CompanyId=@CompanyId
				SET @errorCount=@errorCount+@@ERROR	
			END

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END
		ELSE
		BEGIN
			BEGIN TRAN
			--审核信息处理
			--UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckTime=@CheckTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			UPDATE tbl_SysApplyService SET ApplyState=@ApplyState,CheckText=@DomainName,CheckTime=@CheckTime,EnableTime=@EnableTime,ExpireTime=@ExpireTime,OperatorId=@OperatorId WHERE Id=@ApplyId
			SET @errorCount=@errorCount+@@ERROR
			--续费信息处理
			DELETE FROM tbl_SysApplyServiceFee WHERE CompanyId=@CompanyId  AND ApplyServiceId=@ApplyId AND EnableTime=@CEnableTime AND ExpireTime=@CExpireTime
			SET @errorCount=@errorCount+@@ERROR
			--域名信息处理
			UPDATE [tbl_SysCompanyDomain] SET [IsDisabled]='1' WHERE CompanyId=@CompanyId AND [Domain]=@CDomainName
			SET @errorCount=@errorCount+@@ERROR
			--已开通服务处理
			UPDATE tbl_CompanyPayService SET IsEnabled='0' WHERE CompanyId=@CompanyId AND ServiceId=2
			SET @errorCount=@errorCount+@@ERROR

			--提交事务
			IF(@errorCount>0)
			BEGIN
				ROLLBACK TRAN
				SET @Result=0
			END
			ELSE
			BEGIN		
				COMMIT TRAN
				SET @Result=1
			END
		END	
	END

	RETURN @Result	
END
GO


GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-03-26
-- Description: 短信平台-客户列表视图
-- history:zhangzy   2010-11-12  UNION ALL 平台的组团社数据
-- history:zhangzy   2010-12-07  增加字段ProvinceId,CityId,ProvinceName,CityName
-- history:lihc   2010-12-16     UNION ALL 平台的数据，增加表tbl_PlatformPhone
-- =============================================
ALTER VIEW [dbo].[view_SMS_Customers]
AS
SELECT A.ID as ID
	, A.CompanyID
	, A.UserID
	, A.CustomerCompanyName
	, A.CustomerContactName
	, A.ClassID
	, A.ReMark
	, A.MobileNumber
	, A.IssueTime
	,'' AS ClassName
	, '0' as TongYCompanyId
	, A.ProvinceId
	, A.CityId
	, (SELECT ProvinceName FROM tbl_SysProvince WHERE ID=A.ProvinceId) AS ProvinceName
	, (SELECT CityName FROM tbl_SysCity WHERE ID=A.CityId) AS CityName
	--,'' AS ProvinceName
	--,'' AS CityName
FROM SMS_CustomerList AS A 
UNION ALL
SELECT '0' AS ID
	,'0' AS CompanyID
	,'0' AS UserID
	,c.CompanyName AS CustomerCompanyName
	,c.ContactName AS CustomerContactName
	,2 AS ClassID
	,'' AS ReMark
	,c.ContactMobile AS MobileNumber
	,c.CheckTime AS IssueTime
	,'平台共享客户' AS ClassName
	,c.Id as TongYCompanyId
	,c.ProvinceId
	,c.CityId
	,(SELECT ProvinceName FROM tbl_SysProvince WHERE ID=c.ProvinceId) AS ProvinceName
	,(SELECT CityName FROM tbl_SysCity WHERE ID=c.CityId) AS CityName
	--,'' AS ProvinceName
	--,'' AS CityName
 FROM tbl_CompanyInfo AS c where c.IsCheck='1' AND c.IsDeleted='0' AND c.IsEnabled='1' AND c.CompanyType=1 AND c.ContactMobile<>'' AND LEN(c.ContactMobile)=11 AND LEFT(c.ContactMobile, 1)='1'
UNION ALL
SELECT '0' AS ID
	,'0' AS CompanyID
	,'0' AS UserID
	,d.CompanyName AS CustomerCompanyName
	,d.Contacter AS CustomerContactName
	,2 AS ClassID
	,'' AS ReMark
	,d.Mobile AS MobileNumber
	,'' AS IssueTime
	,'平台共享客户' AS ClassName
	,d.Id as TongYCompanyId
	,d.ProvinceId
	,d.CityId
	,ProvinceName
	,CityName
 FROM tbl_PlatformPhone AS d
GO

go
UPDATE SMS_CustomerList SET ClassID=2
go



GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-21
-- Description:	是否最近出团、最近出团数量处理
-- History:
-- 1.2010-12-30 汪奇志 将模板团(模板团下所有团队均已出团)下最后出团的团队[IsRecentLeave]标记为1
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_RecentLeave]
	@TourId CHAR(36),--团队编号
	@IsParentTour CHAR(1)='0'--是否是模板团
AS
BEGIN	
	IF(@TourId IS NULL OR @TourId='') 
		RETURN 0

	IF(@IsParentTour='0')
		SELECT @TourId=ParentTourId FROM tbl_TourList WHERE Id=@TourId

	--注：以下@TourId 为模板团编号	

	DECLARE @RecentLeaveCount INT --最近出团数量
	SELECT @RecentLeaveCount=COUNT(*) FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE()
	UPDATE [tbl_TourList] SET [IsRecentLeave]='0',[RecentLeaveCount]=0,[IsRecentLeaveNoControl]='0' WHERE ParentTourId=@TourId
	--受团队状态控制的最近出团
	UPDATE [tbl_TourList] SET [IsRecentLeave]='1',[RecentLeaveCount]=@RecentLeaveCount WHERE Id=(	SELECT TOP 1 [Id] FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE() AND TourState=1 ORDER BY [LeaveDate] ASC)
	--不受团队状态控制的最近出团
	UPDATE [tbl_TourList] SET [IsRecentLeaveNoControl]='1',[RecentLeaveCount]=@RecentLeaveCount WHERE Id=(	SELECT TOP 1 [Id] FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE() ORDER BY [LeaveDate] ASC)

	--将模板团(模板团下所有团队均已出团)下最后出团的团队[IsRecentLeave]标记为1
	DECLARE @MaxLeaveDate DATETIME--模板团下最后出团的出团日期
	DECLARE @MaxLeaveDateTourId CHAR(36)--模板团下最后出团的团队编号
	SELECT TOP 1 @MaxLeaveDate=LeaveDate,@MaxLeaveDateTourId=Id FROM tbl_TourList WHERE ParentTourid=@TourId Order By LeaveDate DESC
	IF(@MaxLeaveDate<=GETDATE())
	BEGIN
		UPDATE [tbl_TourList] SET [IsRecentLeave]='1' WHERE Id=@MaxLeaveDateTourId
	END
	
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-26
-- Description:	更新团队信息(单个子团)
-- 1.2011-01-25 汪奇志 自动停收改为正常
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_UpdateTourInfo]
	@TourId CHAR(36),--团队编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT,--行程是否异常(百分制)
	@TourPriceError INT,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(MAX)=NULL,--温馨提醒
	@LeaveCity INT,--出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN
	SET @Result=0
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号

	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc

	BEGIN TRANSACTION UpdateStandardTourInfo
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新子团信息 订单人数(除留位过期、不受理的订单)>计划人数时 计划人数=订单人数
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays,[ComeBackDate]=DATEADD(DAY,@TourDays-1,[LeaveDate])
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]= CASE WHEN OrderPeopleNumber>@PlanPeopleCount THEN OrderPeopleNumber ELSE @PlanPeopleCount END
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[CreateTime]=GETDATE()
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id=@TourId

	--写入行程信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID]=@TourId
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT @TourId,A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(MAX),Vehicle NVARCHAR(MAX),TrafficNumber NVARCHAR(MAX),House NVARCHAR(MAX),Dinner NVARCHAR(MAX),PlanContent NVARCHAR(MAX),PlanDay INT,PlanId CHAR(36)) AS A
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR	
	END
	ELSE--快速发布
	BEGIN
		DELETE FROM [tbl_TourFastPlan] WHERE [TourBasicID]=@TourId
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) VALUES(@TourId,@QuickPlan)
	END

	--写入报价信息 先删除再新增
	DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID]=@TourId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @TourId,A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36), CustomerLevelId INT,RowMark TINYINT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--写入服务标准信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID]=@TourId
		INSERT INTO [tbl_TourServiceStandard]
			([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		VALUES(
			 @TourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice)
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--写入出港城市信息 先删除再新增
	DELETE FROM [tbl_TourAreaControl] WHERE [TourId]=@TourId
	INSERT INTO tbl_TourAreaControl(TourId,CityId) VALUES(@TourId,@LeaveCity)

	--写入销售城市信息 先删除再新增
	DELETE FROM [tbl_TourCityControl] WHERE [TourId]=@TourId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT @TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--写入线路主题信息 先删除再新增
	DELETE FROM [tbl_TourThemeControl] WHERE [TourId]=@TourId
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT @TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] =@TourId
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT @TourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队自动停收状态 非手动停收手动客满→自动停收
	UPDATE [tbl_TourList] SET [TourState]=3 WHERE Id=@TourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE() AND TourState NOT IN(0,2)
	SET @errorCount=@errorCount+@@ERROR
    --标记团队正常状态 自动停收→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE Id=@TourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])>GETDATE() AND TourState IN(3)
	SET @errorCount=@errorCount+@@ERROR
	
	--标记团队状态 自动客满→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE Id=@TourId AND [PlanPeopleCount]>[OrderPeopleNumber] AND TourState=4
	SET @errorCount=@errorCount+@@ERROR
	--标记团队状态 正常→自动客满
	UPDATE [tbl_TourList] SET [TourState]=4 WHERE Id=@TourId AND [PlanPeopleCount]=[OrderPeopleNumber] AND TourState=1
	SET @errorCount=@errorCount+@@ERROR

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TourId,@IsParentTour=N'0'

	--更新团队订单的线路区域信息
	UPDATE tbl_TourOrder SET AreaId=@AreaId WHERE TourId=@TourId
	
	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-20
-- Description:	修改团队信息(模板团)
-- 1.2011-01-25 汪奇志 自动停收改为正常
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_UpdateTemplateTourInfo]
	@TemplateTourId CHAR(36),--模板团编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@TourType TINYINT,--团队类型
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT=0,--行程是否异常(百分制)
	@TourPriceError INT=0,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(MAX)=NULL,--温馨提醒
	@LeaveCity INT,--出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN	
	SET @Result=0
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号
	
	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc
	
	BEGIN TRANSACTION UpdateStandardTourInfo
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新模板团信息
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]=@PlanPeopleCount
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id=@TemplateTourId	

	--表变量存放此次要更新的子团信息
	DECLARE @tmptbl TABLE(TourId CHAR(36))
	INSERT INTO @tmptbl(TourId) SELECT Id FROM tbl_TourList WHERE ParentTourID=@TemplateTourId AND LeaveDate>=GETDATE()	

	--更新子团信息 订单人数(除留位过期、不受理的订单)>计划人数时 计划人数=订单人数
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays,[ComeBackDate]=DATEADD(DAY,@TourDays-1,[LeaveDate])
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]= CASE WHEN OrderPeopleNumber>@PlanPeopleCount THEN OrderPeopleNumber ELSE @PlanPeopleCount END
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[CreateTime]=GETDATE()
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR
	
	--表变量中加入模板团进行行程价格等信息的修改
	INSERT INTO @tmptbl(TourId) VALUES(@TemplateTourId)

	--写入行程信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT B.[TourId],A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(MAX),Vehicle NVARCHAR(MAX),TrafficNumber NVARCHAR(MAX),House NVARCHAR(MAX),Dinner NVARCHAR(MAX),PlanContent NVARCHAR(MAX),PlanDay INT,PlanId CHAR(36)) AS A,@tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
		EXECUTE sp_xml_removedocument @hdoc		
	END
	ELSE--快速发布
	BEGIN
		DELETE FROM [tbl_TourFastPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) SELECT B.[TourId],@QuickPlan FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR	
	END

	--写入报价信息 先删除再新增
	DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT B.[TourId],A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT) AS A,@tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_removedocument @hdoc

	--写入服务标准信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourServiceStandard]
			([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		SELECT
			 B.TourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice
		FROM @tmptbl AS B
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--写入出港城市信息 先删除再新增	
	DELETE FROM [tbl_TourAreaControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	INSERT INTO [tbl_TourAreaControl](TourId,CityId) SELECT B.TourId,@LeaveCity FROM @tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR

	--写入销售城市信息
	DELETE FROM [tbl_TourCityControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT B.TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	DELETE FROM [tbl_TourThemeControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT B.TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT B.TourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A,@tmptbl AS B
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队自动停收状态 非手动停收手动客满→自动停收
	UPDATE [tbl_TourList] SET [TourState]=3 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE() AND TourState NOT IN(0,2)
	SET @errorCount=@errorCount+@@ERROR
    --标记团队正常状态 自动停收→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])>GETDATE() AND TourState IN(3)
	SET @errorCount=@errorCount+@@ERROR
	
	--标记团队状态 自动客满→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]>[OrderPeopleNumber] AND TourState=4
	SET @errorCount=@errorCount+@@ERROR
	--标记团队状态 正常→自动客满
	UPDATE [tbl_TourList] SET [TourState]=4 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]=[OrderPeopleNumber] AND TourState=1
	SET @errorCount=@errorCount+@@ERROR

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TemplateTourId,@IsParentTour=N'1'

	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--更新团队订单的线路区域信息
	UPDATE tbl_TourOrder SET AreaId=@AreaId WHERE TourId IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




--批量更新：将模板团(模板团下所有团队均已出团)下最后出团的团队[IsRecentLeave]标记为1
GO
UPDATE tbl_TourList SET [IsRecentLeave]='1' WHERE Id IN(
	SELECT A.Id FROM tbl_TourList AS A INNER JOIN(
		SELECT MAX(B.LeaveDate) AS MaxLeaveDate,B.ParentTourId FROM tbl_TourList AS B WHERE B.ParentTourId>'' GROUP BY B.ParentTourId
	) AS C ON A.ParentTourId=C.ParentTourId AND A.LeaveDate=C.MaxLeaveDate
)
GO


--添加随便逛逛身份标识--

insert into dbo.tbl_CompanyTypeList(CompanyId
,TypeId) select ID,11 from tbl_CompanyINfo where CompanyType=9



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-12
-- Description:	查询单位ID,名称,企业形象图片
-- =============================================
ALTER PROCEDURE [dbo].[proc_CompanyIDNAME_Select] 
	@XMLRoot NVARCHAR(MAX),  --sqlxml脚本
	@TypeId INT   --1:查询公司ID,名称  2:查询公司ID,名称,企业LOGO
AS
BEGIN	
	DECLARE @hdoc int;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	IF(@TypeId=1)
		SELECT Id,CompanyName FROM tbl_CompanyInfo a,
		(SELECT CompanyId,SortId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36),SortId int)) b
	WHERE a.ID=b.CompanyId--ID IN (SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36)));
	order by b.SortId
	ELSE	
		SELECT c.Id,c.CompanyName,attach.FieldValue AS CompanyLogo FROM tbl_CompanyInfo AS c
		LEFT JOIN tbl_CompanyAttachInfo AS attach ON c.ID=attach.CompanyId AND attach.FieldName='CompanyLogo',
		(SELECT CompanyId,SortId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36),SortId int)) b WHERE c.ID=b.CompanyID--c.ID IN (SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/CompanyInfo') WITH (CompanyId CHAR(36)));
		order by b.SortId
	EXEC sp_xml_removedocument @hdoc;
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


--系统整合 begin


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-24
-- Description:	修改公司明细资料
-- @ReturnValue  0:操作失败  1:操作成功
-- History:zhangzy  2010-12-02  修改内容:若省份为“其他采购商”的时候，则权限等同与组团身份权限
-- History:wangqizhi  2011-04-01  修改内容:-位信息管理修改自己公司资料时能修改公司名称
-- =============================================
ALTER PROC [dbo].[proc_CompanyInfo_Update]	
	@UpdateType CHAR(1),   --修改类型:1:运营后台修改公司资料,2:单位信息管理修改自己公司资料,3:高级网店修改公司基本档案
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CompanyId CHAR(36),   --公司ID
	@CityId INT,    --城市
	@CompanyAddress NVARCHAR(250),  --地址
	@CompanyBrand NVARCHAR(50),   --名牌
	@CompanyName NVARCHAR(250),   --公司名称
	@ContactName NVARCHAR(20),   --联系人
	@ContactEmail NVARCHAR(50),   --EMAIL
	@ContactFax NVARCHAR(50),  --传真
	@ContactMobile NVARCHAR(50),   --手机
	@ContactMSN NVARCHAR(50),   --MSN
	@ContactQQ NVARCHAR(250),   --QQ
	@ContactTel NVARCHAR(50),   --电话	
	@License NVARCHAR(50),  --许可证号
	@ProvinceId INT,   --省份ID
	@Remark NVARCHAR(MAX),   --公司介绍备注
	@ShortRemark NVARCHAR(250),   --公司简介(业务优势)	
	@CompanyImg  NVARCHAR(250),  --宣传图片	
	@CompanyLogo NVARCHAR(250),  --企业logo
	@CompanySignet  NVARCHAR(250),  --电子公章
	@BusinessCertImg NVARCHAR(250),  --经营许可证
	@LicenceImg NVARCHAR(250),  --营业执照
	@TaxRegImg NVARCHAR(250),  --税务登记证
	@XMLBankRoot NVARCHAR(MAX),  --银行帐号sqlxml脚本	
	@XMLRootSaleCity NVARCHAR(MAX),  --公司销售城市的sqlxml脚本
	@XMLRootArea NVARCHAR(MAX),  --公司线路区域的sqlxml脚本
	@BusinessProperties TINYINT,  --企业性质  1:旅行社
	@XMLRootCompanyType NVARCHAR(MAX),  --公司类型的sqlxml脚本	
	@UserId CHAR(36) OUTPUT,    --管理员的用户ID
	@ReturnValue CHAR(1) OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN	
	--DECLARE @UserId CHAR(36);  --管理员的用户ID	
	DECLARE @SQLCompanyUser NVARCHAR(MAX);
	DECLARE @SQLAttach NVARCHAR(MAX);
	DECLARE @PermissionList NVARCHAR(3000);   --权限列表
	DECLARE @CompanyTypeListOld NVARCHAR(20);  --原公司类型列表,无逗号分割,如:批发商,零售商,地接社身份,则为123,id值为升序排列 
	DECLARE @CompanyTypeListNew NVARCHAR(20);  --新的公司类型列表,无逗号分割,如:批发商,零售商,地接社身份,则为123,id值为升序排列
	DECLARE @ErrorAttach INT; --操作公司附件产生的错误信息
	SET @ErrorAttach = 0;
	SET @UserId = '';
	SET @SQLCompanyUser = '';
	SET @SQLAttach = '';
	SET @PermissionList = '';
	SET @CompanyTypeListOld = '';
	SET @CompanyTypeListNew = '';

	--开始事务
	BEGIN TRAN CompanyDetailInfo_Update
	----------------------修改公司管理员用户以及公司的资料-------------
	IF(@UpdateType='1')  --表示为运营后台的修改,则要修改公司总帐号的资料
	BEGIN
		--查询管理员的用户ID
		SELECT @UserId=ID FROM tbl_CompanyUser WHERE CompanyId=@CompanyId AND IsAdmin='1'	
		--修改管理员用户表
		SET @SQLCompanyUser = 'UPDATE [tbl_CompanyUser] SET [ProvinceId]=' + CAST(@ProvinceId AS NVARCHAR(10)) + ',[CityId]=' + CAST(@CityId AS NVARCHAR(10)) + ',[ContactName]=''' + @ContactName + ''',[ContactTel]=''' + @ContactTel + ''',[ContactFax]=''' + @ContactFax + ''',[ContactMobile]=''' + @ContactMobile + ''',[ContactEmail]=''' + @ContactEmail + ''',[QQ]=''' + @ContactQQ + ''',[MSN]=''' + @ContactMSN + '''';
		IF(@Password<>'')
		BEGIN
			SET @SQLCompanyUser = @SQLCompanyUser + ',[Password]=''' + @Password + ''',[EncryptPassword]=''' + @EncryptPassword + ''',[MD5Password]=''' + @MD5Password + '''';
			UPDATE im_member SET im_password=@MD5Password WHERE bs_uid=@UserId;
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN User_Update;
				RETURN;
			END	
		END
		SET @SQLCompanyUser = @SQLCompanyUser + ' WHERE Id=''' + @UserId + ''' AND CompanyId=''' + @CompanyId + '''';
		EXECUTE sp_executesql @SQLCompanyUser;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
		--修改公司表	
		UPDATE [tbl_CompanyInfo] SET [ProvinceId]=@ProvinceId,[CityId]=@CityId,[CompanyType]=@BusinessProperties,[CompanyName]=@CompanyName,[CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[License]=@License,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark,[ShortRemark]=@ShortRemark WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END		
		--修改公司身份类型
		--公司类型的sqlxml脚本
		DECLARE @hdocCompanyType int;
		EXEC sp_xml_preparedocument @hdocCompanyType OUTPUT, @XMLRootCompanyType;	
		--查询原来的公司类型
		SELECT @CompanyTypeListOld=@CompanyTypeListOld + CAST(TypeId AS NVARCHAR(5)) FROM [tbl_CompanyTypeList] WHERE CompanyId=@CompanyId ORDER BY TypeId;
		--查询新的公司类型
		SELECT @CompanyTypeListNew=@CompanyTypeListNew + CAST(TypeId AS NVARCHAR(5)) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) ORDER BY TypeId;
		IF(@CompanyTypeListOld<>@CompanyTypeListNew)  --表示公司身份有变动
		BEGIN
			--先删除原公司类型
			DELETE FROM [tbl_CompanyTypeList] WHERE [CompanyId]=@CompanyId;		
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
			--添加公司所有类型
			INSERT INTO [tbl_CompanyTypeList](
			[CompanyId],[TypeId]
			) SELECT @CompanyID,TypeId
			FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') 
			  WITH (TypeId TINYINT);
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;

			END
			---------------查询权限列表-----------------------
			--判断是否包含“其他采购商(10)”身份，若包含，则授予组团权限
			IF(SELECT COUNT(*) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) WHERE TypeId IN (10,2))>0
				SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=2;

			--根据身份查询特有权限
			SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;

			--查询通用服务的权限
			SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (4,5,6,34)
			IF(LEN(@PermissionList)>0 AND CHARINDEX(',', @PermissionList)>0)
				SET @PermissionList = SUBSTRING(@PermissionList, 1, LEN(@PermissionList)-1);
			ELSE
				SET @PermissionList = '';
	---------------查询权限列表-----------------------

			--修改管理员角色权限
			UPDATE [tbl_CompanyRoles] SET [PermissionList]=@PermissionList WHERE [CompanyID]=@CompanyId AND IsAdminRole=1;
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
			--清除其他所有非管理员角色的权限
			UPDATE [tbl_CompanyRoles] SET [PermissionList]='' WHERE [CompanyID]=@CompanyId AND IsAdminRole=0;			
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update;
				EXEC sp_xml_removedocument @hdocCompanyType; 
				RETURN;
			END
		END
		EXEC sp_xml_removedocument @hdocCompanyType; 
	END
	ELSE IF(@UpdateType='2')   --单位信息管理修改自己公司资料
	BEGIN
		UPDATE [tbl_CompanyInfo] SET [CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark,[CompanyName]=@CompanyName WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
	ELSE IF(@UpdateType='3')   --高级网店修改公司基本档案
	BEGIN
		UPDATE [tbl_CompanyInfo] SET [CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile WHERE Id=@CompanyId 
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END	

	----------------------修改公司附件信息------------
	IF(@UpdateType='1')  --运营后台->证书管理(营业执照,经营许可证,税务登记证)
	BEGIN
		--先删除,再新增		
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('LicenceImg','BusinessCertImg','TaxRegImg');
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'LicenceImg',@LicenceImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'BusinessCertImg',@BusinessCertImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TaxRegImg',@TaxRegImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
	ELSE IF(@UpdateType='2')  --单位信息管理->宣传图片,企业LOGO,企业公章,营业执照,经营许可证,税务登记证
	BEGIN
		--先删除,再新增
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND [FieldValue]=@CompanyImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND [FieldValue]=@CompanyLogo)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('CompanyImg','CompanyLogo','CompanySignet','LicenceImg','BusinessCertImg','TaxRegImg');
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanyImg',@CompanyImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanyLogo',@CompanyLogo);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanySignet',@CompanySignet);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'LicenceImg',@LicenceImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'BusinessCertImg',@BusinessCertImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TaxRegImg',@TaxRegImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
	END
--	ELSE IF(@UpdateType='2')  --修改高级网店公司档案
--	BEGIN
--		----高级网店公司档案,现在不对证书进行修改
--	END	
	----------------银行帐号信息------------	
	IF(@UpdateType='1' OR @UpdateType='2')  --运营后台,单位信息管理
	BEGIN
		--先删除所有银行帐号信息
		DELETE FROM [tbl_BankAccount] WHERE CompanyId=@CompanyId;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			RETURN;
		END
		--添加银行帐号
		DECLARE @bankhdoc int;
		EXEC sp_xml_preparedocument @bankhdoc OUTPUT, @XMLBankRoot;
		INSERT INTO [tbl_BankAccount](
		[CompanyId],[BankAccountName],[AccountNumber],[BankName],[TypeId]
		) SELECT @CompanyID,BankAccountName,AccountNumber,BankName,TypeId
		FROM OPENXML(@bankhdoc, N'/ROOT/BankAccountsUpdate') 
		  WITH (BankAccountName  NVARCHAR(100),AccountNumber NVARCHAR(250),BankName NVARCHAR(100),TypeId TINYINT);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @bankhdoc; 
			RETURN;
		END
		EXEC sp_xml_removedocument @bankhdoc; 
	END

	IF(@UpdateType='1')  --运营后台,修改公司以及所有用户销售城市,线路区域信息
	BEGIN
		-----------------------销售城市------------------
		DECLARE @hdocSaleCity int;
		EXEC sp_xml_preparedocument @hdocSaleCity OUTPUT, @XMLRootSaleCity;

		--先删除公司已不存在的销售城市
		DELETE FROM [tbl_CompanyCityControl] WHERE [CompanyId]=@CompanyID AND CityId NOT IN (SELECT CityId	FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') WITH (ProvinceId INT,CityId INT));
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocSaleCity; 
			RETURN;
		END
		--添加公司销售城市		
		INSERT INTO [tbl_CompanyCityControl](
			[CompanyId],[ProvinceId],[CityId]
			) SELECT @CompanyID,A.ProvinceId,A.CityId
		FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') WITH (ProvinceId INT,CityId INT) AS A
			WHERE A.CityId NOT IN (SELECT CityId FROM [tbl_CompanyCityControl] WHERE CompanyId=@CompanyID);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocSaleCity; 
			RETURN;
		END
		EXEC sp_xml_removedocument @hdocSaleCity; 
		
		-----------------------公司线路区域------------------
		DECLARE @hdocArea int;
		EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;
		---先删除公司线路区域,已不存在了的线路区域
		DELETE FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));	
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END
		--添加公司线路区域
		INSERT INTO [tbl_CompanyAreaConfig](
			[CompanyId],[AreaId]
			) SELECT @CompanyID,A.AreaId
		FROM OPENXML(@hdocArea, N'/ROOT/Area') 
		  WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyAreaConfig] WHERE [CompanyId]=@CompanyID);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END

		-----------------------用户线路区域------------------
		--删除已不存在了的所有帐号的线路区域
		DELETE FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));	
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0';
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END
		--添加管理员用户新的线路区域
		INSERT INTO [tbl_CompanyUserAreaControl](
			[UserId],[CompanyId],[AreaId]
			) SELECT @UserId,@CompanyID,A.AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND [UserId]=@UserId);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update; 
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END		
		EXEC sp_xml_removedocument @hdocArea; 
	END

	--执行成功		
	SET @ReturnValue = '1';
	COMMIT TRAN CompanyDetailInfo_Update;
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	新增公司
-- @ReturnValue  0:操作失败  1:操作成功   2:用户名数据已存在   3:Email数据已在用户表中存在
-- History:zhangzy  2010-11-17  修改内容:注册的时候新增通用权限 财务管理[34]
-- History:zhangzy  2010-11-29  修改内容:判断email是否存在增加条件 IsDeleted='0'
-- History:zhangzy  2010-12-02  修改内容:若省份为“其他采购商”的时候，则权限等同与组团身份权限
-- History:wangqizhi  2011-04-01  修改内容:注册不做邮箱验证
-- =============================================
ALTER PROC [dbo].[proc_CompanyInfo_Insert]	
	@UserId CHAR(36),  --用户ID
	@CompanyId CHAR(36),   --公司ID
	@UserName NVARCHAR(100),  --用户名
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CityId INT,    --城市
	@CompanyAddress NVARCHAR(250),  --公司地址
	@CompanyBrand NVARCHAR(50),   --名牌
	@CompanyName NVARCHAR(250),   --公司名称
	@ContactName NVARCHAR(20),   --联系人
	@ContactEmail NVARCHAR(50),   --EMAIL
	@ContactFax NVARCHAR(50),  --传真
	@ContactMobile NVARCHAR(50),   --手机
	@ContactMSN NVARCHAR(50),   --MSN
	@ContactQQ NVARCHAR(250),   --QQ
	@ContactTel NVARCHAR(50),   --电话	
	@License NVARCHAR(50),  --许可证号
	@ProvinceId INT,   --省份ID
	@CommendPeople NVARCHAR(50),   --引荐人	
	@ShortRemark NVARCHAR(250),   --公司简介(业务优势)
	@XMLRootSaleCity NVARCHAR(MAX),  --公司销售城市的sqlxml脚本
	@XMLRootArea NVARCHAR(MAX),  --公司线路区域的sqlxml脚本
	@BusinessProperties TINYINT,  --企业性质  1:旅行社
	@XMLRootCompanyType NVARCHAR(MAX),  --公司类型的sqlxml脚本
	@ReturnMQID NVARCHAR(250) OUTPUT,   --操作成功返回MQID
	@ReturnValue INT OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN	
	DECLARE @MQID INT;  --MQID
	DECLARE @IsCheck CHAR(1);   --是否审核
	DECLARE @RoleId CHAR(36);   --管理员角色ID  
	DECLARE @RoleName NVARCHAR(20);  --管理员角色名称
	DECLARE @PermissionList NVARCHAR(3000);   --权限列表
	DECLARE @DepartId CHAR(36);   --部门ID
	DECLARE @DepartName NVARCHAR(50);   --部门名称
	DECLARE @ContactSex CHAR(1);   --性别
	DECLARE @IsAdmin CHAR(1);  --是否管理员	
	DECLARE @ErrorRate INT;  --操作诚信数据的事务结果	
	SET @IsCheck = '0';
	SET @RoleId = NEWID();
	SET @RoleName = '管理员';
	SET @DepartId = '00000000-0000-0000-0000-000000000000';
	SET @DepartName = '';
	SET @ContactSex = '2';
	SET @IsAdmin = '1';
	SET @PermissionList = '';
	SET @ReturnMQID = '';
	SET @ErrorRate = 0;

	--检查Email是否已存在
	/*IF((SELECT COUNT(1) FROM tbl_CompanyUser WHERE ContactEmail=@ContactEmail AND IsDeleted='0')>0)
	BEGIN
		SET @ReturnValue = 3;
		RETURN;
	END*/
	--检查用户名是否已存在
	IF((SELECT COUNT(1) FROM im_member WHERE im_username=@UserName)>0)
	BEGIN
		SET @ReturnValue = 2;
		RETURN;
	END	
	--开始事务
	BEGIN TRAN Company_Insert		
	--计算MQ的ID
	SELECT @MQID=MAX(im_uid)+1 FROM im_member;
	IF(@MQID IS NULL)
		SET @MQID = 1;
	--插入MQ
	INSERT INTO [im_member](
	[im_uid],[im_password],[im_displayname],[im_username],[bs_uid]
	)VALUES(
	@MQID,@MD5Password,@ContactName,@UserName,@UserId
	);
--	INSERT INTO [im_member](
--		[im_password],[im_displayname],[im_username],[bs_uid]
--		)VALUES(
--		@MD5Password,@ContactName,@UserName,@UserId
--		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Insert;		
		RETURN;
	END	
	--SET @MQID = @@Identity
	--插入公司表	
	INSERT INTO [tbl_CompanyInfo](
		[Id],[ProvinceId],[CityId],[CompanyType],[CompanyName],[CompanyBrand],[CompanyAddress],[License],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[ContactMQ],[ContactQQ],[ContactMSN],[IsCheck],[CommendPeople],[ShortRemark]
		)VALUES(
		@CompanyId,@ProvinceId,@CityId,@BusinessProperties,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,CAST(@MQID AS VARCHAR(20)),@ContactQQ,@ContactMSN,@IsCheck,@CommendPeople,@ShortRemark
		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	--插入用户表
	INSERT INTO [tbl_CompanyUser](
		[Id],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],[ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],[RoleID],[IsAdmin],[DepartId],[DepartName]
		)VALUES(
		@UserId,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactQQ,@MQID,@ContactMSN,@RoleID,@IsAdmin,@DepartId,@DepartName
		);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	--公司类型的sqlxml脚本
	DECLARE @hdocCompanyType int;
	EXEC sp_xml_preparedocument @hdocCompanyType OUTPUT, @XMLRootCompanyType;	
	--添加公司所有类型
	INSERT INTO [tbl_CompanyTypeList](
	[CompanyId],[TypeId]
	) SELECT @CompanyID,TypeId
	FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') 
	  WITH (TypeId TINYINT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocCompanyType; 
		RETURN;
	END

	---------------查询权限列表-----------------------
	--判断是否包含“其他采购商(10)”身份，若包含，则授予组团权限
	IF(SELECT COUNT(*) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) WHERE TypeId IN (10,2))>0
		SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=2;

	--根据身份查询特有权限
	SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;

	--查询通用服务的权限
	SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (4,5,6,34)
	IF(LEN(@PermissionList)>0 AND CHARINDEX(',', @PermissionList)>0)
		SET @PermissionList = SUBSTRING(@PermissionList, 1, LEN(@PermissionList)-1);
	ELSE
		SET @PermissionList = '';
	---------------查询权限列表-----------------------

	--创建管理员角色	
	INSERT INTO [tbl_CompanyRoles]
           ([ID],[RoleName],[PermissionList],[IsAdminRole],[CompanyID],[OperatorID])
     VALUES (@RoleId, @RoleName, @PermissionList, @IsAdmin, @CompanyId,@UserId);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocCompanyType; 
		RETURN;
	END
	--以上执行正确,则删除公司类型的XML
	EXEC sp_xml_removedocument @hdocCompanyType; 

    --新增常规团价格等级
	INSERT INTO [tbl_CompanyPriceStand]
           ([ID],[CommonPriceStandID],[PriceStandName],[CompanyID],[IsSystem]) VALUES(NEWID(),'COMPRICE-0000-0000-0000-000000000026', '常规团', @CompanyId, 0);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		RETURN;
	END
	
	--添加公司附件信息
--	INSERT INTO [tbl_CompanyAttachInfo](
--		[CompanyId],[CompanyImg],[CompanyLogo],[LicenceImg],[BusinessCertImg],[TaxRegImg],[CommitmentImg]
--		)VALUES(
--		@CompanyId,'','','','','',''
--		);
    --添加公司设置
--	INSERT INTO [tbl_CompanySetting](
--		[CompanyId],[TourStopTime],[TourCity]
--		)VALUES(
--		@CompanyId,0,0
--		);
	--添加团队状态基础数据
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,1,'推荐');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,2,'促销');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,3,'最新');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,4,'品质');
--	INSERT INTO [tbl_TourStateBase] ([CompanyId],[TypeId],[PromoText]) VALUES (@CompanyId,5,'纯玩');
	
	--添加诚信体系基础数据
	--信用分值信息 
	--[tbl_RateScoreTotal].[ScoreType] 1.交易总分值2.评价总分值 3:服务品质总星星数,4:性价比总星星数,5:旅游内容安排总星星数,6:活跃总分值,7:推荐总分值
	DECLARE @i TINYINT;
	SET @i=1;
	WHILE(@i>=1 AND @i<=7)
	BEGIN
		INSERT INTO tbl_RateScoreTotal(CompanyId,ScoreType,ScorePoint) VALUES(@CompanyId,@i,0);
		SET @ErrorRate = @ErrorRate + @@ERROR;
		SET @i=@i+1;
	END

	--次数信息
	--[tbl_RateCountTotal].[ScoreType] 1.交易总次数, 2:好评总次数,3:中评总次数,4:差评总次数,5:活跃总次数,6:推荐总次数
	SET @i=1;
	WHILE(@i>=1 AND @i<=6)
	BEGIN
		INSERT INTO tbl_RateCountTotal(CompanyId,ScoreType,ScoreNumber) VALUES(@CompanyId,@i,0);
		SET @ErrorRate = @ErrorRate + @@ERROR;
		SET @i=@i+1;
	END
	--判断插入诚信数据有无出错
	IF @ErrorRate <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;		 
		RETURN;
	END

	--添加公司销售城市
	DECLARE @hdocSaleCity int;
	EXEC sp_xml_preparedocument @hdocSaleCity OUTPUT, @XMLRootSaleCity;
	INSERT INTO [tbl_CompanyCityControl](
		[CompanyId],[ProvinceId],[CityId]
		) SELECT @CompanyID,ProvinceId,CityId
	FROM OPENXML(@hdocSaleCity, N'/ROOT/SaleCity') 
	  WITH (ProvinceId INT,CityId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocSaleCity; 
		RETURN;
	END
	EXEC sp_xml_removedocument @hdocSaleCity; 
	DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;
	--添加公司线路区域
	INSERT INTO [tbl_CompanyAreaConfig](
		[CompanyId],[AreaId]
		) SELECT @CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	--添加管理员用户线路区域
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = 0; 
		ROLLBACK TRAN Company_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	EXEC sp_xml_removedocument @hdocArea; 
	--写入公司常用出港城市[插入热门城市:北京 上海 广州和本省]
	--热门
	INSERT INTO [tbl_CompanySiteControl](
		[CompanyId],[CityId]
		) SELECT @CompanyID,ID FROM tbl_SysCity WHERE CityName IN ('北京', '上海', '广州');
	--本省
	INSERT INTO [tbl_CompanySiteControl](
		[CompanyId],[CityId]
		) SELECT @CompanyID,ID FROM tbl_SysCity WHERE ID NOT IN (SELECT DISTINCT CityId FROM [tbl_CompanySiteControl] WHERE CompanyId=@CompanyID) AND ProvinceId=@ProvinceId AND IsSite='1';	

	--执行成功
	SET @ReturnValue = 1;
	SET @ReturnMQID = @MQID;
	COMMIT TRAN Company_Insert;
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	新增公司子帐号信息
-- @ReturnValue  0:操作失败  1:操作成功   2:用户名数据已存在
-- history: 
-- 1.@ContactQQ字段长度有50调整为250   2010-11-8
-- 2.汪奇志 2011-04-08 角色编号为空时默认取公司管理员角色，用于系统整合，因大平台创建子账号时角色必选将不受影响
-- =============================================
ALTER PROC [dbo].[proc_CompanyUserChild_Insert]	
	@UserId char(36),   --用户ID	
	@CompanyId char(36),   --公司ID	
	@UserName nvarchar(100),   --用户名
	@Password varchar(50),    --未加密密码
	@EncryptPassword nvarchar(128),   --SHA加密密码
	@MD5Password nvarchar(50),   --MD5加密密码
	@ContactName nvarchar(100),   --联系人
	@ContactSex char(1),   --性别
	@ContactTel nvarchar(50),   --电话
	@ContactFax nvarchar(50),   --传真
	@ContactMobile nvarchar(50),   --手机
	@ContactEmail nvarchar(50),   --EMAIL
	@ContactQQ varchar(250),  
	@ContactMSN nvarchar(50),
	@RoleID char(36),   --角色ID
	@DepartId char(36),  --部门ID
	@DepartName nvarchar(50),   --部门名称
    @XMLRootArea NVARCHAR(MAX), --用户线路区域的sqlxml脚本
	@ReturnMQID NVARCHAR(250) OUTPUT,   --操作成功返回MQID
	@ReturnProvinceId INT OUTPUT,   --操作成功返回省份ID
	@ReturnValue CHAR(1) OUTPUT    --0:操作失败  1:操作成功   2:用户名数据已存在
AS
BEGIN		
	DECLARE @MQID INT;  --MQID 
	DECLARE @ProvinceId int;   --省份ID
	DECLARE @CityId int;   --城市ID
	DECLARE @IsAdmin CHAR(1);  --是否管理员
	SET @IsAdmin = '0';
	SET @ReturnMQID = '';
	SET @ReturnProvinceId = 0;

	--检查用户名是否已存在
	IF((SELECT COUNT(1) FROM im_member WHERE im_username=@UserName)>0)
	BEGIN
		SET @ReturnValue = '2';
		RETURN;
	END
	--开始事务
	BEGIN TRAN UserChild_Insert

	IF(@RoleID IS NULL OR LEN(@RoleID)<1)--角色编号为空时默认取公司管理员角色，用于系统整合，因大平台创建子账号时角色必选将不受影响
	BEGIN
		SELECT @RoleID=[ID] FROM [tbl_CompanyRoles] WHERE [CompanyID]=@CompanyId AND [IsAdminRole]='1'
	END

	--计算MQ的ID
	SELECT @MQID=MAX(im_uid)+1 FROM im_member;
	--插入MQ
	INSERT INTO [im_member](
	[im_uid],[im_password],[im_displayname],[im_username],[bs_uid]
	)VALUES(
	@MQID,@MD5Password,@ContactName,@UserName,@UserId
	);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0';
		ROLLBACK TRAN UserChild_Insert;
		RETURN;
	END
	
	--查询公司所在的省份,城市ID
	SELECT @ProvinceId=ProvinceId,@CityId=CityId FROM tbl_CompanyInfo WHERE Id=@CompanyId

	--插入用户表
	INSERT INTO [tbl_CompanyUser](
		[Id],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],[ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],[RoleID],[IsAdmin],[DepartId],[DepartName])VALUES(
		@UserId,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactQQ,@MQID,@ContactMSN,@RoleID,@IsAdmin,@DepartId,@DepartName);

	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN UserChild_Insert;
		RETURN;
	END	
	
	--添加用户线路区域
    DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;	
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,AreaId
	FROM OPENXML(@hdocArea, N'/ROOT/Area') 
	  WITH (AreaId INT);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN UserChild_Insert;
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END
	
	--执行成功
	EXEC sp_xml_removedocument @hdocArea; 
	SET @ReturnValue = '1';
	SET @ReturnMQID = @MQID;
	SET @ReturnProvinceId = @ProvinceId;
	COMMIT TRAN UserChild_Insert;
END
GO
--系统整合 end








--Table: tbl_LogTicket 机票接口访问日志  汪奇志 2011-05-10                      
if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_LogTicket')
            and   type = 'U')
   drop table tbl_LogTicket
go

/*==============================================================*/
/* Table: tbl_LogTicket 机票接口访问日志  汪奇志 2011-05-10                                       */
/*==============================================================*/
create table tbl_LogTicket (
   LogId                int                  identity,
   CompanyId            char(36)             null,
   UserId               char(36)             null,
   LCity                char(5)              null,
   RCity                char(5)              null,
   LDate                datetime             null,
   RDate                datetime             null,
   CDate                datetime             not null default getdate(),
   constraint PK_TBL_LOGTICKET primary key (LogId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '自动编号',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'LogId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'CompanyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户编号',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出发城市',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'LCity'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '目的城市',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'RCity'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出发日期',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'LDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '回程日期',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'RDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作时间',
   'user', @CurrentUser, 'table', 'tbl_LogTicket', 'column', 'CDate'
go



--
-- =============================================
-- Author:		汪奇志
-- Create date: 2011-06-09
-- Description: 机票接口访问日志视图
-- =============================================
CREATE VIEW [dbo].[view_LogTicket]
AS
SELECT 
	[CompanyId]
	,[UserId]
	,MAX([CDate]) AS LatestDate--最后访问时间
	,(SELECT COUNT(*) FROM [tbl_LogTicket] A WHERE A.[UserId]=[tbl_LogTicket].[UserId]) AS TotalTimes--总访问次数
	,(SELECT COUNT(*) FROM [tbl_LogTicket] A WHERE A.[UserId]=[tbl_LogTicket].[UserId] AND DATEDIFF(dd,[CDate],GETDATE())<7 ) AS WeekTimes--一周内访问次数
FROM [tbl_LogTicket]
GROUP BY [CompanyId],[UserId]
GO



--发布计划更改 start  2011-07-28 汪奇志

--更新前准备工作
--快速发布计划行程信息临时表
CREATE TABLE [tbl_TourFastPlan_TMP]([TourBasicID] [char](36) NOT NULL,[RoutePlan] [nvarchar](max) NULL)
GO
--导入要保留的快速发布计划行程信息到临时表
INSERT INTO [tbl_TourFastPlan_TMP]([TourBasicID],[RoutePlan])
SELECT [TourBasicID],[RoutePlan] FROM [tbl_TourFastPlan] WHERE [TourBasicID] IN(SELECT [Id] FROM [tbl_TourList] WHERE [ParentTourId]='' OR [ComeBackDate]>=GETDATE())
GO
--以下删除操作每次执行一行
--删除标准发布计划行程信息
DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID] IN(SELECT id from tbl_Tourlist WHERE parenttourid>'' and ComeBackDate<getdate())
GO
--删除标准发布计划服务标准信息
DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID] IN(SELECT id from tbl_Tourlist WHERE parenttourid>'' and ComeBackDate<getdate())
GO
--删除标准发布计划地接社信息
DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] IN(SELECT id from tbl_Tourlist WHERE parenttourid>'' and ComeBackDate<getdate())
GO
--删除计划报价信息
DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID] IN(SELECT id from tbl_Tourlist WHERE parenttourid>'' and ComeBackDate<getdate())
GO
--删除快速发布计划行程表
DROP TABLE [tbl_TourFastPlan]
GO

--以上操作完成后
EXEC  sp_rename  'tbl_TourFastPlan_TMP' ,  'tbl_TourFastPlan'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团队基本信息编号(默认为newid())  单聚集唯一索引 主键, 外键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_TourFastPlan', @level2type=N'COLUMN',@level2name=N'TourBasicID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行程内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_TourFastPlan', @level2type=N'COLUMN',@level2name=N'RoutePlan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快速发布团队行程' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_TourFastPlan'
GO
ALTER TABLE [dbo].[tbl_TourFastPlan]  WITH CHECK ADD  CONSTRAINT [FK_TBL_TOUR_REFERENCE_TBL_TOUR] FOREIGN KEY([TourBasicID])
REFERENCES [dbo].[tbl_TourList] ([ID])
GO
ALTER TABLE [dbo].[tbl_TourFastPlan] CHECK CONSTRAINT [FK_TBL_TOUR_REFERENCE_TBL_TOUR]
GO

 ALTER TABLE [dbo].[tbl_TourFastPlan] ADD CONSTRAINT [PK_TBL_TOURFASTPLAN] PRIMARY KEY NONCLUSTERED 
(
	[TourBasicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_TourFastPlan] ADD  CONSTRAINT [PK_tbl_TourFastPlan] PRIMARY KEY CLUSTERED 
(
	[TourBasicID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO



-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-19
-- Description:	新增团队信息(模板团)
-- History:
-- 1.2011-07-27 写入计划时仅模板团写行程、报价、服务标准、地接社信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_AddTemplateTourInfo]
	@TemplateTourId CHAR(36),--模板团编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@TourType TINYINT,--团队类型
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT=0,--行程是否异常(百分制)
	@TourPriceError INT=0,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(MAX)=NULL,--温馨提醒
	@LeaveCity INT, --出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@InsertChildren NVARCHAR(MAX),--要写入的子团信息 XML:<ROOT><TourInfo TourId="团队编号 CHAR(36)" TourCode="团号 NVARCHAR(200)" LeaveDate="出团日期 DATETIME"></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN	
	SET @Result=0
	
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号

	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc

	BEGIN TRANSACTION AddStandardTourInfo
	DECLARE @errorCount INT	
	SET @errorCount=0
	
	--写入模板团信息
	INSERT INTO [tbl_TourList]
		([ID],[CompanyID],[UnitCompanyId],[CompanyName],[RouteName]--R1
		,[TourNo],[TourDays],[LeaveDate],[ComeBackDate],[TourState]--R2
		,[TourClassID],[RouteState],[RouteType],[AreaId],[TourDescription]--R3
		,[ParentTourID],[LeaveTraffic] ,[BackTraffic],[PlanPeopleCount],[RemnantNumber]--R4
		,[SendContactName],[SendContactTel],[UrgentContactName],[UrgentContactTel]--R5
		,[TourContact],[TourContactTel],[StopAcceptNum],[TourContacMQ],[TourContacUserName]--R6
		,[MeetTourContect],[CollectionContect],[PersonalPrice],[ChildPrice],[RealPrice]--R7
		,[MarketPrice],[OperatorID],[IssueTime],[IsDelete],[IsChecked]--R8
		,[ShowCount],[IsRecentLeave],[CreateTime],[RecentLeaveCount],[OccupySeat]--R9
		,[StandardPlanError],[TourPriceError],[IsCompanyCheck],[TourReleaseType]--R10
		,[RetailAdultPrice],[RetailChildrenPrice])--R11
	VALUES
		(@TemplateTourId,@CompanyId,@UnitCompanyId,@CompanyName,@RouteName--R1
		,'',@TourDays,GETDATE(),DATEADD(DAY,@TourDays-1,GETDATE()),1--R2
		,@TourType,0,@AreaType,@AreaId,''--R3		
		,'',@LeaveTraffic,@BackTraffic,@PlanPeopleCount,@PlanPeopleCount--R4
		,@SendContactName,@SendContactTel,@UrgentContactName,@UrgentContactTel--R5
		,@TourContact,@TourContactTel,@AutoOffDays,@TourContactMQ,@TourContactUserName--R6
		,@MeetTourContect,@CollectionContect,@PersonalPrice,@ChildPrice,@RealPrice--R7
		,@MarketPrice,@OperatorID,GETDATE(), '0','1'--R8
		,0,'0',GETDATE(),0,''--R9
		,@StandardPlanError,@TourPriceError,@IsCompanyCheck,@ReleaseType--R10
		,@RetailAdultPrice,@RetailChildrenPrice)
	SET @errorCount=@errorCount+@@ERROR

	--团号前缀
	DECLARE @Prefix NVARCHAR(50)
	SELECT @Prefix=ISNULL(PrefixText,'') FROM tbl_CompanyAreaConfig WHERE CompanyId=@CompanyId AND AreaId=@AreaId

	--写入子团信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@InsertChildren
	INSERT INTO [tbl_TourList]
		([ID],[CompanyID],[UnitCompanyId],[CompanyName],[RouteName]--R1
		,[TourNo],[TourDays],[LeaveDate],[ComeBackDate],[TourState]--R2
		,[TourClassID],[RouteState],[RouteType],[AreaId],[TourDescription]--R3
		,[ParentTourID],[LeaveTraffic] ,[BackTraffic],[PlanPeopleCount],[RemnantNumber]--R4
		,[SendContactName],[SendContactTel],[UrgentContactName],[UrgentContactTel]--R5
		,[TourContact],[TourContactTel],[StopAcceptNum],[TourContacMQ],[TourContacUserName]--R6
		,[MeetTourContect],[CollectionContect],[PersonalPrice],[ChildPrice],[RealPrice]--R7
		,[MarketPrice],[OperatorID],[IssueTime],[IsDelete],[IsChecked]--R8
		,[ShowCount],[IsRecentLeave],[CreateTime],[RecentLeaveCount],[OccupySeat]--R9
		,[StandardPlanError],[TourPriceError],[IsCompanyCheck],[TourReleaseType]--R10
		,[RetailAdultPrice],[RetailChildrenPrice])--R11
	SELECT
		 A.[TourId],@CompanyId,@UnitCompanyId,@CompanyName,@RouteName--R1
		,dbo.fn_TourList_CreateTourCode(@CompanyId,@Prefix,A.[LeaveDate]),@TourDays,A.[LeaveDate],DATEADD(DAY,@TourDays-1,A.[LeaveDate]),1--R2
		,@TourType,0,@AreaType,@AreaId,''--R3		
		,@TemplateTourId,@LeaveTraffic,@BackTraffic,@PlanPeopleCount,@PlanPeopleCount--R4
		,@SendContactName,@SendContactTel,@UrgentContactName,@UrgentContactTel--R5
		,@TourContact,@TourContactTel,@AutoOffDays,@TourContactMQ,@TourContactUserName--R6
		,@MeetTourContect,@CollectionContect,@PersonalPrice,@ChildPrice,@RealPrice--R7
		,@MarketPrice,@OperatorID,GETDATE(), '0','1'--R8
		,0,'0',GETDATE(),0,''--R9
		,@StandardPlanError,@TourPriceError,@IsCompanyCheck,@ReleaseType--R10
		,@RetailAdultPrice,@RetailChildrenPrice
	FROM OPENXML(@hdoc,'/ROOT/TourInfo')
	WITH(TourId CHAR(36),TourCode NVARCHAR(200),LeaveDate DATETIME) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--表变量存放此次写入的团队(模板团及子团)
	DECLARE @tmptbl TABLE(TourId CHAR(36))
	INSERT INTO @tmptbl(TourId) SELECT [Id] FROM [tbl_TourList] WHERE [Id]=@TemplateTourId OR [ParentTourID]=@TemplateTourId

	--写入行程信息
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT @TemplateTourId,A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(MAX),Vehicle NVARCHAR(MAX),TrafficNumber NVARCHAR(MAX),House NVARCHAR(MAX),Dinner NVARCHAR(MAX),PlanContent NVARCHAR(MAX),PlanDay INT,PlanId CHAR(36)) AS A
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR	
	END
	ELSE --快速发布
	BEGIN
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) VALUES (@TemplateTourId,@QuickPlan)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--写入报价信息	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @TemplateTourId,A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT) AS A
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR	
	
	--写入服务标准信息 快速发布无服务标准
	IF(@ReleaseType='0')
	BEGIN
		INSERT INTO [tbl_TourServiceStandard]([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		VALUES(@TemplateTourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice)		
		SET @errorCount=@errorCount+@@ERROR
	END

	--写入出港城市信息
	INSERT INTO [tbl_TourAreaControl](TourId,CityId) SELECT B.TourId,@LeaveCity FROM @tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR

	--写入销售城市信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT B.TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT B.TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT @TemplateTourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队状态：过了停收日期的团队标记团队状态为自动停收
	UPDATE [tbl_TourList] SET TourState=3 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE()
	SET @errorCount=@errorCount+@@ERROR	

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TemplateTourId,@IsParentTour=N'1'

	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--更新城市团队数量
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	UPDATE tbl_SysCity SET ParentTourCount=ParentTourCount+1,TourCount=TourCount+(SELECT COUNT(*)-1 FROM @tmptbl)
	WHERE Id IN(SELECT CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT))
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION AddStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN		
		COMMIT TRANSACTION AddStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-05-20
-- Description:	修改团队信息(模板团)
-- History:
-- 1.2011-07-27 修改计划时仅模板团写行程、报价、服务标准、地接社信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_TourList_UpdateTemplateTourInfo]
	@TemplateTourId CHAR(36),--模板团编号
	@CompanyId CHAR(36),--公司编号
	@CompanyName NVARCHAR(250),--公司名称
	@IsCompanyCheck CHAR(1),--公司是否通过审核
	@TourType TINYINT,--团队类型
	@RouteName NVARCHAR(250),--线路名称
	@TourDays INT,--团队天数
	@AreaId INT,--线路区域编号
	@AreaType TINYINT,--线路区域类型	
	@PlanPeopleCount INT,--计划人数
	@ReleaseType CHAR(1),--团队发布类型		
	@AutoOffDays INT,--自动停收时间(单位:天)
	@LeaveTraffic NVARCHAR(500)=NULL,--出发交通(交通安排)
	@BackTraffic NVARCHAR(500)=NULL,--返程交通	
	@SendContactName NVARCHAR(150)=NULL,--送团人
	@SendContactTel NVARCHAR(100)=NULL,--送团电话
	@UrgentContactName NVARCHAR(150)=NULL,--紧急联系人
	@UrgentContactTel NVARCHAR(100)=NULL,--紧急联系电话	
	@OperatorID CHAR(36),--操作员编号
	@TourContact NVARCHAR(100),--团队负责人
	@TourContactTel NVARCHAR(100),--团队负责人电话
	@TourContactMQ NVARCHAR(20),--团队负责人MQ
	@TourContactUserName NVARCHAR(100),--团队负责人用户名	
	@MeetTourContect NVARCHAR(250)=NULL,--接团方式
	@CollectionContect NVARCHAR(250)=NULL,--集合方式
	@StandardPlanError INT=0,--行程是否异常(百分制)
	@TourPriceError INT=0,--价格是否异常(百分制)	
	@ResideContent NVARCHAR(MAX)=NULL,--住宿(包含项目)	
	@DinnerContent NVARCHAR(MAX)=NULL,--用餐(包含项目)	
	@SightContent NVARCHAR(MAX)=NULL,--景点(包含项目)	
	@CarContent NVARCHAR(MAX)=NULL,--用车(包含项目)	
	@GuideContent NVARCHAR(MAX)=NULL,--导游(包含项目)	
	@TrafficContent NVARCHAR(MAX)=NULL,--往返交通(包含项目)	
	@IncludeOtherContent NVARCHAR(MAX)=NULL,--包含项目其它内容(包含项目)	
	@NotContainService NVARCHAR(MAX)=NULL,--不包含项目 (合并不包含项目、自费项目、儿童安排、购物安排、赠送项目、注意事项)	
	@SpeciallyNotice NVARCHAR(MAX)=NULL,--温馨提醒
	@LeaveCity INT,--出港城市
	@SaleCity NVARCHAR(MAX),--销售城市 XML:<ROOT><SaleCityInfo  CityId="销售城市编号 INT" /></ROOT>
	@RouteTheme NVARCHAR(MAX),--线路主题 XML:<ROOT><ThemeInfo ThemeId="主题编号 INT" /></ROOT>
	@PriceDetail NVARCHAR(MAX),--报价信息 XML:<ROOT><PriceInfo AdultPrice="成人价|同行价格 MONEY" ChildrenPrice="儿童价|门市价 MONEY" PriceStandId="报价标准编号 CHAR(36)" CustomerLevelId="客户等级编号 0:同行 1:门市 2:单房差"  RowMark="报价等级行标识 TINYINT" /></ROOT>
	@StandardPlan NVARCHAR(MAX)=NULL,--标准发布的行程信息 XML:<ROOT><PlanInfo PlanId="行程编号 CHAR(36)" PlanInterval="行程区间 NVARCHAR(50)" Vehicle="交通工具 NVARCHAR(50)" TrafficNumber="班次 NVARCHAR(50)" House="住宿 NVARCHAR(50)" Dinner="用餐 NVARCHAR(50)" PlanContent="行程内容 NVARCHAR(2000)" PlanDay="第几天行程 INT"  /></ROOT>
	@LocalTravelAgency NVARCHAR(MAX)=NULL,--地接社信息 XML:<ROOT><AgencyInfo AgencyId="" AgencyName="" License="" Telephone=""  /></ROOT>
	@QuickPlan NVARCHAR(MAX)=NULL,--快速发布的行程内容
	@Result INT OUTPUT--操作结果
AS
BEGIN	
	SET @Result=0
	--经营单位信息处理
	DECLARE @UnitCompanyId CHAR(36)--经营单位编号
	
	--报价信息(同行价中的最低价)
	DECLARE @PersonalPrice MONEY --成人价
	DECLARE @ChildPrice MONEY --儿童价
	DECLARE @RealPrice MONEY --单房差同行价
	DECLARE @MarketPrice MONEY --单房差门市价
	DECLARE @RetailAdultPrice MONEY --门市成人价
	DECLARE @RetailChildrenPrice MONEY --门市儿童价

	DECLARE @hdoc INT
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail

	SELECT @PersonalPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.AdultPrice ELSE 0 END)
		,@ChildPrice=SUM(CASE WHEN C.CustomerLevelId=0  THEN C.ChildrenPrice ELSE 0 END)
		,@RealPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.AdultPrice ELSE 0 END)
		,@MarketPrice=SUM(CASE WHEN C.CustomerLevelId=2  THEN C.ChildrenPrice ELSE 0 END)
		,@RetailAdultPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.AdultPrice ELSE 0 END)
		,@RetailChildrenPrice=SUM(CASE WHEN C.CustomerLevelId=1 THEN C.ChildrenPrice ELSE 0 END)
	FROM (SELECT B.AdultPrice,B.ChildrenPrice,B.CustomerLevelId,B.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
					WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS B WHERE B.PriceStandId=(SELECT TOP 1 A.PriceStandId FROM OPENXML(@hdoc,'/ROOT/PriceInfo')
						WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT) AS A
						WHERE A.CustomerLevelId=0 ORDER BY A.AdultPrice ASC)
	) AS C
	GROUP BY C.PriceStandId
	EXECUTE sp_xml_removedocument @hdoc
	
	BEGIN TRANSACTION UpdateStandardTourInfo
	DECLARE @errorCount INT
	SET @errorCount=0

	--更新模板团信息
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]=@PlanPeopleCount
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id=@TemplateTourId	

	--表变量存放此次要更新的子团信息
	DECLARE @tmptbl TABLE(TourId CHAR(36))
	INSERT INTO @tmptbl(TourId) SELECT Id FROM tbl_TourList WHERE ParentTourID=@TemplateTourId AND LeaveDate>=GETDATE()	

	--更新子团信息 订单人数(除留位过期、不受理的订单)>计划人数时 计划人数=订单人数
	UPDATE [tbl_TourList] SET
		 [UnitCompanyId]=@UnitCompanyId,[CompanyName]=@CompanyName,[RouteName]=@RouteName
		,[TourDays]=@TourDays,[ComeBackDate]=DATEADD(DAY,@TourDays-1,[LeaveDate])
		,[RouteType]=@AreaType,[AreaId]=@AreaId
		,[LeaveTraffic]=@LeaveTraffic,[BackTraffic]=@BackTraffic,[PlanPeopleCount]= CASE WHEN OrderPeopleNumber>@PlanPeopleCount THEN OrderPeopleNumber ELSE @PlanPeopleCount END
		,[SendContactName]=@SendContactName,[SendContactTel]=@SendContactTel,[UrgentContactName]=@UrgentContactName,[UrgentContactTel]=@UrgentContactTel
		,[TourContact]=@TourContact,[TourContactTel]=@TourContactTel,[StopAcceptNum]=@AutoOffDays,[TourContacMQ]=@TourContactMQ,[TourContacUserName]=@TourContactUserName
		,[MeetTourContect]=@MeetTourContect,[CollectionContect]=@CollectionContect,[PersonalPrice]=@PersonalPrice,[ChildPrice]=@ChildPrice,[RealPrice]=@RealPrice
		,[MarketPrice]=@MarketPrice,[OperatorID]=@OperatorID
		,[CreateTime]=GETDATE()
		,[StandardPlanError]=@StandardPlanError,[TourPriceError]=@TourPriceError,[IsCompanyCheck]=@IsCompanyCheck
		,[RetailAdultPrice]=@RetailAdultPrice,[RetailChildrenPrice]=@RetailChildrenPrice
	WHERE Id IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR
	
	--表变量中加入模板团进行行程价格等信息的修改
	INSERT INTO @tmptbl(TourId) VALUES(@TemplateTourId)

	--写入行程信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@StandardPlan
		DELETE FROM [tbl_TourStandardPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourStandardPlan] ([TourBasicID],[PlanInterval],[Vehicle],[TrafficNumber],[House],[Dinner],[PlanContent],[PlanDay])		
		SELECT @TemplateTourId,A.[PlanInterval],A.[Vehicle],A.[TrafficNumber],A.[House],A.[Dinner],A.[PlanContent],A.[PlanDay]
		FROM OPENXML(@hdoc,'/ROOT/PlanInfo') WITH(PlanInterval NVARCHAR(MAX),Vehicle NVARCHAR(MAX),TrafficNumber NVARCHAR(MAX),House NVARCHAR(MAX),Dinner NVARCHAR(MAX),PlanContent NVARCHAR(MAX),PlanDay INT,PlanId CHAR(36)) AS A
		SET @errorCount=@errorCount+@@ERROR	
		EXECUTE sp_xml_removedocument @hdoc		
	END
	ELSE--快速发布
	BEGIN
		DELETE FROM [tbl_TourFastPlan] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourFastPlan]([TourBasicID],[RoutePlan]) VALUES (@TemplateTourId,@QuickPlan)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--写入报价信息 先删除再新增
	DELETE FROM [tbl_TourBasicPriceDetail] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@PriceDetail
	INSERT INTO [tbl_TourBasicPriceDetail]([TourBasicID],[PerosonalPrice],[ChildPrice],[CompanyPriceStandID],[SysCustomLevel],[RowMark])
    SELECT @TemplateTourId,A.[AdultPrice],A.[ChildrenPrice],A.[PriceStandId],A.[CustomerLevelId],A.[RowMark]
	FROM OPENXML(@hdoc,'/ROOT/PriceInfo') WITH(AdultPrice MONEY,ChildrenPrice MONEY,PriceStandId CHAR(36),CustomerLevelId INT,RowMark TINYINT) AS A
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_removedocument @hdoc

	--写入服务标准信息 先删除再新增
	IF(@ReleaseType='0') --标准发布
	BEGIN
		DELETE FROM [tbl_TourServiceStandard] WHERE [TourBasicID] IN (SELECT TourId FROM @tmptbl)
		SET @errorCount=@errorCount+@@ERROR	
		INSERT INTO [tbl_TourServiceStandard]([TourBasicID],[ResideContent],[DinnerContent],[SightContent],[CarContent]
			,[GuideContent],[TrafficContent],[IncludeOtherContent],[NotContainService],[SpeciallyNotice])
		VALUES(@TemplateTourId,@ResideContent,@DinnerContent,@SightContent,@CarContent
			,@GuideContent,@TrafficContent,@IncludeOtherContent,@NotContainService,@SpeciallyNotice)
		SET @errorCount=@errorCount+@@ERROR
	END
	
	--写入出港城市信息 先删除再新增	
	DELETE FROM [tbl_TourAreaControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	INSERT INTO [tbl_TourAreaControl](TourId,CityId) SELECT B.TourId,@LeaveCity FROM @tmptbl AS B
	SET @errorCount=@errorCount+@@ERROR

	--写入销售城市信息
	DELETE FROM [tbl_TourCityControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@SaleCity
	INSERT INTO [tbl_TourCityControl] (TourId,CityId)
	SELECT B.TourId,A.CityId FROM OPENXML(@hdoc,'/ROOT/SaleCityInfo') WITH(CityId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入主题信息
	DELETE FROM [tbl_TourThemeControl] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
	EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@RouteTheme
	INSERT INTO [tbl_TourThemeControl] (TourId,ThemeId)
	SELECT B.TourId,A.ThemeId FROM OPENXML(@hdoc,'/ROOT/ThemeInfo') WITH(ThemeId INT) AS A,@tmptbl AS B	
	EXECUTE sp_xml_removedocument @hdoc
	SET @errorCount=@errorCount+@@ERROR

	--写入地接社信息
	IF(@ReleaseType='0')
	BEGIN
		DELETE FROM [tbl_TourLocalityInfo] WHERE [TourId] IN (SELECT TourId FROM @tmptbl)
		EXECUTE sp_xml_preparedocument @hdoc OUTPUT,@LocalTravelAgency
		INSERT INTO tbl_TourLocalityInfo(TourId,LocalComoanyID,LocalCompanyName,LicenseNumber,ContactTel,OperatorID)
		SELECT @TemplateTourId,A.AgencyId,A.AgencyName,A.License,A.Telephone,@OperatorID FROM OPENXML(@hdoc,'/ROOT/AgencyInfo') 
		WITH(AgencyId CHAR(36),AgencyName NVARCHAR(200),License NVARCHAR(250),Telephone NVARCHAR(100)) AS A
		EXECUTE sp_xml_removedocument @hdoc
		SET @errorCount=@errorCount+@@ERROR
	END

	--标记团队自动停收状态 非手动停收手动客满→自动停收
	UPDATE [tbl_TourList] SET [TourState]=3 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])<=GETDATE() AND TourState NOT IN(0,2)
	SET @errorCount=@errorCount+@@ERROR
    --标记团队正常状态 自动停收→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND DATEADD(DAY,-@AutoOffDays,[LeaveDate])>GETDATE() AND TourState IN(3)
	SET @errorCount=@errorCount+@@ERROR
	
	--标记团队状态 自动客满→正常
	UPDATE [tbl_TourList] SET [TourState]=1 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]>[OrderPeopleNumber] AND TourState=4
	SET @errorCount=@errorCount+@@ERROR
	--标记团队状态 正常→自动客满
	UPDATE [tbl_TourList] SET [TourState]=4 WHERE [ParentTourID]=@TemplateTourId AND [PlanPeopleCount]=[OrderPeopleNumber] AND TourState=1
	SET @errorCount=@errorCount+@@ERROR

	--标记最近出团的团、最近出团的数量等
	EXECUTE proc_TourList_RecentLeave @TourId=@TemplateTourId,@IsParentTour=N'1'

	--写入团队联系人信息
	IF NOT EXISTS(SELECT 1 FROM [tbl_TourContactInfo] WHERE [CompanyId]=@CompanyId AND [ContactName]=@TourContact)
	BEGIN
		INSERT INTO [tbl_TourContactInfo]([CompanyId],[ContactName],[ContactTel],[ContactQQ],[ContactMQId],[UserName])
		VALUES(@CompanyId,@TourContact,@TourContactTel,'',@TourContactMQ,@TourContactUserName)
		SET @errorCount=@errorCount+@@ERROR	
	END

	--更新团队订单的线路区域信息
	UPDATE tbl_TourOrder SET AreaId=@AreaId WHERE TourId IN(SELECT TourId FROM @tmptbl)
	SET @errorCount=@errorCount+@@ERROR	
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateStandardTourInfo
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateStandardTourInfo
		SET @Result=1
	END

	RETURN @Result
END
GO
--发布计划更改 end



--2011-11-24 QGroup update
GO
CREATE TABLE [dbo].[tbl_qqgroupmessage](
	[MId] [char](36) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Content] [nvarchar](max) NULL,
	[QGID] [varchar](20) NULL,
	[QUID] [varchar](20) NULL,
	[QMTime] [varchar](20) NULL,
	[IssueTime] [datetime] NOT NULL CONSTRAINT [DF__tbl_qqgro__Issue__780AAFAB]  DEFAULT (getdate()),
	[Status] [tinyint] NOT NULL CONSTRAINT [DF__tbl_qqgro__Statu__78FED3E4]  DEFAULT ((0)),
 CONSTRAINT [PK_TBL_QQGROUPMESSAGE] PRIMARY KEY CLUSTERED 
(
	[MId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'MId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'群号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'QGID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Q号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'QUID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送消息时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'QMTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'IssueTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_qqgroupmessage', @level2type=N'COLUMN',@level2name=N'Status'
GO

-- =============================================
-- Author:		<曹胡生>
-- Create date: <2011-06-22>
-- Description:	<获得MQ内嵌页供求信息列表>
-- History:
-- 2011-11-24 汪奇志 增加过滤条件，只显示供求信息，不显示QGroup信息
-- =============================================
ALTER PROCEDURE [dbo].[proc_GetMQSupplierInfoList]
	@ProvinceIds nvarchar(500),--同业中心选中的省份(多个逗号分隔)
	@Stime int,--时间（全部=0,今天=1,昨天=2,前天=3,更早=4）
	@ExchangeTitle nvarchar(500) --搜索的标题关键字
AS
BEGIN
	DECLARE @IsTopNum INT --置顶数
	BEGIN TRAN GetMQSupplierInfoList
		BEGIN
			--按省份搜索或不按省份搜索后缀条件，0表示不按省份搜索。
			DECLARE @Where NVARCHAR(MAX)
			IF(@ProvinceIds='0')
				SELECT @Where=' AND 1=1 AND [Category] IN(1,2)'
			ELSE
				BEGIN
				 SELECT @Where='ProvinceId in(select items from dbo.fn_split('''+@ProvinceIds+''','',''))'
				 --SELECT @Where=' AND EXISTS(SELECT 1 FROM tbl_ExchangeCityContact WHERE '+@Where+' AND ExchangeId=[tbl_ExchangeList].ID)'
				 SELECT @Where=' AND ID IN(SELECT ExchangeId FROM tbl_ExchangeCityContact WHERE '+@Where+') AND [Category] IN(1,2)'
				END
			--按标题关键字加时间搜索,最多查找40条
			IF(@ExchangeTitle IS NOT NULL AND LEN(@ExchangeTitle)>0)
				BEGIN
					IF(@Stime=0)--全部
						EXECUTE('SELECT TOP 40 * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE ExchangeTitle LIKE ''%'+@ExchangeTitle+'%'''+@Where+'
								ORDER BY IsTop DESC,TopTime DESC,IssueTime DESC')	
					ELSE IF(@Stime=1)--今天						
						EXECUTE('SELECT TOP 40 * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = 0 or DATEDIFF(dd,GETDATE(),TopTime) = 0) 
								AND ExchangeTitle LIKE ''%'+@ExchangeTitle+'%'''+@Where+'
								ORDER BY IsTop DESC,TopTime DESC,IssueTime DESC')
					ELSE IF(@Stime=2)--昨天						
						EXECUTE('SELECT TOP 40 * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = -1 or DATEDIFF(dd,GETDATE(),TopTime) = -1) 
								AND ExchangeTitle LIKE ''%'+@ExchangeTitle+'%'''+@Where+' 
								ORDER BY IsTop DESC,TopTime DESC,IssueTime DESC')
					ELSE IF(@Stime=3)--前天					
						EXECUTE('SELECT TOP 40 * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = -2 or DATEDIFF(dd,GETDATE(),TopTime) = -2) 
								AND ExchangeTitle LIKE ''%'+@ExchangeTitle+'%'''+@Where+' 
								ORDER BY IsTop DESC,TopTime DESC,IssueTime DESC')
					ELSE IF(@Stime=4)--更早						
						EXECUTE('SELECT TOP 40 * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE (DATEDIFF(dd,GETDATE(),IssueTime) <-2 or DATEDIFF(dd,GETDATE(),TopTime) < -2) 
								AND ExchangeTitle LIKE ''%'+@ExchangeTitle+'%'''+@Where+' 
								ORDER BY IsTop DESC,TopTime DESC,IssueTime DESC')
				END
			--按时间搜索,先取前5条置顶的，再取35条非置顶的,如果置顶的没有5条，则非置顶取40-置顶数。
			ELSE
				BEGIN		
					DECLARE @SQL NVARCHAR(1000)
					--获取置顶的数量@IsTopNum
					IF(@Stime=0)
						SET @SQL='SELECT @a=COUNT(*) FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
						WHERE IsTop=1 '+@Where
					ELSE IF(@Stime=1)					
						SET @SQL='SELECT @a=COUNT(*) FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
						WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = 0 or DATEDIFF(dd,GETDATE(),TopTime)=0) AND IsTop=1 '+@Where
					ELSE IF(@Stime=2)				
						SET @SQL='SELECT @a=COUNT(*) FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
						WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = -1 or DATEDIFF(dd,GETDATE(),TopTime)=-1) AND IsTop=1 '+@Where 
					ELSE IF(@Stime=3)					
						SET @SQL='SELECT @a=COUNT(*) FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
						WHERE (DATEDIFF(dd,GETDATE(),IssueTime) = -2 or DATEDIFF(dd,GETDATE(),TopTime)=-2) AND IsTop=1 '+@Where
					ELSE IF(@Stime=4)	
						SET @SQL='SELECT @a=COUNT(*) FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
						WHERE (DATEDIFF(dd,GETDATE(),IssueTime) <-2 or DATEDIFF(dd,GETDATE(),TopTime)<-2) AND IsTop=1 '+@Where
					EXEC sp_executesql @SQL,N'@a int output',@IsTopNum output
					IF(@IsTopNum>5) SET @IsTopNum=5
					IF(@Stime=0) 
						EXECUTE('SELECT * FROM (SELECT TOP '+@IsTopNum+' * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=1 '+@Where+' 
								ORDER BY TopTime DESC) A 
								UNION ALL
								SELECT * FROM (SELECT TOP (40-'+@IsTopNum+') * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=0 '+@Where+' 
								ORDER BY IssueTime DESC) B')
					ELSE IF(@Stime=1)							
						EXECUTE('SELECT * FROM (SELECT TOP '+@IsTopNum+' * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=1 AND (DATEDIFF(dd,GETDATE(),IssueTime) = 0 or DATEDIFF(dd,GETDATE(),TopTime)= 0) '+@Where+ ' 
								ORDER BY TopTime DESC) A 
								UNION ALL 
								SELECT * FROM (SELECT TOP (40-'+@IsTopNum+') * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=0 AND DATEDIFF(dd,GETDATE(),IssueTime) = 0 '+@Where+ ' 
								ORDER BY IssueTime DESC) B ')
					ELSE IF(@Stime=2)							
						EXECUTE('SELECT * FROM (SELECT TOP '+@IsTopNum+' * FROM [ImportPlatform].[dbo].[tbl_ExchangeList]
								WHERE IsTop=1 AND (DATEDIFF(dd,GETDATE(),IssueTime) = -1 or DATEDIFF(dd,GETDATE(),TopTime)= -1) '+@Where+' 
								ORDER BY TopTime DESC) A 
								UNION ALL 
								SELECT * FROM (SELECT TOP (40-'+@IsTopNum+') * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=0 AND DATEDIFF(dd,GETDATE(),IssueTime) = -1  '+@Where+' 
								ORDER BY IssueTime DESC) B ')
					ELSE IF(@Stime=3)							
						EXECUTE('SELECT * FROM (SELECT TOP '+@IsTopNum+' * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=1 AND (DATEDIFF(dd,GETDATE(),IssueTime) = -2 or DATEDIFF(dd,GETDATE(),TopTime)= -2) '+@Where+' 
								ORDER BY TopTime DESC) A 
								UNION  ALL 
								SELECT * FROM (SELECT TOP (40-'+@IsTopNum+') * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=0 AND DATEDIFF(dd,GETDATE(),IssueTime) = -2  '+@Where+' 
								ORDER BY IssueTime DESC) B ')
					ELSE IF(@Stime=4)							
						EXECUTE('SELECT * FROM (SELECT TOP '+@IsTopNum+' * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=1 AND (DATEDIFF(dd,GETDATE(),IssueTime) <-2 or DATEDIFF(dd,GETDATE(),TopTime)<-2) '+@Where+' 
								ORDER BY TopTime DESC) A 
								UNION  ALL 
								SELECT * FROM (SELECT TOP (40-'+@IsTopNum+') * FROM [ImportPlatform].[dbo].[tbl_ExchangeList] 
								WHERE IsTop=0 AND DATEDIFF(dd,GETDATE(),IssueTime) <-2  '+@Where+' 
								ORDER BY IssueTime DESC) B ')
				END
		END
	COMMIT TRAN
END
GO

ALTER TABLE dbo.tbl_qqgroupmessage ADD
	FUID varchar(50) NULL
GO
DECLARE @v sql_variant 
SET @v = N'登录Q号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_qqgroupmessage', N'COLUMN', N'FUID'
GO

--2011-11-24 QGroup update


--2011-12-28 汪奇志 常旅客表增加字段
GO
ALTER TABLE dbo.tbl_TicketVistorInfo ADD
	Mobile nvarchar(50) NULL,
	MailingAddress nvarchar(255) NULL,
	ZipCode nvarchar(50) NULL,
	Birthday datetime NULL,
	IDCard nvarchar(50) NULL,
	Passport nvarchar(255) NULL,
	CountryId int NOT NULL CONSTRAINT DF_tbl_TicketVistorInfo_CountryId DEFAULT 0,
	ProvinceId int NOT NULL CONSTRAINT DF_tbl_TicketVistorInfo_ProvinceId DEFAULT 0,
	CityId int NOT NULL CONSTRAINT DF_tbl_TicketVistorInfo_CityId DEFAULT 0,
	CountyId int NOT NULL CONSTRAINT DF_tbl_TicketVistorInfo_CountyId DEFAULT 0
GO
DECLARE @v sql_variant 
SET @v = N'手机号码'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'Mobile'
GO
DECLARE @v sql_variant 
SET @v = N'邮寄地址'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'MailingAddress'
GO
DECLARE @v sql_variant 
SET @v = N'邮编'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'ZipCode'
GO
DECLARE @v sql_variant 
SET @v = N'生日'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'Birthday'
GO
DECLARE @v sql_variant 
SET @v = N'身份证'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'IDCard'
GO
DECLARE @v sql_variant 
SET @v = N'护照'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'Passport'
GO
DECLARE @v sql_variant 
SET @v = N'国家编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'CountryId'
GO
DECLARE @v sql_variant 
SET @v = N'省份编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'ProvinceId'
GO
DECLARE @v sql_variant 
SET @v = N'城市编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'CityId'
GO
DECLARE @v sql_variant 
SET @v = N'县区编号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_TicketVistorInfo', N'COLUMN', N'CountyId'
GO

--==============================================================
-- Table: tbl_SysSetting 平台配置信息表 汪奇志 2011-12-30
--==============================================================
CREATE TABLE [dbo].[tbl_SysSetting](
	[Key] [varchar](50) NOT NULL,
	[Value] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TBL_SYSSETTING] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'setting key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SysSetting', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'setting value' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SysSetting', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台配置信息表 2011-12-29 created by wangqizhi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_SysSetting'
GO
