
GO
/****** 对象:  Table [dbo].[tbl_Country]    脚本日期: 05/27/2011 17:11:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Areas] [int] NOT NULL,
	[CountryCn] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CountryEn] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CountryJp] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[CountryCd] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[FlagPath] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_TBL_COUNTRY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'洲名【欧洲、亚洲、美洲、非洲】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'Areas'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家中文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'CountryCn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家英文名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'CountryEn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家简拼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'CountryJp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'CountryCd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家国旗' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Country', @level2type=N'COLUMN',@level2name=N'FlagPath'





GO
/****** 对象:  Table [dbo].[tbl_Visa]    脚本日期: 05/27/2011 17:12:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Visa](
	[CountryID] [int] NOT NULL,
	[EmbassyInfo] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[HintInfo] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FilePath] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_TBL_VISA] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家编号编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Visa', @level2type=N'COLUMN',@level2name=N'CountryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使馆信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Visa', @level2type=N'COLUMN',@level2name=N'EmbassyInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特别提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Visa', @level2type=N'COLUMN',@level2name=N'HintInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请表下载' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_Visa', @level2type=N'COLUMN',@level2name=N'FilePath'
GO
ALTER TABLE [dbo].[tbl_Visa]  WITH CHECK ADD  CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_COUN] FOREIGN KEY([CountryID])
REFERENCES [dbo].[tbl_Country] ([Id])
GO
ALTER TABLE [dbo].[tbl_Visa] CHECK CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_COUN]




GO
/****** 对象:  Table [dbo].[tbl_VisaLink]    脚本日期: 05/27/2011 17:12:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_VisaLink](
	[ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CountryID] [int] NULL,
	[LinkTile] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[LinkUrl] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_TBL_VISALINK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaLink', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家编号编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaLink', @level2type=N'COLUMN',@level2name=N'CountryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaLink', @level2type=N'COLUMN',@level2name=N'LinkTile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'站点连接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaLink', @level2type=N'COLUMN',@level2name=N'LinkUrl'
GO
ALTER TABLE [dbo].[tbl_VisaLink]  WITH CHECK ADD  CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_LINK] FOREIGN KEY([CountryID])
REFERENCES [dbo].[tbl_Visa] ([CountryID])
GO
ALTER TABLE [dbo].[tbl_VisaLink] CHECK CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_LINK]



GO
/****** 对象:  Table [dbo].[tbl_VisaSub]    脚本日期: 05/31/2011 11:55:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_VisaSub](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NULL,
	[FileType] [tinyint] NOT NULL,
	[FileInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_TBL_VISASUB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaSub', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'国家编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaSub', @level2type=N'COLUMN',@level2name=N'CountryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签证资料类型【个人身份证明、资产证明、工作证明、学生及儿童、老人、其他】' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaSub', @level2type=N'COLUMN',@level2name=N'FileType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签证资料信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbl_VisaSub', @level2type=N'COLUMN',@level2name=N'FileInfo'
GO
ALTER TABLE [dbo].[tbl_VisaSub]  WITH CHECK ADD  CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_COUNTRY] FOREIGN KEY([CountryId])
REFERENCES [dbo].[tbl_Country] ([Id])
GO
ALTER TABLE [dbo].[tbl_VisaSub] CHECK CONSTRAINT [FK_TBL_VISA_REFERENCE_TBL_COUNTRY]

GO

/*==============================================================*/
/* Table: tbl_NewHotelInfo                                      */
/*==============================================================*/
create table tbl_NewHotelInfo (
   Id                   int                  identity,
   OrderId              int                  not null,
   HotelName            nvarchar(255)        not null,
   CityName             nvarchar(20)         not null,
   CityAreaType         tinyint              not null,
   HotelStar            tinyint              not null,
   MenShiPrice          money                not null default 0,
   TeamPrice            money                null default 0,
   QQ                   nvarchar(50)         not null,
   OperateId            int                  not null,
   OperateTime          datetime             not null default getdate(),
   constraint PK_TBL_NEWHOTELINFO primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'OrderId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '酒店名称',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'HotelName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '酒店所在城市',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'CityName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '华东五市,港澳',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'CityAreaType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '一星,二星,三星,四星,五星',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'HotelStar'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '门市价格',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'MenShiPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '团队价格',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'TeamPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'QQ',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'QQ'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布人',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'OperateId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布时间',
   'user', @CurrentUser, 'table', 'tbl_NewHotelInfo', 'column', 'OperateTime'
go


/*==============================================================*/
/* Table: tbl_SupplyDemandRules                                 */
/*==============================================================*/
create table tbl_SupplyDemandRules (
   Id                   int                  identity,
   NewsTitle            nvarchar(255)         null,
   LinkAddress          nvarchar(255)        null,
   NewsContent          nvarchar(1000)        null,
   Category             tinyint              not null,
   PicPath              nvarchar(255)         null,
   OperateId            int                  null,
   OperateTime          datetime             null default getdate(),
   constraint PK_TBL_SUPPLYDEMANDRULES primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新闻标题',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'NewsTitle'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '链接地址',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'LinkAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新闻概要',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'NewsContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '供求规则，供求图片',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'Category'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片路径',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'PicPath'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作人',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'OperateId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作时间',
   'user', @CurrentUser, 'table', 'tbl_SupplyDemandRules', 'column', 'OperateTime'
go




GO

-- =============================================
-- Author:		<曹胡生>
-- Create date: <2011-05-11>
-- Description:	<获得首页推广产品>
-- =============================================
CREATE PROCEDURE [dbo].[proc_SelectExtendProduct]
  @CityID int --城市编号
AS
BEGIN
	declare @error int 
	set @error=0
	BEGIN Transaction
		if(NOT Exists(select 1 from tbl_ExtendProduct where ShowCityID=@CityID))
			--从线路产品表选择某城市的公司发布的前五个已审核,未删除,收费公司发布产品优先显示,tbl_ExtendProduct
			insert into tbl_ExtendProduct(SortId,CompanyID,ProductID,ShowCityID,ShowProID) 
			select  ROW_NUMBER() OVER(ORDER BY PayCount DESC) AS RowNumber,B.CompanyID,B.ProductID,@CityID,B.ProvinceId from
			(select top(5) CompanyID,Id as ProductID,
			(SELECT ProvinceId FROM [tbl_SysCity] WHERE Id=@cityid) as ProvinceId,
			(select count(*) from tbl_CompanyPayService where CompanyId=[tbl_TourList].CompanyID and IsEnabled='1') as PayCount
			from [tbl_TourList] where  IsChecked='1' and IsDelete='0' and Exists
			(select TourId from tbl_TourAreaControl where TourId=[tbl_TourList].ID and CityId=@cityid) order by PayCount desc
			) B
		
			execute('select *,
					(select CityName from tbl_SysCity where ID=tbl_ExtendProduct.ShowCityID) as cityname,
					(select RouteName,CompanyName,TourContacMQ,RetailAdultPrice from tbl_TourList 
					where ID=tbl_ExtendProduct.ProductID FOR XML RAW,ROOT(''ROOT'')) as ProductInfo
					from tbl_ExtendProduct where ShowCityID='+@CityID+' order by SortID ASC')
		if(@error>0)
			rollback tran
		else
			commit tran
END

GO


-- =============================================
-- Author:		<曹胡生>
-- Create date: <2011-05-09>
-- Description:	<获得分站旅行社推广列表>
-- =============================================
CREATE PROCEDURE [dbo].[proc_SiteExtendList]
  @CityID int, --城市编号
 @IsShow char(1) --是否显示
AS
BEGIN
	declare @error int 
	set @error=0
	BEGIN Transaction SiteExtendList
		if(NOT Exists(select 1 from tbl_FenSiteExtend where ShowCityID=@CityID))
			--从会员表选择已审核，优先显示有产品的公司，最多21多
			insert into tbl_FenSiteExtend(SortId,ShowCity,ShowCityId,ShowProId,CompanyID) 
			select  ROW_NUMBER() OVER(ORDER BY ProductNum DESC,LoginCount desc) AS RowNumber,B.CityName,B.CityId,B.ProvinceId,B.Id from
			(select top(21) Id,CityId,ProvinceId,LoginCount,
			(select count(*) from [tbl_TourList] where CompanyID=tbl_CompanyInfo.Id) as ProductNum,
			(SELECT CityName FROM [tbl_SysCity] WHERE Id=tbl_CompanyInfo.CityId) as CityName
			from tbl_CompanyInfo where CityId=@CityID 
			and IsCheck='1' and IsDeleted='0' 
			order by ProductNum desc,LoginCount desc) B
			if(@IsShow is not null)
			execute('select *,
				(SELECT COUNT(*) FROM [tbl_TourList] WHERE CompanyID=tbl_FenSiteExtend.CompanyID) as ProductNum,
				(SELECT CompanyName FROM [tbl_CompanyInfo] WHERE ID=tbl_FenSiteExtend.CompanyID) as CompanyName,
				(SELECT CityName FROM [tbl_SysCity] WHERE Id=tbl_FenSiteExtend.ShowCityID) as CityName,
				(SELECT LoginCount FROM [tbl_CompanyInfo] WHERE Id=tbl_FenSiteExtend.CompanyID) as LoginCount 
				from tbl_FenSiteExtend where ShowCityID='+@CityID+' and IsShow ='+@IsShow+' order by SortId ASC,ProductNum DESC')
			else
				execute('select *,
				(SELECT COUNT(*) FROM [tbl_TourList] WHERE CompanyID=tbl_FenSiteExtend.CompanyID) as ProductNum,
				(SELECT CompanyName FROM [tbl_CompanyInfo] WHERE ID=tbl_FenSiteExtend.CompanyID) as CompanyName,
				(SELECT CityName FROM [tbl_SysCity] WHERE Id=tbl_FenSiteExtend.ShowCityID) as CityName,
				(SELECT LoginCount FROM [tbl_CompanyInfo] WHERE Id=tbl_FenSiteExtend.CompanyID) as LoginCount 
				from tbl_FenSiteExtend where ShowCityID='+@CityID+' order by SortId ASC,ProductNum DESC')
		if(@error>0)
			rollback tran
		else
			commit tran
END

go

/*==============================================================*/
/* Table: tbl_ExtendProduct                                     */
/*==============================================================*/
create table tbl_ExtendProduct (
   ID                   int                  identity,
   SortID               int                  null default 0,
   CompanyID            char(36)             not null,
   ProductID            char(36)             not null,
   ProductName          nvarchar(200)        null default '0',
   Price                money                null default 0,
   ShowCityID           int                  not null default 0,
   ShowProID            int                  null default 0,
   constraint PK_TBL_EXTENDPRODUCT primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'SortID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'CompanyID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品编号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'ProductID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '产品名称',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'ProductName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '价格',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '投放城市编号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'ShowCityID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '投放省份编号',
   'user', @CurrentUser, 'table', 'tbl_ExtendProduct', 'column', 'ShowProID'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create unique clustered index Index_1 on tbl_ExtendProduct (
ShowCityID ASC,
ProductID ASC
)
go



go

/*==============================================================*/
/* Table: tbl_FenSiteExtend                                     */
/*==============================================================*/
create table tbl_FenSiteExtend (
   ID                   int                  identity,
   SortID               int                  null default 0,
   CompanyID            char(36)             not null,
   IsBold               char(1)              null default '0',
   Color                nvarchar(50)         null,
   IsShow               char(1)              null default '1',
   ShowCity             nvarchar(50)         null,
   ShowCityID           int                  not null default 0,
   ShowProID            int                  null default 0,
   constraint PK_TBL_FENSITEEXTEND primary key nonclustered (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'ID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '序号',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'SortID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'CompanyID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否加粗',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'IsBold'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司名称显示颜色',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'Color'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '前台是否显示',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'IsShow'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '投放城市',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'ShowCity'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '投放城市编号',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'ShowCityID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '投放省份编号',
   'user', @CurrentUser, 'table', 'tbl_FenSiteExtend', 'column', 'ShowProID'
go

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create unique clustered index Index_1 on tbl_FenSiteExtend (
CompanyID ASC,
ShowCityID ASC
)
go



GO
ALTER TABLE tbl_SysCity ADD
	IsExtendSite char(1) NOT NULL CONSTRAINT DF_tbl_SysCity_IsExtendSite DEFAULT '1'
GO

GO

update tbl_SysCity set IsExtendSite=1 where CityName not in('北京','广州','杭州','上海','南京','济南','宁波','昆明','沈阳')
update tbl_SysCity set IsExtendSite=0 where CityName  in('北京','广州','杭州','上海','南京','济南','宁波','昆明','沈阳')

GO

go
/**
tbl_ExchangeList添加 Category tinyint字段
**/
if not exists(select a.[name] as columnName from syscolumns as a,
			sysobjects as b where a.ID = b.ID and b.[name] = 'tbl_ExchangeList'
								and a.[name] = 'Category')
begin
	alter table tbl_ExchangeList add Category tinyint 
end

go
/**
修改老数据(TopicClassID不修改(修改了枚举))
**/
UPDATE tbl_ExchangeList SET 
	ExchangeTagId = 1,
	Category = CASE ExchangeTagId WHEN 1 THEN 1 
								  WHEN 2 THEN 2 
								  WHEN 3 THEN 1 
								  WHEN 4 THEN 1 
								  WHEN 5 THEN 1 
								  ELSE 2 END
go
/**
将添加的Category null 改为 not null
**/
alter table tbl_ExchangeList alter column Category tinyint not null


/**特价机票**/
go
create table tbl_SpecialFares
(
	ID int identity(1,1) primary key not null,
	Title nvarchar(255) not null,
	Category tinyint not null,
	IsJump char(1) default '0' not null,
	ContentText nvarchar(max),
	Contact nvarchar(255),
	ContactWay nvarchar(255),
	QQ nvarchar(255),
	AddTime datetime default getdate() not null
);

/**团队票**/
go
create table tbl_GroupTickets
(
	ID int identity(1,1) primary key not null,
	GroupType tinyint not null,
	StartCity nvarchar(255) not null,
	EndCity nvarchar(255) not null,
	PelopeCount int check(PelopeCount >= 10) not null,
	Phone nvarchar(255) not null,
	Airlines tinyint not null,
	FlightNumber nvarchar(255),
	StartTime nvarchar(255) not null,
	TimeRange nvarchar(255) not null,
	RoundTripTime nvarchar(255),
	Notes nvarchar(max),
	Contact nvarchar(255),
	ContactQQ nvarchar(255),
	HopesPrice money default 0,
	Email nvarchar(255),
	IsChecked char(1) default '0',
	IssueTime datetime default getdate() not null
);

/**乘客信息**/
go
create table tbl_PassengerInformation
(
	ID int identity(1,1) primary key not null,
	GroupTicketsID int references tbl_GroupTickets(ID) not null,
	UName nvarchar(255),
	PassengerType tinyint,
	DocumentType tinyint,
	DocumentNo nvarchar(255),
	Mobile nvarchar(255)
);

/**设置级联删除**/
alter table tbl_PassengerInformation   
Add constraint fk_tbl_PassengerInformation_GroupTicketsID
Foreign key(GroupTicketsID) references tbl_GroupTickets(ID)
on delete cascade  


/**营销公司**/
go
create table tbl_MarketingCompany
(
	ID int identity(1,1) primary key not null,
	CompanyName nvarchar(255) not null,
	ProvinceId int not null,
	CityId int not null,
	Contact nvarchar(255) not null,
	ContactWay nvarchar(255) not null,
	Question nvarchar(max),
	Category tinyint not null,
	RegisterDate datetime default getdate() not null
);


GO



GO

INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (70,'首页国内长线广告',1,'首页广告',5,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (71,'首页国际长线广告',1,'首页广告',5,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (72,'首页周边长线广告',1,'首页广告',5,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (73,'首页金牌企业展示广告',1,'首页广告',4,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (74,'首页推荐产品广告',1,'首页广告',4,5)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (75,'首页资讯通栏广告',1,'首页广告',1,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (76,'首页资讯非通栏广告',1,'首页广告',1,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (77,'首页推荐企业',1,'首页广告',6,2)
INSERT [tbl_SysAdvArea] ([AreaId],[AreaName],[CatalogId],[CatalogName],[AdvCount],[DisplayType]) VALUES (78,'散拼中心普通版推荐企业',1,'首页广告',21,2)

GO


GO
ALTER TABLE tbl_SysAdvImg ADD
	AdvThumbnail nvarchar(250) NULL 
GO

GO

-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-14
-- Description:	发布广告
-- History:
-- 1.汪奇志 2010-09-01 类别为同业114广告(@CategoryId=3)时不做有效性验证
-- 2.华磊   2011-05-13 添加参数缩略图路径(@AdvThumbnail)
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysAdv_InsertAdv]
	@PositionId INT,--位置编号
	@CategoryId TINYINT,--类别编号
	@Title NVARCHAR(250),--标题
	@Remark NVARCHAR(MAX),--内容
	@RedirectURL NVARCHAR(250),--链接地址
	@ImgPath NVARCHAR(250)=NULL,--图片路径
	@AdvThumbnail NVARCHAR(250)=NULL,--缩略图路径
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
	IF(@CategoryId<>3)
	BEGIN
		EXECUTE proc_SysAdv_IsValid @PositionId=@PositionId,@StartDate=@StartDate,@EndDate=@EndDate,@Range=@Range,@Relation=@Relation,@Result=@Result OUTPUT
		IF(@Result=0) RETURN @Result
	END
	
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
		INSERT INTO tbl_SysAdvImg(AdvId,AdvImg,AdvThumbnail) VALUES(@AdvId,@ImgPath,@AdvThumbnail)
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


GO


-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-14
-- Description:	修改广告
-- History:
-- 1.汪奇志 2010-09-01 类别为同业114广告(@CategoryId=3)时不做有效性验证
-- 2.华磊   2011-05-13 缩略图路径(@AdvThumbnail)
-- =============================================
ALTER PROCEDURE [dbo].[proc_SysAdv_UpdateAdv]
	@AdvId INT,--广告编号
	@PositionId INT,--位置编号
	@CategoryId TINYINT,--类别编号
	@Title NVARCHAR(250),--标题
	@Remark NVARCHAR(MAX),--内容
	@RedirectURL NVARCHAR(250),--链接地址
	@ImgPath NVARCHAR(250)=NULL,--图片路径
	@AdvThumbnail NVARCHAR(250)=NULL,--缩略图路径
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
	@Result INT OUTPUT--结果 0:失败 1:成功
AS
BEGIN
	--判断是否有效
	IF(@CategoryId<>3)
	BEGIN
		EXECUTE proc_SysAdv_IsValid @AdvId=@AdvId,@PositionId=@PositionId,@StartDate=@StartDate,@EndDate=@EndDate,@Range=@Range,@Relation=@Relation,@Result=@Result OUTPUT
		IF(@Result=0) RETURN @Result
	END
	
	BEGIN TRANSACTION UpdateAdv
	DECLARE @errorCount INT
	SET @errorCount=0

	SET @CompanyId=ISNULL(@CompanyId,'')
	--更新广告基本信息
	UPDATE tbl_SysAdv SET AreaId=@PositionId,ClassId=@CategoryId,AdvDescript=@Title,AdvRemark=@Remark,AdvLink=@RedirectURL
		,CompanyId=@CompanyId,CompanyName=@CompanyName,ContactInfo=@ContactInfo,StartDate=@StartDate,EndDate=@EndDate
		,OperatorId=@OperatorId,OperatorName=@OperatorName,IssueTime=@IssueTime,AdvArea=@Range
	WHERE Id=@AdvId
	SET @errorCount=@errorCount+@@ERROR

	--广告图片信息
	IF(@ImgPath IS NOT NULL)
	BEGIN
		DELETE FROM tbl_SysAdvImg WHERE AdvId=@AdvId
		INSERT INTO tbl_SysAdvImg(AdvId,AdvImg,AdvThumbnail) VALUES(@AdvId,@ImgPath,@AdvThumbnail)
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

	--写入相应关系 先删除不需要的关系 再写入未曾写入的关系
	DELETE FROM tbl_SysAdvAreaControl WHERE AdvId=@AdvId AND AreaType=@AdvType 
		AND AreaId NOT IN(SELECT RelationId FROM @tmpRelation)
	
	INSERT INTO tbl_SysAdvAreaControl(AdvId,AreaType,AreaId,SortId) 
	SELECT @AdvId,@AdvType,A.RelationId,0 FROM @tmpRelation AS A 
	WHERE A.RelationId NOT IN(SELECT AreaId FROM tbl_SysAdvAreaControl WHERE AreaType=@AdvType AND AdvId=@AdvId)

	SET @errorCount=@errorCount+@@ERROR
	
	--提交事务
	IF(@errorCount>0)
	BEGIN
		ROLLBACK TRANSACTION UpdateAdv
		SET @Result=0
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION UpdateAdv
		SET @Result=1
	END

	RETURN @Result
END

GO


GO
CREATE VIEW [dbo].[view_Remind]
AS
SELECT     *
FROM         (SELECT     TOP 6 tbl_CompanyInfo.id AS id, tbl_CompanyInfo.CompanyName AS Operator, tbl_CompanyUser.IssueTime AS IssueTime, 
                                              CASE WHEN 1 = 1 THEN '1' END AS TypeTile
                       FROM          tbl_CompanyUser LEFT JOIN
                                              tbl_CompanyInfo ON tbl_CompanyUser.CompanyID = tbl_CompanyInfo.ID
                       WHERE      tbl_CompanyUser.IssueTime > dateadd(day, - 15, getdate()) AND tbl_CompanyInfo.IsCheck = 1
                       ORDER BY tbl_CompanyUser.IssueTime DESC) AS CompanyUser
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, CompanyName AS Operator, tbl_TourList.IssueTime AS IssueTime, CASE WHEN 1 = 1 THEN '2' END AS TypeTile
                       FROM          tbl_TourList
                       WHERE      tbl_TourList.CreateTime > dateadd(day, - 15, getdate())
                       ORDER BY tbl_TourList.CreateTime DESC) AS TourList
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, CompanyName AS Operator, IssueTime AS IssueTime, CASE WHEN 1 = 1 THEN '3' END AS TypeTile
                       FROM          tbl_TourList
                       WHERE      tbl_TourList.CreateTime > dateadd(day, - 15, getdate()) AND tbl_TourList.RouteState = 2
                       ORDER BY tbl_TourList.CreateTime DESC) AS cxTourList
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, CompanyName AS Operator, tbl_TourList.IssueTime AS IssueTime, CASE WHEN 1 = 1 THEN '4' END AS TypeTile
                       FROM          tbl_TourList
                       WHERE      tbl_TourList.CreateTime > dateadd(day, - 15, getdate()) AND TourState IN (2, 3)
                       ORDER BY tbl_TourList.IssueTime DESC) AS kmTourList
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, CompanyName AS Operator, PayTime AS IssueTime, CASE WHEN 1 = 1 THEN '5' END AS TypeTile
                       FROM          SMS_PayMoney
                       WHERE      IsChecked = 1 AND PayTime > dateadd(day, - 15, getdate())) AS PayMoney
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, OperatorName AS Operator, IssueTime AS IssueTime, CASE WHEN 1 = 1 THEN '6' END AS TypeTile
                       FROM          tbl_ExchangeList
                       WHERE      IssueTime > dateadd(day, - 15, getdate()) AND IsCheck = 1
                       ORDER BY IssueTime DESC) AS ExchangeList
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 orderid AS id, SupplierUName AS Operator, CreateTime AS IssueTime, CASE WHEN 1 = 1 THEN '7' END AS TypeTile
                       FROM          tbl_ticketOrder
                       WHERE      OrderState = 2 AND CreateTime > dateadd(day, - 15, getdate())
                       ORDER BY CreateTime DESC) AS ticketOrder
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 orderid AS id, BuyerUName AS Operator, CreateDateTime AS IssueTime, CASE WHEN 1 = 1 THEN '8' END AS TypeTile
                       FROM          tbl_HotelOrder
                       WHERE      CreateDateTime > dateadd(day, - 15, getdate()) AND CheckState = 1) AS HotelOrder
UNION ALL
SELECT     *
FROM         (SELECT     TOP 6 id, BuyCompanyName AS Operator, SuccessTime AS IssueTime, CASE WHEN 1 = 1 THEN '9' END AS TypeTile
                       FROM          tbl_TourOrder
                       WHERE      OrderState = 5 AND SuccessTime > dateadd(day, - 15, getdate())) AS TourOrder

GO






GO
CREATE VIEW [dbo].[View_TongHang]
AS
SELECT     a.Id, a.CompanyName, a.ContactQQ, a.ContactMQ,
                          (SELECT     ProvinceName
                            FROM          dbo.tbl_SysProvince AS p
                            WHERE      (ID = a.ProvinceId)) AS ProvinceName,
                          (SELECT     CityName
                            FROM          dbo.tbl_SysCity AS c
                            WHERE      (ID = a.CityId)) AS CityName, a.CompanyAddress, a.ContactTel, a.ContactFax, a.ContactName, a.IssueTime, a.ProvinceId, a.CityId, 
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


GO



update tbl_sysadvarea set advcount = 4 where areaid = 1


GO




--运营后台权限部分
go

select * from tbl_SysPermissionCategory where typeid = 1 and id in (4,5,13,14,8)

select * from tbl_SysPermissionClass where id in (48,49,50,51,52,53,54)

select * from tbl_SysPermissionList where id in (144,145,146,147,148,149,150)

select * from tbl_SystemUser where UserName = 'admin'

go


--大类别开始
go
if not exists (select 1 from tbl_SysPermissionCategory where typeid = 1 and id = 4 and CategoryName = '供求管理')
	insert into tbl_SysPermissionCategory(Id,TypeId,CategoryName,SortId,IsEnable) values (4,1,'供求管理',0,1)
go


go
if not exists (select 1 from tbl_SysPermissionCategory where typeid = 1 and id = 5 and CategoryName = '产品管理')
	insert into tbl_SysPermissionCategory(Id,TypeId,CategoryName,SortId,IsEnable) values (5,1,'产品管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionCategory where typeid = 1 and id = 8 and CategoryName = '用户反馈')
	insert into tbl_SysPermissionCategory(Id,TypeId,CategoryName,SortId,IsEnable) values (8,1,'用户反馈',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionCategory where typeid = 1 and id = 13 and CategoryName = '机票首页管理')
	insert into tbl_SysPermissionCategory(Id,TypeId,CategoryName,SortId,IsEnable) values (13,1,'机票首页管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionCategory where typeid = 1 and id = 14 and CategoryName = '酒店首页管理')
	insert into tbl_SysPermissionCategory(Id,TypeId,CategoryName,SortId,IsEnable) values (14,1,'酒店首页管理',0,1)
go

--小类别开始
go
if not exists (select 1 from tbl_SysPermissionClass where id = 48 and ClassName = '供求焦点图片管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (48,4,'供求焦点图片管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionClass where id = 49 and ClassName = '供求规则管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (49,4,'供求规则管理',0,1)
go


go
if not exists (select 1 from tbl_SysPermissionClass where id = 50 and ClassName = '首页登录提醒管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (50,5,'首页登录提醒管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionClass where id = 51 and ClassName = '特价机票管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (51,13,'特价机票管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionClass where id = 52 and ClassName = '团队票申请管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (52,13,'团队票申请管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionClass where id = 53 and ClassName = '特价酒店管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (53,14,'特价酒店管理',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionClass where id = 54 and ClassName = '网络营销反馈管理')
	insert into tbl_SysPermissionClass(Id,CategoryId,ClassName,SortId,IsEnable) values (54,8,'网络营销反馈管理',0,1)
go


--权限开始
go
if not exists (select 1 from tbl_SysPermissionList where id = 144 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (144,4,48,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 145 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (145,4,49,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 146 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (146,5,50,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 147 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (147,13,51,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 148 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (148,13,52,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 149 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (149,14,53,'管理该栏目',0,1)
go

go
if not exists (select 1 from tbl_SysPermissionList where id = 150 and PermissionName = '管理该栏目')
	insert into tbl_SysPermissionList(Id,CategoryId,ClassId,PermissionName,SortId,IsEnable) values (150,8,54,'管理该栏目',0,1)
go


go
--update tbl_SystemUser set PermissionList = PermissionList + ',144,145,146,147,148,149,150' where UserName = 'admin'
go



--Table: tbl_LogTicket 机票接口访问日志  汪奇志 2011-05-10                      
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




insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('Buyers',0)
insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('BuyersVirtual',0)
insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('Suppliers',0)
insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('SuppliersVirtual',0)
insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('SupplyInfos',0)
insert into [tbl_SysSummaryCount](fieldname,fieldvalue)values('SupplyInfosVirtual',0)



GO

ALTER PROCEDURE [dbo].[SQLPlan_SystemSummaryCount]
AS
BEGIN
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=1)
	where FieldName='TravelAgency';
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=2)
	where FieldName='Sight';
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=3)
	where FieldName='Hotel';
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=4)
	where FieldName='Car';
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=5)
	where FieldName='Goods';
	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=6)
	where FieldName='Shop';
	update tbl_SysSummaryCount set FieldValue=(
	select count(*) from tbl_CompanyInfo
	)
	where FieldName='User';
	update tbl_SysSummaryCount set FieldValue=(
	select count(*) from tbl_ExchangeList where DATEDIFF(wk,IssueTime,getdate())=0
	)
	where FieldName='Intermediary';
	update tbl_SysSummaryCount set FieldValue=(
	select sum(ParentTourCount) from tbl_SysCity
	)
	where FieldName='Route'
	update tbl_SysSummaryCount set FieldValue=(
	select count(*) from im_member
	)
	where FieldName='MQUser';

	update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_CompanyInfo where CompanyType=7)
	where FieldName='TicketCompany';

    update tbl_SysSummaryCount set FieldValue=(
	SELECT COUNT(*) FROM tbl_TicketFreightInfo)
	where FieldName='TicketFreight';
	
	/**zhengfj 修改时间:2011-5-17**/
	update tbl_SysSummaryCount set FieldValue=(
		SELECT COUNT(*) FROM tbl_CompanyInfo where ID IN (SELECT CompanyId from tbl_CompanyTypeList WHERE TypeId IN (2,10)))
	where FieldName ='Buyers'
	update tbl_SysSummaryCount set FieldValue=(
		SELECT COUNT(*) FROM tbl_CompanyInfo where ID IN (SELECT CompanyId from tbl_CompanyTypeList WHERE TypeId IN (1,3,4,5,6,7,8,9)))
	where FieldName ='Suppliers'
	update tbl_SysSummaryCount set FieldValue=(
		select count(*) from tbl_exchangelist where datediff(day,issuetime,getdate()) <= 30)
	where FieldName = 'SupplyInfos'
END


GO



/*

执行完此后还要执行数据插入Sql（旅游签证数据采集脚本.sql，旅游签证所需资料SQL脚本.sql，旅游同行SQL脚本.sql）
*/