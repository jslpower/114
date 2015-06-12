<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HonoredGuest.aspx.cs" Inherits="SiteOperationsCenter.SupplierManage.HonoredGuest" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/GuestInterviewMenu.ascx" TagPrefix="cc1" TagName="GuestInterviewMenu" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>嘉宾访谈</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:GuestInterviewMenu runat="server" ID="GuestInterviewMenu1"></cc1:GuestInterviewMenu>
    <table width="100%" align="center" border="0" cellpadding="0" cellspacing="0" onkeydown="if(event.keyCode == 13){ QueryHandle();return false;event.returnValue=false;return false;}">
        <tr>
            <td width="23%" background="<%= ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                height="25">
                <table width="98%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%= ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20">
                        </td>
                        <td width="12%">
                            <a href="/SupplierManage/AddHonoredGuest.aspx">
                                <img src="<%= ImageServerUrl %>/images/yunying/xinzengjiabing.gif" width="113" border="0"
                                    height="25"></a>
                        </td>
                        <td width="12%">
                            <a href="javascript:void(0);" onclick="javascript:EditHandle();return false;">
                                <img src="<%= ImageServerUrl %>/images/yunying/xiugai.gif" width="50" border="0"
                                    height="25"></a>
                        </td>
                        <td width="5%">
                            <img src="<%= ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25">
                        </td>
                        <td width="23%">
                            <asp:ImageButton runat="server" ID="ibtnDel" Width="51" Height="25" OnClick="ibtnDel_Click" />
                        </td>
                        <td width="17%">
                            <img src="<%= ImageServerUrl %>/images/yunying/ge_d.gif" width="11" height="25">
                        </td>
                    </tr>
                </table>
            </td>
            <td width="77%" align="left" background="<%= ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                &nbsp;关键字：
                <input id="txtKeyWord" name="txtKeyWord" runat="server" class="textfield" size="15" type="text">
                <a id="Query" href="javascript:void(0);" onclick="javascript:QueryHandle();return false;">
                    <img src="<%= ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21"></a>
            </td>
        </tr>
    </table>
    <table id="tblList" class="kuang" width="100%" align="center" border="0" cellpadding="0"
        cellspacing="1">
        <tr class="white" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif" height="23">
            <td valign="middle" width="5%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>序号</strong>
            </td>
            <td valign="middle" width="14%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>嘉宾访谈</strong>
            </td>
            <td valign="middle" width="41%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>嘉宾介绍</strong>
            </td>
            <td width="27%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>小编总结</strong>
            </td>
            <td width="13%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>评论</strong>
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="RepeaterList_ItemDataBound">
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td align="center" height="25">
                        <input name="ckbId" value="<%# Eval("ID") %>" type="checkbox">
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td align="left">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>" title="<%# Eval("Title") %>">
                            <%# Utils.GetText(Eval("Title").ToString(),10,true) %></a>
                    </td>
                    <td align="center" height="25">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>" title="<%# EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Content").ToString()) %>">
                            <%# Utils.GetText(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Content").ToString()), 20, true)%></a>
                    </td>
                    <td align="left">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>"  title="<%# EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Summary").ToString()) %>">
                            <%# Utils.GetText(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Summary").ToString()), 20, true)%></a>
                    </td>
                    <td align="center">
                        <a href="/SupplierManage/CommentList.aspx?ID=<%# Eval("ID") %>&Type=2" target="_blank">
                            <%# Eval("CommentCount")%></a>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)" bgcolor="#f3f7ff">
                    <td align="center" height="25">
                        <input name="ckbId" value="<%# Eval("ID") %>" type="checkbox">
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td align="left">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>" title="<%# Eval("Title") %>">
                            <%# Utils.GetText(Eval("Title").ToString(),10,true) %></a>
                    </td>
                    <td align="center" height="25">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>" title="<%# EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Content").ToString()) %>">
                            <%# Utils.GetText(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Content").ToString()),20, true)%></a>
                    </td>
                    <td align="left">
                        <a href="/SupplierManage/AddHonoredGuest.aspx?ID=<%# Eval("ID") %>" title="<%# EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Summary").ToString()) %>">
                             <%# Utils.GetText(EyouSoft.Common.Function.StringValidate.LoseHtml(Eval("Summary").ToString()), 20, true)%></a>
                    </td>
                    <td align="center">
                        <a href="/SupplierManage/CommentList.aspx?ID=<%# Eval("ID") %>&Type=2" target="_blank">
                            <%# Eval("CommentCount")%></a>
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
            <td valign="middle" width="5%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>序号</strong>
            </td>
            <td valign="middle" width="14%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>嘉宾访谈</strong>
            </td>
            <td valign="middle" width="41%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>嘉宾介绍</strong>
            </td>
            <td width="27%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>小编总结</strong>
            </td>
            <td width="13%" align="center" background="<%= ImageServerUrl %>/images/yunying/hangbg.gif">
                <strong>评论</strong>
            </td>
        </tr>
        <tr>
            <td height="30" align="right" colspan="5">
                <cc2:ExportPageInfo ID="ExportPageInfo" runat="server" />
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">

        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }

        function QueryHandle() {
            var kw = document.getElementById("<%= txtKeyWord.ClientID %>").value;
            location.href = "/SupplierManage/HonoredGuest.aspx?keyword=" + encodeURI(kw);
        }

        function DelConfirm() {
            var delPermission="<%= CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_删除) %>";
            if(delPermission=="False")
            {
                alert("对不起，您还没有该权限！");
                return false;
            }
            var count = $("#tblList").find(":checkbox[checked]='true'").length;
            if (count <= 0) {
                alert('请选择要删除的项！');
                return false;
            }
            else {
                return confirm('确定要删除选中项吗？');
            }
        }

        function EditHandle() {
            var editPermission="<%= CheckMasterGrant(YuYingPermission.嘉宾访谈_管理该栏目, YuYingPermission.嘉宾访谈_修改) %>";
            if(editPermission=="False")
            {
                    alert("对不起，您还没有该权限！");
                    return false;
            }
            var count = $("#tblList").find(":checkbox[checked]='true'").length;
            if (count <= 0) {
                alert('请选择要修改的项！');
                return false;
            }
            else if (count > 1) {
                alert('一次只能修改一条记录！');
                return false;
            }
            else {
                    location.href = "/SupplierManage/AddHonoredGuest.aspx?ID=" + encodeURI($("input:checkbox[checked]='true'").val());
            }
        }
    </script>

    </form>
</body>
</html>
