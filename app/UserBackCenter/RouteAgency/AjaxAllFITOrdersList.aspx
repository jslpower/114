<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxAllFITOrdersList.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxAllFITOrdersList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#B9D3E7"
    class="liststyle" style="margin-top: 1px;">
    <tr class="list_basicbg">
        <th nowrap="nowrap" class="list_basicbg">
            订单号
        </th>
        <th nowrap="nowrap" class="list_basicbg" style="width: 450px">
            线路
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            出发时间
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            组团社
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            联系人
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            电话
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            预订时间
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            人数
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            订单状态
        </th>
        <th nowrap="nowrap" class="list_basicbg">
            支付状态
        </th>
        <%--<th nowrap="nowrap" class="list_basicbg">
                            状态修改
                        </th>--%>
        <th class="list_basicbg">
            结算金额
        </th>
        <th class="list_basicbg">
            打印
        </th>
        <th class="list_basicbg">
            操作
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_list">
                        <ItemTemplate>
                            <tr <%# Container.ItemIndex%2==0? "class=odd":"" %>>
                                <td height="30" align="left">
                                   <%#Eval("OrderNo")%>
                                </td>
                                <td align="left" style=" padding-left:10px; width:150px">
                                    <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#((DateTime)Eval("LeaveDate")).ToString("MM-dd")%>
                                </td>
                                <td align="left">
                                  <%#Eval("TravelName")%>
                                </td>
                                <td align="center">
                                   <%#Eval("TravelContact")%>
                                </td>
                                <td align="center">
                                   <%#Eval("TravelTel")%>
                                </td>
                                <td align="center">
                                <%#((DateTime)Eval("IssueTime")).ToString("MM-dd HH:mm")%>
                                </td>
                                <td align="center">
                                   <%#Eval("AdultNum")%><sup>+<%#Eval("ChildrenNum")%></sup>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderStatus")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PaymentStatus")%>
                                </td>
                                <td align="left">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSettlementPrice").ToString())%>
                                </td>
                                <%--<td align="left">
                                    <a href="#" class="basic_btn"><span>取消</span></a>
                                </td>--%>
                                <td align="center">
                                    <a href='PrintPage/TeamConfirm.aspx?OrderId=<%#Eval("OrderId") %>' target="_blank">团队确认单</a>
                                </td>
                                <td align="center" nowrap="nowrap">
                                    <a class="a_show" href="/Order/RouteAgency/OrderStateUpdate.aspx?orderID=<%#Eval("OrderId") %>">查看</a><br /> 
                                    <a target="_blank" href="/RouteAgency/OrderStateLog.aspx?orderID=<%#Eval("OrderId") %>">日志</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:repeater>
</table>
<asp:panel runat="server" id="pnlNodata">
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
