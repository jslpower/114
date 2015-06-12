GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-16
-- Description:	真实删除公司记录,并同时删除公司所有的关联表
-- @ReturnValue  1:操作成功   0:操作失败
-- =============================================
CREATE PROC [dbo].[proc_CompanyInfo_Delete] 
	@XMLRoot NVARCHAR(MAX)  --公司ID,sqlxml脚本
AS
BEGIN
	DECLARE @ReturnValue INT;
	SET @ReturnValue = 0;	-- 1:操作成功   0:操作失败
	DECLARE @hdoc INT;
	EXEC sp_xml_preparedocument @hdoc OUTPUT, @XMLRoot;
	--开始事务
	BEGIN TRAN Company_Delete

	DELETE FROM im_member WHERE bs_uid IN( 
SELECT u.ID FROM tbl_CompanyUser AS u,(SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))) AS c WHERE u.CompanyId=c.CompanyId
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE FROM tbl_RateScoreTotal WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END	
	DELETE FROM tbl_RateCountTotal WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END	
	DELETE FROM tbl_CompanyUnCheckedCity WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE FROM tbl_CompanyCustomerList WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE FROM tbl_CommonPriceStandAdd WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE FROM tbl_CompanyFavor WHERE CompanyId IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END
	DELETE FROM tbl_CompanyInfo WHERE Id IN( 
SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
	);
	IF @@ERROR <> 0		
	BEGIN
		SET @ReturnValue = 0;
		ROLLBACK TRAN Company_Delete;
		EXEC sp_xml_removedocument @hdoc;
		RETURN @ReturnValue;
	END

	--执行成功
	EXEC sp_xml_removedocument @hdoc;
	SET @ReturnValue = 1;
	COMMIT TRAN Company_Delete;
	RETURN @ReturnValue;
--	DELETE FROM tbl_CompanyUser WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END

--	DELETE FROM tbl_CompanyAttachInfo WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanyPriceStand WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanyDayMemo WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanyDepartment WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanyPayService WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_ServiceStandard WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_TendCompanyInfo WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_TourContactInfo WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_TourStateBase WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanySetting WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END	
	
--	DELETE FROM tbl_CompanyTypeList WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END	

--	DELETE FROM tbl_CompanyRoles WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END		
--	DELETE FROM tbl_CompanyCityControl WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END	
--	DELETE FROM tbl_CompanyAreaConfig WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END	
--	DELETE FROM tbl_CompanyUserAreaControl WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END	
	
--	DELETE FROM tbl_CompanySiteControl WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_BankAccount WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END

--	DELETE FROM tbl_CompanyProduct WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
--	DELETE FROM tbl_CompanyAffiche WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END

--	DELETE FROM tbl_CompanyTag WHERE CompanyId IN( 
--SELECT CompanyId FROM OPENXML(@hdoc, N'/ROOT/Company') WITH (CompanyId CHAR(36))
--	);
--	IF @@ERROR <> 0		
--	BEGIN
--		SET @ReturnValue = 0;
--		ROLLBACK TRAN Company_Delete;
--		EXEC sp_xml_removedocument @hdoc;
--		RETURN @ReturnValue;
--	END
END
GO