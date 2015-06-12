<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedControl1.ascx.cs"
    Inherits="UserPublicCenter.AdvancedShop.AdvancedControl1" %>
<asp:Repeater ID="rptNewsList" runat="server">
    <ItemTemplate>
        <li><a href="ShopNewsInfo.aspx?NewsId=<%#Eval("Id") %>&CityId=<%#this.CityId %>">
            <%# EyouSoft.Common.Utils.GetText(Eval("AfficheTitle").ToString(),15,true)%>
        </a></li>
    </ItemTemplate>
</asp:Repeater>
