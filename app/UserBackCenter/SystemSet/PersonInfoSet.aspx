<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInfoSet.aspx.cs"
    Inherits="UserBackCenter.SystemSet.PersonInfoSet" %>

<%@ Register Src="/usercontrol/szindexNavigationbar.ascx" TagName="sznb" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<asp:content id="SystemIndex" runat="server" contentplaceholderid="ContentPlaceHolder1">


<table width="100%" border="0" cellpadding="0" cellspacing="0" class="tablewidth">
      <tr>
<td valign="top" align="left">
				   <table width="98%" cellspacing="0" cellpadding="0" border="0" align="center" class="margintop5">
                        <tbody><tr>
                          <td width="1%" background="<%=ImageServerUrl%>/images/bgsz.gif" align="left"><img width="11" height="59" src="<%=ImageServerUrl%>/images/leftsz.gif"></td>
                          <td width="11%" background="<%=ImageServerUrl%>/images/bgsz.gif" align="center"><span style="font-size:20px; font-weight:bold; color:#009900; text-decoration:none;">个人设置</span></td>
                          <td width="67%" background="<%=ImageServerUrl%>/images/bgsz.gif" align="left">设置您个人信息、个人联系方式、密码</td>
                          <td width="18%" background="<%=ImageServerUrl%>/images/bgsz.gif" align="center">&nbsp;</td>
                          <td width="3%" background="<%=ImageServerUrl%>/images/bgsz.gif" align="right"><img width="20" height="59" src="<%=ImageServerUrl%>/images/rightsz.gif"></td>
                        </tr>
                    </tbody></table>
				   <table width="98%" cellspacing="0" cellpadding="0" bordercolor="#9dc4dc" border="1" align="center" class="padd5 margintop5" style="border-bottom:2px dashed #ccc; margin-bottom:15px;">
                       <tbody>
                       <%if(!string.IsNullOrEmpty(userModel.ContactInfo.MQ.Trim())){%>
                       <tr>
                         <td width="15%" height="25" align="right">MQ用户名：</td>
                         <td width="38%" align="left"><span id="spanMQName" runat="server"></span></td>
                         <td width="47%" bgcolor="#F6EDA2" align="left">5-20个字符(包括字母、数字、下划线、中文)，一个汉字为两个字符，推荐使用中文会员名。一旦注册成功会员名不能修改</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">MQ昵称：</td>
                         <td align="left"><input type="text" size="20"  class="shurukuang" name="txtMQNickName" id="txtMQNickName" runat="server"></td>
                         <td bgcolor="#F6EDA2" align="left">&nbsp;</td>
                       </tr>
                       <%} %>
                       <tr>
                         <td height="25" align="right">真实姓名：</td>
                         <td align="left">
                        <input id="pis_txtUserName" name="pis_txtUserName" maxlength="20" runat="server" type="text" class="bitian" value="yytyyt" size="20" valid="required"  errmsg="请填写姓名" />
                     <span id="errMsg_<%=pis_txtUserName.ClientID %>" class="errmsg"></span>
						</td>
						<td bgcolor="#F6EDA2" align="left">为方便客服与您联系，请填写您的真实姓名。</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">所在部门：</td>
                         <td align="left">
                            <%--<asp:DropDownList runat="server" ID="ddlDepart"></asp:DropDownList>--%>
                            <span id="spanDepart" runat="server"></span>
                         </td>
                         <td align="left">&nbsp;</td>
                       </tr>                       
                       <tr>
                         <td height="25" align="right">公司职务：</td>
                         <td align="left">
                                <input type="text"  class="shurukuang" name="txtDuty" id="txtDuty" runat="server" valid="limit" min="0" max="10" errmsg="10个字以内！">
                                <span class="errmsg" id="errMsg_<%=this.txtDuty.ClientID%>"></span>
                         </td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">性别：</td>
                         <td align="left">
														 <asp:RadioButtonList runat="server" id="pis_rdiSex" RepeatDirection="Horizontal">
								<asp:ListItem  Value="0" Text="男"></asp:ListItem>
								<asp:ListItem Value="1" Text="女"></asp:ListItem>
								</asp:RadioButtonList>
							   <%-- <input type="radio" name="pis_rdiSex" value="1" runat="server" checked="true" id="pis_rdiSex1" />男
								<input type="radio" name="pis_rdiSex" value="0" id="pis_rdiSex2" runat="server" />女--%>
						 </td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">电话：</td>
                         <td align="left"><input id="pis_txtTel" name="pis_txtTel" type="text" maxlength="50" runat="server" class="shurukuang"  size="20" valid="isPhone"  errmsg="请填写有效的电话格式"/>
                     <span id="errMsg_<%=pis_txtTel.ClientID %>" class="errmsg"></span></td>
                         <td bgcolor="#F6EDA2" align="left">为了客户能及时联系到您，请仔细填写。请填写含区号的完整格式，如：0571-88888888</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">手机：</td>
                         <td align="left"><input  id="pis_txtMobile" name="pis_txtMobile" runat="server" maxlength="50"  type="text" class="shurukuang"  size="20"  valid="isMobile"  errmsg="请填写有效的手机格式"/>
                    <span id="errMsg_<%=pis_txtMobile.ClientID%>" class="errmsg"></span></td>
                         <td bgcolor="#F6EDA2" align="left">手机号码是以后客服与您沟通联系的主要方式之一</td>
                       </tr>
                       <tr>
                         <td height="25" align="right"> 传真：</td>
                         <td align="left"><input id="pis_txtFax"  name="pis_txtFax" maxlength="50" runat="server"  type="text" class="shurukuang"  size="20" /></td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">QQ： </td>
                         <td align="left"><input id="pis_txtQQ" type="text" class="shurukuang" valid="regexp" regexp="^[0-9\,]+$" errmsg="只能输入数字和英文逗号！" runat="server" size="40" name="pis_txtQQ" maxlength="40"  />
                      <span id="errMsg_<%=pis_txtQQ.ClientID%>" class="errmsg"></span>
                      <span class="huise">(多个QQ请用英文","隔开)</span>
                      </td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right">MSN： </td>
                         <td align="left"><input id="pis_txtMSN" name="pis_txtMSN" valid="isEmail" errmsg="MSN格式不正确！" runat="server" type="text" class="shurukuang" maxlength="50"  size="35" />
                         <span id="errMsg_<%=this.pis_txtMSN.ClientID%>" class="errmsg"></span>
                         </td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right"><span class="ff0000">*</span>Email：</td>
                         <td align="left"><input id="pis_txtEmail" name="pis_txtEmail" runat="server" maxlength="50" type="text" class="bitian"  size="35" valid="required|isEmail"  errmsg="请填写邮箱|请填写有效的邮箱格式"/>
                    <span id="errMsg_<%=pis_txtEmail.ClientID %>" class="errmsg"></span></td>
                         <td align="left">&nbsp;</td>
                       </tr>
                       <tr>
                         <td height="25" align="right" colspan="3"><table width="100%" cellspacing="0" cellpadding="0" border="0">
                           <tbody><tr>
                             <td width="7%">&nbsp;</td>
                             <td width="93%" align="left"><img src="<%=ImageServerUrl%>/images/jingyingquyu.gif"></td>
                           </tr>
                         </tbody></table></td>
                       </tr>
                                         <%if (haveArea)
                                           { %>
                      <tr>
                    <td height="50" colspan="3" align="left">
					 <table width="80%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="7%">&nbsp;</td>
                    <td width="93%" align="left"><%if (longCount == 0 && shortCount == 0 && exitCount == 0)
                                                                                                           { %>暂无经营区域<%} %></td>
                  </tr>
                       <tr>
                         <td height="25" align="center" colspan="3">
                         <table width="100%" cellspacing="0" cellpadding="0" border="0" align="center">
                           <tbody>
                           <tr>
                             <td>
                             
		             <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><%if (longCount > 0)
          { %><table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> <tr onmouseover="PersonInfoSet.mouseovertr(this)" onmouseout="PersonInfoSet.mouseouttr(this)">
      <td align="middle" width="4%" bgcolor="#D1E7F4">国内长线</td>
      <td width="96%"><table cellspacing="0" width="100%" border="0"> 
       <asp:CustomRepeater id="pis_rpt_LongAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id="Checkbox1"  disabled="disabled"  type="checkbox" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> value="135|134|21" name="checkbox_Area" /><label for="input_135_134"><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table></td>
    </tr>
</table><%} if (exitCount > 0)
          { %>
<table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> 
<tr onmouseover="PersonInfoSet.mouseovertr(this)" onmouseout="PersonInfoSet.mouseouttr(this)">
      <td align="middle" width="4%" bgcolor="#D1E7F4">国际线</td>
      <td width="96%"><table cellspacing="0" width="100%" border="0"> 
     <asp:CustomRepeater id="pis_rpt_ExitAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id="Checkbox2" disabled="disabled" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> type="checkbox"  value="135|134|21" name="checkbox_Area" /><label for="input_135_134"><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table>
      
      </td>
    </tr>
</table><%} if (shortCount > 0)
          { %>
<table cellspacing="3" cellpadding="2" width="100%" border="0" style="border-bottom:1px dashed #cccccc;"> <tr onmouseover="PersonInfoSet.mouseovertr(this)" onmouseout="PersonInfoSet.mouseouttr(this)">
      <td align="middle" width="4%" bgcolor="#D1E7F4">国内短线</td>
      <td width="96%"><table cellspacing="0" width="100%" border="0"> 
      <asp:CustomRepeater id="pis_rpt_ShortAreaList" runat="server"  >
       <HeaderTemplate>
       <tr>
       </HeaderTemplate>
       <ItemTemplate>
       <td width="25%" align="left"><input id="Checkbox3" disabled="disabled" <%#GetChecked(Convert.ToInt32(Eval("AreaId"))) %> type="checkbox"  value="135|134|21" name="checkbox_Area" /><label for="input_135_134"><%# Eval("areaName") %></label></td>
        <%# GetItem()%>
       </ItemTemplate>
       <FooterTemplate>
       </tr>
       </FooterTemplate>
       </asp:CustomRepeater>
      </table>	<%}
                                           } %>
      
                               </td>
                           </tr>
                         </tbody></table></td>
                       </tr>
                   </tbody></table></td>
  </tr>
  <tr>
    <td colspan="3" align="center">
        注册时间：<span id="spanRegTime" runat="server"></span>
        最近登录时间：<span id="spanLoginTime" runat="server"></span>
        登录次数：<span id="spanLoginCount" runat="server"></span>
        <%--操作时间：<span id="spanHandTime" runat="server"></span>--%>
    </td>
  </tr>
   <tr>
     <td height="50" align="center" colspan="3" style="text-align:center"><a href="javascript:void(0)" class="xiayiye" id="pis_aSave" onclick="return PersonInfoSet.save(this)">保存</a><a href="javascript:void(0)" class="xiayiye" onclick="return PersonInfoSet.clear(this)">重置</a></td>
   </tr>
