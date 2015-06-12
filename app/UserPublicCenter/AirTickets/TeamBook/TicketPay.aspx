<%@ Page Language="C#" MasterPageFile="~/MasterPage/AirTicket.Master" AutoEventWireup="true" CodeBehind="TicketPay.aspx.cs" Inherits="UserPublicCenter.AirTickets.TeamBook.TicketPay" %>
<%@ Register Src="~/AirTickets/TeamBook/TicketTopMenu.ascx" TagName="TopMenu" TagPrefix="myASP" %>
<%@ MasterType VirtualPath="~/MasterPage/AirTicket.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="myASP" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="c1" runat="server">
<myASP:TopMenu id="ts_ucTopMenu" runat="server" TabIndex="tab4"></myASP:TopMenu>
<div class="sidebar02_con">
   
	  <div class="sidebar02_con_table02">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr align="left" bgcolor="#E0F4FD">
            <td height="30" bgcolor="#E0F4FD"><span class="title">航班信息</span></td>
          </tr>
          <tr>
            <td height="35"><table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                <tr>
                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0" class="search_results">
                      <tr>
                         <td width="19%" align="center"><span style="font-size:14px;"><%=flightName %></span></td>
                        <td width="8%" height="25" align="center">去程：</td>
                        <td width="24%" align="left"><%=orderInfo.HomeCityName%>--&gt;<%=orderInfo.DestCityName%></td>
                        <td width="16%" align="left">航班号：暂无
                          </td>
                        <td width="16%" align="left">出发时间：<%=orderInfo.LeaveTime.ToString("yyyy-MM-dd") %></td>
                        <td width="17%" align="left">旅客类型：<%=orderInfo.TravellerType %></td>
                      </tr>
                      <%if (orderInfo.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                        { %>
                      <tr>
                         <td width="19%" align="center"><span style="font-size:14px;"></span></td>
                        <td width="8%" height="25" align="center">回程：</td>
                        <td width="24%" align="left"><%=orderInfo.DestCityName%>--&gt;<%=orderInfo.HomeCityName%></td>
                        <td width="16%" align="left">航班号：暂无
                          </td>
                        <td width="16%" align="left">返回时间：<%=orderInfo.ReturnTime.ToString("yyyy-MM-dd")%></td>
                        <td width="17%" align="left">旅客类型：<%=orderInfo.TravellerType%></td>
                      </tr><%} %>
                  </table></td>
                </tr>
                <tr>
                  <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td height="30" align="center"><table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                            <tr bgcolor="#EDF8FC">
                              <th width="12%" height="30" align="center">订单号</th>
                                <th width="12%" align="center">PNR</th>
                                <th width="12%" align="center">更换PNR</th>
                                <th width="12%" align="center">代理商名称</th>
                                <th width="12%" align="center">类型</th>
                            </tr>
                            <tr>
                               <td height="25" align="center"><%=orderInfo.OrderNo %></td>
                                <td align="center"></td>
                                <td align="center"></td>
                              
                                <td align="center"><%=orderInfo.SupplierCName %></td>
                                <td align="center"><%=orderInfo.RateType %></td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="30" align="center"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
                            <tr bgcolor="#EDF8FC">
                              <th width="5%" height="30" align="center">&nbsp;</th>
                              <th width="10%" align="center">面价</th>
                              <th width="10%" align="center">参考扣率</th>
                              <th width="14%" align="center">运价有效期</th>
                              <th width="18%" align="center">结算价（不含税）</th>
                              <th width="10%" align="center">人数上限</th>
                              <th width="14%" align="center">燃油/机建</th>
                            </tr>
                            <!--  <tr><td colspan="7" height="1" class="line"></td></tr>-->
                            <tr>
                              <td height="25" align="center">去程</td>
                              <td align="center">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.LeaveFacePrice) %></td>
                              <td align="center"><font color="#FF6600"><%=Utils.GetMoney(orderInfo.OrderRateInfo.LeaveDiscount) %>%</font></td>
                              <td align="center"><%=orderInfo.OrderRateInfo.LeaveTimeLimit %></td>
                              <td align="center"><span class="jiesuanjia">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.LeavePrice) %></span></td>
                              <td align="center"><%=orderInfo.OrderRateInfo.MaxPCount %></td>
                              <td align="center">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.LFuelPrice)%>/<%=Utils.GetMoney(orderInfo.OrderRateInfo.LBuildPrice)%></td>
                            </tr>
                             <%if (orderInfo.FreightType == EyouSoft.Model.TicketStructure.FreightType.来回程)
                               { %>
                            <tr>
                              <td height="25" align="center">回程</td>
                              <td align="center">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.ReturnFacePrice)%></td>
                              <td align="center"><font color="#FF6600"><%=Utils.GetMoney(orderInfo.OrderRateInfo.ReturnDiscount) %>%</font></td>
                              <td align="center"><%=orderInfo.OrderRateInfo.ReturnTimeLimit %></td>
                              <td align="center"><span class="jiesuanjia">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.ReturnPrice)%></span></td>
                              <td align="center"><%=orderInfo.OrderRateInfo.MaxPCount %></td>
                              <td align="center">￥<%=Utils.GetMoney(orderInfo.OrderRateInfo.RFuelPrice)%>/<%=Utils.GetMoney(orderInfo.OrderRateInfo.RBuildPrice)%></td>
                            </tr><%} %>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="8%" rowspan="2" align="center"><a href="#"><img src="<%=ImageServerUrl%>/images/jipiao/MQ.jpg" width="46" height="16" /></a></td>
                        <td width="18%" height="25" align="left">联系人：<%=supplierInfo.ContactName %></td>
                        <td width="18%" align="left">上下班时间：<%=supplierInfo.WorkStartTime %>--<%=supplierInfo.WorkEndTime %></td>
                        <td width="20%" align="left">代理级别：<%=supplierInfo.ProxyLev%></td>
                        <td width="28%" align="left">供应商主页：<a href="http://<%= supplierInfo.WebSite %>" target="_blank">http://<%= supplierInfo.WebSite %></a></td>
                      </tr>
                      <tr>
                        <td height="25" align="left">联系电话：<%=supplierInfo.ContactTel %></td>
                        <td align="left">出票成功率：<%=(supplierInfo.SuccessRate*100).ToString("F2")%>%</td>
                        <td align="left"><span style="line-height:33px;">处理数/提交数：<%=supplierInfo.HandleNum %>/<%=supplierInfo.SubmitNum%>（张）</span></td>
                        <td colspan="2" align="left">退票平均时间：自愿/非自愿：<%=supplierInfo.RefundAvgTime %>/<%=supplierInfo.NoRefundAvgTime %>（小时）</td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td align="left"><span class="zhechebz">！供应商备注:<%=orderInfo.OrderRateInfo.SupplierRemark %></span></td>
                </tr>
            </table></td>
          </tr>
        </table>
      </div>
	  <div class="sidebar02_con_table03">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">旅客信息</span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                <tr>
                  <td height="25" colspan="7" align="left"><table width="45%" border="0" cellspacing="0" cellpadding="0" >
                      <tr>
                        <th width="15%" align="center"><font color="#FF6600">旅客人数：<%=orderInfo.PCount %>位</font></th>
                        <th width="15%" align="center"><font color="#FF6600">购买保险：<%= buyInsCount %>份</font></th>
                        <th width="15%" align="center"><font color="#FF6600">购买行程单：<%= buyItineraryCount %>份</font></th>
                      </tr>
                  </table></td>
                </tr>
                <myASP:CustomRepeater id="acl_rptCustomerList" runat="server">
                <HeaderTemplate>
                <tr>
                  <th width="17%" height="30" align="center">乘客姓名</th>
                  <th width="10%" align="center">乘客类型</th>
                  <th width="15%" align="center">证件类型</th>
                  <th width="17%" align="center">证件号码</th>
                  <th width="17%" align="center">票号</th>
                  <th width="15%" align="center">购买行程单</th>
                  <th width="15%" align="center">购买保险</th>
                </tr>
                </HeaderTemplate>
                <ItemTemplate>
                 <tr>
                  <td height="25" align="center"><%# Eval("TravellerName")%></td>
                  <td  align="center"><%# (EyouSoft.Model.CompanyStructure.Sex)Eval("Gender")%></td>
                  <td align="center"><%# Eval("CertType")%></td>
                  <td align="center"><%# Eval("CertNo")%></td>
                  <td  align="center"></td>
                  <td  align="center"><%#Convert.ToBoolean(Eval("IsBuyItinerary"))?"是":"否"%></td>
                  <td  align="center"><%#Convert.ToBoolean(Eval("IsBuyIns")) ? "是" : "否"%></td>
                </tr>
                </ItemTemplate>
                
              
                </myASP:CustomRepeater>
            </table></td>
          </tr>
        </table>
      </div>
	  <div class="sidebar02_con_table03">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">支付方式和支付金额</span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                <tr>
                  <td height="25" colspan="2" align="left"><span style="padding-left:40px; color:#FF6600; font-weight:bold;">乘客人数:<%=orderInfo.PCount %>位</span></td>
                </tr>
                <tr>
                  <td width="18%" height="30" align="center">订单总金额：</td>
                  <td width="80%" align="left">&nbsp;共计：<font color="#FF0000" size="+2"><%= Utils.GetMoney(orderInfo.TotalAmount) %></font>元</td>
                </tr>
                <tr>
                  <td height="30" colspan="2" align="left">&nbsp;&nbsp;<font color="#FF0000">注：（结算价格+燃油/机建）*人数+保险*人数+行程单*人数+快递费 &nbsp;&nbsp;</font> </td>
                </tr>
            </table></td>
          </tr>
        </table>
      </div>
      
      
	  <%--<div class="sidebar02_con_table03" style=" display:none;">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">订单状态</span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8"  class="sidebar02_con_lianxi">
                <tr>
                  <td width="25%" height="25" align="center">订单状态</td>
                  <td width="37%" align="center">操作时间</td>
                  <td width="37%" align="center">操作人</td>
                </tr>
                <tr>
                  <td width="25%" height="25" align="center">等待审核</td>
                  <td width="37%" align="center">2010-9-19 10:08:39</td>
                  <td width="37%" align="center">张小姐</td>
                </tr>
                <tr class="orderform_state">
                  <td width="25%" height="25" align="center">等待支付</td>
                  <td width="37%" align="center">2010-9-22 10:08:39</td>
                  <td width="37%" align="center">张小姐</td>
                </tr>
                <tr>
                  <td width="25%" height="25" align="center">支付成功</td>
                  <td width="37%" align="center">2010-9-24 10:08:39</td>
                  <td width="37%" align="center">张小姐</td>
                </tr>
                <tr>
                  <td width="25%" height="25" align="center">等待出票</td>
                  <td width="37%" align="center">2010-9-26 10:08:39</td>
                  <td width="37%" align="center">张小姐</td>
                </tr>
            </table></td>
          </tr>
        </table>
      </div>--%>
    
    
	  <div class="sidebar02_con_table03">
	  <form id="tp_form1" action="" method="post">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">联系方式 <a href="#"><img src="<%=ImageServerUrl%>/images/jipiao/MQ.jpg" width="46" height="16" /></a></span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8"  class="sidebar02_con_lianxi">
                <tr>
                  <td width="25%" height="25" align="left">联系公司：</td>
                  <td width="74%" align="left"><input name="textfield" type="text" id="tp_txtCompany" value="<%=companyName %>" /></td>
                </tr>
                <tr>
                  <td height="25" align="left">联系人：</td>
                  <td align="left"><input name="textfield2" type="text" id="tp_txtContact" value="<%=contactName %>" /></td>
                </tr>
                <tr>
                  <td height="25" align="left">手机：</td>
                  <td align="left"><input name="textfield3" type="text" id="tp_txtMoible" value="<%=moible %>" /></td>
                </tr>
                <tr>
                  <td height="25" align="left">地址：</td>
                  <td align="left"><input name="textfield4" type="text" id="tp_txtAddress" value="<%=address %>" /></td>
                </tr>
                <tr>
                  <td colspan="2" align="left"><span class="zhechebz"></span></td>
                </tr>
                
            </table>
            </td>
          </tr>
        </table></form>
      </div>
      <table border="0" width="30%" align="center" cellpadding="0" cellspacing="0">
     <%-- <tr>
        <td width="100%" height="10" colspan="6" align="left"></td>
      </tr>--%>
      <tr>
      <%if (list != null && list.Count > 0)
        {
            foreach (EyouSoft.Model.TicketStructure.TicketCompanyAccount account in list)
            {
                if (account.IsSign)
                {
                    switch (account.InterfaceType)
                    {
                     
                        case EyouSoft.Model.TicketStructure.TicketAccountType.财付通:%>
        
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('1');"><img src="<%=ImageServerUrl%>/images/jipiao/cft.jpg" width="104" height="32" /></a></td>
         <%break;
                           case EyouSoft.Model.TicketStructure.TicketAccountType.支付宝:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('2');"><img src='<%=ImageServerUrl%>/images/jipiao/alipay.jpg' /></a></td>
        <%break;

                        case EyouSoft.Model.TicketStructure.TicketAccountType.工行:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('3');"><img src="<%=ImageServerUrl%>/images/jipiao/gs.jpg" width="102" height="32" /></a></td>
         <%break;

                        case EyouSoft.Model.TicketStructure.TicketAccountType.建行:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('4');"><img src="<%=ImageServerUrl%>/images/jipiao/jh.jpg" width="102" height="32" /></a></td>
         <%break;

                        case EyouSoft.Model.TicketStructure.TicketAccountType.农行:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('5');"><img src="<%=ImageServerUrl%>/images/jipiao/ny.jpg" width="102" height="32" /></a></td>
         <%break;

                        case EyouSoft.Model.TicketStructure.TicketAccountType.招行:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('6');"><img src="<%=ImageServerUrl%>/images/jipiao/zs.jpg" width="102" height="32" /></a></td>
         <%break;

                        case EyouSoft.Model.TicketStructure.TicketAccountType.其他银行:%>
        <td align="center"><a href="javascript:void(0);"  onclick="return InsertAccount('7');"><img src="<%=ImageServerUrl%>/images/jipiao/qt.jpg" width="102" height="32" /></a></td>
        <%break;
                    }
                }
            }
        } %>
     
      </tr>
    </table>
    </div>
    <script type="text/javascript">
        function InsertAccount(pay) {
            var companyName1 = $("#tp_txtCompany").val();
            var contactName1 = $("#tp_txtContact").val();
            var moible1 = $("#tp_txtMoible").val();
            var address1 = $("#tp_txtAddress").val();
            $.ajax(
	             {
	                 url: "TicketPay.aspx",
	                 data: { orderId: "<%=orderInfo.OrderId %>", homeCityName: "<%=orderInfo.HomeCityName %>", destCityName: "<%=orderInfo.DestCityName %>", leaveTime: '<%=orderInfo.LeaveTime.ToString("yyyy-MM-dd")%>', returnTime: '<%=orderInfo.ReturnTime==null?DateTime.Now.ToString("yyyy-MM-dd"):orderInfo.ReturnTime.ToString("yyyy-MM-dd")%>', freightType: "<%=(int)orderInfo.FreightType%>", orderNo: "<%=orderInfo.OrderNo %>", sellCId: "<%=orderInfo.SupplierCId %>", total: "<%=orderInfo.TotalAmount %>", method: "InsertAccount", paywhich: pay, companyname: companyName1, contactname: contactName1, moible: moible1, address: address1 },
	                 dataType: "json",
	                 cache: false,
	                 type: "post",
	                 success: function(result) {
	                     if (result.success == "1") {
	                         window.location = result.message;
	                     }
	                     else {
	                         alert(result.message);
	                     }
	                 },
	                 error: function() {

	                     alert("操作失败");
	                 }
	             });
	             return false;
        }
    
    </script>
</asp:Content>
