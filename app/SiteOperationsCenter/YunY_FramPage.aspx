<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YunY_FramPage.aspx.cs" Inherits="SiteOperationsCenter.PlatformManagement.YunY_FramPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>运营后台框架页面</title>
</head>

<frameset id="attachucp" framespacing="0" border="0" frameborder="no" cols="160,10,*" rows="*">
	<frame noresize="" scrolling="no" frameborder="no" name="leftFrame" src="LeftMenuList.aspx"></frame>
	<frame id="leftbar" scrolling="no" noresize="" name="leftbar" src="swich.html"></frame>

	<frameset rows="30,*" cols="*" framespacing="0" frameborder="no" border="0">
    	<frame src="Top.aspx" name="topFrame" scrolling="No" noresize />
		<frame scrolling="yes" noresize="" border="0" name="mainFrame" src="PlatformManagement/BasicInfoManagement.aspx"></frame>
	</frameset>

</frameset>
<noframes></noframes>

</html>