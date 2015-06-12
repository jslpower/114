<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostToMiddlewareDemo.aspx.cs" Inherits="WEB.PostToMiddlewareDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发送数据到中间处理程序DEMO</title>
</head>
<body>
    <!-- post action:http://192.168.1.254:30009/middleware.ashx -->
    <form id="form1" runat="server" method="post" action="middleware.ashx">
    <div>
        <textarea id="request" cols="100" rows="20" runat="server" name="request"></textarea>
        <br />
        <input id="btnSubmit" type="submit" value="提交" />
    </div>
    </form>
</body>
</html>
