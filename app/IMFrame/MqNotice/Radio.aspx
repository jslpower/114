<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Radio.aspx.cs" Inherits="IMFrame.MqNotice.Radio" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<html>
<head runat="server">
    <title>广播</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            border: 0px;
            overflow: hidden;
        }
        A:visited
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:active
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:link
        {
            color: rgb(1,0,9);
            text-decoration: none;
        }
        A:hover
        {
            color: rgb(255,2,4);
        }
    </style>

    <script type="text/javascript">
        var Marquee = function(id) {
            try { document.execCommand("BackgroundImageCache", false, true); } catch (e) { };
            var container = document.getElementById(id),
original = container.getElementsByTagName("dt")[0],
clone = container.getElementsByTagName("dd")[0],
speed = arguments[1] || 15;
            clone.innerHTML = original.innerHTML;
            container.scrollLeft = clone.offsetLeft
            var rolling = function() {
                if (container.scrollLeft == 0) {
                    container.scrollLeft = clone.offsetLeft;
                } else {
                    container.scrollLeft--;
                }
            }
            var timer = setInterval(rolling, speed)
            container.onmouseover = function() { clearInterval(timer) }
            container.onmouseout = function() { timer = setInterval(rolling, speed) }
        }
        window.onload = function() {
            var dtHtml = document.getElementsByTagName("dt")[0].innerHTML;
            if (dtHtml != '<span style=\"color: rgb(1,0,9);\">暂无广播信息！</span>' & dtHtml.split("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").length > 2) {
                Marquee("marquee");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="font-size: 12px; border: 0px; margin: 0px; height: 16px" cellpadding="0"
            cellspacing="0">
            <tr>
                <td>
                    <span id="marquee" style="display: block; position: relative; overflow: hidden; width: 100%;">
                        <dl style="width: 1000%; margin: 0; padding: 0">
                            <dt style="margin: 0; padding: 0; float: left;">
                                <%=Html%>
                            </dt>
                            <dd style="margin: 0; padding: 0; float: left;">
                            </dd>
                        </dl>
                    </span>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
