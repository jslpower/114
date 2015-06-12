<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditScenicTicket.aspx.cs"
    Inherits="SiteOperationsCenter.ScenicManage.EditScenicTicket" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>景区管理-门票编辑</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="80%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="19%" align="right">
                所属景区：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="ddlSight">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>门票类型名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" type="text" name="txtName" id="txtName" valid="required" errmsg="请填写门票类型名称"
                    maxlength="20" />
                <span id="errMsg_txtName" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                英文名称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" type="text" id="txtEnName" maxlength="100" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>门市价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtMSPrice" id="txtMSPrice" type="text" size="10" valid="required|isNumber"
                    errmsg="请填写门市价|门市价只能为数字" />
                <input type="hidden" id="hidMSPrice" runat="server" />
                元 <span id="errMsg_txtMSPrice" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                网站优惠价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtWZPrice" id="txtWZPrice" type="text" size="10" valid="isNumber"
                    errmsg="网站优惠价只能为数字" />
                <input type="hidden" id="hidWZPrice" runat="server" />
                元 <span id="errMsg_txtWZPrice" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                市场限制最低价：
            </td>
            <td width="81%" align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtSCPrice" id="txtSCPrice" type="text" size="10" valid="isNumber"
                    errmsg="市场限制最低价只能为数字" />
                <input type="hidden" id="hidSCPrice" runat="server" />
                元 <span id="errMsg_txtSCPrice" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                同行分销价：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtTHPrice" id="txtTHPrice" type="text" size="10" valid="isNumber"
                    errmsg="同行分销价只能为数字" />
                <input type="hidden" id="hidTHPrice" runat="server" />
                元 <span id="errMsg_txtTHPrice" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                最少限制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtLimit" id="txtLimit" type="text" size="10" valid="isInt"
                    errmsg="最少限制只能为数字" />
                （张/套） <span id="errMsg_txtLimit" class="unnamed1"></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>支付方式：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:RadioButtonList runat="server" ID="rblPayment" RepeatDirection="Horizontal">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right">
                票价有效时间段：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtStartDate" onfocus="WdatePicker()" id="txtStartDate"
                    type="text" style="width: 70px" />
                至
                <input runat="server" name="txtEndDate" onfocus="WdatePicker()" id="txtEndDate" type="text"
                    style="width: 70px" />
                （不写为无时间限制）
            </td>
        </tr>
        <tr>
            <td align="right">
                门票说明：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea runat="server" name="txtDescription" id="txtDescription" cols="50" rows="3"></textarea>
            </td>
        </tr>
        <tr>
            <td align="right">
                同业销售须知：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea runat="server" name="txtSaleDescription" id="txtSaleDescription" cols="50"
                    rows="3"></textarea>
                （只有同业分销商能看到）
            </td>
        </tr>
        <tr>
            <td align="right">
                状态：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="ddlIsCheck">
                </asp:DropDownList>
                <asp:DropDownList runat="server" ID="ddlState">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                门票类型排序：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input runat="server" name="txtSort" type="text" id="txtSort" size="10" valid="isInt"
                    errmsg="门票类型排序只能为数字" value="9" />
                <span id="errMsg_txtSort" class="unnamed1"></span>按照1，2，3顺序排列，前3位在列表时显示更突出
            </td>
        </tr>
        <tr>
            <td align="right">
                B2B显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="ddlB2B">
                </asp:DropDownList>
                <input runat="server" type="text" id="txtB2BSort" name="txtB2BSort" maxlength="2"
                    valid="isPIntegers" errmsg="B2B显示排序只能为数字" value="50" />
                <span id="errMsg_txtB2BSort" class="unnamed1"></span>（1~99）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td align="right">
                B2C显示控制：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="ddlB2C">
                </asp:DropDownList>
                <input runat="server" type="text" id="txtB2CSort" name="txtB2CSort" maxlength="2"
                    valid="isPIntegers" errmsg="B2C显示排序只能为数字" value="50" />
                <span id="errMsg_txtB2CSort" class="unnamed1"></span>（1~99）正向排序，默认50（易诺管理员控制）
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" bgcolor="#FFFFFF">
                <asp:Button runat="server" ID="btnSave" Text=" 保 存 " Height="27px" OnClick="SaveDate" />
                <input type="button" value="返 回" style="height: 27px" onclick="javascript:window.location.href = '/ScenicManage/ScenicTicket.aspx'" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">
        function CheckFormData() {
            $("#<%= btnSave.ClientID %>").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "span")) {
                    var msp = $("#<%= txtMSPrice.ClientID %>").val();  //门市价
                    var yhp = $("#<%= txtWZPrice.ClientID %>").val();   //优惠价
                    var zdp = $("#<%= txtSCPrice.ClientID %>").val();   //最低价
                    var thp = $("#<%= txtTHPrice.ClientID %>").val();   //同行价
                    var oldmsp = $("#<%= hidMSPrice.ClientID %>").val();  //修改前门市价
                    var oldyhp = $("#<%= hidWZPrice.ClientID %>").val();   //修改前优惠价
                    var oldzdp = $("#<%= hidSCPrice.ClientID %>").val();   //修改前最低价
                    var oldthp = $("#<%= hidTHPrice.ClientID %>").val();   //修改前同行价
                    if (msp == "") msp = 0;
                    if (yhp == "") yhp = 0;
                    if (zdp == "") zdp = 0;
                    if (thp == "") thp = 0;
                    if (oldmsp == "") oldmsp = 0;
                    if (oldyhp == "") oldyhp = 0;
                    if (oldzdp == "") oldzdp = 0;
                    if (oldthp == "") oldthp = 0;
                    //同业价 < 最低限价 < 优惠价 < 门市价
                    if (parseFloat(thp) <= parseFloat(zdp) && parseFloat(zdp) <= parseFloat(yhp) && parseFloat(yhp) <= parseFloat(msp)) {
                        //修改门票价格后给出提示
                        if ("<%= IsEdit %>" == "1" && (msp != oldmsp || yhp != oldyhp || zdp != oldzdp || thp != oldthp)) {
                            return confirm("由于门票价格的修改导致要重新审核门票\n确定要提交更改吗？");
                        }
                        return true;
                    }
                    else {
                        alert("请确保\n门市价大于等于优惠价\n优惠价大于等于最低限价\n最低限价大于等于同业价");
                        $("#<%= txtMSPrice.ClientID %>").focus();
                        return false;
                    }
                }
                else {
                    return false;
                }

            });

            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
            FV_onBlur.initValid($("#<%= btnSave.ClientID %>").closest("form").get(0));
        }

        $(document).ready(function() {
            CheckFormData();
        });
    </script>

    </form>
</body>
</html>
