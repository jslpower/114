<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EShopMenu.ascx.cs" Inherits="UserBackCenter.usercontrol.EShopControl.EShopMenu" %>
<asp:Panel ID="pnlDefaultMenu" runat="server">
    <div class="nav">
        <ul>
            <li><a href="/AddedServices/EShop/Default.aspx" class="nav-on">首 页</a></li>
            <li><a href="/AddedServices/EShop/Tours/Default.aspx">散拼计划</a></li>
            <li><a href="/AddedServices/EShop/Prices/NewPriceInfoList.aspx">最新报价</a></li>
            <li><a href="/AddedServices/EShop/Guide/TravelGuids1.aspx">出游指南</a></li>
            <li><a href="/AddedServices/EShop/Resource/ResourceList.aspx">旅游资源推荐</a></li>
            <li><a href="/AddedServices/EShop/News/NewsList.aspx">最新动态</a></li>
            <li><a href="/AddedServices/EShop/Abouts/AboutUs.aspx?IsAbuouts=1">关于我们</a></li>
        </ul>
    </div>
</asp:Panel>
