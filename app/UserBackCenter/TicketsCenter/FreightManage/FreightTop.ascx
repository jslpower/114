<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FreightTop.ascx.cs"
    Inherits="UserBackCenter.TicketsCenter.FreightManage.FreightTop" %>
<table cellspacing="0" cellpadding="0" bordercolor="#99ccff" border="0" width="835px"
    class="userInfo">
    <tbody>
        <tr>
            <td height="35" align="left" colspan="3">
                <span style="margin-left: 10px; font-size: 14px; color: rgb(255, 106, 3); font-weight: bold;">
                    当前购买运价数    (可用/已用)</span>
            </td>
        </tr>
        <tr>
            <td width="70%">
                <table cellspacing="0" cellpadding="0" bordercolor="#99ccff" border="1" align="center"
                    width="100%" style="margin: 0pt 10px 10px;">
                    <tbody>
                        <tr>
                            <td height="25" width="20%">
                                &nbsp;
                            </td>
                            <th align="center" width="20%">
                                常规购买(条)
                            </th>
                            <th align="center" width="20%">
                                套餐购买(条)
                            </th>
                            <th align="center" width="20%">
                                促消购买(条)
                            </th>
                            <th align="center" width="20%">
                                合计(条)
                            </th>
                        </tr>
                        <tr>
                            <th height="25" align="center">
                                当月
                            </th>
                            <td align="center">
                                <asp:Label ID="frei_lblThisGeneral" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblThisGeneralUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblThisPackage" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblThisPackageUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblThisPromotions" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblThisPromotionsUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblThisAll" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th height="25" align="center">
                                次月
                            </th>
                            <td align="center">
                                <asp:Label ID="frei_lblNextGeneral" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblNextGeneralUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblNextPackage" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblNextPackageUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblNextPromotions" runat="server" Text=""></asp:Label>/<asp:Label ID="frei_lblNextPromotionsUsed" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="frei_lblNextAll" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td align="left" width="1%">
                <p style="text-indent: 24px; margin-left: 10px;">
                    &nbsp;</p>
            </td>
            <td align="left" width="29%" valign="top">
                <span style="text-indent: 24px; padding-right: 20px; font-weight: bold; color: rgb(255, 102, 0);">
                    备注：如次月少于当月系统将按照添加时间，从添加的时间早到晚关闭部分运价<a href="javascript:void(0);" onclick="topTab.open('/ticketscenter/purchaserouteship/default.aspx','购买运价航');return false;" > 点击购买运价位</a></span>
            </td>
        </tr>
    </tbody>
</table>
