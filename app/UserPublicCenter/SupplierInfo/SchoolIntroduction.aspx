<%@ Page Title="同业学堂" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master"
    AutoEventWireup="true" CodeBehind="SchoolIntroduction.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.SchoolIntroduction" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/SchoolLeftAndRight.ascx" TagName="SchoolLeftAndRight"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/NewsControl.ascx" TagName="NewsControl"
    TagPrefix="uc2" %>
<%@ Register Src="~/SupplierInfo/UserControl/SWFControl.ascx" TagName="SWFControl"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:SchoolLeftAndRight runat="server" ID="SchoolLeftAndRight1" />
            </td>
            <td valign="top">
                <table width="710" border="0" cellspacing="0" cellpadding="0" style="margin-left: 10px;">
                    <tr>
                        <td valign="top">
                            <table width="710" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="464">
                                        <uc3:SWFControl runat="server" ID="SWFControl1" TopNumber="5" IsShowTitle="true"
                                            SwfType="3" SwfHeight="160" SwfWidth="464" />
                                    </td>
                                    <td width="10">
                                    </td>
                                    <td width="236" valign="top">
                                        <%--经验交流--%>
                                        <uc2:NewsControl runat="server" ContentCss="xtneibiao2" ID="NewsControl1" IsMarginTop="false"
                                            TopNumber="7" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                                <tr>
                                    <td class="xtjitiao">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="xtkuang">
                                        <%--新手计调--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl2" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" />
                                        <%--业务进阶--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl3" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" />
                                        <%--同业集结号--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl4" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" ContentCss="xtneibiaori" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0 " style="border: 1px solid #FEC698;
                                padding: 2px; height: 94px; margin-bottom: 10px; margin-top: 10px;">
                                <tr>
                                    <asp:Repeater runat="server" ID="rptAdv1">
                                        <ItemTemplate>
                                            <td>
                                                <a title="<%# Eval("Title") %>" target="<%# Eval("RedirectURL").ToString() == EyouSoft.Common.Utils.EmptyLinkCode ? "_self" : "_blank" %>" href="<%# Eval("RedirectURL") %>">
                                                    <img alt="<%# Eval("Title") %>" src='<%# Domain.FileSystem + Eval("ImgPath").ToString() %>'
                                                        width="170" height="84" /></a>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                                <tr>
                                    <td class="xtanli">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="xtkuang">
                                        <%--导游导购词--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl5" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" />
                                        <%--导游考试--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl6" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" />
                                        <%--带团分享--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl7" IsShowMore="true" TopNumber="7"
                                            IsMarginTop="false" ContentCss="xtneibiaori" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0 " style="border: 1px solid #FEC698;
                                padding: 2px; height: 94px; margin-bottom: 10px; margin-top: 10px;">
                                <tr>
                                    <asp:Repeater runat="server" ID="rptAdv2">
                                        <ItemTemplate>
                                            <td>
                                                <a title="<%# Eval("Title") %>" target="<%# Eval("RedirectURL").ToString() == EyouSoft.Common.Utils.EmptyLinkCode ? "_self" : "_blank" %>" href="<%# Eval("RedirectURL") %>">
                                                    <img alt="<%# Eval("Title") %>" src='<%# Domain.FileSystem + Eval("ImgPath").ToString() %>'
                                                        width="170" height="84" /></a>
                                            </td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                                <tr>
                                    <td class="xtdaoyou">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="xtkuang">
                                        <%--景区案例--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl8" IsShowMore="true" TopNumber="8"
                                            IsMarginTop="false" />
                                        <%--酒店案例--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl9" IsShowMore="true" TopNumber="8"
                                            IsMarginTop="false" />
                                        <%--旅行社案例--%>
                                        <uc2:NewsControl runat="server" ID="NewsControl10" IsShowMore="true" TopNumber="8"
                                            IsMarginTop="false" ContentCss="xtneibiaori" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
