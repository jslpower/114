<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpecialHotelControl.ascx.cs" Inherits="UserPublicCenter.WebControl.HotelControl.SpecialHotelControl" %>
<div class="sidebar_2">
    <p class="more moreClassPage">
        <span>特推酒店</span></p>
    <ul>
        <asp:Repeater ID="rpt_Special" runat="server">
            <ItemTemplate>
                <li><a title="<%#Eval("HotelName") %>" href="<%#EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(),CityId)%>"  target="_blank"><%#EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(),11,true)%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>