<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayMoney.aspx.cs" Inherits="UserBackCenter.SMSCenter.PayMoney" %>

<asp:content id="PayMoney" runat="server" contentplaceholderid="ContentPlaceHolder1">
<style type="text/css">
    .STYLE1
    {
        font-size: 18px;
        font-weight: bold;
        color: #009900;
    }
</style>
      <table width="99%" border="0"  cellspacing="0" cellpadding="4" class="mobilebox" style="margin-bottom:10px; margin-top:10px; width:99%;">
        <tr>
          <td width="20%" align="right">客户名称：</td>
          <td width="80%" align="left"><input  id="PayMoney_txtCompanyName" runat="server"  disabled="disabled" name="PayMoney_txtCompanyName" type="text" size="30" style="border: 1px solid rgb(126, 157, 176);" /></td>
        </tr>
        <tr>
          <td align="right">充值人：</td>
          <td align="left"><input name="PayMoney_txtContactName" runat="server" type="text"  disabled="disabled" id="PayMoney_txtContactName" size="20" style="border: 1px solid rgb(126, 157, 176);" /></td>
        </tr>
        <tr>
          <td align="right">充值时间：</td>
          <td align="left"><input name="PayMoney_txtPayTime" runat="server" onfocus=" WdatePicker();" id="PayMoney_txtPayTime" type="text"  size="20" /></td>
        </tr>
        <tr>
          <td align="right">充值金额：</td>
          <td align="left"><input name="PayMoney_txtPayMoney" id="PayMoney_txtPayMoney"  maxlength="8" type="text" size="12" valid="required|isMoney" errmsg="请输入充值金额|请输入正确的充值金额" />
           <span id="errMsg_PayMoney_txtPayMoney" class="errmsg"></span>
            <span id="errMsg_PayMoneyMin" style="display:none" class="errmsg">充值金额必须大于0</span>
          </td>
          
        </tr>
        <tr>
          <td align="right">&nbsp;</td>
          <td align="left"><input type="button" name="PayMoney_btnPay" value="我要充值" id="PayMoney_btnPay" style="width:80px; height:30px;"/>
          <span id="span_PayMoneyError" style="display:none"></span>
          </td>
        </tr>
      </table>
      <table width="100%" border="0"  cellpadding="10" cellspacing="0"  style="margin-bottom:10px; margin-top:10px; width:99%;">
        <tr>
          <td valign="top" align="left"><strong>公司账户<br />
            杭州易诺科技有限公司</strong><br />
            <span class="STYLE1">383158327148</span><br />
            中国银行杭州市高新技术开发区支行
          </p>
            <p>汇款后我们将及时为您开通短信！<br />
            详情请致电客服何小姐  0571-56884627</p></td>
          <td align="left"><strong>个人账户<br />        
        <strong>1、朱永蕾： 农行银行杭州科技城支行卡号 </strong><br />
        <span class="STYLE1">622848&nbsp; 0322&nbsp; 1100&nbsp; 65115</span><br />
        <br />
        <strong>2、朱永蕾：中国建设银行杭州</strong><br />
        <span class="STYLE1">6222&nbsp; 8015&nbsp; 4111&nbsp; 1051&nbsp; 601</span></td>
        </tr>
      </table>
<script type="text/javascript">
    $(function() {
        FV_onBlur.initValid($("#PayMoney_btnPay").closest("form").get(0));
        $("#PayMoney_txtPayMoney").focus(function() {
            $("#errMsg_PayMoneyMin").hide();
        });
        $("#PayMoney_btnPay").click(function() {
            var isTrue = true;
            var form = $(this).closest("form").get(0);
            if (ValiDatorForm.validator(form, "span")) {
            } else {
                isTrue = false;
            }
            if (isTrue) {
                if (parseFloat($.trim($("#PayMoney_txtPayMoney").val())) == 0) {
                    isTrue = false;
                    $("#errMsg_PayMoneyMin").show();
                }
            }
            if (isTrue) {
                if (confirm("您确定要充值吗?")) {
                    $.newAjax({
                        type: "GET",
                        dataType: 'html',
                        url: "/SMSCenter/PayMoney.aspx",
                        data: { "isPay": 1, "PayMoney": $("#PayMoney_txtPayMoney").val(), "PayTime": $("#<%=PayMoney_txtPayTime.ClientID %>").val() },
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                alert("充值成功，请等待审核！");
                                $("#PayMoney_txtPayMoney").val("");
                            } else {
                                var resultTojson=eval('('+html+')')
                                if(resultTojson.isCheck!=null)
                                {
                                    alert("对不起，您的账户尚未审核不能执行该操作！");
                                }
                                else if(resultTojson.isLogin!=null)
                                {
                                    alert("对不起，您还没有登录不能执行该操作！");
                                }
                                else
                                {
                                    alert("对不起，充值失败！");
                                }
                                return false;
                            }
                        }
                    });
                }
            }
        });
    });
 </script>
</asp:content>
