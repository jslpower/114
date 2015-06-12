<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" AutoEventWireup="true" CodeBehind="HotelComplete.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelComplete" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<%@ Register Src="~/WebControl/HotelControl/HotelSearchControl.ascx" TagName="HotelSearch" TagPrefix="uc2" %>
<%@ Register Src="~/WebControl/HotelControl/CommonUserControl.ascx" TagName="CommonUser" TagPrefix="uc5" %>
<%@ Register Src="~/WebControl/HotelControl/ImgFristControl.ascx" TagName="ImgFrist" TagPrefix="uc6" %>
<%@ Register Src="~/WebControl/HotelControl/ImgSecondControl.ascx" TagName="ImgSecond" TagPrefix="uc7" %>
<%@ Register Src="~/WebControl/HotelControl/ImgBannerControl.ascx" TagName="ImgBanner" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
<link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
<div class="main">
 <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    	   <uc8:ImgBanner ID="ImgBanner1" runat="server" />
    	
        <!--content start-->
        <div class="content">
       		<!--sidebar start-->
        	<div class="sidebar sidebarSearch">
              <!--sidebar_1-->
           	    <uc2:HotelSearch ID="HotelSearch1" runat="server" />
              <!--sidebar_1 end-->
              <uc6:ImgFrist ID="ImgFrist1" runat="server" ImageWidth="224px" />
             <!-- sidebar_2 start-->
              <uc5:CommonUser ID="CommonUser1" runat="server" />
              <uc7:ImgSecond ID="ImgSecond1" runat="server" ImageWidth="224px" />
            </div>
           <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
            	<div class="sidebar02_1">
                    <p class="xuanzhe"><span>预订提交完成</span><img src="<%=ImageServerUrl%>/Images/hotel/liucheng05.gif" /></p>
              <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                      <div class="yd_jiange yd_jiange02"><span class="yd_jiange_T"><img src="<%=ImageServerUrl%>/Images/hotel/gou.gif" class="ToHook">订单已提交！ 订单编号: <font class="frb"><%=resOrderId%></font>  预订状态: <font class="frb" id="hc_orderState"><%=orderState%></font></span> <span class="C_red  note">特别提示:订单订妥后如遇紧急情况（房间、房价发生变化）客服人员会即时和您电话联系 </span>
                    </div>  
					
     <div class="yuding"><h1>入住信息</h1>
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="20%" align="center" valign="middle" bgcolor="#FDF5EA">房间数：</td>
            <td align="left">&nbsp;<font class="frb"><%=orderModel.Quantity%></font>（间）</td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人姓名：<br /></td>
            <td align="left"><div  style='word-wrap:break-word;width:527px;overflow:hidden; padding-left:3px;'><%= guestNames %></div> </td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人类型：<br /></td>
            <td align="left">&nbsp;<%=guestTypes %></td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人手机：<br /></td>
            <td align="left">&nbsp;<%=guestMible %><font class="C_red" style="margin-left:40px;"><%=orderModel.IsMobileContact?"手机通知":"" %></font></td>
            </tr>
        </table>
	 </div>
	 </div>
	 
	 <div class="yuding">
		<h1>预订酒店信息</h1>
            <div class="biaoge2">
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                  <td align="left" class="C_Grb">&nbsp;<strong>￥<%=EyouSoft.Common.Utils.GetMoney(decimal.Round((orderModel.TotalAmount - orderModel.TotalAmount * orderModel.CommissionPercent)))%></strong></td>
                  <td align="center" bgcolor="#FDF5EA">返佣比例：</td>
                  <td align="left" class="frb">&nbsp;<%=Utils.GetMoney(orderModel.CommissionPercent*100) %>%</td>
                </tr>
                <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">其他要求：</td>
                  <td colspan="3" align="left">&nbsp;<%=orderModel.SpecialRequest %> </td>
                </tr>
              </table>
            </div>
	 		</div>
	 	<div class="yuding">
        <h1>联系人信息</h1>
        <div class="biaoge2">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <th width="13%" align="center" class="pandl">姓名：</th>
            <td width="20%" align="center" class="pandl"><%=orderModel.ContacterFullname %></td>
            <th width="13%" align="center" class="pandl">手机：</th>
            <td width="20%" align="center" class="pandl"><%=orderModel.ContacterMobile %></td>
            <th width="13%" align="center" class="pandl">座机：</th>
            <td width="20%" align="center" class="pandl"><%=orderModel.ContacterTelephone %></td>
          </tr>
        </table>
        </div>
	   </div>
           <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#fceedb" class="OrderState">
<tr>
                <td width="26%" align="center">&nbsp;</td>
              
              <td width="12%" align="center"><a href="/HotelManage/AdvanceSearch.aspx">新订单</a></td>
              <%if (orderState != "取消")
                { %>
              <td width="12%" align="center"><a href="javascript:;" onclick="return HotelComplete.cancel();">取消订单</a></td>
              <%}%>
               
              <td width="12%" align="center"><a href="/HotelManage/OrderPrint.aspx?resOrderId=<%=resOrderId %>">打印行程</a></td>
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
        <script type="text/javascript">
            var HotelComplete = {
                //保存订单
                cancel: function() {
                    $.ajax(
	             {
	                 url: "/HotelManage/HotelComplete.aspx",
	                 data: { resOrderId: "<%=resOrderId %>", method: "cancel" },
	                 dataType: "json",
	                 cache: false,
	                 type: "get",
	                 success: function(result) {
	                 if (result.success == "1") {
	                          alert("订单已取消");
	                         $("#hc_orderState").html("订单已取消");
	                     }
	                     else {
	                         alert(result.message);
	                         $("#hc_orderState").html("取消失败");
	                     }
	                 },
	                 error: function() {
	                     alert("操作失败!");
	                 }
	             });
                    return false;
                }
            }
        </script>
</asp:Content>
