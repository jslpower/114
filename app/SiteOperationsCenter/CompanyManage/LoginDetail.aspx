<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginDetail.aspx.cs" Inherits="SiteOperationsCenter.CompanyManage.LoginDetail" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<%@ Register assembly="ControlLibrary" namespace="Adpost.Common.ExportPageSet" tagprefix="cc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:CustomRepeater ID="repLoginList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" class="kuang"
                id="tbCompanyLoginList">
                <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                    <td width="10%" height="23" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>序号</strong>
                    </td>
                    <td width="20%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>登陆人姓名</strong>
                    </td>
                    <td width="30%" align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>用户名</strong>
                    </td>
                    <td width="40%" align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                        <strong>登录时间</strong>
                    </td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                <td align="center">
                    <%# GetCount()%>
                </td>
                <td align="center">
                    <%# Eval("ContactName")%>
                </td>
                <td align="center">
                    <%# Eval("OperatorName")%>
                </td>
                <td align="center">
                    <%# Eval("EventTime")%>
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr class="huanghui" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                 <td align="center">
                    <%# GetCount()%>
                </td>
                <td align="center">
                    <%# Eval("ContactName")%>
                </td>
                <td align="center">
                    <%# Eval("OperatorName")%>
                </td>
                <td align="center">
                    <%# Eval("EventTime")%>
                </td>
            </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table></FooterTemplate>
    </cc1:CustomRepeater>
    <div align="right">
        <cc3:ExportPageInfo ID="ExportPageInfo1" runat="server" />
    </div>

    <script type="text/javascript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF6C7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
    </script>

    </form>
</body>
</html>
