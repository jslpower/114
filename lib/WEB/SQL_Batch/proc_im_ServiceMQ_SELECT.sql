GO
-- =============================================
-- Author:		张志瑜
-- Create date: 2010-07-09
-- Description:	获得指定省份下的MQ客服号码
-- =============================================
CREATE PROC [dbo].[proc_im_ServiceMQ_SELECT]	
	@ProvinceId INT   --省份ID
AS
BEGIN		
	DECLARE @ServiceMQIds NVARCHAR(MAX);
	--获取客服号码为指定省份的客服MQ编号,多个用","间隔
	SET @ServiceMQIds= STUFF((SELECT ','+CAST(MQId AS NVARCHAR(255)) FROM im_ServiceMQ WHERE ProvinceId=@provinceId AND IsDefault=0 FOR XML PATH('')),1,1,'')	
	IF(@ServiceMQIds IS NULL)--若指定省份暂无设定客服，则获取默认的客服MQ编号,多个用","间隔
	BEGIN
		SET @ServiceMQIds= STUFF((SELECT ','+CAST(MQId AS NVARCHAR(255)) FROM im_ServiceMQ WHERE IsDefault=1 FOR XML PATH('')),1,1,'')
	END
	SELECT @ServiceMQIds
END
GO