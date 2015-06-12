--===================
--修改人：鲁功源
--修改时间：2010-04-28
--描述：用户后台广告添加
--===================
create procedure [dbo].[proc_CompanyAdv_Insert]
(
	@AdvTitle nvarchar(50),
	@AdvContent ntext,
	@AdvLink nvarchar(150),
	@IsNewOpen char(1),
	@AdvCompanyType tinyint,
	@AdvType tinyint,
	@OperatorID int,
	@OperatorName nvarchar(50),
	--@SortID int,
	@IssueTime datetime,
	@InternalLink nvarchar(150)
)
as
begin
	declare @ID char(36) --广告编号
	set @ID=newid()
	declare @NewAdvLink nvarchar(150) --广告链接地址
	--生成链接地址开始---
	if len(@AdvLink)=0 or @AdvLink is null 
	begin
		--内部链接
		set @NewAdvLink=@InternalLink+@ID
	end
	else 
	begin
		--外部链接
		set @NewAdvLink=@AdvLink
	end
	--生成链接地址结束---
	insert into tbl_CompanyAdv
	(
		ID,
		AdvTitle,
		AdvContent,
		AdvLink,
		IsNewOpen,
		AdvCompanyType,
		AdvType,
		OperatorID,
		OperatorName,
		--SortID,
		IssueTime
	)
	values
	(
		@ID,
		@AdvTitle,
		@AdvContent,
		@NewAdvLink,
		@IsNewOpen,
		@AdvCompanyType,
		@AdvType,
		@OperatorID,
		@OperatorName,
		--@SortID,
		@IssueTime
	)
end
