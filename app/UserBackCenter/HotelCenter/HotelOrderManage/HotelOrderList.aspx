<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelOrderList.aspx.cs" Inherits="UserBackCenter.HotelCenter.HotelOrderManage.HotelOrderList" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>

<asp:Content id="HotelOrderList" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table id="tb_hotelOrderList" width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
        <td align="left" valign="top" >
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#eef7ff" style="border:1px solid #AACFE6;">
		<tr>
            <td style="font-size:14px;"  class="pand"height="26" colspan="2" align="left" bgcolor="#AACFE6"><strong><font color="#003C61">查询我的订单</font></strong></td>
            </tr>
          <tr>
            <td width="233" height="28" align="right">订单编号：</td>
            <td width="580" height="28"><input runat="server" id="hol_txtOrderId" type="text" name="hol_txtOrderId" /></td>
          </tr> 
          <tr>
            <td height="28" align="right">订单类别/类型：</td>
            <td height="28">
            <asp:DropDownList ID="hol_ddlOrderType" runat="server"></asp:DropDownList>
            </td>
          </tr>
         <tr>
            <td height="28" align="right">预订日期： </td>
            <td height="28"><input runat="server" type="text" name="hol_txtOrderStartTime" onfocus="WdatePicker()"  id="hol_txtOrderStartTime" />
              <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/time.gif" width="16" height="13" id="imgOrderStartTime" />- 
              <input runat="server" type="text" name="hol_txtOrderEndTime"  onfocus="WdatePicker()" id="hol_txtOrderEndTime" />
             <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/time.gif" width="16" height="13" id="imgOrderEndTime" /> <font color="#FF0000"> 默认查询2周内的订单</font></td>
          </tr>
		  <tr>
            <td height="28" align="right">入住日期： </td>
            <td height="28"><input runat="server" type="text" name="hol_txtCheckInStartTime" onfocus="WdatePicker()"  id="hol_txtCheckInStartTime" />
              <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/time.gif" width="16" height="13" id="imgCheckInStartTime" />- 
              <input runat="server" type="text" name="hol_txtCheckInEndTime" onfocus="WdatePicker()"  id="hol_txtCheckInEndTime" />              
              <img style="position:relative; left:-24px; top:3px; top:1px;" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/time.gif" width="16" height="13" id="imgCheckInEndTime" /></td>
          </tr>
		  
		  <tr>
            <td height="28" align="right">客人姓名：</td>
            <td height="28"><input runat="server" id="hol_txtCustomerName" name="hol_txtCustomerName" /></td>
          </tr>

          <tr>
            <td height="28" align="right">酒店名称：</td>
            <td height="28"><input runat="server" name="hol_txtHotelName" id="hol_txtHotelName" size="60" /></td>
          </tr>
		  
          <tr>
            <td height="28" align="right">订单状态查询：</td>
            <td height="28" id="tb_ckOrderStatus">      
                <input runat="server" id="ahol_ckAll" type="radio" value="" name="ahol_ckStatus" checked="true" /><label for='<%=ahol_ckAll.ClientID %>'>全部</label>
                <input runat="server" id="ahol_ckOK" type="radio" value="0"  name="ahol_ckStatus" /><label for='<%=ahol_ckOK.ClientID %>'>已确认</label>
                <input runat="server" id="ahol_ckCansel" type="radio" value="2"  name="ahol_ckStatus" /><label for='<%=ahol_ckCansel.ClientID %>'>取消</label>
                <input runat="server" id="ahol_ckHandle" type="radio" value="1"  name="ahol_ckStatus" /><label for='<%=ahol_ckHandle.ClientID %>'>处理中</label>
                <%--<input runat="server" id="ahol_ckNOWSHOW" type="radio" value="4"  name="ahol_ckStatus" />NOWSHOW--%>
            </td>
          </tr>
          <tr>
            <td height="48" align="right">&nbsp;</td>
            <td height="48">
            <a href="#">
            <img onclick="return HotelOrderList.OnSearch();" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/admin_orderform_ybans_03.jpg" width="79" height="25" border="0"  alt="查询"/></a>
            <a href="#" id="hvp_btnReset"><img  src="<%=ImageServerUrl %>/images/hotel/userBackCenter/admin_orderform_ybans_05.jpg" width="79" height="25"  alt="重置"/>
            </a>
            </td>
          </tr>
        </table> 
 
      <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#AACFE6">

		<cc1:CustomRepeater ID="hol_crp_HotelOrderList" runat="server">
		 <HeaderTemplate>
		<tr>
            <td height="26" colspan="9" align="left" bgcolor="#AACFE6"  class="pand" style="font-size:14px;"><strong><font color="#003C61">订单详情</font></strong></td>
            <td height="26" colspan="3" align="right" bgcolor="#AACFE6"  class="pand"><a id="hol_a_btnExcel" href="javascript:void(0);">
            <img style="padding-right:10px;" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/exceldc.gif" /></a>
            </td>
         </tr>
          <tr>
            <th width="52" height="30" align="center" bgcolor="#EEF7FF"><strong>订单号</strong></th>
            <th width="57" height="30" align="center" bgcolor="#EEF7FF"><strong>订单状态</strong></th>
            <th width="53" height="30" align="center" bgcolor="#EEF7FF"><strong>审核状态</strong></th>
            <th  height="30" align="center" bgcolor="#EEF7FF"><strong>旅客姓名</strong></th>
            <th width="142" height="30" align="center" bgcolor="#EEF7FF"><strong>酒店名称</strong></th>
            <th width="70" height="30" align="center" bgcolor="#EEF7FF"><strong>房型</strong></th>
            <th width="80" height="30" align="center" bgcolor="#EEF7FF"><strong>预定入住日期</strong></th>
            <th width="82" height="30" align="center" bgcolor="#EEF7FF"><strong>预定离店日期</strong></th>
            <th width="51" height="30" align="center" bgcolor="#EEF7FF"><strong>间夜数</strong></th>
            <th width="32" height="30" align="center" bgcolor="#EEF7FF"><strong>总价</strong></th>
            <th width="58" height="30" align="center" bgcolor="#EEF7FF"><strong>返佣比例</strong></th>
            <th width="53" height="30" align="center" bgcolor="#EEF7FF"><strong>返佣金额</strong></th>
          </tr>
          </HeaderTemplate>
          <ItemTemplate>
           <tr>
           
            <td height="28" align="center" bgcolor="#FFFFFF">
            <a id="hol_Link" target="_blank" href='<%#EyouSoft.Common.Domain.UserPublicCenter %>/HotelManage/HotelComplete.aspx?cityId=<%=cityId %>&resOrderId=<%#Eval("ResOrderId")%>'><%#Eval("ResOrderId")%></a></td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("OrderState")%></td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("CheckState")%></td>
            <td align="center" bgcolor="#FFFFFF">
            <%#GetHotelVisitors(((EyouSoft.Model.HotelStructure.OrderInfo)GetDataItem()).ResGuests)%>
            </td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("HotelName")%> </td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("RoomTypeName")%></td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("CheckInDate", "{0:yyyy-MM-dd}")%> </td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("CheckOutDate", "{0:yyyy-MM-dd}")%></td>
            <td align="center" bgcolor="#FFFFFF"><%#Eval("RoomNight")%></td>
            <td align="center" bgcolor="#FFFFFF"><%#Utils.GetMoney(Convert.ToDecimal(Eval("TotalAmount")))%> </td>
            <td align="center" bgcolor="#FFFFFF"><%#Utils.GetMoney(Convert.ToDecimal(Eval("CommissionPercent"))*100)%>%</td>
            <td align="center" bgcolor="#FFFFFF"><%#Utils.GetMoney(decimal.Round(Convert.ToDecimal(Eval("TotalAmount")) * Convert.ToDecimal(Eval("CommissionPercent"))))%></td>
          </tr>	 
          </ItemTemplate>  
		</cc1:CustomRepeater>	
    <tr id="hol_ExportPage" bgcolor="#eef7ff">
        <td height="28" colspan="12" class="F2Back" align="center">
        <cc2:ExportPageInfo ID="hol_ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4" runat="server"></cc2:ExportPageInfo>
      </td>	
      </tr>
