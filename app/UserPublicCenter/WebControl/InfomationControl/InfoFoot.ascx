<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoFoot.ascx.cs" Inherits="UserPublicCenter.WebControl.InfomationControl.InfoFoot" %>
<div class="list-btm">
    <!--列表 start-->
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%=GetMoreUrl(4) %>" target="_blank">更多></a></span>旅游交通</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=TourTraffic %>
                </ul>
            </div>
        </div>
    </div>
    <!--列表 end-->
    <!--列表 start-->
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%=GetMoreUrl(3)%>" target="_blank">更多></a></span>酒店住宿</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=HotelLive %>
                </ul>
            </div>
        </div>
    </div>
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%=GetMoreUrl(10)%>" target="_blank">更多></a></span>旅游攻略</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=TourGonglv %>
                </ul>
            </div>
        </div>
    </div>
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%=GetMoreUrl(7)%>" target="_blank">更多></a></span>旅游展会</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=TourZhanHui%>
                </ul>
            </div>
        </div>
    </div>
    <!--列表 end-->
</div>
