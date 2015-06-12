<%@ Page Title="团队票申请_单程_往返_联程_缺程_同业114团队票频道" Language="C#" MasterPageFile="~/MasterPage/NewPublicCenter.Master" AutoEventWireup="true" CodeBehind="TicketApply.aspx.cs" Inherits="UserPublicCenter.AirTickets.TicketApply" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<meta name="keywords" content="团队票，机票团队票，国航团队票，普通团队票" />
<meta name="description" content="同业114申请团队票，单程、往返、联程 、缺程申请，团队需10人以上，方便，快捷，申请团队时 如果没有乘客名单" />
<style type="text/css">
 table,tr,tr td
 {
      border-collapse:collapse;
       border:1px solid #d0ccca;
  }
  .errmsg{ color:Red;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="c1" runat="server">
<!--head end-->
    <div class="hr_10"></div>
    <!--团队票申请开始-->
    <div class="mainbox fixed">
        <div class="ticketreg-sidebar">
        	<img src="<%=ImageServerPath %>/images/new2011/index/ticketsreg_03.jpg" style=" vertical-align:top;">
            <div class="hr_10"></div>
            <div class="tickets-contact">
            	<h3>联系我们</h3>
                <p>公司地址：浙江省杭州市塘苗路18号华星现代产业园B座4楼<br />
电话：0571-56893746<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 0571-56893761<br />传真：0571-56893768<br />联系人：黄先生<br />QQ:34737111 <a href="<%=Utils.GetQQUrl("34737111") %>"><img src="<%=ImageServerPath %>/images/new2011/index/ticketsreg_05.jpg" /></a><br />MQ:<%=Utils.GetMQ("34737111") %></p>
            </div>
        </div>
      <div class="ticketreg-sidebar02">
        	<span class="regbutton">团队票申请</span>
            <div class="formtitle">
           	  <h4>单程、往返、联程 、缺程申请表</h4>
                <em>带*代表必填项</em>
            </div>           <form id="ticketForm" name="ticketForm"  runat="server"><input type="hidden" name="hidSave" value="save" />
    		<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#d0ccca">              
              <tr>                
                <td height="28" colspan="3" bgcolor="#FFFFFF" style="padding-left:20px;"><input name="rdiType" type="radio"  value="1" checked="checked" onclick="setMess(1);" />
                单程 
                <input type="radio" name="rdiType" value="2" onclick="setMess(2);" /> 往返 <input type="radio" name="rdiType" value="3" onclick="setMess(3);" /> 联程 <input type="radio" name="rdiType" value="4"  onclick="setMess(4);" /> 缺程</td>
              </tr>
              <tr>
                <td width="15%" height="28" align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">*</font> 城市从：</td>
                <td width="52%" bgcolor="#FFFFFF">&nbsp;<input type="text" name="txtStartCity" id="txtStartCity" maxlength="20" valid="required"  errmsg="请填写城市"/> 到 <input name="txtEndCity" maxlength="20" type="text" id="txtEndCity" valid="required"  errmsg="请填写城市" /><span id="errMsg_txtEndCity" class="errmsg"></span></td>
                <td width="33%" align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">联系人:</font> <input type="text" name="txtContacter" id="txtContacter" maxlength="20" /></td>
              </tr> 
              <tr>
        <td height="28" align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">*</font> 乘机人数：</td>
                <td bgcolor="#FFFFFF">&nbsp;<input type="text" name="txtNum" id="txtNum" maxlength="8"  valid="required|isNumber"  max="1000" min="10" errmsg="请填写乘机人数|请输入合适数字" />（团队必需10人以上）<span id="errMsg_txtNum" class="errmsg"></span></td>
                <td align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">联系QQ:</font> <input type="text" name="txtQQ" id="txtQQ" maxlength="45" /></td>
              </tr>
              <tr>
                <td height="28" align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">*</font> 联系电话：</td>
                <td bgcolor="#FFFFFF">&nbsp;<input type="text" name="txtTel" id="txtTel" maxlength="20"  valid="required"  errmsg="请填写联系电话"/><span id="errMsg_txtTel" class="errmsg"></span></td>
                <td align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">希望价格:</font> <input type="text" name="txtPrice" id="txtPrice" maxlength="20" /></td>
              </tr>
              <tr>
                <td height="28" align="right" bgcolor="#FFFFFF" class="regformth">航空公司：</td>
                <td bgcolor="#FFFFFF">&nbsp;<select name="selAirCompany" id="selAirCompany">                  <option value="0">选择航空公司</option>                  <option value="1">CA-国际航空</option>                  <option value="2">东方航空</option>
                </select>                </td>
                <td align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">邮件地址:</font> <input type="text" name="txtEmail" id="txtEmail"  maxlength="50"/></td>
              </tr>
              <tr>
                <td height="28" align="right" bgcolor="#FFFFFF" class="regformth">去程航班号：</td>
                <td bgcolor="#FFFFFF">&nbsp;<input type="text" name="txtNo" id="txtNo"  maxlength="50"/> 航班号不填表示不际航班</td>
                <td align="right" bgcolor="#FFFFFF">&nbsp;</td>
              </tr>
              <tr>
                <td height="28" align="right" bgcolor="#FFFFFF" class="regformth"><font color="#FF0000">*</font> 出发时间：</td>
                <td bgcolor="#FFFFFF">&nbsp;<input type="text" name="txtStartDate" id="txtStartDate" onfocus="WdatePicker();" valid="required"  errmsg="请填写出发时间" /> 时间范围：                <select name="selSTime" id="selSTime">
                   
                  <option value="06：00">06：00</option>
                  <option value="07：00">07：00</option>
                  <option value="08：00">08：00</option>
                  <option value="09：00">09：00</option>
                  <option value="09：00">10：00</option>
                  <option value="11：00">11：00</option>
                  <option value="12：00" selected="selected">12：00</option>
                  <option value="13：00">13：00</option>
                  <option value="14：00">14：00</option>
                  <option value="15：00">15：00</option>
                  <option value="16：00">16：00</option>
                  <option value="17：00">17：00</option>
                  <option value="18：00">18：00</option>
                  <option value="19：00">19：00</option>
                  <option value="20：00">20：00</option>
                  <option value="21：00">21：00</option>
                  <option value="22：00">22：00</option>
                  <option value="23：00">23：00</option>
                  <option value="23：59">23：59</option>      </select> 至                 <select name="selETime" id="selETime">                 <option value="06：00">06：00</option>
                  <option value="07：00">07：00</option>
                  <option value="08：00">08：00</option>
                  <option value="09：00">09：00</option>
                  <option value="09：00">10：00</option>
                  <option value="11：00">11：00</option>
                  <option value="12：00" selected="selected">12：00</option>
                  <option value="13：00">13：00</option>
                  <option value="14：00">14：00</option>
                  <option value="15：00">15：00</option>
                  <option value="16：00">16：00</option>
                  <option value="17：00">17：00</option>
                  <option value="18：00">18：00</option>
                  <option value="19：00">19：00</option>
                  <option value="20：00">20：00</option>
                  <option value="21：00">21：00</option>
                  <option value="22：00">22：00</option>
                  <option value="23：00">23：00</option>
                  <option value="23：59">23：59</option>                  <option value="次日01:00">次日01:00</option>
                  <option value="次日02:00">次日02:00</option>
                  <option value="次日03:00">次日03:00</option>
                  <option value="次日04:00">次日04:00</option>
                  <option value="次日05:00">次日05:00</option>                                </select>      <span id="errMsg_txtStartDate" class="errmsg"></span>          </td>
                <td align="right" bgcolor="#FFFFFF">&nbsp;</td>
              </tr>
              <tr>
                <td align="right" bgcolor="#FFFFFF" class="regformth">备注：</td>
                <td bgcolor="#FFFFFF">&nbsp;<textarea name="txtReamrk" id="txtReamrk" cols="45" rows="5"></textarea></td>
                <td align="right" bgcolor="#FFFFFF">&nbsp;</td>
              </tr>
              <tr>
                <td colspan="3" align="left" bgcolor="#FFFFFF" style="padding:10px 0 10px 20px;"><h5 class="warmT">温馨提醒：</h5>1.申请团队时如果没有乘客名单，请拨打 0571-56893746 或联系在线客服<br />2.团队周一到周日均可申请； <br />3.提交团队申请时请将乘客信息填写完整； <br />4.如有其他要求请在“备注”栏内写明。</td>
              </tr>
        </table>
        <div class="addbtn"><a  id="addPassengers" style="cursor:pointer"><span>添加乘客信息</span></a> <a href="javascript:;"  style="cursor:pointer" id="addbtn" onclick="return Save();"><span>提交</span></a></div>
        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#d0ccca" id="PassengersInfo" style=" display:none;" id="tGuest">
          <tr bgcolor="#EFF5F9">
            <th width="18%" height="25" align="center">姓名</th>
            <th width="18%" align="center">乘客类型</th>
            <th width="20%" align="center">证件类型</th>
            <th width="24%" align="center">证件号码</th>
            <th width="20%">手机</th>
          </tr>                      <tr id="trSave">
            <td height="25" colspan="5" align="center"><div class="addbtn"><a href="javascript:;" style="cursor:pointer" onclick="return Save();"><span>提交</span></a></div></td>
          </tr>
        </table>          </form>
      </div>
    </div>
    <!--团队票申请结束-->
     <div class="hr_10"></div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="c2" runat="server">
<script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
<script type="text/javascript">
    var oldNum = 0;//原来人数
    $(function() {
        $("#addPassengers").click(function() {
            if ($.trim($("#txtNum").val()) == "") {
                alert("请填写乘机人数！");
                return false;
            }
            $("#addbtn").hide();
            $("#PassengersInfo").show();
            ChangeGuest();
        });
        FV_onBlur.initValid($("#<%=ticketForm.ClientID %>").get(0), null, false);
    });
    function setMess(v) {
       if(v==3||v==4)
           $("#txtReamrk").val("请详细填写联城缺程路经城市!");
        else
            $("#txtReamrk").val("");
    }
    //改变人数
    function ChangeGuest() {
        var nowNum = parseInt($.trim($("#txtNum").val()));
        var addNum = Math.abs(nowNum - oldNum);
        var sTable = $("#trSave");
        if (nowNum > oldNum) {
            var arr = [];
            for (var i = 0; i < addNum; i++) {
                arr.push("<tr><td height='25' align='center'><input name='txtName' type='text'  size='15' /></td><td align='center'><select name='selpType'><option value='1'>成人</option><option value='2'>儿童</option></select></td><td align='center'><select name='selcType' ><option value='1'>身份证</option><option value='2'>学生证</option><option value='3'>军人证</option><option value='4'>残疾证</option></select></td><td align='center'><input name='txtcNo' type='text'  size='25' /></td><td align='center'><input name='txtMobile' type='text'   size='15' /></td> </tr>");
            }
            sTable.before(arr.join(""));
        }
        else {
            var sTable = $("#trSave");
            for (var i = 0; i < addNum; i++) {
                sTable.prev("tr").remove();
            }
        }
        oldNum = nowNum;
    }
    //提交
    function Save() {
        var form = $("#<%=ticketForm.ClientID %> ").get(0);
         if (ValiDatorForm.validator(form, "span", null, false)) {
             form.submit();
         }
         return false;
         
    }
</script>
</asp:Content>
