<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportToExcel.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.ReportToExcel" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Model.TicketStructure" %>

<%@ Import Namespace="EyouSoft.Common" %>
<cc1:CustomRepeater ID="crpReportList" runat="server">
<HeaderTemplate>
<table width="100%" id="tbcrpReportList" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
      <tr bgcolor="#EDF8FC">
            <th width="9%" height="30" align="center" bgcolor="#EDF8FC">订单编号 </th>
            <th width="7%" align="center" bgcolor="#EDF8FC">PNR</th> 
            <th width="7%" align="center" bgcolor="#EDF8FC">旅客人数</th>
            <th width="9%" align="center">去程面价/回程面价</th>
            <th width="9%" align="center">去程参考扣率/回程参考扣率</th>
            <th width="9%" align="center">去程机建/去程燃油</th>    
            <th width="9%" align="center">回程机建/回程燃油</th>        
            <th width="7%" align="center">支付金额</th>
            <th width="7%" align="center">退款金额</th>                
            <th width="7%" align="center">支付方式</th>
            <th width="7%" align="center">支付时间</th>
            <th width="7%" align="center">操作员</th>
      </tr>
</HeaderTemplate>
<ItemTemplate>
      <tr>
             <td height="30" align="center"><%#Eval("OrderNo")%>&nbsp;</td>   
             <td align="center"><%#GetPNR(Convert.ToString(Eval("OPNR")),Convert.ToString(Eval("PNR")))%></td>        
            <td align="center"><%#Eval("PCount")%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.LeaveFacePrice)) %>/￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.ReturnFacePrice)) %></td>
            <td align="center">
            <%#GetPercent(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.LeaveDiscount))%>
            /<%#GetPercent(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.ReturnDiscount))%>
            </td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.LBuildPrice)) %>/￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.LFuelPrice)) %></td>  
             <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.RBuildPrice)) %>/￥<%#Utils.GetMoney(Convert.ToDecimal(((OrderInfo)GetDataItem()).OrderRateInfo.RFuelPrice)) %></td>            
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("RefundTotalAmount")))%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalAmount")))%></td>
            <td align="center"><%#Eval("PayType")%></td>
            <td align="center"><%#Eval("PayTime", "{0:yyyy-MM-dd}")%></td>
            <td align="center"><%#Eval("LastOperatorName")%></td>
   </tr>
</ItemTemplate>
<FooterTemplate>   
  </table> 
</table>
</FooterTemplate>
</cc1:CustomRepeater>
