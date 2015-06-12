<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotHotelControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.HotelControl.HotHotelControl" %>
<div class="sidebar_2">
    <p class="more moreClassPage">
        <span>最新加入酒店</span></p>
    <ul>
        <asp:Repeater ID="rpt_NewHotel" runat="server">
            <ItemTemplate>
                <li class="Number0<%#Container.ItemIndex+1 %>"><a href="<%#EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(),CityId)%>"  target="_blank">
                    <%#EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(),11,true)%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
