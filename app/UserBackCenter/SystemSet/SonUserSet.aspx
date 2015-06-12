<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SonUserSet.aspx.cs" Inherits="UserBackCenter.SystemSet.SonUserSet" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
<link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
     <style type="text/css">
     .errmsg{
          color:#FF0000;
         }
         .bitian,.shurukuang
         {
           height:20px;
         }
        
</style>
</head>
<body>
    <form id="form1" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="tablewidth">
  <tr>
    <td width="5%">&nbsp;</td>
    <td width="95%" align="left"><img src="<%=ImageServerUrl%>/images/zizhanghao1.gif" width="88" height="36" /></td>
  </tr>
</table>
<table width="90%"  border="0" align="center" cellpadding="3" cellspacing="0" style="border:1px solid #EBEBEB;">
  
  
  <tr>
    <td width="18%" align="right"><span class="ff0000">*</span>用&nbsp;户&nbsp;名：</td>
    <td align="left"><input name="sus_txtAccount" isIn="" id="sus_txtAccount" runat="server" maxlength="20" type="text" class="bitian" size="15"   valid="required|limit|custom" custom="SonUserSet.checkUserName"  errmsg="请填写用户名|用户名长度为5-20|用户名不能为中文" min="5" max="20"  /><label><%= account%></label>
    <span id="errMsg_<%=sus_txtAccount.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right"><span class="ff0000">*</span>密&nbsp;&nbsp;&nbsp;&nbsp;码：</td>
    <td align="left"><input  id="sus_txtNewPassword1" runat="server"  type="password" class="bitian" size="16"/>
     <span id="errMsg_<%=sus_txtNewPassword1.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right"><span class="ff0000">*</span>密码确认：</td>
    <td align="left">
    <input  id="sus_txtNewPassword2"  type="password"  runat="server"  class="bitian" size="16"  valid="equal" eqaulName="sus_txtNewPassword1" errmsg="密码不匹配"/>
    <span id="errMsg_<%=sus_txtNewPassword2.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right"> <span class="ff0000">*</span>真实姓名：</td>
    <td align="left"><input name="sus_txtUserName" id="sus_txtUserName" runat="server" maxlength="20" type="text"  class="bitian" size="15"  valid="required"  errmsg="请填写用户名" />
    <span id="errMsg_<%=sus_txtUserName.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right"><span class="unnamed1"></span>部&nbsp;&nbsp;&nbsp; 门：</td>
    <td align="left"><select name="sus_selDepart" id="sus_selDepart" runat="server">
     
    </select></td>
  </tr>
  <tr>
    <td align="right"><span class="ff0000">*</span>角 色 组：</td>
    <td align="left"><select name="sus_selRole" id="sus_selRole" valid="required" runat="server"  errmsg="请选择角色">
      
        </select>
        <span id="errMsg_<%=sus_selRole.ClientID %>" class="errmsg"></span>
        </td>
  </tr>
  <tr>
    <td align="right"><span class="ff0000">*</span> 电&nbsp;&nbsp;&nbsp;&nbsp;话：</td>
    <td align="left"><input name="sus_txtTel" id="sus_txtTel" runat="server"  type="text" class="shurukuang" size="15" valid="required|isPhone" maxlength="20"  errmsg="请填写电话|请填写有效的电话格式" />
    <span id="errMsg_<%=sus_txtTel.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right"><span class="ff0000">*</span>手&nbsp;&nbsp;&nbsp;&nbsp;机：</td>
    <td align="left"><input name="sus_txtMobile" id="sus_txtMobile" runat="server" type="text" class="shurukuang" size="15" maxlength="20" valid="required|isMobile"  errmsg="请填写手机|请填写有效的手机格式"/>
    <span id="errMsg_<%=sus_txtMobile.ClientID %>" class="errmsg"></span>
    </td>
  </tr>
  <tr>
    <td align="right">传&nbsp;&nbsp;&nbsp;&nbsp;真：</td>
    <td align="left">
    <input name="sus_txtFax" id="sus_txtFax" maxlength="50"   runat="server" type="text" class="shurukuang" size="15" /></td>
  </tr>
  <tr>
    <td align="right">QQ：</td>
    <td align="left"><input name="sus_txtQQ"  id="sus_txtQQ" maxlength="20" runat="server" type="text" valid="isQQ" errmsg="请填写有效的QQ格式" class="shurukuang" size="12" />
     <span id="errMsg_sus_txtQQ" class="errmsg"></span>
    <span class="huise">MSN：<input name="sus_txtMSN" id="sus_txtMSN" maxlength="50"  runat="server" type="text" class="shurukuang" size="12" />
    </span></td>
  </tr>
</table>

