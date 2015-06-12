<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UniversalLineManage.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.UniversalLineManage" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通用专线维护</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1">
        <tr charoff="2">
            <td width="131" height="23" align="right" bgcolor="#C3E3F2">
                <strong>国内长线</strong>：
            </td>
            <td align="left" bgcolor="#C3E3F2">
                <a href="javascript:;" class="add_btn" routeType="0">添 加</a>
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                <div id="divGuoNei"> <%=GetArealistHtml%>
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1">
        <tr charoff="2">
            <td width="131" height="23" align="right" bgcolor="#C3E3F2">
                <strong>出境短线</strong>：
            </td>
            <td align="left" bgcolor="#C3E3F2">
                <a href="javascript:;" class="add_btn" routeType="1">添 加</a>
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                <div id="div_CJRoute"> <%= GetOutRouteList %>
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" align="center" cellpadding="1" cellspacing="1">
        <tr charoff="2">
            <td width="131" height="23" align="right" bgcolor="#C3E3F2">
                <strong>国内短线</strong>：
            </td>
            <td align="left" bgcolor="#C3E3F2">
                <a href="javascript:;" class="add_btn" routeType="2">添 加</a>
            </td>
        </tr>
    </table>
    <table width="95%" border="1" align="center" cellpadding="0" cellspacing="0" bordercolor="#C8E0EB">
        <tr>
            <td>
                <div id="div_GNShortRoute"><%= GetRouteAreaList %>
                </div>
            </td>
        </tr>
    </table>
    <table width="95%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(document).ready(function() {
            $(".add_btn").click(function() {
                var title = "专线区域添加";
                var routeType = $(this).attr("routeType");
                var url = "/PlatformManagement/AddLineArea.aspx?routeType=" + routeType;
                Boxy.iframeDialog({ title: title, iframeUrl: url, height: 350, width: 600, draggable: false });
                return false;
            });

            $(".updateRouteArea").click(function() {
                var title = "专线区域修改";
                var RouteAreaID = $(this).attr("RouteAreaId");
                var routeType = $(this).attr("routeType");
                var url = "/PlatformManagement/AddLineArea.aspx?routeType=" + routeType + "&action=update&AreaID=" + RouteAreaID;
                Boxy.iframeDialog({ title: title, iframeUrl: url, height: 350, width: 600, draggable: false });
                return false;
            });

            $(".deleteRouteArea").click(function() {
                if (confirm('您确定要删除此线路区域吗？\n\n此操作不可恢复！')) {
                    var RouteAreaId = $(this).attr("RouteAreaId");
                    if (RouteAreaId != "" && RouteAreaId != undefined) {
                        $.ajax({
                            url: "/PlatformManagement/UniversalLineManage.aspx?DeletID=" + RouteAreaId,
                            cache: false,
                            success: function(html) {
                                if (html == "False") {
                                    alert("删除失败，请查看此线路区域下是否包含未出团的团队！");
                                    return false;
                                } else {
                                    alert("删除成功");
                                    window.location.href = window.location.href;
                                }
                            }
                        });
                    }
                }
                return false;
            });
        });
    </script>
</body>
</html>
