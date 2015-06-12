


/*
	2011-12-12  开始
*/




/*
	线路区域增加游览城市关系
	周文超 2011-12-19
*/
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_SysAreaVisitCity') and o.name = 'FK_TBL_SYSAREAVISITCITY_REFERENCE_TBL_SYSAREA')
alter table tbl_SysAreaVisitCity
   drop constraint FK_TBL_SYSAREAVISITCITY_REFERENCE_TBL_SYSAREA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_SysAreaVisitCity')
            and   type = 'U')
   drop table tbl_SysAreaVisitCity
go

/*==============================================================*/
/* Table: tbl_SysAreaVisitCity                                  */
/*==============================================================*/
create table tbl_SysAreaVisitCity (
   AreaId               int                  not null,
   CountryId            int                  not null default 0,
   ProvinceId           int                  not null default 0,
   CityId               int                  not null default 0,
   CountyId             int                  not null default 0
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '国家编号',
   'user', @CurrentUser, 'table', 'tbl_SysAreaVisitCity', 'column', 'CountryId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '省份编号',
   'user', @CurrentUser, 'table', 'tbl_SysAreaVisitCity', 'column', 'ProvinceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '城市编号',
   'user', @CurrentUser, 'table', 'tbl_SysAreaVisitCity', 'column', 'CityId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '县区编号',
   'user', @CurrentUser, 'table', 'tbl_SysAreaVisitCity', 'column', 'CountyId'
go

alter table tbl_SysAreaVisitCity
   add constraint FK_TBL_SYSAREAVISITCITY_REFERENCE_TBL_SYSAREA foreign key (AreaId)
      references tbl_SysArea (ID)
go






/*
	同业资讯表
	周文超 2011-12-19
*/

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_PeerNewsAttachInfo') and o.name = 'FK_TBL_PEERNEWSATTACHIN_REFERENCE_TBL_PEERNEWS')
alter table tbl_PeerNewsAttachInfo
   drop constraint FK_TBL_PEERNEWSATTACHIN_REFERENCE_TBL_PEERNEWS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_PeerNews')
            and   type = 'U')
   drop table tbl_PeerNews
go

