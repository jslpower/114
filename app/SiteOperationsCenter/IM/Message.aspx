<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="SiteOperationsCenter.IM.Message" %>

<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register src="../usercontrol/ProvinceAndCityList.ascx" tagname="ProvinceAndCityList" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>给客户发送MQ消息</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("json2.js") %>"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#radSendType1").click(function() { $("#txtSendMQId").hide(); });
            $("#radSendType2").click(function() { $("#txtSendMQId").show(); });
            $("#radAcceptCompanyType1").click(function() { $("#spanCompanyTypeArea").hide(); });
            $("#radAcceptCompanyType2").click(function() { $("#spanCompanyTypeArea").show(); });

            $("#btnSubmit").click(function() { btnSubmitClick(); });
        });

        function btnSubmitClick() {
            var params = { sendType: '', sendMQId: '', provinceId: '', cityId: '', companyType: '', companyTypes: [], onlineState: '', message: '', type: "1" };
            params.sendType = $("input[name='radSendType']:checked").val();
            params.sendMQId = $.trim($("#txtSendMQId").val());
            params.provinceId = $("#" + provinceAndCityUserControl["<%=this.ProvinceAndCityList1.ClientID %>"].provinceId).val();
            params.cityId = $("#" + provinceAndCityUserControl["<%=this.ProvinceAndCityList1.ClientID %>"].cityId).val();
            params.companyType = $("input[name='radAcceptCompanyType']:checked").val();
            params.onlineState = $("input[name='radOnlineState']:checked").val();
            params.message = $.trim($("#txtMessage").val());
            $("input[name='chkCompanyType']:checked").each(function() { params.companyTypes.push(this.value); });

            if (params.sendType == "1") { params.sendMQId = 0; }
            if (params.sendType == "2" && (params.sendMQId == "" || params.sendMQId < 1 || isNaN(params.sendMQId))) { alert("请输入指定的用户MQID"); return; }
            if (params.companyType == "2" && params.companyTypes.length == 0) {alert("请选择指定的客户类型");return;};
            if (params.message == "") { alert("请输入消息内容"); return; }

            var confirmMessage = "";

            if (params.provinceId == "0") {
                confirmMessage = "【注意:】当前未选择任何省份、城市，消息将会发送到所有省份、城市，请仔细确认！\n\n";
            } else if (params.cityId == "0") {
                confirmMessage = "【注意:】当前未选择任何城市，消息将会发送到当前省份的所有城市，请仔细确认！\n\n";
            }

            confirmMessage += "你确定要发送消息吗？";

            if (!confirm(confirmMessage)) { return; }
            
            $("#btnSubmit").attr("disabled", "disabled");
            $("#spanSendState").show();
            
            $.ajax({
                type: "POST",
                url: "message.aspx?isajax=yes",
                data: params,
                //dataType:"json",
                success: function(responseText) {
                    if (responseText == "notLogin") {
                        alert("对不起，当前未登录，请先登录!");
                        return;
                    }

                    var data = JSON.parse(responseText);
                    if (data.isSuccess) {
                        alert("发送成功，成功发送记录" + data.successCount + "条");
                        window.location.href = window.location.href;
                        return;
                    }

                    if (!data.isSuccess) {
                        alert(data.errorMsg);
                        $("#btnSubmit").removeAttr("disabled");
                        $("#spanSendState").hide();
                        return;
                    }
                }
            }); 
            
            return;
        }
    </script>
    
    <style type="text/css">
    .note{margin-left:10px; color:#999999}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="1" cellspacing="0" cellpadding="6">
        <tr>
            <td align="right" style="width:15%">
                <strong>发&nbsp;送&nbsp;人：</strong>
            </td>
            <td align="left">
                <input type="radio" name="radSendType" value="1" id="radSendType1" checked="checked" /><label for="radSendType1">系统</label>
                <input type="radio" name="radSendType" value="2" id="radSendType2" /><label for="radSendType2">指定的用户MQID</label>
                <input id="txtSendMQId" type="text" name="txtSendMQId" style="display:none;width:50px" maxlength="10" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <strong>发送城市：</strong>
            </td>
            <td align="left">
                <uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                <span class="note">注：不选择任何省份城市时为全国</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                <strong>公司类型：</strong>
            </td>
            <td align="left">
                <input type="radio" name="radAcceptCompanyType" value="1" id="radAcceptCompanyType1" checked="checked" /><label for="radAcceptCompanyType1">全部</label>
                <input type="radio" name="radAcceptCompanyType" value="2" id="radAcceptCompanyType2" /><label for="radAcceptCompanyType2">指定类型</label>
                
                <span id="spanCompanyTypeArea" style="display:none; margin-left:20px;">
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType1" value="1" /><label for="chkCompanyType1">专线</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType2" value="2"/><label for="chkCompanyType2">组团</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType3" value="3"/><label for="chkCompanyType3">地接</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType4" value="4"/><label for="chkCompanyType4">景区</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType5" value="5"/><label for="chkCompanyType5">酒店</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType6" value="6"/><label for="chkCompanyType6">车队</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType7" value="7"/><label for="chkCompanyType7">旅游用品</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType8" value="8"/><label for="chkCompanyType8">购物店</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType9" value="9"/><label for="chkCompanyType9">机票</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType10" value="10"/><label for="chkCompanyType10">其他采购商</label>
                <input type="checkbox" name="chkCompanyType" id="chkCompanyType11" value="-1"/><label for="chkCompanyType11">随便逛逛</label>
                </span>
                
            </td>
        </tr>
        <tr>
            <td align="right">
                <strong>发送对象：</strong>
            </td>
            <td align="left">
                <input type="radio" name="radOnlineState" value="0" id="radOnlineState1" checked="checked" /><label for="radOnlineState1">所有用户</label>
                <input type="radio" name="radOnlineState" value="1" id="radOnlineState2" /><label for="radOnlineState2">在线用户</label>
                <input type="radio" name="radOnlineState" value="2" id="radOnlineState3" /><label for="radOnlineState3">所有登陆过的用户</label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <strong>发送内容：</strong>
            </td>
            <td align="left">
                <textarea name="textarea" id="txtMessage" style="width: 550px; height: 150px;" rows="10" cols="10"></textarea>
                <span class="note">注：消息内容不能超过200个汉字</span>
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 24px">
                &nbsp;
            </td>
            <td>
                <input type="button" id="btnSubmit" value=" 发 送 " />
                
                <span style="display:none; color:#ff0000" id="spanSendState">正在发消息，请稍候....</span>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
