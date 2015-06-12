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
-- Create date: 2010-07-19
-- Description:	设置团队虚拟剩余人数
-- =============================================
CREATE PROCEDURE proc_TourList_SetTourRemnantNumber
	@TourId CHAR(36),--团队编号
	@RemnantNumber INT,--虚拟剩余人数
	@Result INT OUTPUT--操作结果 1:success 0:failure
AS
BEGIN
	SET @Result=0

	DECLARE @PlanPeopleCount INT--计划人数
	DECLARE @OrderPeopleNumber INT--订单人数

	SELECT @PlanPeopleCount=PlanPeopleCount,@OrderPeopleNumber=OrderPeopleNumber FROM tbl_TourList WHERE Id=@TourId
	--虚拟剩余人数>实际剩余人数(计划人数-订单人数)时，虚拟剩余人数=实际剩余人数(计划人数-订单人数)
	SET @RemnantNumber=CASE WHEN @RemnantNumber>@PlanPeopleCount-@OrderPeopleNumber THEN @PlanPeopleCount-@OrderPeopleNumber ELSE @RemnantNumber END
	--更新虚拟剩余人数
	UPDATE [tbl_TourList] SET [RemnantNumber]=@RemnantNumber WHERE [Id]=@TourId

	IF(@RemnantNumber=0)--虚拟剩余人数=0，团队状态为收客时，设置团队状态为自动客满
	BEGIN
		UPDATE tbl_TourList SET TourState=4 WHERE Id=@TourId AND TourState=1
	END
	ELSE--虚拟剩余人数>0，团队状态为自动客满时，设置团队状态为收客
	BEGIN
		UPDATE tbl_TourList SET TourState=1 WHERE Id=@TourId AND TourState=4
	END

	SET @Result=1
END
GO
