<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuestInterviewMenu.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.GuestInterviewMenu" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30">
            <a runat="server" id="a1" class="button_xz" href="/SupplierManage/GuestInterview.aspx"><span>
                <img src="<%= ImageServerUrl %>/images/yunying/hang_zhibiao.gif" width="6" border="0" height="10">访谈介绍</span></a>
            <a runat="server" id="a2" class="button" href="/SupplierManage/CommunityAdvisor.aspx"><span>
                <img src="<%= ImageServerUrl %>/images/yunying/hang_zhibiao.gif" width="6" border="0" height="10">顾问团队</span></a>
            <a runat="server" id="a3" class="button" href="/SupplierManage/HonoredGuest.aspx"><span>
                <img src="<%= ImageServerUrl %>/images/yunying/hang_zhibiao.gif" width="6" border="0" height="10">嘉宾访谈</span></a>
        </td>
        <td width="4%">
            &nbsp;
        </td>
    </tr>
</table>
