<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxContacters.aspx.cs" Inherits="SiteOperationsCenter.CustomerManage.AjaxContacters" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看联系人详细信息</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Repeater ID="rptContacter" runat="server">
        <HeaderTemplate>
            <table width="100%" border="1">
                <tr height="20" bgcolor="#F3F7FF" align="center">
                    <td width="9%">联系人</td>
                    <td width="9%">生日</td>
                    <td width="9%">纪念日</td>
                    <td width="10%">职务</td>
                    <td width="10%">手机</td>
                    <td width="15%">邮箱</td>
                    <td width="8%">QQ</td>
                    <td width="8%">性格</td>
                    <td width="8%">爱好</td>
                    <td width="8%">备注</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
                <tr align="center" height="20">
                    <td><%# DataBinder.Eval(Container.DataItem, "Fullname")%> </td>
                    <td><%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "Birthday")).ToShortDateString() %> </td>
                    <td><%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "RememberDay")).ToShortDateString() %> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "JobTitle")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "Mobile")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "Email")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "QQ")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "Character")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "Interest")%> </td>
                    <td><%# DataBinder.Eval(Container.DataItem, "Remark")%> </td>
                </tr>
        </ItemTemplate>
        <FooterTemplate>
             </table>
        </FooterTemplate>
    </asp:Repeater>
    <div id="divNodata" runat="server" bgcolor="#F3F7FF" align="center" style=" height:20px; width:100%;"  visible="false">没有联系人信息!</div>
    </form>
</body>
</html>
