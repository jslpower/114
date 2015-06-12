<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMapShow.aspx.cs"
    Inherits="UserBackCenter.ScenicManage.GoogleMapShow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head></head>
<body onunload="GUnload()" style="text-align:center;">
    <div id="map_canvas" style="width: 550px; height: 380px"></div>
    <script src='http://ditu.google.com/maps?file=api&amp;v=2&amp;key=<%=this.mapKey%>&hl=zh-CN' type="text/javascript"></script>
    <script type="text/javascript">
           var x=<%=this.x%>;//经度
           var y=<%=this.y%>;//纬度
           var map = new GMap2(document.getElementById("map_canvas"));//创建地图
           map.enableScrollWheelZoom();
           map.setCenter(new GLatLng(y,x), 13);     //区域 深 度
           var point = new GLatLng(y, x);            //坐标
           map.addOverlay(new GMarker(point));                      //增加标点
           // map.openInfoWindow(map.getCenter(),document.createTextNode("Hello, world")); //增加提示框
           var topRight = new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(10,10));//取得右上10*10的中心点
           var bottomRight = new GControlPosition(G_ANCHOR_BOTTOM_RIGHT, new GSize(10,10));//取得右下10*10的中心点
           map.addControl(new GSmallMapControl(), topRight);       //增加控件 左上角放大缩小工具
           var smallMapControl = new GOverviewMapControl();        //创建右下缩略图
           map.addControl(smallMapControl, topRight);              //增加控件
    </script>
</body>
</html>