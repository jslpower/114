<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestInterview.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.GuestInterview" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/GuestInterviewMenu.ascx" TagPrefix="cc1" TagName="GuestInterviewMenu" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>访谈介绍</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:GuestInterviewMenu runat="server" ID="GuestInterviewMenu1"></cc1:GuestInterviewMenu>
    <table width="99%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30">
                访谈介绍
            </td>
        </tr>
        <tr>
            <td>
                <FCKeditorV2:FCKeditor runat="server" ID="txtGuestInfo" Height="300">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                同业交流专区
            </td>
        </tr>
        <tr>
            <td>
                <FCKeditorV2:FCKeditor runat="server" ID="txtTongye" Height="300">
                </FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td height="30" align="center">
                <asp:Button runat="server" ID="btnSave" CssClass="baocun_an" Text="保 存" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
