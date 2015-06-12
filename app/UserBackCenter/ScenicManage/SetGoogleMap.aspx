﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGoogleMap.aspx.cs" Inherits="UserBackCenter.ScenicManage.SetGoogleMap" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置公司经纬度</title>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script src="http://ditu.google.com/maps?file=api&amp;v=2&amp;key=<%= GoogleMapKey %>"
        type="text/javascript"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("googleMap_UC") %>"></script>

    <script type="text/javascript">
        function closewin() {
            //alert("aaa");
            //window.parent.close();
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
    </script>

</head>
<body onunload="GUnload()">
    <form id="form1" runat="server">
    <div id="Set_Map" style="margin-left: 5px; width: 99%; height: 650px">
    </div>

    <script type="text/javascript">
        //var SceniceName=<%=SceniceName %>;
        function initialize() {
            if (GBrowserIsCompatible()) {
                var map = new GMap2(document.getElementById('Set_Map'));
                map.enableScrollWheelZoom();
                // 给地图添加内置的控件，分别为：
                // 平移及缩放控件（左上角）、比例尺控件（左下角）、缩略图控件（右下角）
                map.addControl(new GLargeMapControl());
                map.addControl(new GScaleControl());
                map.addControl(new GOverviewMapControl());

                /* 添加自定义的控件 2011-11-16 注释
                //map.addControl(new GRulerControl({ SaveF: SavePositionInfo, Domain: '<%= EyouSoft.Common.Domain.ServerComponents %>',BackUrl:'<%= EyouSoft.Common.Domain.UserBackCenter %>/ScenicManage/AddOrUpdateScenice.aspx' }));
                
                //2011-11-16   修改   CancelF:CancelPositionInfo ,,BackUrl:'<%= EyouSoft.Common.Domain.UserBackCenter %>/ScenicManage/AddOrUpdateScenice.aspx' */
                map.addControl(new GRulerControl({ SaveF: SavePositionInfo, Domain: '<%= EyouSoft.Common.Domain.ServerComponents %>'}));

                // 将视图移到初始位置
                map.setCenter(new GLatLng(<%= Latitude %>, <%= Longitude %>), 13);
                var ico = new GIcon(G_DEFAULT_ICON, '<%= EyouSoft.Common.Domain.ServerComponents %>/scenicspots/T1/images/googleMapRed.png');
                var targetPt = new GLatLng(<%= Latitude %>, <%= Longitude %>);
                var marker = new GMarker(targetPt, { icon: new GIcon(ico) });
                //alert("<%=SceniceID %>");
                if("<%=SceniceID %>"!="" ){
                    marker.openInfoWindow(createWindow());
                }
                // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
	            GEvent.addListener(marker, 'click', function() { 
			        marker.openInfoWindow(createInfoWindow()); 
	            } );
                map.addOverlay(marker);
                if(<%= ZoomLevel %>!=0)
                    map.setZoom(<%= ZoomLevel %>);
            }
        }

        function SavePositionInfo(lng, lat,ZoomLevel) {
            window.parent.document.getElementById("X").innerHTML=lng;
            window.parent.document.getElementById("jingdu").value=lng;
            window.parent.document.getElementById("Y").innerHTML=lat;
            window.parent.document.getElementById("weidu").value=lat;
            alert("设置成功！");
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
        
        function CancelPositionInfo(){
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
        /**
        * 为气泡提示窗口创建 DOM 对象
        */
        function createInfoWindow() {
            // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
            var div = document.createElement('div');
            div.style.fontSize = '10.5pt';
	        div.style.width = '250px'; 
            //div.innerHTML = '经度：<%= Longitude %><br />纬度：<%= Latitude %><hr style="border:solid 1px #cccccc" /><a href="IframeSightShop.aspx">结束设置</a>';
            div.innerHTML = '此处为您当前所在地！<hr style="border:solid 1px #cccccc" /><a href="javascript:void(0)" onclick="closewin()">结束设置</a>';

            // 当用户关闭气泡时 Google Map API 会自动释放该对象  
            return div;
        }
        function createWindow() {
            // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
            var div = document.createElement('div');
            div.style.fontSize = '10.5pt';
	        div.style.width = '250px'; 
            div.innerHTML = '此处为您当前所在地！<hr style="border:solid 1px #cccccc" /><%=SceniceName %>';

            // 当用户关闭气泡时 Google Map API 会自动释放该对象  
            return div;
        }
        
        $(function() {
            initialize();
        });
    </script>

    </form>
</body>
</html>