</table>
   </tr>
</table>
      <script type="text/javascript"> 
      var HotelOrderList={
          //查询订单
          OnSearch:function(HrefUrl)
          {          
           if(/Page/.test(HrefUrl))
            {
               topTab.url(topTab.activeTabIndex,HrefUrl);
               return false;
            }
             HrefUrl="/HotelCenter/HotelOrderManage/HotelOrderList.aspx";
             var orderId=$("#<%=hol_txtOrderId.ClientID%>").val().trim();//订单编号
             var orderType=$("#<%=hol_ddlOrderType.ClientID%>").val();//订单类型
             var orderStartTime=$("#<%=hol_txtOrderStartTime.ClientID%>").val().trim();//开始预订日期
             var orderEndTime=$("#<%=hol_txtOrderEndTime.ClientID%>").val().trim();//预订结束日期
             var checkInStartTime=$("#<%=hol_txtCheckInStartTime.ClientID%>").val().trim();//开始入住日期
             var checkInEndTime=$("#<%=hol_txtCheckInEndTime.ClientID %>").val().trim();//结束入住日期
             var customerName=$("#<%=hol_txtCustomerName.ClientID %>").val().trim();//客人姓名
             var hotelName=$("#<%=hol_txtHotelName.ClientID%>").val().trim();//酒店名称
             var orderStatus="";
             //订单状态
             $("#tb_ckOrderStatus").find("input[type='radio'][name!='hol_ckStatus']:checked").each(function() {
                orderStatus=$(this).val();
             });
             var url= HrefUrl+"?orderId="+encodeURIComponent(orderId)+
             "&orderType="+encodeURIComponent(orderType)+
             "&orderStartTime="+encodeURIComponent(orderStartTime)+
             "&orderEndTime="+encodeURIComponent(orderEndTime)+
             "&checkInStartTime="+encodeURIComponent(checkInStartTime)+
             "&checkInEndTime="+encodeURIComponent(checkInEndTime)+
             "&customerName="+encodeURIComponent(customerName)+
             "&hotelName="+encodeURIComponent(hotelName)+
             "&orderStatus="+encodeURIComponent(orderStatus);
             topTab.url(topTab.activeTabIndex,url); 
             return false;
          },
           getParam: function() {//获取查询参数
             var orderId=$("#<%=hol_txtOrderId.ClientID%>").val().trim();//订单编号
             var orderType=$("#<%=hol_ddlOrderType.ClientID%>").val();//订单类型
             var orderStartTime=$("#<%=hol_txtOrderStartTime.ClientID%>").val().trim();//开始预订日期
             var orderEndTime=$("#<%=hol_txtOrderEndTime.ClientID%>").val().trim();//预订结束日期
             var checkInStartTime=$("#<%=hol_txtCheckInStartTime.ClientID%>").val().trim();//开始入住日期
             var checkInEndTime=$("#<%=hol_txtCheckInEndTime.ClientID %>").val().trim();//结束入住日期
             var customerName=$("#<%=hol_txtCustomerName.ClientID %>").val().trim();//客人姓名
             var hotelName=$("#<%=hol_txtHotelName.ClientID%>").val().trim();//酒店名称           var orderStatus="";
             //订单状态
             $("#tb_ckOrderStatus").find("input[type='radio'][name!='hol_ckStatus']:checked").each(function() {
                orderStatus=$(this).val();
             });
                 return $.param({ orderId: orderId, orderType: orderType, orderStartTime: orderStartTime, orderEndTime: orderEndTime,checkInStartTime:checkInStartTime,checkInEndTime:checkInEndTime,customerName:customerName,hotelName:hotelName,orderStatus:orderStatus})
             },
           ExportExcel:function(){//导入到Excel中
                var goToUrl = "/HotelCenter/HotelOrderManage/HotelOrderList.aspx?isExport=1&&urltype=tab&" + HotelOrderList.getParam();
                window.open(goToUrl,"Excel导出");
                return false;
            },
            OpenNewList:function()
            {
               var openUrl="";
               window.open(openUrl,"");
               return false;
            }  
            
      }
      $(document).ready(function() {
          $("#hol_ExportPage").find("a").click(//绑定分页控件
                function(event) {
                    HotelOrderList.OnSearch($(this).attr("href"));
                    return false;
                });
          //导出Excel中
          $("#hol_a_btnExcel").click(function() {
              HotelOrderList.ExportExcel();
              return false;
          });
          //表单重置
          $("#hvp_btnReset").click(function() {
              var myForm = $(this).closest("form").get(0);
              myForm.reset();
              return false;
          });
          //回车查询
          $("#tb_hotelOrderList input").bind("keypress", function(e) {
              if (e.keyCode == 13) {
                  HotelOrderList.OnSearch();
                  return false;
              }
          });
          $("#imgOrderStartTime").click(function() {
              $("#<%=hol_txtOrderStartTime.ClientID %>").focus();
          });
          $("#imgOrderEndTime").click(function() {
              $("#<%=hol_txtOrderEndTime.ClientID %>").focus();
          });
          $("#imgCheckInStartTime").click(function() {
              $("#<%=hol_txtCheckInStartTime.ClientID %>").focus();
          });
          $("#imgCheckInEndTime").click(function() {
              $("#<%=hol_txtCheckInEndTime.ClientID %>").focus();
          });
      });
        
    
      </script>
</asp:Content>

