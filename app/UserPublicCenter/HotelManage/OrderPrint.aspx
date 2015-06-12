<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPrint.aspx.cs" Inherits="UserPublicCenter.HotelManage.OrderPrint" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("body") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <!--main start-->
	<div class="main" style="width:680px;">
        <!--content start-->
        <div class="content">
            <div class="sidebar02 sidebar02Search">
            	<div class="sidebar02_1">
                    <p class="xuanzhe"><span>旅客预订行程通知单</span></p>
                   <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        	
                      <div class="yd_jiange C_red" style="height:2px;">
                    </div>  
					
     <div class="yuding">
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td colspan="4" align="center" valign="middle" background="<%=ImageServerUrl%>/Images/hotel/search_06.gif">&nbsp;</td>
            </tr>
          <tr>
            <td colspan="4" align="left" valign="middle"  class="pandl">尊敬的 <%=userName %>
     ：<br />
     感谢您使用同业114酒店预订系统，如果您入住时出现任何的问题请在酒店前台拨打电话：0571-56884627 以下是您的预订内容：</td>
            </tr>
          <tr>
            <th width="16%" align="center" valign="middle" bgcolor="#FDF5EA"><strong>订单号：
              
            </strong></th>
            <td width="30%" align="left" class="pandl orange"><%=resOrderId %></td>
            <td width="17%" align="left" bgcolor="#FDF5EA" class="pandl"><strong>预订日期</strong></td>
            <td width="37%" align="left" class="pandl orange"><%=orderModel.CreateDateTime.ToString("yyyy-MM-dd") %></td>
          </tr>
        </table>
	 </div>
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
	            <tr>
                  <td colspan="4" align="left" background="<%=ImageServerUrl%>/Images/hotel/search_06.gif"  bgcolor="#FDF5EA" class="pandl pand2"><h1>行程信息</h1></td>
                </tr>
               <tr>
                  <td width="20%" align="center" valign="middle" bgcolor="#FDF5EA">酒店名称：</td>
                  <td width="25%" align="left">&nbsp;<%=orderModel.HotelName %></td>
                  <td width="20%" align="center" bgcolor="#FDF5EA">房型：</td>
                  <td width="33%" align="left">&nbsp;<%=orderModel.RoomTypeName %></td>
                </tr>
                <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">住店日期：</td>
                  <td align="left">&nbsp;<%=orderModel.CheckInDate.ToString("yyyy-MM-dd") %>/<%=orderModel.CheckOutDate.ToString("yyyy-MM-dd") %></td>
                  <td align="center" bgcolor="#FDF5EA">支付方式：</td>
                  <td align="left">&nbsp;<%=orderModel.PaymentType == EyouSoft.HotelBI.HBEPaymentType.T ? "前台现付" : ""%></td>
                </tr>
                 <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">结算价：</td>
                  <td align="left" class="C_Grb">&nbsp;<strong>￥<%=EyouSoft.Common.Utils.GetMoney(decimal.Round(orderModel.TotalAmount - orderModel.TotalAmount * orderModel.CommissionPercent))%></strong></td>
                  <td align="center" bgcolor="#FDF5EA">反佣比例：</td>
                  <td align="left" class="frb">&nbsp;<%=Utils.GetMoney(orderModel.CommissionPercent*100)%>%</td>
                </tr>
                <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">其他要求：</td>
                  <td colspan="3" align="left">&nbsp;<%=orderModel.SpecialRequest %> </td>
                </tr>
              </table>
	 </div>
	 
	 <div class="biaoge2">
	   <table width="100%" border="0" cellspacing="0" cellpadding="0">
	 <tr><td colspan="11" align="left" background="<%=ImageServerUrl%>/Images/hotel/search_06.gif"  bgcolor="#FDF5EA" class="pandl pand2"><h1>价格清单</h1></td>
     </tr>
	 </table>
        <%=hotelRateList%>    
	 </div>
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
	            <tr>
                  <td colspan="11" align="left" background="<%=ImageServerUrl%>/Images/hotel/search_06.gif"  bgcolor="#FDF5EA" class="pandl pand2"><h1>入住人信息</h1></td>
                </tr>
                <%if (orderModel != null && orderModel.ResGuests != null)
                  {
                      foreach (EyouSoft.HotelBI.HBEResGuestInfo guestInfo in orderModel.ResGuests)
                      { %>
                <tr>
    <th width="13%" align="center" class="pandl">姓名：</th>
    <td width="20%" align="center" class="pandl"><%=guestInfo.PersonName%></td>
    <th width="13%" align="center" class="pandl">手机：</th>
    <td width="20%" align="center" class="pandl"><%=guestInfo.Mobile%></td>
    <th width="13%" align="center" class="pandl">座机：</th>
    <td width="20%" align="center" class="pandl"><%=guestInfo.Telephone%></td>
  </tr>
  <%}
                  }%>
              </table>
	 </div>
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
	            <tr>
                  <td colspan="6" align="left" background="<%=ImageServerUrl%>/Images/hotel/search_06.gif"  bgcolor="#FDF5EA" class="pandl pand2"><h1>酒店预订须知</h1></td>
                </tr>
                <tr>
    <td align="left" class="pandl">
      <%=orderModel.Comments%></td>
    </tr>
              </table>
	 </div>
	 
	 </div>

	 <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#fceedb" class="OrderState">
<tr>
                <td width="26%" align="center">&nbsp;</td>
              <td width="12%" align="center"><a href="javascript:;" onclick="return OrderPrint();">打印</a></td>
              <td width="26%" align="left">&nbsp;</td>
</tr>
            </table>
	                </div>
                    <!--sidebar02SearchC end-->
              </div>
                    </div>
            </div>
          <!--sidebar02 end-->
        </div>
        <div class="clear"></div>
        <!--content end-->
       </div>
    
    <!--main end-->
    <script type="text/javascript">
        //打印订单
        function OrderPrint() {
            if (window.print) {
                window / print();
            }
            return false;
        }
    </script>
</body>
</html>
