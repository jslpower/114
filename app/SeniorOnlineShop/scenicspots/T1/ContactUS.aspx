<%@ Page Title="联系我们" Language="C#" MasterPageFile="~/master/ScenicSpotsT1.Master"
    AutoEventWireup="true" CodeBehind="ContactUS.aspx.cs" Inherits="SeniorOnlineShop.scenicspots.T1.ContactUS" %>

<%@ MasterType VirtualPath="~/master/ScenicSpotsT1.Master" %>
<%@ Register Src="~/scenicspots/usercontrol/ScenicPic.ascx" TagPrefix="uc1" TagName="ScenicPic" %>
<%@ Register Src="~/scenicspots/usercontrol/ChildMenu.ascx" TagPrefix="uc1" TagName="ChildMenu" %>
<%@ Register Src="~/scenicspots/usercontrol/GoogleMapControl.ascx" TagPrefix="uc1" TagName="GoogleMapControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="sidebar02 sidebar02Scenic">
        <uc1:ChildMenu runat="server" ID="ChildMenu1" />
        <div class="content">
            <span style="float:left; display:block;">
                <img src="<%= ImageServerPath %>/scenicspots/T1/images/people.gif" /></span>
            <p>
                <table width="450" border="0" align="right" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC"
                    class="contact_container">
                    <tr>
                        <td height="25" colspan="2" bgcolor="#ecf8e9">
                            <font class="C_green">&nbsp;&nbsp;联系方式</font>
                        </td>
                    </tr>
                    <tr>
                        <td width="100" height="30" align="center" bgcolor="#FFFFFF">
                            联系人：
                        </td>
                        <td width="352" align="left" bgcolor="#FFFFFF">
                            &nbsp;<asp:Literal runat="server" ID="ltrContacter"></asp:Literal>
                            <asp:Literal runat="server" ID="ltrMQ"></asp:Literal>
                            <asp:Literal runat="server" ID="ltrQQ"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            电话：
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;<asp:Literal runat="server" ID="ltrTelephone"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            传真：
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;<asp:Literal runat="server" ID="ltrFax"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            网址：
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;<asp:Literal runat="server" ID="ltrWebSite"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            地址：
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            &nbsp;<asp:Literal runat="server" ID="ltrAddress"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </p>
            <div class="clearboth">
            </div>            
        </div>
        <div class="text">
                <uc1:GoogleMapControl runat="server" ID="GoogleMapControl1" ShowMapWidth="690" ShowMapHeight="319" IsShowTitle="true" IsShowGLargeMap="true" IsShowGOverviewMap="true" />
            </div>
    </div>
    <div class="clearboth">
    </div>
     <div>
         &nbsp;
        </div>
</asp:Content>
