<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxOrderList.aspx.cs"
    Inherits="IMFrame.RouteAgency.TourOrder.AjaxOrderList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<asp:repeater id="Repeater1" runat="server">
    <HeaderTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td style="border-bottom: 1px dashed #cccccc; line-height: 16px; padding-top: 5px;">
                <a class="a_Order" href="<%# GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + "/order/routeagency/OrderStateUpdate.aspx?orderID=" + Eval("OrderId"))%>" target="_blank">
                    订单号：<%# Eval("OrderNo")%><span style="color:#009900">【<%# Eval("TravelName")%>】</span>预定<span style=" color:#990066">【<%#Eval("LeaveDate")==null?"":Convert.ToDateTime(Eval("LeaveDate")).ToString("yyyy-MM-dd")%>】</span><%#Eval("RouteName")%> <span style="color:#3300CC">【<%# Convert.ToUInt32(Eval("AdultNum")) + Convert.ToUInt32(Eval("ChildrenNum"))%>  个位置】</span>。
                    <strong>点击处理</strong></a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:repeater>
<asp:panel id="Nodata" runat="server" visible="false">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="height: 20px; float:left;">
        <tr>
            <td align="left" valign="middle">
                暂无数据
            </td>
        </tr>
    </table>
</asp:panel>
<div class="digg" style="text-align: center">
    <table>
        <tr>
            <td align="center">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="MostEasyNewButtonStyle"
                    HrefType="JsHref" runat="server"></cc2:ExporPageInfoSelect>
            </td>
        </tr>
    </table>
</div>
