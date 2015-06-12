<%@ Page Title="景区介绍" Language="C#" MasterPageFile="~/master/ScenicSpotsT1.Master" AutoEventWireup="true"
    CodeBehind="ScenicIntro.aspx.cs" Inherits="SeniorOnlineShop.scenicspots.T1.ScenicIntro" %>

<%@ MasterType VirtualPath="~/master/ScenicSpotsT1.Master" %>
<%@ Register Src="~/scenicspots/usercontrol/ScenicPic.ascx" TagPrefix="uc1" TagName="ScenicPic" %>
<%@ Register Src="~/scenicspots/usercontrol/ChildMenu.ascx" TagPrefix="uc1" TagName="ChildMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="sidebar02 sidebar02Scenic">
        <uc1:ChildMenu runat="server" ID="ChildMenu1" />
        <div class="content">
            <asp:Literal runat="server" ID="ltrIntro"></asp:Literal>            
            <div class="clearboth">
            </div>
        </div>
        <uc1:ScenicPic runat="server" ID="ScenicPic1" />
    </div>
    <div class="clearboth">
    </div>
     <div>
         &nbsp;
        </div>
</asp:Content>
