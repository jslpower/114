<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSet.aspx.cs" Inherits="SiteOperationsCenter.UserManage.UserSet" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
</head>
<body>    
<form id="us_form" name="form1" method="post" runat="server">
<table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" class="lr_bg" id="table1"  style="border:1px solid 8aaad9">
        
        <tr class="lr_hangbg">
            <td width="17%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>用户名：</td>
            <td width="27%">
              &nbsp;<input id="us_txtUserName1" type="text"  class="yonghushuru" runat="server" maxlength="16" style="width:115px;" valid="required|custom" custom="UserSet.checkUserName"  errmsg="请填写用户名|用户名不能中文" />
                 <span id="errMsg_<%=us_txtUserName1.ClientID %>" class="errmsg"></span>
              </td>
                        
             </td>
            <td width="10%" align="right" class="lr_shangbg">
                <span class="unnamed1">*</span>密码：</td>
            <td width="46%">
                &nbsp;<input id="us_txtPass1" type="password" class="textfield" style="width:115px;" runat="server"  />
                 <span id="errMsg_<%=us_txtPass1.ClientID %>" class="errmsg"></span>
                </td>
        </tr>
        <tr class="lr_hangbg">
            <td class="lr_shangbg" align="right">
                <span class="unnamed1">*</span>密码确认：</td>
            <td>
                &nbsp;<input id="us_txtPass2" type="password"  class="textfield" style="width:115px;" runat="server" valid="equal" eqaulName="us_txtPass1" errmsg="密码不匹配"/>
                      <span id="errMsg_<%=us_txtPass2.ClientID %>" class="errmsg"></span>
                </td>
            <td align="right" class="lr_shangbg"><span class="unnamed1">*</span>真实姓名：</td>
            <td>&nbsp;<input  type="text" id="us_txtRealName" runat="server" class="textfield" style="width:115px;"  maxlength="16"  valid="required|limit"  errmsg="请填写姓名" />
             <span id="errMsg_<%=us_txtRealName.ClientID %>" class="errmsg"></span>
         </td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                <span class="style2" style="color: #ff0000">*</span>电话：</td>
            <td>
                &nbsp;<input type="text" id="us_txtTel" runat="server" class="textfield" style="width:115px;" valid="required|isPhone"  errmsg="请填写电话|请填写有效的电话格式"/>
                 <span id="errMsg_<%=us_txtTel.ClientID %>" class="errmsg"></span>
                </td>
            <td align="right" class="lr_shangbg">
             传真：
              </td>
            <td>
                &nbsp;<input  type="text" id="us_txtFax" class="textfield"    maxlength="30" style="width:115px;" runat="server" /></td>
        </tr>
        <tr class="lr_hangbg">
            <td align="right" class="lr_shangbg">
                 手机：</td>
            <td colspan="3">
                &nbsp;<input  type="text" id="us_txtMoible" runat="server" class="textfield" style="width:115px;" valid="isMobile"  errmsg="请填写有效的手机格式"/>
                <span id="errMsg_<%=us_txtMoible.ClientID %>" class="errmsg"></span>
                </td>
                 
        </tr>
        <tr class="lr_hangbg">
          <td height="26" align="right" class="lr_shangbg"><span class="style2" style="color: #ff0000">*</span>负责区域：</td>
          <td  colspan="3"> &nbsp;<a href="javascript:void(0)" onclick="return UserSet.selCity()"><img src="<%=ImageServerUrl %>/images/yunying/zhibiao.gif" width="12" height="12" style="border:0px; margin-bottom:-2px;" />请选择区域&nbsp;</a><span id="errmsg_spanSellCity" style="color:Red"></span>
          <div id="spanSellCity" style="width:500px;">
          <%=areaHTML%>
          </div>
          </td>
         
        </tr>
        <tr class="lr_hangbg">
          <td height="26" align="right" class="lr_shangbg"><span class="style2" style="color: #ff0000">*</span>用户类别：</td>
          <td  colspan="3" id="tr_chkUserType">
          <%=userTypeHtml%>&nbsp;<span id="err_userType" style="color:Red"></span>
          </td>
         
        </tr>
  </table>
     <table width="98%" border="0" align="center" cellpadding="5" cellspacing="0" style="margin-top:15px;">
          <tr>
            <td width="13%" bgcolor="#B1CFF3"><strong><img src="<%=ImageServerUrl %>/images/yunying/icn_pen02.gif" width="13" height="13" />权限管理</strong></td>
            <td width="87%" bgcolor="#B1CFF3">&nbsp;</td>
          </tr>
       </table>
       
       
