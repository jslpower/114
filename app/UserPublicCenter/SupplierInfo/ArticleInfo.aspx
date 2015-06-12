<%@ Page Title="行业资讯详细" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master" AutoEventWireup="true"
    CodeBehind="ArticleInfo.aspx.cs" Inherits="UserPublicCenter.ArticleInfo" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/ArticleInfoControl.ascx" TagName="ArticleInfo"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top">
                <uc2:ArticleInfo runat="server" ID="ArticleInfo1" />
            </td>
            <td valign="top" width="250">
                <uc1:AdveControl runat="server" ID="AdveControl1" />
            </td>
        </tr>
    </table>
</asp:Content>
