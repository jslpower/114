<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvDetail.aspx.cs" Inherits="UserBackCenter.AdvDetail" %>

<asp:content id="AdvDetail" contentplaceholderid="ContentPlaceHolder1" runat="server">
<table width="99%" border="0" cellspacing="0" cellpadding="2" style="background:#F7FCFF; border:1px solid #D3D3D3;">
    <tr>
      <td width="100%" height="40" colspan="2" align="center"><span style="font-size:22px; font-weight:bold;"><asp:Literal id="ltrAdvTitle" runat="server"></asp:Literal></span></td>
    </tr>    
    <tr>
      <td colspan="2" align="left" style="padding:30px 15px 50px 15px; line-height:150%; font-size:14px;"><asp:Literal id="ltrAdvContent" runat="server"></asp:Literal></td>
    </tr>    
  </table>
</asp:content>
