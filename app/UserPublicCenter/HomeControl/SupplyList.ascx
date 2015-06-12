<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplyList.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.SupplyList" %>
<div class="indextabtop">
    <span>供求信息</span> <a target="_blank" href="<%=EyouSoft.Common.Domain.UserBackCenter %>/Default.aspx#page%3A%2Fsupplyinformation%2Faddsupplyinfo.aspx"
        class="redh">发布我的供求</a> <a href="<%=EyouSoft.Common.URLREWRITE.SupplierInfo.InfoDefaultUrlWrite(362) %>" target="_blank" class="all">全部</a></div>
<%= strAllSupplyList %>
<div class="clear">
</div>
