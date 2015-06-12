<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxUserListManage.aspx.cs" Inherits="SiteOperationsCenter.UserManage.AjaxUserListManage" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc3" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%=result %>

   <asp:CustomRepeater id="aulm_rpt_UserList" runat="server">
         <HeaderTemplate>
         <table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang">
            <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td width="7%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
            <td width="27%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>用户名</strong></td>
            <td width="8%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>姓名</strong></td>
            <td width="19%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>电话</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>手机</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>权限</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>状态操作</strong></td>
            </tr>
       </HeaderTemplate>
       <ItemTemplate>
            <tr class="baidi" onMouseOver="UserListManage.mouseovertr(this)" onMouseOut="UserListManage.mouseouttr(this)">
                <td height="25" align="center"><strong> </strong>
                  <input type="checkbox" value='<%# Eval("ID") %>'/>
                  <%=itemIndex++ %></td>
                <td height="25" align="center"><%# Eval("UserName") %></td>
                <td align="center"><%# Eval("ContactName") %></td>
                <td align="center"><%# Eval("ContactTel")%> </td>
                <td width="15%" align="center"><%# Eval("ContactMobile")%></td>
                <td width="8%" align="center"><a href='UserSet.aspx?userid=<%# Eval("ID") %>' onclick="return UserListManage.updateUser_link();">修改</a></td>
                <td width="16%" align="center"><a href="javascript:void(0)" onclick='return UserListManage.setState(this,<%# Eval("ID") %>)'><%# ((bool)Eval("IsDisable"))? "停用" : "启用"%></a></td>
            </tr>
       </ItemTemplate>
       <AlternatingItemTemplate>
            <tr bgcolor="#F3F7FF" onMouseOver="UserListManage.mouseovertr(this)" onMouseOut="UserListManage.mouseouttr(this)">
                <td height="23" align="center"><strong> </strong>
                    <input type="checkbox"   value='<%# Eval("ID") %>'/>
                  <%=itemIndex++ %></td>
                <td height="23" align="center"><%# Eval("UserName") %><br/></td>
                <td align="center"><%# Eval("ContactName") %></td>
                <td align="center"><%# Eval("ContactTel")%> </td>
                <td align="center"><%# Eval("ContactMobile")%></td>
                <td align="center"><a href='UserSet.aspx?userid=<%# Eval("ID") %>' onclick="return UserListManage.updateUser_link();">修改</a></td>
                <td align="center"><a href="javascript:void(0)" onclick='return UserListManage.setState(this,<%# Eval("ID") %>)'><%# ((bool)Eval("IsDisable")) ? "停用" : "启用"%></a></td>
              </tr>
       </AlternatingItemTemplate>
       <FooterTemplate>
           <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
            <td height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
            <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>用户名</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>姓名</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>电话</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">&nbsp;</td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>权限</strong></td>
            <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>状态操作</strong></td>
           </tr>
           </table>
      </FooterTemplate>
      </asp:CustomRepeater>

     <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc3:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </td>
        </tr>
    </table>
