<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxReportTotal.aspx.cs" Inherits="UserPublicCenter.AirTickets.OrderManage.AjaxReportTotal" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Model" %>
<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>
<div id="divReportTotal">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="1%">&nbsp;</td>
                <th align="left">共有<font color="#FF0000">[<%= RecordCount %>]</font>条记录</th>
              </tr>
        </table>
<cc1:CustomRepeater ID="crpReportTotalList" runat="server">
<HeaderTemplate>
<table width="100%" id="tbcrpReportList" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
    <table width="100%" id="Table1" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
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
  <tr>
  <td colspan="13" height="30" align="center">   
       <a  href="ReportTotalToExcel.aspx?StartTime=<%=StartTime%>&EndTime=<%=EndTime %>&Stats=<%=Stats%>&Type=<%=Type%>" 
       target="_blank">导出Excel</a>      
  </td>
  </tr>
  </table> 
</table>
</FooterTemplate>
</cc1:CustomRepeater> 
</div>
