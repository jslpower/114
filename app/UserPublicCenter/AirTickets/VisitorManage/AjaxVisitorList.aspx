<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxVisitorList.aspx.cs" Inherits="UserPublicCenter.AirTickets.VisitorManage.AjaxVisitorList" %>
<%@ Import Namespace="EyouSoft.Common" %>

<%@ Register assembly="ControlLibrary" namespace="ControlLibrary" tagprefix="cc1" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExporPage" tagprefix="cc2" %>
<cc1:CustomRepeater ID="crpVisitorList" runat="server">
<HeaderTemplate>
<table width="100%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#dcd8d8">
        <tr>
            <th width="11%" height="30" align="center" bgcolor="#EDF8FC">中文名</th>
            <th width="11%" align="center" bgcolor="#EDF8FC">英文名</th>
            <th width="10%" align="center" bgcolor="#EDF8FC">旅客类型</th>
            <th width="11%" align="center" bgcolor="#EDF8FC">性别</th>
            <th width="8%" align="center" bgcolor="#EDF8FC">证件类型</th>
            <th width="16%" align="center" bgcolor="#EDF8FC">证件号码</th>
            <th width="11%" align="center" bgcolor="#EDF8FC">联系电话</th>
            <th width="11%" align="center" bgcolor="#EDF8FC">国家</th>
            <th width="11%" align="center" bgcolor="#EDF8FC">操作</th>
          </tr>
</HeaderTemplate>
<ItemTemplate>
            <tr>
            <td height="30" align="center"><%#Eval("ChinaName")%></td>
            <td align="center"><%#Eval("EnglishName")%></td>
            <td align="center"><%#Eval("VistorType")%></td>
            <td align="center"><%#Eval("ContactSex")%></td>
            <td align="center"><%#Eval("CardType")%></td>
            <td align="center"><%#Eval("CardNo")%></td>
            <td align="center"><%#Eval("ContactTel")%></td>
            <td align="center"><%#((EyouSoft.Model.TicketStructure.TicketVistorInfo)GetDataItem()).NationInfo.CountryName.ToString() %></td>
            
           <td align="center">           
            <a href='VisitorsPage.aspx?EditId=<%#Eval("Id") %>'>修改</a><strong>|</strong>
            <a href='javascript:void(0);' onclick="VisitorList.DeleteVisitorInfo('<%#Eval("Id") %>')">删除</a>
         </td> </tr>
</ItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</cc1:CustomRepeater>
<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>
    <div class="digg" align="center">       
        <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" PageStyleType="NewButton" />    
        </div>   
    </td>
  </tr>
</table>

