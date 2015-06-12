<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FrindLinkList.ascx.cs" Inherits="UserPublicCenter.HomeControl.FrindLinkList" %>
  
  
  
  
  <div class="friendlink">
        友情链接</div>
    <div class="friendlinkword">
        <%=strAllFriendLink %>
        <asp:Label ID="lblLinkType" runat="server" Visible="false"></asp:Label>
    </div>
