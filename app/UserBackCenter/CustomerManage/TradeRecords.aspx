<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TradeRecords.aspx.cs" Inherits="UserBackCenter.CustomerManage.TradeRecords" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<style>
body { margin: auto 0; padding:0;}
td,div { font-size:12px; line-height:120%;}
</style>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%"  border="0" cellpadding="4" cellspacing="1" bgcolor="#E0E0E0">
  <tr bgcolor="#DBF7FD">
    <td height="20" colspan="5" align="center"><strong>交易次数：<%=recordCount %></strong></td>
  </tr>
 
  <asp:CustomRepeater id="tr_rpt_TradeList" runat="server">
  <HeaderTemplate>
  <tr class="baidi">
    <td width="7%" align="center" bgcolor="#FFFFFF"><strong>序号</strong></td>
    <td width="11%" align="center" bgcolor="#FFFFFF"><strong>预订时间</strong></td>
    <td width="10%" align="center" bgcolor="#FFFFFF"><strong>预订人</strong></td>
    <td width="11%" align="center" bgcolor="#FFFFFF"><strong>预订人数</strong></td>
    <td width="61%" align="center" bgcolor="#FFFFFF"><strong>团队信息</strong></td>
  </tr>
  </HeaderTemplate>
  <ItemTemplate>
  <tr class="baidi">
    <td align="center" bgcolor="#FFFFFF"><%=No++ %></td>
    <td align="center" bgcolor="#FFFFFF"><%# Convert.ToDateTime(Eval("IssueTime")).ToString("yyyy-MM-dd") %></td>
    <td align="center" bgcolor="#FFFFFF"><%# Eval("OperatorName") %></td>
    <td align="center" bgcolor="#FFFFFF"><%# Eval("PeopleNumber") %></td>
    <td align="left" bgcolor="#FFFFFF">【<%# Eval("TourNo") %>】<a style="text-decoration:none; cursor:default;" href="javascript:void(0)"><%# Eval("RouteName") %></a></td>
  </tr>
  </ItemTemplate>
  
</asp:CustomRepeater>

</table>
   <div id="tr_noData" style="text-align:center; display:none" runat="server">暂无交易信息!</div>
<table id="rs_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
<table width="100%" border="0" >
  <tr>
    <td align="center"></td>

    <td align="center"><input type="button" name="Submit" value="关闭" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()"/></td>
  </tr>
  </tbody>
</table>
    </div>
    </form>
</body>
</html>
