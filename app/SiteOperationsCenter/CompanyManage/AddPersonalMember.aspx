<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPersonalMember.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.AddPersonalMember" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>个人会员管理-添加</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="80%" border="1" align="center" cellpadding="3" cellspacing="0" bordercolor="#cccccc"
        class="lr_hangbg table_basic">
        <tr>
            <td width="19%" align="right">
                <span class="unnamed1">*</span>用户名：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_UserName" runat="server" id="txt_UserName" />
                <span id="errMsg_txt_UserName" class="unnamed1" style="display: none;">请输入长度为16个字母的用户名</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                MQ：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_MQ" runat="server" id="txt_MQ" />
            </td>
        </tr>
        <tr>
            <td align="right">
                MQ昵称：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_MQNickname" runat="server" id="txt_MQNickname" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>真实姓名：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_RealName" id="txt_RealName" runat="server" />
                <span id="errMsg_txt_RealName" class="unnamed1" style="display: none;">请输入真实姓名</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>密码：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Password" id="txt_Password" runat="server" />
                <span id="errMsg_txt_Password" class="unnamed1" style="display: none;">请输入密码</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>重复密码：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" id="txt_RePassword" runat="server" name="txt_RePassword" />
                <span id="errMsg_txt_RePassword" class="unnamed1" style="display: none;">请输入重复密码且应与密码相同</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                公司职务：
            </td>
            <td width="81%" align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Post" id="txt_Post" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                性别：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropSex" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                电话：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_tel" id="txt_tel" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                手机：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Mobile" id="txt_Mobile" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                传真：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Fax" id="txt_Fax" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                QQ：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_QQ" id="txt_QQ" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                MSN：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_MSN" id="txt_MSN" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <span class="unnamed1">*</span>Email：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Email" id="txt_Email" runat="server" val />
                <span id="errMsg_txt_Email" class="unnamed1" style="display: none;">请输入email或者格式有误</span>
            </td>
        </tr>
<%--        <tr>
            <td align="right">
                个性签名：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <textarea name="txt_Signature" id="txt_Signature" runat="server" cols="50" rows="3"></textarea>
                （MQ签名）
            </td>
        </tr>--%>
        <tr>
            <td align="right">
                经营区域：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <%=strArea %>
                <%--                <asp:DropDownList ID="dropBusinessarea" runat="server">
                </asp:DropDownList>--%>
            </td>
        </tr>
        <tr>
            <td align="right">
                权限分配：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropPermissions" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                部门：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="dropDepartment" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                是否主账户：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <asp:RadioButton ID="radTrue" runat="server" Text="是" />
                <asp:RadioButton ID="radFalse" runat="server" Text="否" />
            </td>
        </tr>
        <tr>
            <td align="right">
                相关公司：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_RelatedCompany" />
            </td>
        </tr>
        <tr>
            <td align="right">
                登录次数：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Logintime" id="txt_Logintime" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">
                注册时间：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input name="txt_Registertime" id="txt_Registertime" onfocus="WdatePicker()" runat="server"
                    type="text" />
            </td>
        </tr>
        <tr>
            <td align="right">
                最近登录：
            </td>
            <td align="left" bgcolor="#FFFFFF">
                <input type="text" name="txt_Lastlogin" id="txt_Lastlogin" runat="server" onfocus="WdatePicker()" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" bgcolor="#FFFFFF">
                <asp:Button ID="btnSave" runat="server" Text="保 存" Style="height: 27px;" OnClick="btnSave_Click" />
                &nbsp;
                <asp:Button ID="btnClosed" runat="server" Text="关 闭" Style="height: 27px;" OnClick="btnClosed_Click" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    
        var isSubmit = false; //区分按钮是否提交过
        //模拟一个提交按钮事件    
        function doSubmit(){
             isSubmit = true;
             $("#<%=btnSave.ClientID%>").click();
        }
    
        $(function(){
            $("#<%=btnSave.ClientID%>").click(function(){
                if(isSubmit){
                //如果按钮已经提交过一次验证，则返回执行保存操作
                    return true;
                }
                
                
                //验证用户名
                var username=$("#<%=txt_UserName.UniqueID %>").val();
                if(username.length<1||username.length>32)
                {
                    $("#errMsg_txt_UserName").show();
                    $("#txt_UserName").focus();
                    return false;
                }else
                {
                    $("#errMsg_txt_UserName").hide();
                }
                
                
                //验证真实姓名
                var realName=$("#<%=txt_RealName.UniqueID %>").val();
                if(realName=="")
                {
                    $("#errMsg_txt_RealName").show();
                    $("#txt_UserName").focus();
                    return false;
                }
                else
                {
                    $("#errMsg_txt_Password").hide();
                }
                
                
                //验证密码
                var Password=$("#<%=txt_Password.UniqueID %>").val();
                var RePassword=$("#<%=txt_RePassword.UniqueID %>").val();
                if(Password!="")
                {
                    //验证重复密码
                    if(RePassword==""||Password!=Password)
                    {
                        $("#errMsg_txt_RePassword").show();
                        $("#txt_RePassword").focus();
                        return false;
                    }
                    else
                    {
                        $("#errMsg_txt_RePassword").hide();
                    }
                    $("#errMsg_txt_Password").show();
                    $("#txt_Password").focus();
                    return false;
                }
                else
                {
                    $("#errMsg_txt_Password").hide();
                }
                
                
                

                
                //验证email
                var email=$("#<%=txt_Email.UniqueID%>").val();
                if(email.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
                {
                    $("errMsg_txt_Email").hide();
                }
                else
                {
                    $("errMsg_txt_Email").show();
                    $("#txt_Email").focus();
                    return false;
                }
                
            });
	        FV_onBlur.initValid($("#form1").get(0));
        });
    
    </script>

    </form>
</body>
</html>
