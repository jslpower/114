<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLineRight.ascx.cs" Inherits="UserPublicCenter.WebControl.UCLineRight" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>


<div id="news-list-bar">
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add add-icon">
                        <span><a href="<%= EyouSoft.Common.Domain.UserPublicCenter%>/TourManage/TourList.aspx" target="_blank">更多></a></span><%=HotCityName %>最新旅游线路</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list list-ten">
                    <%=NewestRouteList %>
                    
                </ul>
            </div>
        </div>
    </div>
    <div class="hr-10">
    </div>
    <a href="http://im.tongye114.com">
        <img src="<%=ImageServerPath %>/Images/new2011/news-list_19.gif" alt="#" /></a>
    <div class="hr-10">
    </div>
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        旅游企业</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=HotLists %>
                </ul>
            </div>
        </div>
    </div>
    <div class="hr-10">
    </div>
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%=EyouSoft.Common.Domain.UserPublicCenter%>/SupplierInfo/SupplierInfo.aspx" target="_blank">更多></a></span>供求信息</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%=SupplyList%>
                </ul>
            </div>
        </div>
    </div>
</div>

