<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" AutoEventWireup="true" CodeBehind="Head_Index.aspx.cs" Inherits="UserPublicCenter.HelpCenter.Head_Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

 <div style="height: 90px; margin: 0 auto; width: 1004px;">
    </div>
    <div style="border: solid 1px blue; width: 1004px; min-height: 525px;">
        <div style="float: left; width: 200px;">
            <iframe src="menu.html" id="leftFrame" name="leftFrame" scrolling="no" title="leftFrame"
                height="525px" width="200px"></iframe>
        </div>
        <div style="float: left; width: 804px;">
            <iframe src="main.html" id="mainFrame" name="mainFrame" title="mainFrame" height="525px"
                width="804px"></iframe>
        </div>
    </div>

</asp:Content>
