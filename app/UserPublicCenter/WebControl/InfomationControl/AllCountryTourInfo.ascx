<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllCountryTourInfo.ascx.cs"
    Inherits="UserPublicCenter.WebControl.InfomationControl.AllCountryTourInfo" %>
<div class="box">
    <div class="box-l">
        <div class="box-r">
            <div class="box-c">
                <h3 class="add">
                    全国旅游资讯</h3>
            </div>
        </div>
    </div>
    <div class="box-main">
        <div class="box-content box-content-a">
            <%=ShowQuanGuoHtml%>
            <%--全国的区域编号--%>
            <%= AllCounryInfoList%>
            <%=EmptyContent %>
        </div>
    </div>
</div>
