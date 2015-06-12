<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewActivityControl.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.NewActivityControl" %>
<%@ Import Namespace="EyouSoft.Common" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="heise12">
    <asp:Repeater runat="server" ID="rptNewActivity">
        <ItemTemplate>
            <tr>
                <td>
                    <a href="/PlaneInfo/NewsDetailInfo.aspx?TypeID=<%# (int)Eval("AfficheClass") %>&NewsID=<%# Eval("ID") %>">
                        ·<%# Utils.GetText(Eval("AfficheTitle").ToString(), 17) %></a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
