<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCurrentOrderList.aspx.cs"
    Inherits="UserPublicCenter.AirTickets.OrderManage.AjaxCurrentOrderList" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>

<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
            <td width="1%">
                &nbsp;
            </td>
            <th align="left">
                共找到<font color="#ff0000">[<asp:literal id="ltr_OrderCount" runat="server"></asp:literal>]</font>笔订单
            </th>
        </tr>
    </tbody>
</table>
<table width="100%" border="1" bordercolor="#dcd8d8" cellpadding="0" cellspacing="0">
    <tbody>
        <tr bgcolor="#edf8fc">
            <th width="9%" align="center" height="30">
                订单编号
            </th>
            <th width="7%" align="center" bgcolor="#edf8fc">
                旅客人数
            </th>
           <%-- <th width="7%" align="center" bgcolor="#edf8fc">
                记录编号
            </th>--%>
            <th width="8%" align="center">
                始发地
            </th>
            <th width="8%" align="center">
                目的地
            </th>
            <th width="7%" align="center">
                航班号
            </th>
            <th width="8%" align="center">
                出发日期
            </th>
            <th width="8%" align="center">
                预订时间
            </th>
            <th width="7%" align="center">
                出票效率
            </th>
            <th width="8%" align="center">
                订单状态
            </th>
            <th width="8%" align="center">
                供应商公司名
            </th>
            <th width="4.5%" align="center">
                操作
            </th>
        </tr>
        <cc1:CustomRepeater ID="crptOrderList" runat="server">
            <ItemTemplate>
                <tr>
                    <td align="center" height="30">
                        <a href="OrderDetails.aspx?OrderId=<%#Eval("OrderId")%>"><%#Eval("OrderNo")%></a>
                    </td>
                    <td align="center">
                        <%#Eval("PCount")%>
                    </td>
                  <%--  <td align="center">
                        <%#Eval("OrderId")%>
                    </td>--%>
                    <td align="center">
                        <%#Eval("HomeCityName")%>
                    </td>
                    <td align="center">
                       <%#Eval("DestCityName")%>
                    </td>
                    <td align="center">
                        <%#Eval("RFlightCode")%>
                    </td>
                    <td align="center">
                        <%#Eval("LeaveTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <%#Eval("OrderTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td align="center">
                        <span id="OrderRepeater_ctl00_Sdate"><%# (Convert.ToDecimal(Eval("SuccessRate"))*100).ToString("F1")%>%</span>
                    </td>
                    <td align="center">
                        <span id="OrderRepeater_ctl00_OrderTime">  <%# GetOrderState(Eval("OrderState").ToString(),Eval("OrderId").ToString())%></span>
                    </td>
                    <td align="center">
                        <%#Eval("SupplierCName")%>
                    </td>
                    <td align="center">
                        <%# OpearCancelOrder(Eval("OrderState").ToString(),Eval("OrderId").ToString())%>
                    </td>                
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </tbody>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <div class="digg" align="center">
                <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" PageStyleType="NewButton" runat="server" />
            </div>
        </td>
    </tr>
</table>
