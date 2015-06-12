<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleSet.aspx.cs" Inherits="UserBackCenter.SystemSet.RoleSet" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="asp" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .errmsg
        {
            color: #FF0000;
        }
    </style>
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            FV_onBlur.initValid($("#<%=rs_form.ClientID%>").get(0));

            $(".checkall1").click(function() {
                $(this).closest("table").find(":checkbox").attr("checked", $(this).attr("checked"));
            });
            $(".checkall").click(function() {
                $(this).closest(".parent_table").find(":checkbox").attr("checked", $(this).attr("checked"));
            });
            $(".checkall1").each(function() {

                var isAll = true;
                var sonNum = 0;
                $(this).closest("tr").siblings().find(":checkbox").each(function() {
                    if (!$(this).attr("checked")) {
                        isAll = false;
                        sonNum++;
                    }
                });
                if (isAll && sonNum > 0) {
                    $(this).attr("checked", "checked");
                }

            })


        }
);
        var RoleSet =
 {
     save: function() {
         var form = $("#<%=rs_form.ClientID %>").get(0);
         if (ValiDatorForm.validator(form, "span")) {
             return true;
         }
         return false;
     }
 }
    </script>

</head>
<body>
    <form action="" onsubmit="return RoleSet.save()" id="rs_form" runat="server">
    <table width="855" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top: 20px;">
        <tr>
            <td width="113" align="right">
                <span class="ff0000"><strong>*</strong></span>请输入角色名称：
            </td>
            <td width="742" align="left">
                <input name="rs_txtRoleName" id="rs_txtRoleName" type="text" class="bitian" runat="server"
                    valid="required" errmsg="请填写角色名称" size="40" maxlength="10" />
                <span id="errMsg_rs_txtRoleName" class="errmsg"></span>
            </td>
        </tr>
    </table>
    <table width="855" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="2">
            </td>
        </tr>
        <tr>
            <td>
                <table width="855" border="0" align="center" cellpadding="0" cellspacing="0" class="parent_table">
                    <tr>
                        <td colspan="3">
                            <table width="855" height="23" border="0" cellpadding="0" cellspacing="0" background="<%=ImageServerUrl%>/images/quanxian_01.gif">
                                <tr>
                                    <td>
                                        <table width="855" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="108" align="center" class="white">
                                                    <strong>
                                                        <asp:Literal runat="server" ID="ltrCategoryName"></asp:Literal>
                                                    </strong>
                                                </td>
                                                <td width="65" class="fontcolor">
                                                    <input type="checkbox" class="checkall" value="checkbox" runat="server" />
                                                    全选
                                                </td>
                                                <td width="340">
                                                    <span class="fontcolor"></span>
                                                </td>
                                                <td width="171">
                                                    &nbsp;
                                                </td>
                                                <td width="171">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="7" background="<%=ImageServerUrl%>/images/quanxian_02.gif">
                        </td>
                        <td width="842">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <asp:CustomRepeater runat="server" ID="rs_PermitList">
                                    <HeaderTemplate>
                                        <tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <td valign="top">
                                            <table width="98%">
                                                <tr>
                                                    <td align="left" width="10%">
                                                        <input name='checkallper_<%#Eval("Id") %>' class="checkall1" type="checkbox" value='<%# Eval("Id") %>'>
                                                    </td>
                                                    <td align="left" class="fontcolor">
                                                        <strong>
                                                            <%# Eval("ClassName")%></strong>
                                                    </td>
                                                </tr>
                                                <%#GetCateSonItem(Eval("SysPermission"))%>
                                            </table>
                                        </td>
                                        <%#GetItem()%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tr></FooterTemplate>
                                </asp:CustomRepeater>
                            </table>
                        </td>
                        <td width="6" background="<%=ImageServerUrl%>/images/quanxian_04.gif">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="<%=ImageServerUrl%>/images/quanxian_05.gif" width="855" height="13" alt="" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="25%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <input name="rs_btnSave" id="rs_btnSave" type="submit" class="baocun_an" value="保 存" />
            </td>
            <td align="center">
            </td>
            <td align="center">
                <input name="rs_btnClose" type="button" class="baocun_an close" value="取消" onclick="window.parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide()" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
