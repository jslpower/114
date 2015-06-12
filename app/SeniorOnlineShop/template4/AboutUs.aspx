<%@ Page Language="C#" MasterPageFile="~/master/T4.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="SeniorOnlineShop.template4.AboutUs" Title="关于我们--专线网店" %>
<%@ MasterType VirtualPath="~/master/T4.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server" ID="Content1">
<div class="linetj">
	<div class="linetjtk">
	<div class="linetjth">关于我们 </div>
	<div class="linetjxx">
	
<table width="665" border="0" align="center" cellpadding="0" cellspacing="0" style=" margin:20px 5px;">
        
        <tbody>
          <tr>
            <td align="left" class="jiange2"><strong>单位名称：</strong> <asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal> (许可证号：<asp:Literal ID="ltrXuKeZheng" runat="server"></asp:Literal>) </td>
            </tr>
          <tr>
            <td align="left" class="jiange2"><strong>品牌名称：</strong><asp:Literal ID="ltrBrandName" runat="server"></asp:Literal></td>
            </tr>
          <tr>
            <td align="left" class="jiange2 h20"><asp:Literal ID="ltrAboutUs" runat="server"></asp:Literal></td>
            </tr>
        </tbody>
      </table>
	  
	</div>
	</div>
	  </div>    
</asp:Content>

