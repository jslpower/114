<%@ Page Title="嘉宾访谈详细" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master" AutoEventWireup="true"
    CodeBehind="HonoredGuestInfo.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.HonoredGuestInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/HonoredGuestLeftAndRight.ascx" TagName="HonoredGuestLeft"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("gongqiu") %>" />
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("body") %>" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-right: 10px;">
                    <tr>
                        <td valign="top" style="border: 1px solid #C4C4C4; padding: 1px; text-align: left;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="background: url(<%=ImageServerPath %>/images/UserPublicCenter/bg_new.gif) repeat-x;
                                padding: 10px;">
                                <tr>
                                    <td class="huise1" align="left">
                                        &nbsp;<a href="/SupplierInfo/HonoredGuest.aspx">嘉宾访谈</a> > 正文
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="padding: 10px;">
                                <tr>
                                    <td align="center">
                                        <asp:Literal runat="server" ID="ltrImg"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 14px;">
                                        <h1 style="text-align: center; padding-top: 15px;"><asp:Literal runat="server" ID="ltrTitle"></asp:Literal></h1>
                                        <strong>文章摘要：</strong>
                                        <br />
                                        <asp:Literal runat="server" ID="ltrInfo"></asp:Literal>
                                        <br />
                                        <strong>同业观点：</strong>
                                        <br />
                                        <asp:Literal runat="server" ID="ltrView1"></asp:Literal>
                                        <br />
                                        <br />
                                        <asp:Literal runat="server" ID="ltrView2"></asp:Literal>
                                        <br />
                                        <br />
                                        <asp:Literal runat="server" ID="ltrView3"></asp:Literal>
                                        <br />
                                        <br />
                                        <strong>小编总结：</strong>
                                        <br />
                                        <asp:Literal runat="server" ID="ltrSummary"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top" width="250">
                <uc1:HonoredGuestLeft runat="server" ID="HonoredGuestLeft1" />
            </td>
        </tr>
    </table>
</asp:Content>