<%if (haveArea)
  { %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="7%">&nbsp;</td>
                    <td width="93%" align="left"><img src="<%=ImageServerUrl%>/images/jingyingquyu.gif"/></td>
                  </tr>
</table>
<table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:1px solid #EBEBEB;">
  <tr>
    <td>
<table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> <tr onmouseover="SonUserSet.mouseovertr(this)" onmouseout="SonUserSet.mouseouttr(this)">
      <td align="middle" width="3%" bgcolor="#D1E7F4">国内长线</td>
      <td width="96%">
      <table cellspacing="0" width="100%" border="0"> 
      <asp:CustomRepeater id="sus_rpt_LongAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id='chk_<%# Eval("AreaId") %>' <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> type="checkbox"  value='<%#Eval("AreaId") %>' name='checkbox_Area_<%#Eval("AreaId") %>' /><label for='chk_<%# Eval("AreaId") %>'><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table></td>
    </tr>
</table>
<table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> <tr onmouseover="SonUserSet.mouseovertr(this)" onmouseout="SonUserSet.mouseouttr(this)">
      <td align="middle" width="3%" bgcolor="#D1E7F4">国际线</td>
      <td width="96%"><table cellspacing="0" width="100%" border="0"> 
         <asp:CustomRepeater id="sus_rpt_ExitAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id='chk_<%# Eval("AreaId") %>'  <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> type="checkbox"  value='<%#Eval("AreaId") %>' name='checkbox_Area_<%#Eval("AreaId") %>' /><label for='chk_<%# Eval("AreaId") %>'><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table></td>
    </tr>
</table>
<table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> <tr onmouseover="SonUserSet.mouseovertr(this)" onmouseout="SonUserSet.mouseouttr(this)">
      <td align="middle" width="3%" bgcolor="#D1E7F4">国内短线</td>
      <td width="96%"><table cellspacing="0" width="100%" border="0"> 
       <asp:CustomRepeater id="sus_rpt_ShortAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id='chk_<%# Eval("AreaId") %>'  <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> type="checkbox"  value='<%#Eval("AreaId") %>' name='checkbox_Area_<%#Eval("AreaId") %>' /><label for='chk_<%# Eval("AreaId") %>'><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table>	</td>
    </tr>
</table></td>
  </tr>
</table><%} %>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr><td width="150"></td>
    <td><a href="javascript:void(0);" id="sus_aSave" class="xiayiye" onclick="return SonUserSet.save(this)">保存</a></td>
  </tr>
</table>
    </form>
    
</body>
<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

 <script type="text/javascript">
  var SonUserSet=
  { 
      funHandleIeLayout:null,
     isScroll:false,
     handleIELayout:function(){
        if(SonUserSet.isScroll){
            SonUserSet.isScroll = false;
            document.body.style.display="none";
            document.body.style.display="";
            clearInterval(SonUserSet.funHandleIeLayout);
            SonUserSet.funHandleIeLayout = null;
        }
     },
     scroll1:function()
     { 
        SonUserSet.isScroll = true;
        clearInterval(SonUserSet.funHandleIeLayout);
        SonUserSet.funHandleIeLayout=setInterval(SonUserSet.handleIELayout,300);
    },
      mouseovertr:function(o){
          o.style.backgroundColor="#FFF9E7";
      },
      mouseouttr:function(o){
         o.style.backgroundColor="";
      },
      checkPass:function()
     {  
      var newPassword=$("#<%=sus_txtNewPassword1.ClientID %>").val().replace(/\s*/,'');
      if((/^\d+$/i.test(newPassword))||(/^[a-z]+$/i.test(newPassword))||(/^\W*[_]+\W*$/i.test(newPassword)))
		{    
	    
		     return false;
	    }
	    return true;
     },
     checkUserName:function(){
           var userName=$("#<%=sus_txtAccount.ClientID %>").val().replace(/\s+/g,'');
           if(/.*[\u4e00-\u9fa5]+.*$/.test(userName))
           {
             return false;
           }
           return true;
         },
    //全选
    checkAll:function(tar_chk){
      $("#su_areaList").find(":checkbox").attr("checked",$(tar_chk).attr("checked"));
    },
    //保存子账户
    save:function(tar_a){
       var form = $(tar_a).closest("form").get(0);
       if(ValiDatorForm.validator(form,"span",null,false))
       {  
          if($("#<%=sus_txtAccount.ClientID %>").attr("isIn")=="1")
          {
             $("#errMsg_<%=sus_txtAccount.ClientID %>").html("该账户已经存在");
             return false;
          }
          form.submit();
       }
       return false;
    }
  }
  $(document).ready(function()
  { 
    FV_onBlur.initValid($("#sus_aSave").closest("form").get(0),null,false);
    
     if ( $.browser.msie )
          if($.browser.version=="6.0")
          { 
             window.parent.$('#<%=Request.QueryString["iframeId"] %>').get(0).contentWindow.document.body.onscroll=SonUserSet.scroll1;
          }
    $("#<%=sus_txtAccount.ClientID %>").blur(function(){
       var value=$(this).val().replace(/\s+/g,'');
      if(value.length>4&&value.length<21&&$(this).attr("isIn")=="1")
      { 
       
        $("#errMsg_<%=sus_txtAccount.ClientID %>").html("该账户已经存在");
      }
    });
     $("#<%=sus_txtAccount.ClientID %>").change(function(){
        var thisInput=$(this);
        var userNameValue=$(this).val().replace(/\s*/g,'');
       if(userNameValue.length>=5&&userNameValue.length<=20)
       { 
             $.ajax(
              {
               url:"SonUserSet.aspx",
               data:{method:"checkAccount",username:userNameValue},
               dataType:"json",
               cache:false,
               type:"get",
               success:function(result){
                
                 if(result.success=="0")
                 {
                   $("#errMsg_<%=sus_txtAccount.ClientID %>").html("该账户已经存在");
                   thisInput.attr("isIn","1");
                 }
                 else
                 {
                   $("#errMsg_<%=sus_txtAccount.ClientID %>").html("");
                   thisInput.attr("isIn","0");
                 }
               },
               error:function(){
              
                    $("#errMsg_<%=sus_txtAccount.ClientID %>").html("验证账户名时发生错误");
               }
             })
       }
    });
    $(":checkbox").not("[id='su_checkall']").click(function()
    { 
       var checked="";
       if($("#su_areaList").find(":checkbox").not("[id='su_checkall']").not(":checked").length>0)
         checked="";
       else
         checked="checked";
       $("#su_checkall").attr("checked",checked);
    });
  });
 </script>
</html>
