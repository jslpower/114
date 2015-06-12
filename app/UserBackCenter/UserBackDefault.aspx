<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBackDefault.aspx.cs"
    Inherits="UserBackCenter.UserBackDefault" %>

<%@ Register Src="~/usercontrol/UserBackDefault.ascx" TagName="userbackdefault" TagPrefix="uc1" %>
<asp:content id="UserBackDefault" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <uc1:userbackdefault ID="userbackdefault1" runat="server" />
    
    <script type="text/javascript">
        setTimeout(function(){setTimeRefresh=true;},300000);
    </script>
</asp:content>
