<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeeSupplier.aspx.cs" Inherits="SiteOperationsCenter.SupplierManage.SeeSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>供求信息查看</title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="margin-left: 10px;" width="710" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="text-align: center;">
                <h1>
                    <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h1>
            </td>
        </tr>
        <tr>
            <td style="border-bottom: 1px solid rgb(204, 204, 204); height: 40px;" align="center">
                <span class="huise">
                    <asp:Literal runat="server" ID="ltrTime"></asp:Literal></span>&nbsp;&nbsp;<asp:Literal
                        runat="server" ID="ltrCompanyName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="heise" style="padding-top: 15px;">
                <div style="text-align: center;">
                    <asp:Literal runat="server" ID="ltrImg"></asp:Literal>
                </div>
                <br>
                <asp:Literal runat="server" ID="ltrInfo"></asp:Literal>
                <br />
                <asp:Literal runat="server" ID="ltrDownLoad"></asp:Literal>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
