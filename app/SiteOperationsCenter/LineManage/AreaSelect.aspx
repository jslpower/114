<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaSelect.aspx.cs" Inherits="SiteOperationsCenter.LineManage.AreaSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组团社查找</title>
    <link href="<%=EyouSoft.Common.CssManage.GetCssFilePath("main") %>" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <style>
        body
        {
            width: 730px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#E1E6F1"
        style="padding-top: 20px;">
        <tr>
            <td>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr style="text-align: center;">
                        <asp:Repeater runat="server" ID="rptQueryTour">
                            <ItemTemplate>
                                <td width="33%" style="background-color: #EBF4FF; text-align: left; font-size: 12px;
                                    word-break: break-all; border: 1px solid #4F9DE3;">
                                    <strong style="color: #A94711">
                                        <input name="areaID" id="areaID<%#Eval("AreaId") %>" type="radio" value='<%#Eval("AreaId") %>'>
                                        <label for="areaID<%#Eval("AreaId") %>">
                                            <%#Eval("AreaName")%></label></strong>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,count,3) %>
                            </ItemTemplate>
                        </asp:Repeater>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" height="40" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            &nbsp;<input type="button" name="btnSelect" value="选择" id="btnSelect" class="renyuan_an" />
                            &nbsp;&nbsp;<input name="button" type="button" class="renyuan_an" value="关　闭" onclick="closeWin()"
                                id="Button1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function closeWin() {
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }

        $(function() {
            $("#btnSelect").click(function() {
                var backCallFun = '<%=Request.QueryString["backCallFun"] %>';
                var data = { id: "", name: "" };
                if ($("input[name='areaID']:checked").length == 0) {
                    alert("请选择一项!");
                } else {
                    data.id = $("input[name='areaID']:checked").val();
                    data.name = $.trim($("input[name='areaID']:checked").next().html());
                }
                parent.window[backCallFun](data, "1");
                closeWin();
            })
        })        
    </script>

    </form>
</body>
</html>
