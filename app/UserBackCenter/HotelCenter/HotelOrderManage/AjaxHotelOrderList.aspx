<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxHotelOrderList.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.AjaxHotelOrderList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>订单查询页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <table width="815" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" style="border:1px solid #AACFE6;">
		<tr>
            <td width="233" align="right">&nbsp;</td>
            <td width="580">&nbsp;</td>
          </tr>
          <tr>
            <td width="233" height="28" align="right">订单编号：</td>
            <td height="28"><input id="ahol_txtOrderId" name="ahol_txtOrderId" /></td>
          </tr>

          <tr>
            <td height="28" align="right">订单类别/类型：</td>
            <td height="28">            
                <asp:DropDownList ID="ahol_ddlOrderType" runat="server">
                </asp:DropDownList>            
            </td>
          </tr>
          <tr>
            <td height="28" align="right">预订日期： </td>
            <td height="28"><input type="text" name="ahol_txtOrderStartTime" id="ahol_txtOrderStartTime" />
              <img style="position:relative; left:-24px; top:3px; *top:1px;" src="../images/time.gif" width="16" height="13" /> - 
              <input type="text" name="ahol_txtOrderEndTime" id="ahol_txtOrderEndTime" />
              <font color="#FF0000"><img style="position:relative; left:-24px; top:3px; *top:1px;" src="../images/time.gif" width="16" height="13" /> 默认查询2周内的订单</font></td>
          </tr>
		  <tr>
            <td height="28" align="right">入住日期： </td>
            <td height="28"><input type="text" name="ahol_txtCheckInStartTime" id="ahol_txtCheckInStartTime" />
              <img style="position:relative; left:-24px; top:3px; *top:1px;" src="../images/time.gif" width="16" height="13" /> - 
              <input type="text" name="ahol_txtCheckInEndTime" id="ahol_txtCheckInEndTime" />              
              <img style="position:relative; left:-24px; top:3px; *top:1px;" src="../images/time.gif" width="16" height="13" /><font color="#FF0000"> 默认查询2周内的订单</font></td>
          </tr>
		  
		  <tr>
            <td height="28" align="right">客人姓名：</td>
            <td height="28"><input id="ahol_txtCustomerName" name="ahol_txtCustomerName" /></td>
          </tr>

          <tr>
            <td height="28" align="right">酒店名称：</td>
            <td height="28"><input name="ahol_txtHotelName" id="ahol_txtHotelName" size="60" /></td>
          </tr>
		  
          <tr>
            <td height="28" align="right">订单状态查询：</td>
            <td height="28">
                <input id="ahol_ckAll" type="checkbox" value="ST" name="ahol_ckAll" />全部
                <input id="ahol_ckOK" type="checkbox"  name="ahol_ckOK" />已确认
                <input id="ahol_ckCansel" type="checkbox"  name="ahol_ckCansel" />取消
                <input id="ahol_ckHandle" type="checkbox"  name="ahol_ckHandle" />处理中
                <input id="ahol_ckNOWSHOW" type="checkbox"  name="ahol_ckNOWSHOW" />NOWSHOW
            </td>
          </tr>
          <tr>
            <td height="48" align="right">&nbsp;</td>
            <td height="48"><a href="query-results.html"><img src="../images/admin_orderform_ybans_03.jpg" width="79" height="25" border="0" /></a> <img src="../images/admin_orderform_ybans_05.jpg" width="79" height="25" /></td>
          </tr>
        </table>
  </div>
  <div id="HotelOrderList">
  
  </div>
    </form>
</body>
</html>
