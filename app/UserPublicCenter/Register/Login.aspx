<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UserPublicCenter.Register.Login"
    MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" Title="登录" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/CityAndMenu.ascx" TagName="CityAndMenu" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="Main" ID="cph_Main" runat="server">
    <link rel="stylesheet" type="text/css" href="<%=CssManage.GetCssFilePath("login") %>" />
    <uc1:CityAndMenu ID="CityAndMenu1" runat="server" />
    <div class="px10">
    </div>
    <table width="970" border="0" cellpadding="0" cellspacing="0" id="tbMessage" style="display: none">
        <tr>
            <td style="color: #FF6600; font-size: 14px; font-weight: bold; text-align: left;
                padding-left: 100px;">
                <img src="<%=ImageServerPath %>/images/UserPublicCenter/dl2_ico.gif" width="17" height="18" />
                <%=Message%>
            </td>
        </tr>
    </table>
    <div class="xiaodengl">
        <div class="dlleft">
            <div class="dlleftt">
                <img src="<%=ImageServerPath %>/images/new2011/denglul_03.jpg" /></div>
            <div class="dlcenter">
                <h2>
                    <img src="<%=ImageServerPath %>/images/new2011/denglul_07.jpg" alt="商家登录" /></h2>
                <div class="dlkkk">
                    <p>
                        <label>
                            用户名</label><input tabindex="1" type="text" name="u" id="u" size="10" class="iputt" />
                        <span id="err_UserMessage" style="color: Red; display: none; line-height: 30px; float: left;
                            padding-left: 2px;">*</span></p>
                    <p>
                        <label>密　码</label>
                        <input type="hidden" id="vc" name="vc" />
                        <input tabindex="5" type="password" name="p" id="p" size="10" class="iputt" />
                        <span id="err_PassMessage" style="color: Red; line-height: 30px; float: left; 
                            padding-left: 2px;">*</span>
                    </p>
                    <p class="pkk">
                        <input name="" type="checkbox" value="" />
                        下次自动登录</p>
                    <p class="pkk" >
                        <span>
                            <img id="btnLogin" style="cursor:pointer" src="<%=ImageServerPath %>/images/new2011/denglul_17.jpg" alt=" " />
                        </span>
                        <span class="wjmima"><a href="/Register/FindPassWord.aspx" target="_blank" class="huise">
                            忘了密码？</a></span></p>
                </div>
                <div style="clear: both; height: 1px; overflow: hidden;">
                </div>
                <div class="zcym">
                    <p class="hmzc">
                        还没有注册账号？</p>
                    <p class="ljzc">
                        <a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/Register/CompanyUserRegister.aspx">
                            <img src="<%=ImageServerPath %>/images/new2011/denglul_21.jpg" width="123" height="37"
                                alt=" " /></a></p>
                </div>
            </div>
            <div class="dlleftr">
                <img src="<%=ImageServerPath %>/images/new2011/denglul_23.jpg" alt="" /></div>
        </div>
        <div class="dlright">
            <p class="geyou">
                个人游客如何报名？</p>
            <p class="dianjic">
                <a href="javascript:">点击查找离你最近的旅行社。</a> <a href="javascript:">
                    <img src="<%=ImageServerPath %>/images/new2011/ai_10.jpg" width="43" height="20"
                        alt=" " /></a></p>
        </div>
    </div>
    <div class="px10">
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
        var login = {
            UserLogin: function() {
                var UserName = $.trim($("#u").val());
                var PassWord = $.trim($("#p").val());
                if (UserName == "") {
                    $("#err_UserMessage").show();
                    $("#u").focus();
                }
                if (PassWord == "") {
                    $("#err_PassMessage").show();
                    $("#p").focus();
                }
                if (UserName != "" && PassWord != "") {
                    var url = '<%=Request.QueryString["returnurl"] %>';
                    if (url == "") {
                        url = "/Default.aspx";
                        var city = <%=CityId %>;
                        if (city != "" && city != "0") {
                            url = url + "?CityId=" + city;
                        }
                    }

                    $("#btnLogin").attr("src", "<%=ImageServerPath %>/images/new2011/dengluhui_23.jpg");
                    $("#btnLogin").css("cursor", "default").unbind("click");
                    blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
                    blogin(document.getElementById("aspnetForm"), "", url, function(message) {
                        $("#p").val("").focus();                       
                        $("#btnLogin").attr("src", "<%=ImageServerPath %>/images/new2011/denglul_17.jpg");
                        $("#btnLogin").css("cursor", "pointer").click(function(){
                          login.UserLogin();
                        }); 
                        alert(message);
                    });
                }
            }
        }
        $(function() {
            if ($.trim("<%= UserName %>") != "") {
                $("#u").val("<%= UserName %>");
                $("#p").focus();
            } else {
                $("#u").focus();
            }
            $(".iputt").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    $("#btnLogin").click();
                    return false;
                }
            });

            if ("<%=Request.QueryString["isShow"] %>" != "" || "<%=Request.QueryString["nlm"] %>" != "") {
               $("#tbMessage").show();
            }
            $("#u").focus(function() {
                $("#err_UserMessage").hide();
            });
            $("#u").blur(function() {
                if ($.trim($(this).val()) == "") {
                    $("#err_UserMessage").show();
                }
            });
            $("#p").focus(function() {
                $("#err_PassMessage").hide();
            });
            $("#p").blur(function() {
                if ($.trim($(this).val()) == "") {
                    $("#err_PassMessage").show();
                }
            });
            $("#btnLogin").click(function() {
                login.UserLogin();
                return false;
            });

            $(".dianjic a:eq(0)").click(function() {
                var cityId = '<%=CityId %>';
                window.location.href = "/rival_" + cityId;
                return false;
            });

            $(".dianjic a:eq(1)").click(function() {
                var cityId = '<%=CityId %>';
                window.location.href = "/rival_" + cityId;
                return false;
            });
        });
        
    </script>

</asp:Content>
