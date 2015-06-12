<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolMenu.ascx.cs"
    Inherits="SiteOperationsCenter.usercontrol.SchoolMenu" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30">
            <a runat="server" id="a1" class="button_xz" href="/SupplierManage/SchoolIntroduction.aspx">
                <span>
                    <img src="<%= ImageServerUrl %>/images/yunying/hang_zhibiao.gif" width="6" border="0" height="10">学堂介绍
                </span>
            </a>
            <a runat="server" id="a2" class="button" href="/SupplierManage/SchoolList.aspx">
                <span>
                    <img src="<%= ImageServerUrl %>/images/yunying/hang_zhibiao.gif" width="6" border="0" height="10">学堂资讯
                </span>
            </a>
        </td>
        <td width="4%">
            &nbsp;
        </td>
    </tr>
</table>
