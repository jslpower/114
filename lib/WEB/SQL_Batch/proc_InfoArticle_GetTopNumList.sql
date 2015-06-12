set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


--==============================
--创建人:鲁功源
--创建时间:2010-07-15
--描述:获取制定条数的资讯列表
--==============================
ALTER procedure [dbo].[proc_InfoArticle_GetTopNumList]
(
	@topNumber int,--需要返回的总记录数
	@topClass int,--大类别编号
	@areaId int,--子类别编号
	@IsPicNew int, --是否图片资讯
	@IsFrontPage int, --是否首页显示
	@KeyWord nvarchar(250),--需要匹配的关键字
	@IsCurrWeekNew char(1), --是否本周最新
	@CurrInfoID char(36), --是否当前资讯的相关文章
	@IsFocusPic int, --是否焦点图片资讯
	@IsTopPic int --是否推荐图片资讯
)
as
declare @strSql nvarchar(max) --sql主语句变量
-----------------生成查询主语句开始------
if @topNumber>0
	begin
		set @strSql='select top '+cast(@topNumber as varchar(10))+' id,ArticleTitle,ImgThumb,ArticleText,TitleColor,IssueTime,ImgPath,TopicClassId,AreaId from tbl_CommunityInfoArticle where 1=1 '
	end
else
	begin
		set @strSql='select id,ArticleTitle,ImgThumb,ArticleText,TitleColor,IssueTime,ImgPath,TopicClassId,AreaId from tbl_CommunityInfoArticle where 1=1 '
	end
-----------------结束----------------------

-----------------生成查询条件开始------------

if @topClass>-1 --大类别
begin
	set @strSql=@strSql+' and TopicClassId='+cast(@topClass as varchar(10))+' '
end

if @areaId>-1 --子类别
begin
	set @strSql=@strSql+' and AreaId='+cast(@areaId as varchar(10))+' '
end

if @IsPicNew>-1 --是否图片资讯
begin
	set @strSql=@strSql+' and IsImage='+ cast(@IsPicNew as char(1))+' '
end

if @IsFrontPage>-1 --是否首页显示
begin
	set @strSql=@strSql+' and IsFrontPage='+ cast(@IsPicNew as char(1))+' '
end

if @IsCurrWeekNew='1' --是否本周最新
begin
	set @strSql=@strSql+' and datediff(wk,IssueTime,getdate())=0 '
end

if @IsFocusPic>-1 --是否焦点图片资讯
begin
	if @IsFocusPic=1
		set @strSql=@strSql+' and len(ImgThumb)>0 '
end

if @IsTopPic>-1 --是否推荐图片资讯
begin
	if @IsTopPic=1
		set @strSql=@strSql+' and len(ImgPath)>0 '
end

if len(@KeyWord)>0 --匹配关键字
begin
	set @strSql=@strSql+'and ArticleTitle like ''%' + @KeyWord + '%'''
end

if len(@CurrInfoID)>0 
begin
	declare @Tags nvarchar(250)
	declare @strWhere nvarchar(500)
	select @Tags=ArticleTag from tbl_CommunityInfoArticle where ID=@CurrInfoID
	set @strWhere='where ArticleId<>'''+@CurrInfoID + ''' and ArticleTag like ''%' + @Tags + '%'''
	if @areaId>-1 --子类别
		set @strWhere = @strWhere + ' and AreaId= '+ cast(@areaId as varchar(10))
	set @strSql=@strSql+' and ID in(select ArticleId from tbl_CommunityArticleTag '+@strWhere+')'
end
set @strSql=@strSql+' order by IsTop desc,IssueTime desc'
print @strWhere
if len(@strSql)>0
execute(@strSql)
else
return
-----------------结束-------------------------

