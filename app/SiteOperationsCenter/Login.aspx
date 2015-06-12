<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SiteOperationsCenter.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>同业114</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="480" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 80px;">
            <tr>
                <td width="13" height="261" style="background: url(<%=ImageServerPath%>/images/yunying/login_l.gif) no-repeat;">
                </td>
                <td width="452" valign="top" background="<%=ImageServerPath%>/images/yunying/login_m.gif">
                    <table width="98%" height="148" border="0" align="center" cellpadding="0" cellspacing="0"
                        style="margin-top: 20px;">
                        <tr>
                            <td height="41" colspan="2" align="left" valign="top" class="dhc">
                                <strong>同业114平台管理登录口</strong>
                            </td>
                        </tr>
                        <tr>
                            <td width="28%" height="33" align="right">
                                用户名：
                            </td>
                            <td width="72%" align="left">
                                <input type="text" name="u" id="u" style="border: 1px solid #659BC1; height: 17px;
                                    padding: 3px 0 0 3px;" />
                            </td>
                        </tr>
                        <tr>
                            <td height="32" align="right">
                                密&nbsp; 码：
                            </td>
                            <td align="left">
                                <input type="password" name="p" id="p" style="border: 1px solid #659BC1; height: 17px;
                                    padding: 3px 0 0 3px;" />
                            </td>
                        </tr>
                        <tr>
                            <td height="42">
                                &nbsp;<input type="hidden" name="vc" id="vc" />
                            </td>
                            <td align="left">
                                <input type="submit" id="btnLogin" value=" 登  录 " style="cursor:pointer;background: url(<%=ImageServerPath%>/images/yunying/sub.gif);
                                    border: none; width: 75px; height: 22px; color: #093370;" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="14" style="background: url(<%=ImageServerPath%>/images/yunying/login_r.gif) no-repeat;">
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("ylogin") %>"></script>
    <script type="text/javascript">
    $(function(){
        $("#btnLogin").click(function() {
                var UserName = $.trim($("#u").val());
                var PassWord = $.trim($("#p").val());
                var url="/Default.aspx";
                //var url = '<%=Request.QueryString["returnurl"] %>';
//                if (url == "") { 
//                    url = "/Default.aspx";
//                }
                $(this).val("正在登录....");
                $(this).attr("disabled","disabled");
                blogin.ssologinurl="<%=EyouSoft.Common.Domain.SiteOperationsCenter %>";
                blogin(document.getElementById("form1"), "", url, function(message) {
                    $("#btnLogin").val("登录");
                    $("#p").val("");
                    $("#btnLogin").attr("disabled","");
                    $("#p").focus();
                    alert(message);
                });
                return false; 
            });
            
        $("#u").focus();
    });
    </script>
</body>
</html>
