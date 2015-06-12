<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SettlementAccount.aspx.cs"
    Inherits="UserBackCenter.HotelCenter.HotelOrderManage.SettlementAccount"  %>

<%@ Import Namespace="EyouSoft.Common" %>
<asp:content id="SettlementAccount" contentplaceholderid="ContentPlaceHolder1" runat="server">
   <table id="tb_SettlementAccount" width="100%" height="500" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
       <tr>
            <td align="left" valign="top">
                    <table width="815" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="35" align="left" class="pand" style="font-size: 14px;">
                                <strong><font color="#003C61">结算账户管理</font></strong>
                            </td>
                        </tr>
                    </table>
                    <table width="815" border="0" align="left" cellpadding="0" cellspacing="0" bgcolor="#eef7ff"
                        style="border: 1px solid #AACFE6;">
                        <tr>
                            <td width="629" align="right">
                                &nbsp;
                            </td>
                            <td width="184">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="629" height="28" align="left" valign="top">
                                <div>
                                    <table width="500" border="0" cellspacing="0">
                                        <tr>
                                            <td width="140" height="26" align="right">
                                                结算账户：
                                            </td>
                                            <td width="356" height="26">
                                              <span id="sla_IsShowDDL" runat="server"><asp:DropDownList ID="sla_clearanceaccount" runat="server"></asp:DropDownList></span>
                                              <asp:Label  ID="sla_Lblclearanceaccount" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div_1"  runat="server">
                                    <table width="500" border="0" cellspacing="0">
                                        <tr>
                                            <td width="140" height="26" align="right">
                                                结算方式：
                                            </td>
                                            <td width="356" height="26">
                                                <span id="sla_spclearancetype" runat="server">
                                                    <label><input type="radio" name="sla_clearancetype" id="sla_clearancetype" value="0" checked="checked"/></label>
                                                    <asp:Literal ID="sla_balancetype1"  runat="server"></asp:Literal>
                                                </span>
                                                <asp:Label ID="lab_Sla_clearancetype" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="26" align="right">
                                                开户姓名：
                                            </td>
                                            <td height="26">
                                                <input id="sla_accountname" name="sla_accountname" type="text"  runat="server" valid="required" errmsg="请填写开户姓名" isvalid="1" />
                                                <asp:Label ID="lab_sla_accountname" runat="server" style="display:none"></asp:Label>
                                                <span id="errMsg_<%=sla_accountname.ClientID %>" class="errmsg"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="26" align="right">
                                                开户行及支行：
                                            </td>
                                            <td height="26">
                                                <input id="sla_subbranch" name="sla_subbranch"  type="text"  runat="server" valid="required" errmsg="请填写开户行以及支行" isvalid="1" />
                                                <asp:Label ID="lab_sla_subbranch" runat="server" style="display:none"></asp:Label>
                                                <span id="errMsg_<%=sla_subbranch.ClientID %>" class="errmsg"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="26" align="right">
                                                银行卡号：
                                            </td>
                                            <td height="26">
                                                <input name="sla_cardnumber" type="text" id="sla_cardnumber" size="60"  runat="server" valid="required" errmsg="请填写银行卡号" isvalid="1"/>
                                                <asp:Label ID="lab_sla_cardnumber" runat="server" style="display:none"></asp:Label>
                                                <span id="errMsg_<%=sla_cardnumber.ClientID %>" class="errmsg"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="26" align="right">
                                                是否邮寄发票：
                                            </td>
                                            <td height="26">
                                               <span id="spn_SlaMailInvoice" runat="server"><label>
                                                    <input id="MailInvoice1" checked="checked" type="radio"  name="MailInvoice"  value="1"/></label>是
                                                <label>
                                                    <input type="radio" id="MailInvoice2" name="MailInvoice" value="0"/></label>否
                                               </span>
                                               <asp:Label ID="lbl_SlaMailInvoice" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <font color="#FF0000">备注：银行卡返佣规则：酒店所有银行卡转帐是在“杭州中国银行”柜台进行汇款，手续费根据中国银行规定，会在佣金里扣除。</font>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div_2" style="display:none;" runat="server">
                                    <table width="500" border="0" cellspacing="0">
                                        <tr>
                                            <td width="140" align="right">
                                                结算方式：
                                            </td>
                                            <td>
                                             <span id="span_sla_clearancetype1" runat="server"><label><input type="radio" id="sla_clearancetype1" name="sla_clearancetype1" value="0" checked="checked"/></label>                                                 
                                                   <asp:Literal ID="sla_balancetype2"  runat="server"></asp:Literal>                                                   
                                              <label><input type="radio" id="sla_clearancetype2" name="sla_clearancetype1"  value="1"/></label> 
                                                  <asp:Literal ID="sla_balancetype3"  runat="server"></asp:Literal> 
                                              </span> 
                                              <asp:Label ID="lbl_sla_clearancetype1" runat="server"></asp:Label>                                                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                财付通账号：
                                            </td>
                                            <td>
                                                <input name="sla_tenpaycard" type="text" id="sla_tenpaycard" size="60" runat="server" valid="required" errmsg="请填写财付通账号" isvalid="2"/>
                                                <asp:Label ID="lab_sla_tenpaycard" runat="server" style="display:none"></asp:Label>
                                                <span id="errMsg_<%=sla_tenpaycard.ClientID %>" class="errmsg"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                是否邮寄发票：
                                            </td>
                                            <td>
                                              <span id="span_slaMailInvoice1" runat="server"><label>
                                                    <input type="radio" id="MailInvoice3" name="MailInvoice1" checked="checked" value="1"/></label>是
                                                <label>
                                                    <input type="radio" id="MailInvoice4" name="MailInvoice1"  value="0"/></label>否
                                              </span>
                                              <asp:Label ID="lbl_slaMailInvoice1" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <font color="#FF0000">备注：通过财付通帐户返佣无手续费</font>
                                            </td>
                                        </tr>                                       
                                    </table>
                                </div>
                                <div style="width:100%; height:48px;" id="div_Img" runat="server">
                                     <a href="javascript:void(0);">
                                       <img id="btnSubmit" onclick="SettlementAccount.OnSave();" src="<%=ImageServerUrl %>/images/hotel/userBackCenter/baocun_btn.jpg" width="79" height="25" border="0"  style="margin-left:100px" alt="保存" /></a>                                       
                                     <a href="javascript:void(0);" id="Sla_btnReset">
                                       <img src="<%=ImageServerUrl %>/images/hotel/userBackCenter/admin_orderform_ybans_05.jpg" width="79" height="25"  alt="重置"/></a>
                                </div>
                            </td>
                            <td height="28" valign="top">
                                <strong>备注：</strong><br />
                               1、 请仔细核对以上信息，为了保证顺利结算，一但保存将不允许修改。<br /> 
                               2、 如果您不寄全国统一的服务业发票给同业114，我们将从您的佣金中扣除10%发票税.<br />
                               3、 银行帐号当前只支持“一月一结”的结算方式。<br />
                            </td>
                     </tr>
                  </table>
            </td>
        </tr>
   </table>
          <div style="clear:both">&nbsp:</div>
          <script type="text/javascript">
              var SettlementAccount = {
                  init: function() {
                      //验证初始化
                      FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
                      $("#ctl00_ContentPlaceHolder1_sla_clearanceaccount").bind("change", function() {  //结算账户下拉框绑定事件                          
                          if ($.trim($(this).val()) == "0") {   //获取下拉框选中的值
                              $("#ctl00_ContentPlaceHolder1_div_1").show();    //显示银行卡用户填写信息
                              $("#ctl00_ContentPlaceHolder1_div_2").hide();   //隐藏中间账户财付通用户填写信息
                          }
                          else {
                              $("#ctl00_ContentPlaceHolder1_div_1").hide();   //隐藏银行卡用户填写信息
                              $("#ctl00_ContentPlaceHolder1_div_2").show();   //显示中间账户财付通用户填写信息
                          }
                      });
                  },
                  OnSave: function() {  //保存
                      var AccountType = $("#ctl00_ContentPlaceHolder1_sla_clearanceaccount").val(); //获取结算账户类型值
                      var attribute_No_Key = "isvalid", attribute_No_Value;  //匹配元素是否有attribute_No_Key 并且值等于attribute_No_Value的元素不进行验证                      
                      if (AccountType == 0) {           //银行卡结账
                          attribute_No_Value = "2";    //不验证财付通账号
                      }
                      else {                          //财付通结账
                          attribute_No_Value = "1";  //不验证银行卡信息
                      }

                      var validatorshow = ValiDatorForm.validator2($("#btnSubmit").closest("form").get(0), "span", attribute_No_Key, attribute_No_Value, true);    //获取提示信息
                      if (validatorshow == false) {   //验证不通过
                          return;
                      }
                      
                      if (!confirm("请仔细核对以上信息,一但保存将不允许修改.是否继续?")) {  
                          return false;
                      }

                      var box = new Boxy("<p>正在进行保存...</p>", { title: "操作信息", modal: true });

                      $.newAjax({
                          type: "POST",
                          url: "/HotelCenter/HotelOrderManage/SettlementAccount.aspx?issave=1", //保存提交的信息
                          cache: false,
                          data: $("#btnSubmit").closest("form").serialize(), //获取表单数据
                          dataType: 'json',
                          success: function(msg) {
                              alert(msg.errMsg);
                              box.hide(); //关闭提示信息框
                              topTab.url(topTab.activeTabIndex, "/hotelcenter/hotelordermanage/SettlementAccount.aspx");
                          },
                          error: function() {
                              alert("服务器繁忙!请稍候再进行此操作!");
                              box.hide(); //关闭提示信息框
                              return false;
                          }
                      });
                  }
              }

              $(function() {
                  SettlementAccount.init();
                  //表单重置
                  $("#Sla_btnReset").click(function() {
                      var myForm = $(this).closest("form").get(0);
                      myForm.reset();
                      return false;
                  });
              });
          </script>
</asp:content>
