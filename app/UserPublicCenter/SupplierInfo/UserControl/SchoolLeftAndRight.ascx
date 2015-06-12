<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SchoolLeftAndRight.ascx.cs"
    Inherits="UserPublicCenter.SupplierInfo.UserControl.SchoolLeftAndRight" %>
<%@ Register Src="~/SupplierInfo/UserControl/CommonTopicControl.ascx" TagName="CommonTopicControl"
    TagPrefix="uc1" %>
<%@ Register Src="~/SupplierInfo/UserControl/PopularityCompanyAdv.ascx" TagName="PopularityCompanyAdv"
    TagPrefix="uc4" %>
    
<%--学堂介绍Start--%>
<uc1:CommonTopicControl runat="server" ID="CommonTopicControl1" PartCss="hangleft"
    TextCss="hanglk" TopNumber="1" PartText="学堂介绍" />
<%--学堂介绍End--%>
<%--本周最具人气企业推荐Start--%>
<uc4:PopularityCompanyAdv runat="server" ID="PopularityCompanyAdv1" />
<%--本周最具人气企业推荐End--%>
<%--同业之星访谈Start--%>
<uc1:CommonTopicControl runat="server" ID="CommonTopicControl2" PartText="同业之星访谈"
    PartCss="hangleft" TextCss="hanglk" TopNumber="1" />
<%--同业之星访谈End--%>
<%--新闻资讯Start--%>
<div class="maintop10">
</div>
<uc1:CommonTopicControl runat="server" ID="CommonTopicControl3" PartText="新闻资讯" PartCss="hangleft"
    TextCss="hanglk" TopNumber="6" />
<%--新闻资讯End--%>
<%--行业动态Start--%>
<div class="maintop10">
</div>
<uc1:CommonTopicControl runat="server" ID="CommonTopicControl4" PartText="行业动态" PartCss="hangleft"
    TextCss="hanglk" TopNumber="13" />
<%--行业动态End--%>
