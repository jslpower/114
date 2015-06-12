<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxFITOrders.aspx.cs"
    Inherits="UserBackCenter.TeamService.AjaxFITOrders" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<table width="100%" border="1" cellpadding="1" cellspacing="0" bordercolor="#9dc4dc"
    class="liststylesp">
    <tr class="list_basicbg">
        <th class="list_basicbg">
            团号
        </th>
        <th class="list_basicbg" style="width: 400px">
            线路名称
        </th>
        <th class="list_basicbg">
            出发时间
        </th>
        <th class="list_basicbg">
            游客
        </th>
        <th class="list_basicbg">
            预定时间
        </th>
        <th class="list_basicbg">
            人数
        </th>
        <th class="list_basicbg">
            订单状态
        </th>
        <th class="list_basicbg">
            支付状态
        </th>
        <%--<th class="list_basicbg">
                        状态修改
                    </th>--%>
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
                            <td height="30" align="center">
                                <%#Eval("TourNo")%>
                            </td>
                            <td align="left">
                                <a href='/PrintPage/TeamTourInfo.aspx?TeamId=<%#Eval("TourId") %>' target="_blank"><%#Eval("RouteName")%></a><br />
                               专线商：<a target="_blank" href="<%#Eval("CompanyType")!=null?EyouSoft.Common.Utils.GetCompanyDomain(Eval("Publishers").ToString(),(EyouSoft.Model.CompanyStructure.CompanyType)Eval("CompanyType")):"javascript:void(0);" %>"><%#Eval("PublishersName")%></a>
                            </td>
                            <td align="center">
                                <%#((DateTime)Eval("LeaveDate")).ToString("MM-dd")%>
                            </td>
                            <td align="left">
                              <%#Eval("VisitorContact")%><br />
                              <%#Eval("VisitorTel")%>
                            </td>
                            <td align="center">
                               <%#Eval("IssueTime")%>
                            </td>
                            <td align="center">
                                <%#Eval("AdultNum")%><sup>+<%#Eval("ChildrenNum")%></sup>
                            </td>
                            <td align="center">
                                <%#Eval("OrderStatus")%>
                            </td>
                            <td align="left">
                                <%#Eval("PaymentStatus")%>
                            </td>
                            <%--<td align="left">
                                <a href="#" class="basic_btn"><span>取消</span></a>
                            </td>--%>
                            <td align="center">
                                <a href='/PrintPage/TeamConfirm.aspx?OrderId=<%#Eval("OrderId") %>' target="_blank">团队确认单</a>
                            </td>
                            <td align="center" nowrap="nowrap">
                               <a href="javascript:void(0);" onclick="javascript:topTab.open('/order/touragency/orderupdate.aspx?orderID=<%#Eval("OrderID") %>','订单查看')">查看</a><br />
                                <a href="/RouteAgency/OrderStateLog.aspx?orderID=<%#Eval("OrderId") %>" target="_blank">日志</a>
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
