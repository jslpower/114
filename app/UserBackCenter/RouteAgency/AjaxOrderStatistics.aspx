<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxOrderStatistics.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxOrderStatistics" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="100%" border="1" align="center" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    class="liststylesp">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            时间
        </th>
        <th class="list_basicbg">
            专线
        </th>
        <th class="list_basicbg">
            订单量
        </th>
        <th class="list_basicbg">
            总人数
        </th>
        <th class="list_basicbg">
            成人
        </th>
        <th class="list_basicbg">
            儿童
        </th>
        <th class="list_basicbg">
            销售总额
        </th>
        <th class="list_basicbg">
            结算总额
        </th>
        <th class="list_basicbg">
            功能
        </th>
    </tr>
    <asp:repeater runat="server" id="rpt_parentList">
                        <ItemTemplate>
                            <tr val="<%#Eval("AreaId") %>" >
                                <td height="30" align="left" nowrap="nowrap" bgcolor="#FFFFFF">
                                    <%#((DateTime)Eval("LeaveDateMin")).ToString("yyyy年MM月dd日")%>至<%#((DateTime)Eval("LeaveDateMax")).ToString("yyyy年MM月dd日")%>
                                </td>
                                <td align="left" bgcolor="#FFFFFF">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <a class="a_showOrders" href="/routeagency/allfitorders.aspx?goTimeS=<%#((DateTime)Eval("LeaveDateMin")).ToString("yyyy-MM-dd")%>&goTimeE=<%#((DateTime)Eval("LeaveDateMax")).ToString("yyyy-MM-dd")%>&lineId=<%#Eval("AreaId")%>"><%#Eval("TotalOrder")%></a>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalPeople")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalAdult")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#Eval("TotalChild")%>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSale").ToString())%>元
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <%#EyouSoft.Common.Utils.FilterEndOfTheZeroString(Eval("TotalSettle").ToString())%>元
                                </td>
                                <td align="center" nowrap="nowrap" bgcolor="#FFFFFF">
                                    <a href="javascript:void(0);" onclick="OrderStatistics.ShowDetailed(this)">明细查看</a>
                                </td>
                            </tr>
                            <%#GetList((int)Eval("AreaId"))%>
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
            <cc1:exportpageinfo id="ExportPageInfo1" visible="false" linktype="4" runat="server" />
        </td>
    </tr>
</table>
