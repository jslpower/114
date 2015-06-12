<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="UserPublicCenter.AirTickets.tenpay.directpay.Show" %>
<%@ Register Src="~/AirTickets/TeamBook/TicketTopMenu.ascx" TagName="TopMenu" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
<style type="text/css">
.font_red{ color:#CC0000;}
</style>
<myASP:TopMenu id="ts_ucTopMenu" runat="server" TabIndex="tab5"></myASP:TopMenu>
 <div class="sidebar02_con">
      <div class="sidebar02_con_table03">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F7F7F7" style="border:1px #61B6DB solid;"><tr><td width="20%" height="45" align="center"><table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#F7F7F7" style="border:1px #61B6DB solid;">
          <tr>
            <th width="15%" height="45" align="right">订单号：</th>
            <td width="10%" align="left">&nbsp;</td>
            <td align="left"><span class="font_red"><%=order_no %></span></td>
          </tr>
          <tr>
            <th height="45" align="right">付款总金额：</th>
            <td align="left">&nbsp;</td>
            <td align="left"><span class="font_red">￥<%=total_fee %></span></td>
          </tr>
          <tr>
            <th height="45" align="right">商品标题：</th>
            <td align="left">&nbsp;</td>
            <td align="left"><span class="font_red"><%= proName %></span></td>
          </tr>
          <tr>
            <th height="45" align="right">商品描述：</th>
            <td align="left">&nbsp;</td>
            <td align="left"><span class="font_red"><%= proDetail %></span></td>
          </tr>
         
          <tr>
            <th height="45" align="right">交易结果：</th>
            <th height="45" align="right">&nbsp;</th>
            <td align="left"><span class="font_red"><%=resultMess%></span></td>
          </tr>
          <tr>
            <td height="35" align="right">&nbsp;</td>
            <th align="right">&nbsp;</th>
            <td align="left" valign="top"><a href="/AirTickets/OrderManage/OrderDetails.aspx?OrderId=<%=orderid %>"><img src="<%=ImageServerUrl%>/images/jipiao/check_btn.gif" width="70" height="25" /></a></td>
          </tr>
        </table></td>
            </tr>
      </table>
      </div>
    </div>

</asp:Content>
