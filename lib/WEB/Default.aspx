<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WEB._Default" %>
<%@ Register Src="VaryingDate.ascx" TagName="VaryingDate" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="/JS/ssologin.js"></script>
</head>
<body>
<script>
    
    window.onload = function() {
        
    }
    //try { sinaSSOController.setCrossDomainUrlList({ "retcode": 0, "arrURL": ["http:\/\/passport.51uc.com\/sso\/do_crossdomain.php?action=login&savestate=1276755299"] }); } catch (e) { } try {debugger; sinaSSOController.crossDomainAction('login', function() { location.replace('http://www.ty.com/portfolio/login.php?f_c=stock&savestate=30&u=jslpower&retcode=0'); }); } catch (e) { }



</script>
    <form id="frmLogin" runat=server>
    <a href="~/a/">asdfadf</a>
    <input type=hidden name=RedirectUrl value="<%=Server.UrlEncode(Request.Url.ToString())%>" />
    <input id="btnLogin" runat="server" onserverclick="btnLogin_Click" type="submit"
        value="login" />&nbsp;用户名:<input 
        id="txtUsername" name="txtUsername" type="text" runat=server />
   
    <input type=hidden name=RedirectUrl value="<%=Server.UrlEncode(Request.Url.ToString())%>" />
    <input type=submit value="logout" id="btnLogOut" onserverclick="btnLogOut_Click" runat=server />&nbsp;
    
    <a href="#">sdfsdg</a>
    11
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        style="height: 26px" Text="退出" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="更新缓存" />
    <div>
        <uc1:VaryingDate ID="VaryByControl1" runat="server" />
    
    </div>

    </form>
</body>
</html>
