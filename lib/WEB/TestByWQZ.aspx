<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestByWQZ.aspx.cs" Inherits="WEB.TestByWQZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WangQZ Test</title>
    <script type="text/javascript" src="js/jquery.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("div").data("A", [1, 2, 3]);
            //alert($("div").data("A"));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>
