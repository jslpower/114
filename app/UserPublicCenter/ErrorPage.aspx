<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="UserPublicCenter.ErrorPage"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<asp:Content ID="ErrorPage" runat="server" ContentPlaceHolderID="Main">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu" runat="server" />
    <table width="80%" border="0" align="center" cellpadding="10" cellspacing="0" style="margin-top: 40px;
        margin-bottom: 80px;">
        <tr>
            <td width="11%" align="center" valign="top">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/errorico.gif" width="86" height="81" />
            </td>
            <td width="89%" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="62" align="left">
                            <h1 style="font-size: 16px; line-height: 120%;">
                                <asp:Label ID="lblErrorText" runat="server"></asp:Label></h1>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <span style="color: #ff0000;" style="display:none">原因：</span><asp:Literal ID="ltrCause" runat="server"></asp:Literal><br />
                            您可以：<br />
                            1、<a href="/Default.aspx">返回同业114首页</a><asp:Literal ID="ltrCustom" runat="server"></asp:Literal><br />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
