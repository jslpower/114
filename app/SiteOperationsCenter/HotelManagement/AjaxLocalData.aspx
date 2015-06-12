<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLocalData.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.AjaxLocalData" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang" id="tbHotelList">
<tr><td colspan="4"></td></tr>
  <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">    
     <td width="9%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><input type="checkbox" onclick="FirstPageDataAdd.AllCheckControl(this)" />序号<strong></strong></td>
    <td width="56%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>酒店名称</strong></td>
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>添加时间</strong></td>
    <td width="10%"  style=" display:none"  align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>是否置顶</strong></td>
    <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>操作</strong></td>
  </tr>
 <cc1:CustomRepeater ID="crptLocalList" runat="server">
     <ItemTemplate>
        <tr bgcolor="#f3f7ff" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
            <td width="9%" height="25" align="center"><strong> </strong>
                <input type="checkbox" name="chkHotel" value="<%#Eval("Id") %>">
              <%# (Container.ItemIndex+1)+(pageIndex-1)*pageSize %></td>
            <td width="56%" height="25" align="center"><%#Eval("HotelName")%></td>
            <td width="15%" align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
            <td width="10%"  style=" display:none"  align="center"><%#Convert.ToBoolean(Eval("IsTop"))==true ?"置顶":"未置顶" %></td>
            <td width="10%" align="center"><a href="javascript:void(0)"  onclick="FirstPageDataAdd.DeleteLocalHotel('<%#Eval("Id") %>')">删除</a></td>
          </tr>
     </ItemTemplate>
     <AlternatingItemTemplate>
          <tr class="baidi" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
            <td width="9%" height="25" align="center"><strong> </strong>
                <input type="checkbox" name="chkHotel" value="<%#Eval("Id") %>">
                <%#(Container.ItemIndex+1) +(pageIndex-1)*pageSize%>   
             </td>
            <td width="56%" height="25" align="center"><%#Eval("HotelName")%></td>
            <td width="15%" align="center"><%#Eval("IssueTime","{0:yyyy-MM-dd}")%></td>
            <td width="10%" style="display:none" align="center"><%#Convert.ToBoolean(Eval("IsTop"))==true ?"置顶":"未置顶" %></td>
            <td width="10%" align="center"><a href="javascript:void(0)" onclick="FirstPageDataAdd.DeleteLocalHotel('<%#Eval("Id") %>')">删除</a></td>
          </tr>
     </AlternatingItemTemplate>
 </cc1:CustomRepeater>     
  <tr background="images/hangbg.gif" class="white" height="23">
   <td width="9%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
    <td width="56%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>酒店名称</strong></td>
    <td width="15%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>添加时间</strong></td>
    <td width="10%" align="center" valign="middle" style=" display:none" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>是否置顶</strong></td>
    <td width="10%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>操作</strong></td></td>
  </tr>
</table>
<div style=" text-align:right; width:98% "> 
        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>
