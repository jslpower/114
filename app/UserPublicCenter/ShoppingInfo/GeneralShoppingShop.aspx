<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralShoppingShop.aspx.cs"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Inherits="UserPublicCenter.ShoppingInfo.GeneralShoppingShop" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register Src="../WebControl/AdveControl.ascx" TagName="AdveControl" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc4" %>
<%@ Register Src="../WebControl/GeneralShopControl.ascx" TagName="GeneralShopControl"
    TagPrefix="uc5" %>
<%@ Register Src="../WebControl/ShopAdvControl.ascx" TagName="ShopAdvControl" TagPrefix="uc6" %>
<asp:Content ContentPlaceHolderID="Main" ID="Default_ctMain" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <uc5:GeneralShopControl ID="GeneralShopControl1" runat="server" />
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
                <uc6:ShopAdvControl ID="ShopAdvControl1" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</asp:Content>
