<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LookUserInfo.aspx.cs" Inherits="IMFrame.Card.LookUserInfo" %>

<%@ OutputCache Duration="3600" VaryByParam="im_username;version" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style>
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        a.red
        {
            color: #cc0000;
        }
        a.red:visited
        {
            color: #cc0000;
        }
        a.red:hover
        {
            color: #ff0000;
        }
        a.cs
        {
            color: #723B00;
        }
        a.cs:visited
        {
            color: #723B00;
        }
        a.cs:hover
        {
            color: #ff0000;
        }
    </style>

    <script type="text/javascript">
        function click(e) {
            if (document.all) {
                if (event.button == 2 || event.button == 3) {
                    oncontextmenu = 'return false';
                }
            }
        }
        document.onmousedown = click;
        document.oncontextmenu = new Function('return false;'); 
    </script>

</head>
<body oncontextmenu="return false;" style="border: 0px;">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td width="1%" align="left" background="<%= ImageServerUrl %>/IM/images/jitiaohangbgz.gif"
                valign="top">
                <img src="<%= ImageServerUrl %>/IM/images/jitiaohangbgl.gif" width="11" height="23" />
            </td>
            <td width="98%" align="left" background="<%= ImageServerUrl %>/IM/images/jitiaohangbgz.gif">
                <asp:Label ID="lblName" runat="server" Text="" />的个人详细信息
            </td>
            <td width="1%" align="right" background="<%= ImageServerUrl %>/IM/images/jitiaohangbgz.gif">
                <img src="<%= ImageServerUrl %>/IM/images/jitiaohangbgr.gif" width="10" height="23" />
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="5">
        <tr>
            <td width="23%" align="left" bgcolor="#EBF4FE">
                <strong>单位名称：</strong>
            </td>
            <td align="left" colspan="3">
                <asp:Label ID="lblCompanyName" runat="server" Text="" />
                许可证号：<asp:Label ID="lblLicense" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#EBF4FE">
                <strong>用户名：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblUserName" runat="server" Text="" />
            </td>
            <td align="left" bgcolor="#EBF4FE" width="15%">
                <strong>电话：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblContactTel" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#EBF4FE">
                <strong>联系人：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblContactName" runat="server" Text="" />
            </td>
            <td align="left" bgcolor="#EBF4FE">
                <strong>传真：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblContactFax" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#EBF4FE">
                <strong>手机：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblContactMobile" runat="server" Text="" />
                <%--<span id="lblQQ"><a href="#">123456<img src="<%= ImageServerUrl %>/IM/images/qqonline.gif" width="23"
                    height="16" /></a>，33<a href="#"><img src="<%= ImageServerUrl %>/IM/images/qqonline.gif" width="23" height="16" /></a></span>--%>
            </td>
            <td align="left" bgcolor="#EBF4FE">
                <strong>Email：</strong>
            </td>
            <td align="left">
                <asp:Label ID="lblContactEmail" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#EBF4FE">
                <strong>经营专线区域：</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="lblAreaName" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td align="left" bgcolor="#EBF4FE">
                <strong>公司介绍：</strong>
            </td>
            <td colspan="3" align="left">
                <asp:Label ID="lblCompanyInfo" runat="server" Text="" />
            </td>
        </tr>
    </table>
</body>
</html>
