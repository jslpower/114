<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderListByStateANDType.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.OrderManage.OrderListByStateANDType" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="OrderListByStateANDType" contentplaceholderid="ContentPlaceHolder1" runat="server">
      <table width="835" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#7dabd8">
        <tr>
          <th width="104" height="30" align="center" bgcolor="#EEF7FF">订单号</th>
          <th width="93" align="center" bgcolor="#EEF7FF">类型</th>
          <th width="93" align="center" bgcolor="#EEF7FF">状态</th>
          <th width="93" align="center" bgcolor="#EEF7FF">PNR</th>
          <th width="85" align="center" bgcolor="#EEF7FF">人数</th>
          <th width="93" align="center" bgcolor="#EEF7FF">航空公司</th>
          <th width="93" align="center" bgcolor="#EEF7FF">始发地</th>
          <th width="93" align="center" bgcolor="#EEF7FF">目的地</th>
          <th width="90" align="center" bgcolor="#EEF7FF">操作</th>
        </tr>
        <asp:Repeater id="OrderListByStateANDType_rptOrderList" runat="server">
            <itemtemplate>
                 <tr>
                      <td height="30" align="center" bgcolor="#FFFFFF"><a href="/ticketscenter/ordermanage/orderdetailinfo.aspx" onclick="topTab.open($(this).attr('href'),'订单操作处理',{data:{orderid:'<%# DataBinder.Eval(Container.DataItem,"OrderId") %>',orderstate:'<%=tmpOrderState %>',changetype:'<%=tmpChangeType %>'}});return false;"><%# DataBinder.Eval(Container.DataItem,"OrderNo") %></a></td>
                      <td align="center" bgcolor="#FFFFFF"><%=RateType %></td>
                      <td align="center" bgcolor="#FFFFFF"><%=OrderStatType %></td>
                      <td align="center" bgcolor="#FFFFFF"><%# DataBinder.Eval(Container.DataItem,"OPNR") != DBNull.Value ? DataBinder.Eval(Container.DataItem,"OPNR") : DataBinder.Eval(Container.DataItem,"PNR") %></td>
                      <td align="center" bgcolor="#FFFFFF"><%# DataBinder.Eval(Container.DataItem, "PCount")%></td>
                      <td align="center" bgcolor="#FFFFFF"><%# GetFlightName(DataBinder.Eval(Container.DataItem, "FlightId").ToString())%></td>
                      <td align="center" bgcolor="#FFFFFF"><%# DataBinder.Eval(Container.DataItem, "HomeCityName")%></td>
                      <td align="center" bgcolor="#FFFFFF"><%# DataBinder.Eval(Container.DataItem, "DestCityName")%></td>
                      <td align="center" bgcolor="#FFFFFF"><a href="/ticketscenter/ordermanage/orderdetailinfo.aspx" onclick="topTab.open($(this).attr('href'),'订单操作处理',{data:{orderid:'<%# DataBinder.Eval(Container.DataItem,"OrderId") %>',orderstate:'<%=tmpOrderState %>',changetype:'<%=tmpChangeType %>'}});return false;"><%=strOrderStaType %></a></td>
                </tr>
            </itemtemplate>
        </asp:Repeater>  
        <asp:Panel id="OrderListByStateANDType_pnlNodata" runat="server" visible="false">
            <tr style="height:50px;">
                <td colspan="9">暂无数据</td>
            </tr>
        </asp:Panel>
        <tr>
            <td><input type="button" value="返回" id="OrderListByStateANDType_Back" /></td>
            <td height="30" align="right" colspan="11" id="OrderListByStateANDType_td"><cc1:ExportPageInfo ID="ExportPageInfo1" LinkType="4" runat="server"></cc1:ExportPageInfo></td>
          </tr>         
      </table>
      <script type="text/javascript">
      $(function(){
        $("#OrderListByStateANDType_Back").click(function(){
            topTab.url(topTab.activeTabIndex,"/ticketscenter/ordermanage/orderlist.aspx");
        });
        $("#OrderListByStateANDType_td").find("a").click(function(){
            topTab.url(topTab.activeTabIndex,$(this).attr("href"));
            return false;
        });
      });
      </script>
</asp:content>
