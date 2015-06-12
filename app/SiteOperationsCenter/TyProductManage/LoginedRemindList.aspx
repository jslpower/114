<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginedRemindList.aspx.cs"
    Inherits="SiteOperationsCenter.TyProductManage.LoginedRemindList" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>提醒管理列表</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script language="JavaScript">
        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
            //o.style.cursor="hand";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
        $(function() {
            $("#tbList").find(":checkbox").each(function() {

                $(this).click(function() {
                    if ($(this).attr("checked")) {
                        $(this).val("1");
                    }
                    else {
                        $(this).val("0");
                    }
                });

            });
            $("#tbList").find(":select").each(function() {
                $(this).change(function() {
                    $(this).prev().val($(this).val());
                    return false;
                });

            });

            $("#btnSave").click(function() {

                var IsSubmit = false;
                var msg = "请选择您要操作的行！";
                $("#tbList").find(":checkbox[checked='true']").each(function() {
                    if ($(this).parent().parent().find(":hidden[name='hType'][value!='-1']").length > 0) {
                        msg = "请选择您要操作的行！";
                        IsSubmit = true;
                    }
                    else {
                        msg = "请选择类型!";
                        IsSubmit = false;
                    }
                });
                if (!IsSubmit) {
                    alert(msg);
                }
                return IsSubmit;
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="30%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="23%" height="25" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif">
                                <table width="51%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="4%" align="right">
                                            <img src="<%=ImageServerUrl %>/images/yunying/ge_da.gif" width="3" height="20" />
                                        </td>
                                        <td width="23%">
                                            <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" />
                                        </td>
                                        <td width="4%">
                                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                                        </td>
                                        <td width="24%">
                                            &nbsp;
                                        </td>
                                        <td width="5%">
                                            <img src="<%=ImageServerUrl %>/images/yunying/ge_hang.gif" width="2" height="25" />
                                        </td>
                                        <td width="23%">
                                            &nbsp;
                                        </td>
                                        <td width="17%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="77%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                                align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="70%" background="<%=ImageServerUrl %>/images/yunying/gongneng_bg.gif"
                    align="left">
                </td>
            </tr>
        </table>
        <cc1:CustomRepeater ID="crp_LoginRemindList" runat="server" OnItemDataBound="crp_LoginRemindList_ItemDataBound">
            <HeaderTemplate>
                <table id="tbList" width="98%" border="0" align="center" cellpadding="0" cellspacing="1"
                    class="kuang">
                    <tr background="<%=ImageServerUrl %>/images/yunying/hangbg.gif" class="white" height="23">
                        <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>序号</strong>
                        </td>
                        <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>标题</strong>
                        </td>
                        <td align="center" valign="middle" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>类型</strong>
                        </td>
                        <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>时间</strong>
                        </td>
                        <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>操作人</strong>
                        </td>
                        <td align="center" background="<%=ImageServerUrl %>/images/yunying/hangbg.gif">
                            <strong>是否显示</strong>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="baidi" onmouseover="mouseovertr(this)" onmouseout="mouseouttr(this)">
                    <td height="25" align="center">
                        <strong></strong>
                        <input type="text" name="txtSerialNo" value="0" size="3" />
                    </td>
                    <td height="25" align="center">
                        <%#Eval("EventTitle") %>
                    </td>
                    <td height="25" align="center">
                        <input type="hidden" name="hType" value="-1" />
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                         <input type="text" name="txtEventTime" value='<%# Eval("EventTime","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                    </td>
                    <td align="center">
                        <input type="hidden" name="hOperatorName" value="<%# Eval("OperatorName") %>" />
                        <lable name="lbOperatorName"><%# Eval("OperatorName") %></lable>
                    </td>
                    <td align="center">
                        <input type="checkbox" value="0" name="cbIsShow">
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </cc1:CustomRepeater>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
