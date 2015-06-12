<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="SiteOperationsCenter.SupplierManage.SchoolList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/usercontrol/SchoolMenu.ascx" TagPrefix="cc1" TagName="SchoolMenu" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>同业学堂资讯</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script language="JavaScript" type="text/javascript">
        //页面成员方法
        var NewsInfoPage = {
            //鼠标选中后的背景样式
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            },
            DeleteAll: function() {
                var DelPermission="<%= CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_删除) %>";
                if(DelPermission=="False")
                {
                    alert("对不起，您还没有该权限！");
                    return  false;
                }
                var count = 0;
                $("#tblList").find(":checkbox[checked]='true'").each(function() {
                    if (this.name == "ckbId")
                        count++;
                });
                if (count <= 0) {
                    alert('请选择要删除的项！');
                    return false;
                }
                else {
                    return confirm('确定要删除选中项吗？');
                }
            },
            //全选
            CheckAll: function(obj) {
                var ck = document.getElementsByTagName("input");
                for (var i = 0; i < ck.length; i++) {
                    if (ck[i].type == "checkbox") {
                        ck[i].checked = obj.checked;
                    }
                }
            }
        };
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:SchoolMenu runat="server" ID="SchoolMenu1" MenuIndex="2"></cc1:SchoolMenu>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr onkeydown="if(event.keyCode == 13){ QueryHandle();return false;event.returnValue=false;return false;}">
            <td colspan="2" width="100%" height="25" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif">
                &nbsp;大类别：<asp:DropDownList runat="server" ID="ddlTopicClass">
                </asp:DropDownList>
                &nbsp;&nbsp; 小类别：<asp:DropDownList runat="server" ID="ddlTopicArea">
                </asp:DropDownList>
                &nbsp;&nbsp; 是否图片：
                <asp:DropDownList runat="server" ID="ddlIsPic">
                    <asp:ListItem Text="请选择" Value="-1"></asp:ListItem>
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp; 关键字：
                <input id="txtKeyWord" name="txtKeyWord" runat="server" class="textfield" size="15" type="text">&nbsp;&nbsp;
                标签：
                <input id="txtTag" runat="server" name="txtTag" class="textfield" size="15" type="text">&nbsp;&nbsp;
                <a id="Query" href="javascript:void(0);" onclick="javascript:QueryHandle();return false;">
                    <img src="<%= ImageServerUrl %>/images/yunying/chaxun.gif" width="62" height="21"></a>
            </td>
        </tr>
        <tr>
            <td width="30%" height="25" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif">
                <table width="99%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="11%">
                            <a href="/SupplierManage/AddSchoolInfo.aspx">新增同业学堂资讯</a>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton runat="server" border="0" ID="hyDelItem" Width="51" Height="25"
                                OnClick="hyDelItem_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="70%" background="<%=ImageServerUrl%>/images/yunying/gongneng_bg.gif" align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <table id="tblList" width="100%" id="Table2" border="1" cellspacing="0" cellpadding="0"
        style="border: 1px solid #ccc;">
        <tr style="background: #C0DEF3; height: 28px; text-align: center; font-weight: bold;">
            <td width="8%">
                <strong>
                    <input type="checkbox" onclick="NewsInfoPage.CheckAll(this)" />
                    序号</strong>
            </td>
            <td width="16%">
                类别
            </td>
            <td width="40%">
                标题
            </td>
            <td width="9%">
                发布时间
            </td>
            <td width="4%">
                查看<br>次数
            </td>
            <td width="7%">
                设置头条
            </td>
            <td width="7%">
                首页显示
            </td>
            <td width="15%">
                操作
            </td>
        </tr>
        <asp:Repeater ID="rpt_NewsList" runat="server" OnItemDataBound="rpt_NewsList_ItemDataBound"
            OnItemCommand="rpt_NewsList_ItemCommand">
            <ItemTemplate>
                <tr style="height: 24px; text-align: center;" onmouseover="NewsInfoPage.mouseovertr(this)"
                    onmouseout="NewsInfoPage.mouseouttr(this)">
                    <td height="25" align="center">
                        <input name="ckbId" type="checkbox" value="<%# Eval("ID") %>" />
                        <asp:Literal runat="server" ID="ltrXH"></asp:Literal>
                    </td>
                    <td align="center">
                        <%# Eval("TopicClassId")%>
                        -
                        <%# Eval("AreaId")%>
                    </td>
                    <td align="left">
                        <%# Eval("ArticleTitle")%>
                    </td>
                    <td>
                        <%# Eval("IssueTime","{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%# Eval("Click")%>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="lkbSetIsTop" Text='<%# bool.Parse(Eval("IsTop").ToString()) == true ? "取消头条" : "设为头条" %>'
                            CommandName="Top" CommandArgument='<%# Eval("ID").ToString() + "," + Eval("IsTop").ToString() %>'></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="lkbSetFrontPage" Text='<%# bool.Parse(Eval("IsFrontPage").ToString()) == true ? "取消显示" : "设为显示" %>'
                            CommandName="FrontPage" CommandArgument='<%# Eval("ID").ToString() + "," + Eval("IsFrontPage").ToString() %>'></asp:LinkButton>
                    </td>
                    <td>
                        <a href="<%# CheckMasterGrant(YuYingPermission.同业学堂_管理该栏目, YuYingPermission.同业学堂_修改) ? " /SupplierManage/AddSchoolInfo.aspx?ID="+Eval("ID").ToString() : "javascript:alert('对不起，您还没有该权限！');" %>">修改</a>&nbsp;/&nbsp;
                        <asp:LinkButton runat="server" ID="lkbDel" Text="删除" CommandName="Del" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoData" runat="server" Visible="false">
            <tr>
                <td colspan="8" align="center" style="height: 100px">
                    暂无数据
                </td>
            </tr>
        </asp:Panel>
        <tr style="background: #C0DEF3; height: 28px; text-align: center; font-weight: bold;">
            <td width="8%">
                <strong>
                    <input type="checkbox" onclick="NewsInfoPage.CheckAll(this)" />
                    序号</strong>
            </td>
            <td width="16%">
                类别
            </td>
            <td width="40%">
                标题
            </td>
            <td width="9%">
                发布时间
            </td>
            <td width="4%">
                查看<br>次数
            </td>
            <td width="7%">
                设置置顶
            </td>
             <td width="7%">
                首页显示
            </td>
            <td width="15%">
                操作
            </td>
        </tr>
    </table>
    <table width="99%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td height="30" align="right">
                <cc2:ExportPageInfo ID="ExportPageInfo" runat="server" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function QueryHandle() {
            var type = $("#<%= ddlTopicClass.ClientID %>").val();
            var area = $("#<%= ddlTopicArea.ClientID %>").val();
            var ispic = $("#<%= ddlIsPic.ClientID %>").val();
            var kw = $("#<%= txtKeyWord.ClientID %>").val();
            var tag = $("#<%= txtTag.ClientID %>").val();
            location.href = "/SupplierManage/SchoolList.aspx?keyword=" + encodeURI(kw) + "&type=" + type + "&area=" + area + "&ispic=" + ispic + "&tag=" + encodeURI(tag);
        }
    </script>

    </form>
</body>
</html>
