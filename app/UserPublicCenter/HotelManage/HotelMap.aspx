<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelMap.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelMap" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    html,body,form
    {
    	padding:0px;
    	margin:0px;
    }
    body
    {
    	text-align:center;
    	margin:0px auto;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div id="map_canvas" style="width: 580px; height: 380px;">
        loading...</div>
        
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>

    <script type="text/javascript">
        function initialize(title, lat, lng) {
            // var title = "TESTTSTE"
            var map;
            var infowindow = new google.maps.InfoWindow(); //地图内置信息窗口
            var marker;
            // var lat = 30.26; //维度
            // var lng = 120.19; //经度
            var oLatLng = new google.maps.LatLng(lat, lng); //地图经纬度对象

            /*
            地图初始化参数
            */
            var myOptions = {
                zoom: 13,
                center: oLatLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                mapTypeControl: false,
                streetViewControl: false
            }
            var oMapContainer = document.getElementById("map_canvas"); //地图容器
            map = new google.maps.Map(oMapContainer, myOptions);
            //初始化对应经纬度的标记
            marker = new google.maps.Marker({
                position: oLatLng,
                map: map,
                title: title/* 鼠标放上去的提示 */
            });

        }

        $(function() {
        initialize('<%=Server.HtmlDecode(Request.QueryString["title"]) %>', '<%=Request.QueryString["lat"] %>', '<%=Request.QueryString["lng"] %>');
            
        });
    </script>

    <script type="text/javascript">

    </script>

</body>
</html>
