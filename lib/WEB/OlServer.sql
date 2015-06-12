


--网店客服系统S表结构ql语句   2011-02-24


GO
CREATE TABLE [dbo].[OL_ServerUsers](
	[Id] [char](36) NOT NULL,
	[UserId] [char](36) NULL,
	[UserName] [nvarchar](250) NULL,
	[OlName] [nvarchar](250) NULL,
	[IsService] [char](1) NOT NULL CONSTRAINT [DF_tbl_OlServerUsers_IsService]  DEFAULT ((0)),
	[LoginTime] [datetime] NOT NULL CONSTRAINT [DF_tbl_OlServerUsers_LoginTime]  DEFAULT (getdate()),
	[AcceptId] [char](36) NULL,
	[AcceptName] [nvarchar](250) NULL,
	[IsOnline] [char](1) NOT NULL CONSTRAINT [DF_tbl_OlServerUsers_IsOnline]  DEFAULT ((0)),
	[LastSendMessageTime] [datetime] NULL CONSTRAINT [DF_tbl_OlServerUsers_LastSendMessageTime]  DEFAULT (getdate()),
	[CompanyId] [char](36) NOT NULL,
 CONSTRAINT [PK_tbl_OlServerUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录用户编号 未登录用户为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录用户用户名 未登录用户默认为空' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在线显示的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'OlName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是客服人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'IsService'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进入在线客服系统时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'LoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认接收消息的用户在线编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'AcceptId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认接收消息的用户在线名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'AcceptName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在线状态   0：不在线；1在线；' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'IsOnline'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后发送消息时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'LastSendMessageTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerUsers', @level2type=N'COLUMN',@level2name=N'CompanyId'


Go



GO
CREATE TABLE [dbo].[OL_ServerMessage](
	[Id] [char](36) NOT NULL,
	[SendId] [char](36) NULL,
	[SendName] [nvarchar](250) NULL,
	[AcceptId] [char](36) NULL,
	[AcceptName] [nvarchar](250) NULL,
	[Message] [nvarchar](max) NULL,
	[SendTime] [datetime] NOT NULL CONSTRAINT [DF_tbl_OlServerMessage_SendTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_tbl_OlServerMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送信息用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'SendId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送信息用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'SendName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收信息用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'AcceptId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收信息用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'AcceptName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'Message'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送信息时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OL_ServerMessage', @level2type=N'COLUMN',@level2name=N'SendTime'



Go