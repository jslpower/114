<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="SiteOperationsCenter.UserManage.PasswordChange" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
       <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <style>
    table
    {
       
        background-color:#8aaad9;
        
    }
    
    </style>
</head>
<body>
    <form id="pc_form">
    <div>
   <table width="98%"  align="center" cellpadding="2" cellspacing="1" class="lr_bg" id="table1">
        
 <tr class="lr_hangbg">
            <td width="17%" align="right" class="lr_shangbg">
                用户名：</td>
            <td  > &nbsp;<label><%=userName %></label>
                </td>
        </tr>
        <tr class="lr_hangbg">
            <td class="lr_shangbg" align="right">
                <span class="unnamed1">*</span>原密码：</td>
            <td  > &nbsp;<input type="password" name="pc_txtOldPass" id="pc_txtOldPass"  class="textfield" style="width:115px;" valid="required"   errmsg="请填写原密码"/>
                 <span id="errMsg_pc_txtOldPass" class="errmsg"></span></td>
           
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="style2" style="color: #ff0000">*</span>新密码：</td>
           
               
           <td  > &nbsp;<input type="password" name="pc_txtNewPass1" id="pc_txtNewPass1" class="textfield" style="width:115px;" valid="required|limit|custom" min="6" max="16"  errmsg="请填写密码|密码长度为6-16个字符|密码不能单独使用数字，字母或特殊字符" custom="PasswordChange.checkPass"/>
                 <span id="errMsg_pc_txtNewPass1" class="errmsg"></span></td>
               
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                 <span class="style2" style="color: #ff0000">*</span>确认密码：</td>
            <td >
                &nbsp;<input  type="password" name="pc_txtNewPass2" id="pc_txtNewPass2" class="textfield" style="width:115px;" valid="equal" eqaulName="pc_txtNewPass1" errmsg="密码不匹配"/>
                <span id="errMsg_pc_txtNewPass2" class="errmsg"></span>
                </td>
                 
        </tr>
        <tr><td colspan="2" align="center"><input type="button" id="pc_btnSave" onclick="PasswordChange.save()"  value="保存"/></td></tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
    <script type="text/javascript">
      $(document).ready(function()
       { 
          FV_onBlur.initValid($("#pc_form").get(0));
          $("#pc_txtOldPass").val('');
       });
      var PasswordChange={
            save:function(){
              if("<%=haveUpdate %>"=="False")
              {
                    alert("对不起，你没有修改密码的权限!");
                    return false;
              }
             var form=$("#pc_form").get(0);
             if(ValiDatorForm.validator(form,"span"))
             {
                 $.ajax({
                            type: "GET",
                            dataType: "json",
                            url: "PasswordChange.aspx?isajax=yes",
                            data: {method:"update",oldpass:$("#pc_txtOldPass").val(),newpass1:$("#pc_txtNewPass1").val(),newpass2:$("#pc_txtNewPass2").val()},
                            cache: false,
                            success: function(result) {
                                if(/^notLogin$/.test(result))
                                {
                                  alert("对不起，你尚未登录请登录!");
                                  return false;
                                }
                                alert(result.message);
                            },
                            error:function(){
                                alert("操作失败!");
                            }
                        });
                }
            },
             checkPass:function(){  
                 var newPassword=$("#pc_txtNewPass1").val().replace(/\s*/,'');
                 if((/^\d*$/i.test(newPassword))||(/^[a-z]*$/i.test(newPassword))||(/^\W*[_]*\W*$/i.test(newPassword)))
		         {    
	                return false;
	             }
	             return true;
            }
    
      }
    </script>
</body>
</html>
