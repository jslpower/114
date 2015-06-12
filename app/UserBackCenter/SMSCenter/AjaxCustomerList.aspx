<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCustomerList.aspx.cs"
    Inherits="UserBackCenter.SMSCenter.AjaxCustomerList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="uc1" %>
<table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0"
    class="tablewidth">
    <tr>
        <td width="60px" align="center" bgcolor="#C5DCF5">
            全选<input type="checkbox" id="acl_chkAll" style="vertical-align: middle" onclick="CustomerList.chkAll(this);" />
        </td>
        <td width="100px" align="center" bgcolor="#C5DCF5">
            客户类型
        </td>
        <td  align="center" bgcolor="#C5DCF5">
            所在地
        </td>
        <td width="100px" align="center" bgcolor="#C5DCF5">
            手机号码
        </td>
    </tr>
    <asp:customrepeater id="acl_rptCustomerList" runat="server">
     <HeaderTemplate>     
     </HeaderTemplate>
      <ItemTemplate>
         <tr>
          <td align="center">
               <%# GetCheckBox(Eval("CompanyId").ToString(), Eval("CustomerId").ToString())%>
          </td>
           <td><%# Eval("CategoryName")%></td>
            <td><%# Eval("ProvinceName")%>-<%# Eval("CityName")%> </td>
          <td><%# GetEncryptMobileText(Eval("CompanyId").ToString(), Eval("Mobile").ToString())%></td>
        </tr>
      </ItemTemplate>
       </asp:customrepeater>
</table>
<table id="acl_ExportPage" cellspacing="0" cellpadding="0" width="98%" align="center"
    border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
            <uc1:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"
                runat="server"></uc1:ExportPageInfo>
        </td>
    </tr>
</table>
