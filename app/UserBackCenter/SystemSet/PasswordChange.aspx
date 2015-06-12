<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="UserBackCenter.SystemSet.PasswordChange" %>
<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<asp:Content id="SystemIndex" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<style type="text/css">
.errmsg{
color:#FF0000;
}
</style>
<script type="text/javascript">
  $(document).ready(function()
  {
    FV_onBlur.initValid($("#pc_aSave").closest("form").get(0),null,false);
  });
  
  var PasswordChange=
  {  
   checkPass:function()
    {  
      var newPassword=$("#pc_txtNewPass1").val().replace(/\s*/,'');
        if(!/^[a-zA-Z\W_\d]{6,16}$/.test(newPassword))
		{    
	    
		     return false;
	    }
	    return true;
     },
    save:function(tar_a){
    var form = $(tar_a).closest("form").get(0);
		if(ValiDatorForm.validator(form,"span",null,false))
		{ 
		   var accountData=$("#pc_spanAccount").html();
		   var newPassword=$("#pc_txtNewPass1").val();
		   var oldPassword=$("#pc_txtOldPass").val();
		  $.newAjax(
		   { 
		     url:"/SystemSet/PasswordChange.aspx",
		     data:{account:accountData,newpass:newPassword,oldpass:oldPassword,method:"save"},
             dataType:"json",
             cache:false,
             type:"post",
             success:function(result){ 
               alert(result.message);
               if(result.success=="1")
               {
                 topTab.url(topTab.activeTabIndex,"/SystemSet/PersonInfoSet.aspx");
                 return false;
               }
             },
             error:function(){ 
               alert("操作失败!");
             }
            });
		  }
		return false;
    },
    clear:function(tar_a){
     form.reset();
    }
  }
 </script>
       <table cellspacing="0" cellpadding="3" bordercolor="#9dc4dc" border="1" align="center" class="liststyle padd5 tablewidth" style="margin-top:5px;">
         <tbody>
		 
<tr>
                    <td align="right" width="25%">帐号：</td>
                    <td align="left" width="75%"><span id="pc_spanAccount" ><%=account%></span></td>
                  </tr>
                  <tr>
                    <td align="right">原密码：</td>
                    <td align="left"><input name="pc_txtOldPass" id="pc_txtOldPass" class="shurukuang" value="" size="20" type="password" valid="required|limit" min="6" max="16"  errmsg="请填写原密码|密码长度为6-16个字符" />
                    <span id="errMsg_pc_txtOldPass" class="errmsg"></span>
                    </td>
                  </tr>
                  <tr>
                    <td align="right">新密码：</td>
                    <td align="left"><input name="pc_txtNewPass1" id="pc_txtNewPass1" class="shurukuang" value="" size="20"  type="password" valid="required|limit|custom" custom="PasswordChange.checkPass"  min="6" max="16" errmsg="请填写密码|密码长度为6-16个字符|密码不能单独使用数字，字母或特殊字符" />
                    <span id="errMsg_pc_txtNewPass1" class="errmsg"></span>
                    </td>
                  </tr>
                  <tr>
                    <td align="right">密码确认：</td>
                    <td align="left"><input name="pc_txtNewPass2"  id="pc_txtNewPass2" class="shurukuang" value="" size="20" type="password" valid="equal" eqaulName="pc_txtNewPass1" errmsg="密码不匹配" />
                    <span id="errMsg_pc_txtNewPass2" class="errmsg"></span>
                    </td>
                  </tr>


         <tr>
           <td height="48" align="center" colspan="2"><a href="javascript:void(0)" class="baocun_btn" id="pc_aSave" onclick="return PasswordChange.save(this)">保 存</a> <a class="baocun_btn" href="javascript:void(0);" onclick="topTab.remove(topTab.activeTabIndex);">取 消</a></td>
         </tr>

       </tbody></table>
</asp:Content>
