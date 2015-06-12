<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelAccountInfo.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.HotelAccountInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看采购账户</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" border="0" align="center" cellspacing="1"  cellpadding="1" bgcolor="#B7D2F7" style="margin-top:5px;">
      <asp:Label ID="labHotelAoccountList" runat="server"></asp:Label>
    </table>
    </form>
</body>
</html>
