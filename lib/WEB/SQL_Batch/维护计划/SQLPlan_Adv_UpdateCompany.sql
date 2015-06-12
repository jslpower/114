SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		蒋胜蓝
-- Description:	广告数据同步单位名称、单位LOGO、网店地址
-- =============================================
CREATE PROCEDURE [dbo].[SQLPlan_Adv_UpdateCompany]
AS
BEGIN
	UPDATE tbl_SysAdv SET CompanyName=
	(   SELECT CompanyName FROM tbl_CompanyInfo where 	
		tbl_SysAdv.CompanyId = tbl_CompanyInfo.ID
	)
	WHERE 
	AreaId IN (SELECT ID FROM tbl_SysAdvArea WHERE DisplayType in (5,6,7))

	UPDATE tbl_SysAdvImg SET AdvImg=(
		SELECT FieldValue FROM tbl_CompanyAttachInfo WHERE FieldName='CompanyLogo' 
		AND tbl_CompanyAttachInfo.CompanyId=(SELECT CompanyId FROM tbl_SysAdv where id=tbl_SysAdvImg.advid)
	)
	WHERE 
	AdvId IN (SELECT ID FROM tbl_SysAdv WHERE
	AreaId IN (SELECT ID FROM tbl_SysAdvArea WHERE DisplayType in (5,6,7)))

	UPDATE tbl_SysAdv SET
	AdvDescript=CompanyName
	WHERE
	AreaId IN (SELECT ID FROM tbl_SysAdvArea WHERE DisplayType in (5,6,7))

	/*UPDATE tbl_SysAdv SET
	AdvLink=CompanyName
	WHERE
	AreaId IN (SELECT ID FROM tbl_SysAdvArea WHERE DisplayType in (5,6,7))*/
END
