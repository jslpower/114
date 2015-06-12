<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="meituapp.aspx.cs" Inherits="WEB.meituapp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>meitu app</title>
    <script src="/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {            
            init();
        });

        function init() {
            $("#btnTry").bind("click", trySubmit);
            $("form").bind("submit", function() { return false; })
            
            var objtxt = $("#txtName");
            objtxt.focus(function() {
                if ($.trim(objtxt.val()) == "请输入你的名字") {
                    objtxt.val("");
                    objtxt.css({ "color": "#333333" });
                }
            });
            objtxt.blur(function() {
                if ($.trim(objtxt.val()) == "") {
                    objtxt.val("请输入你的名字");
                    objtxt.css({ "color": "#A78C6E" });
                }
            });
            if (objtxt.val() != "请输入你的名字") {
                objtxt.css({ "color": "#333333" });
            }
            if (objtxt.val() == "") {
                objtxt.val("请输入你的名字");
                objtxt.css({ "color": "#A78C6E" });
            }
        }

        function trySubmit() {
            var name = $.trim($("#txtName").val());

            if (name.length < 1 || name == "请输入你的名字") {
                alert("请输入您的名字");
                return false;
            }
            
            var regzhcn = /^[\u4e00-\u9fa5]+$/;
            var regen = /^[A-Za-z]+$/;

            if (regzhcn.test(name)) {
                if (name.length > 5) {
                    alert('请输入的小于5个字的中文名字');
                    return false;
                }
            }
            else if (regen.test(name)) {
                if (name.length > 10) {
                    alert('请输入的小于10字的英文名字');
                    return false;
                }
            } else {
                if (name.length > 5) {
                    alert("请输入小于5个字的名字");
                    return false;
                }
            }

            $("#pic").attr("src", "pic_character.gif");

            var cacheData = $("div").data("name_" + name);
            if (cacheData != undefined && cacheData != 'undefined') {
                $("#pic").attr("src", cacheData.url);
                return false;
            }

            $.ajax({
                url: "meituapp001.ashx",
                data: { "t": 1, "name": name },
                cache: false,
                type: "post",
                dataType: "json",
                success: function(data) {
                    if (data.success) {
                        $("#pic").attr("src", data.url);
                        $("div").data("name_" + name, data);
                    } else {
                        alert(data.msg);
                    };
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="text" id="txtName" style="color:#A78C6E" onkeydown="if(event.keyCode==13) {trySubmit();}" />
    <input type="button" value="try" id="btnTry" />
    <br />
    <img src="pic_character.gif" alt="" id="pic" />
    </div>
    </form>
</body>
</html>
