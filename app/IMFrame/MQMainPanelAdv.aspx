<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MQMainPanelAdv.aspx.cs" Inherits="TourUnion.WEB.IM.MQMainPanelAdv" %>
<%@ OutputCache Duration="86400" VaryByParam="loginuid;version;chat" Location="Client" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<base target="_blank" />
<script language="javascript" src="<%=JsManage.GetJsFilePath("lanrenxixi") %>"></script>
<script language="javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>
<style>
body {margin:0px; font: 75% Tahoma;line-height:150%;}
ul, ol, li { list-style-type: none; margin:0; padding:0; }
h3 {margin:0;font-size: 130%;line-height: 150%;}
p {margin: 0 0 10px;}
img {border: none;vertical-align: middle;}
a {color: #1A7AAE;text-decoration: none;}
a:hover {text-decoration: underline;}
.index-splash-block {width: <%=flashWidth%>px;height: <%=flashHeight%>px;text-align:left;}
.index-splash-block .feature-slide-preview {margin: 0 auto; padding-top:0px; display: none; width: <%=flashWidth%>px; height: <%=flashHeight%>px; overflow: hidden;}
.index-splash-block .feature-slide-preview .screenshot {display: block;margin: auto;}
.index-splash-block .feature-slide-list {height: 12px;overflow: hidden;margin: 0px auto ;bottom:13px;position:relative;float:right}
.index-splash-block .feature-slide-list a {float: left;display: inline;width: 12px;height: 12px;background: url(<%=imageServerUrl %>/im/images/dian-kong.png) center no-repeat;_background: url(<%=imageServerUrl %>/im/images/dian-kong.gif) center no-repeat;}
.index-splash-block .feature-slide-list a.current {background: url(<%=imageServerUrl %>/im/images/dian-shi.png) center no-repeat;_background: url(<%=imageServerUrl %>/im/images/dian-shi.gif) center no-repeat;}
.index-splash-block  .feature-slide-list .feature-slide-list-items {display: inline;}
.index-splash-block  .feature-slide-list a.feature-slide-list-previous {background: url(<%=imageServerUrl %>/im/images/jiantou-l.png);_background: url(<%=imageServerUrl %>/im/images/jiantou-l.gif);
}
.index-splash-block  .feature-slide-list a.feature-slide-list-next {background: url(<%=imageServerUrl %>/im/images/jiantou-r.png);  _background: url(<%=imageServerUrl %>/im/images/jiantou-r.gif);}

.gq-title h2,.gq-title a,.gq-content ul li s,.ud_nav_today a{ background:url(<%=imageServerUrl %>/im/images/main.gif) no-repeat -999em 0;}
.gq-content{ position:relative;top:0px;*top:0px; height:53px;*width:<%=flashWidth%>px; *height:<%=flashHeight%>px; _width:<%=flashWidth%>px; _height:<%=flashHeight%>px; border:1px solid #CCCCCC; background:url(<%=imageServerUrl %>/im/images/gq-bg.gif) repeat-x;}
#nav_today{height:40px; min-height:25px;overflow:hidden; position:relative; top:5px; left:5px;}
#nav_today ul{padding:0px; float:left;}
#nav_today li{ height:21px;}
.gq-content ul li{position:relative; line-height:20px; text-indent:20px;}
.gq-content ul li s{ width:15px; height:16px; display:inline-block; position:absolute; top:1px; _top:2px; left:0px;}
.gq-content ul li.gong s{ background-position:0 0px;}
.gq-content ul li.qiu s{ background-position:-19px 0px;}
.gq-content ul li a:hover{ color:#CF3200; text-decoration:underline;}
</style>
<script type="text/javascript">
    (function($) {
        $.fn.extend({
            Scroll: function(opt, callback) {
                //参数初始化
                if (!opt) var opt = {};


                var timerID;
                var _this = this.eq(0).find("ul:first");
                var lineH = _this.find("li:first").height(), //获取行高
                        line = opt.line ? parseInt(opt.line, 10) : parseInt(this.height() / lineH, 10), //每次滚动的行数，默认为一屏，即父容器高度
                        speed = opt.speed ? parseInt(opt.speed, 10) : 900; //卷动速度，数值越大，速度越慢（毫秒）
                timer = opt.timer //?parseInt(opt.timer,10):3000; //滚动的时间间隔（毫秒）
                if (line == 0) line = 1;
                var upHeight = 0 - line * lineH;
                //滚动函数
                var scrollUp = function() {

                    _this.animate({
                        marginTop: upHeight
                    }, speed, function() {
                        for (i = 1; i <= line; i++) {
                            _this.find("li:first").appendTo(_this);
                        }
                        _this.css({ marginTop: 0 });

                    });
                }

                //Shawphy:自动播放
                var autoPlay = function() {
                    if (timer) timerID = window.setInterval(scrollUp, timer);
                };
                var autoStop = function() {
                    if (timer) window.clearInterval(timerID);
                };
                //鼠标事件绑定
                _this.hover(autoStop, autoPlay).mouseout();
                //_btnUp.css("cursor", "pointer").click(scrollUp).hover(autoStop, autoPlay); //Shawphy:向上向下鼠标事件绑定
                //_btnDown.css("cursor", "pointer").click(scrollDown).hover(autoStop, autoPlay);
            }
        })
    })(jQuery);
    $(document).ready(function() {
        $("#nav_today").Scroll({ line: 2, speed: 900, timer: 3000, up: "btn1", down: "btn2" });
    });
</script>
<title>广告切换</title>
</head>

<body scroll="no" style="border:0px;">

<div id="index-splash-block" class="index-splash-block">
	<div id="feature-slide-block" class="feature-slide-block">
	
		<div class="feature-slide-preview" style="display: none; ">
			 <div class="gq-content fixed">
                <div id="nav_today">
                  <ul>
                   <%=supplysHtml%>
                  </ul>
                </div>
              </div>
		</div>
		
		<div class="feature-slide-preview" style="display: none; ">
			<a href="http://shop.tongye114.com/shop_d5d29c73-02ad-449e-9b2a-8b0402b629c9" class="screenshot"><img alt="" src="http://resource.tongye114.com/im/images/adv_110328.jpg" width="<%=flashWidth%>" height="<%=flashHeight%>" /></a>

		</div>
		<div class="feature-slide-preview" style="display: none; ">
			<a href="/MQTransit.aspx?desurl=http%3a%2f%2fwww.tongye114.com%2fPlaneInfo%2fPlaneListPage.aspx&loginuid=10009&passwd=670b14728ad9902aecba32e22fa4f6bd" class="screenshot"><img alt="" src="http://resource.tongye114.com/im/images/2.jpg" width="<%=flashWidth%>" height="<%=flashHeight%>" /></a>
		</div>
		
	<div id="feature-slide-list" class="feature-slide-list">
	    <a href="#" id="feature-slide-list-previous" class="feature-slide-list-previous"></a>
	   <div id="feature-slide-list-items" class="feature-slide-list-items"></div>
		<a href="#" id="feature-slide-list-next" class="feature-slide-list-next"></a>
	</div>

	
	</div>
	<script type="text/javascript">
	    initFeatureSlide();
	</script>
</div>
</body>
</html>
