

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author: 周文超 
-- Create date: 2010-08-13
-- Description:	MQ查找好友
-- =============================================
alter view [dbo].[view_im_member_SearchFriend]
as
select im.im_uid,im.im_displayname,im.im_username,im.im_status,u.Id as UserId,c.CompanyType,c.CompanyName,city.CityName,c.ProvinceId,c.CityId,
IsOnline = 
 CASE 
   WHEN im.im_status > 11 THEN 1
   ELSE 0
END , u.ContactName,C.Id AS CompanyId
from im_member im,tbl_CompanyUser u,tbl_CompanyInfo c,tbl_SysCity city
where im.bs_uid = u.Id and u.CompanyId=c.Id and c.CityId=city.Id and c.IsEnabled = '1' and c.IsDeleted = '0' and c.IsCheck = '1' and u.IsDeleted = '0' and u.IsEnable = '1'

GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

