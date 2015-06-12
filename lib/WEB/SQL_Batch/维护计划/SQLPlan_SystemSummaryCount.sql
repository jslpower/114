
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		蒋胜蓝
-- Description:	系统数量更新
-- =============================================
CREATE PROCEDURE [dbo].[SQLPlan_SystemSummaryCount]
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
END
