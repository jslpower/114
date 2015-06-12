<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultTours.aspx.cs" Inherits="SeniorOnlineShop.template4.DefaultTours" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<table width="665" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#E3E3E3"
    style="margin: 0px auto;">
    <thead>
        <tr>
            <th width="70" height="26" align="center" bgcolor="#F4F4F4">
                出发
            </th>
            <th width="240" height="26" align="center" bgcolor="#F4F4F4">
                团队基本信息
            </th>
            <th width="111" height="26" align="center" bgcolor="#F4F4F4">
                出团时间/空位
            </th>
            <th width="82" height="26" align="center" bgcolor="#F4F4F4">
                成人价
            </th>
            <th width="87" height="26" align="center" bgcolor="#F4F4F4">
                儿童价
            </th>
            <th width="71" height="26" align="center" bgcolor="#F4F4F4">
                交易操作
            </th>
        </tr>
    </thead>
    <tbody>
        <cc1:CustomRepeater ID="rptTours" runat="server" EmptyText="<tr><td colspan='7' style='line-height:30px'>暂无相关旅游线路</td></tr>"
            OnItemCreated="rptToursCreated">
            <ItemTemplate>
                <tr>
                    <td align="center" width="70">
                        <asp:literal runat="server" id="ltrPriceDFC">￥2980/2650</asp:literal>
                    </td>
                    <td align="left" class="jiange">
                        <span style="float: left"><a target="_blank" style="font-size: 13px" href="<%#Utils.GenerateShopPageUrl2("/TourDetail2_"+Eval("RouteId"),Utils.GetQueryStringValue("cid")) %>" title="<%# Eval("RouteName") %>">
                            <%#Utils.GetText(Eval("RouteName").ToString(), 15, false)%></a> </span>
                        <%#GetRecommendType(((int)Eval("RecommendType")).ToString())%>
                    </td>
                    <td align="center" class="jiange2">
                        <asp:literal runat="server" id="ltrLeaveDate"></asp:literal>
                        <br />
                        <asp:literal runat="server" id="ltrRemnantNumber">剩:10</asp:literal>
                    </td>
                    <td align="center">
                        <asp:literal runat="server" id="ltrPriceMS">￥2980/2650</asp:literal>
                        <br />
                    </td>
                    <td align="center" runat="server" id="tdPriceTH">
                        <asp:literal runat="server" id="ltrPriceTH">￥2980/2650</asp:literal>
                    </td>
                    <td align="center" width="71">
                        <%# GetOrderByRoute(Eval("TourId").ToString())%><br />
                        <%=MqURlHtml%>
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </tbody>
</table>
<link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
<link href="<%= EyouSoft.Common.CssManage.GetCssFilePath("boxy2011") %>" rel="Stylesheet"
    type="text/css" />

<script type="text/javascript">
    $(document).ready(function() {
        $(".tourcalendar a[rel='calendar']").click(function() {
            var o = $(this);
            ClickCalendar(o.attr("pid"), this, parseInt(o.attr("areatype")));
            return false;
        });

        $(".goumai0").click(function() {
            if ("<%=IsLogin %>" == "False") {
                Boxy.iframeDialog({ title: "马上登录同业114", iframeUrl: "<%=GetLoginUrl() %>", width: "400px", height: "250px", modal: true }); return false;
            }
        });
    });
</script>

