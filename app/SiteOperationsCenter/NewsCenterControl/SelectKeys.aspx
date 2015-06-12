<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectKeys.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.SelectKeys" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <form id="form1" runat="server" style="font-size: 12px">
    <input id="hidKeyTagName" type="hidden" /><input id="hidKeyTagId" type="hidden" />
    <div align="center">
        <table>
            <tr>
                <td>
                    <input id="txtSearch" type="text" /><input id="btnSearch" type="button" value="搜索" />
                </td>
            </tr>
            <tr>
                <td>
                    <table id="KeyTaglist">
                        <%=Html%></table>
                </td>
            </tr>
            <tr>
                <td colspan="10" align="center">
                    <input id="btnSelect" type="button" value="选择" />
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        var SelectKey = {
            queryString: function(val) {
                var uri = window.location.search;
                var re = new RegExp("" + val + "\=([^\&\?]*)", "ig");
                return ((uri.match(re)) ? (uri.match(re)[0].substr(val.length + 1)) : null);
            },
            //初始化
            KeyTagInit: function() {
                $("#txtSearch").focus();
                var NameId = window.parent.$("#" + SelectKey.queryString("keyName"));
                var IdId = window.parent.$("#" + SelectKey.queryString("keyId"));
                var arrayList = new Array();
                arrayList = IdId.val().split(',');
                for (var i in arrayList) {
                    $("input[id='" + arrayList[i] + "']").attr("checked", true);
                }
                $("#hidKeyTagName").val(NameId.val());
                $("#hidKeyTagId").val(IdId.val());
            },
            //Ajax请求
            AjaxPost: function() {
                $.ajax({
                    type: "Get",
                    url: "SelectKeys.aspx",
                    cache: false,
                    dataType: "html",
                    data: "keyOrTags=<%=Request.QueryString["keyOrTags"] %>&keyOrTagsTitle=" +encodeURIComponent($("#txtSearch").val()) + "&isAjax=true",
                    success: function(data) {
                        $("#KeyTaglist").html(data);
                    }
                });
            }
        }
        $(function() {
            SelectKey.KeyTagInit();
            $("#btnSelect").click(function() {
                var NameId = window.parent.$("#" + SelectKey.queryString("keyName"));
                var IdId = window.parent.$("#" + SelectKey.queryString("keyId"));
                var idList = "";
                var NameList = "";
                var hidKeyTagName = $("#hidKeyTagName").val();
                var hidKeyTagId = $("#hidKeyTagId").val();
                $("input[name='selKey']").each(function() {
                    if ($(this).attr("checked") == true) {
                        //当前选中的是否已选过
                        var isZxist = false;
                        var arrayList = new Array();
                        arrayList = hidKeyTagId.split(',');
                        for (var i in arrayList) {
                            if ($(this).attr("id") == arrayList[i]) {
                                isZxist = true;
                                break;
                            }
                        }
                        if (!isZxist) {
                            idList += $(this).attr("id") + ",";
                            NameList += $(this).next("label").eq(0).text() + " ";
                        }
                    }
                });
                NameList += hidKeyTagName;
                idList += hidKeyTagId;

                NameId.val(NameList);
                IdId.val(idList);
                window.parent.Boxy.getIframeDialog(SelectKey.queryString('iframeId')).hide();
            });

            $("input[name='selKey']").change(function() {
                var isZxist = false;
                var position=-1;
                var hidKeyTagName = $("#hidKeyTagName").val();
                var hidKeyTagId = $("#hidKeyTagId").val();
                var hidKeyTagIdList = new Array();
                hidKeyTagIdList = hidKeyTagId.split(',');
                var hidKeyTagNameList = new Array();
                hidKeyTagNameList = hidKeyTagName.split(' ');
                for (var i in hidKeyTagIdList) {
                    if ($(this).attr("id") == hidKeyTagIdList[i]) {
                        position=i;
                        isZxist = true;
                        break;
                    }
                }
                if ($(this).attr("checked") == false && isZxist) {
                    hidKeyTagIdList.splice(position, 1);
                    hidKeyTagNameList.splice(position, 1);
                } else if($(this).attr("checked") == true && !isZxist) {
                    hidKeyTagIdList.push($(this).attr("id"));
                    hidKeyTagNameList.push($(this).next("label").eq(0).text());
                }
                $("#hidKeyTagName").val(hidKeyTagNameList.join(" "));
                $("#hidKeyTagId").val(hidKeyTagIdList.join(","));

            });

            //回车查询
            $("#txtSearch").keydown(function(event) {
                if (event.keyCode == 13) {
                    SelectKey.AjaxPost();
                    return false;
                }
            });

            $("#btnSearch").click(function() {
                SelectKey.AjaxPost();
            });
        });
    </script>

</body>
</html>
