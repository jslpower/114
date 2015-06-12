<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportTotalToExcel.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.ReportTotalToExcel" %>
<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>
<cc1:CustomRepeater ID="crpReportTotalList" runat="server">
<HeaderTemplate>
<table width="100%" id="tbcrpReportList" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
      <tr bgcolor="#EDF8FC">          
            <th width="7%" align="center" bgcolor="#EDF8FC">旅客人数</th>
            <th width="7%" align="center">票面价</th>
            <th width="7%" align="center">参考代理费</th>
            <th width="9%" align="center">机建/燃油</th>           
            <th width="7%" align="center">支付金额</th>
            <th width="7%" align="center">退款金额</th>  
      </tr>
</HeaderTemplate>
<ItemTemplate>
      <tr>           
           <td align="center"><%#Eval("Pcount")%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalFacePrice")))%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalAgencyPrice")))%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalBuidPrice")))%>/￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalFuelPrice")))%></td>            
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("BalanceAmount")))%></td>
            <td align="center">￥<%#Utils.GetMoney(Convert.ToDecimal(Eval("RefundTotalAmount")))%></td> 
   </tr>
</ItemTemplate>
<FooterTemplate>    
  </table> 
</table>
</FooterTemplate>
</cc1:CustomRepeater> 