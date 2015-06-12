<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxPersonalMemberList.aspx.cs"
    Inherits="SiteOperationsCenter.CompanyManage.AjaxPersonalMemberList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<cc1:CustomRepeater ID="repList" runat="server">
    <HeaderTemplate>
        <table width="98%" border="1" align="center" cellspacing="0" bordercolor="#C7DEEB"
            class="table_basic">
            <tr>
                <th>
                    序号
                </th>
                <th>
                    账户
                </th>
                <th>
                    姓名
                </th>
                <th>
                    性别
                </th>
                <th>
                    单位名称
                </th>
                <th>
                    手机
                </th>
                <th>
                    QQ
                </th>
                <th>
                    MQ
                </th>
                <th>
                    登录
                </th>
                <th>
                    注册时间
                </th>
                <th>
                    最近登录
                </th>
                <th>
                    功能
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" id="tr">
            <td height="25" align="left">
                <input type="checkbox" name="checkbox" value="<%#Eval("UserId") %>">
            </td>
            <td align="center">
                <%#Eval("Username") %>
            </td>
            <td align="center">
                <%#Eval("Name") %>
            </td>
            <td align="center">
                <%#Eval("Gender") %>
            </td>
            <td align="center">
                <%#Eval("CompanyName") %>
            </td>
            <td align="center">
                <%#Eval("Mobile") %>
            </td>
            <td align="center">
                <%#Eval("QQ") %>
            </td>
            <td align="center">
                <%#Eval("MQ") %>
            </td>
            <td align="center">
                <%#Eval("LoginTimes") %>
            </td>
            <td align="center">
                <%#Eval("RegisterTime") %>
            </td>
            <td align="center">
                <%#Eval("RecentlyLoginTime") %>
            </td>
            <td align="center">
                <a href="UpdatePersonalMember.aspx?UserId=<%#Eval("UserId") %>&CompanyId=<%#Eval("CompanyId") %>">
                    修改</a>
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <th>
                序号
            </th>
            <th>
                账户
            </th>
            <th>
                姓名
            </th>
            <th>
                性别
            </th>
            <th>
                单位名称
            </th>
            <th>
                手机
            </th>
            <th>
                QQ
            </th>
            <th>
                MQ
            </th>
            <th>
                登录
            </th>
            <th>
                注册时间
            </th>
            <th>
                最近登录
            </th>
            <th>
                功能
            </th>
        </tr>
        </table>
    </FooterTemplate>
</cc1:CustomRepeater>
<table width="99%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="30" align="right">
            <cc2:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    function mouseovertr(o) {
        o.style.backgroundColor = "#FFF6C7";
    }
    function mouseouttr(o) {
        o.style.backgroundColor = ""
    }

</script>

