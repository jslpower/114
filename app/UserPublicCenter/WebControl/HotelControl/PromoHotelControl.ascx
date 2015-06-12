<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoHotelControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.HotelControl.PromoHotelControl" %>
<div class="cuxiao" style="  height:258px;">
    <p class="more more02">
        <span>促销酒店</span></p>
    <ul>
        <asp:Repeater ID="rpt_Promotions" runat="server">
            <ItemTemplate>
                <li>
                    <%#Container.ItemIndex > 2 ? "<a href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(), CityId) + "\"  title=\"" + Eval("HotelName") + "\"  target=\"_blank\">" + EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(), 14, true) + "</a>" : "<a class=\"fg\" href=\"" + EyouSoft.Common.URLREWRITE.Hotel.GetHotelDetailsUrl(Eval("HotelCode").ToString(), CityId) + "\" title=\"" + Eval("HotelName") + "\"  target=\"_blank\">" + EyouSoft.Common.Utils.GetText(Eval("HotelName").ToString(), 14, true) + "</a>"%>
                    <span class="frb">￥<%#Convert.ToDecimal(Eval("MarketingPrice")).ToString("0")%></span>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
