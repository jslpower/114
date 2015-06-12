<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSystemCustomer.aspx.cs" Inherits="UserBackCenter.SMSCenter.AjaxSystemCustomer" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="uc1"%>
<asp:CustomRepeater id="asc_rptCustomerList" runat="server">
     <HeaderTemplate>
     <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0" class="tablewidth" >
     
        <tr>
          <td width="5%" align="center" bgcolor="#C5DCF5">选择</td>
          <td width="13%" align="center" bgcolor="#C5DCF5">手机号码<br /></td>
          <td width="32%" align="center" bgcolor="#C5DCF5">单位名称</td>
          <td width="11%" align="center" bgcolor="#C5DCF5">负责人</td>
          <td width="13%" align="center" bgcolor="#C5DCF5">所在城市</td>
          
        </tr>
     </HeaderTemplate>
      <ItemTemplate>
         <tr>
          <td align="center">
          <input type="checkbox" name="cl_chk" value='<%# Eval("ID") %>' /></td>
          <td><%#Eval("ContactInfo.Mobile")%></td>
          <td><%#Eval("CompanyName")%></td>
          <td><%#Eval("ContactInfo.ContactName")%></td>
          <td><%# GetProAndCity(Convert.ToInt32(Eval("ProvinceId")),Convert.ToInt32(Eval("CityId")))%></td>
     
        </tr>
      </ItemTemplate>
      
      <FooterTemplate>
         </table>
      </FooterTemplate>
</asp:CustomRepeater>


<table id="asc_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <uc1:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></uc1:ExportPageInfo>
        </td>
    </tr>
</table>