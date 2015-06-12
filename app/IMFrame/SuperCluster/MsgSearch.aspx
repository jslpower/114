<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MsgSearch.aspx.cs" Inherits="IMFrame.SuperCluster.MsgSearch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        html, div, p, ul, li
        {
            margin: 0;
            padding: 0;
        }
        body
        {
            font-family: "宋体";
            font-size: 12px;
            background: #ffffff;
            margin: 3px;
            padding: 0px;
        }
        img
        {
            border: 0 none;
            vertical-align: top;
        }
        ul, li
        {
            list-style-type: none;
        }
        .mainbox
        {
            width: 100%;
            margin: 5px auto;
        }
        .mainbox ul
        {
            margin: 0 auto;
            overflow-y: auto;
        }
        .mainbox ul li
        {
            line-height: 200%;
        }
        .mainbox ul li.mq-name
        {
            color: #0054ff;
        }
        .mainbox ul li.mq-Record
        {
            color: #0604b5;
            padding-top: 5px;
            line-height: 150%;
        }
        .mainbox ul li.mq-Record a
        {
            text-decoration: none;
            font-size: 12px;
            font-weight: normal;
            color: #0054ff;
        }
        .mainbox ul li.Record-time
        {
            font-weight: bold;
            color: #1b6cbf;
            border-bottom: 1px #72c0f5 solid;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainbox">
        <%= InitRecord() %>
    </div>

    <script type="text/javascript">
        //hideid要隐藏的元素ID，showeid要显示的元素ID
        function show(hideid, showeid) {
            //            if (sign == '1') {
            //                that.parentNode.style.display = "none";
            //                that.parentNode.nextSibling.style.display = "block";
            //            } else if (sign == '2') {
            //            var aaa = that.parentNode;
            //            alert(aaa.previousSibling.innerHTML);
            //                that.parentNode.style.display = "none";
            //                that.parentNode.previousSibling.style.display = "block";
            //            }
            document.getElementById(hideid).style.display = "none";
            document.getElementById(showeid).style.display = "inline";
            return false;
        }
        //所有图片自适应窗口大小
        window.onload = function() {
            var that = document.getElementsByTagName("img");
            for (var i = 0; i < that.length; i++) {
                //初始化图片的源宽度。
                that[i].setAttribute("sourcewidth", that[i].width);
            }
            //为了解决无法获得隐藏P标签的图片宽度。
            var Pelement = document.getElementsByTagName("p");
            for (var i = 0; i < Pelement.length; i++) {

                if (Pelement[i].getAttribute("sign") != null) {
                    Pelement[i].style.display = "none";
                }
            }
            window.onresize();
        }
        window.onresize = function() {
            //当前窗口的可见区域宽度,因为包括了margin，所以减掉。
            var width = document.documentElement.clientWidth - 6;
            //得到当前文档的所有img
            var that = document.getElementsByTagName("img");
            //递代所有的img
            for (var i = 0; i < that.length; i++) {
                //图片的源宽度
                var sourcewidth = that[i].getAttribute("sourcewidth");
                //如果当前窗口大小，小于图片源宽度，则图片宽度为当前窗口宽度。
                if (width < sourcewidth) {
                    that[i].width = width - 3;
                }
                //如果当前窗口大小，大于图片源宽度，则图片宽度为源宽度。
                else {
                    that[i].width = sourcewidth;
                }
            }
        }
    </script>

    </form>
</body>
</html>
