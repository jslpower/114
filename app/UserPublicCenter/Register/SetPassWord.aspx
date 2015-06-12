<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPassWord.aspx.cs" Inherits="UserPublicCenter.Register.SetPassWord"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Title="修改密码" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <table width="970" border="0" cellpadding="0" align="center" cellspacing="0">
        <tr id="tr_SetPassWord" runat="server">
            <td>
                <table width="570" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FF9900; margin-top:15px;" align="center">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop15"
                                align="center">
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
                                        <strong>新密码：</strong>
                                    </td>
                                    <td align="left">
                                        <input id="txtPassWord" name="txtPassWord" size="10" style="width: 220px; height: 16px;"
                                            valid="required" errmsg="请输入密码" type="password" />
                                        <span id="errMsg_txtPassWord" class="errmsg"></span><span id="errMsg_txtValidPassWord"
                                            class="errmsg" style="display: none">密码格式不正确！</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="font-size: 12px; text-align: left">
                                        密码由6-16个字符组成，请使用英文字母加数字或符号的组合密码，不能单独使用英文字母、数字或符号作为您的密码。
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 14px; color: #000000; height: 28px;">
                                        <strong>确认新密码：</strong>
                                    </td>
                                    <td align="left">
                                        <input id="txtVerifyPassWord" name="txtVerifyPassWord" size="10" style="width: 220px;
                                            height: 16px;" valid="required" errmsg="请输入确认密码" type="password" />
                                        <span id="errMsg_txtVerifyPassWord" class="errmsg"></span><span id="errMsg_isExitPassword"
                                            style="display: none" class="errmsg">两次密码输入不一致！</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="15">
                                        <asp:Button ID="btnSetPassWord" runat="server" Text="下一步" Style="width: 80px; height: 26px;
                                            font-size: 14px; font-weight: bold;" OnClick="btnSetPassWord_Click" />
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
        <tr id="tr_TrueSetPwd" runat="server" visible="false">
            <td style="font-size: 14px; color: #000000; height: 150px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/zhucgg.gif" width="84" height="76" />密码重置成功!点此<a
                    href="Login.aspx"><strong>登录</strong></a>
            </td>
        </tr>
        <tr id="tr_FalseSetPwd" runat="server" visible="false">
            <td style="font-size: 14px; color: #000000; height: 150px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/paoqian.gif" height="58"
                    width="54" />
                非法参数！ 
            </td>
        </tr>
        <tr id="tr_LinkOut" runat="server" visible="false">
            <td style="font-size: 14px; color: #000000; height: 150px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/paoqian.gif" height="58"
                    width="54" />
                该链接已经失效，请您点击 <a href="FindPassWord.aspx"><strong>这里</strong></a> 重新获取修复密码邮件!
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
            $("#txtPassWord").focus(function() {
                $("#errMsg_txtValidPassWord").hide();
            });
            $("#txtVerifyPassWord").focus(function() {
                $("#errMsg_isExitPassword").hide();
            });
            FV_onBlur.initValid($("#<%=btnSetPassWord.ClientID %>").closest("form").get(0));
            $("#<%=btnSetPassWord.ClientID %>").click(function() {
                var form = $(this).closest("form").get(0);
                if (!ValiDatorForm.validator(form, "span")) {
                    return false;
                } else {
                    var isTrue = true;
                    var PassWord = $.trim($("#txtPassWord").val());
                    var VerifyPassWord = $.trim($("#txtVerifyPassWord").val());
                    if (PassWord != "") {
                        //长度6-16
                        if (PassWord.length < 6 || PassWord.length > 16) {
                            $("#errMsg_txtValidPassWord").show();
                            isTrue = false;
                        }
                        //不能为纯数字，纯字母，纯符号
                        if ((/^[\d]*$/.test(PassWord)) || (/^[a-z]*$/i.test(PassWord)) || (/^[\W_]*$/.test(PassWord))) {
                            $("#errMsg_txtValidPassWord").show();
                            isTrue = false;
                        }
                    }

                    if (PassWord != "" && VerifyPassWord != "") {
                        if (PassWord != VerifyPassWord) {
                            $("#errMsg_isExitPassword").show();
                            isTrue = false;
                        }
                    }

                    return isTrue;
                }
            });
        });
    </script>

</asp:Content>
