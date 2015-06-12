<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongyeCenterManager.aspx.cs"
    Inherits="SiteOperationsCenter.TongyeCenter.TongyeCenterManager" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript">
        var TongyeNoticeM = {
            GetSelectValue: function() {
                var arraylist = new Array();
                $(":checkbox[name='chkId'][checked='true']").each(function() {
                    arraylist.push($(this).val());
                });
                return arraylist
            },
            AddCenter: function() {
                window.location.href = "TongyeCenterEdit.aspx";
                return false;
            },
            DelCenter: function(id) {
                if (confirm("确定要删除该记录!")) {
                    if (id.constructor == Array) {
                        id = id.join(",");
                    }
                    var params = { ids: id, state: 'Del' };
                    window.location.href = "TongyeCenterManager.aspx?" + $.param(params);
                    return false;
                }
            },
            EditCenter: function(id) {
                var params = { id: id };
                window.location.href = "TongyeCenterEdit.aspx?" + $.param(params);
                return false;
            },
            //鼠标选中后的背景样式
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="98%" cellspacing="0" cellpadding="0" border="0" align="center">
            <tbody>
                <tr>
                    <td height="25" width="23%" background="<%=ImageServerUrl%>/images/gongneng_bg.gif">
                        <table width="51%" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td width="4%" align="right">
                                        <img height="20" width="3" src="<%=ImageServerUrl%>/images/yunying/ge_da.gif" />
                                    </td>
                                    <td width="23%">
                                        <a href="javascript:" id="AddCenter">
                                            <img height="25" width="50" border="0" src="<%=ImageServerUrl%>/images/yunying/xinzeng.gif" /></a>
                                    </td>
                                    <td width="4%">
                                        <img height="25" width="2" src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif">
                                    </td>
                                    <td width="24%">
                                        <a href="javascript:" id="EditCenter">
                                            <img height="25" width="50" border="0" src="<%=ImageServerUrl%>/images/yunying/xiugai.gif"></a>
                                    </td>
                                    <td width="5%">
                                        <img height="25" width="2" src="<%=ImageServerUrl%>/images/yunying/ge_hang.gif">
                                    </td>
                                    <td width="23%">
                                        <a href="javascript:" id="DelCenter">
                                            <img height="25" width="51" src="<%=ImageServerUrl%>/images/yunying/shanchu.gif" /></a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td align="left" width="77%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif"
                        align="center">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存序号" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="98%" cellspacing="1" cellpadding="0" border="0" align="center" class="kuang">
            <tbody>
                <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white">
                    <td height="23" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif"
                        align="center">
                        <input type="checkbox" value="<%#Eval("ID")%>" name="chkAll" /><strong>序号</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>名称</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>成员构成</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>总管理员</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>密码</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>操作人</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>时间</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>操作</strong>
                    </td>
                </tr>
                <cc1:CustomRepeater ID="repList" runat="server">
                    <ItemTemplate>
                        <tr onmouseout="TongyeNoticeM.mouseouttr(this)" onmouseover="TongyeNoticeM.mouseovertr(this)">
                            <td height="25" align="center">
                                <input type="checkbox" value="<%#Eval("ID")%>" name="chkId">
                                <input type="text" size="2" maxlength="3" value="<%#Eval("Num")%>" name="sort">
                                <input id="hidid" name="hidid" type="hidden" value="<%#Eval("ID")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("Title")%>
                            </td>
                            <td align="center">
                                <%# string.IsNullOrEmpty(Eval("CountValue").ToString()) ? "无" : Eval("CountValue")%>
                            </td>
                            <td align="center">
                                <%#Eval("Master")%>
                            </td>
                            <td align="center">
                                <%#Eval("PassWord")%>
                            </td>
                            <td align="center">
                                <%#Eval("Opertor")%>
                            </td>
                            <td align="center">
                                <%#Eval("OperateTime")%>
                            </td>
                            <td align="center">
                                <a href="javascript:" onclick="return TongyeNoticeM.EditCenter('<%#Eval("ID")%>');">
                                    修改</a>|<a href="javascript:" onclick="return TongyeNoticeM.DelCenter('<%#Eval("ID")%>')">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </cc1:CustomRepeater>
                <tr background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" class="white">
                    <td height="23" valign="middle" background="<%=ImageServerUrl%>/images/yunying/hangbg.gif"
                        align="center">
                        <input type="checkbox" value="<%#Eval("ID")%>" name="chkAll" /><strong>序号</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>名称</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>成员构成</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>总管理员</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>密码</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>操作人</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>时间</strong>
                    </td>
                    <td background="<%=ImageServerUrl%>/images/yunying/hangbg.gif" align="center">
                        <strong>操作</strong>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td align="right">
                        <cc2:ExportPageInfo ID="ExportPageInfo1" runat="server" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>

    <script type="text/javascript">

        $(document).ready(function() {
            $(":checkbox[name='chkAll']").click(function() {
                var isChecked = $(this).attr("checked");
                $(":checkbox[name='chkId']").each(function() {
                    $(this).attr("checked", isChecked)
                });
                $(":checkbox[name='chkAll']").each(function() {
                    $(this).attr("checked", isChecked)
                });
            });
            $("#AddCenter").click(function() {
                TongyeNoticeM.AddCenter();
                return false;
            });
            $("#EditCenter").click(function() {
                var arraylist = TongyeNoticeM.GetSelectValue();
                if (arraylist.length > 1) {
                    alert("只能选择一条记录操作!");
                    return false;
                } else if (arraylist.length == 0) {
                    alert('请选择要修改的记录!');
                    return false;
                }
                else {
                    TongyeNoticeM.EditCenter(arraylist[0]);
                }
                return false;
            });
            $("#DelCenter").click(function() {
                var arraylist = TongyeNoticeM.GetSelectValue();
                if (arraylist.length == 0) {
                    alert('请选择要删除的记录!');
                    return false;
                } else {
                    TongyeNoticeM.DelCenter(arraylist);
                }
                return false;
            });
            
            $("#btnSave").click(function() {
                var arraylist = TongyeNoticeM.GetSelectValue();
                if (arraylist.length == 0) {
                    alert('请选择要保存序号的记录!');
                                                return false;
                                            } 
                });

        });     
    </script>

</body>
</html>
