<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommunityAdvisor.aspx.cs"
    Inherits="SiteOperationsCenter.SupplierManage.CommunityAdvisor" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/GuestInterviewMenu.ascx" TagPrefix="cc1" TagName="GuestInterviewMenu" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>顾问团队</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <cc1:GuestInterviewMenu runat="server" ID="GuestInterviewMenu1"></cc1:GuestInterviewMenu>
    <br />
    <a href="/SupplierManage/AddCommunityAdvisor.aspx" title="新增顾问团队">新增顾问团队</a>
    <br />
    <table style="border: 1px solid rgb(204, 204, 204);" width="100%" border="1" cellpadding="0"
        cellspacing="0">
        <tr style="background: none repeat scroll 0% 0% rgb(192, 222, 243); height: 28px;
            text-align: center; font-weight: bold;">
            <td width="5%">
                序号
            </td>
            <td width="23%">
                姓名
            </td>
            <td width="14%">
                照片
            </td>
            <td width="46%">
                介绍
            </td>
            <td width="12%">
                操作
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="RepeaterList_ItemDataBound"
            OnItemCommand="RepeaterList_ItemCommand">
            <ItemTemplate>
                <tr style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 24px;
                    text-align: center;">
                    <td>
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td>
                        <%# Eval("ContactName")%>
                    </td>
                    <td>
                        <a target="_blank" href="<%# Domain.FileSystem %><%# Eval("ImgPath") %>">
                            <img alt="" src="<%# Domain.FileSystem %><%# Eval("ImgPath") %>" width="110" height="85"></a>
                    </td>
                    <td>
                        <%# Eval("Job")%><br />
                        <%# Eval("Achieve")%>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="lkbCancel" Text='<%# (!string.IsNullOrEmpty(Eval("IsShow").ToString()) && bool.Parse(Eval("IsShow").ToString())) ? "取消显示" : "前台显示" %>'
                            CommandName="Show" CommandArgument='<%# Eval("ID").ToString() + "," + Eval("IsShow").ToString() %>'></asp:LinkButton>
                        <a href="<%# CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_顾问团队审核)?" /SupplierManage/AddCommunityAdvisor.aspx?ID="+Eval("ID").ToString():"javascript:alert('对不起，您还没有该权限！');" %>" title="修改">修改</a>
                        <asp:LinkButton runat="server" ID="lkbDel" Text="删除" CommandName="Del" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="trNoData" Visible="false">
            <tr style="background: none repeat scroll 0% 0% rgb(255, 255, 255); height: 24px;
                text-align: center;">
                <td colspan="5">
                    暂无数据
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td height="30" align="right" colspan="5">
                <cc2:ExportPageInfo ID="ExportPageInfo" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
