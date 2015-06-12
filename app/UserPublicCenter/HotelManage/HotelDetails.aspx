<%@ Page  Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HotelDetails.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelDetails" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/GeneralShopControl.ascx" TagName="GeneralShopControl"
    TagPrefix="uc1" %>
<%@ Register Src="HotelRightControl.ascx" TagName="HotelRightControl" TagPrefix="uc2" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc3" %>
<%@ Register src="NewAttrHotelControl.ascx" tagname="NewAttrHotelControl" tagprefix="uc4" %>
<%@ Register src="DiscountControl.ascx" tagname="DiscountControl" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc3:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="maintop15">
        <tr>
            <td width="735" valign="top">
                <uc1:GeneralShopControl ID="GeneralShopControl1" runat="server" />
            </td>
            <td width="15">
                &nbsp;
            </td>
            <td width="220" valign="top">
               
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="jdrh">
                            本周最热酒店
                        </td>
                    </tr>
                    <tr>
                        <td class="jdrk">
                            <uc4:newattrhotelcontrol id="NewAttrHotelControl1" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="gwright">
                            <div class="gwrhang">
                                特价房
                            </div>
                            <uc5:discountcontrol id="DiscountControl2" runat="server" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop10">
                    <tr>
                        <td>
                            <uc2:HotelRightControl ID="HotelRightControl1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
