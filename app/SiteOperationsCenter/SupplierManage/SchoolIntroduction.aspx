<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolIntroduction.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.SchoolIntroduction" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SchoolMenu.ascx" TagPrefix="cc1" TagName="SchoolMenu" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学堂介绍</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:SchoolMenu runat="server" ID="SchoolMenu1" MenuIndex="1">
    </cc1:SchoolMenu>
    <table width="99%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <strong>介绍：</strong><br>
                <FCKeditorV2:FCKeditor runat="server" ID="txtIntroduction" Height="450"></FCKeditorV2:FCKeditor>
                <br>
            </td>
        </tr>
        <tr>
            <td height="30" align="center">
                <asp:Button runat="server" ID="btnSave" Text="保 存" CssClass="baocun_an" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
