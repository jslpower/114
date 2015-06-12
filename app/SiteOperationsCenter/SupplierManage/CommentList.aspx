<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentList.aspx.cs" Inherits="SiteOperationsCenter.SupplierManage.CommentList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>评论列表</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="tblList" class="kuang" width="100%" align="center" border="0" cellpadding="0"
        cellspacing="1">
        <tr class="white" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif" height="23">
            <td valign="middle" width="3%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>序号</strong>
            </td>
            <td valign="middle" width="10%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>用户名</strong>
            </td>
            <td valign="middle" width="40%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>正文</strong>
            </td>
            <td width="5%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>操作</strong>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="RepeaterList_ItemDataBound" OnItemCommand="RepeaterList_ItemCommand">
            <ItemTemplate>
                <tr class="baidi">
                    <td align="center" height="25">
                       <%-- <input name="ckbId" value="<%# Eval("ID") %>" type="checkbox">--%>
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td align="left">
                        <%# Eval("OperatorName")%>
                    </td>
                    <td align="center" height="25">
                        <%# Eval("CommentText")%>
                    </td>
                    <td align="center">
                        <%--<asp:Button runat="server" ID="btnChecked" Text="通过" CommandName="ckd" CommandArgument='<%# Eval("ID") %>' />
                        <asp:Button runat="server" ID="btnNoChecked" Text="不通过" CommandName="nckd" CommandArgument='<%# Eval("ID") %>' />--%>
                        <asp:Button runat="server" ID="btnDel" Text="删除" OnClientClick="return confirm('您确定删除该条数据？');"  CommandName="del" CommandArgument='<%# Eval("ID") %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="#f3f7ff">
                    <td align="center" height="25">
                        <%--<input name="ckbId" value="<%# Eval("ID") %>" type="checkbox">--%>
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td align="left">
                        <%# Eval("OperatorName")%>
                    </td>
                    <td align="center" height="25">
                        <%# Eval("CommentText")%>
                    </td>
                    <td align="center">
                        <%--<asp:Button runat="server" ID="btnChecked" Text="通过" CommandName="ckd" CommandArgument='<%# Eval("ID") %>' />
                        <asp:Button runat="server" ID="btnNoChecked" Text="不通过" CommandName="nckd" CommandArgument='<%# Eval("ID") %>' />--%>
                        <asp:Button runat="server" ID="btnDel" Text="删除" OnClientClick="return confirm('您确定删除该条数据？');" CommandName="del" CommandArgument='<%# Eval("ID") %>' />
                    </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="trNoData" Visible="false">
            <tr>
                <td colspan="5" align="center" style="height: 150px" visible="false">
                    暂无数据
                </td>
            </tr>
        </asp:Panel>
        <tr class="white" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif" height="23">
            <td valign="middle" width="3%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>序号</strong>
            </td>
            <td valign="middle" width="10%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>用户名</strong>
            </td>
            <td valign="middle" width="40%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>正文</strong>
            </td>
            <td width="5%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>操作</strong>
            </td>
        </tr>
        <tr>
            <td height="30" align="right" colspan="5">
                <cc2:ExportPageInfo ID="ExportPageInfo" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
