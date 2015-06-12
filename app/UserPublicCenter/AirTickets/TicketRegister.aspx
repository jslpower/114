<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketRegister.aspx.cs"
    Inherits="UserPublicCenter.TicketRegister" EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../WebControl/ProvinceAndCityList.ascx" TagName="ProvinceAndCityList"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-size: 14px;
            font-family: "宋体";
        }
        table
        {
            border-collapse: collapse;
        }
        .btn
        {
            border: 0 none;
            width: 97px;
            height: 31px;
            font-weight: bold;
            font-size: 14px;
            color: #FFFFFF;
            cursor: pointer;
        }
        .errmsg
        {
            color: Red;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="hidden"  name="vc" id="vc" value=""/>
        <table width="600" border="0" align="center" cellpadding="5" cellspacing="2">
            <tr>
                <td width="145" align="right">
                    <font color="red">*</font>公司名称：
                </td>
                <td width="455" align="left">
                    <input name="txtCompanyName" id="txtCompanyName" type="text" size="30" errmsg="请输入公司名称!"
                        valid="required"  tabindex="1"/>
                    <span id="errMsg_txtCompanyName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>用户名：
                </td>
                <td align="left">
                    <input name="u" id="u" type="text" size="30" errmsg="请输入用户名!"
                        valid="required"   tabindex="2"/>
                    <span id="errMsg_u" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>密码 ：
                </td>
                <td align="left">
                    <input name="p" id="p" type="password" size="20" errmsg="请输入密码!" valid="required"  tabindex="3"/>
                    <span id="errMsg_txtPwd" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>确认密码：
                </td>
                <td align="left">
                    <input name="txtPwdTwo" id="txtPwdTwo" type="password" size="20" errmsg="密码不匹配!" eqaulname="txtPwd" valid="equal"  tabindex="4" />
                    <span id="errMsg_txtPwdTwo" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>真实姓名：
                </td>
                <td align="left">
                    <input name="txtTrueName" id="txtTrueName" type="text" size="20" errmsg="请输入真实姓名!"
                        valid="required"  tabindex="5"/>
                    <span id="errMsg_txtTrueName" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>手机号码：
                </td>
                <td align="left">
                    <input name="txtPhone" id="txtPhone" type="text" size="20" errmsg="请输入手机号!|号码格式不正确!"
                        valid="required|isMobile"  tabindex="6" />
                    <span id="errMsg_txtPhone" class="errmsg"></span><font size="2px" color="#990000">（提示：用于机票订单提醒）</font>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>所在地：
                </td>
                <td align="left">
                    &nbsp;<uc1:ProvinceAndCityList ID="ProvinceAndCityList1" runat="server" />
                    <span id="errMsg_City" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>邮箱：
                </td>
                <td align="left">
                    <input name="txtEmail" type="text" id="txtEmail" size="25" errmsg="请输入邮箱!|Eamil格式不正确!"
                        valid="required|isEmail"  tabindex="9"/>
                    <span id="errMsg_txtEmail" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>QQ：
                </td>
                <td align="left">
                    <input name="txtQQ" type="text" id="txtQQ" size="25" errmsg="请输入联系QQ号!" valid="required"  tabindex="10"/>
                    <span id="errMsg_txtQQ" class="errmsg"></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <font color="red">*</font>验证码 ：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtValidateCode" runat="server" errmsg="请输入验证码" valid="required"
                       size="6" MaxLength="4"  tabindex="11"></asp:TextBox>
                    <a title="刷新验证码" style="font-size: 12px; text-decoration: none;" href="javascript:void(0);"
                        onclick="javascript:document.getElementById('<%=imgValidateCode.ClientID %>').src='<%=Domain.UserPublicCenter %>/ValidateCode.aspx?ValidateCodeName=CompanyRegisterCode&id='+Math.random();$('#spanCodeisNull').hide();return false;">
                        <asp:Image ID="imgValidateCode" runat="server" />看不清，换一张</a>

                    <script language="javascript" type="text/javascript">
                        document.getElementById('<%= imgValidateCode.ClientID %>').src = '/ValidateCode.aspx?id=' + Math.random() + "&ValidateCodeName=CompanyRegisterCode";
                    </script>

                    <span id="errMsg_<%=txtValidateCode.ClientID %>" class="errmsg"></span><span id="spanCodeErr"
                        style="display: none" class="errmsg">请输入正确的验证码</span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnSubmit" runat="server" Text="注册" CssClass="btn" OnClientClick="return RegisterPage.FunFormCheck()" />
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery1.4") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
        $(function() {
            var form = $("#<%=btnSubmit.ClientID %>").closest("form").get(0);
            FV_onBlur.initValid(form);

            $("#u").blur(function() {
                RegisterPage.ckUserName();
            })

        })

        //验证帐号
        var isNameExist = true;

        var RegisterPage = {
            ckValidateCode: function() { //验证码
                var $CodeObj = $("#<%=txtValidateCode.ClientID %>");
                var CodeisTrue = true;

                var validResult = false;
                var arrStrCookie = document.cookie;
                for (var i = 0; i < arrStrCookie.split(";").length; i++) {
                    var temName = arrStrCookie.split(";")[i].split("=")[0];
                    var temCode = arrStrCookie.split(";")[i].split("=")[1];
                    if ($.trim(temName) == "CompanyRegisterCode") {
                        if (temCode == $.trim($CodeObj.val())) {
                            validResult = true;
                        }
                    }
                }
                if (!validResult) {
                    $("#spanCodeErr").show();
                    CodeisTrue = false;
                }

                return CodeisTrue;
            },
            FunFormCheck: function() {
                var valiDator = true;

                //城市验证
                if ($("#ProvinceAndCityList1_ddl_ProvinceList").val() == "0" || $("#ProvinceAndCityList1_ddl_CityList").val() == "0") {
                    $("#errMsg_City").html("请选择省份和城市!");
                    valiDator = false;
                } else {
                    $("#errMsg_City").html("");
                }


                var form = $("#<%=btnSubmit.ClientID %>").closest("form").get(0);
                valiDator = ValiDatorForm.validator(form, "span");


                if (valiDator) {
                    if (!RegisterPage.ckValidateCode()) {
                        valiDator = false;
                    }
                }

                if (valiDator) {
                    RegisterPage.formSubmit();
                }

                return false;
            }
            , ckUserName: function() { //验证用户名是否存在
                var UserName = $.trim($("#u").val());
                if (UserName != "") {
                    $.ajax({
                        type: "GET",
                        async: false,
                        dataType: 'html',
                        url: "<%=Domain.UserPublicCenter %>/Register/ExistUserName.ashx?UserName=" + UserName,
                        cache: false,
                        success: function(html) {
                            if (html == "True") {
                                $("#errMsg_u").html("该用户名已存在!");
                                isNameExist = false;
                            } else {
                                $("#errMsg_u").html("");
                            }
                        }
                    });
                }
            },
            formSubmit: function() {
                $("#<%=btnSubmit.ClientID %>").val("正在注册...");
                $("#<%=btnSubmit.ClientID %>").attr("disabled", "disabled");
                $.ajax({
                    type: "POST",
                    url: "<%=Domain.UserPublicCenter %>/AirTickets/TicketRegister.aspx?type=submit&v=" + Math.random(),
                    data: $($("#<%=btnSubmit.ClientID %>").closest("form").get(0)).serializeArray(),
                    cache: false,
                    success: function(state) {
                        if (state == "OK") {
                            var UserName = $.trim($("#u").val());
                            var PassWord = $.trim($("#p").val());

                            if (UserName != "" && PassWord != "") {
                                
                                $("#<%=btnSubmit.ClientID %>").val("正在登录....");
                                $("#<%=btnSubmit.ClientID %>").attr("disabled", "disabled");
                                blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
                                blogin5($("#btnSubmit").closest("form").get(0), LoginSuccess, NoLogin);
                            }
                            return false;
                        } else {
                            alert(state);
                            $("#<%=btnSubmit.ClientID %>").val("注册");
                            $("#<%=btnSubmit.ClientID %>").attr("disabled", "");
                        }
                    }, error: function() {
                        alert("注册失败,请稍后再试!");
                        $("#<%=btnSubmit.ClientID %>").val("注册");
                        $("#<%=btnSubmit.ClientID %>").attr("disabled", "");
                    }
                });
            }
        }

        function LoginSuccess() {

            var url = '<%=Domain.UserPublicCenter %>/AirTickets/TicketNew.aspx';
            if ('<%= Request.QueryString["goUrl"] %>' != '') {
                url = decodeURIComponent('<%= Request.QueryString["goUrl"] %>');
            }

            window.parent.location.href = '<%=Domain.UserPublicCenter %>/AirTickets/TicketNew.aspx';
        }

        function NoLogin() {
            alert("登录失败,请稍后再试.");
            window.parent.location.href = "<%=Domain.UserPublicCenter %>/AirTickets/TicketNew.aspx";
        }
    </script>

    </form>
</body>
</html>
