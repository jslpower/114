<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxLineCompanyList.aspx.cs" Inherits="UserBackCenter.TeamService.AjaxLineCompanyList" %>
<head id="Head1" runat="server">
</head>
   
      <asp:CustomRepeater id="alcl_rpt_companyList1" runat="server"  >
      <HeaderTemplate>
      <table width="100%" border="1" align="center" id="alcl_table" cellpadding="3" cellspacing="0" >
          <tr>
            <td width="10%" bgcolor="#CEDFF2">操作</td>
			<td width="47%" bgcolor="#CEDFF2">公司名称</td>
            <td width="12%" bgcolor="#CEDFF2">联系人</td>
            <td width="16%" bgcolor="#CEDFF2">电话</td>
            <td width="15%" bgcolor="#CEDFF2">手机</td>
          </tr>
      </HeaderTemplate>
        <ItemTemplate>   
         <tr onmouseover="DirectorySet.mouseovertr(this)" onmouseout="DirectorySet.mouseouttr(this)">
                <td height="24"><input  type="checkbox" id='alcl_<%# Eval("ID") %>' onclick="DirectorySet.checkCompany(this);"  <%# GetChecked(Eval("ID").ToString()) %>  value='<%# Eval("ID") %>'/><label for='alcl_<%# Eval("ID") %>'>选定</label></td>
                <td align="left"><%# Eval("CompanyName") %> <a href="javascript:void(0)" onclick="return DirectorySet.OpenDialog('产品查看','/TeamService/RouteList.aspx?companyid=<%# Eval("ID") %>','550px',GetAddOrderHeight())" title="点击查看该公司产品">【查看产品 (<%# GetTourCount(Eval("ID").ToString()) %>)】</a></td>
                <td><%# Eval("ContactInfo.ContactName") %></td>
                <td><span style="color:#333333; line-height:14px;"><%# Eval("ContactInfo.Tel")%></span></td>
                <td><span style="color:#333333; line-height:14px;"><%# Eval("ContactInfo.Mobile")%></span></td>
              </tr>
        </ItemTemplate>
        <FooterTemplate>
        </table>
        </FooterTemplate>
        </asp:CustomRepeater>
  


 <table id="ajlc_ExportPage"  cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
    <tr>
        <td class="F2Back" align="right" height="40">
          <cc2:ExportPageInfo ID="ExportPageInfo1" CurrencyPageCssClass="RedFnt" LinkType="4"  runat="server"></cc2:ExportPageInfo>
        </td>
    </tr>
</table>
 