<asp:CustomRepeater ID="us_rpt_PCateList" runat="server" onitemdatabound="us_rpt_PCateList_ItemDataBound">
    <ItemTemplate>
     
       <table width="98%" border="1" align="center" cellpadding="0" cellspacing="1" bordercolor="#9FBFE6" style="margin-bottom:15px;">
         <tr>
    <td width="17%" height="23" bgcolor="#D7E9FF"><img src="<%=ImageServerUrl %>/images/yunying/zhibiao.gif" width="12" height="12" style="border:0px; margin-bottom:-2px;" /> <strong><%# Eval("CategoryName") %></strong> </td>
    <td width="74%" bgcolor="#D7E9FF">
     
    <td width="9%" align="right" bgcolor="#D7E9FF"><input type="checkbox" onclick="return UserSet.chkAllCate(this)"   />
      全选</td>
   </tr>
        <tr>
    <td colspan="3">
        <asp:CustomRepeater ID="us_rpt_PClassList" runat="server">
            <ItemTemplate>
        
               <table width="100%" border="1" cellpadding="0" cellspacing="1" bordercolor="#9FBFE6" style="margin-top:1px;">
         <tr>
        <td width="17%" height="25" align="left" style="background:#D7E9FF"><input type="checkbox" onclick="UserSet.chkAllClass(this)"/>
          <%# Eval("ClassName")%>
          </td>
        <td width="98%" style="background:#EBF4FF">
        

<table width="100%">
<%#GetPermitList((IList<EyouSoft.Model.SystemStructure.SysPermission>)Eval("SysPermission")) %>
</table>

</td>
      </tr>
    </table>
             
            </ItemTemplate>
        </asp:CustomRepeater>
     </td>
  </tr>
</table>

    </ItemTemplate>
</asp:CustomRepeater>



<table width="99%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="25" align="center"><input type="submit" value="保存" onclick="return UserSet.save()"/></td>
  </tr>
</table>
</form>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>
 <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
 <script type="text/javascript">
     $(document).ready(function() {
         FV_onBlur.initValid($("#us_form").get(0));
         $("#us_txtPass1").val("");
         if ("<%=isAdd %>" == "True") {
             $("#us_txtUserName1").val("");
         }
     });
     var UserSet = {
         selCity: function() {
             UserSet.openDialog("选择负责区域", "/CompanyManage/OtherAllSaleCity.aspx?isall=yes&title=" + encodeURIComponent("已选择的负责区域"), "500px", GetAddOrderHeight());
             return false;
         },
         chkAllCate: function(tar_chk) {
             $(tar_chk).closest("table").find(":checkbox").not("input:disabled").attr("checked", $(tar_chk).attr("checked"));
         },
         chkAllClass: function(tar_chk) {
             $(tar_chk).parent("td").next("td").find(":checkbox").not("input:disabled").attr("checked", $(tar_chk).attr("checked"));
         },
         openDialog: function(title, url, width, height) {
             Boxy.iframeDialog({ title: title, iframeUrl: url, width: width, height: height, draggable: true, data: null });
             return false;
         },
         checkUserName: function() {
             var userName = $("#<%=us_txtUserName1.ClientID %>").val().replace(/\s+/g, '');
             if (/.*[\u4e00-\u9fa5]+.*$/.test(userName)) {
                 return false;
             }
             return true;
         },
         checkPass: function() {
             var newPassword = $("#<%=us_txtPass1.ClientID %>").val().replace(/\s*/, '');
             if (newPassword == "")
                 return true;
             if ((/^\d*$/i.test(newPassword)) || (/^[a-z]*$/i.test(newPassword)) || (/^\W*[_]*\W*$/i.test(newPassword))) {
                 return false;
             }
             return true;
         },
         save: function(tar_) {
             var form = $("#us_form").get(0);
             var isPass = true;
             if (ValiDatorForm.validator(form, "span", null, false)) {
                 if ($("#spanSellCity").find(":checkbox:checked").length < 1) {
                     $("#errmsg_spanSellCity").html("请选择负责区域");
                     isPass = false;

                 }
                 if ($("#tr_chkUserType").find(":checkbox:checked").length < 1) {
                     $("#err_userType").html("请选择用户类别");
                     isPass = false;
                 }
                 if (!isPass) {
                     return false;
                 }
                 $("#us_form").find(":checkbox").removeAttr("disabled");
                 return true;
             }
             return false;
         }
     }
     </script>
</body>
</html>
