<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRever.aspx.cs" Inherits="SiteOperationsCenter.HotelManagement.AddRever" %>
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加回复</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:20px;">
          <tr>
            <td align="center">【<strong>添加回复</strong>】</td>

          </tr>
        </table>
        <table width="95%" border="0" align="center" cellspacing="1" cellpadding="4" bgcolor="#B7D2F7">
          <tr>
            <td width="40%" height="24" align="right" bgcolor="#F3F7FF"><strong>联系人：</strong></td>
            <td width="60%" height="24" bgcolor="#FFFFFF"><input name="txtLinkPerson" id="txtLinkPerson" runat="server" type="text" readonly="readonly"/></td>
          </tr>
          <tr>
            <td height="24" align="right" bgcolor="#F3F7FF"><strong>时间：</strong></td>

            <td height="24" bgcolor="#FFFFFF"><input name="txtDate" type="text" id="txtDate" runat="server" readonly="readonly" /></td>
          </tr>
          <tr>
            <td height="24" align="right" bgcolor="#F3F7FF"><strong>回复内容：</strong></td>
            <td height="24" bgcolor="#FFFFFF"><textarea name="txaContent" id="txaContent" cols="50" rows="8"></textarea></td>
          </tr>
        </table>

        <table width="40%" border="0" align="center" cellpadding="0" cellspacing="0">

          <tr>
            <td height="50" align="center"><a href="javascript:void(0)" id="hfSave"><img src="<%=ImageServerUrl %>/images/yunying/baocun_btn.jpg" alt="添加" width="79" height="25" /></a>
                        <input type="hidden" id="hdTid" runat="server" />
            </td>
          </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>  
    <script type="text/javascript">
        $(document).ready(function() {
            $("#hfSave").click(function() {
                $.ajax({
                    url: "AddRever.aspx?method=save&content=" + escape($.trim($("#txaContent").val())) + "&tid=" + $("#hdTid").val(),
                    dataType: 'json',
                    type: "get",
                    cache: false,
                    async: false,
                    success: function(result) {
                        alert(result.message);
                        if (result.success == "1") {
                            parent.Boxy.getIframeDialog(parent.Boxy.queryString("iframeId")).hide();
                            //  window.location.href = "GroupOrderSearch.aspx";
                            window.parent.location.reload();
                        }
                    }, error: function() {
                        alert("未能成功获取响应的结果");
                    }
                });

            });
        });
    </script>
</body>
</html>
