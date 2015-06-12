<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineDescription.aspx.cs" Inherits="SeniorOnlineShop.shop.LineDescription" MasterPageFile="~/master/Shop.Master" %>
<%@ MasterType VirtualPath="~/master/Shop.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphLine" runat="server">
<table width="970" border="0" align="center" cellpadding="0" cellspacing="0" class="margin10" style="background:url(<%=ImageManage.GetImagerServerUrl(1) %>/images/listheadbj.gif)">
  <tr>
    <td width="109" height="26" align="center" valign="bottom" class="listun"><a id="line" runat="server">旅游线路</a></td>
    
    <td width="103" align="center" valign="bottom" class="liston"> <a id="info" runat="server">专线介绍 </a></td>
	
    <td width="758" align="center" valign="bottom">
	</td>
  </tr>
</table>
<table width="970" height="10" border="0" align="center" cellpadding="0" cellspacing="0" class="xianluhangcx" style="line-height:10px; padding:0px;border:1px solid #ccc; border-bottom:0px;">
  <tr>
    <td width="67%" align="left" style="padding-left:65px;">&nbsp;</td>
    <td width="22%" align="left" style="padding-left:45px;"><strong></strong></td>
    <td width="11%"></td>
  </tr>
</table>
<table width="970" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center" style="padding:10px;"><h1><asp:Literal ID="ltr_CompanyName" runat="server"></asp:Literal></h1></td>
  </tr>
  <tr>
    <td style="padding:10px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="left"><asp:Literal ID="ltr_CompanyInfo" runat="server"></asp:Literal></td>
      </tr>
    </table>
      <table width="100%" border="1" cellspacing="0" cellpadding="2" style="border:1px solid #cccccc; background:#f9f9f9; margin:10px;">
        <tr>
          <td width="14%" align="right">许可证号：</td>
          <td width="40%" align="left">
              <asp:Literal ID="ltr_License" runat="server"></asp:Literal></td>
          <td width="14%" align="right">负责人：</td>
          <td width="32%" align="left"><asp:Literal ID="ltr_Person" runat="server"></asp:Literal></td>
        </tr>
        <tr>
          <td align="right">电话：</td>
          <td align="left"><asp:Literal ID="ltr_Phone" runat="server"></asp:Literal></td>
          <td align="right">手机：</td>
          <td align="left"><asp:Literal ID="ltr_Mobile" runat="server"></asp:Literal></td>
        </tr>
        <tr>
          <td align="right">地址：</td>
          <td colspan="3" align="left"><asp:Literal ID="ltr_Address" runat="server"></asp:Literal></td>
        </tr>
      </table></td>
  </tr>
</table>
</asp:Content>