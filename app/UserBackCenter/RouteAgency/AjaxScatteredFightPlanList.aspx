<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxScatteredFightPlanList.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxScatteredFightPlanList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    style="width: 100%; margin-top: 1px;" class="liststylesp">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            <input type="checkbox" id="chk_All" />
            全
        </th>
        <th class="list_basicbg">
            团号
        </th>
        <th class="list_basicbg"  style="width: 400px">
            线路名称
        </th>
        <th class="list_basicbg">
            出团日期
        </th>
        <th class="list_basicbg">
            报名截止
        </th>
        <th class="list_basicbg">
            人数
        </th>
        <th class="list_basicbg">
            余位
        </th>
        <th class="list_basicbg">
            推荐类型
        </th>
        <th class="list_basicbg">
            成人价
        </th>
        <th class="list_basicbg">
            儿童价
        </th>
        <th class="list_basicbg ShowJSPrice" style="display: none">
            结算价
        </th>
        <th class="list_basicbg">
            单房差
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
                            <input type="checkbox" class="list_id" value="<%#Eval("TourId") %>" />
                        </td>
                        <td align="center">
                            <%#Eval("TourNo")%>
                        </td>
                        <td align="center">
                            <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a>
                        </td>
                        <td align="center">
                            <%#((DateTime)Eval("LeaveDate")).ToString("yyyy-MM-dd") %>
                        </td>
                        <td align="center">
                            <%#((DateTime)Eval("RegistrationEndDate")).ToString("yyyy-MM-dd")%>
                        </td>
                        <td align="center">
                           <%#Eval("TourNum")%>
                        </td>
                        <td align="center">
                            <%#Eval("MoreThan")%>
                        </td>
                        <td align="center">
                            <span class="state<%#(int)Eval("RecommendType")-1%>"><%#Eval("RecommendType").ToString() == "无" ? "" : Eval("RecommendType")%></span>
                        </td>
                        <td align="center">
                           <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailAdultPrice").ToString())%>
                        </td>
                        <td align="center">
                          <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("RetailChildrenPrice").ToString())%>
                        </td>
                        <td align="center" nowrap="NOWRAP" class="ShowJSPrice"  style="display:none">
                           <span class="ff0000"> <%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementAudltPrice").ToString())%></span>
                            /
                            <span class="ff0000"><%# EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("SettlementChildrenPrice").ToString())%></span>
                        </td>
                        <td align="center">
                           <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MarketPrice").ToString()) == "0" ? "" : EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("MarketPrice").ToString())%>
                        </td>
                        
                        <td align="center" nowrap="nowrap">
                            <a target="_blank" href='/PrintPage/TouristInfo.aspx?TeamId=<%#Eval("TourId") %>'>名单</a> <a  href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">
                                行程单</a><br />
                            <a href='/PrintPage/OutGroupNotices.aspx?TeamId=<%#Eval("TourId") %>' target="_blank">出团通知书</a>
                        </td>
                        <td align="center" nowrap="nowrap" class="list-caozuo">
                            <a href="/Order/RouteAgency/AddOrderByRoute.aspx?tourID=<%#Eval("TourId") %>" class="boto">代定</a> <a href="/RouteAgency/UpdateScatteredFightPlan.aspx?tourId=<%#Eval("TourId") %>" class="a_Update">修改</a><br />
                            <a href="/RouteAgency/PlanList.aspx?routeId=<%#Eval("RouteId") %>" class="a_goPlanList">计划</a> 
                            <a class="a_orders" href="/Order/RouteAgency/NewOrders.aspx?tourId=<%#Eval("TourId") %>">订单</a>
                            <a class="a_Del" ordernum="<%#Eval("OrderNum") %>" href="javascript:void(0);" tourid="<%#Eval("TourId") %>">删除</a>
                        </td>
                    </tr>
                    </ItemTemplate>
                </asp:repeater>
</table>
<asp:panel runat="server" id="pnlNodata" visible="false">
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
