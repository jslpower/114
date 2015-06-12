-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		汪奇志
-- Create date: 2010-07-21
-- Description:	是否最近出团、最近出团数量处理
-- =============================================
ALTER PROCEDURE proc_TourList_RecentLeave
	@TourId CHAR(36),--团队编号
	@IsParentTour CHAR(1)='0'--是否是模板团
AS
BEGIN	
	IF(@TourId IS NULL OR @TourId='') 
		RETURN 0

	IF(@IsParentTour='0')
		SELECT @TourId=ParentTourId FROM tbl_TourList WHERE Id=@TourId

	DECLARE @RecentLeaveCount INT --最近出团数量
	SELECT @RecentLeaveCount=COUNT(*) FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE()
	UPDATE [tbl_TourList] SET [IsRecentLeave]='0',[RecentLeaveCount]=0,[IsRecentLeaveNoControl]='0' WHERE ParentTourId=@TourId
	--受团队状态控制的最近出团
	UPDATE [tbl_TourList] SET [IsRecentLeave]='1',[RecentLeaveCount]=@RecentLeaveCount WHERE Id=(	SELECT TOP 1 [Id] FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE() AND TourState=1 ORDER BY [LeaveDate] ASC)
	--不受团队状态控制的最近出团
	UPDATE [tbl_TourList] SET [IsRecentLeaveNoControl]='1',[RecentLeaveCount]=@RecentLeaveCount WHERE Id=(	SELECT TOP 1 [Id] FROM [tbl_TourList] WHERE [ParentTourID]=@TourId AND IsDelete='0' AND LeaveDate>=GETDATE() ORDER BY [LeaveDate] ASC)
END
GO
