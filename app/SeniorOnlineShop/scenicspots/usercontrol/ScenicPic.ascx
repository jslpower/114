<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScenicPic.ascx.cs" Inherits="SeniorOnlineShop.scenicspots.usercontrol.ScenicPic" %>
<ul class="contentpic">
<asp:DataList runat="server" ID="dlPics" onitemdatabound="dlPics_ItemDataBound">
    <ItemTemplate>
        <li>
            <asp:Literal runat="server" ID="ltimg"></asp:Literal>
        </li>
    </ItemTemplate>
</asp:DataList>
</ul>