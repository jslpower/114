




go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicImg') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN_IMG')
alter table tbl_ScenicImg
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_IMG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicRelationTheme') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN_Relation')
alter table tbl_ScenicRelationTheme
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_Relation
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicTickets') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN')
alter table tbl_ScenicTickets
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicArea')
            and   type = 'U')
   drop table tbl_ScenicArea
go

/*==============================================================*/
/* Table: tbl_ScenicArea                                        */
/*==============================================================*/
create table tbl_ScenicArea (
   ScenicId             char(36)             not null,
   ScenicName           nvarchar(100)        not null,
   EnName               varchar(100)         null,
   Telephone            varchar(50)          null,
   SetYear              int                  null default 0,
   X                    varchar(100)         null,
   Y                    varchar(100)         null,
   ProvinceId           int                  not null default 0,
   CityId               int                  not null default 0,
   CountyId             int                  not null default 0,
   CnAddress            nvarchar(100)        null,
   EnAddress            varchar(100)         null,
   ScenicLevel          tinyint              not null default 0,
   OpenTime             nvarchar(max)        null,
   Description          nvarchar(max)        null,
   Traffic              nvarchar(max)        null,
   Facilities           nvarchar(max)        null,
   Notes                nvarchar(max)        null,
   B2B                  tinyint              not null default 0,
   B2BOrder             int                  not null default 50,
   B2C                  tinyint              not null default 0,
   B2COrder             int                  not null default 50,
   Status               tinyint              not null default 0,
   IssueTime            datetime             not null default getdate(),
   CompanyId            char(36)             not null,
   ContactOperator      char(36)             null,
   Operator             char(36)             not null,
   ExamineOperator      int                  null,
   ClickNum             int                  not null default 0,
   LastUpdateTime       datetime             null,
   constraint PK_TBL_SCENICAREA primary key (ScenicId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ScenicId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区名称',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ScenicName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '英文名',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'EnName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客服电话',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Telephone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '成立年份',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'SetYear'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '经纬度横坐标',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'X'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '经纬度纵坐标',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Y'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '省份',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ProvinceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '城市',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'CityId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '县区',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'CountyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '中文地址',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'CnAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '英文地址',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'EnAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区A级',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ScenicLevel'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日程开放时间',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'OpenTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区详细介绍',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交通说明',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Traffic'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '周边设施',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Facilities'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Notes'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B显示控制(首页 侧边 列表 常规 隐藏 排序)',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'B2B'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B排序值',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'B2BOrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2C显示控制(首页 侧边 列表 常规 隐藏 排序)',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'B2C'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2C排序值',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'B2COrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核状态(待审核，已审核)',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布时间',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'IssueTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布公司',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'CompanyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '联系人',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ContactOperator'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布用户',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'Operator'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核用户',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ExamineOperator'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '点击量',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'ClickNum'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'tbl_ScenicArea', 'column', 'LastUpdateTime'
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicTickets') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN')
alter table tbl_ScenicTickets
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicTickets')
            and   type = 'U')
   drop table tbl_ScenicTickets
go

/*==============================================================*/
/* Table: tbl_ScenicTickets                                     */
/*==============================================================*/
create table tbl_ScenicTickets (
   TicketsId            char(36)             not null,
   ScenicId             char(36)             not null,
   TypeName             nvarchar(50)         not null,
   EnName               varchar(100)         null,
   RetailPrice          money                null,
   WebsitePrices        money                null,
   MarketPrice          money                null,
   DistributionPrice    money                null,
   Limit                int                  not null default 0,
   Payment              tinyint              not null default 0,
   StartTime            datetime             null,
   EndTime              datetime             null,
   Description          nvarchar(max)        null,
   SaleDescription      nvarchar(max)        null,
   Status               tinyint              not null default 0,
   ExamineStatus        tinyint              not null default 0,
   CustomOrder          int                  not null default 0,
   B2B                  tinyint              not null default 0,
   B2BOrder             int                  not null default 50,
   B2C                  tinyint              not null default 0,
   B2COrder             int                  not null default 50,
   IssueTime            datetime             not null default getdate(),
   Operator             char(36)             not null,
   CompanyId            char(36)             not null,
   ExamineOperator      int                  null,
   LastUpdateTime       datetime             null,
   constraint PK_TBL_SCENICTICKETS primary key (TicketsId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '门票编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'TicketsId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'ScenicId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '门票类型名称',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'TypeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '英文名称',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'EnName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '门市价',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'RetailPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '网站优惠价',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'WebsitePrices'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '市场限制最低价',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'MarketPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '同行分销价',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'DistributionPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最少限制（张/套）',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'Limit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '支付方式',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'Payment'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '票价有效时间_始',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'StartTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '票价游戏时间_止',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'EndTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '门票说明',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '同业销售须知 （只有同业分销商能看到）',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'SaleDescription'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态（上架，下架）',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核状态(待审核，已审核)',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'ExamineStatus'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '自定义排序',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'CustomOrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B显示控制',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'B2B'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2B排序值',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'B2BOrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2C显示控制',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'B2C'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'B2C排序值',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'B2COrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布时间',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'IssueTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布人',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'Operator'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发布公司',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'CompanyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '审核人',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'ExamineOperator'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'user', @CurrentUser, 'table', 'tbl_ScenicTickets', 'column', 'LastUpdateTime'
go

alter table tbl_ScenicTickets
   add constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN foreign key (ScenicId)
      references tbl_ScenicArea (ScenicId)
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicImg') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN_IMG')
alter table tbl_ScenicImg
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_IMG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicImg')
            and   type = 'U')
   drop table tbl_ScenicImg
