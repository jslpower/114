<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IMFrame.RouteAgency.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批发商</title>
</head>
<body oncontextmenu="return false;" style="margin: 0px;">
    <form id="form1" runat="server">
    <div id="divData" runat="server">
        <table width="210" height="17" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="toolbj" align="left">
                    <a href="javascript:void(0);" id="Add" runat="server" class="menubarleft" target="_blank">
                        <img src="<%=ImageServerUrl %>/IM/images/add.gif" width="13" height="17" border="0" />发布计划</a>
                    <a href="#" id="Update" runat="server" class="menubarleft" onclick="update()">
                        <img src="<%=ImageServerUrl %>/IM/images/modified.gif" border="0" />修改</a> <a href="#"
                            id="Copy" runat="server" class="menubarleft" onclick="copy()">
                            <img src="<%=ImageServerUrl %>/IM/images/copy.gif" border="0" />复制</a>
                </td>
            </tr>
        </table>
        <input type="hidden" value="" id="hidOperatorId" />
        <table width="200" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <div id="mainMenu">
                        <ul id="menuList">
                            <asp:Repeater ID="rptAreaList" runat="server" OnItemDataBound="rptAreaList_ItemDataBound">
                                <ItemTemplate>
                                    <li class="menubarname" id='<%# "li_"+Eval("AreaId").ToString() %>'><a href="javascript:void(0)"
                                        id="<%# "li_AreaID"+Eval("AreaId").ToString() %>" onfocus="this.blur()">
                                        <img src="<%=ImageServerUrl %>/IM/images/leftmenuico.gif" border="0" /><%# Eval("AreaName").ToString() %>
                                        <input type="hidden" id="hidAreaID" name="hidAreaID" value='<%# Eval("AreaId") %>' />
                                    </a>
                                        <ul class="menulist">
                                            <asp:Repeater ID="rptTourList" runat="server">
                                                <ItemTemplate>
                                                    <li style="white-space: nowrap;">
                                                        <table style="border: 0px; float: left; width: 205px">
                                                            <tr>
                                                                <td align="right" style="width: 20px">
                                                                    <input name="chkTourID" type="checkbox" value="<%# Eval("RouteId") %>" />
                                                                </td>
                                                                <td align="left">
                                                                    <a href='<%# GetDesPlatformUrl(EyouSoft.Common.Domain.UserBackCenter + "/PrintPage/RouteDetail.aspx?RouteId=" + Eval("RouteId")) %>'
                                                                        target="_blank" class="lineon">
                                                                        <%# EyouSoft.Common.Utils.GetText2(Eval("RouteName").ToString(),15,false) %>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="pnlNoData" runat="server" Visible="false">
                                <li>暂无团队 </li>
                            </asp:Panel>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divNoData" runat="server" visible="false">
        <br />
        对不起，您不是专线用户，不能进行专线操作!</div>
    </form>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("IMCommon") %>"></script>

    <script language="javascript" type="text/javascript">
        var AreaId = "";
        var strAreaId = "";
        var IsTourPermissions = "<%= IsTourPermissions %>";

        if (IsTourPermissions.toLowerCase() == "false") {
            $("#mainMenu").html("\n\n\n您未开通专线_线路管理权限，\n请联系总帐号开通。");
        }

        $("#menuList li").each(function(i) {
            if (i == 0) {
                $(this).find("ul").attr("class", "menulist");
            } else {
                $(this).find("ul").attr("class", "submenu");
            }

            $(this).find("a[id^=li_AreaID]").click(function() {
                if ($(this).parent("li").find("ul").attr("class") == "menulist") {
                    $(this).parent("li").find("ul").attr("class", "submenu");
                } else {
                    $(this).parent("li").find("ul").attr("class", "menulist");
                    $(this).parent("li").siblings("li").find("ul").attr("class", "submenu");
                }
            });
        });

        function GetCheck() {
            var arr = new Array();
            $("input[type=checkbox][name=chkTourID]:checked").each(function() {
                arr.push($(this).val());
            });
            return arr;
        }

        function update() {
            var array = GetCheck();
            var count = array.length;
            if (count == 0) {
                alert("请选择要修改的线路");
            }
            else if (count > 1) {
                alert("每次只能修改一条线路");
            }
            else {
                var RoutesId = array[0];
                var url = '<%=EyouSoft.Common.Domain.UserBackCenter %>/RouteAgency/RouteManage/AddTourism.aspx?type=update&RouteSource=1&RouteId=' + RoutesId;

                window.open(GetDesPlatformUrlForMQMsg(url, '<%=MQLoginId %>', '<%=Password %>'));
            }
        }

        function copy() {
            var array = GetCheck();
            var count = array.length;
            if (count == 0) {
                alert("请选择要复制的线路");
            }
            else if (count > 1) {
                alert("每次只能复制一条线路");
            }
            else {
                var RouteId = array[0];
                var url = '<%=EyouSoft.Common.Domain.UserBackCenter %>/RouteAgency/RouteManage/AddTourism.aspx?type=copy&RouteSource=1&RouteId=' + RouteId;

                window.open(GetDesPlatformUrlForMQMsg(url, '<%=MQLoginId %>', '<%=Password %>'));
            }
        }
        $(function() {
            $(".lineon").hover(function() {
                $(this).closest("li").css({ background: "#FFEBCC", border: "1px solid #C3C9DE" })
            }, function() {
                $(this).closest("li").css({ background: "none", border: "none" })
            })
        })
    </script>

</body>
</html>
