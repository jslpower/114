<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMsgManage.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.OrderMsgManage" EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单短信管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="search_bg">
                <font class="fontblue">订单类型：线路散客短信</font>
            </td>
        </tr>
    </table>
    <table width="98%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="15%" align="right" bgcolor="#f2f9fe">
                发送供应商类型：
            </td>
            <td colspan="2" align="left" bgcolor="#FFFFFF">
                <%=SendBusinessType%>
                <span id="errMsg_BusinessType" class="unnamed1"></span>
                <input type="hidden" id="hid_BusinessType" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                发送内容：
            </td>
            <td width="38%" align="left" bgcolor="#FFFFFF">
                <textarea name="SendContent" cols="50" rows="6" runat="server" id="SendContent" valid="required"
                    errmsg="请输入发送内容！"></textarea>
                <span id="errMsg_SendContent" class="errmsg"></span>
            </td>
            <td width="47%" align="left" valign="middle" bgcolor="#FFFFFF">
                <a href="#" onclick="OrderManage.SetValue(this)">[预订公司]</a><br />
                <a href="#" onclick="OrderManage.SetValue(this)">[预订联系电话]</a><br />
                <a href="#" onclick="OrderManage.SetValue(this)">[预订出发时间]</a><br />
                <a href="#" onclick="OrderManage.SetValue(this)">[预订产品]</a><br />
                <a href="#" onclick="OrderManage.SetValue(this)">[预订数量]</a>
            </td>
        </tr>
        <tr>
            <td width="16%" align="right">
                <span class="unnamed1">*</span>公司编号：
            </td>
            <td align="left" bgcolor="#FFFFFF" colspan="2">
                <input type="text" name="txt_CompanyId" id="txt_CompanyId" runat="server" valid="required"
                    errmsg="请输入公司编号！" style="width: 200px" />
                <span id="errMsg_txt_CompanyId" class="errmsg"></span>
                <input id="GetContactMoney" runat="server" type="button" value="获取联系人及用户余额" onclick="OrderManage.GetContactMoney();"
                    style="width: 140px" />
                <span class="unnamed1">*</span>联系人：
                <asp:DropDownList ID="CompanyContact" runat="server" Style="width: 100px">
                </asp:DropDownList>
                <span id="errMsg_CompanyContact" class="errmsg"></span><span>账户余额：</span><asp:Label
                    ID="CompanyMoney" runat="server"></asp:Label>
                <input id="hid_Operator" type="hidden" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                选择发送通道：
            </td>
            <td align="left" bgcolor="#FFFFFF" colspan="2">
                <asp:DropDownList runat="server" ID="ddlSendChannel">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" bgcolor="#f2f9fe">
                &nbsp;
            </td>
            <td colspan="3" align="left" bgcolor="#FFFFFF">
                <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
        </tr>
    </table>
    </form>

    <script type="text/javascript">
        var OrderManage = {
            GetContactMoney: function() {
                var companyId = $("#<%=txt_CompanyId.ClientID %>").val();
                if (companyId == "") {
                    $("#errMsg_txt_CompanyId").html("请输入公司编号！");
                }
                else {
                    $("#errMsg_txt_CompanyId").html("");
                    $("#<%=CompanyContact.ClientID %>").append("<option value='0'>正在加载...</option>");
                    $.ajax({
                        url: "OrderMsgManage.aspx?type=GetContactMoney&CompanyId=" + companyId,
                        cache: false,
                        type: "post",
                        success: function(result) {
                            //alert(result);
                            if (result == "error") {
                                $("#errMsg_txt_CompanyId").html("没有该公司编号的成员，请检查公司编号是否错误");
                            }
                            else {
                                var argumentlist = result.toString().split('$');
                                $("#hid_Operator").val(argumentlist[1]);
                                $("#<%=CompanyMoney.ClientID %>").html(argumentlist[2] + "元");
                                var list = eval(argumentlist[0]); 
                                $("#<%=CompanyContact.ClientID %>").html("");
                                for (var i = 0; i < list.length; i++) {
                                    $("#<%=CompanyContact.ClientID %>").append("<option value='" + list[i].UserId + "'>" + list[i].UserName + "</option>");
                                }
                            }
                        },
                        error: function() {
                            alert("操作失败");
                        }
                    })
                }
            },
            SetValue: function(obj) {
                var sendcontent = $("#<%=SendContent.ClientID %>").val();
                if (sendcontent != "") {
                    $("#<%=SendContent.ClientID %>").val(sendcontent + "\n" + $(obj).html());
                }
                else {
                    $("#<%=SendContent.ClientID %>").val($(obj).html());
                }
            }
        };
        $(function() {
            if ($.trim($("#<%=hid_BusinessType.ClientID %>").val()).length > 0) {
                var arr = $("#<%=hid_BusinessType.ClientID %>").val().split(',');
                var i = arr.length;
                while (i-- > 0) {
                    $("input[value='" + arr[i] + "']").attr("checked", "checked");
                }
            }
            OrderManage.GetContactMoney();
        })
        $(function() {
            $("#<%=btnSubmit.ClientID %>").click(function() {
                if ($("#errMsg_txt_CompanyId").html() != "") {
                    return false;
                }
                if ($("#<%=CompanyContact.ClientID %>").val() == null) {
                    $("#errMsg_CompanyContact").html("请获取联系人");
                    return false;
                }
                else {
                    $("#errMsg_CompanyContact").html("");
                }
                var form = $(this).closest("form").get(0);
                return ValiDatorForm.validator(form, "span");
            });
            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
            FV_onBlur.initValid($("#<%=btnSubmit.ClientID %>").closest("form").get(0));
        })
    </script>

</body>
</html>
