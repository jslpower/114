<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQTransit.aspx.cs" Inherits="IMFrame.MQTransit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("blogin") %>"></script>

    <script type="text/javascript">
        blogin.ssologinurl = "<%=EyouSoft.Common.Domain.PassportCenter %>";
        blogin4("<%=UserName %>", "<%=Pwd %>", "", "<%=DesUrl %>", function(message) {
            $("#loading").find("font").val("跳转失败，请稍后再试");
        });
    </script>

</head>
<body>
    <div id="loading">
        <br />
        <font size="+2">正在跳转...</font><br />
    </div>
</body>
</html>
