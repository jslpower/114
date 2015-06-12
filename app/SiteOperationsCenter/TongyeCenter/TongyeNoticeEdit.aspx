<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongyeNoticeEdit.aspx.cs"
    Inherits="SiteOperationsCenter.TongyeCenter.TongyeNoticeEdit" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellspacing="0" cellpadding="5" bordercolor="#B9D3F2" border="1">
            <tbody>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>标题：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <asp:TextBox ID="txtTitle" runat="server" Width="400px" MaxLength="50" valid="required"
                            errmsg="请输入标题!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>排序：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <input type="text" id="txtSort" runat="server" maxlength="3" valid="required|isNumber" errmsg="请输入数字序号!|请输入数字序号!" />
                    </td>
                </tr>
                <tr>
                    <td width="17%" bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>公告对象：
                    </td>
                    <td width="83%" bgcolor="#F7FBFF" align="left">
                        <asp:CheckBoxList ID="chkNoticeList" runat="server" RepeatDirection="Horizontal" RepeatColumns="6">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>正文：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <FCKeditorV2:FCKeditor ID="FCK_PlanTicketContent" ToolbarSet="Default" Height="420px"
                            runat="server">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>时间：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <asp:TextBox ID="txtDate" runat="server" onfocus="WdatePicker({skin:'default',dateFmt:'yyyy-MM-dd HH:mm:ss'})"
                            class="Wdate" valid="required" errmsg="请输入日期!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        <span style="color: Red;">*</span>发布人：
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <asp:TextBox ID="txtOper" runat="server" valid="required" errmsg="请输入发布人!" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#D7E9FF" align="right">
                        &nbsp;
                    </td>
                    <td bgcolor="#F7FBFF" align="left">
                        <asp:Button ID="btnSubmit" runat="server" Text="提交" OnClick="btnSubmit_Click"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        $("#<%=this.btnSubmit.ClientID%>").click(function() {
            var form = $(this).closest("form").get(0);
            //点击按纽触发执行的验证函数
            return ValiDatorForm.validator(form, "alert");
        });
        //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
        FV_onBlur.initValid($("#<%=this.btnSubmit.ClientID%>").closest("form").get(0));
    </script>
</body>
</html>
