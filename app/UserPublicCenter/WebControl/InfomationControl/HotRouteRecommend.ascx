<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotRouteRecommend.ascx.cs"
    Inherits="UserPublicCenter.WebControl.InfomationControl.HotRouteRecommend" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<style>
.btmContent{ _margin-top:0px;}
div.btmMain{ height:auto}
ul.hotLine li{ margin-bottom:5px; padding-bottom:5px; border-bottom:1px dashed #CCC; line-height:22px;}
ul.hotLine li b{ font-size:14px;}
.friendlink{ margin-top:-30px;*+margin-top:0!important;_margin-top:-20px;}
</style>
<div class="btmMain">
    <div class="btmNav">
        <div class="btmNavR">
            <div class="btmNavC">
                <h3>
                    热门线路区域推荐</h3>
                <p>
                    <a href="<%=Tour.GetXianLuUrl(CityId) %>">线路</a> ┊ <a href="<%=Plane.PlaneDefaultUrl(CityId) %>">
                        机票</a> ┊ <a href="<%=Hotel.GetHotelBannerUrl(CityId) %>">酒店</a> ┊
                    <a href="<%=ScenicSpot.ScenicDefalutUrl(CityId) %>">景区</a>
                </p>
            </div>
        </div>
    </div>
    <div class="btmContent">
        <ul class="hotLine">
            <%=HotRouteRecList%>
        </ul>
        <script type="text/javascript" language="javascript">
            $("ul.hotLine li:last").css({ "margin-bottom": 0 }, { "padding-bottom": 0 }, { "border-bottom": 0 });
        </script>
    </div>
</div>
