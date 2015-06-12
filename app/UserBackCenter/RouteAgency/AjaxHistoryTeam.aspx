<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxHistoryTeam.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxHistoryTeam" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    style="width: 100%; margin-top: 1px;" class="liststylesp">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            团号
        </th>
        <th class="list_basicbg" style="width: 450px">
            线路名称
        </th>
        <th class="list_basicbg">
            出团日期
        </th>
        <th class="list_basicbg">
            返回日期
        </th>
        <th class="list_basicbg">
            团队人数
        </th>
        <th class="list_basicbg">
            订单量
        </th>
        <th class="list_basicbg">
            实收人数
        </th>
        <th class="list_basicbg">
            成人价
        </th>
        <th class="list_basicbg">
            儿童价
        </th>
        <th class="list_basicbg">
            打印
        </th>
        <th class="list_basicbg">
            功能
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_list">
        <ItemTemplate>
            <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                <td align="center">
                    <%#Eval("TourNo")%>
                </td>
                <td align="left">
                    <a href='/PrintPage/LineTourInfo.aspx?RouteId=<%#Eval("RouteId") %>' target="_blank"><%#Eval("RouteName")%></a>
                </td>
                <td align="center">
                    <%#((DateTime)Eval("LeaveDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("LeaveDate")).ToString("ddd")%>)
                </td>
                <td align="center">
                    <%#((DateTime)Eval("ComeBackDate")).ToString("MM/dd")%>(<%#((DateTime)Eval("ComeBackDate")).ToString("ddd")%>)
                </td>
                <td align="center">
                     <%#Eval("TourNum")%>
                </td>
                <td align="center">
                     <%#Eval("OrderNum")%>
                </td>
                <td align="center">
                     <%#Eval("OrderPeopleNum")%>
                </td>
                <td align="center">
                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                </td>
                <td align="center">
                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                </td>
                <td align="center" nowrap="nowrap">
                    <a href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>&OrderStatus=lishi' target="_blank">名单</a>
                </td>
                <td align="center" nowrap="nowrap" class="list-caozuo">
                     <a class="a_orders" href="/RouteAgency/AllFITOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                     <a class="a_Del" ordernum="<%#Eval("OrderNum") %>" href="javascript:void(0);" tourid="<%#Eval("TourId") %>">删除</a>
                </td>
            </tr>
        </ItemTemplate>
    </asp:repeater>
</table>
<asp:panel id="pnlNodata" runat="server" visible="false">
                 <table cellpadding="1"cellspacing="0"style="width:100%;margin-top:1px;">
                    <tr>
                        <td>暂无线路数据!</td>
                    </tr>
                 </table>
             </asp:panel>
<table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="right"
    border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
            <cc1:ExportPageInfo ID="ExportPageInfo1" Visible="false" LinkType="4" runat="server" />
        </td>
    </tr>
</table>
