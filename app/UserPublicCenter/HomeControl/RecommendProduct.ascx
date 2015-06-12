<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecommendProduct.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.RecommendProduct" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div class="zuixinxl">
    <h3>
        <span><a href="<%= EyouSoft.Common.Domain.UserPublicCenter%>/TourManage/TourList.aspx">
            更多&gt;&gt;</a></span>最新线路</h3>
    <div class="zuixinxl_li">
        <ul>
            <li runat="server" id="emptydata" visible="false">暂无最新线路信息</li>
            <asp:Repeater ID="rptProduct" runat="server">
                <ItemTemplate>
                    <li><a href="<%# EyouSoft.Common.URLREWRITE.Tour.GetTourToUrl(string.IsNullOrEmpty(Eval("Id").ToString()) ? 0 : long.Parse(Eval("Id").ToString()),CityId) %>"
                        target="_blank" title='<%# Eval("RouteName") %>'>·<%# EyouSoft.Common.Utils.GetText2(Convert.ToString(Eval("RouteName")), 15,false)%></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
