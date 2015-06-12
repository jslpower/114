<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logout.aspx.cs" Inherits="WEB.sso.logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language=javascript src="/JS/ssologin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <script language=javascript>
    
   window.onload=function(){    
    var sso = new EyouSoftSSO();
    sso.addLogin([<%="\"http://www.lifepop.com/sso/sso.ashx?logout=\",\"http://www.1111.com/sso/sso.ashx?logout=\",\"http://www.ty.com/sso/sso.ashx?logout=\"" %>],"<%=Server.UrlDecode(Request["RedirectUrl"]) %>");
    sso.customLogin(); 
    }
            
    </script>
    </div>
    </form>
</body>
</html>
