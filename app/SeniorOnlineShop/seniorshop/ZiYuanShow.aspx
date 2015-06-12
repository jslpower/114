<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiYuanShow.aspx.cs" Inherits="SeniorOnlineShop.seniorshop.ZiYuanShow" MasterPageFile="~/master/SeniorShop.Master" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ID="c1" runat="server" ContentPlaceHolderID="c1">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringht">
                旅游资源推荐
            </td>
        </tr>
        <tr>
            <td>
                <div class="neiringhtk">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maint5">
                        <tr>
                            <td width="91%" class="hangbg" align="center">
                                <h1>
                                    <asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h1>
                            </td>
                        </tr>
                    </table>
                    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="color: #666; line-height: 30px;">　　
                                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #666; line-height: 30px; text-align: right">
                                <asp:Literal ID="ltrIssuetime" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>