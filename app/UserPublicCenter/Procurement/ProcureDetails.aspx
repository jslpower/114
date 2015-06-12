<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcureDetails.aspx.cs"
    Inherits="UserPublicCenter.Procurement.ProcureDetails" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="../WebControl/GeneralShopControl.ascx" TagName="GeneralShopControl"
    TagPrefix="uc1" %>
<%@ Register Src="../AdvancedShop/AdvancedControl1.ascx" TagName="AdvancedControl1"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <link href="<%=CssManage.GetCssFilePath("gouwu") %>" rel="stylesheet" type="text/css" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
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
                        <td>
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/lxfs.gif" width="220" height="28" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="border-left: 1px solid #DDDDDD; border-right: 1px solid #DDDDDD;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 10px;">
                                <tr>
                                    <td width="30%" height="25" align="left">
                                        联系人：
                                    </td>
                                    <td width="70%" align="left">
                                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                        &nbsp;
                                        <%=Utils.GetBigImgMQ(MQ) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        电 话：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblTelPhone" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left">
                                        传 真：
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="25" align="left" valign="top">
                                        地 址：
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/lxfsbb.gif" width="220" height="10" />
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="margin10">
                    <tr>
                        <td align="left" class="gwright">
                            &nbsp;</td>
                    </tr>
                </table>
                <iframe src="http://m.weather.com.cn/m/pn12/weather.htm " width="220px" height="130px"
                    marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0" scrolling="no">
                </iframe>
            </td>
        </tr>
    </table>
</asp:Content>
