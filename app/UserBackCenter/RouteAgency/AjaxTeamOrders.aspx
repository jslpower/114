<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxTeamOrders.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxTeamOrders" %>

<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 10px;
    margin-bottom: 3px;">
    <tr>
        <td align="right" valign="top">
            <table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
                class="liststylesp">
                <tr class="list_basicbg">
                    <th class="list_basicbg">
                        出发城市
                    </th>
                    <th class="list_basicbg">
                        线路名称
                    </th>
                    <th class="list_basicbg">
                        组团社联系人
                    </th>
                    <th class="list_basicbg">
                        团队时间
                    </th>
                    <th class="list_basicbg">
                        预定时间
                    </th>
                    <th class="list_basicbg">
                        人数
                    </th>
                    <th class="list_basicbg">
                        状态
                    </th>
                    <th class="list_basicbg">
                        状态修改
                    </th>
                    <th class="list_basicbg">
                        操作
                    </th>
                </tr>
                <asp:repeater runat="server">
                    <ItemTemplate>
                         <tr>
                            <td align="center">
                                <%#Eval("") %>
                            </td>
                            <td align="left">
                                <a href="../../团队确认单.html"><%#Eval("") %></a><br />
                                组团社：<a href="#"><%#Eval("") %> </a>
                            </td>
                            <td align="left">
                                <%#Eval("") %><br />
                                <%#Eval("") %>
                            </td>
                            <td align="center">
                                <%#Eval("") %>
                            </td>
                            <td align="center">
                                <%#Eval("") %>
                            </td>
                            <td align="center">
                                <%#Eval("") %><sup>+<%#Eval("") %></sup>
                            </td>
                            <td align="left">
                                <%#Eval("") %>
                            </td>
                            <td align="left">
                                <a href="" class="zhuangtai_btn"><span>团队状态</span></a>
                            </td>
                            <td align="center" nowrap="nowrap">
                                <a href="duli-dindan-ck.html">查看</a> <a href="#">取消</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:repeater>
                <tr>
                    <td align="center">
                        杭州
                    </td>
                    <td align="left">
                        <a href="../../团队确认单.html">澳新旅游15天</a><br />
                        组团社：<a href="#">杭州猎鹰旅行社 </a>
                    </td>
                    <td align="left">
                        耿先生<br />
                        122343413
                    </td>
                    <td align="center">
                        07-29
                    </td>
                    <td align="center">
                        06-30 15:51
                    </td>
                    <td align="center">
                        9<sup>+2</sup>
                    </td>
                    <td align="left">
                        预定
                    </td>
                    <td align="left">
                        <a href="" class="zhuangtai_btn"><span>团队状态</span></a>
                    </td>
                    <td align="center" nowrap="nowrap">
                        <a href="duli-dindan-ck.html">查看</a> <a href="#">取消</a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table id="Table1" cellspacing="0" cellpadding="4" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right">
            第 1 页/总 11 页 首页 前页 <a href="/RouteAgency/TourManger/Default.aspx?Page=2">后页</a>
            <a href="/RouteAgency/TourManger/Default.aspx?Page=11">末页</a>
            <br />
            <span class="RedFnt">1</span> <a href="/RouteAgency/TourManger/Default.aspx?Page=2">
                2</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=3">3</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=4">
                    4</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=5">5</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=6">
                        6</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=7">7</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=8">
                            8</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=9">9</a>
            <a href="/RouteAgency/TourManger/Default.aspx?Page=10">10</a> <a href="/RouteAgency/TourManger/Default.aspx?Page=11">
                &gt;&gt;</a>
        </td>
    </tr>
</table>
