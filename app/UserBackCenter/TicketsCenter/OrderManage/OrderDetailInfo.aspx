<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailInfo.aspx.cs"
    Inherits="UserBackCenter.TicketsCenter.OrderManage.OrderDetailInfo" %>

<asp:content id="OrderDetailInfo" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="sidebar02_con" id="<%=ContainerID %>" style="width:90%">
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
                        <th width="15%" align="center"><span style="font-size:14px;"><asp:Label id="OrderDetailInfo_lblFlightName" runat="server"></asp:Label></span></th>
                        <td width="8%" height="25" align="center" class="showandhide">去程：</td>
                        <td width="25%" align="left" class="row2"><asp:Label id="OrderDetailInfo_lblHomeCityId" runat="server"></asp:Label>--&gt;<asp:Label id="OrderDetailInfo_lblDestCityId" runat="server"></asp:Label></td>
                        <td align="left" class="row2">航班号：<asp:Label id="OrderDetailInfo_lblFlightCode" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtFlightCode" runat="server" visible="false" width="70px"></asp:TextBox></td>
                        <td width="16%" align="left" class="row2">出发日期：<asp:Label id="OrderDetailInfo_lblLeaveTime" runat="server"></asp:Label></td>
                        <td width="13%" align="left" class="row2">旅客类型：<asp:Label id="OrderDetailInfo_lblTravellerType" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                        <td width="15%" align="center">航班类型：<asp:Label id="OrderDetailInfo_lblFreightType" runat="server"></asp:Label> </td>
                        <td height="25" align="center" class="showandhide">回程：</td>
                        <td align="left" class="showandhide"><asp:Label id="OrderDetailInfo_lblBackHomeCityId" runat="server"></asp:Label>--&gt;<asp:Label id="OrderDetailInfo_lblBackDestCityId" runat="server"></asp:Label></td>
                        <td align="left" class="showandhide">航班号：<asp:Label id="OrderDetailInfo_lblBackFlightCode" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtBackFlightCode" runat="server" visible="false" width="70px"></asp:TextBox></td>
                        <td align="left" class="showandhide">返回日期：<asp:Label id="OrderDetailInfo_lblBackTime" runat="server"></asp:Label></td>
                        <td align="left" class="showandhide">旅客类型：<asp:Label id="OrderDetailInfo_lblBackTravellerType" runat="server"></asp:Label></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td height="30" align="center"><table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                            <tr bgcolor="#EDF8FC">
                              <th width="12%" height="30" align="center">订单号</th>
                              <th width="13%" align="center">PNR</th>
                              <th width="12%" align="center" id="td_PNR" runat="server">更换PNR</th>
                              <th width="25%" align="center">代理商名称</th>
                              <th width="25%" align="center">类型</th>
                            </tr>
                            <tr>
                              <td height="30" align="center"><asp:Label id="OrderDetailInfo_lblOrderNo" runat="server"></asp:Label></td>
                              <td height="30" align="center"><asp:Label id="OrderDetailInfo_lblPNR" runat="server"></asp:Label></td>
                              <td align="center" id="td_PNR1" runat="server"><asp:Label id="OrderDetailInfo_lblUpdatePNR" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtUpdatePNR" runat="server" width="70px" visible="false"></asp:TextBox></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblSupplierName" runat="server"></asp:Label></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblRateType" runat="server"></asp:Label></td>
                            </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="30" align="center"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#CCCCCC">
                            <tr bgcolor="#EDF8FC">
                              <th width="5%" height="30" align="center" class="showandhide">&nbsp;</th>
                              <th width="10%" align="center">面价</th>
                              <th width="10%" align="center">参考扣率</th>
                              <th width="14%" align="center">运价有效期</th>
                              <th width="18%" align="center">结算价（不含税）</th>
                              <th width="10%" align="center">人数上限</th>
                              <th width="14%" align="center">燃油/机建</th>
                            </tr>
                            <tr>
                              <td height="30" align="center" class="showandhide">去程</td>
                              <td align="center">￥<asp:Label id="OrderDetailInfo_lblLeaveFacePrice" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtLeaveFacePrice" runat="server" visible="false" width="70px"></asp:TextBox></td>
                              <td align="center"><font color="#FF6600"><asp:Label id="OrderDetailInfo_lblLeaveDiscount" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtLeaveDiscount" runat="server" visible="false" width="70px" ></asp:TextBox><asp:Literal id="ltrLeaveDiscount" runat="server" visible="false">%</asp:Literal></font></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblLeaveTimeLimit" runat="server"></asp:Label></td>
                              <td align="center"><span class="jiesuanjia">￥<asp:Label id="OrderDetailInfo_lblLeavePrice" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtLeavePrice" runat="server" visible="false" width="70px" readonly="true" ></asp:TextBox></span></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblMaxPCount" runat="server"></asp:Label></td>
                              <td align="center">￥<asp:label id="OrderDetailInfo_lblLOtherPrice" runat="server"></asp:label><asp:TextBox id="OrderDetailInfo_txtLOtherPrice" runat="server" visible="false" width="40px"></asp:TextBox><asp:TextBox id="OrderDetailInfo_txtLOtherPrice2" runat="server" visible="false" width="40px"></asp:TextBox></td>
                            </tr>
                            <tr class="showandhide">
                              <td height="30" align="center">回程</td>
                              <td align="center">￥<asp:Label id="OrderDetailInfo_lblReturnFacePrice" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtReturnFacePrice" runat="server" visible="false" width="70px"></asp:TextBox></td>
                              <td align="center"><font color="#FF6600"><asp:Label id="OrderDetailInfo_lblReturnDiscount" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtReturnDiscount" runat="server" visible="false" width="70px"></asp:TextBox><asp:Literal id="ltrReturnDiscount" runat="server" visible="false">%</asp:Literal></font></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblReturnTimeLimit" runat="server"></asp:Label></td>
                              <td align="center"><span class="jiesuanjia">￥<asp:Label id="OrderDetailInfo_lblReturnPrice" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtReturnPrice" runat="server" visible="false" width="70px" readonly="true"></asp:TextBox></span></td>
                              <td align="center"><asp:Label id="OrderDetailInfo_lblMaxPCount1" runat="server"></asp:Label></td>
                              <td align="center">￥<asp:Label id="OrderDetailInfo_lblROtherPrice" runat="server"></asp:Label><asp:TextBox id="OrderDetailInfo_txtROtherPrice" runat="server" visible="false" width="40px"></asp:TextBox><asp:TextBox id="OrderDetailInfo_txtROtherPrice2" runat="server" visible="false" width="40px"></asp:TextBox></td>
                            </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="19%" height="25" align="left">&nbsp;联系人：<asp:Label id="OrderDetailInfo_lblContactName" runat="server"></asp:Label></td>
                        <td width="19%" align="left">上下班时间：<asp:Label id="OrderDetailInfo_lblWorkTime" runat="server"></asp:Label></td>
                        <td width="18%" align="left">代理级别：<asp:Label id="OrderDetailInfo_lblProxyLev" runat="server"></asp:Label></td>
                        <%--<td width="13%" align="left"></td>--%>
                        <td width="34%" colspan="2" align="left">供应商主页：<asp:Label id="OrderDetailInfo_lblWebSite" runat="server"></asp:Label></td>
                      </tr>
                      <tr>
                        <td height="25" align="left">&nbsp;联系电话：<asp:Label id="OrderDetailInfo_lblContactTel" runat="server"></asp:Label></td>
                        <td align="left">出票成功率：<asp:Label id="OrderDetailInfo_lblSuccessRate" runat="server"></asp:Label></td>
                        <td align="left"><span style="line-height:33px;">处理数/提交数：<asp:Label id="OrderDetailInfo_lblHandleNum" runat="server"></asp:Label>/<asp:Label id="OrderDetailInfo_lblSubmitNum" runat="server"></asp:Label>（张）</span></td>
                        <td colspan="2" align="left">退票平均时间：自愿/非自愿：<asp:label id="OrderDetailInfo_lblRefundAvgTime" runat="server"></asp:label>/<asp:Label id="OrderDetailInfo_lblNoRefundAvgTime" runat="server"></asp:Label>（小时）</td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td height="40" align="left"><span class="zhechebz">！供应商备注：<asp:Label id="OrderDetailInfo_lblSupplierRemark" runat="server"></asp:Label></span></td>
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
                  <td height="25" colspan="8" align="left"><table width="45%" border="0" cellspacing="0" cellpadding="0" >
                      <tr>
                        <th width="15%" align="center"><font color="#FF6600">旅客人数：<asp:label id="OrderDetailInfo_lblTravellerCount" runat="server"></asp:label>位</font></th>
                        <th width="15%" align="center"><font color="#FF6600">购买保险：[<asp:Label id="OrderDetailInfo_lblByInsCount" runat="server"></asp:Label>]份</font></th>
                        <th width="15%" align="center"><font color="#FF6600">购买行程单：[<asp:Label id="OrderDetailInfo_lblBuyItinerary" runat="server"></asp:Label>]份</font></th>
                      </tr>
                  </table></td>
                </tr>                    
                <tr>
                  <th width="13%" height="30" align="center">乘客姓名</th>
                  <th width="12%" align="center">乘客类型</th>
                  <th width="12%" align="center">证件类型</th>
                  <th width="18%" align="center">证件号码</th>
                  <th width="10%" align="center" class="ticketnumber">票号</th>
                  <th width="12%" align="center">购买保险</th>
                  <th width="12%" align="center">购买行程单</th>
                  <th align="center" width="12%">旅客状态</th>
                </tr>
                <asp:repeater id="OrderDetailInfo_rptTravellerInfo" runat="server">
                    <itemtemplate>
                        <tr>
                          <td height="30" align="center"><%# DataBinder.Eval(Container.DataItem, "TravellerName")%><input type="hidden" name="OrderDetailInfo_hidTravellerID" value='<%# DataBinder.Eval(Container.DataItem,"TravellerId") %>' /></td>
                          <td width="16%" align="center"><%# DataBinder.Eval(Container.DataItem,"TravellerType") %></td>
                          <td align="center"><%# DataBinder.Eval(Container.DataItem, "CertType")%></td>
                          <td align="center"><%# DataBinder.Eval(Container.DataItem, "CertNo")%></td>
                          <td width="8%" align="center" class="ticketnumber"><%# ShowTicketNumber(DataBinder.Eval(Container.DataItem, "TravellerId").ToString(), DataBinder.Eval(Container.DataItem, "TicketNumber").ToString())%> </td>
                          <td width="8%" align="center"><%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBuyIns")) ? "是" : "否"%></td>
                          <td width="8%" align="center"><%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IsBuyItinerary")) ? "是" : "否"%></td>
     <td align="center"><%# DataBinder.Eval(Container.DataItem,"TravellerState").ToString() %></td>
                        </tr>
                    </itemtemplate>
                </asp:repeater>                    
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
                  <td height="25" colspan="4" align="left"><span style="padding-left:40px; color:#FF6600; font-weight:bold;">乘客人数:<asp:label id="OrderDetailInfo_lblCustomerCount" runat="server"></asp:label>位</span></td>
                </tr>
                <tr>
                  <td width="10%" height="30" align="center">订单总金额：</td>
                  <td width="30%" align="left">&nbsp;共计：<font color="#FF0000" size="+2"><asp:Label id="OrderDetailInfo_lblPayPrice" runat="server"></asp:Label></font>元</td>
                  <td width="18%" align="center" id="td_PayType" runat="server">支付方式：</td>
                  <td align="left" id="td_PayType1" runat="server"><font color="#990000">&nbsp;<asp:Label id="OrderDetailInfo_lblPayType" runat="server"></asp:Label></font></td>
                </tr>
                <tr>
                  <td height="30" colspan="4" align="left">&nbsp;&nbsp;<font color="#FF0000">注：（结算价格+燃油/机建）*人数+保险*人数+行程单*人数+快递费 &nbsp;&nbsp;共计 <strong><asp:Label id="OrderDetailInfo_lblPayPrice1" runat="server"></asp:Label></strong> 元</font> </td>
                </tr>
            </table></td>
          </tr>
        </table>
      </div>
       <div class="sidebar02_con_table03">
          <table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8" bgcolor="#FAFAFA">
            <tr align="left">
              <td height="30" bgcolor="#E0F4FD"><span class="title"><%=OrderStateText %>费用</span></td>
            </tr>
            <tr>
              <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                  <tr>
                    <td height="25" colspan="4" align="left"><span style="padding-left:40px; color:#FF6600; font-weight:bold;"><%=OrderStateText %>人数:<asp:label id="OrderDetailInfo_lblRefundCount" runat="server"></asp:label>位</span></td>
                </tr>
                  <tr>
                    <td width="16%" height="30" align="center"><%=OrderStateText %>手续费：</td>
                    <td width="16%" align="center"><asp:TextBox id="OrderDetailInfo_txtFee" runat="server" width="50px"></asp:TextBox> 元 / 人</td>
                    <td width="38%" align="center">总金额（<font color="#FF0000"><%=OrderStateText %>人数 * <%=OrderStateText %>手续费</font>）：</td>
                    <td width="30%" align="left">&nbsp;共计：<font color="#FF0000" size="+2"><asp:Label id="OrderDetailInfo_lblRefundprice" runat="server">0</asp:Label></font>元</td>
                  </tr>
              </table></td>
            </tr>
          </table>
        </div>
      <div class="sidebar02_con_table03">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">订单处理状态</span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8"  class="sidebar02_con_lianxi">
                <tr>
                  <td width="25%" height="25" align="center">订单状态</td>
                  <td width="27%" align="center">操作时间</td>
                  <td width="27%" align="center">操作人</td>
                  <td width="27%" align="center">备  注</td>
                </tr>
                <asp:Repeater id="OrderDetailInfo_rptOrderLog" runat="server">
                    <itemtemplate>
                        <tr class="orderform_state">
                          <td width="25%" height="25" align="center"><%# DataBinder.Eval(Container.DataItem, "State")%></td>
                          <td width="27%" align="center"><%# DataBinder.Eval(Container.DataItem, "Time","{0:yyyy-MM-dd HH:mm:ss}")%></td>
                          <td width="27%" align="center"><%# DataBinder.Eval(Container.DataItem, "UserName")%></td>
                          <td width="27%" align="center"><%# DataBinder.Eval(Container.DataItem, "Remark")%></td>
                        </tr>
                    </itemtemplate>
                </asp:Repeater> 
            </table></td>
          </tr>
        </table>
      </div>
      <div class="sidebar02_con_table03">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FAFAFA">
          <tr align="left">
            <td height="30" bgcolor="#E0F4FD"><span class="title">采购商联系方式 <asp:Literal id="OrderDetailInfo_ltrMQ" runat="server"></asp:Literal></span></td>
          </tr>
          <tr>
            <td align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
                <tr>
                  <th width="25%" height="25" align="center">联系公司</th>
                  <th width="25%" align="center">地址</th>
                  <th width="25%" align="center">联系人</th>
                  <th width="25%" align="center">手机</th>
                </tr>
                <tr>
                  <td height="25" align="center"><asp:Label id="OrderDetailInfo_lblBuyerCName" runat="server"></asp:Label></td>
                  <td align="center"><asp:Label id="OrderDetailInfo_lblBuyerContactAddress" runat="server"></asp:Label></td>
                  <td align="center"><asp:Label id="OrderDetailInfo_lblBuyerContactName" runat="server"></asp:Label></td>
                  <td align="center"><asp:Label id="OrderDetailInfo_lblBuyerContactMobile" runat="server"></asp:Label></td>
                </tr>
                <tr>
                  <td height="40" colspan="4" align="left"><span class="zhechebz">！采购商备注：<asp:Label id="OrderDetailInfo_lblBuyerRemark" runat="server"></asp:Label></span></td>
                </tr>
            </table></td>
          </tr>
        </table>
      </div>
      <table width="835" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="43%" height="55" align="right"><input type="button" name="OrderDetailInfo_btnCancle" id="OrderDetailInfo_btnCancle" value="拒绝审核" style="height:25px;" /></td>
        <td width="6%" align="center">&nbsp;</td>
        <td width="48%" align="left"><input type="button" name="OrderDetailInfo_btnSubmit" id="OrderDetailInfo_btnSubmit" value="审核通过" style="height:25px;"/></td>
      </tr>
    </table>
    <input type="hidden" id="OrderDetailInfo_hidOrderId" name="OrderDetailInfo_hidOrderId" runat="server" />
    <input type="hidden" id="OrderDetailInfo_hidOrderState" name="OrderDetailInfo_hidOrderState" runat="server" />
    <input type="hidden" id="OrderDetailInfo_hidChangeType" name="OrderDetailInfo_hidChangeType" runat="server" />
