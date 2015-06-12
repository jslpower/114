<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxHistoryOrder.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.AjaxHistoryOrder" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="1%">&nbsp;</td>
                <th align="left">共找到<font color="#FF0000">[<%=BarCount.ToString()%>]</font>笔订单 </th>
              </tr>
        </table>

      		<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
              <tr bgcolor="#EDF8FC">
                <th width="9%" height="30" align="center">乘客</th>
                <th width="12%" align="center" bgcolor="#EDF8FC"> 票号</th>
                <th width="8%" align="center" bgcolor="#EDF8FC">出票时间</th>
                <th width="9%" align="center">乘机时间</th>
                <th width="8%" align="center">航程</th>
                <th width="7%" align="center">PNR</th>
                <th width="10%" align="center">票价</th>
                <th width="8%" align="center">燃油费</th>
                <th width="8%" align="center">机建费</th>
                <th width="10%" align="center">订单状态</th>
                <th width="11%" align="center">返点</th>
              </tr>
              <cc1:CustomRepeater ID="crptList" runat="server">
                  <ItemTemplate>
                  <tr>
                    <td height="30" align="center"><%#Eval("Passenger") %></td>
                    <td align="center"><%#Eval("number")%></td>
                    <td align="center"><%# Eval("ticketTime","{0:yyyy-MM-dd}")%></td>
                    <td align="center"><%# Eval("flightTime", "{0:yyyy-MM-dd}")%></td>
                    <td align="center"><span id="OrderRepeater_ctl00_Scity"><%#Eval("Voyage")%></span></td>
                    <td align="center"><span id="OrderRepeater_ctl00_FlightNo"><%#Eval("PNR")%></span></td>
                    <td align="center"><%#Eval("ticketPrice")%></td>
                    <td align="center"><%#Eval("ranyouFee")%></td>
                    <td align="center"><%#Eval("jijianFee")%></td>
                    <td align="center"><%#Eval("state")%></td>
                    <td align="center"><%#Eval("rebate")%></td>
                  </tr>
                  </ItemTemplate>
              </cc1:CustomRepeater>
             
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

