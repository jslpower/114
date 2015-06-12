<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="UserBackCenter.Test" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

</head>
<body>
    <input type="button" id="btnTest" value="OK" />
    <input type="radio" name="radio" value="1" />
    <input type="radio" name="radio" value="2" />

    <script type="text/javascript">
        $(function() {
            $("#btnTest").click(function() {
                alert($("input[type='radio']:checked").val());
                //$("input[name='radio']:selected").val();
            })
        })
    </script>

</body>
</html>
