<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMQWeb.aspx.cs" Inherits="TourUnion.WEB.IM.LoginMQWeb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
<script language="javascript" type="text/javascript">
    var url = "<%= url %>";
    var isTrue = "<%= isTrue %>";
    if(isTrue.toLowerCase() == "true")
        window.location.href=url;
    else 
        document.write(url);
</script>
</body>
</html>
