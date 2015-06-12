<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StarHotelControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.HotelControl.StarHotelControl" %>
<div class="sidebar02_2_2" style=" padding-top:1px;* padding-top:12px;">
    <div class="more">
        <span>明星酒店展示</span></div>
    <ul style=" height:266px;">
        <asp:Repeater ID="rpt_StarHotel" runat="server">
            <ItemTemplate>
                <li><a href="<%#EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(),CityId)%>"  target="_blank">
                   <img src="<%#EyouSoft.HotelBI.Utils.ImagesUrl  %><%#Eval("HotelImg") %>"  width="135px" height="90px" /></a>
                    <a target="_blank" title="<%#Eval("HotelName") %> " title="<%#Eval("HotelName") %>" href="<%#EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(),CityId)%>"><%#EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(),7,false)%></a></li>
            </ItemTemplate>
        </asp:Repeater>
        <div class="clear">
        </div>
    </ul>
</div>
