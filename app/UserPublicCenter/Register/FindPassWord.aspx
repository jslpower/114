<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindPassWord.aspx.cs" Inherits="UserPublicCenter.Register.FindPassWord"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Title="找回密码" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" cellpadding="0" align="center" cellspacing="0">
        <tr id="tr_FindPwd" runat="server">
            <td>
                <table width="570" border="0" cellspacing="0" cellpadding="0" align="center">
                    <tr>
                        <td style="margin: 15px 0 15px 0; padding: 15px; color: #FF6600; font-size: 14px;
                            font-weight: bold; text-align: left;">
                            <img src="<%=ImageServerPath %>/images/UserPublicCenter/dl2_ico.gif" width="17" height="18" />
                            找回密码｜<br />
                            <span style="font-size: 12px; color: #000000">(您填写的邮箱地址必须您注册时填写的邮箱地址一致，我们会发送密码找回邮件至您的邮箱;<br />
                                如果您忘记邮箱或者忘记用户名时，请联系我们的客服，电话：0571-56884627)</span>
                        </td>
                    </tr>
                </table>
                <table width="570" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FF9900;" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15"
                                align="center" style="margin-top:15px">
                                <tr>
                                    <td width="32%" align="right" style="font-size: 14px; color: #000000; height: 28px;">
                                        <strong>会员登录名：</strong>
                                    </td>
                                    <td width="68%" align="left">
                                        <input id="txtUserName" name="txtUserName" type="text" size="10" style="width: 220px;
                                            height: 16px;" valid="required" errmsg="请输入会员登录名" />
                                        <span id="errMsg_txtUserName" class="errmsg"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px; color: #000000; height: 28px;">
                                        <strong>邮 箱：</strong>
                                    </td>
                                    <td align="left">
                                        <input id="txtContactEmail" name="txtContactEmail" size="10" style="width: 220px;
                                            height: 16px;" valid="required|isEmail" errmsg="请输入Email|请输入正确的Email" />
                                        <span id="errMsg_txtContactEmail" class="errmsg"></span><span id="errMsg_isExitEmail"
                                            class="errmsg" style="display: none">Email与该用户不匹配!</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="15">
                                        <asp:Button ID="btnFindPassWord" runat="server" Text="确    定" Style="width: 80px;
                                            height: 26px; font-size: 14px; font-weight: bold;" OnClick="btnFindPassWord_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="15">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="tr_SendToEmail" runat="server" visible="false">
            <td style="font-size: 14px; color: #000000; height: 150px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/zhucgg.gif" width="84" height="76" />
                密码找回邮件发送成功,请注意接收邮件!<br />
            </td>
        </tr>
    </table>
    <style>
        .black_overlay
        {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: white;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .100;
            filter: alpha(opacity=0);
        }
        .white_content
        {
            display: none;
            position: absolute;
            top: 560px;
            left: 25%;
            width: 600px;
            height: 120px;
            text-align: right;
            padding: 5px;
            border: 2px solid #FFC267;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">
        $(function() {
            $("#txtContactEmail").focus(function() {
                $("#errMsg_isExitEmail").hide();
            });
            FV_onBlur.initValid($("#<%=btnFindPassWord.ClientID %>").closest("form").get(0));
            $("#<%=btnFindPassWord.ClientID %>").click(function() {
                var form = $(this).closest("form").get(0);
                if (!ValiDatorForm.validator(form, "span")) {
                    return false;
                } else {
                    var isTrue = false;
                    if ($("#errMsg_isExitEmail").css("display") == "none") {
                        $("#<%=btnFindPassWord.ClientID %>").attr("disabled", "disabled");
                        $.ajax({
                            type: "GET",
                            async: false,
                            dataType: 'html',
                            url: "/Register/FindPassWord.aspx?isExitEmail=1&UserName=" + encodeURIComponent($.trim($("#txtUserName").val())) + "&Email=" + encodeURIComponent($.trim($("#txtContactEmail").val())),
                            cache: false,
                            success: function(html) {
                                if (html == "True") {
                                    isTrue = true;
                                } else {
                                    $("#errMsg_isExitEmail").show();
                                }
                                $("#<%=btnFindPassWord.ClientID %>").attr("disabled", "");
                            }
                        });
                    }

                    return isTrue;
                }
            });

        });
    </script>

</asp:Content>
