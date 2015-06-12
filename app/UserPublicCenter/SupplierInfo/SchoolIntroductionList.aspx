<%@ Page Title="资讯列表页" Language="C#" MasterPageFile="~/SupplierInfo/Supplier.Master"
    AutoEventWireup="true" CodeBehind="SchoolIntroductionList.aspx.cs" Inherits="UserPublicCenter.SchoolIntroductionList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/SupplierInfo/UserControl/SchoolLeftAndRight.ascx" TagName="SchoolLeftAndRight"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/ArticleListControl.ascx" TagName="ArticleList" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SupplierMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gongqiu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td valign="top" width="250">
                <uc1:SchoolLeftAndRight runat="server" ID="SchoolLeftAndRight1" />
            </td>
            <td valign="top">
                <uc2:ArticleList runat="server" ID="ArticleList1" />
            </td>
        </tr>
    </table>
</asp:Content>
