<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralShopControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.GeneralShopControl" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="15" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/line2.gif) no-repeat;
            width: 730px;">
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td style="border: 1px solid #DFDFDF; border-top: 0px solid #ffffff;">
            <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="table-layout: fixed">
                <tr>
                    <td height="32" align="center" style="border-bottom: 1px solid #F1C37B; font-size: 24px;
                        line-height: 120%;">
                        <strong>
                            <asp:Label ID="lbl_Title" runat="server"></asp:Label></strong>
                    </td>
                </tr>
                <tr id="isShowImprotent" runat="server">
                    <td align="left" style="padding: 3px;">
                        <table width="100%" border="0" cellpadding="3" cellspacing="0" bgcolor="#FFFDF2"
                            style="border: 1px dashed #ECECEC">
                            <tr>
                                <td>
                                    <strong>主要优势：</strong><asp:Label ID="lbl_Importent" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding: 10px; word-break: break-all; white-space: normal;
                        word-wrap: break-word;">
                        <p>
                            <asp:Literal ID="lit_Content" runat="server"></asp:Literal></p>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