</div>
   	  
   	  <script type="text/javascript">
   	      var OrderDetailInfo = {
   	          Show_AND_Hide: function() {
   	              var orderstate = '<%=OrderState %>';
   	              var changetype = '<%=ChangeType %>';
   	              var showtype = '<%=showtype %>';
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);

   	              if (showtype == 'search') {   // 如果是查看页面不显示操作按钮和退改签费用处理块
   	                  divID.find(".sidebar02_con_table03").eq(2).css("display", "none");
   	                  divID.find("#OrderDetailInfo_btnCancle").css("display", "none");
   	                  divID.find("#OrderDetailInfo_btnSubmit").css("display", "none");

   	                  if (orderstate == '等待审核' || orderstate == '支付成功') {  // 审核时旅客列表不显示票号
   	                      divID.find("td .ticketnumber").css("display", "none");
   	                  }
   	              } else {
   	                  if (orderstate == '等待审核') {
   	                      divID.find(".sidebar02_con_table03").eq(2).css("display", "none");//隐藏退票费用信息
   	                      divID.find("td .ticketnumber").css("display", "none");//隐藏票号信息

   	                      divID.find("#OrderDetailInfo_btnCancle").val("拒绝审核");
   	                      divID.find("#OrderDetailInfo_btnSubmit").val("审核通过");
   	                      divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                          OrderDetailInfo.OpenDialog('拒绝审核理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=0&orderid=<%=OrderId %>', 330, 200);
   	                          return false;
   	                      });
   	                  }
   	                  else if (orderstate == '支付成功') {
   	                      divID.find(".sidebar02_con_table03").eq(2).css("display", "none");

   	                      divID.find("#OrderDetailInfo_btnCancle").val("拒绝出票");
   	                      divID.find("#OrderDetailInfo_btnSubmit").val("出票完成");
   	                      divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                          OrderDetailInfo.OpenDialog('拒绝出票理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=3&orderid=<%=OrderId %>', 330, 200);
   	                          return false;
   	                      });
   	                  }
   	                  else if (orderstate == '出票完成') {
   	                      if (changetype == '退票') {
   	                          divID.find("#OrderDetailInfo_btnCancle").val("拒绝退票");
   	                          divID.find("#OrderDetailInfo_btnSubmit").val("退票完成");
   	                          divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                              OrderDetailInfo.OpenDialog('拒绝退票理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=5&orderid=<%=OrderId %>&changetype=0', 330, 200);
   	                              return false;
   	                          });
   	                      }
   	                      else if (changetype == '作废') {
   	                          divID.find("#OrderDetailInfo_btnCancle").val("拒绝作废");
   	                          divID.find("#OrderDetailInfo_btnSubmit").val("作废完成");
   	                          divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                              OrderDetailInfo.OpenDialog('拒绝作废理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=5&orderid=<%=OrderId %>&changetype=1', 330, 200);
   	                              return false;
   	                          });
   	                      }
   	                      else if (changetype == '改期') {
   	                          divID.find(".sidebar02_con_table03").eq(2).css("display", "none");

   	                          divID.find("#OrderDetailInfo_btnCancle").val("拒绝改期");
   	                          divID.find("#OrderDetailInfo_btnSubmit").val("改期完成");
   	                          divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                              OrderDetailInfo.OpenDialog('拒绝改期理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=5&orderid=<%=OrderId %>&changetype=2', 330, 200);
   	                              return false;
   	                          });
   	                      }
   	                      else if (changetype == '改签') {
   	                          divID.find(".sidebar02_con_table03").eq(2).css("display", "none");

   	                          divID.find("#OrderDetailInfo_btnCancle").val("拒绝改签");
   	                          divID.find("#OrderDetailInfo_btnSubmit").val("改签完成");
   	                          divID.find("#OrderDetailInfo_btnCancle").click(function() {
   	                              OrderDetailInfo.OpenDialog('拒绝改签理由', '/ticketscenter/ordermanage/cancleorder.aspx?orderstate=5&orderid=<%=OrderId %>&changetype=3', 330, 200);
   	                              return false;
   	                          });
   	                      }
   	                  }
   	              }
   	          },
   	          OpenDialog: function(title, url, width, height, data) {
   	              Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: data });
   	          },
   	          CheckFreightType: function() {  // 如果是单程将航班信息里面的回程信息隐藏
   	              var frighttype = '<%=FreightType %>';
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              if (frighttype == '单程') {    // 单程
   	                  divID.find("table td.showandhide").css("display", "none");
   	                  divID.find("table th.showandhide").css("display", "none");
   	                  divID.find("table tr.showandhide").css("display", "none");
   	                  divID.find("table td.row2").attr("rowspan", "2");
   	              }
   	          },
   	          SubmitData: function(state,box) {
   	              var changetype = '<%=ChangeType %>';
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              var data = null;
   	              if (state == '等待审核' || state == '支付成功' || state == '出票完成') {
   	                  data = $(divID.find("form").get(0)).serializeArray();
   	              }
   	              $.newAjax({
   	                  type: "POST",
   	                  url: "/ticketscenter/ordermanage/orderdetailinfo.aspx?orderid=<%=OrderId %>&orderstate=" + divID.find("#<%=OrderDetailInfo_hidOrderState.ClientID %>").val() + "&changetype=" + divID.find("#<%=OrderDetailInfo_hidChangeType.ClientID %>").val() + "&flag=" + state,
   	                  data: data,
   	                  cache: false,
   	                  dataType:"json",
   	                  success: function(result) {
   	                      switch (state) {
   	                          case '等待审核':
   	                              if (result.success == '1') {
   	                                  alert(result.message);
   	                                  box.hide();
   	                                  topTab.url(topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%=OrderId %>');
   	                                  return false;
   	                              } else {
   	                                  if(box){
   	                                    box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
   	                                  }else{
   	                                    alert(result.message);
   	                                  }
   	                              }
   	                              break;
   	                          case '支付成功':
   	                              if (result.success == '1') {
   	                                  alert(result.message);
   	                                  box.hide();
   	                                  topTab.url(topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%=OrderId %>');
   	                              } else {
   	                                  if(box){
   	                                    box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
   	                                  }else{
   	                                    alert(result.message);
   	                                  }
   	                              }
   	                              break;
   	                          case '出票完成':
   	                              if (changetype == '退票'||changetype == '作废') {
   	                                  if (result.success == '1') {
   	                                      
   	                                      if(result.paytype==undefined||result.paytype==null){
                                                alert(result.message);
                                                box.hide();
   	                                            topTab.url(topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%=OrderId %>');
                                                return;
                                            }
   	                                      
   	                                      box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
   	                                      var search = function(){
                                            $.newAjax({
                                                type: "POST",
                                                url: "/ticketscenter/ordermanage/orderdetailinfo.aspx?orderid=<%=OrderId %>&orderstate=" + divID.find("#<%=OrderDetailInfo_hidOrderState.ClientID %>").val() + "&changetype=" + divID.find("#<%=OrderDetailInfo_hidChangeType.ClientID %>").val() + "&flag=isalipayrefund&changeid="+result.changeid+"&batchno="+result.batchno,
                                                data: data,
                                                dataType:"json",
                                                cache: false,
                                                success:function(result){
                                                    if(result.success='1'){
                                                        if(result.search=="1"){
                                                            box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
                                                            setTimeout(function(){
                                                                search();
                                                            },500);
                                                        }else{
                                                          alert(result.message);
                                                          box.hide();
                                                          topTab.url(topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%=OrderId %>');
                                                          //box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
                                                        }
                                                    }else{
                                                        //alert(result.message);
                                                        box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
                                                    } 
                                                }
                                            });
                                        }
                                        setTimeout(function(){search();},100);
                                        
   	                                  } else {
   	                                      if(box){
   	                                        box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
   	                                      }else{
   	                                        alert(result.message);
   	                                      }
   	                                  }
   	                              }else if (changetype == '改期'||changetype == '改签') {
   	                                  if (result.success == '1') {
   	                                      alert(result.message);
   	                                      box.hide();
   	                                      topTab.url(topTab.activeTabIndex,'/ticketscenter/ordermanage/orderdetailinfo.aspx?type=search&orderid=<%=OrderId %>');
   	                                  } else {
   	                                      if(box){
   	                                        box.setContent(OrderDetailInfo.formatBoxyContent(result.message));
   	                                      }else{
   	                                        alert(result.message);
   	                                      }
   	                                  }
   	                              }
   	                              break;
   	                      }
   	                  }
   	              });
   	          },
   	          SubmitCheckPrice: function() {
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              var orderstate = '<%=OrderState %>';
   	              var frighttype = '<%=FreightType %>';
   	              var msg = '';
   	              
                  //航班号
   	              var LFlightCode = divID.find("#<%=OrderDetailInfo_txtFlightCode.ClientID %>").val();
   	              LFlightCode = $.trim(LFlightCode);

   	              if (LFlightCode == '') {
   	                  msg += '请填写去程航班号!\n';
   	              }
   	              if (frighttype == '来回程') {
   	                  var RFlightCode = divID.find("#<%=OrderDetailInfo_txtBackFlightCode.ClientID %>").val();
   	                  RFlightCode = $.trim(RFlightCode);
   	                  if (RFlightCode == '') {
   	                      msg += '请填写回程航班号!\n';
   	                  }
   	              }
   	              
   	              //去程面价
                  var LeaveFacePrice = divID.find("#<%=OrderDetailInfo_txtLeaveFacePrice.ClientID %>").val();
                  LeaveFacePrice = $.trim(LeaveFacePrice);
   	              if (LeaveFacePrice == '') {
   	                  msg += '请填写去程面价!\n';
   	              }else{
   	                  if(!OrderDetailInfo.isDecimalTwo.test(LeaveFacePrice)){
   	                    msg+="请填写正确的去程面价\n";
   	                  }
   	              }
   	              //去程参考扣率
   	              var LeaveDiscount = divID.find("#<%=OrderDetailInfo_txtLeaveDiscount.ClientID %>").val();
   	              LeaveDiscount = $.trim(LeaveDiscount);
   	              if ( LeaveDiscount == '') {
   	                  msg += '请填写去程参考扣率!\n';
   	              }else{
   	                if(!/^[0-9]+([.]\d{0,1})?$/.test(LeaveDiscount)){
   	                    msg+="请填写正确的去程参考扣率\n";
   	                }else{
   	                    var rate = Number(LeaveDiscount);
                        if(rate>100||rate<0){
                            msg+="参考扣率必须在0-100之间\n";
                        }
   	                }
   	              }
   	              //去程燃油
   	              var LeaveOtherPrice = divID.find("#<%=OrderDetailInfo_txtLOtherPrice.ClientID %>").val();
   	              LeaveOtherPrice = $.trim(LeaveOtherPrice);
   	              if (LeaveOtherPrice == '') {
   	                  msg += '请填写去程燃油费!\n';
   	              }else{
   	                if(!OrderDetailInfo.isDecimalTwo.test(LeaveOtherPrice)){
   	                    msg+="请填写正确的去程燃油费\n";
   	                  }
   	              }
   	              //去程机建
                    var LeaveOtherPrice2 = divID.find("#<%=OrderDetailInfo_txtLOtherPrice2.ClientID %>").val();
   	              LeaveOtherPrice2 = $.trim(LeaveOtherPrice2);
   	              if (LeaveOtherPrice2 == '') {
   	                  msg += '请填写去程机建费!\n';
   	              }else{
   	                if(!OrderDetailInfo.isDecimalTwo.test(LeaveOtherPrice2)){
   	                    msg+="请填写正确的去程机建费\n";
   	                  }
   	              }
   	              if (frighttype == '来回程') {
   	                  var ReturnFacePrice = divID.find("#<%=OrderDetailInfo_txtReturnFacePrice.ClientID %>").val();
   	                  var ReturnDiscount = divID.find("#<%=OrderDetailInfo_txtReturnDiscount.ClientID %>").val();
   	                  var ReturnOtherPrice = divID.find("#<%=OrderDetailInfo_txtROtherPrice.ClientID %>").val();
   	                  var ReturnOtherPrice2 = divID.find("#<%=OrderDetailInfo_txtROtherPrice2.ClientID %>").val();
   	                  //回程面价
                      ReturnFacePrice = $.trim(ReturnFacePrice);
   	                  if (ReturnFacePrice == '') {
   	                      msg += '请填写回程面价!\n';
   	                  }else{
   	                      if(!OrderDetailInfo.isDecimalTwo.test(ReturnFacePrice)){
   	                        msg+="请填写正确的回程面价\n";
   	                      }
   	                  }
   	                  //回程参考扣率
   	                  ReturnDiscount = $.trim(ReturnDiscount);
   	                  if ( ReturnDiscount == '') {
   	                      msg += '请填写回程参考扣率!\n';
   	                  }else{
   	                    if(!/^[0-9]+([.]\d{0,1})?$/.test(ReturnDiscount)){
   	                        msg+="请填写正确的回程参考扣率\n";
   	                    }else{
   	                        var rate = Number(ReturnDiscount.toString().replace(/^[0-9]+([.]\d{0,1})?$/,"$1"));
                            if(rate>100||rate<0){
                                msg+="参考扣率必须在0-100之间\n";
                            }
   	                    }
   	                  }
   	                  //回程燃油
   	                  ReturnOtherPrice = $.trim(ReturnOtherPrice);
   	                  if (ReturnOtherPrice == '') {
   	                      msg += '请填写回程燃油费!\n';
   	                  }else{
   	                    if(!OrderDetailInfo.isDecimalTwo.test(ReturnOtherPrice)){
   	                        msg+="请填写正确的回程燃油费\n";
   	                      }
   	                  }
   	                  //回程机建
   	                  ReturnOtherPrice2 = $.trim(ReturnOtherPrice2);
   	                  if (ReturnOtherPrice2 == '') {
   	                      msg += '请填写回程机建费!\n';
   	                  }else{
   	                    if(!OrderDetailInfo.isDecimalTwo.test(ReturnOtherPrice2)){
   	                        msg+="请填写正确的回程机建费\n";
   	                      }
   	                  }
   	              }

   	              if (msg != '') {
   	                  alert(msg);
   	                  return false;
   	              } else{
   	                  return true;
   	              }
   	          },
   	          SubmitCheckFlight:function(){
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              var orderstate = '<%=OrderState %>';
   	              var frighttype = '<%=FreightType %>';
   	              var msg = '';
   	              
                  //航班号
   	              var LFlightCode = divID.find("#<%=OrderDetailInfo_txtFlightCode.ClientID %>").val();
   	              LFlightCode = $.trim(LFlightCode);

   	              if (LFlightCode == '') {
   	                  msg += '请填写去程航班号!\n';
   	              }
   	              if (frighttype == '来回程') {
   	                  var RFlightCode = divID.find("#<%=OrderDetailInfo_txtBackFlightCode.ClientID %>").val();
   	                  RFlightCode = $.trim(RFlightCode);
   	                  if (RFlightCode == '') {
   	                      msg += '请填写回程航班号!\n';
   	                  }
   	              }
   	              
   	              //票号
   	              var oInputs = divID.find(".ticketnumber input");
   	              oInputs.each(function(){
   	                var n = $.trim($(this).val());
   	                if(n==""){
   	                    msg+="请填写旅客人员票号信息\n";
   	                    return false;
   	                }
   	              });
   	              if (msg != '') {
   	                  alert(msg);
   	                  return false;
   	              } else{
   	                  return true;
   	              }
   	          },
   	          SubmitCheckFee:function(){
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              var orderstate = '<%=OrderState %>';
   	              var frighttype = '<%=FreightType %>';
   	              var msg = '';
   	              
   	              var price = divID.find("#<%=OrderDetailInfo_txtFee.ClientID %>").val();
   	              price = $.trim(price);
   	              if(price==""){
   	                alert("请填写退票手续费");
   	                return false;
   	              }else{
   	                if(OrderDetailInfo.isDecimalTwo.test(price)==false){
   	                    alert('请填写正确的手续费!');
   	                      return false;
   	                }
   	              }
   	              
   	              return true;
   	          },
   	          SetPrice: function(o, type) {
   	                var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	                var reg = OrderDetailInfo.isDecimalTwo;
                    if (type == "1") {//去程
                        //cankao
                        var price = Number(divID.find("input[type=text][id$=<%=OrderDetailInfo_txtLeaveFacePrice.ClientID%>]").val());
                        if(!reg.test(price)){
                            alert("请填写正确的去程面价");
                            return;
                        }
                        //koulv
                        var rate = Number(divID.find("input[type=text][id$=<%=OrderDetailInfo_txtLeaveDiscount.ClientID%>]").val());
                        if(!/^[0-9]+([.]\d{0,1})?$/.test(rate)){
                            alert("请填写正确的去程参考扣率");
                            return;
                        }
                        if (rate > 100 || rate < 0 && rate != "") {
                            alert("去程参考扣率必须在0-100之间");
                            divID.find("input[type=text][id$=<%=OrderDetailInfo_txtLeaveDiscount.ClientID%>]").val("");
                            return;
                        }
                        //jisuan  jiesuan
                        var ratePrice = price * rate / 100;
                        if (ratePrice < 0.01) {
                            ratePrice = 0;
                        } else {
                            ratePrice = Number(ratePrice.toString().replace(/^(\d+\.\d{2})\d*$/, "$1 "));
                        }

                        var val = (Number((price - ratePrice))).toString().replace(/^(\d+\.\d{2})\d*$/, "$1");

                        if (Number(val) < 0.01) {
                            val = 0.01;
                        }
                        if (val.toString() == "NaN") {

                            val = "";
                        }
                        divID.find("input[type=text][id$=<%=OrderDetailInfo_txtLeavePrice.ClientID%>]").val(val);
                    }
                    else {//回程
                        var price = Number(divID.find("input[type=text][id$=<%=OrderDetailInfo_txtReturnFacePrice.ClientID%>]").val());
                        if(!reg.test(price)){
                            alert("请填写正确的回程面价");
                            return;
                        }
                        var rate = Number(divID.find("input[type=text][id$=<%=OrderDetailInfo_txtReturnDiscount.ClientID%>]").val());
                        if(!/^[0-9]+([.]\d{0,1})?$/.test(rate)){
                            alert("请填写正确的回程参考扣率");
                            return;
                        }
                        if (rate > 100 || rate < 0 && rate != "") {
                            alert("回程参考扣率必须在0-100之间");
                            divID.find("input[type=text][id$=<%=OrderDetailInfo_txtReturnDiscount.ClientID%>]").val("");
                            return;
                        }
                        
                        var ratePrice = price * rate / 100;
                        if (ratePrice < 0.01) {
                            ratePrice = 0;
                        } else {
                            ratePrice = Number(ratePrice.toString().replace(/^(\d+\.\d{2})\d*$/, "$1 "));
                        }

                        var val = (Number((price - ratePrice))).toString().replace(/^(\d+\.\d{2})\d*$/, "$1");

                        if (Number(val) < 0.01) {
                            val = 0.01;
                        }
                        if (val.toString() == "NaN") {
                            val = "";
                        }
                        divID.find("input[type=text][id$=<%=OrderDetailInfo_txtReturnPrice.ClientID%>]").val(val);
                    }
                    OrderDetailInfo.ChangeTotalPrice();
                },
   	          ChangeCheckPrice: function() {
   	              var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	              var orderstate = '<%=OrderState %>';
   	              //去程 参考价
   	              divID.find("#<%=OrderDetailInfo_txtLeaveFacePrice.ClientID %>").blur(function(){
   	                    OrderDetailInfo.SetPrice(this,'1');
   	              }).focus(function(){
   	                    this.select();
   	              });
   	              //去程 参考扣率
   	             divID.find("#<%=OrderDetailInfo_txtLeaveDiscount.ClientID %>").blur(function(){
   	                    OrderDetailInfo.SetPrice(this,'1');
   	              }).focus(function(){
   	                    this.select();
   	              });
                    //去程 燃油
   	              divID.find("#<%=OrderDetailInfo_txtLOtherPrice.ClientID %>").blur(function() {
   	                  var price = $.trim($(this).val());
                        if(OrderDetailInfo.isDecimalTwo.test(price)==false){
                            alert("请填写正确的去程燃油费!");
                            return;
                        }
                        OrderDetailInfo.ChangeTotalPrice();
   	              }).focus(function(){
   	                this.select();
   	              });
   	               //去程 机建
   	              divID.find("#<%=OrderDetailInfo_txtLOtherPrice2.ClientID %>").blur(function(){
   	                    var price = $.trim($(this).val());
   	                    if(OrderDetailInfo.isDecimalTwo.test(price)==false){
   	                        alert("请填写正确的去程机建费!");
   	                        return;
   	                    }
   	                    OrderDetailInfo.ChangeTotalPrice();
   	               }).focus(function(){
   	                this.select();
   	               });
   	               //回程 参考价格
                  divID.find("#<%=OrderDetailInfo_txtReturnFacePrice.ClientID %>").blur(function() {
   	                  OrderDetailInfo.SetPrice(this,'2');
   	              }).focus(function(){
   	                this.select();
   	              });
   	              //回程 参考扣率
   	              divID.find("#<%=OrderDetailInfo_txtReturnDiscount.ClientID %>").blur(function(){
   	                    OrderDetailInfo.SetPrice(this,'2');
   	              }).focus(function(){
   	                    this.select();
   	              });
                  //回程 燃油
                  divID.find("#<%=OrderDetailInfo_txtROtherPrice.ClientID %>").blur(function() {
   	                  var price = $.trim($(this).val());
                        if(OrderDetailInfo.isDecimalTwo.test(price)==false){
                            alert("请填写正确的回程燃油费!");
                            return;
                        }
                        OrderDetailInfo.ChangeTotalPrice();
   	              }).focus(function(){
   	                this.select();
   	              });
                  //回程 机建
                  divID.find("#<%=OrderDetailInfo_txtROtherPrice2.ClientID %>").blur(function() {
   	                  var price = $.trim($(this).val());
                        if(OrderDetailInfo.isDecimalTwo.test(price)==false){
                            alert("请填写正确的回程燃油费!");
                            return;
                        }
                        OrderDetailInfo.ChangeTotalPrice();
   	              }).focus(function(){
   	                this.select();
   	              });
   	              
   	              divID.find("#<%=OrderDetailInfo_txtFee.ClientID %>").blur(function() {
   	                  var pcount = <%=OrderChangeCount %>;
   	                  var price =  $.trim($(this).val());
   	                  if(price==""){
   	                    alert("退票手续费不能为空");
   	                    return false;
   	                  }
   	                  if (OrderDetailInfo.isDecimalTwo.test(price)==false) {
   	                      alert('请填写正确的手续费!');
   	                      return false;
   	                  }
   	                  //根据退票人数计算  退票费用总金额
   	                  var o = $("#"+divID).find("#<%=OrderDetailInfo_lblRefundprice.ClientID %>");
   	                  o.html(pcount*price);
   	              }).focus(function(){
   	                this.select();
   	              });
   	          },
   	          isDecimalTwo:/^[0-9]+([.]\d{1,2})?$/,
   	          formatBoxyContent:function(content){
   	            return "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+content+"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>";
   	          },
   	          ChangeTotalPrice:function(){
   	                var divID = topTab.GetActivePanel(topTab.activeTabIndex);
   	                var LeavePrice = divID.find("input[type=text][id$=<%=OrderDetailInfo_txtLeavePrice.ClientID%>]").val();
                    var ReturnPrice = divID.find("input[type=text][id$=<%=OrderDetailInfo_txtReturnPrice.ClientID%>]").val();
                    //去程燃油
   	                var LeaveOtherPrice = divID.find("#<%=OrderDetailInfo_txtLOtherPrice.ClientID %>").val();
   	                //去程机建
   	                var LeaveOtherPrice2 = divID.find("#<%=OrderDetailInfo_txtLOtherPrice2.ClientID %>").val();
                    //回程燃油
   	                var ReturnOtherPrice = divID.find("#<%=OrderDetailInfo_txtROtherPrice.ClientID %>").val();
   	                //回程机建
   	                var ReturnOtherPrice2 = divID.find("#<%=OrderDetailInfo_txtROtherPrice2.ClientID %>").val();
   	                var tmpPrice = (LeavePrice*100 + LeaveOtherPrice*100 + LeaveOtherPrice2*100) + (ReturnPrice*100 + ReturnOtherPrice*100 + ReturnOtherPrice2*100);
   	                var pCount = <%=PeopleCount %>,buyInsCount = <%=BuyInsCount %>,buyItineraryCount = <%=BuyItineraryCount %>;
   	                tmpPrice = tmpPrice * pCount;
   	                var itineraryPrice = <%=ItineraryPrice %>,emsPrice = <%=EMSPrice %>;
   	                tmpPrice = tmpPrice + (buyItineraryCount*1) * (itineraryPrice*100) + (buyInsCount*1) * 0;
   	                if(buyItineraryCount*1 > 0)
   	                {
   	                    tmpPrice += emsPrice*100;
   	                }
//   	                tmpPrice = tmpPrice.toString().replace(/^(\d+\.\d{2})\d*$/, "$1");
                    tmpPrice = tmpPrice/100;
   	                divID.find("span[id=<%=OrderDetailInfo_lblPayPrice.ClientID %>]").text(tmpPrice);
   	                divID.find("span[id=<%=OrderDetailInfo_lblPayPrice1.ClientID %>]").text(tmpPrice);
   	          }
   	      };
   	  
   	  $(document).ready(function(){
   	    //初始化 按钮事件
   	    OrderDetailInfo.Show_AND_Hide();
   	    //根据是否来回程，显示回程信息
   	    OrderDetailInfo.CheckFreightType();
   	    
   	    var orderstate = '<%=OrderState %>';
   	    var changetype = '<%=ChangeType %>';
   	    
   	    //初始化 价格等输入框参数有效性验证
        OrderDetailInfo.ChangeCheckPrice();
   	    topTab.GetActivePanel(topTab.activeTabIndex).find("#OrderDetailInfo_btnSubmit").click(function(){
   	        if(orderstate == '等待审核')
   	        {
   	            if(OrderDetailInfo.SubmitCheckPrice())
   	            {
   	                //show msg.
   	                var box = new Boxy(OrderDetailInfo.formatBoxyContent("正在进行审核..."),{title:"操作信息",modal:true});
   	                OrderDetailInfo.SubmitData(orderstate,box);
   	            }
   	        }else if(orderstate == '支付成功'){
   	            if(OrderDetailInfo.SubmitCheckFlight()){
   	                //show msg.
   	                var box = new Boxy(OrderDetailInfo.formatBoxyContent("正在处理出票..."),{title:"操作信息",modal:true});
   	                OrderDetailInfo.SubmitData(orderstate,box);
   	            }
   	        }else if(orderstate=='出票完成'){
   	            if(changetype=="改期"||changetype=="改签"){
   	                //show msg.
   	                var box = new Boxy(OrderDetailInfo.formatBoxyContent("正在处理"+changetype+"..."),{title:"操作信息",modal:true});
   	                OrderDetailInfo.SubmitData(orderstate,box);
   	            }else{
   	                var b = OrderDetailInfo.SubmitCheckFee();
   	                if(b==false){
   	                    return;
   	                }
   	                var box = new Boxy(OrderDetailInfo.formatBoxyContent("正在处理"+changetype+"..."),{title:"操作信息",modal:true});
   	                OrderDetailInfo.SubmitData(orderstate,box);
   	            }
   	        }else{
   	            OrderDetailInfo.SubmitData(orderstate);
   	        }
   	    });
   	  });
   	  </script>
</asp:content>
