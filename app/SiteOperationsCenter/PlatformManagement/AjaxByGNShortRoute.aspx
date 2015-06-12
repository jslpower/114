<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxByGNShortRoute.aspx.cs"
    Inherits="SiteOperationsCenter.PlatformManagement.AjaxByGNShortRoute" %>

<%@ Import Namespace="EyouSoft.Common" %>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" >
  <tr charoff="2">
    <td width="131" height="23" align="right" bgcolor="#C3E3F2"><strong>国内短线</strong>：</td>
    <td align="left" bgcolor="#C3E3F2"><a href="javascript:;" class="add_btn">添 加</a></td>
  </tr>
</table>
<div id="div_OutRoute">
  <%= GetRouteAreaList %>
</div>
<script type="text/javascript">
    $(document).ready(function() {
        $(".add_btn").click(function() {
            var title = "专线区域添加";
            var url = "/PlatformManagement/AddLineArea.aspx?Action=GN";
            Boxy.iframeDialog({ title: title, iframeUrl: url, height: 300, width: 600, draggable: false });
            return false;
        });
    });

    $(".updateRouteAreaI").click(function() {
        var title = "专线区域修改";
        var AreaID = $(this).attr("RouteAreaId");
        var url = "/PlatformManagement/AddLineArea.aspx?action=update&AreaID=" + AreaID;
        Boxy.iframeDialog({ title: title, iframeUrl: url, height: 300, width: 600, draggable: false });
        return false;
    });

    $(".deleteRouteAreaI").click(function() {
        if (confirm('您确定要删除此线路区域吗？\n\n此操作不可恢复！')) {
            var RouteAreaId = $(this).attr("RouteAreaId");
            if (RouteAreaId != "" && RouteAreaId != undefined) {
                $.ajax({
                    url: "AjaxByGNShortRoute.aspx?DeletID=" + RouteAreaId,
                    cache: false,
                    success: function(html) {
                        if (html == "False") {
                            alert("删除失败，请查看此线路区域下是否包含未出团的团队！");
                            return false;
                        } else {
                            alert("删除成功");
                            ajaxPages.init(2);
                        }
                    }
                });
            }
        }
        return false;
    });
</script>
