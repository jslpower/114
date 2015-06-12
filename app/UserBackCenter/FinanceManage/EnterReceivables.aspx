﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterReceivables.aspx.cs"
    Inherits="UserBackCenter.FinanceManage.EnterReceivables" EnableViewState="true" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.DatePicker" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登记应收账款</title>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>" cache="true"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <style type="text/css">
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="800px" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#CBECFF"
        bgcolor="#E0E0E0" id="EnterReceivables_tableHead">
        <tr bgcolor="#DBF7FD" width="800px">
            <td height="20" colspan="11" align="center" bgcolor="#CBECFF" style="font-size: 14px;">
                【合同金额：<font color="#990066"><strong><asp:Literal runat="server" ID="EnterReceivables_ltrSumPrice"></asp:Literal></strong></font>
                已收金额：<font color="#990099"><strong><asp:Literal runat="server" ID="EnterReceivables_ltrYS"></asp:Literal></strong></font>
                未收金额：<font color="#990066"><strong><asp:Literal runat="server" ID="EnterReceivables_ltrWS"></asp:Literal></strong></font>】
            </td>
        </tr>
        <tr class="baidi">
            <td width="5%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>序号</strong></font>
            </td>
            <td width="9%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>收款日期</strong></font>
            </td>
            <td width="6%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>收款人</strong></font>
            </td>
            <td width="8%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>收款金额</strong></font>
            </td>
            <td width="16%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>收款方式</strong></font>
            </td>
            <td width="5%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>开票</strong></font>
            </td>
            <td width="9%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>开票金额</strong></font>
            </td>
            <td width="10%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>发票(收据)号</strong></font>
            </td>
            <td width="15%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>备注</strong></font>
            </td>
            <td width="4%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>审核</strong></font>
            </td>
            <td width="13%" align="center" bgcolor="#0A8ED9">
                <font color="#FFFFFF"><strong>操作</strong></font>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="EnterReceivables_rptReceivables" OnItemDataBound="EnterReceivables_rptReceivables_ItemDataBound"
            OnItemCommand="EnterReceivables_rptReceivables_ItemCommand">
            <ItemTemplate>
                <tr class="baidi" id="EnterReceivables_trShow<%# Container.ItemIndex + 1 %>">
                    <td width="5%" align="center" bgcolor="#FFFFFF">
                        <%# Container.ItemIndex + 1 %>
                    </td>
                    <td width="9%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("ItemTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td width="6%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("ContactName")%>
                    </td>
                    <td width="8%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("ItemAmount","{0:C2}")%>
                    </td>
                    <td width="16%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("PayType")%>
                    </td>
                    <td width="5%" align="center" bgcolor="#FFFFFF">
                        <asp:CheckBox runat="server" ID="EnterReceivables_ckbIsBilling" Enabled="false" Checked='<%# bool.Parse(Eval("IsBilling").ToString())%>' />
                    </td>
                    <td width="9%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("BillingAmount","{0:C2}")%>
                    </td>
                    <td width="10%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("InvoiceNo")%>
                    </td>
                    <td width="15%" align="center" bgcolor="#FFFFFF">
                        <%# Eval("Remark")%>
                    </td>
                    <td width="4%" align="center" bgcolor="#FFFFFF">
                        <asp:CheckBox runat="server" ID="EnterReceivables_ckbIsCheck" Enabled="false" Checked='<%# bool.Parse(Eval("IsChecked").ToString())%>' />
                    </td>
                    <td width="13%" align="center" bgcolor="#FFFFFF">
                        <asp:Panel runat="server" ID="EnterReceivables_tdHandle">
                            <a href="javascript:void(0);" onclick="EnterReceivables.editReceivables(<%# Container.ItemIndex + 1 %>)">
                                修改</a>
                            <asp:LinkButton runat="server" ID="EnterReceivables_lkbDel" Text="删除" CommandName="del"
                                CommandArgument='<%# Eval("RegisterId") %>'></asp:LinkButton>
                        </asp:Panel>
                        <asp:LinkButton Visible='<%# bool.Parse(Eval("IsChecked").ToString())%>' runat="server"
                            ID="EnterReceivables_lkbCheck" Text="取消审核" CommandName="nocheck" CommandArgument='<%# Eval("RegisterId") %>'></asp:LinkButton>
                    </td>
                </tr>
                <tr style="display: none" class="baidi" id="EnterReceivables_trEdit<%# Container.ItemIndex + 1 %>">
                    <td width="5%" align="center" bgcolor="#FFFFFF">
                        <%# Container.ItemIndex + 1 %>
                    </td>
                    <td width="9%" align="center" bgcolor="#FFFFFF">
                        <cc1:DatePicker runat="server" trIndex='<%# Container.ItemIndex + 1 %>' ID="EnterReceivables_txtEditEnterDate"
                            Text='<%# Eval("ItemTime","{0:yyyy-MM-dd}")%>' DisplayTime="false" Width="70px" />
                    </td>
                    <td width="6%" align="center" bgcolor="#FFFFFF">
                        <input type="text" id="EnterReceivables_txtEditEnterPeople<%# Container.ItemIndex + 1 %>"
                            value="<%# Eval("ContactName")%>" style="width: 50px" />
                    </td>
                    <td width="8%" align="center" bgcolor="#FFFFFF">
                        <input type="text" id="EnterReceivables_txtEditPrice<%# Container.ItemIndex + 1 %>"
                            value="<%# Eval("ItemAmount","{0:F2}")%>" style="width: 50px" />
                        <input type="hidden" id="EnterReceivables_hidEditPrice<%# Container.ItemIndex + 1 %>"
                            value="<%# Eval("ItemAmount","{0:F2}")%>" />
                    </td>
                    <td width="16%" align="center" bgcolor="#FFFFFF">
                        <select id="EnterReceivables_ddlEditEnterFS<%# Container.ItemIndex + 1 %>" paytype="<%# (int)((EyouSoft.Model.ToolStructure.FundPayType)Eval("PayType")) %>">
                            <option value="-1">请选择付款方式</option>
                            <option value="0">现金（财务现金）</option>
                            <option value="1">签单挂账</option>
                            <option value="2">银行电汇</option>
                            <option value="3">转账支票</option>
                            <option value="4">现金（导游支付）</option>
                            <option value="5">现金收款</option>
                            <option value="6">支付宝支付</option>
                        </select>
                    </td>
                    <td width="5%" align="center" bgcolor="#FFFFFF">
                        <input type="checkbox" id="EnterReceivables_ckbEditIsBilling<%# Container.ItemIndex + 1 %>"
                            <%# bool.Parse(Eval("IsBilling").ToString()) ? "checked" : "" %> />
                    </td>
                    <td width="9%" align="center" bgcolor="#FFFFFF">
                        <input type="text" id="EnterReceivables_txtEditBillingPrice<%# Container.ItemIndex + 1 %>"
                            value="<%# Eval("BillingAmount","{0:F2}")%>" style="width: 50px" />
                    </td>
                    <td width="10%" align="center" bgcolor="#FFFFFF">
                        <input type="text" id="EnterReceivables_txtEditInvoiceNo<%# Container.ItemIndex + 1 %>"
                            value="<%# Eval("InvoiceNo")%>" style="width: 50px" />
                    </td>
                    <td width="15%" align="center" bgcolor="#FFFFFF">
                        <textarea id="EnterReceivables_txtEditRemark<%# Container.ItemIndex + 1 %>" rows="2"
                            cols="20"><%# Eval("Remark")%></textarea>
                    </td>
                    <td width="4%" align="center" bgcolor="#FFFFFF">
                        <input type="checkbox" id="EnterReceivables_ckbEditIsCheck<%# Container.ItemIndex + 1 %>"
                            <%# bool.Parse(Eval("IsChecked").ToString()) ? "checked" : "" %> <%# IsCheck ? "" : "disabled='disabled'" %> />
                    </td>
                    <td width="13%" align="center" bgcolor="#FFFFFF">
                        <asp:Panel runat="server" ID="EnterReceivables_panEdit">
                            <asp:LinkButton runat="server" ID="EnterReceivables_btnEditSave" Text="保存" CommandName="edit"
                                CommandArgument='<%# Eval("RegisterId") %>'></asp:LinkButton>
                            <a href="javascript:void(0);" onclick="EnterReceivables.changeEdit(<%# Container.ItemIndex + 1 %>)">
                                取消</a>
                        </asp:Panel>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr runat="server" class="baidi" id="EnterReceivables_trAdd">
            <td width="5%" align="center" bgcolor="#FFFFFF">
                新增
            </td>
            <td width="9%" align="center" bgcolor="#FFFFFF">
                <cc1:DatePicker runat="server" ID='EnterReceivables_txtEnterDate' DisplayTime="false"
                    Width="70px" />
            </td>
            <td width="6%" align="center" bgcolor="#FFFFFF">
                <asp:TextBox runat="server" ID="EnterReceivables_txtEnterPeople" valid="required"
                    errmsg="请填写收款人！" Width="50px"></asp:TextBox>
            </td>
            <td width="8%" align="center" bgcolor="#FFFFFF">
                <asp:TextBox runat="server" ID="EnterReceivables_txtPrice" valid="required|isMoney"
                    errmsg="请填写收款金额！|收款金额不合法！" Width="50px"></asp:TextBox>
            </td>
            <td width="16%" align="center" bgcolor="#FFFFFF">
                <asp:DropDownList runat="server" ID="EnterReceivables_ddlEnterFS" valid="RegInteger"
                    errmsg="请选择付款方式！">
                    <asp:ListItem Value="-1" Text="请选择付款方式"></asp:ListItem>
                    <asp:ListItem Value="0" Text="现金（财务现金）"></asp:ListItem>
                    <asp:ListItem Value="1" Text="签单挂账"></asp:ListItem>
                    <asp:ListItem Value="2" Text="银行电汇"></asp:ListItem>
                    <asp:ListItem Value="3" Text="转账支票"></asp:ListItem>
                    <asp:ListItem Value="4" Text="现金（导游支付）"></asp:ListItem>
                    <asp:ListItem Value="5" Text="现金收款"></asp:ListItem>
                    <asp:ListItem Value="6" Text="支付宝支付"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="5%" align="center" bgcolor="#FFFFFF">
                <asp:CheckBox runat="server" ID="EnterReceivables_ckbAddIsBilling" />
            </td>
            <td width="9%" align="center" bgcolor="#FFFFFF">
                <asp:TextBox runat="server" qubie="1" ID="EnterReceivables_txtBillingPrice" valid="isMoney"
                    errmsg="开票金额不合法！" Width="50px"></asp:TextBox>
            </td>
            <td width="10%" align="center" bgcolor="#FFFFFF">
                <asp:TextBox runat="server" ID="EnterReceivables_txtInvoiceNo" Width="100px"></asp:TextBox>
            </td>
            <td width="15%" align="center" bgcolor="#FFFFFF">
                <asp:TextBox TextMode="MultiLine" runat="server" ID="EnterReceivables_txtRemark"></asp:TextBox>
            </td>
            <td width="4%" align="center" bgcolor="#FFFFFF">
                <asp:CheckBox runat="server" ID="EnterReceivables_ckbAddIsCheck" />
            </td>
            <td width="13%" align="center" bgcolor="#FFFFFF">
                <asp:LinkButton runat="server" ID="EnterReceivables_btnSave" Text="保存" OnClick="EnterReceivables_btnSave_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="EnterReceivables_hidEditValue" />
    <asp:HiddenField runat="server" ID="EnterReceivables_WS" />

    <script type="text/javascript">
        var EnterReceivables = {
            editReceivables: function(trIndex) {
                $("#EnterReceivables_trShow" + trIndex).hide();
                $("#EnterReceivables_trEdit" + trIndex).show();
                $("#<%= EnterReceivables_trAdd.ClientID %>").hide();
                return false;
            },
            changeEdit: function(trIndex) {
                $("#EnterReceivables_trShow" + trIndex).show();
                $("#EnterReceivables_trEdit" + trIndex).hide();
                $("#<%= EnterReceivables_trAdd.ClientID %>").show();
                return false;
            },
            getEditValue: function(trIndex, obj) {
                var date = $($(obj).parent().parent().parent().find("input[id*='EnterReceivables_txtEditEnterDate_dateTextBox']")[0]).val();
                var name = $("#EnterReceivables_txtEditEnterPeople" + trIndex).val();
                var price = $("#EnterReceivables_txtEditPrice" + trIndex).val();
                var oldPrice = $("#EnterReceivables_hidEditPrice" + trIndex).val();
                var fs = $("#EnterReceivables_ddlEditEnterFS" + trIndex).val();
                var isb = "0";
                if ($("#EnterReceivables_ckbEditIsBilling" + trIndex).attr("checked"))
                    isb = "1";
                var bPrice = $("#EnterReceivables_txtEditBillingPrice" + trIndex).val();
                var INo = $("#EnterReceivables_txtEditInvoiceNo" + trIndex).val();
                var remark = $("#EnterReceivables_txtEditRemark" + trIndex).val();
                var isCheck = "0";
                if ($("#EnterReceivables_ckbEditIsCheck" + trIndex).attr("checked"))
                    isCheck = "1";
                var strErr = new Array();
                if (date == "" || (!RegExps.isDate.test(date)))
                    strErr.push("请选择收款日期！\n");
                if (name == "")
                    strErr.push("请填写收款人！\n");
                if (price == "" || (!RegExps.isMoney.test(price)))
                    strErr.push("收款金额不合法！\n");
                if (!RegExps.RegInteger.test(fs))
                    strErr.push("请选择收款方式！\n");
                if (isb == "1") {
                    if (bPrice == "" || (!RegExps.isMoney.test(bPrice)))
                        strErr.push("开票金额不合法！\n");
                    if (INo == "")
                        strErr.push("请填写发票号！\n");
                }
                if (strErr.length > 0) {
                    alert(strErr.join(""));
                    return false;
                }
                else {
                    $("#<%= EnterReceivables_hidEditValue.ClientID %>").val(date + "^" + name + "^" + price + "^" + oldPrice + "^" + fs + "^" + isb + "^" + bPrice + "^" + INo + "^" + remark + "^" + isCheck);
                    return true;
                }
            }
        };
        function closeWin() {
            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();
        }
        $(function() {
            $("#<%= EnterReceivables_txtEnterDate.ClientID %>_dateTextBox").attr("valid", "required");
            $("#<%= EnterReceivables_txtEnterDate.ClientID %>_dateTextBox").attr("errmsg", "请选择收款日期！");
            $("#<%= EnterReceivables_btnSave.ClientID %>").click(function() {
                var ws = $("#<%= EnterReceivables_WS.ClientID %>").val();
                var cyis = $("#<%= EnterReceivables_txtPrice.ClientID %>").val();
                if (ValiDatorForm.validator(document.forms[0], "alert")) {
                    if (parseFloat(cyis) > parseFloat(ws)) {
                        alert("收款金额不能大于未收金额！");
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            });
            $("#EnterReceivables_tableHead").find(":select").each(function() {
                if (RegExps.RegInteger.test($(this).attr("payType"))) {
                    $(this).val($(this).attr("payType"));
                }
            });
        });
        
        
    </script>

    </form>
</body>
</html>