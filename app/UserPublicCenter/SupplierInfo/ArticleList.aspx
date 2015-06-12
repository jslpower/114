<%@ Page Title="行业资讯列表" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master" AutoEventWireup="true"
    CodeBehind="ArticleList.aspx.cs" Inherits="UserPublicCenter.SupplierInfo.ArticleList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/ArticleListControl.ascx" TagName="ArticleListControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:AdveControl runat="server" ID="AdveControl1" />
            </td>
            <td valign="top">
                <uc2:ArticleListControl runat="server" ID="ArticleListControl1" />
            </td>
        </tr>
    </table>
</asp:Content>
