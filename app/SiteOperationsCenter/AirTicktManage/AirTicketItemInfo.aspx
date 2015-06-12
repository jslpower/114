<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AirTicketItemInfo.aspx.cs"
    Inherits="SiteOperationsCenter.AirTicktManage.AirTicketItemInfo"  %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="FCKeditor.Net_2.6.3" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特价/免票/K位管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

   

    <style type="text/css">
        .style2
        {
            width: 470px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="1" cellpadding="5" cellspacing="0" bordercolor="#B9D3F2">
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>类别：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="hidden" id="hType" valid="custom" custom="ValidType" errmsg="请选择类别" />
                    <asp:RadioButtonList ID="rdoTypeList" RepeatDirection="Horizontal" runat="server">
                    </asp:RadioButtonList>
                    <span id="errMsg_hType" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>标题：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input id="txtTitle" name="txtTitle" valid="required" runat="server" errmsg="请输入标题"
                        type="text" class="style2" />
                    <span id="errMsg_txtTitle" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    跳转至散客票平台：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="checkbox" id="ckGoSan" name="ckGoSan" runat="server" />跳转至散客票平台
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                   正文：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Width="100%" Height="350px">
                    </FCKeditorV2:FCKeditor>
                 
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>联系人：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input type="text" id="txtContactName" name="txtContactName" valid="required" runat="server"
                        errmsg="请输入联系人" />
                    <span id="errMsg_txtContactName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>联系方式：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input id="txtContactPhone" name="txtContactPhone" valid="required" runat="server"
                        errmsg="请输入联系方式" type="text" />
                    <span id="errMsg_txtContactPhone" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    <span style="color: Red;">*</span>QQ：
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <input id="txtQQ" name="txtQQ" valid="required|isQQ" runat="server" errmsg="请输入QQ|请输入正确的QQ"
                        type="text" />
                    <span id="errMsg_txtQQ" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#D7E9FF">
                    &nbsp;
                </td>
                <td align="left" bgcolor="#F7FBFF">
                    <asp:Button ID="btnSubmit" runat="server" Text="提 交" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        //********
        //表单验证
        //********
        $(document).ready(function() {
            $("#<%=btnSubmit.ClientID %>").click(function() {
                return ValiDatorForm.validator($(this).closest("form").get(0), "span");
            });
            FV_onBlur.initValid($("#<%=btnSubmit.ClientID %>").closest("form").get(0));
        });

        function ValidType(frm, obj) {
            if ($(":radio[checked='true']").length > 0)
                return true;
            else
                return false;
        }
    </script>

    </form>
</body>
</html>
