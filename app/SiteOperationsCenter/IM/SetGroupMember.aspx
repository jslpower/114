<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGroupMember.aspx.cs" Inherits="SiteOperationsCenter.IM.SetGroupMember" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置MQ群人数上限</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("json2.js") %>"></script>
    <script type="text/javascript">
        var groupMember = {
            Set: function() {
                var params = { type: 1, txtGroupId: $.trim($("#txtGroupId").val()), txtMaxMember: $.trim($("#txtMaxMember").val()) };
                $.ajax({
                    type: "POST",
                    url: "SetGroupMember.aspx?isajax=yes",
                    data: params,
                    success: function(responseText) {
                        if (responseText == "notLogin") {
                            alert("对不起，当前未登录，请先登录!");
                            return;
                        }

                        var data = JSON.parse(responseText);
                        if (data.isSuccess) {
                            alert(data.msg);
                            window.location.href = window.location.href;
                            return;
                        }

                        if (!data.isSuccess) {
                            alert(data.msg);
                            return;
                        }
                    }
                });
            },
            Get: function() {
                var params = { type: 2, txtGroupId: $.trim($("#txtGroupId").val()) };
                $.ajax({
                    type: "POST",
                    url: "SetGroupMember.aspx?isajax=yes",
                    data: params,
                    success: function(responseText) {
                        if (responseText == "notLogin") {
                            alert("对不起，当前未登录，请先登录!");
                            return;
                        }

                        var data = JSON.parse(responseText);
                        if (data.isSuccess) {
                            $("#txtMaxMember").val(data.msg);
                            return;
                        }

                        if (!data.isSuccess) {
                            alert(data.msg);
                            return;
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#btnSet").bind("click", groupMember.Set);
            $("#btnGet").bind("click", groupMember.Get);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="line-height:30px;">
        群号：<input type="text" name="txtGroupId" id="txtGroupId" maxlength="8" style="width: 100px;" />
        人数：<input type="text" name="txtMaxMember" id="txtMaxMember" maxlength="3" style="width: 80px;" />
        <input type="button" id="btnSet" value="设置人数上限" />
        <input type="button" id="btnGet" value="获取人数上限" />
    </div>
    </form>
</body>
</html>
