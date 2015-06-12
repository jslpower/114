<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InfoRight.ascx.cs" Inherits="UserPublicCenter.WebControl.InfomationControl.InfoRight" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>
<!--右边 开始-->
<div id="news-list-bar">
    <!--列表 start-->
    <div class="box">
        <div class="box-l">
            <div class="box-r">
                <div class="box-c">
                    <h3 class="add">
                        <span><a href="<%= EyouSoft.Common.Domain.UserPublicCenter%>/RouteManage/Default.aspx" target="_blank">更多></a></span>最新旅游线路</h3>
                </div>
            </div>
        </div>
        <div class="box-main">
            <div class="box-content">
                <ul class="list">
                    <%-- <li><a href="#" title="#">武大第一朵樱花绽放赴日游退</a></li>--%>
                    <%=NewestRouteList %>
                </ul>
            </div>
        </div>
    </div>
    <!--列表 end-->
    <div class="hr-10">
    </div>
    <a href="http://im.tongye114.com">
        <img src="<%=ImageServerPath %>/Images/News/news-list_19.gif" alt="#" /></a>
    <div class="hr-10">
    </div>
    <!--列表 start-->
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
                    <%=TourCompanyList %>
                </ul>
            </div>
        </div>
    </div>
    <!--列表 end-->
    <div class="hr-10">
    </div>
    <!--列表 start-->
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
    <!--列表 end-->
</div>
<!--右边 结束-->