/*==============================================================*/
/* Table: tbl_PeerNews                                          */
/*==============================================================*/
create table tbl_PeerNews (
   NewId                char(36)             not null,
   Title                nvarchar(255)        not null,
   TypeId               tinyint              not null default 0,
   Content              nvarchar(max)        null,
   CompanyId            char(36)             not null,
   CompanyName          nvarchar(255)        null,
   OperatorId           char(36)             not null,
   OperatorName         nvarchar(255)        null,
   B2BDisplay           tinyint              not null default 0,
   SortId               int                  not null default 50,
   ClickNum             int                  not null default 0,
   IP                   varchar(50)          null,
   IssueTime            datetime             not null default getdate(),
   LastUpdateTime       datetime             not null default getdate(),
   AreaId               int                  null,
   AreaName             nvarchar(255)        null,
   AreaType             tinyint              null,
   ScenicId             char(36)             null,
   constraint PK_TBL_PEERNEWS primary key (NewId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '2011-12-12  改版
   新同业资讯表',
   'user', @CurrentUser, 'table', 'tbl_PeerNews'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资讯编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'NewId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资讯标题',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'Title'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资讯类别   活动资讯、特价促销、同业资料、旅行动态',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'TypeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资讯内容',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'Content'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布公司编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'CompanyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布公司名称',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'CompanyName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布用户编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'OperatorId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布用户名称',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'OperatorName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B显示控制    首页 侧边 列表 常规 隐藏   默认常规',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'B2BDisplay'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B显示控制排序值',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'SortId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布IP',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'IP'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布时间',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'IssueTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'LastUpdateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '线路区域编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'AreaId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '线路区域名称',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'AreaName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '线路区域类型',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'AreaType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNews', 'column', 'ScenicId'
go




/*
	同业资讯附件表
	周文超 2011-12-19
*/
go
if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_PeerNewsAttachInfo') and o.name = 'FK_TBL_PEERNEWSATTACHIN_REFERENCE_TBL_PEERNEWS')
alter table tbl_PeerNewsAttachInfo
   drop constraint FK_TBL_PEERNEWSATTACHIN_REFERENCE_TBL_PEERNEWS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_PeerNewsAttachInfo')
            and   type = 'U')
   drop table tbl_PeerNewsAttachInfo
go

/*==============================================================*/
/* Table: tbl_PeerNewsAttachInfo                                */
/*==============================================================*/
create table tbl_PeerNewsAttachInfo (
   Id                   char(36)             not null,
   NewId                char(36)             not null,
   Type                 tinyint              not null default 0,
   Path                 nvarchar(255)        null,
   FileName             nvarchar(255)        null,
   Remark               nvarchar(255)        null,
   constraint PK_TBL_PEERNEWSATTACHINFO primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '同业资讯附件表',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资讯编号',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'NewId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件类型     图片、文件',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'Type'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件路径',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'Path'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件名称',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'FileName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '附件说明',
   'user', @CurrentUser, 'table', 'tbl_PeerNewsAttachInfo', 'column', 'Remark'
go

alter table tbl_PeerNewsAttachInfo
   add constraint FK_TBL_PEERNEWSATTACHIN_REFERENCE_TBL_PEERNEWS foreign key (NewId)
      references tbl_PeerNews (NewId)
go






GO

--==========================
--创建人：周文超 2011-12-20
--描述：同业资讯信息和发布用户信息
--==========================
CREATE view view_PeerNewsUserInfo
as
	select a.[NewId],a.[Title],a.[TypeId],a.[Content],a.[CompanyId],
	a.[CompanyName],a.[OperatorId],a.[OperatorName],a.[B2BDisplay],a.[SortId],a.[ClickNum],
	a.[IP],a.[IssueTime],a.[LastUpdateTime],b.[MQ],a.[AreaId],a.[AreaName],a.[AreaType],a.[ScenicId]
	from [tbl_PeerNews] as a left join [tbl_CompanyUser] as b on a.[OperatorId] = b.Id

go







/*
	公司基本信息表增加字段
	周文超 2011-12-19
*/
GO
ALTER TABLE dbo.tbl_CompanyInfo ADD
	Introduction nvarchar(255) NULL,
	Scale tinyint NOT NULL CONSTRAINT DF_tbl_CompanyInfo_Scale DEFAULT 0,
	Longitude decimal(18, 9) NOT NULL CONSTRAINT DF_tbl_CompanyInfo_Longitude DEFAULT 0,
	Latitude decimal(18, 9) NOT NULL CONSTRAINT DF_tbl_CompanyInfo_Latitude DEFAULT 0,
	PeerContact nvarchar(MAX) NULL,
	AlipayAccount nvarchar(255) NULL,
	B2BDisplay tinyint NOT NULL CONSTRAINT DF_tbl_CompanyInfo_B2BDisplay DEFAULT 0,
	B2BSort int NOT NULL CONSTRAINT DF_tbl_CompanyInfo_B2BSort DEFAULT 50,
	B2CDisplay tinyint NOT NULL CONSTRAINT DF_tbl_CompanyInfo_B2CDisplay DEFAULT 0,
	B2CSort int NOT NULL CONSTRAINT DF_tbl_CompanyInfo_B2CSort DEFAULT 50,
	ClickNum int NOT NULL CONSTRAINT DF_tbl_CompanyInfo_ClickNum DEFAULT 0,
	CompanyLev tinyint NOT NULL CONSTRAINT DF_tbl_CompanyInfo_CompanyLev DEFAULT 0,
	InfoFull decimal(8, 4) NOT NULL CONSTRAINT DF_tbl_CompanyInfo_InfoFull DEFAULT 0,
	ContractStart datetime NULL,
	ContractEnd datetime NULL
GO
DECLARE @v sql_variant 
SET @v = N'公司简称'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'Introduction'
GO
DECLARE @v sql_variant 
SET @v = N'公司规模   20人以下、20~100人、100~200人、200~500人、500人以上'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'Scale'
GO
DECLARE @v sql_variant 
SET @v = N'公司地址经度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'Longitude'
GO
DECLARE @v sql_variant 
SET @v = N'公司地址纬度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'Latitude'
GO
DECLARE @v sql_variant 
SET @v = N'同业联系方式'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'PeerContact'
GO
DECLARE @v sql_variant 
SET @v = N'支付宝账号'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'AlipayAccount'
GO
DECLARE @v sql_variant 
SET @v = N'B2B显示控制  首页 侧边 列表 常规 隐藏'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'B2BDisplay'
GO
DECLARE @v sql_variant 
SET @v = N'B2B显示排序'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'B2BSort'
GO
DECLARE @v sql_variant 
SET @v = N'B2C显示控制  首页 侧边 列表 常规 隐藏'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'B2CDisplay'
GO
DECLARE @v sql_variant 
SET @v = N'B2C显示排序'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'B2CSort'
GO
DECLARE @v sql_variant 
SET @v = N'网店点击量'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'ClickNum'
GO
DECLARE @v sql_variant 
SET @v = N'公司等级
签约免费入驻的商家
  网站初期合作签约的商户
  每个地区的专线有2个免费入驻名额
  显示推荐VIP标志
收费商户
  超越专线商2个限额的收费入住的商户
  显示推荐VIP标志
免费认证
  上传公司图片和认证资料的商户
  前台显示线路
在线审核
  初步电话审核
  权限 专线商可以发布线路，但是前台不显示线路，只在后台显示线路
在线注册
  网站上注册的会员，只能查看后台功能'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'CompanyLev'
GO
DECLARE @v sql_variant 
SET @v = N'资料完整度'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'InfoFull'
GO
DECLARE @v sql_variant 
SET @v = N'签约开始时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'ContractStart'
GO
DECLARE @v sql_variant 
SET @v = N'签约结束时间'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyInfo', N'COLUMN', N'ContractEnd'
GO




/*
	添加公司资质子表
	周文超 2011-12-20
*/
if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_CompanyQualification')
            and   type = 'U')
   drop table tbl_CompanyQualification
go

/*==============================================================*/
/* Table: tbl_CompanyQualification                              */
/*==============================================================*/
create table tbl_CompanyQualification (
   CompanyId            char(36)             not null,
   Qualification        tinyint              not null default 0,
   constraint PK_TBL_COMPANYQUALIFICATION primary key (CompanyId, Qualification)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'tbl_CompanyQualification', 'column', 'CompanyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '资质类型',
   'user', @CurrentUser, 'table', 'tbl_CompanyQualification', 'column', 'Qualification'
go




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
-- History:zhouwenchao  2011-12-22  修改内容:注册时添加字段（Introduction、Scale、Remark、CompanyLev、InfoFull、CompanyQualification）
-- =============================================
ALTER PROC [dbo].[proc_CompanyInfo_Insert]	
	@UserId CHAR(36),  --用户ID
	@CompanyId CHAR(36),   --公司ID
	@UserName NVARCHAR(100),  --用户名
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CityId INT,    --城市
	@CountyId INT,  --县区
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
	@ReturnValue INT OUTPUT,    --0:操作失败  1:操作成功   2:用户名数据已存在
	--以下为2011-12-20线路改版新加参数
	@Introduction nvarchar(255),	--公司简称
	@Scale tinyint,		--公司规模
	@Remark nvarchar(Max),		--公司介绍
	@CompanyLev tinyint,	--公司等级
	@InfoFull decimal(8,4),		--资料完整度
	@CompanyQualification nvarchar(Max)	--公司资质XML
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
		[Id],[ProvinceId],[CityId],[CountyId],[CompanyType],[CompanyName],[CompanyBrand],[CompanyAddress],[License],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[ContactMQ],[ContactQQ],[ContactMSN],[IsCheck],[CommendPeople],[ShortRemark],[Introduction],[Scale],[Remark],[CompanyLev],[InfoFull]
		)VALUES(
		@CompanyId,@ProvinceId,@CityId,@CountyId,@BusinessProperties,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,CAST(@MQID AS VARCHAR(20)),@ContactQQ,@ContactMSN,@IsCheck,@CommendPeople,@ShortRemark,@Introduction,@Scale,@Remark,@CompanyLev,@InfoFull
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
	IF(SELECT COUNT(*) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) WHERE TypeId in (5,6,7,8,9,10))>0
	begin
		SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=2;
	end
	else
	begin
		--根据身份查询特有权限
		SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;
	end 

	--查询通用服务的权限
	SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (97,98,99)

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

	--添加公司资质
	if @CompanyQualification is not null and len(@CompanyQualification) > 0
	begin
		EXEC sp_xml_preparedocument @hdocArea OUTPUT, @CompanyQualification;
		INSERT INTO [tbl_CompanyQualification](
			[CompanyId],[Qualification]
			) SELECT @CompanyID,Qualification
		FROM OPENXML(@hdocArea, N'/ROOT/CompanyQualification') 
		  WITH (Qualification tinyint);
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = 0;
			ROLLBACK TRAN Company_Insert;
			EXEC sp_xml_removedocument @hdocArea; 
			RETURN;
		END
		EXEC sp_xml_removedocument @hdocArea; 
	end
	
	--执行成功
	SET @ReturnValue = 1;
	SET @ReturnMQID = @MQID;
	COMMIT TRAN Company_Insert;
END
go






GO


-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-24
-- Description:	修改公司明细资料
-- @ReturnValue  0:操作失败  1:操作成功
-- History:zhangzy  2010-12-02  修改内容:若省份为“其他采购商”的时候，则权限等同与组团身份权限
-- History:wangqizhi  2011-04-01  修改内容:-位信息管理修改自己公司资料时能修改公司名称
-- History:zhouwenchao  2011-12-25  修改内容:公司表新加字段和单位资质、新版宣传图片的修改
-- =============================================
ALTER PROC [dbo].[proc_CompanyInfo_Update]	
	@UpdateType CHAR(1),   --修改类型:1:运营后台修改公司资料,2:单位信息管理修改自己公司资料,3:高级网店修改公司基本档案
	@MD5Password NVARCHAR(50),   --MD5密码
	@Password NVARCHAR(50),     --未加密密码
	@EncryptPassword NVARCHAR(128),   --SHA加密密码
	@CompanyId CHAR(36),   --公司ID
	@CityId INT,    --城市
	@CountyId INT,  --县区
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
	@ReturnValue CHAR(1) OUTPUT,    --0:操作失败  1:操作成功   2:用户名数据已存在
	--以下为2011-12-20线路改版新加参数
	@Introduction nvarchar(255),	--公司简称
	@Scale tinyint,		--公司规模
	@CompanyLev tinyint,	--公司等级
	@InfoFull decimal(8,4),		--资料完整度
	@AlipayAccount nvarchar(255),	--支付宝账号
	@Longitude decimal(18,9),		--公司地址经度
	@Latitude decimal(18,9),		--公司地址纬度
	@PeerContact nvarchar(max),		--同业联系方式
	@WarrantImg	NVARCHAR(250),  --授权证书
	@PersonCardImg NVARCHAR(250),  --负责人身份证
	@B2BDisplay tinyint,	--B2B显示控制
	@B2BSort int,	--B2B显示排序
	@B2CDisplay tinyint,	--B2C显示控制
	@B2CSort int,	--B2C显示排序
	@ContractStart datetime,	--签约开始时间
	@ContractEnd datetime,		--签约结束时间
	@WebSite nvarchar(250),		--公司网址
	@CompanyQualification nvarchar(Max),	--公司资质XML
	@CompanyPublicityPhoto nvarchar(Max)	--新版公司宣传图片
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
	DECLARE @CCompanyName NVARCHAR(255)
	DECLARE @Photohdoc int; --新版公司宣传图片SqlXml变量
	
	SELECT @CCompanyName=CompanyName FROM tbl_CompanyInfo WHERE Id=@CompanyId
	IF(@CompanyName IS NULL OR LEN(@CompanyName)<1)
	BEGIN
		SET @CompanyName=@CCompanyName
	END

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
		UPDATE [tbl_CompanyInfo] SET [ProvinceId]=@ProvinceId,[CityId]=@CityId,[CountyId] = @CountyId,[CompanyType]=@BusinessProperties,[CompanyName]=@CompanyName,[CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[License]=@License,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark,[ShortRemark]=@ShortRemark,[Introduction] = @Introduction,[Scale] = @Scale,[Longitude] = @Longitude,[Latitude] = @Latitude,[PeerContact] = @PeerContact,[AlipayAccount] = @AlipayAccount,[B2BDisplay] = @B2BDisplay,[B2BSort] = @B2BSort,[B2CDisplay] = @B2CDisplay,[B2CSort] = @B2CSort,[CompanyLev] = @CompanyLev,[InfoFull] = @InfoFull,[ContractStart] = @ContractStart,[ContractEnd] = @ContractEnd,[WebSite] = @WebSite
		WHERE Id=@CompanyId 
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
			IF(SELECT COUNT(*) FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT) WHERE TypeId in (5,6,7,8,9,10))>0
			begin
				SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=2;
			end
			else
			begin
				--根据身份查询特有权限
				SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList AS list,(SELECT TypeId FROM OPENXML(@hdocCompanyType, N'/ROOT/CompanyTypeList') WITH (TypeId TINYINT)) AS companyType WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId=companyType.TypeId;
			end 

			--查询通用服务的权限
			SELECT @PermissionList=@PermissionList + CAST(list.Id AS NVARCHAR(10)) + ',' FROM tbl_SysPermissionList list WHERE list.CategoryId=1 AND list.IsEnable='1' AND list.ClassId IN (97,98,99)

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
		UPDATE [tbl_CompanyInfo] SET [ProvinceId]=@ProvinceId,[CityId]=@CityId,[CountyId] = @CountyId,[CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark, [Introduction] = @Introduction,[Longitude] = @Longitude,[Latitude] = @Latitude,[PeerContact] = @PeerContact,[AlipayAccount] = @AlipayAccount,[InfoFull] = @InfoFull,[WebSite] = @WebSite,[Scale] = @Scale
		WHERE Id=@CompanyId 
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
	IF(@UpdateType='1')  --运营后台->证书管理(营业执照,经营许可证,税务登记证、授权证书、负责人身份证、新版公司宣传图片)
	BEGIN
		--先删除,再新增		
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='WarrantImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='WarrantImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='PersonCardImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='PersonCardImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND [FieldValue]=@CompanyLogo)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND FieldValue<>''		

		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('LicenceImg','BusinessCertImg','TaxRegImg','WarrantImg','PersonCardImg','CompanyLogo');
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'LicenceImg',@LicenceImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'BusinessCertImg',@BusinessCertImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'TaxRegImg',@TaxRegImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'WarrantImg',@WarrantImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'PersonCardImg',@PersonCardImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'CompanyLogo',@CompanyLogo);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		

		--新版公司宣传图片处理
		if @CompanyPublicityPhoto is not null and len(@CompanyPublicityPhoto) > 0
		begin
			EXEC sp_xml_preparedocument @Photohdoc OUTPUT, @CompanyPublicityPhoto;
			--大图
			IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int)))=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND FieldValue<>'' AND [FieldName] in (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int))
			DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN 
			(SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM 
				OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') 
				WITH (PhotoIndex int));
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--缩略图
			IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int)))=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND FieldValue<>'' AND [FieldName] in (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int))
			DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN 
			(SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM 
				OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') 
				WITH (PhotoIndex int));
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--添加大图
			INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) select @CompanyId,'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)),ImagePath from OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int,ImagePath nvarchar(250))
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--添加小图
			INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) select @CompanyId,'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb',ThumbPath from OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int,ThumbPath nvarchar(250))
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
		end
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @Photohdoc; 
			RETURN;
		END
		EXEC sp_xml_removedocument @Photohdoc; 
	END
	ELSE IF(@UpdateType='2')  --单位信息管理->宣传图片,企业LOGO,企业公章,营业执照,经营许可证,税务登记证、授权证书、负责人身份证、新版公司宣传图片
	BEGIN
		--先删除,再新增
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND [FieldValue]=@CompanyImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND [FieldValue]=@CompanyLogo)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='CompanyLogo' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND [FieldValue]=@LicenceImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='LicenceImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND [FieldValue]=@BusinessCertImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='BusinessCertImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='TaxRegImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='WarrantImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='WarrantImg' AND FieldValue<>''
		IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='PersonCardImg' AND [FieldValue]=@TaxRegImg)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName]='PersonCardImg' AND FieldValue<>''	
		DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN ('CompanyImg','CompanyLogo','CompanySignet','LicenceImg','BusinessCertImg','TaxRegImg','WarrantImg','PersonCardImg');
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
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'WarrantImg',@WarrantImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) VALUES(@CompanyId,'PersonCardImg',@PersonCardImg);
		SET @ErrorAttach = @ErrorAttach + @@ERROR;
		
		--新版公司宣传图片处理
		if @CompanyPublicityPhoto is not null and len(@CompanyPublicityPhoto) > 0
		begin
			EXEC sp_xml_preparedocument @Photohdoc OUTPUT, @CompanyPublicityPhoto;
			--大图
			IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int)))=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND FieldValue<>'' AND [FieldName] in (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int))
			DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN 
			(SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) FROM 
				OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') 
				WITH (PhotoIndex int));
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--缩略图
			IF (SELECT COUNT(*) FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int)))=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [FieldValue] FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND FieldValue<>'' AND [FieldName] in (SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int))
			DELETE FROM [tbl_CompanyAttachInfo] WHERE [CompanyId]=@CompanyId AND [FieldName] IN 
			(SELECT 'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb' FROM 
				OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') 
				WITH (PhotoIndex int));
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--添加大图
			INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) select @CompanyId,'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)),ImagePath from OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int,ImagePath nvarchar(250))
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
			--添加小图
			INSERT INTO [tbl_CompanyAttachInfo]([CompanyId],[FieldName],[FieldValue]) select @CompanyId,'PublicityPhoto' + cast(PhotoIndex as nvarchar(10)) + 'Thumb',ThumbPath from OPENXML(@Photohdoc, N'/ROOT/CompanyPublicityPhoto') WITH (PhotoIndex int,ThumbPath nvarchar(250))
			SET @ErrorAttach = @ErrorAttach + @@ERROR;
		end
			
		IF @ErrorAttach <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN CompanyDetailInfo_Update;
			EXEC sp_xml_removedocument @Photohdoc; 
			RETURN;
		END
		EXEC sp_xml_removedocument @Photohdoc; 
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

	IF @UpdateType = '1' or @UpdateType = '2'  --运营后台、用户后台,修改公司资质
	begin
		if @CompanyQualification is not null and len(@CompanyQualification) > 0
		begin
			--删除原来的公司资质
			delete from [tbl_CompanyQualification] where [CompanyId] = @CompanyID;
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update; 
				RETURN;
			END
			EXEC sp_xml_preparedocument @hdocArea OUTPUT, @CompanyQualification;
			--添加新的公司资质
			INSERT INTO [tbl_CompanyQualification]([CompanyId],[Qualification]) SELECT @CompanyID,Qualification
			FROM OPENXML(@hdocArea, N'/ROOT/CompanyQualification') WITH (Qualification tinyint);
			IF @@ERROR <> 0 
			BEGIN
				SET @ReturnValue = '0'; 
				ROLLBACK TRAN CompanyDetailInfo_Update; 
				EXEC sp_xml_removedocument @hdocArea; 
				RETURN;
			END
			EXEC sp_xml_removedocument @hdocArea; 
		end
	end
	
	--执行成功		
	SET @ReturnValue = '1';
	COMMIT TRAN CompanyDetailInfo_Update;
