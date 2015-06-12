<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialogLogin.aspx.cs" Inherits="UserPublicCenter.Register.DialogLogin" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>登录</title>
</head>
<body>
    <form id="aspnetForm" runat="server">
    
    <div>
        <div>
            <table>
                <tbody>
                    <tr>
                        <td width="32%" align="right">
                            会员登录名：
                        </td>
                        <td>
                            <input name="u" id="u" type="text" />
                            <a href="/Register/CompanyUserRegister.aspx" target="_blank">新用户注册</a>
                        </td>
                        <td>
                        <span id="err_UserMessage" style="color: Red; display: none">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            密 码：
                        </td>
                        <td>
                            <input name="p" id="p" type="password" />
                            <a href="/Register/FindPassWord.aspx" target="_blank">忘记密码?</a>
                        </td>
                         <td>
                          <span id="err_PassMessage" style="color: Red; display: none">*</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        </td>
                        <td>
                          <span id="logintip"></span>  
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div>
            <input type="button" id="btnLogin" value="马上登录" />
            <input type="hidden" id="vc" name="vc" />
        </div>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
        
    $(function() {
         
             if($.trim("<%= UserName %>")!=""){
                $("#u").val("<%= UserName %>");  
                   $("#p").focus();
             }else{
                 $("#u").focus();
             }
             
            $("#btnLogin").attr("disabled","");
            $("#p").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                   $("#btnLogin").click();
                   return false;
                }
            });
           
            $("#u").focus(function(){   
                 $("#err_UserMessage").hide();
            });
             $("#u").blur(function(){ 
                if($.trim($(this).val())==""){  
                    $("#err_UserMessage").show();
                 }
             });
              $("#p").focus(function(){   
                 $("#err_PassMessage").hide();
            });
             $("#p").blur(function(){ 
                if($.trim($(this).val())==""){  
                    $("#err_PassMessage").show();
                 }
             });
            $("#btnLogin").click(function() {
                var UserName = $.trim($("#u").val());
                var PassWord = $.trim($("#p").val());
                if (UserName == "") {
                   $("#err_UserMessage").show();
                }
                if (PassWord == "") {
                    $("#err_PassMessage").show();
                }
                if (UserName != "" && PassWord != "") {
                    var url = '<%=Request.QueryString["returnurl"] %>';
                    if (url == "") { 
                        url = "/Default.aspx";
                        var city=<%=CityId %>;
                        if(city!=""&&city!="0"){
                          url=url+"?CityId="+city;
                        }
                    }
                    $(this).val("正在登录....");
                    $(this).attr("disabled","disabled");
                    blogin.ssologinurl="<%=EyouSoft.Common.Domain.PassportCenter %>";
                    blogin(document.getElementById("aspnetForm"), "", url, function(message) {
                        $("#btnLogin").val("登录");
                        $("#p").val("");
                        $("#btnLogin").attr("disabled","");
                        $("#logintip").html(message);
                    });
                }
                return false; 
            });
        });
        
    </script>

    </form>
</body>
</html>
