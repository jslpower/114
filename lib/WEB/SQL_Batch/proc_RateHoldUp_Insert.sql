GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-28
-- Description:	插入一条推荐信息，同时更新推荐公司的信用与积分信息
-- =============================================
CREATE PROCEDURE [dbo].[proc_RateHoldUp_Insert]
	--推荐人所在公司编号
	@FROMCOMPANYID CHAR(36),
	--推荐人所在公司名称
	@FROMCOMPANYNAME NVARCHAR(250),
	--推荐人编号
	@FROMUSERID CHAR(36),
	--推荐人姓名
	@FROMUSERCONTACTNAME NVARCHAR(50),
	--推荐的公司编号
	@TOCOMPANYID CHAR(36),
	--推荐增加分值
	@HOLDUPSCORE FLOAT,
	--结果 0:失败 1:成功 2:已推荐过该公司
	@RESULT INT=0 OUTPUT
AS
BEGIN
	IF(EXISTS(SELECT 1 FROM tbl_RateHoldUp WHERE FromCompanyId=@FROMCOMPANYID AND ToCompanyId=@TOCOMPANYID))
	BEGIN
		SET @RESULT=2;
	END
	ELSE
	BEGIN
		DECLARE @error INT;
		SET @error = 0;
		SET XACT_ABORT ON;
		BEGIN TRANSACTION;
		--插入推荐信息
		INSERT INTO tbl_RateHoldUp
				   ([FromCompanyId]
				   ,[FromCompanyName]
				   ,[FromUserId]
				   ,[FromUserContactName]
				   ,[ToCompanyId]
				   ,[HoldUpScore])
			 VALUES
				   (@FROMCOMPANYID
				   ,@FROMCOMPANYNAME
				   ,@FROMUSERID
				   ,@FROMUSERCONTACTNAME
				   ,@TOCOMPANYID
				   ,@HOLDUPSCORE);
		SET @error = @@ERROR;
		--更新推荐的公司推荐次数与推荐分值
		
		--tbl_RateCountTotal.ScoreType=6 推荐总次数
		UPDATE tbl_RateCountTotal SET ScoreNumber=ScoreNumber+1 WHERE CompanyId=@TOCOMPANYID AND ScoreType=6;
		SET @error = @@ERROR;
		--tbl_RateScoreTotal.ScoreType=7 推荐总分值
		UPDATE tbl_RateScoreTotal SET ScorePoint=ScorePoint+@HOLDUPSCORE WHERE CompanyId=@TOCOMPANYID AND ScoreType=7;
		SET @error = @@ERROR;

		--更新公司表中的推荐次数
		UPDATE tbl_CompanyInfo SET CommendCount=CommendCount+1 WHERE ID=@TOCOMPANYID;
		SET @error = @@ERROR;
		IF(@error<>0)
		BEGIN
			SET @RESULT=0;
			ROLLBACK TRANSACTION;
			RETURN;
		END

		SET @RESULT=1;

		COMMIT TRANSACTION;
	END
END
GO