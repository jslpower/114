<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxRevertInfo.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.AjaxRevertInfo" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#BAD4F2" >
    <cc1:CustomRepeater ID="crptRevers" runat="server">
        <ItemTemplate>
            <tr bgcolor="#e8f2fe">
            <td  width="77%" height="25" align="left" bgcolor="#E8F2FE"><font color="#3366CC">【<strong>回复</strong>】<%#Eval("AskContent")%></font></td>
            <td   width="16%" height="25" align="center" bgcolor="#E8F2FE"><strong><font color="#3366CC"><%#Eval("AskTime")%></font></strong></td>
            <td   height="25" align="center" bgcolor="#E8F2FE"><strong><font color="#3366CC"></font><%#Eval("AskName")%></strong></td>
            </tr>    
        </ItemTemplate>
    </cc1:CustomRepeater>
          <tr >
            <td colspan="3"  style=" background-color:#E8F2FE" align="right"><cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
              </td>
          </tr> 
</table>
