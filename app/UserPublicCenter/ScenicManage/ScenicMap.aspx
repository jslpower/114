<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicMap.aspx.cs" Inherits="UserPublicCenter.ScenicManage.ScenicMap" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置公司经纬度</title>
    <style type="text/css">
        #Set_Map
        {
            width: 700px;
            height: 400px;
        }
    </style>
</head>
<body>
    <form id="form1">
    <div id="Set_Map">
    </div>
    </form>
</body>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

<script type="text/javascript" src="http://ditu.google.com/maps?file=api&amp;v=2&amp;key=undefined"></script>

<script type="text/javascript">
    /**
    * 创建地图控件
    */
    function initialize() {
        if (GBrowserIsCompatible()) {
            map = new GMap2(document.getElementById('Set_Map'));
            // 将视图定位到当前经纬度
            map.setCenter(new GLatLng(<%= Longitude %>, <%= Latitude %>), 13);

            var ico = new GIcon(G_DEFAULT_ICON, 'http://localhost:30001/scenicspots/T1/images/googleMapRed.png');
            var targetPt = new GLatLng(<%= Longitude %>, <%= Latitude %>);
            var marker = new GMarker(targetPt, { icon: new GIcon(ico) });
            if (true)
            {
                marker.openInfoWindow(createInfoWindow());
            }
            // 平移及缩放控件（左上角）、缩略图控件（右下角）
            if(true)
            {
                map.addControl(new GLargeMapControl());
            }
            if(true)
            {
                map.addControl(new GOverviewMapControl());
            }
            //启用鼠标滚轮缩放地图
            map.enableScrollWheelZoom();
            // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
	        GEvent.addListener(marker, 'click', function() { 
			        marker.openInfoWindow(createInfoWindow()); 
	        } );
            map.addOverlay(marker);
            if(13!=0)
                    map.setZoom(13);
        }
    }

    /**
    * 为气泡提示窗口创建 DOM 对象
    */
    function createInfoWindow() {
        // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
        var div = document.createElement('div');
        div.innerHTML = '<div style="font-size: 9pt; width:300px;height:20px"><%= scenicname %></div>';

        // 当用户关闭气泡时 Google Map API 会自动释放该对象  
        return div;
    }
    $(function() {
        initialize();
    });
    
    $(window).bind("unload",function() {GUnload();})
</script>

</html>
