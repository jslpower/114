<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPhraseList.aspx.cs" Inherits="UserBackCenter.SMSCenter.AjaxPhraseList" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="uc1"%>
<asp:CustomRepeater id="apl_rptPhraseList" runat="server">
<HeaderTemplate>
    <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#ABCCF0" class="tablewidth" >
        <tr>
          <td width="6%" align="center" bgcolor="#C5DCF5">全选<input type="checkbox" style="vertical-align:middle" id="acl_chkAll" onclick="PhraseList.chkAll(this);" /></td>
          <td width="18%" align="center" bgcolor="#C5DCF5">类型</td>
          <td width="76%" align="center" bgcolor="#C5DCF5">常用语</td>
        </tr>
    </HeaderTemplate>
     <ItemTemplate>
       <tr>
          <td align="center"> <input type="checkbox"  name="pl_chk" value='<%# Eval("TemplateId") %>' /></td>
          <td><%# Eval("CategoryName")%></td>
          <td><%# Eval("Content")%></td>
        </tr>
     </ItemTemplate>
     <FooterTemplate> </table></FooterTemplate>
    
</asp:CustomRepeater>

<table id="apl_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <uc1:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></uc1:ExportPageInfo>
        </td>
    </tr>
</table>