</table>
<script type="text/javascript">
    $(document).ready(function() {
        FV_onBlur.initValid($("#pis_aSave").closest("form").get(0));
    });
    var PersonInfoSet = {

        mouseovertr: function(o) {
            o.style.backgroundColor = "#FFF9E7";
        },
        mouseouttr: function(o) {
            o.style.backgroundColor = "";
        },
        //链接到密码修改页面
        linkPass: function() {
            topTab.url(topTab.activeTabIndex, "/SystemSet/PasswordChange.aspx");
            return false;
        },
        //保存个人信息
        save: function(tar_a) {
            if ($(tar_a).attr("disabled") == "disabled") {
                return false;
            }
            var form = $(tar_a).closest("form").get(0);
            if (ValiDatorForm.validator(form, "span")) {
                $.newAjax(
		   {
		       url: "/SystemSet/PersonInfoSet.aspx",
		       data: $(form).serialize().replace(/&Input=/, '') + "&method=save",
		       dataType: "json",
		       cache: false,
		       type: "post",
		       success: function(result) {
		           $(tar_a).removeAttr("disabled").html("保存");
		           alert(result.message);
		       },
		       beforeSend: function() {
		           $(tar_a).attr({ "disabled": "disabled" }).html("保存中...");
		       },
		       error: function() {
		           alert("操作失败!");
		       }
		   });
            }
            return false;
        },
        //重置
        clear: function(tar_a) {
            $(tar_a).closest("form").get(0).reset();
            return false;
        }


    }
</script>
</asp:content>