END

go



/*
	用户表增加公司职位字段
	周文超 2011-12-20
*/
GO
ALTER TABLE dbo.tbl_CompanyUser ADD
	Job nvarchar(255) NULL
GO
DECLARE @v sql_variant 
SET @v = N'公司职位'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tbl_CompanyUser', N'COLUMN', N'Job'
GO





GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	新增公司子帐号信息
-- @ReturnValue  0:操作失败  1:操作成功   2:用户名数据已存在
-- history: 
-- 1.@ContactQQ字段长度有50调整为250   2010-11-8
-- 2.汪奇志 2011-04-08 角色编号为空时默认取公司管理员角色，用于系统整合，因大平台创建子账号时角色必选将不受影响
-- 3.周文超 2011-12-20 增加职位字段，将MQ昵称座位单独参数传递，与联系人名称分开
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
	@ReturnValue CHAR(1) OUTPUT,    --0:操作失败  1:操作成功   2:用户名数据已存在
	@Job nvarchar(255),		--公司职位
	@MqNickName nvarchar(100)	--MQ昵称
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
	if @MqNickName is null or len(@MqNickName) <= 0
		set @MqNickName = @ContactName;
	--插入MQ
	INSERT INTO [im_member](
	[im_uid],[im_password],[im_displayname],[im_username],[bs_uid]
	)VALUES(
	@MQID,@MD5Password,@MqNickName,@UserName,@UserId
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
		[Id],[CompanyId],[ProvinceId],[CityId],[UserName],[Password],[EncryptPassword],[MD5Password],[ContactName],[ContactSex],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[QQ],[MQ],[MSN],[RoleID],[IsAdmin],[DepartId],[DepartName],[Job])VALUES(
		@UserId,@CompanyId,@ProvinceId,@CityId,@UserName,@Password,@EncryptPassword,@MD5Password,@ContactName,@ContactSex,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,@ContactQQ,@MQID,@ContactMSN,@RoleID,@IsAdmin,@DepartId,@DepartName,@Job);

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

