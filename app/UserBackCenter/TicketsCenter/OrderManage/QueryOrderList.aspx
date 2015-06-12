<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryOrderList.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.OrderManage.QueryOrderList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="QueryOrderList" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="sidebar02_con_table02" style="width:90%">
  		<table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="1%">&nbsp;</td>
            <th align="left">共找到<font color="#FF0000">[<asp:Label id="QueryOrderList_lblOrderCount" runat="server"></asp:Label>]</font>笔订单 </th>
          </tr>
    </table>

  		<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
          <tr bgcolor="#EDF8FC">
            <th width="9%" height="30" align="center">订单编号 </th>
            <th width="7%" align="center" bgcolor="#EDF8FC"> 旅客人数</th>
            <th width="7%" align="center" bgcolor="#EDF8FC">记录编号</th>
            <th width="8%" align="center">始发地</th>
            <th width="8%" align="center">目的地</th>
            <th width="7%" align="center">航班号</th>
            <th width="8%" align="center">出发日期</th>
            <th width="10%" align="center">预订时间</th>
            <%--<th width="7%" align="center">出票效率</th>--%>
            <th width="8%" align="center">订单状态</th>
            <%--<th width="8%" align="center">供应商用户名</th>
            <th width="8%" align="center">供应商公司名</th>--%>
          </tr>
          <asp:Repeater id="QueryOrderList_rptOrderList" runat="server">
              <itemtemplate>
                  <tr>
                    <td height="30" align="center"><a href="javascript:void(0);" onclick='topTab.open("/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%# DataBinder.Eval(Container.DataItem,"OrderId") %>","查看订单");return false;'><%# DataBinder.Eval(Container.DataItem,"OrderNo") %></a></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "PCount")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem,"OrderNo") %></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "HomeCityName")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "DestCityName")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "LFlightCode")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "LeaveTime","{0:yyyy-MM-dd}")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "OrderTime","{0:MM-dd hh:mm}")%></td>
                   <%-- <td align="center"><%# (GetSuccessRate(DataBinder.Eval(Container.DataItem, "SupplierCId").ToString())*100).ToString("F1")%>%</td>--%>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "OrderState")%></td>
                    <%--<td align="center"><%# DataBinder.Eval(Container.DataItem, "SupplierUName")%></td>
                    <td align="center"><%# DataBinder.Eval(Container.DataItem, "SupplierCName")%></td>--%>
                  </tr>
              </itemtemplate>
          </asp:Repeater>
          <asp:Panel id="QueryOrderList_pnlNoData" runat="server" visible="false">
              <tr style="height:50px;">
                 <td colspan="12">暂无数据</td>
              </tr>
          </asp:Panel>
          <tr>
            <td><input type="button" value="返回" id="QueryOrderList_Back" /></td>
            <td id="QueryOrderList_tb" height="30" align="right" colspan="11"><cc1:ExportPageInfo ID="ExportPageInfo1" LinkType="4" runat="server"></cc1:ExportPageInfo></td>
          </tr>
    </table>     
  </div>
  <script type="text/javascript">
  $(function(){
    $("#QueryOrderList_tb").find("a").click(function(){
        topTab.url(topTab.activeTabIndex,$(this).attr("href"));
        return false;
    });
    $("#QueryOrderList_Back").click(function(){
        topTab.url(topTab.activeTabIndex,"/TicketsCenter/OrderManage/QueryOrder.aspx");
    });
  });
  </script>
</asp:content>
