<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoldCompany.ascx.cs" Inherits="UserPublicCenter.HomeControl.GoldCompany" %>
<%@ Import Namespace="EyouSoft.Common" %>

<div class="tjpp">
  <h3><span class="biaoti">金牌企业展示</span><span class="jinpai"></span></h3>
  <div class="tjpp_nei">
  <asp:Repeater ID="rptGoldCompany"  runat="server">
    <ItemTemplate>
        <dl>
             <dd><a href="<%# Eval("RedirectURL")%>" ><img width="116" height="87" src="<%# Utils.GetNewImgUrl((string)Eval("ImgPath"),3) %>" title="<%# Eval("Title") %>"></a></dd>
             
             <dt><a  href="<%# Eval("RedirectURL") %>"><%# Utils.GetText(Eval("CompanyName").ToString(), 8)%></a></dt>
        </dl>
    </ItemTemplate>
  </asp:Repeater>
 </div>
</div>