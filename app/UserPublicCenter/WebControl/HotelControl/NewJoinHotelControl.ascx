<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewJoinHotelControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.HotelControl.NewJoinHotelControl" %>
<div class="sidebar_2">
    <p class="more">
        <span>最新加入酒店</span></p>
    <ul>
        <asp:Repeater ID="rpt_NewHotel" runat="server">
            <ItemTemplate>
                <li ><a href="<%#EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(),CityId)%>" title="<%#Eval("HotelName") %>" target="_blank">
                    <%#EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(),11,true)%></a><span class="frb" style="font-weight:lighter">[<%#Convert.ToDateTime( Eval("IssueTime")).ToString("yyyy-MM-dd")%>]</span>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>