go



GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-06-08
-- Description:	修改公司帐号信息
-- @ReturnValue  0:操作失败  1:操作成功 
-- history: @ContactQQ字段长度有50调整为250   2010-11-8
-- history: 周文超 2011-12-20 增加职位字段，将MQ昵称座位单独参数传递，与联系人名称分开
-- =============================================
ALTER PROC [dbo].[proc_CompanyUser_Update]	
	@Password varchar(50),    --未加密密码,若为空则不修改
	@EncryptPassword nvarchar(128),   --SHA加密密码,若为空则不修改
	@MD5Password nvarchar(50),   --MD5加密密码,若为空则不修改
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
	@UserId NVARCHAR(36),  --用户ID
	@CompanyID NVARCHAR(36),  --公司ID
	@XMLRootArea NVARCHAR(MAX), --用户线路区域的sqlxml脚本
	@ReturnValue CHAR(1) OUTPUT,    --0:操作失败  1:操作成功
	@Job nvarchar(255),		--公司职位
	@MqNickName nvarchar(100)	--MQ昵称
AS
BEGIN	
	DECLARE @SQL NVARCHAR(MAX);
	DECLARE @SQLIM NVARCHAR(MAX);
	SET @SQLIM = '';
	SET @SQL = '';

	--开始事务
	BEGIN TRAN User_Update
	--修改用户表
	SET @SQL = 'UPDATE [tbl_CompanyUser] SET [ContactName]=''' + isnull(@ContactName,'') + ''',[ContactSex]=''' + isnull(@ContactSex,'') + ''',[ContactTel]=''' + isnull(@ContactTel,'') + ''',[ContactFax]=''' + isnull(@ContactFax,'') + ''',[ContactMobile]=''' + isnull(@ContactMobile,'') + ''',[ContactEmail]=''' + isnull(@ContactEmail,'') + ''',[QQ]=''' + isnull(@ContactQQ,'') + ''',[MSN]=''' + isnull(@ContactMSN,'') + '''';

	--判断是否要修改密码
	IF(@Password is not null and @Password<>'')
	BEGIN
		SET @SQL = @SQL + ',[Password]=''' + @Password + ''',[EncryptPassword]=''' + @EncryptPassword + ''',[MD5Password]=''' + @MD5Password + '''';	
		UPDATE im_member SET im_password=@MD5Password WHERE bs_uid=@UserId;
		IF @@ERROR <> 0 
		BEGIN
			SET @ReturnValue = '0'; 
			ROLLBACK TRAN User_Update;
			RETURN;
		END		
	END

	--判断是否要修改角色,角色ID不能空,才进行修改
	IF(@RoleID is not null and @RoleID<>'')
	BEGIN
		SET @SQL = @SQL + ',[RoleID]=''' + @RoleID + '''';
	END	
	--修改部门
	SET @SQL = @SQL + ',[DepartId]=''' + isnull(@DepartId,'') + ''',[DepartName]=''' + isnull(@DepartName,'') + '''';
	--修改职位
	set @SQL = @SQL + ',[Job] = ''' + isnull(@Job ,'')+ ''' ';
	--设置修改条件
	SET @SQL = @SQL + ' WHERE Id=''' + @UserId + ''' AND CompanyID=''' + @CompanyID + '''';

	--修改MQ昵称
	if @MqNickName is null or len(@MqNickName) <= 0
		set @MqNickName = @ContactName;
	UPDATE im_member SET [im_displayname]=@MqNickName WHERE bs_uid=@UserId; 
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN User_Update;
		RETURN;
	END

	--执行sql语句
	execute sp_executesql @SQL
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 
		ROLLBACK TRAN User_Update;
		RETURN;
	END	

	DECLARE @hdocArea int;
	EXEC sp_xml_preparedocument @hdocArea OUTPUT, @XMLRootArea;	
	--先删除用户不存在了的线路区域关系
	DELETE tbl_CompanyUserAreaControl WHERE UserId=@UserId AND CompanyId=@CompanyID AND AreaId NOT IN (SELECT AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT));
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0'; 		
		ROLLBACK TRAN User_Update;		
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END	
	--添加用户新的线路区域	
	INSERT INTO [tbl_CompanyUserAreaControl](
		[UserId],[CompanyId],[AreaId]
		) SELECT @UserId,@CompanyID,A.AreaId FROM OPENXML(@hdocArea, N'/ROOT/Area') WITH (AreaId INT) AS A WHERE A.AreaId NOT IN (SELECT AreaId FROM [tbl_CompanyUserAreaControl] WHERE [CompanyId]=@CompanyID AND [UserId]=@UserId);
	IF @@ERROR <> 0 
	BEGIN
		SET @ReturnValue = '0';
		ROLLBACK TRAN User_Update;		
		EXEC sp_xml_removedocument @hdocArea; 
		RETURN;
	END	
	EXEC sp_xml_removedocument @hdocArea; 

	SET @ReturnValue = '1';
	COMMIT TRAN User_Update;
END

go



/*
	周文超 2011-12-20线路改版找同行视图修改
*/

GO
ALTER VIEW [dbo].[View_TongHang]
AS
SELECT     a.Id, a.CompanyName, a.ContactQQ, a.ContactMQ, a.CompanyAddress, a.ContactTel, a.ContactFax, a.ContactName, a.IssueTime, a.ProvinceId, a.CityId,a.CountyId,a.CompanyLev,a.B2BDisplay,a.B2BSort,a.InfoFull,a.LastLoginTime,a.OPCompanyId,
                          (SELECT     ProvinceName
                            FROM          dbo.tbl_SysProvince AS p
                            WHERE      (ID = a.ProvinceId)) AS ProvinceName,
                          (SELECT     CityName
                            FROM          dbo.tbl_SysCity AS c
                            WHERE      (ID = a.CityId)) AS CityName, 
						(select	  DistrictName 
								from tbl_sysDistrictCounty as dc
								where dc.Id = a.CountyId) as CountyName,
						(select top 1 FieldValue 
								from tbl_CompanyAttachInfo as cai
								 where cai.CompanyId = a.Id 
							and cai.FieldName = 'CompanyLogo') as CompanyLogo,
                      (CASE WHEN EXISTS
                          (SELECT     1
                            FROM          tbl_CompanyPayService
                            WHERE      serviceid = 2 AND companyid = a.id) THEN 1 ELSE 0 END) AS serviceid,
                          (SELECT     typeid
                            FROM          tbl_CompanyTypeList
                            WHERE      companyid = a.id FOR xml auto, root('CompanyTypes')) AS CompanyTypes
FROM         dbo.tbl_CompanyInfo AS a
WHERE     a.IsCheck = 1 AND a.IsDeleted = '0' AND a.id IN
                          (SELECT     companyid
                            FROM          tbl_CompanyTypeList
                            WHERE      typeid IN (1, 2, 3))
go





/*
	新版资讯增加类别  用户后台公告
	周文超 2011-12-30
*/

go

INSERT INTO [tbl_NewsClass] ([Category],[ClassName],[IsSystem],[OperatorId],[IssueTime])
 VALUES (3,'用户后台公告','1',0,getdate())
 
go


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



GO
/* =============================================
 Author:		张志瑜
 Create date: 2010-07-10
 Description:	查询所有我的客户公司信息
 =============================================*/
ALTER VIEW [dbo].[view_CompanyCustomerList]
AS
SELECT     customer.ID, customer.CompanyID, customer.OperatorID, customer.CustomerCompanyID, customer.IssueTime, c.ProvinceId, c.CityId, 
                      c.CompanyName, c.CompanyBrand, c.ContactName, c.ContactTel, c.ContactFax, c.ContactMobile, c.ContactMQ, c.CompanyType, c.ContactEmail, 
                      c.ContactQQ, c.ContactMSN, c.License,c.OpCompanyId
FROM         dbo.tbl_CompanyCustomerList AS customer INNER JOIN
                      dbo.tbl_CompanyInfo AS c ON customer.CustomerCompanyID = c.Id
WHERE     (c.IsDeleted = '0') AND (c.IsCheck = '1') AND (c.IsEnabled = '1')



go


go


--城市补充
INSERT INTO [tbl_SysCity] ([ID],[ProvinceId],[CityName],[CenterCityId],[HeaderLetter],[ParentTourCount],[TourCount],[IsSite],[DomainName],[IsEnabled],[IsExtendSite],[RewriteCode])
     VALUES (374,31,'阿勒泰',343,'aletai',0,0,0,'aletai.tongye114.com',0,1,'aletai')
     

go
     
     
     
     
     
GO
-- =============================================
-- Author:		周文超
-- Create date: 2012-2-13
-- Description: 运营后台个人会员管理
-- =============================================
create VIEW view_CompanyUserInfo
AS
SELECT 	
	Id,CompanyId,UserName,ContactName,ContactSex,ContactMobile,QQ,MQ,IssueTime,LastLoginTime,IsDeleted,CityId,ProvinceId
    ,(SELECT CompanyName FROM tbl_CompanyInfo AS A WHERE A.Id=tbl_CompanyUser.CompanyId) AS CompanyName
    ,(SELECT COUNT(*) FROM tbl_LogUserLogin AS A WHERE A.OperatorId=tbl_CompanyUser.Id) AS LoginTimes
FROM [tbl_CompanyUser]
where IsDeleted = '0'

go




go

--delete from tbl_SystemLandMark where Id = 840
--delete from tbl_SystemLandMark where Id = 835
--delete from tbl_SystemLandMark where Id = 834
--delete from tbl_SystemLandMark where Id = 836

go



go

update tbl_SysAdvArea set AdvCount = 6 where AreaId = 73 
update tbl_SysAdvArea set AdvCount = 5 where AreaId = 1

go





GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-17
-- Description:	城市团队数量维护
-- =============================================
ALTER PROCEDURE [dbo].[SQLPlan_SysCity_TourNumber]
	
AS
BEGIN
	--城市编号
	DECLARE @CityId INT
	--子团数量
	DECLARE @CityChildrenTourNumber INT
	--模板团数量
	DECLARE @CityTemplateTourNumber INT

	DECLARE tmpcursor CURSOR
	FOR SELECT Id FROM tbl_SysCity
	OPEN tmpcursor
	
	FETCH NEXT FROM tmpcursor INTO @CityId
	
	WHILE(@@FETCH_STATUS=0)
	BEGIN
		/*
		SELECT @CityTemplateTourNumber=COUNT(*) FROM tbl_TourList AS A
		WHERE A.LeaveDate>=GETDATE() AND A.IsDelete='0' AND A.IsRecentLeave='1'
		AND EXISTS(SELECT 1 FROM tbl_TourCityControl AS B WHERE B.TourId=A.Id AND B.CityId=@CityId) 
		*/

		select @CityTemplateTourNumber = count(Id) from tbl_NewRouteBasicInfo as a where a.IsDeleted = '0' 
		and B2B <> 99 and exists (select 1 from tbl_NewRouteCityControl as b where b.RouteId = a.RouteId 
			and b.CityId = @CityId)
		
		/*
		SELECT @CityChildrenTourNumber=COUNT(*) FROM tbl_TourList AS A
		WHERE A.LeaveDate>=GETDATE() AND IsDelete='0'
		AND EXISTS(SELECT 1 FROM tbl_TourCityControl AS B WHERE B.TourId=A.Id AND B.CityId=@CityId)
		*/
		
		--散拼计划
		SELECT @CityChildrenTourNumber = COUNT(TourId) FROM tbl_NewPowderList as a where a.IsDeleted = '0' 
		and B2B <> 99 and a.LeaveDate >= GETDATE() and exists (select 1 from tbl_NewTourCityControl as b 
			where b.TourId = a.TourId and b.CityId = @CityId)

		--团队计划
		select @CityChildrenTourNumber = isnull(@CityChildrenTourNumber,0) + count(TourId) from tbl_NewTourList as a where a.RouteId in (select RouteId from tbl_NewRouteCityControl as b where b.CityId = @CityId)
		
		UPDATE tbl_SysCity SET ParentTourCount=@CityTemplateTourNumber,TourCount=@CityChildrenTourNumber WHERE Id=@CityId
		
		FETCH NEXT FROM tmpcursor INTO @CityId
	END

	CLOSE tmpcursor
	DEALLOCATE tmpcursor
	
	--全国模板团数量
	DECLARE @TemplateTourNumber INT
	SELECT @TemplateTourNumber=SUM(ParentTourCount) FROM tbl_SysCity
	UPDATE tbl_SysSummaryCount SET FieldValue=@TemplateTourNumber WHERE FieldName='Route'
END


go
     
     
     
     


