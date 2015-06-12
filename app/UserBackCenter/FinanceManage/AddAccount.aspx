<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAccount.aspx.cs" Inherits="UserBackCenter.FinanceManage.AddAccount" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <style>
        .tr_collectinfo
        {
            display: none;
        }
        .tr_payinfo
        {
            display: none;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    
      <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script type="text/javascript" language="javascript">
        var AddAccount = {
            //            SetTourInfo: function(tourId, tourNo, RouteName, LeaveDate) {
            //                $("#hidTourId").val(tourId);
            //                $("#txtTourNo").val(tourNo);
            //                $("#txtRouteName").val(RouteName);
            //                $("#txtLeaveDate").val(LeaveDate);
            //            },
            CheckData: function() {
                if (ValiDatorForm.validator($("#form1").get(0), "alertspan")) {
                    return confirm("是否确定提交?");
                }
                else
                    return false;
            },
            SelectTour: function() {
                var NeedId = '<%=Request.QueryString["iframeId"] %>';
                parent.Boxy.iframeDialog({ title: "选择团队", iframeUrl: "/FinanceManage/SelectTour.aspx?NeedId=" + NeedId, width: 800, height: 430, draggable: true });
                return false;
            },
            iframeId: '<%=Request.QueryString["iframeId"] %>',
            addType: '<%=AddType %>',
            addCallback: function(result) {
                var msg = "操作成功";
                if (!result) msg = "操作失败";
                alert(msg);

                var pagePath = { receivable: '/FinanceManage/AccountsReceivable.aspx', payable: '/FinanceManage/AccountPayable.aspx' };
                var openPath = pagePath.receivable;
                if (this.addType == "pay") openPath = pagePath.payable;

                parent.Boxy.getIframeDialog(this.iframeId).hide(function() {
                    parent.topTab.remove(parent.topTab.activeTabIndex);
                    parent.topTab.open(openPath, '账务管理', { isRefresh: true });
                });
            }
        }
        $(document).ready(
            function() {
                var title = "新 增 收 款";
                var span_money_label = "合同金额：";
                var span_leavedate_label = "发团时间：";
                if ("<%= AddType %>" == "pay") {   //表示为新增付款
                    title = "新 增 付 款";
                    span_money_label = "应付金额：";
                    span_leavedate_label = "出团日期：";
                    $(".tr_payinfo").each(
                        function() {
                            $(this).show();
                        }
                    );
                }
                else {
                    $(".tr_collectinfo").each(
                        function() {
                            $(this).show();
                        }
                    );
                }
                $("#span_title").text(title);
                $(document).attr("title", title);
                $("#span_money_label").text(span_money_label);
                $("#span_leavedate_label").text(span_leavedate_label);
                $("#btnSave").bind("click", function() { return AddAccount.CheckData(); });
            }
        );       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidTourId" runat="server" Value="0" />
    <table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#0A8ED9">
        <tr>
            <td height="30" align="center" bgcolor="#0A8ED9" style="font-size: 14px; font-weight: bold;">
                <span id="span_title"></span>
            </td>
        </tr>
        <tr>
            <td align="center" bgcolor="#E6F3FB">
                <table width="95%" border="0" align="center" cellpadding="2" cellspacing="0">
                    <tr>
                        <td width="20" align="center">
                            &nbsp;
                        </td>
                        <td width="80%" align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            <strong>团号：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtTourNo" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            <strong>线路名称：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtRouteName" runat="server" Width="193"></asp:TextBox><a href="javascript:void(0);"
                                onclick="return AddAccount.SelectTour();">&nbsp;从团队导入</a>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            <strong><span id="span_leavedate_label">发团时间：</span></strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtLeaveDate" runat="server" Width="193" valid="isDate" errmsg="时间格式不正确！" onfocus="WdatePicker()"></asp:TextBox>
                            <span id="errMsg_txtLeaveDate" style="color: Red;" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr class="tr_collectinfo">
                        <td height="30" align="right">
                            <strong>订单号：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtOrderNo" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_collectinfo">
                        <td height="30" align="right">
                            <strong>组团社：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtBuyOrderCompanyName" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_payinfo">
                        <td height="30" align="right">
                            <strong>供应商：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtProviderName" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_payinfo">
                        <td height="30" align="right">
                            <strong>供应商类型：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtProviderType" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            <strong>人数：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtPeopleNumber" runat="server" Width="193" valid="RegInteger" errmsg="请填写正确的人数数量！"></asp:TextBox><span id="errMsg_txtPeopleNumber" style="color: Red;" class="errmsg"></span>
                        </td>
                    </tr>
                    <tr class="tr_collectinfo">
                        <td height="30" align="right">
                            <strong>下单人：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtBuyOrderUserName" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tr_collectinfo">
                        <td height="30" align="right">
                            <strong>下单人MQ：</strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtBuyOrderUserMQ" runat="server" Width="193"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            <strong><span style="color: Red">*</span><span id="span_money_label">合同金额：</span></strong>
                        </td>
                        <td height="30" align="left">
                            <asp:TextBox ID="txtMoney" runat="server" Width="193" class="bitian" valid="required|isMoney"
                                errmsg="请填写金额！|只能填写金额！"></asp:TextBox><span id="errMsg_txtMoney" style="color: Red;"
                                    class="errmsg"></span>
                        </td>
                    </tr>
                    <tr>
                        <td height="30" align="right">
                            &nbsp;
                        </td>
                        <td height="30" align="left">
                            <asp:Button ID="btnSave" runat="server" Text="提 交" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
