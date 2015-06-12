<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MinLogin.aspx.cs" Inherits="UserPublicCenter.MinLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
    body{ font-size:14px; position:relative;}
    input{ border:1px solid #ddd; width:130px; height:18px;}
    table{ /*border:5px solid #ddd;*/
            width:330px;}
    a{ color:Green; text-decoration:none;}
    #mbtnLogin{ background:#ecf3eb; width:70px; height:22px;
                 border:1px solid #b9dcce;color:#4f946e; margin-right:15px;cursor:pointer;}
    .mdivLogin{ text-align:center; vertical-align:middle;}
    #tbMessage
    {
         border-collapse:collapse;
         border:1px solid #ff6600;
         width:98%;
         height:40px;
         margin:10px 0px 10px 0px;
    }
    #tbMessage td
    {
    	border-collapse:collapse;color: #FF6600; font-size: 14px; font-weight: bold; text-align: left;
    	padding:5px 0px 5px 40px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <% if (!string.IsNullOrEmpty(Msg))
       { %>
    <table id="tbMessage">
        <tr>
            <td>
                <%=Msg%>
            </td>
        </tr>
    </table>
    <% } %>
    <div class="mdivLogin">
    <table cellpadding="8">
    <tr><td colspan="2" align="left"><a target="_blank" href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/Register/CompanyUserRegister.aspx">您还没有注册？</a></td></tr>
    <tr><td align="right">用户名</td><td align="left"><input tabindex="1" type="text" name="u" id="u"/></td></tr>
    <tr><td align="right">密&nbsp;&nbsp;&nbsp;码</td><td align="left"><input tabindex="5" type="password" name="p" id="p" />&nbsp;&nbsp;<a target="_blank" href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/Register/FindPassWord.aspx">忘记密码了？</a></td></tr>
    <tr><td>&nbsp;</td><td  align="left"><input tabindex="10" type="button" id="mbtnLogin" value="登录"/>&nbsp;<span id="err_Message" style="color:red"></span></td></tr>
    </table>
    </div>
    
    <input type="hidden" id="vc" name="vc" />
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">

        $(function() {

            $("#u").focus();
            $("#mbtnLogin").attr("disabled", "");
            $("#p,#u").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    $("#mbtnLogin").click();
                    return false;
                }
            });

            $("#u").focus(function() {
                $("#err_Message").hide();
            });
            $("#p").focus(function() {
                $("#err_Message").hide();
            });
            $("#mbtnLogin").click(function() {
                var UserName = $.trim($("#u").val());
                var PassWord = $.trim($("#p").val());
                var msg = "";
                if (UserName == "") {
                    msg = "请填写用户名";
                }
                if (PassWord == "") {
                    msg += "   请填写密码";
                }
                $("#err_Message").html(msg).show();
                if (UserName != "" && PassWord != "") {
                    var url = '<%=Request.QueryString["returnurl"] %>';
                    if (url == "") {
                        url = "/Default.aspx";

                    }
                    $(this).val("正在登录....");
                    $(this).attr("disabled", "disabled");
                    blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
                    blogin5($("form").get(0), function() {
                        var returnUrl = "<%=ReturnUrl %>";
                        var target = "<%=LocationTarget %>";
                        if (returnUrl == "") {
                            parent.window.location.reload(true);
                            return;
                        }
                        if (target == "_top") {
                            top.window.location.href = returnUrl;
                        } else if (target == "_parent") {
                            parent.window.location.href = returnUrl;
                        } else if (target == "_blank") {
                            window.open(returnUrl);
                        } else {
                            window.location.href = returnUrl;
                        }
                    }, function(message) {
                        $("#mbtnLogin").val("登录");
                        $("#p").val("");
                        $("#mbtnLogin").attr("disabled", "");
                        $("#err_Message").html(message).show();
                    });
                }
                return false;
            });
        });
        
    </script>
</body>
</html>
