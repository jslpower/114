


/*
	新建国家数据表
*/

go

if exists (select 1
            from  sysobjects
           where  id = object_id('tbl_SysCountry')
            and   type = 'U')
   drop table tbl_SysCountry
go

/*==============================================================*/
/* 新建国家数据表												*/
/* Table: tbl_SysCountry                                        */
/*==============================================================*/
create table tbl_SysCountry (
   Id                   int                  identity,
   EnName               varchar(255)         null,
   Zones                int                  not null default 0,
   CName                nvarchar(255)        null,
   Continent            tinyint              not null default 0,
   constraint PK_TBL_SYSCOUNTRY primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统国家表
   中文名称
   英文名称
   洲',
   'user', @CurrentUser, 'table', 'tbl_SysCountry'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '国家编号',
   'user', @CurrentUser, 'table', 'tbl_SysCountry', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '英文名称',
   'user', @CurrentUser, 'table', 'tbl_SysCountry', 'column', 'EnName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Zones',
   'user', @CurrentUser, 'table', 'tbl_SysCountry', 'column', 'Zones'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '中文名称',
   'user', @CurrentUser, 'table', 'tbl_SysCountry', 'column', 'CName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '洲际编号  亚洲 = 1,欧洲 = 2,非洲 = 3,大洋洲 = 4,北美洲 = 5,中美洲 = 6,南美洲 = 7',
   'user', @CurrentUser, 'table', 'tbl_SysCountry', 'column', 'Continent'
go






/*
	国家数据脚本
*/

go


SET IDENTITY_INSERT [tbl_SysCountry] ON

INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 1,N'Afghanistan',13,N'阿富汗',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 2,N'Albania',10,N'阿尔巴尼亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 3,N'Algeria',13,N'阿尔及利亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 4,N'American Samoa',13,N'美属萨摩亚',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 5,N'Andorra',13,N'安道尔',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 6,N'Angola',13,N'安哥拉',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 7,N'Anguilla',13,N'安圭拉',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 8,N'Antigua',13,N'安提瓜',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 9,N'Argentina',13,N'阿根廷',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 10,N'Armenia',10,N'亚美尼亚',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 11,N'Aruba',13,N'阿鲁巴',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 12,N'Australia',5,N'澳大利亚',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 13,N'Austria',9,N'奥地利',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 14,N'Azerbaijan',10,N'阿塞拜疆',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 15,N'Bahamas',13,N'巴哈马',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 16,N'Bahrain',12,N'巴林',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 17,N'Bangladesh',11,N'孟加拉',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 18,N'Barbados',13,N'巴巴多斯',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 19,N'Belarus',10,N'白俄罗斯',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 20,N'Belgium',7,N'比利时',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 21,N'Belize',13,N'伯利兹',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 22,N'Benin',13,N'贝宁',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 23,N'Bermuda',13,N'百慕大',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 24,N'Bhutan',13,N'不丹',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 25,N'Bolivia',13,N'玻利维亚',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 26,N'Bosnia & Herzegovina',10,N'波斯尼亚和黑塞哥维那',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 27,N'Botswana',13,N'博茨瓦纳',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 28,N'Brazil',13,N'巴西',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 29,N'Brunei',13,N'文莱',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 30,N'Bulgaria',9,N'保加利亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 31,N'Burkina Faso',13,N'布基纳法索',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 32,N'Burundi',13,N'布隆迪',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 33,N'Cambodia (Kampuchea)',13,N'柬埔寨',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 34,N'Cameroon',13,N'喀麦隆',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 35,N'Canada',6,N'加拿大',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 36,N'Cape Verde',13,N'佛得角',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 37,N'Cayman Islands',13,N'开曼群岛',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 38,N'Central African Republic',13,N'中非共和国',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 39,N'Chad',13,N'乍得',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 40,N'Chile',13,N'智利',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 41,N'CHRISTMAS ISLAND',13,N'圣诞岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 42,N'COCOS  KEELING  ISLANDS',13,N'科科斯（基灵）群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 43,N'Colombia',13,N'哥伦比亚',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 44,N'Comoros',13,N'科摩罗',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 45,N'Congo',13,N'刚果',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 46,N'Congo The Democratic Republic',13,N'刚果民主共和国',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 47,N'Cook Islands',13,N'库克群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 48,N'Costa Rica',13,N'哥斯达黎加',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 49,N'Cote Dlvoire',13,N'科特迪瓦',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 50,N'Croatia',9,N'克罗地亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 51,N'Cuba',13,N'古巴',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 52,N'Cyprus',9,N'塞浦路斯',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 53,N'Czech Republic',9,N'捷克共和国',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 54,N'Demark',8,N'丹麦',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 55,N'Djibouti',13,N'吉布提',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 56,N'Dominica',13,N'多米尼加',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 57,N'Dominican Republic',13,N'多米尼加共和国',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 58,N'Ecuador',13,N'厄瓜多尔',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 59,N'Egypt',12,N'埃及',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 60,N'El Salvador',13,N'萨尔瓦多',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 61,N'Equatorial Guinea',13,N'赤道几内亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 62,N'Eritrea',13,N'厄立特里亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 63,N'Estonia',9,N'爱沙尼亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 64,N'Ethiopia',13,N'埃塞俄比亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 65,N'Faroe Islands',13,N'法罗群岛',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 66,N'Fiji Islands',13,N'斐济群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 67,N'Finland',8,N'芬兰',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 68,N'France',8,N'法国',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 69,N'French Guiana',13,N'法属圭亚那',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 70,N'Gabon',13,N'加蓬',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 71,N'Gambia',13,N'冈比亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 72,N'Georgia',10,N'格鲁吉亚',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 73,N'Germany',7,N'德国',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 74,N'Ghana',13,N'加纳',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 75,N'Gibraltar',13,N'直布罗陀',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 76,N'Greece',8,N'希腊',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 77,N'Greenland',10,N'格陵兰岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 78,N'Grenada',13,N'格林纳达',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 79,N'Guadeloupe',13,N'瓜德罗普',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 80,N'Guam',13,N'关岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 81,N'Guatemala',13,N'危地马拉',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 82,N'Guinea Republic',13,N'几内亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 83,N'Guinea-Bissau',13,N'几内亚比绍',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 84,N'Guyana(British)',13,N'圭亚那(英)',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 85,N'Haiti',13,N'海地',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 86,N'HOLY SEE  VATICAN CITY STATE',10,N'梵蒂冈',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 87,N'Honduras',13,N'洪都拉斯',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 88,N'Hungary',9,N'匈牙利',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 89,N'Iceland',10,N'冰岛',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 90,N'India',11,N'印度',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 91,N'Indonesia',4,N'印度尼西亚',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 92,N'Iran Islamic Republic of',12,N'伊朗',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 93,N'Iraq',12,N'伊拉克',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 94,N'Ireland Republic of',8,N'爱尔兰',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 95,N'Israel',12,N'以色列',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 96,N'Italy',8,N'意大利',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 97,N'Jamaica',13,N'牙买加',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 98,N'Japan',3,N'日本',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 99,N'Jordan',12,N'约旦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 100,N'Kazakhstan',13,N'哈萨克斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 101,N'Kenya',13,N'肯尼亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 102,N'Kiribati',13,N'基里巴斯',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 103,N'Korea D.P.R.of',13,N'朝鲜',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 104,N'Korea Republic of',2,N'韩国',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 105,N'KOSOVO',10,N'科索沃',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 106,N'Kuwait',12,N'科威特',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 107,N'Kyrgyzstan',13,N'吉尔吉斯斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 108,N'Laos',13,N'老挝',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 109,N'Latvia',9,N'拉脱维亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 110,N'Lebanon',12,N'黎巴嫩',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 111,N'Lesotho',13,N'莱索托',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 112,N'Liberia',13,N'利比里亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 113,N'Libya',13,N'利比亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 114,N'Liechtenstein',10,N'列支敦士登',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 115,N'Lithuania',9,N'立陶宛',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 116,N'Luxembourg',7,N'卢森堡',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 117,N'Macedonia',10,N'马其顿',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 118,N'Madagascar',13,N'马达加斯加',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 119,N'Malawi',13,N'马拉维',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 120,N'Malaysia',4,N'马来西亚',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 121,N'Maldives',13,N'马尔代夫',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 122,N'Mali',13,N'马里',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 123,N'Malta',8,N'马耳他',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 124,N'Marshall Islands',13,N'马绍尔群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 125,N'Martinique',13,N'马提尼克',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 126,N'Mauritania',13,N'毛里塔尼亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 127,N'Mauritius',13,N'毛里求斯',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 128,N'MAYOTTE',13,N'马约特岛',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 129,N'Mexico',13,N'墨西哥',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 130,N'MICRONESIA',13,N'密克罗尼西亚',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 131,N'Moldova, Republic of',10,N'摩尔多瓦',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 132,N'Monaco',10,N'摩纳哥',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 133,N'Mongolia',13,N'蒙古',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 134,N'Montenegro',10,N'黑山共和国',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 135,N'Montserrat',13,N'蒙特塞拉特岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 136,N'Morocco',13,N'摩洛哥',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 137,N'Mozambique',13,N'莫桑比克',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 138,N'Myanmar',13,N'缅甸',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 139,N'Namibia',13,N'纳米比亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 140,N'Nauru Republic of',13,N'瑙鲁',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 141,N'Nepal',11,N'尼泊尔',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 142,N'NETHERLANDS ANTILLES',13,N'荷属安的列斯',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 143,N'Netherlands, The',7,N'荷兰',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 144,N'New Caledonia',13,N'新喀里多尼亚',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 145,N'New Zealand',5,N'新西兰',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 146,N'Nicaragua',13,N'尼加拉瓜',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 147,N'Niger',13,N'尼日尔',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 148,N'Nigeria',13,N'尼日利亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 149,N'NORFOLK ISLAND',13,N'诺夫克群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 150,N'Norway',8,N'挪威',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 151,N'Oman',12,N'阿曼',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 152,N'Pakistan',11,N'巴基斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 153,N'PALAU',13,N'帕劳',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 154,N'PALESTINE',12,N'巴勒斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 155,N'Panama',13,N'巴拿马',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 156,N'Papua New Guinea',13,N'巴布亚新几内亚',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 157,N'Paraguay',13,N'巴拉圭',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 158,N'Peru',13,N'秘鲁',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 159,N'Philipines, The',4,N'菲律宾',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 160,N'Poland',9,N'波兰',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 161,N'Portugal',8,N'葡萄牙',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 162,N'Puerto Rico',13,N'波多黎各',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 163,N'Qatar',12,N'卡塔尔',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 164,N'Renuion Island of',13,N'留尼汪岛',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 165,N'Romania',9,N'罗马尼亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 166,N'Russian Federation The',10,N'俄罗斯',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 167,N'Rwanda',13,N'卢旺达',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 168,N'SAINT PIERRE AND MIQUELON',13,N'圣皮埃尔和密克隆',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 169,N'Saipan',13,N'塞班岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 170,N'Samoa',13,N'萨摩亚',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 171,N'SAN MARINO',10,N'圣马力诺',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 172,N'Sao Tome and Principe',13,N'圣多美和普林西比',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 173,N'Saudi Arabia',12,N'沙特阿拉伯',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 174,N'Senegal',13,N'塞内加尔',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 175,N'SERBIA',10,N'塞尔维亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 176,N'Seychelles',13,N'塞舌尔',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 177,N'Sierra Leone',13,N'塞拉利昂',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 178,N'Singapore',4,N'新加坡',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 179,N'Slovakia',9,N'斯洛伐克',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 180,N'Slovenia',9,N'斯洛文尼亚',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 181,N'Soloman Islands',13,N'所罗门群岛',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 182,N'Somalia',13,N'索马里',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 183,N'South Africa',11,N'南非',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 184,N'Spain',8,N'西班牙',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 185,N'Sri Lanka',11,N'斯里兰卡',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 186,N'St Kitts',13,N'圣基茨',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 187,N'St Lucia',13,N'圣卢西亚岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 188,N'St Vincent',13,N'圣文森特岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 189,N'Sudan',13,N'苏丹',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 190,N'Suriname',13,N'苏里南',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 191,N'Swaziland',13,N'斯威士兰',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 192,N'Sweden',8,N'瑞典',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 193,N'Switzerland',8,N'瑞士',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 194,N'Syria',12,N'叙利亚',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 195,N'Tahiti',13,N'大溪地/塔希提',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 196,N'Tajikistan',13,N'塔吉克斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 197,N'Tanzania',13,N'坦桑尼亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 198,N'Thailand',4,N'泰国',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 199,N'TIMOR-LESTE',13,N'东帝汶',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 200,N'Togo',13,N'多哥',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 201,N'Tonga',13,N'汤加',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 202,N'Trinidad and Tobago',13,N'特立尼达和多巴哥',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 203,N'Tunisia',13,N'突尼斯',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 204,N'Turkey',8,N'土耳其',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 205,N'Turkmeinistan',13,N'土库曼斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 206,N'Turks And Caicos Islands',13,N'特克斯和凯科斯群岛',6)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 207,N'Tuvalu',13,N'图瓦卢',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 208,N'Uganda',13,N'乌干达',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 209,N'Ukraine',10,N'乌克兰',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 210,N'United Arad Emirates',12,N'阿拉伯联合酋长国',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 211,N'United Kingdom',7,N'英国',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 212,N'United States of America',6,N'美国',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 213,N'Uruguay',13,N'乌拉圭',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 214,N'Uzbekistan',13,N'乌兹别克斯坦',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 215,N'Vanuatu',13,N'瓦努阿图',4)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 216,N'Venezuela',13,N'委内瑞拉',7)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 217,N'Vietnam',4,N'越南',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 218,N'Virgin Islands (British)',13,N'英属维尔群岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 219,N'Virgin Islands (U.S.)',13,N'美属维尔京群岛',5)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 220,N'WALLIS AND FUTUNA ISLANDS',13,N'瓦利斯及富图纳群岛',2)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 221,N'Yemen',11,N'也门',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 222,N'Zambia',13,N'赞比亚',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 223,N'Zimbabwe',13,N'津巴布韦',3)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 224,N'China',13,N'中国',1)

INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 225,N'Hong Kong',13,N'香港',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 226,N'Macao',13,N'澳门',1)
INSERT [tbl_SysCountry] ([Id],[EnName],[Zones],[CName],[Continent]) VALUES ( 227,N'Taiwan',13,N'台湾',1)


SET IDENTITY_INSERT [tbl_SysCountry] OFF

go