go

/*==============================================================*/
/* Table: tbl_ScenicImg                                         */
/*==============================================================*/
create table tbl_ScenicImg (
   ImgId                char(36)             not null,
   ScenicId             char(36)             not null,
   ImgType              tinyint              not null,
   Address              nvarchar(500)        not null,
   ThumbAddress         nvarchar(500)        null,
   Description          nvarchar(max)        null,
   CompanyId            char(36)             not null,
   constraint PK_TBL_SCENICIMG primary key (ImgId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'ImgId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'ScenicId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片类型',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'ImgType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片地址',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'Address'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片缩略图',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'ThumbAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片说明',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'Description'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicImg', 'column', 'CompanyId'
go

alter table tbl_ScenicImg
   add constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_IMG foreign key (ScenicId)
      references tbl_ScenicArea (ScenicId)
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicTheme')
            and   type = 'U')
   drop table tbl_ScenicTheme
go

/*==============================================================*/
/* Table: tbl_ScenicTheme                                       */
/*==============================================================*/
create table tbl_ScenicTheme (
   ThemeId              int                  identity,
   ThemeName            nvarchar(100)        not null,
   IsDelete             char(1)              not null default '0',
   constraint PK_TBL_SCENICTHEME primary key (ThemeId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主题编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicTheme', 'column', 'ThemeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主题名称',
   'user', @CurrentUser, 'table', 'tbl_ScenicTheme', 'column', 'ThemeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否删除(0:否1：是)',
   'user', @CurrentUser, 'table', 'tbl_ScenicTheme', 'column', 'IsDelete'
go


if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_SystemLandMark')
            and   type = 'U')
   drop table tbl_SystemLandMark
go

/*==============================================================*/
/* Table: tbl_SystemLandMark                                    */
/*==============================================================*/
create table tbl_SystemLandMark (
   Id                   int                  identity,
   Por                  nvarchar(100)        not null,
   CityId               int                  not null,
   CityCode             varchar(3)           not null,
   constraint PK_TBL_SYSTEMLANDMARK primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'tbl_SystemLandMark', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '地标名称',
   'user', @CurrentUser, 'table', 'tbl_SystemLandMark', 'column', 'Por'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '城市编号',
   'user', @CurrentUser, 'table', 'tbl_SystemLandMark', 'column', 'CityId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '三字码',
   'user', @CurrentUser, 'table', 'tbl_SystemLandMark', 'column', 'CityCode'
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicRelationLandMark')
            and   type = 'U')
   drop table tbl_ScenicRelationLandMark
go

/*==============================================================*/
/* Table: tbl_ScenicRelationLandMark                            */
/*==============================================================*/
create table tbl_ScenicRelationLandMark (
   ScenicId             char(36)             not null,
   LandMarkId           int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicRelationLandMark', 'column', 'ScenicId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '地标编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicRelationLandMark', 'column', 'LandMarkId'
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('tbl_ScenicRelationTheme') and o.name = 'FK_TBL_SCEN_REFERENCE_TBL_SCEN_Relation')
alter table tbl_ScenicRelationTheme
   drop constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_Relation
go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_ScenicRelationTheme')
            and   type = 'U')
   drop table tbl_ScenicRelationTheme
go

/*==============================================================*/
/* Table: tbl_ScenicRelationTheme                               */
/*==============================================================*/
create table tbl_ScenicRelationTheme (
   ScenicId             char(36)             not null,
   ThemeId              int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '景区编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicRelationTheme', 'column', 'ScenicId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '主题编号',
   'user', @CurrentUser, 'table', 'tbl_ScenicRelationTheme', 'column', 'ThemeId'
go

alter table tbl_ScenicRelationTheme
   add constraint FK_TBL_SCEN_REFERENCE_TBL_SCEN_Relation foreign key (ScenicId)
      references tbl_ScenicArea (ScenicId)
go


go
/**
tbl_CompanyInfo添加 CountyId int字段
**/
if not exists(select a.[name] as columnName from syscolumns as a,
			sysobjects as b where a.ID = b.ID and b.[name] = 'tbl_CompanyInfo'
								and a.[name] = 'CountyId')
begin
	alter table tbl_CompanyInfo add CountyId int not null default 0
end

go


go


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
		[Id],[ProvinceId],[CityId],[CountyId],[CompanyType],[CompanyName],[CompanyBrand],[CompanyAddress],[License],[ContactName],[ContactTel],[ContactFax],[ContactMobile],[ContactEmail],[ContactMQ],[ContactQQ],[ContactMSN],[IsCheck],[CommendPeople],[ShortRemark]
		)VALUES(
		@CompanyId,@ProvinceId,@CityId,@CountyId,@BusinessProperties,@CompanyName,@CompanyBrand,@CompanyAddress,@License,@ContactName,@ContactTel,@ContactFax,@ContactMobile,@ContactEmail,CAST(@MQID AS VARCHAR(20)),@ContactQQ,@ContactMSN,@IsCheck,@CommendPeople,@ShortRemark
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

go

go


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
	DECLARE @CCompanyName NVARCHAR(255)
	
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
		UPDATE [tbl_CompanyInfo] SET [ProvinceId]=@ProvinceId,[CityId]=@CityId,[CountyId] = @CountyId,[CompanyType]=@BusinessProperties,[CompanyName]=@CompanyName,[CompanyBrand]=@CompanyBrand,[CompanyAddress]=@CompanyAddress,[License]=@License,[ContactName]=@ContactName,[ContactTel]=@ContactTel,[ContactFax]=@ContactFax,[ContactMobile]=@ContactMobile,[ContactEmail]=@ContactEmail,[ContactQQ]=@ContactQQ,[ContactMSN]=@ContactMSN,[Remark]=@Remark,[ShortRemark]=@ShortRemark WHERE Id=@CompanyId 
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

go


--写入默认景区主题
go
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('风景区','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('主题乐园','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('户外拓展','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('农家乐','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('温泉','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('滑雪','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('漂流','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('世界遗产','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('生态山水','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('名胜古迹','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('城市观光','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('古镇','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('海岛','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('园林','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('场馆','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('演出','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('摄影基地','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('采摘','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('避暑','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('度假区','0');
insert into [tbl_ScenicTheme] ([ThemeName],[IsDelete]) values ('游船','0');

go


if object_id('proc_ScenicArea_Delete') is not null
	drop proc proc_ScenicArea_Delete
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-10-27>
-- Description:	<删除景区>
-- =============================================
create proc proc_ScenicArea_Delete
	@scenicId char(36),
	@companyId char(36)
as
begin
	declare @return int,@error int
--	if exists(select TicketsId from tbl_ScenicTickets where ScenicId = @scenicId and CompanyId = @companyId)
--	begin
--		set @return = 2
--	end
--	else
--	begin
		begin tran --
		set @error = 0
		--添加图片到待删除附件
		insert into tbl_SysDeletedFileQue(FilePath,FileState)
		select Address,0 from tbl_ScenicImg where ScenicId = @scenicId
		set @error = @@error + @error
		delete from tbl_ScenicImg where ScenicId = @scenicId--景区图片
		set @error = @@error + @error
		delete from tbl_ScenicRelationTheme where ScenicId = @scenicId --景区主题关联
		set @error = @@error + @error
		delete from tbl_ScenicRelationLandMark where ScenicId = @scenicId
		set @error = @@error + @error
		delete from tbl_ScenicTickets where ScenicId = @scenicId and CompanyId = @companyId
		set @error = @@error + @error
		delete from tbl_ScenicArea where ScenicId = @scenicId and CompanyId = @companyId
		set @error = @@error + @error
		if @@ROWCOUNT > 0
			set @return = 1
		else
			set @return = 3
		if @error > 0
			rollback tran
		else
			commit tran
	--end
	return @return
end
go

if object_id('proc_ScenicTickets_Update') is not null
	drop proc proc_ScenicTickets_Update
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-10-28>
-- Description:	<修改景区门票>
-- =============================================
create proc proc_ScenicTickets_Update
	@TicketsId char(36),
	@ScenicId char(36),
	@TypeName nvarchar(50),
	@RetailPrice money,
	@WebsitePrices money,	
	@MarketPrice money,	
	@DistributionPrice money,
	@Limit int,
	@Payment tinyint,
	@StartTime datetime,
	@EndTime datetime,	
	@Description nvarchar(max),
	@SaleDescription nvarchar(max),
	@Status tinyint,
	@CustomOrder int,	
	@B2B tinyint,
	@B2BOrder int,
	@B2C tinyint,
	@B2COrder int,
	@EnName varchar(100),
	@ExamineStatus tinyint,
	@ExamineStatus1 tinyint = null
as
begin
	declare @m1 money,@m2 money,@m3 money,@m4 money,@error int
	set @error = 0
	if exists(select TicketsId from tbl_ScenicTickets where TicketsId = @TicketsId)
	begin
		--查出之前的4种价格
		select @m1=RetailPrice,@m2=WebsitePrices,@m3=MarketPrice,@m4=DistributionPrice from tbl_ScenicTickets 
		where TicketsId = @TicketsId
		--修改门票信息
		begin tran
		update tbl_ScenicTickets set TypeName = @TypeName,RetailPrice=@RetailPrice,WebsitePrices=@WebsitePrices,
			MarketPrice =@MarketPrice,DistributionPrice=@DistributionPrice,Limit=@Limit,Payment=@Payment,
			StartTime=@StartTime,EndTime=@EndTime,Description=@Description,SaleDescription=@SaleDescription,EnName = @EnName,
			Status=@Status,CustomOrder=@CustomOrder,B2B=@B2B,B2BOrder=@B2BOrder,B2C=@B2C,B2COrder=@B2COrder,LastUpdateTime =getdate(),ScenicId = @ScenicId
		where TicketsId = @TicketsId  
		set @error = @error + @@error
		if @ExamineStatus1 is null
		begin
			--如果4种价格其中一个被改，则状态还原为待审核
			if @m1 <> @RetailPrice or @m2 <> @WebsitePrices or @m3 <> @MarketPrice or @m4 <> @DistributionPrice
			begin
				update tbl_ScenicTickets set ExamineStatus = @ExamineStatus where TicketsId = @TicketsId
				set @error = @error + @@error
			end
		end
		else 
		begin
			update tbl_ScenicTickets set ExamineStatus = @ExamineStatus1 where TicketsId = @TicketsId
			set @error = @error + @@error
		end
		if @error > 0
			rollback tran
		else
			commit tran
	end
end
go

if object_id('view_ScenicArea_SeniorOnlineShop_Select') is not null
	drop view view_ScenicArea_SeniorOnlineShop_Select
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-12-2>
-- Description:	<高级网店景区>
-- =============================================
create view view_ScenicArea_SeniorOnlineShop_Select
as
select (select ImgId,ScenicId,ImgType,Address,ThumbAddress,Description from tbl_ScenicImg where ScenicId = a.ScenicId for xml raw ,root('item')) ScenicImg,
	a.ScenicId,a.ScenicName,a.CompanyId,a.Description,a.LastUpdateTime,a.B2B,a.B2BOrder,a.Status
from tbl_ScenicArea a  
go


if object_id('proc_ScenicArea_Update') is not null
	drop proc proc_ScenicArea_Update
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-2>
-- Description:	<景区修改>
-- =============================================
create proc proc_ScenicArea_Update
    @ScenicName nvarchar(100),
    @EnName varchar(100),
    @Telephone varchar(50),
    @SetYear int,
    @Y varchar(50),
    @X varchar(50),
    @ProvinceId int,
    @CityId	int,
    @CountyId	int,
    @LankId	xml,  --格式 <item><id>值</id></item>
    @CnAddress nvarchar(100),
    @EnAddress	varchar(100),
    @ThemeId xml, --格式 <item><themeid>值</themeid></item>
    @ScenicLevel tinyint,
    @OpenTime	nvarchar(max),
    @Description nvarchar(max),
    @Traffic nvarchar(max),
    @Facilities nvarchar(max),
    @Notes nvarchar(max),
    @B2B tinyint,
    @B2BOrder int,
    @B2C tinyint,
    @B2COrder int,
	@ScenicId char(36),
    @CompanyId char(36),
	@ContactOperator char(36),
	@StatusValue tinyint,  --已审核的枚举值
	@Status tinyint = null
as
begin
	declare @error int,@doc int
	set @error = 0
	begin tran
	UPDATE tbl_ScenicArea SET ScenicName = @ScenicName,EnName = @EnName,Telephone = @Telephone,SetYear = @SetYear,
            X=@X,Y=@Y,ProvinceId=@ProvinceId,CityId=@CityId,CountyId=@CountyId,CnAddress=@CnAddress,EnAddress=@EnAddress,
            ScenicLevel=@ScenicLevel,OpenTime=@OpenTime,Description=@Description,Traffic=@Traffic,Facilities=@Facilities,
            Notes=@Notes,B2B=@B2B,B2BOrder=@B2BOrder,B2C=@B2C,B2COrder=@B2COrder,ContactOperator = @ContactOperator,
			CompanyId = @CompanyId,LastUpdateTime = getdate()
             WHERE ScenicId = @ScenicId 
	set @error = @error + @@error
	if @Status <> 0
	begin
		update tbl_ScenicArea set Status = @Status where ScenicId = @ScenicId 
		set @error = @error + @@error
	end
	set @error = @error + @@error
	--景区主题
	if @ThemeId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@ThemeId
		delete from tbl_ScenicRelationTheme 
			from tbl_ScenicRelationTheme tb1
			left join (
				select ScenicId,ThemeId from tbl_ScenicRelationTheme where ScenicId = @ScenicId
				except
				select @ScenicId,ThemeId from openxml(@doc,'/item/themeid') with(ThemeId int 'text()')
			) tb2 on tb1.ThemeId = tb2.ThemeId
		where tb2.ScenicId = @ScenicId and tb1.ThemeId = tb2.ThemeId
		set @error = @@error + @error
		insert into tbl_ScenicRelationTheme(ScenicId,ThemeId)
		select @ScenicId,ThemeId from openxml(@doc,'/item/themeid') with(ThemeId int 'text()')
		except
		select ScenicId,ThemeId from tbl_ScenicRelationTheme where ScenicId = @ScenicId
		set @error = @@error + @error
		exec sp_xml_removedocument @doc
	end
	--景区地标
	if @LankId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@LankId
		delete from tbl_ScenicRelationLandMark 
			from tbl_ScenicRelationLandMark tb1
			left join (
				select ScenicId,LandMarkId from tbl_ScenicRelationLandMark where ScenicId = @ScenicId
				except
				select @ScenicId,id from openxml(@doc,'/item/id') with(id int 'text()')
			) tb2 on tb1.LandMarkId = tb2.LandMarkId
		where tb2.ScenicId = @ScenicId and tb1.LandMarkId = tb2.LandMarkId
		set @error = @@error + @error
		insert into tbl_ScenicRelationLandMark(ScenicId,LandMarkId)
		select @ScenicId,id from openxml(@doc,'/item/id') with(id int 'text()')
		except
		select ScenicId,LandMarkId from tbl_ScenicRelationLandMark where ScenicId = @ScenicId
		set @error = @@error + @error
		exec sp_xml_removedocument @doc
	end
	if @error > 0
		rollback tran
	else 
		commit tran
end
go

if object_id('proc_ScenicArea_Add') is not null
	drop proc proc_ScenicArea_Add
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-2>
-- Description:	<景区添加>
-- =============================================
create proc proc_ScenicArea_Add
	@ScenicId char(36),
    @ScenicName nvarchar(100),
    @EnName varchar(100),
    @Telephone varchar(50),
    @SetYear int,
    @Y varchar(50),
    @X varchar(50),
    @ProvinceId int,
    @CityId	int,
    @CountyId	int,
    @LankId	xml,  --格式<item><id>值</id></item>
    @CnAddress nvarchar(100),
    @EnAddress	varchar(100),
    @ThemeId xml, --格式 <item><themeid>值</themeid></item>
    @ScenicLevel tinyint,
    @OpenTime	nvarchar(max),
    @Description nvarchar(max),
    @Traffic nvarchar(max),
    @Facilities nvarchar(max),
    @Notes nvarchar(max),
    @B2B tinyint,
    @B2BOrder int,
    @B2C tinyint,
    @B2COrder int,
    @CompanyId char(36),
    @Operator char(36),
    @Status tinyint,
	@ContactOperator char(36),
	@ExamineOperator int = null --审核用户,默认已审核的时候
as
begin
	declare @error int,@doc int
	set @error = 0
	begin tran
	INSERT INTO tbl_ScenicArea(ScenicId,ScenicName,EnName,Telephone,SetYear,X,Y,ProvinceId,CityId
            ,CountyId,CnAddress,EnAddress,ScenicLevel,OpenTime,Description,Traffic,Facilities
            ,Notes,B2B,B2BOrder,B2C,B2COrder,CompanyId,Operator,Status,ContactOperator,ExamineOperator,LastUpdateTime)
             VALUES(@ScenicId,@ScenicName,@EnName,@Telephone,@SetYear,@X,@Y,@ProvinceId,@CityId,@CountyId,
            @CnAddress,@EnAddress,@ScenicLevel,@OpenTime,@Description,@Traffic,@Facilities
            ,@Notes,@B2B,@B2BOrder,@B2C,@B2COrder,@CompanyId,@Operator,@Status,@ContactOperator,@ExamineOperator,getdate())
	set @error = @@error + @error	
	if @ThemeId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@ThemeId
		--添加到景区主题关联
		insert into tbl_ScenicRelationTheme
		select @ScenicId,themeid from openxml(@doc,'/item/themeid') with(themeid int 'text()')
		set @error = @@error + @error	
		exec sp_xml_removedocument @doc
	end
	if @LankId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@LankId
		--添加到景区地标关联
		insert into tbl_ScenicRelationLandMark
		select @ScenicId,id from openxml(@doc,'/item/id') with(id int 'text()')
		set @error = @@error + @error	
		exec sp_xml_removedocument @doc
	end
	if @error > 0
		rollback tran
	else
		commit tran
end
go

if object_id('view_ScenicImg_Select') is not null
	drop view view_ScenicImg_Select
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-10>
-- Description:	<景区图片>
-- =============================================
create view view_ScenicImg_Select
as
	select a.ImgId,a.ScenicId,a.CompanyId,a.ImgType,a.Address,a.ThumbAddress,a.Description,b.ScenicName
		from tbl_ScenicImg a
	inner join tbl_ScenicArea b  on a.ScenicId = b.ScenicId
go

if object_id('view_ScenicArea_Select') is not null
	drop view view_ScenicArea_Select
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-10>
-- Description:	<景区>
-- =============================================
create view view_ScenicArea_Select
as
	
	select a.ScenicId,a.ScenicName,b.ThumbAddress,b.Address,b.ImgType,a.Status ,a.CompanyId,a.ProvinceId,c.ProvinceName,
				a.CityId,d.CityName,a.CountyId,e.DistrictName,a.B2B,a.B2C,a.LastUpdateTime,a.B2BOrder,a.B2COrder,a.ScenicLevel,a.ClickNum,a.X,a.Y,a.CnAddress,a.Description,
				(select TicketsId,TypeName,MarketPrice,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,Payment,Status,RetailPrice,ExamineStatus,WebsitePrices,DistributionPrice from tbl_ScenicTickets 
						where ScenicId = a.ScenicId for xml path,root('item')) Tickets ,
				(select a1.ThemeId,b.ThemeName from tbl_ScenicRelationTheme a1 
				inner join tbl_ScenicTheme b on a1.ThemeId = b.ThemeId
					where a1.ScenicId = a.ScenicId for xml raw,root('item')) ThemeId
				from tbl_ScenicArea a
				left join tbl_ScenicImg b on a.ScenicId = b.ScenicId and b.ImgType = 1
				left join tbl_SysProvince c on a.ProvinceId = c.Id
				left join tbl_SysCity d on a.CityId = d.Id
				left join tbl_sysDistrictCounty e on a.CountyId = e.Id
				

go

if object_id('view_ScenicArea_SelectA') is not null
	drop view view_ScenicArea_SelectA
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-10>
-- Description:	<景区>
-- =============================================
create view view_ScenicArea_SelectA
as
	select cast((select ScenicId,ThemeId from tbl_ScenicRelationTheme where ScenicId = a.ScenicId for xml raw, root('item')) as xml) ThemeId,
	a.ScenicId,a.ScenicName,a.ProvinceId,a.ScenicLevel,c.ProvinceName,a.CityId,d.CityName,a.CountyId,e.DistrictName,b.CompanyName,
    b.ContactName,b.ContactTel,b.ContactMobile,b.ContactFax,b.ContactQQ,a.CompanyId,a.Description,
    a.Status,a.B2B,a.B2BOrder,a.B2COrder,a.B2C,a.ClickNum,a.LastUpdateTime,a.ContactOperator,f.ContactName ContactOperatorName
    from tbl_ScenicArea a 
    left join tbl_CompanyInfo b
    on a.CompanyId = b.Id
    left join tbl_SysProvince c
    on a.ProvinceId = c.Id
    left join tbl_SysCity d
    on a.CityId = d.Id
    left join tbl_sysDistrictCounty e
    on a.CountyId = e.Id
    left join tbl_CompanyUser f
    on a.ContactOperator = f.Id
go

set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

--添加实际景区数量，虚拟景区数量，初始为0
insert into tbl_SysSummaryCount(FieldName,FieldValue) values('Scenic','0')
insert into tbl_SysSummaryCount(FieldName,FieldValue) values('ScenicVirtual','0')

go

/****/
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
	
	/**zhengfj 修改时间:2011-11-22 修改内容 加了景区数量**/
	update tbl_SysSummaryCount set FieldValue=(select count(*) from tbl_ScenicArea where Status = 2 and B2B <> 99) --Status:已审核
		where FieldName ='Scenic'
	
END


if object_id('view_ScenicTickets_Select') is not null
	drop view view_ScenicTickets_Select
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-10>
-- Description:	<景区门票>
-- =============================================
create view view_ScenicTickets_Select
as
select a.CompanyId,a.TicketsId,a.TypeName,a.RetailPrice,a.WebsitePrices,a.StartTime,a.EndTime,
a.Payment,a.LastUpdateTime,
             a.ExamineStatus,a.Status,a.B2B,a.B2C,a.ScenicId,b.ScenicName,a.IssueTime
             from tbl_ScenicTickets a inner join tbl_ScenicArea b on a.ScenicId = b.ScenicId
			 
			 
			 
			 
go





-- 景区添加自增列

go

/**
tbl_ScenicArea添加 Id bigint identity字段
**/
if not exists(select a.[name] as columnName from syscolumns as a,
		sysobjects as b where a.ID = b.ID and b.[name] = 'tbl_ScenicArea'
				and a.[name] = 'Id')
begin
	alter table tbl_ScenicArea add Id bigint identity not null 
end
go




GO
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-10>
-- Description:	<景区>
-- =============================================
ALTER view [dbo].[view_ScenicArea_Select]
as
	
	select a.Id,a.ScenicId,a.ScenicName,b.ThumbAddress,b.Address,b.ImgType,a.Status ,a.CompanyId,a.ProvinceId,c.ProvinceName,
				a.CityId,d.CityName,a.CountyId,e.DistrictName,a.B2B,a.B2C,a.LastUpdateTime,a.B2BOrder,a.B2COrder,a.ScenicLevel,a.ClickNum,a.X,a.Y,a.CnAddress,a.Description,
				(select TicketsId,TypeName,MarketPrice,isnull(cast(StartTime as varchar(50)),'') StartTime,isnull(cast(EndTime as varchar(50)),'') EndTime,Payment,Status,RetailPrice,ExamineStatus,WebsitePrices,DistributionPrice from tbl_ScenicTickets 
						where ScenicId = a.ScenicId for xml path,root('item')) Tickets ,
				(select a1.ThemeId,b.ThemeName from tbl_ScenicRelationTheme a1 
				inner join tbl_ScenicTheme b on a1.ThemeId = b.ThemeId
					where a1.ScenicId = a.ScenicId for xml raw,root('item')) ThemeId
				from tbl_ScenicArea a
				left join tbl_ScenicImg b on a.ScenicId = b.ScenicId and b.ImgType = 1
				left join tbl_SysProvince c on a.ProvinceId = c.Id
				left join tbl_SysCity d on a.CityId = d.Id
				left join tbl_sysDistrictCounty e on a.CountyId = e.Id
				
go



				
GO

-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-12-2>
-- Description:	<高级网店景区>
-- =============================================
ALTER view [dbo].[view_ScenicArea_SeniorOnlineShop_Select]
as
select (select ImgId,ScenicId,ImgType,Address,ThumbAddress,Description from tbl_ScenicImg where ScenicId = a.ScenicId for xml raw ,root('item')) ScenicImg,
	a.ScenicId,a.ScenicName,a.CompanyId,a.Description,a.LastUpdateTime,a.B2B,a.B2BOrder,a.Status,a.Id
from tbl_ScenicArea a
GO








if object_id('proc_ScenicArea_Update') is not null
	drop proc proc_ScenicArea_Update
go
-- =============================================
-- Author:		<郑付杰>
-- Create date: <2011-11-2>
-- Description:	<景区修改>
-- =============================================
create proc proc_ScenicArea_Update
    @ScenicName nvarchar(100),
    @EnName varchar(100),
    @Telephone varchar(50),
    @SetYear int,
    @Y varchar(50),
    @X varchar(50),
    @ProvinceId int,
    @CityId	int,
    @CountyId	int,
    @LankId	xml,  --格式 <item><id>值</id></item>
    @CnAddress nvarchar(100),
    @EnAddress	varchar(100),
    @ThemeId xml, --格式 <item><themeid>值</themeid></item>
    @ScenicLevel tinyint,
    @OpenTime	nvarchar(max),
    @Description nvarchar(max),
    @Traffic nvarchar(max),
    @Facilities nvarchar(max),
    @Notes nvarchar(max),
    @B2B tinyint,
    @B2BOrder int,
    @B2C tinyint,
    @B2COrder int,
	@ScenicId char(36),
    @CompanyId char(36),
	@ContactOperator char(36),
	@StatusValue tinyint,  --已审核的枚举值
	@Status tinyint = null
as
begin
	declare @error int,@doc int
	set @error = 0
	begin tran
	UPDATE tbl_ScenicArea SET ScenicName = @ScenicName,EnName = @EnName,Telephone = @Telephone,SetYear = @SetYear,
            X=@X,Y=@Y,ProvinceId=@ProvinceId,CityId=@CityId,CountyId=@CountyId,CnAddress=@CnAddress,EnAddress=@EnAddress,
            ScenicLevel=@ScenicLevel,OpenTime=@OpenTime,Description=@Description,Traffic=@Traffic,Facilities=@Facilities,
            Notes=@Notes,B2B=@B2B,B2BOrder=@B2BOrder,B2C=@B2C,B2COrder=@B2COrder,ContactOperator = @ContactOperator,
			CompanyId = @CompanyId,LastUpdateTime = getdate()
             WHERE ScenicId = @ScenicId 
	set @error = @error + @@error
	if @Status <> 0
	begin
		update tbl_ScenicArea set Status = @Status where ScenicId = @ScenicId 
		set @error = @error + @@error
	end
	set @error = @error + @@error
	--景区主题
	if @ThemeId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@ThemeId
		delete from tbl_ScenicRelationTheme 
			from tbl_ScenicRelationTheme tb1
			left join (
				select ScenicId,ThemeId from tbl_ScenicRelationTheme where ScenicId = @ScenicId
				except
				select @ScenicId,ThemeId from openxml(@doc,'/item/themeid') with(ThemeId int 'text()')
			) tb2 on tb1.ThemeId = tb2.ThemeId
		where tb2.ScenicId = @ScenicId and tb1.ThemeId = tb2.ThemeId
		set @error = @@error + @error
		insert into tbl_ScenicRelationTheme(ScenicId,ThemeId)
		select @ScenicId,ThemeId from openxml(@doc,'/item/themeid') with(ThemeId int 'text()')
		except
		select ScenicId,ThemeId from tbl_ScenicRelationTheme where ScenicId = @ScenicId
		set @error = @@error + @error
		exec sp_xml_removedocument @doc
	end
	else
	begin	
		delete from tbl_ScenicRelationTheme where ScenicId = @ScenicId
		set @error = @@error + @error
	end
	--景区地标
	if @LankId.exist('/item') = 1
	begin
		exec sp_xml_preparedocument @doc output,@LankId
		delete from tbl_ScenicRelationLandMark 
			from tbl_ScenicRelationLandMark tb1
			left join (
				select ScenicId,LandMarkId from tbl_ScenicRelationLandMark where ScenicId = @ScenicId
				except
				select @ScenicId,id from openxml(@doc,'/item/id') with(id int 'text()')
			) tb2 on tb1.LandMarkId = tb2.LandMarkId
		where tb2.ScenicId = @ScenicId and tb1.LandMarkId = tb2.LandMarkId
		set @error = @@error + @error
		insert into tbl_ScenicRelationLandMark(ScenicId,LandMarkId)
		select @ScenicId,id from openxml(@doc,'/item/id') with(id int 'text()')
		except
		select ScenicId,LandMarkId from tbl_ScenicRelationLandMark where ScenicId = @ScenicId
		set @error = @@error + @error
		exec sp_xml_removedocument @doc
	end	
	else
	begin	
		delete from tbl_ScenicRelationLandMark where ScenicId = @ScenicId	
		set @error = @@error + @error
	end
	if @error > 0
		rollback tran
	else 
		commit tran
end
go







/*
	以下为员景区公司导入景区表
*/




go

--导入抓取的景区,主题未知 
insert into tbl_ScenicArea([ScenicId],[ScenicName],[EnName],[Telephone]
       ,[SetYear],[X],[Y],[ProvinceId] ,[CityId],[CountyId],[CnAddress],[EnAddress] ,[ScenicLevel]
       ,[OpenTime],[Description],[Traffic],[Facilities],[Notes],[B2B]
       ,[B2BOrder] ,[B2C],[B2COrder],[Status],[IssueTime],[CompanyId]
       ,[ContactOperator] ,[Operator],[ExamineOperator] ,[ClickNum],[LastUpdateTime])
select newid(),a.CompanyName,null,a.ContactTel,0,substring(b.FieldValue,0,charindex(',',b.FieldValue)),
		substring(b.FieldValue,charindex(',',b.FieldValue)+1,
			case charindex(',',substring(b.FieldValue,charindex(',',b.FieldValue)+1,len(b.FieldValue)-charindex(',',b.FieldValue))) 
			when 0 then len(b.FieldValue)-charindex(',',b.FieldValue)
			else charindex(',',substring(b.FieldValue,charindex(',',b.FieldValue)+1,len(b.FieldValue))) - 1 end
		)
		,a.ProvinceId,a.CityId,a.CountyId,
	   a.CompanyAddress,null,0,null,a.Remark,null,null,null,0,50,0,50,
		1,a.IssueTime,a.Id,d.Id,'',null,0,null
from tbl_CompanyInfo a
left join tbl_CompanySetting b on a.Id = b.CompanyId and b.FieldName = 'PositionInfo'
left join tbl_CompanyTypeList c on a.Id = c.CompanyId   
left join tbl_CompanyUser d on a.Id = d.CompanyId and d.IsDeleted = '0' and d.IsAdmin = '1'  --景区联系人取公司帐号下管理员编号
where a.CompanyType = 2 and a.IsCheck = '1' and a.IsDeleted = '0' and c.TypeId = 4  --公司类型表 景区c.TypeId = 4
	 and convert(varchar(10),a.IssueTime,120) = '2010-08-28' and a.LoginCount = 0
	 
	 
go

--导入抓取的景区图片
insert into tbl_ScenicImg
select newid(),a.ScenicId,1,b.FieldValue,b.FieldValue,null,a.CompanyId from tbl_ScenicArea a
inner join tbl_CompanyAttachInfo b on a.CompanyId = b.CompanyId
where b.FieldName = 'CompanyLogo'

go

--导入抓取的景区其他图片
--导入抓取的景区其他图片
insert into tbl_ScenicImg
select newid(),a.ScenicId,3,b.ImagePath,b.ImagePath,b.ContentText,a.CompanyId from tbl_ScenicArea a
inner join tbl_HighShopTripGuide b on a.CompanyId = b.CompanyId and b.TypeId = 6  --typeid=6,景区美图

go


--导入抓取的景区跟图片后，将景区表的CompanyId改成易诺的
Update tbl_ScenicArea set CompanyId = '96ee7672-a50b-46b1-ac46-98f1052902b8'
Update tbl_ScenicImg set CompanyId = '96ee7672-a50b-46b1-ac46-98f1052902b8'

go


--将抓取的景区联系人修改成易诺管理员
Update tbl_ScenicArea set ContactOperator = b.Id
	from tbl_ScenicArea a inner join tbl_CompanyUser b on a.CompanyId = b.CompanyId 
		and b.IsAdmin = '1' and b.IsDeleted = '0' 
	where a.CompanyId = '96ee7672-a50b-46b1-ac46-98f1052902b8'
	
go










go
--导入注册的景区
insert into tbl_ScenicArea([ScenicId],[ScenicName],[EnName],[Telephone]
       ,[SetYear],[X],[Y],[ProvinceId] ,[CityId],[CountyId],[CnAddress],[EnAddress] ,[ScenicLevel]
       ,[OpenTime],[Description],[Traffic],[Facilities],[Notes],[B2B]
       ,[B2BOrder] ,[B2C],[B2COrder],[Status],[IssueTime],[CompanyId]
       ,[ContactOperator] ,[Operator],[ExamineOperator] ,[ClickNum],[LastUpdateTime])
select newid(),a.CompanyName,null,a.ContactTel,0,substring(b.FieldValue,0,charindex(',',b.FieldValue)),
		substring(b.FieldValue,charindex(',',b.FieldValue)+1,
			case charindex(',',substring(b.FieldValue,charindex(',',b.FieldValue)+1,len(b.FieldValue)-charindex(',',b.FieldValue))) 
			when 0 then len(b.FieldValue)-charindex(',',b.FieldValue)
			else charindex(',',substring(b.FieldValue,charindex(',',b.FieldValue)+1,len(b.FieldValue))) - 1 end
		)
		,a.ProvinceId,a.CityId,a.CountyId,
	   a.CompanyAddress,null,0,null,a.Remark,null,null,null,0,50,0,50,
		2,a.IssueTime,a.Id,d.Id,'',null,0,null
from tbl_CompanyInfo a
left join tbl_CompanySetting b on a.Id = b.CompanyId and b.FieldName = 'PositionInfo'
left join tbl_CompanyTypeList c on a.Id = c.CompanyId   
left join tbl_CompanyUser d on a.Id = d.CompanyId and d.IsDeleted = '0' and d.IsAdmin = '1'  --景区联系人取公司帐号下管理员编号
where a.CompanyType = 2 and a.IsCheck = '1' and a.IsDeleted = '0' and c.TypeId = 4  --公司类型表 景区c.TypeId = 4
	 and convert(varchar(10),a.IssueTime,120) <> '2010-08-28' and a.LoginCount > 0
	 
	 
go

--导入注册的景区图片
insert into tbl_ScenicImg
select newid(),a.ScenicId,1,b.FieldValue,b.FieldValue,null,a.CompanyId from tbl_ScenicArea a
inner join tbl_CompanyAttachInfo b on a.CompanyId = b.CompanyId
where b.FieldName = 'CompanyLogo'

go

--导入注册的景区其他图片
insert into tbl_ScenicImg
select newid(),a.ScenicId,3,b.ImagePath,b.ImagePath,b.ContentText,a.CompanyId from tbl_ScenicArea a
inner join tbl_HighShopTripGuide b on a.CompanyId = b.CompanyId and b.TypeId = 6  --typeid=6,景区美图

go

--修改注册的景区联系人
Update tbl_ScenicArea set ContactOperator = b.Id
	from tbl_ScenicArea a inner join tbl_CompanyUser b on a.CompanyId = b.CompanyId 
		and b.IsAdmin = '1' and b.IsDeleted = '0' 
	where a.CompanyId = b.CompanyId and a.CompanyId <> '96ee7672-a50b-46b1-ac46-98f1052902b8'
	
go






go

--导入地标
Insert into tbl_SystemLandMark (Por,CityId,CityCode)
select Por,0,CityCode from tbl_HotelLandMarks 
where CityCode in (select CityCode from tbl_HotelCity as a 
	where exists (select 1 from tbl_SysCity as b where b.cityName = a.cityname))
	
go

--修改地标的cityid
update tbl_SystemLandMark set CityId = c.Id 
	from tbl_SystemLandMark a
left join tbl_HotelCity b
	on a.CityCode = b.CityCode
left join tbl_SysCity c
	on b.CityName = c.CityName 

	
		 