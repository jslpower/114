<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetMapXY.aspx.cs" Inherits="UserBackCenter.SystemSet.GetMapXY" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("jquery") %>"></script>
    <script src='http://ditu.google.com/maps?file=api&amp;v=2&amp;key=<%=this.mapKey%>&hl=zh-CN' type="text/javascript"></script>
    <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("googleMap_UC") %>"></script>
</head>
<body>
    <div id="Set_Map" style="width: 550px; height: 380px"></div>
   <script type="text/javascript">
       var GetMapXY = {
           /*地图开始*/
           initialize: function() {
               var mapDiv = document.getElementById("Set_Map");
               if (GBrowserIsCompatible()) {
                   var x = <%=this.x%>, y = <%=this.y%>;
                   var map = new GMap2(mapDiv);
                   map.enableScrollWheelZoom();
                   // 给地图添加内置的控件，分别为：
                   // 平移及缩放控件（左上角）、比例尺控件（左下角）、缩略图控件（右下角）
                   map.addControl(new GLargeMapControl());
                   //map.addControl(new GScaleControl());
                   map.addControl(new GOverviewMapControl());
                   // 添加自定义的控件
                   map.addControl(new GRulerControl({ SaveF: GetMapXY.SavePositionInfo, Domain: '<%= EyouSoft.Common.Domain.ServerComponents %>',BackUrl:'<%= EyouSoft.Common.Domain.UserBackCenter %>/SystemSet/CompanyInfoSet.aspx' }));
                   // 将视图移到初始位置
                   map.setCenter(new GLatLng(y, x), 13);
                   var ico = new GIcon(G_DEFAULT_ICON, '<%= EyouSoft.Common.Domain.ServerComponents %>/scenicspots/T1/images/googleMapRed.png');
                   var targetPt = new GLatLng(y, x);
                   var marker = new GMarker(targetPt, { icon: new GIcon(ico) });
                   marker.openInfoWindow(GetMapXY.createInfoWindow());
                   // 为标注添加事件处理函数：单击标注时要显示气泡窗口 
                   GEvent.addListener(marker, 'click', function() {
                       marker.openInfoWindow(GetMapXY.createInfoWindow());
                   });
                   map.addOverlay(marker);
               }
           },
           createInfoWindow: function() {
               // 为气泡提示窗口创建动态 DOM 对象，这里我们用 DIV 标签
               var div = document.createElement('div');
               div.style.fontSize = '10.5pt';
               div.style.width = '250px';
               div.innerHTML = '<a href="javascript:GetMapXY.close();">结束设置</a>';
               // 当用户关闭气泡时 Google Map API 会自动释放该对象  
               return div;
           },
           SavePositionInfo: function(lng, lat, ZoomLevel) {
               parent.window["<%=this.callBackFunction %>"](lng, lat);
               GetMapXY.close();
           },
           close:function(){
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
           }
           /*地图结束*/
       }
       $(function() {
           GetMapXY.initialize();
       });
   </script>
</body>
</html>
