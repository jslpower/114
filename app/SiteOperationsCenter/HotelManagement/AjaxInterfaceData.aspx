<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxInterfaceData.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.AjaxInterfaceData" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<table width="98%"  border="0" align="center" cellpadding="0" cellspacing="1" class="kuang" id="tbHotelList">
<tr><td colspan="4"></td></tr>
  <tr background="images/hangbg.gif" class="white" height="23">
    <td width="9%" height="23" align="center" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif"><input type="checkbox" onclick="FirstPageDataAdd.AllCheckControl(this)" /><strong> 序号</strong></td>
    <td width="50%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>酒店名称</strong></td>
    <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>添加时间</strong></td>
    <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="10%" valign="middle" style=" display:none"><strong>是否置顶</strong></td>
    <td width="13%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>操作</strong></td>
  </tr>
    <cc1:CustomRepeater ID="crptInterList" runat="server">
        <ItemTemplate>
          <tr bgcolor="#f3f7ff" onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
            <td width="9%" height="25" align="center"><strong> </strong>
                <input type="checkbox" name="chkHotel" value="<%#Eval("HotelCode") %>">
                <%# (Container.ItemIndex+1)+(pageIndex-1)*pageSize %>
              </td>
            <td width="50%" height="25" align="center"><%#Eval("HotelName")%></td>
            <td width="18%" align="center"><%#Eval("Opendate")%></td>
      
            <td align="center" width="10%" style="display:none"><input type="checkbox" name="chkSetTop" id="chkSetTop<%#Eval("HotelCode") %>" value="<%#Eval("HotelCode") %>">置顶</td>
            <td width="13%" align="center"><a href="javascript:void(0)" onclick="FirstPageDataAdd.AddLocalDateByItems('<%#Eval("HotelCode") %>')">添加</a></td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>            
          <tr class="baidi"  onMouseOver=mouseovertr(this) onMouseOut=mouseouttr(this)>
            <td width="9%" height="25" align="center"><strong> </strong>
                <input type="checkbox" name="chkHotel" value="<%#Eval("HotelCode") %>">
                <%# (Container.ItemIndex+1)+(pageIndex-1)*pageSize %>
              </td>
            <td width="50%" height="25" align="center"><%#Eval("HotelName")%></td>
            <td width="18%" align="center"><%#Eval("Opendate")%></td>
            <td align="center" width="10%" style="display:none"><input type="checkbox" name="chkSetTop" id="chkSetTop<%#Eval("HotelCode") %>" value="<%#Eval("HotelCode") %>">置顶</td>
            <td width="13%" align="center"><a href="javascript:void(0)" onclick="FirstPageDataAdd.AddLocalDateByItems('<%#Eval("HotelCode") %>')">添加</a></td>
          </tr>
        </AlternatingItemTemplate>
    </cc1:CustomRepeater>
  <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
    <td width="9%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong> 序号</strong></td>
    <td width="50%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>酒店名称</strong></td>
    <td width="18%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>添加时间</strong></td>
    <td background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" align="center" width="10%" valign="middle" style="display:none"><strong>是否置顶</strong></td>
    <td width="13%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif"><strong>操作</strong></td>
  </tr>
</table>
<div style=" text-align:right; width:98% "> 
    <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
</div>
<input type="hidden" runat="server" id="hidInterHotel" />